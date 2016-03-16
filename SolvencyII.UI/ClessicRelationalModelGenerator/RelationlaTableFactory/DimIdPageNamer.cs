using AT2DPM.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation.RelationlaTableFactory
{
    class DimIdPageNamer : IPageColumnNameFactory
    {
        string IPageColumnNameFactory.PageColumnIdx(mOrdinateCategorisation oc)
        {
            string idx = (oc.mAxisOrdinate.mAxi.IsOpenAxis != null && ((bool)oc.mAxisOrdinate.mAxi.IsOpenAxis) == true) ?
                Convert.ToString(oc.mAxisOrdinate.AxisID) : Convert.ToString(oc.DimensionID);
            return idx;
        }
        
        string IPageColumnNameFactory.PageColumnIdx(mOpenAxisValueRestriction moavr)
        {
            return Convert.ToString(moavr.AxisID);
        }
    }
}
