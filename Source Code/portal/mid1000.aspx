<%@ Page Title="Initial Information" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid1000.aspx.cs" Inherits="mid1000" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

        <!-- this tells the code behind how many responces to process -->
        <input type="hidden" name="ResponseCount" value="79" /><br />

        <h1>Initial Information</h1>
        <h3>Demographic Questions</h3>
        <table class="tbl-quest">
            <tr id="TID1_QID1">
                <td>
                    <strong>Are you enrolled in MyHealtheVet?</strong>
                    <div>
                        <input type="radio" name="grpRadio_1" value="Yes|2|0" id="rid_2" /><label for="rid_2">Yes</label><br />
                        <input type="radio" name="grpRadio_1" value="No|4|0" id="rid_4" /><label for="rid_4">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID1_QID2">
                <td>
                    <strong>Indicate the highest level of education that you&#8217;ve completed:</strong>
                    <div>
                        <input type="radio" name="grpRadio_2" value="elementary school|6|0" id="rid_6" />
                        <label for="rid_6">elementary school</label><br />

                        <input type="radio" name="grpRadio_2" value="high school or equivalent|8|0" id="rid_8" />
                        <label for="rid_8">high school or equivalent</label><br />

                        <input type="radio" name="grpRadio_2" value="associate&#8217;s degree|10|0" id="rid_10" />
                        <label for="rid_10">associate&#8217;s degree</label><br />

                        <input type="radio" name="grpRadio_2" value="bachelor&#8217;s degree|12|0" id="rid_12" />
                        <label for="rid_12">bachelor&#8217;s degree</label><br />

                        <input type="radio" name="grpRadio_2" value="graduate degree(s)|14|0" id="rid_14" />
                        <label for="rid_14">graduate degree(s)</label><br />
                    </div>
                </td>
            </tr>


            <tr id="TID1_QID3">
                <td>
                    <strong>Sex:</strong>
                    <div>
                        <input type="radio" name="grpRadio_3" value="male|16|0" id="rid_16" />
                        <label for="rid_16">male</label><br />

                        <input type="radio" name="grpRadio_3" value="female|18|0" id="rid_18" />
                        <label for="rid_18">female</label><br />
                    </div>
                </td>
            </tr>
        </table>

        <h3>Health History Questions</h3>
        <table class="tbl-quest">
            <tr id="TID2_QID1">
                <td>
                    <strong>Please indicate whether you have any of the following sleep problems.  Check all that apply</strong>
                    <div>
                        <input type="checkbox" name="grpCheck_4" id="rid_20" value="Snoring|20|0" />
                        <label for="rid_20">Snoring</label><br />

                        <input type="checkbox" name="grpCheck_5" id="rid_22" value="My breathing stops at night|22|0" />
                        <label for="rid_22">My breathing stops at night</label><br />

                        <input type="checkbox" name="grpCheck_6" id="rid_24" value="Sleepiness during the day|24|0" />
                        <label for="rid_24">Sleepiness during the day</label><br />

                        <input type="checkbox" name="grpCheck_7" id="rid_26" value="Sleep not refreshing|26|0" />
                        <label for="rid_26">Sleep not refreshing</label><br />

                        <input type="checkbox" name="grpCheck_8" id="rid_28" value="Difficulty falling asleep|28|0" />
                        <label for="rid_28">Difficulty falling asleep</label><br />

                        <input type="checkbox" name="grpCheck_9" id="rid_30" value="Difficulty staying asleep|30|0" />
                        <label for="rid_30">Difficulty staying asleep</label><br />

                        <input type="checkbox" name="grpCheck_10" id="rid_32" value="Difficulty keeping a normal sleep schedule|32|0" />
                        <label for="rid_32">Difficulty keeping a normal sleep schedule</label><br />

                        <input type="checkbox" name="grpCheck_11" id="rid_34" value="Talk, walk, and/or other behavior in my sleep|34|0" />
                        <label for="rid_34">Talk, walk, and/or other behavior in my sleep</label><br />

                        <input type="checkbox" name="grpCheck_12" id="rid_36" value="I do not have a sleep problem|36|0" clearabove="clearabove" />
                        <label for="rid_36">I do not have a sleep problem</label><br />
                    </div>
                </td>
            </tr>

            <br />

            <tr id="TID2_QID2">
                <td>
                    <strong>Have you ever seen a sleep specialist, either for a daytime or overnight appointment?</strong>
                    <div>
                        <input type="radio" name="grpRadio_13" value="Yes|38|0" id="rid_38" />
                        <label for="rid_38">Yes</label><br />

                        <input type="radio" name="grpRadio_13" value="No|40|0" id="rid_40" />
                        <label for="rid_40">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID2_QID3">
                <td>
                    <strong>Are you currently pregnant?</strong>
                    <div>
                        <input type="radio" name="grpRadio_14" value="Yes|42|0" id="rid_42" />
                        <label for="rid_42">Yes</label><br />

                        <input type="radio" name="grpRadio_14" value="No|44|0" id="rid_44" />
                        <label for="rid_44">No</label><br />

                        <input type="radio" name="grpRadio_14" value="Don't know|46|0" id="rid_46" />
                        <label for="rid_46">Don't know</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID2_QID4">
                <td>
                    <strong>What is your menopausal status?</strong>
                    <div>
                        <input type="radio" name="grpRadio_15" value="Pre-menopause|48|0" id="rid_48" />
                        <label for="rid_48">Pre-menopause</label><br />

                        <input type="radio" name="grpRadio_15" value="Nearing or undergoing menopause|50|0" id="rid_50" />
                        <label for="rid_50">Nearing or undergoing menopause</label><br />

                        <input type="radio" name="grpRadio_15" value="Post menopause|52|0" id="rid_52" />
                        <label for="rid_52">Post menopause</label><br />

                        <input type="radio" name="grpRadio_15" value="Don't know|54|0" id="rid_54" />
                        <label for="rid_54">Don't know</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID2_QID5">
                <td>
                    <strong>Have you had both of your ovaries removed?</strong>
                    <div>
                        <input type="radio" name="grpRadio_16" value="Yes|56|0" id="rid_56" />
                        <label for="rid_56">Yes</label><br />

                        <input type="radio" name="grpRadio_16" value="No|58|0" id="rid_58" />
                        <label for="rid_58">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID2_QID6">
                <td>
                    <strong>Do you have a bed partner or roommate?</strong>
                    <div>
                        <input type="radio" name="grpRadio_17" value="No bed partner or roommate|60|0" id="rid_60" />
                        <label for="rid_60">No bed partner or roommate</label><br />

                        <input type="radio" name="grpRadio_17" value="Partner/roommate in other room|62|0" id="rid_62" />
                        <label for="rid_62">Partner/roommate in other room</label><br />

                        <input type="radio" name="grpRadio_17" value="Partner in same room but not same bed|64|0" id="rid_64" />
                        <label for="rid_64">Partner in same room but not same bed</label><br />

                        <input type="radio" name="grpRadio_17" value="Partner in same bed|66|0" id="rid_66" />
                        <label for="rid_66">Partner in same bed</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID2_QID7">
                <td>
                    <strong>Please indicate if any of the following regularly disrupt your sleep.  Check all that apply.</strong>
                    <div>
                        <input type="checkbox" name="grpCheck_18" id="rid_68" value="My sleep is rarely disrupted|68|0" />
                        <label for="rid_68">My sleep is rarely disrupted</label><br />

                        <input type="checkbox" name="grpCheck_19" id="rid_70" value="Bed partner|70|0" />
                        <label for="rid_70">Bed partner</label><br />

                        <input type="checkbox" name="grpCheck_20" id="rid_72" value="The need to care for another (i.e. children, elderly parent, etc.)|72|0" />
                        <label for="rid_72">The need to care for another (i.e. children, elderly parent, etc.)</label><br />

                        <input type="checkbox" name="grpCheck_21" id="rid_74" value="Pet(s)|74|0" />
                        <label for="rid_74">Pet(s)</label><br />

                        <input type="checkbox" name="grpCheck_22" id="rid_76" value="Pain/discomfort|76|0" />
                        <label for="rid_76">Pain/discomfort</label><br />

                        <input type="checkbox" name="grpCheck_23" id="rid_78" value="The need to urinate|78|0" />
                        <label for="rid_78">The need to urinate</label><br />

                        <input type="checkbox" name="grpCheck_24" id="rid_80" value="Bed/mattress|80|0" />
                        <label for="rid_80">Bed/mattress</label><br />

                        <input type="checkbox" name="grpCheck_25" id="rid_82" value="Light|82|0" />
                        <label for="rid_82">Light</label><br />

                        <input type="checkbox" name="grpCheck_26" id="rid_84" value="Worries|84|0" />
                        <label for="rid_84">Worries</label><br />

                        <input type="checkbox" name="grpCheck_27" id="rid_86" value="Temperature|86|0" />
                        <label for="rid_86">Temperature</label><br />

                        <input type="checkbox" name="grpCheck_28" id="rid_88" value="Noise|88|0" />
                        <label for="rid_88">Noise</label><br />

                        Other (please describe):
					    <input type="text" name="grpCtrlText_29" id="rid_txt_90" />
                        <input type="hidden" name="grpHidden_29" id="rid_90" value="|90|0" />
                    </div>
                </td>
            </tr>

            <tr id="TID2_QID8">
                <td>
                    <strong>Please indicate if any of the following regularly disrupt your sleep.  Check all that apply.</strong>
                    <div>
                        <input type="checkbox" name="grpCheck_30" id="rid_92" value="My sleep is rarely disrupted|92|0" />
                        <label for="rid_92">My sleep is rarely disrupted</label><br />

                        <input type="checkbox" name="grpCheck_31" id="rid_94" value="The need to care for another (i.e. children, elderly parent, etc.)|94|0" />
                        <label for="rid_94">The need to care for another (i.e. children, elderly parent, etc.)</label><br />

                        <input type="checkbox" name="grpCheck_32" id="rid_96" value="Pet(s)|96|0" />
                        <label for="rid_96">Pet(s)</label><br />

                        <input type="checkbox" name="grpCheck_33" id="rid_98" value="Pain/discomfort|98|0" />
                        <label for="rid_98">Pain/discomfort</label><br />

                        <input type="checkbox" name="grpCheck_34" id="rid_100" value="The need to urinate|100|0" />
                        <label for="rid_100">The need to urinate</label><br />

                        <input type="checkbox" name="grpCheck_35" id="rid_102" value="Bed/mattress|102|0" />
                        <label for="rid_102">Bed/mattress</label><br />

                        <input type="checkbox" name="grpCheck_36" id="rid_104" value="Light|104|0" />
                        <label for="rid_104">Light</label><br />

                        <input type="checkbox" name="grpCheck_37" id="rid_106" value="Worries|106|0" />
                        <label for="rid_106">Worries</label><br />

                        <input type="checkbox" name="grpCheck_38" id="rid_108" value="Temperature|108|0" />
                        <label for="rid_108">Temperature</label><br />

                        <input type="checkbox" name="grpCheck_39" id="rid_110" value="Noise|110|0" />
                        <label for="rid_110">Noise</label><br />

                        Other (please describe):
					    <input type="text" name="grpCtrlText_40" id="rid_txt_112" />
                        <input type="hidden" name="grpHidden_40" id="rid_112" value="|112|0" />
                    </div>
                </td>
            </tr>

            <tr id="TID2_QID9">
                <td>
                    <strong>Are you currently employed?</strong>
                    <div>
                        <input type="radio" name="grpRadio_41" value="Yes|114|0" id="rid_114" />
                        <label for="rid_114">Yes</label><br />

                        <input type="radio" name="grpRadio_41" value="No|116|0" id="rid_116" />
                        <label for="rid_116">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID2_QID10">
                <td>
                    <strong>Please indicate what work or school schedule(s) best represents your current situation.  Check all that apply. 
                        <br />
                        Are you currently:</strong>
                    <div>
                        <input type="checkbox" name="grpCheck_42" id="rid_118" value="a full-time student|118|0" />
                        <label for="rid_118">a full-time student</label><br />

                        <input type="checkbox" name="grpCheck_43" id="rid_120" value="a part-time student|120|0" />
                        <label for="rid_120">a part-time student</label><br />

                        <input type="checkbox" name="grpCheck_44" id="rid_122" value="full-time caretaker of another person|122|0" />
                        <label for="rid_122">full-time caretaker of another person</label><br />

                        <input type="checkbox" name="grpCheck_45" id="rid_124" value="full-time homemaker|124|0" />
                        <label for="rid_124">full-time homemaker</label><br />

                        <input type="checkbox" name="grpCheck_46" id="rid_126" value="retired|126|0" />
                        <label for="rid_126">retired</label><br />

                        <input type="checkbox" name="grpCheck_47" id="rid_128" value="on disability|128|0" />
                        <label for="rid_128">on disability</label><br />
                    </div>


                </td>
            </tr>

            <tr id="TID2_QID11">
                <td>
                    <strong>Please indicate what work or school schedule(s) best represents your current situation.  Check all that apply.<br />
                        Are you currently:</strong>
                    <div>
                        <input type="checkbox" name="grpCheck_48" id="rid_130" value="employed full-time (includes volunteer work)|130|0" />
                        <label for="rid_130">employed full-time (includes volunteer work)</label><br />

                        <input type="checkbox" name="grpCheck_49" id="rid_132" value="employed part-time(includes volunteer work)|132|0" />
                        <label for="rid_132">employed part-time(includes volunteer work)</label><br />

                        <input type="checkbox" name="grpCheck_50" id="rid_134" value="a full-time student|134|0" />
                        <label for="rid_134">a full-time student</label><br />

                        <input type="checkbox" name="grpCheck_51" id="rid_136" value="a part-time student|136|0" />
                        <label for="rid_136">a part-time student</label><br />

                        <input type="checkbox" name="grpCheck_52" id="rid_138" value="full-time caretaker of another person|138|0" />
                        <label for="rid_138">full-time caretaker of another person</label><br />

                        <input type="checkbox" name="grpCheck_53" id="rid_140" value="full-time homemaker|140|0" />
                        <label for="rid_140">full-time homemaker</label><br />

                        <input type="checkbox" name="grpCheck_54" id="rid_142" value="retired|142|0" />
                        <label for="rid_142">retired</label><br />

                        <input type="checkbox" name="grpCheck_55" id="rid_144" value="on disability|144|0" />
                        <label for="rid_144">on disability</label><br />
                    </div>
                </td>
            </tr>
        </table>

        <h3>Napping, Exercise, Caffeine, Alcohol</h3>
        <table class="tbl-quest">
            <tr id="TID3_QID1">
                <td>
                    <strong>How often do you nap?</strong>
                    <div>
                        <%--					    <input type="checkbox" name="grpCheck_56" id="rid_146" value="I rarely or never nap|146|0" clearabove="clearabove" />
                        <label for="rid_146">I rarely or never nap</label><br/>					
						
					    <input type="text" name="grpCtrlText_57" id="rid_txt_148" />&nbsp;times per day
                        <input type="hidden" name="grpHidden_57" id="rid_148"  value="times per day|148|0"/><br/>

					    <input type="text" name="grpCtrlText_58" id="rid_txt_150" />&nbsp;times per week
                        <input type="hidden" name="grpHidden_58" id="rid_150"  value="times per week|150|0"/><br/>	
					
					    <input type="text" name="grpCtrlText_59" id="rid_txt_152" />&nbsp;times per month
                        <input type="hidden" name="grpHidden_59" id="rid_152"  value="times per month|152|0"/><br/>	
                        --%>
                        <input type="radio" name="grpRadio_56" value="I rarely or never nap|146|0" id="rid_146" />
                        <label for="rid_146">I rarely or never nap</label><br />

                        <input type="radio" name="grpRadio_56" value="I usually nap 1-2 days per week |148|0" id="rid_148" />
                        <label for="rid_148">I usually nap 1-2 days per week</label><br />

                        <input type="radio" name="grpRadio_56" value="I usually nap 3-4 days per week|150|0" id="rid_150" />
                        <label for="rid_150">I usually nap 3-4 days per week</label><br />

                        <input type="radio" name="grpRadio_56" value="I nap almost every day |152|0" id="rid_152" />
                        <label for="rid_152">I nap almost every day</label><br />

                    </div>
                </td>
            </tr>

            <tr id="TID3_QID2">
                <td>
                    <strong>What is the usual length of your naps?</strong>
                    <div>
                        <%--					<input type="text" name="grpCtrlText_60" id="rid_txt_154" />&nbsp;hour(s)
                        <input type="hidden" name="grpHidden_60" id="rid_154" value="hour(s)|154|0"/><br/>
                        --%>
                        <input type="text" name="grpCtrlText_57" id="rid_txt_154" />&nbsp;minutes
                        <input type="hidden" name="grpHidden_57" id="rid_154" value="minutes|154|0" /><br />
                    </div>
                </td>
            </tr>

            <tr id="TID3_QID3">
                <td>
                    <strong>After a nap, do you feel refreshed (not sleepy anymore)?</strong>
                    <div>
                        <input type="radio" name="grpRadio_58" value="rarely (never)|158|0" id="rid_158" />
                        <label for="rid_158">rarely (never)</label><br />

                        <input type="radio" name="grpRadio_58" value="sometimes (mostly not refreshed)|160|0" id="rid_160" />
                        <label for="rid_160">sometimes (mostly not refreshed)</label><br />

                        <input type="radio" name="grpRadio_58" value="half of the time|162|0" id="rid_162" />
                        <label for="rid_162">half of the time</label><br />

                        <input type="radio" name="grpRadio_58" value="often (almost every nap)|164|0" id="rid_164" />
                        <label for="rid_164">often (almost every nap)</label><br />

                        <input type="radio" name="grpRadio_58" value="always (every nap)|166|0" id="rid_166" />
                        <label for="rid_166">always (every nap)</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID3_QID4">
                <td>
                    <strong>How often do you exercise?</strong>
                    <div>
                        <%--					    <input type="checkbox" name="grpCheck_63" id="rid_168" value="I rarely or never exercise|168|0" clearabove="clearabove" />
                        <label for="rid_168">I rarely or never exercise</label><br/>						
						
					    <input type="text" name="grpCtrlText_64" id="rid_txt_170" />&nbsp;times per day
                        <input type="hidden" name="grpHidden_64" id="rid_170"  value="times per day|170|0"/><br/>

					    <input type="text" name="grpCtrlText_65" id="rid_txt_172" />&nbsp;times per week
                        <input type="hidden" name="grpHidden_65" id="rid_172"  value="times per week|172|0"/><br/>	
					
					    <input type="text" name="grpCtrlText_66" id="rid_txt_174" />&nbsp;times per month
                        <input type="hidden" name="grpHidden_66" id="rid_174"  value="times per month|174|0"/><br/>	
                        --%>
                        <input type="radio" name="grpRadio_59" value="Not during the past month|168|0" id="rid_168" />
                        <label for="rid_168">Not during the past month</label><br />

                        <input type="radio" name="grpRadio_59" value="Less than once a week|170|0" id="rid_170" />
                        <label for="rid_170">Less than once a week</label><br />

                        <input type="radio" name="grpRadio_59" value="Once or twice a week |172|0" id="rid_172" />
                        <label for="rid_172">Once or twice a week</label><br />

                        <input type="radio" name="grpRadio_59" value="Three or more times a week|174|0" id="rid_174" />
                        <label for="rid_174">Three or more times a week</label><br />

                    </div>
                </td>
            </tr>

            <tr id="TID3_QID5">
                <td>
                    <strong>Do you drink alcohol?</strong>
                    <div>
                        <input type="radio" name="grpRadio_60" value="Yes|176|0" id="rid_176" />
                        <label for="rid_176">Yes</label><br />

                        <input type="radio" name="grpRadio_60" value="No|178|0" id="rid_178" />
                        <label for="rid_178">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID3_QID6">
                <td>
                    <strong>How many alcohol drinks do you typically have in one day?
