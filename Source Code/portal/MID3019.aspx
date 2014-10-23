<%@ Page Title="REVAMP Portal - Follow-up Short SAQLI Questionnaire" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid3019.aspx.cs" Inherits="mid3019" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

        <!-- this tells the code behind how many responces to process -->
        <input type="hidden" name="ResponseCount" value="110" /><br />

        <asp:MultiView ID="mvSymptoms" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwQuestions" runat="server">
                <!-- ************************************************************ -->
                <!-- ************************************************************ -->
                <!-- STAGE 1 -->
                <div id="divStage1" style="display: block;">
                    <!-- Stage 1 Contents -->
                    <h1>Sleep Apnea Symptoms On Treatment</h1>
                    <h5 style="margin-bottom: 25px;">Here are the sleep related symptoms you selected as being the most important to you before you started treatment for your sleep apnea. Tell us how much of a problem each of these symptoms is to you now by making a selection from the drop down list below each symptom. Using the sliding scale at the bottom of the page, indicate how much of an impact your symptoms are having on your quality of life. After you have finished, click “submit” at the bottom of the page to save your selections.
		</h5>
                    <table id="tblTID1">
                        <tr id="TID1_QID1">
                            <td>
                                <strong>About your decreased energy, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2002" name="grpCombo_1">
                                        <option value=""></option>
                                        <option value="A very large problem|2002|1|1">A very large problem</option>
                                        <option value="A large problem|2002|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2002|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2002|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2002|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2002|6|6">A small problem</option>
                                        <option value="No problem|2002|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID2">
                            <td>
                                <strong>About your excessive fatigue, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2004" name="grpCombo_2">
                                        <option value=""></option>
                                        <option value="A very large problem|2004|1|1">A very large problem</option>
                                        <option value="A large problem|2004|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2004|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2004|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2004|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2004|6|6">A small problem</option>
                                        <option value="No problem|2004|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID3">
                            <td>
                                <strong>About your feeling that ordinary activities required an extra effort to perform or complete, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2006" name="grpCombo_3">
                                        <option value=""></option>
                                        <option value="A very large problem|2006|1|1">A very large problem</option>
                                        <option value="A large problem|2006|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2006|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2006|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2006|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2006|6|6">A small problem</option>
                                        <option value="No problem|2006|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID4">
                            <td>
                                <strong>About your falling asleep at inappropriate times or places, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2008" name="grpCombo_4">
                                        <option value=""></option>
                                        <option value="A very large problem|2008|1|1">A very large problem</option>
                                        <option value="A large problem|2008|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2008|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2008|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2008|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2008|6|6">A small problem</option>
                                        <option value="No problem|2008|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID5">
                            <td>
                                <strong>About your falling asleep if not stimulated or active, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2010" name="grpCombo_5">
                                        <option value=""></option>
                                        <option value="A very large problem|2010|1|1">A very large problem</option>
                                        <option value="A large problem|2010|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2010|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2010|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2010|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2010|6|6">A small problem</option>
                                        <option value="No problem|2010|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID6">
                            <td>
                                <strong>About your difficulty with a dry or sore mouth/throat upon awakening, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2012" name="grpCombo_6">
                                        <option value=""></option>
                                        <option value="A very large problem|2012|1|1">A very large problem</option>
                                        <option value="A large problem|2012|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2012|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2012|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2012|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2012|6|6">A small problem</option>
                                        <option value="No problem|2012|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID7">
                            <td>
                                <strong>Waking up often during the night, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2014" name="grpCombo_7">
                                        <option value=""></option>
                                        <option value="A very large problem|2014|1|1">A very large problem</option>
                                        <option value="A large problem|2014|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2014|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2014|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2014|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2014|6|6">A small problem</option>
                                        <option value="No problem|2014|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID8">
                            <td>
                                <strong>About your difficulty returning to sleep if you wake up in the night, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2016" name="grpCombo_8">
                                        <option value=""></option>
                                        <option value="A very large problem|2016|1|1">A very large problem</option>
                                        <option value="A large problem|2016|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2016|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2016|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2016|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2016|6|6">A small problem</option>
                                        <option value="No problem|2016|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID9">
                            <td>
                                <strong>Concern about the times you stop breathing at night, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2018" name="grpCombo_9">
                                        <option value=""></option>
                                        <option value="A very large problem|2018|1|1">A very large problem</option>
                                        <option value="A large problem|2018|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2018|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2018|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2018|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2018|6|6">A small problem</option>
                                        <option value="No problem|2018|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID10">
                            <td>
                                <strong>About your waking up at night feeling like you were choking, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2020" name="grpCombo_10">
                                        <option value=""></option>
                                        <option value="A very large problem|2020|1|1">A very large problem</option>
                                        <option value="A large problem|2020|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2020|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2020|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2020|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2020|6|6">A small problem</option>
                                        <option value="No problem|2020|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID11">
                            <td>
                                <strong>About your waking up in the morning with a headache, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2022" name="grpCombo_11">
                                        <option value=""></option>
                                        <option value="A very large problem|2022|1|1">A very large problem</option>
                                        <option value="A large problem|2022|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2022|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2022|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2022|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2022|6|6">A small problem</option>
                                        <option value="No problem|2022|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID12">
                            <td>
                                <strong>About your waking up in the morning feeling unrefreshed and/or tired, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2024" name="grpCombo_12">
                                        <option value=""></option>
                                        <option value="A very large problem|2024|1|1">A very large problem</option>
                                        <option value="A large problem|2024|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2024|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2024|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2024|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2024|6|6">A small problem</option>
                                        <option value="No problem|2024|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID13">
                            <td>
                                <strong>About your waking up more than once per night (on average) to urinate, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2026" name="grpCombo_13">
                                        <option value=""></option>
                                        <option value="A very large problem|2026|1|1">A very large problem</option>
                                        <option value="A large problem|2026|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2026|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2026|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2026|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2026|6|6">A small problem</option>
                                        <option value="No problem|2026|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID14">
                            <td>
                                <strong>About your  feeling that your sleep is restless, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2028" name="grpCombo_14">
                                        <option value=""></option>
                                        <option value="A very large problem|2028|1|1">A very large problem</option>
                                        <option value="A large problem|2028|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2028|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2028|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2028|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2028|6|6">A small problem</option>
                                        <option value="No problem|2028|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID15">
                            <td>
                                <strong>About your aifficulty staying awake while reading, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2030" name="grpCombo_15">
                                        <option value=""></option>
                                        <option value="A very large problem|2030|1|1">A very large problem</option>
                                        <option value="A large problem|2030|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2030|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2030|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2030|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2030|6|6">A small problem</option>
                                        <option value="No problem|2030|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID16">
                            <td>
                                <strong>About your difficulty staying awake while trying to carry on a conversation, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2032" name="grpCombo_16">
                                        <option value=""></option>
                                        <option value="A very large problem|2032|1|1">A very large problem</option>
                                        <option value="A large problem|2032|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2032|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2032|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2032|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2032|6|6">A small problem</option>
                                        <option value="No problem|2032|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID17">
                            <td>
                                <strong>About your difficulty staying awake while trying to watch something , how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2034" name="grpCombo_17">
                                        <option value=""></option>
                                        <option value="A very large problem|2034|1|1">A very large problem</option>
                                        <option value="A large problem|2034|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2034|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2034|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2034|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2034|6|6">A small problem</option>
                                        <option value="No problem|2034|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID18">
                            <td>
                                <strong>About your fighting the urge to fall asleep while driving, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2036" name="grpCombo_18">
                                        <option value=""></option>
                                        <option value="A very large problem|2036|1|1">A very large problem</option>
                                        <option value="A large problem|2036|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2036|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2036|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2036|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2036|6|6">A small problem</option>
                                        <option value="No problem|2036|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID19">
                            <td>
                                <strong>About your reluctance or inability to drive for more than 1 hour, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2038" name="grpCombo_19">
                                        <option value=""></option>
                                        <option value="A very large problem|2038|1|1">A very large problem</option>
                                        <option value="A large problem|2038|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2038|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2038|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2038|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2038|6|6">A small problem</option>
                                        <option value="No problem|2038|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID20">
                            <td>
                                <strong>About your concern regarding close calls while driving caused partially or totally by your inability to remain alert, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2040" name="grpCombo_20">
                                        <option value=""></option>
                                        <option value="A very large problem|2040|1|1">A very large problem</option>
                                        <option value="A large problem|2040|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2040|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2040|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2040|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2040|6|6">A small problem</option>
                                        <option value="No problem|2040|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID1_QID21">
                            <td>
                                <strong>About your concern regarding yours or other’s safety when you are operating a motor vehicle and/or machinery, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_2042" name="grpCombo_21">
                                        <option value=""></option>
                                        <option value="A very large problem|2042|1|1">A very large problem</option>
                                        <option value="A large problem|2042|2|2">A large problem</option>
                                        <option value="A moderate to large problem|2042|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|2042|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|2042|5|5">A small to moderate problem</option>
                                        <option value="A small problem|2042|6|6">A small problem</option>
                                        <option value="No problem|2042|7|7">No problem</option>
                                    </select>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />

                    <h1>Impact</h1>
                    <!-- <strong>I.  Daily Functioning, Social Interactions, Emotional Functioning, Symptoms</strong> -->
                    <p>
                        Please think of the questions in the previous sections. Having been treated for your breathing problem during sleep, how much of an impact has it had on the quality of your life since you started treatment?
	
                    </p>
                    <table>
                        <tr id="TID2_QID1">
                            <td>
                                <div style="margin-left: 15px;">
                                    <div style="width: 450px; float: left;">
                                        <table style="width: inherit;">
                                            <tr>
                                                <td colspan="2">
                                                    <div id="div_rid_3002"></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width: 50%; color: #0094ff;">0<br />
                                                    <span style="font-size: 12px;">No Impact</span>
                                                </td>
                                                <td style="text-align: right; width: 50%; color: #0094ff;">10<br />
                                                    <span style="font-size: 12px;">Extremely Large Impact</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="float: left; margin-left: 20px;">
                                        <select id="rid_3002" name="grpCombo_22">
                                            <option value=""></option>
                                            <option value="0|3002|0|0">0</option>
                                            <option value="1|3002|1|1">1</option>
                                            <option value="2|3002|2|2">2</option>
                                            <option value="3|3002|3|3">3</option>
                                            <option value="4|3002|4|4">4</option>
                                            <option value="5|3002|5|5">5</option>
                                            <option value="6|3002|6|6">6</option>
                                            <option value="7|3002|7|7">7</option>
                                            <option value="8|3002|8|8">8</option>
                                            <option value="9|3002|9|9">9</option>
                                            <option value="10|3002|10|10">10</option>
                                        </select>
                                    </div>
                                    <div style="clear: both;"></div>
                                </div>

                            </td>
                        </tr>
                    </table>
                    <br />
                    <input type="button" value="Submit" onclick="saqli.gotoStage2();" />
                </div>

                <!-- ************************************************************ -->
                <!-- ************************************************************ -->
                <!-- STAGE 2 -->
                <div id="divStage2" style="display: none;">
                    <!-- Stage 2 Contents -->
                    <h1>Treatment Related Symptoms</h1>
                    <h5>We want to know if you are having any difficulty using your PAP device. Below is a list of symptoms that some people have with this treatment.  Select the symptoms that are most bothersome to you. You can select as many symptoms as you like. After you have finished, click “submit” at the bottom of the page to save your selections.
		</h5>
                    <table class="tbl-quest">
                        <tr id="TID3_QID1">
                            <td>
                                <div>
                                    <input type="checkbox" name="grpCheck_23" value="Runny nose|4002|0" id="rid_4002" />
                                    <label for="rid_4002">Runny nose<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_24" value="A change in how your voice sounds|4004|1" id="rid_4004" />
                                    <label for="rid_4004">A change in how your voice sounds<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_25" value="Stuffed or congested or blocked nose|4006|2" id="rid_4006" />
                                    <label for="rid_4006">Stuffed or congested or blocked nose<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_26" value="Pain in the throat when swallowing that lasts at least an hour|4008|3" id="rid_4008" />
                                    <label for="rid_4008">Pain in the throat when swallowing that lasts at least an hour<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_27" value="Excessive dryness of the nose or throat passages |4010|4" id="rid_4010" />
                                    <label for="rid_4010">Excessive dryness of the nose or throat passages<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_28" value="Pain or aching in your jaw joint or jaw muscles especially upon awakening|4012|5" id="rid_4012" />
                                    <label for="rid_4012">Pain or aching in your jaw joint or jaw muscles especially upon awakening<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_29" value="Feeling self conscious|4014|6" id="rid_4014" />
                                    <label for="rid_4014">Feeling self conscious Soreness in the nose or throat passages<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_30" value="Aching in your teeth that lasts for at least an hour|4016|7" id="rid_4016" />
                                    <label for="rid_4016">Aching in your teeth that lasts for at least an hour Headaches<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_31" value="Discomfort, aching, or tenderness of your gums|4018|8" id="rid_4018" />
                                    <label for="rid_4018">Discomfort, aching, or tenderness of your gums Eye irritation<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_32" value="Hardship in being able to pay for the treatment|4020|9" id="rid_4020" />
                                    <label for="rid_4020">Hardship in being able to pay for the treatment Ear Pain<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_33" value="A sense of suffocation|4022|10" id="rid_4022" />
                                    <label for="rid_4022">A sense of suffocation Waking up frequently during the night<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_34" value="Excessive salivation|4024|11" id="rid_4024" />
                                    <label for="rid_4024">Excessive salivation difficulty returning to sleep if you awaken<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_35" value="Difficulty chewing in the morning|4026|12" id="rid_4026" />
                                    <label for="rid_4026">Difficulty chewing in the morning Air leakage from the nasal mask<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_36" value="Difficulty chewing with your back teeth that persists through most of the day|4028|13" id="rid_4028" />
                                    <label for="rid_4028">Difficulty chewing with your back teeth that persists through most of the day<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_37" value="Discomfort from the nasal mask|4030|14" id="rid_4030" />
                                    <label for="rid_4030">Discomfort from the nasal mask<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_38" value="Marks or rash on your face|4032|15" id="rid_4032" />
                                    <label for="rid_4032">Marks or rash on your face<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_39" value="Movement of your teeth so that the upper and lower teeth do not meet properly any longer |4034|16" id="rid_4034" />
                                    <label for="rid_4034">Movement  of your teeth so that the upper and lower teeth do not meet properly any longer </label>
                                    <br />
                                    <input type="checkbox" name="grpCheck_40" value="Complaints from your partner about the noise of the CPAP machine|4036|17" id="rid_4036" />
                                    <label for="rid_4036">Complaints from your partner about the noise of the CPAP machine<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_41" value="Having fluid/food pass into your nose when you swallow|4038|18" id="rid_4038" />
                                    <label for="rid_4038">Having fluid/food pass into your nose when you swallow<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_42" value="Soreness in the nose or throat passages|4040|19" id="rid_4040" />
                                    <label for="rid_4040">Soreness in the nose or throat passages<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_43" value="Headaches|4042|20" id="rid_4042" />
                                    <label for="rid_4042">Headaches<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_44" value="Eye irritation|4044|21" id="rid_4044" />
                                    <label for="rid_4044">Eye irritation<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_45" value="Ear Pain|4046|22" id="rid_4046" />
                                    <label for="rid_4046">Ear Pain<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_46" value="Waking up frequently during the night|4048|23" id="rid_4048" />
                                    <label for="rid_4048">Waking up frequently during the night<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_47" value="Difficulty returning to sleep if you awaken|4050|24" id="rid_4050" />
                                    <label for="rid_4050">Difficulty returning to sleep if you awaken<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_48" value="Air leakage from the nasal mask|4052|25" id="rid_4052" />
                                    <label for="rid_4052">Air leakage from the nasal mask<br />
                                    </label>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <input type="button" value="Back" onclick="saqli.goBack(1);" />&nbsp;<input type="button" value="Submit" onclick="saqli.gotoStage3();" />
                </div>

                <!-- ************************************************************ -->
                <!-- ************************************************************ -->
                <!-- STAGE 3 -->
                <div id="divStage3" style="display: none;">
                    <!-- Stage 3 Contents -->
                    <h1>Treatment Related Symptoms</h1>
                    <h5 style="margin-bottom: 25px;">This is the list of symptoms you just selected. Now select the symptoms that are the most important to you. If your list only has 5 or fewer symptoms, select all of them. After you have finished, click “submit” at the bottom of the page to save your selections.
		</h5>
                    <table class="tbl-quest">
                        <tr id="TID4_QID0">
                            <td></td>
                        </tr>
                        <tr id="TID4_QID1">
                            <td>
                                <div>
                                    <input type="checkbox" name="grpCheck_49" value="Runny nose|5002|0" id="rid_5002" />
                                    <label for="rid_5002">Runny nose<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_50" value="A change in how your voice sounds|5004|1" id="rid_5004" />
                                    <label for="rid_5004">A change in how your voice sounds<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_51" value="Stuffed or congested or blocked nose|5006|2" id="rid_5006" />
                                    <label for="rid_5006">Stuffed or congested or blocked nose<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_52" value="Pain in the throat when swallowing that lasts at least an hour|5008|3" id="rid_5008" />
                                    <label for="rid_5008">Pain in the throat when swallowing that lasts at least an hour<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_53" value="Excessive dryness of the nose or throat passages |5010|4" id="rid_5010" />
                                    <label for="rid_5010">Excessive dryness of the nose or throat passages<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_54" value="Pain or aching in your jaw joint or jaw muscles especially upon awakening|5012|5" id="rid_5012" />
                                    <label for="rid_5012">Pain or aching in your jaw joint or jaw muscles especially upon awakening<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_55" value="Feeling self conscious|5014|6" id="rid_5014" />
                                    <label for="rid_5014">Feeling self conscious Soreness in the nose or throat passages<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_56" value="Aching in your teeth that lasts for at least an hour|5016|7" id="rid_5016" />
                                    <label for="rid_5016">Aching in your teeth that lasts for at least an hour Headaches<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_57" value="Discomfort, aching, or tenderness of your gums|5018|8" id="rid_5018" />
                                    <label for="rid_5018">Discomfort, aching, or tenderness of your gums Eye irritation<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_58" value="Hardship in being able to pay for the treatment|5020|9" id="rid_5020" />
                                    <label for="rid_5020">Hardship in being able to pay for the treatment Ear Pain<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_59" value="A sense of suffocation|5022|10" id="rid_5022" />
                                    <label for="rid_5022">A sense of suffocation Waking up frequently during the night<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_60" value="Excessive salivation|5024|11" id="rid_5024" />
                                    <label for="rid_5024">Excessive salivation difficulty returning to sleep if you awaken<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_61" value="Difficulty chewing in the morning|5026|12" id="rid_5026" />
                                    <label for="rid_5026">Difficulty chewing in the morning Air leakage from the nasal mask<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_62" value="Difficulty chewing with your back teeth that persists through most of the day|5028|13" id="rid_5028" />
                                    <label for="rid_5028">Difficulty chewing with your back teeth that persists through most of the day<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_63" value="Discomfort from the nasal mask|5030|14" id="rid_5030" />
                                    <label for="rid_5030">Discomfort from the nasal mask<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_64" value="Marks or rash on your face|5032|15" id="rid_5032" />
                                    <label for="rid_5032">Marks or rash on your face<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_65" value="Movement of your teeth so that the upper and lower teeth do not meet properly any longer |5034|16" id="rid_5034" />
                                    <label for="rid_5034">Movement  of your teeth so that the upper and lower teeth do not meet properly any longer <br /></label>
                                    
                                    <input type="checkbox" name="grpCheck_66" value="Complaints from your partner about the noise of the CPAP machine|5036|17" id="rid_5036" />
                                    <label for="rid_5036">Complaints from your partner about the noise of the CPAP machine<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_67" value="Having fluid/food pass into your nose when you swallow|5038|18" id="rid_5038" />
                                    <label for="rid_5038">Having fluid/food pass into your nose when you swallow<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_68" value="Soreness in the nose or throat passages|5040|19" id="rid_5040" />
                                    <label for="rid_5040">Soreness in the nose or throat passages<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_69" value="Headaches|5042|20" id="rid_5042" />
                                    <label for="rid_5042">Headaches<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_70" value="Eye irritation|5044|21" id="rid_5044" />
                                    <label for="rid_5044">Eye irritation<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_71" value="Ear Pain|5046|22" id="rid_5046" />
                                    <label for="rid_5046">Ear Pain<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_72" value="Waking up frequently during the night|5048|23" id="rid_5048" />
                                    <label for="rid_5048">Waking up frequently during the night<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_73" value="Difficulty returning to sleep if you awaken|5050|24" id="rid_5050" />
                                    <label for="rid_5050">Difficulty returning to sleep if you awaken<br />
                                    </label>
                                    <input type="checkbox" name="grpCheck_74" value="Air leakage from the nasal mask|5052|25" id="rid_5052" />
                                    <label for="rid_5052">Air leakage from the nasal mask<br />
                                    </label>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <input type="button" value="Back" onclick="saqli.goBack(2);" />&nbsp;<input type="button" value="Submit" onclick="saqli.gotoStage4();" />
                </div>

                <!-- ************************************************************ -->
                <!-- ************************************************************ -->
                <!-- STAGE 4 -->
                <div id="divStage4" style="display: none;">
                    <!-- Stage 4 Contents -->
                    <h1>Treatment Related Symptoms</h1>
                    <h5 style="margin-bottom: 25px;">These are the symptoms you selected as being the most important to you. Tell us how much of a problem each of these symptoms is to you now by making a selection from the drop down list below each symptom.  Using the sliding scale at the bottom of the page, indicate how much of an impact your treatment related symptoms are having on your quality of life. After you have finished, click “submit” at the bottom of the page to save your selections.
		</h5>
                    <table id="tblTID5">
                        <tr id="TID5_QID1">
                            <td>
                                <strong>About your runny nose, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_6002" name="grpCombo_75">
                                        <option value=""></option>
                                        <option value="A very large problem|6002|1|1">A very large problem</option>
                                        <option value="A large problem|6002|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6002|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6002|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6002|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6002|6|6">A small problem</option>
                                        <option value="No problem|6002|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID5_QID2">
                            <td>
                                <strong>About your a change in how your voice sounds, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_6004" name="grpCombo_76">
                                        <option value=""></option>
                                        <option value="A very large problem|6004|1|1">A very large problem</option>
                                        <option value="A large problem|6004|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6004|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6004|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6004|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6004|6|6">A small problem</option>
                                        <option value="No problem|6004|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID5_QID3">
                            <td>
                                <strong>About your stuffed or congested or blocked nose, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_6006" name="grpCombo_77">
                                        <option value=""></option>
                                        <option value="A very large problem|6006|1|1">A very large problem</option>
                                        <option value="A large problem|6006|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6006|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6006|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6006|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6006|6|6">A small problem</option>
                                        <option value="No problem|6006|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID5_QID4">
                            <td>
                                <strong>About your pain in the throat when swallowing that lasts at least an hour, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_6008" name="grpCombo_78">
                                        <option value=""></option>
                                        <option value="A very large problem|6008|1|1">A very large problem</option>
                                        <option value="A large problem|6008|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6008|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6008|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6008|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6008|6|6">A small problem</option>
                                        <option value="No problem|6008|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID5_QID5">
                            <td>
                                <strong>About your excessive dryness of the nose or throat passages, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_6010" name="grpCombo_79">
                                        <option value=""></option>
                                        <option value="A very large problem|6010|1|1">A very large problem</option>
                                        <option value="A large problem|6010|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6010|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6010|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6010|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6010|6|6">A small problem</option>
                                        <option value="No problem|6010|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID5_QID6">
                            <td>
                                <strong>About your pain or aching in your jaw joint or jaw muscles especially upon awakening, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_6012" name="grpCombo_80">
                                        <option value=""></option>
                                        <option value="A very large problem|6012|1|1">A very large problem</option>
                                        <option value="A large problem|6012|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6012|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6012|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6012|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6012|6|6">A small problem</option>
                                        <option value="No problem|6012|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>

                        <tr id="TID5_QID7">
                            <td>
                                <strong>About your feeling self conscious, how large is this problem to you?</strong>
                                <div>
                                    <select id="rid_6014" name="grpCombo_81">
                                        <option value=""></option>
                                        <option value="A very large problem|6014|1|1">A very large problem</option>
                                        <option value="A large problem|6014|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6014|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6014|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6014|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6014|6|6">A small problem</option>
                                        <option value="No problem|6014|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID8">
                            <td>
                                <strong>About your aching in your teeth that lasts for at least an hour, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6016" name="grpCombo_82">
                                        <option value=""></option>
                                        <option value="A very large problem|6016|1|1">A very large problem</option>
                                        <option value="A large problem|6016|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6016|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6016|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6016|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6016|6|6">A small problem</option>
                                        <option value="No problem|6016|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID9">
                            <td>
                                <strong>About your discomfort, aching, or tenderness of your gums, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6018" name="grpCombo_83">
                                        <option value=""></option>
                                        <option value="A very large problem|6018|1|1">A very large problem</option>
                                        <option value="A large problem|6018|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6018|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6018|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6018|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6018|6|6">A small problem</option>
                                        <option value="No problem|6018|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID10">
                            <td>
                                <strong>About your hardship in being able to pay for the treatment, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6020" name="grpCombo_84">
                                        <option value=""></option>
                                        <option value="A very large problem|6020|1|1">A very large problem</option>
                                        <option value="A large problem|6020|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6020|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6020|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6020|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6020|6|6">A small problem</option>
                                        <option value="No problem|6020|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID11">
                            <td>
                                <strong>About your a sense of suffocation, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6022" name="grpCombo_85">
                                        <option value=""></option>
                                        <option value="A very large problem|6022|1|1">A very large problem</option>
                                        <option value="A large problem|6022|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6022|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6022|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6022|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6022|6|6">A small problem</option>
                                        <option value="No problem|6022|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID12">
                            <td>
                                <strong>About your excessive salivation, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6024" name="grpCombo_86">
                                        <option value=""></option>
                                        <option value="A very large problem|6024|1|1">A very large problem</option>
                                        <option value="A large problem|6024|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6024|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6024|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6024|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6024|6|6">A small problem</option>
                                        <option value="No problem|6024|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID13">
                            <td>
                                <strong>About your difficulty chewing in the morning, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6026" name="grpCombo_87">
                                        <option value=""></option>
                                        <option value="A very large problem|6026|1|1">A very large problem</option>
                                        <option value="A large problem|6026|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6026|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6026|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6026|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6026|6|6">A small problem</option>
                                        <option value="No problem|6026|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID14">
                            <td>
                                <strong>About your difficulty chewing with your back teeth that persists through most of the day, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6028" name="grpCombo_88">
                                        <option value=""></option>
                                        <option value="A very large problem|6028|1|1">A very large problem</option>
                                        <option value="A large problem|6028|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6028|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6028|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6028|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6028|6|6">A small problem</option>
                                        <option value="No problem|6028|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID15">
                            <td>
                                <strong>About your discomfort from the nasal mask, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6030" name="grpCombo_89">
                                        <option value=""></option>
                                        <option value="A very large problem|6030|1|1">A very large problem</option>
                                        <option value="A large problem|6030|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6030|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6030|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6030|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6030|6|6">A small problem</option>
                                        <option value="No problem|6030|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID16">
                            <td>
                                <strong>About your marks or rash on your face, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6032" name="grpCombo_90">
                                        <option value=""></option>
                                        <option value="A very large problem|6032|1|1">A very large problem</option>
                                        <option value="A large problem|6032|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6032|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6032|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6032|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6032|6|6">A small problem</option>
                                        <option value="No problem|6032|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID17">
                            <td>
                                <strong>About your movement of your teeth so that the upper and lower teeth do not meet properly any longer, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6034" name="grpCombo_91">
                                        <option value=""></option>
                                        <option value="A very large problem|6034|1|1">A very large problem</option>
                                        <option value="A large problem|6034|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6034|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6034|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6034|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6034|6|6">A small problem</option>
                                        <option value="No problem|6034|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID18">
                            <td>
                                <strong>About your complaints from your partner about the noise of the CPAP machine, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6036" name="grpCombo_92">
                                        <option value=""></option>
                                        <option value="A very large problem|6036|1|1">A very large problem</option>
                                        <option value="A large problem|6036|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6036|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6036|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6036|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6036|6|6">A small problem</option>
                                        <option value="No problem|6036|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID19">
                            <td>
                                <strong>About your having fluid/food pass into your nose when you swallow, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6038" name="grpCombo_93">
                                        <option value=""></option>
                                        <option value="A very large problem|6038|1|1">A very large problem</option>
                                        <option value="A large problem|6038|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6038|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6038|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6038|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6038|6|6">A small problem</option>
                                        <option value="No problem|6038|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID20">
                            <td>
                                <strong>About your soreness in the nose or throat passages, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6040" name="grpCombo_94">
                                        <option value=""></option>
                                        <option value="A very large problem|6040|1|1">A very large problem</option>
                                        <option value="A large problem|6040|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6040|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6040|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6040|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6040|6|6">A small problem</option>
                                        <option value="No problem|6040|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID21">
                            <td>
                                <strong>About your headaches, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6042" name="grpCombo_95">
                                        <option value=""></option>
                                        <option value="A very large problem|6042|1|1">A very large problem</option>
                                        <option value="A large problem|6042|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6042|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6042|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6042|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6042|6|6">A small problem</option>
                                        <option value="No problem|6042|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID22">
                            <td>
                                <strong>About your eye irritation, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6044" name="grpCombo_96">
                                        <option value=""></option>
                                        <option value="A very large problem|6044|1|1">A very large problem</option>
                                        <option value="A large problem|6044|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6044|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6044|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6044|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6044|6|6">A small problem</option>
                                        <option value="No problem|6044|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID23">
                            <td>
                                <strong>About your ear Pain, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6046" name="grpCombo_97">
                                        <option value=""></option>
                                        <option value="A very large problem|6046|1|1">A very large problem</option>
                                        <option value="A large problem|6046|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6046|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6046|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6046|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6046|6|6">A small problem</option>
                                        <option value="No problem|6046|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID24">
                            <td>
                                <strong>About your Waking up frequently during the night, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6048" name="grpCombo_98">
                                        <option value=""></option>
                                        <option value="A very large problem|6048|1|1">A very large problem</option>
                                        <option value="A large problem|6048|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6048|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6048|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6048|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6048|6|6">A small problem</option>
                                        <option value="No problem|6048|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID25">
                            <td>
                                <strong>About your difficulty returning to sleep if you awaken, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6050" name="grpCombo_99">
                                        <option value=""></option>
                                        <option value="A very large problem|6050|1|1">A very large problem</option>
                                        <option value="A large problem|6050|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6050|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6050|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6050|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6050|6|6">A small problem</option>
                                        <option value="No problem|6050|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>


                        <tr id="TID5_QID26">
                            <td>
                                <strong>About your Air leakage from the nasal mask, how large is this problem to you?</strong>

                                <div>
                                    <select id="rid_6052" name="grpCombo_100">
                                        <option value=""></option>
                                        <option value="A very large problem|6052|1|1">A very large problem</option>
                                        <option value="A large problem|6052|2|2">A large problem</option>
                                        <option value="A moderate to large problem|6052|3|3">A moderate to large problem</option>
                                        <option value="A moderate problem|6052|4|4">A moderate problem</option>
                                        <option value="A small to moderate problem|6052|5|5">A small to moderate problem</option>
                                        <option value="A small problem|6052|6|6">A small problem</option>
                                        <option value="No problem|6052|7|7">No problem</option>

                                    </select>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <h1>Impact</h1>
                    <!-- <strong>I.  Daily Functioning, Social Interactions, Emotional Functioning, Symptoms</strong> -->
                    <p>
                        Please think of the questions in the previous sections. Having been treated for your breathing problem during sleep, how much of an impact has it had on the quality of your life since you started treatment?
	
                    </p>
                    <table>
                        <tr id="TID6_QID1">
                            <td>
                                <div style="margin-left: 15px;">
                                    <div style="width: 450px; float: left;">
                                        <table style="width: inherit;">
                                            <tr>
                                                <td colspan="2">
                                                    <div id="div_rid_7002"></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left; width: 50%; color: #0094ff;">0<br />
                                                    <span style="font-size: 12px;">No Impact</span>
                                                </td>
                                                <td style="text-align: right; width: 50%; color: #0094ff;">10<br />
                                                    <span style="font-size: 12px;">Extremely Large Impact</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="float: left; margin-left: 20px;">
                                        <select id="rid_7002" name="grpCombo_101">
                                            <option value=""></option>
                                            <option value="0|7002|0|0">0</option>
                                            <option value="1|7002|1|1">1</option>
                                            <option value="2|7002|2|2">2</option>
                                            <option value="3|7002|3|3">3</option>
                                            <option value="4|7002|4|4">4</option>
                                            <option value="5|7002|5|5">5</option>
                                            <option value="6|7002|6|6">6</option>
                                            <option value="7|7002|7|7">7</option>
                                            <option value="8|7002|8|8">8</option>
                                            <option value="9|7002|9|9">9</option>
                                            <option value="10|7002|10|10">10</option>
                                        </select>
                                    </div>
                                    <div style="clear: both;"></div>
                                </div>

                            </td>
                        </tr>
                    </table>
                    <input type="button" id="btnBack4" value="Back" onclick="saqli.goBack(3);" />&nbsp;<asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_OnClick" />
                </div>
            </asp:View>
            <asp:View ID="vwMessage" runat="server">
                <div style="width: 95%; margin: 10px auto; padding: 5px; border: 1px solid #e1e1e1; background-color: #f2f0d0; line-height: 155%;">
                    The Follow-Up Sleep Apnea Symptoms questionnaire is not available if the Sleep Apnea Symptoms questionnaire has not been previously completed. 
