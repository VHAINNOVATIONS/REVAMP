<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucEncounterType.ascx.cs" Inherits="ucEncounterType" %>
<br />
<%--EncounterTypes Taken From Modality DropDownList--%>
<div id="divEncounterType">
    <asp:DropDownList id="ddlModality" runat="server" OnSelectedIndexChanged="ddlModality_SelectedIndexChanged" >
    </asp:DropDownList>
</div> 
<br />
