<%@ Page Title="PSYCHOTHERAPIES FOR PANIC DISORDER (PPD)" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid3030.aspx.cs" Inherits="mid3030" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

        <!-- this tells the code behind how many responces to process -->
        <input type="hidden" name="ResponseCount" value="13" /><br />

        <h1>PSYCHOTHERAPIES FOR PANIC DISORDER (PPD) </h1>
        <br />
        <h3>Instructions: The following sentences describe some of the different ways a person might think or feel about his or her therapist/doctor.  As you read the sentences, mentally insert the name of your therapist/doctor in place of _____________in the text.  Below each statement, please indicate the extent to which the statement describes your experience.  </h3>
        <table class="tbl-quest">
            <tr id="TID1_QID1">
                <td>
                    <strong>__________ and I agree about the things I will need to do in treatment to help improve my situation.</strong>
                    <div>
                        <input type="radio" name="grpRadio_1" value="Never|2|1" id="rid_2" /><label for="rid_2">Never</label><br />
                        <input type="radio" name="grpRadio_1" value="Rarely|4|2" id="rid_4" /><label for="rid_4">Rarely</label><br />
                        <input type="radio" name="grpRadio_1" value="Occasionally|6|3" id="rid_6" /><label for="rid_6">Occasionally</label><br />
                        <input type="radio" name="grpRadio_1" value="Sometimes|8|4" id="rid_8" /><label for="rid_8">Sometimes</label><br />
                        <input type="radio" name="grpRadio_1" value="Often|10|5" id="rid_10" /><label for="rid_10">Often</label><br />
                        <input type="radio" name="grpRadio_1" value="Very Often|12|6" id="rid_12" /><label for="rid_12">Very Often</label><br />
                        <input type="radio" name="grpRadio_1" value="Always|14|7" id="rid_14" /><label for="rid_14">Always</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID2">
                <td>
                    <strong>What I am doing in treatment gives me new ways of looking at my problem.</strong>
                    <div>
                        <input type="radio" name="grpRadio_2" value="Never|16|1" id="rid_16" /><label for="rid_16">Never</label><br />
                        <input type="radio" name="grpRadio_2" value="Rarely|18|2" id="rid_18" /><label for="rid_18">Rarely</label><br />
                        <input type="radio" name="grpRadio_2" value="Occasionally|20|3" id="rid_20" /><label for="rid_20">Occasionally</label><br />
                        <input type="radio" name="grpRadio_2" value="Sometimes|22|4" id="rid_22" /><label for="rid_22">Sometimes</label><br />
                        <input type="radio" name="grpRadio_2" value="Often|24|5" id="rid_24" /><label for="rid_24">Often</label><br />
                        <input type="radio" name="grpRadio_2" value="Very Often|26|6" id="rid_26" /><label for="rid_26">Very Often</label><br />
                        <input type="radio" name="grpRadio_2" value="Always|28|7" id="rid_28" /><label for="rid_28">Always</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID3">
                <td>
                    <strong>I believe _______________ likes me.</strong>
                    <div>
                        <input type="radio" name="grpRadio_3" value="Never|30|1" id="rid_30" /><label for="rid_30">Never</label><br />
                        <input type="radio" name="grpRadio_3" value="Rarely|32|2" id="rid_32" /><label for="rid_32">Rarely</label><br />
                        <input type="radio" name="grpRadio_3" value="Occasionally|34|3" id="rid_34" /><label for="rid_34">Occasionally</label><br />
                        <input type="radio" name="grpRadio_3" value="Sometimes|36|4" id="rid_36" /><label for="rid_36">Sometimes</label><br />
                        <input type="radio" name="grpRadio_3" value="Often|38|5" id="rid_38" /><label for="rid_38">Often</label><br />
                        <input type="radio" name="grpRadio_3" value="Very Often|40|6" id="rid_40" /><label for="rid_40">Very Often</label><br />
                        <input type="radio" name="grpRadio_3" value="Always|42|7" id="rid_42" /><label for="rid_42">Always</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID4">
                <td>
                    <strong>_______________ does not understand what I am trying to accomplish in treatment.</strong>
                    <div>
                        <input type="radio" name="grpRadio_4" value="Never|44|1" id="rid_44" /><label for="rid_44">Never</label><br />
                        <input type="radio" name="grpRadio_4" value="Rarely|46|2" id="rid_46" /><label for="rid_46">Rarely</label><br />
                        <input type="radio" name="grpRadio_4" value="Occasionally|48|3" id="rid_48" /><label for="rid_48">Occasionally</label><br />
                        <input type="radio" name="grpRadio_4" value="Sometimes|50|4" id="rid_50" /><label for="rid_50">Sometimes</label><br />
                        <input type="radio" name="grpRadio_4" value="Often|52|5" id="rid_52" /><label for="rid_52">Often</label><br />
                        <input type="radio" name="grpRadio_4" value="Very Often|54|6" id="rid_54" /><label for="rid_54">Very Often</label><br />
                        <input type="radio" name="grpRadio_4" value="Always|56|7" id="rid_56" /><label for="rid_56">Always</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID5">
                <td>
                    <strong>I am confident in _______________ 's ability to help me.</strong>
                    <div>
                        <input type="radio" name="grpRadio_5" value="Never|58|1" id="rid_58" /><label for="rid_58">Never</label><br />
                        <input type="radio" name="grpRadio_5" value="Rarely|60|2" id="rid_60" /><label for="rid_60">Rarely</label><br />
                        <input type="radio" name="grpRadio_5" value="Occasionally|62|3" id="rid_62" /><label for="rid_62">Occasionally</label><br />
                        <input type="radio" name="grpRadio_5" value="Sometimes|64|4" id="rid_64" /><label for="rid_64">Sometimes</label><br />
                        <input type="radio" name="grpRadio_5" value="Often|66|5" id="rid_66" /><label for="rid_66">Often</label><br />
                        <input type="radio" name="grpRadio_5" value="Very Often|68|6" id="rid_68" /><label for="rid_68">Very Often</label><br />
                        <input type="radio" name="grpRadio_5" value="Always|70|7" id="rid_70" /><label for="rid_70">Always</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID6">
                <td>
                    <strong>_______________ and I are working towards mutually agreed upon goals.</strong>
                    <div>
                        <input type="radio" name="grpRadio_6" value="Never|72|1" id="rid_72" /><label for="rid_72">Never</label><br />
                        <input type="radio" name="grpRadio_6" value="Rarely|74|2" id="rid_74" /><label for="rid_74">Rarely</label><br />
                        <input type="radio" name="grpRadio_6" value="Occasionally|76|3" id="rid_76" /><label for="rid_76">Occasionally</label><br />
                        <input type="radio" name="grpRadio_6" value="Sometimes|78|4" id="rid_78" /><label for="rid_78">Sometimes</label><br />
                        <input type="radio" name="grpRadio_6" value="Often|80|5" id="rid_80" /><label for="rid_80">Often</label><br />
                        <input type="radio" name="grpRadio_6" value="Very Often|82|6" id="rid_82" /><label for="rid_82">Very Often</label><br />
                        <input type="radio" name="grpRadio_6" value="Always|84|7" id="rid_84" /><label for="rid_84">Always</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID7">
                <td>
                    <strong>I feel that _______________ appreciates me.</strong>
                    <div>
                        <input type="radio" name="grpRadio_7" value="Never|86|1" id="rid_86" /><label for="rid_86">Never</label><br />
                        <input type="radio" name="grpRadio_7" value="Rarely|88|2" id="rid_88" /><label for="rid_88">Rarely</label><br />
                        <input type="radio" name="grpRadio_7" value="Occasionally|90|3" id="rid_90" /><label for="rid_90">Occasionally</label><br />
                        <input type="radio" name="grpRadio_7" value="Sometimes|92|4" id="rid_92" /><label for="rid_92">Sometimes</label><br />
                        <input type="radio" name="grpRadio_7" value="Often|94|5" id="rid_94" /><label for="rid_94">Often</label><br />
                        <input type="radio" name="grpRadio_7" value="Very Often|96|6" id="rid_96" /><label for="rid_96">Very Often</label><br />
                        <input type="radio" name="grpRadio_7" value="Always|98|7" id="rid_98" /><label for="rid_98">Always</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID8">
                <td>
                    <strong>We agree on what is important for me to work on.</strong>
                    <div>
                        <input type="radio" name="grpRadio_8" value="Never|100|1" id="rid_100" /><label for="rid_100">Never</label><br />
                        <input type="radio" name="grpRadio_8" value="Rarely|102|2" id="rid_102" /><label for="rid_102">Rarely</label><br />
                        <input type="radio" name="grpRadio_8" value="Occasionally|104|3" id="rid_104" /><label for="rid_104">Occasionally</label><br />
                        <input type="radio" name="grpRadio_8" value="Sometimes|106|4" id="rid_106" /><label for="rid_106">Sometimes</label><br />
                        <input type="radio" name="grpRadio_8" value="Often|108|5" id="rid_108" /><label for="rid_108">Often</label><br />
                        <input type="radio" name="grpRadio_8" value="Very Often|110|6" id="rid_110" /><label for="rid_110">Very Often</label><br />
                        <input type="radio" name="grpRadio_8" value="Always|112|7" id="rid_112" /><label for="rid_112">Always</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID9">
                <td>
                    <strong>_______________ and I trust one another.</strong>
                    <div>
                        <input type="radio" name="grpRadio_9" value="Never|114|1" id="rid_114" /><label for="rid_114">Never</label><br />
                        <input type="radio" name="grpRadio_9" value="Rarely|116|2" id="rid_116" /><label for="rid_116">Rarely</label><br />
                        <input type="radio" name="grpRadio_9" value="Occasionally|118|3" id="rid_118" /><label for="rid_118">Occasionally</label><br />
                        <input type="radio" name="grpRadio_9" value="Sometimes|120|4" id="rid_120" /><label for="rid_120">Sometimes</label><br />
                        <input type="radio" name="grpRadio_9" value="Often|122|5" id="rid_122" /><label for="rid_122">Often</label><br />
                        <input type="radio" name="grpRadio_9" value="Very Often|124|6" id="rid_124" /><label for="rid_124">Very Often</label><br />
                        <input type="radio" name="grpRadio_9" value="Always|126|7" id="rid_126" /><label for="rid_126">Always</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID10">
                <td>
                    <strong>_______________ and I have different ideas on what my problems are.</strong>
                    <div>
                        <input type="radio" name="grpRadio_10" value="Never|128|1" id="rid_128" /><label for="rid_128">Never</label><br />
                        <input type="radio" name="grpRadio_10" value="Rarely|130|2" id="rid_130" /><label for="rid_130">Rarely</label><br />
                        <input type="radio" name="grpRadio_10" value="Occasionally|132|3" id="rid_132" /><label for="rid_132">Occasionally</label><br />
                        <input type="radio" name="grpRadio_10" value="Sometimes|134|4" id="rid_134" /><label for="rid_134">Sometimes</label><br />
                        <input type="radio" name="grpRadio_10" value="Often|136|5" id="rid_136" /><label for="rid_136">Often</label><br />
                        <input type="radio" name="grpRadio_10" value="Very Often|138|6" id="rid_138" /><label for="rid_138">Very Often</label><br />
                        <input type="radio" name="grpRadio_10" value="Always|140|7" id="rid_140" /><label for="rid_140">Always</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID11">
                <td>
                    <strong>We have established a good understanding of the kind of changes that would be good for me.</strong>
                    <div>
                        <input type="radio" name="grpRadio_11" value="Never|142|1" id="rid_142" /><label for="rid_142">Never</label><br />
                        <input type="radio" name="grpRadio_11" value="Rarely|144|2" id="rid_144" /><label for="rid_144">Rarely</label><br />
                        <input type="radio" name="grpRadio_11" value="Occasionally|146|3" id="rid_146" /><label for="rid_146">Occasionally</label><br />
                        <input type="radio" name="grpRadio_11" value="Sometimes|148|4" id="rid_148" /><label for="rid_148">Sometimes</label><br />
                        <input type="radio" name="grpRadio_11" value="Often|150|5" id="rid_150" /><label for="rid_150">Often</label><br />
                        <input type="radio" name="grpRadio_11" value="Very Often|152|6" id="rid_152" /><label for="rid_152">Very Often</label><br />
                        <input type="radio" name="grpRadio_11" value="Always|154|7" id="rid_154" /><label for="rid_154">Always</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID12">
                <td>
                    <strong>I believe the way we are working with my problem is correct.</strong>
                    <div>
                        <input type="radio" name="grpRadio_12" value="Never|156|1" id="rid_156" /><label for="rid_156">Never</label><br />
                        <input type="radio" name="grpRadio_12" value="Rarely|158|2" id="rid_158" /><label for="rid_158">Rarely</label><br />
                        <input type="radio" name="grpRadio_12" value="Occasionally|160|3" id="rid_160" /><label for="rid_160">Occasionally</label><br />
                        <input type="radio" name="grpRadio_12" value="Sometimes|162|4" id="rid_162" /><label for="rid_162">Sometimes</label><br />
                        <input type="radio" name="grpRadio_12" value="Often|164|5" id="rid_164" /><label for="rid_164">Often</label><br />
                        <input type="radio" name="grpRadio_12" value="Very Often|166|6" id="rid_166" /><label for="rid_166">Very Often</label><br />
                        <input type="radio" name="grpRadio_12" value="Always|168|7" id="rid_168" /><label for="rid_168">Always</label><br />
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