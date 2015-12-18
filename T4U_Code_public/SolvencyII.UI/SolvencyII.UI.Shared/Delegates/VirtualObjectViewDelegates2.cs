using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain;
using SolvencyII.UI.Shared.Data;

namespace SolvencyII.UI.Shared.Delegates
{
    /// <summary>
    /// Management of events for Virtual Object List Views.
    /// Preiously used for cell editing before the separate close type control for row editing.
    /// </summary>
    public static class VirtualObjectViewDelegates2
    {
        private static List<OpenColInfo2> _columns;
        public static vDataSource3 vDataSource;

        public static event GenericDelegates.BoolResponse PanelChange;
        public static void SelectionChanged(bool subControlVisible)
        {
            if (PanelChange != null)
                PanelChange(subControlVisible);
        }

        public static event GenericDelegates.BoolResult SaveCurrentRow;


        public delegate void RowMethod(OpenTableDataRow2 row);
        public static event RowMethod CacheCurrentRow;
        private static void CacheThisRow(OpenTableDataRow2 row)
        {
            if (CacheCurrentRow != null)
                CacheCurrentRow(row);
        }

        public static event RowMethod DeleteCurrentRow;
        private static void DeleteThisRow(OpenTableDataRow2 row)
        {
            if (DeleteCurrentRow != null)
                DeleteCurrentRow(row);
        }

        public static void ClearAllRefs()
        {
            if (SaveCurrentRow != null)
                foreach (Delegate d in SaveCurrentRow.GetInvocationList())
                {
                    SaveCurrentRow -= (GenericDelegates.BoolResult)d;
                }
            if (CacheCurrentRow != null)
                foreach (Delegate d in CacheCurrentRow.GetInvocationList())
                {
                    CacheCurrentRow -= (RowMethod)d;
                }
            if (DeleteCurrentRow != null)
                foreach (Delegate d in DeleteCurrentRow.GetInvocationList())
                {
                    DeleteCurrentRow -= (RowMethod)d;
                }
        }

