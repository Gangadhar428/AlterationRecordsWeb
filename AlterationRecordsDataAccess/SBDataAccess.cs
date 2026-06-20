using AlterationRecordsModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlterationRecordsDataAccess
{
    public class SBDataAccess
    {
        public static SBModel.SUserDataTable GetUsersDetails(string strConn, string userName)
        {
            SBModel psm = new SBModel();
            SqlConnection con = new SqlConnection(strConn);
            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@UserName", userName)};
                psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "SUser", con, CommandType.StoredProcedure, "SEL_SUserByUserName", paramsArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }

            return psm.SUser;
        }

        public static SBModel.SRoleDataTable GetRoles(string strConn)
        {
            SBModel psm = new SBModel();
            SqlConnection con = new SqlConnection(strConn);
            try
            {
                psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "SRole", con, CommandType.StoredProcedure, "SEL_SRoleList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return psm.SRole;
        }

        public static SBModel.SUserDataTable GetUsers(string strConn)
        {
            SBModel psm = new SBModel();
            SqlConnection con = new SqlConnection(strConn);
            try
            {
                psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "SUser", con, CommandType.StoredProcedure, "SEL_SUserList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return psm.SUser;
        }

        public static SBModel.SSchoolDataTable GetSchools(string strConn)
        {
            SBModel psm = new SBModel();
            SqlConnection con = new SqlConnection(strConn);
            try
            {
                psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "SSchool", con, CommandType.StoredProcedure, "SEL_SSchoolList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return psm.SSchool;
        }

        public static int InsertFile(string strConn, SBModel.SFileRow row, DataTable dt, bool showWarning)
        {
            int iNoOfRowEffected = 0;

            SqlConnection con = new SqlConnection(strConn);
            con.Open();
            DataSet dsRecruit = new DataSet();

            using (SqlTransaction trans = con.BeginTransaction())
            {
                try
                {
                    SqlParameter[] paramsArray = new SqlParameter[]{
                                                    new SqlParameter("@FileName", row.FileName),
                                                    new SqlParameter("@Description", row.Description),
                                                    new SqlParameter("@FileLocation", row.FileLocation),
                                                    new SqlParameter("@SchoolID", row.SchoolID),
                                                    new SqlParameter("@UploadedByUserID", row.UploadedByUserID),
                                                    new SqlParameter("@FileStatusID", row.FileStatusID),
                                                    new SqlParameter("@RecordCount", row.RecordCount)};

                    iNoOfRowEffected = Convert.ToInt32(SqlHelper.ExecuteScalar(trans, CommandType.StoredProcedure, "INS_SFile", paramsArray));

                    try
                    {
                        int schoolID = row.SchoolID;
                        int fileID = iNoOfRowEffected;
                        string date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        foreach (DataRow dr in dt.Rows)
                        {
                            string RecruitName = dr["FIRST NAME"] + " " + dr["LAST NAME"];
                            string components = dr["COMPONENT"].ToString().ToLower();
                            int component = 0;
                            if (components == "a" || components == "active" || components.Length == 0)
                            {
                                component = 1;
                            }
                            //else if (components == "r" || components == "reserve")
                            //{
                            //    component = 0;
                            //}

                            string SSN = dr["SSN"].ToString() == "" ? "" : dr["SSN"].ToString();
                            //string platoon = dr["Platoon"].ToString() == "" ? "" : dr["Platoon"].ToString() + "(" + date + ")";
                            string platoon = dr["Platoon"].ToString() == "" ? "" : "(" + date + ")-" + dr["Platoon"].ToString();
                            string rank = dr["RANK"].ToString() == "" ? null : dr["RANK"].ToString();
                            string gender = dr["SEX"].ToString() == "" ? "M" : dr["SEX"].ToString();

                            DataSet ds = new DataSet();
                            ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, "SELECT RecruitID FROM SRecruit Where RecruitName='" + RecruitName + "' AND SSN like '" + SSN + "' AND PlatoonNumber='" + platoon + "' AND SchoolID =" + schoolID);

                            DataTable dts = new DataTable();
                            dts = ds.Tables[0];

                            if (RecruitName == " " && SSN == "" && platoon == "")
                            {
                                continue;
                            }

                            // 08/07/2023 - used sp 
                            /*if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "INSERT INTO SRecruit(SchoolID,FileID,RecruitName,SSN,PlatoonNumber,UploadDate,Note,Component,Rank,Gender) VALUES(" + schoolID + "," + fileID + ",'" + RecruitName + "','" + SSN + "','" + platoon + "','" + DateTime.Now.ToString() + "',''," + component + ",'" + rank + "','" + gender + "')"); */

                            paramsArray = new SqlParameter[]{
                                                    new SqlParameter("@SchoolID", schoolID),
                                                    new SqlParameter("@FileID", fileID),
                                                    new SqlParameter("@RecruitName", RecruitName),
                                                    new SqlParameter("@SSN", SSN),
                                                    new SqlParameter("@PlatoonNumber", platoon),
                                                    new SqlParameter("@UploadDate", DateTime.Now),
                                                    new SqlParameter("@Note", ""),
                                                    new SqlParameter("@Component", component),
                                                    new SqlParameter("@Rank", rank),
                                                    new SqlParameter("@Gender", gender)};

                            DataSet resultt = SqlHelper.ExecuteDataset(trans, CommandType.StoredProcedure, "InsRecruit", paramsArray);

                            DataRow drr = resultt.Tables[0].Rows[0];

                            if (showWarning && drr["ExistingRecruitCount"] != null && drr["ExistingRecruitCount"] != Convert.DBNull && drr["ExistingRecruitCount"].ToString() != ""
                                    && Convert.ToInt32(drr["ExistingRecruitCount"]) > 0)
                            {
                                trans.Rollback();
                                return -2;
                            }


                            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            //{
                            //}
                            //else
                            //    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "INSERT INTO SRecruit(SchoolID,FileID,RecruitName,SSN,PlatoonNumber,UploadDate,Note,Component,Rank,Gender) VALUES(" + schoolID + "," + fileID + ",'" + RecruitName + "','" + SSN + "','" + platoon + "','" + DateTime.Now.ToString() + "',''," + component + ",'" + rank + "','" + gender + "')");

                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    trans.Commit();
                }

                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                finally
                {
                    try
                    {
                        con.Close();
                        con = null;
                    }
                    catch { }
                }
            }

            return iNoOfRowEffected;
        }

        public static SBModel.SFileDataTable GetUploadList(string strConn, int Status, int SchoolID)
        {
            SBModel psm = new SBModel();
            SqlConnection con = new SqlConnection(strConn);

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@FileStatusID", Status),
                                                new SqlParameter("@SchoolID", SchoolID)};

                psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "SFile", con, CommandType.StoredProcedure, "SEL_SFiles", paramsArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return psm.SFile;
        }

        public static int InsertUser(string DBConnectionString, SBModel.SUserRow row)
        {
            int iNoOfRowEffected = 0;
            SqlConnection con = new SqlConnection(DBConnectionString);
            con.Open();
            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@UserID", row.UserID),
                                                new SqlParameter("@UserName", row.UserName),
                                                new SqlParameter("@Password", row.Password==""?Convert.DBNull:row.Password),
                                                new SqlParameter("@RoleID", row.RoleID),
                                                new SqlParameter("@Name", row.Name),
                                                new SqlParameter("@IsActive", row.IsActive),
                                                new SqlParameter("@EmailAddress", row.EmailAddress),
                                                new SqlParameter("@PhoneNumber",row.PhoneNumber)};

                iNoOfRowEffected = Convert.ToInt32(SqlHelper.ExecuteScalar(con, CommandType.StoredProcedure, "INSUPD_SUser", paramsArray));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }

            return iNoOfRowEffected;
        }

        public static int UpdateFileStatus(string DBConnectionString, int FileID, int FileStatus, long UserID, string SynchFileLocation)
        {
            int iNoOfRowEffected = 0;
            SqlConnection con = new SqlConnection(DBConnectionString);
            con.Open();
            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@FileID", FileID),
                                                new SqlParameter("@FileStatusID", FileStatus),
                                                new SqlParameter("@UserID",UserID),
                                                new SqlParameter("@SynchFileLocation",SynchFileLocation==""?Convert.DBNull:SynchFileLocation)};

                iNoOfRowEffected = Convert.ToInt32(SqlHelper.ExecuteNonQuery(con, CommandType.Text, "Update SFile Set FileStatusID =" + FileStatus + "Where FileID=" + FileID, paramsArray));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }

            return iNoOfRowEffected;
        }

        public static SBModel.SFileDataTable GetSFileByFileID(string DBConnectionString, int FileID)
        {
            SBModel psm = new SBModel();
            SqlConnection con = new SqlConnection(DBConnectionString);

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@FileID", FileID)};

                psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "SFile", con, CommandType.StoredProcedure, "SEL_SFileByID", paramsArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return psm.SFile;
        }

        public static SBModel.SUserDataTable GetSUserByUserID(string DBConnectionString, int UserID)
        {
            SBModel psm = new SBModel();
            SqlConnection con = new SqlConnection(DBConnectionString);

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@UserID", UserID)};

                psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "SUser", con, CommandType.StoredProcedure, "SEL_SUserByID", paramsArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return psm.SUser;
        }

        public static int UpdatePassword(string DBConnectionString, long userID, string Password)
        {
            int iNoOfRowEffected = 0;
            SqlConnection con = new SqlConnection(DBConnectionString);
            con.Open();
            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@UserID", userID),
                                                new SqlParameter("@Password", Password)};

                iNoOfRowEffected = Convert.ToInt32(SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "UPD_USERPWD", paramsArray));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }

            return iNoOfRowEffected;
        }

        public static DataSet GetSFileByStatus(string DBConnectionString, int FileStatus)
        {
            DataSet psm = new DataSet();
            SqlConnection con = new SqlConnection(DBConnectionString);

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@FileStatusID", FileStatus)};

                psm = (DataSet)SqlHelper.ExecuteSBDataset(psm, "SFile", con, CommandType.StoredProcedure, "SEL_SFilesByStatusID", paramsArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return psm;
        }

        // TODO : Need to review this later
        public static int UploadDataTODB(string DBConnectionString, DataSet ds, DataSet dsRecruit)
        {
            int iNoOfRowEffected = 0;
            int counter = 0;
            SqlConnection con = new SqlConnection(DBConnectionString);
            //con.Open();
            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@RecruitID",dr["RecruitID"]==null?Convert.DBNull :Convert.ToInt32(dr["RecruitID"])),
                                                new SqlParameter("@SchoolID",dr["SchoolID"]==null?Convert.DBNull :Convert.ToInt32(dr["SchoolID"])),
                                                new SqlParameter("@RecruitName",dr["RecruitName"]==null?Convert.DBNull :dr["RecruitName"].ToString()),
                                                new SqlParameter("@SSN",dr["SSN"]==null?Convert.DBNull :dr["SSN"].ToString()),
                                                new SqlParameter("@PlatoonNumber",dr["PlatoonNumber"]==null?Convert.DBNull :dr["PlatoonNumber"].ToString()),
                                                new SqlParameter("@UploadDate",dr["UploadDate"]==null?Convert.DBNull :Convert.ToDateTime(dr["UploadDate"])),
                                                new SqlParameter("@Note",dr["Note"]==null?Convert.DBNull :dr["Note"].ToString()),
                                                new SqlParameter("@FileID",dr["FileID"]==null?Convert.DBNull :Convert.ToInt32(dr["FileID"])),
                                                new SqlParameter("@Component",dr["Component"]==null?Convert.DBNull :Convert.ToBoolean(dr["Component"])),
                                                new SqlParameter("@Rank",dr["Rank"]==null?Convert.DBNull :dr["Rank"].ToString()),
                                                new SqlParameter("@Gender",dr["Gender"]==null?Convert.DBNull :dr["Gender"].ToString()),
                                                new SqlParameter("@BC_btnAdjustFrontPanel",(dr["BC_btnAdjustFrontPanel"]== null)?Convert.DBNull: dr["BC_btnAdjustFrontPanel"]),
                                                new SqlParameter("@BC_btnSleeveLength",dr["BC_btnSleeveLength"]== null?Convert.DBNull: dr["BC_btnSleeveLength"]),
                                                new SqlParameter("@BC_btnUnderarm",dr["BC_btnUnderarm"]== null?Convert.DBNull: dr["BC_btnUnderarm"]),
                                                new SqlParameter("@BC_btnWaist",dr["BC_btnWaist"]== null?Convert.DBNull: dr["BC_btnWaist"]),
                                                new SqlParameter("@BC_btnSkirt",dr["BC_btnSkirt"]== null?Convert.DBNull: dr["BC_btnSkirt"]),
                                                new SqlParameter("@BC_btnChest",dr["BC_btnChest"]== null?Convert.DBNull: dr["BC_btnChest"]),
                                                new SqlParameter("@BC_btnChest",dr["BC_btnBackWrinkles"]== null?Convert.DBNull: dr["BC_btnBackWrinkles"]),
                                                new SqlParameter("@BC_btnCoatFront",dr["BC_btnCoatFront"]== null?Convert.DBNull: dr["BC_btnCoatFront"]),
                                                new SqlParameter("@BC_btnPFCButton",dr["BC_btnPFCButton"]== null?Convert.DBNull: dr["BC_btnPFCButton"]),
                                                new SqlParameter("@BC_btnAdjustRearPanel",dr["BC_btnAdjustRearPanel"]== null?Convert.DBNull: dr["BC_btnAdjustRearPanel"]),
                                                new SqlParameter("@BC_btnReplaceCollar",dr["BC_btnReplaceCollar"]== null?Convert.DBNull: dr["BC_btnReplaceCollar"]),
                                                new SqlParameter("@BC_btnShortenCoat",dr["BC_btnShortenCoat"]== null?Convert.DBNull: dr["BC_btnShortenCoat"]),
                                                new SqlParameter("@BC_btnShoulders",dr["BC_btnShoulders"]== null?Convert.DBNull: dr["BC_btnShoulders"]),
                                                new SqlParameter("@BC_btnSholderpadding",dr["BC_btnShoulderpadding"]== null?Convert.DBNull: dr["BC_btnShoulderpadding"]),
                                                new SqlParameter("@BC_btnHooks",dr["BC_btnHooks"]== null?Convert.DBNull: dr["BC_btnHooks"]),
                                                new SqlParameter("@GC_btnAdjustFrontPanel",dr["GC_btnAdjustFrontPanel"]== null?Convert.DBNull: dr["GC_btnAdjustFrontPanel"]),
                                                new SqlParameter("@GC_btnSleeveLength",dr["GC_btnSleeveLength"]== null?Convert.DBNull: dr["GC_btnSleeveLength"]),
                                                new SqlParameter("@GC_btnUnderarm",dr["GC_btnUnderarm"]== null?Convert.DBNull: dr["GC_btnUnderarm"]),
                                                new SqlParameter("@GC_btnWaist",dr["GC_btnWaist"]== null?Convert.DBNull: dr["GC_btnWaist"]),
                                                new SqlParameter("@GC_btnSkirt",dr["GC_btnSkirt"]== null?Convert.DBNull: dr["GC_btnSkirt"]),
                                                new SqlParameter("@GC_btnChest",dr["GC_btnChest"]== null?Convert.DBNull: dr["GC_btnChest"]),
                                                new SqlParameter("@GC_btnBackWrinkles",dr["GC_btnBackWrinkles"]== null?Convert.DBNull: dr["GC_btnBackWrinkles"]),
                                                new SqlParameter("@GC_btnCoatFront",dr["GC_btnCoatFront"]== null?Convert.DBNull: dr["GC_btnCoatFront"]),
                                                new SqlParameter("@GC_btnPFCButton",dr["GC_btnPFCButton"]== null?Convert.DBNull: dr["GC_btnPFCButton"]),
                                                new SqlParameter("@GC_btnAdjustRearPanel",dr["GC_btnAdjustRearPanel"]== null?Convert.DBNull: dr["GC_btnAdjustRearPanel"]),
                                                new SqlParameter("@GC_btnShortenCoat",dr["GC_btnShortenCoat"]== null?Convert.DBNull: dr["GC_btnShortenCoat"]),
                                                new SqlParameter("@GC_btnShoulders",dr["GC_btnShoulders"]== null?Convert.DBNull: dr["GC_btnShoulders"]),
                                                new SqlParameter("@GC_btnShoulderPadding",dr["GC_btnShoulderPadding"]== null?Convert.DBNull: dr["GC_btnShoulderPadding"]),
                                                new SqlParameter("@BT_btnWaist",dr["BT_btnWaist"]== null?Convert.DBNull: dr["BT_btnWaist"]),
                                                new SqlParameter("@BT_btnSeat",dr["BT_btnSeat"]== null?Convert.DBNull: dr["BT_btnSeat"]),
                                                new SqlParameter("@BT_btnHemTrousers",dr["BT_btnHemTrousers"]== null?Convert.DBNull: dr["BT_btnHemTrousers"]),
                                                new SqlParameter("@BT_btnStride",dr["BT_btnStride"]== null?Convert.DBNull: dr["BT_btnStride"]),
                                                new SqlParameter("@BT_btnFaceTrousers",dr["BT_btnFaceTrousers"]== null?Convert.DBNull: dr["BT_btnFaceTrousers"]),
                                                new SqlParameter("@BT_btnWaistband",dr["BT_btnWaistband"]== null?Convert.DBNull: dr["BT_btnWaistband"]),
                                                new SqlParameter("@GT_btnWaist",dr["GT_btnWaist"]== null?Convert.DBNull: dr["GT_btnWaist"]),
                                                new SqlParameter("@GT_btnSeat",dr["GT_btnSeat"]== null?Convert.DBNull: dr["GT_btnSeat"]),
                                                new SqlParameter("@GT_btnHemTrousers",dr["GT_btnHemTrousers"]== null?Convert.DBNull: dr["GT_btnHemTrousers"]),
                                                new SqlParameter("@GT_btnStride",dr["GT_btnStride"]== null?Convert.DBNull: dr["GT_btnStride"]),
                                                new SqlParameter("@GT_btnFaceTrousers",dr["GT_btnFaceTrousers"]== null?Convert.DBNull: dr["GT_btnFaceTrousers"]),
                                                new SqlParameter("@GT_btnWaistband",dr["GT_btnWaistband"]== null?Convert.DBNull: dr["GT_btnWaistband"]),
                                                new SqlParameter("@CAM_btnDesertBlouse",dr["CAM_btnDesertBlouse"]== null?Convert.DBNull: dr["CAM_btnDesertBlouse"]),
                                                new SqlParameter("@CAM_btnDesertTrouser",dr["CAM_btnDesertTrouser"]== null?Convert.DBNull: dr["CAM_btnDesertTrouser"]),
                                                new SqlParameter("@CAM_btnWoodlandBlouse",dr["CAM_btnWoodlandBlouse"]== null?Convert.DBNull: dr["CAM_btnWoodlandBlouse"]),
                                                new SqlParameter("@CAM_btnWoodlandTrouser",dr["CAM_btnWoodlandTrouser"]== null?Convert.DBNull: dr["CAM_btnWoodlandTrouser"]),
                                                new SqlParameter("@MORE_btnButtons",dr["MORE_btnButtons"]== null?Convert.DBNull: dr["MORE_btnButtons"]),
                                                new SqlParameter("@MORE_btnSewSeam",dr["MORE_btnSewSeam"]== null?Convert.DBNull: dr["MORE_btnSewSeam"]),
                                                new SqlParameter("@MORE_btnWaistFasteners",dr["MORE_btnWaistFasteners"]== null?Convert.DBNull: dr["MORE_btnWaistFasteners"]),
                                                new SqlParameter("@MORE_btnBeltLoops",dr["MORE_btnBeltLoops"]== null?Convert.DBNull: dr["MORE_btnBeltLoops"]),
                                                new SqlParameter("@MORE_btnKhakishirtsleevelength",dr["MORE_btnKhakishirtsleevelength"]== null?Convert.DBNull: dr["MORE_btnKhakishirtsleevelength"]),
                                                new SqlParameter("@MORE_btnNameTapes",dr["MORE_btnNameTapes"]== null?Convert.DBNull: dr["MORE_btnNameTapes"]),
                                                new SqlParameter("@MORE_btnSewon",dr["MORE_btnSewon"]== null?Convert.DBNull: dr["MORE_btnSewon"]),
                                                new SqlParameter("@MORE_btnNCOStripes",dr["MORE_btnNCOStripes"]== null?Convert.DBNull: dr["MORE_btnNCOStripes"]),
                                                new SqlParameter("@MORE_btnAcceleratedDelivery",dr["MORE_btnAcceleratedDelivery"]== null?Convert.DBNull: dr["MORE_btnAcceleratedDelivery"]),
                                                new SqlParameter("@MORE_btnChevronsRemove",dr["MORE_btnChevronsRemove"]== null?Convert.DBNull: dr["MORE_btnChevronsRemove"]),
                                                new SqlParameter("@MORE_btnAWCSleevelength",dr["MORE_btnAWCSleevelength"]== null?Convert.DBNull: dr["MORE_btnAWCSleevelength"]),
                                                new SqlParameter("@MORE_btnAWCShortenCoat",dr["MORE_btnAWCShortenCoat"]== null?Convert.DBNull: dr["MORE_btnAWCShortenCoat"]),
                                                new SqlParameter("@MORE_btnPocketFlaps",dr["MORE_btnPocketFlaps"]== null?Convert.DBNull: dr["MORE_btnPocketFlaps"]),
                                                new SqlParameter("@MORE_btnPocketsReset",dr["MORE_btnPocketsReset"]== null?Convert.DBNull: dr["MORE_btnPocketsReset"]),
                                                new SqlParameter("@MORE_btnEmblemsHoles",dr["MORE_btnEmblemsHoles"]== null?Convert.DBNull: dr["MORE_btnEmblemsHoles"]),
                                                new SqlParameter("@MORE_btnEpaulets",dr["MORE_btnEpaulets"]== null?Convert.DBNull: dr["MORE_btnEpaulets"]),
                                                new SqlParameter("@MORE_btnButtonHoles",dr["MORE_btnButtonHoles"]== null?Convert.DBNull: dr["MORE_btnButtonHoles"]),
                                                new SqlParameter("@MORE_btnZipper",dr["MORE_btnZipper"]== null?Convert.DBNull: dr["MORE_btnZipper"]),
                                                new SqlParameter("@MORE_btnEmergencyTailoring",dr["MORE_btnEmergencyTailoring"]== null?Convert.DBNull: dr["MORE_btnEmergencyTailoring"]),
                                                new SqlParameter("@MORE_btnWalkinTailoring",dr["MORE_btnWalkinTailoring"]== null?Convert.DBNull: dr["MORE_btnWalkinTailoring"]),
                                                new SqlParameter("@WC_btnDarts",dr["WC_btnDarts"]== null?Convert.DBNull: dr["WC_btnDarts"]),
                                                new SqlParameter("@WC_btnHips",dr["WC_btnHips"]== null?Convert.DBNull: dr["WC_btnHips"]),
                                                new SqlParameter("@WC_btnSideArms",dr["WC_btnSideArms"]== null?Convert.DBNull: dr["WC_btnSideArms"]),
                                                new SqlParameter("@WC_btnCoatFrontRemodel",dr["WC_btnCoatFrontRemodel"]== null?Convert.DBNull: dr["WC_btnCoatFrontRemodel"]),
                                                new SqlParameter("@WS_btnAllSeams",dr["WS_btnAllSeams"]== null?Convert.DBNull: dr["WS_btnAllSeams"]),
                                                new SqlParameter("@WSK_btnHem",dr["WSK_btnHem"]== null?Convert.DBNull: dr["WSK_btnHem"]),
                                                new SqlParameter("@WSK_btnWaistBand",dr["WSK_btnWaistBand"]== null?Convert.DBNull: dr["WSK_btnWaistBand"]),
                                                new SqlParameter("@WSK_btnHips",dr["WSK_btnHips"]== null?Convert.DBNull: dr["WSK_btnHips"]),
                                                new SqlParameter("@WSL_btnWaist",dr["WSL_btnWaist"]== null?Convert.DBNull: dr["WSL_btnWaist"]),
                                                new SqlParameter("@WSL_btnHemSlacks",dr["WSL_btnHemSlacks"]== null?Convert.DBNull: dr["WSL_btnHemSlacks"]),
                                                new SqlParameter("@WSL_btnFaceSlacks",dr["WSL_btnFaceSlacks"]== null?Convert.DBNull: dr["WSL_btnFaceSlacks"]),
                                                new SqlParameter("@WSL_btnStride",dr["WSL_btnStride"]== null?Convert.DBNull: dr["WSL_btnStride"]),
                                                new SqlParameter("@WSL_btnHips",dr["WSL_btnHips"]== null?Convert.DBNull: dr["WSL_btnHips"]),
                                                new SqlParameter("@WC_btnAdjustFrontPanel",dr["WC_btnAdjustFrontPanel"]== null?Convert.DBNull: dr["WC_btnAdjustFrontPanel"]),
                                                new SqlParameter("@WC_btnBackWrinkles",dr["WC_btnBackWrinkles"]== null?Convert.DBNull: dr["WC_btnBackWrinkles"]),
                                                new SqlParameter("@WC_btnAdjustRearPanel",dr["WC_btnAdjustRearPanel"]== null?Convert.DBNull: dr["WC_btnAdjustRearPanel"]),
                                                new SqlParameter("@WC_btnShortenCoat",dr["WC_btnShortenCoat"]== null?Convert.DBNull: dr["WC_btnShortenCoat"]),
                                                new SqlParameter("@WC_btnSholderpadding",dr["WC_btnShoulderPadding"]== null?Convert.DBNull: dr["WC_btnShoulderPadding"]),
                                                new SqlParameter("@WS_btnSleeveLength",dr["WS_btnSleeveLength"]== null?Convert.DBNull: dr["WS_btnSleeveLength"]),
                                                new SqlParameter("@WC_btnSleeveLenght",dr["WC_btnSleeveLength"]== null?Convert.DBNull: dr["WC_btnSleeveLength"])};

                    try
                    {
                        iNoOfRowEffected = Convert.ToInt32(SqlHelper.ExecuteScalar(con, CommandType.StoredProcedure, "Ins_SRecruit", paramsArray));
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToLower().IndexOf("violation of primary key") > -1)
                        {
                            foreach (DataRow dr1 in dsRecruit.Tables[0].Rows)
                            {
                                if (dr1["RecruitID"].ToString() == dr["RecruitID"].ToString())
                                {
                                    SqlParameter[] paramsArrays = new SqlParameter[]{
                                                new SqlParameter("@RecruitID",dr["RecruitID"]==null?Convert.DBNull :Convert.ToInt32(dr["RecruitID"])),
                                                new SqlParameter("@SchoolID",dr["SchoolID"]==null?Convert.DBNull :Convert.ToInt32(dr["SchoolID"])),
                                                new SqlParameter("@RecruitName",dr["RecruitName"]==null?Convert.DBNull :dr["RecruitName"].ToString()),
                                                new SqlParameter("@SSN",dr["SSN"]==null?Convert.DBNull :dr["SSN"].ToString()),
                                                new SqlParameter("@PlatoonNumber",dr["PlatoonNumber"]==null?Convert.DBNull :dr["PlatoonNumber"].ToString()),
                                                new SqlParameter("@UploadDate",dr["UploadDate"]==null?Convert.DBNull :Convert.ToDateTime(dr["UploadDate"])),
                                                new SqlParameter("@Note",dr["Note"]==null?Convert.DBNull :dr["Note"].ToString()),
                                                new SqlParameter("@FileID",dr["FileID"]==null?Convert.DBNull :Convert.ToInt32(dr["FileID"])),
                                                new SqlParameter("@Component",dr["Component"]==null?Convert.DBNull :Convert.ToBoolean(dr["Component"])),
                                                new SqlParameter("@Rank",dr["Rank"]==null?Convert.DBNull :dr["Rank"].ToString()),
                                                new SqlParameter("@Gender",dr["Gender"]==null?Convert.DBNull :dr["Gender"].ToString()),
                                                new SqlParameter("@BC_btnAdjustFrontPanel",(dr["BC_btnAdjustFrontPanel"]== null)?Convert.DBNull: dr["BC_btnAdjustFrontPanel"]),
                                                new SqlParameter("@BC_btnSleeveLength",dr["BC_btnSleeveLength"]== null?Convert.DBNull: dr["BC_btnSleeveLength"]),
                                                new SqlParameter("@BC_btnUnderarm",dr["BC_btnUnderarm"]== null?Convert.DBNull: dr["BC_btnUnderarm"]),
                                                new SqlParameter("@BC_btnWaist",dr["BC_btnWaist"]== null?Convert.DBNull: dr["BC_btnWaist"]),
                                                new SqlParameter("@BC_btnSkirt",dr["BC_btnSkirt"]== null?Convert.DBNull: dr["BC_btnSkirt"]),
                                                new SqlParameter("@BC_btnChest",dr["BC_btnChest"]== null?Convert.DBNull: dr["BC_btnChest"]),
                                                new SqlParameter("@BC_btnChest",dr["BC_btnBackWrinkles"]== null?Convert.DBNull: dr["BC_btnBackWrinkles"]),
                                                new SqlParameter("@BC_btnCoatFront",dr["BC_btnCoatFront"]== null?Convert.DBNull: dr["BC_btnCoatFront"]),
                                                new SqlParameter("@BC_btnPFCButton",dr["BC_btnPFCButton"]== null?Convert.DBNull: dr["BC_btnPFCButton"]),
                                                new SqlParameter("@BC_btnAdjustRearPanel",dr["BC_btnAdjustRearPanel"]== null?Convert.DBNull: dr["BC_btnAdjustRearPanel"]),
                                                new SqlParameter("@BC_btnReplaceCollar",dr["BC_btnReplaceCollar"]== null?Convert.DBNull: dr["BC_btnReplaceCollar"]),
                                                new SqlParameter("@BC_btnShortenCoat",dr["BC_btnShortenCoat"]== null?Convert.DBNull: dr["BC_btnShortenCoat"]),
                                                new SqlParameter("@BC_btnShoulders",dr["BC_btnShoulders"]== null?Convert.DBNull: dr["BC_btnShoulders"]),
                                                new SqlParameter("@BC_btnSholderpadding",dr["BC_btnShoulderpadding"]== null?Convert.DBNull: dr["BC_btnShoulderpadding"]),
                                                new SqlParameter("@BC_btnHooks",dr["BC_btnHooks"]== null?Convert.DBNull: dr["BC_btnHooks"]),
                                                new SqlParameter("@GC_btnAdjustFrontPanel",dr["GC_btnAdjustFrontPanel"]== null?Convert.DBNull: dr["GC_btnAdjustFrontPanel"]),
                                                new SqlParameter("@GC_btnSleeveLength",dr["GC_btnSleeveLength"]== null?Convert.DBNull: dr["GC_btnSleeveLength"]),
                                                new SqlParameter("@GC_btnUnderarm",dr["GC_btnUnderarm"]== null?Convert.DBNull: dr["GC_btnUnderarm"]),
                                                new SqlParameter("@GC_btnWaist",dr["GC_btnWaist"]== null?Convert.DBNull: dr["GC_btnWaist"]),
                                                new SqlParameter("@GC_btnSkirt",dr["GC_btnSkirt"]== null?Convert.DBNull: dr["GC_btnSkirt"]),
                                                new SqlParameter("@GC_btnChest",dr["GC_btnChest"]== null?Convert.DBNull: dr["GC_btnChest"]),
                                                new SqlParameter("@GC_btnBackWrinkles",dr["GC_btnBackWrinkles"]== null?Convert.DBNull: dr["GC_btnBackWrinkles"]),
                                                new SqlParameter("@GC_btnCoatFront",dr["GC_btnCoatFront"]== null?Convert.DBNull: dr["GC_btnCoatFront"]),
                                                new SqlParameter("@GC_btnPFCButton",dr["GC_btnPFCButton"]== null?Convert.DBNull: dr["GC_btnPFCButton"]),
                                                new SqlParameter("@GC_btnAdjustRearPanel",dr["GC_btnAdjustRearPanel"]== null?Convert.DBNull: dr["GC_btnAdjustRearPanel"]),
                                                new SqlParameter("@GC_btnShortenCoat",dr["GC_btnShortenCoat"]== null?Convert.DBNull: dr["GC_btnShortenCoat"]),
                                                new SqlParameter("@GC_btnShoulders",dr["GC_btnShoulders"]== null?Convert.DBNull: dr["GC_btnShoulders"]),
                                                new SqlParameter("@GC_btnShoulderPadding",dr["GC_btnShoulderPadding"]== null?Convert.DBNull: dr["GC_btnShoulderPadding"]),
                                                new SqlParameter("@BT_btnWaist",dr["BT_btnWaist"]== null?Convert.DBNull: dr["BT_btnWaist"]),
                                                new SqlParameter("@BT_btnSeat",dr["BT_btnSeat"]== null?Convert.DBNull: dr["BT_btnSeat"]),
                                                new SqlParameter("@BT_btnHemTrousers",dr["BT_btnHemTrousers"]== null?Convert.DBNull: dr["BT_btnHemTrousers"]),
                                                new SqlParameter("@BT_btnStride",dr["BT_btnStride"]== null?Convert.DBNull: dr["BT_btnStride"]),
                                                new SqlParameter("@BT_btnFaceTrousers",dr["BT_btnFaceTrousers"]== null?Convert.DBNull: dr["BT_btnFaceTrousers"]),
                                                new SqlParameter("@BT_btnWaistband",dr["BT_btnWaistband"]== null?Convert.DBNull: dr["BT_btnWaistband"]),
                                                new SqlParameter("@GT_btnWaist",dr["GT_btnWaist"]== null?Convert.DBNull: dr["GT_btnWaist"]),
                                                new SqlParameter("@GT_btnSeat",dr["GT_btnSeat"]== null?Convert.DBNull: dr["GT_btnSeat"]),
                                                new SqlParameter("@GT_btnHemTrousers",dr["GT_btnHemTrousers"]== null?Convert.DBNull: dr["GT_btnHemTrousers"]),
                                                new SqlParameter("@GT_btnStride",dr["GT_btnStride"]== null?Convert.DBNull: dr["GT_btnStride"]),
                                                new SqlParameter("@GT_btnFaceTrousers",dr["GT_btnFaceTrousers"]== null?Convert.DBNull: dr["GT_btnFaceTrousers"]),
                                                new SqlParameter("@GT_btnWaistband",dr["GT_btnWaistband"]== null?Convert.DBNull: dr["GT_btnWaistband"]),
                                                new SqlParameter("@CAM_btnDesertBlouse",dr["CAM_btnDesertBlouse"]== null?Convert.DBNull: dr["CAM_btnDesertBlouse"]),
                                                new SqlParameter("@CAM_btnDesertTrouser",dr["CAM_btnDesertTrouser"]== null?Convert.DBNull: dr["CAM_btnDesertTrouser"]),
                                                new SqlParameter("@CAM_btnWoodlandBlouse",dr["CAM_btnWoodlandBlouse"]== null?Convert.DBNull: dr["CAM_btnWoodlandBlouse"]),
                                                new SqlParameter("@CAM_btnWoodlandTrouser",dr["CAM_btnWoodlandTrouser"]== null?Convert.DBNull: dr["CAM_btnWoodlandTrouser"]),
                                                new SqlParameter("@MORE_btnButtons",dr["MORE_btnButtons"]== null?Convert.DBNull: dr["MORE_btnButtons"]),
                                                new SqlParameter("@MORE_btnSewSeam",dr["MORE_btnSewSeam"]== null?Convert.DBNull: dr["MORE_btnSewSeam"]),
                                                new SqlParameter("@MORE_btnWaistFasteners",dr["MORE_btnWaistFasteners"]== null?Convert.DBNull: dr["MORE_btnWaistFasteners"]),
                                                new SqlParameter("@MORE_btnBeltLoops",dr["MORE_btnBeltLoops"]== null?Convert.DBNull: dr["MORE_btnBeltLoops"]),
                                                new SqlParameter("@MORE_btnKhakishirtsleevelength",dr["MORE_btnKhakishirtsleevelength"]== null?Convert.DBNull: dr["MORE_btnKhakishirtsleevelength"]),
                                                new SqlParameter("@MORE_btnNameTapes",dr["MORE_btnNameTapes"]== null?Convert.DBNull: dr["MORE_btnNameTapes"]),
                                                new SqlParameter("@MORE_btnSewon",dr["MORE_btnSewon"]== null?Convert.DBNull: dr["MORE_btnSewon"]),
                                                new SqlParameter("@MORE_btnNCOStripes",dr["MORE_btnNCOStripes"]== null?Convert.DBNull: dr["MORE_btnNCOStripes"]),
                                                new SqlParameter("@MORE_btnAcceleratedDelivery",dr["MORE_btnAcceleratedDelivery"]== null?Convert.DBNull: dr["MORE_btnAcceleratedDelivery"]),
                                                new SqlParameter("@MORE_btnChevronsRemove",dr["MORE_btnChevronsRemove"]== null?Convert.DBNull: dr["MORE_btnChevronsRemove"]),
                                                new SqlParameter("@MORE_btnAWCSleevelength",dr["MORE_btnAWCSleevelength"]== null?Convert.DBNull: dr["MORE_btnAWCSleevelength"]),
                                                new SqlParameter("@MORE_btnAWCShortenCoat",dr["MORE_btnAWCShortenCoat"]== null?Convert.DBNull: dr["MORE_btnAWCShortenCoat"]),
                                                new SqlParameter("@MORE_btnPocketFlaps",dr["MORE_btnPocketFlaps"]== null?Convert.DBNull: dr["MORE_btnPocketFlaps"]),
                                                new SqlParameter("@MORE_btnPocketsReset",dr["MORE_btnPocketsReset"]== null?Convert.DBNull: dr["MORE_btnPocketsReset"]),
                                                new SqlParameter("@MORE_btnEmblemsHoles",dr["MORE_btnEmblemsHoles"]== null?Convert.DBNull: dr["MORE_btnEmblemsHoles"]),
                                                new SqlParameter("@MORE_btnEpaulets",dr["MORE_btnEpaulets"]== null?Convert.DBNull: dr["MORE_btnEpaulets"]),
                                                new SqlParameter("@MORE_btnButtonHoles",dr["MORE_btnButtonHoles"]== null?Convert.DBNull: dr["MORE_btnButtonHoles"]),
                                                new SqlParameter("@MORE_btnZipper",dr["MORE_btnZipper"]== null?Convert.DBNull: dr["MORE_btnZipper"]),
                                                new SqlParameter("@MORE_btnEmergencyTailoring",dr["MORE_btnEmergencyTailoring"]== null?Convert.DBNull: dr["MORE_btnEmergencyTailoring"]),
                                                new SqlParameter("@MORE_btnWalkinTailoring",dr["MORE_btnWalkinTailoring"]== null?Convert.DBNull: dr["MORE_btnWalkinTailoring"]),
                                                new SqlParameter("@WC_btnDarts",dr["WC_btnDarts"]== null?Convert.DBNull: dr["WC_btnDarts"]),
                                                new SqlParameter("@WC_btnHips",dr["WC_btnHips"]== null?Convert.DBNull: dr["WC_btnHips"]),
                                                new SqlParameter("@WC_btnSideArms",dr["WC_btnSideArms"]== null?Convert.DBNull: dr["WC_btnSideArms"]),
                                                new SqlParameter("@WC_btnCoatFrontRemodel",dr["WC_btnCoatFrontRemodel"]== null?Convert.DBNull: dr["WC_btnCoatFrontRemodel"]),
                                                new SqlParameter("@WS_btnAllSeams",dr["WS_btnAllSeams"]== null?Convert.DBNull: dr["WS_btnAllSeams"]),
                                                new SqlParameter("@WSK_btnHem",dr["WSK_btnHem"]== null?Convert.DBNull: dr["WSK_btnHem"]),
                                                new SqlParameter("@WSK_btnWaistBand",dr["WSK_btnWaistBand"]== null?Convert.DBNull: dr["WSK_btnWaistBand"]),
                                                new SqlParameter("@WSK_btnHips",dr["WSK_btnHips"]== null?Convert.DBNull: dr["WSK_btnHips"]),
                                                new SqlParameter("@WSL_btnWaist",dr["WSL_btnWaist"]== null?Convert.DBNull: dr["WSL_btnWaist"]),
                                                new SqlParameter("@WSL_btnHemSlacks",dr["WSL_btnHemSlacks"]== null?Convert.DBNull: dr["WSL_btnHemSlacks"]),
                                                new SqlParameter("@WSL_btnFaceSlacks",dr["WSL_btnFaceSlacks"]== null?Convert.DBNull: dr["WSL_btnFaceSlacks"]),
                                                new SqlParameter("@WSL_btnStride",dr["WSL_btnStride"]== null?Convert.DBNull: dr["WSL_btnStride"]),
                                                new SqlParameter("@WSL_btnHips",dr["WSL_btnHips"]== null?Convert.DBNull: dr["WSL_btnHips"]),
                                                new SqlParameter("@WC_btnAdjustFrontPanel",dr["WC_btnAdjustFrontPanel"]== null?Convert.DBNull: dr["WC_btnAdjustFrontPanel"]),
                                                new SqlParameter("@WC_btnBackWrinkles",dr["WC_btnBackWrinkles"]== null?Convert.DBNull: dr["WC_btnBackWrinkles"]),
                                                new SqlParameter("@WC_btnAdjustRearPanel",dr["WC_btnAdjustRearPanel"]== null?Convert.DBNull: dr["WC_btnAdjustRearPanel"]),
                                                new SqlParameter("@WC_btnShortenCoat",dr["WC_btnShortenCoat"]== null?Convert.DBNull: dr["WC_btnShortenCoat"]),
                                                new SqlParameter("@WC_btnSholderpadding",dr["WC_btnShoulderPadding"]== null?Convert.DBNull: dr["WC_btnShoulderPadding"]),
                                                new SqlParameter("@WS_btnSleeveLength",dr["WS_btnSleeveLength"]== null?Convert.DBNull: dr["WS_btnSleeveLength"]),
                                                new SqlParameter("@WC_btnSleeveLenght",dr["WC_btnSleeveLength"]== null?Convert.DBNull: dr["WC_btnSleeveLength"])};

                                    iNoOfRowEffected = Convert.ToInt32(SqlHelper.ExecuteScalar(con, CommandType.StoredProcedure, "Upd_SRecruit", paramsArrays));
                                    break;
                                }
                            }
                            continue;
                        }
                    }
                    counter++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return iNoOfRowEffected;
        }

        public static DataSet GetRecruitID(string DBConnectionString)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(DBConnectionString);

            try
            {
                ds = SqlHelper.ExecuteSBDataset(ds, "SRecruit", con, CommandType.Text, "Select RecruitID from SRecruit");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return ds;
        }

        public static DataSet GetUsersList(string DBConnectionString, string DateCreated)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(DBConnectionString);
            try
            {
                return SqlHelper.ExecuteSBDataset(ds, "SUser", con, CommandType.Text, "Select UserID,UserName,Password,RoleID,Name,IsActive,DateCreated,IsSuperUser,EmailAddress,PhoneNumber from Suser");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
        }

        /// <summary>
        /// This is refactored will not be using
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="DateModified"></param>
        /// <returns></returns>
        public static ArrayList GetRecruitRecord(string ConnectionString, string DateModified)
        {
            DataSet dsRecruit = new DataSet();
            DataSet dsMeasurement = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);
            ArrayList aList = new ArrayList();
            try
            {
                StringBuilder strRecruitsQuery = new StringBuilder();
                strRecruitsQuery.Append("SELECT SR.RecruitID, SR.SchoolID, SR.RecruitName, SR.SSN, SR.PlatoonNumber, SR.UploadDate, SR.Note, SR.FileID,");
                strRecruitsQuery.Append("SR.Component,SR.Rank,SR.Gender,SR.LastModifiedDate,SR.Verified,SR.VerifiedByRoleID,SR.SessionApproved,");
                strRecruitsQuery.Append("SR.TailorUserID,SR.sonabendApproved,SF.FileID,SF.FileName,");
                strRecruitsQuery.Append("SF.FileLocation,SF.Description,SF.SchoolID,SF.DateUploaded,SF.FileStatusID,SF.RecordCount,SF.UploadedByUserID,");
                strRecruitsQuery.Append("SF.SyncToTabletDate,SF.SyncToTabletUserID,SF.SyncFromTabletDate,SF.SyncFromTabletUserID,SF.SynchFileLocation ");
                strRecruitsQuery.Append("FROM SRecruit SR LEFT OUTER JOIN SFile SF ON SR.FileID = SF.FileID Where SF.FileStatusID > 1 ");
                strRecruitsQuery.Append("AND LastModifiedDate IS NULL OR LastModifiedDate >'" + DateModified + "' AND UploadDate >='" + DateTime.Now.AddYears(-1) + "'");



                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("Select RecruitDressMeasureID,RecruitID,SchoolID,DressID,MeasurementAttributeID,Value,DateCreated,IsFirstFit,");
                strQuery.Append("IsAccelerated,SubsequentFit,IsSecondFit,TailorID,BoardmanID,SonabendUserID,SupervisorUserID");
                strQuery.Append(" FROM RecruitDressMeasurementMap WHERE DateCreated > '" + DateModified + "' AND DateCreated  >= '" + DateTime.Now.AddYears(-1) + "'");

                //dsRecruit = SqlHelper.ExecuteSBDataset(dsRecruit, "SRecruit", con, CommandType.Text, "Select * from SRecruit SR LEFT OUTER JOIN SFile SF ON SR.FileID = SF.FileID Where SF.FileStatusID > 1 AND LastModifiedDate IS NULL OR LastModifiedDate >'" + DateModified + "' AND UploadDate >='" + DateTime.Now.AddYears(-1) + "'");

                //dsMeasurement = SqlHelper.ExecuteSBDataset(dsMeasurement, "RecruitDressMeasurementMap", con, CommandType.Text, "SELECT * FROM RecruitDressMeasurementMap WHERE DateCreated > '" + DateModified + "' AND DateCreated  >= '" + DateTime.Now.AddYears(-1) + "'");

                dsRecruit = SqlHelper.ExecuteSBDataset(dsRecruit, "SRecruit", con, CommandType.Text, strRecruitsQuery.ToString());

                dsMeasurement = SqlHelper.ExecuteSBDataset(dsMeasurement, "RecruitDressMeasurementMap", con, CommandType.Text, strQuery.ToString());

                aList.Add(dsRecruit);
                aList.Add(dsMeasurement);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return aList;
        }

        public static ArrayList GetRecruitRecord(string ConnectionString, string DateModified, int milliseconds)
        {
            DataSet dsRecruit = new DataSet();
            DataSet dsMeasurement = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);
            ArrayList aList = new ArrayList();
            try
            {
                StringBuilder strRecruitsQuery = new StringBuilder();
                strRecruitsQuery.Append("SELECT SR.RecruitID, SR.SchoolID, SR.RecruitName, SR.SSN, SR.PlatoonNumber, SR.UploadDate, SR.Note, SR.FileID,");
                strRecruitsQuery.Append("SR.Component,SR.Rank,SR.Gender,SR.LastModifiedDate,SR.Verified,SR.VerifiedByRoleID,SR.SessionApproved,");
                strRecruitsQuery.Append("SR.TailorUserID,SR.sonabendApproved,SF.FileID,SF.FileName,");
                strRecruitsQuery.Append("SF.FileLocation,SF.Description,SF.SchoolID,SF.DateUploaded,SF.FileStatusID,SF.RecordCount,SF.UploadedByUserID,");
                strRecruitsQuery.Append("SF.SyncToTabletDate,SF.SyncToTabletUserID,SF.SyncFromTabletDate,SF.SyncFromTabletUserID,SF.SynchFileLocation ");
                strRecruitsQuery.Append("FROM SRecruit SR LEFT OUTER JOIN SFile SF ON SR.FileID = SF.FileID Where SF.FileStatusID > 1 ");
                strRecruitsQuery.Append("AND LastModifiedDate IS NULL OR LastModifiedDate >'" + DateModified + "' AND UploadDate >='" + DateTime.Now.AddYears(-1) + "'");



                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("Select RecruitDressMeasureID,RecruitID,SchoolID,DressID,MeasurementAttributeID,Value,DateCreated,IsFirstFit,");
                strQuery.Append("IsAccelerated,SubsequentFit,IsSecondFit,TailorID,BoardmanID,SonabendUserID,SupervisorUserID");
                strQuery.Append(" FROM RecruitDressMeasurementMap WHERE DateCreated > '" + DateModified + "' AND DateCreated  >= '" + DateTime.Now.AddYears(-1) + "'");


                //dsRecruit = SqlHelper.ExecuteSBDataset(dsRecruit, "SRecruit", con, CommandType.Text, "Select * from SRecruit SR LEFT OUTER JOIN SFile SF ON SR.FileID = SF.FileID Where SF.FileStatusID > 1 AND LastModifiedDate IS NULL OR LastModifiedDate >'" + DateModified + "' AND UploadDate >='" + DateTime.Now.AddYears(-1) + "'");
                dsRecruit = SqlHelper.ExecuteSBDataset(dsRecruit, "SRecruit", con, CommandType.Text, strRecruitsQuery.ToString());
                //dsMeasurement = SqlHelper.ExecuteSBDataset(dsMeasurement, "RecruitDressMeasurementMap", con, CommandType.Text,
                //    "select RecruitDressMeasureID, RecruitID, SchoolID, DressID, MeasurementAttributeID, Value, DATEADD(millisecond,-" + milliseconds.ToString() + ",DateCreated) AS DateCreated, IsFirstFit, IsAccelerated, IsSecondFit, SubsequentFit, TailorID, BoardmanID FROM RecruitDressMeasurementMap WHERE DateCreated > '" + DateModified + "' AND DateCreated  >= '" + DateTime.Now.AddYears(-1) + "'");

                dsMeasurement = SqlHelper.ExecuteSBDataset(dsMeasurement, "RecruitDressMeasurementMap", con, CommandType.Text, strQuery.ToString());

                aList.Add(dsRecruit);
                aList.Add(dsMeasurement);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return aList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="DateModified"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static DataSet GetRecruitsData(string ConnectionString, string lastSyncDate, int milliseconds)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                DataSet dsRecruit = new DataSet();

                StringBuilder strRecruitsQuery = new StringBuilder();
                strRecruitsQuery.Append("SELECT SR.RecruitID, SR.SchoolID, SR.RecruitName, SR.SSN, SR.PlatoonNumber, SR.UploadDate, SR.Note, SR.FileID,");
                strRecruitsQuery.Append("SR.Component,SR.Rank,SR.Gender,SR.LastModifiedDate,SR.Verified,SR.VerifiedByRoleID,SR.SessionApproved,");
                strRecruitsQuery.Append("SR.TailorUserID,SR.sonabendApproved,SF.FileID,SF.FileName,");
                strRecruitsQuery.Append("SF.FileLocation,SF.Description,SF.SchoolID,SF.DateUploaded,SF.FileStatusID,SF.RecordCount,SF.UploadedByUserID,");
                strRecruitsQuery.Append("SF.SyncToTabletDate,SF.SyncToTabletUserID,SF.SyncFromTabletDate,SF.SyncFromTabletUserID,SF.SynchFileLocation ");
                strRecruitsQuery.Append(",SR.IsFirstFit,SR.IsSecondFit,SR.IsThirdFit,SR.IsFourthFit,SR.IsSubsequentFit ");
                strRecruitsQuery.Append("FROM SRecruit SR LEFT OUTER JOIN SFile SF ON SR.FileID = SF.FileID Where SF.FileStatusID > 1 ");
                strRecruitsQuery.Append("AND LastModifiedDate IS NULL OR LastModifiedDate >'" + lastSyncDate + "' AND UploadDate >='" + DateTime.Now.AddYears(-1) + "'");

                return SqlHelper.ExecuteSBDataset(dsRecruit, "SRecruit", con, CommandType.Text, strRecruitsQuery.ToString());

                //return SqlHelper.ExecuteSBDataset(dsRecruit, "SRecruit", con, CommandType.Text, "Select * from SRecruit SR LEFT OUTER JOIN SFile SF ON SR.FileID = SF.FileID Where SF.FileStatusID > 1 AND LastModifiedDate IS NULL OR LastModifiedDate >'" + lastSyncDate + "' AND UploadDate >='" + DateTime.Now.AddYears(-1) + "'");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
        }

        /// <summary>
        /// Returns recruit measurement data based on last sync date, lastMeasurementID and chunkSize
        /// </summary>
        /// <param name="lastSyncDate"></param>
        /// <param name="offsetTicks">Not implemented</param>
        /// <param name="lastMeasurementID"></param>
        /// <param name="chunkSize">Max records to return, -1 - all records</param>
        /// <returns></returns>
        public static DataSet GetRecruitsMeasurementData(string ConnectionString, string lastSyncDate, int milliseconds, long lastMeasurementID, int chunkSize)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                //dsMeasurement = SqlHelper.ExecuteSBDataset(dsMeasurement, "RecruitDressMeasurementMap", con, CommandType.Text,
                //    "select RecruitDressMeasureID, RecruitID, SchoolID, DressID, MeasurementAttributeID, Value, DATEADD(millisecond,-" + milliseconds.ToString() + ",DateCreated) AS DateCreated, IsFirstFit, IsAccelerated, IsSecondFit, SubsequentFit, TailorID, BoardmanID FROM RecruitDressMeasurementMap WHERE DateCreated > '" + DateModified + "' AND DateCreated  >= '" + DateTime.Now.AddYears(-1) + "'");
                DataSet dsMeasurement = new DataSet();
                string strQuery = "SELECT RecruitDressMeasureID,RecruitID,SchoolID,DressID,MeasurementAttributeID,Value,DateCreated,IsFirstFit,";
                strQuery += "IsAccelerated,SubsequentFit,IsSecondFit,IsThirdFit,IsFourthFit,TailorID,BoardmanID,SonabendUserID,SupervisorUserID";
                strQuery += " FROM RecruitDressMeasurementMap WHERE DateCreated > '" + lastSyncDate + "' AND DateCreated  >= '" + DateTime.Now.AddYears(-1) + "'";
                if (lastMeasurementID > 0)
                    strQuery += " AND RecruitDressMeasureID > " + lastMeasurementID;
                if (chunkSize > 0)
                {
                    strQuery = strQuery.Replace("SELECT RecruitDressMeasureID,RecruitID,SchoolID,DressID,MeasurementAttributeID,Value,DateCreated,IsFirstFit,IsAccelerated,SubsequentFit,IsSecondFit,IsThirdFit,IsFourthFit,TailorID,BoardmanID,SonabendUserID,SupervisorUserID", string.Format("SELECT TOP {0} *", chunkSize));
                }
                strQuery += " ORDER by RecruitDressMeasureID ASC";
                return SqlHelper.ExecuteSBDataset(dsMeasurement, "RecruitDressMeasurementMap", con, CommandType.Text, strQuery);
            }
            catch (Exception ex)
            {
                WriteEntry(ex.Message + ex.StackTrace);
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
        }

        public static DataSet GetRecruitsMeasurementDataDM(string ConnectionString, string lastSyncDate)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                DataSet dsMeasurement = new DataSet();
                string strQuery = "SELECT RecruitDressDMID,RecruitID,SchoolID,DressID,LengthID,SizeID,Count,DateCreated,IsFirstFit,";
                strQuery += "SubsequentFit,IsSecondFit,IsThirdFit,IsFourthFit,TailorID,BoardmanID,SonabendUserID,SupervisorUserID";
                strQuery += " FROM sRecruitDressDMMaster WHERE DateCreated > '" + lastSyncDate + "' AND DateCreated  >= '" + DateTime.Now.AddYears(-1) + "'";
                strQuery += " ORDER by RecruitDressDMID ASC";
                return SqlHelper.ExecuteSBDataset(dsMeasurement, "RecruitDressDMMaster", con, CommandType.Text, strQuery);
            }
            catch (Exception ex)
            {
                WriteEntry(ex.Message + ex.StackTrace);
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
        }

        public static DataSet GetRecruitsMeasurementDataPFC(string ConnectionString, string lastSyncDate)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                DataSet dsMeasurement = new DataSet();
                string strQuery = "SELECT RecruitDressPFCID,RecruitID,SchoolID,DressID,Count,DateCreated,IsFirstFit,";
                strQuery += "SubsequentFit,IsSecondFit,IsThirdFit,IsFourthFit,TailorID,BoardmanID,SonabendUserID,SupervisorUserID";
                strQuery += " FROM sRecruitDressPFCMaster WHERE DateCreated > '" + lastSyncDate + "' AND DateCreated  >= '" + DateTime.Now.AddYears(-1) + "'";
                strQuery += " ORDER by RecruitDressPFCID ASC";
                return SqlHelper.ExecuteSBDataset(dsMeasurement, "RecruitDressPFCMaster", con, CommandType.Text, strQuery);
            }
            catch (Exception ex)
            {
                WriteEntry(ex.Message + ex.StackTrace);
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
        }

        public static DataSet GetSFile(string ConnectionString, string DateUploaded)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append("Select FileID,FileName,FileLocation,Description,SchoolID,DateUploaded,FileStatusID,");
                strQuery.Append("RecordCount,UploadedByUserID,SyncToTabletDate,SyncToTabletUserID,SyncFromTabletDate,SyncFromTabletUserID,");
                strQuery.Append("SynchFileLocation from Sfile Where dateuploaded > '" + DateUploaded + "' AND FileStatusID>1");
                ds = SqlHelper.ExecuteSBDataset(ds, "SFile", con, CommandType.Text, strQuery.ToString());

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return ds;
        }

        public static int CheckAndUploadWithDMData(string DBConnectionString, DataTable dsRecruit, DataTable dtDMData, DataTable dtPFCData)
        {
            int count = CheckAndUpload(DBConnectionString, dsRecruit);
            int count2 = CheckAndUploadDM(DBConnectionString, dtDMData);
            int count3 = CheckAndUploadPFC(DBConnectionString, dtPFCData);
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DBConnectionString"></param>
        /// <param name="dsRecruit">Contains recruits measurement data(Inner Join)</param>
        /// <returns></returns>
        public static int CheckAndUpload(string DBConnectionString, DataTable dsRecruit)
        {
            int j = 0, SRecruitid = 0;
            //DataSet ds = new DataSet();
            //bool isfirstfit = true;
            int recid = 0;
            SqlConnection con = new SqlConnection(DBConnectionString);
            // Open the Database Connection
            con.Open();

            try
            {


                using (SqlTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        //if (dsRecruit != null && dsRecruit.Rows.Count > 0)
                        //{
                        //int reid = dsRecruit.Rows[0]["ServerRecruitID"].ToString() != "" ? Convert.ToInt32(dsRecruit.Rows[0]["ServerRecruitID"]) : 0;
                        //ds = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select * from recruitDressMeasurementMap Where RecruitID = " + reid);
                        //if (ds != null && ds.Tables[0].Rows.Count > 0)
                        //{
                        //    isfirstfit = false;
                        //}
                        //}
                        int Accelerated = 0;
                        int isFirstFit = 0;
                        int isSecondFit = 0;
                        int SubsequentFit = 0;
                        int isThirdFit = 0;
                        int isForthFit = 0;
                        Dictionary<string, string> dRecruits = new Dictionary<string, string>();
                        foreach (DataRow dr in dsRecruit.Rows)
                        {
                            int verified = dr["Verified"].ToString() == "True" ? 1 : 0;
                            int sessionApproved = dr["SessionApproved"].ToString() == "True" ? 1 : 0;
                            int sonabendApproved = dr["SonabendApproved"].ToString() == "True" ? 1 : 0;
                            int component = dr["COMPONENT"].ToString() == "True" ? 1 : 0;
                            Accelerated = dr["IsAccelerated"].ToString() == "True" ? 1 : 0;
                            isFirstFit = dr["IsFirstFit"].ToString() == "True" ? 1 : 0;
                            isSecondFit = dr["IsSecondFit"].ToString() == "True" ? 1 : 0;
                            isThirdFit = dr["IsThirdFit"].ToString() == "True" ? 1 : 0;
                            isForthFit = dr["IsFourthFit"].ToString() == "True" ? 1 : 0;
                            SubsequentFit = dr["SubsequentFit"].ToString() == "True" ? 1 : 0;

                            try { WriteEntry("Invoking Recruit Details:" + DateTime.Now.ToString("F")); }
                            catch { }

                            DataSet dsRecruitMap = null;
                            if (dr["ServerRecruitID"].ToString() != "")
                            {
                                if (isFirstFit == 1)
                                    dsRecruitMap = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select RecruitDressMeasureID,RecruitID,MeasurementAttributeID,Value From RecruitDressMeasurementMap Where RecruitID =" + dr["ServerRecruitID"] + " AND MeasurementAttributeID =" + dr["MeasurementAttributeID"] + " AND IsFirstFit =" + isFirstFit);
                                //Adapter = new SqlCeDataAdapter("Select * From RecruitDressMeasurementMap Where RecruitID =" + recruitID + " AND MeasurementAttributeID =" + dr["MeasurementAttributeID"] + " AND DateCreated <='" + DateCreated + "' AND IsFirstFit =" + IsFirstFit + " AND IsSecondFit = " + IsSecondFit + " AND SubsequentFit=" + IsSubsequentFit, Conn);
                                else if (isSecondFit == 1)
                                    dsRecruitMap = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select RecruitDressMeasureID,RecruitID,MeasurementAttributeID,Value From RecruitDressMeasurementMap Where RecruitID =" + dr["ServerRecruitID"] + " AND MeasurementAttributeID =" + dr["MeasurementAttributeID"] + " AND IsSecondFit =" + isSecondFit);
                                else if (SubsequentFit == 1)    // Also date condition, 
                                    dsRecruitMap = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select RecruitDressMeasureID,RecruitID,MeasurementAttributeID,Value From RecruitDressMeasurementMap Where RecruitID =" + dr["ServerRecruitID"] + " AND MeasurementAttributeID =" + dr["MeasurementAttributeID"] + " AND DateCreated >='" + DateTime.Now.ToString().Split(' ')[0] + "' AND SubsequentFit =" + SubsequentFit);
                                else if (isThirdFit == 1)    // Also date condition, 
                                    dsRecruitMap = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select RecruitDressMeasureID,RecruitID,MeasurementAttributeID,Value From RecruitDressMeasurementMap Where RecruitID =" + dr["ServerRecruitID"] + " AND MeasurementAttributeID =" + dr["MeasurementAttributeID"] + " AND IsThirdFit =" + isThirdFit);
                                else if (isForthFit == 1)    // Also date condition, 
                                    dsRecruitMap = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select RecruitDressMeasureID,RecruitID,MeasurementAttributeID,Value From RecruitDressMeasurementMap Where RecruitID =" + dr["ServerRecruitID"] + " AND MeasurementAttributeID =" + dr["MeasurementAttributeID"] + " AND IsFourthFit =" + isForthFit);

                            }
                            if (dsRecruitMap != null && dsRecruitMap.Tables.Count > 0 && dsRecruitMap.Tables[0].Rows.Count > 0)
                            {
                                if (SubsequentFit == 1)
                                {
                                    if (dr["MeasurementAttributeID"].ToString() == dsRecruitMap.Tables[0].Rows[0]["MeasurementAttributeID"].ToString())
                                    {
                                        if (Convert.ToInt32(dr["Value"]) > 0)
                                        {
                                            // update only if value differs
                                            j = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE RecruitDressMeasurementMap SET SchoolID = " + dr["SchoolID"] + ",DressID =" + dr["DressID"] + ",Value =" + dr["Value"] + ",IsAccelerated=" + Accelerated + " Where MeasurementAttributeID =" + dr["MeasurementAttributeID"] + " AND RecruitID =" + dr["ServerRecruitID"] + " AND IsFirstFit =" + isFirstFit + " AND IsSecondFit=" + isSecondFit + " AND SubsequentFit=" + SubsequentFit);
                                        }
                                    }

                                }
                            }
                            else
                            {
                                if (dr["ServerRecruitID"].ToString() != "")
                                {
                                    try { WriteEntry("Insert INTO recruitDressMeasurementMap(RecruitID,SchoolID,DressID,MeasurementAttributeID,Value,DateCreated,IsFirstFit,IsAccelerated,IsSecondFit,SubsequentFit,TailorID,BoardmanID,SonabendUserID,SupervisorUserID,IsThirdFit,IsFourthFit) Values(" + dr["ServerRecruitID"] + "," + dr["SchoolID"] + "," + dr["DressID"] + "," + dr["MeasurementAttributeID"] + "," + dr["Value"] + ",'" + DateTime.Now.ToString() + "'," + isFirstFit + "," + Accelerated + "," + isSecondFit + "," + SubsequentFit + "," + dr["TailorID"] + "," + dr["BoardmanID"] + "," + dr["SonabendUserID"] + "," + dr["SupervisorUserID"] + "," + isThirdFit + "," + isForthFit + ")"); }
                                    catch { }

                                    //if (!isfirstfit)
                                    j = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "Insert INTO recruitDressMeasurementMap(RecruitID,SchoolID,DressID,MeasurementAttributeID,Value,DateCreated,IsFirstFit,IsAccelerated,IsSecondFit,SubsequentFit,TailorID,BoardmanID,SonabendUserID,SupervisorUserID,IsThirdFit,IsFourthFit) Values(" + dr["ServerRecruitID"] + "," + dr["SchoolID"] + "," + dr["DressID"] + "," + dr["MeasurementAttributeID"] + "," + dr["Value"] + ",'" + DateTime.Now.ToString() + "'," + isFirstFit + "," + Accelerated + "," + isSecondFit + "," + SubsequentFit + "," + dr["TailorID"] + "," + dr["BoardmanID"] + "," + dr["SonabendUserID"] + "," + dr["SupervisorUserID"] + "," + isThirdFit + "," + isForthFit + ")");

                                    try { WriteEntry("UPDATE SRecruit set IsFirstFit = " + isFirstFit + ",IsSecondFit=" + isSecondFit + ",IsThirdFit=" + isThirdFit + ",IsFourthFit=" + isForthFit + ",IsSubsequentFit=" + SubsequentFit + ",LastModifiedDate='" + dr["LastModifiedDate"] + "'  WHERE RecruitID = " + dr["ServerRecruitID"] + " AND SchoolID=" + dr["SchoolID"]); }
                                    catch { }
                                    //updating the Srecurit
                                    SRecruitid = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE SRecruit set IsFirstFit = " + isFirstFit + ",IsSecondFit=" + isSecondFit + ",IsThirdFit=" + isThirdFit + ",IsFourthFit=" + isForthFit + ",IsSubsequentFit=" + SubsequentFit + ",LastModifiedDate='" + dr["LastModifiedDate"] + "'  WHERE RecruitID = " + dr["ServerRecruitID"] + " AND SchoolID=" + dr["SchoolID"]);

                                    // 12th nov 2012 modified to run once per recriut
                                    string key = dr["VerifiedByRoleID"].ToString() + "," + dr["TailorUserID"].ToString() + "," + dr["ServerRecruitID"].ToString();
                                    if (!dRecruits.ContainsKey(key))
                                        dRecruits.Add(key, dr["Note"].ToString());
                                    //SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "Update srecruit set LastModifiedDate ='" + DateTime.Now.ToString() + "',SessionApproved = 1,Verified = 1,VerifiedByRoleID=" + dr["VerifiedByRoleID"] + ",TailorUserID=" + dr["TailorUserID"] + ",SonabendApproved =1,Note ='" + dr["Note"] + "' Where RecruitID = " + dr["ServerRecruitID"]);
                                }
                                else
                                {
                                    // 
                                    DataSet dsRecID = SqlHelper.ExecuteDataset(trans, CommandType.Text, "SELECT RecruitID,RecruitName FROM SRECRUIT WHERE RecruitName = '" + dr["RecruitName"] + "' AND SSN like '%" + dr["SSN"] + "' AND PlatoonNumber = '" + dr["PlatoonNumber"] + "' AND SchoolID=" + dr["SchoolID"]);
                                    if (dsRecID != null && dsRecID.Tables[0].Rows.Count > 0)  // 2nd measurement onwards
                                    {
                                        recid = Convert.ToInt32(dsRecID.Tables[0].Rows[0]["RecruitID"]);
                                    }
                                    else
                                    {
                                        try { WriteEntry("INSERT INTO srecruit(SchoolID,RecruitName,SSN,PlatoonNumber,UploadDate,Note,Component,Rank,Gender,LastModifiedDate,Verified,VerifiedByRoleID,SessionApproved,TailorUserID,SonabendApproved,IsFirstFit,IsSecondFit,IsSubsequentFit,IsThirdFit,IsFourthFit) VALUES(" + dr["SchoolID"] + ",'" + dr["RecruitName"] + "','" + dr["SSN"] + "','" + dr["PlatoonNumber"] + "','" + DateTime.Now.ToString() + "','" + dr["Note"] + "'," + component + ",'" + dr["Rank"] + "','" + dr["Gender"] + "','" + DateTime.Now.ToString() + "'," + verified + "," + dr["VerifiedByRoleID"] + "," + sessionApproved + "," + dr["TailorUserID"] + "," + sonabendApproved + "," + isFirstFit + "," + isSecondFit + "," + SubsequentFit + "," + isThirdFit + "," + isForthFit + ")"); }
                                        catch { }

                                        // if recruit is created in tablet
                                        SqlHelper.ExecuteScalar(trans, CommandType.Text, "INSERT INTO srecruit(SchoolID,RecruitName,SSN,PlatoonNumber,UploadDate,Note,Component,Rank,Gender,LastModifiedDate,Verified,VerifiedByRoleID,SessionApproved,TailorUserID,SonabendApproved,IsFirstFit,IsSecondFit,IsSubsequentFit,IsThirdFit,IsFourthFit) VALUES(" + dr["SchoolID"] + ",'" + dr["RecruitName"] + "','" + dr["SSN"] + "','" + dr["PlatoonNumber"] + "','" + DateTime.Now.ToString() + "','" + dr["Note"] + "'," + component + ",'" + dr["Rank"] + "','" + dr["Gender"] + "','" + DateTime.Now.ToString() + "'," + verified + "," + dr["VerifiedByRoleID"] + "," + sessionApproved + "," + dr["TailorUserID"] + "," + sonabendApproved + "," + isFirstFit + "," + isSecondFit + "," + SubsequentFit + "," + isThirdFit + "," + isForthFit + ")");

                                        recid = Convert.ToInt32(SqlHelper.ExecuteScalar(trans, CommandType.Text, "SELECT RecruitID FROM SRECRUIT WHERE RecruitName = '" + dr["RecruitName"] + "' AND SSN like '" + dr["SSN"] + "' AND PlatoonNumber = '" + dr["PlatoonNumber"] + "' AND SchoolID =" + dr["SchoolID"] + " "));
                                    }

                                    try { WriteEntry("Insert INTO recruitDressMeasurementMap(RecruitID,SchoolID,DressID,MeasurementAttributeID,Value,DateCreated,IsFirstFit,IsAccelerated,IsSecondFit,SubsequentFit,TailorID,BoardmanID,SonabendUserID,SupervisorUserID,IsThirdFit,IsFourthFit) Values(" + recid + "," + dr["SchoolID"] + "," + dr["DressID"] + "," + dr["MeasurementAttributeID"] + "," + dr["Value"] + ",'" + DateTime.Now.ToString() + "'," + isFirstFit + "," + Accelerated + "," + isSecondFit + "," + SubsequentFit + "," + dr["TailorID"] + "," + dr["BoardmanID"] + "," + dr["SonabendUserID"] + "," + dr["SupervisorUserID"] + "," + isThirdFit + "," + isForthFit + ")"); }
                                    catch { }

                                    j = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "Insert INTO recruitDressMeasurementMap(RecruitID,SchoolID,DressID,MeasurementAttributeID,Value,DateCreated,IsFirstFit,IsAccelerated,IsSecondFit,SubsequentFit,TailorID,BoardmanID,SonabendUserID,SupervisorUserID,IsThirdFit,IsFourthFit) Values(" + recid + "," + dr["SchoolID"] + "," + dr["DressID"] + "," + dr["MeasurementAttributeID"] + "," + dr["Value"] + ",'" + DateTime.Now.ToString() + "'," + isFirstFit + "," + Accelerated + "," + isSecondFit + "," + SubsequentFit + "," + dr["TailorID"] + "," + dr["BoardmanID"] + "," + dr["SonabendUserID"] + "," + dr["SupervisorUserID"] + "," + isThirdFit + "," + isForthFit + ")");

                                }
                            }
                        }

                        //Update notes in single shot for each recruit
                        foreach (KeyValuePair<string, string> pair in dRecruits)
                        {
                            string[] keys = pair.Key.Split(',');
                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "Update srecruit set LastModifiedDate ='" + DateTime.Now.ToString() + "',SessionApproved = 1,Verified = 1,VerifiedByRoleID=" + keys[0] + ",TailorUserID=" + keys[1] + ",SonabendApproved =1,Note ='" + pair.Value + "' Where RecruitID = " + keys[2]);
                        }
                        trans.Commit();

                    }

                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    return j;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
        }

        public static int CheckAndUploadDM(string DBConnectionString, DataTable dsRecruit)
        {
            int j = 0, SRecruitid = 0;
            //DataSet ds = new DataSet();
            //bool isfirstfit = true;
            int recid = 0;
            SqlConnection con = new SqlConnection(DBConnectionString);
            // Open the Database Connection
            con.Open();

            try
            {


                using (SqlTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        int isFirstFit = 0;
                        int isSecondFit = 0;
                        int SubsequentFit = 0;
                        int isThirdFit = 0;
                        int isForthFit = 0;
                        Dictionary<string, string> dRecruits = new Dictionary<string, string>();
                        foreach (DataRow dr in dsRecruit.Rows)
                        {
                            int verified = dr["Verified"].ToString() == "True" ? 1 : 0;
                            int sessionApproved = dr["SessionApproved"].ToString() == "True" ? 1 : 0;
                            int sonabendApproved = dr["SonabendApproved"].ToString() == "True" ? 1 : 0;
                            int component = dr["COMPONENT"].ToString() == "True" ? 1 : 0;
                            isFirstFit = dr["IsFirstFit"].ToString() == "True" ? 1 : 0;
                            isSecondFit = dr["IsSecondFit"].ToString() == "True" ? 1 : 0;
                            isThirdFit = dr["IsThirdFit"].ToString() == "True" ? 1 : 0;
                            isForthFit = dr["IsFourthFit"].ToString() == "True" ? 1 : 0;
                            SubsequentFit = dr["SubsequentFit"].ToString() == "True" ? 1 : 0;

                            try { WriteEntry("Invoking Recruit Details:" + DateTime.Now.ToString("F")); }
                            catch { }

                            DataSet dsRecruitMap = null;
                            if (dr["ServerRecruitID"].ToString() != "")
                            {
                                if (isFirstFit == 1)
                                    dsRecruitMap = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select RecruitDressDMID,RecruitID,DressID,SizeID,LengthID,Count From sRecruitDressDMMaster Where RecruitID =" + dr["ServerRecruitID"] + " AND DressID =" + dr["DressID"] + " AND IsFirstFit =" + isFirstFit);
                                else if (isSecondFit == 1)
                                    dsRecruitMap = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select RecruitDressDMID,RecruitID,DressID,SizeID,LengthID,Count From sRecruitDressDMMaster Where RecruitID =" + dr["ServerRecruitID"] + " AND DressID =" + dr["DressID"] + " AND isSecondFit =" + isSecondFit);
                                else if (SubsequentFit == 1)    // Also date condition, 
                                    dsRecruitMap = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select RecruitDressDMID,RecruitID,DressID,SizeID,LengthID,Count From sRecruitDressDMMaster Where RecruitID =" + dr["ServerRecruitID"] + " AND DressID =" + dr["DressID"] + " AND DateCreated >='" + DateTime.Now.ToString().Split(' ')[0] + "' AND SubsequentFit =" + SubsequentFit);
                                else if (isThirdFit == 1)    // Also date condition, 
                                    dsRecruitMap = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select RecruitDressDMID,RecruitID,DressID,SizeID,LengthID,Count From sRecruitDressDMMaster Where RecruitID =" + dr["ServerRecruitID"] + " AND DressID =" + dr["DressID"] + " AND IsThirdFit =" + isThirdFit);
                                else if (isForthFit == 1)    // Also date condition, 
                                    dsRecruitMap = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select RecruitDressDMID,RecruitID,DressID,SizeID,LengthID,Count From sRecruitDressDMMaster Where RecruitID =" + dr["ServerRecruitID"] + " AND DressID =" + dr["DressID"] + " AND IsFourthFit =" + isForthFit);

                            }
                            if (dsRecruitMap != null && dsRecruitMap.Tables.Count > 0 && dsRecruitMap.Tables[0].Rows.Count > 0)
                            {
                                if (SubsequentFit == 1)
                                {
                                    if (dr["DressID"].ToString() == dsRecruitMap.Tables[0].Rows[0]["DressID"].ToString())
                                    {
                                        if (Convert.ToInt32(dr["Count"]) > 0)
                                        {
                                            // update only if value differs
                                            j = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE sRecruitDressDMMaster SET SchoolID = " + dr["SchoolID"] + ",DressID =" + dr["DressID"] + ",Count =" + dr["Count"] + ",SizeID=" + CheckNull(dr["SizeID"]) + ",LengthID=" + CheckNull(dr["LengthID"]) + " Where DressID =" + dr["DressID"] + " AND RecruitID =" + dr["ServerRecruitID"] + " AND IsFirstFit =" + isFirstFit + " AND IsSecondFit=" + isSecondFit + " AND SubsequentFit=" + SubsequentFit);
                                        }
                                    }

                                }
                            }
                            else
                            {
                                int recrid = 0;
                                if (dr["ServerRecruitID"].ToString() != "")
                                {
                                    recrid = Convert.ToInt32(dr["ServerRecruitID"].ToString());
                                }
                                else
                                {
                                    recrid = Convert.ToInt32(SqlHelper.ExecuteScalar(trans, CommandType.Text, "SELECT RecruitID FROM SRECRUIT WHERE RecruitName = '" + dr["RecruitName"] + "' AND SSN like '" + dr["SSN"] + "' AND PlatoonNumber = '" + dr["PlatoonNumber"] + "' AND SchoolID =" + dr["SchoolID"] + " "));
                                }


                                try { WriteEntry("Insert INTO sRecruitDressDMMaster(RecruitID,SchoolID,DressID,SizeID,LengthID,Count,DateCreated,IsFirstFit,IsSecondFit,SubsequentFit,TailorID,BoardmanID,SonabendUserID,SupervisorUserID,IsThirdFit,IsFourthFit) Values(" + recrid + "," + dr["SchoolID"] + "," + dr["DressID"] + "," + CheckNull(dr["SizeID"]) + "," + CheckNull(dr["LengthID"]) + "," + dr["Count"] + ",'" + DateTime.Now.ToString() + "'," + isFirstFit + "," + isSecondFit + "," + SubsequentFit + "," + dr["TailorID"] + "," + dr["BoardmanID"] + "," + dr["SonabendUserID"] + "," + dr["SupervisorUserID"] + "," + isThirdFit + "," + isForthFit + ")"); }
                                catch { }

                                //if (!isfirstfit)
                                j = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "Insert INTO sRecruitDressDMMaster(RecruitID,SchoolID,DressID,SizeID,LengthID,Count,DateCreated,IsFirstFit,IsSecondFit,SubsequentFit,TailorID,BoardmanID,SonabendUserID,SupervisorUserID,IsThirdFit,IsFourthFit) Values(" + recrid + "," + dr["SchoolID"] + "," + dr["DressID"] + "," + CheckNull(dr["SizeID"]) + "," + CheckNull(dr["LengthID"]) + "," + dr["Count"] + ",'" + DateTime.Now.ToString() + "'," + isFirstFit + "," + isSecondFit + "," + SubsequentFit + "," + dr["TailorID"] + "," + dr["BoardmanID"] + "," + dr["SonabendUserID"] + "," + dr["SupervisorUserID"] + "," + isThirdFit + "," + isForthFit + ")");
                            }
                        }

                        trans.Commit();

                    }

                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    return j;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
        }

        public static int CheckAndUploadPFC(string DBConnectionString, DataTable dsRecruit)
        {
            int j = 0, SRecruitid = 0;
            //DataSet ds = new DataSet();
            //bool isfirstfit = true;
            int recid = 0;
            SqlConnection con = new SqlConnection(DBConnectionString);
            // Open the Database Connection
            con.Open();

            try
            {


                using (SqlTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        int isFirstFit = 0;
                        int isSecondFit = 0;
                        int SubsequentFit = 0;
                        int isThirdFit = 0;
                        int isForthFit = 0;
                        Dictionary<string, string> dRecruits = new Dictionary<string, string>();
                        foreach (DataRow dr in dsRecruit.Rows)
                        {
                            int verified = dr["Verified"].ToString() == "True" ? 1 : 0;
                            int sessionApproved = dr["SessionApproved"].ToString() == "True" ? 1 : 0;
                            int sonabendApproved = dr["SonabendApproved"].ToString() == "True" ? 1 : 0;
                            int component = dr["COMPONENT"].ToString() == "True" ? 1 : 0;
                            isFirstFit = dr["IsFirstFit"].ToString() == "True" ? 1 : 0;
                            isSecondFit = dr["IsSecondFit"].ToString() == "True" ? 1 : 0;
                            isThirdFit = dr["IsThirdFit"].ToString() == "True" ? 1 : 0;
                            isForthFit = dr["IsFourthFit"].ToString() == "True" ? 1 : 0;
                            SubsequentFit = dr["SubsequentFit"].ToString() == "True" ? 1 : 0;

                            try { WriteEntry("Invoking Recruit Details:" + DateTime.Now.ToString("F")); }
                            catch { }

                            DataSet dsRecruitMap = null;
                            if (dr["ServerRecruitID"].ToString() != "")
                            {
                                if (isFirstFit == 1)
                                    dsRecruitMap = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select RecruitDressPFCID,RecruitID,DressID,Count From sRecruitDressPFCMaster Where RecruitID =" + dr["ServerRecruitID"] + " AND DressID =" + dr["DressID"] + " AND IsFirstFit =" + isFirstFit);
                                else if (isSecondFit == 1)
                                    dsRecruitMap = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select RecruitDressPFCID,RecruitID,DressID,Count From sRecruitDressPFCMaster Where RecruitID =" + dr["ServerRecruitID"] + " AND DressID =" + dr["DressID"] + " AND isSecondFit =" + isSecondFit);
                                else if (SubsequentFit == 1)    // Also date condition, 
                                    dsRecruitMap = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select RecruitDressPFCID,RecruitID,DressID,Count From sRecruitDressPFCMaster Where RecruitID =" + dr["ServerRecruitID"] + " AND DressID =" + dr["DressID"] + " AND DateCreated >='" + DateTime.Now.ToString().Split(' ')[0] + "' AND SubsequentFit =" + SubsequentFit);
                                else if (isThirdFit == 1)    // Also date condition, 
                                    dsRecruitMap = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select RecruitDressPFCID,RecruitID,DressID,Count From sRecruitDressPFCMaster Where RecruitID =" + dr["ServerRecruitID"] + " AND DressID =" + dr["DressID"] + " AND IsThirdFit =" + isThirdFit);
                                else if (isForthFit == 1)    // Also date condition, 
                                    dsRecruitMap = SqlHelper.ExecuteDataset(trans, CommandType.Text, "Select RecruitDressPFCID,RecruitID,DressID,Count From sRecruitDressPFCMaster Where RecruitID =" + dr["ServerRecruitID"] + " AND DressID =" + dr["DressID"] + " AND IsFourthFit =" + isForthFit);

                            }
                            if (dsRecruitMap != null && dsRecruitMap.Tables.Count > 0 && dsRecruitMap.Tables[0].Rows.Count > 0)
                            {
                                if (SubsequentFit == 1)
                                {
                                    if (dr["DressID"].ToString() == dsRecruitMap.Tables[0].Rows[0]["DressID"].ToString())
                                    {
                                        if (Convert.ToInt32(dr["Count"]) > 0)
                                        {
                                            // update only if value differs
                                            j = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE sRecruitDressPFCMaster SET SchoolID = " + dr["SchoolID"] + ",DressID =" + dr["DressID"] + ",Count =" + dr["Count"] + " Where DressID =" + dr["DressID"] + " AND RecruitID =" + dr["ServerRecruitID"] + " AND IsFirstFit =" + isFirstFit + " AND IsSecondFit=" + isSecondFit + " AND SubsequentFit=" + SubsequentFit);
                                        }
                                    }

                                }
                            }
                            else
                            {
                                int recrid = 0;
                                if (dr["ServerRecruitID"].ToString() != "")
                                {
                                    recrid = Convert.ToInt32(dr["ServerRecruitID"].ToString());
                                }
                                else
                                {
                                    recrid = Convert.ToInt32(SqlHelper.ExecuteScalar(trans, CommandType.Text, "SELECT RecruitID FROM SRECRUIT WHERE RecruitName = '" + dr["RecruitName"] + "' AND SSN like '" + dr["SSN"] + "' AND PlatoonNumber = '" + dr["PlatoonNumber"] + "' AND SchoolID =" + dr["SchoolID"] + " "));
                                }


                                try { WriteEntry("Insert INTO sRecruitDressPFCMaster(RecruitID,SchoolID,DressID,Count,DateCreated,IsFirstFit,IsSecondFit,SubsequentFit,TailorID,BoardmanID,SonabendUserID,SupervisorUserID,IsThirdFit,IsFourthFit) Values(" + recrid + "," + dr["SchoolID"] + "," + dr["DressID"] + "," + dr["Count"] + ",'" + DateTime.Now.ToString() + "'," + isFirstFit + "," + isSecondFit + "," + SubsequentFit + "," + dr["TailorID"] + "," + dr["BoardmanID"] + "," + dr["SonabendUserID"] + "," + dr["SupervisorUserID"] + "," + isThirdFit + "," + isForthFit + ")"); }
                                catch { }

                                //if (!isfirstfit)
                                j = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "Insert INTO sRecruitDressPFCMaster(RecruitID,SchoolID,DressID,Count,DateCreated,IsFirstFit,IsSecondFit,SubsequentFit,TailorID,BoardmanID,SonabendUserID,SupervisorUserID,IsThirdFit,IsFourthFit) Values(" + recrid + "," + dr["SchoolID"] + "," + dr["DressID"] + "," + dr["Count"] + ",'" + DateTime.Now.ToString() + "'," + isFirstFit + "," + isSecondFit + "," + SubsequentFit + "," + dr["TailorID"] + "," + dr["BoardmanID"] + "," + dr["SonabendUserID"] + "," + dr["SupervisorUserID"] + "," + isThirdFit + "," + isForthFit + ")");
                            }
                        }

                        trans.Commit();

                    }

                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    return j;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
        }

        private static string CheckNull(object data)
        {
            try
            {
                int value = Convert.ToInt32(data.ToString());
                if (value > 0)
                    return value.ToString();
            }
            catch { }
            return "null";
        }

        public static int insertMacLog(string connectionString, string MacID, string syncDownDate, string syncUpDate)
        {
            int j = 0;
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                // Open the Database Connection
                con.Open();

                ds = SqlHelper.ExecuteSBDataset(ds, "SFile", con, CommandType.Text, "Select MacLogID,MachineID,SyncDownDate,SyncUpDate,DateCreated from SMacLog Where MachineID = '" + MacID + "'");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (syncDownDate != "")
                    {
                        j = Convert.ToInt32(SqlHelper.ExecuteScalar(con, CommandType.Text, "UPDATE SMacLog SET SyncDownDate ='" + syncDownDate + "' WHERE MachineID ='" + MacID + "'"));
                    }
                    else
                    {
                        j = Convert.ToInt32(SqlHelper.ExecuteScalar(con, CommandType.Text, "UPDATE SMacLog SET SyncUpDate ='" + syncUpDate + "' WHERE MachineID ='" + MacID + "'"));
                    }
                }
                else
                {
                    if (syncDownDate != "")
                    {
                        j = Convert.ToInt32(SqlHelper.ExecuteScalar(con, CommandType.Text, "Insert INTO SMacLog(MachineID,SyncDownDate,DateCreated) Values('" + MacID + "','" + syncDownDate + "','" + DateTime.Now.ToString() + "')"));
                    }
                    else
                    {
                        j = Convert.ToInt32(SqlHelper.ExecuteScalar(con, CommandType.Text, "Insert INTO SMacLog(MachineID,SyncUpDate,DateCreated) Values('" + MacID + "','" + syncUpDate + "','" + DateTime.Now.ToString() + "')"));
                    }
                }

                return j;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
        }

        public static DataSet GetSyncDate(string connectionString, int macID)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                // Open the Database Connection
                con.Open();

                ds = SqlHelper.ExecuteSBDataset(ds, "SFile", con, CommandType.Text, "Select MacLogID,MachineID,SyncDownDate,SyncUpDate,DateCreated from SMacLog Where MachineID = '" + macID + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return ds;
        }

        public static DataTable GetPriceList(string DBConnectionString, int schoolID)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(DBConnectionString);
            try
            {
                if (schoolID > 0)
                {
                    ds = SqlHelper.ExecuteSBDataset(ds, "SMeasurementAttirbute", con, CommandType.Text, "select Distinct Attributecode,price,AtrributeName from smeasurementattribute where attributecode is not null AND SchoolID = " + schoolID);
                }
                else
                    ds = SqlHelper.ExecuteSBDataset(ds, "SPriceMaster", con, CommandType.StoredProcedure, "SEL_AttributePrice");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return ds.Tables[0];
        }

        public static int InsertPrice(string DBConnectionString, double price, string attributeCode, string attName)
        {
            int i = 0;
            SqlConnection con = new SqlConnection(DBConnectionString);

            try
            {
                i = SqlHelper.ExecuteNonQuery(con, CommandType.Text, "Insert into SPriceMaster(AttributeCode,Price,EffectiveDate,DateCreated,AttributeName) Values('" + attributeCode + "'," + price + ",'" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + attName + "')");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return i;
        }

        public static DataTable GetFileDetails(string DBConnectionString)
        {
            SqlConnection con = new SqlConnection(DBConnectionString);
            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteSBDataset(ds, "SFile", con, CommandType.Text, "Select filename from Sfile");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return ds.Tables[0];
        }

        public static SBModel.MainInvoiceByDateRangeDataTable MainInvoiceByDateRange(string strConn, string fromDate, string toDate, string SchoolID, int component, string platoon, int MeasurementID)
        {
            SBModel psm = new SBModel();

            SqlConnection con = new SqlConnection(strConn);
            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@FromDate", fromDate),
                                                new SqlParameter("@ToDate", toDate),
                                                new SqlParameter("@SchoolID", SchoolID),
                                                new SqlParameter("@COMPONENT", component),
                                                new SqlParameter("@PLATOON", platoon),
                                                new SqlParameter("@MeasurementAttributeID", MeasurementID)};

                //  psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "MainInvoiceByDateRange", con, CommandType.StoredProcedure, "rpt_MainInvoiceByDateRange", paramsArray);
                psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "MainInvoiceByDateRange", con, CommandType.StoredProcedure, "rpt_MainInvoiceByDateRangeCrystal", paramsArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return psm.MainInvoiceByDateRange;
        }

        public static SBModel.MainVoiceFilterCriteriaDataTable MainInvoiceFilter(string strConn, string fromDate, string toDate, string SchoolID, int component, string platoon, int MeasurementID)
        {
            SBModel psm = new SBModel();

            SqlConnection con = new SqlConnection(strConn);
            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@FromDate", fromDate),
                                                new SqlParameter("@ToDate", toDate),
                                                new SqlParameter("@SchoolID", SchoolID),
                                                new SqlParameter("@COMPONENT", component),
                                                new SqlParameter("@PLATOON", platoon),
                                                new SqlParameter("@MeasurementAttributeID", MeasurementID)};

                // psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "MainVoiceFilterCriteria", con, CommandType.StoredProcedure, "rpt_InvoiceByFilterCriteria", paramsArray);
                psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "MainVoiceFilterCriteria", con, CommandType.StoredProcedure, "rpt_InvoiceByFilterCriteriaCrystal", paramsArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return psm.MainVoiceFilterCriteria;
        }

        public static SBModel.SSchoolDataTable GetReportSchools(string DBConnectionString)
        {
            SBModel psm = new SBModel();

            SqlConnection con = new SqlConnection(DBConnectionString);
            try
            {
                psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "SSchool", con, CommandType.StoredProcedure, "rpt_Schools");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return psm.SSchool;
        }

        public static SBModel.SSchoolDataTable GetReportSchoolsSigs(string DBConnectionString)
        {
            SBModel psm = new SBModel();

            SqlConnection con = new SqlConnection(DBConnectionString);
            try
            {
                psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "SSchool", con, CommandType.StoredProcedure, "rpt_SchoolSigs");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return psm.SSchool;
        }

        public static SBModel.SSchoolDataTable GetReportSchoolsSigsW(string DBConnectionString)
        {
            SBModel psm = new SBModel();

            SqlConnection con = new SqlConnection(DBConnectionString);
            try
            {
                psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "SSchool", con, CommandType.StoredProcedure, "rpt_SchoolSigsW");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return psm.SSchool;
        }

        public static DataSet GetReportPlatoons(string DBConnectionString, string schools)
        {
            DataSet ds = new DataSet();

            SqlConnection con = new SqlConnection(DBConnectionString);
            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@SchoolID", schools),
                                               };
                ds = SqlHelper.ExecuteSBDataset(ds, "SFile", con, CommandType.StoredProcedure, "rpt_PlantoonsBySchoolID", paramsArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return ds;
        }


        public static DataTable GetInvoiceAccelDetails(string DBConnectionString, string fromDate, string toDate, string SchoolID, int component, string platoon, int MeasurementID)
        {
            DataSet ds = new DataSet();

            SqlConnection con = new SqlConnection(DBConnectionString);
            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@FromDate", fromDate),
                                                new SqlParameter("@ToDate", toDate),
                                                new SqlParameter("@SchoolID", SchoolID),
                                                new SqlParameter("@COMPONENT", component),
                                                new SqlParameter("@PLATOON", platoon),
                                                new SqlParameter("@MeasurementAttributeID", MeasurementID)};

                ds = SqlHelper.ExecuteSBDataset(ds, "SFile", con, CommandType.StoredProcedure, "rpt_AccPlatoonByFilterCriteria", paramsArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return ds.Tables[0];
        }

        public static SBModel.MainVoiceFilterCriteriaDataTable DateFilter(string DBConnectionString, string fromDate, string toDate, string SchoolID, int component, string platoon, int MeasurementID, int AttributeID, int isFirstFit, int isSecond, int subsequent, int isThirdFit, int isFourthFit)
        {
            SBModel psm = new SBModel();

            SqlConnection con = new SqlConnection(DBConnectionString);
            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@FromDate", fromDate),
                                                new SqlParameter("@ToDate", toDate),
                                                new SqlParameter("@SchoolID", SchoolID),
                                                new SqlParameter("@COMPONENT", component),
                                                new SqlParameter("@PLATOON", platoon),
                                                new SqlParameter("@MeasurementAttributeID", MeasurementID),
                                                new SqlParameter("@AttributeID",AttributeID>0?AttributeID:Convert.DBNull),
                                                new SqlParameter("@IsFirstFit",isFirstFit),
                                                new SqlParameter("@IsSecondFit",isSecond),
                                                new SqlParameter("@IsThirdFit",isThirdFit),
                                                new SqlParameter("@IsFourthFit",isFourthFit),
                                                new SqlParameter("@SubsequentFit",subsequent)};

                psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "MainVoiceFilterCriteria", con, CommandType.StoredProcedure, "rpt_DateByFilterCriteriaCrystal", paramsArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return psm.MainVoiceFilterCriteria;
        }

        public static SBModel.MainVoiceFilterCriteriaDataTable SchoolFilter(string DBConnectionString, string fromDate, string toDate, string schools, int component, string platoon, int MeasurementID)
        {
            SBModel psm = new SBModel();

            SqlConnection con = new SqlConnection(DBConnectionString);
            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@FromDate", fromDate),
                                                new SqlParameter("@ToDate", toDate),
                                                new SqlParameter("@SchoolID", schools),
                                                new SqlParameter("@COMPONENT", component),
                                                new SqlParameter("@PLATOON", platoon),
                                                new SqlParameter("@MeasurementAttributeID", MeasurementID)};

                //psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "MainVoiceFilterCriteria", con, CommandType.StoredProcedure, "rpt_SchoolByFilterCriteria", paramsArray);
                psm = (SBModel)SqlHelper.ExecuteSBDataset(psm, "MainVoiceFilterCriteria", con, CommandType.StoredProcedure, "rpt_SchoolByFilterCriteriaCrystal", paramsArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return psm.MainVoiceFilterCriteria;
        }

        public static DataTable PlatoonFilter(string DBConnectionString, string fromDate, string toDate, string schools, int component, string platoon, int MeasurementID)
        {
            DataSet ds = new DataSet();

            SqlConnection con = new SqlConnection(DBConnectionString);
            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@FromDate", fromDate),
                                                new SqlParameter("@ToDate", toDate),
                                                new SqlParameter("@SchoolID", schools),
                                                new SqlParameter("@COMPONENT", component),
                                                new SqlParameter("@PLATOON", platoon),
                                                new SqlParameter("@MeasurementAttributeID", MeasurementID)};

                //ds = SqlHelper.ExecuteSBDataset(ds, "SFile", con, CommandType.StoredProcedure, "rpt_Signrooster", paramsArray);
                ds = SqlHelper.ExecuteSBDataset(ds, "SFile", con, CommandType.StoredProcedure, "rpt_SignroosterCrystal", paramsArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return ds.Tables[0];
        }

        public static int AccEmrReportTodaysCount(string DBConnectionString)
        {
            int count = 0;
            SqlConnection con = new SqlConnection(DBConnectionString);
            con.Open();
            try
            {
                count = Convert.ToInt32(SqlHelper.ExecuteScalar(DBConnectionString, CommandType.StoredProcedure, "rptAccEmrReportCountToday"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return count;
        }



        public static DataTable GetAccEmrReportToday(string DBConnectionString)
        {
            DataSet ds = new DataSet();

            SqlConnection con = new SqlConnection(DBConnectionString);
            try
            {

                ds = SqlHelper.ExecuteSBDataset(ds, "SFile", con, CommandType.StoredProcedure, "rptAccEmrReportToday");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    con.Close();
                    con = null;
                }
                catch { }
            }
            return ds.Tables[0];
        }

        public static void WriteEntry(string entry)
        {
            entry += Environment.NewLine;
            entry += "------------------------------------------------------------------------";
            entry += Environment.NewLine;
            try
            {
                System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
                string sPath = System.IO.Path.GetDirectoryName(ass.CodeBase);
                sPath = sPath.Replace(@"file:\", "");
                sPath = System.IO.Path.Combine(sPath, "Logs");
                if (!System.IO.Directory.Exists(sPath))
                {
                    System.IO.Directory.CreateDirectory(sPath);
                }
                string sFileName = string.Format(@"{0}\Sonabend_{1}.Log", sPath, DateTime.Today.ToString("MMddyyyy"));
                FileStream fs = null;
                try
                {
                    fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
                    fs.Write(System.Text.Encoding.ASCII.GetBytes(entry), 0, entry.Length);
                    fs.Flush();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs = null;
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }

        }
    }
}
