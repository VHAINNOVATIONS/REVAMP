<%@ Page Title="REVAMP Practitioner - CMS Edit Page" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="cms_page_edit.aspx.cs" Inherits="cms_page_edit" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register TagPrefix="ucPageEdit" TagName="ucPageEdit" Src="~/ucCMSPageEdit.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" Runat="Server">
    <script src="js/tiny_mce/jquery.tinymce.js"></script>
    <script src="js/cms/cms.js"></script>
    <script src="js/cms/cms.page.js"></script>
    
    <style>
        .radBtnList input[type="radio"] {
            margin-right: 4px;
        }

        .radBtnList label {
            color: rgb(7, 76, 131);
            font: 11px Verdana, Geneva, Tahoma, sans-serif;
            font-weight: bolder;
            margin-right: 10px;
        }

        .cms-templates-contents h1, 
        .cms-templates-contents h2, 
        .cms-templates-contents h3, 
        .cms-templates-contents h4 {
            margin:10px 0;
        }

        .cms-templates-contents p {
            margin-bottom: 8px;
        }

        .cms-templates-contents a {
            color: Blue;
            text-decoration:underline;
        }

        .cms-templates-contents ul {
           list-style: disc;
           margin-left: 45px;
        }

        .cms-templates-contents ol {
           margin-left: 45px;
        }

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upWrapperUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <ucPageEdit:ucPageEdit ID="ucPageEdit" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        cms.page.init();
        cms.page.opts.authorID = '<%= Master.FXUserID.ToString() %>';
        cms.page.opts.mainPanel = '<%= upWrapperUpdatePanel.ClientID %>';

        $(document).ready(function () {
            setTimeout(function () {
                $('.master-save, [id$="lnkMasterSave"]').attr('onclick', 'return cms.page.setMasterSave();');
            }, 100);
        });

        var prm_soap = Sys.WebForms.PageRequestManager.getInstance();
        prm_soap.add_initializeRequest(InitializeRequest);
        prm_soap.add_endRequest(EndRequest);

        function InitializeRequest(sender, args) {
            $('div.ajax-loader').show();
        }

        function EndRequest(sender, args) {

            cms.page.opts.authorID = '<%= Master.FXUserID.ToString() %>';

            $('div.ajax-loader').hide();
            $(document).ready(function () {

                setTimeout(function () {
                    $('.master-save, [id$="lnkMasterSave"]').attr('onclick', 'return cms.page.setMasterSave();');
                    cms.page.init();
                    wp.adjustMain();
                    (tinyMCE.activeEditor).save();
                }, 300);
            });
            restartSessionTimeout();
        }

    </script>
</asp:Content>

