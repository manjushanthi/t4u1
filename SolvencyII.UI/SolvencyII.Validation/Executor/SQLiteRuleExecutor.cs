using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.Validation.Model;

namespace SolvencyII.Validation.Executor
{
    public class SQLiteRuleExecutor : RuleExecutorBase, IRuleExecutor
    {
        public new ProgressChangedEventHandler ValidationProgress
        {
            get
            {
                return this.validationProgress;
            }
            set
            {
                this.validationProgress = value;
            }
        }

        public IEnumerable<ValidationError> ValidateIntraTable(IEnumerable<vValidationRule> validationRules, IEnumerable<vIntraTableSQL> intraTableVr, IEnumerable<EvaluationCells> evalCells, long instanceID)
        {
            IList<ValidationError> overallError = new List<ValidationError>();

            foreach (vIntraTableSQL v in intraTableVr)
            {
                /*IEnumerable<EvaluationCells> filteredEvalCells = from e in evalCells
                                                                 where e.TableId == v.TableID
                                                                 select e;*/

                IEnumerable<ValidationError> error = ValidateRule(instanceID, v.SQL, v.CELLS, validationRules);

                if (error != null)
                {
                    foreach (ValidationError e in error)
                    {
                        e.TableId = v.TableID;
                        overallError.Add(e);
                    }
                }

            }
            return overallError;
        }

        public IEnumerable<ValidationError> ValidateCrossTable(IEnumerable<vValidationRule> validationRules, IEnumerable<vValidationRuleSQL> crossTableVr, IEnumerable<EvaluationCells> evalCells, long instanceID)
        {
            IList<ValidationError> overallError = new List<ValidationError>();

            foreach (vValidationRuleSQL v in crossTableVr)
            {
                /*IEnumerable<EvaluationCells> filteredEvalCells = from e in evalCells
                                                                 where e.ValidationRuleId == v.ValidationRuleID
                                                                 select e;
*/

                IEnumerable<ValidationError> error = ValidateRule(instanceID, v.SQL, v.CELLS, validationRules);

                if (error != null)
                {
                    foreach (ValidationError e in error)
                        overallError.Add(e);
                }
            }

            return overallError;
        }

        private IEnumerable<ValidationError> ValidateRule(long instanceID, string sqlScript, string cells, IEnumerable<vValidationRule> validationRules)
        {
            AddState("Validation query: " + sqlScript);
            AddState("Evaluation cells: " + cells);


            HashSet<EvaluationCells> evalCells = Helper.MapEvaluationCells(cells);


            vValidationRule rule = (from v in validationRules
                                    where v.ValidationRuleID == evalCells.FirstOrDefault().ValidationRuleId
                                    select v).FirstOrDefault();

            //Verify that the cells are evaluated against a validation rule
            if (rule == null)
                throw new ValidationException("Mismatch validation rule against the evaluating cells.");

            AddState("Valiation code: " + rule.ValidationCode);

            //Trigger an event
            OnProgress(this, new ValidationProgressChangedEventArgs("Validating " + rule.ValidationCode, 0, evalCells));     


            IList<EvalResult> result = null;

            //Execute validation query
            if (DpmContext != null)
                result = DpmContext.Query<EvalResult>(sqlScript);

            if (result == null)
                return null;    //Should i throw an exception here


            //Get the errors from the result

            IList<ValidationError> validationError = new List<ValidationError>();

            //BRAG
            foreach (EvalResult er in result)
            {
                //Get the total number of Formulas in the sql script
                er.TotalFormula = evalCells.Count();

                for (int i = 1; i <= er.TotalFormula; i++)
                {
                    string propName = "E" + i.ToString() + "_FORMULA";

                    string formula = (string)er.GetType().GetProperty(propName).GetValue(er, null);

                    string l = (string)er.GetType().GetProperty("E" + i.ToString() + "_L").GetValue(er, null);
                    string r = (string)er.GetType().GetProperty("E" + i.ToString() + "_R").GetValue(er, null);
                    string difference;
                    if(l == null || r == null)
                    {
                        l = null;
                        r = null;
                    }
                    try
                    {
                        difference = l != null && r != null ? Math.Abs(double.Parse(l.Replace(".", ",")) - double.Parse(r.Replace(".", ","))).ToString() : null;
                    }
                    catch (Exception)
                    {
                        l = null;
                        r = null;
                        difference = null;
                    }

                    //Select corresponding cells and other information

                    //BRAG TODO: remove +1 after database update
                    EvaluationCells ec = (from e in evalCells
                                          where e.EvalNr == i + 1
                                          select e).FirstOrDefault();

                    if (ec == null)
                        throw new ValidationException("No evaluation cells found for the formula: " + propName);

                    if (formula != null)
                    {
                        ValidationError ve = new ValidationError
                        {
                            InstanceId = instanceID,
                            PK_ID = er.PK_ID,
                            ValidationId = ec.ValidationRuleId,
                            Cells = ec.Cells,
                            Formula = formula,
                            Context = er.CONTEXT,
                            ValidationCode = rule.ValidationCode,
                            ExpressionId = rule.ExpressionID.Value,
                            LeftSideValue = l != null ? l : "N/A",
                            RightSideValue = r != null ? r : "N/A",
                            Difference = difference != null ? difference : "N/A",
                        };

                        validationError.Add(ve);
                    }
                }
            }


            //Once everything is successful clear the state
            ClearState();

            return validationError;
        }

        public string GetValidationState()
        {
            return GetState();
        }
    }
}
