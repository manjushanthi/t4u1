using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;


namespace SolvencyII.GUI.RSS
{
    public class RssNewsReader
    {
        /// <summary>
        /// To read the RSS link information from the RSSLinkInformation.xml file
        /// </summary>
        /// <returns></returns>
        public List<RssFeed> ReadRSS()
        {
            List<RssFeed> lst = null;
            //If the File RSSLinkInformation exists, get the RSS feed(XML) file link
            if (File.Exists(Path.Combine(System.Windows.Forms.Application.StartupPath, "RSSLinkInformation.xml")))
            {
                XElement rssLinkDetails = XElement.Load(Path.Combine(System.Windows.Forms.Application.StartupPath, "RSSLinkInformation.xml"));
                if (rssLinkDetails != null)
                {
                    //Read the RSS XML link from the element
                    var rssLink = rssLinkDetails.Element("RssLink");
                    string path = rssLink == null ? string.Empty : rssLink.Value;
                    return (Read(path));                   

                }
                
            }
            return (lst);


            
        }

        /// <summary>
        /// Method to read the RSS feed from the url
        /// </summary>
        /// <param name="url">RSS feed URL</param>
        /// <returns>List of RSS feeds</returns>
        public List<RssFeed> Read(string url)
        {
            List<RssFeed> lst = null;
            try
            {
               
                var webClient = new WebClient();
                //Read the RSS file contents from the RSS XML file
                if (!string.IsNullOrEmpty(url))
                {
                    string result = webClient.DownloadString(url);
                    webClient.Encoding = Encoding.GetEncoding("windows-1255");
                    if (!string.IsNullOrEmpty(result))
                    {

                        XDocument document = XDocument.Parse(result);
                        return (from descendant in document.Descendants("item")
                                select new RssFeed()
                                {
                                    Description = descendant.Element("description").Value,
                                    Title = descendant.Element("title").Value,
                                    PublicationDate = descendant.Element("pubDate").Value
                                }).ToList();
                    }
                    else
                    {
                        return (lst);
                    }
                }
                else
                    return (lst);
            }
            catch (System.Xml.XmlException e)
            {
                return (lst);
            }
            catch (Exception e)
            {
                return (lst);
            }
           
        }
        

    }

}
