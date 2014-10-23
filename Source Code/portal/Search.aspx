<%@ Page Title="REVAMP Portal - Search" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" Runat="Server">
    <style>
        
        div[id$="divResults"] ol {
            list-style-type:decimal-leading-zero;
            margin-left: 40px;
        }

        div[id$="divResults"] ol li {
            margin-bottom: 15px;
        }

        div[id$="divResults"] ul {
            list-style-type: square;
            margin-left: 15px;
        }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="font-size:14px; font-weight: bold; margin-bottom:15px;">
        Search results for: "<asp:Label ID="lblSearchTopic" runat="server"></asp:Label>"
    </div>
    <div id="divResults" runat="server">

    </div>
</asp:Content>

