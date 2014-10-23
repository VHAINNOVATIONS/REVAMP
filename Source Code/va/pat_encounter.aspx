<%@ Page Title="REVAMP Practitioner - SOAP Note" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pat_encounter.aspx.cs" Inherits="pat_encounter" %>
<%@ MasterType VirtualPath="MasterPage.master" %>
<%@ Register TagPrefix="ucPatSOAPP" TagName="ucPatSOAPP" Src="~/ucPatientSOAPP.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManagerProxy ID="smpSoapNote" runat="server" ></asp:ScriptManagerProxy>

<style type="text/css">
    .obj-txt
    {
        width: 90%;
    }
</style>

<script src="js/soap/soap.js" type="text/javascript"></script>
<script src="js/soap/soap.assessment.js" type="text/javascript"></script>
<script src="js/soap/soap.objective.js"></script>
<script src="js/soap/soap.plan.js" type="text/javascript"></script>
<script src="js/diagnosisAxis.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="upWrapperUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:UpdatePanel ID="upPatSOAP" runat="server">
                <ContentTemplate>
                    <ucPatSOAPP:ucPatSOAPP Visible="true" ID="ucPatSOAPP" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <script type="text/javascript" defer="defer">

        soap.objective.init();
        
        soap.plan.problem.init();

        soap.assessment.init();
        soap.init();
        
        
        var prm_soap = Sys.WebForms.PageRequestManager.getInstance();
        prm_soap.add_initializeRequest(InitializeRequest);
        prm_soap.add_endRequest(EndRequest);

        function InitializeRequest(sender, args)
        {
            $('div.ajax-loader').show();
        }

        function EndRequest(sender, args)
        {
            soap.objective.init();
            
            soap.plan.problem.init();

            soap.assessment.init();
            soap.init();

            dp();

            autoAdjustMainDiv();
            $('div.ajax-loader').hide();

            //restartSessionTimeout();
        }

        //enable the date-picker for all
        //text boxes with the css class 'date-picker'
        function dp()
        {
            $('.date-picker').datepicker({
                beforeShow: function()
                {
                    $(document).ready(function () {
                        setTimeout(function () {
                            wp.adjustMain();
                        }, 1);
                    });
                }
            });
        }

        dp();
    </script>
    
</asp:Content>

