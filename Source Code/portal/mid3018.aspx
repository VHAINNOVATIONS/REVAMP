<%@ Page Title="Sleep Quality of Life Index (SQLI)" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mid3018.aspx.cs" Inherits="mid3018" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHeader" runat="Server">
    <link href="css/questions.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divQuestions" runat="server" class="intake-wrapper">

        <!-- this tells the code behind how many responces to process -->
        <input type="hidden" name="ResponseCount" value="63" /><br />

        <h1>Sleep Apnea Symptoms</h1>
        <%--Stage 1--%>
        <div id="divStage1" style="display: block;">
            <h4>Select the sleep related symptoms below that are most bothersome to you. 
            You can select as many symptoms as you like. 
            After you have finished, click “submit” at the bottom of the page to save your selections.</h4>
            <table class="tbl-quest">

                <tr id="TID1_QID1">
                    <td>
                        <%--<strong>From the following list of symptoms, select the 5 symptoms that are a very large problem to you.</strong>--%>
                        <div>
                            <input type="checkbox" name="grpCheck_1" value="Decreased energy|2|0" id="rid_2" /><label for="rid_2">Decreased energy</label><br />
                            <input type="checkbox" name="grpCheck_2" value="Excessive fatigue|4|0" id="rid_4" /><label for="rid_4">Excessive fatigue</label><br />
                            <input type="checkbox" name="grpCheck_3" value="Feeling that ordinary activities required an extra effort to perform or complete|6|0" id="rid_6" /><label for="rid_6">Feeling that ordinary activities required an extra effort to perform or complete</label><br />
                            <input type="checkbox" name="grpCheck_4" value="Falling asleep at inappropriate times or places|8|0" id="rid_8" /><label for="rid_8">Falling asleep at inappropriate times or places</label><br />
                            <input type="checkbox" name="grpCheck_5" value="Falling asleep if not stimulated or active|10|0" id="rid_10" /><label for="rid_10">Falling asleep if not stimulated or active</label><br />
                            <input type="checkbox" name="grpCheck_6" value="Difficulty with a dry or sore mouth/throat upon awakening|12|0" id="rid_12" /><label for="rid_12">Difficulty with a dry or sore mouth/throat upon awakening</label><br />
                            <input type="checkbox" name="grpCheck_7" value="Waking up often (more than twice) during the night|14|0" id="rid_14" /><label for="rid_14">Waking up often (more than twice) during the night</label><br />
                            <input type="checkbox" name="grpCheck_8" value="Difficulty returning to sleep if you wake up in the night|16|0" id="rid_16" /><label for="rid_16">Difficulty returning to sleep if you wake up in the night</label><br />
                            <input type="checkbox" name="grpCheck_9" value="Concern about the times you stop breathing at night|18|0" id="rid_18" /><label for="rid_18">Concern about the times you stop breathing at night</label><br />
                            <input type="checkbox" name="grpCheck_10" value="Waking up at night feeling like you were choking|20|0" id="rid_20" /><label for="rid_20">Waking up at night feeling like you were choking</label><br />
                            <input type="checkbox" name="grpCheck_11" value="Waking up in the morning with a headache|22|0" id="rid_22" /><label for="rid_22">Waking up in the morning with a headache</label><br />
                            <input type="checkbox" name="grpCheck_12" value="Waking up in the morning feeling unrefreshed and/or tired|24|0" id="rid_24" /><label for="rid_24">Waking up in the morning feeling unrefreshed and/or tired</label><br />
                            <input type="checkbox" name="grpCheck_13" value="Waking up more than once per night (on average) to urinate|26|0" id="rid_26" /><label for="rid_26">Waking up more than once per night (on average) to urinate</label><br />
                            <input type="checkbox" name="grpCheck_14" value="A feeling that your sleep is restless|28|0" id="rid_28" /><label for="rid_28">A feeling that your sleep is restless</label><br />
                            <input type="checkbox" name="grpCheck_15" value="Difficulty staying awake while reading|30|0" id="rid_30" /><label for="rid_30">Difficulty staying awake while reading</label><br />
                            <input type="checkbox" name="grpCheck_16" value="Difficulty staying awake while trying to carry on a conversation|32|0" id="rid_32" /><label for="rid_32">Difficulty staying awake while trying to carry on a conversation</label><br />
                            <input type="checkbox" name="grpCheck_17" value="Difficulty staying awake while trying to watch something (eg.  concert,  theatre production,  movie,  planned TV show)|34|0" id="rid_34" /><label for="rid_34">Difficulty staying awake while trying to watch something (eg.  concert,  theatre production,  movie,  planned TV show)</label><br />
                            <input type="checkbox" name="grpCheck_18" value="Fighting the urge to fall asleep while driving|36|0" id="rid_36" /><label for="rid_36">Fighting the urge to fall asleep while driving</label><br />
                            <input type="checkbox" name="grpCheck_19" value="A reluctance or inability to drive for more than 1 hour|38|0" id="rid_38" /><label for="rid_38">A reluctance or inability to drive for more than 1 hour</label><br />
                            <input type="checkbox" name="grpCheck_20" value="Concern regarding close calls while driving caused partially or totally by your inability to remain alert|40|0" id="rid_40" /><label for="rid_40">Concern regarding close calls while driving caused partially or totally by your inability to remain alert</label><br />
                            <input type="checkbox" name="grpCheck_21" value="Concern regarding yours or other’s safety when you are operating a motor vehicle and/or machinery|42|0" id="rid_42" /><label for="rid_42">Concern regarding yours or other’s safety when you are operating a motor vehicle and/or machinery</label><br />
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <input type="button" value="Submit" onclick="saqli.gotoStage2();" />
        </div>

        <%--Stage 2--%>
        <div id="divStage2" style="display: none;">
            <h4>This is the list of sleep related symptoms you just selected. 
                Now select the 5 symptoms that are the most important to you. 
                If your list only has 5 or fewer symptoms, select all of them. 
                After you have finished, click “submit” at the bottom of the page to save your selections.</h4>
            <table class="tbl-quest">
                <tr id="TID2_QID0">
                    <td></td>
                </tr>
                <tr id="TID2_QID1">
                    <td>
                        <%--<strong>From the following list of symptoms, select the 5 symptoms that are a very large problem to you?</strong>--%>
                        <div>
                            <input type="checkbox" name="grpCheck_22" style="display: none;" value="Decreased energy|1002|0" id="rid_1002" /><label for="rid_1002" style="display: none;">Decreased energy<br />
                            </label>
                            <input type="checkbox" name="grpCheck_23" style="display: none;" value="Excessive fatigue|1004|0" id="rid_1004" /><label for="rid_1004" style="display: none;">Excessive fatigue<br />
                            </label>
                            <input type="checkbox" name="grpCheck_24" style="display: none;" value="Feeling that ordinary activities required an extra effort to perform or complete|1006|0" id="rid_1006" /><label for="rid_1006" style="display: none;">Feeling that ordinary activities required an extra effort to perform or complete<br />
                            </label>
                            <input type="checkbox" name="grpCheck_25" style="display: none;" value="Falling asleep at inappropriate times or places|1008|0" id="rid_1008" /><label for="rid_1008" style="display: none;">Falling asleep at inappropriate times or places<br />
                            </label>
                            <input type="checkbox" name="grpCheck_26" style="display: none;" value="Falling asleep if not stimulated or active|1010|0" id="rid_1010" /><label for="rid_1010" style="display: none;">Falling asleep if not stimulated or active<br />
                            </label>
                            <input type="checkbox" name="grpCheck_27" style="display: none;" value="Difficulty with a dry or sore mouth/throat upon awakening|1012|0" id="rid_1012" /><label for="rid_1012" style="display: none;">Difficulty with a dry or sore mouth/throat upon awakening<br />
                            </label>
                            <input type="checkbox" name="grpCheck_28" style="display: none;" value="Waking up often (more than twice) during the night|1014|0" id="rid_1014" /><label for="rid_1014" style="display: none;">Waking up often (more than twice) during the night<br />
                            </label>
                            <input type="checkbox" name="grpCheck_29" style="display: none;" value="Difficulty returning to sleep if you wake up in the night|1016|0" id="rid_1016" /><label for="rid_1016" style="display: none;">Difficulty returning to sleep if you wake up in the night<br />
                            </label>
                            <input type="checkbox" name="grpCheck_30" style="display: none;" value="Concern about the times you stop breathing at night|1018|0" id="rid_1018" /><label for="rid_1018" style="display: none;">Concern about the times you stop breathing at night<br />
                            </label>
                            <input type="checkbox" name="grpCheck_31" style="display: none;" value="Waking up at night feeling like you were choking|1020|0" id="rid_1020" /><label for="rid_1020" style="display: none;">Waking up at night feeling like you were choking<br />
                            </label>
                            <input type="checkbox" name="grpCheck_32" style="display: none;" value="Waking up in the morning with a headache|1022|0" id="rid_1022" /><label for="rid_1022" style="display: none;">Waking up in the morning with a headache<br />
                            </label>
                            <input type="checkbox" name="grpCheck_33" style="display: none;" value="Waking up in the morning feeling unrefreshed and/or tired|1024|0" id="rid_1024" /><label for="rid_1024" style="display: none;">Waking up in the morning feeling unrefreshed and/or tired<br />
                            </label>
                            <input type="checkbox" name="grpCheck_34" style="display: none;" value="Waking up more than once per night (on average) to urinate|1026|0" id="rid_1026" /><label for="rid_1026" style="display: none;">Waking up more than once per night (on average) to urinate<br />
                            </label>
                            <input type="checkbox" name="grpCheck_35" style="display: none;" value="A feeling that your sleep is restless|1028|0" id="rid_1028" /><label for="rid_1028" style="display: none;">A feeling that your sleep is restless<br />
                            </label>
                            <input type="checkbox" name="grpCheck_36" style="display: none;" value="Difficulty staying awake while reading|1030|0" id="rid_1030" /><label for="rid_1030" style="display: none;">Difficulty staying awake while reading<br />
                            </label>
                            <input type="checkbox" name="grpCheck_37" style="display: none;" value="Difficulty staying awake while trying to carry on a conversation|1032|0" id="rid_1032" /><label for="rid_1032" style="display: none;">Difficulty staying awake while trying to carry on a conversation<br />
                            </label>
                            <input type="checkbox" name="grpCheck_38" style="display: none;" value="Difficulty staying awake while trying to watch something (eg.  concert,  theatre production,  movie,  planned TV show)|1034|0" id="rid_1034" /><label for="rid_1034" style="display: none;">Difficulty staying awake while trying to watch something (eg.  concert,  theatre production,  movie,  planned TV show)<br />
                            </label>
                            <input type="checkbox" name="grpCheck_39" style="display: none;" value="Fighting the urge to fall asleep while driving|1036|0" id="rid_1036" /><label for="rid_1036" style="display: none;">Fighting the urge to fall asleep while driving<br />
                            </label>
                            <input type="checkbox" name="grpCheck_40" style="display: none;" value="A reluctance or inability to drive for more than 1 hour|1038|0" id="rid_1038" /><label for="rid_1038" style="display: none;">A reluctance or inability to drive for more than 1 hour<br />
                            </label>
                            <input type="checkbox" name="grpCheck_41" style="display: none;" value="Concern regarding close calls while driving caused partially or totally by your inability to remain alert|1040|0" id="rid_1040" /><label for="rid_1040" style="display: none;">Concern regarding close calls while driving caused partially or totally by your inability to remain alert<br />
                            </label>
                            <input type="checkbox" name="grpCheck_42" style="display: none;" value="Concern regarding yours or other’s safety when you are operating a motor vehicle and/or machinery|1042|0" id="rid_1042" /><label for="rid_1042" style="display: none;">Concern regarding yours or other’s safety when you are operating a motor vehicle and/or machinery<br />
                            </label>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <input type="button" value="Back" onclick="return saqli.goBack(1);" />&nbsp;<input type="button" value="Submit" onclick="    saqli.gotoStage3();" />
        </div>

        <%--Stage 3--%>
        <div id="divStage3" style="display: none;">
            <h4>These are the sleep related symptoms you selected as being the most important to you. Tell us how much of a problem each of these symptoms is to you now by making a selection from the drop down list below each symptom. After you have finished, click “submit” at the bottom of the page to save your selections.
            </h4>

            <table>
                <tr id="TID3_QID23">
                    <td>
                        <strong>About your decreased energy, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2002" name="grpCombo_43">
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

                <tr id="TID3_QID24">
                    <td>
                        <strong>About your excessive fatigue, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2004" name="grpCombo_44">
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

                <tr id="TID3_QID25">
                    <td>
                        <strong>About your feeling that ordinary activities required an extra effort to perform or complete, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2006" name="grpCombo_45">
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

                <tr id="TID3_QID26">
                    <td>
                        <strong>About your falling asleep at inappropriate times or places, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2008" name="grpCombo_46">
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

                <tr id="TID3_QID27">
                    <td>
                        <strong>About your falling asleep if not stimulated or active, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2010" name="grpCombo_47">
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

                <tr id="TID3_QID28">
                    <td>
                        <strong>About your difficulty with a dry or sore mouth/throat upon awakening, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2012" name="grpCombo_48">
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

                <tr id="TID3_QID29">
                    <td>
                        <strong>Waking up often during the night, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2014" name="grpCombo_49">
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

                <tr id="TID3_QID30">
                    <td>
                        <strong>About your difficulty returning to sleep if you wake up in the night, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2016" name="grpCombo_50">
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

                <tr id="TID3_QID31">
                    <td>
                        <strong>Concern about the times you stop breathing at night, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2018" name="grpCombo_51">
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

                <tr id="TID3_QID32">
                    <td>
                        <strong>About your waking up at night feeling like you were choking, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2020" name="grpCombo_52">
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

                <tr id="TID3_QID33">
                    <td>
                        <strong>About your waking up in the morning with a headache, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2022" name="grpCombo_53">
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

                <tr id="TID3_QID34">
                    <td>
                        <strong>About your waking up in the morning feeling unrefreshed and/or tired, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2024" name="grpCombo_54">
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

                <tr id="TID3_QID35">
                    <td>
                        <strong>About your waking up more than once per night (on average) to urinate, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2026" name="grpCombo_55">
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

                <tr id="TID3_QID36">
                    <td>
                        <strong>About your  feeling that your sleep is restless, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2028" name="grpCombo_56">
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

                <tr id="TID3_QID37">
                    <td>
                        <strong>About your aifficulty staying awake while reading, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2030" name="grpCombo_57">
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

                <tr id="TID3_QID38">
                    <td>
                        <strong>About your difficulty staying awake while trying to carry on a conversation, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2032" name="grpCombo_58">
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

                <tr id="TID3_QID39">
                    <td>
                        <strong>About your difficulty staying awake while trying to watch something, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2034" name="grpCombo_59">
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

                <tr id="TID3_QID40">
                    <td>
                        <strong>About your fighting the urge to fall asleep while driving, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2036" name="grpCombo_60">
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

                <tr id="TID3_QID41">
                    <td>
                        <strong>About your reluctance or inability to drive for more than 1 hour, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2038" name="grpCombo_61">
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

                <tr id="TID3_QID42">
                    <td>
                        <strong>About your concern regarding close calls while driving caused partially or totally by your inability to remain alert, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2040" name="grpCombo_62">
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

                <tr id="TID3_QID43">
                    <td>
                        <strong>About your concern regarding yours or other’s safety when you are operating a motor vehicle and/or machinery, how large is this problem to you?</strong>
                        <div>
                            <select id="rid_2042" name="grpCombo_63">
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
            <input type="button" value="Back" onclick="return saqli.goBack(2);" />&nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_OnClick" />
        </div>
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
                rid: 1002,
                checked: {
                    show: ['TID3_QID23']
                }
            },

        {
            rid: 1004,
            checked: {
                show: ['TID3_QID24']
            }
        },

        {
            rid: 1006,
            checked: {
                show: ['TID3_QID25']
            }
        },

        {
            rid: 1008,
            checked: {
                show: ['TID3_QID26']
            }
        },

        {
            rid: 1010,
            checked: {
                show: ['TID3_QID27']
            }
        },

        {
            rid: 1012,
            checked: {
                show: ['TID3_QID28']
            }
        },

        {
            rid: 1014,
            checked: {
                show: ['TID3_QID29']
            }
        },

        {
            rid: 1016,
            checked: {
                show: ['TID3_QID30']
            }
        },

        {
            rid: 1018,
            checked: {
                show: ['TID3_QID31']
            }
        },

        {
            rid: 1020,
            checked: {
                show: ['TID3_QID32']
            }
        },

        {
            rid: 1022,
            checked: {
                show: ['TID3_QID33']
            }
        },

        {
            rid: 1024,
            checked: {
                show: ['TID3_QID34']
            }
        },

        {
            rid: 1026,
            checked: {
                show: ['TID3_QID35']
            }
        },

        {
            rid: 1028,
            checked: {
                show: ['TID3_QID36']
            }
        },

        {
            rid: 1030,
            checked: {
                show: ['TID3_QID37']
            }
        },

        {
            rid: 1032,
            checked: {
                show: ['TID3_QID38']
            }
        },

        {
            rid: 1034,
            checked: {
                show: ['TID3_QID39']
            }
        },

        {
            rid: 1036,
            checked: {
                show: ['TID3_QID40']
            }
        },

        {
            rid: 1038,
            checked: {
                show: ['TID3_QID41']
            }
        },

        {
            rid: 1040,
            checked: {
                show: ['TID3_QID42']
            }
        },

        {
            rid: 1042,
            checked: {
                show: ['TID3_QID43']
            }
        }
        ];

        questions.checkSkipPatterns = function () {
            var _me = this,
                _checks = $('input[type="checkbox"]', $('div[id$="divQuestions"]')),
                _radios = $('input[type="radio"]', $('div[id$="divQuestions"]'));

            //return if there is not a pattern defined
            if (typeof (_me.opts.skipPatterns) != "undefined") {
                $.each(_me.opts.skipPatterns, function (i, o) {
                    var mObj = $('[id="rid_' + o.rid + '"]')[0];
                    if (mObj.getAttribute('type') == "radio" || mObj.getAttribute('type') == "checkbox") {
                        if (mObj.checked) {

                            if (typeof (o.checked.show) != "undefined") {
                                $.each(o.checked.show, function (n, v) {
                                    $('tr[id="' + v + '"]').show().removeAttr('skipped');
                                });
                            }

                            if (typeof (o.checked.hide) != "undefined") {
                                $.each(o.checked.hide, function (n, v) {
                                    _me.clearQuestionControls(v);
                                    $('tr[id="' + v + '"]').hide().attr('skipped', 'skipped');
                                });
                            }
                        }
                        else {
                            if (typeof (o.checked.show) != "undefined") {
                                $.each(o.checked.show, function (n, v) {
                                    _me.clearQuestionControls(v);
                                    $('tr[id="' + v + '"]').hide().attr('skipped', 'skipped');
                                });
                            }

                            if (typeof (o.checked.hide) != "undefined") {
                                $.each(o.checked.hide, function (n, v) {
                                    $('tr[id="' + v + '"]').show().removeAttr('skipped');
                                });
                            }
                        }
                    }

                    if (mObj.tagName.toLowerCase() == "select") {
                        if (mObj.selectedIndex > 0) {

                            if (typeof (o.checked.show) != "undefined") {
                                $.each(o.checked.show, function (n, v) {
                                    $('tr[id="' + v + '"]').show().removeAttr('skipped');
                                });
                            }

                            if (typeof (o.checked.hide) != "undefined") {
                                $.each(o.checked.hide, function (n, v) {
                                    _me.clearQuestionControls(v);
                                    $('tr[id="' + v + '"]').hide().attr('skipped', 'skipped');
                                });
                            }
                        }
                        else {
                            if (typeof (o.checked.show) != "undefined") {
                                $.each(o.checked.show, function (n, v) {
                                    _me.clearQuestionControls(v);
                                    $('tr[id="' + v + '"]').hide().attr('skipped', 'skipped');
                                });
                            }

                            if (typeof (o.checked.hide) != "undefined") {
                                $.each(o.checked.hide, function (n, v) {
                                    $('tr[id="' + v + '"]').show().removeAttr('skipped');
                                });
                            }
                        }
                    }
                });
            }

            return;
        };

        questions.checkRequiredAnswers = function () {
            var _me = this,
                _allTRs = $('tr[id^="TID3"]'),
                _trs = $('tr[id^="TID3"]').not('[skipped]'),
                _validate = true;

            //clear previous error messages
            $('td', _allTRs).css({
                'background-color': '#fff'
            });
            $('div.err-caption', _allTRs).remove();

            $.each(_trs, function (i, t) {
                var ansCount = 0;

                $('input[type="radio"], input[type="checkbox"]', t).each(function () {
                    if (this.checked) {
                        ansCount = ansCount + 1;
                    }
                });

                $('input[type="text"]', t).each(function () {
                    if (this.value.length > 0) {
                        ansCount = ansCount + 1;
                    }
                });

                $('select', t).each(function () {
                    if (this.selectedIndex > 0) {
                        ansCount = ansCount + 1;
                    }
                });

                //if count of responses < 1 then display error message
                if (ansCount < 1) {

                    _validate = false;

                    $('td:first', t).each(function (z, x) {
                        $(x).prepend('<div class="err-caption"><span style="color:Red;">A response for this question is required!</span></div>');
                        $('td', $(x).parent('tr')).css({
                            'background-color': '#F9FC9D'
                        });
                    });
                }
            });

            return _validate;
        };


        if (typeof (saqli) == "undefined") {
            var saqli = {};
        }

        saqli.gotoStage2 = function () {
            var me = this,
                symptoms = $('input[type="checkbox"]:checked', $('#divStage1')),
                nextSymptoms = $('input[type="checkbox"]', $('#divStage2')),
                selectedSymptoms = [];

            //initially hide checkboxes on stage 2
            $.each(nextSymptoms, function (i, o) {
                var rid = parseInt(o.id.replace(/\D/gi, ''));

                $('input[type="checkbox"][id="rid_' + rid + '"]').each(function () {
                    $(this).removeAttr('maxfive').hide();
                });
                $('label[for="rid_' + rid + '"]').removeAttr('maxfive').hide();

            });

            if (symptoms.length < 1) {
                $('td:first', $('tr[id="TID1_QID1"]')).each(function (z, x) {
                    $(x).prepend('<div class="err-caption"><span style="color:Red;">A response for this question is required!</span></div>');
                    $('td', $(x).parent('tr')).css({
                        'background-color': '#F9FC9D'
                    });
                });

                var _pos = $('div.err-caption').eq(0).position();
                window.scrollTo(0, _pos.top);
                alert('Please review the responses for the highlighted question!');

                return false;
            }

            $('div.err-caption', $('tr[id="TID1_QID1"]')).remove();

            $('td:first', $('tr[id="TID1_QID1"]')).each(function (z, x) {
                $('td', $(x).parent('tr')).css({ 'background-color': '#ffffff' });
            });

            $.each(symptoms, function (i, o) {
                var rid = parseInt(o.id.replace(/\D/gi, '')) + 1000;

                $('input[type="checkbox"][id="rid_' + rid + '"]').each(function () {
                    $(this).attr('maxfive', 'maxfive').show();
                    selectedSymptoms.push(this);
                });
                $('label[for="rid_' + rid + '"]').attr('maxfive', 'maxfive').show();

            });

            //make previouly selected symptoms and now not visible
            $('input[type="checkbox"][maxfive!="maxfive"]', $('#divStage2')).each(function (i, o) {
                o.checked = false;
            });

            //bind maximum_five check
            $.each(selectedSymptoms, function (i, o) {
                $(o).bind({
                    click: function () {
                        me.checkMaxFive();
                    }
                });
            });

            me.checkMaxFive();
            questions.checkSkipPatterns();

            //hide stage 1
            $('#divStage1').hide();

            //show stage 2
            $('#divStage2').show();
        };

        saqli.gotoStage3 = function () {
            var me = this,
                Symptoms = $('input[type="checkbox"][maxfive="maxfive"]', $('#divStage2')),
                selSymptoms = $('input[type="checkbox"][maxfive="maxfive"]:checked', $('#divStage2'));

            $('div.err-caption', $('tr[id="TID2_QID0"]')).remove();

            if (selSymptoms.length < 1) {
                $('td:first', $('tr[id="TID2_QID0"]')).each(function (z, x) {
                    $(x).prepend('<div class="err-caption"><span style="color:Red;">A response for this question is required!</span></div>');
                    $('td', $(x).parent('tr')).css({
                        'background-color': '#F9FC9D'
                    });
                });

                var _pos = $('div.err-caption').eq(0).position();
                window.scrollTo(0, _pos.top);
                alert('Please review the responses!');

                return false;
            }

            if (Symptoms.length > 5 && selSymptoms.length < 5) {
                $('td:first', $('tr[id="TID2_QID0"]')).each(function (z, x) {
                    $(x).prepend('<div class="err-caption"><span style="color:Red;">Please select 5 symptoms!</span></div>');
                    $('td', $(x).parent('tr')).css({
                        'background-color': '#F9FC9D'
                    });
                });

                var _pos = $('div.err-caption').eq(0).position();
                window.scrollTo(0, _pos.top);
                alert('Please review the responses!');

                return false;
            }
            else if (selSymptoms.length < Symptoms.length && Symptoms.length <= 5) {
                $('td:first', $('tr[id="TID2_QID0"]')).each(function (z, x) {
                    $(x).prepend('<div class="err-caption"><span style="color:Red;">If your list only has 5 or fewer symptoms, please select all of them.</span></div>');
                    $('td', $(x).parent('tr')).css({
                        'background-color': '#F9FC9D'
                    });
                });

                var _pos = $('div.err-caption').eq(0).position();
                window.scrollTo(0, _pos.top);
                alert('Please review the responses for the highlighted question!');

                return false;
            }

            var _allTRs = $('tr[id^="TID3"]'),
                _trs = $('tr[id^="TID3"]').not('[skipped]');

            //clear previous error messages
            $('td', _allTRs).css({
                'background-color': '#fff'
            });
            $('div.err-caption', _allTRs).remove();

            questions.checkSkipPatterns();

            //hide stage 2
            $('#divStage2').hide();

            //show stage 3
            $('#divStage3').show();
        },

        saqli.checkMaxFive = function () {
            var me = this,
                Symptoms = $('input[type="checkbox"]', $('#divStage2')),
                selSymptoms = $('input[type="checkbox"]:checked', $('#divStage2'));

            if (selSymptoms.length > 4) {
                //disable remaining unselected symptoms
                $.each(Symptoms, function (i, o) {
                    if (!o.checked) {
                        $(o).attr('disabled', 'disabled');
                    }
                });
            }
            else {
                //re-enable remaining unselected symptoms
                $.each(Symptoms, function (i, o) {
                    if (!o.checked) {
                        $(o).removeAttr('disabled');
                    }
                });
            }

        };

        saqli.goBack = function (nStage) {
            //hide stage n+1
            $('#divStage' + (nStage + 1)).hide();

            //show stage n
            $('#divStage' + nStage).show();

            switch (nStage) {
                case 1:
                    $('div.err-caption', $('tr[id="TID2_QID0"]')).remove();
                    break;

                case 2:
                    var _allTRs = $('tr[id^="TID3"]'),
                        _trs = $('tr[id^="TID3"]').not('[skipped]');

                    //clear previous error messages
                    $('td', _allTRs).css({
                        'background-color': '#fff'
                    });
                    $('div.err-caption', _allTRs).remove();
                    break;
            }
        };
    </script>
</asp:Content>
