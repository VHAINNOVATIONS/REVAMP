using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mid3020 : System.Web.UI.Page
{
    protected const long lMID = 3020;
    protected const int nQuestionCount = 12;
    protected char[] cSplitChars = new char[1] { '|' };
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

        for (int a = 1; a <= nQuestionCount; a++)
        {
            // build name attribute string to retrieve selected responses
            string strRadio = "grpRadio_" + a.ToString();

            if (Request.Form[strRadio] != null)
            {
                string[] splitResponse = Request.Form[strRadio].Split(cSplitChars);
                strQResponse += enc.GetRecordForInsert(splitResponse);
            }

            strAllResponse += strQResponse;
            strQResponse = null;
        }

        if (!enc.WriteIntakeResponses(Master, Master.SelectedEncounterID, lEncIntakeID, lMID, lGRP, strAllResponse))
        {
            return;
        }

        if (!Score(lEncIntakeID, lMID, 3020))
        {
            return;
        }

        //mark module complete
        if (enc.CompleteModule(Master, lMID, lGRP))
        {
            Response.Redirect("patient_assessment.aspx");
        }
    }

    // scores the assessment based on the responses selected
    // and stores the scores in the database
    protected bool Score(long lEncIntakeID, long lMID, long lScoreType)
    {
        // get selected response values
        double dSF1 = 0.0;
        switch (Convert.ToInt64(Request.Form["grpRadio_1"].Split(cSplitChars)[2]))
        {
            case 1:
                dSF1 = 5;
                break;
            case 2:
                dSF1 = 4.4;
                break;
            case 3:
                dSF1 = 3.4;
                break;
            case 4:
                dSF1 = 2;
                break;
            case 5:
                dSF1 = 1;
                break;
        };
        long lSF2a = Convert.ToInt64(Request.Form["grpRadio_2"].Split(cSplitChars)[2]);
        long lSF2b = Convert.ToInt64(Request.Form["grpRadio_3"].Split(cSplitChars)[2]);
        long lSF3a = Convert.ToInt64(Request.Form["grpRadio_4"].Split(cSplitChars)[2]);
        long lSF3b = Convert.ToInt64(Request.Form["grpRadio_5"].Split(cSplitChars)[2]);
        long lSF4a = Convert.ToInt64(Request.Form["grpRadio_6"].Split(cSplitChars)[2]);
        long lSF4b = Convert.ToInt64(Request.Form["grpRadio_7"].Split(cSplitChars)[2]);
        long lSF5 = 6 - Convert.ToInt64(Request.Form["grpRadio_8"].Split(cSplitChars)[2]);
        long lSF6a = 6 - Convert.ToInt64(Request.Form["grpRadio_9"].Split(cSplitChars)[2]);
        long lSF6b = 6 - Convert.ToInt64(Request.Form["grpRadio_10"].Split(cSplitChars)[2]);
        long lSF6c = Convert.ToInt64(Request.Form["grpRadio_11"].Split(cSplitChars)[2]);
        long lSF7 = Convert.ToInt64(Request.Form["grpRadio_12"].Split(cSplitChars)[2]);

        // create scales
        double dPF = lSF2a + lSF2b;
        double dRP = lSF3a + lSF3b;
        double dBP = lSF5;
        double dGH = dSF1;
        double dVT = lSF6b;
        double dSF = lSF7;
        double dRE = lSF4a + lSF4b;
        double dMH = lSF6a + lSF6c;

        dPF = (dPF - 2) / 4 * 100;
        dRP = (dRP - 2) / 8 * 100;
        dBP = (dBP - 1) / 4 * 100;
        dGH = (dGH - 1) / 4 * 100;
        dVT = (dVT - 1) / 4 * 100;
        dSF = (dSF - 1) / 4 * 100;
        dRE = (dRE - 2) / 8 * 100;
        dMH = (dMH - 2) / 8 * 100;

        // transform to z-scores
        double dPF_Z = (dPF - 81.18122) / 29.10588;
        double dRP_Z = (dRP - 80.52856) / 27.13526;
        double dBP_Z = (dBP - 81.74015) / 24.53019;
        double dGH_Z = (dGH - 72.19795) / 23.19041;
        double dVT_Z = (dVT - 55.59090) / 24.84380;
        double dSF_Z = (dSF - 83.73973) / 24.75775;
        double dRE_Z = (dRE - 86.41051) / 22.35543;
        double dMH_Z = (dMH - 70.18217) / 20.50597;

        // create physical and mental health composite scores
        double dAggPhys = (dPF_Z * 0.42402)
            + (dRP_Z * 0.35119)
            + (dBP_Z * 0.31754)
            + (dGH_Z * 0.24954)
            + (dVT_Z * 0.02877)
            + (dSF_Z * -.00753)
            + (dRE_Z * -.19206)
            + (dMH_Z * -.22069);
        double dAggMent = (dPF_Z * -.22999)
            + (dRP_Z * -.12329)
            + (dBP_Z * -.09731)
            + (dGH_Z * -.01571)
            + (dVT_Z * 0.23534)
            + (dSF_Z * 0.26876)
            + (dRE_Z * 0.43407)
            + (dMH_Z * 0.48581);

        // transform composite and scale scores to t-scores
        double dAggPhys_T = 50 + (dAggPhys * 10);
        double dAggMent_T = 50 + (dAggMent * 10);
        double dPF_T = 50 + (dPF_Z * 10);
        double dRP_T = 50 + (dRP_Z * 10);
        double dBP_T = 50 + (dBP_Z * 10);
        double dGH_T = 50 + (dGH_Z * 10);
        double dVT_T = 50 + (dVT_Z * 10);
        double dSF_T = 50 + (dSF_Z * 10);
        double dRE_T = 50 + (dRE_Z * 10);
        double dMH_T = 50 + (dMH_Z * 10);

        // move scores and descriptions to an array for easy insertion
        int[] dEncIntakeScores = new int[10] {
            Convert.ToInt32(Math.Round(dAggPhys_T)),
            Convert.ToInt32(Math.Round(dAggMent_T)),
            Convert.ToInt32(Math.Round(dPF_T)),
            Convert.ToInt32(Math.Round(dRP_T)),
            Convert.ToInt32(Math.Round(dBP_T)),
            Convert.ToInt32(Math.Round(dGH_T)),
            Convert.ToInt32(Math.Round(dVT_T)),
            Convert.ToInt32(Math.Round(dSF_T)),
            Convert.ToInt32(Math.Round(dRE_T)),
            Convert.ToInt32(Math.Round(dMH_T))};
        string[] dEncIntakeInperpretation = new string[10] {
            "NEMC PHYSICAL HEALTH T-SCORE - SF12",
            "NEMC MENTAL HEALTH T-SCORE - SF12",
            "NEMC PHYSICAL FUNCTIONING T-SCORE",
            "NEMC ROLE LIMITATION PHYSICAL T-SCORE",
            "NEMC PAIN T-SCORE",
            "NEMC GENERAL HEALTH T-SCORE",
            "NEMC VITALITY T-SCORE",
            "NEMC ROLE LIMITATION EMOTIONAL T-SCORE",
            "NEMC SOCIAL FUNCTIONING T-SCORE",
            "NEMC MENTAL HEALTH T-SCORE"};

        CIntake intake = new CIntake();
        for (int nIndex = 0; nIndex < dEncIntakeScores.Length; nIndex++)
        {
            bool bSuccess = intake.InsertEncIntakeScore(
                Master,
                Master.SelectedEncounterID,
                lEncIntakeID,
                lMID,
                lScoreType,
                dEncIntakeScores[nIndex],
                0,
                dEncIntakeInperpretation[nIndex],
                nIndex+1,
                lGRP);

            if (!bSuccess)
                return bSuccess;
        }

        return true;
    }

} // END OF CLASS