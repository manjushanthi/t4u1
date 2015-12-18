# -*- coding: utf-8 -*-
"""
:mod:`re.RefManager`
~~~~~~~~~~~~~~~~~~~
Edgar(tm) Renderer was created by staff of the U.S. Securities and Exchange Commission.
Data and content created by government employees within the scope of their employment 
are not subject to domestic copyright protection. 17 U.S.C. 105.
"""

import os.path, re, lxml
import arelle.ModelDocument
from arelle.FileSource import openFileSource
from arelle import PythonUtil # define 2.x or 3.x string types
PythonUtil.noop(0) # Get rid of warning on PythonUtil import
from . import ErrorMgr

taxonomyManagerFile = 'TaxonomyAddonManager.xml'

"""
The Add on manager is a hold over from RE2.  The purpose is to load standard taxonomy doc and ref
files without requiring them to be in the DTS during validation.  There is a configuration file that
maps schema file names to all associated documentation and reference file names. After a document
is loaded, then each schema in the dts is checked to see whether its associated doc and/or ref files
(there could be zero, one, or more of each) need to be loaded.
"""

class RefManager(object):

    def __init__(self,resources):
        managerPath = os.path.join(resources,taxonomyManagerFile)
        self.tree = lxml.etree.parse(managerPath)

    # method getUrls on CntlrAddOnManager
    # returns: set of strings representing additional linkbases to be loaded.
    # return the set of URLs that must be loaded due to the presence of schemas in the DTS.
    def getUrls(self,modelXbrl): 
        urls = set()
        from urllib.parse import urlparse,urljoin
        namespacesInFacts = {f.qname.namespaceURI for f in modelXbrl.facts}
        for fileUri,doc in modelXbrl.urlDocs.items():
            if doc.targetNamespace in namespacesInFacts:
                parsedUri = urlparse(fileUri)
                fileBasename = os.path.basename(parsedUri.path)
                if re.compile('.*\.xsd$').match(fileBasename): # Assume we only care about urls ending in .xsd
                    xp = "/TaxonomyAddonManager/TaxonomyList/TaxonomyAddon[Taxonomy[.='" + fileBasename + "']]/*/string"
                    moreUrls = self.tree.xpath(xp)
                    for u in moreUrls:
                        urls.add(urljoin(fileUri,u.text))
        return urls

    def loadAddedUrls(self,modelXbrl,controller):
        mustClear = False
        urls = self.getUrls(modelXbrl)
        # load without SEC/EFM validation (doc file would not be acceptable)
        priorValidateDisclosureSystem = modelXbrl.modelManager.validateDisclosureSystem
        modelXbrl.modelManager.validateDisclosureSystem = False
        for url in urls:
            doc = None
            try: # isDiscovered is needed here to force the load.
                doc = arelle.ModelDocument.load(modelXbrl, url, isDiscovered=True) 
            except (arelle.ModelDocument.LoadingException):
                pass
            if doc is None:
                message = ErrorMgr.getError('UNABLE_TO_LOAD_ADDON_LINKBASE')
                controller.logWarn(message.format(url))
            else:
                mustClear = True
        modelXbrl.modelManager.validateDisclosureSystem = priorValidateDisclosureSystem
        if mustClear:
            # Code comment in Arelle's own loader says this is necessary but I don't think it is.
            # modelXbrl.relationshipSets.clear() 
            pass
        return
