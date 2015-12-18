'''
StreamingExtensions is a plug-in to both GUI menu and command line/web service
that provides an alternative approach to big instance documents without building a DOM, to save
memory footprint.  lxml iterparse is used to parse the big instance.  ModelObjects are specialized by features
for efficiency and to avoid dependency on an underlying DOM.

(Note that this module is based on a parser target, an alternate based on iterparse is under examples/plugin.)

(c) Copyright 2013 Mark V Systems Limited, All rights reserved.

Calls these plug-in classes:
   Streaming.BlockingPlugin(modelXbrl):  returns name of plug in blocking streaming if it is being blocked, else None
   Streaming.Start(modelXbrl): notifies that streaming is starting for modelXbrl; simulated modelDocument is established
   Streaming.ValidateFacts(modelXbrl, modelFacts) modelFacts are available for streaming processing
   Streaming.Finish(modelXbrl): notifies that streaming is finished
'''

import io, os, time, sys, re, gc
from decimal import Decimal, InvalidOperation
from lxml import etree
from arelle import FileSource, XbrlConst, XmlUtil, XmlValidate, ValidateXbrlDimensions
from arelle.ModelDocument import load, ModelDocument, Type, create as createModelDocument, ModelDocumentReference
from arelle.ModelObjectFactory import parser
from arelle.ModelObject import ModelObject
from arelle.ModelInstanceObject import ModelFact
from arelle.PluginManager import pluginClassMethods
from arelle.Validate import Validate
from arelle.HashUtil import md5hash, Md5Sum
from arelle.plugin.xbrlDB.XbrlSemanticSqlDB import insertIntoDB as insertIntoSemanticSqlDB, XbrlSqlDatabaseConnection

_dbLoaderCheck = True  # check streaming if enabled except for CmdLine, then only when requested

def dbCanLoad(modelXbrl, mappedUrl, normalizedUrl, **kwarg):
    if not _dbLoaderCheck or kwarg["isEntry"] or not modelXbrl.modelManager.disclosureSystem.standardTaxonomyDatabase:
        return False
    if normalizedUrl in getattr(modelXbrl, "_blockLoadFromDbUrlInRecursion", set()):
        return False
    return modelXbrl.modelManager.disclosureSystem.standardTaxonomyUrlPattern.match(normalizedUrl)
    
