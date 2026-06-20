<%@ Page Title="" Language="C#" MasterPageFile="~/AlterationRecords.Master" AutoEventWireup="true" CodeBehind="FileDetails.aspx.cs" Inherits="AlterationRecords.FileDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SonabendContent" runat="server">
    <div class="">
        <div class="main-container container-fluid">
            <div class="page-header d-sm-flex d-block">
                <h4 class="page-title"><i class="si si-docs me-1"></i>Details</h4>
            </div>
            <div class="row">
                <div class="col-md-12 col-lg-12">

                    <div class="card">
                        <div class="card-header">
                            <%--<div class="card-title">
										Left Label Alignment
									</div>--%>
                        </div>
                        <div class="card-body">
                            <form action="#">
                                <div class="row align-items-center">
                                    <div class="col-md-3">
                                        <asp:Label Text="File Name :" ID="lblFileName" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblFileNameValue" CssClass="form-label" runat="server" />
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-3">
                                        <asp:Label Text="Description :" ID="lblDescription" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblDescriptionValue" CssClass="form-label" runat="server" />
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblSchool" CssClass="form-label" Text="School :" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblSchoolValue" CssClass="form-label" runat="server" />
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-3">
                                        <asp:Label Text="Date Uploaded :" CssClass="form-label" ID="lblDateUploaded" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblDateValue" CssClass="form-label" runat="server" />
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblStatus" Text="Status :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2-no-search">
                                                <asp:ListItem Text="For Review" Value="1" />
                                                <asp:ListItem Text="Ready To Sync With Tablet" Value="2" />
                                            </asp:DropDownList>
                                            <asp:Label ID="lblStatusValue" runat="server" CssClass="form-label" Visible="false" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblRecordCount" Text="Num of Uploaded Records :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblCountValue" CssClass="form-label" runat="server" />
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblUploadedBy" Text="Uploaded By :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblUploadedByValue" CssClass="form-label" runat="server" />
                                    </div>
                                </div>


                                <div class="row align-items-center">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblSyncToTabletDate" Text="SyncToTabletDate :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblSyncToTabletDateValue" CssClass="form-label" runat="server" />
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblSyncToTabletUser" Text="Sync To Tablet User :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblSyncToTabletUserValue" CssClass="form-label" runat="server" />
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblSyncFromTabletDate" Text="Sync From Tablet Date :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblSyncFromTabletDateValue" CssClass="form-label" runat="server" />
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblSyncFromTabletUSer" Text="Sync From Tablet User :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblSyncFromTabletUserValue" CssClass="form-label" runat="server" />
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblSyncFileLocation" Text="Sync File Location :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblSyncFileLocationValue" CssClass="form-label" runat="server" />
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-4">
                                        <asp:LinkButton ID="lbtnSave" CssClass="btn btn-primary" Font-Underline="false" Text="Update"
                                            runat="server" OnClick="lbtnSave_Click" />
                                        <asp:LinkButton ID="lbtnClose" Width="73px" Font-Underline="false" Text="Close" CssClass="btn btn-secondary"
                                            runat="server" PostBackUrl="~/ListOfFiles.aspx" />
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
     <!-- Back to top -->
 <a href="#top" id="back-to-top"><i class="fas fa-angle-up "></i></a>

 <!-- JQuery min js -->
 <script src="assets/plugins/jquery/jquery.min.js"></script>


 <!--Bootstrap.min js-->
 <%--<script src="assets/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>--%>
 <script src="assets/plugins/select2/js/select2.min.js"></script>
</asp:Content>
