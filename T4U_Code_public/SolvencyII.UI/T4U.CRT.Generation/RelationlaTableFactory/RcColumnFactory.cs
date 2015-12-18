using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AT2DPM.DAL.Model;
using AT2DPM.DAL.ExtendedModel;
using AT2DPM.DAL;
using DpmDB.BusinessData;
using T4U.CRT.Generation.Model;
using T4U.CRT.Generation.ExcelTemplateProcessor;
using System.Data;


namespace T4U.CRT.Generation.RelationlaTableFactory
{
    public class RcColumnFactory : IRelationalColumnFactory
    {
        private mAxisOrdinate xOrdinate;
        private mAxisOrdinate yOrdinate;
        private mTable table;
        private ICellShadeChecker shadeChecker;

        //New
        private int hierarchyId=0;
        private bool forExcelTemplateGeneretion = false;
        private string xOrdinateName = string.Empty;
        private string yOrdinateName = string.Empty;
        private int HierarchyStartingMemberID = 0;
        private int IsStartingMemberIncluded = 0;


        public RcColumnFactory(mAxisOrdinate xordinate, mAxisOrdinate yordinate, ICellShadeChecker cellShadeChecker, mTable taxonomyTable)
        {            
            this.xOrdinate = xordinate;
            this.yOrdinate = yordinate;
            this.table = taxonomyTable;
            this.shadeChecker = cellShadeChecker;
        }

