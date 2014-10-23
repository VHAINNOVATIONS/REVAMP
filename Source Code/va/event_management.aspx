<%@ Page Title="REVAMP Practitioner - Case Management" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="event_management.aspx.cs" Inherits="event_management" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register TagPrefix="ucEvtManagement" TagName="ucEvtManagement" Src="~/ucEventManagement.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" Runat="Server">
    <style>
        input[type="text"][id$="txtSearchPatient"] {
            width: 140px;
            height: 24px;
            padding-left: 30px;
            color: #808080;
            background: #fff url(Images/zoom.png) 4px 50% no-repeat;
            font-weight: bold;
            font-size: 14px;
        }
        
        table.tbl_evt_headings, table.tbl_evt_rows {
            border-collapse: collapse;
        }

        .tbl_evt_headings thead td {
            vertical-align: bottom;
        }

        .tbl_evt_headings thead td:first-child {
            font-weight: bold;
            font-size: 18px;
            padding: 4px;
            width: 300px;
        }

        .tbl_evt_rows tbody td {
            vertical-align: middle;
            padding: 2px 4px;
            margin: 0;
        }

        .tbl_evt_rows tbody td {
            border: 1px solid #e0e0e0;
            font-weight: bold;
            width: 36px;
        }

        .tbl_evt_rows tbody td a {
            font-weight: bold;
        }

        .tbl_evt_rows tbody td:first-child {
            width: 299px;
        }

        .tbl_evt_rows tr:nth-child(odd) {
            background-color: #eee;
        }

        .tbl_evt_rows tr:nth-child(even) {
            background-color: #fff;
        }

        
    </style>
<script src="js/management/management.event.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ucEvtManagement:ucEvtManagement ID="ucEvtManagement" runat="server" />
    <script>
        var prm_soap = Sys.WebForms.PageRequestManager.getInstance();
        prm_soap.add_initializeRequest(InitializeRequest);
        prm_soap.add_endRequest(EndRequest);

        function InitializeRequest(sender, args) {
            $('div.ajax-loader').show();
        }

        function EndRequest(sender, args) {

            $('div', $('div[id$="div-page-contents"]')).eq(0).width($('table.tbl_evt_headings').width());
            $('input[type="button"], input[type="submit"]').css({ 'padding': '2px 6px' });

            setTimeout(function () {
                management.event.init();
                $('table.tbl_evt_rows tr:nth-child(odd)').css({ 'background-color': '#eee' });
                $('table.tbl_evt_rows tr:nth-child(even)').css({ 'background-color': '#fff' });
            }, 100);

            $('div.ajax-loader').hide();
            restartSessionTimeout();
        }

        //function dp() {
        //    $('.date-picker').datepicker({
        //        beforeShow: function () {
        //            wp.adjustMain();
        //        }
        //    });
        //}

        $(document).ready(function () {
            $('.master-save, #lnkMasterSave').remove();
            $('div', $('div[id$="div-page-contents"]')).eq(0).width($('table.tbl_evt_headings').width());


        });

        $(window).bind({
            load: function () {
                setTimeout(function () {
                    management.event.init();
                    $('table.tbl_evt_rows tr:nth-child(odd)').css({ 'background-color': '#eee' });
                    $('table.tbl_evt_rows tr:nth-child(even)').css({ 'background-color': '#fff' });
                }, 100);
            }
        });

        (function () {
            var patdata = [];
            $('.tbl_evt_rows tr').each(function (i, t) {
                var name = $('td div div a', t).text(),
                    obj = {};
                obj.id = t.id;
                obj.name = name;
                patdata.push(obj);
            });
            window.patdata = patdata;
        })(window);


    </script>
</asp:Content>