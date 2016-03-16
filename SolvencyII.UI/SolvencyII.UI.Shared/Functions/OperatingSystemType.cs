using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SolvencyII.UI.Shared.Functions
{
    public static class OperatingSystemType
    {
        public static bool Is64BitOperatingSystem()
        {
            if (IntPtr.Size == 8)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }       
    }
}
