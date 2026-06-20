<%@ Page Title="" Language="C#" MasterPageFile="~/AlterationRecords.Master" AutoEventWireup="true" CodeBehind="SchoolsList.aspx.cs" Inherits="AlterationRecords.SchoolsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="">
        <div class="main-container container-fluid">
            <div class="page-header d-sm-flex d-block">
                <h4 class="page-title"><i class="fa fa-institution"></i>List Of Schools</h4>
            </div>
            <div class="row">
                <div class="col-md-12 col-lg-12">
                    <div class="card">
                        <div class="card-header d-flex">
                        </div>
                        <div class="card-body">
                            <div class="table-responsive ">
                                <asp:GridView runat="server" ID="grdSchoolList" CssClass="table table-striped table-bordered text-nowrap" AllowPaging="False"
                                    AllowSorting="False" AutoGenerateColumns="False" OnRowDataBound="grdSchoolList_DataBound"
                                    Width="100%">
                                    <Columns>
                                        <asp:BoundField HeaderText="Sl#" />
                                        <asp:BoundField HeaderText="School Name" DataField="SchoolName" />
                                        <asp:BoundField HeaderText="Description" DataField="Description" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Records
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
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

    <!--Circle-Progress js -->
    <%--<script src="assets/js/vendors/circle-progress.min.js"></script>--%>

    <!--Bootstrap.min js-->
    <%--<script src="assets/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>--%>




    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_grdSchoolList').DataTable({
                //"aLengthMenu": false,
                //"sLengthChange": false,
                language: {
                    searchPlaceholder: 'Search...',
                    sSearch: '',
                    "slengthchange": false
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SonabendContent" runat="server">
</asp:Content>
