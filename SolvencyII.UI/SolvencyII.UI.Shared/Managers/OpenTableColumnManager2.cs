using System;
using System.Collections.Generic;
using System.Linq;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Extensions;
using ucGenerator;

namespace SolvencyII.UI.Shared.Managers
{
    /// <summary>
    /// Used in the User Control Generator primarily to get column and row information.
    /// </summary>
    public class OpenTableColumnManager2
    {

        private readonly List<string> _tableVIDs = new List<string>();
        public List<OpenColInfo2> ColumnInfo = new List<OpenColInfo2>();
        public List<OpenColInfo2> RowInfo = new List<OpenColInfo2>();
        public List<OpenColInfo2> CellInfo = new List<OpenColInfo2>();
        private readonly bool _twoOpenDimensions;


        public OpenTableColumnManager2(string tableVID, GetSQLData getData, List<AxisOrdinateControls> controlList, bool twoDimOpen)
        {
            _twoOpenDimensions = twoDimOpen;
            _tableVIDs.Clear();
            _tableVIDs.Add(tableVID.ToString());
            bool dbOpen = false;
            if (getData == null)
            {
                getData = new GetSQLData();
                dbOpen = true;
            }
            if (!twoDimOpen)
                Setup(getData, controlList);
            else
                Setup2D(getData, controlList);
            if (dbOpen) getData.Dispose();
        }

        private void Setup(GetSQLData getData, List<AxisOrdinateControls> controlList)
        {

            // Get the columns:
            int colNumber = 0;

            List<NPageData> comboData = getData.GetOpenPageData(controlList);

            int tableVid = int.Parse(_tableVIDs[0]);
            List<AxisOrdinateControls> xDimensions = controlList.Where(d => d.AxisOrientation == "X" && d.TableID == tableVid && string.IsNullOrEmpty(d.SpecialCase)).ToList();
            xDimensions.AddRange(controlList.Where(d => d.AxisOrientation == "Y" && d.TableID == tableVid && string.IsNullOrEmpty(d.SpecialCase)).OrderBy(d => d.Order).ToList());
            xDimensions.SetupTopBranchOrder();

            CompareOpenAxisOrdinateControls compare2 = new CompareOpenAxisOrdinateControls();
            xDimensions = xDimensions.OrderBy(d => d, compare2).ToList();
            
            // xDimensions = xDimensions.OrderByDescending(d => d.AxisOrientation).OrderBy(d => d.Order).ToList();

            OrdinateLoop(getData, xDimensions, colNumber, comboData, ref ColumnInfo);
        }

        private void Setup2D(GetSQLData getData, List<AxisOrdinateControls> controlList)
        {

            // Get the columns:
            int colNumber = 0;

            List<NPageData> comboData = getData.GetOpenPageData(controlList);

            int tableVid = int.Parse(_tableVIDs[0]);

            // Columns
            List<AxisOrdinateControls> xDimensions = controlList.Where(d => d.AxisOrientation == "Y" && d.TableID == tableVid && !string.IsNullOrEmpty(d.SpecialCase)).ToList();
            xDimensions.SetupTopBranchOrder();
            CompareOpenAxisOrdinateControls compare2 = new CompareOpenAxisOrdinateControls();
            xDimensions = xDimensions.OrderBy(d => d, compare2).ToList();
            OrdinateLoop(getData, xDimensions, colNumber, comboData, ref ColumnInfo, true);

            // Rows
            xDimensions = controlList.Where(d => d.AxisOrientation == "X" && d.TableID == tableVid && !string.IsNullOrEmpty(d.SpecialCase)).ToList();
            xDimensions.SetupTopBranchOrder();
            xDimensions = xDimensions.OrderBy(d => d, compare2).ToList();
            OrdinateLoop(getData, xDimensions, colNumber, comboData, ref RowInfo, true);

            // Cells
            xDimensions = controlList.Where(d => (d.AxisOrientation == "Y" || d.AxisOrientation == "X") && d.TableID == tableVid && string.IsNullOrEmpty(d.SpecialCase)).ToList();
            xDimensions.SetupTopBranchOrder();
            xDimensions = xDimensions.OrderByDescending(d => d.AxisOrientation).ToList();
            OrdinateLoop(getData, xDimensions, colNumber, comboData, ref CellInfo);

            SetupCellInfo(ref CellInfo);

        }

