using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Ext.Net;

/// <summary>
/// Summary description for CVerticalMenu
/// </summary>
public class CVerticalMenu
{
    const long cnNoteClosed = 1;

    private BaseMaster m_BaseMstr;
    private DataSet m_dsTreatments;
    private DataSet m_dsEncounters;
    private DataSet m_dsAssessments;
    private DataSet m_dsEncounterTypes;
    private CSec usrsec;
    private CEncounter enctype;

    public CVerticalMenu(BaseMaster BaseMstr, DataSet dsTreatments, DataSet dsEncounters, DataSet dsIntakes)
	{
        m_BaseMstr = BaseMstr;
        m_dsTreatments = dsTreatments;
        m_dsEncounters = dsEncounters;
        m_dsAssessments = dsIntakes;
        usrsec = new CSec();
        enctype = new CEncounter();
        m_dsEncounterTypes = enctype.GetAllEncounterTypesDS(BaseMstr);
	}

    //Render Patient's Node
    protected void RenderPatient(BaseMaster BaseMstr, Ext.Net.TreeNode tn, long lTreatmentID) 
    {
        Ext.Net.TreeNode patient = new Ext.Net.TreeNode("Patient");

        Ext.Net.TreeNode tnPatDemographics = new Ext.Net.TreeNode("Demographics");
        tnPatDemographics.Href = "pat_demographics.aspx";
        
        Ext.Net.TreeNode tnPatSummary = new Ext.Net.TreeNode("Summary");
        tnPatSummary.Href = "pat_summary.aspx";

        //Ext.Net.TreeNode tnMedicalHistory = new Ext.Net.TreeNode("Medical History");
        //tnMedicalHistory.Href = "pat_medical_hx.aspx";

        patient.Expanded = true;

        if(BaseMstr.APPMaster.HasUserRight((long)SUATUserRight.ProcessNewPatientsUR))
        {
            patient.Nodes.Add(tnPatDemographics);
            patient.Nodes.Add(tnPatSummary);
            //patient.Nodes.Add(tnMedicalHistory);
        }

        tn.Nodes.Add(patient);
    }

    protected void RenderPatientAssessments(BaseMaster BaseMstr, Ext.Net.TreeNode tn, long lTreatmentID)
    {
        Ext.Net.TreeNode assessments = new Ext.Net.TreeNode("Assessments");
        assessments.Expanded = true;

        tn.Nodes.Add(assessments);
    }

    protected void RenderCPAP(Ext.Net.TreeNode tn)
    {
        Ext.Net.TreeNode cpap = new Ext.Net.TreeNode("Daily CPAP Results");

        Ext.Net.TreeNode tnTxAdherence = new Ext.Net.TreeNode("Treatment Adherence");
        //Todo: link to datepicker

        Ext.Net.TreeNode tnMaskLeak = new Ext.Net.TreeNode("Mask Leak");
        //Todo: link to datepicker

        Ext.Net.TreeNode tnAHI = new Ext.Net.TreeNode("Apnea-Hypopnea Index");
        //Todo: link to datepicker;

        cpap.Expanded = true;

        cpap.Nodes.Add(tnTxAdherence);
        cpap.Nodes.Add(tnMaskLeak);
        cpap.Nodes.Add(tnAHI);

        tn.Nodes.Add(cpap);
    }

    protected void RenderTreatments(Ext.Net.TreeNode tn) 
    {
        if (m_dsTreatments != null) 
        {
            foreach (DataTable dt in m_dsTreatments.Tables) 
            {
                foreach (DataRow dr in dt.Rows) 
                {
                    string strDate = Convert.ToDateTime(dr["initial_visit_date"]).ToShortDateString();
                    Ext.Net.TreeNode tnTreatment = new Ext.Net.TreeNode("Treatment");
                    tnTreatment.Expanded = true;

                    long lTreatmentID = -1;
                    if (!dr.IsNull("treatment_id"))
                    {
                        long.TryParse(dr["treatment_id"].ToString(), out lTreatmentID);
                    }

                    long lCaseClosed = Convert.ToInt32(dr["CASE_CLOSED"]);

                    if (!dr.IsNull("treatment_id"))
                    {
                        long.TryParse(dr["treatment_id"].ToString(), out lTreatmentID);
                    }

                    //Render Episode Node
                    if (m_BaseMstr.APPMaster.HasUserRight((long)SUATUserRight.ProcessNewPatientsUR))
                    {
                        if (m_BaseMstr.APPMaster.PatientHasOpenCase)
                        {
                            this.RenderPatient(m_BaseMstr, tnTreatment, lTreatmentID);
                        }
                    }

                    //Render Patient Assesments
                    this.RenderPatientAssessments(tnTreatment, lTreatmentID, lCaseClosed);
                    
                    //Render Encounters Node
                    this.RenderEncounters(tnTreatment, lTreatmentID, lCaseClosed);

                    if (lCaseClosed == 1)
                    {
                        tnTreatment.Icon = Icon.Lock;
                        tnTreatment.Expanded = false;
                    }

                    tn.Nodes.Add(tnTreatment);
                }
            }
        }
    }

