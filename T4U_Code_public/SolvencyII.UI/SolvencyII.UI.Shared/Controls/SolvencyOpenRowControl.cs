using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Base class for open row
    /// </summary>
    public class SolvencyOpenRowControl : UserControl
    {
        private List<Control> _formControls;
        private List<ISolvencyDataControl> _dataControls;
        private List<ISolvencyComboBox> _comboControls;
        private List<ISolvencyDisplayControl> _displayControls;
        private List<ISolvencyDataComboBox> _dataCombos;

        private List<Control> FormControls
        {
            get
            {
                if (_formControls == null)
                {
                    _formControls = new List<Control>();
                    GetAllControls(this, ref _formControls);
                }
                return _formControls;
            }
        }

        private void GetAllControls(Control container, ref List<Control> result)
        {
            foreach (Control c in container.Controls)
            {
                GetAllControls(c, ref result);
                result.Add(c);
            }
        }
        public List<ISolvencyDataControl> GetDataControls()
        {
            if (_dataControls == null)
            {
                Type solvencyType = typeof(ISolvencyDataControl);
                IEnumerable<Control> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c));
                _dataControls = (from ISolvencyDataControl control in midStep select control).ToList();
            }
            return _dataControls;
        }
        public List<ISolvencyComboBox> GetComboControls()
        {
            if (_comboControls == null)
            {
                Type solvencyType = typeof(ISolvencyComboBox);
                IEnumerable<Control> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c));
                _comboControls = (from ISolvencyComboBox control in midStep select control).ToList();
            }
            return _comboControls;
        }
        public List<ISolvencyDataComboBox> GetDataComboControls()
        {
            if (_dataCombos == null)
            {
                Type solvencyType = typeof(ISolvencyDataComboBox);
                IEnumerable<Control> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c));
                _dataCombos = (from ISolvencyDataComboBox control in midStep select control).ToList();
            }
            return _dataCombos;
        }
        public List<ISolvencyDisplayControl> GetDisplayControls()
        {
            if (_displayControls == null)
            {
                Type solvencyType = typeof(ISolvencyDisplayControl);
                IEnumerable<Control> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c));
                _displayControls = (from ISolvencyDisplayControl control in midStep select control).ToList();
            }
            return _displayControls;
        }
        public bool IsValid()
        {
            return true;
        }
        public long GetPK_ID(List<string> dataTables, string tableName)
        {
            int pos = dataTables.IndexOf(tableName);
            if (pos == -1) return 0;
            return ((IClosedRowControl)this).PK_IDs[pos];
        }

        public void SetPK_ID(List<string> dataTables, long pK_ID, string tableName = null)
        {
            if (!string.IsNullOrEmpty(tableName))
            {
                int pos = dataTables.IndexOf(tableName);
                if (pos != -1)
                    ((IClosedRowControl)this).PK_IDs[pos] = pK_ID;
            }
            else
            {
                for (int i = 0; i < ((IClosedRowControl)this).PK_IDs.Count(); i++)
                {
                    ((IClosedRowControl)this).PK_IDs[i] = 0;
                }
            }
        }
        public string ColName { get; set; } // Delete Me

        public void ResetCacheRefs()
        {
            // This means a reset will be performed and new controls added when appropriate.
            _formControls = null;
            _dataControls = null;
            _comboControls = null;
            _displayControls = null;
        }

    }
}

