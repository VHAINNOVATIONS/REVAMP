<%@ Page Title="REVAMP Portal - Education" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="education.aspx.cs" Inherits="education" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div id="divEducationContainer">
        <div id="divMenuTree" runat="server" style="float: left; width: 250px; margin-right: 10px;"></div>
        <div id="divEducationContents" style="float: left; width: 650px; border-left: 1px solid #e1e1e1; padding-left: 10px;">
            <div id="divEduInfo" runat="server" style="width: 95%; margin: 10px auto; padding: 5px; border: 1px solid #e1e1e1; background-color: #f2f0d0; line-height: 155%;">
                This Education page will allow you to view two videos. You will need to view both videos before your sleep study. To watch the videos, you need to have audio on your computer. The <b>What is Sleep Apnea?</b> Video will tell you about sleep apnea and its treatment. The <b>Sleep Study Setup</b> video will tell you how to perform the home sleep study. You can access the videos by clicking the titles on the drop down list on the left hand side of this screen. The drop down list will also allow you to access other information about sleep apnea and sleep disorders.
            </div>
            <asp:UpdatePanel ID="upEduContents" runat="server" OnLoad="upEduContents_OnLoad">
                <ContentTemplate>
                    <div class="PageTitle">
                        <asp:Literal ID="litTitle" runat="server"></asp:Literal>
                    </div>
                    <div class="cms-contents">
                        <asp:Literal ID="litContents" runat="server"></asp:Literal>
                    </div>
                    
                    <asp:HiddenField ID="htxtEduID" runat="server" />
                    <asp:HiddenField ID="htxtEventID" runat="server" />
                    <asp:HiddenField ID="htxtOPT0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            
            <%-- US 2862: Display a message after viewing the videos --%>
            <asp:UpdatePanel ID="upCheckVideosStatus" runat="server">
                <ContentTemplate>
                    <div id="divCheckVideosStatus" runat="server" style="display: none;">
                        <asp:Button ID="btnCheckVideosStatus" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <div style="clear: both;"></div>

    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cpScripts" runat="Server">
    <script src="js/cms/cms.education.js"></script>
    <script src="js/cms/cms.video.js"></script>
    <script>

        cms.education.opts.contentsPanelID = '<%= upEduContents.ClientID %>';
        cms.video.opts.upCheckVideosStatus = '<%= upCheckVideosStatus.ClientID %>';

        var prm_soap = Sys.WebForms.PageRequestManager.getInstance();
        prm_soap.add_initializeRequest(InitializeRequest);
        prm_soap.add_endRequest(EndRequest);

        adjustHeight({ _containerID: 'divEducationContainer', _offset: 20 });

        cms.video.init();

        function InitializeRequest(sender, args) {
            $('div.ajax-loader').show();
        }

        function EndRequest(sender, args) {
            $('div.ajax-loader').hide();

            adjustHeight({ _containerID: 'divEducationContainer', _offset: 20 });
            $('input[type="button"], input[type="submit"]').css({ padding: '2px 6px' });

            cms.video.init();
            trackEduTitle();
        }

        function trackEduTitle() {
            if (typeof (piwikTracker) != "undefined") {
                if (typeof (window.eduTitle) != "undefined") {
                    piwikTracker.setCustomVariable(2, "Education Topic", window.eduTitle, "page");
                    piwikTracker.trackPageView();
                    piwikTracker.enableLinkTracking();
                }
            }
        }

</script>
</asp:Content>
