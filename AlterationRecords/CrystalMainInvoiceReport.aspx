<%@ Page Title="" Language="C#" MasterPageFile="~/AlterationRecords.Master" AutoEventWireup="true" CodeBehind="CrystalMainInvoiceReport.aspx.cs" Inherits="AlterationRecords.CrystalMainInvoiceReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SonabendContent" runat="server">
    <%--<asp:ScriptManager ID="smRI" runat="server" AsyncPostBackTimeout="600"></asp:ScriptManager>--%>
    <div class="">
        <div class="main-container container-fluid">
            <!--Page Header-->
            <div class="page-header d-sm-flex d-block">
                <h4 class="page-title"><i class="si si-docs me-1"></i>Alterations Per Fitting Report</h4>

            </div>
            <!--page header-->
            <div class="row">
                <div class="col-md-12 col-lg-12">
                    <div class="card">
                        <div class="card-header d-flex">
                        </div>


                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-5 col-lg-2">
                                            <label for="date" id="lblFromDate" class="form-label text-end">From Date :</label>
                                        </div>
                                        <div class="col-md-5 col-lg-3">
                                            <div class="input-group">
                                                <div class="input-group-text">
                                                    <i class="ri-calendar-line fs-17 lh-9 op-6"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="Server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                                <%--<asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFromDate"
                                                    Format="MM/dd/yyyy">
                                                </asp:CalendarExtender>--%>
                                                <%--<br />
                                                <asp:RequiredFieldValidator ID="rfvFromDate" ErrorMessage="  Please Select From Date"
                                                    CssClass="validationfont" ControlToValidate="txtFromDate" runat="server" Display="Dynamic" />--%>
                                            </div>
                                            <div class="input-group">
                                                <label class="form-label invalid-feedback fs-12" id="lblFromdateError"></label>
                                            </div>
                                        </div>
                                        <div class="col-md-5 col-lg-2">
                                            <asp:Label ID="lblToDate" Text="To Date :" runat="server" CssClass="form-label text-end" />
                                        </div>
                                        <div class="col-md-5 col-lg-3">
                                            <div class="input-group">
                                                <div class="input-group-text">
                                                    <i class="ri-calendar-line fs-17 lh-9 op-6"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="Server" CssClass="form-control"></asp:TextBox>
                                                <%--<asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                                    Animated="true" FirstDayOfWeek="Sunday" PopupPosition="BottomLeft" Format="MM/dd/yyyy">
                                                </asp:CalendarExtender>--%>
                                                <%--<br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="  Please Select To Date"
                                                    CssClass="validationfont" ControlToValidate="txtToDate" runat="server" Display="Dynamic" />
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtToDate"
                                                    Display="Dynamic" CultureInvariantValues="true" ControlToCompare="txtfromDate"
                                                    Operator="GreaterThanEqual" Type="Date" ErrorMessage="  To Date cannot be prior than From Date."
                                                    CssClass="validationfont"></asp:CompareValidator>--%>
                                            </div>
                                            <div class="input-group">
                                                <label class="form-label invalid-feedback fs-12" id="lblTodateError"></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row my-4">
                                        <div class="col-md-5 col-lg-2">
                                            <asp:Label ID="lblSchool" Text="School :" class="form-label text-end" runat="server" />
                                        </div>
                                        <div class="col-md-5 col-lg-3">
                                            <asp:ListBox ID="lstSchool" runat="server" class="form-control" SelectionMode="Multiple" OnSelectedIndexChanged="lstSchools_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Text="ALL" Value=" " />
                                            </asp:ListBox>
                                            <%--<select name="School" id="ddlSchool" class="form-control SlectBox" onclick="console.log($(this).val())" onchange="console.log('change is firing')">
                                        <option title="eg1 is a car" value="eg1">.</option>
                                        <option value="saab"></option>

                                    </select>--%>
                                        </div>
                                        <div class="col-md-5 col-lg-2">
                                            <asp:Label ID="lblStatus" Text="Status :" runat="server" class="form-label text-end" />
                                        </div>
                                        <div class="col-md-5 col-lg-3">
                                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control select2-no-search">
                                                <asp:ListItem Text="ALL" Value="2" />
                                                <asp:ListItem Text="Active" Value="1" />
                                                <asp:ListItem Text="Reserve" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<select name="Status" id="ddlStatus" class="form-control SlectBox" onclick="console.log($(this).val())" onchange="console.log('change is firing')">
                                        <option title="eg1 is a car" value="eg1">.</option>
                                        <option value="saab"></option>

                                    </select>--%>
                                        </div>
                                    </div>

                                    <div class="row my-4">
                                        <div class="col-md-5 col-lg-2">
                                            <asp:Label ID="lblPlatoon" Text="Platoon :" runat="server" class="form-label text-end" />
                                        </div>
                                        <div class="col-md-5 col-lg-3">
                                            <asp:ListBox ID="lstPlatoon" runat="server" SelectionMode="Multiple" class="form-control" Rows="10"></asp:ListBox>
                                            <%--<select name="Platoon" id="ddlPlatoon" class="form-control SlectBox" onclick="console.log($(this).val())" onchange="console.log('change is firing')">
                                        <option title="eg1 is a car" value="eg1">.</option>
                                        <option value="saab"></option>

                                    </select>--%>
                                        </div>
                                        <div class="col-md-5 col-lg-2">
                                            <asp:Label ID="lblAccelerated" Text="Accelerated :" runat="server" class="form-label text-end" />
                                            <%--<label for="ddlAccelerated" id="lblAccelerated" class="form-label text-end">Accelerated :</label>--%>
                                        </div>
                                        <div class="col-md-5 col-lg-3">
                                            <asp:DropDownList ID="ddlAccel" runat="server" class="form-control select2-no-search">
                                                <asp:ListItem Text="ALL" Value="2" />
                                                <asp:ListItem Text="Normal" Value="0" />
                                                <asp:ListItem Text="Accelerated" Value="1" />
                                            </asp:DropDownList>
                                            <%--<select name="Accelerated" id="ddlAccelerated" class="form-control SlectBox" onclick="console.log($(this).val())" onchange="console.log('change is firing')">
                                        <option title="eg1 is a car" value="eg1">.</option>
                                        <option value="saab"></option>

                                    </select>--%>
                                        </div>
                                    </div>


                                    <div class="row  my-4">
                                        <div class="col-md-5 col-lg-2">
                                        </div>
                                        <div class="col-md-5 col-lg-3">
                                        </div>
                                        <div class="col-md-5 col-lg-2">
                                        </div>
                                        <div class="col-md-5 col-lg-3">
                                            <div class="btn-list">
                                                <asp:LinkButton ID="lbtnSubmit" Text="View" CssClass="btn btn-secondary  mb-1 col-6" runat="server" OnClientClick="return CheckFromDateTodate()" OnClick="btnSubmit_Click"></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;
                                            <%--<asp:LinkButton ID="lbtnBack" OnClick="lbtnBack_Click" Text="Close" CssClass="btn btn-secondary"
                                                CausesValidation="false" runat="server" />--%>
                                                <%--<asp:LinkButton ID="lbtnExport" Width="73px" CssClass="export" runat="server" OnClick="lbtnExport_Click"
                                                    Visible="false" />--%>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row  my-4">
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblFilter" runat="server" CssClass="form-label text-end" />
                                        </div>
                                        <div class="col-md-9 col-lg-9">
                                            <asp:Label ID="lblQueryTitle" runat="server" CssClass="form-label" />
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- JQuery min js -->
    <script src="assets/plugins/jquery/jquery.min.js"></script>

    <!--Internal Fileuploads js-->
