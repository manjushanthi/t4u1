using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using SolvencyII.Data.CRT.ETL.DataConnectors;
using System.Globalization;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers.Loading
{
    /// <summary>
    /// Quick query constructor
    /// </summary>
    public class QuickQueryConstructor : IQueryConstructor
    {
        Dictionary<string, string> insertsQueries = new Dictionary<string, string>();
        Dictionary<string, string> updateQueries = new Dictionary<string, string>();
        IDataConnector dataConnector;

        string pkIdParamName = "@PkId";

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickQueryConstructor"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        public QuickQueryConstructor(IDataConnector dataConnector)
        {
            this.dataConnector = dataConnector;
        }

        /// <summary>
        /// Constructs the update query.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        public IDbCommand constructUpdateQuery(CrtRowIdentification ri, IEnumerable<CrtRow> inserts)
        {
            IDbCommand command;

            StringBuilder sb = new StringBuilder(ri.TABLE_NAME);
            foreach (string item in (from i in inserts from cv in i.rcColumnsValues select cv.Key).Distinct())
                sb.Append(item);
            string cmdId = sb.ToString();            
            sb = null;

            string cmdTxt = "";
            if (updateQueries.TryGetValue(cmdId, out cmdTxt))
                command = createCommand(cmdTxt, ri, inserts);           
            else
            {
                cmdTxt = createUpdateCommandText(ri, inserts);
                updateQueries.Add(cmdId, cmdTxt);
                command = createCommand(cmdTxt, ri, inserts);
            }

            IDbDataParameter param = command.CreateParameter();
            param.Value = ri.PK_ID;
            param.ParameterName = pkIdParamName;
            command.Parameters.Add(param);
            return command;
        }

        /// <summary>
        /// Creates the update command text.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        private string createUpdateCommandText(CrtRowIdentification ri, IEnumerable<CrtRow> inserts)
        {
            bool first = true;

            StringBuilder query = new StringBuilder();
            query.Append("UPDATE [T__");
            query.Append(ri.TABLE_NAME);
            query.Append("] SET ");
            foreach (CrtRow insert in inserts)
            {
                foreach (KeyValuePair<string, object> kvp in insert.rcColumnsValues)
                {
                    if (!first)
                        query.Append(", ");
                    else
                        first = false;

                    query.Append(kvp.Key);
                    query.Append("= @");
                    query.Append(kvp.Key);
                    query.Append(" ");
                }
            }
            query.Append(" where PK_ID = ");
            query.Append(pkIdParamName);
            query.Append(";");

            return query.ToString();
        }

        /// <summary>
        /// Constructs the insert query.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        public IDbCommand constructInsertQuery(CrtRowIdentification ri, IEnumerable<CrtRow> inserts)
        {
            StringBuilder sb = new StringBuilder(ri.TABLE_NAME);
            foreach (string item in (from i in inserts from cv in i.rcColumnsValues select cv.Key).Distinct())
                sb.Append(item);

            string cmdId = sb.ToString();
            sb = null;

            string cmdTxt = "";
            if (insertsQueries.TryGetValue(cmdId, out cmdTxt))
                return createCommand(cmdTxt, ri, inserts);

            cmdTxt = createInsertCommandText(ri, inserts);
            insertsQueries.Add(cmdId, cmdTxt);
            return createCommand(cmdTxt, ri, inserts);
        }

        /// <summary>
        /// Creates the insert command text.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        /// <exception cref="EtlException">Unknown type of DB connection</exception>
        private string createInsertCommandText(CrtRowIdentification ri, IEnumerable<CrtRow> inserts)
        {
            if ((from i in inserts from cv in i.rcColumnsValues select cv).Count() > 999)
                return createComplexQuery(ri, inserts);

            StringBuilder query = new StringBuilder();
            query.Append("insert into [T__");
            query.Append(ri.TABLE_NAME);
            query.Append("] (INSTANCE, ");
            query.Append(getColumnNames(ri, inserts));
            query.Append(") ");
            query.Append(" values ( ");
            query.Append(ri.INSTANCE);
            query.Append(" , ");
            query.Append(getColumnParameters(ri, inserts));

            if (this.dataConnector != null && this.dataConnector.DbmsType == DbmsType.SQLite)
                query.Append("); SELECT last_insert_rowid();");
            else if (this.dataConnector != null && this.dataConnector.DbmsType == DbmsType.MSSQL)
                query.Append("SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]");
            else
                throw new EtlException("Unknown type of DB connection");

            return query.ToString();
        }

        /// <summary>
        /// Creates the complex query.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        /// <exception cref="EtlException">Unknown type of DB connection</exception>
        private string createComplexQuery(CrtRowIdentification ri, IEnumerable<CrtRow> inserts)
        {
            IEnumerable<CrtRow> passedInserts = inserts.Take(900);
            List<CrtRow> currentInserts = new List<CrtRow>();
            StringBuilder sb = new StringBuilder(createInsertCommandText(ri, passedInserts));

            foreach (CrtRow insert in inserts.Except(passedInserts))
            {
                currentInserts.Add(insert);

                if(currentInserts.Count%999 == 0)
                {
                    sb.Append(createUpdateCommandText(ri, currentInserts));
                    currentInserts.Clear();
                }
            }

            if (currentInserts.Count > 0)
                sb.Append(createUpdateCommandText(ri, currentInserts));

            string query = null;
            if (this.dataConnector != null & this.dataConnector.DbmsType == DbmsType.SQLite)
                query = sb.ToString().Replace(pkIdParamName, "(SELECT last_insert_rowid())");
            else if (this.dataConnector != null & this.dataConnector.DbmsType == DbmsType.MSSQL)
                query = sb.ToString().Replace(pkIdParamName, "(SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY])");
            else 
                throw new EtlException("Unknown type of DB connection");
            return query;
        }



        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="cmdTxt">The command text.</param>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        private IDbCommand createCommand(string cmdTxt, CrtRowIdentification ri, IEnumerable<CrtRow> inserts)
        {
            IDbCommand command = null;
            command = dataConnector.createCommand();
            command.CommandText = cmdTxt;

            IDbDataParameter param;
            foreach (DictionaryEntry item in ri.contextColumnsValues)
            {
                param = command.CreateParameter();
                param.ParameterName = "@" + item.Key;
                if (item.Value is DateTime)
                    param.Value = ((DateTime)item.Value).ToString("yyyy-MM-dd");
                else
                    param.Value = item.Value;

                command.Parameters.Add(param);
                //command.Parameters.AddWithValue("@" + item.Key, item.Value.ToString()); 
            }

            foreach (var item in (from i in inserts from cv in i.rcColumnsValues select cv))
            {
                param = command.CreateParameter();
                param.ParameterName = "@" + item.Key;
                if (item.Value is DateTime)
                    param.Value = ((DateTime)item.Value).ToString("yyyy-MM-dd");
                else if (item.Value is bool)
                    param.Value = item.Value;
                else
                    param.Value = item.Value;

                command.Parameters.Add(param);
                //command.Parameters.AddWithValue("@" + item.Key, item.Value);            
            }

            return command;
        }

        /// <summary>
        /// Gets the column parameters.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        private string getColumnParameters(CrtRowIdentification ri, IEnumerable<CrtRow> inserts)
        {
            StringBuilder columns = new StringBuilder();
            bool first = true;
            foreach (DictionaryEntry item in ri.contextColumnsValues)
            {
                if (!first)
                    columns.Append(" , ");

                first = false;
                columns.Append("@");
                columns.Append(item.Key);
            }

            foreach (var item in (from i in inserts from cv in i.rcColumnsValues select cv))
            {
                if (!first)
                    columns.Append(" , ");

                first = false;
                columns.Append("@");
                columns.Append(item.Key);
            }
            return columns.ToString();
        }

        /// <summary>
        /// Gets the column names.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        private string getColumnNames(CrtRowIdentification ri, IEnumerable<CrtRow> inserts)
        {
            StringBuilder columns = new StringBuilder();
            bool first = true;
            foreach (DictionaryEntry item in ri.contextColumnsValues)
            {
                if (!first)
                    columns.Append(" , ");

                first = false;
                columns.Append(item.Key);
            }

            foreach (var item in (from i in inserts from cv in i.rcColumnsValues select cv))
            {
                if (!first)
                    columns.Append(" , ");

                first = false;
                columns.Append(item.Key);
            }
            return columns.ToString();
        }


        /// <summary>
        /// Gets the insert command.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        public System.Data.IDbCommand getInsertCommand(dFact fact)
        {
            string cmdTxt = @"INSERT INTO dFact (InstanceID, TextValue, NumericValue, DateTimeValue, BooleanValue, DataPointSignature, Unit, Decimals) values 
(@InstanceID, @TextValue, @NumericValue, @DateTimeValue, @BooleanValue, @DataPointSignature, @Unit, @Decimals);";

            IDbCommand command = this.dataConnector.createCommand();
            
            command.CommandText = cmdTxt;
            command.Parameters.Add(createParameter(command, "@InstanceID", fact.instanceId));
            command.Parameters.Add(createParameter(command, "@TextValue", fact.textValue));
            //command.Parameters.Add(createParameter(command, "@NumericValue", fact.numericValue != null ? Convert.ToString(fact.numericValue, dFact.numberFormatInfo):null));
            //command.Parameters.Add(createParameter(command, "@NumericValue", fact.numericValue != null ? ((decimal)fact.numericValue).ToString("0.00") : null));
            command.Parameters.Add(createParameter(command, "@NumericValue", fact.numericValue != null ? ((decimal)fact.numericValue).ToString("G", CultureInfo.InvariantCulture) : null));
            command.Parameters.Add(createParameter(command, "@DateTimeValue", fact.dateTimeValue != null ? ((DateTime)fact.dateTimeValue).ToString("yyyy-MM-dd"):null));
            command.Parameters.Add(createParameter(command, "@BooleanValue", fact.boolValue == true ? "1" : fact.boolValue == null ? null : "0"));
            command.Parameters.Add(createParameter(command, "@DataPointSignature", fact.dataPointSignature));
            command.Parameters.Add(createParameter(command, "@Unit", fact.unit));
            command.Parameters.Add(createParameter(command, "@Decimals", fact.decimals));            

            return command;
        }

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paraValue">The para value.</param>
        /// <returns></returns>
        private IDbDataParameter createParameter(IDbCommand command, string paramName, object paraValue)
        {
            IDbDataParameter param = command.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paraValue;
            return param;
        }

        /// <summary>
        /// Constructs the query for duplication.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        public IDbCommand constructQueryForDuplication(dFact fact)
        {
            return constructQueryForDuplication2(fact);

//            string cmdTxt = @"SELECT TextValue, NumericValue, DateTimeValue, BooleanValue FROM dFact 
//              WHERE 
//                InstanceID = @InstanceID and
//                DataPointSignature = @DataPointSignature
//                and (
//                    ifnull(TextValue, 'xxx') = ifnull(@TextValue, 'xxx') and 
//                    ifnull(NumericValue, 'xxx') = ifnull(@NumericValue, 'xxx') and
//                    ifnull(DateTimeValue, 'xxx') = ifnull(@DateTimeValue, 'xxx') and
//                    ifnull(BooleanValue, 'xxx') = ifnull(@BooleanValue, 'xxx')
//                );";

//            IDbCommand command = this.dataConnector.createCommand();
//            command.CommandText = cmdTxt;

//            command.Parameters.Add(createParameter(command, "@InstanceID", fact.instanceId));
//            command.Parameters.Add(createParameter(command, "@TextValue", fact.textValue));
//            //command.Parameters.Add(createParameter(command, "@NumericValue", fact.numericValue != null ? Convert.ToString(fact.numericValue, dFact.numberFormatInfo) : null));
//            command.Parameters.Add(createParameter(command, "@NumericValue", fact.numericValue != null ? Convert.ToString(fact.numericValue, dFact.numberFormatInfo) : null));
//            command.Parameters.Add(createParameter(command, "@DateTimeValue", fact.dateTimeValue != null ? ((DateTime)fact.dateTimeValue).ToString("yyyy-MM-dd"):null));
//            command.Parameters.Add(createParameter(command, "@BooleanValue", fact.boolValue == true ? "1" : fact.boolValue == null ? null : "0"));
//            command.Parameters.Add(createParameter(command, "@DataPointSignature", fact.dataPointSignature));

//            return command;
        }

        /// <summary>
        /// Constructs the query for duplication2.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        public IDbCommand constructQueryForDuplication2(dFact fact)
        {
            string cmdTxt = @"SELECT TextValue, NumericValue, DateTimeValue, BooleanValue FROM dFact 
              WHERE 
                InstanceID = @InstanceID and
                DataPointSignature = @DataPointSignature
                and ( {0} = @Value
                );";            
            object value = null;

            if (fact.dateTimeValue != null)
            {
                value = ((DateTime)fact.dateTimeValue).ToString("yyyy-MM-dd");
                cmdTxt = string.Format(cmdTxt, "DateTimeValue");
            }                
            else if(fact.boolValue != null)
            {
                value = fact.boolValue == true ? "1" : fact.boolValue == null ? null : "0";
                cmdTxt = string.Format(cmdTxt, "BooleanValue");
            }
            else if (fact.numericValue != null)
            {
                //value = fact.numericValue != null ? Convert.ToString(fact.numericValue, dFact.numberFormatInfo) : null;
                value = fact.numericValue;
                cmdTxt = string.Format(cmdTxt, "NumericValue");
            }
            else
            {
                value = fact.textValue;
                cmdTxt = string.Format(cmdTxt, "TextValue");
            } 

            IDbCommand command = this.dataConnector.createCommand();
            command.CommandText = cmdTxt;
            command.Parameters.Add(createParameter(command, "@Value", value));
            command.Parameters.Add(createParameter(command, "@DataPointSignature", fact.dataPointSignature));
            command.Parameters.Add(createParameter(command, "@InstanceID", fact.instanceId));

            return command;
        }

        /// <summary>
        /// Constructs the query for fact.
        /// </summary>
        /// <param name="InstanceID">The instance identifier.</param>
        /// <param name="dataPointSignature">The data point signature.</param>
        /// <returns></returns>
        public IDbCommand constructQueryForFact(int InstanceID, string dataPointSignature)
        {
            string query = @"select *
from dFact f
where f.InstanceID = @InstanceID
    and f.DataPointSignature = @DPsignature;";

            IDbCommand command = this.dataConnector.createCommand();
            command.CommandText = query;

            command.Parameters.Add(createParameter(command, "@InstanceID", InstanceID));
            command.Parameters.Add(createParameter(command, "@DPsignature", dataPointSignature));

            return command;
        }

        int dpSeq = 0;
        /// <summary>
        /// Getds the message insert command.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="InstanceID">The instance identifier.</param>
        /// <returns></returns>
        public IDbCommand getdMessageInsertCommand(DataPointDuplicationException ex, int InstanceID)
        {
            IDbCommand comm = this.dataConnector.createCommand();
            comm.CommandText = @"insert into dMessage (InstanceID, SequenceInReport, MessageCode, MessageLevel, Value)
values ( @InstanceID, @SequenceInReport, @MessageCode, @MessageLevel, @Value)";

            IDbDataParameter param = comm.CreateParameter();
            param.ParameterName = "@InstanceID";
            param.Value = InstanceID;
            comm.Parameters.Add(param);
            param = comm.CreateParameter();
            param.ParameterName = "@SequenceInReport";
            param.Value = --dpSeq;
            comm.Parameters.Add(param);
            param = comm.CreateParameter();
            param.ParameterName = "@MessageCode";
            param.Value = "DP Duplication";
            comm.Parameters.Add(param);
            param = comm.CreateParameter();
            param.ParameterName = "@MessageLevel";
            param.Value = "error";
            comm.Parameters.Add(param);
            param = comm.CreateParameter();
            param.ParameterName = "@Value";
            param.Value = ex.Message;
            comm.Parameters.Add(param);

            return comm;
        }
    }
}
