<%@ Page Title="" Language="C#" MasterPageFile="~/AlterationRecords.Master" AutoEventWireup="true" CodeBehind="ListOfFiles.aspx.cs" Inherits="AlterationRecords.ListOfFiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SonabendContent" runat="server">
    <div class="">
        <div class="main-container container-fluid">
            <div class="page-header d-sm-flex d-block">
                <h4 class="page-title"><i class="si si-docs me-1"></i>List Of Files Uploaded</h4>
            </div>
            <div class="row">
                <asp:UpdatePanel ID="upListOfFiles" runat="server">
                    <ContentTemplate>
                        <div class="col-md-12 col-lg-12">
                            <div class="card">
                                <div class="card-header d-flex">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblSchool" Text="School :" CssClass="form-label" runat="server" />
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlSchool" runat="server" CssClass="form-control select2-no-search" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedChanged">
                                                <asp:ListItem Text="All" Value="-1" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <div class="table-responsive ">
                                        <asp:GridView runat="server" ID="grdUploadFilesList" CssClass="table table-striped table-bordered" AutoGenerateColumns="false" OnRowDataBound="grdUploadFilesList_DataBound">
                                            <Columns>
                                                <asp:BoundField HeaderText="Sl#" ><headerstyle CssClass="wd-15p border-bottom-0"> </headerstyle><ItemStyle CssClass="wd-15p border-bottom-0"></ItemStyle></asp:BoundField>
                                                <asp:BoundField HeaderText="File Name" DataField="FileName" ><headerstyle CssClass="wd-15p border-bottom-0"> </headerstyle><ItemStyle CssClass="wd-15p border-bottom-0"></ItemStyle></asp:BoundField>
                                                <asp:BoundField HeaderText="Description" DataField="Description" ><headerstyle CssClass="wd-15p border-bottom-0"> </headerstyle><ItemStyle CssClass="wd-15p border-bottom-0"></ItemStyle></asp:BoundField>
                                                <asp:BoundField HeaderText="School Name" DataField="SchoolName" ><headerstyle CssClass="wd-15p border-bottom-0"> </headerstyle><ItemStyle CssClass="wd-15p border-bottom-0"></ItemStyle></asp:BoundField>
                                                <asp:BoundField HeaderText="Date Uploaded" DataField="DateUploaded" ><headerstyle CssClass="wd-15p border-bottom-0"> </headerstyle><ItemStyle CssClass="wd-15p border-bottom-0"></ItemStyle></asp:BoundField>
                                                <asp:BoundField HeaderText="File Status" DataField="FileStatus"><headerstyle CssClass="wd-15p border-bottom-0"> </headerstyle><ItemStyle CssClass="wd-15p border-bottom-0"></ItemStyle></asp:BoundField>
                                                <asp:BoundField HeaderText="Record Count" DataField="RecordCount" ><headerstyle CssClass="wd-15p border-bottom-0"> </headerstyle><ItemStyle CssClass="wd-15p border-bottom-0"></ItemStyle></asp:BoundField>
                                                <asp:BoundField HeaderText="Uploaded By" DataField="Name" ><headerstyle CssClass="wd-15p border-bottom-0"> </headerstyle><ItemStyle CssClass="wd-15p border-bottom-0"></ItemStyle></asp:BoundField>
                                                <asp:TemplateField HeaderText="Details">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDetails" Text="Details" runat="server" OnClick="lbtnDetails_Click"
                                                            CssClass="btn btn-link" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings Mode="Numeric" Position="Bottom" />
                                            <EmptyDataTemplate>
                                                No Records
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Back to top -->
    <a href="#top" id="back-to-top"><i class="fas fa-angle-up "></i></a>

    <!-- JQuery min js -->
    <script src="assets/plugins/jquery/jquery.min.js"></script>

    <!--Circle-Progress js -->
    <%--<script src="assets/js/vendors/circle-progress.min.js"></script>--%>

    <!--Bootstrap.min js-->
    <%--<script src="assets/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>--%>




    <script type="text/javascript">
        $(document).ready(function () {
            $('#SonabendContent_grdUploadFilesList').DataTable({
                //"aLengthMenu": false,
                //"sLengthChange": false,
                language: {
                    searchPlaceholder: 'Search...',
                    sSearch: '',
                    "slengthchange": false
                }
            });
            //$('#ContentPlaceHolder1_SonabendContent_length').hide();
        });
        function reloadDatatable() {
            $('#SonabendContent_grdUploadFilesList').DataTable({
                language: {
                    searchPlaceholder: 'Search...',
                    sSearch: '',
                    "slengthchange": false
                }
            });
        }
    </script>
</asp:Content>