    // Render Patient's Assessments
    protected void RenderPatientAssessments(Ext.Net.TreeNode tn,
                                    long lTreatmentID,
                                    long lCaseClosed)
    {
        if (m_dsEncounters != null)
        {
            DataRow[] drEncounters = m_dsEncounters.Tables[0].Select("treatment_id = " + lTreatmentID.ToString() + " AND encounter_type_id = 99");

            if (drEncounters.GetLength(0) > 0)
            {
                Ext.Net.TreeNode tnAssessment = new Ext.Net.TreeNode("Assessments");
                tnAssessment.Expanded = true;

                foreach (DataRow dr in drEncounters)
                {
                    if (!dr.IsNull("encounter_date"))
                    {
                        string strDate = Convert.ToDateTime(dr["encounter_date"]).ToShortDateString();
                        Ext.Net.TreeNode tnDate = new Ext.Net.TreeNode(strDate);

                        //Render Assessments Nodes
                        this.RenderAssessmentsNodes(tnDate, dr["encounter_id"].ToString(), lTreatmentID); 

                        tnAssessment.Nodes.Add(tnDate);
                    }
                }

                //SOAP note related rights: 
                if (m_BaseMstr.APPMaster.HasUserRight(m_BaseMstr.APPMaster.lSOAPNoteRights))
                {
                    tn.Nodes.Add(tnAssessment);
                }
            }
        }
    }

    //Render patient assessments nodes
    protected void RenderAssessmentsNodes(Ext.Net.TreeNode tn, string strEncounterID, long lTreatmentID)
    {
        if (m_dsAssessments != null) 
        {
            DataRow[] drAssessments = m_dsAssessments.Tables[0].Select("encounter_id = '" + strEncounterID + "'");
            foreach (DataRow dr in drAssessments)
            {
                long lEncounterIntakeID = -1;
                if (!dr.IsNull("ENCOUNTER_INTAKE_ID"))
                {
                    long.TryParse(dr["ENCOUNTER_INTAKE_ID"].ToString(), out lEncounterIntakeID);
                }

                long lIntakeType = -1;
                if (!dr.IsNull("intake_type"))
                {
                    long.TryParse(dr["intake_type"].ToString(), out lIntakeType);
                }

                string strModuleTitle = String.Empty;
                if (!dr.IsNull("module"))
                {
                    strModuleTitle = dr["module"].ToString();
                }
                Ext.Net.TreeNode tnMod = new Ext.Net.TreeNode(strModuleTitle);
                tnMod.Icon = Icon.Report;

                string strWindowTitle = String.Empty;
                if (lIntakeType == 1)
                {
                    strWindowTitle = "Patient Assessment";
                }

                tnMod.Listeners.Click.Handler = "showAssessmentReport({encounterID:'" + strEncounterID 
                    + "', encounterIntakeID:'" + lEncounterIntakeID.ToString() 
                    + "', windowTitle:'"+ strWindowTitle +"'})";

                tn.Nodes.Add(tnMod);
            }
        }
    }
    
    // Render Encounters Node
    protected void RenderEncounters(Ext.Net.TreeNode tn, 
                                    long lTreatmentID, 
                                    long lCaseClosed) 
    {
        if (m_dsEncounters != null) 
        {
            DataRow[] drEncounters = m_dsEncounters.Tables[0].Select("treatment_id = " + lTreatmentID.ToString() + " AND encounter_type_id <> 99", "encounter_date DESC");

            if (drEncounters.GetLength(0) > 0)
            { 
                Ext.Net.TreeNode tnEncounter = new Ext.Net.TreeNode("Encounters");
                tnEncounter.Expanded = true;

                foreach (DataRow dr in drEncounters) 
                {
                    bool bEncounterClosed = false;
                    if (!dr.IsNull("closed")) 
                    { 
                        bEncounterClosed = (Convert.ToInt32(dr["closed"])==1) ? true : false;
                    }

                    bool hasAddendum = false;
                    if (!dr.IsNull("ADDENDUM")) 
                    {
                        hasAddendum = true;
                    }

                    if (!dr.IsNull("encounter_date")) 
                    {
                        if (Convert.ToInt32(dr["ENCOUNTER_TYPE_ID"]) != (long)EncounterType.SELF_MANAGEMENT)
                        {
                            //string strDate = "Progress - " + Convert.ToDateTime(dr["encounter_date"]).ToShortDateString();
                            string strEncTypeDesc = GetEncounterType(Convert.ToInt32(dr["ENCOUNTER_TYPE_ID"]));
                            string strEncTypeLabel = strEncTypeDesc + " - " + Convert.ToDateTime(dr["encounter_date"]).ToShortDateString();

                            Ext.Net.TreeNode tnDate = new Ext.Net.TreeNode(strEncTypeLabel);
                            if (bEncounterClosed)
                            {
                                tnDate.Icon = Icon.Lock;
                            }

                            //Render Encounter Components
                            this.RenderEncounterComponents(tnDate,
                                                dr["encounter_id"].ToString(),
                                                lTreatmentID,
                                                lCaseClosed,
                                                bEncounterClosed,
                                                hasAddendum);

                            tnEncounter.Nodes.Add(tnDate);
                        }

                    }
                }

                //SOAP note related rights: 
                if (m_BaseMstr.APPMaster.HasUserRight(m_BaseMstr.APPMaster.lSOAPNoteRights))
                {
                    tn.Nodes.Add(tnEncounter);
                }
            }
        }
    }

