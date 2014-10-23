using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AjaxControlToolkit;
using DataAccess;

public partial class ucPatientSOAPP : System.Web.UI.UserControl
{

    // Access modes (no access, read only, read write)
    // check for ReadOnly mode
    protected CSec usrsec = new CSec();
    public long lROSubjective;  // Subjective
    public long lROObjective;   // Objective
    public long lROAssessment;  // Assessment

    public bool bNoteSigned;
    public bool bNoteLocked;
    protected bool bSecondPass = false;

    public DataSet dsEnc = null;
    public CEncounter enc = new CEncounter();
    
    //get/set basemaster info
    protected BaseMaster m_BaseMstr;
    public BaseMaster BaseMstr
    {
        get { return m_BaseMstr; }
        set { ucAxes.BaseMstr = value;
            ucProblemList.BaseMstr = value;
            m_BaseMstr = value;
        }
    }

    protected string strDelimiter = "\r\n~\r\n";
    //Initialize Encounter Type
    protected string strEncounterType = "";

    //page load
    protected void Page_Load(object sender, EventArgs e)
    {
        //TIU SUPPORT
        if (m_BaseMstr.APPMaster.TIU)
        {
            tpTIUNote.Visible = true;

            string strConfirm = "";
            strConfirm += "if( confirm('";
            strConfirm += "You are about to send a TIU note to CPRS. REVAMP TIU Notes on CPRS cannot be modified. If you do not want to send the TIU note at this time click Cancel.";
            strConfirm += "\\n\\n";
            strConfirm += "Do you want to continue?";
            strConfirm += "') )";
            strConfirm += "{";
            strConfirm += " __doPostBack('";
            strConfirm += btnWriteTIU.ClientID;
            strConfirm += "', ''); ";
            //if you dont return true you lose the please wait!
            strConfirm += " return true;";
            strConfirm += "}";
            strConfirm += "else";
            strConfirm += "{";
            strConfirm += "  return false;";
            strConfirm += "}";

            //add the JS to the button click
            btnWriteTIU.OnClientClick = strConfirm;

            if (!IsPostBack)
            {
                string strUserName = String.Empty;
                string strPWD = String.Empty;
                long lRegionID = 0;
                long lSiteID = 0;
                string strNoteTitleLabel = String.Empty;
                long lNoteClinicID = 0;

                bool bStatus = GetMDWSAccountInfo(m_BaseMstr.FXUserID,
                                                  out strUserName,
                                                  out strPWD,
                                                  out lRegionID,
                                                  out lSiteID,
                                                  out strNoteTitleLabel,
                                                  out lNoteClinicID);

                CMDWSUtils mdws = new CMDWSUtils();
                mdws.LoadNoteTitlesDDL(strNoteTitleLabel,
                                       m_BaseMstr,
                                       ddlNoteTitle);
                mdws.LoadClinicsDDL(lNoteClinicID,
                                    m_BaseMstr,
                                    ddlClinic);
                GetTIUNote();
            }
        }
        else
        {
            pnlViewNote.Visible = false;
            pnlWriteNote.Visible = false;
            tpTIUNote.Visible = false;
        }
        
        if (!IsPostBack && !bSecondPass)
        {
            //VERIFY STATUS OF THE PATIENT RECORD
            CPatientLock plock = new CPatientLock(m_BaseMstr);

            string strLockProviderName = String.Empty;
            string strLockProviderEmail = String.Empty;

            m_BaseMstr.IsPatientLocked = plock.IsPatientLocked(m_BaseMstr.SelectedPatientID, out strLockProviderName, out strLockProviderEmail);
            Session["PAT_LOCK_PROVIDER"] = strLockProviderName;
            Session["PAT_LOCK_EMAIL"] = strLockProviderEmail; 
        }
        
        bool bRecLock = BaseMstr.IsPatientLocked;
        
        //check soap sections rights mode
        lROSubjective = usrsec.GetRightMode(BaseMstr, (long)SUATUserRight.NoteSubjectiveUR);
        lROObjective = usrsec.GetRightMode(BaseMstr, (long)SUATUserRight.NoteObjectiveUR);
        lROAssessment = usrsec.GetRightMode(BaseMstr, (long)SUATUserRight.NoteAssessmentUR);

        if (bRecLock) 
        {
            lROSubjective = (long)RightMode.ReadOnly;
            lROObjective = (long)RightMode.ReadOnly;
            lROAssessment = (long)RightMode.ReadOnly;
        }
        
        //just return if no patient is loaded
        if (BaseMstr.SelectedPatientID.Length < 1)
        {
            return;
        }

        //wire up the main save button
        if (BaseMstr.OnMasterSAVE())
        {
            MasterSave();
        }

        DisableSOAPNote();

        switch (rblAssessmentView.SelectedIndex)
        {
            case 0:
                vwDiagnosis.Style.Add("display", "block");
                vwAssessmentNote.Style.Add("display", "none");
                txtAssessment.Visible = false;
                break;

            case 1:
                vwDiagnosis.Style.Add("display", "none");
                vwAssessmentNote.Style.Add("display", "block");
                txtAssessment.Visible = true;
                break;
        }

        //-----------------------------------------------------------------
        //  NOT POSTBACK
        #region NotPostback
        if (!IsPostBack) 
        {
            mvwObjectiveOptions.ActiveViewIndex = 0;

            rblObjectiveView.SelectedIndex = 1; //Default Note

            rblAssessmentView.SelectedIndex = 0;
            vwDiagnosis.Style.Add("display", "block"); //Diagnosis View

            ucAxes.Load_Assessment();
            ucAxes.AutoSelectProblemInit();

            ucProblemList.Initialize();
        }
        #endregion


        //-----------------------------------------------------------------
        //  IS POSTBACK
        #region IsPostBack
        if (IsPostBack)
        {
            string EvtSender = String.Empty;
            if (Request.Params.Get("__EVENTTARGET") != null)
            {
                EvtSender = Request.Params.Get("__EVENTTARGET").ToString();
                //if a selection was made on the diagnosis popups
                if (Regex.IsMatch(EvtSender, "axis(1)", RegexOptions.IgnoreCase))
                {
                    ucProblemList.Initialize();
                }
            }
        }
        #endregion

        //TIU SUPPORT
        if (m_BaseMstr.APPMaster.TIU)
        {
            //get the postback control
            string strPostBackControl = Request.Params["__EVENTTARGET"];
            if (strPostBackControl != null)
            {
                //did we do a click write tiu?
                if (strPostBackControl.IndexOf("btnWriteTIU") > -1)
                {
                    WriteTIU();
                }
            }
        }
    }

