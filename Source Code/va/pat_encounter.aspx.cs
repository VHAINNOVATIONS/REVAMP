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
using DataAccess;

public partial class pat_encounter : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //get mastersave control
        Button btnMasterSave = (Button)Master.FindControl("btnMasterSave");
        AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
        trigger.ControlID = btnMasterSave.ID;
        upWrapperUpdatePanel.Triggers.Add(trigger);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        #region notpostback
        if (!IsPostBack)
        {
            //get values from the query string for this encounter
            Master.SelectedPatientID = Request.QueryString[0].ToString();
            Master.SelectedEncounterID = Request.QueryString[1].ToString();
            Master.SelectedTreatmentID = Convert.ToInt32(Request.QueryString[2].ToString());

            //------------------------------------------------------------------------------
            //GET INITIAL VISIT ID
            //GET PATIENT NAME FOR THE DEMOGRAPHICS BLURB
            if (Master.IsLoggedIn() && !String.IsNullOrEmpty(Master.SelectedPatientID))//must be logged in too... 
            {
                if (Session["InitialVisit"] == null)
                {
                    CEncounter patInitVisit = new CEncounter();
                    CDataUtils dUtils = new CDataUtils();
                    DataSet dsInitVisit = patInitVisit.GetInitialVisitDS(Master, Master.SelectedPatientID, Master.SelectedTreatmentID);
                    Session["InitialVisit"] = dUtils.GetStringValueFromDS(dsInitVisit, "encounter_id");
                }

                if (Session["PATIENTNAME"] == null)
                {
                    CPatient cpat = new CPatient();
                    Session["PATIENTNAME"] = cpat.GetPatientName(Master);
                }

                //GET SELECTED PATIENT'S DEMOGRAPHICS
                CPatient pat = new CPatient();
                CDataUtils utils = new CDataUtils();
                DataSet clientDemographics = new DataSet();

                Session["PAT_DEMOGRAPHICS_DS"] = pat.GetPatientDemographicsDS(Master);
                clientDemographics = (DataSet)Session["PAT_DEMOGRAPHICS_DS"];

                foreach (DataTable patTable in clientDemographics.Tables)
                {
                    foreach (DataRow patRow in patTable.Rows)
                    {
                        Master.APPMaster.PatientHasOpenCase = false;
                        if (!patRow.IsNull("OPENCASE_COUNT"))
                        {
                            if (Convert.ToInt32(patRow["OPENCASE_COUNT"]) > 0)
                            {
                                Master.APPMaster.PatientHasOpenCase = true;
                            }
                        }
                    }
                }
            }

            CEncounter enc = new CEncounter();
            
            //Reset all Sessions valriables related to the Encounter
            Session["ENCOUNTERDS"] = null;
            Session["ENCOUNTERS_LIST_DS"] = null;
            Session["INTAKESCOREDS"] = null;
            Session["INTAKE_SCORES_DS"] = null;

            //------------------
            Session["PROBLEMS_LIST_DS"] = null;
        }
        #endregion

        ucPatSOAPP.BaseMstr = Master;

        if (!IsPostBack) 
        { 
            //todo: a readonly mode if already signed, waiting on jeff
            //init the soap with the values passed in       
            
            ucPatSOAPP.Initialize( Master.SelectedPatientID,
                                   Master.SelectedTreatmentID,
                                   Master.SelectedEncounterID,
                                   "",
                                   "",
                                   "",
                                   "");
        }

    }
}
