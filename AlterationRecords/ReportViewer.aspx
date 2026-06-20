<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="AlterationRecords.ReportViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>-->
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        .popup-contnt table {
            width: 20px !important;
            margin: 0px 0px 0px !important;
        }

        .popup-wrapper {
            width: 0%;
            padding: 0px;
        }

        div#Box1 {
            clip: rect(0px 0px 0px 0px) !important;
        }
    </style>
    <title></title>
    <!--Bootstrap.min css-->
    <link href="assets/css/style.css" rel="stylesheet" />
</head>
<body>
    <div id="Div1" class="popup-wrapper" runat="server">
        <div class="popup-contnt heightFix" runat="server" style="width: 1340px ! important">
            <form id="frmReportViewer" runat="server">
                <asp:ScriptManager runat="server">
                    <Scripts>
                        <%--To learn more about bundling scripts in ScriptManager see https://go.microsoft.com/fwlink/?LinkID=301884 --%>
                        <%--Framework Scripts--%>
                        <asp:ScriptReference Name="MsAjaxBundle" />
                        <asp:ScriptReference Name="jquery" />
                        <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                        <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                        <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                        <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                        <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                        <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                        <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                        <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                        <asp:ScriptReference Name="WebFormsBundle" />
                        <%--Site Scripts--%>
                    </Scripts>
                </asp:ScriptManager>
                <div class="popup-title" id="popupTitle" runat="server">
                    <%--style="width: 1337px ! important;"--%>
                    <asp:Label ID="rptName" CssClass="page-title" Text="" runat="server" />
                    <div class="row" style="display: inline-flex !important; float: right">
                        <label  style="padding: 5px;">Current Page : </label>
                        <asp:Label ID="lblCurrentPage" runat="server" style="padding: 5px;"></asp:Label>
                        <label style="padding: 5px;">Total Pages :  </label>
                        <asp:Label ID="lblTotPages" runat="server" style="padding: 5px;"></asp:Label>                        
                        <asp:HiddenField ID="hid_current_page" runat="server" />
                        <div class="col-2 text-center" style="margin-left: 5px; margin-right: 5px">
                            <asp:Button ID="btnFirst" runat="server" OnClick="btnFirst_Click" Text="First" CssClass="btn btn-primary" />
                        </div>
                        <div class="col-2 text-center" style="margin-left: 5px; margin-right: 5px">
                            <asp:Button ID="btnPrew" runat="server" OnClick="btnPrew_Click" Text="Previous" CssClass="btn btn-primary" />
                        </div>
                        <div class="col-2 text-center" style="margin-left: 5px; margin-right: 5px">
                            <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="Next" CssClass="btn btn-primary" />
                        </div>
                        <div class="col-2 text-center" style="margin-left: 5px; margin-right: 5px">
                            <asp:Button ID="btnLats" runat="server" OnClick="btnLats_Click" Text="Last" CssClass="btn btn-primary" />
                        </div>
                    </div>
                </div>
                <div>
                    <CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="true" ShowAllPageIds="true" HasGotoPageButton="False"
                        HasPageNavigationButtons="False" EnableDatabaseLogonPrompt="false" EnableParameterPrompt="false" HasCrystalLogo="False"
                        BorderWidth="0px" Width="100%" ReuseParameterValuesOnRefresh="true" Height="100px" ToolPanelWidth="100%" />
                </div>
            </form>
        </div>
    </div>
</body>
</html>
