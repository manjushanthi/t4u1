using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DpmDB;
using DpmDB.BusinessData;
using T4U.CRT.Generation.Model;
using AT2DPM.DAL;
using AT2DPM.DAL.ExtendedModel;
using AT2DPM.DAL.Model;
using T4U.CRT.Generation.ExcelTemplateProcessor;
using System.Data;
using T4U.CRT.Generation.RelationlaTableFactory;

namespace T4U.CRT.Generation
{
    public class RelationalTablesProcessor
    {
        DPMdb _dpmDB;
        ICellShadeChecker _cellShadeChecker;
        IPageColumnNameFactory _pageNameFactory;

        public RelationalTablesProcessor(string dbFilePath, ICellShadeChecker cellShadeChecker)
        {
            //DPM_release_ver6_cleanEntities dpmDb
            DpmDbHelper dpmHelper = new DpmDbHelper();

            _dpmDB = dpmHelper.loadDpmDbModel(dbFilePath);
            _cellShadeChecker = cellShadeChecker;
            _pageNameFactory = new DimCodePageNamefactory();
            //dpmDB.Database.Initialize(false);
        }

        public ClassicRelationalTable generateRelationalTable(int TableID)
        {
            mTable tv = _dpmDB.mTables.FirstOrDefault(x => x.TableID == TableID);
            if(tv == null)
                throw new ArgumentNullException("no table version " + TableID);

            RelationalTableFactory stgp = new RelationalTableFactory(tv, _cellShadeChecker, _pageNameFactory);
            return stgp.generateTable();
        }

        public HashSet<ClassicRelationalTable> generateRelationalTables()
        {
            RelationalTableFactory stg = null;
            HashSet<ClassicRelationalTable> tables = new HashSet<ClassicRelationalTable>();

            foreach (mTable tv in _dpmDB.mTaxonomyTables.Select(x=>x.mTable))
            {
                stg = new RelationalTableFactory(tv, _cellShadeChecker, _pageNameFactory);
                tables.Add(stg.generateTable());
            }

            return tables;
        }

        #region ExcelTemplate Generation
        public ExcelTemplateColumns getColumnsForExcel(mTable tableName,  DataTable dt)
        {
            RelationalTableFactory stg = null;
            stg = new RelationalTableFactory(tableName, _cellShadeChecker, _pageNameFactory);
            ExcelTemplateColumns obj = stg.getcolumns(tableName, dt);

            return (obj);
        }
        #endregion
    }
}
