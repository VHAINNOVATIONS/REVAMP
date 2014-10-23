<%@ Page Title="Center for Epidemiologic Studies Depression Scale (CES-D)" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid3024.aspx.cs" Inherits="mid3024" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

        <!-- this tells the code behind how many responces to process -->
        <input type="hidden" name="ResponseCount" value="11" /><br />

        <h1>Center for Epidemiological Studies Depression - CES-D </h1>
        <h4>These questions will assess if you are depressed.<br />
            It will take about one minute to answer these questions.<br />
            After you have answered the questions, click submit.</h4>
        <br />
        <h3>Below is a list of the ways you might have felt or behaved. Please tell me how often you have felt this way during the past week.</h3>
        <br />

        <table class="tbl-quest">
            <tr id="TID1_QID1">
                <td>
                    <strong>I was bothered by things that usually don't bother me.</strong>
                    <div>
                        <input type="radio" name="grpRadio_1" value="Rarely or none of the time (less than 1 day )|2|0" id="rid_2" /><label for="rid_2">Rarely or none of the time (less than 1 day )</label><br />
                        <input type="radio" name="grpRadio_1" value="Some or a little of the time (1-2 days)|4|1" id="rid_4" /><label for="rid_4">Some or a little of the time (1-2 days)</label><br />
                        <input type="radio" name="grpRadio_1" value="Occasionally or a moderate amount of time (3-4 days)|6|2" id="rid_6" /><label for="rid_6">Occasionally or a moderate amount of time (3-4 days)</label><br />
                        <input type="radio" name="grpRadio_1" value="All of the time (5-7 days)|8|3" id="rid_8" /><label for="rid_8">All of the time (5-7 days)</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID2">
                <td>
                    <strong>I had trouble keeping my mind on what I was doing.</strong>
                    <div>
                        <input type="radio" name="grpRadio_2" value="Rarely or none of the time (less than 1 day )|10|0" id="rid_10" /><label for="rid_10">Rarely or none of the time (less than 1 day )</label><br />
                        <input type="radio" name="grpRadio_2" value="Some or a little of the time (1-2 days)|12|1" id="rid_12" /><label for="rid_12">Some or a little of the time (1-2 days)</label><br />
                        <input type="radio" name="grpRadio_2" value="Occasionally or a moderate amount of time (3-4 days)|14|2" id="rid_14" /><label for="rid_14">Occasionally or a moderate amount of time (3-4 days)</label><br />
                        <input type="radio" name="grpRadio_2" value="All of the time (5-7 days)|16|3" id="rid_16" /><label for="rid_16">All of the time (5-7 days)</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID3">
                <td>
                    <strong>I felt depressed.</strong>
                    <div>
                        <input type="radio" name="grpRadio_3" value="Rarely or none of the time (less than 1 day )|18|0" id="rid_18" /><label for="rid_18">Rarely or none of the time (less than 1 day )</label><br />
                        <input type="radio" name="grpRadio_3" value="Some or a little of the time (1-2 days)|20|1" id="rid_20" /><label for="rid_20">Some or a little of the time (1-2 days)</label><br />
                        <input type="radio" name="grpRadio_3" value="Occasionally or a moderate amount of time (3-4 days)|22|2" id="rid_22" /><label for="rid_22">Occasionally or a moderate amount of time (3-4 days)</label><br />
                        <input type="radio" name="grpRadio_3" value="All of the time (5-7 days)|24|3" id="rid_24" /><label for="rid_24">All of the time (5-7 days)</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID4">
                <td>
                    <strong>I felt that everything I did was an effort.</strong>
                    <div>
                        <input type="radio" name="grpRadio_4" value="Rarely or none of the time (less than 1 day )|26|0" id="rid_26" /><label for="rid_26">Rarely or none of the time (less than 1 day )</label><br />
                        <input type="radio" name="grpRadio_4" value="Some or a little of the time (1-2 days)|28|1" id="rid_28" /><label for="rid_28">Some or a little of the time (1-2 days)</label><br />
                        <input type="radio" name="grpRadio_4" value="Occasionally or a moderate amount of time (3-4 days)|30|2" id="rid_30" /><label for="rid_30">Occasionally or a moderate amount of time (3-4 days)</label><br />
                        <input type="radio" name="grpRadio_4" value="All of the time (5-7 days)|32|3" id="rid_32" /><label for="rid_32">All of the time (5-7 days)</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID5">
                <td>
                    <strong>I felt hopeful about the future.</strong>
                    <div>
                        <input type="radio" name="grpRadio_5" value="Rarely or none of the time (less than 1 day )|34|3" id="rid_34" /><label for="rid_34">Rarely or none of the time (less than 1 day )</label><br />
                        <input type="radio" name="grpRadio_5" value="Some or a little of the time (1-2 days)|36|2" id="rid_36" /><label for="rid_36">Some or a little of the time (1-2 days)</label><br />
                        <input type="radio" name="grpRadio_5" value="Occasionally or a moderate amount of time (3-4 days)|38|1" id="rid_38" /><label for="rid_38">Occasionally or a moderate amount of time (3-4 days)</label><br />
                        <input type="radio" name="grpRadio_5" value="All of the time (5-7 days)|40|0" id="rid_40" /><label for="rid_40">All of the time (5-7 days)</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID6">
                <td>
                    <strong>I felt fearful.</strong>
                    <div>
                        <input type="radio" name="grpRadio_6" value="Rarely or none of the time (less than 1 day )|42|0" id="rid_42" /><label for="rid_42">Rarely or none of the time (less than 1 day )</label><br />
                        <input type="radio" name="grpRadio_6" value="Some or a little of the time (1-2 days)|44|1" id="rid_44" /><label for="rid_44">Some or a little of the time (1-2 days)</label><br />
                        <input type="radio" name="grpRadio_6" value="Occasionally or a moderate amount of time (3-4 days)|46|2" id="rid_46" /><label for="rid_46">Occasionally or a moderate amount of time (3-4 days)</label><br />
                        <input type="radio" name="grpRadio_6" value="All of the time (5-7 days)|48|3" id="rid_48" /><label for="rid_48">All of the time (5-7 days)</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID7">
                <td>
                    <strong>My sleep was restless.</strong>
                    <div>
                        <input type="radio" name="grpRadio_7" value="Rarely or none of the time (less than 1 day )|50|0" id="rid_50" /><label for="rid_50">Rarely or none of the time (less than 1 day )</label><br />
                        <input type="radio" name="grpRadio_7" value="Some or a little of the time (1-2 days)|52|1" id="rid_52" /><label for="rid_52">Some or a little of the time (1-2 days)</label><br />
                        <input type="radio" name="grpRadio_7" value="Occasionally or a moderate amount of time (3-4 days)|54|2" id="rid_54" /><label for="rid_54">Occasionally or a moderate amount of time (3-4 days)</label><br />
                        <input type="radio" name="grpRadio_7" value="All of the time (5-7 days)|56|3" id="rid_56" /><label for="rid_56">All of the time (5-7 days)</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID8">
                <td>
                    <strong>I was happy.</strong>
                    <div>
                        <input type="radio" name="grpRadio_8" value="Rarely or none of the time (less than 1 day )|58|3" id="rid_58" /><label for="rid_58">Rarely or none of the time (less than 1 day )</label><br />
                        <input type="radio" name="grpRadio_8" value="Some or a little of the time (1-2 days)|60|2" id="rid_60" /><label for="rid_60">Some or a little of the time (1-2 days)</label><br />
                        <input type="radio" name="grpRadio_8" value="Occasionally or a moderate amount of time (3-4 days)|62|1" id="rid_62" /><label for="rid_62">Occasionally or a moderate amount of time (3-4 days)</label><br />
                        <input type="radio" name="grpRadio_8" value="All of the time (5-7 days)|64|0" id="rid_64" /><label for="rid_64">All of the time (5-7 days)</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID9">
                <td>
                    <strong>I felt lonely.</strong>
                    <div>
                        <input type="radio" name="grpRadio_9" value="Rarely or none of the time (less than 1 day )|66|0" id="rid_66" /><label for="rid_66">Rarely or none of the time (less than 1 day )</label><br />
                        <input type="radio" name="grpRadio_9" value="Some or a little of the time (1-2 days)|68|1" id="rid_68" /><label for="rid_68">Some or a little of the time (1-2 days)</label><br />
                        <input type="radio" name="grpRadio_9" value="Occasionally or a moderate amount of time (3-4 days)|70|2" id="rid_70" /><label for="rid_70">Occasionally or a moderate amount of time (3-4 days)</label><br />
                        <input type="radio" name="grpRadio_9" value="All of the time (5-7 days)|72|3" id="rid_72" /><label for="rid_72">All of the time (5-7 days)</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID10">
                <td>
                    <strong>I could not get "going".</strong>
                    <div>
                        <input type="radio" name="grpRadio_10" value="Rarely or none of the time (less than 1 day )|74|0" id="rid_74" /><label for="rid_74">Rarely or none of the time (less than 1 day )</label><br />
                        <input type="radio" name="grpRadio_10" value="Some or a little of the time (1-2 days)|76|1" id="rid_76" /><label for="rid_76">Some or a little of the time (1-2 days)</label><br />
                        <input type="radio" name="grpRadio_10" value="Occasionally or a moderate amount of time (3-4 days)|78|2" id="rid_78" /><label for="rid_78">Occasionally or a moderate amount of time (3-4 days)</label><br />
                        <input type="radio" name="grpRadio_10" value="All of the time (5-7 days)|80|3" id="rid_80" /><label for="rid_80">All of the time (5-7 days)</label><br />
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