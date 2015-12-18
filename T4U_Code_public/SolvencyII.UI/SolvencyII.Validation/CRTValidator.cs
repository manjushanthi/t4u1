using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using SolvencyII.Data.SQLite;
using SolvencyII.Data.Shared;
using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Validation.Model;
using SolvencyII.Validation.Executor;
using SolvencyII.Validation.Query;
using SolvencyII.Validation.Parser;

namespace SolvencyII.Validation
{
    public class CRTValidator : IValidator
    {

        BackgroundWorker asyncWorker;
        eDataTier tier;
        string connString;
        dInstance instance;
        mModule module;
        IEnumerable<TreeViewData> treeView;
        ProgressChangedEventHandler ValidationProgress;
        DoWorkEventHandler instanceHandler;
        DoWorkEventHandler tableHandler;
        int[] tableIDs;

        IEnumerable<vValidationRule> validationRules;

        HashSet<vIntraTableSQL> intraTableValidationRule;
        HashSet<vValidationRuleSQL> crossTableValidationRule;

        //IDictionary<long, IEnumerable<EvaluationCells>> intraTableEvalCells;
        //IDictionary<long, IEnumerable<EvaluationCells>> crossTableEvalCells;

        IEnumerable<ValidationError> intraTableValidationError;
        IEnumerable<ValidationError> crossTableValidationError;


        public CRTValidator(eDataTier tier, string connectionString, ProgressChangedEventHandler validationProgress, RunWorkerCompletedEventHandler validationComplete)
        {
            this.tier = tier;
            this.connString = connectionString;
            asyncWorker = new BackgroundWorker();
            asyncWorker.WorkerSupportsCancellation = true;
            asyncWorker.WorkerReportsProgress = true;
            asyncWorker.ProgressChanged += validationProgress;
            ValidationProgress = validationProgress;
            asyncWorker.RunWorkerCompleted += validationComplete;

        }

        public void ValidateAsync(long instanceID)
        {
            if (asyncWorker.IsBusy)
                return;

            asyncWorker.DoWork -= instanceHandler;
            asyncWorker.DoWork -= tableHandler;

            instanceHandler = delegate(object sender, DoWorkEventArgs e)
            {
                Validate(instanceID);
            };            

            asyncWorker.DoWork += instanceHandler;

            asyncWorker.RunWorkerAsync();
        }

        public void ValidateAsync(long instanceID, string tableCode)
        {
            if (asyncWorker.IsBusy)
                return;

            asyncWorker.DoWork -= instanceHandler;
            asyncWorker.DoWork -= tableHandler;

            tableHandler = delegate(object sender, DoWorkEventArgs e)
            {
                Validate(instanceID, tableCode);
            };

            asyncWorker.DoWork += tableHandler;

            asyncWorker.RunWorkerAsync();
        }

