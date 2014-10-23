<%@ Page Title="MAP SLEEP SYMPTOM-FREQUENCY QUESTIONNAIRE" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid3002.aspx.cs" Inherits="mid3002" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

        <!-- this tells the code behind how many responces to process -->
        <input type="hidden" name="ResponseCount" value="10" /><br />

        <h1>MAP Sleep Symptom-Frequency Questionaire</h1>
        <h3>During the last month on how many nights or days per week have you had or been told you had the following (please select one response per question)?</h3>
        <table class="tbl-quest">
            <tr id="TID1_QID1">
                <td>
                    <strong>Loud snoring</strong>
                    <div>
                        <input type="radio" name="grpRadio_1" value="never|2|0" id="rid_2" /><label for="rid_2">never</label><br />
                        <input type="radio" name="grpRadio_1" value="rarely (less than once per week)|4|1" id="rid_4" /><label for="rid_4">rarely (less than once per week)</label><br />
                        <input type="radio" name="grpRadio_1" value="sometimes (1-2 times per week)|6|2" id="rid_6" /><label for="rid_6">sometimes (1-2 times per week)</label><br />
                        <input type="radio" name="grpRadio_1" value="frequently (3-4 times per week)|8|3" id="rid_8" /><label for="rid_8">frequently (3-4 times per week)</label><br />
                        <input type="radio" name="grpRadio_1" value="always (5-7 times per week)|10|4" id="rid_10" /><label for="rid_10">always (5-7 times per week)</label><br />
                        <input type="radio" name="grpRadio_1" value="do not know|12|0" id="rid_12" /><label for="rid_12">do not know</label><br />
                    </div>
                </td>
            </tr>
            <tr id="TID1_QID2">
                <td>
                    <strong>Snorting or gasping</strong>
                    <div>
                        <input type="radio" name="grpRadio_2" value="never|22|0" id="rid_22" /><label for="rid_22">never</label><br />
                        <input type="radio" name="grpRadio_2" value="rarely (less than once per week)|24|1" id="rid_24" /><label for="rid_24">rarely (less than once per week)</label><br />
                        <input type="radio" name="grpRadio_2" value="sometimes (1-2 times per week)|26|2" id="rid_26" /><label for="rid_26">sometimes (1-2 times per week)</label><br />
                        <input type="radio" name="grpRadio_2" value="frequently (3-4 times per week)|28|3" id="rid_28" /><label for="rid_28">frequently (3-4 times per week)</label><br />
                        <input type="radio" name="grpRadio_2" value="always (5-7 times per week)|210|4" id="rid_210" /><label for="rid_210">always (5-7 times per week)</label><br />
                        <input type="radio" name="grpRadio_2" value="do not know|212|0" id="rid_212" /><label for="rid_212">do not know</label><br />
                    </div>
                </td>
            </tr>
            <tr id="TID1_QID3">
                <td>
                    <strong>Your breathing stops or you choke or struggle for breath</strong>
                    <div>
                        <input type="radio" name="grpRadio_3" value="never|32|0" id="rid_32" /><label for="rid_32">never</label><br />
                        <input type="radio" name="grpRadio_3" value="rarely (less than once per week)|34|1" id="rid_34" /><label for="rid_34">rarely (less than once per week)</label><br />
                        <input type="radio" name="grpRadio_3" value="sometimes (1-2 times per week)|36|2" id="rid_36" /><label for="rid_36">sometimes (1-2 times per week)</label><br />
                        <input type="radio" name="grpRadio_3" value="frequently (3-4 times per week)|38|3" id="rid_38" /><label for="rid_38">frequently (3-4 times per week)</label><br />
                        <input type="radio" name="grpRadio_3" value="always (5-7 times per week)|310|4" id="rid_310" /><label for="rid_310">always (5-7 times per week)</label><br />
                        <input type="radio" name="grpRadio_3" value="do not know|312|0" id="rid_312" /><label for="rid_312">do not know</label><br />
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
