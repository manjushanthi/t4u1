using System;
using System.Collections.Generic;
using SolvencyII.Domain.Entities;

namespace SolvencyII.Domain.Interfaces
{
    /// <summary>
    /// Interface used with special cases and closed templates.
    /// </summary>
    public interface IClosedRowRepository
    {
        void PopulateAll(string dataTables, List<object> data, bool firstTable);
        void ClearAllControls();
        void PopulateLabels(List<mAxisOrdinate> labelText);
        void SetToolTipObject(object toolTip);
        bool IsValid();
        bool IsDirty { get; set; }
        bool Enabled { get; set; }
        event GenericDelegates.BoolResultQuestion AskUserQuestion;
        event GenericDelegates.Response DeleteControlDR;
        event GenericDelegates.ListLongs DeleteControl;
        event GenericDelegates.StringResponse AlertUser;
        // event GenericDelegates.BoolResultQuestion UserQuestion;

        bool SaveAll(ISolvencyUserControl userControl, List<ISolvencyPageControl> nPageControls, TreeItem selectedItem, long instanceID, out string errorText);
        void AddControl();
        bool DeleteSingleControl();
        void SetPkId(int pkId, List<Type> dataTypes, List<string> dataTables);
        int PkID { get; }
    }
}
