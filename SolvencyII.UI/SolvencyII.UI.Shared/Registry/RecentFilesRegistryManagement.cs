namespace SolvencyII.UI.Shared.Registry
{
    /// <summary>
    /// Manages the recently used database files within the registry
    /// </summary>
    public static class RecentFilesRegistryManagement
    {
        public const int RECENTFILE_COUNT = 5;
        public const string RECENTFILE_SIGNATURE = "Recent{0}";
        public static void OpenFile(string fileName)
        {
            // Here we need to manage the items in the registry for the this opened file.
            ModifyRegistry modifyRegistry = new ModifyRegistry();
            // Does the registry already contain this path?
            bool found = false;
            for (int i = 1; i <= RECENTFILE_COUNT; i++)
            {
                string key = string.Format(RECENTFILE_SIGNATURE, i);
                string value = modifyRegistry.Read(key);
                if (string.IsNullOrEmpty(value)) break;
                if(value.ToUpper() == fileName.ToUpper())
                {
                    found = true;
                    // File name already exists so we just need to juggle the order
                    JuggleToTop(i, fileName, modifyRegistry);
                    break;
                }
            }
            if(!found)
            {
                // Insert the entry
                InsertEntry(fileName, modifyRegistry);
            }

        }

        private static void JuggleToTop(int pos, string fileName, ModifyRegistry modifyRegistry)
        {
            for (int i = pos; i > 1; i--)
            {
                string value = modifyRegistry.Read(string.Format(RECENTFILE_SIGNATURE, i - 1));
                if (!string.IsNullOrEmpty(value)) modifyRegistry.Write(string.Format(RECENTFILE_SIGNATURE, i), value);
            }
            modifyRegistry.Write(string.Format(RECENTFILE_SIGNATURE, 1), fileName);
        }

        private static void InsertEntry(string fileName, ModifyRegistry modifyRegistry)
        {
            for (int i = RECENTFILE_COUNT; i > 1; i--)
            {
                string value = modifyRegistry.Read(string.Format(RECENTFILE_SIGNATURE, i-1));
                if (!string.IsNullOrEmpty(value)) modifyRegistry.Write(string.Format(RECENTFILE_SIGNATURE, i), value);
            }
            modifyRegistry.Write(string.Format(RECENTFILE_SIGNATURE, 1), fileName);
        }

    }
}
