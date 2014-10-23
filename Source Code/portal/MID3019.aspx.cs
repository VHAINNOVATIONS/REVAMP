using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mid3019 : System.Web.UI.Page
{
    protected long lMID = 3019;
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

        btnSubmit.Attributes.Add("onclick", "return saqli.validateStage4();");

        //load previous responses
        GetPrevResponses();
    }

    protected void GetPrevResponses() 
    {
        CDataUtils utils = new CDataUtils();
        CIntake intake = new CIntake();
        
        DataSet ds = intake.GetSymptomsFUResponsesDS(Master);

        if (ds != null)
        {
            if(ds.Tables[0].Rows.Count > 0)
            {
                htxtResponses.Value = utils.GetJSONString(ds);
            }
            else
            {
                mvSymptoms.SetActiveView(vwMessage);
            }
        }
        else
        {
            mvSymptoms.SetActiveView(vwMessage);
        }
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
            string strCombo = "grpCombo_" + iQID.ToString();
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

            //get combo response
            if (Request.Form[strCombo] != null)
            {
                if (Request.Form[strCombo].ToString().Trim() != "")
                {
                    string[] splitResponse = Request.Form[strCombo].Split(new Char[] { '|' });
                    strQResponse += enc.GetRecordForInsert(splitResponse);
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
}