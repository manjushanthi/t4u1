using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Domain.ENumerators;
using SolvencyII.Validation.Executor;
using SolvencyII.Validation.Query;

namespace SolvencyII.Validation
{
    public static class ValidationFactory
    {
        public static IRuleExecutor GetRuleExecutor(eDataTier tier)
        {
            switch (tier)
            {
                case eDataTier.SqLite:
                    return new SQLiteRuleExecutor();

                default:
                    return null;
            }
        }

        public static IValidationQuery GetValidationQuery(eDataTier tier)
        {
            switch(tier)
            {
                case eDataTier.SqLite:
                    return new SqliteValidationQuery();

                default:
                    return null;
            }
        }
    }
}
