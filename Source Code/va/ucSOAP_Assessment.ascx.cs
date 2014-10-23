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

public partial class ucSOAP_Assessment : System.Web.UI.UserControl
{
    public BaseMaster BaseMstr {set; get;}
    public bool bAllowUpdate;
    public long lROAssessment;

    protected CSec usrsec = new CSec();
    protected CTreatmentPlan tplan = new CTreatmentPlan();
    protected DataSet dsProblemList;
    protected DataSet dsAllAxes;
    protected CDiagnosis diagnosis = new CDiagnosis();
    protected CDataUtils utils = new CDataUtils();

    public long lAddedProblemID;

    protected void Page_Load(object sender, EventArgs e)
    {
        // get the Rigt's Mode to the Assessmnet View
        // 0-No Access, 1-Read Only, 2-Read Write
        lROAssessment = usrsec.GetRightMode(BaseMstr, (long)SUATUserRight.NoteAssessmentUR);

        //check patient record lock status
        bool bRecLock = BaseMstr.IsPatientLocked;

        if (bRecLock) 
        {
            lROAssessment = (long)RightMode.ReadOnly;
        }
        
        //bind JS events to controls
        AddJSAttributes();

        //show selected axis div
        ShowSelectedAxisDiv();

        if (!IsPostBack)
        {
            //Load Diagnosis Axis Items Popups
            LoadDiagnosisPopups();

            //auto select first problem when accessing the page
            BaseMstr.SetVSValue("PROBLEM_AUTO_SELECT", true);
        }

        #region IsPostback
        if (IsPostBack) 
        { 
            //get name of postback generating control
            string EvtSender = Request.Params["__EVENTTARGET"];
            string EvtArgs = Request.Params["__EVENTARGUMENT"];

            //if a selection was made on the diagnosis popups
            
            if (Regex.IsMatch(EvtSender, "axis(1)", RegexOptions.IgnoreCase))
            {
                long lAxisType = Convert.ToInt32(Regex.Replace(EvtSender, "\\D", String.Empty));
                long lNewProblemID = -1;

                if (InsertDiagnosis(EvtSender, EvtArgs, out lNewProblemID))
                {
                    //insert criteria & criteria definitions
                    //for the new problem
                    diagnosis.InsertProblemCriteria(
                                                    BaseMstr,
                                                    BaseMstr.SelectedPatientID,
                                                    BaseMstr.SelectedEncounterID,
                                                    BaseMstr.SelectedTreatmentID,
                                                    lNewProblemID,
                                                    Convert.ToInt32(EvtArgs));

                    //reset the criteria & definitions repeater
                    
                    //reload the axes data grids
                    Session["PROBLEMS_LIST_DS"] = null;
                    Load_Assessment();

                    //close the selection popups
                    switch (lAxisType)
                    {
                        case 1:
                            winAxis1.Hide();
                            break;
                    }
                    BaseMstr.SetVSValue("PROBLEM_AUTO_SELECT", false);
                    SelectNewProblem(lNewProblemID, lAxisType);
                    return;
                }
           }
        }
        #endregion
    }


    //Initialize Assessment view when the tab is selected
    public void Load_Assessment() 
    {
        LoadProblemList();
        createGeneralDataset(dsProblemList);
        LoadA1Grid();
        
        //auto select problem
        AutoSelectProblemInit();
    }

    //load and render diagnosis items popup
    protected void LoadDiagnosisPopups()
    {
        litAxisI.Text = diagnosis.RenderAxisScreen(BaseMstr, 1);
       
        CDiagnosis diag = new CDiagnosis();
        if(Session["DIAGNOSIS_ITEMS_DS"] == null)
        {
            Session["DIAGNOSIS_ITEMS_DS"] = diag.GetAllChildItemsDS(BaseMstr);
        }
    }

    //Add JS event listeners
    protected void AddJSAttributes() 
    {
        // bind 'onclick' event to axis selection radio buttons 
        foreach (ListItem li in rblDiagAxes.Items) 
        {
            li.Attributes.Add("onclick", "soap.assessment.showAxisDiv()");
        }

        //bind 'onkeyup' to the discontinue text field
        txtDiscDiag.Attributes.Add("onkeyup","soap.assessment.checkTextLength(this);");
    }

