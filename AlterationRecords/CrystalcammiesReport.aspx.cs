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
using System.Web.UI.HtmlControls;

namespace AlterationRecords
{
    public partial class CrystalcammiesReport : PageBase
    {
        public string schools = " ";
        public string platoon = " ";
        string schoolTitle = "";
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
                if (schools.Contains("3"))
                    schools = schools.Replace("3", "0");
                if (Session["fromdate"] != null) // && Session["schoolselectedItems"] != null && Session["platoonselectedItems"] != null)
                {
                    //ListItemCollection lst = (ListItemCollection)Session["schoolselectedItems"];
                    //ListItemCollection lstpla = (ListItemCollection)Session["platoonselectedItems"];
                    //for (int i = 0; i < lst.Count; i++)
                    //{
                    //    lstSchool.Items[i].Selected = lst[i].Selected;
                    //}
                    //for (int i = 0; i < lstpla.Count; i++)
                    //{
                    //    lstPlatoon.Items[i].Selected = lstpla[i].Selected;
                    //}
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
                    if (selecteditem.Selected && selecteditem.Text != "BRC Women")
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
                //if (schools.Contains("3"))
                //    schools = schools.Replace("3", "0");
                //BindPlatoons();
                GetPlatoon();
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
                sDt = SBDataAccess.GetReportSchoolsSigs(DBConnectionString);
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
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }

        DataTable platoonReport = new DataTable();
        private void BindData()
        {
            string strReportName = "Signature Report Cammies";
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
            //try
            //{
            //    platoonReport = SBDataAccess.PlatoonFilter(DBConnectionString, txtFromDate.Text, txtToDate.Text, schools, Convert.ToInt32(ddlStatus.SelectedValue), platoon, Convert.ToInt32(ddlAccel.SelectedValue));
            //    string[] columnNames = { "PlatoonNumber", "IsFirstFit", "IsSecondFit", "SubsequentFit", "MeasureDate", "SonabendUser", "SupervisorUser" };
            //    DataTable distinctSchoolTable = platoonReport.DefaultView.ToTable(true, columnNames);

            //    rptrDress.DataSource = distinctSchoolTable;
            //    rptrDress.DataBind();

            //    Label lblExcelHeader = (Label)rptrDress.Controls[0].Controls[0].FindControl("lblExportLabel");
            //    lblExcelHeader.CssClass = "dressbold";
            //    if (distinctSchoolTable.Rows.Count > 0)
            //    {
            //        lbtnExport.Visible = true;
            //    }
            //    else
            //    {
            //        lblExcelHeader.Visible = true;
            //        lblExcelHeader.Text = "No Records found for the search criteria";
            //        lbtnExport.Visible = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            //}
        }
    }
}