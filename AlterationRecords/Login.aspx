<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AlterationRecords.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name='viewport' content='width=device-width, initial-scale=1.0, user-scalable=0' />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="Alteration Records - Simple light Bootstrap Nice Admin Panel Dashboard Design Responsive HTML5 Template" name="description" />
    <meta content="Spruko Technologies Private Limited" name="author" />
    <meta name="keywords" content="bootstrap panel, bootstrap admin template, dashboard template, bootstrap dashboard, dashboard design, best dashboard, html css admin template, html admin template, admin panel template, admin dashbaord template, bootstrap dashbaord template, it dashbaord, hr dashbaord, marketing dashbaord, sales dashbaord, dashboard ui, admin portal, bootstrap 4 admin template, bootstrap 4 admin" />

    <!-- Favicon -->
    <link rel="shortcut icon" type="image/x-icon" href="assets/images/brand/favicon.ico" />

    <!-- Title -->
    <title>Alteration Records - Login</title>

    <!--Bootstrap.min css-->
    <link rel="stylesheet" href="assets/plugins/bootstrap/css/bootstrap.min.css" />

    <!-- Dashboard css -->
    <link href="assets/css/style.css" rel="stylesheet" />
    <link href="assets/css/plugins.css" rel="stylesheet" />

    <!--Font Awesome css-->
    <link href="assets/plugins/fontawesome-free/css/all.css" rel="stylesheet" />

    <!---Font icons css-->
    <link href="assets/plugins/iconfonts/plugin.css" rel="stylesheet" />

    <!-- Switcher css -->
    <link href="assets/switcher/css/switcher.css" rel="stylesheet" />


</head>
<body class="auth-page">

    <div id="global-loader">
        <img src="assets/images/svgs/loader.svg" alt="loader" />
    </div>
    <div class="page custom-pages">
        <div class="container">
            <div class="row">
                <div class="col  mx-auto">
                    <form id="login_form" runat="server">
                        <div class="row justify-content-center">
                            <div class="col-md-6 col-xl-4">
                                <div class="card p-4">
                                    <div class="card-body">
                                        <div class="mx-auto mb-3 ">
                                            <img src="assets/images/users/eboardsheet-logo_BW.png" class="desktop-logo ps-4" />
                                        </div>
                                        <p class="text-center text-muted">Enter your details to Login</p>
                                        <asp:TextBox runat="server" ID="txtUserName" onkeypress="handleKeyPress(event)" placeholder="Enter Username" CssClass="form-control my-3"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="txtPassword" onkeypress="handleKeyPress(event)" TextMode="Password" CssClass="form-control my-3" placeholder="Enter Password"></asp:TextBox>
                                        <div class="row">
                                            <div class="col-12">
                                                <%--<input type="button" id="submit" name="submit" class="btn btn-primary btn-block" value="Login" onclick="Login()"/>--%>
                                                <asp:LinkButton ID="btnSave" runat="server" Text="Login" OnClientClick="return validate_login();" CssClass="btn btn-primary btn-block" OnClick="btnSave_Click"></asp:LinkButton>
                                                <asp:Literal ID="ltrlScript" runat="server"></asp:Literal>
                                            </div>
                                            <div id="fail" class="info_div" style="color:red;"></div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-12 text-end">
                                                <label class="col-lable-control">Version : 4.0</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <script src="assets/plugins/jquery/jquery.min.js"></script>

    <!--Bootstrap.min js-->
    <script src="assets/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Custom js -->
    <script src="assets/js/custom.js"></script>

    <!-- custom-switcher js -->
    <script src="assets/js/custom-switcher.js"></script>

    <script src="Scripts/WebForms/validatedata.js"></script>
</body>
<script type="text/javascript">
    $(document).ready(function () {

    });
    function handleKeyPress(e) {
        var key = e.keyCode || e.which;
        if (key == 13) {
            if (validate_login()) {
                __doPostBack('btnSave', '');
            }
        }
    }
</script>
</html>
