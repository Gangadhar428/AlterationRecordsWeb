using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlterationRecords.Common;
using AlterationRecordsModel;
using AlterationRecordsDataAccess;
using System.Data;
using System.Text;

namespace AlterationRecords
{
    public partial class ChangePassword : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //added tooltip for password textbox for Password Strength module showing the conditions for password on 04-12-2012
                txtNewPassword.ToolTip = "Minimum 8 charecters, " + Environment.NewLine + "Should Contain 1 Upper Case Letter, " + Environment.NewLine + "Should Contain 1 Special Charecter," + Environment.NewLine + "Should Contain 1 Numeric";
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Identity identity = (Identity)Session["LoggedUser"];
                if (identity.Password == Encryption.Encrypt(txtOldPassword.Text))
                {
                    ltrlScript.Text = "";
                    int i = SBDataAccess.UpdatePassword(DBConnectionString, identity.UserID, Encryption.Encrypt(txtNewPassword.Text));

                    if (i > 0)
                    {
                        AlertScript("Password Updated Successfully");
                        ClearFields();
                    }
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<script language='javascript' type='text/javascript'>{0}", Environment.NewLine);
                    sb.Append("alert('Incorrect Old password');");
                    sb.AppendFormat("</script>");
                    ltrlScript.Text = sb.ToString();
                }
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Identity lUser = (Identity)Session["LoggedUser"];
                if (lUser.Role.ToLower().IndexOf("staff") > -1)
                {
                    Response.Redirect("Upload.aspx");
                }
                else
                    Response.Redirect("RolesList.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ClearFields()
        {
            txtOldPassword.Text = txtNewPassword.Text = txtConfirmPassword.Text = "";
        }
    }
}