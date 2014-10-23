<%@ Page Title="Pittsburgh Sleep Quality Index - PSQI" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid3014.aspx.cs" Inherits="mid3014" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link rel="stylesheet" type="text/css" href="css/jquery.ui.timepicker.css" />
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

            <!-- this tells the code behind how many responces to process -->
            <input type="hidden" name="ResponseCount" value="22" />
            <br />
            <h1>Pittsburgh Sleep Quality Index - PSQI </h1>
            <h4>These questions will help us to determine the quality of your sleep.<br />
                It will take about two minutes to answer these questions.<br />
                After you have answered the questions, click submit.</h4>
            <table class="tbl-quest">
                <tr id="TID1_QID1">
                    <td><strong>During the past month, when have you usually gone to bed at night?</strong>
                        <div>
                            <input type="text" name="grpCtrlText_1" id="rid_txt_2" maxlength="5" size="8" />
                            <input type="hidden" name="grpHidden_1" id="rid_2" value="|2|0" />
                        </div>
                    </td>
                </tr>
                <tr id="TID1_QID2">
                    <td><strong>During the past month, how long has it usually taken you to fall asleep each night?</strong>
                        <div>
                            <input type="text" name="grpCtrlText_2" id="rid_txt_4" size="5" />&nbsp;minutes
                            <input type="hidden" name="grpHidden_2" id="rid_4" value="minutes|4|0" />
                        </div>
                    </td>
                </tr>
                <tr id="TID1_QID3">
                    <td><strong>During the past month, when have you usually gotten up in the morning?</strong>
                        <div>
                            <input type="text" name="grpCtrlText_3" id="rid_txt_6" maxlength="5" size="8" />
                            <input type="hidden" name="grpHidden_3" id="rid_6" value="|6|0" />
                        </div>
                    </td>
                </tr>
                <tr id="TID1_QID4">
                    <td><strong>During the past month, how many hours of actual sleep did you get at night?</strong>
                        <div>
                            <input type="text" name="grpCtrlText_4" id="rid_txt_8"  size="5" />&nbsp;hours per night
                            <input type="hidden" name="grpHidden_4" id="rid_8" value="hours per night|8|0" />
                        </div>
                    </td>
                </tr>
            </table>
            <h3>During the past month, how often have you had trouble sleeping because you.....</h3>
            <table class="tbl-quest">
                <tr id="TID2_QID1">
                    <td>
                        <div>
                            <strong>Cannot get to sleep within 30 minutes</strong>&nbsp;
          <select id="rid_10" name="grpCombo_5">
              <option value=""></option>
              <option value="Not during the past month|10|0">Not during the past month </option>
              <option value="Less than once a week|10|1">Less than once a week </option>
              <option value="Once or twice a week|10|2">Once or twice a week </option>
              <option value="Three or more times a week|10|3">Three or more times a week </option>
          </select>
                        </div>
                    </td>
                </tr>
                <tr id="TID2_QID2">
                    <td>
                        <div>
                            <strong>Wake up in the middle of the night or early morning</strong>&nbsp;
          <select id="rid_12" name="grpCombo_6">
              <option value=""></option>
              <option value="Not during the past month|12|0">Not during the past month </option>
              <option value="Less than once a week|12|1">Less than once a week </option>
              <option value="Once or twice a week|12|2">Once or twice a week </option>
              <option value="Three or more times a week|12|3">Three or more times a week </option>
          </select>
                        </div>
                    </td>
                </tr>
                <tr id="TID2_QID3">
                    <td>
                        <div>
                            <strong>Have to go to the bathroom</strong>&nbsp;
          <select id="rid_14" name="grpCombo_7">
              <option value=""></option>
              <option value="Not during the past month|14|0">Not during the past month </option>
              <option value="Less than once a week|14|1">Less than once a week </option>
              <option value="Once or twice a week|14|2">Once or twice a week </option>
              <option value="Three or more times a week|14|3">Three or more times a week </option>
          </select>
                        </div>
                    </td>
                </tr>
                <tr id="TID2_QID4">
                    <td>
                        <div>
                            <strong>Cannot breathe comfortably</strong>&nbsp;
          <select id="rid_16" name="grpCombo_8">
              <option value=""></option>
              <option value="Not during the past month|16|0">Not during the past month </option>
              <option value="Less than once a week|16|1">Less than once a week </option>
              <option value="Once or twice a week|16|2">Once or twice a week </option>
              <option value="Three or more times a week|16|3">Three or more times a week </option>
          </select>
                        </div>
                    </td>
                </tr>
                <tr id="TID2_QID5">
                    <td>
                        <div>
                            <strong>Cough or snore loudly</strong>&nbsp;
          <select id="rid_18" name="grpCombo_9">
              <option value=""></option>
              <option value="Not during the past month|18|0">Not during the past month </option>
              <option value="Less than once a week|18|1">Less than once a week </option>
              <option value="Once or twice a week|18|2">Once or twice a week </option>
              <option value="Three or more times a week|18|3">Three or more times a week </option>
          </select>
                        </div>
                    </td>
                </tr>
                <tr id="TID2_QID6">
                    <td>
                        <div>
                            <strong>Feel too cold</strong>&nbsp;
          <select id="rid_20" name="grpCombo_10">
              <option value=""></option>
              <option value="Not during the past month|20|0">Not during the past month </option>
              <option value="Less than once a week|20|1">Less than once a week </option>
              <option value="Once or twice a week|20|2">Once or twice a week </option>
              <option value="Three or more times a week|20|3">Three or more times a week </option>
          </select>
                        </div>
                    </td>
                </tr>
                <tr id="TID2_QID7">
                    <td>
                        <div>
                            <strong>Feel too hot</strong>&nbsp;
          <select id="rid_22" name="grpCombo_11">
              <option value=""></option>
              <option value="Not during the past month|22|0">Not during the past month </option>
              <option value="Less than once a week|22|1">Less than once a week </option>
              <option value="Once or twice a week|22|2">Once or twice a week </option>
              <option value="Three or more times a week|22|3">Three or more times a week </option>
          </select>
                        </div>
                    </td>
                </tr>
                <tr id="TID2_QID8">
                    <td>
                        <div>
                            <strong>Had bad dreams</strong>&nbsp;
          <select id="rid_24" name="grpCombo_12">
              <option value=""></option>
              <option value="Not during the past month|24|0">Not during the past month </option>
              <option value="Less than once a week|24|1">Less than once a week </option>
              <option value="Once or twice a week|24|2">Once or twice a week </option>
              <option value="Three or more times a week|24|3">Three or more times a week </option>
          </select>
                        </div>
                    </td>
                </tr>
                <tr id="TID2_QID9">
                    <td>
                        <div>
                            <strong>Have pain</strong>&nbsp;
                            <select id="rid_26" name="grpCombo_13">
                                <option value=""></option>
                                <option value="Not during the past month|26|0">Not during the past month </option>
                                <option value="Less than once a week|26|1">Less than once a week </option>
                                <option value="Once or twice a week|26|2">Once or twice a week </option>
                                <option value="Three or more times a week|26|3">Three or more times a week </option>
                            </select>
                        </div>
                    </td>
                </tr>
                <tr id="TID2_QID10" bypass="bypass">
                    <td>
                        <div>
                            <strong>Other reason(s), please describe:</strong>&nbsp;
                            <input type="text" name="grpCtrlText_14" id="rid_txt_28" size="64" />
                            <input type="hidden" name="grpHidden_14" id="rid_28" value="|28|1" /><br />
                        </div>
                    </td>
                </tr>
            </table>
            <h3></h3>
            <table class="tbl-quest">
                <tr id="TID3_QID1">
                    <td><strong>During the past month, how would you rate your sleep quality overall?</strong>
                        <div>
                            <input type="radio" name="grpRadio_15" value="Very Good|30|0" id="rid_30" />
                            <label for="rid_30">Very Good</label>
                            <br />
                            <input type="radio" name="grpRadio_15" value="Fairly Good|32|1" id="rid_32" />
                            <label for="rid_32">Fairly Good</label>
                            <br />
                            <input type="radio" name="grpRadio_15" value="Fairly Bad|34|2" id="rid_34" />
                            <label for="rid_34">Fairly Bad</label>
                            <br />
                            <input type="radio" name="grpRadio_15" value="Very Bad|36|3" id="rid_36" />
                            <label for="rid_36">Very Bad</label>
                            <br />
                        </div>
                    </td>
                </tr>
                <tr id="TID3_QID2">
                    <td><strong>During the past month, how often have you taken medicine (prescribed or “over the counter”) to help you sleep?</strong>
                        <div>
                            <input type="radio" name="grpRadio_16" value="Not during the past month|38|0" id="rid_38" />
                            <label for="rid_38">Not during the past month</label>
                            <br />
                            <input type="radio" name="grpRadio_16" value="Less than once a week|40|1" id="rid_40" />
                            <label for="rid_40">Less than once a week</label>
                            <br />
                            <input type="radio" name="grpRadio_16" value="Once or twice a week|42|2" id="rid_42" />
                            <label for="rid_42">Once or twice a week</label>
                            <br />
                            <input type="radio" name="grpRadio_16" value="Three or more times a week|44|3" id="rid_44" />
                            <label for="rid_44">Three or more times a week</label>
                            <br />
                        </div>
                    </td>
                </tr>
                <tr id="TID3_QID3">
                    <td><strong>During the past month, how often have you had trouble staying awake while driving, eating meals, or engaging in social activity?</strong>
                        <div>
                            <input type="radio" name="grpRadio_17" value="Not during the past month|46|0" id="rid_46" />
                            <label for="rid_46">Not during the past month</label>
                            <br />
                            <input type="radio" name="grpRadio_17" value="Less than once a week|48|1" id="rid_48" />
                            <label for="rid_48">Less than once a week</label>
                            <br />
                            <input type="radio" name="grpRadio_17" value="Once or twice a week|50|2" id="rid_50" />
                            <label for="rid_50">Once or twice a week</label>
                            <br />
                            <input type="radio" name="grpRadio_17" value="Three or more times a week|52|3" id="rid_52" />
                            <label for="rid_52">Three or more times a week</label>
                            <br />
                        </div>
                    </td>
                </tr>
                <tr id="TID3_QID4">
                    <td><strong>During the past month, how much of a problem has it been for you to keep up enough enthusiasm to get things done?</strong>
                        <div>
                            <input type="radio" name="grpRadio_18" value="No problem at all|54|0" id="rid_54" />
                            <label for="rid_54">No problem at all</label>
                            <br />
                            <input type="radio" name="grpRadio_18" value="Only a slight problem|56|1" id="rid_56" />
                            <label for="rid_56">Only a slight problem</label>
                            <br />
                            <input type="radio" name="grpRadio_18" value="Somewhat of a problem|58|2" id="rid_58" />
                            <label for="rid_58">Somewhat of a problem</label>
                            <br />
                            <input type="radio" name="grpRadio_18" value="A very big problem|60|3" id="rid_60" />
                            <label for="rid_60">A very big problem</label>
                            <br />
                        </div>
                    </td>
                </tr>
                <tr id="TID3_QID5">
                    <td><strong>Do you have a bed partner or roommate?</strong>
                        <div>
                            <input type="radio" name="grpRadio_19" value="No bed partner or roommate|62|0" id="rid_62" />
                            <label for="rid_62">No bed partner or roommate</label>
                            <br />
                            <input type="radio" name="grpRadio_19" value="Partner/roommate in other room|64|1" id="rid_64" />
                            <label for="rid_64">Partner/roommate in other room</label>
                            <br />
                            <input type="radio" name="grpRadio_19" value="Partner/roommate in same room, but not in same bed|66|2" id="rid_66" />
                            <label for="rid_66">Partner/roommate in same room, but not in same bed</label>
                            <br />
                            <input type="radio" name="grpRadio_19" value="Partner/roommate in same bed|68|3" id="rid_68" />
                            <label for="rid_68">Partner/roommate in same bed</label>
                            <br />
                        </div>
                    </td>
                </tr>
                <tr id="TID4_QID1">
                    <td>
                        <h4>Ask him/her how often, in the past month, you have had (please check one per question):</h4>
                        <strong>Loud snoring</strong>
                        <div>
                            <input type="radio" name="grpRadio_20" 
                                value="Loud snoring - Not during the past month|70|0" id="rid_70" />
                            <label for="rid_70">Not during the past month</label>
                            <br />
                            <input type="radio" name="grpRadio_20" 
                                value="Loud snoring - Less than once a week|72|1" id="rid_72" />
                            <label for="rid_72">Less than once a week</label>
                            <br />
                            <input type="radio" name="grpRadio_20" 
                                value="Loud snoring - Once or twice a week|74|2" id="rid_74" />
                            <label for="rid_74">Once or twice a week</label>
                            <br />
                            <input type="radio" name="grpRadio_20" 
                                value="Loud snoring - Three or more times a week|76|3" id="rid_76" />
                            <label for="rid_76">Three or more times a week</label>
                            <br />
                        </div>
                    </td>
                </tr>
                <tr id="TID4_QID2">
                    <td>
                        <strong>Long pauses between breaths while asleep</strong>
                        <div>
	                        <input type="radio" name="grpRadio_21" 
		                        value="Long pauses between breaths while asleep - Not during the past month|78|0" id="rid_78" />
	                        <label for="rid_78">Not during the past month</label>
	                        <br />
	                        <input type="radio" name="grpRadio_21" 
		                        value="Long pauses between breaths while asleep - Less than once a week|80|1" id="rid_80" />
	                        <label for="rid_80">Less than once a week</label>
	                        <br />
	                        <input type="radio" name="grpRadio_21" 
		                        value="Long pauses between breaths while asleep - Once or twice a week|82|2" id="rid_82" />
	                        <label for="rid_82">Once or twice a week</label>
	                        <br />
	                        <input type="radio" name="grpRadio_21" 
		                        value="Long pauses between breaths while asleep - Three or more times a week|84|3" id="rid_84" />
	                        <label for="rid_84">Three or more times a week</label>
	                        <br />
                        </div>
                    </td>
                </tr>
                <tr id="TID4_QID3">
                    <td>
                        <strong>Legs twitching or jerking</strong>
                        <div>
	                        <input type="radio" name="grpRadio_22" 
		                        value="Legs twitching or jerking - Not during the past month|86|0" id="rid_86" />
	                        <label for="rid_86">Not during the past month</label>
	                        <br />
	                        <input type="radio" name="grpRadio_22" 
		                        value="Legs twitching or jerking - Less than once a week|88|1" id="rid_88" />
	                        <label for="rid_88">Less than once a week</label>
	                        <br />
	                        <input type="radio" name="grpRadio_22" 
		                        value="Legs twitching or jerking - Once or twice a week|90|2" id="rid_90" />
	                        <label for="rid_90">Once or twice a week</label>
	                        <br />
	                        <input type="radio" name="grpRadio_22" 
		                        value="Legs twitching or jerking - Three or more times a week|92|3" id="rid_92" />
	                        <label for="rid_92">Three or more times a week</label>
	                        <br />
                        </div>
                    </td>
                </tr>
                <tr id="TID4_QID4">
                    <td>
                        <strong>Episodes of disorientation or confusion during sleep</strong>
                        <div>
	                        <input type="radio" name="grpRadio_23" 
		                        value="Episodes of disorientation or confusion during sleep - Not during the past month|94|0" id="rid_94" />
	                        <label for="rid_94">Not during the past month</label>
	                        <br />
	                        <input type="radio" name="grpRadio_23" 
		                        value="Episodes of disorientation or confusion during sleep - Less than once a week|96|1" id="rid_96" />
	                        <label for="rid_96">Less than once a week</label>
	                        <br />
	                        <input type="radio" name="grpRadio_23" 
		                        value="Episodes of disorientation or confusion during sleep - Once or twice a week|98|2" id="rid_98" />
	                        <label for="rid_98">Once or twice a week</label>
	                        <br />
	                        <input type="radio" name="grpRadio_23" 
		                        value="Episodes of disorientation or confusion during sleep - Three or more times a week|100|3" id="rid_100" />
	                        <label for="rid_100">Three or more times a week</label>
	                        <br />
                        </div>
                    </td>
                </tr>
                <tr id="TID4_QID5"  bypass="bypass">
                    <td>
                        <strong>Other restlessness while you sleep, please describe:</strong><br />
                        <input type="text" name="grpCtrlText_24" id="rid_txt_102" size="64" />
                        <input type="hidden" name="grpHidden_24" id="rid_102" value="|102|0" /><br />
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_OnClick" Text="Submit" />
        </div>
