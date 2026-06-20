using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlterationRecords.Common;
using AlterationRecordsDataAccess;
using AlterationRecordsModel;

namespace AlterationRecords
{
    public partial class RolesList : PageBase
    {
        int serialNum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindRoles();
        }

        protected void grdRolesList_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = Convert.ToString((++serialNum));
            }
        }

        private void BindRoles()
        {
            SBModel.SRoleDataTable SRole = new SBModel.SRoleDataTable();
            SRole = SBDataAccess.GetRoles(DBConnectionString);

            grdRolesList.DataSource = SRole;
            grdRolesList.DataBind();

            grdRolesList.UseAccessibleHeader = true;
            grdRolesList.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        [WebMethod]
        public static List<string> BindUserRoles(string name)
        {
            SBModel.SRoleDataTable SRole = new SBModel.SRoleDataTable();
            string con = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            SRole = SBDataAccess.GetRoles(con);            
            return null;
        }
    }
}