        public void Initialize(long instanceID)
        {
            IValidationQuery validationQuery = ValidationFactory.GetValidationQuery(tier);
            ISolvencyData dpmConn = null;


            try
            {
                //Open the database connection
                dpmConn = new SQLiteConnection(connString);
                //int count = dpmConn.ExecuteScalar<int>("Select count(*) from mTable");


                //Get the instance from dpm
                GetSQLData sqlData = new GetSQLData(connString);
                instance = sqlData.GetInstanceDetails(instanceID);

                module = sqlData.GetModuleDetails(instance.ModuleID);

                asyncWorker.ReportProgress(0, "Extracting Validation rules");


                treeView = dpmConn.Query<TreeViewData>(string.Format("SELECT distinct i.InstanceID, td.* FROM vwGetTreeData td left outer join dInstance i on (i.ModuleID = td.ModuleID) Where InstanceID = {0} ORDER BY BusinessOrder, TemplateOrder, TemplateOrder2 ", instanceID));

                List<dFilingIndicator> fillingIndicator = dpmConn.Query<dFilingIndicator>(string.Format("select * from dFilingIndicator where instanceid = {0}", instanceID)).ToList();

                /*if(fillingIndicator != null)
                    tableIDs = (from m in treeView
                                from d in fillingIndicator
                                where m.FilingTemplateOrTableID == d.BusinessTemplateID
                                select m.TableID).ToArray<int>();
                else*/
                tableIDs = (from m in treeView
                            where m.ModuleID == instance.ModuleID
                            select m.TableID).ToArray<int>();

                //Validation rules
                validationRules = dpmConn.Query<vValidationRule>("select * from vValidationRule");

                var temp = validationQuery.CrossTableValidationScripts(dpmConn, tableIDs);

                crossTableValidationRule = new HashSet<vValidationRuleSQL>();

                string deactivatedRule = "ERROR(DEACTIVATED)";

                //Do not add deactivated rules
                foreach(vValidationRuleSQL v in temp)
                {
                    if (v.Severity.ToUpper() != deactivatedRule)
                        crossTableValidationRule.Add(v);
                }

                var temp2 = validationQuery.IntraTableValidationScripts(dpmConn, tableIDs);

                intraTableValidationRule = new HashSet<vIntraTableSQL>();

                foreach(vIntraTableSQL v in temp2)
                {
                    EvaluationCells evc = Helper.MapEvaluationCells(v.CELLS).FirstOrDefault();

                    if(evc != null)
                    {
                       var deactivatedRules =  validationRules.Where(t =>t.ValidationRuleID == evc.ValidationRuleId && t.Severity.ToUpper() == deactivatedRule);

                       if (!deactivatedRules.Any())
                           intraTableValidationRule.Add(v);
                    }
                }       


                //Replace @INSTANCE in the validation sql query to instance id
                foreach (vValidationRuleSQL v in crossTableValidationRule)
                {
                    string query = v.SQL.Replace("@INSTANCE", instanceID.ToString());
                    query = query.Replace("\r\n", string.Empty);
                    v.SQL = query;
                }

                foreach (vIntraTableSQL v in intraTableValidationRule)
                {
                    string query = v.SQL.Replace("@INSTANCE", instanceID.ToString());
                    query = query.Replace("\r\n", string.Empty);
                    v.SQL = query;
                }
            }
            catch (SQLiteException se)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("An error occured while executing validation query" + se.Message);

                throw new ValidationException(sb.ToString(), se);
            }
            catch (Exception e)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("An unknown error occured." + e.Message);

