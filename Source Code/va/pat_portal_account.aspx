<%@ Page Title="REVAMP Practitioner - Patient Portal Account Management" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pat_portal_account.aspx.cs" Inherits="pat_portal_account" %>

<%@ MasterType VirtualPath="MasterPage.master" %>
<%@ Register TagPrefix="ucPatientPortalAccount" TagName="ucPatientPortalAccount"
    Src="~/ucPatientPortalAccount.ascx" %>
<asp:Content ID="ctPortalAccount" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span id="sTitle" style="padding-left: 26px; float: left" class="PageTitle">Patient Portal Account </span>
    <br />
    <% if (bShowReadOnlyAlert && (lPortalAccRightsMode == (long)RightMode.ReadOnly))
        { %>
            <div style="display: block; margin-top: 10px; background-color: #f1f1f1; padding: 4px;">
                <asp:Image ID="Image1" AlternateText="Read Only Access" ImageUrl="~/Images/lock16x16.png" runat="server" />&nbsp;
                You have <b>Read-Only access</b> to this section.
            </div>
        <%} %>
    <div id="dPatientPortalAccount" style="text-align: left; padding: 10px 10px 10px 10px;
        width: 95%; margin: 10px auto; border: 1px solid #8692c4; clear: both;">
        <ucPatientPortalAccount:ucPatientPortalAccount Visible="true" ID="ucPatientPortalAccount" runat="server" />
    </div>
</asp:Content>