        public static void objectListView_CellEditStarting(object sender, CellEditEventArgs e)
        {
            //OpenTableDataRow2 row = (OpenTableDataRow2)e.RowObject;

            //// Check to see if there has been a row change:
            //if (vDataSource.CacheItem != null && row.PK_ID != vDataSource.CacheItem.PK_ID)
            //{
            //    // Row changed so save cached results.
            //    SaveThisRow();
            //}

            //// The appending of a new row has initially a Pk_ID of 0.
            //// There is no way of clicking off this when there is only one row.
            //// The code adds an additional row with an id of 0 when the zero row is edited
            //// When the focus come off the original row it gets saved and the 
            //// PK_ID allocated.
            //if (row.PK_ID == 0)
            //{
            //    // We are editing the new row so add a new one
            //}

            //Loggers.Logger.WriteLog(eSeverity.Debug, "objectListView_CellEditStarting", string.Format("PK_ID {0}", row.PK_ID));
            //if (_columns[e.SubItemIndex].HierarchyID != 0)
            //{
            //    // We have a dropdown that must be created and populated
            //    SolvencyRowComboBox cCombo = new SolvencyRowComboBox
            //                                     {
            //                                         Bounds = e.CellBounds,
            //                                         DropDownStyle = ComboBoxStyle.DropDownList
            //                                     };

            //    ListViewItem listItem = new ListViewItem {Name = "", Text = "Select"};
            //    cCombo.Items.Add(listItem);

            //    cCombo.PopulateWithHierachy2(_columns[e.SubItemIndex].Dimensions, row.ColValues[e.SubItemIndex]);
            //    cCombo.SetDropDownWidth();
            //    if (cCombo.SelectedItem == null)
            //        cCombo.SelectedItem = listItem;
            //    e.Control = cCombo;
            //    return;
            //}

            //// This column is not a drop down.
            //ISolvencyDataControl solvencyControl;
            //object dataValue = null;

            //switch (_columns[e.SubItemIndex].ColType)
            //{
            //    case "BOOLEAN":
            //        solvencyControl = new SolvencyCheckBox { ColumnType = SolvencyDataType.Boolean };
            //        if (e.Value != null) solvencyControl.Result = e.Value;
            //        solvencyControl.Bounds = e.CellBounds;
            //        e.Control = (Control)solvencyControl;
            //        return;
            //    case "MONETARY":
            //        solvencyControl = new SolvencyCurrencyTextBox { ColumnType = SolvencyDataType.Monetry };
            //        if (!string.IsNullOrEmpty(row.ColValues[e.SubItemIndex]))
            //            dataValue = Convert.ToDecimal(row.ColValues[e.SubItemIndex], CultureInfo.CurrentCulture);
            //        //dataValue =  Convert.ToDecimal(e.Value, CultureInfo.CurrentCulture);
            //        break;
            //    case "INTEGER":
            //        solvencyControl = new SolvencyCurrencyTextBox { ColumnType = SolvencyDataType.Integer };
            //        if (!string.IsNullOrEmpty(row.ColValues[e.SubItemIndex]))
            //            dataValue = Convert.ToInt32(row.ColValues[e.SubItemIndex], CultureInfo.CurrentCulture);
            //        break;
            //    case "PERCENTAGE":
            //        solvencyControl = new SolvencyCurrencyTextBox { ColumnType = SolvencyDataType.Percentage };
            //        if (!string.IsNullOrEmpty(row.ColValues[e.SubItemIndex]))
            //            dataValue = Convert.ToDecimal(row.ColValues[e.SubItemIndex], CultureInfo.CurrentCulture);
            //        //dataValue = (100 * Convert.ToDecimal(row.ColValues[e.SubItemIndex], CultureInfo.InvariantCulture));
            //        break;
            //    case "DECIMAL":
            //        solvencyControl = new SolvencyCurrencyTextBox { ColumnType = SolvencyDataType.Decimal };
            //        if (!string.IsNullOrEmpty(e.Value.ToString()))
            //            dataValue = Convert.ToDecimal(row.ColValues[e.SubItemIndex], CultureInfo.CurrentCulture);
            //        break;
            //    case "DATE":
            //        solvencyControl = new SolvencyDateTimePickerOpen { ColumnType = SolvencyDataType.Date };
            //        if (!string.IsNullOrEmpty(row.ColValues[e.SubItemIndex]))
            //            dataValue = Convert.ToDateTime(e.Value, CultureInfo.CurrentCulture);
            //        break;
            //    default:
            //        solvencyControl = new SolvencyTextBox { ColumnType = SolvencyDataType.String };
            //        dataValue = e.Value;
            //        break;
            //}
            //if (e.Value != null) solvencyControl.Result = dataValue;

            //solvencyControl.Bounds = e.CellBounds;
            //e.Control = (Control)solvencyControl;

        }

        public static void objectListView_OnCellEditValidating(object sender, CellEditEventArgs e)
        {
            //if (!e.Cancel)
            //{
            //    try
            //    {
            //        ISolvencyDataControl ctrl = (ISolvencyDataControl)e.Control;
            //        e.Cancel = !ctrl.IsValid();
            //    }
            //    catch (Exception ex)
            //    {
            //        e.Cancel = true;
            //        Console.WriteLine(ex);
            //    }
            //}
        }

