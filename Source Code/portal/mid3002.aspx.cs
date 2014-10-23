using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mid3002 : System.Web.UI.Page
{
    protected long lMID = 3002;
    protected long lEncIntakeID;
    protected long lGRP;

    protected void Page_Load(object sender, EventArgs e)
    {
        CEncounter enc = new CEncounter();
        if (!IsPostBack)
        {
            if (String.IsNullOrEmpty(Master.SelectedEncounterID) || String.IsNullOrEmpty(Master.SelectedPatientID))
            {
                Server.Transfer("Default.aspx");
            }

            //check if module is assigned
            if (!String.IsNullOrEmpty(Master.SelectedPatientID))
            {
                if (!enc.IsModuleAssigned(Master, lMID))
                {
                    Server.Transfer("Default.aspx");
                }
            }
        }

        //get module group id
        if (Request.QueryString["grp"] != null)
        {
            lGRP = Convert.ToInt32(Request.QueryString["grp"]);
        }

        lEncIntakeID = enc.GetEncIntakeId(Master, lMID);

        btnSubmit.Attributes.Add("onclick", "return questions.validateResponses();");
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        CEncounter enc = new CEncounter();

        string strAllResponse = String.Empty;
        string strQResponse = String.Empty;
        Int32 nCount = 0;

        String strResponseCount = Request.Form["ResponseCount"];
        if (strResponseCount != null)
        {
            nCount = Convert.ToInt32(strResponseCount);
        }
        else
        {
            //ERROR
            return;
        }

        long nScore = 0;

        //loop thru the responses
        for (int a = 1; a < nCount; a++)
        {
            //get responses controls
            string strRadio = "grpRadio_" + a.ToString();

            //get radio response
            if (Request.Form[strRadio] != null)
            {
                string[] splitResponse = Request.Form[strRadio].Split(new Char[] { '|' });
                strQResponse += enc.GetRecordForInsert(splitResponse);
                nScore += Convert.ToInt64(splitResponse[2]);
            }


            strAllResponse += strQResponse;
            strQResponse = null;
        }


        // Write responses
        if (enc.WriteIntakeResponses(Master, Master.SelectedEncounterID, lEncIntakeID, lMID, lGRP, strAllResponse) == false)
        {
            return;
        }

        long lScoreType = 3002;
        if (Score(lEncIntakeID, lMID, nScore, lScoreType) == false)
        {
            return;
        }

        //mark module complete
        if (enc.CompleteModule(Master, lMID, lGRP))
        {
            Response.Redirect("patient_assessment.aspx");
        }
    }

//-------------------------------------------------------------------------------------
//INDEX = AVERAGE OF Q1, Q3, Q6
//BMI	= WEIGHT / HEIGHT * HEIGHT NOTE: HEIGHT and WEIGHT must be in Kg and Meters)
//AGE	= years
//GENDER = 1 for male, 0 for female
//X = 8.16 + (1.299 * INDEX) + (0.163 * BMI) - (0.028 * INDEX * BMI) + (0.032 * AGE) + (1.278 * GENDER)
//
//MAP SCORE  = EXP(x)  / (1 + EXP(x))
//LRMAP = EXP(x - 0.45)
//------------------------------------------------------------------------------------
    protected bool Score(long lEncIntakeID, long lMID, long nScore, long lScoreType)
    {
        if (lScoreType == -1)
        {
            return true;
        }

        CPatient pat = new CPatient();

        long lGender = pat.GetPatientGender(Master);
        double dGender = Convert.ToDouble(lGender);

        long lAge = pat.GetPatientAge(Master);
        double dAge = Convert.ToDouble(lAge);

        double dHeight = pat.GetPatientHeight(Master);
        double dWeight = pat.GetPatientWeight(Master);

        if ((lAge == 0) || (dHeight == 0) || (dWeight == 0))
        {
            return false;
        }

        double nAvgIndex = nScore / 3;
        double dBMI = dWeight / (dHeight * dHeight);

        double dX = -8.16 + (1.299 * nAvgIndex) + (0.163 * dBMI) - (0.028 * nAvgIndex * dBMI) + (0.032 * dAge) + (1.278 * dGender);

        double dMapScore = Math.Exp(dX) / (1 + Math.Exp(dX));


        //write intake score
        //long lMapScore = Convert.ToInt64(dMapScore);

        // interpretation
        String strInterpret = "";
        if (nScore > 11)
        {
            strInterpret = "excessive daytime sleepiness";
        }


        CIntake intake = new CIntake();
        if (intake.InsertEncIntakeScore(Master, Master.SelectedEncounterID, lEncIntakeID, lMID, lScoreType, dMapScore, 0, strInterpret, 1, lGRP) == false)
        {
            return false;
        }

        return true;
    }

} // END OF CLASS