using AT2DPM.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation.RelationlaTableFactory
{
    public interface IPageColumnNameFactory
    {
        string PageColumnIdx(mOrdinateCategorisation oc);
        string PageColumnIdx(mOpenAxisValueRestriction moavr);
    }
}
