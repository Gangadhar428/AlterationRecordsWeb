<%@ Page Title="" Language="C#" MasterPageFile="~/AlterationRecords.Master" AutoEventWireup="true" CodeBehind="UsersList.aspx.cs" Inherits="AlterationRecords.UsersList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="">
        <div class="main-container container-fluid">
            <div class="page-header d-sm-flex d-block">
                <h4 class="page-title"><i class="icon icon-people"></i>List Of Users</h4>
            </div>
            <div class="row">
                <div class="col-md-12 col-lg-12">
                    <div class="card">
                        <div class="card-header d-flex">
                            <a class="card-title btn btn-primary" href="AddUsers.aspx">Add User</a>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive ">
                                <asp:GridView runat="server" ID="grdUsersList" CssClass="table table-striped table-bordered text-nowrap" AutoGenerateColumns="False" OnRowDataBound="grdUsersList_DataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="Sl# " />
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnUserName" Text='<%#DataBinder.Eval(Container.DataItem,"UserName") %>'
                                                    CssClass="btn btn-link" OnClick="lbtnUserName_Click" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"UserID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Name" DataField="Name" />
                                        <asp:BoundField HeaderText="Role" DataField="Role" />
                                        <asp:BoundField HeaderText="Active" DataField="IsActive" />
                                        <asp:BoundField HeaderText="Email Address" DataField="EmailAddress" />
                                        <asp:BoundField HeaderText="Phone Number" DataField="PhoneNumber" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Users
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="card-footer d-flex">
                            <a class="card-title btn btn-primary" href="AddUsers.aspx">Add User</a>
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


   <%-- <!--Bootstrap.min js-->
    <script src="assets/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_grdUsersList').DataTable({
                language: {
                    searchPlaceholder: 'Search...',
                    sSearch: ''
                }
            });
        });
        //$("#ContentPlaceHolder1_grdUsersList").on("click", function (e) {
        //    placeholder.unbind();
        //    $("#title").text("Default without legend");
        //    $("#description").text("The default pie chart when the legend is disabled. Since the labels would normally be outside the container, the chart is resized to fit.");
        //    $.plot(placeholder, data, {
        //        series: {
        //            pie: {
        //                show: true
        //            }
        //        },
        //        colors: ['#089e60 ', '#1396cc', '#31a550', '#e41313', '#ff9933'],
        //        legend: {
        //            show: false
        //        }
        //    });
        //});
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SonabendContent" runat="server">
</asp:Content>
