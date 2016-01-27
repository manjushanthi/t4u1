using System;
using System.IO;
using SolvencyII.Domain.ENumerators;
using SolvencyII.UI.Shared.Registry;

namespace SolvencyII.UI.Shared.Configuration
{
    /// <summary>
    /// Registry management
    /// </summary>
    public static class RegSettings
    {

        private static string _connectionString;
        public static string ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(_connectionString)) return _connectionString;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                return (_connectionString = modifyRegistry.Read("LastDatabasePath") ?? "");
            }
            set
            {
                _connectionString = value;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("LastDatabasePath", value);
            }
        }

        private static eDataTier _dataTier;
        public static eDataTier DataTier
        {
            get
            {
                if (_dataTier > 0) return _dataTier;
                ////Modified
                //if the DataTierType key not found in the Registry.
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                if (modifyRegistry.Read("DataTierType") == null)
                {
                    modifyRegistry.Write("DataTierType", eDataTier.SqLite);
                }
                ////Modified
                string result = modifyRegistry.Read("DataTierType");
                _dataTier = (eDataTier)Enum.Parse(typeof(eDataTier), result);
                return _dataTier;
                
                //return 0;
            }
            set
            {
                _dataTier = value;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("DataTierType", value.ToString());
            }
        }

        private static string _SQLServerConnection;
        public static string SQLServerConnection
        {
            get
            {
                if (!string.IsNullOrEmpty(_SQLServerConnection)) return _SQLServerConnection;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                return (_SQLServerConnection = modifyRegistry.Read("SQLServerConnection") ?? "");
            }
            set
            {
                _SQLServerConnection = value;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("SQLServerConnection", value);
            }
        }

        public static string ArellePath
        {
            get
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.BaseRegistryKey = Microsoft.Win32.Registry.LocalMachine;
                modifyRegistry.SubKey = "SOFTWARE\\Arelle\\";
                string result = modifyRegistry.Read(null);
                if (result != null)
                    return Path.Combine(result, "arelleCmdLine.exe");
                return "";
            }

        }


        private static string _id;
        public static string ID
        {
            get
            {
                if (!string.IsNullOrEmpty(_id)) return _id;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                return _id = modifyRegistry.Read("UserIDNumber");
            }
            set
            {
                _id = value;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("UserIDNumber", value);
            }
        }

        private static long _instanceID;
        public static long InstanceID
        {
            get
            {
                if (_instanceID > 0) return _instanceID;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                string result = modifyRegistry.Read("UserInstanceID");
                if (long.TryParse(result, out _instanceID))
                {
                    return _instanceID;
                }
                return 0;
            }
            set
            {
                _instanceID = value;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("UserInstanceID", value.ToString());
            }
        }

        private static string _remoteValidationURL;

        public static string RemoteValidationURL
        {
            get
            {
                if (!string.IsNullOrEmpty(_remoteValidationURL)) return _remoteValidationURL;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                return _remoteValidationURL = modifyRegistry.Read("RemoteValidationURL");
            }
            set
            {
                _remoteValidationURL = value;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("RemoteValidationURL", value);
            }
        }

        private static bool? _localValidation;
        public static bool LocalValidation
        {
            get
            {
                if (_localValidation != null) return _localValidation ?? false;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                bool localVal;
                if(bool.TryParse(modifyRegistry.Read("LocalValidation"), out localVal)) _localValidation = localVal;
                return _localValidation ?? true;
            }
            set
            {
                _localValidation = value;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("LocalValidation", value);
            }
        }

        public static int FormTop
        {
            get
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                string result = modifyRegistry.Read("FormTop");
                int formDim;
                if (int.TryParse(result, out formDim))
                {
                    return formDim;
                }
                return 0;
            }
            set
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("FormTop", value.ToString());
            }
        }

        public static int FormLeft
        {
            get
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                string result = modifyRegistry.Read("FormLeft");
                int formDim;
                if (int.TryParse(result, out formDim))
                {
                    return formDim;
                }
                return 0;
            }
            set
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("FormLeft", value.ToString());
            }
        }

        public static int FormHeight
        {
            get
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                string result = modifyRegistry.Read("FormHeight");
                int formDim;
                if (int.TryParse(result, out formDim))
                {
                    return formDim;
                }
                return 0;
            }
            set
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("FormHeight", value.ToString());
            }
        }

        public static int FormWidth
        {
            get
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                string result = modifyRegistry.Read("FormWidth");
                int formDim;
                if (int.TryParse(result, out formDim))
                {
                    return formDim;
                }
                return 0;
            }
            set
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("FormWidth", value.ToString());
            }
        }

        public static int LanguageID
        {
            get
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                string result = modifyRegistry.Read("LanguageID");
                int var;
                if (int.TryParse(result, out var))
                {
                    return var;
                }
                return 1;
            }
            set
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("LanguageID", value.ToString());
            }
        }

        public static int FormLanguage
        {
            get
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                string result = modifyRegistry.Read("FormLanguage");
                int var;
                if (int.TryParse(result, out var))
                {
                    return var;
                }
                return -1;
            }
            set
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("FormLanguage", value.ToString());
            }
        }




        private static bool? _testingMode;
        public static bool TestingMode
        {
            get
            {
                if (_testingMode != null) return _testingMode ?? false;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                bool localVal;
                if (bool.TryParse(modifyRegistry.Read("TestMode"), out localVal)) _testingMode = localVal;
                return _testingMode ?? false;
            }
            set
            {
                _testingMode = value;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("TestMode", value);
            }
        }

        private static string _applicationVersion;
        public static string ApplicationVersion
        {
            get
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                return (modifyRegistry.Read("ApplicationVersion"));                
            }
            set
            {
                _applicationVersion = value;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("ApplicationVersion", value);
            }
        }

        private static string _clickOnceVersion;
        public static string ClickOnceVersion
        {
            get
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                return (modifyRegistry.Read("ClickOnceVersion"));
            }
            set
            {
                _clickOnceVersion = value;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("ClickOnceVersion", value);
            }
        }

        private static int _rssCount;
        public static int RssCount
        {
            get
            {
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                string result = modifyRegistry.Read("RssCount");
                int rssCount;
                if (int.TryParse(result, out rssCount))
                {
                    return rssCount;
                }
                return 0;
            }
            set
            {
                _rssCount = value;
                ModifyRegistry modifyRegistry = new ModifyRegistry();
                modifyRegistry.Write("RssCount", value.ToString());
            }
        }


    }
}