        //New Contructor 
        public RcColumnFactory(mAxisOrdinate xordinate, mAxisOrdinate yordinate, ICellShadeChecker cellShadeChecker, 
            mTable taxonomyTable, bool forExcelTemplateGeneretion)
        {
            this.xOrdinate = xordinate;
            this.yOrdinate = yordinate;
            this.table = taxonomyTable;
            this.shadeChecker = cellShadeChecker;
            this.forExcelTemplateGeneretion = forExcelTemplateGeneretion;

            if (this.xOrdinate != null)
            {
                this.xOrdinateName = xordinate.OrdinateLabel;

                var results = (from m in HDOrdinateHierarchyIdList.HDOrdinateHierarchyIdListTable.AsEnumerable()
                               where Convert.ToInt32(m["OrdinateID"].ToString()) == this.xOrdinate.OrdinateID
                               select m).FirstOrDefault();
                
                if (results != null)                
                    this.hierarchyId = Convert.ToInt32(results["hierarchyId"]);
                //Code changes to handle the Metrics
                if (results != null)
                {
                    this.HierarchyStartingMemberID = results["HierarchyStartingMemberID"] == DBNull.Value ? 0 : Convert.ToInt32(results["HierarchyStartingMemberID"]);
                    //this.IsStartingMemberIncluded = results["IsStartingMemberIncluded"] == DBNull.Value ? 0 : Convert.ToInt32(results["IsStartingMemberIncluded"]);
                    if (results["IsStartingMemberIncluded"] == DBNull.Value)
                        this.IsStartingMemberIncluded = 0;
                    else
                        if (results["IsStartingMemberIncluded"].ToString().Trim() == "FALSE")
                            this.IsStartingMemberIncluded = 0;
                        else if (results["IsStartingMemberIncluded"].ToString().Trim() == "TRUE")
                            this.IsStartingMemberIncluded = 1;

                }                    

                if (this.hierarchyId == 0)
                {
                    results = (from m in MDOrdinateHierarchyIdList.MDOrdinateHierarchyIdTable.AsEnumerable()
                               where Convert.ToInt32(m["OrdinateID"].ToString()) == this.xOrdinate.OrdinateID
                               select m).FirstOrDefault();
                    if (results != null)
                        this.hierarchyId = Convert.ToInt32(results["hierarchyId"]);

                   // Code changes to handle the Metrics
                    if (results != null)
                    {
                        //int t1 = Convert.ToInt32(results["hierarchyId"]);
                        this.HierarchyStartingMemberID = results["HierarchyStartingMemberID"] == DBNull.Value ? 0 : Convert.ToInt32(results["HierarchyStartingMemberID"]);
                        //this.IsStartingMemberIncluded = results["IsStartingMemberIncluded"] == DBNull.Value ? 0 : Convert.ToInt32(results["IsStartingMemberIncluded"]);
                        if (results["IsStartingMemberIncluded"] == DBNull.Value)
                            this.IsStartingMemberIncluded = 0;
                        else
                            if (results["IsStartingMemberIncluded"].ToString().Trim()=="FALSE")
                                this.IsStartingMemberIncluded = 0;
                            else if (results["IsStartingMemberIncluded"].ToString().Trim() == "TRUE")
                                this.IsStartingMemberIncluded = 1;
                       

                    }
                   

                }
            }

            if (this.yOrdinate != null)
            {
                this.yOrdinateName = yordinate.OrdinateLabel;
                if (this.hierarchyId == 0)
                {
                    var results = (from m in HDOrdinateHierarchyIdList.HDOrdinateHierarchyIdListTable.AsEnumerable()
                                   where Convert.ToInt32(m["OrdinateID"].ToString()) == this.yOrdinate.OrdinateID
                                   select m).FirstOrDefault();
                    if (results != null)
                        this.hierarchyId = Convert.ToInt32(results["hierarchyId"]);
                    //Code changes to handle the Metrics
                    if (results != null)
                    {
                        this.HierarchyStartingMemberID = results["HierarchyStartingMemberID"] == DBNull.Value ? 0 : Convert.ToInt32(results["HierarchyStartingMemberID"]);
                        //this.IsStartingMemberIncluded = results["IsStartingMemberIncluded"] == DBNull.Value ? 0 : Convert.ToInt32(results["IsStartingMemberIncluded"]);
                        if (results["IsStartingMemberIncluded"] == DBNull.Value)
                            this.IsStartingMemberIncluded = 0;
                        else
                            if (results["IsStartingMemberIncluded"].ToString().Trim() == "FALSE")
                                this.IsStartingMemberIncluded = 0;
                            else if (results["IsStartingMemberIncluded"].ToString().Trim() == "TRUE")
                                this.IsStartingMemberIncluded = 1;

                    }
                }

                if (this.hierarchyId == 0)
                {
                    var results = (from m in MDOrdinateHierarchyIdList.MDOrdinateHierarchyIdTable.AsEnumerable()
                                   where Convert.ToInt32(m["OrdinateID"].ToString()) == this.yOrdinate.OrdinateID
                                   select m).FirstOrDefault();
                    if (results != null)
                        this.hierarchyId = Convert.ToInt32(results["hierarchyId"]);

                    //Code changes to handle the Metrics
                    if (results != null)
                    {
                        this.HierarchyStartingMemberID = results["HierarchyStartingMemberID"] == DBNull.Value ? 0 : Convert.ToInt32(results["HierarchyStartingMemberID"]);
                        //this.IsStartingMemberIncluded = results["IsStartingMemberIncluded"] == DBNull.Value ? 0 : Convert.ToInt32(results["IsStartingMemberIncluded"]);
                        if (results["IsStartingMemberIncluded"] == DBNull.Value)
                            this.IsStartingMemberIncluded = 0;
                        else
                            if (results["IsStartingMemberIncluded"].ToString().Trim() == "FALSE")
                                this.IsStartingMemberIncluded = 0;
                            else if (results["IsStartingMemberIncluded"].ToString().Trim() == "TRUE")
                                this.IsStartingMemberIncluded = 1;

                    }
                }
            }
        }
        
        private RcColumn column;

        public IRelationalColumn getColumn()
        {
            if (column == null)
                produceColumn();

            return column;
        }

        private void produceColumn()
        {
            string rowCode = null;
            string columnCode = null;
                        
            if(this.yOrdinate != null)
                rowCode = this.yOrdinate.OrdinateCode.Trim();
            if(this.xOrdinate != null)
                columnCode = this.xOrdinate.OrdinateCode.Trim();

            if (this.isGeyedOut())
            {
                column = null;
                return;
            }

            if (forExcelTemplateGeneretion==false)
             column = new RcColumn(rowCode, columnCode);
            else
                column = new RcColumn(rowCode, columnCode, hierarchyId, xOrdinateName, yOrdinateName,HierarchyStartingMemberID,IsStartingMemberIncluded);
            produceIsKey();
            produceDimCharacteristics();
            produceDataType();

            if ((xOrdinate == null && yOrdinate != null && yOrdinate.mOrdinateCategorisations.Count() == 1
                && yOrdinate.mOrdinateCategorisations.First().DimensionMemberSignature.Contains("*?"))
                ||
                (yOrdinate == null && xOrdinate != null && xOrdinate.mOrdinateCategorisations.Count() == 1
                && xOrdinate.mOrdinateCategorisations.First().DimensionMemberSignature.Contains("*?")))
                column.forceOrigin(ProcesorGlobals.ContextOrigin);
        }

