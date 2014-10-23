<%@ Page Title="Week Follow-up Questionnaire" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid2000.aspx.cs" Inherits="mid2000" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

        <!-- this tells the code behind how many responces to process -->
        <input type="hidden" name="ResponseCount" value="17" /><br />

        <h1>Week Follow-up Questionnaire </h1>
        <h3>You have had your automatic positive airway pressure device to treat your sleep apnea for about a week.<br />
            Tell us how you are doing!</h3>
        <table class="tbl-quest">

            <%--<tr id="TID1_QID1">
                <td>
                    <strong>Have you had any problems with your APAP device?  </strong>
                    <div>
                        <input type="radio" name="grpRadio_1" value="Yes|2|0" id="rid_2" /><label for="rid_2">Yes</label><br />
                        <input type="radio" name="grpRadio_1" value="No|4|0" id="rid_4" /><label for="rid_4">No</label><br />
                    </div>
                </td>
            </tr>--%>

            <tr id="TID1_QID2">
                <td>
                    <strong>Is the mask comfortable and fitting properly?</strong>
                    <div>
                        <input type="radio" name="grpRadio_2" value="Yes|6|0" id="rid_6" /><label for="rid_6">Yes</label><br />
                        <input type="radio" name="grpRadio_2" value="No|8|0" id="rid_8" /><label for="rid_8">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID3">
                <td>
                    <strong>Is the mask causing a skin rash or soreness?</strong>
                    <div>
                        <input type="radio" name="grpRadio_3" value="Yes|10|0" id="rid_10" /><label for="rid_10">Yes</label><br />
                        <input type="radio" name="grpRadio_3" value="No|12|0" id="rid_12" /><label for="rid_12">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID4">
                <td>
                    <strong>Does APAP make your nose stuffy or block breathing through your nose?</strong>
                    <div>
                        <input type="radio" name="grpRadio_4" value="Yes|14|0" id="rid_14" /><label for="rid_14">Yes</label><br />
                        <input type="radio" name="grpRadio_4" value="No|16|0" id="rid_16" /><label for="rid_16">No</label><br />
                    </div>
                </td>
            </tr>

            <%--<tr id="TID1_QID5">
                <td>
                    <strong>Are you using the ramp function when you turn on the APAP?</strong>
                    <div>
                        <input type="radio" name="grpRadio_5" value="Yes|18|0" id="rid_18" /><label for="rid_18">Yes</label><br />
                        <input type="radio" name="grpRadio_5" value="No|20|0" id="rid_20" /><label for="rid_20">No</label><br />
                    </div>
                </td>
            </tr>--%>

            <tr id="TID1_QID6">
                <td>
                    <strong>Are you using the humidifier in the APAP unit?</strong>
                    <div>
                        <input type="radio" name="grpRadio_6" value="Yes|22|0" id="rid_22" /><label for="rid_22">Yes</label><br />
                        <input type="radio" name="grpRadio_6" value="No|24|0" id="rid_24" /><label for="rid_24">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID7">
                <td>
                    <strong>Does the APAP cause dryness of your throat or nasal passage?</strong>
                    <div>
                        <input type="radio" name="grpRadio_7" value="Yes|26|0" id="rid_26" /><label for="rid_26">Yes</label><br />
                        <input type="radio" name="grpRadio_7" value="No|28|0" id="rid_28" /><label for="rid_28">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID8">
                <td>
                    <strong>Are you adding water to the humidifier chamber?  </strong>
                    <div>
                        <input type="radio" name="grpRadio_8" value="Yes|30|0" id="rid_30" /><label for="rid_30">Yes</label><br />
                        <input type="radio" name="grpRadio_8" value="No|32|0" id="rid_32" /><label for="rid_32">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID9">
                <td>
                    <strong>Do you know how to adjust the humidifier’s temperature?</strong>
                    <div>
                        <input type="radio" name="grpRadio_9" value="Yes|34|0" id="rid_34" /><label for="rid_34">Yes</label><br />
                        <input type="radio" name="grpRadio_9" value="No|36|0" id="rid_36" /><label for="rid_36">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID10">
                <td>
                    <strong>Are you changing the water daily?</strong>
                    <div>
                        <input type="radio" name="grpRadio_10" value="Yes|38|0" id="rid_38" /><label for="rid_38">Yes</label><br />
                        <input type="radio" name="grpRadio_10" value="No|40|0" id="rid_40" /><label for="rid_40">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID11">
                <td>
                    <strong>Do people tell you that you are snoring when you are sleeping with APAP?    </strong>
                    <div>
                        <input type="radio" name="grpRadio_11" value="Yes|42|0" id="rid_42" /><label for="rid_42">Yes</label><br />
                        <input type="radio" name="grpRadio_11" value="No|44|0" id="rid_44" /><label for="rid_44">No</label><br />
                        <input type="radio" name="grpRadio_11" value="Do not know|46|0" id="rid_46" /><label for="rid_46">Do not know</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID12">
                <td>
                    <strong>Has the quality of your sleep improved when you use APAP?</strong>
                    <div>
                        <input type="radio" name="grpRadio_12" value="Yes|48|0" id="rid_48" /><label for="rid_48">Yes</label><br />
                        <input type="radio" name="grpRadio_12" value="No|50|0" id="rid_50" /><label for="rid_50">No</label><br />
                        <input type="radio" name="grpRadio_12" value="Do not know|52|0" id="rid_52" /><label for="rid_52">Do not know</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID13">
                <td>
                    <strong>Has your daytime sleepiness improved on APAP treatment?</strong>
                    <div>
                        <input type="radio" name="grpRadio_13" value="Yes|54|0" id="rid_54" /><label for="rid_54">Yes</label><br />
                        <input type="radio" name="grpRadio_13" value="No|56|0" id="rid_56" /><label for="rid_56">No</label><br />
                        <input type="radio" name="grpRadio_13" value="Do not know|58|0" id="rid_58" /><label for="rid_58">Do not know</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID14">
                <td>
                    <strong>Overall, to what extent are you satisfied with APAP as treatment for your sleep apnea?</strong>
                    <div>
                        <input type="radio" name="grpRadio_14" value="Very unsatisfied|60|0" id="rid_60" /><label for="rid_60">Very unsatisfied</label><br />
                        <input type="radio" name="grpRadio_14" value="Unsatisfied|62|0" id="rid_62" /><label for="rid_62">Unsatisfied</label><br />
                        <input type="radio" name="grpRadio_14" value="Satisfied|64|0" id="rid_64" /><label for="rid_64">Satisfied</label><br />
                        <input type="radio" name="grpRadio_14" value="Very satisfied|66|0" id="rid_66" /><label for="rid_66">Very satisfied</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID15">
                <td>
                    <strong>Overall, to what extent is your bed partner satisfied with APAP as a treatment for your sleep apnea?</strong>
                    <div>
                        <input type="radio" name="grpRadio_15" value="Very unsatisfied|68|0" id="rid_68" /><label for="rid_68">Very unsatisfied</label><br />
                        <input type="radio" name="grpRadio_15" value="Unsatisfied|70|0" id="rid_70" /><label for="rid_70">Unsatisfied</label><br />
                        <input type="radio" name="grpRadio_15" value="Satisfied|72|0" id="rid_72" /><label for="rid_72">Satisfied</label><br />
                        <input type="radio" name="grpRadio_15" value="Very satisfied|74|0" id="rid_74" /><label for="rid_74">Very satisfied</label><br />
                        <input type="radio" name="grpRadio_15" value="Not applicable (no bed partner)|76|0" id="rid_76" /><label for="rid_76">Not applicable (no bed partner)</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID16">
                <td>
                    <strong>Have you had any other problems with your APAP device?<br />
                        If yes please explain in the comment box.</strong>
                    <div>
                        <input type="radio" name="grpRadio_16" value="Yes|78|0" id="rid_78" onclick="return toggleCommentBox();" /><label for="rid_78">Yes</label><br />
                        <input type="radio" name="grpRadio_16" value="No|80|0" id="rid_80" onclick="return toggleCommentBox();" /><label for="rid_80">No</label><br />
                        Comment:&nbsp;<input type="text" name="grpCtrlText_16" id="rid_txt_82" style="width: 80%;" />
                        <input type="hidden" name="grpHidden_16" id="rid_82" value="|82|0" />
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
            
            var radioYes = $('input[id$="rid_78"]')[0],
                radioNo = $('input[id$="rid_78"]')[0],
                box = $('input[id$="rid_txt_82"]')[0];

            $(box).val('').attr('disabled', 'disabled');
        });

        function toggleCommentBox() {
            var box = $('input[id$="rid_txt_82"]')[0],
                radioYes = $('input[id$="rid_78"]')[0];

            if (radioYes.checked) {
                $(box).removeAttr('disabled').focus();
            }
            else {
                $(box).val('').attr('disabled', 'disabled');
            }

            return true;
        }

    </script>
</asp:Content>