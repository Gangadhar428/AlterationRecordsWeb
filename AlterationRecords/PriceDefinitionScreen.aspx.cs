using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlterationRecordsModel;
using AlterationRecordsDataAccess;
using AlterationRecords.Common;
using System.Data;

namespace AlterationRecords
{
    public partial class PriceDefinitionScreen : PageBase
    {
        int serialNum = 0;
        bool IsPageRefreshed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string postBackID = System.Guid.NewGuid().ToString();
                ViewState["postids"] = System.Guid.NewGuid().ToString();
                Session["postid"] = ViewState["postids"].ToString();
                BindSchools();
                BindPriceList(false, 0);
            }
            else
            {
                try
                {
                    if (ViewState["postids"].ToString() != Session["postid"].ToString())
                    {
                        IsPageRefreshed = true;
                        grdPriceDefinition.EditIndex = -1;
                    }
                    Session["postid"] = System.Guid.NewGuid().ToString();
                    ViewState["postids"] = Session["postid"].ToString();
                }
                catch (Exception ex)
                {
                    AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
                }
            }
        }

        protected void grdPriceDefinition_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdPriceDefinition.PageIndex = e.NewPageIndex;
                grdPriceDefinition.EditIndex = -1;
                BindPriceList(true, grdPriceDefinition.PageIndex);
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }

        protected void grdPriceDefinition_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (!IsPageRefreshed)
            {
                try
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "pageIndex();", true);
                    grdPriceDefinition.EditIndex = e.NewEditIndex;
                    BindPriceList(true, grdPriceDefinition.PageIndex);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "reloadDatatable();", true);
                }
                catch (Exception ex)
                {
                    AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
                }
            }
        }

        protected void grdPriceDefinition_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (!IsPageRefreshed)
            {
                try
                {
                    TextBox txtPrice = (TextBox)grdPriceDefinition.Rows[e.RowIndex].FindControl("txtPrice");
                    HiddenField hdn = (HiddenField)grdPriceDefinition.Rows[e.RowIndex].FindControl("hdnPrice");
                    HiddenField hdnAtt = (HiddenField)grdPriceDefinition.Rows[e.RowIndex].FindControl("hdnAttributeName");
                    SBDataAccess.InsertPrice(DBConnectionString, Convert.ToDouble(txtPrice.Text), hdn.Value, hdnAtt.Value);
                    grdPriceDefinition.EditIndex = -1;
                    BindPriceList(true, grdPriceDefinition.PageIndex);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "reloadDatatable();", true);
                }
                catch (Exception ex)
                {
                    AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
                }
            }
        }

        protected void grdPriceDefinition_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (!IsPageRefreshed)
            {
                try
                {
                    grdPriceDefinition.EditIndex = -1;
                    BindPriceList(true, grdPriceDefinition.PageIndex);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "reloadDatatable();", true);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void BindSchools()
        {
            try
            {
                SBModel.SSchoolDataTable sSchool = new SBModel.SSchoolDataTable();
                sSchool = SBDataAccess.GetSchools(DBConnectionString);
                ddlSchool.DataSource = sSchool;
                ddlSchool.DataTextField = "SchoolName";
                ddlSchool.DataValueField = "SchoolID";
                ddlSchool.DataBind();
                ddlSchool.Items.Insert(0, new ListItem("All", "-1"));
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }

        protected void ddlStatus_SelectedChanged(object sender, EventArgs e)
        {
            grdPriceDefinition.EditIndex = -1;
            BindPriceList(false, 0);
        }

        private void BindPriceList(bool pagination, int pageIndex)
        {
            try
            {
                grdPriceDefinition.PageIndex = pageIndex;
                DataTable sfile = new DataTable();

                int schoolID = Convert.ToInt32(ddlSchool.SelectedValue);
                sfile = SBDataAccess.GetPriceList(DBConnectionString, schoolID);
                sfile.DefaultView.Sort = "AtrributeName";
                grdPriceDefinition.DataSource = sfile.DefaultView;
                if (pagination)
                {
                    serialNum = grdPriceDefinition.PageIndex * grdPriceDefinition.PageSize;
                }
                grdPriceDefinition.DataBind();
                grdPriceDefinition.UseAccessibleHeader = true;
                grdPriceDefinition.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }

        protected void grdPriceDefinition_DataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Text = Convert.ToString((++serialNum));
                }
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }

        protected void grdPriceDefinition_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["SortExpression"] = e.SortExpression;
            BindPriceList(true, grdPriceDefinition.PageIndex);
        }
    }
}