        private void produceIsKey()
        {
            bool isKey = false;

            if (xOrdinate != null && xOrdinate.IsRowKey != null && xOrdinate.IsRowKey == true)
                isKey = true;
            if (yOrdinate != null && yOrdinate.IsRowKey != null && yOrdinate.IsRowKey == true)
                isKey = true;
            else if (yOrdinate != null)
                isKey = false;

            this.column.setIsKey(isKey);
        }

        private void produceDataType()
        {
            if(this.column == null)            
                throw new NullReferenceException("no column produced");            

            mMetric metric = this.findMetricFromOrdinates();

            string datatype = "";
            if (metric == null)
                metric = this.findMetricFromTable();

            if (metric == null)
                datatype = this.findDataTypeFromDomain();
            if (metric == null && string.IsNullOrEmpty(datatype))
                if(xOrdinate != null && xOrdinate.mOrdinateCategorisations.Any(x=>x.DimensionMemberSignature.Contains("*?")))
                    datatype = "Enumeration";
                else if (yOrdinate != null && yOrdinate.mOrdinateCategorisations.Any(x => x.DimensionMemberSignature.Contains("*?")))
                    datatype = "Enumeration";
                else
                    datatype = "String";
            if (string.IsNullOrEmpty(datatype) && metric != null)
                datatype = metric.DataType;

            this.column.setDataType(this.mapDataType(datatype));
        }

        private string findDataTypeFromDomain()
        {
            List<mOrdinateCategorisation> ordCats = new List<mOrdinateCategorisation>();
            if (this.xOrdinate != null)
                ordCats = xOrdinate.mOrdinateCategorisations.ToList();
            if (this.yOrdinate != null)
                ordCats = ordCats.Union(yOrdinate.mOrdinateCategorisations).ToList();

            mOrdinateCategorisation ordCat = null;
            if (ordCats.Count != 1)
                return "";
            ordCat = ordCats[0];

            if (ordCat.mDimension.IsTypedDimension == true)
                return ordCat.mDimension.mDomain.DataType;

            return "";
        }

        private IRelationColumnDataType mapDataType(string dataTypeString)
        {
            switch (dataTypeString.Trim().ToLower())
            {
                case "monetary":
                    return IRelationColumnDataType.Monetary;
                case "string":
                    return IRelationColumnDataType.String;                
                case "percentage":
                    return IRelationColumnDataType.Percentage;
                case "boolean":
                    return IRelationColumnDataType.Boolean;
                case "date":
                    return IRelationColumnDataType.Date;
                case "integer":
                    return IRelationColumnDataType.Integer;
                case "percentage/decimal":
                    return IRelationColumnDataType.Percentage;
                case "decimal":
                    return IRelationColumnDataType.Decimal;
                case "domainbased":
                    return IRelationColumnDataType.Enumeration;
                case "text":
                    return IRelationColumnDataType.String;
                case "enumeration":
                    return IRelationColumnDataType.Enumeration;
                case "code":
                    return IRelationColumnDataType.Enumeration;
                case "enumeration/code":
                    return IRelationColumnDataType.Enumeration;
                case "true":
                    return IRelationColumnDataType.Boolean;
                case "uri":
                    return IRelationColumnDataType.String;
            }

            return IRelationColumnDataType.String;
        }

        private mMetric findMetricFromTable()
        {
            mMetric result = (from ax in this.table.mTableAxis.Where(x => x.mAxi.AxisOrientation.Equals("Z"))
                    from ao in ax.mAxi.mAxisOrdinates
                    from oc in ao.mOrdinateCategorisations
                    where oc.mMember != null && oc.mMember.mMetrics.Count > 0 && oc.Source.Trim().Equals("MD")
                    select oc.mMember.mMetrics.First()).SingleOrDefault();

            return result;
        }

