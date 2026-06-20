<%@ Page Title="" Language="C#" MasterPageFile="~/AlterationRecords.Master" AutoEventWireup="true" CodeBehind="TabletClientReleaseNotes.aspx.cs" Inherits="AlterationRecords.TabletClientReleaseNotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SonabendContent" runat="server">
    <div class="">
        <div class="main-container container-fluid">
            <div class="page-header d-sm-flex d-block">
                <h4 class="page-title"><i class="fa fa-file-text-o"></i>eboardsheet Tablet Client Release Notes</h4>
                <ol class="breadcrumb py-0">
                    <%--<li class="breadcrumb-item"><a href="/Files/Upload"><span><i class="fe fe-home fs-30 text-dark"></i></span></a></li>--%>
                    <li class="breadcrumb-item"><a href="Upload.aspx">Home</a></li>
                    <li class="breadcrumb-item"><a href="TabletClientInstallation.aspx">Back</a></li>
                </ol>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header">
                            <%--<div class="card-title">eboardsheet Tablet Client Documentation</div>--%>
                        </div>
                        <div class="card-body border border-dark radius-3 overflow-hidden">
                            <div class="text">
                                <ol class="list-unstyled">
                                    <li class="my-0 fw-bold">Release 2.0 dated Jan 01, 2013</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Optimization of Sync Process.</li>
                                            <li class="my-0">Optimization of Verify Process.</li>
                                            <li class="my-0">Implemented logic for saving SonabendUser and SupervisorUser while approving session.</li>
                                            <li class="my-0">Modified Career Planners MaxValues,FirstPress Values.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.32 dated Dec 05, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Fixed issue regarding user password.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.31 dated Nov 21, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Optimize sync down process.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.30 dated Nov 12, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Modified first press value for skirt in BRC, Career Recruiters, Career Planners Schools.</li>
                                        </ol>
                                    </li>

                                    <li class="my-0 fw-bold">Release 1.29 dated Nov 02, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Fixed issue regarding fit iteration of recruit measurements.</li>
                                        </ol>
                                    </li>

                                    <li class="my-0 fw-bold">Release 1.28 dated Oct 15, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Modified logic for buttons as per recent document from zac.</li>
                                            <li class="my-0">Fixed issue related to duplicate records.</li>
                                        </ol>
                                    </li>

                                    <li class="my-0 fw-bold">Release 1.27 dated Sep 27, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Modified logic for incrementing button values by 1 for all buttons that do not have automatic values.</li>
                                            <li class="my-0">Implemented logic for Platoon Report.</li>
                                            <li class="my-0">Changed Name of the button Name Tapes to Create Name Tapes in More tab.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.26 dated Aug 31, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Replaced eboardsheet with 216.224.173.31 for temporary access until eboardsheet propagation is completed.</li>
                                        </ol>
                                    </li>

                                    <li class="my-0 fw-bold">Release 1.25 dated Aug 28, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Fixed Issue related to auto population of values in cammies tab.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.24 dated Aug 27, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Fixed Issue related to auto population of values in cammies tab.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.23 dated Aug 21, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Modified Reports screen to display Session specific Reports.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.22 dated Aug 20, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Modified tablet client to display button names on warning message during submit process.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.21 dated Aug 17, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Fixed issue releated to Warning message in BRC School.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.20 dated Aug 16, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Fixed issue releated to Max Values.</li>
                                            <li class="my-0">Implemented logic for purging of old data older than an year.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.19 dated Aug 14, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Fixed issue releated to special characters in the name of the recruit on Alert Screen.</li>

                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.18 dated Aug 10, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Updated platoon number format to desired format.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.17 dated Aug 09, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Removed Reset Button functionality and replaced reset button with clear button.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.16 dated Aug 08, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Implemented logic for clear functionality.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.15 dated Aug 03, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Fixed issue on auto population of measurement values.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.14 dated July 30, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Implemented logic for saving TailorID and BoardmanID.</li>
                                            <li class="my-0">Modified the logic for saving values of seat and waist.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.13 dated July 27, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Implemented logic for showing alert on verify when a recruit has any missing counts on tabs.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.12 dated July 26, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Removed Active, Reserve selection in Add Recruit Screen other than RTR School.</li>
                                            <li class="my-0">Removed Platoon Number Text Box and Added DropDown for selecting platoon from existing platoons.</li>
                                            <li class="my-0">Removed school Selection and showing the current school in Add Recruit Screen.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.11 dated July 25, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Fixed Issue on user password update when sync to tablet.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.10 dated July 24, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Implementation of revised max values on measurements.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.9 dated July 23, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Fixed Issue on count of recruits in session on report header in reports screen.</li>
                                            <li class="my-0">Removed count of recruits on report header for More tab.</li>
                                            <li class="my-0">Implementation of revised max values on measurements.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.8 dated July 18, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Updated SideArms Measurement Attribute in BRCWomen on Blue Coat to Seams.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.7 dated July 18, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Updated MaxLength of the column PlatoonNumber in Recruit table.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.6 dated July 17, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Implemented logic for saving Measurement regarding iteration number to the server (FirstFit, SecondFit, etc.,)</li>
                                            <li class="my-0">Changed Platoon Number format, prefixing the date to Platoon Number instead of suffixing.</li>
                                            <li class="my-0">Included missing space in between selected platoons in the search criteria on reports.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.5 dated July 16, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Correction of tab names in BRCWomen School as</li>
                                            <ol class="list-style-2">
                                                <li class="my-0">Blue Coat remains Same.</li>
                                                <li class="my-0">Green Coat becomes Shirts.</li>
                                                <li class="my-0">Blue Trou becomes Skirts.</li>
                                                <li class="my-0">Green Trou becomes Slacks.</li>
                                            </ol>
                                            <li class="my-0">Removed PFC button on the Blue Coat tab for all schools other than RTR School.</li>
                                        </ol>
                                    </li>

                                    <li class="my-0 fw-bold">Release 1.4 dated July 13, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Optimization of verify process.</li>
                                            <li class="my-0">Navigational fix for Alerts Screen from drilldown.</li>
                                            <li class="my-0">Implementation of revised max values on measurements.</li>
                                        </ol>
                                    </li>

                                    <li class="my-0 fw-bold">Release 1.3 dated July 12, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Bug fix for duplicate inserts of measurements.</li>
                                            <li class="my-0">Second fit implementation.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.2 dated July 09, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Moved the Accelerated from Recruit More tab to Confirm screen.</li>
                                            <li class="my-0">Implemented two step confirmation for Accelerated flag setting for RTR School.</li>
                                        </ol>
                                    </li>
                                    <li class="my-0 fw-bold">Release 1.1 dated July 02, 2012</li>
                                    <li>
                                        <ol class="list-style-1 mt-0">
                                            <li class="my-0">Fixed accidental minimization of app when touch on top part of screen.</li>
                                            <li class="my-0">Aligned progress icon circle to the center of screen.</li>
                                            <li class="my-0">Fixed ALERTS screen issue - Clicking on an alert it takes you to a different recruit while you are working on multiple schools in the same session.</li>
                                            <li class="my-0">Fixed Issue of KDB popup on password box.</li>
                                        </ol>
                                    </li>
                                </ol>
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
</asp:Content>
