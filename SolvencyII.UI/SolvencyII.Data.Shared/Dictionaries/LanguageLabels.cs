using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Data.SQLite;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using System.Text.RegularExpressions;

namespace SolvencyII.Data.Shared.Dictionaries
{
    /// <summary>
    /// Manage multiple languages on the t4u ui. All words are found in the database.
    /// </summary>
    public static class LanguageLabels
    {
        private static int _applicationID;
        private static eLanguageID _languageID;
        private static Dictionary<int, string> _dataSource;
        private static Dictionary<int, string> _defaultDataSource;

        /// <summary>
        /// Retrun the application label in the selected language.
        /// If language is not available return it in English.
        /// In not avaialble in english return the paramater default label (if exist->differwent than "=")
        /// If not label availble at all return unkown label.
        /// </summary>
        /// <param name="interfaceComponentID"></param>
        /// <param name="defaultLabel"></param>
        /// <returns></returns>
        public static string GetLabel(int interfaceComponentID, string defaultLabel = "unkown label")
        {
            //If the settuing has changed since last request
            if (_applicationID != StaticSettings.ApplicationID || _languageID != StaticSettings.FormLanguage)
            {
                _dataSource = null; // Change in ID so need to requery.
                _applicationID = StaticSettings.ApplicationID;
                _languageID = StaticSettings.FormLanguage;
            }

            if (_dataSource == null)
            {
                if (StaticSettings.DataTier == eDataTier.SqLite)
                {
                    if (!string.IsNullOrEmpty(StaticSettings.SolvencyIITemplateDBConnectionString))
                    {
                        _dataSource = BuildDataSource(StaticSettings.SolvencyIITemplateDBConnectionString);
                    }
                }
                else
                {
                    _dataSource = BuildDataSource();
                }
            }
            string result;

            if (_dataSource != null && _dataSource.ContainsKey(interfaceComponentID))
            {
                result = _dataSource[interfaceComponentID];
                //if not available, then taking from defalut language. 
                if (string.IsNullOrEmpty(result))
                {
                    result = DefaultLanguageLookup(interfaceComponentID, defaultLabel);
                }
            }
            else
            {
                result = DefaultLanguageLookup(interfaceComponentID, defaultLabel);
            }

            if (string.IsNullOrEmpty(result))
            {
                result = "unkown label";
            }

            //TODO: Just its a fix, Once the Localization is completed need to remove this check
            if (result.Contains('\\'))
            {
                 result = result.Replace("\\n", "\n");
            }
             
            

           
            return result;
            
        }

        private static string DefaultLanguageLookup(int interfaceComponentID, string defaultLabel)
        {
            string result = null;
            if (!string.IsNullOrEmpty(StaticSettings.SolvencyIITemplateDBConnectionString))
            {
                if (_defaultDataSource == null) _defaultDataSource = BuildDefaultDataSource(StaticSettings.SolvencyIITemplateDBConnectionString);
            }

            if (_defaultDataSource != null)
            {
                if (!_defaultDataSource.TryGetValue(interfaceComponentID, out result))
                    result = defaultLabel; 
            }
            else
                result = defaultLabel;
            return result;
        }

        private static Dictionary<int, string> BuildDataSource(string conString = "")
        {
            GetSQLData getData = null;
            try
            {
                if (!string.IsNullOrEmpty(conString))
                    getData = new GetSQLData(conString);
                else
                    getData = new GetSQLData();
                List<LanguageLabel> result = getData.GetLanguageDictionary(_applicationID, _languageID);
                return result.ToDictionary(k => k.LabelID, v => v.LabelText);
            }
            catch (SQLiteException)
            {
                return null;
            }
            finally
            {
                if (getData != null)
                    getData.Dispose();
            }
        }


        private static Dictionary<int, string> BuildDefaultDataSource(string constring)
        {
            try
            {
                using (GetSQLData getData = new GetSQLData(constring))
                {
                    List<LanguageLabel> result = getData.GetLanguageDictionary(_applicationID, eLanguageID.InEnglish);
                    return result.ToDictionary(k => k.LabelID, v => v.LabelText);
                }
            }
            catch (SQLiteException)
            {
                return null;
            }
        }

    }
}
