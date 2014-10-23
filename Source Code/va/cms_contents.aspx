<%@ Page Title="REVAMP Practitioner - Content Management System" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="cms_contents.aspx.cs" Inherits="cms_contents" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="upContents" runat="server">
        <ContentTemplate>
            <div style="margin:0 20px;">
                <div id="divCMSContents" class="cms-contents">
                    <div class="PageTitle" style="margin-top: 20px;"><asp:Literal ID="litTitle" runat="server"></asp:Literal></div>
                    <asp:Literal ID="litContents" runat="server"></asp:Literal>
                </div>
                <div style="clear:both;"></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        $(function () {
            $('.master-save, #lnkMasterSave').remove();
        });
    </script>
</asp:Content>

