<%@ Page Title="Client Satisfaction Questionnaire (CSQ-8)" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid3028.aspx.cs" Inherits="mid3028" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

        <!-- this tells the code behind how many responces to process -->
        <input type="hidden" name="ResponseCount" value="9" /><br />

        <h1>Client Satisfaction Questionnaire (CSQ-8)</h1>
        <h4>These questions allow you to tell us whether or not you are satisfied with your care.<br />
            It will take about one minute to answer these questions.<br />
            After you have answered the questions, click submit. This information will not be shared with your provider.</h4>

        <table class="tbl-quest">
            <tr id="TID1_QID1">
                <td>
                    <strong>How would you rate the quality of service you received?</strong>
                    <div>
                        <input type="radio" name="grpRadio_1" value="Excellent|2|1" id="rid_2" /><label for="rid_2">Excellent</label><br />
                        <input type="radio" name="grpRadio_1" value="Good|4|2" id="rid_4" /><label for="rid_4">Good</label><br />
                        <input type="radio" name="grpRadio_1" value="Fair|6|3" id="rid_6" /><label for="rid_6">Fair</label><br />
                        <input type="radio" name="grpRadio_1" value="Poor|8|4" id="rid_8" /><label for="rid_8">Poor</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID2">
                <td>
                    <strong>Did you get the kind of service you wanted?</strong>
                    <div>
                        <input type="radio" name="grpRadio_2" value="No, definitively not|10|1" id="rid_10" /><label for="rid_10">No, definitively not</label><br />
                        <input type="radio" name="grpRadio_2" value="No, not really|12|2" id="rid_12" /><label for="rid_12">No, not really</label><br />
                        <input type="radio" name="grpRadio_2" value="Yes, generally|14|3" id="rid_14" /><label for="rid_14">Yes, generally</label><br />
                        <input type="radio" name="grpRadio_2" value="Yes, definitively|16|4" id="rid_16" /><label for="rid_16">Yes, definitively</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID3">
                <td>
                    <strong>To what extent has our program met your needs?</strong>
                    <div>
                        <input type="radio" name="grpRadio_3" value="Almost all of my needs have been met|18|1" id="rid_18" /><label for="rid_18">Almost all of my needs have been met</label><br />
                        <input type="radio" name="grpRadio_3" value="Most of my needs have been met|20|2" id="rid_20" /><label for="rid_20">Most of my needs have been met</label><br />
                        <input type="radio" name="grpRadio_3" value="Only a few of my needs have been met|22|3" id="rid_22" /><label for="rid_22">Only a few of my needs have been met</label><br />
                        <input type="radio" name="grpRadio_3" value="None of my needs have been met |24|4" id="rid_24" /><label for="rid_24">None of my needs have been met </label>
                        <br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID4">
                <td>
                    <strong>If a friend were in need of similar help, would you recommend our program to him or her?</strong>
                    <div>
                        <input type="radio" name="grpRadio_4" value="No, definitively not|26|1" id="rid_26" /><label for="rid_26">No, definitively not</label><br />
                        <input type="radio" name="grpRadio_4" value="No, not really|28|2" id="rid_28" /><label for="rid_28">No, not really</label><br />
                        <input type="radio" name="grpRadio_4" value="Yes, generally|30|3" id="rid_30" /><label for="rid_30">Yes, generally</label><br />
                        <input type="radio" name="grpRadio_4" value="Yes, definitively|32|4" id="rid_32" /><label for="rid_32">Yes, definitively</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID5">
                <td>
                    <strong>How satisfied are you with the amount of help you have received?</strong>
                    <div>
                        <input type="radio" name="grpRadio_5" value="Quite dissatisfied|34|1" id="rid_34" /><label for="rid_34">Quite dissatisfied</label><br />
                        <input type="radio" name="grpRadio_5" value="Indifferent or mildly dissatisfied|36|2" id="rid_36" /><label for="rid_36">Indifferent or mildly dissatisfied</label><br />
                        <input type="radio" name="grpRadio_5" value="Mostly satisfied|38|3" id="rid_38" /><label for="rid_38">Mostly satisfied</label><br />
                        <input type="radio" name="grpRadio_5" value="Very satisfied|40|4" id="rid_40" /><label for="rid_40">Very satisfied</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID6">
                <td>
                    <strong>Have the services you received helped you to deal more effectively with your problems?</strong>
                    <div>
                        <input type="radio" name="grpRadio_6" value="Yes, they helped a great deal|42|1" id="rid_42" /><label for="rid_42">Yes, they helped a great deal</label><br />
                        <input type="radio" name="grpRadio_6" value="Yes, they helped somewhat|44|2" id="rid_44" /><label for="rid_44">Yes, they helped somewhat</label><br />
                        <input type="radio" name="grpRadio_6" value="No, they really didn’t help|46|3" id="rid_46" /><label for="rid_46">No, they really didn’t help</label><br />
                        <input type="radio" name="grpRadio_6" value="No, they seemed to make things worse |48|4" id="rid_48" /><label for="rid_48">No, they seemed to make things worse </label>
                        <br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID7">
                <td>
                    <strong>In an overall, general sense, how satisfied are you with the service you have received?</strong>
                    <div>
                        <input type="radio" name="grpRadio_7" value="Very satisfied|50|1" id="rid_50" /><label for="rid_50">Very satisfied</label><br />
                        <input type="radio" name="grpRadio_7" value="Mostly satisfied|52|2" id="rid_52" /><label for="rid_52">Mostly satisfied</label><br />
                        <input type="radio" name="grpRadio_7" value="Indifferent or mildly dissatisfied|54|3" id="rid_54" /><label for="rid_54">Indifferent or mildly dissatisfied</label><br />
                        <input type="radio" name="grpRadio_7" value="Quite dissatisfied|56|4" id="rid_56" /><label for="rid_56">Quite dissatisfied</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID8">
                <td>
                    <strong>If you were to seek help again, would you come back to our program?</strong>
                    <div>
                        <input type="radio" name="grpRadio_8" value="No, definitively not|58|1" id="rid_58" /><label for="rid_58">No, definitively not</label><br />
                        <input type="radio" name="grpRadio_8" value="No, not really|60|2" id="rid_60" /><label for="rid_60">No, not really</label><br />
                        <input type="radio" name="grpRadio_8" value="Yes, generally|62|3" id="rid_62" /><label for="rid_62">Yes, generally</label><br />
                        <input type="radio" name="grpRadio_8" value="Yes, definitively |64|4" id="rid_64" /><label for="rid_64">Yes, definitively </label>
                        <br />
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