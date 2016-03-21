using System;
using System.IO;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.Domain.Configuration;
using SolvencyII.UI.Shared.Arelle;
using SolvencyII.UI.Shared.Configuration;

namespace SolvencyII.UI.Shared.ImpExpVal
{
    public class ImportExportValuationAsync
    {
        
        #region Events

        public enum ResponseType
        {
            Import,
            Export,
            Validation
        }

        public delegate void Response(bool success, string message, ResponseType type);

        public Response AsyncResponse;


        private void SendResponse(bool success, string message, ResponseType type)
        {
            if (AsyncResponse != null)
                AsyncResponse(success, message, type);
        }

        #endregion

        public void ValuationAsync(string sourceXBRL)
        {
            try
            {
                string resultValidate = "";
                bool passed = false;

                // This one does get run.

                // Remote access
                WebServiceApella apella = new WebServiceApella(RegSettings.RemoteValidationURL);
                passed = apella.Validate(sourceXBRL, out resultValidate);
                SendResponse(passed, resultValidate, ResponseType.Validation);
            }
            catch (Exception exception)
            {
                SendResponse(false, string.Format("{0}\n{1}", LanguageLabels.GetLabel(82, "Unfortunately there was an error:"), exception.Message), ResponseType.Validation);
            }
        }

    }
}
