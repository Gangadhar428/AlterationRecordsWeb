<%@ Page Title="" Language="C#" MasterPageFile="~/AlterationRecords.Master" AutoEventWireup="true" CodeBehind="RolesList.aspx.cs" Inherits="AlterationRecords.RolesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="">
        <div class="main-container container-fluid">
            <div class="page-header d-sm-flex d-block">
                <h4 class="page-title"><i class="icon icon-people"></i>List Of Roles</h4>
            </div>
            <div class="row">
                <div class="col-md-12 col-lg-12">
                    <div class="card">
                        <div class="card-header d-flex">
                        </div>
                        <div class="card-body">
                            <div class="table-responsive ">
                                <asp:GridView runat="server" ID="grdRolesList" CssClass="table table-striped table-bordered text-nowrap" AutoGenerateColumns="False" OnRowDataBound="grdRolesList_DataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="Sl#" />
                                        <asp:BoundField HeaderText="Role" DataField="Role" />
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
    <%--<div>
        <div class="page-header d-sm-flex d-block">
            <h4 class="page-title"><i class="icon icon-people"></i>List Of Roles</h4>

        </div>
        <div class="row">
            <div class="col-md-12 col-lg-12">
                <div class="card">
                    <div class="table-responsive ">
                        <asp:GridView runat="server" ID="grdRolesList" CssClass="table table-striped table-bordered text-nowrap" AllowPaging="False"
                            AllowSorting="False" AutoGenerateColumns="False" OnRowDataBound="grdRolesList_DataBound"
                            Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="Sl#" />
                                <asp:BoundField HeaderText="Role" DataField="Role" />
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
    </div>--%>
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
            $('#ContentPlaceHolder1_grdRolesList').DataTable({
                //"aLengthMenu": false,
                //"sLengthChange": false,
                    language: {
                        searchPlaceholder: 'Search...',
                    sSearch: '',
                    "slengthchange": false
                    }
            });
            $('#ContentPlaceHolder1_grdRolesList_length').hide();
        });
    </script>
</asp:Content>
