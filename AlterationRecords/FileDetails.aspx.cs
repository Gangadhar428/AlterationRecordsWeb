using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlterationRecordsDataAccess;
using AlterationRecords.Common;
using AlterationRecordsModel;

namespace AlterationRecords
{
    public partial class FileDetails : PageBase
    {
        int FileID;
        protected void Page_Load(object sender, EventArgs e)
        {
            FileID = Convert.ToInt32(Request["ID"]);
            if (!IsPostBack)
            {
                BindFileDetails();
            }
        }

        private void BindFileDetails()
        {
            try
            {
                SBModel.SFileDataTable sFile = SBDataAccess.GetSFileByFileID(DBConnectionString, FileID);
                SBModel.SFileRow SFileRow = (SBModel.SFileRow)sFile.Rows[0];

                if (SFileRow.FileStatusID > 1)
                {
                    ddlStatus.Visible = true;
                    lblStatusValue.Visible = false;
                    lbtnSave.Visible = true;
                }
                lblFileNameValue.Text = SFileRow.IsFileNameNull() ? "" : SFileRow.FileName;
                lblDescriptionValue.Text = SFileRow.IsDescriptionNull() ? "" : SFileRow.Description;
                lblSchoolValue.Text = SFileRow.IsSchoolNameNull() ? "" : SFileRow.SchoolName;
                lblDateValue.Text = SFileRow.IsDateUploadedNull() ? "" : SFileRow.DateUploaded.ToString();
                ddlStatus.SelectedValue = SFileRow.FileStatusID.ToString();
                lblStatusValue.Text = SFileRow.IsFileStatusNull() ? "" : SFileRow.FileStatus;
                lblCountValue.Text = SFileRow.IsRecordCountNull() ? "" : SFileRow.RecordCount.ToString();
                lblUploadedByValue.Text = SFileRow.IsUploadedByUserNull() ? "" : SFileRow.UploadedByUser;
                lblSyncToTabletDateValue.Text = SFileRow.IsSyncToTabletDateNull() ? "N/A" : SFileRow.SyncToTabletDate.ToString();
                lblSyncToTabletUserValue.Text = SFileRow.IsSyncToTabletUserNull() ? "N/A" : SFileRow.SyncToTabletUser;
                lblSyncFromTabletDateValue.Text = SFileRow.IsSyncFromTabletDateNull() ? "N/A" : SFileRow.SyncFromTabletDate.ToString();
                lblSyncFromTabletUserValue.Text = SFileRow.IsSyncFromTabletUserNull() ? "N/A" : SFileRow.SyncFromTabletUser;
                lblSyncFileLocationValue.Text = SFileRow.IsSynchFileLocationNull() ? "N/A" : SFileRow.SynchFileLocation;
                lblSyncToTabletDateValue.Visible = lblSyncToTabletDate.Visible = lblSyncToTabletUserValue.Visible = lblSyncToTabletUser.Visible = lblSyncFromTabletDateValue.Visible = lblSyncFromTabletDate.Visible = lblSyncFromTabletUserValue.Visible = lblSyncFromTabletUSer.Visible = lblSyncFileLocationValue.Visible = lblSyncFileLocation.Visible = false;
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Identity identity = (Identity)Session["LoggedUser"];
                int FileStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                int noofRowsAffected = SBDataAccess.UpdateFileStatus(DBConnectionString, FileID, FileStatus, identity.UserID, "");
                if (noofRowsAffected > 0)
                    AlertScript("File Status Updated Successfully");
                //Response.Redirect("ListOfFiles.aspx");
            }
            catch (Exception ex)
            {
                AlertScript(ex.Message.Replace("'", "").Replace("\r\n", ""));
            }
        }
    }
}