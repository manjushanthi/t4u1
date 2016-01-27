using System;
using System.Collections.Generic;
using System.Drawing;

namespace SolvencyII.Domain.Interfaces
{
    /// <summary>
    /// Main interface for closed templates
    /// </summary>
    public interface IClosedRowControl : IDisposable
    {
        event GenericDelegates.Response AddControl;
        event GenericDelegates.ListLongs DelControl;
        event GenericDelegates.Response DelControlDR;
        event EventHandler AddRow;
        event EventHandler AddCol;
        event EventHandler DelRow;
        event EventHandler DelCol;
        Point Location { get; set; }
        int Height { get; set; }
        int Width { get; set; }
        List<ISolvencyDataControl> GetDataControls();
        List<ISolvencyComboBox> GetComboControls();
        List<ISolvencyDataComboBox> GetDataComboControls();
        List<ISolvencyDisplayControl> GetDisplayControls();
        void ResetCacheRefs();
        List<long> PK_IDs { get; set; }
        

    }
}
