using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlterationRecords
{
    public partial class TabletClientInstallation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["NavFrom"] != null && Session["NavFrom"].ToString() != "")
                {
                    if(Session["NavFrom"].ToString() == "releaseNotes")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "jscript", string.Format("activetabInstallation();"), true);
                        Session.Remove("NavFrom");
                    }
                }
            }
        }
    }
}