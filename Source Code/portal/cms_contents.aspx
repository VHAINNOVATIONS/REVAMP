<%@ Page Title="REVAMP Portal - CMS Contents" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="cms_contents.aspx.cs" Inherits="cms_contents" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="cHeader" runat="server" ContentPlaceHolderID="cpHeader">
        <link href="css/cms-css-support.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="upContents" runat="server">
        <ContentTemplate>
            <div style="margin:0 20px;">
                <div class="PageTitle" style="margin-top: 20px;"><asp:Literal ID="litTitle" runat="server"></asp:Literal></div>
                <div id="divCMSContents" class="cms-contents">
                    <asp:Literal ID="litContents" runat="server"></asp:Literal>
                </div>
                <div style="clear:both;"></div>
            </div>
            <asp:HiddenField ID="htxtEventID" runat="server" />
            <asp:HiddenField ID="htxtOPT0" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="cScripts" runat="server" ContentPlaceHolderID="cpScripts">
    <script src="js/cms/cms.video.js"></script>
    <script>
        $(function () {
            $('.master-save, #lnkMasterSave').remove();
        });

        cms.video.init();
    </script>
</asp:Content>