def dbLoader(modelXbrl, normalizedUrl, filePath, **kwarg):
    # check if big instance and has header with an initial incomplete tree walk (just 2 elements
    if (not _dbLoaderCheck or kwarg["isEntry"] or 
        not modelXbrl.modelManager.disclosureSystem.standardTaxonomyDatabase or
        not modelXbrl.modelManager.disclosureSystem.standardTaxonomyUrlPattern.match(normalizedUrl)):
        return None
    if not hasattr(modelXbrl, "_blockLoadFromDbUrlInRecursion"):
        modelXbrl._blockLoadFromDbUrlInRecursion = set()
    if normalizedUrl in modelXbrl._blockLoadFromDbUrlInRecursion:
        return None
    
    cntlr = modelXbrl.modelManager.cntlr
    
    try:
        db = os.path.join(cntlr.webCache.cacheDir,
                          modelXbrl.modelManager.disclosureSystem.standardTaxonomyDatabase)
        dbConn = XbrlSqlDatabaseConnection(modelXbrl, None, None, None, None, db, 0, "sqlite")
        dbConn.verifyTables()
        
        if not dbConn.execute("SELECT d.document_id FROM document d WHERE d.document_url = '{}'"
                              .format(normalizedUrl)):
            # load normalizedUrl into database
            modelXbrl._blockLoadFromDbUrlInRecursion.add(normalizedUrl)
            filesource = FileSource.openFileSource(filePath, cntlr)
            modelXbrl2 = modelXbrl.modelManager.load(filesource, _("views loading"))
            insertIntoSemanticSqlDB(modelXbrl2, host=None, port=None, user=None, password=None, 
                                    database=db, timeout=5, product="sqlite")
            modelXbrl2.close()
            del modelXbrl2
            modelXbrl._blockLoadFromDbUrlInRecursion.discard(normalizedUrl)
        results = dbConn.execute("SELECT d.document_id, d.document_type, d.namespace "
                                 "FROM document d WHERE d.document_url = '{}'"
                                 .format(normalizedUrl))
        if not results:
            return None
        documentId, documentType, namespace = results[0]
        modelDoc = createModelDocument(
            modelXbrl, 
            Type.typeName.index(documentType),
            normalizedUrl,
            schemaRefs=[],
            isEntry=False)
        results = dbConn.execute(
            "SELECT r.document_id rd.document_type, rd.document_url, rd.namespace "
            "FROM referenced_documents r , document rd "
            "WHERE r.object_id = {} and rd.document_id = r.document_id"
            .format(documentId))
        for r in results:
            refDocId, refDocType, refDocUrl, refDocNamespace = r
            isIncluded = False
            if refDocType == "schema":
                if refDocNamespace == namespace:
                    refType = "include"
                    isIncluded = True
                else:
                    refType = "import"
            elif refDocType == "linkbase":
                refType = "href"
            else:
                continue
            refDoc = load(modelXbrl, refDocUrl, isIncluded=isIncluded)
            modelDoc.referencesDocument[refDoc] = ModelDocumentReference(refType, None)
        # load document type
        if documentType == "schema":
            results = dbConn.execute(
                "SELECT d.xml_id, d.qname, d.base_type, df.qname "
                "FROM data_type d LEFT JOIN data_type df ON d.derived_from_type_id = df.data_type_id "
                "WHERE d.document_id = {}"
                .format(documentId))
            for _id, _qn, _baseType, _derivedFromQn in results:
                print ("data type {} {} {} {}".format(_id, _qn, _baseType, _derivedFromQn))
            results = dbConn.execute(
                "SELECT d.xml_id, d.qname, d.base_type, df.qname "
                "FROM data_type d LEFT JOIN data_type df ON d.derived_from_type_id = df.data_type_id "
                "WHERE d.document_id = {}"
                .format(documentId))
            for _id, _qn, _baseType, _derivedFromQn in results:
                print ("data type {} {} {} {}".format(_id, _qn, _baseType, _derivedFromQn))
            results = dbConn.execute(
                "SELECT a.xml_id, a.qname, a.base_type, dt.qname, sg.qname, "
                "   a.balance, a.period_type, a.abstract, a.nillable "
                "FROM aspect a "
                "LEFT JOIN data_type dt ON dt.data_type_id = a.datatype_id "
                "LEFT JOIN aspect sg ON sg.aspect_id = a.substitution_group_aspect_id "
                "WHERE a.document_id = {} "
                .format(documentId))
            for _id, _qn, _baseType, _derivedFromQn, _sgQn, _bal, _pre, _abs, _nil in results:
                print ("data type {} {} {} {} {} {} {} {} {}".format(_id, _qn, _baseType, _derivedFromQn, _sgQn, _bal, _pre, _abs, _nil ))
        elif documentType == "linkbase":
            pass

    except:
        raise
    startedAt = time.time()
    

    modelXbrl.profileStat(_("Load from DB complete"), time.time() - startedAt)
    return None # modelDocument

def dbLoaderSetup(cntlr, options, **kwargs):
    global _streamingExtensionsCheck, _streamingExtensionsValidate
    # streaming only checked in CmdLine/web server mode if requested
    # _streamingExtensionsCheck = getattr(options, 'check_streaming', False)
    _streamingExtensionsValidate = options.validate

'''
   Do not use _( ) in pluginInfo itself (it is applied later, after loading
'''

__pluginInfo__ = {
    'name': 'Database XBRL Loader',
    'version': '0.9',
    'description': "This plug-in loads XBRL DTS components without building a DOM in memory.  "
                    "Objects of the Abstract Model DB are loaded directly into an object model without a DOM.  ",
    'license': 'Apache-2',
    'author': 'Mark V Systems Limited',
    'copyright': '(c) Copyright 2014 Mark V Systems Limited, All rights reserved.',
    # classes of mount points (required)
    # take out for now: 'CntlrCmdLine.Options': streamingOptionsExtender,
    'CntlrCmdLine.Utility.Run': dbLoaderSetup,
    'ModelDocument.IsPullLoadable': dbCanLoad,
    'ModelDocument.PullLoader': dbLoader,
}
