using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using NetOffice.ExcelApi;

using SolvencyII.Data.SQLite;
using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.ExcelImportExportLib.Domain;
using SolvencyII.ExcelImportExportLib.Events;
using SolvencyII.ExcelImportExportLib.Transform;
using SolvencyII.ExcelImportExportLib.Load;


namespace SolvencyII.ExcelImportExportLib
{
    public abstract class ImportExportBase : ExportImportEvents, IExcelExport, IExcelImport
    {
        protected virtual bool Invoke(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance, TransformBase transformData, LoadBase loadData, string[] tableFilter = null, string version = null) { return false; }
        protected virtual TransformBase GetTransformer() { return null; }
        protected virtual LoadBase GetLoader() { return null; }
        //Transaction management
        protected virtual void BeginTransaction(ISolvencyData sqliteConnection, Workbook workbook) { }
        protected virtual void Commit(ISolvencyData sqliteConnection, Workbook workbook) { }
        protected virtual void Rollback(ISolvencyData sqliteConnection, Workbook workbook) { }

        public string[] GetTableCodes(IExcelConnection excelConnection)
        {
            throw new NotImplementedException();
        }

        protected void Run(object obj)
        {
            ThreadParam param = (ThreadParam)obj;

            try
            {

                if (param.TableFilter != null)
                    Invoke(param.Behaviour, param.SqliteConnection, param.ExcelConnection, param.Instance, param.Transform, param.Load, param.TableFilter, param.CurrentExcelTemplateVersion);
                else
                    Invoke(param.Behaviour, param.SqliteConnection, param.ExcelConnection, param.Instance, param.Transform, param.Load, null, param.CurrentExcelTemplateVersion);

            }
            catch (Exception e)
            {
                OnCompleted(e, true, "An error occured, see exception for more details.");
            }
        }

        public bool ExportToFile(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance)
        {
            Invoke(behaviour, sqliteConnection, excelConnection, instance, new TransformDpmBusinessData(), new LoadBusinessExcelFromDpm());

            return true;
        }

        public void ExportToFileAsync(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance)
        {
            ThreadParam param = new ThreadParam
            {
                Behaviour = behaviour,
                SqliteConnection = sqliteConnection,
                ExcelConnection = excelConnection,
                Instance = instance,

                Transform = GetTransformer(),
                Load = GetLoader()
            };

            Thread thread = new Thread(Run);
            thread.Start(param);
        }

        public bool ExportToFile(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance, string[] tableFilter)
        {
            Invoke(behaviour, sqliteConnection, excelConnection, instance, GetTransformer(), GetLoader(), tableFilter);

            return true;
        }

        public void ExportToFileAsync(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance, string[] tableFilter)
        {
            ThreadParam param = new ThreadParam
            {
                Behaviour = behaviour,
                SqliteConnection = sqliteConnection,
                ExcelConnection = excelConnection,
                Instance = instance,
                TableFilter = tableFilter,

                Transform = GetTransformer(),
                Load = GetLoader()
            };

            Thread thread = new Thread(Run);
            thread.Start(param);
        }


        public bool ImportFromFile(ImportExportBehaviour behaviour, ISolvencyData conn, IExcelConnection excelConnection, dInstance instance, string supportedExcelTemplateVersion)
        {

            Invoke(behaviour, conn, excelConnection, instance, GetTransformer(), GetLoader(), null, supportedExcelTemplateVersion);

            return true;
        }


        public bool ImportFromFile(ImportExportBehaviour behaviour, ISolvencyData conn, IExcelConnection excelConnection, dInstance instance, string[] tableFilter, string supportedExcelTemplateVersion)
        {
            Invoke(behaviour, conn, excelConnection, instance, GetTransformer(), GetLoader(), tableFilter, supportedExcelTemplateVersion);

            return true;
        }

        public void ImportFromFileAsync(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance, string supportedExcelTemplateVersion)
        {
            ThreadParam param = new ThreadParam
            {
                Behaviour = behaviour,
                SqliteConnection = sqliteConnection,
                ExcelConnection = excelConnection,
                Instance = instance,

                Transform = GetTransformer(),
                Load = GetLoader(),
                CurrentExcelTemplateVersion = supportedExcelTemplateVersion
            };

            Thread thread = new Thread(Run);
            thread.IsBackground = true;
            thread.Start(param);
        }

        public void ImportFromFileAsync(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance, string[] tableFilter, string supportedExcelTemplateVersion)
        {
            ThreadParam param = new ThreadParam
            {
                Behaviour = behaviour,
                SqliteConnection = sqliteConnection,
                ExcelConnection = excelConnection,
                Instance = instance,
                TableFilter = tableFilter,

                Transform = GetTransformer(),
                Load = GetLoader(),
                CurrentExcelTemplateVersion = supportedExcelTemplateVersion
            };

            Thread thread = new Thread(Run);
            thread.IsBackground = true;
            thread.Start(param);
        }
    }
}
