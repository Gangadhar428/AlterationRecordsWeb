<%@ Page Title="" Language="C#" MasterPageFile="~/AlterationRecords.Master" AutoEventWireup="true" CodeBehind="PriceDefinitionScreen.aspx.cs" Inherits="AlterationRecords.PriceDefinitionScreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SonabendContent" runat="server">
    <asp:ScriptManager ID="smListOfFiles" runat="server">
    </asp:ScriptManager>
    <div class="">
        <div class="main-container container-fluid">
            <div class="page-header d-sm-flex d-block">
                <h4 class="page-title"><i class="icon ti-money"></i>Price Definition</h4>
            </div>
            <div class="row">
                <asp:UpdatePanel ID="upListOfFiles" runat="server">
                    <ContentTemplate>
                        <div class="col-md-12 col-lg-12">
                            <div class="card">
                                <div class="card-header d-flex">
                                    <div class="row" style="display: none;">
                                        <div class="col-md-5">
                                            <asp:Label ID="lblSchool" Text="Select School :" CssClass="form-label" runat="server" />
                                        </div>
                                        <div class="col-md-7">
                                            <asp:DropDownList ID="ddlSchool" runat="server" AutoPostBack="true" CssClass="form-control select2-no-search" OnSelectedIndexChanged="ddlStatus_SelectedChanged">
                                                <asp:ListItem Text="All" Value="0" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <div class="table-responsive ">
                                        <asp:GridView runat="server" ID="grdPriceDefinition" CssClass="table table-striped table-bordered" PageSize="10" AllowSorting="true"
                                            OnPageIndexChanging="grdPriceDefinition_PageIndexChanging"
                                            AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Left"
                                            OnRowDataBound="grdPriceDefinition_DataBound"
                                            OnRowCancelingEdit="grdPriceDefinition_RowCancelEdit" AutoGenerateEditButton="false"
                                            OnRowEditing="grdPriceDefinition_RowEditing" OnRowUpdating="grdPriceDefinition_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.no">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblslno" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Attribute Name">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%#Bind("AtrributeName") %>' runat="server" ID="lblName" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dress">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%#Bind("Dress") %>' runat="server" ID="lblDressID" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Effective Date">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%#Bind("EffectiveDate") %>' runat="server" ID="lblEffectiveDate" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control input-sm" Text='<%#Bind("Price") %>'></asp:TextBox>
                                                        <asp:HiddenField ID="hdnPrice" runat="server" Value='<%#Bind("AttributeCode") %>' />
                                                        <asp:HiddenField ID="hdnAttributeName" runat="server" Value='<%#Bind("AtrributeName") %>' />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%#Bind("Price") %>' runat="server" ID="lblPrice" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowEditButton="true" ButtonType="Link" HeaderText="Edit" EditText="<span class='btn  btn-def text-muted' title='Edit'><i class='ri-edit-box-line fs-16'></i></span>" UpdateText="<span title='Update' class='btn  btn-def text-muted'><i class='fe fe-check-circle'></i></span>" CancelText="<span title='Cancel' class='btn  btn-def text-muted'><i class='fe fe-x-circle'> </i></span>" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="hdnPageIndex" runat="server" Value="0" />
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
            $('#SonabendContent_grdPriceDefinition').DataTable({
                //"aLengthMenu": false,
                //"sLengthChange": false,
                language: {
                    searchPlaceholder: 'Search...',
                    sSearch: '',
                    "slengthchange": false
                },
                "fnDrawCallback": function () {
                    debugger;
                    var pageIndex = $('#SonabendContent_grdPriceDefinition').DataTable().page();
                    $('#SonabendContent_hdnPageIndex').val(pageIndex);
                }
            });
            //$('.paginate_button a[data-dt-idx="2"]').click();
            //$('#ContentPlaceHolder1_SonabendContent_length').hide();
            $('.pageIndexClick').click(function () {
                debugger;
                var pageIndex = $('#SonabendContent_grdPriceDefinition').DataTable().page();
        //debugger;
            <%--alert('<%=grdPriceDefinition.PageIndex%>');--%>
            });
        });
        function pageIndex() {
            debugger;
            var pageIndex = $('#SonabendContent_grdPriceDefinition').DataTable().page();
        }
        function reloadDatatable() {
            var pageIndex = $('#SonabendContent_hdnPageIndex').val();
            $('#SonabendContent_grdPriceDefinition').DataTable({
                language: {
                    searchPlaceholder: 'Search...',
                    sSearch: '',
                    "slengthchange": false
                }
            });
            $('.paginate_button a[data-dt-idx="' + pageIndex +'"]').click();
        }
        function reloadEditDatatable(editIndex) {
            //var count = $('#SonabendContent_grdPriceDefinition').DataTable({}).rows().count();
            $('#SonabendContent_grdPriceDefinition').DataTable({
                language: {
                    searchPlaceholder: 'Search...',
                    sSearch: '',
                    "slengthchange": false
                }
            });
        }
    </script>
</asp:Content>
