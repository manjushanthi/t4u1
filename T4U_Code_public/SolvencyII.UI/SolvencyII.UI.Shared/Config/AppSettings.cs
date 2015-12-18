using System;
using System.Configuration;

namespace SolvencyII.UI.Shared.Config
{
    public static class AppSettings
    {
        private static string _name;
        public static string Name
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_name)) return _name;
                return (_name = GetSetting<string>("UserName"));
            }
            set
            {
                _name = value;
                PutSetting("UserName", value);
            }
        }

        private static string _id;
        public static string ID
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_id)) return _id;
                return _id = GetSetting<string>("UserIDNumber");
            }
            set
            {
                _id = value;
                PutSetting("UserIDNumber", value);
            }
        }

        private static int _instanceID;
        public static int InstanceID
        {
            get
            {
                if(_instanceID>0) return _instanceID;
                if(int.TryParse(GetSetting<int>("UserInstanceID"), out _instanceID))
                {
                    return _instanceID;
                }
                return 0;
            }
            set
            {
                _instanceID = value;
                PutSetting("UserInstanceID", value.ToString());
            }
        }

        private static string _dictionaryFile;
        public static string DictionaryFile
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_dictionaryFile)) return _dictionaryFile;
                return (_dictionaryFile = GetSetting<string>("DictionaryFile"));
            }
            set
            {
                _dictionaryFile = value;
                PutSetting("DictionaryFile", value);
            }
        }

        private static string _connectionString;
        public static string ConnectionString
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_connectionString)) return _connectionString;
                return (_connectionString = GetSetting<string>("TestDatabase"));            
            }
            set
            {
                _connectionString = value;
                PutSetting("TestDatabase", value);
                SolvencyII.Data.Shared.Configuration.AppSettings.ConnectionString = value;
            }
        }

        #region Helper Functions

        private static void PutSetting(string setting, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                AppSettingsSection app = config.AppSettings;
                app.Settings.Remove(setting);
                app.Settings.Add(setting, value);
                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static string GetSetting<T>(string setting)
        {
            try
            {
                AppSettingsReader reader = new AppSettingsReader();
                return reader.GetValue(setting, typeof (T)).ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        #endregion

    }
}
