using AlterationRecords.Common;
using AlterationRecordsDataAccess;
using AlterationRecordsModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlterationRecords
{
    public partial class Login : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.RemoveAll();
                Session.Abandon();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SBModel.SUserDataTable uData = SBDataAccess.GetUsersDetails(DBConnectionString, txtUserName.Text.Trim().ToLower());

                if (uData.Rows.Count > 0)
                {
                    DataRow row = (DataRow)uData.Rows[0];
                    if (row["Password"].ToString() == Encryption.Encrypt(txtPassword.Text.Trim()))
                    {
                        Identity identity = new Identity(row["UserName"].ToString(),
                           Convert.ToInt64(row["UserID"]), row["Name"].ToString(), Convert.ToInt32(row["RoleID"]), Convert.ToBoolean(row["IsSuperUser"]), row["Password"].ToString(), row["Role"].ToString());
                        Session["LoggedUser"] = identity;

                        if (row["Role"].ToString().ToLower().IndexOf("staff") > -1)
                        {
                            Response.Redirect("Upload.aspx", true);
                        }
                        else if (row["Role"].ToString().ToLower().IndexOf("tailor") > -1 || row["Role"].ToString().ToLower().IndexOf("boardman") > -1)
                        {
                            SetFailMessage("user has no access to server module");
                            return;
                        }
                        Response.Redirect("RolesList.aspx", true);
                    }
                    else
                    {
                        SetFailMessage("Password does not match.");
                    }
                }
                else
                {
                    SetFailMessage("No user exists with the username.");
                }
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }

        private void SetFailMessage(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<script language='javascript' type='text/javascript'>{0}", Environment.NewLine);
            sb.AppendLine("setTimeout(() => {");
            sb.AppendFormat("ShowMessageDiv('fail','{0}');{1}", msg, Environment.NewLine);
            sb.AppendLine("}, \"1000\");");
            sb.AppendFormat("</script>");
            ltrlScript.Text = sb.ToString();
        }
    }
}