    /// <summary>
    /// clear patient
    /// </summary>
    public void ClearPatient()
    {
        BaseMstr.SelectedPatientID = "";
        BaseMstr.SelectedTreatmentID = 0;
        BaseMstr.SelectedEncounterID = "";
        BaseMstr.ClosePatient();
        
        txtAssessment.Text = "";
        txtObjective.Text = "";
        txtSessionTime.Text = "";
        txtSubjective.Text = "";
        txtVisitDate.Text = "";

        txtVisitDate.ReadOnly = false;

        txtSessionTime.ReadOnly = false;
        txtSubjective.ReadOnly = false;
        txtObjective.ReadOnly = false;
        txtAssessment.ReadOnly = false;

        cboSTemplates.Enabled = true;
        cboATemplates.Enabled = true;
        cboOTemplates.Enabled = true;
    }

    /// <summary>
    /// disable soap note
    /// </summary>
    protected void DisableSOAPNote()
    {
        dsEnc = (DataSet)Session["ENCOUNTERDS"];

        CDataUtils utils = new CDataUtils();

        long lCaseClosed = utils.GetLongValueFromDS(dsEnc, "CASE_CLOSED");
        long lEncounterClosed = utils.GetLongValueFromDS(dsEnc, "CLOSED");
        bNoteLocked = (lEncounterClosed == 1);

        //set label of "Lock Note" button
        btnLockNote.Text = bNoteLocked ? "Unlock Note" : "Lock Note";

        //TIU SUPPORT
        if (m_BaseMstr.APPMaster.TIU)
        {
            if (bNoteLocked)
            {
                tpTIUNote.Visible = true;
            }
            else
            {
                tpTIUNote.Visible = false;
            }
        }

        if (lCaseClosed == 1 || lEncounterClosed == 1)
        {
            txtVisitDate.ReadOnly = true;
            txtSessionTime.ReadOnly = true;
            txtSubjective.ReadOnly = true;
            txtObjective.ReadOnly = true;
            txtAssessment.ReadOnly = true;

            cboSTemplates.Enabled = false;
            cboATemplates.Enabled = false;
            cboOTemplates.Enabled = false;

            ucAxes.bAllowUpdate = false;
        }
        else
        {
            ucAxes.bAllowUpdate = true;

            //enable/disable tabs by Right mode value
            CheckTabAccessModes();
        }
    }