    protected string GetEncounterType(long lEncType) {
        string strEncType = String.Empty;

        if (m_dsEncounterTypes != null) {
            foreach (DataTable dt in m_dsEncounterTypes.Tables) {
                foreach (DataRow dr in dt.Rows) {
                    if (!dr.IsNull("ENCOUNTER_TYPE_ID"))
                    {
                        if (lEncType == Convert.ToInt32(dr["ENCOUNTER_TYPE_ID"])) {
                            strEncType = dr["ENC_TYPE_DESC"].ToString();
                        }
                    }
                }
            }
        }

        return strEncType;
    }

    // Render Encounter Components
    protected void RenderEncounterComponents(Ext.Net.TreeNode tn, string strEncounterID, long lTreatmentID, long lCaseClosed, bool bEncounterClosed, bool bHasAddendum) 
    {
        
        Ext.Net.TreeNode tnSOAPNote = new Ext.Net.TreeNode("SOAP Note");
        if (lCaseClosed == 1 || (bHasAddendum && bEncounterClosed))
        {
            tnSOAPNote.Listeners.Click.Handler = "winrpt.showReport('patSOAPPRpt', ['" + m_BaseMstr.SelectedPatientID + "', '" + strEncounterID + "', '" + lTreatmentID.ToString() + "'])";
        }
        else
        {
            tnSOAPNote.Href = "pat_encounter.aspx?op0=" + m_BaseMstr.SelectedPatientID + "&op1=" + strEncounterID + "&op2=" + lTreatmentID.ToString();
        } 

        Ext.Net.TreeNode tnTPlan = new Ext.Net.TreeNode("Treatment Plan");
        tnTPlan.Icon = Icon.Report;
        tnTPlan.Listeners.Click.Handler = "winrpt.showReport('TreatmentPlanRpt', ['" + m_BaseMstr.SelectedPatientID + "', '" + strEncounterID + "', '" + lTreatmentID.ToString() + "'])";

        if (m_BaseMstr.APPMaster.HasUserRight((long)SUATUserRight.NoteFlagsToDoUR)
            || m_BaseMstr.APPMaster.HasUserRight((long)SUATUserRight.NoteSubjectiveUR)
            || m_BaseMstr.APPMaster.HasUserRight((long)SUATUserRight.NoteObjectiveUR)
            || m_BaseMstr.APPMaster.HasUserRight((long)SUATUserRight.NoteAssessmentUR)
            || m_BaseMstr.APPMaster.HasUserRight((long)SUATUserRight.NotePlanUR)
            || m_BaseMstr.APPMaster.HasUserRight((long)SUATUserRight.LockNoteUR))
        {
            tn.Nodes.Add(tnSOAPNote);
        }
        if (m_BaseMstr.APPMaster.HasUserRight((long)SUATUserRight.NotePlanUR))
        {
            //tn.Nodes.Add(tnTPlan);
        }
    }

    public void RenderTreePanel(HtmlGenericControl ph) 
    {
        // Define Ext.Net.TreePanel object
        Ext.Net.TreePanel tree = new Ext.Net.TreePanel();
        tree.ID = "VerticalMenu";
        tree.Width = Unit.Pixel(250);
        tree.AutoWidth = true;
        //tree.Height = Unit.Pixel(500);
        //tree.AutoHeight = true;
        tree.Title = String.Empty;
        tree.TitleCollapse = true;
        tree.AutoScroll = true;
        tree.RootVisible = false;
        tree.Frame = false;
        tree.Border = false;
        tree.BodyCssClass = "tree-back";

        // Create a root node
        Ext.Net.TreeNode root = new Ext.Net.TreeNode("Root");
        root.Expanded = true;
        tree.Root.Add(root);

        //Render Treatments
        RenderTreatments(root);

        ph.Controls.Add(tree);
    }
}
