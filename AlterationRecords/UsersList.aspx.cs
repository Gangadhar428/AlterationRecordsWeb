using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlterationRecordsModel;
using AlterationRecordsDataAccess;
using System.Configuration;
using AlterationRecords.Common;

namespace AlterationRecords
{
    public partial class UsersList : PageBase
    {
        int serialNum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindUsers();
        }

        protected void grdUsersList_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = Convert.ToString((++serialNum));
            }
        }

        protected void lbtnUserName_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = (LinkButton)sender;
                int uID = Convert.ToInt32(lbtn.CommandArgument);
                Response.Redirect("AddUsers.aspx?uid=" + uID, false);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void BindUsers()
        {
            SBModel.SUserDataTable SUser = new SBModel.SUserDataTable();
            SUser = SBDataAccess.GetUsers(DBConnectionString);
            grdUsersList.DataSource = SUser;
            grdUsersList.DataBind();

            grdUsersList.UseAccessibleHeader = true;
            grdUsersList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}