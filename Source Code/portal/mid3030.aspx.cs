using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mid3030 : System.Web.UI.Page
{
    protected long lMID = 3030;
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

        long lScoreType = -1;
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

    //------------------------------------------------------------------------------------
    protected bool Score(long lEncIntakeID, long lMID, long nScore, long lScoreType)
    {
        if (lScoreType == -1)
        {
            return true;
        }

        // interpretation
        String strInterpret = null;
        strInterpret = "";


        //write intake score
        long lMapScore = nScore;

        CIntake intake = new CIntake();
        if (intake.InsertEncIntakeScore(Master, Master.SelectedEncounterID, lEncIntakeID, lMID, lScoreType, lMapScore, 0, strInterpret, 1, lGRP) == false)
        {
            return false;
        }

        return true;
    }

} // END OF CLASS