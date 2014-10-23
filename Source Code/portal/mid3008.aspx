<%@ Page Title="Circadian" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid3008.aspx.cs" Inherits="mid3008" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

        <!-- this tells the code behind how many responces to process -->
        <input type="hidden" name="ResponseCount" value="7" /><br />

        <h1>Circadian</h1>
        <h4>These questions will help us to know more about your body’s daily rhythm.<br />
            It will take about one minute to answer these questions.<br />
            After you have answered the questions, click submit.</h4>
        <table class="tbl-quest">

            <tr id="TID1_QID1">
                <td>
                    <strong>Do you routinely travel to other time zones?</strong>
                    <div>
                        <input type="radio" name="grpRadio_1" value="yes|2|0" id="rid_2" /><label for="rid_2">yes</label><br />
                        <input type="radio" name="grpRadio_1" value="no|4|0" id="rid_4" /><label for="rid_4">no</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID2">
                <td>
                    <strong>Considering only your own 'feeling best' rhythm, at what time of day would you get up if you were entirely free to plan your day?</strong>
                    <div>
                        <input type="radio" name="grpRadio_2" value="5:00am-6:30am|6|5" id="rid_6" /><label for="rid_6">5:00am-6:30am</label><br />
                        <input type="radio" name="grpRadio_2" value="6:30am-7:45am|8|4" id="rid_8" /><label for="rid_8">6:30am-7:45am</label><br />
                        <input type="radio" name="grpRadio_2" value="7:45am-9:45am|10|3" id="rid_10" /><label for="rid_10">7:45am-9:45am</label><br />
                        <input type="radio" name="grpRadio_2" value="9:45am-11:00am|12|2" id="rid_12" /><label for="rid_12">9:45am-11:00am</label><br />
                        <input type="radio" name="grpRadio_2" value="11:00am-12:00 noon|14|1" id="rid_14" /><label for="rid_14">11:00am-12:00 noon</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID3">
                <td>
                    <strong>During the first half hour after having awakened in the morning, how tired do you feel?</strong>
                    <div>
                        <input type="radio" name="grpRadio_3" value="Very tired|16|1" id="rid_16" /><label for="rid_16">Very tired</label><br />
                        <input type="radio" name="grpRadio_3" value="Fairly tired|18|2" id="rid_18" /><label for="rid_18">Fairly tired</label><br />
                        <input type="radio" name="grpRadio_3" value="Fairly refreshed|20|3" id="rid_20" /><label for="rid_20">Fairly refreshed</label><br />
                        <input type="radio" name="grpRadio_3" value="Very refreshed|22|4" id="rid_22" /><label for="rid_22">Very refreshed</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID4">
                <td>
                    <strong>At what time in the evening do you feel tired and, as a result, in need of sleep?</strong>
                    <div>
                        <input type="radio" name="grpRadio_4" value="8:00pm-9:00pm|24|5" id="rid_24" /><label for="rid_24">8:00pm-9:00pm</label><br />
                        <input type="radio" name="grpRadio_4" value="9:00pm-10:15pm|26|4" id="rid_26" /><label for="rid_26">9:00pm-10:15pm</label><br />
                        <input type="radio" name="grpRadio_4" value="10:15pm-12:30am|28|3" id="rid_28" /><label for="rid_28">10:15pm-12:30am</label><br />
                        <input type="radio" name="grpRadio_4" value="12:30am-1:45am|30|2" id="rid_30" /><label for="rid_30">12:30am-1:45am</label><br />
                        <input type="radio" name="grpRadio_4" value="1:45am-3:00am|32|1" id="rid_32" /><label for="rid_32">1:45am-3:00am</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID5">
                <td>
                    <strong>At what time of the day do you think that you reach your 'feeling best' peak?</strong>
                    <div>
                        <input type="radio" name="grpRadio_5" value="12:00am-5:00am|34|1" id="rid_34" /><label for="rid_34">12:00am-5:00am</label><br />
                        <input type="radio" name="grpRadio_5" value="5:00am-8:00am|36|5" id="rid_36" /><label for="rid_36">5:00am-8:00am</label><br />
                        <input type="radio" name="grpRadio_5" value="8:00am-10:00am|38|4" id="rid_38" /><label for="rid_38">8:00am-10:00am</label><br />
                        <input type="radio" name="grpRadio_5" value="10:00am-5:00pm|40|3" id="rid_40" /><label for="rid_40">10:00am-5:00pm</label><br />
                        <input type="radio" name="grpRadio_5" value="5:00pm-10:00pm|42|2" id="rid_42" /><label for="rid_42">5:00pm-10:00pm</label><br />
                        <input type="radio" name="grpRadio_5" value="10:00pm-12:00am|44|1" id="rid_44" /><label for="rid_44">10:00pm-12:00am</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID6">
                <td>
                    <strong>One hears about 'morning' and 'evening' types of people.  Which ONE of these types do you consider yourself to be?</strong>
                    <div>
                        <input type="radio" name="grpRadio_6" value="Definitely a 'morning'|46|5" id="rid_46" /><label for="rid_46">Definitely a 'morning'</label><br />
                        <input type="radio" name="grpRadio_6" value="More a 'morning' than an 'evening'|48|4" id="rid_48" /><label for="rid_48">More a 'morning' than an 'evening'</label><br />
                        <input type="radio" name="grpRadio_6" value="More an 'evening' than a 'morning'|50|2" id="rid_50" /><label for="rid_50">More an 'evening' than a 'morning'</label><br />
                        <input type="radio" name="grpRadio_6" value="Definitely an 'evening'|52|0" id="rid_52" /><label for="rid_52">Definitely an 'evening'</label><br />
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