<script src="assets/plugins/fileuploads/js/fileupload.js"></script>
<%--<script src="assets/plugins/fileuploads/js/file-upload.js"></script>--%>

<!--Internal Fancy uploader js-->
<script src="assets/plugins/fancyuploder/jquery.ui.widget.js"></script>
<script src="assets/plugins/fancyuploder/jquery.fileupload.js"></script>
<script src="assets/plugins/fancyuploder/jquery.iframe-transport.js"></script>
<script src="assets/plugins/fancyuploder/jquery.fancy-fileupload.js"></script>
<%--<script src="assets/plugins/fancyuploder/fancy-uploader.js"></script>--%>

<!--Internal Sumoselect js-->
<%--<script src="assets/plugins/sumoselect/jquery.sumoselect.js"></script>--%>

<!-- Internal TelephoneInput js-->
<%--<script src="assets/plugins/telephoneinput/telephoneinput.js"></script>--%>
<%--<script src="assets/plugins/telephoneinput/inttelephoneinput.js"></script>--%>

<!--Internal  Form-elements js-->
<%--<script src="assets/js/advanced-form-elements.js"></script>--%>

<!--Internal  jquery.maskedinput js -->
<%--<script src="assets/plugins/jquery.maskedinput/jquery.maskedinput.js"></script>--%>
<!-- Flatpickr js -->
<script src="assets/plugins/flatpickr/flatpickr.js"></script>
<script src="assets/js/flatpickr.js"></script>
    <script language="javascript" type="text/javascript">
        //$(document).ready(function () {
        //    flatpickr("#SonabendContent_txtFromDate", {
        //    });
        //    flatpickr("#SonabendContent_txtToDate", {
        //    });

        //});
        flatpickr("#SonabendContent_txtFromDate", {
        });
        flatpickr("#SonabendContent_txtToDate", {
        });
        function CheckFromDateTodate() {
            
            $("#lblFromdateError").text("");
            $("#lblTodateError").text("");
            var txtFromDate = document.getElementById("SonabendContent_txtFromDate").value;
            if (txtFromDate == "" || txtFromDate == undefined) {
                $("#lblFromdateError").text("Please Select From Date");
                if (txtToDate == "" || txtToDate == undefined) {
                    $("#lblTodateError").text("Please Select To Date");
                }
                return false;
            }
            //else {
            //}
            var txtToDate = document.getElementById("SonabendContent_txtToDate").value;
            if (txtToDate == "" || txtToDate == undefined) {
                $("#lblTodateError").text("Please Select To Date");
                return false;
            }
            if (Date.parse($("#SonabendContent_txtToDate").val()) < Date.parse($("#SonabendContent_txtFromDate").val())) {
                $("#lblTodateError").text("To Date cannot be prior than From Date");
                return false;
            }
        }
        $("#SonabendContent_txtFromDate").change(function () {
            $("#lblFromdateError").text("");
            var txtFromDate = document.getElementById("SonabendContent_txtFromDate").value;
            if (txtFromDate == "" || txtFromDate == undefined) {
                $("#lblFromdateError").text("Please Select From Date");
                return false;
            }
        });
        $("#SonabendContent_txtToDate").change(function () {
            $("#lblTodateError").text("");
            var txtToDate = document.getElementById("SonabendContent_txtToDate").value;
            if (txtToDate == "" || txtToDate == undefined) {
                $("#lblTodateError").text("Please Select To Date");
                return false;
            }
            if (Date.parse($("#SonabendContent_txtToDate").val()) < Date.parse($("#SonabendContent_txtFromDate").val())) {
                $("#lblTodateError").text("To Date cannot be prior than From Date");
                return false;
            }
        });
        setTimeout("$('#anchCrystal').click();", 100);
        function ShowReport(reportName) {
            var url = "ReportViewer.aspx?ReportName=" + reportName;
            window.open(url);
            flatpickr("#SonabendContent_txtFromDate", {
            });
            flatpickr("#SonabendContent_txtToDate", {
            });
        }
        setTimeout("$('#anchCrystal').click();", 100);
        function ShowReport(reportName) {
            var url = "ReportViewer.aspx?ReportName=" + reportName;
            window.open(url);
            flatpickr("#SonabendContent_txtFromDate", {
            });
            flatpickr("#SonabendContent_txtToDate", {
            });
        }
        function loadDatePricker() {
            flatpickr("#SonabendContent_txtFromDate", {
            });
            flatpickr("#SonabendContent_txtToDate", {
            });
        }
    </script>
</asp:Content>
