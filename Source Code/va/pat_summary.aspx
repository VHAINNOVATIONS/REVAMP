<%@ Page Title="REVAMP Practitioner - Patient Summary" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pat_summary.aspx.cs" Inherits="pat_summary" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register TagPrefix="ucProblemList" TagName="ucProblemList" Src="~/ucProblemList.ascx" %>
<%@ Register TagPrefix="ucPatEvt" TagName="ucPatEvt" Src="~/ucPatientEvent.ascx" %>

<asp:Content ID="cphHeader" ContentPlaceHolderID="cpHeader" runat="server">
    <style>
        .radio-list input[type="radio"] {
            margin-right: 5px;
        }

        .radio-list label {
            margin-right: 5px;
            font-weight: bold;
            color: #445b93;
        }

        .graph-yaxis {
            float: left;
            position: relative;
            padding: 45px 5px 0px;
            margin-left: 10px;
            font-family: sans-serif;
        }

        .graph-yaxis .yaxis-label {
            display: block;
            position: absolute;
            right: -185px;
            top: 15px;
            -webkit-transform: rotate(-90deg);
            -moz-transform: rotate(-90deg);
        }
    </style>
    
    <!--[if IE]>
        <style>
            .graph-yaxis .yaxis-label {
                filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);
                right:-375px;
                top:-175px;
            }

            .xaxis-label{
                font-weight: bold;
            }
        </style>
    <![endif]-->

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManagerProxy ID="smpSummary" runat="server">
    </asp:ScriptManagerProxy>
    <h1 style="text-align: left; margin-bottom: 15px;">Patient Summary</h1>

    <% if (Master.IsPatientLocked)
       { %>
    <div style="display: block; margin-bottom: 10px; background-color: #f1f1f1; padding: 4px;">
        <asp:Image ID="Image2" AlternateText="Read Only Access" ImageUrl="~/Images/lock16x16.png"
            runat="server" />&nbsp; The patient's record is in use by <%= Session["PAT_LOCK_PROVIDER"].ToString() %>. 
            <a style="text-decoration: underline; color: Blue;" href='mailto:<%= Session["PAT_LOCK_EMAIL"].ToString() %>'>[Send Email]</a>
    </div>
    <%} %>

    <% //Patient Summary Tab Container %>
    <div style="margin-right: 12px;">
        <asp:TabContainer ID="tcPatSummary" runat="server">
            <asp:TabPanel ID="tpCPAPData" runat="server" HeaderText="PAP Data">
                <ContentTemplate>
                    <asp:UpdatePanel ID="upGraphicHub" runat="server">
                        <ContentTemplate>
                            <div style="margin:10px 0">
                                <asp:RadioButtonList ID="rblGraphicsMode" runat="server" CssClass="radio-list" 
                                    RepeatDirection="Horizontal" OnSelectedIndexChanged="rblGraphicsMode_OnSelectedIndexChanged" AutoPostBack="true" >
                                    <asp:ListItem Value="0" Text="Single Graphic" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="All Graphics"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <asp:MultiView ID="mvGraphicHub" runat="server">
                                <asp:View ID="vwSingleGraph" runat="server">
                                    <div>
                                        <div style="width: 200px; float: left; padding-top: 40px; margin-right: 15px;">
                                            <fieldset style="width: 190px; margin-bottom: 15px; border: 1px solid #e0e0e0; background-color: #e4f2f5;">
                                                <legend style="padding: 3px; border: 1px solid #e0e0e0; background-color: #fff; color: #445b93; font-weight: bold;">PAP Machine Data</legend>
                                                <div style="padding: 10px 5px 5px 5px;">
                                                    <table style="width: 180px;">
                                                        <tr>
                                                            <td>
                                                                <input type="button" value="Treatment Adherence" style="width: 100%;" onclick="patient.summary.renderTxAdherence();" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <input type="button" value="Apnea-Hypopnea Index" style="width: 100%;" onclick="patient.summary.renderAHI();" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <input type="button" value="Mask Leak" style="width: 100%;" onclick="patient.summary.renderMaskLeak();" />
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </div>
                                            </fieldset>
                                            <fieldset style="width: 190px; margin-bottom: 15px; border: 1px solid #e0e0e0; background-color: #e4f2f5;">
                                                <legend style="padding: 3px; border: 1px solid #e0e0e0; background-color: #fff; color: #445b93; font-weight: bold;">Questionnaire Data</legend>
                                                <div style="padding: 10px 5px 5px 5px;">
                                                    <asp:DropDownList ID="cboQuestionnaireScores" runat="server" Width="180px"></asp:DropDownList>
                                                </div>
                                            </fieldset>
                                        </div>
                                        <div style="float: left;">
                                            <h2 id="hdGraphTitle" style="margin-bottom: 10px;">Treatment Adherence</h2>
                                            <div style="margin: 8px 0;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="cboSummaryTimeWindow" runat="server">
                                                                <asp:ListItem Text="All Data" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Last Week" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Last Month" Value="2"></asp:ListItem>
                                                                <%-- 
                                                                <asp:ListItem Text="Last Quarter" Value="3"></asp:ListItem>
                                                                --%>
                                                                <asp:ListItem Text="Date Range" Value="4"></asp:ListItem>
                                                            </asp:DropDownList>

                                                        </td>
                                                        <td>
                                                            <div id="divDateRange" style="display: none; margin-left: 10px;">
                                                                From:&nbsp;<asp:TextBox ID="txtDateFrom" runat="server" MaxLength="10"></asp:TextBox>&nbsp;
                                    To:&nbsp;<asp:TextBox ID="txtDateTo" runat="server" MaxLength="10"></asp:TextBox>&nbsp;
                                                <input type="button" value="Update Graph" onclick="patient.summary.updateGraph(this);" />&nbsp;
                                                <input type="button" value="Reset Date Range" onclick="patient.summary.resetDateRange();" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:TabContainer ID="tcCPAPSummary" runat="server">
                                                <asp:TabPanel ID="tpGraphs" runat="server" HeaderText="Graphs">
                                                    <ContentTemplate>
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td style="vertical-align: middle;">
                                                                        <div>
                                                                            <div class="graph-yaxis">
                                                                                <span id="YAxisLabel" class="yaxis-label" style="width: 400px; text-align: center;"></span>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div id="divChartPlaceholder" style="width: 800px; height: 400px;"></div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td>
                                                                        <div id="XAxisLabel" class="xaxis-label" style="margin-top: 10px; text-align: center; font-family: sans-serif;">Therapy Dates</div>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:TabPanel>
                                                <asp:TabPanel ID="tpRawData" runat="server" HeaderText="Raw Data">
                                                    <ContentTemplate>
                                                        <div style="margin: 10px; padding: 10px; width: auto;">
                                                            <table id="tblCPAPRawData" border="0" class="mGrid" style="width: 600px;">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="width: 50%;">Date</th>
                                                                        <th style="width: 50%;" id="thValue">Value</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:TabPanel>
                                            </asp:TabContainer>
                                        </div>
                                        <div style="clear: both;"></div>
                                    </div>
                                </asp:View>
                                <asp:View ID="vwMultipleGraphs" runat="server">

                                    <div style="margin-left:100px;">
                                        <div style="margin-bottom: 10px;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="cboSummaryTimeWindow2" runat="server">
                                                            <asp:ListItem Text="All Data" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Last Week" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Last Month" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Date Range" Value="4"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>
                                                    <td>
                                                        <div id="divDateRange2" style="display: none; margin-left: 10px;">
                                                            From:&nbsp;<asp:TextBox ID="txtDateFrom2" runat="server" MaxLength="10"></asp:TextBox>&nbsp;
                                                            To:&nbsp;<asp:TextBox ID="txtDateTo2" runat="server" MaxLength="10"></asp:TextBox>&nbsp;
                                                            <input type="button" value="Update Graph" onclick="patient.summary.graphs.updateGraph(this);" />&nbsp;
                                                            <input type="button" value="Reset Date Range" onclick="patient.summary.graphs.resetDateRange();" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                        <h2 style="margin-top: 15px;">Treatment Adherence</h2>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="vertical-align: middle;">
                                                        <div>
                                                            <div class="graph-yaxis">
                                                                <span class="yaxis-label" style="width: 400px; text-align: center;">Hours of PAP use</span>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div id="divChartPlaceholder0" style="width: 800px; height: 400px;"></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <div class="xaxis-label" style="margin-top: 10px; text-align: center; font-family: sans-serif;">Therapy Dates</div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <h2 style="margin-top: 15px;">Apnea Hypopnea Index</h2>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="vertical-align: middle;">
                                                        <div>
                                                            <div class="graph-yaxis">
                                                                <span class="yaxis-label" style="width: 400px; text-align: center;">Events per Hour</span>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div id="divChartPlaceholder1" style="width: 800px; height: 400px;"></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <div class="xaxis-label" style="margin-top: 10px; text-align: center; font-family: sans-serif;">Therapy Dates</div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <h2 id="hdGraphTitle2" style="margin-top: 15px;">Mask Leak</h2>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="vertical-align: middle;">
                                                        <div>
                                                            <div class="graph-yaxis">
                                                                <span class="yaxis-label" style="width: 400px; text-align: center;">Liters per Minute</span>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div id="divChartPlaceholder2" style="width: 800px; height: 400px;"></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <div class="xaxis-label" style="margin-top: 10px; text-align: center; font-family: sans-serif;">Therapy Dates</div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <div id="divChartPlaceholder3" style="width: 800px; height: 400px; margin-bottom:15px;"></div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                            <asp:HiddenField ID="htxtTxAdherence" runat="server" />
                            <asp:HiddenField ID="htxtAHI" runat="server" />
                            <asp:HiddenField ID="htxtBaselineAHI" runat="server" />
                            <asp:HiddenField ID="htxtMaskLeak" runat="server" />
                            <asp:HiddenField ID="htxtLeakType" runat="server" />
                            <asp:HiddenField ID="htxtQuestionnaires" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tpTxSummary" runat="server" HeaderText="Events">
                <ContentTemplate>
                    <asp:UpdatePanel ID="upPatSummary" runat="server">
                        <ContentTemplate>
                            <div style="margin: 15px 15px 0 0;">
                                <ucPatEvt:ucPatEvt ID="ucPatEvt" runat="server" />
                            </div>

                            <asp:HiddenField ID="htxtOMProblemID" runat="server" EnableViewState="true" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
