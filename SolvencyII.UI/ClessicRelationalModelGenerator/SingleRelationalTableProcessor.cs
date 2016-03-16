using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AT2DPM.DAL;
using AT2DPM.DAL.Model;
using AT2DPM.DAL.ExtendedModel;
using DpmDB;
using DpmDB.BusinessData;
using T4U.CRT.Generation.Model;
using T4U.CRT.Generation.RelationlaTableFactory;
using T4U.CRT.Generation.ExcelTemplateProcessor;
using System.Data;

namespace T4U.CRT.Generation
{
    public class RelationalTableFactory
    {
        private mTable _taxTable;
        private ICellShadeChecker _cellShadeChecker;
        IPageColumnNameFactory pageColumnNamer;
        
        public string RelationalTableName
        {
            get 
            {  
                if (_taxTable == null)
                    throw new NullReferenceException("No reference to taxonomy");
                
                //return _taxTable.TableID + "_" + _taxTable.TableCode;

                mTaxonomy tax = (from t in _taxTable.mTaxonomyTables
                                    orderby t.mTaxonomy.FromDate ascending
                                    select t.mTaxonomy).FirstOrDefault();
                if(tax == null)
                    throw new ArgumentException(_taxTable.TableID + " table has no taxonomy");
                
                return (_taxTable.TableCode + "__" + tax.TaxonomyCode + "__" + tax.Version).Replace(".", "_").Replace(" ", "_").Replace("-", "_");
            }
        }

        public RelationalTableFactory(mTable taxTable, ICellShadeChecker cellShadeChecker, IPageColumnNameFactory pageColumnNamer)
        {
            if (taxTable == null)
                throw new ArgumentNullException("Table version cannot be null");

            this._taxTable = taxTable;
            this._cellShadeChecker = cellShadeChecker;
            this.pageColumnNamer = pageColumnNamer;
        }

        internal ClassicRelationalTable generateTable()
        {
            ClassicRelationalTable newTable = new ClassicRelationalTable(this._taxTable.TableID);
            newTable.Columns = this.getcolumns();
            newTable.Name = RelationalTableName;            
            return newTable;
        }

        private HashSet<IRelationalColumn> getcolumns()
        {
            HashSet<mAxisOrdinate> xOrdinates = getXordinates();
            HashSet<mAxisOrdinate> yOrdinates = getYordinates();
            HashSet<mAxisOrdinate> zOrdinates = getZordinates();
            HashSet<mAxi> openZaxes = getZaxes();
            HashSet<IRelationalColumn> columns = new HashSet<IRelationalColumn>();

            if (xOrdinates.All(x => x.mAxi.IsOpenAxis == true) || yOrdinates.All(x => x.mAxi.IsOpenAxis == true))
                unionColumns(columns, getRCcolumns(xOrdinates.Union(yOrdinates)));
            else if((xOrdinates.Any(x=>x.mAxi.IsOpenAxis == true) && yOrdinates.Count > 0) || 
                    (yOrdinates.Any(x=>x.mAxi.IsOpenAxis == true) && xOrdinates.Count > 0)) 
            {
                unionColumns(columns, getRCcolumns(xOrdinates.Where(x=>x.mAxi.IsOpenAxis != true), yOrdinates.Where(x=>x.mAxi.IsOpenAxis != true)));
                unionColumns(columns, getPageColumns(xOrdinates.Where(x => x.mAxi.IsOpenAxis == true).Select(x=>x.mAxi)
                                                .Union(yOrdinates.Where(x => x.mAxi.IsOpenAxis == true).Select(x=>x.mAxi))
                                                , columns.Count(x => x is PageColumn) + 1));
            }
            else if (xOrdinates.Count > 0 && yOrdinates.Count > 0 && xOrdinates.All(x=>x.mAxi.IsOpenAxis != true) && yOrdinates.All(x=>x.mAxi.IsOpenAxis != true)) 
                unionColumns(columns, getRCcolumns(xOrdinates, yOrdinates));
            else if (xOrdinates.Count > 0 && yOrdinates.Count == 0) unionColumns(columns, getRCcolumns(xOrdinates));
            else if (yOrdinates.Count > 0 && xOrdinates.Count == 0) unionColumns(columns, getRCcolumns(yOrdinates));
            else throw new ArgumentException("Case not covered in table " + this.RelationalTableName);

            if(zOrdinates.Count > 0) unionColumns(columns, getPageColumns(zOrdinates));
            if(openZaxes.Count > 0) unionColumns(columns, getPageColumns(openZaxes, columns.Count(x => x is PageColumn) + 1));

            columns = filterColumns(columns);
            return columns;
        }

