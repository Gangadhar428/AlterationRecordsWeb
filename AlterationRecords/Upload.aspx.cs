using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using AlterationRecordsModel;
using AlterationRecordsDataAccess;
using AlterationRecords.Common;
using System.Drawing;
using System.Text;

namespace AlterationRecords
{
    public partial class Upload : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("NavFrom");
                DuplicateFileName();
                BindSchools();
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
                ddlSchool.Items.Insert(0, new ListItem("Select School", "-1"));
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }

        private void DuplicateFileName()
        {
            hdnFileName.Value = "";
            DataTable file = SBDataAccess.GetFileDetails(DBConnectionString);
            foreach (DataRow dr in file.Rows)
            {
                hdnFileName.Value += dr["FileName"].ToString() + ",";
            }
        }

        protected void fuSchools_Load(object sender, EventArgs e)
        {
        }

        protected void lbtnSample_Click(object sender, EventArgs e)
        {
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Identity lUser = (Identity)Session["LoggedUser"];
                if (lUser.Role.ToLower().IndexOf("staff") <= -1)
                {
                    Response.Redirect("RolesList.aspx");
                }
            }
            catch (Exception)
            {

            }
        }

        protected void lbtnUploadAfterWarning_Click(object sender, EventArgs e)
        {
            UploadFile(ViewState["strConn"].ToString(), ViewState["fn"].ToString(), ViewState["tempFileLoc"].ToString());


        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuSchools.PostedFile != null && fuSchools.PostedFile.ContentLength > 0)
                {
                    string fn = Path.GetFileName(fuSchools.PostedFile.FileName);
                    //OleDbConnection con = null;
                    string[] day = DateTime.Now.Date.ToString("MMddyyyy").Replace("/", "").Split(' ');
                    string SaveLocation = Server.MapPath("RecruitFiles/" + day[0]);

                    if (!Directory.Exists(SaveLocation))
                    {
                        Directory.CreateDirectory(SaveLocation);
                    }
                    try
                    {
                        string FileLocation = SaveLocation + "\\" + fn;
                        fuSchools.PostedFile.SaveAs(FileLocation);

                        string[] fileLoc = fn.Split('.');
                        string tempFileLoc = SaveLocation + "\\" + fileLoc[fileLoc.Length - 2] + DateTime.Now.Ticks.ToString() + "." + fileLoc[fileLoc.Length - 1];
                        fuSchools.PostedFile.SaveAs(tempFileLoc);

                        string fileExtension = Path.GetExtension(fuSchools.PostedFile.FileName);

                        string strConn = "";

                        if (fileExtension == ".xls")
                        {
                            //strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + tempFileLoc + ";Extended Properties=\"Excel 8.0;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"";
                            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"" + tempFileLoc + "\";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";
                        }
                        else if (fileExtension == ".xlsx")
                        {
                            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + tempFileLoc + ";Extended Properties=\"Excel 8.0;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"";
                        }
                        else
                        {
                            //strConn = "Provider=Microsoft.Jet.OLEDB.12.0;Data Source=\"" + tempFileLoc + "\";Extended Properties=\"Excel 12.0;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"";
                            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"" + tempFileLoc + "\";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";
                        }
                        ViewState["strConn"] = strConn;
                        ViewState["fn"] = fn;
                        ViewState["tempFileLoc"] = tempFileLoc;
                        UploadFile(strConn, fn, tempFileLoc);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        try
                        {
                            //con.Close();
                            //con = null;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
                else
                {
                    AlertScript("Please select file to upload");
                }
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }


        private void UploadFile(string strConn, string fn, string tempFileLoc)
        {
            try
            {
                string[] day = DateTime.Now.Date.ToString("MMddyyyy").Replace("/", "").Split(' ');
                OleDbConnection con = new OleDbConnection(strConn);
                con.Open();

                // Get All Sheets Name
                DataTable dtSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                // Retrive the Data by Sheetwise
                DataSet dsOutput = new DataSet();

                string sSheetName = dtSheetName.Rows[0]["TABLE_NAME"].ToString();
                string sQuery = "Select * From [" + sSheetName + "]";

                OleDbCommand dbCmd = new OleDbCommand(sQuery, con);
                OleDbDataAdapter dbDa = new OleDbDataAdapter(dbCmd);
                DataTable dtData = new DataTable();

                dbDa.Fill(dtData);
                dsOutput.Tables.Add(dtData);

                SBModel.SFileRow Sfilerow = (new SBModel()).SFile.NewSFileRow();
                Sfilerow.FileName = fn;
                Sfilerow.Description = txtDescription.Text.ToString();
                Sfilerow.FileLocation = "RecruitFiles/" + day[0] + "/";
                Sfilerow.SchoolID = Convert.ToInt32(ddlSchool.SelectedValue);
                Sfilerow.UploadedByUserID = Convert.ToInt64(1);
                Sfilerow.FileStatusID = 2;
                Sfilerow.RecordCount = dsOutput.Tables[0].Rows.Count;

                List<string> lstmaleSchools = new List<string> { "1", "2", "4", "5", "6" };
                List<string> lstfemaleSchools = new List<string> { "3", "7", "8", "9", "10" };
                foreach (DataRow dr in dtData.Rows)
                {

                    try
                    {
                        bool isFieldInvalid = false;
                        if (dr["SEX"] != null)
                        {
                            string sex = dr["SEX"].ToString().ToLower();
                            if (sex == "m")
                            {
                                if (!lstmaleSchools.Contains(ddlSchool.SelectedValue))
                                {
                                    AlertScript("Sex column/value is not matching with the selected school, please update and try uploading the file again.");
                                    return;
                                }
                            }
                            else if (sex == "f")
                            {
                                if (!lstfemaleSchools.Contains(ddlSchool.SelectedValue))
                                {
                                    AlertScript("Sex column/value is not matching with the selected school, please update and try uploading the file again.");
                                    return;
                                }
                            }
                            else
                                isFieldInvalid = true;
                        }
                        else
                        {
                            isFieldInvalid = true;
                        }

                        if (isFieldInvalid)
                        {
                            AlertScript("Sex column/value is missing/incorrect in the file, please update and try uploading the file again.");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        AlertScript("Gender Match Error." + ex.Message);
                        return;
                    }

                    string[] str = dr["SSN"].ToString().Split('-');
                    string ssn = dr["SSN"].ToString();
                    if (ssn.Length < 4 || str[str.Length - 1].Length != 4)
                    {
                        AlertScript("The file contains Invalid SSN Data. Please correct and Retry!");
                        return;
                    }
                    else if (ssn.Length > 4 && ssn.Length < 11)
                    {
                        AlertScript("The file contains Invalid SSN Data. Please correct and Retry!");
                        return;
                    }
                    ssn = ssn.Substring(ssn.Trim().Length - 4, 4);
                    for (int i = 0; i < ssn.Length; i++)
                    {
                        if (!char.IsNumber(ssn[i]))
                        {
                            AlertScript("The file contains Invalid SSN Data. Please correct and Retry!");
                            return;
                        }
                    }

                    string component = dr["component"].ToString().ToLower();
                    if (component == "a" || component == "r" || component == "active" || component == "reserve" || component.Length == 0)
                    {

                    }
                    else
                    {
                        AlertScript("The file contains Invalid Component Data. Please correct and Retry!");
                        return;
                    }
                }
                int noofROwsInserted = SBDataAccess.InsertFile(DBConnectionString, Sfilerow, dtData, (hdnWarning.Value == ""));

                if (noofROwsInserted == -2)
                {
                    WarningScript("You are about to override a previous upload. Do you wish to continue?");
                    return;
                }

                if (noofROwsInserted > 0)
                {
                    AlertScript("File Successfully Saved");
                    DuplicateFileName();
                    BindSchools();
                    ClearFields();
                }
                con.Close();
                try
                {
                    // FileStream stream = new FileStream();
                    File.Delete(tempFileLoc);
                    //stream.Close();
                }
                catch { }  // ignore
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }

        private void ClearFields()
        {
            txtDescription.Text = "";
            ddlSchool.SelectedValue = "-1";
        }
    }
}