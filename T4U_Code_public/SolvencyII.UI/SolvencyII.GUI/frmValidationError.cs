using System.Windows.Forms;

using SolvencyII.Validation.Model;

namespace SolvencyII.GUI
{
    public partial class frmValidationError : Form
    {
       
        public frmValidationError(ValidationError ve)
        {
            InitializeComponent();

            if (ve != null)
            {

                this.Text = "Validation Code: " + ve.ValidationCode;
                txtScope.Text = ve.TableCode;
                txtValidationCode.Text = ve.ValidationCode;
                txtErrorMessage.Text = ve.ErrorMessage;
                txtContext.Text = ve.SerializedContext + "(" + ve.Context + ")";
                txtCells.Text = ve.Cells;
                txtExpression.Text = ve.Expression;
                txtFormula.Text = ve.Formula;
            }
        }
    }
}
