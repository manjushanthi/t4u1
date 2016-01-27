using System;


namespace SolvencyII.UI.Shared.xBRL
{
    public class ImportExport : IDisposable
    {
        private string _dllPath;

        public ImportExport(string dllPath)
        {
            _dllPath = dllPath;
            // Load Library
        }

        public string Export(string inputDbFilePath, string dictionaryPath, string outputXbrlPath)
        {
            return UnManagedCall("wv", inputDbFilePath, outputXbrlPath, dictionaryPath);
        }

        public string Import(string outputDbFilePath, string dictionaryPath, string inputXbrlPath)
        {
            return UnManagedCall("2p", inputXbrlPath, outputDbFilePath, dictionaryPath);
        }

        private string UnManagedCall(string command, string input, string output, string dictionary)
        {
            return "";
        }

        public void Dispose()
        {
            // Unload Library if needed.

        }
    }
}
