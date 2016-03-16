using System;
using System.Collections.Generic;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared.Entities;

namespace SolvencyII.Domain.Interfaces
{
    /// <summary>
    /// Interface for the main base class used in closed templates
    /// </summary>
    public interface IUserControlBase
    {
        List<ISolvencyDataControl> GetDataControls();
        List<ISolvencyPageControl> GetPAGEnControls();
        List<ISolvencyComboBox> GetUserComboBoxControls();
        List<ISolvencyDataComboBox> GetDataComboBoxControls();
        void SetText(ISolvencyDisplayControl label, string text);
        void PopulateLabels(List<mAxisOrdinate> axisOrdinates);
        string BuildSQLQuery_Select(string tableName, bool comboUpdate);
        // string BuildRepositoryWhere_Select(string tableName, bool comboUpdate);
        string BuildSQLQuery_Delete(string tableName, bool usePageN);
        long InstanceID { get; set; }
        bool PageCombosCheck();
        void GreyOutInputControls(List<FactInformation> dataFacts);
        void PopulatePAGEnControls(IEnumerable<FormDataPage> controlSetupPagEn);
        List<string> GetDataTables();
        List<Type> GetDataTypes();
        bool IsDirty { get; set; }
        string RowKeyCheck();
        
    }
}