        public static void objectListView_OnCellEditFinishing(object sender, CellEditEventArgs e)
        {
            //if (!e.Cancel)
            //{
            //    bool changeDetected = false;
            //    #region Update the data and manage the row

            //    // Update the display
            //    OpenTableDataRow2 row = (OpenTableDataRow2)e.RowObject;
            //    row.Modified = true;

            //    Logger.WriteLog(eSeverity.Debug, "objectListView_OnCellEditFinishing", string.Format("PK_ID {0}", row.PK_ID));
            //    if (_columns[e.SubItemIndex].HierarchyID != 0)
            //    {
            //        // We have a combo.
            //        ListViewItem listViewItem = (ListViewItem)((ComboBox)e.Control).SelectedItem;
            //        if (listViewItem != null)
            //        {
            //            // Item has been selected.
            //            if (row.ColValues[e.SubItemIndex] != listViewItem.Name) changeDetected = true;
            //            row.ColValues[e.SubItemIndex] = listViewItem.Name;
            //            e.NewValue = listViewItem.Name;

            //            if (listViewItem.Tag != null && (bool)listViewItem.Tag)
            //            {
            //                string colLabel = _columns[e.SubItemIndex].Label;
            //                VirtualObjectListView vList = (VirtualObjectListView)sender;
            //                ShowUserMessage(string.Format("You cannot select a parent value from the '{0}' column.", colLabel), vList, Color.MistyRose);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        switch (_columns[e.SubItemIndex].ColType)
            //        {
            //            case "DATE":
            //                DateTime controlValue = ((SolvencyDateTimePickerOpen)e.Control).Value;

            //                DateTime minDate = new DateTime(1753, 01, 01, 0, 0, 0);
            //                if (controlValue != minDate)
            //                {
            //                    if (row.ColValues[e.SubItemIndex] != controlValue.ConvertToDateString()) changeDetected = true;
            //                    row.ColValues[e.SubItemIndex] = controlValue.ConvertToDateString();
            //                    e.NewValue = controlValue.ConvertToDateString();
            //                }
            //                else
            //                {
            //                    row.ColValues[e.SubItemIndex] = "";
            //                    e.NewValue = null;
            //                }
            //                break;
            //            case "BOOLEAN":
            //                object result = (bool?)((SolvencyCheckBox)e.Control).Result;
            //                string value = "";
            //                if (result != null)
            //                {
            //                    value = (bool)result ? "1" : "0";
            //                }
            //                if (row.ColValues[e.SubItemIndex] != value) changeDetected = true;
            //                row.ColValues[e.SubItemIndex] = value;
            //                e.NewValue = value;
            //                break;
            //            case "PERCENTAGE":
            //                string cText = e.Control.Text;

            //                if (string.IsNullOrEmpty(cText))
            //                {
            //                    if (row.ColValues[e.SubItemIndex] != cText) changeDetected = true;
            //                    row.ColValues[e.SubItemIndex] = cText;
            //                    e.NewValue = cText;
            //                }
            //                else
            //                {
            //                    cText = cText.Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");
            //                    decimal trueDbeP = Convert.ToDecimal(cText, CultureInfo.CurrentCulture);
            //                    trueDbeP = trueDbeP / 100;
            //                    if (row.ColValues[e.SubItemIndex] != trueDbeP.ToString()) changeDetected = true;
            //                    row.ColValues[e.SubItemIndex] = trueDbeP.ToString();
            //                    e.NewValue = trueDbeP.ToString();
            //                }
            //                break;
            //            case "DECIMAL":
            //                // Update the displayed data
            //                if (row.ColValues[e.SubItemIndex] != e.Control.Text) changeDetected = true;
            //                row.ColValues[e.SubItemIndex] = e.Control.Text;
            //                e.NewValue = e.Control.Text;
            //                break;


            //            default:
            //                // Update the displayed data
            //                if (row.ColValues[e.SubItemIndex] != e.Control.Text) changeDetected = true;
            //                row.ColValues[e.SubItemIndex] = e.Control.Text;
            //                e.NewValue = e.Control.Text;
            //                break;

            //        }

            //    }

            //    #endregion

            //    // Row now contains the updated info


            //    // The user has entered data so attempt to save it
            //    if (!row.IsEmpty())
            //    {
            //        try
            //        {
            //            // Run a quick validation to ensure all Key fields are populated.

            //            VirtualObjectListView vList = (VirtualObjectListView)sender;
            //            StringBuilder sb = ListOfKeyErrors(row);

            //            // Save the current info to a cache to save when appropriate (cache needs to be read for display thus the DataSource persists it).
            //            if (changeDetected) vDataSource.CacheItem = row;

            //            if (sb.Length != 0)
            //            {
            //                // We need to update the visible text on the vList

            //                // We have an error so hightlight the row.

            //                ShowUserMessage(sb.ToString(), vList, Color.DarkRed);

            //                e.Cancel = false;


            //                //MessageBox.Show("Cannot save this row yet:\n" + sb.ToString(), "Temp Dialog message");
            //                //e.Cancel = true;
            //            }


            //            int rowCount = vList.VirtualListDataSource.GetObjectCount();
            //            if (rowCount != vList.VirtualListSize)
            //            {
            //                // Change numvber
            //                vList.VirtualListSize += 1;
            //            }

            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex);
            //            e.Cancel = true;
            //            throw;
            //        }
            //    }
            //}
        }

