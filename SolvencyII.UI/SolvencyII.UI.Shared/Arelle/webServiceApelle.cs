using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using SolvencyII.UI.Shared.Extensions;
using Ionic.Zip;

namespace SolvencyII.UI.Shared.Arelle
{
    public class WebServiceApella
    {
        private readonly string _apelleWebserviceURL = "http://{0}/rest/xbrl/";
        private const string APELLE_WEBSERVICE_PARAM = "/validation/xbrl?media=text";

        #region Delegates

        public delegate void CoreResponse(bool success, HttpStatusCode statusCode, string fileName, string error);

        // public delegate void StringResponse(string response);

        #endregion

        #region Events

        public event CoreResponse SyncResponse;

        private void RaiseManagementResponse(bool success, HttpStatusCode statusCode, string fileName, string error = "")
        {
            if (SyncResponse != null)
                SyncResponse(success, statusCode, fileName, error);
        }

        #endregion

        public WebServiceApella(string remoteURL)
        {
            _apelleWebserviceURL = string.Format(_apelleWebserviceURL, remoteURL);
        }

        public bool Validate(string fileUrl, out string errors)
        {
            string serviceUri = Url(fileUrl);

            try
            {
                HttpWebRequest req = WebRequest.Create(serviceUri) as HttpWebRequest;

                req.Method = "POST";
                req.ContentType = "application/zip";

                Stream binStream = new FileStream(fileUrl, FileMode.Open, FileAccess.Read);
                ZipFile zipFile = new ZipFile();
                zipFile.AddEntry(Path.GetFileName(fileUrl), binStream);
                Stream requestStream = req.GetRequestStream();
                zipFile.Save(requestStream);
                requestStream.Close();
                binStream.Close();

                using (HttpWebResponse response = req.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    StringBuilder sb = new StringBuilder();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                        sb.AppendLine(line);
                    if (sb.Length > 0)
                    {
                        var regex = "\\[(xbrl.*)?\\].*"; // This works: \[(xbrl.*)?\].*
                        var match = Regex.Match(sb.ToString(), regex, RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            // Matches - so we have an error
                            errors = sb.ToString();
                            return false;
                        }
                    }
                    errors = "";
                    return true;
                }
            }
            catch (WebException e)
            {
                HttpWebResponse res = (HttpWebResponse) e.Response;
                if (res != null)
                    errors = res.StatusDescription;
                else
                    errors = e.Message;
            }
            catch (Exception e)
            {
                errors = e.Message;
            }
            return false;
        }

        public void ValidateThreaded(string fileUrl)
        {
            string serviceUri = Url(fileUrl);
            try
            {
                HttpWebRequest req = WebRequest.Create(serviceUri) as HttpWebRequest;
                using (HttpWebResponse response = req.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    StringBuilder sb = new StringBuilder();
                    bool error = false;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                        sb.AppendLine(line);
                    if(sb.Length > 0)
                    {
                        var regex = @"\[(xbrl.*)?\].*";
                        var match = Regex.Match(sb.ToString(), regex, RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            // Matches - so we have an error
                            error = true;
                        }
                    }
                    RaiseManagementResponse(response.StatusCode == HttpStatusCode.OK, !error ? response.StatusCode : HttpStatusCode.ExpectationFailed, fileUrl, sb.ToString());
                }
            }
            catch (WebException e)
            {
                HttpWebResponse res = (HttpWebResponse)e.Response;
                if(res != null)
                    RaiseManagementResponse(false, res.StatusCode, fileUrl, res.StatusDescription);
                else
                    RaiseManagementResponse(false, HttpStatusCode.NotFound, fileUrl, e.Message);
                
            }
            catch (Exception)
            {
                RaiseManagementResponse(false, HttpStatusCode.NonAuthoritativeInformation, fileUrl, "An error occured during the validation. Please try again later.");
            }

        }

        private string Url(string fileURL)
        {
            return string.Format("{0}{1}{2}", _apelleWebserviceURL, Path.GetFileName(fileURL), APELLE_WEBSERVICE_PARAM);
            // http://localhost:8080/rest/xbrl/" + "C:/Users/John%20Doe/Samples/instance0010000.xbrl" + "/validation/xbrl?media=text";
        }
    
    }
}



