using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlterationRecords.Common;
using AlterationRecordsModel;
using AlterationRecordsDataAccess;

namespace AlterationRecords
{
    public partial class AddUsers : PageBase
    {
        int UserID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserID = Convert.ToInt32(Request["uid"]);

            if (!IsPostBack)
            {
                //added tooltip for password textbox for Password Strength module showing the conditions for password on 04-12-2012
                txtPassword.ToolTip = "Minimum 8 charecters, " + Environment.NewLine + "Should Contain 1 Upper Case Letter, " + Environment.NewLine + "Should Contain 1 Special Charecter," + Environment.NewLine + "Should Contain 1 Numeric";
                if (UserID > 0)
                {
                    BindUsers();
                    lbtnUpdate.Visible = true;
                    lbtnSave.Visible = false;
                    rfPasword.Visible = false;
                    spnTitle.InnerText = "Update Users";
                }
                else
                {
                    lbtnSave.Visible = true;
                    lbtnUpdate.Visible = false;
                    spnTitle.InnerText = "Add Users";
                }
            }
        }
        private void BindUsers()
        {
            try
            {
                SBModel.SUserDataTable sUser = SBDataAccess.GetSUserByUserID(DBConnectionString, UserID);
                SBModel.SUserRow SRow = (SBModel.SUserRow)sUser.Rows[0];
                txtUserName.Text = SRow.UserName;
                txtName.Text = SRow.Name;
                txtEmail.Text = SRow.IsEmailAddressNull() ? "" : SRow.EmailAddress;
                txtPhone.Text = SRow.IsPhoneNumberNull() ? "" : SRow.PhoneNumber;
                ddlRole.SelectedValue = SRow.RoleID.ToString();
                chkActive.Checked = SRow.IsActive;
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SBModel.SUserRow row = (new SBModel()).SUser.NewSUserRow();
                row.UserID = 0;
                row.UserName = txtUserName.Text;
                row.Password = Encryption.Encrypt(txtPassword.Text);
                row.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
                row.Name = txtName.Text;
                row.IsActive = chkActive.Checked;
                row.EmailAddress = txtEmail.Text == "" ? "" : txtEmail.Text;
                row.PhoneNumber = txtPhone.Text == "" ? "" : txtPhone.Text;

                int i = SBDataAccess.InsertUser(DBConnectionString, row);
                if (i > 0)
                {
                    AlertScript("Record Saved Sucessfully");
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("cannot insert duplicate key row in object 'dbo.suser' with unique index"))
                {
                    AlertScript("User Name Already Exists");
                }
            }
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SBModel.SUserRow row = (new SBModel()).SUser.NewSUserRow();
                row.UserID = UserID;
                row.UserName = txtUserName.Text;
                row.Password = Encryption.Encrypt(txtPassword.Text);
                row.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
                row.Name = txtName.Text;
                row.IsActive = chkActive.Checked;
                row.EmailAddress = txtEmail.Text == "" ? "" : txtEmail.Text;
                row.PhoneNumber = txtPhone.Text == "" ? "" : txtPhone.Text;

                int i = SBDataAccess.InsertUser(DBConnectionString, row);
                if (i > 0)
                {
                    AlertScript("Record Updated Sucessfully");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("cannot insert duplicate key row in object 'dbo.suser' with unique index"))
                {
                    AlertScript("User Name Already Exists");
                }
                else
                    AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }
        private void ClearFields()
        {
            txtUserName.Text = txtPhone.Text = txtPassword.Text = txtName.Text = txtEmail.Text = "";
            ddlRole.SelectedValue = "0";
        }
    }
}