using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mid3016 : System.Web.UI.Page
{
    protected long lMID = 3016;
    protected long lEncIntakeID;
    protected long lGRP;

    double nScore = 0,
        nScore1 = 0,
        nScore2 = 0,
        nScore3 = 0,
        nScore4 = 0,
        nScore5 = 0;

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

        nScore = 0;
        nScore1 = 0;
        nScore2 = 0;
        nScore3 = 0;
        nScore4 = 0;
        nScore5 = 0;

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

        //loop thru the responses
        for (int a = 1; a < nCount; a++)
        {
            String strResponse = null;
            String strRID = null;
            String strScore = null;

            int iQID = a;

            //get responses controls
            string strRadio = "grpRadio_" + iQID.ToString();

            //get radio response
            if (Request.Form[strRadio] != null)
            {
                string[] splitResponse = Request.Form[strRadio].Split(new Char[] { '|' });
                strQResponse += enc.GetRecordForInsert(splitResponse);

                //comupte sections scores

                //general productivity
                if (iQID == 1 || iQID == 2)
                {
                    nScore1 += Convert.ToInt32(splitResponse[2]);
                }

                //activity level
                if (iQID == 6 || iQID == 8 || iQID == 9)
                {
                    nScore2 += Convert.ToInt32(splitResponse[2]);
                }

                //vigilance
                if (iQID == 3 || iQID == 4 || iQID == 7)
                {
                    nScore3 += Convert.ToInt32(splitResponse[2]);
                }

                //social outcome
                if (iQID == 5)
                {
                    nScore4 += Convert.ToInt32(splitResponse[2]);
                }

                //intimacy and sexual relationships
                if (iQID == 10)
                {
                    nScore5 += Convert.ToInt32(splitResponse[2]);
                }

            }

            strAllResponse += strQResponse;
            strQResponse = null;
        }


        // Write responses
        if (enc.WriteIntakeResponses(Master, Master.SelectedEncounterID, lEncIntakeID, lMID, lGRP, strAllResponse) == false)
        {
            return;
        }

        //scores
        if (WriteScores() == false) 
        {
            return;
        }
        
        //mark module complete
        if (enc.CompleteModule(Master, lMID, lGRP))
        {
            Response.Redirect("patient_assessment.aspx");
        }
    }

    protected bool WriteScores() 
    {
        long lScoreType = 3016;

        nScore = (((nScore1 / 2) + (nScore2 / 3) + (nScore3 / 3) + nScore4 + nScore5) / 5) * 5;

        double[] dScores = new double[6] {
            nScore,
            (nScore1 / 2),
            (nScore2 / 3),
            (nScore3 / 3),
            nScore4,
            nScore5
        };

        string[] strInterpretation = new string[6] { 
            "TOTAL SCORE",
            "GENERAL PRODUCTIVITY",
            "ACTIVITY LEVEL",
            "VIGILANCE",
            "SOCIAL OUTCOME",
            "INTIMACY AND SEXUAL RELATIONSHIPS"
        };


        CIntake intake = new CIntake();
        for (int s = 0; s < dScores.Length; s++)
        {
            bool bSuccess = intake.InsertEncIntakeScore(
                Master,
                Master.SelectedEncounterID,
                lEncIntakeID,
                lMID,
                lScoreType,
                dScores[s],
                0,
                strInterpretation[s],
                s+1,
                lGRP);

            if (!bSuccess)
                return bSuccess;
        }

        return true;
    }

    

} // END OF CLASS