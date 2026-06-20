<%@ Page Title="" Language="C#" MasterPageFile="~/AlterationRecords.Master" AutoEventWireup="true" CodeBehind="TabletClientInstallation.aspx.cs" Inherits="AlterationRecords.TabletClientInstallation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SonabendContent" runat="server">
    <div class="">
        <div class="main-container container-fluid">
            <div class="page-header d-sm-flex d-block">
                <h4 class="page-title"><i class="fa fa-file-text-o"></i>eboardsheet Tablet Client Documentation</h4>
                <ol class="breadcrumb py-0">
                    <li class="breadcrumb-item"><a href="Upload.aspx"><span><i class="fe fe-home fs-30 text-dark"></i></span></a></li>
                </ol>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header">
                            <%--<div class="card-title">eboardsheet Tablet Client Documentation</div>--%>
                        </div>
                        <div class="card-body">
                            <div class="text-wrap">
                                <div class="border border-dark radius-3 overflow-hidden">
                                    <div class="dark-nav-tab">
                                        <ul class="nav nav-tabs">
                                            <li class="nav-item"><a class="nav-link active" data-bs-toggle="tab" id="anchtabWelcome" href="#tabWelcome">Welcome</a></li>
                                            <li class="nav-item"><a class="nav-link" data-bs-toggle="tab" id="anchtabInstallation" href="#tabInstallation">Eboardsheet Tablet Client Installation</a></li>
                                            <li class="nav-item"><a class="nav-link" data-bs-toggle="tab" id="anchtabWorkFlow" href="#tabWorkFlow">WorkFlow</a></li>
                                        </ul>
                                    </div>
                                    <div class="card-body tab-content">
                                        <div class="tab-pane active show" id="tabWelcome">
                                            <div class="col-md-12">
                                                <div class="card">
                                                    <div class="card-body terms">
                                                        <h2 class="mt-0">Welcome to eboardsheet Tablet Client!</h2>
                                                        <div class="row">
                                                            <div class="col-8">
                                                                Thank you for visiting eboardsheet !
													<h3>How to Use This Site
                                                    </h3>
                                                            </div>
                                                            <div class="col-4">
                                                                <div class="text-wrap mb-3">
                                                                    <div class="toast fade show">
                                                                        <div class="toast-header bg-secondary">
                                                                            <h6 class="fs-14 mb-0 me-auto text-white">Getting Started</h6>
                                                                        </div>
                                                                        <div class="toast-body bg-secondary-transparent">
                                                                            <a onclick="return activetabInstallation()"  class="btn btn-link p-0">Install Eboardsheet tablet client</a>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <p class="terms-description">The purpose of this site is to provide you with guidelines for installing eboardsheet tablet client application on your local machine and other related functionalities..</p>

                                                        <h6 class="fs-15 fw-bold">We support issues related to:</h6>
                                                        <ul class="list-unstyled mb-4">
                                                            <li class="mb-0 fs-14 text-dark"><span><i class="fas fa-angle-double-right me-2 text-dark"></i></span>Questions and Queries related to installing pre-requisites </li>
                                                            <li class="mb-0 fs-14 text-dark"><span><i class="fas fa-angle-double-right me-2 text-dark"></i></span>Configuring eboardsheet(Sonabend) client application on your tablet</li>
                                                            <li class="mb-0 fs-14 text-dark"><span><i class="fas fa-angle-double-right me-2 text-dark"></i></span>How to Sychronize data from server application to client application and vice versa...</li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="tabInstallation">
                                            <div class="card">
                                                <div class="card-body terms">
                                                    <h2 class="mt-0">eboardsheet Tablet Client Setup Overview</h2>
                                                    <div class="text-wrap">
                                                        <p class="terms-description">Installing eboardsheet tablet client is easy with our detailed instructions:<a class="btn btn-link fs-15 p-0" href="/TabletClient/Prerequisites.zip">Prerequisites!</a> After downloading the pre-requisites,extract the contents of the zip to "system drive":\Prerequisites folder..</p>
                                                        <p class="terms-description">Navigate to "system drive":\Prerequisites\DotNetFX40Client\ folder and run the executable "dotNetFx40_Client_x86_x64.exe" to install Dotnet Framework 4.0 client if not already installed.</p>
                                                        <p class="terms-description">Once Dotnet Framework 4.0 Client is installed, navigate to "system drive":\Prerequisites\SQL Server Compact Edition\4.0 sp2\ and run SSCERuntime_x64-ENU.exe For 64bit systems or SSCERuntime_x86-ENU.exe For 32bit systems.</p>
                                                        <p class="terms-description">Once SQL Server Compact Edition is Installed, navigate to "system drive":\Prerequisites\WebPlatformInstaller 3.0\ and run WebPlatformInstaller_3_10_amd64_en-US.msi For 64bit systems or WebPlatformInstaller_3_10_x86_en-US.msi For 32bit systems.</p>
                                                        <p class="terms-description">Once the above pre-requisites are downloaded and installed, please download eboardsheet.zip (version 2.0 dated Jan 01, 2013) from <a class="btn btn-link fs-15 p-0" href="/TabletClient/eboardsheet.zip">eboardsheet(Sonabend) tablet client application files!</a> and follow the steps given in the below section to run the application.</p>
                                                        <div class="col-md-12 col-sm-12 text-start">
                                                            Please Click here for <a class="btn btn-link fs-17 p-0" href="TabletClientReleaseNotes.aspx">Release Notes:</a>
                                                        </div>
                                                        <h4 class="mt-4">Downloading Invidual Setup files based on your system configuration</h4>
                                                        <p class="terms-description">This section provides links for downloading individual files required for configuring eboardsheet on your system based on the system configuration</p>
                                                        <p class="terms-description">In order to install eboardsheet(Sonabend) tablet client, install the following items:</p>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-7">
                                                            <ul class="list-unstyled my-4">
                                                                <li class="my-0 py-0 fs-14 text-dark"><span><i class="fas fa fa-hand-o-right text-dark"></i></span>.NET Framework 4.0</li>
                                                                <li class="my-0 py-0 fs-14 text-dark"><span><i class="fas fa fa-hand-o-right text-dark"></i></span>SQL Server Compact Edition</li>
                                                                <li class="my-0 py-0 fs-14 text-dark"><span><i class="fas fa fa-hand-o-right text-dark"></i></span>Web Platform Installer 3.0</li>
                                                                <li class="my-0 py-0 fs-14 text-dark"><span><i class="fas fa fa-hand-o-right text-dark"></i></span>Unzipping and executing eboardsheet(Sonabend) Tablet Client Application files</li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-5">
                                                            <div class="text-wrap mb-3">
                                                                <div class="toast fade show">
                                                                    <div class="toast-header bg-secondary">
                                                                        <h6 class="fs-14 mb-0 me-auto text-white">Downloads</h6>
                                                                    </div>
                                                                    <div class="toast-body bg-secondary-transparent fs-12">
                                                                        <%-- <a onclick="return activetabInstallation()" class="btn btn-link">Download</a>--%>
                                                                        <span><a  class="btn btn-link p-0" href="/TabletClient/Prerequisites/DotNetFX40Client/dotNetFx40_Client_x86_x64.exe">Download</a></span> .NET Framework 4.0<br />
                                                                        <span><a  class="btn btn-link p-0" href="/TabletClient/Prerequisites/SQL Server Compact Edition/4.0 sp2/SSCERuntime_x86-ENU.exe">Download</a></span> SQL Server Compact Edition for 32 bit<br />
                                                                        <span><a  class="btn btn-link p-0" href="/TabletClient/Prerequisites/SQL Server Compact Edition/4.0 sp2/SSCERuntime_x64-ENU.exe">Download</a></span> SQL Server Compact Edition for 64 bit<br />
                                                                        <span><a  class="btn btn-link p-0" href="/TabletClient/Prerequisites/WebPlatformInstaller 3.0/WebPlatformInstaller_3_10_x86_en-US.msi">Download</a></span> Web Platform Installer 3.0 for 32 bit<br />
                                                                        <span><a  class="btn btn-link p-0" href="/TabletClient/Prerequisites/WebPlatformInstaller 3.0/WebPlatformInstaller_3_10_amd64_en-US.msi">Download</a></span> Web Platform Installer 3.0 for 64 bit<br />
                                                                        <span><a  class="btn btn-link p-0" href="/TabletClient/eboardsheet.zip">Download</a></span> eboardsheet tablet client Application Files<br />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="text">
                                                                <p class="fw-bold">
                                                                    For Running eboardsheet (Sonabend) Tablet Client Application 
													Download eboardsheet.zip for the link provided under downloads
													Unzip the contents of eboardsheet.zip to any local drive
													Run sonabend.exe
													Provide User Name and Password provided to you by eboardsheet.com
                                                                </p>
                                                                <h5>Website for eboardsheet</h5>
                                                                <div class="col-md-12 col-sm-12 text-start">
                                                                    URL of Website:<a class="btn btn-link p-0" href="http://eboardsheet.com"> www.eboardsheet.com</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="tabWorkFlow">
                                            <div class="col-md-12">
                                                <div class="card">
                                                    <div class="card-body terms">
                                                        <h2 class="mt-0">Work Flow of eBoardSheet</h2>
                                                        <div class="text-wrap">
                                                            <p class="terms-description">Admin uploads a file regarding recruit details in server module. Once the file status is changed as 'ready to sync'. File can be downloaded to tablet by clicking 'Sync To Tablet' button on tablet.</p>
                                                            <p class="terms-description">Once the syncronization is done tablet user can search the recruit by recruit's SSN# and can enter the values of measurement attributes according to requirement.</p>
                                                            <p class="terms-description">Once entering data completed by clicking on submit the details will save in the client database and when "Sync To Server" button is clicked the modified data will be uploaded back to server.</p>
                                                            <p class="terms-description">Tablet user can also verify sessions by clicking on 'Verify' on client application</p>
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
                </div>
            </div>
        </div>
    </div>
    <!-- Back to top -->
    <a href="#top" id="back-to-top"><i class="fas fa-angle-up "></i></a>

    <!-- JQuery min js -->
    <script src="assets/plugins/jquery/jquery.min.js"></script>

    <script type="text/javascript">
        function activetabInstallation() {
            $("#tabWelcome").removeClass();
            $("#tabWorkFlow").removeClass();
            $("#tabInstallation").addClass("tab-pane active show");
            $("#tabWelcome").addClass("tab-pane");
            $("#tabWorkFlow").addClass("tab-pane");
            $("#anchtabWelcome").removeClass();
            $("#anchtabWorkFlow").removeClass();
            $("#anchtabInstallation").addClass("nav-link active");
            $("#anchtabWelcome").addClass("nav-link");
            $("#anchtabWorkFlow").addClass("nav-link");
            //nav-link active
        }
    </script>
</asp:Content>
