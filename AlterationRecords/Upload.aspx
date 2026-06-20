<%@ Page Title="" Language="C#" MasterPageFile="~/AlterationRecords.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="AlterationRecords.Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="">
        <div class="main-container container-fluid">
            <!--Page Header-->
            <div class="page-header d-sm-flex d-block">
                <h4 class="page-title"><i class="si si-docs me-1"></i>Upload File</h4>

            </div>
            <!--page header-->
            <div class="row">
                <div class="col-md-12 col-lg-12">
                    <div class="card">
                        <div class="card-header d-flex">
                            
                        </div>
                        <div class="card-body">
                            <div class="row row-sm">
                                <div class="col-md-5 col-lg-2">
                                    <label for="uploadfile" class="form-label">Upload File :</label>
                                </div>
                                <div class="col-md-5 col-lg-10">
                                    <div class="col-sm-12 ">
                                        <%--<input type="file" id="uploadfile" class="dropify" data-height="200" />--%>
                                        <asp:FileUpload ID="fuSchools" runat="server" CssClass="dropify" />
                                        <asp:RequiredFieldValidator ID="rfvFU" runat="server" ControlToValidate="fuSchools"
                                            CssClass="validationfont" ErrorMessage="Please select a File" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnWarning" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row row-sm my-2">

                                <div class="col-md-5 col-lg-2">
                                    <label for="firstname" class="form-label">School :</label>

                                </div>
                                <div class="col-md-5 col-lg-5">
                                    <asp:DropDownList ID="ddlSchool" runat="server" AutoPostBack="false" Class="form-control select2-no-search">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlSchool" ID="RequiredFieldValidator1"
                                        CssClass="validationfont" ErrorMessage="Please select a School" InitialValue="-1"
                                        runat="server" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <%--<select name="somename" class="form-control SlectBox" onclick="console.log($(this).val())" onchange="console.log('change is firing')">
                        <option title="eg1 is a car" value="eg1">.</option>

                    </select>--%>
                                </div>
                            </div>

                            <div class="row row-sm">
                                <div class="col-md-5 col-lg-2 ">
                                    <label for="firstname" class="form-label">Description :</label>

                                </div>
                                <div class="col-md-5 col-lg-5">
                                    <div class="row">
                                        <div class="col-lg mg-t-10">
                                            <%--<textarea class="form-control" rows="7"></textarea>--%>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtDescription" TextMode="MultiLine" MaxLength="1000" Height="80" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="row row-sm my-2">
                                <div class="col-md-5 col-lg-2">
                                </div>
                                <div class="col-md-5 col-lg-4 ">
                                    <div class="row">
                                        <div class="btn-list">
                                            <asp:LinkButton CssClass="btn btn-primary" ID="lbtnSave" runat="server" Text="Upload" OnClick="btnUpload_Click"
                                                OnClientClick="return confirmation();"></asp:LinkButton>
                                            <asp:HiddenField ID="hdnFileName" runat="server" />
                                            <asp:Literal ID="ltrlScript" runat="server" />
                                            <%--<button class="btn btn-primary">Upload</button>
                            <button class="btn btn-secondary">Cancel</button>--%>
                                            <asp:LinkButton ID="lbtnClose" Text="Close" runat="server" CssClass="btn btn-secondary" OnClick="lbtnClose_Click"
                                                CausesValidation="false" />

                                            <asp:LinkButton ID="lbtnUploadAfterWarning" runat="server" CssClass="btn btn-secondary" Text="Close" OnClick="lbtnUploadAfterWarning_Click"
                                                CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
   
    	<!-- JQuery min js -->
	<script src="../assets/plugins/jquery/jquery.min.js"></script>

    <!--Internal Fileuploads js-->
    <script src="assets/plugins/fileuploads/js/fileupload.js"></script>
    <script src="assets/plugins/fileuploads/js/file-upload.js"></script>

    <!--Internal Fancy uploader js-->
    <script src="assets/plugins/fancyuploder/jquery.ui.widget.js"></script>
    <script src="assets/plugins/fancyuploder/jquery.fileupload.js"></script>
    <script src="assets/plugins/fancyuploder/jquery.iframe-transport.js"></script>
    <script src="assets/plugins/fancyuploder/jquery.fancy-fileupload.js"></script>
    <%--<script src="assets/plugins/fancyuploder/fancy-uploader.js"></script>--%>


    
		<!-- Internal Select2.min js -->
		<script src="../assets/plugins/select2/js/select2.min.js"></script>

		<!-- Flatpickr js -->
		<script src="../assets/plugins/flatpickr/flatpickr.js"></script>
		<script src="../assets/js/flatpickr.js"></script>

	

		<!-- Internal form-elements js -->
		<%--<script src="../assets/js/form-elements.js"></script>--%>

  <%--  <!--Internal  Form-elements js-->
    <script src="assets/js/advanced-form-elements.js"></script>--%>

    <script language="javascript" type="text/javascript">
        /*setTimeout("$('#anchFiles').click();", 100);*/
        document.getElementById("ContentPlaceHolder1_lbtnUploadAfterWarning").style.visibility = "hidden";
    </script>
    <script language="javascript" type="text/javascript">
        function confirmation() {
            var s = "k";
            s = document.getElementById("ContentPlaceHolder1_fuSchools").value.split("\\");

            var n = document.getElementById("ContentPlaceHolder1_fuSchools").value.split(",");
            var hdn2 = document.getElementById("ContentPlaceHolder1_hdnFileName").value;
            var hdn = hdn2.split(",");
            var val = n[0].split("\\");
            for (var i = 0; i < hdn.length; i++) {
                if (hdn[i] == val[val.length - 1] && document.getElementById("ContentPlaceHolder1_fuSchools").value != "") {
                    /*if (!confirm("File with same name already exists do you want to continue?"))
                        return false;
                    else
                        return true;*/
                    alert("File with same name already exists.");
                    return false;
                }
            }
        }


        function UploadWarning(msg) {
            document.getElementById("ContentPlaceHolder1_hdnWarning").value = "no";
            if (confirm(msg)) {
                document.getElementById("ContentPlaceHolder1_lbtnUploadAfterWarning").click();
            } else {
                document.getElementById("ContentPlaceHolder1_hdnWarning").value = "";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SonabendContent" runat="server">
</asp:Content>
