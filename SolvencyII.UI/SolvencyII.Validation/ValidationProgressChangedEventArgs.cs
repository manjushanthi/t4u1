using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using SolvencyII.Validation.Model;

namespace SolvencyII.Validation
{
    public class ValidationProgressChangedEventArgs : ProgressChangedEventArgs
    {
        public IEnumerable<EvaluationCells> Cells { get; set; }

        public ValidationProgressChangedEventArgs(string message, int percentage, IEnumerable<EvaluationCells> cells)
            : base(percentage, message)
        {
            Cells = cells;
        }
    }
}