        private mMetric findMetricFromOrdinates()
        {
            List<mOrdinateCategorisation> ordCats = new List<mOrdinateCategorisation>();
            if (this.xOrdinate != null)
                ordCats = xOrdinate.mOrdinateCategorisations.ToList();
            if (this.yOrdinate != null)
                ordCats = ordCats.Union(yOrdinate.mOrdinateCategorisations).ToList();

            foreach (mOrdinateCategorisation ordCat in ordCats)
            {
                if (ordCat.mMember != null && ordCat.mMember.mMetrics != null 
                    && ordCat.mMember.mMetrics.Count > 0 && (string.IsNullOrWhiteSpace(ordCat.Source) || ordCat.Source.Equals("MD")))
                    return ordCat.mMember.mMetrics.First();                
            }

            return null;
        }

        private bool isGeyedOut()
        {
            if (shadeChecker.IsGreyedOut(
                this.xOrdinate == null ? -1 : this.xOrdinate.OrdinateID, 
                this.yOrdinate == null ? -1 : this.yOrdinate.OrdinateID))
                return true;

            return false;
        }

        private static mTableCell IsCommonCellGreyedOut(mAxisOrdinate  Xord, mAxisOrdinate Zord)
        {
            if (Xord == null)
                return null;

            foreach (mTableCell tc in Xord.mTableCells)
            {
                if (tc.IsShaded != true)
                    continue;

                if (Zord == null)
                    return tc;

                foreach (mAxisOrdinate ao in tc.mAxisOrdinates)
                {
                    if (ao.Equals(Zord))
                        return tc;
                }
            }
            return null;
        }

        private void produceDimCharacteristics()
        {
            if (xOrdinate == null && yOrdinate != null && yOrdinate.mOrdinateCategorisations.Count() == 1
                && yOrdinate.mOrdinateCategorisations.First().DimensionMemberSignature.Contains("["))
                produceDimCharFromAxis(yOrdinate.mAxi);
            else if (yOrdinate == null && xOrdinate != null && xOrdinate.mOrdinateCategorisations.Count() == 1
                && xOrdinate.mOrdinateCategorisations.First().DimensionMemberSignature.Contains("["))
                produceDimCharFromAxis(xOrdinate.mAxi);
            else
                produceDimCharFromOrdCategorisations();

            //TODO: take care of hierarchies
            //produceDimCharacteristicsFromHierarchies();
        }

        private void produceDimCharFromAxis(mAxi mAxi)
        {
            if (mAxi == null || mAxi.mOpenAxisValueRestrictions.Count() != 1)
                throw new ArgumentException("Invalid axis");

            mOpenAxisValueRestriction oavr = mAxi.mOpenAxisValueRestrictions.First();
            mOAVRHelper help = new mOAVRHelper();
            mDimension dim = help.findDimension(oavr);
            HashSet<mMember> members = help.findMembers(oavr);

            DimCharacteristicsFactory dcf = new DimCharacteristicsFactory();

            foreach (mMember mem in members)            
                column.addDimCharacteristic(dcf.produceDimCharacteristic(dim, mem));            
        }

        private void produceDimCharFromOrdCategorisations()
        {
            HashSet<mOrdinateCategorisation> ordCats = new HashSet<mOrdinateCategorisation>();

            if (this.yOrdinate != null)
                ordCats = new HashSet<mOrdinateCategorisation>(ordCats.Union(this.yOrdinate.mOrdinateCategorisations));

            if (this.xOrdinate != null)
                ordCats = new HashSet<mOrdinateCategorisation>(ordCats.Union(this.xOrdinate.mOrdinateCategorisations));

            DimCharacteristicsFactory dcf = new DimCharacteristicsFactory();

            foreach (mOrdinateCategorisation oc in 
                ordCats.Where(x => x.Source == null || x.Source.Trim().ToUpper() != "HD").OrderBy(x => x.mDimension.DimensionCode))
            {
                if (oc.mDimension.DimensionCode.ToUpper().Contains(ProcesorGlobals.MetDimCode.ToUpper())
                    || oc.mDimension.DimensionCode.ToUpper().Contains(ProcesorGlobals.AtyDimCode.ToUpper()))
                {
                    column.MetDimCharactersitic = dcf.produceDimCharacteristic(oc);
                    continue;
                }

                DimCharacteristic dc = dcf.produceDimCharacteristic(oc);
                column.addDimCharacteristic(dc);
            }
        }        
    }
}
