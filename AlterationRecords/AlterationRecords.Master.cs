using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlterationRecords.Common;
using CrystalDecisions.Web;

namespace AlterationRecords
{
    public partial class AlterationRecords : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedUser"] == null)
            {
                Response.Redirect("Logout.aspx", true);
            }
            if (!Page.IsPostBack)
            {
                Identity lUser = (Identity)Session["LoggedUser"];
                //liTransactionRpt.Visible = lUser.ISSuperUser;
                lblUser.Text = lUser.Name;
                lblUserFName.Text = $"{lUser.Name.FirstOrDefault()}";
                SetMenuActive();

                if (lUser.Role.ToLower().IndexOf("staff") > -1)
                {
                    anchSettings.Visible = false;
                    //anchPrice.Visible = false;
                }

            }
        }
        private void SetMenuActive()
        {
            if (this.Page.ToString().ToLower().IndexOf("user") != -1)
            {
                liUsers.Attributes.Add("class", "slide-item active");
                liSettings.Attributes.Add("class", "slide is-expanded");
                liSettingsSubmenu.Attributes.Add("class", "slide-menu open");
                liSettingsSubmenu.Style.Add("display", "block");
                anchSettings.Attributes.Add("class", "side-menu__item active");
            }
            else if (this.Page.ToString().ToLower().IndexOf("roleslist") != -1)
            {
                liRoles.Attributes.Add("class", "slide-item active");
                liSettings.Attributes.Add("class", "slide is-expanded");
                liSettingsSubmenu.Attributes.Add("class", "slide-menu open");
                liSettingsSubmenu.Style.Add("display", "block");
                anchSettings.Attributes.Add("class", "side-menu__item active");
            }
            else if (this.Page.ToString().ToLower().IndexOf("schoolslist") != -1)
            {
                liSchools.Attributes.Add("class", "slide-item active");
                liSettings.Attributes.Add("class", "slide is-expanded");
                liSettingsSubmenu.Attributes.Add("class", "slide-menu open");
                liSettingsSubmenu.Style.Add("display", "block");
                anchSettings.Attributes.Add("class", "side-menu__item active");
            }
            else if (this.Page.ToString().ToLower().IndexOf("upload") != -1)//|| this.Page.ToString().ToLower().IndexOf("changepassword") != -1
            {
                //liUpload.Attributes.Add("class", "selected");
                liUpload.Attributes.Add("class", "slide-item active");
                liFiles.Attributes.Add("class", "slide is-expanded");
                liFilesSubmenu.Attributes.Add("class", "slide-menu open");
                liFilesSubmenu.Style.Add("display", "block");
                anchFiles.Attributes.Add("class", "side-menu__item active");
            }
            else if (this.Page.ToString().ToLower().IndexOf("file") != -1)
            {
                liViewList.Attributes.Add("class", "slide-item active");
                liFiles.Attributes.Add("class", "slide is-expanded");
                liFilesSubmenu.Attributes.Add("class", "slide-menu open");
                liFilesSubmenu.Style.Add("display", "block");
                anchFiles.Attributes.Add("class", "side-menu__item active");
            }
            else if (this.Page.ToString().ToLower().IndexOf("pricedefinitionscreen") != -1)
            {
                liDefinePrice.Attributes.Add("class", "slide-item active");
                liPrice.Attributes.Add("class", "slide is-expanded");
                liPriceSubmenu.Attributes.Add("class", "slide-menu open");
                liPriceSubmenu.Style.Add("display", "block");
                anchPrice.Attributes.Add("class", "side-menu__item active");
            }
            else if (this.Page.ToString().ToLower().IndexOf("maininvoicereport") != -1 || this.Page.ToString().ToLower().Contains("maininvoiceacceldetails"))
            {

                if (this.Page.ToString().ToLower().IndexOf("crystalmaininvoicereport") != -1)
                {
                    //liCrystalMain.Attributes.Add("class", "selected");
                    liSildeReport.Attributes.Add("class", "slide is-expanded");
                    liReportSubmenu.Attributes.Add("class", "slide-menu open");
                    liReportSubmenu.Style.Add("display", "block");
                    anchReport.Attributes.Add("class", "side-menu__item active");
                    liReport.Attributes.Add("class", "slide-item active");
                }
                else
                {
                    //liReport.Attributes.Add("class", "selected");
                }
            }
            else if (this.Page.ToString().ToLower().IndexOf("datereport") != -1)
            {
                //liDateReport.Attributes.Add("class", "selected");
            }
            else if (this.Page.ToString().ToLower().IndexOf("schoolreport") != -1)
            {
                if (this.Page.ToString().ToLower().IndexOf("crystalschoolreport") != -1)
                {
                    //liCrystalSchool.Attributes.Add("class", "selected");

                    liSildeReport.Attributes.Add("class", "slide is-expanded");
                    liReportSubmenu.Attributes.Add("class", "slide-menu open");
                    liReportSubmenu.Style.Add("display", "block");
                    anchReport.Attributes.Add("class", "side-menu__item active");
                    liSchoolsReport.Attributes.Add("class", "slide-item active");
                }
                else
                {
                    //liSchoolReport.Attributes.Add("class", "selected");
                }
                // liSchoolReport.Attributes.Add("class", "selected");
            }
            else if (this.Page.ToString().ToLower().IndexOf("platoonreport") != -1)
            {
                if (this.Page.ToString().ToLower().IndexOf("crystalplatoonreport") != -1)
                {
                    //liCrystalPlatoon.Attributes.Add("class", "selected");
                    liSildeReport.Attributes.Add("class", "slide is-expanded");
                    liReportSubmenu.Attributes.Add("class", "slide-menu open");
                    liReportSubmenu.Style.Add("display", "block");
                    anchReport.Attributes.Add("class", "side-menu__item active");
                    liCrystalPlatoon.Attributes.Add("class", "slide-item active");
                }
                else
                {
                    //liPlatoonReport.Attributes.Add("class", "selected");
                }
            }
            else if (this.Page.ToString().ToLower().IndexOf("crystalcammiesreportnew") != -1)
            {
                //liCrystalCammiesNew.Attributes.Add("class", "selected");
            }
            else if (this.Page.ToString().ToLower().IndexOf("crystalcammiesreportturnin") != -1)
            {
                //liCrystalCammiesTurnIn.Attributes.Add("class", "selected");
                liSildeReport.Attributes.Add("class", "slide is-expanded");
                liReportSubmenu.Attributes.Add("class", "slide-menu open");
                liReportSubmenu.Style.Add("display", "block");
                anchReport.Attributes.Add("class", "side-menu__item active");
                liCrystalCammiesTurnIn.Attributes.Add("class", "slide-item active");
            }
            else if (this.Page.ToString().ToLower().IndexOf("crystalcammiesreport") != -1)
            {
                //liCrystalCammies.Attributes.Add("class", "selected");
                liSildeReport.Attributes.Add("class", "slide is-expanded");
                liReportSubmenu.Attributes.Add("class", "slide-menu open");
                liReportSubmenu.Style.Add("display", "block");
                anchReport.Attributes.Add("class", "side-menu__item active");
                liCrystalCammies.Attributes.Add("class", "slide-item active");
            }
            else if (this.Page.ToString().ToLower().IndexOf("signaturereportbrcw") != -1)
            {

                if (this.Page.ToString().ToLower().IndexOf("crystalsignaturereportbrcw") != -1)
                {
                    //liCrystalBRCW.Attributes.Add("class", "selected");
                    liSildeReport.Attributes.Add("class", "slide is-expanded");
                    liReportSubmenu.Attributes.Add("class", "slide-menu open");
                    liReportSubmenu.Style.Add("display", "block");
                    anchReport.Attributes.Add("class", "side-menu__item active");
                    liCrystalBRCW.Attributes.Add("class", "slide-item active");
                }
                else
                {
                    //lisignature.Attributes.Add("class", "selected");
                }

            }
            else if (this.Page.ToString().ToLower().IndexOf("installation") != -1 || this.Page.ToString().ToLower().IndexOf("welcome") != -1 || this.Page.ToString().ToLower().IndexOf("workflow") != -1 || this.Page.ToString().ToLower().IndexOf("releasenotes") != -1)
            {
                //liinstallation.Attributes.Add("class", "selected");
                anchinstallation.Attributes.Add("class", "side-menu__item active");
            }
            else if (this.Page.ToString().ToLower().IndexOf("crystalinvoicesummaryreportnew") != -1)
            {
                //liInvoiceSummaryNew.Attributes.Add("class", "selected");
                liSildeReport.Attributes.Add("class", "slide is-expanded");
                liReportSubmenu.Attributes.Add("class", "slide-menu open");
                liReportSubmenu.Style.Add("display", "block");
                anchReport.Attributes.Add("class", "side-menu__item active");
                liInvoiceSummaryNew.Attributes.Add("class", "slide-item active");
            }
            else if (this.Page.ToString().ToLower().IndexOf("accemrauth") != -1)
            {
                //liAccEMRAuth.Attributes.Add("class", "selected");
                liSildeReport.Attributes.Add("class", "slide is-expanded");
                liReportSubmenu.Attributes.Add("class", "slide-menu open");
                liReportSubmenu.Style.Add("display", "block");
                anchReport.Attributes.Add("class", "side-menu__item active");
                liAccEMRAuth.Attributes.Add("class", "slide-item active");
            }
            else if (this.Page.ToString().ToLower().IndexOf("accemr") != -1)
            {
                //liAccEMR.Attributes.Add("class", "selected");
                liSildeReport.Attributes.Add("class", "slide is-expanded");
                liReportSubmenu.Attributes.Add("class", "slide-menu open");
                liReportSubmenu.Style.Add("display", "block");
                anchReport.Attributes.Add("class", "side-menu__item active");
                liAccEMR.Attributes.Add("class", "slide-item active");
            }
            else if (this.Page.ToString().ToLower().IndexOf("crystaldmfulfillmentsummaryreport") != -1)
            {
                liSildeReport.Attributes.Add("class", "slide is-expanded");
                liReportSubmenu.Attributes.Add("class", "slide-menu open");
                liReportSubmenu.Style.Add("display", "block");
                anchReport.Attributes.Add("class", "side-menu__item active");
                liDMFullfillmentSummary.Attributes.Add("class", "slide-item active");
            }
            else if (this.Page.ToString().ToLower().IndexOf("crystaldmsummaryreport") != -1)
            {
                // liDMSummary.Attributes.Add("class", "selected");
                liSildeReport.Attributes.Add("class", "slide is-expanded");
                liReportSubmenu.Attributes.Add("class", "slide-menu open");
                liReportSubmenu.Style.Add("display", "block");
                anchReport.Attributes.Add("class", "side-menu__item active");
                liDMSummary.Attributes.Add("class", "slide-item active");
            }
            //else if (this.Page.ToString().ToLower().IndexOf("crystalinvoicesummaryreport") != -1)
            //{
            //    liInvoiceSummary.Attributes.Add("class", "selected");
            //}

            if (this.Page.ToString().ToLower().IndexOf("datereportbyfiltercriteria") != -1)
            {
                //sidebar.Visible = false;
                //docmenu.Attributes.Remove("class");
                //main_panel_container.Attributes.Remove("class");
            }

            //if (this.Page.ToString().ToLower().IndexOf("welcome") != -1)
            //{
            //    sidebar.Visible = false;
            //    docmenu.Attributes.Remove("class");
            //}
            //else if (this.Page.ToString().ToLower().IndexOf("installation") != -1)
            //{
            //    sidebar.Visible = false;
            //    docmenu.Attributes.Remove("class");
            //}
            //else if (this.Page.ToString().ToLower().IndexOf("workflow") != -1)
            //{
            //    sidebar.Visible = false;
            //    docmenu.Attributes.Remove("class");
            //}
            //else if (this.Page.ToString().ToLower().IndexOf("tabletclientreleasenotes") != -1)
            //{
            //    sidebar.Visible = false;
            //    docmenu.Attributes.Remove("class");
            //}
            //else
            //{
            //    sidebar.Visible = true;
            //    docmenu.Attributes.Add("class", "top_nomenu");
            //}
        }
    }
}