        private HashSet<mAxi> getZaxes()
        {
            HashSet<mAxi> openZaxes = new HashSet<mAxi>((from a in _taxTable.mTableAxis
                                                         where 
                                                         //a.mAxi.AxisOrientation == "Z" &&
                                                         a.mAxi.IsOpenAxis == true
                                                           && a.mAxi.mOpenAxisValueRestrictions.Count > 0
                                                           && !isOnTypedYaxis(a)
                                                           //&& _taxTable.mTableAxis.Where(x => x.mAxi.AxisOrientation.Equals(a.mAxi.AxisOrientation))
                                                           //     .All(x => x.mAxi.mAxisOrdinates.Any(y => y.mOrdinateCategorisations.All(z => z.mDimension.IsTypedDimension != true)))
                                                         select a.mAxi).ToList());
            return openZaxes;
        }

        private bool isOnTypedYaxis(mTableAxi axis)
        {
            if (!axis.mAxi.AxisOrientation.Equals("Y"))
                return false;

            foreach (mTableAxi ta in _taxTable.mTableAxis)
            {
                if (!ta.mAxi.AxisOrientation.Equals("Y"))
                    continue;

                foreach (mAxisOrdinate ap in ta.mAxi.mAxisOrdinates)                
                    foreach (mOrdinateCategorisation oc in ap.mOrdinateCategorisations)
                        if (oc.mDimension != null && oc.mDimension.IsTypedDimension == true)
                            return true;
            }

            return false;
        }

        private HashSet<mAxisOrdinate> getZordinates()
        {
            HashSet<mAxisOrdinate> zOrdinates = new HashSet<mAxisOrdinate>((from at in _taxTable.mTableAxis
                                                                            from o in at.mAxi.mAxisOrdinates
                                                                            where at.mAxi.AxisOrientation == "Z"
                                                                          && (o.IsAbstractHeader == false || o.IsAbstractHeader == null)
                                                                          && (at.mAxi.mOpenAxisValueRestrictions.Count() == 0
                                                                            //|| (at.mAxi.mOpenAxisValueRestrictions.Count() > 0
                                                                            //    && _taxTable.mTableAxis
                                                                            //        .Where(x => x.mAxi.AxisOrientation.Equals("Z"))
                                                                            //        .Any(x => x.mAxi.mAxisOrdinates.Any(y => y.mOrdinateCategorisations.Any(z => z.mDimension.IsTypedDimension == true))))
                                                                            )                                                                         
                                                                            select o));
            return zOrdinates;
        }

        private HashSet<mAxisOrdinate> getYordinates()
        {
            HashSet<mAxisOrdinate> yOrdinates = new HashSet<mAxisOrdinate>((from at in _taxTable.mTableAxis
                                                                            from o in at.mAxi.mAxisOrdinates
                                                                            where at.mAxi.AxisOrientation == "Y"
                                                                         && (o.IsAbstractHeader == false || o.IsAbstractHeader == null)
                                                                         && (at.mAxi.mOpenAxisValueRestrictions.Count() == 0
                                                                            || (at.mAxi.mOpenAxisValueRestrictions.Count() > 0
                                                                                && _taxTable.mTableAxis
                                                                                    .Where(x => x.mAxi.AxisOrientation.Equals("Y"))
                                                                                    .Any(x => x.mAxi.mAxisOrdinates.Any(y => y.mOrdinateCategorisations.Any(z => z.mDimension.IsTypedDimension == true))))
                                                                            )
                                                                            select o).ToList());
            validateCodesOfOrdinates(yOrdinates);
            return yOrdinates;
        }

        private HashSet<mAxisOrdinate> getXordinates()
        {
            HashSet<mAxisOrdinate> xOrdinates = new HashSet<mAxisOrdinate>((from at in _taxTable.mTableAxis
                                                                            from o in at.mAxi.mAxisOrdinates
                                                                            where at.mAxi.AxisOrientation == "X"
                                                                        && (o.IsAbstractHeader == false || o.IsAbstractHeader == null)
                                                                        && (at.mAxi.mOpenAxisValueRestrictions.Count() == 0
                                                                            || (at.mAxi.mOpenAxisValueRestrictions.Count() > 0 
                                                                                && _taxTable.mTableAxis
                                                                                    .Where(x=>x.mAxi.AxisOrientation.Equals("X"))
                                                                                    .Any(x=>x.mAxi.mAxisOrdinates.Any(y=>y.mOrdinateCategorisations.Any(z=>z.mDimension.IsTypedDimension == true))))
                                                                            )
                                                                            select o).ToList());
            validateCodesOfOrdinates(xOrdinates);
            return xOrdinates;
        }

