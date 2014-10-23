<%@ Page Title="Parasomnia Questions" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid3010.aspx.cs" Inherits="mid3010" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

        <!-- this tells the code behind how many responces to process -->
        <input type="hidden" name="ResponseCount" value="40" /><br />

        <h1>Parasomnia Questions</h1>
        <h4>These questions will help us to determine if you have a movement disorder during sleep.<br />
            It will take about one minute to answer these questions.<br />
            After you have answered the questions, click submit.</h4>
        <table class="tbl-quest">

            <tr id="TID1_QID1">
                <td>
                    <strong>Do your legs twitch or kick when you sleep? (Check one)</strong>
                    <div>
                        <input type="radio" name="grpRadio_1" value="Not during the past several months|2|0" id="rid_2" /><label for="rid_2">Not during the past several months</label><br />
                        <input type="radio" name="grpRadio_1" value="Less than once a week|4|0" id="rid_4" /><label for="rid_4">Less than once a week</label><br />
                        <input type="radio" name="grpRadio_1" value="Once or twice a week|6|0" id="rid_6" /><label for="rid_6">Once or twice a week</label><br />
                        <input type="radio" name="grpRadio_1" value="Three or more times a week|8|0" id="rid_8" /><label for="rid_8">Three or more times a week</label><br />
                        <input type="radio" name="grpRadio_1" value="Don’t know|12|0" id="rid_12" /><label for="rid_12">Don’t know</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID2">
                <td>
                    <strong>How often do you walk in your sleep? (Check one)</strong>
                    <div>
                        <input type="radio" name="grpRadio_2" value="Not during the past several months|14|0" id="rid_14" /><label for="rid_14">Not during the past several months</label><br />
                        <input type="radio" name="grpRadio_2" value="Less than once a week|16|0" id="rid_16" /><label for="rid_16">Less than once a week</label><br />
                        <input type="radio" name="grpRadio_2" value="Once or twice a week|18|0" id="rid_18" /><label for="rid_18">Once or twice a week</label><br />
                        <input type="radio" name="grpRadio_2" value="Three or more times a week|20|0" id="rid_20" /><label for="rid_20">Three or more times a week</label><br />
                        <input type="radio" name="grpRadio_2" value="Don’t know|24|0" id="rid_24" /><label for="rid_24">Don’t know</label><br />
                        <input type="radio" name="grpRadio_2" value="I don’t sleepwalk|22|0" id="rid_22" /><label for="rid_22">I don’t sleepwalk</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID3">
                <td>
                    <strong>How old were you when this first happened?</strong>
                    <div>
                        <input type="text" name="grpCtrlText_3" id="rid_txt_26" />&nbsp;years old
                        <input type="hidden" name="grpHidden_3" id="rid_26" value="years old|26|0" /><br />
                        <input type="radio" name="grpRadio_4" value="Don’t know|28|0" id="rid_28" clearabove="clearabove" /><label for="rid_28">Don’t know</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID4">
                <td>
                    <strong>How often do you awaken during the night and are unable to return to sleep without having something to eat or drink (other than water for thirst or a dry mouth)? (Check one)</strong>
                    <div>
                        <input type="radio" name="grpRadio_5" value="Not during the past several months|30|0" id="rid_30" /><label for="rid_30">Not during the past several months</label><br />
                        <input type="radio" name="grpRadio_5" value="Less than once a week|32|0" id="rid_32" /><label for="rid_32">Less than once a week</label><br />
                        <input type="radio" name="grpRadio_5" value="Once or twice a week|34|0" id="rid_34" /><label for="rid_34">Once or twice a week</label><br />
                        <input type="radio" name="grpRadio_5" value="Three or more times a week|36|0" id="rid_36" /><label for="rid_36">Three or more times a week</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID5">
                <td>
                    <strong>Do you grind your teeth while you sleep? (Check one)</strong>
                    <div>
                        <input type="radio" name="grpRadio_6" value="Not during the past several months|40|0" id="rid_40" /><label for="rid_40">Not during the past several months</label><br />
                        <input type="radio" name="grpRadio_6" value="Less than once a week|42|0" id="rid_42" /><label for="rid_42">Less than once a week</label><br />
                        <input type="radio" name="grpRadio_6" value="Once or twice a week|44|0" id="rid_44" /><label for="rid_44">Once or twice a week</label><br />
                        <input type="radio" name="grpRadio_6" value="Three or more times a week|46|0" id="rid_46" /><label for="rid_46">Three or more times a week</label><br />
                        <input type="radio" name="grpRadio_6" value="Don’t know|48|0" id="rid_48" /><label for="rid_48">Don’t know</label><br />
                    </div>
                </td>
            </tr>
            <tr id="TID1_QID6">
                <td>
                    <strong>During sleep, how often do you (or have been told that you) shout, scream, punch or flail your arms in the air? (Check one)</strong>
                    <div>
                        <input type="radio" name="grpRadio_7" value="Not during the past several months|50|0" id="rid_50" /><label for="rid_50">Not during the past several months</label><br />
                        <input type="radio" name="grpRadio_7" value="Less than once a week|52|0" id="rid_52" /><label for="rid_52">Less than once a week</label><br />
                        <input type="radio" name="grpRadio_7" value="Once or twice a week|54|0" id="rid_54" /><label for="rid_54">Once or twice a week</label><br />
                        <input type="radio" name="grpRadio_7" value="Three or more times a week|56|0" id="rid_56" /><label for="rid_56">Three or more times a week</label><br />
                        <input type="radio" name="grpRadio_7" value="Don’t know|58|0" id="rid_58" /><label for="rid_58">Don’t know</label><br />
                        <input type="radio" name="grpRadio_7" value="When I sleepwalk, I don’t shout, scream, punch or flail my arms in the air|510|0" id="rid_510" /><label for="rid_510">When I sleepwalk, I don’t shout, scream, punch or flail my arms in the air</label><br />
                    </div>
                </td>
            </tr>

           <tr id="TID1_QID7">
                <td>
                    <strong>How old were you when this first happened?</strong>
                    <div>
                        <input type="text" name="grpCtrlText_8" id="rid_txt_60" />&nbsp;years old
                        <input type="hidden" name="grpHidden_8" id="rid_60" value="years old|60|0" /><br />
                        <input type="radio" name="grpRadio_9" value="Don’t know|62|0" id="rid_62" clearabove="clearabove" /><label for="rid_62">Don’t know</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID8">
                <td>
                    <strong>How often do you have violent or injurious behavior during sleep? (Check one)</strong>
                    <div>
                        <input type="radio" name="grpRadio_10" value="Never|72|0" id="rid_72" /><label for="rid_72">Never</label><br />
                        <input type="radio" name="grpRadio_10" value="Less than once a month|64|0" id="rid_64" /><label for="rid_64">Less than once a month</label><br />
                        <input type="radio" name="grpRadio_10" value="About once a month|66|0" id="rid_66" /><label for="rid_66">About once a month</label><br />
                        <input type="radio" name="grpRadio_10" value="Several times per month|68|0" id="rid_68" /><label for="rid_68">Several times per month</label><br />
                        <input type="radio" name="grpRadio_10" value="More than once a week|70|0" id="rid_70" /><label for="rid_70">More than once a week</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID9">
                <td>
                    <strong>How old were you when this first happened?</strong>
                    <div>
                        <input type="text" name="grpCtrlText_11" id="rid_txt_74" />&nbsp;years old
                        <input type="hidden" name="grpHidden_11" id="rid_74" value="years old|74|0" /><br />
                        <input type="radio" name="grpRadio_12" value="Don’t know|76|0" id="rid_76" clearabove="clearabove" /><label for="rid_76">Don’t know</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID10">
                <td>
                    <strong>How often do you have nightmares (frightening dreams)? (Check one)</strong>
                    <div>
                        <input type="radio" name="grpRadio_13" value="Not during the past month|86|0" id="rid_86" /><label for="rid_86">Not during the past month</label><br />
                        <input type="radio" name="grpRadio_13" value="Less than once a week|78|0" id="rid_78" /><label for="rid_78">Less than once a week</label><br />
                        <input type="radio" name="grpRadio_13" value="Once or twice a week|80|0" id="rid_80" /><label for="rid_80">Once or twice a week</label><br />
                        <input type="radio" name="grpRadio_13" value="Three or more times a week|82|0" id="rid_82" /><label for="rid_82">Three or more times a week</label><br />
                        <input type="radio" name="grpRadio_13" value="Don’t know|84|0" id="rid_84" /><label for="rid_84">Don’t know</label><br />
                    </div>
                </td>
            </tr>
            <tr id="TID1_QID11">
                <td>
                    <strong>How old were you when this first happened?</strong>
                    <div>
                        <input type="text" name="grpCtrlText_14" id="rid_txt_88" />&nbsp;years old
                        <input type="hidden" name="grpHidden_14" id="rid_88" value="years old|88|0" /><br />
                        <input type="radio" name="grpRadio_15" value="Don’t know|92|0" id="rid_92" clearabove="clearabove" /><label for="rid_92">Don’t know</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID12">
                <td>
                    <strong>In the past 6 months, have you had seizures, convulsions or ‘fits’ during sleep?</strong>
                    <div>
                        <input type="radio" name="grpRadio_16" value="Yes|94|0" id="rid_94" /><label for="rid_94">Yes</label><br />
                        <input type="radio" name="grpRadio_16" value="No|96|0" id="rid_96" /><label for="rid_96">No</label><br />
                        <input type="radio" name="grpRadio_16" value="Don’t know|98|0" id="rid_98" /><label for="rid_98">Don’t know</label><br />
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

        //skip patterns
        questions.opts.skipPatterns = [
            {
                rid: 22,
                checked: {
                    hide: ['TID1_QID3', 'TID1_QID6', 'TID1_QID7']
                }
            },

            {
                rid: 510,
                checked: {
                    hide: ['TID1_QID7']
                }
            },

            {
                rid: 72,
                checked: {
                    hide: ['TID1_QID9']
                }
            }
        ];

        //text masks
        questions.opts.txtMasks = [
            { rid: 26, mask: 'numbersOnly', maxlength: 2 },
            { rid: 60, mask: 'numbersOnly', maxlength: 2 },
            { rid: 74, mask: 'numbersOnly', maxlength: 2 },
            { rid: 88, mask: 'numbersOnly', maxlength: 2 }
        ];


    </script>
</asp:Content>