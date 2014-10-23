<%@ Page Title="REVAMP Practitioner - Error Page" Language="C#" MasterPageFile="~/MasterPageError.master"  AutoEventWireup="true" CodeFile="error_database.aspx.cs" Inherits="error_database" %>
<%@ MasterType VirtualPath="~/MasterPageError.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        body
        {
            background-image: url('Images/doc_computer.jpg');
            background-attachment: fixed;
            background-repeat: no-repeat;
            background-position: center bottom;
            background-color: #023f79;
            font-family: Arial, Helvetica, Verdana, sans-serif;
        }
    </style>
    <div style="display: block; width: 720px; padding: 10px; height: 500px; margin: 0 auto 200px auto;
        overflow: auto;">
        <div style="text-align: center; margin-bottom: 15px;">
            <img alt="WebPSAM" src="Images/webpsam_logo.png" />
        </div>
        <div>
            <p>
                An error occurred while connecting to the web application.
                <br />
                Please contact your system administrator as soon as possible.</p>
            <p>
                An email was sent to Intellica's Application Manager notifying about this error.<br />
                A Technical Support representative from Intellica will soon be contacting your
                <br />
                System Administrator to fix the error.</p>
        </div>
        <div style="text-align: center;">
            <asp:Button ID="btnGoHome" runat="server" Text="Home" OnClick="btnGoHome_OnClick" />
        </div>
        <div id="divErrMain" runat="server" visible="false" style="font-size: 14px; margin: 15px; padding: 10px; border: 1px dashed #808080; background-color: #F0F0A8;
            color: Navy;">
            ***This page is under development. The info box below is not showed to the user *** <br/>
            <div id="divErr" runat="server">
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function()
        {
            $('[id$="btnGoHome"]').button();
        });
    </script>
</asp:Content>