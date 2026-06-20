using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using AlterationRecordsModel;
using AlterationRecordsDataAccess;
using AlterationRecords.Common;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Collections;

namespace AlterationRecords
{
    public partial class ListOfFiles : PageBase
    {
        int serialNum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSchools();
                BindUploadList(false, 0);
            }
        }

        protected void grdUploadFilesList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUploadFilesList.PageIndex = e.NewPageIndex;
            BindUploadList(true, grdUploadFilesList.PageIndex);
        }

        //protected void lbtnDownload_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LinkButton lbtn = (LinkButton)sender;
        //        int FileID = Convert.ToInt32(lbtn.CommandArgument);
        //        SBModel.SFileDataTable sfile = new SBModel.SFileDataTable();
        //        sfile = SBDataAccess.GetSFileByFileID(DBConnectionString, FileID);

        //        string location = sfile.Rows[0]["FileLocation"].ToString();
        //        string Mappath = Server.MapPath("~/" + location + "/");
        //        string filePath = Mappath + sfile.Rows[0]["FileName"].ToString();

        //        String strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";Extended Properties=Excel 8.0;";

        //        OleDbConnection con = new OleDbConnection(strConn);
        //        con.Open();

        //        // Get All Sheets Name
        //        DataTable dtSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        //        // Retrive the Data by Sheetwise
        //        DataSet dsOutput = new DataSet();

        //        string sSheetName = dtSheetName.Rows[dtSheetName.Rows.Count - 1]["TABLE_NAME"].ToString();

        //        string sQuery = "Select * From [" + sSheetName + "]";

        //        OleDbCommand dbCmd = new OleDbCommand(sQuery, con);
        //        OleDbDataAdapter dbDa = new OleDbDataAdapter(dbCmd);
        //        System.Data.DataTable dtData = new System.Data.DataTable();

        //        dbDa.Fill(dsOutput);

        //        GridView grd = new GridView();
        //        grd.DataSource = dsOutput;
        //        grd.DataBind();

        //        grdUploadFilesList.DataSource = dsOutput;
        //        grd.DataBind();

        //        string attachment = "attachment; filename=" + sfile.Rows[0]["FileName"].ToString();
        //        Response.ClearContent();
        //        Response.AddHeader("content-disposition", attachment);
        //        Response.ContentType = "application/ms-excel";
        //        StringWriter stw = new StringWriter();
        //        HtmlTextWriter htextw = new HtmlTextWriter(stw);

        //        // turn off paging 
        //        grdUploadFilesList.AllowPaging = false;
        //        BindUploadList(false, 0);

        //        grd.RenderControl(htextw);
        //        Response.Write(stw.ToString());
        //        Response.End();

        //        // turn the paging on again 
        //        grdUploadFilesList.AllowPaging = true;
        //        BindUploadList(true, grdUploadFilesList.PageIndex);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public override void VerifyRenderingInServerForm(Control control)
        //{

        //}

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

        protected void lbtnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = (LinkButton)sender;
                int FileID = Convert.ToInt32(lbtn.CommandArgument);
                Response.Redirect("FileDetails.aspx?ID=" + FileID, false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BindUploadList(bool paging, int pageIndex)
        {
            try
            {
                SBModel.SFileDataTable sfile = new SBModel.SFileDataTable();
                //int status = Convert.ToInt32(ddlStatus.SelectedValue);
                int schoolID = Convert.ToInt32(ddlSchool.SelectedValue);
                sfile = SBDataAccess.GetUploadList(DBConnectionString, -1, schoolID);
                grdUploadFilesList.DataSource = sfile;
                if (paging)
                {
                    serialNum = grdUploadFilesList.PageIndex * grdUploadFilesList.PageSize;
                }
                grdUploadFilesList.DataBind();
                grdUploadFilesList.UseAccessibleHeader = true;
                grdUploadFilesList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }

        protected void ddlStatus_SelectedChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "reloadDatatable();", true);
            BindUploadList(false, 0);
        }

        protected void grdUploadFilesList_DataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //grdUploadFilesList.AllowPaging = false;
                //BindUploadList(false, 0);
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Text = Convert.ToString((++serialNum));
                    e.Row.Cells[4].Text = GetDateAndTime(e.Row.Cells[4].Text);
                }
                if (e.Row.RowType == DataControlRowType.EmptyDataRow)
                {
                    return;
                }
                if (e.Row.Cells[0].Text != "")
                {
                    //if (e.Row.Cells[5].Text.ToLower().IndexOf("synched from tablet &amp; locked") > -1)
                    //{
                    //    LinkButton link = e.Row.Cells[9].FindControl("lbtnDownload") as LinkButton;
                    //    link.Enabled = true;
                    //}
                }
                //grdUploadFilesList.AllowPaging = true;
                //BindUploadList(true, grdUploadFilesList.PageIndex);
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }
    }
}