</asp:Content>

<asp:Content ID="cScripts" runat="server" ContentPlaceHolderID="cpScripts">
    <script src="js/jquery.ui.timepicker.js"></script>
    <script src="js/questions.js"></script>
    <script type="text/javascript">
        questions.opts.txtMasks = [
            { rid: 2, mask: 'HHmm24h', maxlength: 5 },
            { rid: 4, mask: 'numbersOnly', maxlength: 3, allowDecimal: false },
            { rid: 6, mask: 'HHmm24h', maxlength: 5 },
            { rid: 8, mask: 'numbersOnly', maxlength: 4, allowDecimal: true }
        ];

        questions.opts.skipPatterns = [
            {
                rid: '64|66|68',
                checked: {
                    show: ['TID4_QID1', 'TID4_QID2', 'TID4_QID3', 'TID4_QID4', 'TID4_QID5']
                }
            }
        ];

        $(document).ready(function () {
            questions.opts.validation.dropdown.minIndex = 1;
            questions.init();

            $('#rid_txt_2, #rid_txt_6').timepicker({
                showPeriodLabels: false,
                showNowButton: false,
                showDeselectButton: true,
                defaultTime: '',  // removes the highlighted time for when the input is empty.
                showCloseButton: true
            });
        });

        // *******************************************************************************************************

        questions.checkRequiredAnswers = function () {
            var _me = this,
                _allTRs = $('tr[id^="TID"]'),
                _trs = $('tr[id^="TID"]').not('[skipped]'),
                _validate = true;

            //clear previous error messages
            $('td', _allTRs).css({
                'background-color': '#fff'
            });
            $('div.err-caption', _allTRs).remove();

            $.each(_trs, function (i, t) {
                var ansCount = 0,
                    _bTimeFormat = true,
                    _msg = 'A response for this question is required!';

                $('input[type="radio"], input[type="checkbox"]', t).each(function () {
                    if (this.checked) {
                        ansCount = ansCount + 1;
                    }
                });

                $('input[type="text"]', t).each(function () {
                    if (this.value.length > 0) {
                        ansCount = ansCount + 1;

                        if (t.id == "TID1_QID1" || t.id == "TID1_QID3") {
                            $('#rid_txt_2, #rid_txt_6', t).each(function () {
                                var reTest1 = /([1-9]:[0-5]\d)/gi,
                                    reTest2 = /(([01]\d|2[0-3]):([0-5]\d))/gi;

                                if (this.value.length > 5) {
                                    _bTimeFormat = false;
                                }
                                else {
                                    if (reTest1.test(this.value) || reTest2.test(this.value)) {
                                        _bTimeFormat = true;
                                    }
                                    else {
                                        _bTimeFormat = false;
                                    }
                                }
                            });

                            if (!_bTimeFormat) {
                                ansCount = ansCount - 1;
                                _msg = 'Please enter time using this format: HH:mm<br />For the Hour use range from 00 to 23, where 13 is 1PM.';
                            }
                        }
                    }
                });

                $('select', t).each(function () {

                    if (this.selectedIndex >= _me.opts.validation.dropdown.minIndex) {
                        ansCount = ansCount + 1;
                    }

                });

                //if count of responses < 1 then display error message
                if (ansCount < 1) {
                    if (!t.getAttribute("bypass")) {
                        _validate = false;

                        $('td:first', t).each(function (z, x) {
                            $(x).prepend('<div class="err-caption"><span style="color:Red;">'+ _msg +'</span></div>');
                            $('td', $(x).parent('tr')).css({
                                'background-color': '#F9FC9D'
                            });
                        });
                    }
                }
            });

            return _validate;
        };

    </script>
</asp:Content>