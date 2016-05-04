using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.Repositories
{
    public class CrtErrorsRepository : BaseRepository<CrtError>, ICrtErrorRepository
    {
        private IDataConnector connector;
        List<CrtError> errors = new List<CrtError>();
        IDbCommand insertCommand = null;
        private IDbDataParameter instanceIDParam;
        private IDbDataParameter messageLevelParam;
        private IDbDataParameter messageCodeParam;
        private IDbDataParameter valueParam;
        private string InsertSQL = @"insert into dMessage (InstanceID, SequenceInReport, MessageCode, MessageLevel, Value)
values (@InstanceID, @SequenceInReport, @MessageCode, @MessageLevel, @Value)";
        private IDbDataParameter SequenceInReport;
        int sequence = 1;
        
        public CrtErrorsRepository(IDataConnector connector) : base(connector)
        {
            this.connector = connector;
        }

        public IEnumerable<Model.CrtError> All
        {
            get { return errors.AsReadOnly(); }
        }

        public void Add(IEnumerable<Model.CrtError> objectsToAdd)
        {
            errors.AddRange(objectsToAdd);
            if(insertCommand == null)
                createInsertCommand();

            insertErrors(objectsToAdd);
        }

        private void insertErrors(IEnumerable<CrtError> objectsToAdd)
        {
            foreach (CrtError err in objectsToAdd)
            {
                instanceIDParam.Value = err.InstanceID;
                messageCodeParam.Value = "EtlError";
                messageLevelParam.Value = "error";
                valueParam.Value = err.Message;
                SequenceInReport.Value = sequence++;
                connector.executeNonQuery(insertCommand);
            }
        }

        private void createInsertCommand()
        {
            insertCommand = connector.createCommand();
            insertCommand.CommandText = this.InsertSQL;
            createParameters();
        }

        private void createParameters()
        {
            instanceIDParam = connector.CreateParameter(insertCommand, "InstanceID", null);
            SequenceInReport = connector.CreateParameter(insertCommand, "SequenceInReport", null);
            messageCodeParam = connector.CreateParameter(insertCommand, "MessageCode", null);
            messageLevelParam = connector.CreateParameter(insertCommand, "MessageLevel", null);
            valueParam = connector.CreateParameter(insertCommand, "Value", null);
        }
    }
}
