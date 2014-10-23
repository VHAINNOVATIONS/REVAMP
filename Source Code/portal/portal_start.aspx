<%@ Page Title="REVAMP Portal - Home" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="portal_start.aspx.cs" Inherits="portal_start" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" Runat="Server">
    <style>

        div[id$="divCompletedBaselineQ"], 
        div[id$="divCompletedVideos"],
        div[id$="divStep2"],
        div[id$="divStartedBaseline"],
        div[id$="divStartedVideos"],
        div[id$="divCompletedVideos"],
        div[id$="divCompletedFollowUp"],
        div[id$="divCompletedCSQ"],
        div[id$="divPendingQuestionnaires"]  {
            margin: 20px;
            padding: 10px;
            /*border: 1px solid #e1e1e1;*/
            font-size: large;
        }

        div[id$="divCompletedBaselineQ"] p,
        div[id$="divCompletedVideos"] p,
        div[id$="divStep2"] p,
        div[id$="divStartedBaseline"] p,
        div[id$="divStartedVideos"] p,
        div[id$="divCompletedVideos"] p,
        div[id$="divCompletedFollowUp"] p,
        div[id$="divCompletedCSQ"] p,
        div[id$="divPendingQuestionnaires"] p
        {
            line-height: 150%;
            margin-bottom: 15px;
        }

        div[id$="divStep2"] table {
            margin-bottom: 15px;
        }

        div[id$="divStep2"] td:first-child {
            padding: 4px 10px 4px 0;
            text-align: right;
            font-weight: bold;
            color: #0e2075;
        }
        

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:MultiView ID="mvPortalSteps" runat="server">

        <asp:View ID="vDefault" runat="server" EnableViewState="true">
            <%-- TODO: --%>
        </asp:View>

        <asp:View ID="vStep2" runat="server">
            <div id="divStep2">
                <p><span id="spSubmitTxt" runat="server" visible="false">Thank you for submitting your information. </span>This is your REVAMP screen. It will always have the gray bar with tab functions under the REVAMP header. These will allow you to move easily from one area of the website to another.</p>
                <table>
                    <tr>
                        <td>My Profile</td>
                        <td>Allows you to update your contact information and provides the name of your sleep specialist</td>
                    </tr>
                    <tr>
                        <td>Questionnaires</td>
                        <td>Allows you to tell us about your sleep and how you are responding to treatment</td>
                    </tr>
                    <tr>
                        <td>Education</td>
                        <td>Provides videos and information about sleep apnea</td>
                    </tr>
                    <tr>
                        <td>Treatment results</td>
                        <td>Allows you to see how you are doing</td>
                    </tr>
                    <tr>
                        <td>Messages</td>
                        <td>Allows you to send messages to your sleep specialist</td>
                    </tr>
                </table>
                <p>We would like you to answer some questions that will tell us about your sleep problem and help us to develop your management plan. If you are ready to do this, please click the <a href="patient_assessment.aspx" style="color: blue; text-decoration: underline;">Questionnaires</a> tab on the gray bar under the REVAMP header. If you want to return later to answer the questions, please logoff by clicking the LOGOFF box at the top right corner of the screen.</p>
            </div>
        </asp:View>

        <asp:View ID="vStartedBaseline" runat="server">
            <div id="divStartedBaseline">
                <p>We would like you to answer some questions that will tell us about your sleep problem and help us to develop your management plan. If you are ready to do this, please click the <a href="patient_assessment.aspx" style="color: blue; text-decoration: underline;">Questionnaires</a> tab on the gray bar under the REVAMP header. If you want to return later to answer the questions, please logoff by clicking the LOGOFF box at the top left corner of the screen.</p>
            </div>
        </asp:View>

        <asp:View ID="vCompletedBaselineQ" runat="server" EnableViewState="true">
            <div id="divCompletedBaselineQ" runat="server">
                <p id="pQuestWelcome" runat="server" visible="true" style="font-weight: bold; color: #9f0022; display: block; margin-bottom: 10px;">
                    Welcome to REVAMP, the Remote Veteran Apnea Management Portal!
                </p>
                <p id="pQCongratulations" runat="server" visible="false">
                    Congratulations, you have successfully completed all of the questions. 
                </p>

                <p style="margin-top: 15px;">
                   The next step in the process is to watch two videos. One is on sleep apnea and its treatment. The other tells you about the sleep study. To watch the videos, click the <a href="education.aspx" style="color: blue; text-decoration: underline;">Education</a> tab on the upper part of the screen. When the new window appears, click on the particular video you want to watch. You need to watch both videos before you can be scheduled for your sleep study.
                </p>
            </div>
        </asp:View>

        <asp:View ID="vStartedVideos" runat="server" EnableViewState="true">
            <div id="divStartedVideos" runat="server">
                <p id="p1" runat="server" visible="true" style="font-weight: bold; color: #9f0022; display: block; margin-bottom: 10px;">
                    Welcome to REVAMP, the Remote Veteran Apnea Management Portal!
                </p>
                <p style="margin-top: 15px;">
                    The next step in the process is to watch two videos. One is on sleep apnea and its treatment. The other tells you about the sleep study. To watch the videos, click the <a href="education.aspx" style="color: blue; text-decoration: underline;">Education</a> tab on the upper part of the screen. When the new window appears, click on the particular video you want to watch. You need to watch both videos before you can be scheduled for your sleep study.
                </p>
            </div>
        </asp:View>

        <asp:View ID="vCompletedVideos" runat="server" EnableViewState="true">
            <div id="divCompletedVideos" runat="server">
                <p id="pVidWelcome" runat="server" visible="true" style="font-weight: bold; color: #9f0022; display: block; margin-bottom: 10px;">
                    Welcome to REVAMP, the Remote Veteran Apnea Management Portal!
                </p>

                <%--<p>
                    <span id="spVidCongratulations" runat="server" visible="false">Congratulations, you have successfully watched both videos. We hope 
                     that this has given you a better understanding of sleep apnea and how 
                     the sleep study is performed. </span>If your sleep specialist orders a sleep study 
                     for you, you will be contacted by the sleep center to schedule the test. 
                     If you have not been contacted in the next week, please send another message 
                     to your sleep specialist by clicking the Message tab on the top bar of the REVAMP screen.
                </p>--%>

                <p>
                    Congratulations you have watched both videos. We hope that this has given you a better understanding of sleep apnea and how the home sleep study is performed. You can return to watch the videos again at any time. We strongly suggest that you watch the home sleep study video again on the day you do the sleep study.
                </p>

                <p style="margin-top: 15px;">
                    If your sleep specialist orders a sleep study for you, you will be contacted by the sleep center to schedule the test. If you have not been contacted in the next week, please send a message to your sleep specialist by clicking the <a class="button-yellow" href="message_center.aspx">Message</a> tab on the top bar of the REVAMP screen. 
                </p>

                <p style="margin-top: 15px;">
                    If you are scheduled for a sleep study, you will be mailed a portable monitor with instructions on how to perform the study at home. The packet will contain postal materials so you can mail the monitor back to the sleep center immediately after you have used it. Make sure that your <a class="button-yellow" href="pat_profile.aspx">My Profile</a> page has the correct mailing address.
                </p>
            </div>
        </asp:View>

        <asp:View ID="vPendingQuestionnaires" runat="server">
            <div id="divPendingQuestionnaires">
                <p id="p2" runat="server" style="font-weight: bold; color: #9f0022; display: block; margin-bottom: 10px;">
                    Welcome to REVAMP, the Remote Veteran Apnea Management Portal!
                </p>
                <p>We would like you to answer some follow-up questions that will tell us about your sleep problem and help us to develop your management plan. If you are ready to do this, please click the <a href="patient_assessment.aspx" style="color: blue; text-decoration: underline;">Questionnaires</a> tab on the gray bar under the REVAMP header.</p>
                <asp:Literal ID="litQuestionnaireList" runat="server"></asp:Literal>
            </div>
        </asp:View>

        <asp:View ID="vCompletedFollowUp" runat="server">
            <div id="divCompletedFollowUp">
                <p>
                    Congratulations, you have successfully completed all of the questions. The sleep specialist will review your responses and discuss them with you. You should already have a phone call appointment with your sleep specialist. If not, please send a message to him/her that you have answered the questions and are ready for the phone call visit. You can send a message by clicking the <a class="button-yellow" href="message_center.aspx">Messages</a> tab on the gray bar under the REVAMP header. 
                </p>
            </div>
        </asp:View>

        <asp:View ID="vCompletedCSQ" runat="server">
            <div id="divCompletedCSQ">
                <p>
                    Congratulations, you have successfully completed the questions. You will be contacted to schedule your next follow-up phone call with your sleep specialist. If you have not been contacted within the next two weeks, please send a message to your sleep specialist that you are ready to schedule the phone call visit. You can send a message by clicking the <a class="button-yellow" href="message_center.aspx">Messages</a> tab on the gray bar under the REVAMP header. 
                </p>
            </div>
        </asp:View>

    </asp:MultiView>

</asp:Content>

