<%@ Page Title="REVAMP Practitioner - Questionnaire Assignment" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="pat_module_assignment.aspx.cs" Inherits="pat_Module_Assignment" %>

<%@ Register TagPrefix="ucIntakeModules" TagName="ucIntakeModules" Src="~/ucIntakeModules.ascx" %>
<%@ MasterType VirtualPath="MasterPage.master" %>
<asp:Content ID="ctModuleAssignment" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="js/moduleassign.js" type="text/javascript"></script>

    <div class="PageTitle" style="margin-left: 10px;">
        Assign Assessments
    </div>
    <% if (bReadOnly && !Master.IsPatientLocked)
       { %>
    <div style="display: block; margin-bottom: 10px; background-color: #f1f1f1; padding: 4px;">
        <asp:Image ID="Image1" AlternateText="Read Only Access" ImageUrl="~/Images/lock16x16.png"
            runat="server" />&nbsp; You have <b>Read-Only access</b> to this section.
    </div>
    <%} 
     if (Master.IsPatientLocked)
	   { %>
	<div style="display: block; margin-bottom: 10px; background-color: #f1f1f1; padding: 4px;">
		<asp:Image ID="Image2" AlternateText="Read Only Access" ImageUrl="~/Images/lock16x16.png"
			runat="server" />&nbsp; The patient's record is in use by <%= Session["PAT_LOCK_PROVIDER"].ToString() %>. 
			<a style="text-decoration: underline; color: Blue;" href='mailto:<%= Session["PAT_LOCK_EMAIL"].ToString() %>'> [Send Email]</a>
	</div>
	<%} %>
    <asp:UpdatePanel ID="upWrapperUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="left" id="divStatus" runat="server" style="margin-bottom: 5px; margin-left: 5px;
                padding: 5px; font-weight: bold;">
            </div>
            <div style="text-align: left; margin: 0 10px 0 0;">
                <ucIntakeModules:ucIntakeModules Visible="true" ID="ucIntakeModules" runat="server" />
            </div>
            <asp:HiddenField ID="htxtSelectedModules" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">

        moduleassign.init();
        moduleassign.unbindOnclick(<%= Master.IsPatientLocked.ToString().ToLower() %>);
        

        var prm_soap = Sys.WebForms.PageRequestManager.getInstance();
        prm_soap.add_initializeRequest(InitializeRequest);
        prm_soap.add_endRequest(EndRequest);

        function InitializeRequest(sender, args)
        {
            $('div.ajax-loader').show();
        }

        function EndRequest(sender, args)
        {
            autoAdjustMainDiv();
            $('div.ajax-loader').hide();
            moduleassign.init();
            moduleassign.unbindOnclick(<%= Master.IsPatientLocked.ToString().ToLower() %>);
            restartSessionTimeout();
        }

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
    </script>

</asp:Content>
