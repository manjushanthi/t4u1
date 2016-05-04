using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.DBcontrollers;
using SolvencyII.Data.CRT.ETL.DBcontrollers.Loading;
using SolvencyII.Data.CRT.ETL.ETLControllers;
using SolvencyII.Data.CRT.ETL.EtlPerfomers;
using SolvencyII.Data.CRT.ETL.MappingControllers;
using SolvencyII.Data.CRT.ETL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL
{
    public class EtlOperations
    {
        private static List<IEtlEngine> trigeredEngines = new List<IEtlEngine>();

        /// <summary>
        /// Saving XBRl instance
        /// </summary>
        /// <param name="databasepath"></param>
        /// <param name="instanceID"></param>
        public void etlSavingXBRLinstance(string databasepath, int instanceID)
        {
            //Logger.WriteLog(eSeverity.Note, "Method etlSavingXBRLinstance starts execution");
            //Logger.WriteLog(eSeverity.Note, "Method etlSavingXBRLinstance Parameters ");
            //Logger.WriteLog(eSeverity.Note, "Method etlSavingXBRLinstance Parameter => databasepath : " + databasepath);
            //Logger.WriteLog(eSeverity.Note, "Method etlSavingXBRLinstance Parameter => instanceID : " + instanceID);
            IDataConnector dataConnector = new SQLiteConnector(databasepath);
            
            ITableMappingResolver tableResolver = new SQLiteTableMappingResolver(dataConnector);
            IMappingAnalyzer mapAnalyzer = new DimByDimMapingAnalyzer();

            SQLiteFactsNumberReader fnumReader = new SQLiteFactsNumberReader(dataConnector, instanceID);
            SQLiteMappingProvider mapprovider = new SQLiteMappingProvider(dataConnector, mapAnalyzer);

            IExtractor extractor = new SQLiteExtractor(dataConnector, instanceID, mapprovider);
            ITransformer transformer = new SQLiteTransformer(dataConnector, mapprovider);
            ILoader loader = new SQLiteLoader(dataConnector);

            new dMessageCleaner(dataConnector).CleanDmessage(instanceID);
            CrtErrorsRepository errorRepository = new CrtErrorsRepository(dataConnector);

            Relational2dFactETL etl = new Relational2dFactETL(extractor, loader, transformer, fnumReader, errorRepository);
            extractor.checkAndAddFactIdColumn();
            trigeredEngines.Add(etl);

            //Logger.WriteLog(eSeverity.Note, "Opening database connection: " + databasepath);
            dataConnector.openConnection();
            loader.CleanDFacts(instanceID);

            try
            {
                //Logger.WriteLog(eSeverity.Note, "Performing ETL on the DB: " + databasepath);
                etl.PerformEtl(100000);
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(eSeverity.Error, "Exception occured in etlSavingXBRLinstance method with :" + ex.ToString());
                dataConnector.RollbackTransaction();
                dataConnector.closeConnection();
                throw new EtlException("Exception while saving XBRL instance ", ex);
                
            }
            //Logger.WriteLog(eSeverity.Note, "Closing connection on the DB: " + databasepath);
            dataConnector.closeConnection();
            trigeredEngines.Remove(etl);

            return;
        }

        public void etlLoadingXBRLinstance(string databasepath, int instanceID)
        {
            //Logger.WriteLog(eSeverity.Note, "Method etlLoadingXBRLinstance starts execution");
            //Logger.WriteLog(eSeverity.Note, "Method etlLoadingXBRLinstance Parameters ");
            //Logger.WriteLog(eSeverity.Note, "Method etlLoadingXBRLinstance Parameter => databasepath : " + databasepath);
            //Logger.WriteLog(eSeverity.Note, "Method etlLoadingXBRLinstance Parameter => instanceID : " + instanceID);

            IDataConnector dataConnector = new SQLiteConnector(databasepath);
            ITableMappingResolver tableResolver = new SQLiteTableMappingResolver(dataConnector);
            IMappingAnalyzer mapAnalyzer = new QuickDataPointMappingAnalyzer();

            //SQLiteMappingProvider mapprovider = new FilteredSQLiteMappingProvider(instanceID, dataConnector, mapAnalyzer);
            SQLiteMappingProvider mapprovider = new SQLiteMappingProvider(dataConnector, mapAnalyzer);
            
            IExtractor extractor = new SQLiteExtractor(dataConnector, instanceID, mapprovider);
            SQLiteTransformer transformer = new SQLiteTransformer(dataConnector, mapprovider);
            CrtErrorsRepository errorRepository = new CrtErrorsRepository(dataConnector);
            ILoader loader = new CrtRowValidatingLoader(new SQLiteLoader(dataConnector), errorRepository);

            new dMessageCleaner(dataConnector).CleanDmessage(instanceID);

            IETLPerformer etlPerfomer = new FactsByTableEtlPerfomer();
            dFact2RelationalETL etl = new dFact2RelationalETL(extractor, loader, transformer, etlPerfomer);
            extractor.checkAndAddFactIdColumn();
            trigeredEngines.Add(etl);
            dataConnector.openConnection();

            try
            {
                etl.PerformEtl(150000);
            }
            catch (Exception ex)
            {
                dataConnector.RollbackTransaction();
                dataConnector.closeConnection();
                //logger.writelog(eseverity.error, "exception occured in etlloadingxbrlinstance method :" + ex.tostring());
                throw new EtlException("exception while saving xbrl instance "+ex.StackTrace, ex);
            }

            NotMappedFactsMarker nmfm = new NotMappedFactsMarker(dataConnector);
            nmfm.MarkFacts(transformer.NotMappedFctIds);
            nmfm.InsertDmessage(instanceID);
            dataConnector.closeConnection();
            trigeredEngines.Remove(etl);
        }
        
        public void etlSavingXBRLinstanceMSSQL(string connectionString, int instanceID)
        {
            //Logger.WriteLog(eSeverity.Note, "Method etlSavingXBRLinstanceMSSQL starts execution");
            //Logger.WriteLog(eSeverity.Note, "Method etlSavingXBRLinstanceMSSQL Parameters ");
            //Logger.WriteLog(eSeverity.Note, "Method etlSavingXBRLinstanceMSSQL Parameter => connectionString : " + connectionString);
            //Logger.WriteLog(eSeverity.Note, "Method etlSavingXBRLinstanceMSSQL Parameter => instanceID : " + instanceID);
           

            IDataConnector dataConnector = new MSSQLConnector(connectionString);

            //With high memory consumption
            //DataPointMappingAnalyzer mapAnalyzer = new DataPointMappingAnalyzer();
            //with low memory consumption
            //DimByDimMapingAnalyzer mapAnalyzer = new DimByDimMapingAnalyzer();
            //Mixed
            ITableMappingResolver tableResolver = new SQLiteTableMappingResolver(dataConnector);
            IMappingAnalyzer mapAnalyzer = new DimByDimMapingAnalyzer();

            SQLiteFactsNumberReader fnumReader = new SQLiteFactsNumberReader(dataConnector, instanceID);
            SQLiteMappingProvider mapprovider = new SQLiteMappingProvider(dataConnector, mapAnalyzer);

            IExtractor extractor = new SQLiteExtractor(dataConnector, instanceID, mapprovider);
            ITransformer transformer = new SQLiteTransformer(dataConnector, mapprovider);
            ILoader loader = new SQLiteLoader(dataConnector);

            new dMessageCleaner(dataConnector).CleanDmessage(instanceID);
            CrtErrorsRepository errorRepository = new CrtErrorsRepository(dataConnector);

            Relational2dFactETL etl = new Relational2dFactETL(extractor, loader, transformer, fnumReader, errorRepository);
            extractor.checkAndAddFactIdColumn();
            trigeredEngines.Add(etl);

            dataConnector.openConnection();
            loader.CleanDFacts(instanceID);

            try
            {
                etl.PerformEtl(20000);
            }
            catch (Exception ex)
            {
                dataConnector.RollbackTransaction();
                dataConnector.closeConnection();
                //Logger.WriteLog(eSeverity.Error, "Exception occured in etlSavingXBRLinstanceMSSQL method :" + ex.ToString());              
                throw new EtlException("Exception while saving XBRL instance ", ex);
            }

            dataConnector.closeConnection();
            trigeredEngines.Remove(etl);

            return;
        }

        public void etlLoadingXBRLinstanceMSSQL(string connectionString, int instanceID)
        {
            //Logger.WriteLog(eSeverity.Note, "Method etlLoadingXBRLinstanceMSSQL starts execution");
            //Logger.WriteLog(eSeverity.Note, "Method etlLoadingXBRLinstanceMSSQL Parameters ");
            //Logger.WriteLog(eSeverity.Note, "Method etlLoadingXBRLinstanceMSSQL Parameter => connectionString : " + connectionString);
            //Logger.WriteLog(eSeverity.Note, "Method etlLoadingXBRLinstanceMSSQL Parameter => instanceID : " + instanceID);

            IDataConnector dataConnector = new MSSQLConnector(connectionString);

            //With hihg memory consumption
            //DataPointMappingAnalyzer mapAnalyzer = new DataPointMappingAnalyzer();
            //with low memory consumption
            //DimByDimMapingAnalyzer mapAnalyzer = new DimByDimMapingAnalyzer();
            //Mixed
            ITableMappingResolver tableResolver = new SQLiteTableMappingResolver(dataConnector);
            IMappingAnalyzer mapAnalyzer = new HybridMappingAnalyzer(tableResolver);

            SQLiteMappingProvider mapprovider = new SQLiteMappingProvider(dataConnector, mapAnalyzer);

            IExtractor extractor = new SQLiteExtractor(dataConnector, instanceID, mapprovider);
            ITransformer transformer = new SQLiteTransformer(dataConnector, mapprovider);
            ILoader loader = new SQLiteLoader(dataConnector);

            new dMessageCleaner(dataConnector).CleanDmessage(instanceID);

            dFact2RelationalETL etl = new dFact2RelationalETL(extractor, loader, transformer);
            extractor.checkAndAddFactIdColumn();
            trigeredEngines.Add(etl);
            dataConnector.openConnection();

            try
            {
                etl.PerformEtl(20000);
            }
            catch (Exception ex)
            {
                dataConnector.RollbackTransaction();
                dataConnector.closeConnection();
                //Logger.WriteLog(eSeverity.Error, "Exception occured in etlLoadingXBRLinstanceMSSQL method :" + ex.ToString());                
                throw new EtlException("Exception while saving XBRL instance ", ex);
            }

            dataConnector.closeConnection();
            trigeredEngines.Remove(etl);
        }

        public void Cancel()
        {
            foreach (IEtlEngine item in trigeredEngines)
            {
                item.Cancel();
                trigeredEngines.Remove(item);
            }
        }

        public void etlSavingRecords(string databasepath, int instanceID, object[] records)
        { throw new NotImplementedException(); }

        public void etlDeletingRecords(string databasepath, int instanceID, object[] records)
        { throw new NotImplementedException(); }

    }
}
