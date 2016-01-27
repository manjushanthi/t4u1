using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using SolvencyII.ExcelImportExportLib;

namespace SolvencyII.CMD.Operations
{
    class ExportExcelEvents
    { 
        /// <summary>       
        /// Events to show the progress updates 
        /// </summary>
      
        public void CompletedHandler(object sender, AsyncCompletedEventArgs e)
        {
           string message = (string)e.UserState;            
           if (e != null)
               if (e.Error != null)
                   Console.WriteLine(e.Error.ToString());
           
        }

        /// <summary>
        /// Events : To show - Progress updates
        /// </summary>
      

        public void ProgressChangedHandler(object sender, ProgressChangedEventArgs e)
        {
           Console.WriteLine("Processing " + (string)e.UserState);         
        }


        /// <summary>
        /// Events : To show - Granule Progress updates
        /// </summary>
       

        public void GranuleProgressChangedHandler(object sender, ProgressChangedEventArgs e)
        {           
            Console.WriteLine("Processing " + (string)e.UserState);
        }
       
    }
}