    //display selected axis div
    protected void ShowSelectedAxisDiv() 
    {
        //show the selected axis' div
        if (rblDiagAxes.SelectedIndex > -1)
        {
            switch (rblDiagAxes.SelectedIndex)
            {
                case 0:
                    divDiagAxis1.Style.Add("display", "block");
                    break;
            }
        }
    }

    //Load problem list dataset
    protected void LoadProblemList()
    {
        CEncounter enc = new CEncounter();
        if(Session["PROBLEMS_LIST_DS"] == null){
            Session["PROBLEMS_LIST_DS"] = tplan.GetTreatmentProblemDS(
                                    BaseMstr,
                                    BaseMstr.SelectedPatientID,
                                    BaseMstr.SelectedTreatmentID);
        }
        dsProblemList = (DataSet)Session["PROBLEMS_LIST_DS"];
    }

    //Create a general dataset
    protected void createGeneralDataset(DataSet dsPList) 
    {
        if (dsPList != null) 
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dsPList.Tables[0].Copy());
            ds.Tables[0].TableName = "problem_list";

            //add a table for axis 1 items
            ds.Tables.Add(dsPList.Tables[0].Clone());
            ds.Tables[1].TableName = "a1";

            foreach (DataRow dr in ds.Tables[0].Rows) 
            {
                if (!dr.IsNull("DIAGNOSTIC_AXIS_TYPE")) 
                {
                    int iAxisType = Convert.ToInt32(dr["DIAGNOSTIC_AXIS_TYPE"]);

                    DataRow drNewDR = ds.Tables[iAxisType].NewRow();
                    foreach (DataColumn dc in ds.Tables[0].Columns) 
                    {
                        drNewDR[dc.ColumnName] = dr[dc.ColumnName];
                    }

                    //Insert the new row into the corresponding table
                    ds.Tables[iAxisType].Rows.Add(drNewDR);
                }
            }

