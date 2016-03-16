using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Domain.Interfaces;
using SolvencyII.Domain;

namespace SolvencyII.ExcelImportExportLib.DpmObjects
{
    public class TableTypeInfo
    {
        int xAxisCount = -1;
        int yAxisCount = -1;
        int zAxisCount = -1;

        int xAxisOpenValueRestriction = -1;
        int yAxisOpenValueRestriction = -1;

        private void FindCount(ISolvencyData sqliteConnection, string tableCode)
        {
            if (xAxisCount == -1 || yAxisCount == -1)
            {
                IEnumerable<mAxis> axis = sqliteConnection.Query<mAxis>("select a.* from mTable t, mTableAxis ta, mAxis a where t.tableid = ta.tableid and ta.axisid = a.axisid and t.tablecode = '" + tableCode + "'");
                IEnumerable<mOpenAxisValueRestriction> openAxis = sqliteConnection.Query<mOpenAxisValueRestriction>("select * from mOpenAxisValueRestriction");

                xAxisCount =
                    (
                    from xaxi in axis
                    where xaxi.IsOpenAxis == true && xaxi.AxisOrientation == "X"
                    select xaxi.AxisOrientation
                    ).Distinct().Count();

                yAxisCount =
                    (
                    from yaxi in axis
                    where yaxi.IsOpenAxis == true && yaxi.AxisOrientation == "Y"
                    select yaxi.AxisOrientation
                    ).Distinct().Count();

                zAxisCount =
                    (
                    from zaxi in axis
                    where zaxi.IsOpenAxis == true && zaxi.AxisOrientation == "Z"
                    select zaxi.AxisOrientation
                    ).Distinct().Count();

                xAxisOpenValueRestriction =
                    (
                    from x in axis
                    from oa in openAxis
                    where x.AxisID == oa.AxisID &&
                    x.IsOpenAxis == true &&
                    x.AxisOrientation == "X"
                    select oa
                    ).Count();

                yAxisOpenValueRestriction =
                    (
                    from y in axis
                    from oa in openAxis
                    where y.AxisID == oa.AxisID &&
                    y.IsOpenAxis == true &&
                    y.AxisOrientation == "Y"
                    select oa
                    ).Count();

            }
        }

        public bool IsClosedTable(ISolvencyData sqliteConnection, string tableCode)
        {
            FindCount(sqliteConnection, tableCode);

            if (xAxisCount == 0 && yAxisCount == 0 )
                return true;



            return false;
        }

        public bool IsOpenTable(ISolvencyData sqliteConnection, string tableCode)
        {
            FindCount(sqliteConnection, tableCode);

            if (xAxisCount == 0 && yAxisCount > 0 /*&& yAxisOpenValueRestriction <= 0*/)
                return true;
            

            return false;
        }

        public bool IsSemiOpenTable(ISolvencyData sqliteConnection, string tableCode)
        {
            FindCount(sqliteConnection, tableCode);

            if (xAxisCount > 0 || yAxisCount > 0)
                return true;

            return false;
        }

        public bool IsOpenTableHasDropdowns(ISolvencyData sqliteConnection, string tableCode)
        {
            FindCount(sqliteConnection, tableCode);

            if (xAxisCount == 0 && yAxisCount > 0 && zAxisCount > 0)
                return true;


            return false;
        }
    }
}
