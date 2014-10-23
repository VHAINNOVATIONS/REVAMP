using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;


public partial class mid3014 : System.Web.UI.Page
{
    protected long lMID = 3014;
    protected long lEncIntakeID;
    protected long lGRP;

    //PSQI variables -------------------------------------
    long PSQIDURAT = 0;
    long PSQIDISTB = 0;
    long PSQILATEN = 0;
    long PSQIDAYDYS = 0;
    long PSQIHSE = 0;
    long PSQISLPQUAL = 0;
    long PSQIMEDS = 0;
    long PSQI = 0;
    long lQ2New = 0;
    float fQ1 = 0;
    long lQ2 = 0;
    float fQ3 = 0;
    long lQ4 = 0;
    long lQ5 = 0;
    long lQ5a = 0;
    long lQ5j = 0;
    long lQ8 = 0;
    long lQ9 = 0;
    float fDiffhour = 0;
    float fnewtib = 0;
    float ftmphse = 0;

    //PSQI Score----------------------------------------------------
    long lScore = 0;
    string strInterpretation = String.Empty;
    //----------------------------------------------------

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

        string strAllResponse = String.Empty,
            strQResponse = String.Empty,
            strResponseCount = Request.Form["ResponseCount"];

        Int32 nCount = 0;

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
            int iQID = a;
            string strCurrResponse = String.Empty;

            //get responses controls
            string strRadio = "grpRadio_" + iQID.ToString(),
                   strCheckbox = "grpCheck_" + iQID.ToString(),
                   strText = "grpCtrlText_" + iQID.ToString(),
                   strCombo = "grpCombo_" + iQID.ToString(),
                   strHiddenText = "grpHidden_" + iQID.ToString();

            //get radio response
            if (Request.Form[strRadio] != null)
            {
                strCurrResponse = Request.Form[strRadio];
                string[] splitResponse = strCurrResponse.Split(new Char[] { '|' });
                strQResponse += enc.GetRecordForInsert(splitResponse);
            }

            //get checkbox response
            if (Request.Form[strCheckbox] != null)
            {
                strCurrResponse = Request.Form[strCheckbox];
                string[] splitResponse = strCurrResponse.Split(new Char[] { '|' });
                strQResponse += enc.GetRecordForInsert(splitResponse);
            }

            //get textbox response
            String strT = null;
            strT = Request.Form[strText];
            if (strT != null)
            {
                if (strT.Trim().Length > 0)
                {
                    strT = strT.Trim();

                    strCurrResponse = strT;

                    String strH = null;
                    strH = Request.Form[strHiddenText];

                    if (String.IsNullOrEmpty(strH) == false)
                    {
                        strCurrResponse += " " + Request.Form[strHiddenText];
                        string[] strHidden = Request.Form[strHiddenText].Split(new Char[] { '|' });
                        strQResponse += enc.GetRecordForInsert(strHidden, strT);
                    }
                }
            }

            //get combo response
            if (Request.Form[strCombo] != null)
            {
                strCurrResponse = Request.Form[strCombo];
                string[] splitResponse = strCurrResponse.Split(new Char[] { '|' });
                strQResponse += enc.GetRecordForInsert(splitResponse);
            }


            object objResponse = new { rIndex = iQID, rString = strCurrResponse };

            //start computing the individual variables
            QResponse resp = new QResponse(iQID, strCurrResponse);
            SetPSQIVariables(resp);

