<%@ Page Title="Insomnia Severity Index - ISI" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid3004.aspx.cs" Inherits="mid3004" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

        <!-- this tells the code behind how many responces to process -->
        <input type="hidden" name="ResponseCount" value="8" /><br />

        <h1>Insomnia Severity Index - ISI</h1>
        <h4>These questions will help us to know whether or not you have insomnia.<br />
            It will take about one minute to answer these questions.<br />
            After you have answered the questions, click submit.</h4>
        <table class="tbl-quest">

            <tr id="TID1_QID1">
                <td>
                    <strong>Please rate the current (i.e., last 2 weeks) SEVERITY of your difficulty falling asleep.</strong>
                    <div>
                        <input type="radio" name="grpRadio_1" value="none|2|0" id="rid_2" /><label for="rid_2">none</label><br />
                        <input type="radio" name="grpRadio_1" value="mild|4|1" id="rid_4" /><label for="rid_4">mild</label><br />
                        <input type="radio" name="grpRadio_1" value="moderate|6|2" id="rid_6" /><label for="rid_6">moderate</label><br />
                        <input type="radio" name="grpRadio_1" value="severe|8|3" id="rid_8" /><label for="rid_8">severe</label><br />
                        <input type="radio" name="grpRadio_1" value="very severe|10|4" id="rid_10" /><label for="rid_10">very severe</label>
                        <br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID2">
                <td>
                    <strong>Please rate the current (i.e., last 2 weeks) SEVERITY of your difficulty staying asleep.</strong>
                    <div>
                        <input type="radio" name="grpRadio_2" value="none|12|0" id="rid_12" /><label for="rid_12">none</label><br />
                        <input type="radio" name="grpRadio_2" value="mild|14|1" id="rid_14" /><label for="rid_14">mild</label><br />
                        <input type="radio" name="grpRadio_2" value="moderate|16|2" id="rid_16" /><label for="rid_16">moderate</label><br />
                        <input type="radio" name="grpRadio_2" value="severe|18|3" id="rid_18" /><label for="rid_18">severe</label><br />
                        <input type="radio" name="grpRadio_2" value="very severe|20|4" id="rid_20" /><label for="rid_20">very severe </label>
                        <br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID3">
                <td>
                    <strong>Please rate the current (i.e., last 2 weeks) SEVERITY of your problem waking up too early.</strong>
                    <div>
                        <input type="radio" name="grpRadio_3" value="none|22|0" id="rid_22" /><label for="rid_22">none</label><br />
                        <input type="radio" name="grpRadio_3" value="mild|24|1" id="rid_24" /><label for="rid_24">mild</label><br />
                        <input type="radio" name="grpRadio_3" value="moderate|26|2" id="rid_26" /><label for="rid_26">moderate</label><br />
                        <input type="radio" name="grpRadio_3" value="severe|28|3" id="rid_28" /><label for="rid_28">severe</label><br />
                        <input type="radio" name="grpRadio_3" value="very severe|30|4" id="rid_30" /><label for="rid_30">very severe </label>
                        <br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID4">
                <td>
                    <strong>How SATISFIED/dissatisfied are you with your current sleep pattern?</strong>
                    <div>
                        <input type="radio" name="grpRadio_4" value="0 = very satisfied|32|0" id="rid_32" /><label for="rid_32">0 = very satisfied</label><br />
                        <input type="radio" name="grpRadio_4" value="1|34|1" id="rid_34" /><label for="rid_34">1</label><br />
                        <input type="radio" name="grpRadio_4" value="2|36|2" id="rid_36" /><label for="rid_36">2</label><br />
                        <input type="radio" name="grpRadio_4" value="3|38|3" id="rid_38" /><label for="rid_38">3</label><br />
                        <input type="radio" name="grpRadio_4" value="4 = very dissatisfied|40|4" id="rid_40" /><label for="rid_40">4 = very dissatisfied</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID5">
                <td>
                    <strong>To what extent do you consider your sleep problem to INTERFERE with your daily functioning (e.g. daytime fatigue, ability to function at work/daily chores, concentration, memory, mood, etc.).</strong>
                    <div>
                        <input type="radio" name="grpRadio_5" value="not at all interfering|42|0" id="rid_42" /><label for="rid_42">not at all interfering</label><br />
                        <input type="radio" name="grpRadio_5" value="a little|44|1" id="rid_44" /><label for="rid_44">a little</label><br />
                        <input type="radio" name="grpRadio_5" value="somewhat|46|2" id="rid_46" /><label for="rid_46">somewhat</label><br />
                        <input type="radio" name="grpRadio_5" value="much|48|3" id="rid_48" /><label for="rid_48">much</label><br />
                        <input type="radio" name="grpRadio_5" value="very much interfering|50|4" id="rid_50" /><label for="rid_50">very much interfering</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID6">
                <td>
                    <strong>How NOTICEABLE to others do you think your sleeping problem is in terms of impairing the quality of your life?</strong>
                    <div>
                        <input type="radio" name="grpRadio_6" value="not at all noticeable|52|0" id="rid_52" /><label for="rid_52">not at all noticeable</label><br />
                        <input type="radio" name="grpRadio_6" value="barely|54|1" id="rid_54" /><label for="rid_54">barely</label><br />
                        <input type="radio" name="grpRadio_6" value="somewhat|56|2" id="rid_56" /><label for="rid_56">somewhat</label><br />
                        <input type="radio" name="grpRadio_6" value="much|58|3" id="rid_58" /><label for="rid_58">much</label><br />
                        <input type="radio" name="grpRadio_6" value="very much noticeable|60|4" id="rid_60" /><label for="rid_60">very much noticeable</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID7">
                <td>
                    <strong>How WORRIED/distressed are you about your current sleep problem?</strong>
                    <div>
                        <input type="radio" name="grpRadio_7" value="not at all|62|0" id="rid_62" /><label for="rid_62">not at all</label><br />
                        <input type="radio" name="grpRadio_7" value="a little|64|1" id="rid_64" /><label for="rid_64">a little</label><br />
                        <input type="radio" name="grpRadio_7" value="somewhat|66|2" id="rid_66" /><label for="rid_66">somewhat</label><br />
                        <input type="radio" name="grpRadio_7" value="much|68|3" id="rid_68" /><label for="rid_68">much</label><br />
                        <input type="radio" name="grpRadio_7" value="very much|70|4" id="rid_70" /><label for="rid_70">very much</label><br />
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
            expandLabelWidth();
        });

        function expandLabelWidth() {
            $('label', $('div[id$="divQuestions"]')).each(function (i, l) {
                $(this).css({
                    display: 'inline-block',
                    width: '400px'
                });
            });
        }
    </script>
</asp:Content>