            dsAllAxes = ds;
        }
    }

    // READ/INSERT/UPDATE/DELETE -- AXES PROBLEM LIST
    
    #region Axis_1
    protected void LoadA1Grid() 
    {
        repDiagA1.DataSource = dsAllAxes.Tables["a1"];
        repDiagA1.DataBind();
    }
    protected void repDiagA1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            DataRow dr = drv.Row;

            RadioButton radProb = (RadioButton)e.Item.FindControl("radDiagA1");
            radProb.Attributes["value"] = dr["PROBLEM_ID"].ToString();
            radProb.InputAttributes.Add("diagnosisid", dr["DIAGNOSTIC_ID"].ToString());

            //add the problem_id as an attribute to the Update button
            //add the diagnostic_id as an attribute to the Update button
            ImageButton btn = (ImageButton)e.Item.FindControl("btnUpdateA1Diag");
            if (btn != null && !dr.IsNull("PROBLEM_ID")) 
            {
                btn.Attributes.Add("problemid", dr["PROBLEM_ID"].ToString());
                btn.Attributes.Add("diagnosticid", dr["DIAGNOSTIC_ID"].ToString());
            }

            //render sort order combo
            DropDownList cboSort = (DropDownList)e.Item.FindControl("cboA1SortOrder");
            LoadSortOrderCombos(cboSort);
            if (!dr.IsNull("SORT_ORDER"))
            {
                cboSort.SelectedValue = dr["SORT_ORDER"].ToString();
            }
            else
            {
                HtmlInputHidden htxtSort = (HtmlInputHidden)e.Item.FindControl("htxtA1SortOrderOrig");
                htxtSort.Value = "-1";
            }
        }
    }
    protected void btnUpdateA1Diag_OnClick(object sender, EventArgs e) 
    {
        bool bUpdated = false;
        ImageButton btnSender = (ImageButton)sender;
        string strProbID = btnSender.Attributes["problemid"];
        string strDiagID = btnSender.Attributes["diagnosticid"];
        foreach (RepeaterItem ri in repDiagA1.Items) 
        {
            ImageButton btn = (ImageButton)ri.FindControl("btnUpdateA1Diag");
            string strRI_ProbId = btn.Attributes["problemid"];
            string strRI_DiagId = btn.Attributes["diagnosticid"];

            TextBox tb = (TextBox)ri.FindControl("txtA1Comment");

            DropDownList cboSort = (DropDownList)ri.FindControl("cboA1SortOrder");
            long lItmSortOrder = -1;
            if (cboSort.SelectedIndex > 0) 
            {
                lItmSortOrder = Convert.ToInt32(cboSort.SelectedValue);
            }

            if (true)  //old criteria: strProbID == strRI_ProbId; changed for bulk update... Not final yet
            {
                long lProbId = Convert.ToInt32(strRI_ProbId);
                long lDiagId = Convert.ToInt32(strRI_DiagId);
                
                //0 used for Specifier ID
                bUpdated = UpdateDiagnosis(lProbId, lDiagId, 0, String.Empty, tb.Text.Trim(), lItmSortOrder);
            }
        }
        
        //refresh the datagrid if the diagnosis was updated
        if (true) //bUpdated
        {
            Session["PROBLEMS_LIST_DS"]=null;
            Load_Assessment();
        }

        SelectNewProblem(Convert.ToInt32(strProbID), 1);
    }
    protected void radDiagA1_OnCheckedChanged(object sender, EventArgs e)
    {
        lROAssessment = usrsec.GetRightMode(BaseMstr, (long)SUATUserRight.NoteAssessmentUR);
        if (BaseMstr.IsPatientLocked)
        {
            lROAssessment = (long)RightMode.ReadOnly;
        }
        
        RadioButton btnSender = (RadioButton)sender;
        long lProblemID = Convert.ToInt32(btnSender.Attributes["value"]);
        long lDiagID = Convert.ToInt32(btnSender.InputAttributes["diagnosisid"]);

        CheckRadioButtons(sender, repDiagA1, "1");

        //re-load criteria grid for the selected problem
        GetTxCriteria(lProblemID);
        GetProblemDescription(lDiagID);

        if (bAllowUpdate && (lROAssessment > (long)RightMode.ReadOnly))
        {
            EnableCriteriaCheckboxes();
        }
        else
        {
            DisableCriteriaCheckboxes();
        }
    }
    #endregion  

    //INSERT diagnosis
    protected bool InsertDiagnosis(string strAxis, string strDiagID, out long lNewProblemID) 
    {
        long lAxisType = Convert.ToInt32(Regex.Replace(strAxis, "\\D", String.Empty));
        long lDiagID = Convert.ToInt32(Regex.Replace(strDiagID, "\\D", String.Empty));

        lNewProblemID = -1;
        
        bool bInsertDiag = diagnosis.InsertDiagnosis(
            BaseMstr,
            BaseMstr.SelectedPatientID,
            BaseMstr.SelectedEncounterID,
            BaseMstr.SelectedTreatmentID,
            lDiagID,
            -1,
            String.Empty,
            out lNewProblemID);

        return bInsertDiag;
    }  

    //UPDATE diagnosis
    protected bool UpdateDiagnosis(long lProblemID, long lDiagnosticID, long lSpecifierID, string strA3Text, string strDiagComment, long lSortOrder) 
    {
        bool bUpdtDiagnosis = diagnosis.UpdateDiagnosis(BaseMstr,
                                                        BaseMstr.SelectedPatientID,
                                                        BaseMstr.SelectedEncounterID,
                                                        BaseMstr.SelectedTreatmentID,
                                                        lProblemID,
                                                        lDiagnosticID,
                                                        lSpecifierID,
                                                        strA3Text,
                                                        strDiagComment, 
                                                        lSortOrder);
        return bUpdtDiagnosis;
    }

    //render dropdowns :: sort order
    protected void LoadSortOrderCombos(DropDownList cbo) 
    {
        for (var i = 1; i < 11; i++) 
        {
            ListItem li = new ListItem();
            li.Value = i.ToString();
            li.Text = i.ToString();
            cbo.Items.Add(li);
        }
        cbo.Items.Insert(0, new ListItem("--", "-1"));
    }
    
    protected void repCriteria_OnItemDataBound(object sender, RepeaterItemEventArgs e) 
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            DataRow dr = drv.Row;

            CheckBox chkCriteria = (CheckBox)e.Item.FindControl("chkCriteria");
            
            //set PROBLEM_ID as an attribute
            if (!dr.IsNull("PROBLEM_ID"))
            {
                chkCriteria.InputAttributes.Add("problemid", dr["PROBLEM_ID"].ToString());
            }
            else
            {
                chkCriteria.InputAttributes.Add("problemid", "-1");
            }

            //set CRITERIA_ID as an attribute
            if (!dr.IsNull("CRITERIA_ID"))
            {
                chkCriteria.InputAttributes.Add("criteriaid", dr["CRITERIA_ID"].ToString());
            }
            else
            {
                chkCriteria.InputAttributes.Add("criteriaid", "-1");
            }

            //set STATUS: checked, unchecked
            if (!dr.IsNull("STATUS"))
            {
                chkCriteria.Checked = (Convert.ToInt32(dr["STATUS"]) > 0);
            }
            else 
            {
                chkCriteria.Checked = false;
            }

            if (!dr.IsNull("MID")) 
            { 
                if(Convert.ToInt32(dr["MID"]) == -1)
                {
                    chkCriteria.Visible = false;
                }
            }

            if (BaseMstr.IsPatientLocked) 
            {
                chkCriteria.Enabled = false;
            }
        }
    }
    
    protected void repDefinition_OnItemDataBound(object sender, RepeaterItemEventArgs e) 
    {        
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRow dr = (DataRow)e.Item.DataItem;
            CheckBox chkDef = (CheckBox)e.Item.FindControl("chkCriteriaDef");

            //set PROBLEM_ID as an attribute
            if (!dr.IsNull("PROBLEM_ID"))
            {
                chkDef.InputAttributes.Add("problemid", dr["PROBLEM_ID"].ToString());
            }
            else
            {
                chkDef.InputAttributes.Add("problemid", "-1");
            }

            //set CRITERIA_ID as an attribute
            if (!dr.IsNull("PROBLEM_ID"))
            {
                chkDef.InputAttributes.Add("criteriaid", dr["CRITERIA_ID"].ToString());
            }
            else
            {
                chkDef.InputAttributes.Add("criteriaid", "-1");
            }

            //set DEFINITION_ID as an attribute
            if (!dr.IsNull("DEFINITION_ID"))
            {
                chkDef.InputAttributes.Add("definitionid", dr["DEFINITION_ID"].ToString());
            }
            else
            {
                chkDef.InputAttributes.Add("definitionid", "-1");
            }

            //set STATUS: checked, unchecked
            if (!dr.IsNull("STATUS"))
            {
                long lStatus = Convert.ToInt32(dr["STATUS"]);
                chkDef.Checked = (Convert.ToInt32(dr["STATUS"]) > 0);
            }
            else
            {
                chkDef.Checked = false;
            }

            if (BaseMstr.IsPatientLocked) 
            {
                chkDef.Enabled = false;
            }
        }
    }

    protected void DisableCriteriaCheckboxes()
    {
        foreach (RepeaterItem ri in repCriteria.Items)
        {
            CheckBox chkCriteria = (CheckBox)ri.FindControl("chkCriteria");
            chkCriteria.Enabled = false;

            Repeater repDef = (Repeater)ri.FindControl("repCriteriaDef");
            foreach (RepeaterItem riDef in repDef.Items)
            {
                CheckBox chkCriteriaDef = (CheckBox)riDef.FindControl("chkCriteriaDef");
                chkCriteriaDef.Enabled = false;
            }
        }
    }

    protected void EnableCriteriaCheckboxes()
    {
        foreach (RepeaterItem ri in repCriteria.Items)
        {
            CheckBox chkCriteria = (CheckBox)ri.FindControl("chkCriteria");
            chkCriteria.Enabled = true;

            Repeater repDef = (Repeater)ri.FindControl("repCriteriaDef");
            foreach (RepeaterItem riDef in repDef.Items)
            {
                CheckBox chkCriteriaDef = (CheckBox)riDef.FindControl("chkCriteriaDef");
                chkCriteriaDef.Enabled = true;
            }
        }
    }
      
    //get criteria
    protected void GetCriteria(long lProblemID)
    {
        if (lProblemID == -1)
        {
            repCriteria.DataSource = null;
            repCriteria.DataBind();
            spNoCriteria.Visible = false;
            legendCriteria.InnerText = "Criteria: ";
            return;
        }

        DataSet ds = diagnosis.GetProblemCriteriaDS(BaseMstr, lProblemID);
        DataSet dsDef = diagnosis.GetCriteriaDefinitionsDS(BaseMstr, lProblemID);
        if (ds != null && dsDef != null)
        {
            ds.Tables[0].TableName = "criteria";
            ds.Tables.Add(dsDef.Tables[0].Copy());
            ds.Tables[1].TableName = "criteria_def";

            //a column named "problem_id" is needed in order
            //to re-use the same control for actual problems.

            ds.Tables["criteria"].Columns.Add("PROBLEM_ID", typeof(long));
            ds.Tables["criteria_def"].Columns.Add("PROBLEM_ID", typeof(long));

            ds.Relations.Add("criteria", ds.Tables["criteria"].Columns["criteria_id"], ds.Tables["criteria_def"].Columns["criteria_id"]);
            ds.AcceptChanges();

            repCriteria.DataSource = ds;
            repCriteria.DataBind();

            if (ds.Tables["criteria"].Rows.Count > 0)
            {
                hProbTitle.Visible = false; //disabled, title is appended to the fieldset legend tag
                spProbDescription.Visible = false; //disabled, for now there are no description in DB
                spNoCriteria.Visible = false;
            }
            else
            {
                hProbTitle.Visible = false;
                spProbDescription.Visible = false;
                spNoCriteria.Visible = true;
            }
        }
        else
        {
            hProbTitle.Visible = false;
            spProbDescription.Visible = false;
        }

        htxtSelectedActualProblem.Value = lProblemID.ToString();
    }

    protected void GetTxCriteria(long lProblemID)
    {
        DataSet ds = diagnosis.GetTxProblemCriteriaDS(BaseMstr, lProblemID);
        DataSet dsDef = diagnosis.GetTxProblemCriteriaDefDS(BaseMstr, lProblemID);
        if (ds != null && dsDef != null)
        {
            ds.Tables[0].TableName = "criteria";
            ds.Tables.Add(dsDef.Tables[0].Copy());
            ds.Tables[1].TableName = "criteria_def";
            ds.Relations.Add("criteria", ds.Tables["criteria"].Columns["criteria_id"], ds.Tables["criteria_def"].Columns["criteria_id"]);
            ds.AcceptChanges();

            repCriteria.DataSource = ds;
            repCriteria.DataBind(); 
            
            if (ds.Tables["criteria"].Rows.Count > 0)
            {
                hProbTitle.Visible = false;
                spProbDescription.Visible = false;
                spNoCriteria.Visible = false;
            }
            else
            {
                hProbTitle.Visible = false;
                spProbDescription.Visible = false;
                spNoCriteria.Visible = true;
            }
        }
        else
        {
            hProbTitle.Visible = false;
            spProbDescription.Visible = false;
        }

        htxtSelectedActualProblem.Value = String.Empty;
    }

    protected void CheckRadioButtons(object sender, Repeater repProbList, string strAxisType)
    {
        foreach (RepeaterItem ri in repProbList.Items)
        {
            RadioButton rad = (RadioButton)ri.FindControl("radDiagA" + strAxisType);
            rad.Checked = false;
        }
        if (sender != null)
        {
            RadioButton radBtn = (RadioButton)sender;
            radBtn.Checked = true;
        }
    }

    protected void chkCriteria_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkCriteria = (CheckBox)sender;
        long lStatus = (chkCriteria.Checked) ? 1 : 0;
        long lCriteriaID = Convert.ToInt32(chkCriteria.InputAttributes["criteriaid"]);
        long lProblemID = Convert.ToInt32(chkCriteria.InputAttributes["problemid"]);
        diagnosis.UpdateCriteria(BaseMstr, lProblemID, lCriteriaID, lStatus);
    }

    protected void chkCriteriaDef_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkCriteriaDef = (CheckBox)sender;
        long lStatus = (chkCriteriaDef.Checked) ? 1 : 0;
        long lDefinitionID = Convert.ToInt32(chkCriteriaDef.InputAttributes["definitionid"]);
        long lProblemID = Convert.ToInt32(chkCriteriaDef.InputAttributes["problemid"]);
        long lCriteriaID = Convert.ToInt32(chkCriteriaDef.InputAttributes["criteriaid"]);
        diagnosis.UpdateCriteriaDefinition(BaseMstr, lProblemID, lCriteriaID, lDefinitionID, lStatus);
    }

    protected void GetProblemDescription(long lDiagID) 
    {
        hProbTitle.InnerText = String.Empty;
        spProbDescription.InnerText = string.Empty;
        DataSet dsDiag = diagnosis.GetDiagnosisByIdDS(BaseMstr, lDiagID);

        legendCriteria.InnerText = "Assessment";
    }

    public void AutoSelectProblemInit() 
    {
        lROAssessment = usrsec.GetRightMode(BaseMstr, (long)SUATUserRight.NoteAssessmentUR);
        if (BaseMstr.IsPatientLocked) 
        {
            lROAssessment = (long)RightMode.ReadOnly;
        }

        //The first actual problem (if any) will be selected
        if (repDiagA1.Items.Count > 0)
        {
            RadioButton rad = (RadioButton)repDiagA1.Items[0].FindControl("radDiagA1");
            rad.Checked = true;

            long lProblemID = Convert.ToInt32(rad.Attributes["value"]);
            long lDiagID = Convert.ToInt32(rad.InputAttributes["diagnosisid"]);

            GetTxCriteria(lProblemID);
            GetProblemDescription(lDiagID);

            if (bAllowUpdate && (lROAssessment > (long)RightMode.ReadOnly))
            {
                EnableCriteriaCheckboxes();
            }
            else
            {
                DisableCriteriaCheckboxes();
            }
            return;
        }
    }

    protected void SelectNewProblem(long lNewProblemID, long lAxis) 
    {
        if (!BaseMstr.GetVSBoolValue("PROBLEM_AUTO_SELECT"))
        {
            Repeater[] repActualProblems = { null, repDiagA1};
            string[] strRadiosActual = { String.Empty, "radDiagA1" }; 

            string strActualRadio = strRadiosActual[lAxis];
            Repeater repActual = repActualProblems[lAxis];

            foreach (RepeaterItem ri in repActual.Items)
            {
                RadioButton rad = (RadioButton)ri.FindControl(strActualRadio);
                long lProblemID = Convert.ToInt32(rad.Attributes["value"]);
                long lDiagID = Convert.ToInt32(rad.InputAttributes["diagnosisid"]);

                if (lProblemID == lNewProblemID)
                {
                    //clear all possible problems radios
                    for (int b = 1; b < 2; b++)
                    {
                        CheckRadioButtons(null, repActualProblems[b], b.ToString());
                    }
                    
                    CheckRadioButtons(rad, repActual, lAxis.ToString());
                    rad.Checked = true;

                    GetTxCriteria(lNewProblemID);
                    GetProblemDescription(lDiagID);
                    EnableCriteriaCheckboxes();

                    break; 
                }
            }
        }
    }

    //DISCONTINUE DIAGNOSIS ITEM
    protected void DiscDiagItem_OnClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        long lProbelmID = Convert.ToInt32(htxtSelectedActualProblem.Value);

        bool bDiscDiagItem = diagnosis.DiscontinueDiagnosis(BaseMstr, lProbelmID, txtDiscDiag.Text.Trim());

        if (bDiscDiagItem)
        {
            BaseMstr.SetVSValue("PROBLEM_AUTO_SELECT", false);
            GetCriteria(-1);
            
            Session["PROBLEMS_LIST_DS"] = null;
            Load_Assessment();

            //bubble up the event so someone using 
            //the control can check to see if we discontinued item
            RaiseBubbleEvent(this, e);
        }
    }
}
