 <%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPatLookup.ascx.cs" Inherits="ucPatLookup" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<asp:ScriptManagerProxy ID="smpPatLookup" runat="server" >
    <Scripts>
        <asp:ScriptReference NotifyScriptLoaded="true" Path="~/js/patlookup.js" />
    </Scripts>
</asp:ScriptManagerProxy>

<asp:Panel ID="pnlPatLookup" BorderStyle="None" DefaultButton="btnSearch" runat="server">
    <asp:UpdatePanel ID="upPatLookup" runat="server">
        <ContentTemplate>
            <div style="width: 600px;">
                
                <div style="text-align: center; margin: 10px; padding: 10px; border: 1px solid #808080;
                    background-color: #fff;">
                    <div style="float: left; margin-right:12px;">
                        <div style="text-align: center; float: right;">
                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>&nbsp;
                            <asp:Button runat="server" CssClass="button" Text="Search" ID="btnSearch" OnClick="btnPopupPatlookupSearch_Click"
                                UseSubmitBehavior="false" />
                        </div>
                    </div>
                    <div style="text-align: left; float: left;">
                        <asp:RadioButtonList CellSpacing="10" ID="rblSearchType" runat="server" CssClass="inp-radio"
                            RepeatLayout="Flow" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Selected="True">Last Name</asp:ListItem>
                            <asp:ListItem Value="3">L.I. + Last 4 SSN</asp:ListItem>
                            <asp:ListItem Value="4">User ID</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="clear: both;">
                    </div>
                </div>
                
                <div id="divPatLookupStatus" runat="server" style="text-align: left; margin: 5px auto 5px auto; padding:5px; font-weight:bold;"></div>

                <div style="clear: both; height: 500px; overflow: auto; margin: 0 auto;">
                    <%-- Ext.NET --%>
                    <ext:GridPanel ID="GridPanel1" runat="server" StripeRows="true" TrackMouseOver="true"
                        Width="600" Height="500">
                        <Store>
                            <ext:Store ID="Store1" runat="server">
                                <Reader>
                                    <ext:JsonReader>
                                        <Fields>
                                            <ext:RecordField Name="patient_id" />
                                            <ext:RecordField Name="lnssnlast4" />
                                            <ext:RecordField Name="last_name" />
                                            <ext:RecordField Name="first_name" />
                                            <ext:RecordField Name="birthdate" Type="Date" DateFormat="M/d/Y" />
                                            <ext:RecordField Name="gender" />
                                            <ext:RecordField Name="patient_age" Type="Int" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:Column Header="SSN" DataIndex="lnssnlast4" Width="90">
                                    <Renderer Fn="renderFMP" />
                                </ext:Column>
                                <ext:Column Header="Last Name" DataIndex="last_name" Width="135" />
                                <ext:Column Header="First Name" DataIndex="first_name" Width="135" />
                                <ext:DateColumn Header="DOB" DataIndex="birthdate" Width="100" />
                                <ext:Column Header="Age" DataIndex="patient_age" Width="60" />
                                <ext:Column Header="Gender" DataIndex="gender" Width="65" />
                            </Columns>
                        </ColumnModel>
                        <LoadMask ShowMask="true" />
                        <Plugins>
                            <ext:GridFilters runat="server" ID="GridFilters1" Local="true">
                                <Filters>
                                    <ext:StringFilter DataIndex="last_name" />
                                    <ext:StringFilter DataIndex="first_name" />
                                    <ext:DateFilter DataIndex="birthdate" />
                                    <ext:NumericFilter DataIndex="patient_age" />
                                    <ext:ListFilter DataIndex="gender" Options="M,F" />
                                </Filters>
                            </ext:GridFilters>
                        </Plugins>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelModel1" runat="server" SingleSelect="true" />
                        </SelectionModel>
                    </ext:GridPanel>
                    <%-- Ext.NET --%>
                  
                </div>
                <div id="PopupPostPatLookup" runat="server">
                </div>
                <div style="display:none;">
                    <asp:Button ID="btnClearLookupWindow" runat="server" OnClick="ClearPatLookup" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <script type="text/javascript">
        InitRadios();

        var clearLookupStatusDiv = function(delaySecs)
        {
            Sys.onReady(function()
            {
                var clDivLookup = setTimeout('$(\'div[id$="divPatLookupStatus"]\')[0].innerHTML = \'\'', delaySecs * 1000);
            });
        };
        
    </script>

    <%-- Set null button as default --%>
    <div style="display: none;">
        <asp:Button ID="btnNullPatLookup" CssClass="button" runat="server" UseSubmitBehavior="false" OnClientClick="return false;" />
    </div>
</asp:Panel>
