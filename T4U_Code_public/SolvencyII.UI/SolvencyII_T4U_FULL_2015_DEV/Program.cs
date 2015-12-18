using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;



namespace SolvencyII_T4U_FULL_2015_DEV
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            #if (!FOR_NCA)
                 #error "Incorect Compilation variable & deployment Project type";
            #endif          

            //TODO: we need to add a localizartion string to "Development Environment"
            string environment = "Development Environment";
            // Start the main winForms application
            //TODO:Nicholas, I do not know why but the POC.Program.Main was complaining that args was missing, I have added a fucntion to parse args compatible with clickonce (URL approach). The function is not tested here..
            SolvencyII.GUI.Program.Main(SolvencyII.GUI.Program.GetFirstCommandLineArgument(environment));
        }

       

    }
}