</asp:Content>
<asp:Content ID="cScripts" runat="server" ContentPlaceHolderID="cpScripts">
     <!-- Scripts needed for the graphs -->
    <!--[if lt IE 9]><script type="text/javascript" src="flot/excanvas.min.js"></script>
    <![endif]-->

    <script src="flot/jquery.flot.js"></script>
    <script src="flot/jquery.flot.time.js"></script>
    <script src="flot/jquery.flot.threshold.js"></script>


    <script src="js/patient/patient.summary.js"></script>
    <script src="js/patient/patient.summary.graphs.js"></script>

        <script type="text/javascript" defer="defer">

            var prm_summary = Sys.WebForms.PageRequestManager.getInstance();
            prm_summary.add_initializeRequest(InitializeRequest);
            prm_summary.add_endRequest(EndRequest);

            function InitializeRequest(sender, args) {
                $('div.ajax-loader').show();
            }

            function EndRequest(sender, args) {
                $('div.ajax-loader').hide();
                initializeGraphics();

                dp();
                restartSessionTimeout();
            }

            function initializeGraphics() {
                $(document).ready(function () {
                    setTimeout(function () {
                        if ($('input[name$="rblGraphicsMode"]:checked').val() == 0) {

                            var pos = $('div[id$="divChartPlaceholder"]').position();
                            //$('div[id$="divChartPlaceholder"]').width($(window).width() - pos.left - 340);
                            $('div[id$="divChartPlaceholder"]').width($(window).width() - 650);

                            patient.summary.init();
                            patient.summary.renderTxAdherence();
                        }
                        else {
                            patient.summary.graphs.init();
                            patient.summary.graphs.renderTxAdherence();
                            patient.summary.graphs.renderAHI();
                            patient.summary.graphs.renderMaskLeak();
                        }
                    }, 100);
                });
            }

            function resizeGraphs() {
                $(document).ready(function () {
                    var pos = $('div[id$="divChartPlaceholder"]').position();
                    //$('div[id$="divChartPlaceholder"]').width($(window).width() - pos.left - 340);
                    $('div[id$="divChartPlaceholder"]').width($(window).width() - 650);
                    setTimeout(function () {
                        patient.summary.plot.resize();
                        patient.summary.plot.setupGrid();
                        patient.summary.plot.draw();
                    }, 100);
                });
            }

            function dp() {
                $('.date-picker').datepicker({
                    beforeShow: function () {
                        $(document).ready(function () {
                            setTimeout(function () {
                                wp.adjustMain();
                            }, 1);
                        });
                    }
                });
            }

            $(function () {
                $('.master-save, #lnkMasterSave').remove();
                initializeGraphics();
            });

            $(window).bind({
                resize: function () {
                    resizeGraphs();
                }
            });

    </script>

</asp:Content>