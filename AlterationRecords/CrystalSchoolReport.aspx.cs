using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlterationRecords.Common;
using System.Data;
using AlterationRecordsDataAccess;
using AlterationRecordsModel;
using System.IO;

namespace AlterationRecords
{
    public partial class CrystalSchoolReport : PageBase
    {

        string schools = " ";
        string schoolTitle = "";
        string platoon = " ";
        string platoonTitle = "";
        string QueryString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSchools();
                BindPlatoons();

                txtFromDate.Text = Session["fromDate"] != null ? Session["fromDate"].ToString() : "";
                txtToDate.Text = Session["ToDate"] != null ? Session["ToDate"].ToString() : "";
                platoon = Session["Platoon"] != null ? Session["Platoon"].ToString() : "";
                schools = Session["Schools"] != null ? Session["Schools"].ToString() : "";
                ddlStatus.SelectedValue = Session["Component"] != null ? Session["Component"].ToString() : "";
                ddlAccel.SelectedValue = Session["Accel"] != null ? Session["Accel"].ToString() : "";
                if (Request["school"] != null)
                {
                    schools = Request["school"].ToString();
                }
                if (Session["fromdate"] != null && Session["schoolselectedItems"] != null && Session["platoonselectedItems"] != null)
                {
                    ListItemCollection lst = (ListItemCollection)Session["schoolselectedItems"];
                    ListItemCollection lstpla = (ListItemCollection)Session["platoonselectedItems"];
                    for (int i = 0; i < lst.Count; i++)
                    {
                        lstSchool.Items[i].Selected = lst[i].Selected;
                    }
                    for (int i = 0; i < lstpla.Count; i++)
                    {
                        lstPlatoon.Items[i].Selected = lstpla[i].Selected;
                    }
                    //BindData();
                    string dtFrom = Convert.ToDateTime(txtFromDate.Text).ToString("MMM dd, yyyy");
                    string dtTo = Convert.ToDateTime(txtToDate.Text).ToString("MMM dd, yyyy");
                    schoolTitle = schools == " " ? "ALL" : schools;
                    platoonTitle = platoon == " " ? "ALL Platoons" : platoon;
                    lblFilter.Text = "Filters Selected: ";
                    lblQueryTitle.Text = Session["QueryString"] != null ? Session["QueryString"].ToString() : lblQueryTitle.Text;
                }
            }
            //smRI.RegisterPostBackControl(lbtnExport);
        }
        protected void lstSchools_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem selecteditem in lstSchool.Items)
                {
                    if (selecteditem.Selected)
                    {
                        if (selecteditem.Value == " ")
                            continue;
                        schools += selecteditem.Value + ",";
                        schoolTitle += selecteditem.Text + ",";
                    }
                    else
                        schools.Replace(selecteditem.ToString() + ",", "");
                }
                if (schools != " ")
                    schools = schools.Trim().Remove(schools.Trim().ToString().LastIndexOf(","));
                BindPlatoons();
                schoolTitle = schools = " ";
                RunScript("loadDatePricker();");
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }

        int schoolID = 0;
        protected void rptrDress_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataView dv = new DataView();
                Repeater rptrFit = (Repeater)e.Item.FindControl("rptrFit");
                HiddenField hdnSchoolID = (HiddenField)e.Item.FindControl("hdnSchoolID");
                schoolID = Convert.ToInt32(hdnSchoolID.Value);
                Label lblSchool = (Label)e.Item.FindControl("lblDress");
                lblSchool.Text = lblSchool.Text + ", " + txtFromDate.Text.Replace("/", "-") + " to " + txtToDate.Text.Replace("/", "-");

                dateReport.DefaultView.RowFilter = "SchoolID=" + schoolID;
                dv = dateReport.DefaultView;


                DataTable dtSchools = dv.ToTable();
                string[] columnNames = { "IsFirstFit", "IsSecondFit", "IsSubsequentFit" };
                DataTable distinctDressTable = dv.ToTable(true, columnNames);


                rptrFit.DataSource = distinctDressTable;
                rptrFit.DataBind();
            }
        }

        protected void rptrFit_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataView dv = new DataView();
                GridView grdAttribures = (GridView)e.Item.FindControl("grdAttribures");
                HiddenField hdnFits = (HiddenField)e.Item.FindControl("hdnFits");
                string[] strFit = hdnFits.Value.Split(',');
                Label lblFit = (Label)e.Item.FindControl("lblFit");

                if (strFit[0].ToString() == "True")
                {
                    lblFit.Text = "(1st Fit)";
                }
                else if (strFit[1].ToString() == "True")
                {
                    lblFit.Text = "(2nd Fit)";
                }
                else if (strFit[2].ToString() == "True")
                {
                    lblFit.Text = "(3rd Fit)";
                }
                dateReport.DefaultView.RowFilter = "IsFirstFit = " + Convert.ToBoolean(strFit[0]) + " AND IsSecondFit=" + Convert.ToBoolean(strFit[1]) + " AND IsSubsequentFit=" + Convert.ToBoolean(strFit[2]) + " AND SchoolID=" + schoolID;
                dv = dateReport.DefaultView;
                grdAttribures.DataSource = dv;
                grdAttribures.DataBind();
            }
        }


        protected void grdAtrributes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (schoolID > 1)
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                }
                //if (schoolID != 2)
                //{
                //    e.Row.Cells[4].Visible = false;
                //}
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text == "0")
                        e.Row.Cells[i].Text = "";
                }
                e.Row.Cells[1].Text = Convert.ToDateTime(e.Row.Cells[1].Text).ToString("MM/dd/yyyy");
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (schoolID > 1)
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                }
                //if (schoolID != 2)
                //{
                //    e.Row.Cells[4].Visible = false;
                //    e.Row.Cells[3].Text = "Total Recruits";
                //}
            }
        }

        string accelTitle = " ";
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem selecteditem in lstSchool.Items)
                {
                    if (selecteditem.Selected)
                    {
                        if (selecteditem.Value == " ")
                            continue;
                        schools += selecteditem.Value + ",";
                        schoolTitle += selecteditem.Text + ",";
                    }
                    else
                        schools.Replace(selecteditem.ToString() + ",", "");
                }
                if (schools != " ")
                    schools = schools.Trim().Remove(schools.Trim().ToString().LastIndexOf(","));
                //BindPlatoons();
                GetPlatoon();
               // string date= Convert.ToDateTime(txtFromDate.Text).ToString("MM/dd/yyyy");
                Session["fromDate"] = txtFromDate.Text;
                Session["ToDate"] = txtToDate.Text;
                Session["Platoon"] = platoon;
                Session["Schools"] = schools;
                Session["Component"] = ddlStatus.SelectedValue;
                Session["Accel"] = ddlAccel.SelectedValue;
                //Session["schoolselectedItems"] = lstSchool.Items;
                //Session["platoonselectedItems"] = lstPlatoon.Items;
                schoolTitle = schoolTitle.Trim() == "" ? "ALL" : schoolTitle;
                platoonTitle = platoonTitle.Trim() == "" ? "ALL Platoons" : platoonTitle;
                accelTitle = ddlAccel.Text == "" ? "ALL" : ddlAccel.Text;

                if (accelTitle == "0")
                    accelTitle = "Normal";
                else if (accelTitle == "1")
                    accelTitle = "Accelerated";
                if (schoolTitle != "ALL")
                    schoolTitle = schoolTitle.Trim().Remove(schoolTitle.Trim().ToString().LastIndexOf(","));
                if (platoonTitle != "ALL Platoons")
                    platoonTitle = platoonTitle.Trim().Remove(platoonTitle.Trim().ToString().LastIndexOf(","));

                string dtFrom = Convert.ToDateTime(txtFromDate.Text).ToString("MMM dd, yyyy");
                string dtTo = Convert.ToDateTime(txtToDate.Text).ToString("MMM dd, yyyy");
                lblFilter.Text = "Filters Selected: ";
                QueryString = lblQueryTitle.Text = dtFrom + " to " + dtTo + " | " + schoolTitle + " | " + ddlStatus.SelectedItem + " | " + platoonTitle + " | " + ddlAccel.SelectedItem;
                Session["QueryString"] = lblQueryTitle.Text;
                BindData();
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
                PageMessage = ex.Message;
            }
        }

       

        //public override void VerifyRenderingInServerForm(Control control)
        //{

        //}

        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("MainInvoiceReport.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BindPlatoons()
        {
            try
            {
                DataSet sDt = new DataSet();
                sDt = SBDataAccess.GetReportPlatoons(DBConnectionString, schools);
                lstPlatoon.DataSource = sDt;
                lstPlatoon.DataTextField = "PlatoonNumber";
                lstPlatoon.DataValueField = "PlatoonNumber";
                lstPlatoon.DataBind();
                lstPlatoon.Items.Insert(0, new ListItem("ALL", " "));
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }

        private void BindSchools()
        {
            try
            {
                SBModel.SSchoolDataTable sDt = new SBModel.SSchoolDataTable();
                sDt = SBDataAccess.GetReportSchools(DBConnectionString);
                lstSchool.DataSource = sDt;
                lstSchool.DataTextField = "SchoolName";
                lstSchool.DataValueField = "SchoolID";
                lstSchool.DataBind();
                lstSchool.Items.Insert(0, new ListItem("ALL", " "));
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }

        private void GetPlatoon()
        {
            try
            {
                foreach (ListItem selecteditem in lstPlatoon.Items)
                {
                    if (selecteditem.Selected)
                    {
                        if (selecteditem.Value == " ")
                            continue;
                        platoon += selecteditem.Value + ",";
                        platoonTitle += selecteditem.Text + ", ";
                    }
                }
                if (platoon != " ")
                    platoon = platoon.Trim().Remove(platoon.Trim().ToString().LastIndexOf(","));
            }
            catch (Exception)
            {
                throw;
            }
        }

        SBModel.MainVoiceFilterCriteriaDataTable dateReport = new SBModel.MainVoiceFilterCriteriaDataTable();
        private void BindData()
        {
            string strReportName = "School Report";
            Dictionary<string, string> dctCRParams = new Dictionary<string, string>();
            //string fit = 1;
            try
            {
                dctCRParams.Add("FormDate", txtFromDate.Text);
                dctCRParams.Add("ToDate", txtToDate.Text);
                dctCRParams.Add("Status", Convert.ToInt32(ddlStatus.SelectedValue).ToString());
                dctCRParams.Add("Schools", schools);
                dctCRParams.Add("Platoon", platoon);
                dctCRParams.Add("Accel", Convert.ToInt32(ddlAccel.SelectedValue).ToString());
                dctCRParams.Add("QueryString", QueryString);
                Session["ReceiptCRParameters"] = dctCRParams;
                //Passing Report Name to ReportViewer through Client Script.
                RunScript("ShowReport('" + strReportName + "');");
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }

        private void DisableControls(Control gv)
        {
            LinkButton lb = new LinkButton();
            Literal l = new Literal();

            string name = String.Empty;

            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }

                if (gv.Controls[i].HasControls())
                {
                    DisableControls(gv.Controls[i]);
                }
            }
        }
    }
}