    //runs when user presses the master save button in the app
    protected void MasterSave()
    {
        if(BaseMstr.IsPatientLocked)
        {
            string strBMComment = "<img alt=\"\" src=\"Images/lock16x16.png\" /> <b>Read-Only Access</b>: ";
                   strBMComment += "The patient's record is in use by " + Session["PAT_LOCK_PROVIDER"].ToString() + ".";
            BaseMstr.StatusCode = 1;
            BaseMstr.StatusComment = strBMComment;
            ShowSysFeedback();
            return;
        }
        
        DataSet ds = (DataSet)Session["ENCOUNTERDS"];

        CDataUtils utils = new CDataUtils();

        long lCaseClosed = utils.GetLongValueFromDS(ds, "CASE_CLOSED");
        long lEncounterClosed = utils.GetLongValueFromDS(ds, "CLOSED");
        bNoteLocked = (lEncounterClosed == 1);

        CSoapp soapp = new CSoapp();

        if (tabContSOAP.ActiveTab == btnSubjective)//subjective
        {
            if (lROSubjective > (long)RightMode.ReadOnly)
            {
                UpdateSubjectiveNote(); 
            }

            if (lROSubjective == (long)RightMode.ReadOnly)
            {
                BaseMstr.StatusCode = 1;
                BaseMstr.StatusComment = "<img alt=\"\" src=\"Images/lock16x16.png\" /> You have <b>Read-Only Access</b> to this section.";
            }
        }

        if (tabContSOAP.ActiveTab == btnObjective)//objective
        {

            if (lROObjective > (long)RightMode.ReadOnly)
            {
                UpdateObjectiveNote(); 
            }

            if (lROObjective == (long)RightMode.ReadOnly)
            {
                BaseMstr.StatusCode = 1;
                BaseMstr.StatusComment = "<img alt=\"\" src=\"Images/lock16x16.png\" /> You have <b>Read-Only Access</b> to this section.";
            }
        }

        if (tabContSOAP.ActiveTab == btnAssessment)//assessment
        {
            if (lROAssessment > (long)RightMode.ReadOnly)
            {
                long lDLC = 0;
                UpdateAssessmentNote(); 
            }

            if (lROAssessment == (long)RightMode.ReadOnly)
            {
                BaseMstr.StatusCode = 1;
                BaseMstr.StatusComment = "<img alt=\"\" src=\"Images/lock16x16.png\" /> You have <b>Read-Only Access</b> to this section.";
            }
        }

        //TIU SUPPORT
        //they saved the note so update the text that 
        //will go to tiu
        if (BaseMstr.APPMaster.TIU)
        {
            GetTIUNote();
        }

        // save session time
        soapp.updtSessionTime(BaseMstr,
                              BaseMstr.SelectedEncounterID,
                              BaseMstr.SelectedTreatmentID,
                              "N/A",
                              txtSessionTime.Text);

        //soapp.updtTreatmentPlan(BaseMstr, 
        //                        BaseMstr.SelectedPatientID, 
        //                        BaseMstr.SelectedEncounterID, 
        //                        BaseMstr.SelectedTreatmentID);

        //get the data for this encounter
        Session["ENCOUNTERDS"] = enc.GetEncounterDS(BaseMstr,
                                 BaseMstr.SelectedPatientID,
                                 BaseMstr.SelectedTreatmentID,
                                 BaseMstr.SelectedEncounterID);

        DisableSOAPNote();

        ShowSysFeedback();
    }  

    //load all the template combos
    protected void LoadTemplateCombos()
    {
        //load the template dropdown(s) note A has no templates as designed...
        CTemplate template = new CTemplate();

        //ENCOUNTER_TEMPLATES
        DataSet dsEncTemplates = template.GetTemplateGroupsDS(BaseMstr);
        cboEncounterTemplates.DataTextField = "GROUP_NAME";
        cboEncounterTemplates.DataValueField = "TEMPLATE_GROUP_ID";
        cboEncounterTemplates.DataSource = dsEncTemplates;
        cboEncounterTemplates.DataBind();

        cboEncounterTemplates.Items.Insert(0, new ListItem(String.Empty, "-1"));

        //SUBJECTIVE 
        template.LoadTemplateComboByType(BaseMstr,
                                         1,
                                         cboSTemplates);

        //OBJECTIVE 
        template.LoadTemplateComboByType(BaseMstr,
                                         2,
                                         cboOTemplates);

        //ASSESSMENT 
        template.LoadTemplateComboByType(BaseMstr,
                                         3,
                                         cboATemplates);
    }

