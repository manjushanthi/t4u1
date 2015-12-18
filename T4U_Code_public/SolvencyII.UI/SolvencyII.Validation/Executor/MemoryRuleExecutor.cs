using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using SolvencyII.Domain.Interfaces;

namespace SolvencyII.Validation.Executor
{
    public class MemoryRuleExecutor : RuleExecutorBase, IRuleExecutor
    {

        public ProgressChangedEventHandler ValidationProgress
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<Model.ValidationError> ValidateIntraTable(IEnumerable<SolvencyII.Domain.vValidationRule> validationRules, IEnumerable<SolvencyII.Domain.vIntraTableSQL> intraTableVr, IEnumerable<Model.EvaluationCells> evalCells, long instanceID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.ValidationError> ValidateCrossTable(IEnumerable<SolvencyII.Domain.vValidationRule> validationRules, IEnumerable<SolvencyII.Domain.vValidationRuleSQL> crossTableVr, IEnumerable<Model.EvaluationCells> evalCells, long instanceID)
        {
            throw new NotImplementedException();
        }

        public string GetValidationState()
        {
            throw new NotImplementedException();
        }
    }
}