                throw new ValidationException(sb.ToString(), e);
            }
            finally
            {
                //Close the database connection
                if (dpmConn != null)
                    dpmConn.Close();
            }
        }

        private void ResetErros()
        {
            intraTableValidationError = null;
            crossTableValidationError = null;
        }

        public void Validate(long instanceID)
        {
            IRuleExecutor dataValidator = ValidationFactory.GetRuleExecutor(tier);
            ISolvencyData dpmConn = null;

            try
            {
                //Open the database connection
                dpmConn = new SQLiteConnection(connString);

                dataValidator.DpmContext = dpmConn;
                dataValidator.ValidationProgress = ValidationProgress;

                ResetErros();

                asyncWorker.ReportProgress(0, "Validating intra tables");

                intraTableValidationError = dataValidator.ValidateIntraTable(validationRules, intraTableValidationRule, null, instance.InstanceID);

                asyncWorker.ReportProgress(0, "Validating cross tables");

                crossTableValidationError = dataValidator.ValidateCrossTable(validationRules, crossTableValidationRule, null, instance.InstanceID);
            }
            catch(SQLiteException se)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("An error occured while executing validation query");
                sb.Append(dataValidator.GetValidationState());
                
                throw new ValidationException(sb.ToString(), se);
            }
            catch(Exception e)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("An unknown error occured.");
                sb.Append(dataValidator.GetValidationState());
                throw new ValidationException(sb.ToString(), e);
            }
            finally
            {
                //Close the database connection
                if (dpmConn != null)
                    dpmConn.Close();
            }
        }

        public void Validate(long instanceID, string tableCode)
        {
            IRuleExecutor dataValidator = ValidationFactory.GetRuleExecutor(tier);
            ISolvencyData dpmConn = null;

            try
            {
                //Open the database connection
                dpmConn = new SQLiteConnection(connString);

                dataValidator.DpmContext = dpmConn;
                dataValidator.ValidationProgress = ValidationProgress;

                ResetErros();

                //Get the table information from table code
                string query = string.Format(@"select * from mTable where tablecode = '{0}'", tableCode);
                mTable table = dpmConn.Query<mTable>(query).FirstOrDefault();

                if (table != null)
                {
                    IEnumerable<vIntraTableSQL> intraTableSql = (from n in intraTableValidationRule
                                                                 where n.TableID == table.TableID
                                                                 select n).ToList();

                    asyncWorker.ReportProgress(0, "Validating intra tables");

                    intraTableValidationError = dataValidator.ValidateIntraTable(validationRules, intraTableSql, null, instance.InstanceID);

                    IEnumerable<vValidationRule> validationScope = dpmConn.Query<vValidationRule>("select * from vValidationRule");
                    IEnumerable<vValidationRuleSQL> selectedCrossTableSql = (from n in crossTableValidationRule
                                                                             from s in validationScope
                                                                                 where n.ValidationRuleID == s.ValidationRuleID &&
                                                                                 s.Scope == tableCode
                                                                                 select n).ToList();

                    asyncWorker.ReportProgress(0, "Validating cross tables");

                    crossTableValidationError = dataValidator.ValidateCrossTable(validationRules, selectedCrossTableSql, null, instance.InstanceID);
                }
            }
            catch (SQLiteException se)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("An error occured while executing validation query");
                sb.Append(dataValidator.GetValidationState());

                throw new ValidationException(sb.ToString(), se);
            }
            catch (Exception e)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("An unknown error occured.");
                sb.Append(dataValidator.GetValidationState());
                throw new ValidationException(sb.ToString(), e);
            }
            finally
            {

                //Close the database connection
                if (dpmConn != null)
                    dpmConn.Close();
            }
        }

        private void ExecuteValidator(ISolvencyData dpmConn, long instanceID, long tableId)
        {

        }

        public IEnumerable<ValidationError> SerializeErrors()
        {
            IList<ValidationError> errors = new List<ValidationError>();
            ISolvencyData dpmConn = new SQLiteConnection(connString);
            int count = 1;
            
            //Get the expressions

            IEnumerable<vExpression> expressions = dpmConn.Query<vExpression>("select * from vExpression");
            IEnumerable<vValidationRule> validationScope = dpmConn.Query<vValidationRule>("select * from vValidationRule");

            //Serialize the intratable validation errors

            if (intraTableValidationError != null)
            {
                foreach (ValidationError v in intraTableValidationError)
                {
                    vExpression ex = (from e in expressions
                                      where e.ExpressionID == v.ExpressionId
                                      select e).FirstOrDefault();

                    string tableCode = (from t in treeView
                                        where t.TableID == v.TableId
                                        select t.TableCode).FirstOrDefault();

                    v.TableCode = tableCode;

                    v.Expression = ex.TableBasedFormula;
                    v.ErrorMessage = ex.ErrorMessage;

                    v.SNO = count++;

                    errors.Add(v);
                }
            }

            //Serialize cross table validation errors
            if(crossTableValidationError != null)
            {
                foreach(ValidationError v in crossTableValidationError)
                {
                    vExpression ex = (from e in expressions
                                      where e.ExpressionID == v.ExpressionId
                                      select e).FirstOrDefault();

                    vValidationRule sc = (from s in validationScope
                                           where s.ValidationRuleID == v.ValidationId
                                           select s).FirstOrDefault();

                    v.Expression = ex.TableBasedFormula;
                    v.ErrorMessage = ex.ErrorMessage;

                    v.TableCode = sc.Scope;

                    v.SNO = count++;

                    errors.Add(v);
                }
            }

            //Serialize the page information
            IEnumerable<mDimension> dimensions =  dpmConn.Query<mDimension>("select * from mDimension");
            IEnumerable<mMember> members = dpmConn.Query<mMember>("select * from mMember");


            foreach(ValidationError v in errors)
            {
                ValidationErrorParser ep = new ValidationErrorParser(v);
                v.Pages = ep.GetPagesFromContext();

                if(v.Pages != null)
                {
                    foreach(Page p in v.Pages)
                    {
                        mDimension dim = (from d in dimensions
                                          where d.DimensionXBRLCode == p.DimensionXBRLCode
                                          select d).FirstOrDefault();
                        mMember mem = (from m in members
                                       where m.MemberXBRLCode == p.MemberText
                                       select m).FirstOrDefault();

                        p.Dimension = dim;
                        p.Member = mem;
                    }
                }

                string scope = "";
                foreach(string s in ep.GetTableCodes())
                {
                    scope += s + ", ";
                }

                v.Scope = scope.Trim(new char[] { ' ', ',' });
            }


            return errors;
        }
    }
}