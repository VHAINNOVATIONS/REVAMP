<%@ Page Title="REVAMP Practitioner - CMS Menu Editor" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="cms_menu_edit.aspx.cs" Inherits="cms_menu_edit" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register TagPrefix="ucMenuEdit" TagName="ucMenuEdit" Src="~/ucCMSMenuEdit.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <script src="js/cms/cms.js"></script>
    <script src="js/cms/cms.menu.js"></script>

    <style>
        .radio-list input[type="radio"] {
            margin-right: 4px;
        }

        .radio-list label {
            margin-right: 10px;
        }

        .checkbox-list input[type="checkbox"] {
            margin-right: 4px;
        }

        .checkbox-list label {
            margin-right: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upWrapperUpdatePanel" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <ucMenuEdit:ucMenuEdit ID="ucMenuEdit" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" defer="defer">

        cms.menu.init();

        var prm_soap = Sys.WebForms.PageRequestManager.getInstance();
        prm_soap.add_initializeRequest(InitializeRequest);
        prm_soap.add_endRequest(EndRequest);

        function InitializeRequest(sender, args) {
            $('div.ajax-loader').show();
        }

        function EndRequest(sender, args) {
            $('div.ajax-loader').hide();
            cms.menu.init();
            restartSessionTimeout();
        }

    </script>

</asp:Content>