    //called by the outside to get things going...
    public bool Initialize(string strPatientID,
                            long lTreatmentID,
                            string strEncounterID,
                            string strSTemplateText,
                            string strOTemplateText,
                            string strPTemplateText,
                            string strPrevTemplateText)
    {
        CDataUtils utils = new CDataUtils();

        bool bDiffPatient = (!strPatientID.Equals(BaseMstr.SelectedPatientID));
        bool bDiffEncounter = (!strEncounterID.Equals(BaseMstr.SelectedEncounterID));

        ClearPatient();

        //we are essentially looking up this patient and
        //this treatment and this encounter so load the
        //"selected"s on the base master
        BaseMstr.SelectedPatientID = strPatientID;
        BaseMstr.SelectedTreatmentID = lTreatmentID;
        BaseMstr.SelectedEncounterID = strEncounterID;

        //start on the first view in the multiview
        tabContSOAP.ActiveTab = btnSubjective;

        if (bDiffEncounter || bDiffPatient || Session["ENCOUNTERDS"] == null)
        {
            //get the data for this encounter
            Session["ENCOUNTERDS"] = enc.GetEncounterDS(BaseMstr,
                                     BaseMstr.SelectedPatientID,
                                     BaseMstr.SelectedTreatmentID,
                                     BaseMstr.SelectedEncounterID);
        }

        dsEnc = (DataSet)Session["ENCOUNTERDS"];

        //load the text boxes
        LoadTextBoxes(dsEnc);

        //load the Modality Info
        LoadModaliltyInfo(dsEnc);

        //load all the dropdowns for template selection
        LoadTemplateCombos();

        //call page load to load everything else
        Page_Load(null, null);
        bSecondPass = true;

        BaseMstr = m_BaseMstr;

        return true;
    }

    //handle the clicking of the sign note from the other uc on this uc
    protected override bool OnBubbleEvent(object source, EventArgs e)
    {
        bool handled = false;

        string strSource = source.ToString();
        strSource = strSource.ToUpper();

        if (strSource.IndexOf("UCTP") > -1)
        {
            ucProblemList.Initialize();
            handled = true;
            return handled;
        }
        
        //handle discontinue diagnosis item
        if (strSource.IndexOf("UCSOAP_ASSESSMENT") > -1)
        {
            ucProblemList.Initialize();
            handled = true;
            return handled;
        }

        //Check if problem link button was clicked on the objective view
        if (strSource.IndexOf("LINKBUTTON") > -1)
        {
            LinkButton lnkbtn = (LinkButton)source;
            if (Regex.IsMatch(lnkbtn.ID, "LNKBTNA\\d+PROBLEM", RegexOptions.IgnoreCase))
            {
                handled = true;
                return handled;
            }
        }

        if (strSource.IndexOf("IMAGEBUTTON") > -1)
        {
            handled = true;
            return handled;
        }

        handled = true;
        return handled;
    }

    //load Modality Desc
    protected void LoadModaliltyInfo(DataSet dsEnc)
    {
        CDataUtils utils = new CDataUtils();
        strEncounterType = utils.GetStringValueFromDS(dsEnc, "ENCOUNTER_TYPE_ID");

        CTreatment trt = new CTreatment();

        DataSet dsTrt = trt.GetStatModalityByModalityIDDS(BaseMstr, Convert.ToInt64(strEncounterType));

        string strModalityDesc = utils.GetStringValueFromDS(dsTrt, "MODALITY");
        string strModalityDuration = utils.GetStringValueFromDS(dsTrt, "DURATION");

        txtSessionTime.Text = strModalityDuration;
        txtSessionTime.ReadOnly = true;
        txtSessionTime.Enabled = false;

        spModalityInfo.InnerText = strModalityDesc;
    }

    //load text boxes
    protected void LoadTextBoxes(DataSet dsEnc)
    {
        CDataUtils utils = new CDataUtils();

        txtVisitDate.Text = utils.GetDateValueAsStringFromDS(dsEnc, "ENCOUNTER_DATE");
        txtSessionTime.Text = utils.GetStringValueFromDS(dsEnc, "SESSION_TIME");

        txtSubjective.Text = utils.GetStringValueFromDS(dsEnc, "SUBJECTIVE");
        txtObjective.Text = utils.GetStringValueFromDS(dsEnc, "OBJECTIVE");
        txtAssessment.Text = utils.GetStringValueFromDS(dsEnc, "ASSESSMENT");
    }