        private void unionColumns(HashSet<IRelationalColumn> columns, IEnumerable<IRelationalColumn> enumerable)
        {
            foreach (IRelationalColumn col in enumerable)            
                columns.Add(col);            
        }
        
        private HashSet<IRelationalColumn> filterColumns(IEnumerable<IRelationalColumn> columns)
        {
            if (columns == null)
                return null;

            var res = columns.Where(x => x != null 
                                    && !x.getColumnName().Contains("C999"));

            return new HashSet<IRelationalColumn>(res);
        }

        private IEnumerable<IRelationalColumn> getPageColumns(IEnumerable<mAxi> openZaxes, int startingIndex)
        {
            HashSet<IRelationalColumn> pageColumns = new HashSet<IRelationalColumn>();
            IRelationalColumnFactory pcf;
            HashSet<mMember> members;
            mDimension dimension;
            List<mOpenAxisValueRestriction> moavrs = (from o in openZaxes from res in o.mOpenAxisValueRestrictions select res).ToList();
            mOAVRHelper moavrHelp = new mOAVRHelper();

            foreach (mOpenAxisValueRestriction moavr in moavrs.Distinct())
            {
                members = moavrHelp.findMembers(moavr);
                dimension = moavrHelp.findDimension(moavr);
                pcf = new PageColumnFactory(dimension, members, pageColumnNamer.PageColumnIdx(moavr));
                pageColumns.Add(pcf.getColumn());
            }
            return pageColumns;
        }
        
        private IEnumerable<IRelationalColumn> getPageColumns(IEnumerable<mAxisOrdinate> zOrdinates)
        {
            HashSet<IRelationalColumn> pageColumns = new HashSet<IRelationalColumn>();
            IRelationalColumnFactory pcf;
            HashSet<mMember> members;
            

            var ordCategoristaions = (from o in zOrdinates
                                      from oc in o.mOrdinateCategorisations
                                      where oc.Source == null || oc.Source.Trim().ToUpper() != "HD"
                                      select oc);

            int i = 0;
            foreach (mOrdinateCategorisation oc in ordCategoristaions)
            {
                i++;
                members = new HashSet<mMember>();
                members.Add(oc.mMember);

                string idx = pageColumnNamer.PageColumnIdx(oc);

                pcf = new PageColumnFactory(oc.mDimension, members, idx, true);
                pageColumns.Add(pcf.getColumn());
            }
            return pageColumns;
        }   

        private IEnumerable<IRelationalColumn> getRCcolumns(IEnumerable<mAxisOrdinate> xOrdinates, IEnumerable<mAxisOrdinate> yOrdinates)
        {
            HashSet<IRelationalColumn> rcColumns = new HashSet<IRelationalColumn>();
            RcColumnFactory rcf;
            foreach (mAxisOrdinate xao in xOrdinates)
            {
                foreach (mAxisOrdinate yao in yOrdinates)
                {
                    rcf = new RcColumnFactory(xao, yao, _cellShadeChecker, _taxTable);
                    rcColumns.Add(rcf.getColumn());
                }
            }
            return rcColumns;
        }
        private IEnumerable<IRelationalColumn> getRCcolumns(IEnumerable<mAxisOrdinate> ordinates)
        {
            HashSet<IRelationalColumn> rcColumns = new HashSet<IRelationalColumn>();
            RcColumnFactory rcf;

            foreach (mAxisOrdinate xao in ordinates)
            {
                rcf = new RcColumnFactory(xao, null, _cellShadeChecker, _taxTable);
                rcColumns.Add(rcf.getColumn());
            }

            return rcColumns;
        }

        private void validateCodesOfOrdinates(HashSet<mAxisOrdinate> ordinates)
        {
            foreach (mAxisOrdinate ao in ordinates)
            {
                if (string.IsNullOrWhiteSpace(ao.OrdinateCode))
                    throw new ArgumentException("No ordinate code in ordinate " + ao.OrdinateID);
            }
        }

        //new

        #region ExcelTemplate Generetion


        private IEnumerable<IRelationalColumn> getRCcolumnsForExcelTemplate(IEnumerable<mAxisOrdinate> xOrdinates, IEnumerable<mAxisOrdinate> yOrdinates)
        {
            HashSet<IRelationalColumn> rcColumns = new HashSet<IRelationalColumn>();
            RcColumnFactory rcf;
            foreach (mAxisOrdinate xao in xOrdinates)
            {
                foreach (mAxisOrdinate yao in yOrdinates)
                {
                    rcf = new RcColumnFactory(xao, yao, _cellShadeChecker, _taxTable, true);
                    rcColumns.Add(rcf.getColumn());
                }
            }
            return rcColumns;
        }
        private IEnumerable<IRelationalColumn> getRCcolumnsForExcelTemplate(IEnumerable<mAxisOrdinate> ordinates)
        {
            HashSet<IRelationalColumn> rcColumns = new HashSet<IRelationalColumn>();
            RcColumnFactory rcf;

            foreach (mAxisOrdinate xao in ordinates)
            {
                rcf = new RcColumnFactory(xao, null, _cellShadeChecker, _taxTable, true);
                rcColumns.Add(rcf.getColumn());
            }

            return rcColumns;
        }