        public static void ShowUserMessage(String sb, VirtualObjectListView vList, Color borderColor)
        {
            // Alert the user
            TextOverlay textOverlay = new TextOverlay();
            textOverlay.TextColor = Color.Firebrick;
            textOverlay.BackColor = Color.AntiqueWhite;
            textOverlay.BorderColor = borderColor;
            textOverlay.BorderWidth = 4.0f;
            textOverlay.Font = new Font("Arial", 16);
            // textOverlay.Rotation = -5;
            textOverlay.Text = string.Format("{0}\n{1}", LanguageLabels.GetLabel(86, "This row is invalid:"), sb);

            vList.OverlayText = textOverlay;

            // Set a simple timer to remove the error message
            Timer tm = new Timer { Interval = 5 * 1000 };
            tm.Tick += delegate
            {
                vList.OverlayText = null;
                tm.Stop();
                tm.Dispose();
            };
            tm.Start();
        }



        public static void objectListView_FormatRow(object sender, FormatRowEventArgs e)
        {
            
            // No Check is performed.
            
            //if (e.Model is OpenTableDataRow2)
            //{
            //    OpenTableDataRow2 row = (OpenTableDataRow2) e.Model;
            //    Loggers.Logger.WriteLog(eSeverity.Debug, "objectListView_FormatRow", string.Format("PK_ID {0}, DisplayIndex {1}, RowIndex {2}", row.PK_ID, e.DisplayIndex, e.RowIndex));
            //    if (row.ColValues != null)
            //    {
            //        int populatedCols = row.ColValues.Count(t => !string.IsNullOrEmpty(t));
            //        if (populatedCols != 0)
            //        {
            //            StringBuilder sb = ListOfKeyErrors(row);
            //            e.Item.BackColor = sb.Length == 0 ? Color.White : Color.MistyRose;
            //            e.Item.ToolTipText = sb.ToString();
            //        }
            //        //if (vDataSource.CacheItemError && vDataSource.CacheItem != null)
            //        //{
            //        //    // NB the text comes from the save event.
            //        //    // We have a error with the cache to highlight the row
            //        //    if (row.PK_ID == vDataSource.CacheItem.PK_ID)
            //        //    {
            //        //        e.Item.BackColor = Color.PaleGreen;
            //        //    }
            //        //}
            //    }
            //}
        }

        public static void objectListView_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            if (e.ColumnIndex != _columns.Count)
            {
                if (_columns[e.ColumnIndex].IsRowKey)
                {
                    if (e.Model is OpenTableDataRow2)
                    {
                        OpenTableDataRow2 row = (OpenTableDataRow2) e.Model;
                        if (row.ColValues.Any() && string.IsNullOrEmpty(row.ColValues[e.ColumnIndex]))
                            e.Text = string.Format(LanguageLabels.GetLabel(88, "Please add {0}"), string.IsNullOrEmpty(_columns[e.ColumnIndex].Label) ? "this key field" : _columns[e.ColumnIndex].Label);
                    }
                }
            }
        }

        public static void objectListView_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            // Delete now found in row editor control.

            if (e.Model != null)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                ToolStripItem item = new ToolStripButton("Delete", null, OnRowDeleteClick);
                item.Tag = e.Model;
                menu.Items.Add(item);
                e.MenuStrip = menu;
            }
        }

        private static void OnRowDeleteClick(object sender, EventArgs eventArgs)
        {
            ToolStripItem item = (ToolStripItem)sender;
            OpenTableDataRow2 row = (OpenTableDataRow2)item.Tag;
            DeleteThisRow(row);
        }

        public static void SetColumns(List<OpenColInfo2> columns)
        {
            _columns = columns;
        }

    }


}
