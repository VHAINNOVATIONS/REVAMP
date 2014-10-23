<%@ Page Title="REVAMP Practitioner - User Administration" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="user_admin.aspx.cs" Inherits="user_admin" %>
<%@ MasterType VirtualPath="MasterPage.master" %>
<%@ Register TagPrefix="ucUserAdmin" TagName="ucUserAdmin" Src="~/ucUserAdmin.ascx" %>

<asp:Content ID="ctUserAdmin" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .mGrid tbody tr
        {
            cursor: pointer;
        }
        .mGrid tbody tr:hover
        {
            background-color: #FFFF99;
        }
        
        .tbl-rights
        {
	        font-family: "Lucida Sans Unicode", "Lucida Grande", Sans-Serif;
	        font-size: 12px;
	        background: #fff;
	        border-collapse: collapse;
	        text-align: left;
        }
        .tbl-rights th
        {
	        font-size: 14px;
	        font-weight: normal;
	        color: #039;
	        padding: 10px 8px;
	        border-bottom: 2px solid #6678b1;
        }
        .tbl-rights td
        {
	        border-bottom: 1px solid #ccc;
	        color: #404040;
	        padding: 6px 8px;
        }
        
    </style>

    <script src="js/administration/admin.user.js" type="text/javascript"></script>

    <% if (!bAllowUpdate)
    { %>
	    <div style="display: block; margin: 10px; background-color: #f1f1f1; padding: 4px; text-align:left;">
		    <asp:Image ID="Image1" AlternateText="Read Only Access" ImageUrl="~/Images/lock16x16.png" runat="server" />&nbsp;
		    You have <b>Read-Only access</b> to this section.
	    </div>
    <%} %>
    
    <asp:UpdatePanel ID="upWrapperUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <ucUserAdmin:ucUserAdmin ID="ucUserAdmin" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
