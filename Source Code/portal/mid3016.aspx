<%@ Page Title="Functional Outcomes of Sleep Questionnaire " Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid3016.aspx.cs" Inherits="mid3016" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

        <!-- this tells the code behind how many responces to process -->
        <input type="hidden" name="ResponseCount" value="11" /><br />

        <h1>Functional Outcomes of Sleep Questionnaire </h1>
        <h4>These questions will help us to determine how your sleep may be affecting your ability to function in the daytime.<br />
            It will take about one minute to answer these questions.<br />
            After you have answered the questions, click submit.</h4>
        <table class="tbl-quest">

            <tr id="TID1_QID1">
                <td>
                    <strong>Do you have difficulty concentrating on things you do because you are sleepy or tired?</strong>
                    <div>
                        <input type="radio" name="grpRadio_1" value="I don’t do this activity for other reasons|2|0" id="rid_2" /><label for="rid_2">I don’t do this activity for other reasons</label><br />
                        <input type="radio" name="grpRadio_1" value="Yes, extreme difficulty|4|1" id="rid_4" /><label for="rid_4">Yes, extreme difficulty</label><br />
                        <input type="radio" name="grpRadio_1" value="Yes, moderate difficulty|6|2" id="rid_6" /><label for="rid_6">Yes, moderate difficulty</label><br />
                        <input type="radio" name="grpRadio_1" value="Yes, a little difficulty|8|3" id="rid_8" /><label for="rid_8">Yes, a little difficulty</label><br />
                        <input type="radio" name="grpRadio_1" value="No difficulty|10|4" id="rid_10" /><label for="rid_10">No difficulty</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID2">
                <td>
                    <strong>Do you generally have difficulty remembering things because you are sleepy or tired?</strong>
                    <div>
                        <input type="radio" name="grpRadio_2" value="I don’t do this activity for other reasons|12|0" id="rid_12" /><label for="rid_12">I don’t do this activity for other reasons</label><br />
                        <input type="radio" name="grpRadio_2" value="Yes, extreme difficulty|14|1" id="rid_14" /><label for="rid_14">Yes, extreme difficulty</label><br />
                        <input type="radio" name="grpRadio_2" value="Yes, moderate difficulty|16|2" id="rid_16" /><label for="rid_16">Yes, moderate difficulty</label><br />
                        <input type="radio" name="grpRadio_2" value="Yes, a little difficulty|18|3" id="rid_18" /><label for="rid_18">Yes, a little difficulty</label><br />
                        <input type="radio" name="grpRadio_2" value="No difficulty|20|4" id="rid_20" /><label for="rid_20">No difficulty</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID3">
                <td>
                    <strong>Do you have difficulty operating a motor vehicle for short distances (less than 100 miles) because you become sleepy or tired?</strong>
                    <div>
                        <input type="radio" name="grpRadio_3" value="I don’t do this activity for other reasons|22|0" id="rid_22" /><label for="rid_22">I don’t do this activity for other reasons</label><br />
                        <input type="radio" name="grpRadio_3" value="Yes, extreme difficulty|24|1" id="rid_24" /><label for="rid_24">Yes, extreme difficulty</label><br />
                        <input type="radio" name="grpRadio_3" value="Yes, moderate difficulty|26|2" id="rid_26" /><label for="rid_26">Yes, moderate difficulty</label><br />
                        <input type="radio" name="grpRadio_3" value="Yes, a little difficulty|28|3" id="rid_28" /><label for="rid_28">Yes, a little difficulty</label><br />
                        <input type="radio" name="grpRadio_3" value="No difficulty|30|4" id="rid_30" /><label for="rid_30">No difficulty</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID4">
                <td>
                    <strong>Do you have difficulty operating a motor vehicle for long distances (greater than 100 miles) because you become sleepy or tired?</strong>
                    <div>
                        <input type="radio" name="grpRadio_4" value="I don’t do this activity for other reasons|32|0" id="rid_32" /><label for="rid_32">I don’t do this activity for other reasons</label><br />
                        <input type="radio" name="grpRadio_4" value="Yes, extreme difficulty|34|1" id="rid_34" /><label for="rid_34">Yes, extreme difficulty</label><br />
                        <input type="radio" name="grpRadio_4" value="Yes, moderate difficulty|36|2" id="rid_36" /><label for="rid_36">Yes, moderate difficulty</label><br />
                        <input type="radio" name="grpRadio_4" value="Yes, a little difficulty|38|3" id="rid_38" /><label for="rid_38">Yes, a little difficulty</label><br />
                        <input type="radio" name="grpRadio_4" value="No difficulty|40|4" id="rid_40" /><label for="rid_40">No difficulty</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID5">
                <td>
                    <strong>Do you have difficulty visiting with your family or friends in their homes because you become sleepy or tired?</strong>
                    <div>
                        <input type="radio" name="grpRadio_5" value="I don’t do this activity for other reasons|42|0" id="rid_42" /><label for="rid_42">I don’t do this activity for other reasons</label><br />
                        <input type="radio" name="grpRadio_5" value="Yes, extreme difficulty|44|1" id="rid_44" /><label for="rid_44">Yes, extreme difficulty</label><br />
                        <input type="radio" name="grpRadio_5" value="Yes, moderate difficulty|46|2" id="rid_46" /><label for="rid_46">Yes, moderate difficulty</label><br />
                        <input type="radio" name="grpRadio_5" value="Yes, a little difficulty|48|3" id="rid_48" /><label for="rid_48">Yes, a little difficulty</label><br />
                        <input type="radio" name="grpRadio_5" value="No difficulty|50|4" id="rid_50" /><label for="rid_50">No difficulty</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID6">
                <td>
                    <strong>Has your relationship with family, friends or work colleagues been affected because you are sleepy or tired?</strong>
                    <div>
                        <input type="radio" name="grpRadio_6" value="I don’t do this activity for other reasons|52|0" id="rid_52" /><label for="rid_52">I don’t do this activity for other reasons</label><br />
                        <input type="radio" name="grpRadio_6" value="Yes, extremely  affected|54|1" id="rid_54" /><label for="rid_54">Yes, extremely  affected</label><br />
                        <input type="radio" name="grpRadio_6" value="Yes, moderate affected|56|2" id="rid_56" /><label for="rid_56">Yes, moderate affected</label><br />
                        <input type="radio" name="grpRadio_6" value="Yes, a little affected|58|3" id="rid_58" /><label for="rid_58">Yes, a little affected</label><br />
                        <input type="radio" name="grpRadio_6" value="Not affected|60|4" id="rid_60" /><label for="rid_60">Not affected</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID7">
                <td>
                    <strong>Do you have difficulty watching a movie or videotape because you become sleepy or tired?</strong>
                    <div>
                        <input type="radio" name="grpRadio_7" value="I don’t do this activity for other reasons|62|0" id="rid_62" /><label for="rid_62">I don’t do this activity for other reasons</label><br />
                        <input type="radio" name="grpRadio_7" value="Yes, extreme difficulty|64|1" id="rid_64" /><label for="rid_64">Yes, extreme difficulty</label><br />
                        <input type="radio" name="grpRadio_7" value="Yes, moderate difficulty|66|2" id="rid_66" /><label for="rid_66">Yes, moderate difficulty</label><br />
                        <input type="radio" name="grpRadio_7" value="Yes, a little difficulty|68|3" id="rid_68" /><label for="rid_68">Yes, a little difficulty</label><br />
                        <input type="radio" name="grpRadio_7" value="No difficulty|70|4" id="rid_70" /><label for="rid_70">No difficulty</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID8">
                <td>
                    <strong>Do you have difficulty being as active as you want to be in the evening because you are sleepy or tired?</strong>
                    <div>
                        <input type="radio" name="grpRadio_8" value="I don’t do this activity for other reasons|72|0" id="rid_72" /><label for="rid_72">I don’t do this activity for other reasons</label><br />
                        <input type="radio" name="grpRadio_8" value="Yes, extreme difficulty|74|1" id="rid_74" /><label for="rid_74">Yes, extreme difficulty</label><br />
                        <input type="radio" name="grpRadio_8" value="Yes, moderate difficulty|76|2" id="rid_76" /><label for="rid_76">Yes, moderate difficulty</label><br />
                        <input type="radio" name="grpRadio_8" value="Yes, a little difficulty|78|3" id="rid_78" /><label for="rid_78">Yes, a little difficulty</label><br />
                        <input type="radio" name="grpRadio_8" value="No difficulty|80|4" id="rid_80" /><label for="rid_80">No difficulty</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID9">
                <td>
                    <strong>Do you have difficulty being as active as you want to be in the morning because you are sleepy or tired?</strong>
                    <div>
                        <input type="radio" name="grpRadio_9" value="I don’t do this activity for other reasons|82|0" id="rid_82" /><label for="rid_82">I don’t do this activity for other reasons</label><br />
                        <input type="radio" name="grpRadio_9" value="Yes, extreme difficulty|84|1" id="rid_84" /><label for="rid_84">Yes, extreme difficulty</label><br />
                        <input type="radio" name="grpRadio_9" value="Yes, moderate difficulty|86|2" id="rid_86" /><label for="rid_86">Yes, moderate difficulty</label><br />
                        <input type="radio" name="grpRadio_9" value="Yes, a little difficulty|88|3" id="rid_88" /><label for="rid_88">Yes, a little difficulty</label><br />
                        <input type="radio" name="grpRadio_9" value="No difficulty|90|4" id="rid_90" /><label for="rid_90">No difficulty</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID10">
                <td>
                    <strong>Has your desire for intimacy or sex been affected because you are sleepy or tired?</strong>
                    <div>
                        <input type="radio" name="grpRadio_10" value="I don’t do this activity for other reasons|92|0" id="rid_92" /><label for="rid_92">I don’t do this activity for other reasons</label><br />
                        <input type="radio" name="grpRadio_10" value="Yes, extremely affected|94|1" id="rid_94" /><label for="rid_94">Yes, extremely affected</label><br />
                        <input type="radio" name="grpRadio_10" value="Yes, moderately affected|96|2" id="rid_96" /><label for="rid_96">Yes, moderately affected</label><br />
                        <input type="radio" name="grpRadio_10" value="Yes, a little affected|98|3" id="rid_98" /><label for="rid_98">Yes, a little affected</label><br />
                        <input type="radio" name="grpRadio_10" value="Not affected|100|4" id="rid_100" /><label for="rid_100">Not affected</label><br />
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