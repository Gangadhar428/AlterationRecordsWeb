<%@ Page Title="" Language="C#" MasterPageFile="~/AlterationRecords.Master" AutoEventWireup="true" CodeBehind="AddUsers.aspx.cs" Inherits="AlterationRecords.AddUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="">
        <div class="main-container container-fluid">
            <div class="page-header d-sm-flex d-block">
                <h4 class="page-title"><i class="fe fe-user-plus"></i><span id="spnTitle" runat="server">Add Users</span></h4>
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
                                    <div class="col-md-2">
                                        <asp:Label ID="lblUserName" Text="User Name :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server" MaxLength="50" />
                                            <asp:RequiredFieldValidator ID="rfusername" ErrorMessage="UserName Cannot be Empty"
                                                CssClass="validationfont" Display="Dynamic" ControlToValidate="txtUserName" runat="server" />
                                            <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="UserName cannot contain (')" ControlToValidate="txtUserName"
                                                CssClass="validationfont" runat="server" ValidationExpression="^[^']+$" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-2">
                                        <asp:Label Text="Password :" ID="lblPassword" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group has-validation">
                                            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" MaxLength="128" CssClass="form-control"
                                                onkeyup="passwordStrength(this.value,document.getElementById('strendth'),document.getElementById('passError'))"
                                                ToolTip='Minimum 8 characters, Should Contain 1 Upper Case Letter, Should Contain 1 Special Character, Should Contain 1 Numeric' />
                                            <span id="passError"></span><span id="strendth"></span>
                                            <asp:RequiredFieldValidator ID="rfPasword" ErrorMessage="Password Cannot be Empty"
                                                CssClass="validationfont" Display="Dynamic" ControlToValidate="txtPassword" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblRole" Text="Role :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control select2-no-search">
                                                <asp:ListItem Text="Select Role" Value="0" />
                                                <asp:ListItem Text="Sonabend" Value="1" />
                                                <asp:ListItem Text="Superivisor" Value="2" />
                                                <asp:ListItem Text="Boardman" Value="3" />
                                                <asp:ListItem Text="Staff" Value="4" />
                                                <asp:ListItem Text="Tailor" Value="5" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlRole" ID="RequiredFieldValidator1"
                                                CssClass="validationfont" ErrorMessage="Please select a Role" InitialValue="0"
                                                runat="server" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-2">
                                        <asp:Label Text="Name :" ID="lblName" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="txtName" MaxLength="255" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfName" Display="Dynamic" ErrorMessage="Name Cannot be Empty" ControlToValidate="txtName"
                                                CssClass="validationfont" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblEmail" Text="Email ID :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="255" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfEmail" ErrorMessage="Email Cannot be Empty" ControlToValidate="txtEmail"
                                                Display="Dynamic" CssClass="validationfont" runat="server" />
                                            <asp:RegularExpressionValidator ErrorMessage="Please Enter Valid Email" ControlToValidate="txtEmail"
                                                Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                runat="server" CssClass="validationfont" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblPhone" Text="Phone Number :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="txtPhone" MaxLength="20" CssClass="form-control" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="Please Enter Valid Phone Number"
                                                ControlToValidate="txtPhone" ValidationExpression="^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$"
                                                runat="server" Display="Dynamic" CssClass="validationfont" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblActive" Text="Active :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <%--<div class=" form-group">
                                        <div class="ckbox">
                                            <input type="checkbox"><span class="fs-13"></span>
                                            <asp:CheckBox ID="chkActive" runat="server"  /><span class="fs-13"></span>
                                        </div>
                                            </div>--%>
                                        <div class="form-group">
                                            <div class="custom-controls-stacked">
                                                <label class="custom-control custom-checkbox">
                                                    <input type="checkbox" class="custom-control-input" id="chkActive" runat="server" name="example-checkbox1">
                                                    <span class="custom-control-label"></span>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-4">
                                        <asp:LinkButton ID="lbtnSave" CssClass="btn btn-primary" Text="save" runat="server" OnClientClick="return passwordcheck();" OnClick="lbtnSave_Click"
                                            Visible="true" />
                                        <asp:LinkButton ID="lbtnUpdate" CssClass="btn btn-primary" Text="Update" runat="server" OnClientClick="return passwordcheck();" OnClick="lbtnUpdate_Click"
                                            Visible="false" />
                                        <asp:LinkButton ID="lbtnCancel" CssClass="btn btn-secondary" Text="Cancel" runat="server" CausesValidation="false"
                                            PostBackUrl="~/UsersList.aspx" />
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function passwordcheck() {
            var password = document.getElementById("SonabendContent_txtPassword").value;
            if (password.length <= 0)
                return true;
            if (password.length < 8) {
                alert("password should contain minimum 8 characters");
                return false;
            }
            if (!password.match(/[A-Z]/)) {
                alert("password should contain 1 upper case character");
                return false;
            }
            if (!password.match(/\d+/)) {
                alert("password should contain alteast 1 numeric");
                return false;
            }
            if (!password.match(/.[!,@,#,$,%,^,&,*,?,_,~,-,-,(,),{,},:,;,>,<,.,+,=,\',\",\[,\],`,\/]/)) {
                alert("password should contain 1 special character");
                return false;
            }
        }
        function passwordStrength(password, passwordStrength, errorField) {
            var desc = new Array();
            desc[0] = "Weak";
            desc[1] = "Weak";
            desc[2] = "Medium";
            desc[3] = "Medium";
            desc[4] = "Strong";

            var score = 0;

            if (password.length <= 0) {
                passwordStrength.innerHTML = ""; return false;
            }
            //if password bigger than 7 give 1 point
            if (password.length > 7) score++;

            //            else {
            //                passwordStrength.innerHTML = "Password should contain atleast 8 charecters";
            //                passwordStrength.className = ""; return false;
            //            }
            //if password has both lower and uppercase characters give 1 point 
            if ((password.match(/[A-Z]/))) score++;

            /*else {
            passwordStrength.innerHTML = "Password should contain alteast 1 upper case letter";
            passwordStrength.className = ""; return false;
            }*/

            //if password has at least one number give 1 point
            if (password.match(/\d+/)) score++;

            /*else {
            passwordStrength.innerHTML = "Password should contain alteast 1 numerics";
            passwordStrength.className = ""; return false;
            }*/

            //if password has at least one special caracther give 1 point
            if (password.match(/.[!,@,#,$,%,^,&,*,?,_,~,-,-,(,),{,},:,;,>,<,.,+,=,\',\",\[,\],`,\/]/)) score++;

            /* else {
            passwordStrength.innerHTML = "Password should contain alteast 1 special charecter";
            passwordStrength.className = ""; return false;
            }*/

            //if password bigger than 12 give another 1 point
            //if (password.length > 12) score++;

            passwordStrength.innerHTML = desc[score];
            passwordStrength.className = "strength" + score;
        }
    </script>
    <!-- Back to top -->
    <a href="#top" id="back-to-top"><i class="fas fa-angle-up "></i></a>

    <!-- JQuery min js -->
    <script src="assets/plugins/jquery/jquery.min.js"></script>


    <!--Bootstrap.min js-->
    <%--<script src="assets/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>--%>
    <script src="assets/plugins/select2/js/select2.min.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SonabendContent" runat="server">
</asp:Content>
