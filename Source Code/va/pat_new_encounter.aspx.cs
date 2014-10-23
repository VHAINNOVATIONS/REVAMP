using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class pat_new_encounter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //1. check to see if a patient is looked up
        if (Master.SelectedPatientID.Length < 1)
        {
            //should never be in this state but just in case...
            divStatus.InnerHtml = "<font size='+0'>";
            divStatus.InnerHtml += "Please lookup a patient before creating a new encounter!";
            divStatus.InnerHtml += "</font>";
            return;
        }

        //requires the treatment id be passed in on the query string
        //requires the encounter type be passsed in on the query string
        if (Request.QueryString.Keys.Count < 2)
        {
            divStatus.InnerHtml = "<font size='+0'>";
            divStatus.InnerHtml += "failed to create a new encounter!";
            divStatus.InnerHtml += "</font>";
            return;
        }

        //requires the treatment id be passed in on the query string
        Master.SelectedTreatmentID = Convert.ToInt32(Request.QueryString[0].ToString());

        //requires the encounter type be passsed in on the query string
        long lEncounterType = Convert.ToInt32(Request.QueryString[1].ToString());

        //create a new "other" encounter (this is all normal encs other than the initial one)
        string strNewEnc = "";
        CEncounter enc = new CEncounter();

        //Encounter Types
        //create an encounter - INITIAL_PHONE_CALL = 0, ONE_WEEK_FU = 1, ONE_MONTH_FU = 2, AFTER_1_MONTH_FU = 3,
        //THREE_MONTHS_FU = 4, AFTER_THREE_MONTHS_FU = 5, PHONE_CALL_FU = 6, SELF_MANAGEMENT = 99

        if ((lEncounterType == (long)ReVamp_EncounterType.INITIAL_EVALUATION) ||
            (lEncounterType == (long)ReVamp_EncounterType.INITIAL_PHONE_CALL)   ||
           (lEncounterType == (long)ReVamp_EncounterType.ONE_WEEK_FU)           ||
           (lEncounterType == (long)ReVamp_EncounterType.ONE_MONTH_FU)          ||
           (lEncounterType == (long)ReVamp_EncounterType.AFTER_1_MONTH_FU)      ||
           (lEncounterType == (long)ReVamp_EncounterType.THREE_MONTHS_FU)       ||
           (lEncounterType == (long)ReVamp_EncounterType.AFTER_THREE_MONTHS_FU) ||
           (lEncounterType == (long)ReVamp_EncounterType.PHONE_CALL_FU) ||
           (lEncounterType == (long)ReVamp_EncounterType.SELF_MANAGEMENT))
        {      
            if (enc.CreateEncounter(Master,
                                    Master.SelectedPatientID,
                                    Master.SelectedTreatmentID,
                                    lEncounterType,
                                    out strNewEnc))
            {
                //set the selected encounter to the new encounter
                Master.SelectedEncounterID = strNewEnc;

                string strURL = "";
                strURL += "pat_encounter.aspx?op0=";
                strURL += Master.SelectedPatientID;
                strURL += "&op1=";
                strURL += strNewEnc;
                strURL += "&op2=";
                strURL += Convert.ToString(Master.SelectedTreatmentID);

                Response.Redirect(strURL);
                return;
            }
            else
            {
                divStatus.InnerHtml = "<font size='+0'>";
                divStatus.InnerHtml += Master.StatusComment;
                divStatus.InnerHtml += "</font>";
                return;
            }
        }
    }
}