            strAllResponse += strQResponse;
            strQResponse = null;

        }


        if (!enc.WriteIntakeResponses(Master, Master.SelectedEncounterID, lEncIntakeID, lMID, lGRP, strAllResponse))
        {
            return;
        }

        if (!Score(lEncIntakeID, lMID))
        {
            return;
        }

        //mark module complete
        if (enc.CompleteModule(Master, lMID, lGRP))
        {
            Response.Redirect("patient_assessment.aspx");
        }

    }

    protected bool Score(long lEncIntakeID, long lMID) 
    {
        CIntake intake = new CIntake();
        bool bSuccess = intake.InsertEncIntakeScore(
                                                    Master,
                                                    Master.SelectedEncounterID, 
                                                    lEncIntakeID, 
                                                    lMID, 
                                                    lMID, 
                                                    lScore, 
                                                    0, 
                                                    strInterpretation,
                                                    1,
                                                    lGRP);

        return bSuccess;
    }

    //compute the individual variables
    protected void SetPSQIVariables(QResponse resp)
    {
        if (resp != null)
        {
            //*********************************************
            //  calculate fQ1
            //*********************************************

            if (resp.rIndex == 1)
            {
                string strValue = Regex.Replace(resp.rResponse.Split('|')[0], "[^0-9:]", String.Empty, RegexOptions.IgnoreCase);
                string[] strTime = strValue.Split(':');
                string strHour = strTime[0];
                string strMinutes = strTime[1];
                double dSeconds = (24 * 60 * 60) - (Convert.ToInt32(strHour) * 3600) + (Convert.ToInt32(strMinutes) * 60);
                fQ1 = (float)dSeconds;
            }

            //******************************************************
            //  calculate value for Q2New 
            //******************************************************

            if (resp.rIndex == 2)
            {
                long lValue = -1;
                string strValue = Regex.Replace(resp.rResponse.Split('|')[2], "[^0-9.]", String.Empty, RegexOptions.IgnoreCase);

                if (long.TryParse(strValue, out lValue) && lValue != -1)
                {
                    if (lValue >= 0 && lValue <= 15) lQ2New = 0;
                    if (lValue > 15 && lValue <= 30) lQ2New = 1;
                    if (lValue > 30 && lValue <= 60) lQ2New = 2;
                    if (lValue > 60) lQ2New = 3;
                }
            }

            //*********************************************
            //  calculate fQ3, Diffhour, newtib
            //*********************************************

            if (resp.rIndex == 3)
            {
                string strValue = Regex.Replace(resp.rResponse.Split('|')[0], "[^0-9:]", String.Empty, RegexOptions.IgnoreCase);
                string[] strTime = strValue.Split(':');
                string strHour = strTime[0];
                string strMinutes = strTime[1];
                double dSeconds = (Convert.ToInt32(strHour) * 3600) + (Convert.ToInt32(strMinutes) * 60);
                fQ3 = (float)dSeconds;

                // Calculate Diffhour
                fDiffhour = Math.Abs((fQ1 + fQ3) / 3600);
                if (fDiffhour > 24) fnewtib = fDiffhour - 24;
                if (fDiffhour <= 24) fnewtib = fDiffhour;
            }

            //*****************************************************
            //  calculate value for PSQIDURAT, PSQIHSE & tmphse
            //*****************************************************

            if (resp.rIndex == 4)
            {
                long lValue = -1;
                string strValue = Regex.Replace(resp.rResponse.Split('|')[0], "[^0-9.]", String.Empty, RegexOptions.IgnoreCase);

                if (long.TryParse(strValue, out lValue) && lValue != -1)
                {
                    if (lValue >= 7) PSQIDURAT = 0;
                    if (lValue < 7 && lValue >= 6) PSQIDURAT = 1;
                    if (lValue < 6 && lValue >= 5) PSQIDURAT = 2;
                    if (lValue < 5) PSQIDURAT = 3;

                    lQ4 = lValue;

                    //calculate tmphse
                    ftmphse = (lQ4 / fnewtib) * 100;

                    //calculate PSQIHSE
                    if (ftmphse >= 85) PSQIHSE = 0;
                    if (ftmphse < 85 && ftmphse >= 75) PSQIHSE = 1;
                }
            }

            //******************************************************
            //  calculate value for Q5 
            //  (PSQIDISTB needs to be calculated when iQID >= 14)
            //  when we have the SUM of lQ5 and lQ5j
            //******************************************************

            if (resp.rIndex >= 5 && resp.rIndex <= 13)
            {
                long lValue = -1;
                string strValue = Regex.Replace(resp.rResponse.Split('|')[2], "[^0-9.]", String.Empty, RegexOptions.IgnoreCase);

                if (long.TryParse(strValue, out lValue) && lValue != -1)
                {
                    lQ5 += lValue;

                    if (resp.rIndex == 5)
                    {
                        lQ5a = lValue;

                        //******************************************************
                        //  calculate value for PSQILATEN 
                        //******************************************************

                        // set PSQILATEN based on previous calculation
                        if (lQ5a + lQ2New == 0) PSQILATEN = 0;
                        if (lQ5a + lQ2New >= 1 && lQ5a + lQ2New <= 2) PSQILATEN = 1;
                        if (lQ5a + lQ2New >= 3 && lQ5a + lQ2New <= 4) PSQILATEN = 2;
                        if (lQ5a + lQ2New >= 5 && lQ5a + lQ2New <= 6) PSQILATEN = 3;
                    }
                }
            }

            if (resp.rIndex == 14)
            {
                if (resp.rResponse.Split('|').Length > 2)
                {
                    long lValue = -1;
                    string strValue = Regex.Replace(resp.rResponse.Split('|')[2], "[^0-9.]", String.Empty, RegexOptions.IgnoreCase);

                    if (long.TryParse(strValue, out lValue) && lValue != -1)
                    {
                        lQ5j += lValue;
                    }
                }

                //calculate PSQIDISTB
                if (lQ5 + lQ5j == 0) PSQIDISTB = 0;
                if (lQ5 + lQ5j >= 1 && lQ5 + lQ5j <= 9) PSQIDISTB = 1;
                if (lQ5 + lQ5j > 9 && lQ5 + lQ5j <= 18) PSQIDISTB = 2;
                if (lQ5 + lQ5j > 18) PSQIDISTB = 3;
            }

            //******************************************************
            //  calculate PSQISLPQUAL
            //******************************************************

            if (resp.rIndex == 15)
            {
                long lValue = -1;
                string strValue = Regex.Replace(resp.rResponse.Split('|')[2], "[^0-9.]", String.Empty, RegexOptions.IgnoreCase);

                if (long.TryParse(strValue, out lValue) && lValue != -1)
                {
                    PSQISLPQUAL = lValue;
                }
            }

            //******************************************************
            //  calculate PSQIMEDS
            //******************************************************

            if (resp.rIndex == 16)
            {
                long lValue = -1;
                string strValue = Regex.Replace(resp.rResponse.Split('|')[2], "[^0-9.]", String.Empty, RegexOptions.IgnoreCase);

                if (long.TryParse(strValue, out lValue) && lValue != -1)
                {
                    PSQIMEDS = lValue;
                }
            }


            //******************************************************
            //  get value for Q8, Q9 -> PSQIDAYDYS
            //******************************************************

            if (resp.rIndex == 17)
            {
                long lValue = -1;
                string strValue = Regex.Replace(resp.rResponse.Split('|')[2], "[^0-9.]", String.Empty, RegexOptions.IgnoreCase);

                if (long.TryParse(strValue, out lValue) && lValue != -1)
                {
                    lQ8 = lValue;
                }
            }

            if (resp.rIndex == 18)
            {
                long lValue = -1;
                string strValue = Regex.Replace(resp.rResponse.Split('|')[2], "[^0-9.]", String.Empty, RegexOptions.IgnoreCase);

                if (long.TryParse(strValue, out lValue) && lValue != -1)
                {
                    lQ9 = lValue;
                }

                // calculate PSQIDAYDYS
                if (lQ8 + lQ9 == 0) PSQIDAYDYS = 0;
                if (lQ8 + lQ9 >= 1 && lQ8 + lQ9 <= 2) PSQIDAYDYS = 1;
                if (lQ8 + lQ9 >= 3 && lQ8 + lQ9 <= 4) PSQIDAYDYS = 2;
                if (lQ8 + lQ9 >= 5 && lQ8 + lQ9 <= 6) PSQIDAYDYS = 3;
            }

            //******************************************************
            //  Calculate the total PSQI
            //******************************************************

            if (resp.rIndex > 18)
            {
                PSQI = PSQIDURAT + PSQIDISTB + PSQILATEN + PSQIDAYDYS + PSQIHSE + PSQISLPQUAL + PSQIMEDS;
                lScore = PSQI;

                if (PSQI <= 5) strInterpretation = "Associated with good sleep quality";
                if (PSQI > 5) strInterpretation = "Associated with poor sleep quality";
            }

        }
    }

    //------------------------------------------------------------------------------------
    
} // END OF CLASS

public class QResponse
{
    public QResponse(int iQindex, string strResponse)
    {
        rIndex = iQindex;
        rResponse = strResponse;
    }
    public int rIndex { set; get; }
    public string rResponse { set; get; }
}