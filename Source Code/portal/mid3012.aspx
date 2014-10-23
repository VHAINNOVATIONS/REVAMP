<%@ Page Title="Epworth Sleepiness Scale - ESS" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid3012.aspx.cs" Inherits="mid3012" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

        <!-- this tells the code behind how many responces to process -->
        <input type="hidden" name="ResponseCount" value="9" /><br />

        <h1>Epworth Sleepiness Scale - ESS</h1>
        <h4>These questions will help us to determine how sleepy you are in the daytime<br />
            It will take about one minute to answer these questions.<br />
            After you have answered the questions, click submit.</h4>
        <table class="tbl-quest">
            <tr id="TID1_QID1">
                <td>
                    <p>
                        In contrast to just feeling tired, how likely are you to doze off or fall asleep in the following situations?<br />
                        Below we have outlined various situations to help determine how sleepy you may be.<br />
                        Even if you have not faced these particular situations recently, please tell us how you think you would have responded.
                    </p>
                    <strong>Sitting and reading</strong>
                    <div>
                        <input type="radio" name="grpRadio_1" value="Would never doze|2|0" id="rid_2" /><label for="rid_2">Would never doze</label><br />
                        <input type="radio" name="grpRadio_1" value="Slight chance of dozing|4|1" id="rid_4" /><label for="rid_4">Slight chance of dozing</label><br />
                        <input type="radio" name="grpRadio_1" value="Moderate chance of dozing|6|2" id="rid_6" /><label for="rid_6">Moderate chance of dozing</label><br />
                        <input type="radio" name="grpRadio_1" value="High chance of dozing|8|3" id="rid_8" /><label for="rid_8">High chance of dozing</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID2">
                <td>
                    <strong>Watching TV</strong>
                    <div>
                        <input type="radio" name="grpRadio_2" value="Would never doze|10|0" id="rid_10" /><label for="rid_10">Would never doze</label><br />
                        <input type="radio" name="grpRadio_2" value="Slight chance of dozing|12|1" id="rid_12" /><label for="rid_12">Slight chance of dozing</label><br />
                        <input type="radio" name="grpRadio_2" value="Moderate chance of dozing|14|2" id="rid_14" /><label for="rid_14">Moderate chance of dozing</label><br />
                        <input type="radio" name="grpRadio_2" value="High chance of dozing|16|3" id="rid_16" /><label for="rid_16">High chance of dozing</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID3">
                <td>
                    <strong>Sitting, inactive in a public place (e.g. a theater or a meeting)</strong>
                    <div>
                        <input type="radio" name="grpRadio_3" value="Would never doze|18|0" id="rid_18" /><label for="rid_18">Would never doze</label><br />
                        <input type="radio" name="grpRadio_3" value="Slight chance of dozing|20|1" id="rid_20" /><label for="rid_20">Slight chance of dozing</label><br />
                        <input type="radio" name="grpRadio_3" value="Moderate chance of dozing|22|2" id="rid_22" /><label for="rid_22">Moderate chance of dozing</label><br />
                        <input type="radio" name="grpRadio_3" value="High chance of dozing|24|3" id="rid_24" /><label for="rid_24">High chance of dozing</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID4">
                <td>
                    <strong>As a passenger in a car for an hour without a break</strong>
                    <div>
                        <input type="radio" name="grpRadio_4" value="Would never doze|26|0" id="rid_26" /><label for="rid_26">Would never doze</label><br />
                        <input type="radio" name="grpRadio_4" value="Slight chance of dozing|28|1" id="rid_28" /><label for="rid_28">Slight chance of dozing</label><br />
                        <input type="radio" name="grpRadio_4" value="Moderate chance of dozing|30|2" id="rid_30" /><label for="rid_30">Moderate chance of dozing</label><br />
                        <input type="radio" name="grpRadio_4" value="High chance of dozing|32|3" id="rid_32" /><label for="rid_32">High chance of dozing</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID5">
                <td>
                    <strong>Lying down to rest in the afternoon when circumstances permit</strong>
                    <div>
                        <input type="radio" name="grpRadio_5" value="Would never doze|34|0" id="rid_34" /><label for="rid_34">Would never doze</label><br />
                        <input type="radio" name="grpRadio_5" value="Slight chance of dozing|36|1" id="rid_36" /><label for="rid_36">Slight chance of dozing</label><br />
                        <input type="radio" name="grpRadio_5" value="Moderate chance of dozing|38|2" id="rid_38" /><label for="rid_38">Moderate chance of dozing</label><br />
                        <input type="radio" name="grpRadio_5" value="High chance of dozing|40|3" id="rid_40" /><label for="rid_40">High chance of dozing</label><br />
                    </div>
                </td>
            </tr>


            <tr id="TID1_QID6">
                <td>
                    <strong>Sitting and talking to someone</strong>
                    <div>
                        <input type="radio" name="grpRadio_6" value="Would never doze|42|0" id="rid_42" /><label for="rid_42">Would never doze</label><br />
                        <input type="radio" name="grpRadio_6" value="Slight chance of dozing|44|1" id="rid_44" /><label for="rid_44">Slight chance of dozing</label><br />
                        <input type="radio" name="grpRadio_6" value="Moderate chance of dozing|46|2" id="rid_46" /><label for="rid_46">Moderate chance of dozing</label><br />
                        <input type="radio" name="grpRadio_6" value="High chance of dozing|48|3" id="rid_48" /><label for="rid_48">High chance of dozing</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID7">
                <td>
                    <strong>Sitting quietly after a lunch without alcohol</strong>
                    <div>
                        <input type="radio" name="grpRadio_7" value="Would never doze|50|0" id="rid_50" /><label for="rid_50">Would never doze</label><br />
                        <input type="radio" name="grpRadio_7" value="Slight chance of dozing|52|1" id="rid_52" /><label for="rid_52">Slight chance of dozing</label><br />
                        <input type="radio" name="grpRadio_7" value="Moderate chance of dozing|54|2" id="rid_54" /><label for="rid_54">Moderate chance of dozing</label><br />
                        <input type="radio" name="grpRadio_7" value="High chance of dozing|56|3" id="rid_56" /><label for="rid_56">High chance of dozing</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID8">
                <td>
                    <strong>In a car, while stopped for a few minutes in traffic</strong>
                    <div>
                        <input type="radio" name="grpRadio_8" value="Would never doze|58|0" id="rid_58" /><label for="rid_58">Would never doze</label><br />
                        <input type="radio" name="grpRadio_8" value="Slight chance of dozing|60|1" id="rid_60" /><label for="rid_60">Slight chance of dozing</label><br />
                        <input type="radio" name="grpRadio_8" value="Moderate chance of dozing|62|2" id="rid_62" /><label for="rid_62">Moderate chance of dozing</label><br />
                        <input type="radio" name="grpRadio_8" value="High chance of dozing|64|3" id="rid_64" /><label for="rid_64">High chance of dozing</label><br />
                    </div>
                    
                </td>
            </tr>


        </table>
        <br />
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_OnClick" />
    </div>
</asp:Content>

<asp:Content ID="cScripts" runat="server" ContentPlaceHolderID="cpScripts">
    <script src="js/questions.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            questions.init();
        });
    </script>
</asp:Content>