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
using System.Text.RegularExpressions;

public partial class pat_summary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CCPAPResults cpap = new CCPAPResults(Master);
        CDataUtils utils = new CDataUtils();
        CIntake intake = new CIntake();

        if (!IsPostBack)
        {
            rblGraphicsMode.SelectedIndex = (int)Master.GraphicOption;
            rblGraphicsMode_OnSelectedIndexChanged(null, EventArgs.Empty);

            htxtTxAdherence.Value = cpap.GetTxAdherence();
            htxtAHI.Value = cpap.GetAHI();
            htxtBaselineAHI.Value = cpap.GetBaselineAHI();

            string strLeakType;
            htxtMaskLeak.Value = cpap.GetMaskLeak(out strLeakType);
            htxtLeakType.Value = strLeakType;
            htxtQuestionnaires.Value = utils.GetStringValueFromDS(intake.GetScoreDataStringDS(Master, Master.SelectedPatientID), "MID_DATA");

            CPatientEvent evt = new CPatientEvent(Master);
            evt.CheckPAPEvent();
            ClearTxSessionVars();
            loadPatient();

            cpap.LoadQuestionnaireCombo(cboQuestionnaireScores);
        }

        cboSummaryTimeWindow.Attributes.Add("onchange", "patient.summary.timewindow(this);");
        cboSummaryTimeWindow2.Attributes.Add("onchange", "patient.summary.graphs.timewindow(this);");
        cboQuestionnaireScores.Attributes.Add("onchange", "patient.summary.renderQuestionnaires(this);");

        ucPatEvt.BaseMstr = Master;

        //move to events tab if this is an event lookup
        if (!IsPostBack) 
        {
            if (Session["EVENT_LOOKUP"] != null)
            {
                if ((bool)Session["EVENT_LOOKUP"])
                {
                    tcPatSummary.ActiveTabIndex = 1;
                    Session["EVENT_LOOKUP"] = null;
                } 
            }
        }

    }

    protected bool DefultEncounterSelect(string strPatientID, long lTreatmentID) 
    {
        bool bTreatClosed = false;
        CEncounter enc = new CEncounter();
        CTreatment treatment = new CTreatment();
        DataSet dsTreatment = treatment.GetTreatmentListDS(Master, Master.SelectedPatientID);
        DataSet dsEncs = enc.GetAllEncounterListDS(Master, strPatientID);

        long lTreatmentCount = dsTreatment.Tables[0].Rows.Count;
        DataRow[] drClosedCases = dsTreatment.Tables[0].Select("case_closed = 1");
        long lTreatClosed = drClosedCases.GetLength(0);
        bool bAllowClosedCases = (lTreatmentCount == lTreatClosed) ? true : false;

        foreach (DataTable tdt in dsTreatment.Tables) 
        {
            foreach (DataRow tdr in tdt.Rows) 
            {
                if (!tdr.IsNull("case_closed")) 
                { 
                    bTreatClosed = (Convert.ToInt32(tdr["case_closed"]) == 1) ? true : false;
                }

                if (!bTreatClosed || (bTreatClosed && bAllowClosedCases))
                {   
                    bool bCaseClosed = false;

                    DataRow[] drEncs = dsEncs.Tables[0].Select("treatment_id = " + Convert.ToInt32(tdr["treatment_id"]));

                    long lEncCount = drEncs.GetLength(0);
                    long lClosedEnc = 0;
                    foreach (DataRow drEncDr in drEncs) 
                    {
                        if (!drEncDr.IsNull("closed")) 
                        {
                            if (Convert.ToInt32(drEncDr["closed"]) == 1) 
                            {
                                ++lClosedEnc;
                            }
                        }
                    }
                    bool bAllowClosedEncs = (lEncCount == lClosedEnc) ? true : false;
                    
                    if (dsEncs != null)
                    {
                        foreach (DataTable dt in dsEncs.Tables)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (!dr.IsNull("closed"))
                                {
                                    bCaseClosed = (Convert.ToInt32(dr["closed"]) == 1) ? true : false;
                                }
                                if (!dr.IsNull("encounter_type_id"))
                                {
                                    long lEncounterType = Convert.ToInt32(dr["encounter_type_id"]);
                                    if ((lEncounterType != (long)EncounterType.ADMIN_NOTE && lEncounterType != (long)EncounterType.GROUP_NOTE) && (!bCaseClosed || (bCaseClosed && bAllowClosedEncs)))
                                    {
                                        Master.SelectedEncounterID = dr["encounter_id"].ToString();
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    protected void ClearTxSessionVars() 
    {
        //------------------
        Session["PROBLEMS_LIST_DS"] = null;
    }

    protected void loadPatient()
    {

        CPatient pat = new CPatient();
        CDataUtils utils = new CDataUtils();

        if (Session["PAT_DEMOGRAPHICS_DS"] == null)
        {
            Session["PAT_DEMOGRAPHICS_DS"] = pat.GetPatientDemographicsDS(Master);
        }

        DataSet clientDemographics = (DataSet)Session["PAT_DEMOGRAPHICS_DS"];

        //-- getLastUpdated
        Master.getLastUpdated(clientDemographics);

        CDropDownList DropDownList = new CDropDownList();
        CMilitaryRender MilitaryRender = new CMilitaryRender();
        CDemographics Demographics = new CDemographics();

        pat.IncPatIntakeAssessments(Master, Master.SelectedPatientID);

        //load all of the user's available fields
        if (clientDemographics != null)
        {
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
    }

    protected void rblGraphicsMode_OnSelectedIndexChanged(object sender, EventArgs e) 
    {
        CUserAdmin usr = new CUserAdmin();
        usr.UpdateGraphPref(Master, rblGraphicsMode.SelectedIndex);
        mvGraphicHub.ActiveViewIndex = rblGraphicsMode.SelectedIndex;
    }
}