        public ExcelTemplateColumns getcolumns(mTable tableName,DataTable dt)
        {

            HashSet<mAxisOrdinate> xOrdinates = getXordinates();
            HashSet<mAxisOrdinate> yOrdinates = getYordinates();
            HashSet<mAxisOrdinate> zOrdinates = getZordinates();
            HashSet<mAxi> openZaxes = getZaxes();
            HashSet<IRelationalColumn> columns = new HashSet<IRelationalColumn>();

                       

            if (xOrdinates.All(x => x.mAxi.IsOpenAxis == true) || yOrdinates.All(x => x.mAxi.IsOpenAxis == true))
                unionColumns(columns, getRCcolumnsForExcelTemplate(xOrdinates.Union(yOrdinates)));
            else if ((xOrdinates.Any(x => x.mAxi.IsOpenAxis == true) && yOrdinates.Count > 0) ||
                    (yOrdinates.Any(x => x.mAxi.IsOpenAxis == true) && xOrdinates.Count > 0))
            {
                unionColumns(columns, getRCcolumnsForExcelTemplate(xOrdinates.Where(x => x.mAxi.IsOpenAxis != true), yOrdinates.Where(x => x.mAxi.IsOpenAxis != true)));
               
            }
            else if (xOrdinates.Count > 0 && yOrdinates.Count > 0 && xOrdinates.All(x => x.mAxi.IsOpenAxis != true) && yOrdinates.All(x => x.mAxi.IsOpenAxis != true))
                unionColumns(columns, getRCcolumnsForExcelTemplate(xOrdinates, yOrdinates));
            else if (xOrdinates.Count > 0 && yOrdinates.Count == 0)
                unionColumns(columns, getRCcolumnsForExcelTemplate(xOrdinates));
            else if (yOrdinates.Count > 0 && xOrdinates.Count == 0)
                unionColumns(columns, getRCcolumnsForExcelTemplate(yOrdinates));
            else
                throw new ArgumentException("Case not covered in table " + this.RelationalTableName);

            

            columns = filterColumns(columns);
            ;
           
          
            List<string> dataTypes = new List<string>();
            List<string> xOrdinateNames = new List<string>();
            List<string> yOrdinateNames = new List<string>(); 
            List<string> columnCodes = new List<string>();
            foreach (IRelationalColumn relationalColumn in columns)
            {                
                columnCodes.Add(relationalColumn.getColumnName());
                xOrdinateNames.Add(relationalColumn.getColumnNameForExcelTemplate());
                yOrdinateNames.Add(relationalColumn.getRowNameForExcelTemplate());
                if (relationalColumn.getDataType().ToString() == "Enumeration")
                {
                    //Code changes to handle the Metrics
                    //dataTypes.Add(string.Concat("E:", relationalColumn.getHierarchyIdForExcelTemplate().ToString()));
                    dataTypes.Add(string.Concat("E:", relationalColumn.getHierarchyIdForExcelTemplateWithMetrics().ToString()));
                }
                else
                    dataTypes.Add(relationalColumn.getDataType().ToString());
            }         
           
            foreach(DataRow row in dt.Rows)
            {
                if (row["TableCode"] != DBNull.Value)
                {
                    if(row["TableCode"].ToString()==tableName.TableCode)
                    {
                        if (row["PAGECOLUMN"] != DBNull.Value)
                            columnCodes.Add(row["PAGECOLUMN"].ToString());
                        if (row["PAGECOLUMN_LABEL"] != DBNull.Value)
                            yOrdinateNames.Add(row["PAGECOLUMN_LABEL"].ToString());
                        if (row["PAGECOLUMN_DATATYPE"] != DBNull.Value)
                            dataTypes.Add(row["PAGECOLUMN_DATATYPE"].ToString());
                        if (row["PAGECOLUMN_CODE"] != DBNull.Value)
                            xOrdinateNames.Add(row["PAGECOLUMN_CODE"].ToString());

                    }
                } 
            }
           
            ExcelTemplateColumns obj = new ExcelTemplateColumns();
            obj.Rows = xOrdinateNames;
            obj.columns = yOrdinateNames;
            obj.ColumnsCodes = columnCodes;           
            obj.DataTypes = dataTypes;

            return obj;
        }

        #endregion

    }
}
