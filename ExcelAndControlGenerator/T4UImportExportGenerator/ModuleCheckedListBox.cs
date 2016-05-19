using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace T4UImportExportGenerator
{
    class ModuleCheckedListBox : CheckedListBox
    {
        public ModuleCheckedListBox()
        {
            ItemHeight = 25;
          
        }
        public override int ItemHeight { get; set; }      
    }
}
