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
    public partial class SchoolsList : PageBase
    {
        int serialNum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindSchools();
        }

        protected void grdSchoolList_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = Convert.ToString((++serialNum));
            }
        }

        private void BindSchools()
        {
            SBModel.SSchoolDataTable SSchool = new SBModel.SSchoolDataTable();
            SSchool = SBDataAccess.GetSchools(DBConnectionString);

            grdSchoolList.DataSource = SSchool;
            grdSchoolList.DataBind();

            grdSchoolList.UseAccessibleHeader = true;
            grdSchoolList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}