Please contact your Sleep Specialist and let them know you need to complete the Sleep Apnea Symptoms questionnaire.
                </div>
            </asp:View>
        </asp:MultiView>

        <asp:HiddenField ID="htxtResponses" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="cScripts" runat="server" ContentPlaceHolderID="cpScripts">
    <script type="text/javascript" src="js/questions.js"></script>
    <script type="text/javascript" src="js/questions.mid3019.js"></script>
    <script type="text/javascript">

        var rawResponses = $('[id$="htxtResponses"]').val();
        saqli.opts.responses = eval(rawResponses);

        questions.opts.skipPatterns = [{
            rid: 5002,
            checked: {
                show: ['TID5_QID1']
            }
        }, {
            rid: 5004,
            checked: {
                show: ['TID5_QID2']
            }
        }, {
            rid: 5006,
            checked: {
                show: ['TID5_QID3']
            }
        }, {
            rid: 5008,
            checked: {
                show: ['TID5_QID4']
            }
        }, {
            rid: 5010,
            checked: {
                show: ['TID5_QID5']
            }
        }, {
            rid: 5012,
            checked: {
                show: ['TID5_QID6']
            }
        }, {
            rid: 5014,
            checked: {
                show: ['TID5_QID7']
            }
        }, {
            rid: 5016,
            checked: {
                show: ['TID5_QID8']
            }
        }, {
            rid: 5018,
            checked: {
                show: ['TID5_QID9']
            }
        }, {
            rid: 5020,
            checked: {
                show: ['TID5_QID10']
            }
        }, {
            rid: 5022,
            checked: {
                show: ['TID5_QID11']
            }
        }, {
            rid: 5024,
            checked: {
                show: ['TID5_QID12']
            }
        }, {
            rid: 5026,
            checked: {
                show: ['TID5_QID13']
            }
        }, {
            rid: 5028,
            checked: {
                show: ['TID5_QID14']
            }
        }, {
            rid: 5030,
            checked: {
                show: ['TID5_QID15']
            }
        }, {
            rid: 5032,
            checked: {
                show: ['TID5_QID16']
            }
        }, {
            rid: 5034,
            checked: {
                show: ['TID5_QID17']
            }
        }, {
            rid: 5036,
            checked: {
                show: ['TID5_QID18']
            }
        }, {
            rid: 5038,
            checked: {
                show: ['TID5_QID19']
            }
        }, {
            rid: 5040,
            checked: {
                show: ['TID5_QID20']
            }
        }, {
            rid: 5042,
            checked: {
                show: ['TID5_QID21']
            }
        }, {
            rid: 5044,
            checked: {
                show: ['TID5_QID22']
            }
        }, {
            rid: 5046,
            checked: {
                show: ['TID5_QID23']
            }
        }, {
            rid: 5048,
            checked: {
                show: ['TID5_QID24']
            }
        }, {
            rid: 5050,
            checked: {
                show: ['TID5_QID25']
            }
        }, {
            rid: 5052,
            checked: {
                show: ['TID5_QID26']
            }
        }];

        $(function () {
            saqli.renderSliders(['rid_3002', 'rid_7002']);
            setTimeout(function () {
                saqli.checkStage1opts();
            }, 100);
        });

    </script>
</asp:Content>