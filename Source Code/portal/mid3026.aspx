<%@ Page Title="Self-efficacy questionnaire" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid3026.aspx.cs" Inherits="mid3026" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

        <!-- this tells the code behind how many responces to process -->
        <input type="hidden" name="ResponseCount" value="31" /><br />

        <h1>SELF-EFFICACY QUESTIONNAIRE</h1>
        <br />

        <table class="tbl-quest">
            <tr id="TID1_QID1">
                <td>
                    <strong>Directions:  For each item below, please select the response that best describes you over the next month.</strong>
                    <br />
                    <br />
                    <strong>1. I am confident I can use automatic positive airway pressure device (APAP) regularly</strong>
                    <div>
                        <input type="radio" name="grpRadio_1" value="1 - Disagree completely|2|1" id="rid_2" /><label for="rid_2">1 - Disagree completely</label><br />
                        <input type="radio" name="grpRadio_1" value="2 -|4|2" id="rid_4" /><label for="rid_4">2 -</label><br />
                        <input type="radio" name="grpRadio_1" value="3 -|6|3" id="rid_6" /><label for="rid_6">3 -</label><br />
                        <input type="radio" name="grpRadio_1" value="4 -|8|4" id="rid_8" /><label for="rid_8">4 -</label><br />
                        <input type="radio" name="grpRadio_1" value="5 - Agree completely|10|5" id="rid_10" /><label for="rid_10">5 - Agree completely</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID2">
                <td>
                    <strong>2. I have the ability to use APAP regularly.</strong>
                    <div>
                        <input type="radio" name="grpRadio_2" value="1 - Disagree completely|12|1" id="rid_12" /><label for="rid_12">1 - Disagree completely</label><br />
                        <input type="radio" name="grpRadio_2" value="2 -|14|2" id="rid_14" /><label for="rid_14">2 -</label><br />
                        <input type="radio" name="grpRadio_2" value="3 -|16|3" id="rid_16" /><label for="rid_16">3 -</label><br />
                        <input type="radio" name="grpRadio_2" value="4 -|18|4" id="rid_18" /><label for="rid_18">4 -</label><br />
                        <input type="radio" name="grpRadio_2" value="5 - Agree completely|20|0" id="rid_20" /><label for="rid_20">5 - Agree completely</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID3">
                <td>
                    <strong>3. I am confident I will use APAP regularly even if I do not feel like it</strong>
                    <div>
                        <input type="radio" name="grpRadio_3" value="1 - Disagree completely|22|1" id="rid_22" /><label for="rid_22">1 - Disagree completely</label><br />
                        <input type="radio" name="grpRadio_3" value="2 -|24|2" id="rid_24" /><label for="rid_24">2 -</label><br />
                        <input type="radio" name="grpRadio_3" value="3 -|26|3" id="rid_26" /><label for="rid_26">3 -</label><br />
                        <input type="radio" name="grpRadio_3" value="4 -|28|4" id="rid_28" /><label for="rid_28">4 -</label><br />
                        <input type="radio" name="grpRadio_3" value="5 - Agree completely|30|0" id="rid_30" /><label for="rid_30">5 - Agree completely</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID4">
                <td>
                    <strong>4. I am confident I will use APAP regularly even if I experience uncomfortable side effects</strong>
                    <div>
                        <input type="radio" name="grpRadio_4" value="1 - Disagree completely|32|1" id="rid_32" /><label for="rid_32">1 - Disagree completely</label><br />
                        <input type="radio" name="grpRadio_4" value="2 -|34|2" id="rid_34" /><label for="rid_34">2 -</label><br />
                        <input type="radio" name="grpRadio_4" value="3 -|36|3" id="rid_36" /><label for="rid_36">3 -</label><br />
                        <input type="radio" name="grpRadio_4" value="4 -|38|4" id="rid_38" /><label for="rid_38">4 -</label><br />
                        <input type="radio" name="grpRadio_4" value="5 - Agree completely|40|0" id="rid_40" /><label for="rid_40">5 - Agree completely</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID5">
                <td>
                    <strong>5. I can operate the APAP machine to make it  more comfortable for me</strong>
                    <div>
                        <input type="radio" name="grpRadio_5" value="1 - Disagree completely|42|1" id="rid_42" /><label for="rid_42">1 - Disagree completely</label><br />
                        <input type="radio" name="grpRadio_5" value="2 -|44|2" id="rid_44" /><label for="rid_44">2 -</label><br />
                        <input type="radio" name="grpRadio_5" value="3 -|46|3" id="rid_46" /><label for="rid_46">3 -</label><br />
                        <input type="radio" name="grpRadio_5" value="4 -|48|4" id="rid_48" /><label for="rid_48">4 -</label><br />
                        <input type="radio" name="grpRadio_5" value="5 - Agree completely|50|0" id="rid_50" /><label for="rid_50">5 - Agree completely</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID6">
                <td>
                    <br />
                    <strong>OUTCOME EXPECTATIONS</strong>
                    <br />
                    <strong>Directions:  For each item below, please circle the response that best describes your rating of each item.</strong>
                    <br />
                    <strong>1. How effective do you believe regular use of APAP is:</strong>
                    <br />
                    <strong>a.  managing your sleep apnea?</strong>
                    <div>
                        <input type="radio" name="grpRadio_6" value="1 - Not at all important|300|1" id="rid_300" /><label for="rid_300">1 - Not at all important</label><br />
                        <input type="radio" name="grpRadio_6" value="2 -|302|2" id="rid_302" /><label for="rid_302">2 -</label><br />
                        <input type="radio" name="grpRadio_6" value="3 -|304|3" id="rid_304" /><label for="rid_304">3 -</label><br />
                        <input type="radio" name="grpRadio_6" value="4 -|306|4" id="rid_306" /><label for="rid_306">4 -</label><br />
                        <input type="radio" name="grpRadio_6" value="5 - Extremely important|308|0" id="rid_308" /><label for="rid_308">5 - Extremely important</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID7">
                <td>
                    <strong>b.  in reducing your daytime sleepiness?</strong>
                    <div>
                        <input type="radio" name="grpRadio_7" value="1 - Not at all important|62|1" id="rid_62" /><label for="rid_62">1 - Not at all important</label><br />
                        <input type="radio" name="grpRadio_7" value="2 -|64|2" id="rid_64" /><label for="rid_64">2 -</label><br />
                        <input type="radio" name="grpRadio_7" value="3 -|66|3" id="rid_66" /><label for="rid_66">3 -</label><br />
                        <input type="radio" name="grpRadio_7" value="4 -|68|4" id="rid_68" /><label for="rid_68">4 -</label><br />
                        <input type="radio" name="grpRadio_7" value="5 - Extremely important|70|0" id="rid_70" /><label for="rid_70">5 - Extremely important</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID8">
                <td>
                    <strong>c.  in improving your ability to concentrate?</strong>
                    <div>
                        <input type="radio" name="grpRadio_8" value="1 - Not at all important|72|1" id="rid_72" /><label for="rid_72">1 - Not at all important</label><br />
                        <input type="radio" name="grpRadio_8" value="2 -|74|2" id="rid_74" /><label for="rid_74">2 -</label><br />
                        <input type="radio" name="grpRadio_8" value="3 -|76|3" id="rid_76" /><label for="rid_76">3 -</label><br />
                        <input type="radio" name="grpRadio_8" value="4 -|78|4" id="rid_78" /><label for="rid_78">4 -</label><br />
                        <input type="radio" name="grpRadio_8" value="5 - Extremely important|80|0" id="rid_80" /><label for="rid_80">5 - Extremely important</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID9">
                <td>
                    <strong>2.  How important do you believe regular use of APAP is for controlling your sleep apnea?</strong>
                    <div>
                        <input type="radio" name="grpRadio_9" value="1 - Not at all important|82|1" id="rid_82" /><label for="rid_82">1 - Not at all important</label><br />
                        <input type="radio" name="grpRadio_9" value="2 -|84|2" id="rid_84" /><label for="rid_84">2 -</label><br />
                        <input type="radio" name="grpRadio_9" value="3 -|86|3" id="rid_86" /><label for="rid_86">3 -</label><br />
                        <input type="radio" name="grpRadio_9" value="4 -|88|4" id="rid_88" /><label for="rid_88">4 -</label><br />
                        <input type="radio" name="grpRadio_9" value="5 - Extremely important|90|5" id="rid_90" /><label for="rid_90">5 - Extremely important</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID10">
                <td>
                    <br />
                    <strong>KNOWLEDGE</strong>
                    <br />
                    <strong>Directions:  Please select the “True” if the answer is true, or “False” if the answer is false.</strong>
                    <br />
                    <br />
                    <strong>1.  One of the main symptoms of sleep apnea is excessive daytime sleepiness.</strong>
                    <div>
                        <input type="radio" name="grpRadio_10" value="True|92|1" id="rid_92" /><label for="rid_92">True</label><br />
                        <input type="radio" name="grpRadio_10" value="False|94|0" id="rid_94" /><label for="rid_94">False</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID11">
                <td>
                    <strong>2.  If APAP is not comfortable to use, it should be permanently discontinued.</strong>
                    <div>
                        <input type="radio" name="grpRadio_11" value="True|96|1" id="rid_96" /><label for="rid_96">True</label><br />
                        <input type="radio" name="grpRadio_11" value="False|98|0" id="rid_98" /><label for="rid_98">False</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID12">
                <td>
                    <strong>3.  Being overweight can make sleep apnea worse.</strong>
                    <div>
                        <input type="radio" name="grpRadio_12" value="True|104|1" id="rid_104" /><label for="rid_104">True</label><br />
                        <input type="radio" name="grpRadio_12" value="False|106|0" id="rid_106" /><label for="rid_106">False</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID13">
                <td>
                    <strong>4.  Only one type of APAP mask is available.</strong>
                    <div>
                        <input type="radio" name="grpRadio_13" value="True|108|1" id="rid_108" /><label for="rid_108">True</label><br />
                        <input type="radio" name="grpRadio_13" value="False|110|0" id="rid_110" /><label for="rid_110">False</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID14">
                <td>
                    <strong>5.  Sleep apnea may contribute to thinking problems, such as memory loss and difficulty concentrating.</strong>
                    <div>
                        <input type="radio" name="grpRadio_14" value="True|112|1" id="rid_112" /><label for="rid_112">True</label><br />
                        <input type="radio" name="grpRadio_14" value="False|114|0" id="rid_114" /><label for="rid_114">False</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID15">
                <td>
                    <strong>6.  APAP results in a cure of sleep apnea.</strong>
                    <div>
                        <input type="radio" name="grpRadio_15" value="True|116|1" id="rid_116" /><label for="rid_116">True</label><br />
                        <input type="radio" name="grpRadio_15" value="False|118|0" id="rid_118" /><label for="rid_118">False</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID16">
                <td>
                    <strong>7.  Sleep apnea may contribute to heart problems and high blood pressure.</strong>
                    <div>
                        <input type="radio" name="grpRadio_16" value="True|120|1" id="rid_120" /><label for="rid_120">True</label><br />
                        <input type="radio" name="grpRadio_16" value="False|122|0" id="rid_122" /><label for="rid_122">False</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID17">
                <td>
                    <strong>8.  For best results, APAP must be used every night.</strong>
                    <div>
                        <input type="radio" name="grpRadio_17" value="True|124|1" id="rid_124" /><label for="rid_124">True</label><br />
                        <input type="radio" name="grpRadio_17" value="False|126|0" id="rid_126" /><label for="rid_126">False</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID18">
                <td>
                    <strong>9.  APAP pressure cannot be adjusted to reduce apneas.</strong>
                    <div>
                        <input type="radio" name="grpRadio_18" value="True|128|1" id="rid_128" /><label for="rid_128">True</label><br />
                        <input type="radio" name="grpRadio_18" value="False|130|0" id="rid_130" /><label for="rid_130">False</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID19">
                <td>
                    <strong>10.  After 2 years of regular use, APAP cures the sleep apnea and does not need to be used anymore.</strong>
                    <div>
                        <input type="radio" name="grpRadio_19" value="True|132|1" id="rid_132" /><label for="rid_132">True</label><br />
                        <input type="radio" name="grpRadio_19" value="False|134|0" id="rid_134" /><label for="rid_134">False</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID20">
                <td>
                    <strong>11.  Sleep apnea is best managed with the help of healthcare professionals trained in sleep disorders medicine.</strong>
                    <div>
                        <input type="radio" name="grpRadio_20" value="True|136|1" id="rid_136" /><label for="rid_136">True</label><br />
                        <input type="radio" name="grpRadio_20" value="False|138|0" id="rid_138" /><label for="rid_138">False</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID21">
                <td>
                    <strong>12.  It is OK to use APAP for only the first three hours of the night.</strong>
                    <div>
                        <input type="radio" name="grpRadio_21" value="True|320|1" id="rid_320" /><label for="rid_320">True</label><br />
                        <input type="radio" name="grpRadio_21" value="False|322|0" id="rid_322" /><label for="rid_322">False</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID22">
                <td>
                    <br />
                    <strong>SOCIAL SUPPORT</strong>
                    <br />
                    <strong>Directions:  Please indicate how much you agree or disagree with each item.</strong>
                    <br />
                    <br />
                    <strong>1.  I have people in my life who support me in using APAP regularly.</strong>
                    <div>
                        <input type="radio" name="grpRadio_22" value="1 - Disagree completely|150|1" id="rid_150" /><label for="rid_150">1 - Disagree completely</label><br />
                        <input type="radio" name="grpRadio_22" value="2 -|152|2" id="rid_152" /><label for="rid_152">2 -</label><br />
                        <input type="radio" name="grpRadio_22" value="3 -|154|3" id="rid_154" /><label for="rid_154">3 -</label><br />
                        <input type="radio" name="grpRadio_22" value="4 -|156|4" id="rid_156" /><label for="rid_156">4 -</label><br />
                        <input type="radio" name="grpRadio_22" value="5 - Agree completely|158|5" id="rid_158" /><label for="rid_158">5 - Agree completely</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID23">
                <td>
                    <strong>2.  I have people in my life who will encourage me to keep using APAP even when it is uncomfortable.</strong>
                    <div>
                        <input type="radio" name="grpRadio_23" value="1 - Disagree completely|160|1" id="rid_160" /><label for="rid_160">1 - Disagree completely</label><br />
                        <input type="radio" name="grpRadio_23" value="2 -|162|2" id="rid_162" /><label for="rid_162">2 -</label><br />
                        <input type="radio" name="grpRadio_23" value="3 -|164|3" id="rid_164" /><label for="rid_164">3 -</label><br />
                        <input type="radio" name="grpRadio_23" value="4 -|166|4" id="rid_166" /><label for="rid_166">4 -</label><br />
                        <input type="radio" name="grpRadio_23" value="5 - Agree completely|168|5" id="rid_168" /><label for="rid_168">5 - Agree completely</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID24">
                <td>
                    <strong>3.  I have people in my life who will encourage me to use APAP even when it is noisy.</strong>
                    <div>
                        <input type="radio" name="grpRadio_24" value="1 - Disagree completely|170|1" id="rid_170" /><label for="rid_170">1 - Disagree completely</label><br />
                        <input type="radio" name="grpRadio_24" value="2 -|172|2" id="rid_172" /><label for="rid_172">2 -</label><br />
                        <input type="radio" name="grpRadio_24" value="3 -|174|3" id="rid_174" /><label for="rid_174">3 -</label><br />
                        <input type="radio" name="grpRadio_24" value="4 -|176|4" id="rid_176" /><label for="rid_176">4 -</label><br />
                        <input type="radio" name="grpRadio_24" value="5 - Agree completely|178|5" id="rid_178" /><label for="rid_178">5 - Agree completely</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID25">
                <td>
                    <strong>4.  I have people in my life who will give me ideas for making APAP more comfortable.</strong>
                    <div>
                        <input type="radio" name="grpRadio_25" value="1 - Disagree completely|180|1" id="rid_180" /><label for="rid_180">1 - Disagree completely</label><br />
                        <input type="radio" name="grpRadio_25" value="2 -|182|2" id="rid_182" /><label for="rid_182">2 -</label><br />
                        <input type="radio" name="grpRadio_25" value="3 -|184|3" id="rid_184" /><label for="rid_184">3 -</label><br />
                        <input type="radio" name="grpRadio_25" value="4 -|186|4" id="rid_186" /><label for="rid_186">4 -</label><br />
                        <input type="radio" name="grpRadio_25" value="5 - Agree completely|188|5" id="rid_188" /><label for="rid_188">5 - Agree completely</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID26">
                <td>
                    <strong>5.  I have people in my life who will help me adjust to using APAP.</strong>
                    <div>
                        <input type="radio" name="grpRadio_26" value="1 - Disagree completely|190|1" id="rid_190" /><label for="rid_190">1 - Disagree completely</label><br />
                        <input type="radio" name="grpRadio_26" value="2 -|192|2" id="rid_192" /><label for="rid_192">2 -</label><br />
                        <input type="radio" name="grpRadio_26" value="3 -|194|3" id="rid_194" /><label for="rid_194">3 -</label><br />
                        <input type="radio" name="grpRadio_26" value="4 -|196|4" id="rid_196" /><label for="rid_196">4 -</label><br />
                        <input type="radio" name="grpRadio_26" value="5 - Agree completely|198|5" id="rid_198" /><label for="rid_198">5 - Agree completely</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID27">
                <td>
                    <strong>6.  I have people in my life who will be upset if I stopped using APAP.</strong>
                    <div>
                        <input type="radio" name="grpRadio_27" value="1 - Disagree completely|200|1" id="rid_200" /><label for="rid_200">1 - Disagree completely</label><br />
                        <input type="radio" name="grpRadio_27" value="2 -|202|2" id="rid_202" /><label for="rid_202">2 -</label><br />
                        <input type="radio" name="grpRadio_27" value="3 -|204|3" id="rid_204" /><label for="rid_204">3 -</label><br />
                        <input type="radio" name="grpRadio_27" value="4 -|206|4" id="rid_206" /><label for="rid_206">4 -</label><br />
                        <input type="radio" name="grpRadio_27" value="5 - Agree completely|208|5" id="rid_208" /><label for="rid_208">5 - Agree completely</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID28">
                <td>
                    <strong>7.  I have people in my life who will support me in using APAP nightly.</strong>
                    <div>
                        <input type="radio" name="grpRadio_28" value="1 - Disagree completely|210|1" id="rid_210" /><label for="rid_210">1 - Disagree completely</label><br />
                        <input type="radio" name="grpRadio_28" value="2 -|212|2" id="rid_212" /><label for="rid_212">2 -</label><br />
                        <input type="radio" name="grpRadio_28" value="3 -|214|3" id="rid_214" /><label for="rid_214">3 -</label><br />
                        <input type="radio" name="grpRadio_28" value="4 -|216|4" id="rid_216" /><label for="rid_216">4 -</label><br />
                        <input type="radio" name="grpRadio_28" value="5 - Agree completely|218|5" id="rid_218" /><label for="rid_218">5 - Agree completely</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID29">
                <td>
                    <strong>8.  I will get the help I need to use APAP nightly.</strong>
                    <div>
                        <input type="radio" name="grpRadio_29" value="1 - Disagree completely|220|1" id="rid_220" /><label for="rid_220">1 - Disagree completely</label><br />
                        <input type="radio" name="grpRadio_29" value="2 -|222|2" id="rid_222" /><label for="rid_222">2 -</label><br />
                        <input type="radio" name="grpRadio_29" value="3 -|224|3" id="rid_224" /><label for="rid_224">3 -</label><br />
                        <input type="radio" name="grpRadio_29" value="4 -|226|4" id="rid_226" /><label for="rid_226">4 -</label><br />
                        <input type="radio" name="grpRadio_29" value="5 - Agree completely|228|5" id="rid_228" /><label for="rid_228">5 - Agree completely</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID30">
                <td>
                    <strong>9.  The healthcare staff will be helpful in helping me to use APAP nightly.</strong>
                    <div>
                        <input type="radio" name="grpRadio_30" value="1 - Disagree completely|230|1" id="rid_230" /><label for="rid_230">1 - Disagree completely</label><br />
                        <input type="radio" name="grpRadio_30" value="2 -|232|2" id="rid_232" /><label for="rid_232">2 -</label><br />
                        <input type="radio" name="grpRadio_30" value="3 -|234|3" id="rid_234" /><label for="rid_234">3 -</label><br />
                        <input type="radio" name="grpRadio_30" value="4 -|236|4" id="rid_236" /><label for="rid_236">4 -</label><br />
                        <input type="radio" name="grpRadio_30" value="5 - Agree completely|238|5" id="rid_238" /><label for="rid_238">5 - Agree completely</label><br />
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