    //subjective template dropdown
    protected void cboSTemplates_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        //add the template text to the note...
        if (cboSTemplates.SelectedValue != "")
        {
            CTemplate template = new CTemplate();
            string strParsedTemplate = template.GetParsedTemplateText2(BaseMstr,
                                                                      BaseMstr.SelectedPatientID,
                                                                      BaseMstr.SelectedEncounterID,
                                                                      Convert.ToInt32(cboSTemplates.SelectedValue));

            txtSubjective.Text = txtSubjective.Text + strParsedTemplate;
            cboSTemplates.ClearSelection();
        }
        
    }

    //objective template dropdown
    protected void cboOTemplates_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        //add the template text to the note...
        if (cboOTemplates.SelectedValue != "")
        {
            CTemplate template = new CTemplate();
            string strParsedTemplate = template.GetParsedTemplateText2(BaseMstr,
                                                                      BaseMstr.SelectedPatientID,
                                                                      BaseMstr.SelectedEncounterID,
                                                                      Convert.ToInt32(cboOTemplates.SelectedValue));

            txtObjective.Text = txtObjective.Text + strParsedTemplate;
           cboOTemplates.ClearSelection();
        }
        
    }

    //assessment template dropdown
    protected void cboATemplates_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        //add the template text to the note...
        if (cboATemplates.SelectedValue != "")
        {
            CTemplate template = new CTemplate();
            string strParsedTemplate = template.GetParsedTemplateText2(BaseMstr,
                                                                      BaseMstr.SelectedPatientID,
                                                                      BaseMstr.SelectedEncounterID,
                                                                      Convert.ToInt32(cboATemplates.SelectedValue));

            txtAssessment.Text = txtAssessment.Text + strParsedTemplate;
            cboATemplates.ClearSelection();
        }
       
    }

    protected bool UpdateSubjectiveNote() 
    {
        CSoapp soapp = new CSoapp();
        string strSubjective = txtSubjective.Text.Trim();
        
        return soapp.updtSubjective(BaseMstr,
                              BaseMstr.SelectedEncounterID,
                              BaseMstr.SelectedTreatmentID,
                              strSubjective);
    }

    protected bool UpdateObjectiveNote() 
    {
        CSoapp soapp = new CSoapp();
        string strObjective = txtObjective.Text.Trim();

        bool bUpdtObjective = soapp.updtObjective(BaseMstr,
                            BaseMstr.SelectedEncounterID,
                            BaseMstr.SelectedTreatmentID,
                            strObjective);

        return bUpdtObjective;
    }

    protected bool UpdateAssessmentNote() 
    {
        CSoapp soapp = new CSoapp();
        string strAssessment = txtAssessment.Text.Trim();

        bool bUpdtAssessment = soapp.updtAssessment(BaseMstr,
                             BaseMstr.SelectedEncounterID,
                             BaseMstr.SelectedTreatmentID,
                             strAssessment,
                             0,
                             String.Empty,
                             0
                            );

        return bUpdtAssessment;
    }

    //toggle note's lock state
    protected void btnLockNote_OnClick(object sender, EventArgs e) 
    {
        CEncounter encnote = new CEncounter();
        if (encnote.LockNote(BaseMstr,
                             BaseMstr.SelectedPatientID,
                             BaseMstr.SelectedEncounterID,
                             BaseMstr.SelectedTreatmentID))
        {
            Session["ENCOUNTERDS"] = null;
            Session["ENCOUNTERS_LIST_DS"] = null;
            
            string strPostBackURL = "pat_encounter.aspx?op0=" + BaseMstr.SelectedPatientID;
            strPostBackURL += "&op1=" + BaseMstr.SelectedEncounterID;
            strPostBackURL += "&op2=" + BaseMstr.SelectedTreatmentID.ToString();

            Response.Redirect(strPostBackURL);
        }
        else
        {
            ShowSysFeedback();
        }
    }
    
    //--------------------------------------------
    //update SOAP note's appending Standard text
    protected bool UpdateNoteStandardText() 
    {
        return false;
    }

    protected void rblAssessmentView_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblAssessmentView.SelectedIndex == 0) //Diagnosis
        {
            vwDiagnosis.Style.Add("display", "block");
            vwAssessmentNote.Style.Add("display", "none");
            txtAssessment.Visible = false;
        }

        if (rblAssessmentView.SelectedIndex == 1) //NOTE
        {
            vwDiagnosis.Style.Add("display", "none");
            vwAssessmentNote.Style.Add("display", "block");
            txtAssessment.Visible = true;
        }
    }

    /// <summary>
    /// objective index change
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rblObjectiveView_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblObjectiveView.SelectedIndex == 0 ) //OBJECTIVE MEASURES
        {
            mvwObjectiveOptions.ActiveViewIndex = 1;
        }
        else if (rblObjectiveView.SelectedIndex == 1) //NOTE
        {
            mvwObjectiveOptions.ActiveViewIndex = 0;
        }
    }

    protected bool NoteIsSigned(DataSet ds) 
    {
        long lSignatures = 0;
        if(ds != null)
        {
            foreach (DataTable dt in ds.Tables) 
            { 
                foreach(DataRow dr in dt.Rows)
                {
                    foreach (DataColumn dc in dt.Columns) 
                    {
                        if (Regex.IsMatch(dc.ColumnName, @"(.*)signature_id(.*)", RegexOptions.IgnoreCase))
                        {
                            if (!dr.IsNull(dc.ColumnName))
                            {
                                lSignatures++;
                            }
                        }
                    }
                }
            }
        }
        return (lSignatures > 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MyUserControl_UserControlButtonClicked(object sender, EventArgs e)
    {
        ucProblemList.UpdateProblemSummary();
    }

    /// <summary>
    /// check tab access mode
    /// </summary>
    private void CheckTabAccessModes() 
    { 
        //check patient record lock status
        bool bRecLock = BaseMstr.IsPatientLocked;
        
        // Subjective
        btnSubjective.Enabled = (lROSubjective != (long)RightMode.NoAccess);
        if ((lROSubjective == (long)RightMode.ReadOnly) || bRecLock)
        {
            cboSTemplates.Enabled = false;
            txtSubjective.ReadOnly = true;
        }

        // Objective
        btnObjective.Enabled = (lROObjective != (long)RightMode.NoAccess);
        if ((lROObjective == (long)RightMode.ReadOnly) || bRecLock)
        {
            cboOTemplates.Enabled = false;
            txtObjective.ReadOnly = true;
        }

        // Assessment
        btnAssessment.Enabled = (lROAssessment != (long)RightMode.NoAccess);
        if ((lROAssessment == (long)RightMode.ReadOnly) || bRecLock)
        {
            cboATemplates.Enabled = false;
            txtAssessment.ReadOnly = true;
        }
    }

    /// <summary>
    /// encounter template change
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cboEncounterTemplates_OnSelectedIndexChanged(object sender, EventArgs e) 
    { 
        CTemplate template = new CTemplate();

        if(cboEncounterTemplates.SelectedIndex > 0)
        {
            long lGroupID = Convert.ToInt32(cboEncounterTemplates.SelectedValue);
            DataSet dsTemplates = template.GetGroupTemplatesDS(BaseMstr, lGroupID);

            foreach (DataTable dt in dsTemplates.Tables) 
            {
                foreach (DataRow dr in dt.Rows) 
                {
                    if (!dr.IsNull("TEMPLATE_ID"))
                    {
                        string strTemplateID = dr["TEMPLATE_ID"].ToString();

                        //set SUBJECTIVE templates dropdown
                        foreach (ListItem li in cboSTemplates.Items) 
                        {
                            if (li.Value == strTemplateID) 
                            {
                                string strParsedTemplate = template.GetParsedTemplateText2(BaseMstr,
                                                                      BaseMstr.SelectedPatientID,
                                                                      BaseMstr.SelectedEncounterID,
                                                                      Convert.ToInt32(strTemplateID));

                                txtSubjective.Text = strParsedTemplate;
                            }
                        }

                        //set OBJECTIVE templates dropdown
                        foreach (ListItem li in cboOTemplates.Items)
                        {
                            if (li.Value == strTemplateID)
                            {
                                string strParsedTemplate = template.GetParsedTemplateText2(BaseMstr,
                                                                      BaseMstr.SelectedPatientID,
                                                                      BaseMstr.SelectedEncounterID,
                                                                      Convert.ToInt32(strTemplateID));

                                txtObjective.Text = strParsedTemplate;
                            }
                        }

                        //set ASSESSMENT / PLAN templates dropdown
                        foreach (ListItem li in cboATemplates.Items)
                        {
                            if (li.Value == strTemplateID)
                            {
                                string strParsedTemplate = template.GetParsedTemplateText2(BaseMstr,
                                                                      BaseMstr.SelectedPatientID,
                                                                      BaseMstr.SelectedEncounterID,
                                                                      Convert.ToInt32(strTemplateID));

                                txtAssessment.Text = strParsedTemplate;
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// show sys feedback
    /// </summary>
    protected void ShowSysFeedback()
    {
        if (BaseMstr.StatusCode > 0 && !String.IsNullOrEmpty(BaseMstr.StatusComment))
        {
            HtmlContainerControl div = (HtmlContainerControl)this.BaseMstr.FindControl("divSysFeedback");
            div.InnerHtml = BaseMstr.StatusComment;
            ScriptManager.RegisterStartupScript(upSOAP, typeof(string), "showAlert", "Ext.onReady(function(){winSysFeedback.show();})", true);
            BaseMstr.ClearStatus();
        }
    }
    
    /// <summary>
    /// TIU SUPPORT - write a tiu note
    /// </summary>
    public void WriteTIU()
    {
        if (ddlClinic.SelectedIndex <= 0 ||
            ddlNoteTitle.SelectedIndex <= 0)
        {
            BaseMstr.StatusCode = 1;
            BaseMstr.StatusComment = "Please select a valid note title and clinic!";
            ShowSysFeedback();
            return;
        }

        //status
        REVAMP.TIU.CStatus status = new REVAMP.TIU.CStatus();

        //get data object and connection info
        string strConnectionString = String.Empty;
        REVAMP.TIU.CData data = null;
        bool bAudit = false;
        CMDWSUtils mdwsUtils = new CMDWSUtils();
        mdwsUtils.GetDataInfo(BaseMstr,
                              Session,
                              out data,
                              out strConnectionString,
                              out bAudit);

        //check connection to mdws login if we are not connected
        long lMDWSUserID = 0;
        status = mdwsUtils.MDWSLogin(data,
                                     BaseMstr,
                                     strConnectionString,
                                     bAudit,
                                     out lMDWSUserID);
        if (!status.Status)
        {
            BaseMstr.StatusCode = 1;
            BaseMstr.StatusComment = "Could not login to MDWS, please check your credentials!";
            ShowSysFeedback();
            return;
        }

        //get the MDWS patient id
        string strMDWSPatID = "";
        string strProviderID = "";
        status = mdwsUtils.GetPatientIDs(strConnectionString,
                                         bAudit,
                                         m_BaseMstr,
                                         data,
                                         m_BaseMstr.SelectedPatientID,
                                         out strMDWSPatID,
                                         out strProviderID);

        if (!status.Status || strMDWSPatID == String.Empty)
        {
            BaseMstr.StatusCode = 1;
            BaseMstr.StatusComment = "This patient does not have a MDWS patient id! Please contact your System Administrator!";
            ShowSysFeedback();
            return;
        }

        //get the note title IEN
        long lNoteTitleIEN = 0;
        REVAMP.TIU.CNoteTitleData nd = new REVAMP.TIU.CNoteTitleData(data);
        status = nd.GetNoteTitleIEN(strConnectionString,
                                    bAudit,
                                    m_BaseMstr.FXUserID,
                                    m_BaseMstr.Session,
                                    ddlNoteTitle.SelectedValue,
                                    out lNoteTitleIEN);
        if (!status.Status)
        {
            BaseMstr.StatusCode = 1;
            BaseMstr.StatusComment = "Could not retrieve Note ID from MDWS! Please contact your System Administrator!";
            ShowSysFeedback();
            return;
        }

        //write the note to tiu
        REVAMP.TIU.CMDWSOps ops = new REVAMP.TIU.CMDWSOps(data);
        status = ops.WriteNote(strConnectionString,
                                bAudit,
                                m_BaseMstr.FXUserID,
                                m_BaseMstr.SelectedPatientID,
                                strMDWSPatID,
                                m_BaseMstr.SelectedTreatmentID,
                                m_BaseMstr.SelectedEncounterID,
                                Convert.ToString(lMDWSUserID),
                                txtSign.Text,
                                ddlClinic.SelectedValue,
                                Convert.ToString(lNoteTitleIEN),
                                txtTIUNote.Text);
        if (status.Status)
        {
            //this will get the newly saved note from MDWS
            //and swap the panels to view mode
            GetTIUNote();
        }
        else
        {
            BaseMstr.StatusCode = 1;
            BaseMstr.StatusComment = status.StatusComment;
            ShowSysFeedback();
            return;
        }

    }

    /// <summary>
    /// TIU SUPPORT - get the tiu note
    /// </summary>
    protected void GetTIUNote()
    {
        //status
        REVAMP.TIU.CStatus status = new REVAMP.TIU.CStatus();

        //get data object and connection info
        string strConnectionString = String.Empty;
        REVAMP.TIU.CData data = null;
        bool bAudit = false;
        CMDWSUtils mdwsUtils = new CMDWSUtils();
        mdwsUtils.GetDataInfo(BaseMstr,
                              Session,
                              out data,
                              out strConnectionString,
                              out bAudit);

        //check connection to mdws login if we are not connected
        long lMDWSUserID = 0;
        status = mdwsUtils.MDWSLogin(data,
                                     BaseMstr,
                                     strConnectionString,
                                     bAudit,
                                     out lMDWSUserID);
        if (!status.Status)
        {
            return;
        }

        //get the MDWS patient id
        string strMDWSPatID = "";
        string strProviderID = "";
        status = mdwsUtils.GetPatientIDs(strConnectionString,
                                         bAudit,
                                         m_BaseMstr,
                                         data,
                                         m_BaseMstr.SelectedPatientID,
                                         out strMDWSPatID,
                                         out strProviderID);

        if (!status.Status || strMDWSPatID == String.Empty)
        {
            return;
        }

        //write the note to tiu
        string strNoteText = String.Empty;
        REVAMP.TIU.CMDWSOps ops = new REVAMP.TIU.CMDWSOps(data);
        status = ops.GetMDWSTIUNote(strConnectionString,
                                    bAudit,
                                    m_BaseMstr.FXUserID,
                                    m_BaseMstr.SelectedPatientID,
                                    m_BaseMstr.SelectedTreatmentID,
                                    m_BaseMstr.SelectedEncounterID,
                                    out strNoteText);

        if (status.Status)
        {
            if (strNoteText != String.Empty)
            {
                txtViewTIU.Text = strNoteText;
                pnlWriteNote.Visible = false;
                pnlViewNote.Visible = true;
            }
            else
            {
                pnlWriteNote.Visible = true;
                pnlViewNote.Visible = false;

                //preload the the tiu note from SOAP
                string strSubjective = txtSubjective.Text.Trim();
                string strObjective = txtObjective.Text.Trim();
                string strAssessment = txtAssessment.Text.Trim();

                string strNote = String.Empty;
                strNote += strSubjective;

                strNote += "\r\n\r\n";
                strNote += strObjective;

                strNote += "\r\n\r\nASSESSMENT/PLAN:\r\n\r\n";

                //
                //get the diagnosis
                CDataParameterList pListDiag = new CDataParameterList(m_BaseMstr);
                pListDiag.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);
                pListDiag.AddInputParameter("pi_nTreatmentID", m_BaseMstr.SelectedTreatmentID);
                pListDiag.AddInputParameter("pi_vEncounterID", m_BaseMstr.SelectedEncounterID);

                long lStatusCode = 0;
                string strStatusComment = String.Empty;
                DataSet dsDiag = null;
                CDataSet cds = new CDataSet();
                dsDiag = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                             "PCK_REVAMP_TIU.GetEncTIUNoteDiagRS",
                                             pListDiag,
                                             out lStatusCode,
                                             out strStatusComment);
                string strDiagnosis = "";

                if (!REVAMP.TIU.CDataUtils.IsEmpty(dsDiag))
                {
                    foreach (DataTable table in dsDiag.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            string strDiagCode = "";
                            string strDiagnosticText = "";
                            strDiagCode = REVAMP.TIU.CDataUtils.GetDSStringValue(row, "DIAG_CODE");
                            strDiagnosticText = REVAMP.TIU.CDataUtils.GetDSStringValue(row, "DIAGNOSTIC_TEXT");

                            strDiagnosis += strDiagCode + " " + strDiagnosticText;
                            strDiagnosis += "\r\n";
                        }
                    }
                }

                //add the diagnosis to the text
                strNote += "DIAGNOSIS:\r\n\r\n";

                strNote += strDiagnosis;
                strNote += "\r\n";

                //add the assessment plan to the text
                strNote += strAssessment;

                //set the contents of the text
                txtTIUNote.Text = strNote;
            }

        }
        else
        {
            pnlWriteNote.Visible = true;
            pnlViewNote.Visible = false;
        }
    }

    /// <summary>
    ///TIU SUPPORT - get mdws account info
    /// </summary>
    /// <param name="lFXUserID"></param>
    /// <param name="strMDWSUserName"></param>
    /// <param name="strMDWSPWD"></param>
    /// <param name="lRegionID"></param>
    /// <param name="lSiteID"></param>
    /// <param name="strNoteTitleLabel"></param>
    /// <param name="lNoteClinicID"></param>
    /// <returns></returns>
    protected bool GetMDWSAccountInfo(long lFXUserID,
                                      out string strMDWSUserName,
                                      out string strMDWSPWD,
                                      out long lRegionID,
                                      out long lSiteID,
                                      out string strNoteTitleLabel,
                                      out long lNoteClinicID)
    {
        strMDWSUserName = String.Empty;
        strMDWSPWD = String.Empty;
        lRegionID = 0;
        lSiteID = 0;
        strNoteTitleLabel = String.Empty;
        lNoteClinicID = 0;


        //status
        REVAMP.TIU.CStatus status = new REVAMP.TIU.CStatus();

        //get data object and connection info
        string strConnectionString = String.Empty;
        REVAMP.TIU.CData data = null;
        bool bAudit = false;
        CMDWSUtils mdwsUtils = new CMDWSUtils();
        mdwsUtils.GetDataInfo(m_BaseMstr,
                              Session,
                              out data,
                              out strConnectionString,
                              out bAudit);
        //user data
        REVAMP.TIU.CUserData userData = new REVAMP.TIU.CUserData(data);

        //update the account
        DataSet ds = null;
        status = userData.GetMDWSAccountDS(strConnectionString,
                                            bAudit,
                                            m_BaseMstr.FXUserID,
                                            lFXUserID,
                                            m_BaseMstr.Key,
                                            out ds);
        if (!status.Status)
        {
            //error so update status
            m_BaseMstr.StatusCode = 1;
            m_BaseMstr.StatusComment = status.StatusComment;
        }
        else
        {
            strMDWSUserName = REVAMP.TIU.CDataUtils.GetDSStringValue(ds, "MDWS_USER_NAME");
            strMDWSPWD = REVAMP.TIU.CDataUtils.GetDSStringValue(ds, "MDWS_PWD");
            lRegionID = REVAMP.TIU.CDataUtils.GetDSLongValue(ds, "MDWS_REGION_ID");
            lSiteID = REVAMP.TIU.CDataUtils.GetDSLongValue(ds, "MDWS_SITE_ID");
            strNoteTitleLabel = REVAMP.TIU.CDataUtils.GetDSStringValue(ds, "MDWS_NOTE_TITLE_LABEL");
            lNoteClinicID = REVAMP.TIU.CDataUtils.GetDSLongValue(ds, "MDWS_NOTE_CLINIC_ID");
        }

        return status.Status;
    }
}
