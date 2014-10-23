<%@ Page Title="REVAMP Practitioner - Questionnaire Report" Language="C#" MasterPageFile="~/MasterPageRpt2.master" AutoEventWireup="true" CodeFile="rpt_assessments.aspx.cs" Inherits="rpt_assessments" %>
<%@ MasterType VirtualPath="~/MasterPageRpt2.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder001" Runat="Server">
    <asp:Button ID="btnExportCsv" runat="server"
        OnClick="OnClickExportCsv"
        Text="Export CSV"
        style="float: right; margin: 2px 25px;" />

    <asp:Literal ID="litReport" runat="server"></asp:Literal>
</asp:Content>

