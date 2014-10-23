<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPatientSOAPP.ascx.cs"
    Inherits="ucPatientSOAPP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="ucAxesList" TagName="ucAxesList" Src="~/ucSOAP_Assessment.ascx" %>
<%@ Register TagPrefix="ucProblemList" TagName="ucProblemList" Src="~/ucProblemList.ascx" %>

<div id="divSOAPP" class="main-contents">
    <div style="padding-right: 5px;">
        
        <% if (BaseMstr.IsPatientLocked)
           { %>
        <div style="display: block; margin-bottom: 10px; background-color: #f1f1f1; padding: 4px;">
            <asp:Image ID="Image1" AlternateText="Read Only Access" ImageUrl="~/Images/lock16x16.png"
                runat="server" />&nbsp; The patient's record is in use by <%= Session["PAT_LOCK_PROVIDER"].ToString() %>. 
                <a style="text-decoration: underline; color: Blue;" href='mailto:<%= Session["PAT_LOCK_EMAIL"].ToString() %>'> [Send Email]</a>
        </div>
        <%} %>
        <div>
            <asp:UpdatePanel ID="upNoteHd" runat="server">
                <ContentTemplate>
                    <div style="margin-bottom: 15px; background: #e1e1e1; text-align: left; vertical-align: middle; padding: 3px 0 3px 10px;">

                        <span id="spModalityInfo" runat="server" style="font-weight:bold; font-size:14px; color:darkred;"></span>
                        
                &nbsp; Session time:&nbsp;<asp:TextBox ID="txtSessionTime" Width="60px" MaxLength="3" runat="server"></asp:TextBox>&nbsp;
                Visit date:&nbsp;<asp:TextBox ID="txtVisitDate" Width="90px" runat="server"></asp:TextBox> &nbsp;
                <%if (BaseMstr.APPMaster.HasUserRight((long)SUATUserRight.LockNoteUR) && !BaseMstr.IsPatientLocked)
                  {%>
                        <asp:Button ID="btnLockNote" runat="server" Text="Lock Note" OnClick="btnLockNote_OnClick" />
                        <% } %>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="margin-bottom:10px; padding-left:10px;">
            Encounter Template: <asp:DropDownList ID="cboEncounterTemplates" runat="server" OnSelectedIndexChanged="cboEncounterTemplates_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div style="padding-right: 5px; padding-bottom: 10px;">
            <asp:UpdatePanel ID="upSOAP" runat="server">
                <ContentTemplate>
                    <asp:TabContainer ID="tabContSOAP" runat="server" Width="100%" ActiveTabIndex="0">
                        <asp:TabPanel ID="btnSubjective" runat="server" HeaderText="Subjective" Font-Size="Large"
                            Font-Bold="true">
                            <ContentTemplate>
                                <div style="padding: 10px;">
                                    <% if (lROSubjective == (long)RightMode.ReadOnly)
                                       { %>
                                    <div style="display: block; margin-bottom: 10px; background-color: #f1f1f1; padding: 4px;">
                                        <asp:Image ID="imgReadOnly" AlternateText="Read Only Access" ImageUrl="~/Images/lock16x16.png"
                                            runat="server" />&nbsp; You have <b>Read-Only access</b> to this section.
                                    </div>
                                    <%} %>
                                    <div style="padding-bottom: 10px; display: none;">
                                        Templates&nbsp;&nbsp;
                                        <asp:DropDownList AutoPostBack="True" ID="cboSTemplates" runat="server" OnSelectedIndexChanged="cboSTemplates_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="padding-bottom: 10px; margin: 0 auto;">
                                        <asp:TextBox ID="txtSubjective" runat="server"
                                            Rows="10"
                                            TextMode="MultiLine"
                                            Width="100%">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="btnObjective" runat="server" HeaderText="Objective" Font-Size="Large"
                            Font-Bold="true">
                            <ContentTemplate>
                                <div style="padding: 10px;">
                                    <% if (lROObjective == (long)RightMode.ReadOnly)
                                       { %>
                                    <div style="display: block; margin-bottom: 10px; background-color: #f1f1f1; padding: 4px;">
                                        <asp:Image ID="Image2" AlternateText="Read Only Access" ImageUrl="~/Images/lock16x16.png"
                                            runat="server" />&nbsp; You have <b>Read-Only access</b> to this section.
                                    </div>
                                    <%} %>
                                    <asp:UpdatePanel ID="upObjectiveView" runat="server">
                                        <ContentTemplate>
                                            <asp:RadioButtonList ID="rblObjectiveView" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblObjectiveView_SelectedIndexChanged" RepeatDirection="Horizontal" Visible="false" >
                                               <asp:ListItem>Objective Measures&nbsp;&nbsp;</asp:ListItem>
                                               <asp:ListItem>Note&nbsp;&nbsp;</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:MultiView ID="mvwObjectiveOptions" runat="server">
                                                <asp:View ID="vwObjectiveNote" runat="server">
                                                    <div style="padding-bottom: 10px; display: none;">
                                                        Templates&nbsp;&nbsp;
                                                        <asp:DropDownList ID="cboOTemplates" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboOTemplates_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div style="padding-bottom: 10px; margin: 0 auto;">
                                                        <asp:TextBox ID="txtObjective" runat="server" Width="100%" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                                    </div>
                                                </asp:View>
                                                <asp:View ID="vwObjectiveMeasures" runat="server">
                                                    <div style="padding: 10px;">
                                                        <asp:HiddenField ID="htxtOMProblemID" runat="server" />
                                                        <ucProblemList:ucProblemList ID="ucProblemList" runat="server" />
                                                    </div>
                                                </asp:View>
                                            </asp:MultiView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="btnAssessment" runat="server" HeaderText="Assessment / Plan" Font-Size="Large"
                            Font-Bold="true">
                            <ContentTemplate>
                                <div id="divAssessment" runat="server" style="padding: 10px;">
                                    <% if (lROAssessment == (long)RightMode.ReadOnly)
                                       { %>
                                    <div style="display: block; margin-bottom: 10px; background-color: #f1f1f1; padding: 4px;">
                                        <asp:Image ID="Image3" AlternateText="Read Only Access" ImageUrl="~/Images/lock16x16.png"
                                            runat="server" />&nbsp; You have <b>Read-Only access</b> to this section.
                                    </div>
                                    <%} %>
                                    <!-- container for radio button -->
                                    <div>
                                        <div style="float: left; margin-right: 20px; padding: 6px; border: 1px solid #e1e1e1;
                                            height: 14px;">
                                            <asp:RadioButtonList ID="rblAssessmentView" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblAssessmentView_SelectedIndexChanged"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True">Diagnosis&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem>Note</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                    </div>
                                    <br />
                                    <div id="mvwAssessmentOptions" runat="server">
                                        <div id="vwDiagnosis" runat="server" style="display: none;">
                                            <ucAxesList:ucAxesList ID="ucAxes" runat="server" />
                                        </div>
                                        <div id="vwAssessmentNote" runat="server" style="display: none;">
                                            <div style="padding-bottom: 10px; display: none;">
                                                Templates&nbsp;&nbsp;
                                                <asp:DropDownList ID="cboATemplates" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboATemplates_SelectedIndexChanged"
                                                    Width="580px">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="padding-bottom: 10px; margin: 0 auto;">
                                                <asp:TextBox ID="txtAssessment" runat="server" Width="100%" TextMode="MultiLine"
                                                    Rows="10"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <!--TIU SUPPORT-->
                        <asp:TabPanel ID="tpTIUNote" runat="server" HeaderText="TIU Note" Font-Size="Large"
                            Font-Bold="true">
                            <ContentTemplate>
                       
                          <!--write note panel-->
                          <asp:Panel ID="pnlWriteNote" runat="server">
                            <div style="padding-bottom: 10px;">
                            
                            Note Title:&nbsp;&nbsp;<asp:DropDownList ID="ddlNoteTitle" runat="server"></asp:DropDownList>
                            </div>
                            <div style="padding-bottom: 10px;">
                            Clinic:&nbsp;&nbsp;<asp:DropDownList ID="ddlClinic" runat="server"></asp:DropDownList>
                            </div>
                            <div style="padding-bottom: 10px;">
                            Note:<br />
                                <div style="padding-bottom: 10px; margin: 0 auto;">
                                    <asp:TextBox ID="txtTIUNote" runat="server" 
                                                 Width="100%" ReadOnly="true" TextMode="MultiLine"
                                                 Rows="10"></asp:TextBox>
                                </div>
                            </div>
                            
                            <div style="padding-bottom: 10px;">
                            Sign With MDWS Credentials:&nbsp;&nbsp;
                                <asp:TextBox TextMode="Password" ID="txtSign" runat="server"></asp:TextBox>
                            </div>
                            
                            <asp:Button ID="btnWriteTIU" 
                                runat="server" 
                                Text="Write TIU Note" 
                                CausesValidation="false" 
                                UseSubmitBehavior="false"/>
                          
                          </asp:Panel>     
                          <!--end of write note panel-->
                          
                          <asp:Panel ID="pnlViewNote" runat="server">
                            <div style="padding-bottom: 10px;">
                            TIU Note:<br />
                                <div style="padding-bottom: 10px; margin: 0 auto;">
                                    <asp:TextBox ReadOnly="true" ID="txtViewTIU" runat="server" 
                                                 Width="100%" TextMode="MultiLine"
                                                 Rows="20"></asp:TextBox>
                                </div>
                            </div>
                          </asp:Panel>
                            </ContentTemplate>
                        </asp:TabPanel>
                        </asp:TabContainer>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>