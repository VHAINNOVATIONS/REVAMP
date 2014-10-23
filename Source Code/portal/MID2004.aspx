<%@ Page Title="REVAMP Portal - Follow-up Questionnaire" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid2004.aspx.cs" Inherits="mid2004" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
    <style type="text/css">
        .tbl-quest {
            width: 890px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

        <!-- this tells the code behind how many responces to process -->
        <input type="hidden" name="ResponseCount" value="80" /><br />

        <h1>Patient Follow-up Clinical Questionnaire</h1>
        <h3>To be completed prior to each follow-up phone call so the responses can be reviewed by the provider during the phone call.</h3>
        <h3>Section 1:</h3>

        <table class="tbl-quest">


            <tr id="TID1_QID1">
                <td>
                    <strong>Please rate the usual quality of your current sleep:</strong>
                    <div>
                        <input type="radio" name="grpRadio_1" value="very good|2|0" id="rid_2" /><label for="rid_2">very good</label><br />
                        <input type="radio" name="grpRadio_1" value="good|4|0" id="rid_4" /><label for="rid_4">good</label><br />
                        <input type="radio" name="grpRadio_1" value="fair|6|0" id="rid_6" /><label for="rid_6">fair</label><br />
                        <input type="radio" name="grpRadio_1" value="poor|8|0" id="rid_8" /><label for="rid_8">poor</label><br />
                        <input type="radio" name="grpRadio_1" value="very poor|10|0" id="rid_10" /><label for="rid_10">very poor<br />
                        </label>
                        <br />
                    </div>
                </td>
            </tr>
        </table>

        <h3>Section 2: Since your last sleep appointment:</h3>

        <table class="tbl-quest">


            <tr id="TID2_QID1">
                <td>
                    <strong>Have you had any surgeries or hospitalizations?</strong>
                    <div>
                        <input type="radio" name="grpRadio_2" value="Yes|12|0" id="rid_12" /><label for="rid_12">Yes</label><br />
                        <input type="radio" name="grpRadio_2" value="No|14|0" id="rid_14" /><label for="rid_14">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID2_QID2">
                <td>
                    <strong>You had surgeries or hospitalizations for?</strong>
                    <div>
                        <input type="text" name="grpCtrlText_3" id="rid_txt_16" />
                        <input type="hidden" name="grpHidden_3" id="rid_16" value="|16|0" />
                    </div>
                </td>
            </tr>

            <tr id="TID2_QID3">
                <td>
                    <strong>Have you had any changes in your medical problems?</strong>
                    <div>
                        <input type="radio" name="grpRadio_4" value="Yes|18|0" id="rid_18" /><label for="rid_18">Yes</label><br />
                        <input type="radio" name="grpRadio_4" value="No|20|0" id="rid_20" /><label for="rid_20">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID2_QID4">
                <td>
                    <strong>Please elaborate</strong>
                    <div>
                        <input type="text" name="grpCtrlText_5" id="rid_txt_22" />
                        <input type="hidden" name="grpHidden_5" id="rid_22" value="|22|0" />
                    </div>
                </td>
            </tr>

            <tr id="TID2_QID5">
                <td>
                    <strong>Have your work times or home responsibilities changed?</strong>
                    <div>
                        <input type="radio" name="grpRadio_6" value="Yes|26|0" id="rid_26" /><label for="rid_26">Yes</label><br />
                        <input type="radio" name="grpRadio_6" value="No|28|0" id="rid_28" /><label for="rid_28">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID2_QID6">
                <td>
                    <strong>Please elaborate</strong>
                    <div>
                        <input type="text" name="grpCtrlText_7" id="rid_txt_30" />
                        <input type="hidden" name="grpHidden_7" id="rid_30" value="|30|0" />
                    </div>
                </td>
            </tr>

            <tr id="TID2_QID7">
                <td>
                    <strong>Have you had any accidents due to you falling asleep while driving?</strong>
                    <div>
                        <input type="radio" name="grpRadio_8" value="Yes|32|0" id="rid_32" /><label for="rid_32">Yes</label><br />
                        <input type="radio" name="grpRadio_8" value="No|34|0" id="rid_34" /><label for="rid_34">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID2_QID8">
                <td>
                    <strong>Please elaborate</strong>
                    <div>
                        <input type="text" name="grpCtrlText_9" id="rid_txt_36" />
                        <input type="hidden" name="grpHidden_9" id="rid_36" value="|36|0" />
                        <br />
                    </div>
                </td>
            </tr>

        </table>

        <h3>Section 3: Treatment</h3>

        <table class="tbl-quest">

            <tr id="TID3_QID1">
                <td>
                    <strong>With my PAP treatment, my sleep problem is: (Choose &#8220;4= the same&#8221; if you have not started treatment yet).</strong>
                    <div>
                        <input type="radio" name="grpRadio_10" value="1 = Excellent improvement|38|0" id="rid_38" /><label for="rid_38">1 = Excellent improvement</label><br />
                        <input type="radio" name="grpRadio_10" value="2 = Much better|40|0" id="rid_40" /><label for="rid_40">2 = Much better</label><br />
                        <input type="radio" name="grpRadio_10" value="3 = A bit better|42|0" id="rid_42" /><label for="rid_42">3 = A bit better</label><br />
                        <input type="radio" name="grpRadio_10" value="4 = The same|44|0" id="rid_44" /><label for="rid_44">4 = The same</label><br />
                        <input type="radio" name="grpRadio_10" value="5 = Worse |46|0" id="rid_46" /><label for="rid_46">5 = Worse<br />
                        </label>
                        <br />

                    </div>
                </td>
            </tr>
        </table>

        <h3>Section 5: CPAP/ BiPAP Use and Equipment Questions</h3>

        <table class="tbl-quest">

            <tr id="TID4_QID1">
                <td>
                    <strong>I use my PAP:</strong>
                    <div>
                        <input type="radio" name="grpRadio_11" value="nightly|48|0" id="rid_48" /><label for="rid_48">nightly</label><br />
                        <input type="radio" name="grpRadio_11" value="most nights|50|0" id="rid_50" /><label for="rid_50">most nights</label><br />
                        <input type="radio" name="grpRadio_11" value="inconsistently|52|0" id="rid_52" /><label for="rid_52">inconsistently</label><br />

                    </div>
                </td>
            </tr>

            <tr id="TID4_QID2">
                <td>
                    <strong>I have problems using my PAP all night long and/or with any naps</strong>
                    <div>
                        <input type="radio" name="grpRadio_12" value="Yes|54|0" id="rid_54" /><label for="rid_54">Yes</label><br />
                        <input type="radio" name="grpRadio_12" value="No|56|0" id="rid_56" /><label for="rid_56">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID4_QID3">
                <td>
                    <strong>I may fall asleep before my mask/ PAP is on.</strong>
                    <div>
                        <input type="radio" name="grpRadio_13" value="Yes|58|0" id="rid_58" /><label for="rid_58">Yes</label><br />
                        <input type="radio" name="grpRadio_13" value="No|60|0" id="rid_60" /><label for="rid_60">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID4_QID4">
                <td>
                    <strong>I may remove my mask unknowingly while sleeping.</strong>
                    <div>
                        <input type="radio" name="grpRadio_14" value="Yes|62|0" id="rid_62" /><label for="rid_62">Yes</label><br />
                        <input type="radio" name="grpRadio_14" value="No|64|0" id="rid_64" /><label for="rid_64">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID4_QID5">
                <td>
                    <strong>I may forget to put my mask/ PAP back on after going to the bathroom.</strong>
                    <div>
                        <input type="radio" name="grpRadio_15" value="Yes|66|0" id="rid_66" /><label for="rid_66">Yes</label><br />
                        <input type="radio" name="grpRadio_15" value="No|68|0" id="rid_68" /><label for="rid_68">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID4_QID6">
                <td>
                    <strong>I may forget to use my PAP with naps.</strong>
                    <div>
                        <input type="radio" name="grpRadio_16" value="Yes|70|0" id="rid_70" /><label for="rid_70">Yes</label><br />
                        <input type="radio" name="grpRadio_16" value="No|72|0" id="rid_72" /><label for="rid_72">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID4_QID7">
                <td>
                    <strong>I may forget to take my PAP when I travel. </strong>
                    <div>
                        <input type="radio" name="grpRadio_17" value="Yes|74|0" id="rid_74" /><label for="rid_74">Yes</label><br />
                        <input type="radio" name="grpRadio_17" value="No|76|0" id="rid_76" /><label for="rid_76">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID4_QID8">
                <td>
                    <strong>I think that I use my PAP for </strong>
                    <div>
                        <input type="text" name="grpCtrlText_18" id="rid_txt_78" />
                        hours/ 24 hours (include sleep and if any naps). 
                        <input type="hidden" name="grpHidden_18" id="rid_78" value="|78|0" />

                    </div>
                </td>
            </tr>

            <tr id="TID4_QID9">
                <td>
                    <strong>In the past month, I think that I have missed </strong>
                    <div>
                        <input type="text" name="grpCtrlText_19" id="rid_txt_80" />
                        number of nights per week.<br />
                        &nbsp;<input type="hidden" name="grpHidden_19" id="rid_80" value="|80|0" />
                    </div>
                </td>
            </tr>

        </table>

        <h3>HEATED HUMIDIFIER: </h3>

        <table class="tbl-quest">

            <tr id="TID5_QID1">
                <td>
                    <strong>I use my heated humidifier:</strong>
                    <div>
                        <input type="radio" name="grpRadio_20" value="nightly|82|0" id="rid_82" /><label for="rid_82">nightly</label><br />
                        <input type="radio" name="grpRadio_20" value="most nights|84|0" id="rid_84" /><label for="rid_84">most nights</label><br />
                        <input type="radio" name="grpRadio_20" value="inconsistently|86|0" id="rid_86" /><label for="rid_86">inconsistently</label><br />
                        <input type="radio" name="grpRadio_20" value="rarely or never|88|0" id="rid_88" /><label for="rid_88">rarely or never</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID5_QID2">
                <td>
                    <strong>The heated humidifier is set on:</strong>
                    <div>
                        <input type="radio" name="grpRadio_21" value="1|90|0" id="rid_90" /><label for="rid_90">1</label><br />
                        <input type="radio" name="grpRadio_21" value="2|92|0" id="rid_92" /><label for="rid_92">2</label><br />
                        <input type="radio" name="grpRadio_21" value="3|94|0" id="rid_94" /><label for="rid_94">3</label><br />
                        <input type="radio" name="grpRadio_21" value="4|96|0" id="rid_96" /><label for="rid_96">4</label><br />
                        <input type="radio" name="grpRadio_21" value="5|98|0" id="rid_98" /><label for="rid_98">5</label><br />
                        <input type="radio" name="grpRadio_21" value="Not sure|100|0" id="rid_100" /><label for="rid_100">Not sure</label><br />
                        <input type="radio" name="grpRadio_21" value="Off|102|0" id="rid_102" /><label for="rid_102">Off</label><br />

                    </div>
                </td>
            </tr>

            <tr id="TID5_QID3">
                <td>
                    <strong>I have nasal congestion/runny nose</strong>
                    <div>
                        <input type="radio" name="grpRadio_22" value="Never/rarely |104|0" id="rid_104" /><label for="rid_104">Never/rarely</label><br />
                        <input type="radio" name="grpRadio_22" value="Occasionally|106|0" id="rid_106" /><label for="rid_106">Occasionally</label><br />
                        <input type="radio" name="grpRadio_22" value="Frequently|108|0" id="rid_108" /><label for="rid_108">Frequently</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID5_QID4">
                <td>
                    <strong>I have a dry mouth/throat</strong>
                    <div>
                        <input type="radio" name="grpRadio_23" value="Never/rarely |110|0" id="rid_110" /><label for="rid_110">Never/rarely</label><br />
                        <input type="radio" name="grpRadio_23" value="Occasionally|112|0" id="rid_112" /><label for="rid_112">Occasionally</label><br />
                        <input type="radio" name="grpRadio_23" value="Frequently|114|0" id="rid_114" /><label for="rid_114">Frequently</label><br />
                    </div>
                </td>
            </tr>

        </table>

        <h3>MASK:</h3>

        <table class="tbl-quest">

            <tr id="TID6_QID1">
                <td>
                    <strong>My current mask type is:</strong>
                    <div>
                        <input type="radio" name="grpRadio_25" value="Nasal pillows/prongs |122|0" id="rid_122" /><label for="rid_122">Nasal pillows/prongs</label><br />
                        <input type="radio" name="grpRadio_25" value="Nasal mask|124|0" id="rid_124" /><label for="rid_124">Nasal mask</label><br />
                        <input type="radio" name="grpRadio_25" value="Full face mask|126|0" id="rid_126" /><label for="rid_126">Full face mask</label><br />
                        <input type="radio" name="grpRadio_25" value="Not sure|128|0" id="rid_128" /><label for="rid_128">Not sure</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID6_QID2">
                <td>
                    <strong>My current mask size is:</strong>
                    <div>
                        <input type="radio" name="grpRadio_26" value="Petite|130|0" id="rid_130" /><label for="rid_130">Petite</label><br />
                        <input type="radio" name="grpRadio_26" value="XS|132|0" id="rid_132" /><label for="rid_132">XS</label><br />
                        <input type="radio" name="grpRadio_26" value="Small|134|0" id="rid_134" /><label for="rid_134">Small</label><br />
                        <input type="radio" name="grpRadio_26" value="Wide|136|0" id="rid_136" /><label for="rid_136">Wide</label><br />
                        <input type="radio" name="grpRadio_26" value="Medium|138|0" id="rid_138" /><label for="rid_138">Medium</label><br />
                        <input type="radio" name="grpRadio_26" value="Large|140|0" id="rid_140" /><label for="rid_140">Large</label><br />
                        <input type="radio" name="grpRadio_26" value="Large wide|142|0" id="rid_142" /><label for="rid_142">Large wide</label><br />
                        <input type="radio" name="grpRadio_26" value="Standard|144|0" id="rid_144" /><label for="rid_144">Standard</label><br />
                        <input type="radio" name="grpRadio_26" value="Not sure|145|0" id="rid_145" /><label for="rid_145">Not sure</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID6_QID3">
                <td>
                    <strong>Check all that apply:</strong>
                    <div>
                        <input type="checkbox" name="grpCheck_27" value="I like my current mask and want to stay with the same mask.|146|0" id="rid_146" />
                        <label for="rid_146">I like my current mask and want to stay with the same mask. </label>
                        <br />
                        <input type="checkbox" name="grpCheck_28" value="I like my current mask and but am interested in looking at other masks.|148|0" id="rid_148" />
                        <label for="rid_148">I like my current mask and but am interested in looking at other masks. </label>
                        <br />
                        <input type="checkbox" name="grpCheck_29" value="I do not like my current mask.|150|0" id="rid_150" />
                        <label for="rid_150">I do not like my current mask </label>
                        <br />
                    </div>
                </td>
            </tr>

            <tr id="TID6_QID4">
                <td>
                    <strong>I do not like my current mask because: (check all that apply)</strong>
                    <div>
                        <input type="checkbox" name="grpCheck_30" value="Infrequent mask air leakage|152|0" id="rid_152" />
                        <label for="rid_152">Infrequent mask air leakage</label><br />
                        <input type="checkbox" name="grpCheck_31" value="Occasional mask air leakage|154|0" id="rid_154" />
                        <label for="rid_154">Occasional mask air leakage</label><br />
                        <input type="checkbox" name="grpCheck_32" value="Frequent mask air leakage|156|0" id="rid_156" />
                        <label for="rid_156">Frequent mask air leakage</label><br />
                        <input type="checkbox" name="grpCheck_33" value="Frequently have to adjust or fuss with the mask|158|0" id="rid_158" />
                        <label for="rid_158">Frequently have to adjust or fuss with the mask</label><br />
                        <input type="checkbox" name="grpCheck_34" value="Trouble with the air pressure|160|0" id="rid_160" />
                        <label for="rid_160">Trouble with the air pressure</label><br />
                        <input type="checkbox" name="grpCheck_35" value="Skin irritation|162|0" id="rid_162" />
                        <label for="rid_162">Skin irritation</label><br />
                        <input type="checkbox" name="grpCheck_36" value="Uncomfortable|164|0" id="rid_164" />
                        <label for="rid_164">Uncomfortable</label><br />
                        <input type="checkbox" name="grpCheck_37" value="Eye irritation|166|0" id="rid_166" />
                        <label for="rid_166">Eye irritation</label><br />
                        <input type="checkbox" name="grpCheck_38" value="Difficult for me to put on and take off|168|0" id="rid_168" />
                        <label for="rid_168">Difficult for me to put on and take off</label><br />
                        <input type="checkbox" name="grpCheck_39" value="Nose bleeds|170|0" id="rid_170" />
                        <label for="rid_170">Nose bleeds</label><br />
                        <input type="checkbox" name="grpCheck_40" value="Frequently wakes me up|172|0" id="rid_172" />
                        <label for="rid_172">Frequently wakes me up</label><br />
                        <input type="checkbox" name="grpCheck_41" value="Noisy|174|0" id="rid_174" />
                        <label for="rid_174">Noisy</label><br />
                        <input type="checkbox" name="grpCheck_42" value="Pressure on nasal bridge or nostrils|176|0" id="rid_176" />
                        <label for="rid_176">Pressure on nasal bridge or nostrils</label><br />
                        <input type="checkbox" name="grpCheck_43" value="Water condensation in the mask|178|0" id="rid_178" />
                        <label for="rid_178">Water condensation in the mask</label><br />
                        <input type="checkbox" name="grpCheck_44" value="I wish I could wear glasses with it|180|0" id="rid_180" />
                        <label for="rid_180">I wish I could wear glasses with it</label><br />
                        <input type="checkbox" name="grpCheck_45" value="My bed partner complains about it|182|0" id="rid_182" />
                        <label for="rid_182">My bed partner complains about it</label><br />
                        <input type="checkbox" name="grpCheck_46" value="I need a different size|184|0" id="rid_184" />
                        <label for="rid_184">I need a different size</label><br />
                        <input type="checkbox" name="grpCheck_47" value="Other|186|0" id="rid_186" />
                        <label for="rid_186">Other</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID6_QID5">
                <td>
                    <strong>I do not like my current mask because: (please explain)</strong>
                    <div>
                        <input type="text" name="grpCtrlText_48" id="rid_txt_188" />.<br />
                        &nbsp;<input type="hidden" name="grpHidden_48" id="rid_188" value="|188|0" />
                    </div>
                </td>
            </tr>

        </table>

        <h3>IMPROVEMENTS ON PAP: </h3>

        <table class="tbl-quest">

            <tr id="TID7_QID1">
                <td>
                    <strong>Check all that apply</strong>
                    <div>
                        <input type="checkbox" name="grpCheck_49" value="I snore less or not at all|190|0" id="rid_190" />
                        <label for="rid_190">I snore less or not at all</label><br />
                        <input type="checkbox" name="grpCheck_50" value="My nasal congestion/ allergies are better|192|0" id="rid_192" />
                        <label for="rid_192">My nasal congestion/ allergies are better</label><br />
                        <input type="checkbox" name="grpCheck_51" value="My memory and concentration are better|194|0" id="rid_194" />
                        <label for="rid_194">My memory and concentration are better</label><br />
                        <input type="checkbox" name="grpCheck_52" value="I have fewer headaches when I wake up|196|0" id="rid_196" />
                        <label for="rid_196">I have fewer headaches when I wake up</label><br />
                        <input type="checkbox" name="grpCheck_53" value="My blood pressure is better|198|0" id="rid_198" />
                        <label for="rid_198">My blood pressure is better</label><br />
                        <input type="checkbox" name="grpCheck_54" value="My GERD (reflux/ heartburn) is better|200|0" id="rid_200" />
                        <label for="rid_200">My GERD (reflux/ heartburn) is better</label><br />
                        <input type="checkbox" name="grpCheck_55" value="I feel more refreshed when I wake up|202|0" id="rid_202" />
                        <label for="rid_202">I feel more refreshed when I wake up</label><br />
                        <input type="checkbox" name="grpCheck_56" value="I am less sleepy during the day|204|0" id="rid_204" />
                        <label for="rid_204">I am less sleepy during the day</label><br />
                        <input type="checkbox" name="grpCheck_57" value="I have less choking/gasping in my sleep|206|0" id="rid_206" />
                        <label for="rid_206">I have less choking/gasping in my sleep</label><br />
                        <input type="checkbox" name="grpCheck_58" value="I have to use the bathroom less often at night|208|0" id="rid_208" />
                        <label for="rid_208">I have to use the bathroom less often at night</label><br />
                        <input type="checkbox" name="grpCheck_59" value="My sleep quality is better|210|0" id="rid_210" />
                        <label for="rid_210">My sleep quality is better</label><br />
                        <input type="checkbox" name="grpCheck_60" value="I am not napping or dozing off as often|212|0" id="rid_212" />
                        <label for="rid_212">I am not napping or dozing off as often</label><br />
                        <input type="checkbox" name="grpCheck_61" value="I wake up less often|214|0" id="rid_214" />
                        <label for="rid_214">I wake up less often</label><br />
                        <input type="checkbox" name="grpCheck_62" value="I have more energy|216|0" id="rid_216" />
                        <label for="rid_216">I have more energy</label><br />
                        <input type="checkbox" name="grpCheck_63" value="I do not toss & turn/move in my sleep as much|218|0" id="rid_218" />
                        <label for="rid_218">I do not toss & turn/move in my sleep as much</label><br />
                        <input type="checkbox" name="grpCheck_64" value="I am more alert during the day|220|0" id="rid_220" />
                        <label for="rid_220">I am more alert during the day</label><br />
                        <input type="checkbox" name="grpCheck_65" value="I go to the bathroom less often|222|0" id="rid_222" />
                        <label for="rid_222">I go to the bathroom less often</label><br />
                        <input type="checkbox" name="grpCheck_66" value="My mood is better|224|0" id="rid_224" />
                        <label for="rid_224">My mood is better</label><br />
                        <input type="checkbox" name="grpCheck_67" value="Other|226|0" id="rid_226" />
                        <label for="rid_226">Other</label><br />

                    </div>
                </td>
            </tr>

            <tr id="TID7_QID2">
                <td>
                    <strong>Other improvements: (please explain)</strong>
                    <div>
                        <input type="text" name="grpCtrlText_68" id="rid_txt_228" /><br />
                        &nbsp;<input type="hidden" name="grpHidden_68" id="rid_228" value="|228|0" />
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
                rid: 12,
                checked: {
                    show: ['TID2_QID2']
                }
            },

            {
                rid: 18,
                checked: {
                    show: ['TID2_QID4']
                }
            },

            {
                rid: 26,
                checked: {
                    show: ['TID2_QID6']
                }
            },

            {
                rid: 32,
                checked: {
                    show: ['TID2_QID8']
                }
            },

            {
                rid: 54,
                checked: {
                    show: ['TID4_QID3', 'TID4_QID4', 'TID4_QID5', 'TID4_QID6', 'TID4_QID7']
                }
            },

            {
                rid: 150,
                checked: {
                    show: ['TID6_QID4']
                }
            },

            {
                rid: 186,
                checked: {
                    show: ['TID6_QID5']
                }
            },

            {
                rid: 226,
                checked: {
                    show: ['TID7_QID2']
                }
            }

        ];


    </script>
</asp:Content>