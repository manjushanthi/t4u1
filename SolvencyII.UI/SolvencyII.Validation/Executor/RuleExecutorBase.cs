using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using SolvencyII.Domain.Interfaces;

namespace SolvencyII.Validation.Executor
{
    public class RuleExecutorBase
    {
        StringBuilder validationRuleState;

        public ISolvencyData DpmContext { get; set; }

        protected ProgressChangedEventHandler validationProgress;

        public void OnProgress(object sender, string message, int percentage)
        {
            if (validationProgress != null)
                validationProgress(sender, new ProgressChangedEventArgs(percentage, message));
        }

        public void OnProgress(object sender, ValidationProgressChangedEventArgs args)
        {
            if (validationProgress != null)
                validationProgress(sender, args);
        }

        public void AddState(string info)
        {
            if (validationRuleState == null)
                validationRuleState = new StringBuilder();

            validationRuleState.Append(info);
            validationRuleState.AppendLine();
        }

        public void ClearState()
        {
            validationRuleState = new StringBuilder();
        }

        public virtual string GetState()
        {
            return validationRuleState != null? validationRuleState.ToString(): "";
        }
    }
}
