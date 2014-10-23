<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="referral_clinic_management.aspx.cs" Inherits="referral_clinic_management" %>
<%@ MasterType VirtualPath="MasterPage.master" %>
<%@ Register TagPrefix="ucRFC" TagName="ucRFC" Src="~/ucReferralClinicMgnt.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="smReferralClinic" runat="server"></asp:ScriptManagerProxy>
    
    <script src="js/management/management.js" type="text/javascript"></script>
    <script src="js/management/management.clinic.js" type="text/javascript"></script>

    <% if (!bAllowUpdate)
        { %>
            <div style="display: block; margin: 10px; background-color: #f1f1f1; padding: 4px; text-align:left;">
                <asp:Image ID="Image1" AlternateText="Read Only Access" ImageUrl="~/Images/lock16x16.png" runat="server" />&nbsp;
                You have <b>Read-Only access</b> to this section.
            </div>
        <%} %>
    <asp:UpdatePanel ID="upReferralClinic" runat="server">
        <ContentTemplate>
            <ucRFC:ucRFC ID="ucReferralClinicManagement" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <script type="text/javascript">

        function autoAdjustMainDiv()
        {
            $(document).ready(function () {
                setTimeout(function () {
                    origWidth = $('input[id$="htxtMainDivWidth"]').val();
                    wp.adjustMain();

                    if ($('div[id$="mainContents"]').css('width') != origWidth)
                    {
                        $('div[id$="mainContents"]').css({
                            width: origWidth
                        });
                        $('input[id$="htxtMainDivWidth"]').val(origWidth);
                    }
                    $('input[type="button"], input[type="submit"]').css({
                        padding: '2px 6px'
                    });
                }, 1);
            });
        }
        
        var prm_soap = Sys.WebForms.PageRequestManager.getInstance();
        prm_soap.add_initializeRequest(InitializeRequest);
        prm_soap.add_endRequest(EndRequest);

        function InitializeRequest(sender, args)
        {
            $('div.ajax-loader').show();
        }

        function EndRequest(sender, args)
        {
            management.clinic.init();

            autoAdjustMainDiv();
            $('div.ajax-loader').hide();

            restartSessionTimeout();
        }
    </script>
    
</asp:Content>
