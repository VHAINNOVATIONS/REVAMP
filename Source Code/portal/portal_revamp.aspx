<%@ Page Title="REVAMP Portal - Treatment Results" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="portal_revamp.aspx.cs" Inherits="portal_revamp" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="contentHeader" ContentPlaceHolderID="cpHeader" runat="server">
    
    <style>
        .graph-yaxis {
            /*background-color:#987;*/
            float:left;
            position:relative;
            padding:45px 5px 0px;
            margin-left:10px;
            font-family: sans-serif;
        }

        .graph-yaxis .yaxis-label {
            display:block;
            position:absolute;
            right:-185px;
            top:15px;
            -webkit-transform: rotate(-90deg);
            -moz-transform: rotate(-90deg);
        }

        .graph-info {
            display: none; 
            width: 720px; 
            margin: 0 auto; 
            padding: 5px; 
            border: 1px solid #e1e1e1; 
            background-color: #f2f0d0; 
            line-height: 155%;
        }

        .graph-info p {
            color: #000;
            line-height: 155%;
        }

    </style>

    <!--[if IE]>
        <style>
            .graph-yaxis .yaxis-label {
                filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);
                right:-375px;
                top:-175px;
            }

            #XAxisLabel{
                font-weight: bold;
            }
        </style>
    <![endif]-->
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <% if (Master.APPMaster.UserType != (long)SUATUserType.PATIENT)
       {
           Master.LogOff();
       }
    %>

    <div>
        <div style="width: 200px; float: left; margin-right:15px;">
            <fieldset style="width: 190px; margin-bottom: 15px; border:1px solid #e0e0e0; background-color:#e4f2f5;">
                <legend style="padding:3px; border:1px solid #e0e0e0; background-color:#fff; color:#445b93; font-weight: bold;">PAP Machine Data</legend>
                <div style="padding: 10px 5px 5px 5px;">
                    <table style="width: 180px;">
                        <tr>
                            <td>
                                <a class="button-yellow" style="width:135px; margin-bottom:10px; " href="javascript:void(0);" onclick="patient.summary.renderTxAdherence();">How much I use <br/>the machine</a>
                                <%--<input type="button" value="How much I use <br/>the machine" style="width: 100%;" onclick="patient.summary.renderTxAdherence();" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a class="button-yellow" style="width:135px; margin-bottom:10px; " href="javascript:void(0);" onclick="patient.summary.renderAHI();">How much my breathing <br/>has improved</a>
                                <%--<input type="button" value="How much my breathing <br/>has improved" style="width: 100%;" onclick="patient.summary.renderAHI();" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a class="button-yellow" style="width:135px; margin-bottom:10px; " href="javascript:void(0);" onclick="patient.summary.renderMaskLeak();">How much air leaks <br/>around the mask</a>
                                <%--<input type="button" value="How much air leaks <br/>around the mask" style="width: 100%;" onclick="patient.summary.renderMaskLeak();" />--%>
                            </td>
                        </tr>
                    </table>

                </div>
            </fieldset>
            <fieldset style="display: none; width: 190px; margin-bottom: 15px; border:1px solid #e0e0e0; background-color:#e4f2f5; ">
                <legend style="padding:3px; border:1px solid #e0e0e0; background-color:#fff; color:#445b93; font-weight: bold;">Questionnaire Data</legend>
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
<%--            <asp:TabContainer ID="tcCPAPSummary" runat="server">
                <asp:TabPanel ID="tpGraphs" runat="server" HeaderText="Treatment results">
                    <ContentTemplate>--%>
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
                                        <div id="divChartPlaceholder" style="width: 720px; height: 400px;"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <div id="XAxisLabel" style="margin-top: 10px; text-align: center; font-family: sans-serif;">Therapy Dates</div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <div style="margin:20px 0;">
                            <div id="divExplanationTxt00" class="graph-info">
                                <p>
                                     <b>How much am I using my treatment?</b>– This graph shows the how much you are using your PAP treatment. The height of each bar is the total number of hours each day that you use the treatment.  If you did not use the treatment on a particular day, there will be no bar on that day. All of the bars should be above 4 hours in order to benefit from the treatment.
                                </p>
                            </div>
                            <div id="divExplanationTxt01" class="graph-info">
                                <p>
                                     <b>How much has my breathing improved?</b>– This graph shows how well your PAP treatment is working to improve your breathing. Each bar represents a day and the height of each bar is the average number of breathing pauses and periods of shallow breathing per hour.  The first bar shows the result on your home sleep test, before you started PAP treatment. The subsequent bars show the results on your PAP treatment. Those bars should be below 10 events per hour in order to have the best response to treatment.
                                </p>
                            </div>
                            <div id="divExplanationTxt02" class="graph-info">
                                <p>
                                      <b>How much air leak is present?</b>– This graph shows the amount of air leak. Each bar represents a day and the height of each bar is the average amount of air leak.  All of the bars should be below 40 liters per minute in order have the best results. A large air leak may be due to a poor mask fit. A large air leak can also be due to air leaking from the mouth during sleep if you are using a mask that just covers the nose.
                                </p>
                            </div>
                        </div>
<%--                    </ContentTemplate>
                </asp:TabPanel>--%>
<%--                <asp:TabPanel ID="tpRawData" runat="server" HeaderText="Raw Data" Visible="false">
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
            </asp:TabContainer>--%>
        </div>
        <div style="clear: both;"></div>
    </div>
    <asp:HiddenField ID="htxtTxAdherence" runat="server" />
    <asp:HiddenField ID="htxtAHI" runat="server" />
    <asp:HiddenField ID="htxtMaskLeak" runat="server" />
    <asp:HiddenField ID="htxtQuestionnaires" runat="server" />
</asp:Content>

<asp:Content ID="cScripts" runat="server" ContentPlaceHolderID="cpScripts">
    <script src="flot/jquery.flot.js"></script>
    <script src="flot/jquery.flot.time.js"></script>
    <script src="flot/jquery.flot.threshold.js"></script>
    <script src="js/patient/patient.summary.js"></script>
    <script type="text/javascript" defer="defer">

        var prm_summary = Sys.WebForms.PageRequestManager.getInstance();
        prm_summary.add_initializeRequest(InitializeRequest);
        prm_summary.add_endRequest(EndRequest);

        function InitializeRequest(sender, args) {
            $('div.ajax-loader').show();
        }

        function EndRequest(sender, args) {
            $('div.ajax-loader').hide();

            restartSessionTimeout();
        }

        $(document).ready(function () {

            adjustHeight();

            patient.summary.init();
            patient.summary.renderTxAdherence();
        });
</script>
</asp:Content>