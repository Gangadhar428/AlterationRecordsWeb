<%@ Page Title="" Language="C#" MasterPageFile="~/AlterationRecords.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="AlterationRecords.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SonabendContent" runat="server">
    <div class="">
        <div class="main-container container-fluid">
            <div class="page-header d-sm-flex d-block">
                <h4 class="page-title"><i class="fe fe-user-plus"></i>Change Password</h4>
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
                                        <asp:Label ID="lblOldPassword" Text="Old Password :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtOldPassword" TextMode="Password" CssClass="form-control" runat="server" />
                                            <asp:RequiredFieldValidator ErrorMessage="Old Password Required" ControlToValidate="txtOldPassword"
                                                CssClass="validationfont" Display="Dynamic" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblNewPassword" Text="New Password :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group has-validation">
                                            <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server" onkeyup="passwordStrength(this.value,document.getElementById('strendth'),document.getElementById('passError'))"
                                                ToolTip="Minimum 8 characters, Should Contain 1 Upper Case Letter, Should Contain 1 Special Character, Should Contain 1 Numeric" CssClass="form-control" />
                                            <span id="passError"></span><span id="strendth"></span>
                                            <asp:RequiredFieldValidator ErrorMessage="New Password Required" ControlToValidate="txtNewPassword"
                                                CssClass="validationfont" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblConfirmPassword" Text="Confirm Password :" CssClass="form-label" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ErrorMessage="Confirm Password Required" ControlToValidate="txtConfirmPassword"
                                                CssClass="validationfont" Display="Dynamic" runat="server" />
                                            <asp:CompareValidator ErrorMessage="Password Mismatch" ControlToValidate="txtConfirmPassword"
                                                ControlToCompare="txtNewPassword" Display="Dynamic" CssClass="validationfont"
                                                runat="server" />
                                        </div>

                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-4">
                                        <asp:LinkButton ID="lbtnSave" CssClass="btn btn-primary" Text="Save" Font-Underline="false"
                                            runat="server" OnClientClick="return passwordcheck();" OnClick="lbtnSave_Click" />
                                        <asp:LinkButton ID="lbtnCancel" CssClass="btn btn-secondary" Text="Cancel" Font-Underline="false"
                                            runat="server" OnClick="lbtnCancel_Click" CausesValidation="false" />
                                        <%--<asp:LinkButton ID="lbtnSave" CssClass="btn btn-primary" Text="save" runat="server" OnClientClick="return passwordcheck();" OnClick="lbtnSave_Click"
                                            Visible="true" />
                                        <asp:LinkButton ID="lbtnUpdate" CssClass="btn btn-primary" Text="Update" runat="server" OnClientClick="return passwordcheck();" OnClick="lbtnUpdate_Click"
                                            Visible="false" />
                                        <asp:LinkButton ID="lbtnCancel" CssClass="btn btn-secondary" Text="Cancel" runat="server" CausesValidation="false"
                                            PostBackUrl="~/UsersList.aspx" />--%>
                                    </div>
                                    <asp:Literal ID="ltrlScript" runat="server"></asp:Literal>
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
        if (Page_ClientValidate()) {
            var password = document.getElementById("SonabendContent_txtNewPassword").value;
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
            if (!password.match(/.[!,@,#,$,%,^,&,*,?,_,~,-,-,(,),{,},:,;,>,<,.,+,=,\',\",\[,\],`,\/,|,\\]/)) {
                alert("password should contain 1 special character");
                return false;
            }
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
        //if password bigger than 8 give 1 point
        if (password.length > 7) score++;

        /*else {
        passwordStrength.innerHTML = "Password should contain atleast 8 charecters"; return false;
        }*/
        //if password has both lower and uppercase characters give 1 point 
        if ((password.match(/[A-Z]/))) score++;

        /*else {
        passwordStrength.innerHTML = "Password should contain alteast 1 upper case letter";
        passwordStrength.className = ""; return false;
        }*/

        //if password has at least one number give 1 point
        if (password.match(/\d+/)) score++;

        /* else {
        passwordStrength.innerHTML = "Password should contain alteast 1 numerics";
        passwordStrength.className = ""; return false;
        }
        */
        //if password has at least one special caracther give 1 point
        if (password.match(/.[!,@,#,$,%,^,&,*,?,_,~,-,-,(,),{,},:,;,>,<,.,+,=,\',\",\[,\],`,\/,|,\\]/)) score++;

        /* else {
        passwordStrength.innerHTML = "Password should contain alteast 1 special charecter";
        passwordStrength.className = ""; return false;
        }*/

        passwordStrength.innerHTML = desc[score];
        passwordStrength.className = "strength" + score;
    }
    </script>
</asp:Content>
