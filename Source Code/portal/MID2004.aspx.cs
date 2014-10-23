using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mid2004 : System.Web.UI.Page
{
    protected long lMID = 2004;
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
            string strCheckbox = "grpCheck_" + iQID.ToString();
            string strText = "grpCtrlText_" + iQID.ToString();
            string strHiddenText = "grpHidden_" + iQID.ToString();

            //get radio response
            if (Request.Form[strRadio] != null)
            {
                string[] splitResponse = Request.Form[strRadio].Split(new Char[] { '|' });
                strQResponse += enc.GetRecordForInsert(splitResponse);
            }

            //get checkbox response
            if (Request.Form[strCheckbox] != null)
            {
                string[] splitResponse = Request.Form[strCheckbox].Split(new Char[] { '|' });
                strQResponse += enc.GetRecordForInsert(splitResponse);
            }

            //get textbox response
            String strT = null;
            strT = Request.Form[strText];
            if (strT != null)
            {
                if (strT.Trim().Length > 0)
                {
                    String strH = null;
                    strH = Request.Form[strHiddenText];

                    if (String.IsNullOrEmpty(strH) == false)
                    {
                        string[] strHidden = Request.Form[strHiddenText].Split(new Char[] { '|' });
                        strQResponse += enc.GetRecordForInsert(strHidden, strT);
                    }
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

        //mark module complete
        if (enc.CompleteModule(Master, lMID, lGRP))
        {
            Response.Redirect("patient_assessment.aspx");
        }
    }


} // END OF CLASS