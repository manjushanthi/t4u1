using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Validation.Model;

namespace SolvencyII.Validation
{
    public static class Helper
    {
        public static HashSet<EvaluationCells> MapEvaluationCells(string cells)
        {
            string[] evaluations = cells.Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

            //Throw an error if the cells are not in specific format
            if (evaluations == null || evaluations.Count() < 1)
                throw new ValidationException("An error occured while parsing the following cells information: " + cells);

            HashSet<EvaluationCells> result = new HashSet<EvaluationCells>();
            string[] evalComps;

            foreach (string eval in evaluations)
            {
                evalComps = eval.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                if (evalComps.Count() == 3)
                {
                    EvaluationCells ec = new EvaluationCells
                    {
                        ValidationRuleId = int.Parse(evalComps[0]),
                        EvalNr = int.Parse(evalComps[1]),
                        Cells = evalComps[2]
                    };

                    result.Add(ec);
                }
                else
                    throw new ValidationException("An error occured while parsing the following cells information: " + cells);
            }

            return result;
        }
    }
}