(one drink equals one can of beer, or one glass of wine, or one shot of liquor)
                    </strong>
                    <div>
                        Number of alcohol drinks per day: &nbsp;<input type="text" name="grpCtrlText_61" id="rid_txt_180" />
                        <input type="hidden" name="grpHidden_61" id="rid_180" value="|180|0" /><br />
                    </div>
                </td>
            </tr>

            <tr id="TID3_QID7">
                <td>
                    <strong>Do you consume alcohol to help you fall asleep?</strong>
                    <div>
                        <input type="radio" name="grpRadio_62" value="Yes|182|0" id="rid_182" />
                        <label for="rid_182">Yes</label><br />

                        <input type="radio" name="grpRadio_62" value="No|184|0" id="rid_184" />
                        <label for="rid_184">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID3_QID8">
                <td>
                    <strong>Do you drink beverages with caffeine (for example, coffee, tea or cola?)</strong>
                    <div>
                        <input type="radio" name="grpRadio_63" value="Yes|186|0" id="rid_186" />
                        <label for="rid_186">Yes</label><br />

                        <input type="radio" name="grpRadio_63" value="No|188|0" id="rid_188" />
                        <label for="rid_188">No</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID3_QID9">
                <td>
                    <strong>How many caffeinated drinks do you typically have in one day?<br />
                        (one drink equals one cup of coffee. or one cup of tea, or one cola/energy drink)</strong>
                    <div>
                        Number of caffeinated drinks per day: &nbsp;
                        <input type="text" name="grpCtrlText_64" id="rid_txt_190" />
                        <input type="hidden" name="grpHidden_64" id="rid_190" value="servings|190|0" />
                    </div>
                </td>
            </tr>

        </table>

        <h3>Current or Past Tobacco Users</h3>
        <table class="tbl-quest">
            <tr id="TID4_QID1">
                <td>
                    <strong>Which of the following best describes your use of tobacco products?</strong>
                    <div>
                        <input type="radio" name="grpRadio_65" id="rid_192" value="Never used tobacco/smoked less than 100 cigarettes in my life|192|0" />
                        <label for="rid_192">Never used tobacco/smoked less than 100 cigarettes in my life</label><br />

                        <input type="radio" name="grpRadio_65" id="rid_194" value="former cigarette smoker|194|0" />
                        <label for="rid_194">Former cigarette smoker</label><br />

                        <input type="radio" name="grpRadio_65" id="rid_196" value="Current cigarette smoker|196|0" />
                        <label for="rid_196">Current cigarette smoker</label><br />

                        <input type="radio" name="grpRadio_65" id="rid_198" value="Former smokeless or other tobacco user|198|0" />
                        <label for="rid_198">Former smokeless or other tobacco user</label><br />

                        <input type="radio" name="grpRadio_65" id="rid_200" value="current smokeless or other tobacco user|200|0" />
                        <label for="rid_200">Current smokeless or other tobacco user</label><br />
                    </div>
                </td>
            </tr>

            <tr id="TID4_QID2">
                <td>
                    <strong>How old were you when you started smoking cigarettes?</strong>
                    <div>
                        <input type="text" name="grpCtrlText_66" id="rid_txt_202" />&nbsp;years old
                        <input type="hidden" name="grpHidden_66" id="rid_202" value="years old|202|0" /><br />
                    </div>

                </td>
            </tr>

            <tr id="TID4_QID2_2">
                <td>
                    <strong>How old were you when you started smoking cigarettes?</strong>
                    <div>
                        <input type="text" name="grpCtrlText_67" id="rid_txt_204" />&nbsp;years old
                        <input type="hidden" name="grpHidden_67" id="rid_204" value="years old|204|0" /><br />
                    </div>

                </td>
            </tr>

            <tr id="TID4_QID3">
                <td>
                    <strong>How many packs of cigarettes per day did/do you typically smoke? </strong>
                    <div>
                        <input type="text" name="grpCtrlText_68" id="rid_txt_206" />&nbsp;packs per day (enter number of packs)<br />
                        (If less than 1 pack a day, put the fraction of a pack, for example, 0.5 for half a pack and 0.25 for a quarter of a pack) 
                        <input type="hidden" name="grpHidden_68" id="rid_206" value="packs per day|206|0" /><br />
                    </div>
                </td>
            </tr>

            <tr id="TID4_QID3_2">
                <td>
                    <strong>How many packs of cigarettes per day did/do you typically smoke? </strong>
                    <div>
                        <input type="text" name="grpCtrlText_69" id="rid_txt_208" />&nbsp;packs per day (enter number of packs)<br />
                        (If less than 1 pack a day, put the fraction of a pack, for example, 0.5 for half a pack and 0.25 for a quarter of a pack) 
                        <input type="hidden" name="grpHidden_69" id="rid_208" value="packs per day|208|0" /><br />
                    </div>
                </td>
            </tr>


            <tr id="TID4_QID4">
                <td>
                    <strong>How old were you when you stopped smoking cigarettes?</strong>
                    <div>
                        <input type="text" name="grpCtrlText_70" id="rid_txt_210" />&nbsp;years old
                        <input type="hidden" name="grpHidden_70" id="rid_210" value="years old|210|0" /><br />
                    </div>
                </td>
            </tr>

            <tr id="TID4_QID5">
                <td>
                    <strong>How old were you when you started using smokeless or other tobacco?</strong>
                    <div>
                        <input type="text" name="grpCtrlText_71" id="rid_txt_212" />&nbsp;years old
                        <input type="hidden" name="grpHidden_71" id="rid_212" value="years old|212|0" /><br />
                    </div>
                </td>
            </tr>

            <tr id="TID4_QID5_2">
                <td>
                    <strong>How old were you when you started using smokeless or other tobacco?</strong>
                    <div>
                        <input type="text" name="grpCtrlText_72" id="rid_txt_214" />&nbsp;years old
                        <input type="hidden" name="grpHidden_72" id="rid_214" value="years old|214|0" /><br />
                    </div>
                </td>
            </tr>

            <tr id="TID4_QID6">
                <td>
                    <strong>How old were you when you stopped using smokeless or other tobacco?</strong>
                    <div>
                        <input type="text" name="grpCtrlText_73" id="rid_txt_216" />&nbsp;years old
                        <input type="hidden" name="grpHidden_73" id="rid_216" value="years old|216|0" /><br />
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
                rid: 18,
                checked: {
                    show: ['TID2_QID3', 'TID2_QID4', 'TID2_QID5']
                }
            },

            {
                rid: 66,
                checked: {
                    show: ['TID2_QID7'],
                    hide: ['TID2_QID8']
                }
            },

            {
                rid: 114,
                checked: {
                    show: ['TID2_QID11'],
                    hide: ['TID2_QID10']
                }
            },

            {
                rid: 146,
                checked: {
                    hide: ['TID3_QID2']
                }
            },

            {
                rid: 176,
                checked: {
                    show: ['TID3_QID6', 'TID3_QID7']
                }
            },

            {
                rid: 186,
                checked: {
                    show: ['TID3_QID9']
                }
            },

            {
                rid: 188,
                checked: {
                    hide: ['TID4_QID2']
                }
            },

            {
                rid: 194,
                checked: {
                    show: ['TID4_QID2', 'TID4_QID3', 'TID4_QID4']
                }
            },

            {
                rid: 196,
                checked: {
                    show: ['TID4_QID2_2', 'TID4_QID3_2']
                }
            },


            {
                rid: 198,
                checked: {
                    show: ['TID4_QID5', 'TID4_QID6']
                }
            },

            {
                rid: 200,
                checked: {
                    show: ['TID4_QID5_2']
                }
            }

        ];

        //text masks
        questions.opts.txtMasks = [
            { rid: 148, mask: 'numbersOnly', maxlength: 2 },
            { rid: 150, mask: 'numbersOnly', maxlength: 2 },
            { rid: 152, mask: 'numbersOnly', maxlength: 3 },
            { rid: 154, mask: 'numbersOnly', maxlength: 2 },
            { rid: 156, mask: 'numbersOnly', maxlength: 2 },
            { rid: 170, mask: 'numbersOnly', maxlength: 2 },
            { rid: 172, mask: 'numbersOnly', maxlength: 2 },
            { rid: 174, mask: 'numbersOnly', maxlength: 3 },
            { rid: 176, mask: 'numbersOnly', maxlength: 2 },
            { rid: 184, mask: 'numbersOnly', maxlength: 2 },
            { rid: 198, mask: 'numbersOnly', maxlength: 2 },
            { rid: 200, mask: 'numbersOnly', maxlength: 2 },
            { rid: 202, mask: 'numbersOnly', maxlength: 3 },
            { rid: 204, mask: 'numbersOnly', maxlength: 3 },
            { rid: 206, mask: 'numbersOnly', maxlength: 2 }
        ];

    </script>
</asp:Content>