using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class patient_assessment : System.Web.UI.Page
{
    protected const int _DAYS_PRIOR = 3;
    protected const int _BASELINE_INTAKES_EVENT = 5;

    public bool HasWeightHeight = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Redirect to Login page if user is not logged in
        if (!Master.IsLoggedIn()) 
        {
            Server.Transfer("Default.aspx");
        }

        if (!IsPostBack)
        {
            //flush SelectedEncounterID from previous intakes assignments
            Master.SelectedEncounterID = String.Empty;
        }

        // get or create Self Management (type 99) encounter
        GetCreateSelfMgntEncounter(Master);

        if (!IsPostBack)
        {
            GetPatientDetails();
            HasCompletedFollowUp();

            pnlPatDetails.Visible = (HasPendingModules(Master) && !String.IsNullOrEmpty(Master.SelectedEncounterID));
            pnlInstructions.Visible = (HasPendingModules(Master) && !String.IsNullOrEmpty(Master.SelectedEncounterID));
        }

        // render the modules list
        divModuleGroups.InnerHtml = GetModuleGroupHTML();

        
    }

    protected string GetModuleGroupHTML() {
        CIntake intake = new CIntake();
        string strHTML = String.Empty;

        bool bInsertHeader = false;
        long lModGroup = -1;

        DataSet dsIntakes = intake.GetPatIntakeAssignedDS(Master);

        if (dsIntakes == null) return String.Empty;
        if (dsIntakes.Tables[0].Rows.Count < 1) return "<h4>You don't have questionnaires assigned at this moment!</h4>";

        for (int a = 0; a < dsIntakes.Tables[0].Rows.Count; a++) {
            DataRow dr = dsIntakes.Tables[0].Rows[a];

            bool bShowModule = true;
            // This date calculation is now handled directly in the stored procedure

            //if (!dr.IsNull("SCHEDULED_DATE"))
            //{
            //    if (DateTime.Today < Convert.ToDateTime(dr["SCHEDULED_DATE"]).AddDays(-_DAYS_PRIOR))
            //    {
            //        bShowModule = false;
            //    }
            //}


            // ---------

            if (bShowModule)
            {
                if (!dr.IsNull("MODULE_GROUP_ID"))
                {
                    long lMG = Convert.ToInt32(dr["MODULE_GROUP_ID"]);
                    if (lModGroup != lMG)
                    {
                        lModGroup = lMG;
                        bInsertHeader = true;
                    }
                    else
                    {
                        bInsertHeader = false;
                    }

                    //write header
                    if (bInsertHeader)
                    {
                        strHTML += "<div class=\"module-group\">";
                        if (!dr.IsNull("MODULE_GROUP_DESCR"))
                        {
                            strHTML += String.Format("<h2>{0}</h2>", dr["MODULE_GROUP_DESCR"].ToString());
                        }
                        strHTML += "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
                    }

                    //write modules list
                    string strTRClass = "class=\"pending\"";
                    long lStatus = 0;
                    if (!dr.IsNull("STATUS"))
                    {
                        lStatus = Convert.ToInt32(dr["STATUS"]);
                        strTRClass = (lStatus != 0) ? String.Empty : "class=\"pending\"";
                    }

                    if (lStatus != 2) //skipped module
                    {
                        strHTML += "<tr id=\"trmid_" + dr["MID"].ToString() + "\" " + strTRClass + ">";
                        strHTML += "<td>";
                        if (lStatus == 1)
                        {
                            strHTML += "<img alt=\"module done\" src=\"Images/tick.png\">";
                        }
                        else
                        {
                            strHTML += "<img alt=\"module not-done\" src=\"Images/error.png\">";
                        }
                        strHTML += "</td>";
                        strHTML += "<td>";
                        if (lStatus == 1)
                        {
                            strHTML += "<span>";
                        }
                        else
                        {
                            strHTML += "<a ";
                            strHTML += "href=\"mid" + dr["MID"].ToString() + ".aspx?grp=" + dr["MODULE_GROUP_ID"].ToString() + "\" >";
                        }
                        strHTML += dr["MODULE"].ToString();
                        if (lStatus == 1)
                        {
                            strHTML += "</span>";
                        }
                        else
                        {
                            strHTML += "</a>";
                        }
                        strHTML += "</td>";
                        strHTML += "</tr>";
                    }

                    //write footer
                    if (a == dsIntakes.Tables[0].Rows.Count - 1)
                    {
                        strHTML += "</table>";
                        strHTML += "</div>";
                    }
                    else
                    {
                        if (a < dsIntakes.Tables[0].Rows.Count - 1)
                        {
                            if (Convert.ToInt32(dsIntakes.Tables[0].Rows[a + 1]["MODULE_GROUP_ID"]) != lModGroup)
                            {
                                strHTML += "</table>";
                                strHTML += "</div>";
                            }
                        }
                    }
                } 
            }

            // ---------
        }
        
        return strHTML;
    }

    protected void GetCreateSelfMgntEncounter(BaseMaster BaseMstr) 
    {
        CDataUtils utils = new CDataUtils();
        CEncounter enc = new CEncounter();
        DataSet dsEnc = enc.GetSelfMgntEncounterDS(BaseMstr);
        string strNewEncounterID;

        if (dsEnc != null)
        {
            string strEncounterID = utils.GetStringValueFromDS(dsEnc, "ENCOUNTER_ID");
            if (!String.IsNullOrEmpty(strEncounterID))
            {
                BaseMstr.SelectedEncounterID = strEncounterID;

                //Check if the module group is complete -> trigger event
                CheckModuleGroupStatus(BaseMstr);

                //If no more pending modules, close the encounter
                enc.CloseSelfMgntEncounter(BaseMstr, BaseMstr.SelectedEncounterID);
            }
            else
            {
                if (HasPendingModules(Master))
                {
                    if (enc.CreateSelfMgntEncounter(BaseMstr, out strNewEncounterID))
                    {
                        BaseMstr.SelectedEncounterID = strNewEncounterID;
                    }
                }
            }
        }
        else
        {
            if (HasPendingModules(Master))
            {
                if (enc.CreateSelfMgntEncounter(BaseMstr, out strNewEncounterID))
                {
                    BaseMstr.SelectedEncounterID = strNewEncounterID;
                } 
            }
        }
    }

    protected void CheckModuleGroupStatus(BaseMaster BaseMstr) 
    {
        CPatientEvent evt = new CPatientEvent(BaseMstr);

        CEncounter enc = new CEncounter();
        DataSet dsGrps = enc.GetModuleGroupStatusDS(BaseMstr);
        DataSet dsEvts = evt.GetPatientEventsDS();
        bool bRedirectToBaselineScreen = false;

        if (dsGrps != null) 
        {
            foreach (DataTable dt in dsGrps.Tables) 
            {
                foreach (DataRow dr in dt.Rows) 
                {
                    long lEventID = 0;
                    long lPending = 1;
                    
                    if (!dr.IsNull("EVENT_ID")) 
                    {
                        lEventID = Convert.ToInt32(dr["EVENT_ID"]);
                    }

                    if (!dr.IsNull("PENDING"))
                    {
                        lPending = Convert.ToInt32(dr["PENDING"]);
                    }

                    if (lPending == 0 && lEventID != 0) 
                    {
                        if (lEventID == _BASELINE_INTAKES_EVENT)
                        {
                            bRedirectToBaselineScreen = !IsBaselineMarkedCompleted(dsEvts, _BASELINE_INTAKES_EVENT);
                        }
                        evt.CompletedEvent(lEventID);
                    }
                }
            }

            if (bRedirectToBaselineScreen) 
            {
                Response.Redirect("portal_start.aspx");
            }
        }
    }

    protected bool HasPendingModules(BaseMaster BaseMstr) 
    {
        long lPending = 0;
        CEncounter enc = new CEncounter();
        DataSet dsEnc = enc.GetModuleGroupStatusDS(BaseMstr);
        if (dsEnc != null)
        {
            foreach (DataTable dt in dsEnc.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull("PENDING"))
                    {
                        lPending += Convert.ToInt32(dr["PENDING"]);
                    }
                }
            }
        }
        return (lPending > 0);
    }
    
    protected void btnUpdateInfo_Click(object sender, EventArgs e)
    {
        CEncounter enc = new CEncounter();
        long lInches = 0;
        long lFeet = 0;
        long lPatHeight = 0;
        long lPatWeight = 0;

        if (cboFeet.SelectedIndex > 0)
        {
            lFeet = Convert.ToInt32(cboFeet.SelectedValue) * 12;
        }

        if (cboInches.SelectedIndex > 0) 
        {
            lInches = Convert.ToInt32(cboInches.SelectedValue);
        }

        lPatHeight = lFeet + lInches;
        Master.PatientHeight = lPatHeight;

        if (txtWeight.Text.Trim().Length > 0)
        {
            lPatWeight = Convert.ToInt32(txtWeight.Text.Trim());
        }

        Master.PatientWeight = lPatWeight;

        //update patient details
        enc.UpdatePatientDetails(Master, Master.SelectedEncounterID, lPatHeight, lPatWeight);

        GetPatientDetails();

        ScriptManager.RegisterStartupScript(upUpdatePatInfo, typeof(string), "updateinfo", "var hasWightHeight = " + HasWeightHeight.ToString().ToLower() + ";", true);

    }

    protected void GetPatientDetails() 
    {
        trEditInfo.Visible = true;
        trReadInfo.Visible = false;
        
        Session["PATIENT_DOB"] = null;
        Session["PATIENT_AGE"] = null;
        Session["PATIENT_GENDER"] = null;
        Session["PATIENT_WEIGHT"] = null;
        Session["PATIENT_HEIGHT"] = null;

        CDataUtils utils = new CDataUtils();
        CPatient patient = new CPatient();
        CEncounter enc = new CEncounter();

        DataSet dsPat = patient.GetPatientDemographicsDS(Master);
        if (dsPat != null) 
        {
            Master.PatientDOB = Convert.ToDateTime(utils.GetDateValueAsStringFromDS(dsPat, "DOB"));
            Master.PatientAge = utils.GetLongValueFromDS(dsPat, "PATIENT_AGE");
            Master.PatientGender = utils.GetStringValueFromDS(dsPat, "GENDER");
        }

        DataSet dsEnc = enc.GetPatientDetailsDS(Master, Master.SelectedEncounterID);
        if (dsEnc != null) 
        {
            long lPatHeight = utils.GetLongValueFromDS(dsEnc, "PATIENT_HEIGHT");
            if (lPatHeight > 0)
            {
                int iFeet = (int)lPatHeight / 12;
                int iInches = (int)lPatHeight % 12;

                lblPatHeight.Text = iFeet.ToString() + "' - " + iInches.ToString() + "\"";
                htxtHeightFeet.Value = iFeet.ToString();
                htxtHeightInches.Value = iInches.ToString();

                Master.PatientHeight = lPatHeight;

                foreach (ListItem li in cboFeet.Items)
                {
                    if (li.Value == iFeet.ToString())
                    {
                        li.Selected = true;
                    }
                }

                foreach (ListItem li in cboInches.Items)
                {
                    if (li.Value == iInches.ToString())
                    {
                        li.Selected = true;
                    }
                }
            }
            else 
            {
                lblPatHeight.Text = String.Empty;
                htxtHeightFeet.Value = String.Empty;
                htxtHeightInches.Value = String.Empty;

                cboFeet.SelectedIndex = 0;
                cboInches.SelectedIndex = 0;

                Master.PatientHeight = 0;
            }

            long lPatWeight = utils.GetLongValueFromDS(dsEnc, "PATIENT_WEIGHT");
            if (lPatWeight > 0)
            {
                txtWeight.Text = lPatWeight.ToString();

                lblPatWeight.Text = lPatWeight.ToString() + " pounds";
                htxtWeightPounds.Value = lPatWeight.ToString();

                Master.PatientWeight = lPatWeight;
            }
            else
            {
                txtWeight.Text = String.Empty;

                lblPatWeight.Text = String.Empty;
                htxtWeightPounds.Value = String.Empty;

                Master.PatientWeight = 0;
            }

            if (lPatHeight > 0 && lPatWeight > 0)
            {
                trEditInfo.Visible = false;
                trReadInfo.Visible = true;
                HasWeightHeight = true;
            }
            else 
            {
                trEditInfo.Visible = true;
                trReadInfo.Visible = false;
                HasWeightHeight = false;
            }
        }

    }

    protected void ShowEditRow(object sender, EventArgs e) 
    {
        trEditInfo.Visible = true;
        trReadInfo.Visible = false;
    }

    protected void CancelEditInfo(object sender, EventArgs e) 
    {

        if (String.IsNullOrEmpty(htxtHeightFeet.Value))
        {
            cboFeet.SelectedIndex = 0;
        }
        else
        {
            cboFeet.SelectedValue = htxtHeightFeet.Value.Trim();
        }


        if (String.IsNullOrEmpty(htxtHeightInches.Value))
        {
            cboInches.SelectedIndex = 0;
        }
        else
        {
            cboInches.SelectedValue = htxtHeightInches.Value.Trim();
        }

        if (String.IsNullOrEmpty(htxtWeightPounds.Value)) 
        {
            txtWeight.Text = String.Empty;
        } 
        else 
        {
            txtWeight.Text = htxtWeightPounds.Value.Trim();
        }

        trEditInfo.Visible = false;
        trReadInfo.Visible = true;
    }

    protected bool IsBaselineMarkedCompleted(DataSet dsEvents, int iEventID) 
    {
        long lNotificationSent = 0;
        if (dsEvents != null)
        {
            DataRow dr = dsEvents.Tables[0].Select("event_id = 5")[0];
            if(dr != null)
            {
                if(!dr.IsNull("NOTIFICATION_SENT"))
                {
                    lNotificationSent = Convert.ToInt32(dr["NOTIFICATION_SENT"]);
                }
            }
        }
        return (lNotificationSent > 0);
    }

    protected void HasCompletedFollowUp() 
    {
        Session["COMPLETED_FOLLOWUP"] = null;
        Regex reREF = new Regex("mid\\d*.aspx", RegexOptions.IgnoreCase);

        if (reREF.IsMatch(Request.UrlReferrer.ToString()))
        {
            string strPath = Request.UrlReferrer.ToString();
            string strGroup = strPath.Substring(strPath.IndexOf("grp=") + 4);
            
            string strFollowUpGrps = ",3,5,7,";
            string strCSQGrps = ",2,4,6,8,";

            CPatientEvent evt = new CPatientEvent(Master);
            CEncounter enc = new CEncounter();
            DataSet dsGrps = enc.GetModuleGroupStatusDS(Master);

            if (dsGrps != null)
            {
                foreach (DataTable dt in dsGrps.Tables)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        long lMIDGRP = 0;
                        long lPending = 1;

                        if (!dr.IsNull("MODULE_GROUP_ID"))
                        {
                            lMIDGRP = Convert.ToInt32(dr["MODULE_GROUP_ID"]);
                        }

                        if (!dr.IsNull("PENDING"))
                        {
                            lPending = Convert.ToInt32(dr["PENDING"]);
                        }

                        if (lPending == 0 && lMIDGRP != 0)
                        {
                            if (strGroup.Length > 0)
                            {
                                
                                string strCurrGroup = "," + strGroup + ",";

                                if (lMIDGRP == Convert.ToInt32(strGroup) && (strFollowUpGrps.IndexOf(strCurrGroup) > -1))
                                {
                                    Session["COMPLETED_FOLLOWUP"] = 1;
                                    Response.Redirect("portal_start.aspx");
                                }
                                else if (lMIDGRP == Convert.ToInt32(strGroup) && (strCSQGrps.IndexOf(strCurrGroup) > -1))
                                {
                                    Session["COMPLETED_FOLLOWUP"] = 2;
                                    Response.Redirect("portal_start.aspx");
                                }
                            }
                        }
                    }
                }
            }

            if (!HasPendingModules(Master)) 
            {
                Response.Redirect("portal_start.aspx");
            }
        }
    }
}