        private void SetupCellInfo(ref List<OpenColInfo2> cellInfo)
        {
            if (cellInfo.Count != 2)
            {
                throw new ApplicationException("This should only be two elements - one for x and the other for y.");
            }
            cellInfo[0].ColName = cellInfo[0].ColName + cellInfo[1].ColName;
            cellInfo.RemoveAt(1);
        }

        private void OrdinateLoop(GetSQLData getData, List<AxisOrdinateControls> dimensions, int orNumber, List<NPageData> comboData, ref List<OpenColInfo2> aList, bool usePage = false)
        {
            foreach (AxisOrdinateControls ordinate in dimensions)
            {
                if (!ordinate.IsAbstractHeader && !ordinate.OrdinateCode.Contains("999"))
                {
                    OpenColInfo2 info = new OpenColInfo2
                                            {
                                                AxisID = (int) (ordinate.AxisID),
                                                Label = ordinate.OrdinateLabel,
                                                OrdinateID = (int) ordinate.OrdinateID,
                                                //IsRowKey = ordinate.IsRowKey ?? false,
                                                IsRowKey = ordinate.IsRowKey,
                                                ColNumber = orNumber,
                                                OrdinateCode = ordinate.OrdinateCode
                                            };

                    if (!usePage)
                        info.ColName = CalcColName(ordinate.OrdinateCode);
                    else
                        info.ColName = String.Format("PAGE{0}", ordinate.DimXbrlCode);

                    ComboHierarchy comboHier = getData.GetOrdinateHierarchyID_MD(ordinate.OrdinateID);
                    if (comboHier == null) comboHier = getData.GetOrdinateHierarchyID_HD(ordinate.OrdinateID);

                    if (comboHier != null)
                    {
                        info.HierarchyID = comboHier.HierarchyID;
                        if (info.HierarchyID != 0)
                        {
                            NPageData data = getData.GetHierachyNPageData(comboHier);
                            if (data != null)
                            {
                                info.StartOrder = data.StartOrder;
                                info.NextOrder = data.NextOrder;
                            }
                        }
                    }


                    if (info.HierarchyID == 0 && info.AxisID != 0 && comboData.Any())
                    {
                        NPageData data = comboData.FirstOrDefault(d => d.AxisID == info.AxisID);
                        if (data != null)
                        {
                            info.StartOrder = data.StartOrder;
                            info.NextOrder = data.NextOrder;
                        }
                    }

                    if (info.HierarchyID == 0)
                        info.ColType = getData.GetOrdinateType(ordinate.OrdinateID);
                    else
                        info.ColType = "ENUMERATION/CODE";

                    aList.Add(info);
                    orNumber++;
                }
            }
        }

        private string CalcColName(string ordinateCode)
        {
            if (ordinateCode.IsNumeric())
                return string.Format("C{0}", ordinateCode);
            return ordinateCode;
        }


        // High light cells

        //this.olvSimple.UseCellFormatEvents = true;
        //    this.olvSimple.FormatCell += (sender, args) =>
        //    {
        //        // Only for the columns you want
        //        if (args.Column.Text != "Cooking Skill")
        //            return;

        //        if (!(args.CellValue is int))
        //            return;

        //        switch ((int)args.CellValue) {
        //            case 1:
        //                args.SubItem.BackColor = Color.Aquamarine;
        //                break;
        //            case 30:
        //                args.SubItem.BackColor = Color.GreenYellow;
        //                break;
        //        }
        //    };

    }
}
