using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace AlterationRecords.Common
{
    public class PageBase : Page
    {
        public PageBase()
        {
            SetConnectionString();
        }

        private string strPageMessage = string.Empty;
        public string PageMessage
        {
            get { return strPageMessage; }
            set { strPageMessage = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            SetPageMessge();
            base.OnPreRender(e);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (Request.Url.ToString().IndexOf("http:") >= 0 &&
                Convert.ToBoolean(ConfigurationManager.AppSettings["RedirectSSL"]) == true)
            {
                Response.Redirect(Request.Url.ToString().Replace("http://", "https://"), true);
            }

            CheckAuthentication();

            if (this.DesignMode == true)
            {
                this.EnsureChildControls();
            }
            this.Page.RegisterRequiresControlState(this);
        }

        private void CheckAuthentication()
        {
            string pageName = Page.ToString().ToLower();

            if (pageName.IndexOf("reportviewer") >= 0 && pageName.IndexOf("pdfreport") > 0)
            {
                return;
            }


            if (this.Session["LoggedUser"] == null && pageName.IndexOf("login") == -1 && pageName.IndexOf("reportviewer") == -1)
            {
                Response.Redirect("Login.aspx");
            }
        }

        /// <summary>
        /// To Run Script
        /// </summary>
        /// <param name="message"></param>
        public void RunScript(string message)
        {
            string ss = string.Format("jAlert('{0}');", message.Replace("'", "\\'").Replace("\r\n", "\\n"));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jscript", message, true);
        }

        /// <summary>
        /// This method declares the page message variable
        /// in the javascript ands sets the page message
        /// </summary>
        public void SetPageMessge()
        {
            ClientScriptManager cs = base.ClientScript;
            cs.RegisterHiddenField("PageMessage", PageMessage);
            PageMessage = string.Empty;
        }

        private string connectionString = string.Empty;
        public string DBConnectionString
        {
            get
            {
                return connectionString;
            }
        }


        internal string HttpsBase
        {
            get
            {
                string strProtocol = "https://";
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["RedirectSSL"]) == false)
                {
                    strProtocol = "http://";
                }
                return strProtocol + UrlSuffix;
            }
        }

        internal string HttpBase
        {
            get
            {
                return "http://" + UrlSuffix;
            }
        }

        internal string UrlSuffix
        {
            get
            {
                string strUrlSuffix;
                strUrlSuffix = System.Web.HttpContext.Current.Request.Url.Host;
                return strUrlSuffix + System.Web.HttpContext.Current.Request.ApplicationPath;
            }
        }

        private void SetConnectionString()
        {
            try
            {
                connectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get date in mm/dd/yyyy format
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public string GetDate(string strDate)
        {
            try
            {
                strDate = Convert.ToDateTime(strDate).ToString("MM/dd/yyyy");
            }
            catch { }
            return strDate;
        }

        /// <summary>
        /// Get date in mm/dd/yyyy HH:MM:SS format
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public string GetDateAndTime(string strDate)
        {
            try
            {
                strDate = Convert.ToDateTime(strDate).ToString("MM/dd/yyyy");
            }
            catch { }
            return strDate;
        }

        public string GetServerPath()
        {
            return Server.MapPath("~");
        }

        public static string StripHtml(string html, bool allowHarmlessTags)
        {
            if (html == null || html == string.Empty)
                return string.Empty;

            if (allowHarmlessTags)
                return System.Text.RegularExpressions.Regex.Replace(html, "", string.Empty);

            return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", string.Empty);
        }

        public void AlertScript(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jscript", string.Format("alert('{0}');", message), true);
        }

        public void WarningScript(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jscript", string.Format("UploadWarning('{0}');", message), true);
        }

        public DataView sortGrid(DataTable dt)
        {
            DataView dv;
            dv = dt.DefaultView;
            if (ViewState["SortExpression"] != null)
            {
                dv.Sort = string.Format("{0} {1}", ViewState["SortExpression"].ToString(), sortorder);
            }
            return dv;
        }
        public string sortorder
        {
            get
            {
                if (Convert.ToString(ViewState["sortorder"]) == "desc")
                {
                    ViewState["sortorder"] = "asc";
                }
                else
                {
                    ViewState["sortorder"] = "desc";
                }
                return ViewState["sortorder"].ToString();
            }
            set
            {
                ViewState["sortorder"] = value;
            }
        }

        public ReportDocument GetReportDocument(string pReportFile)
        {
            if (pReportFile == null)
                return null;

            ReportDocument doc = new ReportDocument();
            doc.Load(pReportFile);
            return doc;
        }

        public string GetParamValueByConnStringParamName(string ConnString, string ParamKey)
        {
            string[] DBPrams = ConnString.Split(';');
            Dictionary<string, string> dctDBParamNameValue = new Dictionary<string, string>();

            foreach (string item in DBPrams)
            {
                if (!(dctDBParamNameValue.ContainsKey(item.Split('=')[0])))
                {
                    dctDBParamNameValue.Add(item.Split('=')[0], item.Split('=')[1]);
                }
            }
            return dctDBParamNameValue[ParamKey];
        }
    }
}