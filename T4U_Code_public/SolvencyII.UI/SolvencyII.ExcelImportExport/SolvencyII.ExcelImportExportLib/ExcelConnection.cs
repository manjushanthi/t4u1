using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using NetOffice.ExcelApi;

namespace SolvencyII.ExcelImportExportLib
{
    public class ExcelConnection : IExcelConnection
    {
        string excelInputFile;

        Workbook templateWB;

        Application excelApp;

        Sheets _sheets;

        public Workbook TemplateWorkbook
        {
            get { return templateWB; }
            set { templateWB = value; }
        }

        public Sheets WorkbookSheets
        {
            get {

                if (_sheets == null)
                    _sheets = templateWB.Worksheets;

                return _sheets;

            }
        }

        public Application ExcelApp
        {
            get { return excelApp; }
        }

        public ExcelConnection(string inputFile)
        {
            excelInputFile = inputFile;
        }

        public bool OpenConnection()
        {
            if (templateWB == null)
            {
                //Get workbook from inputfile
                excelApp = new NetOffice.ExcelApi.Application();

                templateWB = excelApp.Workbooks.Open(excelInputFile
                                                        , Type.Missing
                                                        , Type.Missing
                                                        , Type.Missing
                                                        , Type.Missing
                                                        , Type.Missing
                                                        , Type.Missing
                                                        , Type.Missing
                                                        , Type.Missing
                                                        , Type.Missing
                                                        , Type.Missing
                                                        , Type.Missing
                                                        , Type.Missing
                                                        , Type.Missing
                                                        , Type.Missing);
            }

            return true;
        }

        public void CloseConnection()
        {
            if (_sheets != null)
            {

                _sheets.DisposeChildInstances();
                _sheets.Dispose();
            }

            if (templateWB != null)
            {
                templateWB.Close();
                templateWB.DisposeChildInstances();
                templateWB.Dispose();
            }

            excelApp.Quit();
            excelApp.DisposeChildInstances();
            excelApp.Dispose();
            

            excelApp = null;
            templateWB = null;
            _sheets = null;

            /*xWorksheet = null;
            xCharts = null;
            xMyChart = null;
            xGraph = null;
            xSeriesColl = null;
            xSeries = null;*/

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }


        public string GetConnectionString()
        {
            return excelInputFile;
        }
    }
}
