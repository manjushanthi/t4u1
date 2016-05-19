using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Linq;

using NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;

using AT2DPM.DAL.Model;
using AT2DPM.Events.Delegate;

using T4UImportExportGenerator.Domain;

namespace T4UImportExportGenerator
{
    public class GenerateBase
    {
        int contentRow = 2;
        string tocSheetName = "Table of contents";

        event CompletedEventHandler completed;

        event ProgressChangedEventHandler progressChanged;

        protected object objectLock = new Object();

        public event CompletedEventHandler Completed
        {
            add
            {
                lock (objectLock)
                {
                    completed += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    completed -= value;
                }
            }

        }

        public event ProgressChangedEventHandler ProgressChanged
        {
            add
            {
                lock (objectLock)
                {
                    progressChanged += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    progressChanged -= value;
                }
            }
        }

        public void OnCompleted(Exception error, bool cancelled, Object userState)
        {
            if (completed != null)
            {
                AsyncCompletedEventArgs args = new AsyncCompletedEventArgs(error, cancelled, userState);

                completed(this, args);
            }
        }

        public void OnProgressChanged(int progressPercentage, object userState)
        {
            if (progressChanged != null)
            {
                ProgressChangedEventArgs args = new ProgressChangedEventArgs(progressPercentage, userState);

                progressChanged(this, args);
            }
        }


        public void Run(object obj)
        {
            ThreadParam param = (ThreadParam)obj;

            try
            {
                Generate(param.DpmContext, param.GeneratorInfo, param.VersionNumber);
            }
            catch (Exception e)
            {
                OnCompleted(e, true, "An error occured, see exception for more details.");
            }
            finally
            {

            }
        }

        public virtual void Generate(DPMdb dpmContext, IEnumerable<GenerateInfo> generatorInfo, string versionNumber)
        { }

        public void GenerateToc(DPMdb dpmContext, Worksheet tableContents, string[] tableCodes)
        {
            tableContents.Name = tocSheetName;
            tableContents.Cells[contentRow, 2].Value = "S.No";
            tableContents.Cells[contentRow, 3].Value = "Table Code";
            tableContents.Cells[contentRow, 4].Value = "Table Label";

            int sno = 1;
            foreach (string s in tableCodes)
            {
                //Get table information from DPM database
                mTable tv = dpmContext.mTables.FirstOrDefault(x => x.TableCode == s);

                //Add to table of contents
                contentRow++;
                tableContents.Cells[contentRow, 2].Value = sno++;
                tableContents.Cells[contentRow, 3].Value = tv.TableCode;
                tableContents.Cells[contentRow, 4].Value = tv.TableLabel;

                Range link = tableContents.Cells[contentRow, 3];
                tableContents.Hyperlinks.Add(link, "", tv.TableCode + "!A1", "Click to navigate " + tv.TableCode, tv.TableCode);
            }
        }

        protected void DrawBorder(Worksheet ws, int row, int col, int rowLen, int colLen)
        {
            Range borderRange = ws.Range(ws.Cells[row, col], ws.Cells[row + rowLen - 1, col + colLen - 1]);
            borderRange.Borders.Color = Color.Black;
            borderRange.Borders[XlBordersIndex.xlEdgeRight].Weight = 4;
            borderRange.Borders[XlBordersIndex.xlEdgeLeft].Weight = 4;
            borderRange.Borders[XlBordersIndex.xlEdgeBottom].Weight = 4;
            borderRange.Borders[XlBordersIndex.xlEdgeTop].Weight = 4;
        }

        public void FormatToc(Worksheet tableContents, Sheets moveBefore)
        {
            //Format table of contents sheet
            contentRow--;

            //Set the First column width
            Range cell = tableContents.Range("A:A", System.Type.Missing);
            cell.EntireColumn.ColumnWidth = 3;

            //Table code column
            cell = tableContents.Range(tableContents.Cells[2, 3], tableContents.Cells[contentRow, 3]);
            cell.EntireColumn.ColumnWidth = 15;
            //TableLable column
            cell = tableContents.Range(tableContents.Cells[2, 4], tableContents.Cells[contentRow, 4]);
            cell.EntireColumn.ColumnWidth = 70;
            cell.WrapText = true;

            //Header Range
            cell = tableContents.Range(tableContents.Cells[2, 2], tableContents.Cells[2, 4]);
            cell.Font.Bold = true;
            //Change backgroud color of row/col code
            cell.Interior.Color = ColorTranslator.ToOle(Color.Gold);

            DrawBorder(tableContents, 2, 2, contentRow, 3);

            //Hide the grid lines
            tableContents.Activate();
            tableContents.Application.ActiveWindow.DisplayGridlines = false;

            //Move table of contents to first
            tableContents.Move(moveBefore[1]);
            tableContents.Protect();
        }
    }
}
