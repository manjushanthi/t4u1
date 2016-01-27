using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.Validation.Model;

namespace SolvencyII.Validation.Executor
{
    public interface IRuleExecutor
    {
        ISolvencyData DpmContext { get; set; }

        ProgressChangedEventHandler ValidationProgress { get; set; }
        IEnumerable<ValidationError> ValidateIntraTable(IEnumerable<vValidationRule> validationRules, IEnumerable<vIntraTableSQL> intraTableVr, IEnumerable<EvaluationCells> evalCells, long instanceID);
        IEnumerable<ValidationError> ValidateCrossTable(IEnumerable<vValidationRule> validationRules, IEnumerable<vValidationRuleSQL> crossTableVr, IEnumerable<EvaluationCells> evalCells, long instanceID);

        string GetValidationState();
    }
}
