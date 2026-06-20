using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlterationRecords.Common;
using AlterationRecordsModel;
using System.Data;
using AlterationRecordsDataAccess;
using System.IO;

namespace AlterationRecords
{
    public partial class CrystalDMFulfillmentSummaryReport : PageBase
    {
        public string schools = " ";
        public string platoon = " ";
        string schoolTitle = "";
        string platoonTitle = "";
        string accelTitle = "";
        string QueryString = "";
        SBModel.MainVoiceFilterCriteriaDataTable sdt = new SBModel.MainVoiceFilterCriteriaDataTable();
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

                if (Session["fromdate"] != null && Session["platoonselectedItems"] != null)
                {
                    ListItemCollection lstpla = (ListItemCollection)Session["platoonselectedItems"];
                    ListItemCollection lst;
                    if (Session["schoolselectedItems"].ToString() != "")
                    {
                        lst = (ListItemCollection)Session["schoolselectedItems"];
                        for (int i = 0; i < lst.Count; i++)
                        {
                            lstSchool.Items[i].Selected = lst[i].Selected;
                        }
                    }
                    for (int i = 0; i < lstpla.Count; i++)
                    {
                        lstPlatoon.Items[i].Selected = lstpla[i].Selected;
                    }
                    //BindRepeater();
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
        double accelTotal = 0;
        double RTRTotal = 0;
        double BRCTotal = 0;

        private void BindRepeater()
        {

            //DBConnectionString, txtFromDate.Text, txtToDate.Text, schools, Convert.ToInt32(ddlStatus.SelectedValue), platoon, Convert.ToInt32(ddlAccel.SelectedValue), MeasurementID, isFirstFIt, isSecondFit, subsequent
            string strReportName = "DM Fulfillment Summary Report";
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
        double TotalAmount = 0;
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var allSchools = "";
                foreach (ListItem selecteditem in lstSchool.Items)
                {
                    if (selecteditem.Value != " ")
                        allSchools += selecteditem.Value + ",";
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
                {
                    schools = schools.Trim().Remove(schools.Trim().ToString().LastIndexOf(","));
                }
                else
                {
                    schools = allSchools.Trim().Remove(allSchools.Trim().ToString().LastIndexOf(",")); ;
                }
                GetPlatoon();
                Session["fromDate"] = txtFromDate.Text;
                Session["ToDate"] = txtToDate.Text;
                Session["Platoon"] = platoon;
                Session["Schools"] = schools;
                Session["Component"] = ddlStatus.SelectedValue;
                Session["Accel"] = ddlAccel.SelectedValue;
                Session["schoolselectedItems"] = lstSchool.Items;
                Session["platoonselectedItems"] = lstPlatoon.Items;
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
                lblQueryTitle.Text = dtFrom + " to " + dtTo + " | " + schoolTitle + " | " + ddlStatus.SelectedItem + " | " + platoonTitle + " | " + ddlAccel.SelectedItem;
                Session["QueryString"] = QueryString = lblQueryTitle.Text;
                //BindGrid();
                BindRepeater();

            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
                PageMessage = ex.Message;
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
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
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

    }
}