<%@ Page Title="REVAMP Practitioner - Dashboard" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="revamp.aspx.cs" Inherits="revamp" %>
<%@ MasterType VirtualPath="MasterPage.master" %>

<asp:Content ID="ContentH" ContentPlaceHolderID="cpHeader" runat="server">
    <style>
        #divDashboard {
            display: block;
            height: 600px;
            background: url(Images/comp_kb.png) top left no-repeat;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <% if (Master.APPMaster.UserType == (long)SUATUserType.PATIENT)
       {
           Master.LogOff();
       }
    %>

    <div id="divDashboard">
        <div style="padding-left:25px;">
            <div style="text-align: left; margin:10px; padding-top:20px;">
            <img src="Images/REVAMP_trans_logo.png" style="width: 216px; height: 60px;" />
        </div>
        <div style="text-align: left;">
            <% if(Master.APPMaster.HasUserRight(5247)){ %>
               <div style="margin: 6px 0;">
                   <img src="Images/btn_lk_patient.png" style="width: 235px; height: 45px; cursor:pointer;" onclick="Ext.onReady(function(){winPatLookup.show(); getFocus('txtSearch');});" />
               </div>
            <% } %>

            <% 
                //TIU SUPPORT
                if (!Master.APPMaster.TIU)
                {
                    if (Master.APPMaster.HasUserRight(4096))
                    { %>
               <div style="margin: 6px 0;">
                   <img src="Images/btn_add_patient.png" style="width: 235px; height: 45px; cursor:pointer;" onclick="window.location='pat_demographics.aspx?newpatient=true&patconsents=1';" />
               </div>
            <% }
                } %>

            <% if (Master.APPMaster.HasUserRight(4175))
               { %>
               <div style="margin: 6px 0;">
                   <img src="Images/btn_case_mgnt.png" style="width: 235px; height: 45px; cursor:pointer;" onclick="window.location='event_management.aspx';" />
               </div>
            <% } %>

            <% if(true){ %>
               <div style="margin: 6px 0;">
                   <img src="Images/btn_msg_center.png" style="width: 235px; height: 45px; cursor:pointer;" onclick="winrpt.showReport('messagescenter',['null'],{maximizable:false, width:($(window).width() - 50), height:($(window).height() - 50)});" />
               </div>
            <% } %>
        </div>
        </div>
    </div>
    <div style="clear: both;"></div>

</asp:Content>

<asp:Content ID="cScripts" ContentPlaceHolderID="cpScripts" runat="server">
     <script type="text/javascript">

         var prm_soap = Sys.WebForms.PageRequestManager.getInstance();
         prm_soap.add_initializeRequest(InitializeRequest);
         prm_soap.add_endRequest(EndRequest);

         function InitializeRequest(sender, args) {
             $('div.ajax-loader').show();
         }

         function EndRequest(sender, args) {
             $('div.ajax-loader').hide();

             restartSessionTimeout();
         }


         $(function () {
             //Hide Master Save from toolbar and the menu
             $('.master-save, #lnkMasterSave').remove();
         });

    </script>
</asp:Content>
