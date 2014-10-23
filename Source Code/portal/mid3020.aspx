<%@ Page Title="SF-12 Health Survey" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid3020.aspx.cs" Inherits="mid3020" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">
        <h1>SF-12 Health Survey </h1>
        <br />
        <h3>This questionnaire asks for your views about your health. This information will help keep track 
            of how you feel and how well you are able to do your usual activities. Please answer every 
            question. If you are unsure about how to answer, please give the best answer you can.
        </h3>

        <table class="tbl-quest">
            <tr id="TID1_QID1">
                <td>
                    <strong>In general, would you say your health is:</strong>
                    <div>
                        <input type="radio" name="grpRadio_1" value="Excellent|2|1" id="rid_2" /><label for="rid_2">Excellent</label><br />
                        <input type="radio" name="grpRadio_1" value="Very good|4|2" id="rid_4" /><label for="rid_4">Very good</label><br />
                        <input type="radio" name="grpRadio_1" value="Good |6|3" id="rid_6" /><label for="rid_6">Good </label><br />
                        <input type="radio" name="grpRadio_1" value="Fair|8|4" id="rid_8" /><label for="rid_8">Fair</label><br />
                        <input type="radio" name="grpRadio_1" value="Poor|10|5" id="rid_10" /><label for="rid_10">Poor</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID2">
                <td>
                    <br />
                    <strong>The following items are about activities you might do during a typical day.  Does your health now limit you in these activities?  If so, how much?</strong>
                    <br />
                    <br />
                    <strong>Moderate activities, such as moving a table, pushing a vacuum cleaner, bowling, or playing golf?</strong>
                    <div>
                        <input type="radio" name="grpRadio_2" value="Yes, limited a lot|12|1" id="rid_12" /><label for="rid_12">Yes, limited a lot</label><br />
                        <input type="radio" name="grpRadio_2" value="Yes, limited  a little|14|2" id="rid_14" /><label for="rid_14">Yes, limited  a little</label><br />
                        <input type="radio" name="grpRadio_2" value="No, not limited at all|16|3" id="rid_16" /><label for="rid_16">No, not limited at all</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID3">
                <td>
                    <strong>Climbing several flights of stairs?</strong>
                    <div>
                        <input type="radio" name="grpRadio_3" value="Yes, limited a lot|18|1" id="rid_18" /><label for="rid_18">Yes, limited a lot</label><br />
                        <input type="radio" name="grpRadio_3" value="Yes, limited  a little|20|2" id="rid_20" /><label for="rid_20">Yes, limited  a little</label><br />
                        <input type="radio" name="grpRadio_3" value="No, not limited at all|22|3" id="rid_22" /><label for="rid_22">No, not limited at all</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID4">
                <td>
                    <br />
                    <strong>During the past 4 weeks, have you had any of the following problems with your work or other regular daily activities as a result of your physical health?</strong>
                    <br />
                    <br />
                    <strong>Accomplished less than you would like?</strong>
                    <div>
                        <input type="radio" name="grpRadio_4" value="Yes|24|1" id="rid_24" /><label for="rid_24">Yes</label><br />
                        <input type="radio" name="grpRadio_4" value="No|26|2" id="rid_26" /><label for="rid_26">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID5">
                <td>
                    <strong>Were limited in the kind of work or other activities?</strong>
                    <div>
                        <input type="radio" name="grpRadio_5" value="Yes|28|1" id="rid_28" /><label for="rid_28">Yes</label><br />
                        <input type="radio" name="grpRadio_5" value="No|30|2" id="rid_30" /><label for="rid_30">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID6">
                <td>
                    <br />
                    <strong>During the past 4 weeks, have you had any of the following problems with your work or other regular daily activities as a result of any emotional problems (such as feeling depressed or anxious)?</strong>
                    <br />
                    <br />
                    <strong>Accomplished less than you would like?</strong>
                    <div>
                        <input type="radio" name="grpRadio_6" value="Yes|32|1" id="rid_32" /><label for="rid_32">Yes</label><br />
                        <input type="radio" name="grpRadio_6" value="No|34|2" id="rid_34" /><label for="rid_34">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID7">
                <td>
                    <!-- Did work or other activities less carefully than usual -->
                    <strong>Didn’t do work or other activities as carefully as usual?</strong>
                    <div>
                        <input type="radio" name="grpRadio_7" value="Yes|36|1" id="rid_36" /><label for="rid_36">Yes</label><br />
                        <input type="radio" name="grpRadio_7" value="No|38|2" id="rid_38" /><label for="rid_38">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID8">
                <td>
                    <strong>During the past 4 weeks, how much did pain interfere with your normal work (including both work outside the home and housework)?</strong>
                    <div>
                        <input type="radio" name="grpRadio_8" value="Not at all|40|1" id="rid_40" /><label for="rid_40">Not at all</label><br />
                        <input type="radio" name="grpRadio_8" value="A little bit|42|2" id="rid_42" /><label for="rid_42">A little bit</label><br />
                        <input type="radio" name="grpRadio_8" value="Moderately|44|3" id="rid_44" /><label for="rid_44">Moderately</label><br />
                        <input type="radio" name="grpRadio_8" value="Quite a bit|46|4" id="rid_46" /><label for="rid_46">Quite a bit</label><br />
                        <input type="radio" name="grpRadio_8" value="Extremely|48|5" id="rid_48" /><label for="rid_48">Extremely</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID9">
                <td>
                    <br />
                    <strong>These questions are about how you feel and how things have been with you during the past 4 weeks.  For each question, please give the one answer that comes closest to the way you have been feeling.  How much of the time during the past 4 weeks.</strong>
                    <br />
                    <br />
                    <strong>Have you felt calm and peaceful?</strong>
                    <div>
                        <input type="radio" name="grpRadio_9" value="All the time|62|1" id="rid_62" /><label for="rid_62">All the time</label><br />
                        <input type="radio" name="grpRadio_9" value="Most of the time|64|2" id="rid_64" /><label for="rid_64">Most of the time</label><br />
                        <input type="radio" name="grpRadio_9" value="A good bit of the time|66|3" id="rid_66" /><label for="rid_66">A good bit of the time</label><br />
                        <input type="radio" name="grpRadio_9" value="Some of the time|68|4" id="rid_68" /><label for="rid_68">Some of the time</label><br />
                        <input type="radio" name="grpRadio_9" value="A little of the time|70|5" id="rid_70" /><label for="rid_70">A little of the time</label><br />
                        <input type="radio" name="grpRadio_9" value="None of the time|72|6" id="rid_72" /><label for="rid_72">None of the time</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID10">
                <td>
                    <strong>Did you have a lot of energy?</strong>
                    <div>
                        <input type="radio" name="grpRadio_10" value="All the time|74|1" id="rid_74" /><label for="rid_74">All the time</label><br />
                        <input type="radio" name="grpRadio_10" value="Most of the time|76|2" id="rid_76" /><label for="rid_76">Most of the time</label><br />
                        <input type="radio" name="grpRadio_10" value="A good bit of the time|78|3" id="rid_78" /><label for="rid_78">A good bit of the time</label><br />
                        <input type="radio" name="grpRadio_10" value="Some of the time|80|4" id="rid_80" /><label for="rid_80">Some of the time</label><br />
                        <input type="radio" name="grpRadio_10" value="A little of the time|82|5" id="rid_82" /><label for="rid_82">A little of the time</label><br />
                        <input type="radio" name="grpRadio_10" value="None of the time|84|6" id="rid_84" /><label for="rid_84">None of the time</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID11">
                <td>
                    <strong>Have you felt  downhearted and blue?</strong>
                    <div>
                        <input type="radio" name="grpRadio_11" value="All the time|86|1" id="rid_86" /><label for="rid_86">All the time</label><br />
                        <input type="radio" name="grpRadio_11" value="Most of the time|88|2" id="rid_88" /><label for="rid_88">Most of the time</label><br />
                        <input type="radio" name="grpRadio_11" value="A good bit of the time|89|3" id="rid_89" /><label for="rid_89">A good bit of the time</label><br />
                        <input type="radio" name="grpRadio_11" value="Some of the time|90|4" id="rid_90" /><label for="rid_90">Some of the time</label><br />
                        <input type="radio" name="grpRadio_11" value="A little of the time|92|5" id="rid_92" /><label for="rid_92">A little of the time</label><br />
                        <input type="radio" name="grpRadio_11" value="None of the time|94|6" id="rid_94" /><label for="rid_94">None of the time</label><br />

                    </div>
                </td>
            </tr>

            <tr id="TID1_QID12">
                <td>
                    <strong>During the past 4 weeks, how much of the time has your physical health or emotional problems interfered with your social activities (like visiting friends, relatives, etc.)?</strong>
                    <div>
                        <input type="radio" name="grpRadio_12" value="All the time|96|1" id="rid_96" /><label for="rid_96">All the time</label><br />
                        <input type="radio" name="grpRadio_12" value="Most of the time|98|2" id="rid_98" /><label for="rid_98">Most of the time</label><br />
                        <input type="radio" name="grpRadio_12" value="Some of the time|100|3" id="rid_100" /><label for="rid_100">Some of the time</label><br />
                        <input type="radio" name="grpRadio_12" value="A little of the time|102|4" id="rid_102" /><label for="rid_102">A little of the time</label><br />
                        <input type="radio" name="grpRadio_12" value="None of the time|104|5" id="rid_104" /><label for="rid_104">None of the time</label><br />
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