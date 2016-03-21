using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL
{
    public interface IETLPerformer
    {
        void PerformEtl(IExtractor _extractor, ITransformer _transformer, ILoader _loader, int cacheSize);
    }
}
