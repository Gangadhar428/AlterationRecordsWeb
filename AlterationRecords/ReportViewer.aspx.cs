using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Configuration;
using System.Text;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
//using AlterationRecords.Reports;
using AlterationRecords.Common;
using System.Net.Mail;
using System.Net;
using AlterationRecordsDataAccess;
using System.Data;
using CrystalDecisions.Web;
using System.Web.ModelBinding;


namespace AlterationRecords
{
    public partial class ReportViewer : PageBase
    {
        static TableLogOnInfo crTableLogonInfo;
        static ConnectionInfo crConnectionInfo;
        static Tables crTables;
        static Database crDatabase;
        ParameterFields paramFileds = new ParameterFields();
        ReportDocument rd = new ReportDocument();

        private string reportPath;
        /// <summary>
        /// When Page Load getting report name through Query String
        /// and passing it to method to get report.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hid_current_page.Value = "1";
                if (Request.QueryString["ReportName"] != null)
                {
                    if (Request.QueryString["pdfreport"] != null)
                    {
                        try
                        {
                            var reportName = Request.QueryString["ReportName"].ToString();
                            Dictionary<string, string> dctCRParams = new Dictionary<string, string>();
                            //Storing Dictionary Parameters of Report in Session
                            HttpContext.Current.Session["ReceiptCRParameters"] = dctCRParams;
                            HttpContext.Current.Session["ReportName"] = reportName;
                            int count = SBDataAccess.AccEmrReportTodaysCount(DBConnectionString);
                            if (count > 0)
                            {
                                if (Request.QueryString["mailFormat"] != null)
                                {
                                    CreateHTMLAndSendMail(DBConnectionString);
                                }
                                else
                                {
                                    ViewReport(Request.QueryString["ReportName"]);
                                    ExportAndSendMail(rd);
                                }
                            }
                            return;
                        }
                        catch (Exception ex)
                        {
                            //ExceptionHandler.HandleException(ProjectType.Web, ex);
                        }
                    }
                    else
                    {
                        ViewState["ReportName"] = Request.QueryString["ReportName"];
                        ViewReport(Request.QueryString["ReportName"]);
                        UpdatePageInfo();
					}
                }
            }
            else if (ViewState["ReportName"] != null)
            {
                //System.Threading.Tasks.Task.Run(async () =>
                //{
                //ViewReport(ViewState["ReportName"].ToString());
                //});
                //if (Session["ReportDocument"] != null)
                //{
                //    CRViewer.ReportSource = (ReportDocument)Session["ReportDocument"];
                //    CRViewer.DataBind();
                //}
                ReportDocument doc = (ReportDocument)Session["ReportDocument"];
                CRViewer.ReportSource = doc;
            }
        }
        /// <summary>
        /// Report will Display by getting with Report path , Parameters on ReportViewer
        /// </summary>
        /// <param name="reportName">name of required report name</param>
        private void ViewReport(string reportName)
        {
            try
            {
                switch (reportName)
                {
                    case "Signature Report":
                        reportPath = Path.Combine(GetServerPath(), "Reports", "SignatureReport.rpt");
                        rd = GetReportDocument(reportPath);
                        SetParameters(ref rd, reportName);
                        rd.SummaryInfo.ReportSubject = "Signature Report";
                        rptName.Text = "Signature Report Dress:";
                        break;
                    case "Signature Report Cammies":
                        reportPath = Path.Combine(GetServerPath(), "Reports", "SignatureReportCammies.rpt");
                        rd = GetReportDocument(reportPath);
                        SetParameters(ref rd, reportName);
                        rd.SummaryInfo.ReportSubject = "Signature Report Cammies";
                        rptName.Text = "Signature Report Cammies:";
                        break;
                    case "Signature Report Cammies New":
                        reportPath = Path.Combine(GetServerPath(), "Reports", "SignatureReportCammiesNew.rpt");
                        rd = GetReportDocument(reportPath);
                        SetParameters(ref rd, reportName);
                        rd.SummaryInfo.ReportSubject = "Signature Report Cammies New";
                        rptName.Text = "Signature Report Cammies New:";
                        break;
                    case "Signature Report Cammies Turn In":
                        reportPath = Path.Combine(GetServerPath(), "Reports", "SignatureReportCammiesTurnIn.rpt");
                        rd = GetReportDocument(reportPath);
                        SetParameters(ref rd, reportName);
                        rd.SummaryInfo.ReportSubject = "Signature Report Cammies Turn In";
                        rptName.Text = "Signature Report Cammies Turn In:";
                        break;
                    case "Alterations Per Fitting Report":
                        reportPath = Path.Combine(GetServerPath(), "Reports", "MainInvoiceReport.rpt");
                        rd = GetReportDocument(reportPath);
                        SetParameters(ref rd, reportName);
                        rd.SummaryInfo.ReportSubject = "Alterations Per Fitting Report";
                        rptName.Text = "Alterations Per Fitting Report :";
                        break;
                    case "School Report":
                        reportPath = Path.Combine(GetServerPath(), "Reports", "SchoolReport.rpt");
                        rd = GetReportDocument(reportPath);
                        SetParameters(ref rd, reportName);
                        rd.SummaryInfo.ReportSubject = "School Report :";
                        rptName.Text = "School Report";
                        break;
                    case "Signature Report BRCW":
                        reportPath = Path.Combine(GetServerPath(), "Reports", "SignatureReportBRCW.rpt");
                        rd = GetReportDocument(reportPath);
                        SetParameters(ref rd, reportName);
                        rd.SummaryInfo.ReportSubject = "Signature Report BRCW";
                        rptName.Text = "Signature Report BRCW :";
                        break;
                    case "Invoice Summary Report":
                        reportPath = Path.Combine(GetServerPath(), "Reports", "InvoiceSummaryDetail.rpt");
                        rd = GetReportDocument(reportPath);
                        SetParameters(ref rd, reportName);
                        rd.SummaryInfo.ReportSubject = "Invoice Summary Report";
                        rptName.Text = "Invoice Summary Report :";
                        break;
                    case "AR Invoice Summary":
                        reportPath = Path.Combine(GetServerPath(), "Reports", "ARInvoiceReport.rpt");
                        rd = GetReportDocument(reportPath);
                        SetParameters(ref rd, reportName);
                        rd.SummaryInfo.ReportSubject = "AR Invoice Summary";
                        rptName.Text = "AR Invoice Summary :";
                        break;
                    case "Acc EMR":
                        reportPath = Path.Combine(GetServerPath(), "Reports", "rptAcceleratedEmrReport.rpt");
                        rd = GetReportDocument(reportPath);
                        SetParameters(ref rd, reportName);
                        rd.SummaryInfo.ReportSubject = "Accelerated Emr";
                        rptName.Text = "Accelerated Emr :";
                        break;
                    case "Acc EMR Auth":
                        reportPath = Path.Combine(GetServerPath(), "Reports", "rptAccelerateEmergencyAuthorization.rpt");
                        rd = GetReportDocument(reportPath);
                        SetParameters(ref rd, reportName);
                        rd.SummaryInfo.ReportSubject = "Accelerate Emergency Authorization";
                        rptName.Text = "Accelerate Emergency Authorization :";
                        break;
                    case "AccEMREmail":
                        reportPath = Path.Combine(GetServerPath(), "Reports", "rptAcceleratedEmrReportEmail.rpt");
                        rd = GetReportDocument(reportPath);
                        SetParameters(ref rd, reportName);
                        rd.SummaryInfo.ReportSubject = "Accelerated Emr Email";
                        rptName.Text = "Accelerated Emr Email :";
                        break;
                    case "AccEMRAuthEmail":
                        reportPath = Path.Combine(GetServerPath(), "Reports", "rptAccelerateEmergencyAuthorizationEmail.rpt");
                        rd = GetReportDocument(reportPath);
                        SetParameters(ref rd, reportName);
                        rd.SummaryInfo.ReportSubject = "Accelerate Emergency Authorization Email";
                        rptName.Text = "Accelerate Emergency Authorization Email :";
                        break;
                    case "DM Summary Report":
                        reportPath = Path.Combine(GetServerPath(), "Reports", "DMSummaryReport.rpt");
                        rd = GetReportDocument(reportPath);
                        SetParameters(ref rd, reportName);
                        rd.SummaryInfo.ReportSubject = "DM Summary";
                        rptName.Text = "DM Summary :";
                        break;
                    case "DM Fulfillment Summary Report":
                        reportPath = Path.Combine(GetServerPath(), "Reports", "DMSummaryFulfillmentReport.rpt");
                        rd = GetReportDocument(reportPath);
                        SetParameters(ref rd, reportName);
                        rd.SummaryInfo.ReportSubject = "DM Fulfillment Summary";
                        rptName.Text = "DM Fulfillment Summary :";
                        break;

                }
                CRViewer.ReportSource = ReportLogin(rd, GetParamValueByConnStringParamName(DBConnectionString, "data source"), GetParamValueByConnStringParamName(DBConnectionString, "initial catalog"), GetParamValueByConnStringParamName(DBConnectionString, "user id"), GetParamValueByConnStringParamName(DBConnectionString, "password"), "Sonabend");
                //CRViewer.ReportSource = ReportLogin(rd, "IR-APPSERVER\\SQLSVR2017", "eboardsheet", "sa", "Inf0ra1s3*", "AlterationRecords");
                //CRViewer.ReportSource = ReportLogin(rd, GetParamValueByConnStringParamName(DBConnectionString, "data source"), GetParamValueByConnStringParamName(DBConnectionString, "initial catalog"), GetParamValueByConnStringParamName(DBConnectionString, "user id"), GetParamValueByConnStringParamName(DBConnectionString, "Inf0ra1s3*"), "CCWEBCOREDB");
            }
            catch (Exception ex)
            {
                //ExceptionHandler.HandleException(ProjectType.Web, ex);
            }
        }
        /// <summary>
        /// Report Parameters Set
        /// </summary>
        /// <param name="pReport">report document will load for report path</param>
        /// <param name="reportName">name of report</param>
        private void SetParameters(ref ReportDocument pReport, string reportName)
        {
            //Use viewer to get main report parameters
            CRViewer.ReportSource = pReport;

            int parameterCount = 0;
            parameterCount = CRViewer.ParameterFieldInfo.Count;
            for (int i = 0; i < parameterCount; i++)//parameterCount
            {
                try
                {
                    ParameterFieldDefinition parameterFieldDef = pReport.DataDefinition.ParameterFields[i];
                    parameterFieldDef.CurrentValues.Clear();
                    ParameterValues currentValues = new ParameterValues();

                    Dictionary<string, string> dctCrParams = new Dictionary<string, string>();
                    dctCrParams = (Dictionary<string, string>)Session["ReceiptCRParameters"];

                    string parameterName = parameterFieldDef.Name;

                    if (reportName != "")
                    {
                        currentValues = setToCurrentValuesRecipt(parameterName, currentValues, dctCrParams);
                    }
                    parameterFieldDef.ApplyCurrentValues(currentValues);
                }
                catch (Exception ex)
                {
                    // ExceptionHandler.HandleException(ProjectType.Web, ex);
                }
            }
        }
        /// <summary>
        /// Adding Current Report Parameters Values
        /// </summary>
        /// <param name="parameterName">Report Parameter name</param>
        /// <param name="currentValues">Report Parameter values</param>
        /// <param name="ParamsList">List of Parameters for that report</param>
        /// <returns></returns>
        private ParameterValues setToCurrentValuesRecipt(string parameterName, ParameterValues currentValues, Dictionary<string, string> ParamsList)
        {
            switch (parameterName.ToLower())
            {
                case "@fromdate":
                    ParameterDiscreteValue discreteValueDateFrom = new ParameterDiscreteValue();
                    discreteValueDateFrom.Value = ParamsList["FormDate"];
                    currentValues.Add(discreteValueDateFrom);
                    break;

                case "@todate":
                    ParameterDiscreteValue discreteValueDateTO = new ParameterDiscreteValue();
                    discreteValueDateTO.Value = ParamsList["ToDate"];
                    currentValues.Add(discreteValueDateTO);
                    break;

                case "@schoolid":
                    ParameterDiscreteValue discreteValueSchoolID = new ParameterDiscreteValue();
                    discreteValueSchoolID.Value = ParamsList["Schools"] == null ? Convert.DBNull : ParamsList["Schools"];
                    currentValues.Add(discreteValueSchoolID);
                    break;
                case "@component":
                    ParameterDiscreteValue discreteValueCOMPONENT = new ParameterDiscreteValue();
                    discreteValueCOMPONENT.Value = ParamsList["Status"];
                    currentValues.Add(discreteValueCOMPONENT);
                    break;
                case "@platoon":
                    ParameterDiscreteValue discreteValuePLATOON = new ParameterDiscreteValue();
                    discreteValuePLATOON.Value = ParamsList["Platoon"];
                    currentValues.Add(discreteValuePLATOON);
                    break;
                case "@measurementattributeid":
                    ParameterDiscreteValue discreteValueMeasurementAttributeID = new ParameterDiscreteValue();
                    discreteValueMeasurementAttributeID.Value = ParamsList["Accel"];
                    currentValues.Add(discreteValueMeasurementAttributeID);
                    break;
                case "searchcriteria":
                    ParameterDiscreteValue QueryString = new ParameterDiscreteValue();
                    QueryString.Value = ParamsList["QueryString"];
                    currentValues.Add(QueryString);
                    break;
            }

            return currentValues;
        }

        /// <summary>
        /// ReportLogin Crdentials
        /// </summary>
        /// <param name="crDoc">crystal report document load for report</param>
        /// <param name="Server">reoprt contained server</param>
        /// <param name="Database">database name of report</param>
        /// <param name="UserID">Valid User Id for database server</param>
        /// <param name="Password">valid password for database server</param>
        /// <returns></returns>
        public static ReportDocument ReportLogin(ReportDocument crDoc, string Server, string Database, string UserID, string Password, string DSNName)
        {
            crConnectionInfo = new ConnectionInfo();

            crConnectionInfo.ServerName = Server;
            crConnectionInfo.DatabaseName = Database;
            crConnectionInfo.UserID = UserID;
            crConnectionInfo.Password = Password;
            crDatabase = crDoc.Database;
            crTables = crDatabase.Tables;

            foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
            {
                crTableLogonInfo = crTable.LogOnInfo;
                crTableLogonInfo.ConnectionInfo = crConnectionInfo;
                crTable.ApplyLogOnInfo(crTableLogonInfo);
            }

            foreach (CrystalDecisions.CrystalReports.Engine.ReportDocument sro in crDoc.Subreports)
            {
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in sro.Database.Tables)
                {
                    TableLogOnInfo logon = table.LogOnInfo;
                    String connection = Server;
                    int index = connection.IndexOf("=");
                    connection = connection.Substring(index + 1).Trim();

                    logon.ConnectionInfo.ServerName = connection;
                    logon.ConnectionInfo = crConnectionInfo;
                    table.ApplyLogOnInfo(logon);
                    logon.ConnectionInfo.Type = ConnectionInfoType.SQL;
                }
            }
            HttpContext.Current.Session["ReportDocument"] = crDoc;
            return crDoc;
        }
        private void Page_Init(object sender, EventArgs e)
        {
            if (Session["ReportDocument"] != null)
            {
                CRViewer.ReportSource = (ReportDocument)Session["ReportDocument"];
                CRViewer.DataBind();
            }
        }
        private void Page_Unload(object sender, EventArgs e)
        {
            //rd.Close();
            //rd.Clone();
            //rd.Dispose();
            //rd = null;
            //GC.Collect();
            //CRViewer.Dispose();
        }

        public static void CreateHTMLAndSendMail(string ConString)
        {
            try
            {
                //Get Date from DB
                DataTable dt = SBDataAccess.GetAccEmrReportToday(ConString);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var AcceleratedList = (from rw in dt.AsEnumerable()
                                           select new AcceleratedEmergencyAuth()
                                           {
                                               PlatoonNumber = rw["PlatoonNumber"].ToString(),
                                               MeasurementDate = Convert.ToDateTime(rw["MeasurementDate"]),
                                               Accelerated = Convert.ToBoolean(rw["Accelerated"]),
                                               Emergency = Convert.ToBoolean(rw["Emergency"]),
                                               Component = rw["Component"].ToString(),
                                               SchoolName = rw["SchoolName"].ToString(),
                                               RecruitName = rw["RecruitName"].ToString()
                                           }).Where(x => x.Accelerated).ToList();
                    var EmergencyList = (from rw in dt.AsEnumerable()
                                         select new AcceleratedEmergencyAuth()
                                         {
                                             PlatoonNumber = rw["PlatoonNumber"].ToString(),
                                             MeasurementDate = Convert.ToDateTime(rw["MeasurementDate"]),
                                             Accelerated = Convert.ToBoolean(rw["Accelerated"]),
                                             Emergency = Convert.ToBoolean(rw["Emergency"]),
                                             Component = rw["Component"].ToString(),
                                             SchoolName = rw["SchoolName"].ToString(),
                                             RecruitName = rw["RecruitName"].ToString()
                                         }).Where(x => x.Emergency).ToList();
                    string message = "";
                    message = ReadMailContent();
                    string AccTblContent = "";
                    string EmrTblContent = "";
                    if (AcceleratedList != null && AcceleratedList.Count > 0)
                    {
                        AccTblContent += "<table>";
                        AccTblContent += "<thead>";
                        AccTblContent += "<tr>";
                        AccTblContent += "<th style='width:20%;'>ACCELERATED</th>";
                        AccTblContent += "<th style='width:20%'>Class</th>";
                        AccTblContent += "<th style='width:30%'>Recruit Name</th>";
                        AccTblContent += "<th style='width:20%'>School</th>";
                        AccTblContent += "<th style='width:10%'>Type</th>";
                        AccTblContent += "</tr>";
                        AccTblContent += "</thead>";
                        AccTblContent += "<tbody>";
                        foreach (var li in AcceleratedList)
                        {
                            AccTblContent += "<tr>";
                            AccTblContent += "<td></td>";
                            AccTblContent += "<td>" + li.PlatoonNumber + "</td>";
                            AccTblContent += "<td>" + li.RecruitName + "</td>";
                            AccTblContent += "<td>" + li.SchoolName + "</td>";
                            AccTblContent += "<td>" + li.Component + "</td>";
                            AccTblContent += "</tr>";
                        }
                        AccTblContent += "</tbody>";
                        AccTblContent += "</table>";
                    }
                    if (EmergencyList != null && EmergencyList.Count > 0)
                    {
                        EmrTblContent += "<table>";
                        EmrTblContent += "<thead>";
                        EmrTblContent += "<tr>";
                        EmrTblContent += "<th style='width:20%;'>EMERGENCY</th>";
                        EmrTblContent += "<th style='width:20%'>Class</th>";
                        EmrTblContent += "<th style='width:30%'>Recruit Name</th>";
                        EmrTblContent += "<th style='width:20%'>School</th>";
                        EmrTblContent += "<th style='width:10%'>Type</th>";
                        AccTblContent += "</tr>";
                        AccTblContent += "</thead>";
                        AccTblContent += "<tbody>";
                        foreach (var li in EmergencyList)
                        {
                            EmrTblContent += "<tr>";
                            EmrTblContent += "<td></td>";
                            EmrTblContent += "<td>" + li.PlatoonNumber + "</td>";
                            EmrTblContent += "<td>" + li.RecruitName + "</td>";
                            EmrTblContent += "<td>" + li.SchoolName + "</td>";
                            EmrTblContent += "<td>" + li.Component + "</td>";
                            EmrTblContent += "</tr>";
                        }
                        AccTblContent += "</tbody>";
                        EmrTblContent += "</table>";
                    }
                    string tableData = "";
                    tableData += AccTblContent;
                    tableData += EmrTblContent;

                    message = message.Replace("##DATE##", DateTime.Now.ToString("MM/dd/yyyy"));
                    message = message.Replace("##TabelData##", tableData);
                    try
                    {
                        string fromEmailId = ConfigurationManager.AppSettings["FromMailID"];
                        string toEmailId = ConfigurationManager.AppSettings["ToMailID"];
                        string ccEmailId = ConfigurationManager.AppSettings["CCMailID"];

                        SmtpClient client = new SmtpClient();
                        MailAddress from = new MailAddress(fromEmailId, fromEmailId, Encoding.UTF8);
                        MailAddress to = new MailAddress(toEmailId);

                        MailMessage mailmessage = new MailMessage(from, to);
                        if (ccEmailId != null && ccEmailId != "")
                        {
                            string[] ccIds = ccEmailId.Split(',');
                            foreach (var id in ccIds)
                            {
                                mailmessage.CC.Add(new MailAddress(id));
                            }
                        }
                        mailmessage.Body = message;
                        mailmessage.IsBodyHtml = true;
                        mailmessage.BodyEncoding = System.Text.Encoding.UTF8;

                        mailmessage.Subject = "COR Accelerated Authorization";
                        mailmessage.SubjectEncoding = System.Text.Encoding.UTF8;

                        //mailmessage.Attachments.Add(new Attachment(pdfFile));
                        client.EnableSsl = true;
                        client.Send(mailmessage);
                        mailmessage.Dispose();
                    }
                    catch (Exception e1)
                    {


                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private static string ReadMailContent()
        {
            string emailFilePath = System.Web.HttpContext.Current.Server.MapPath("~") + "\\AcceleratedAuth.html";
            System.IO.TextReader txtreader = null;
            try
            {
                txtreader = System.IO.File.OpenText(emailFilePath);
                return txtreader.ReadToEnd();
            }
            catch
            {
                return "";
            }
            finally
            {
                if (txtreader != null)
                {
                    try
                    {
                        txtreader.Close();
                        txtreader = null;
                    }
                    catch { }
                }
            }
        }

        public class AcceleratedEmergencyAuth
        {
            public string PlatoonNumber { get; set; }
            public DateTime MeasurementDate { get; set; }
            public bool Accelerated { get; set; }
            public bool Emergency { get; set; }
            public string Component { get; set; }
            public string SchoolName { get; set; }
            public string RecruitName { get; set; }
        }

        public static void ExportAndSendMail(ReportDocument cryRpt)
        {
            string fileName = "";
            if (HttpContext.Current.Request.QueryString["ReportName"] != null && HttpContext.Current.Request.QueryString["ReportName"].ToString() != "")
            {
                string reportName = HttpContext.Current.Request.QueryString["ReportName"].ToString();
                if (reportName == "AccEMREmail")
                {
                    fileName = "Accelerated";
                }
                else if (reportName == "AccEMRAuthEmail")
                {
                    fileName = "AcceleratedAuthorization";
                }
                else
                {
                    fileName = "EmailReport";
                }
            }
            else
            {
                fileName = "EmailReport";
            }
            string pdfFile = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~"), fileName + ".pdf");
            try
            {

                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = pdfFile;
                CrExportOptions = cryRpt.ExportOptions;
                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;
                cryRpt.Export();
            }
            catch (Exception ex)
            {

            }
            try
            {
                string fromEmailId = ConfigurationManager.AppSettings["FromMailID"];
                string toEmailId = ConfigurationManager.AppSettings["ToMailID"];
                string ccEmailId = ConfigurationManager.AppSettings["CCMailID"];

                SmtpClient client = new SmtpClient();
                MailAddress from = new MailAddress(fromEmailId, fromEmailId, Encoding.UTF8);
                MailAddress to = new MailAddress(toEmailId);

                MailMessage mailmessage = new MailMessage(from, to);
                if (ccEmailId != null && ccEmailId != "")
                {
                    string[] ccIds = ccEmailId.Split(',');
                    foreach (var id in ccIds)
                    {
                        mailmessage.CC.Add(new MailAddress(id));
                    }
                }
                mailmessage.Body = "Attached is the latest Accelerated Authorization notice.";
                mailmessage.IsBodyHtml = true;
                mailmessage.BodyEncoding = System.Text.Encoding.UTF8;

                mailmessage.Subject = "COR Accelerated Authorization";
                mailmessage.SubjectEncoding = System.Text.Encoding.UTF8;

                mailmessage.Attachments.Add(new Attachment(pdfFile));
                //client.EnableSsl = true;
                client.Send(mailmessage);
                mailmessage.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
        private void UpdatePageInfo()
        {
            try
            {
                btnFirst_Click(null, null);

			}
            catch (Exception ex)
            {
                lblTotPages.Text = lblCurrentPage.Text = "";
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            // CRViewer.ShowNextPage();
            int current_page = Convert.ToInt32(hid_current_page.Value);
            int last_page = getLastPageNumber(CRViewer);
            //To check if Next button is clicked when current page No. is set to last page.
            if ((current_page + 1) > last_page)
            {
                hid_current_page.Value = Convert.ToString(last_page);
                CRViewer.ShowNthPage(last_page);
            }
            else
            {
                hid_current_page.Value = Convert.ToString(current_page + 1);
                CRViewer.ShowNthPage(current_page + 1);
            }
			lblCurrentPage.Text = hid_current_page.Value;
			lblTotPages.Text = last_page.ToString();
			//CRViewer.RefreshReport();
		}
        private int getLastPageNumber(CrystalReportViewer crview)
        {
            crview.ShowLastPage();  //To navigate to the last page no. 
            CrystalDecisions.Web.ViewInfo vi = crview.ViewInfo; //Get info of the navigated page 
            return vi.PageNumber;
        }
        protected void btnPrew_Click(object sender, EventArgs e)
        {
            //CRViewer.ShowPreviousPage();
            // Extract on which page currently report is set to.
            int current_page = Convert.ToInt32(hid_current_page.Value);
			//lblTotPages.Text = getLastPageNumber(CRViewer, true).ToString();
			//To check if Previous button is clicked when current page No. is set to first page 
			if ((current_page - 1) < 1)
            {
                //If so, set crystal report to first page.
                hid_current_page.Value = "1";
                CRViewer.ShowNthPage(1);				;
			}
            else
            {
                // If not set to one previous page according to current page number.
                hid_current_page.Value = Convert.ToString(current_page - 1);
                CRViewer.ShowNthPage(current_page - 1);
            }
            lblCurrentPage.Text = hid_current_page.Value;
			// Make sure to refresh the report viewer
			//CRViewer.RefreshReport();
		}

        protected void btnFirst_Click(object sender, EventArgs e)
        { 
            lblTotPages.Text = getLastPageNumber(CRViewer).ToString();
            CRViewer.ShowFirstPage(); // We are sure that user want to see first page
            //CRViewer.RefreshReport();
            hid_current_page.Value = "1"; // we will reset hidden page value to 1
			lblCurrentPage.Text = hid_current_page.Value;
		}

        protected void btnLats_Click(object sender, EventArgs e)
        {
            int last_page = getLastPageNumber(CRViewer);
            hid_current_page.Value = Convert.ToString(last_page);
            CRViewer.ShowNthPage(last_page);         //To navigate to the last page no.
													 //CRViewer.RefreshReport();
			lblCurrentPage.Text = hid_current_page.Value;
			//lblTotPages.Text = getLastPageNumber(CRViewer, false).ToString();
		}
    }
}