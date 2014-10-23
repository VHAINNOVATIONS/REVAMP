using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class portal_start : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckPAPDataStatus();
        CheckPatientStep();
    }

    protected void CheckPatientStep()
    {
        CPatientTxStep patstep = new CPatientTxStep(Master);
        patstep.UpdatePatientSteps();

        bool bHasPendingQuestionnaires = false;

        //check if patient have peding follow-up questionnaires
        CIntake intake = new CIntake();
        DataSet dsIntakes = intake.GetPatIntakeAssignedDS(Master);
        if (dsIntakes != null) 
        {
            DataRow[] drs = dsIntakes.Tables[0].Select("STATUS = 0");
            bHasPendingQuestionnaires = (drs.GetLength(0) > 0);
        }
        
        //completed profile
        if ((Master.PatientTxStep & (long)PatientStep.SavedProfile) == 0)
        {

            //Redirect patient to the profile page
            Response.Redirect("pat_profile.aspx", true);
        }
        else
        {
            //check if patient is redirected here by completing follow up questionnaires
            if (Session["COMPLETED_FOLLOWUP"] != null) 
            {
                if (Convert.ToInt32(Session["COMPLETED_FOLLOWUP"]) == 1) 
                {
                    Session["COMPLETED_FOLLOWUP"] = null;
                    mvPortalSteps.SetActiveView(vCompletedFollowUp);
                    return;
                }
                else if (Convert.ToInt32(Session["COMPLETED_FOLLOWUP"]) == 2)
                {
                    Session["COMPLETED_FOLLOWUP"] = null;
                    mvPortalSteps.SetActiveView(vCompletedCSQ);
                    return;
                }
            }
            
            //Step 2 check (USER_STORY #2851)
            
            //if patient has not started or completed the baseline questionnaires
            if((Master.PatientTxStep & (long)PatientStep.CompletedBaseline) == 0) 
            {
                if ((Master.PatientTxStep & (long)PatientStep.StartedBaseline) == 0)
                {
                    //show "Getting Started" view
                    if (Request.UrlReferrer != null)
                    {
                        string strRefPage = Request.UrlReferrer.ToString().ToLower();
                        if (strRefPage.IndexOf("pat_profile.aspx") > -1)
                        {
                            spSubmitTxt.Visible = true;
                        } 
                    }

                    mvPortalSteps.SetActiveView(vStep2);
                    return;
                }
                else
                {
                    //show complete baseline questionnaire view
                    mvPortalSteps.SetActiveView(vStartedBaseline);
                    return;
                }
            }

            //if patient has not competed videos
            if ((Master.PatientTxStep & (long)PatientStep.CompletedVideos) == 0)
            {
                if ((Master.PatientTxStep & (long)PatientStep.StartedVideos) == 0)
                {
                    if (Request.UrlReferrer != null)
                    {
                        //show videos message
                        string strRefPage = Request.UrlReferrer.ToString().ToLower();
                        strRefPage = strRefPage.Substring(strRefPage.LastIndexOf('/'));
                        Regex reREF = new Regex("mid\\d*.aspx", RegexOptions.IgnoreCase);

                        bool bFromQuestionnaire = reREF.IsMatch(strRefPage);

                        if (strRefPage.IndexOf("patient_assessment.aspx") > -1 || bFromQuestionnaire)
                        {
                            pQCongratulations.Visible = true;
                            pQuestWelcome.Visible = false;
                        }
                    }

                    mvPortalSteps.SetActiveView(vCompletedBaselineQ);
                    return;
                }
                else
                {
                    //show started videos view
                    mvPortalSteps.SetActiveView(vStartedVideos);
                    return;
                }
            }

            if ((Master.PatientTxStep & (long)PatientStep.HasCPAPData) == 0)
            {
                if ((Master.PatientTxStep & (long)PatientStep.CompletedVideos) > 0 
                    && (Master.NotificationTxStep & (long)PatientStep.CompletedVideos) == 0)
                {
                    if (!bHasPendingQuestionnaires)
                    {
                        if (Request.UrlReferrer != null)
                        {
                            string strRefPage = Request.UrlReferrer.ToString().ToLower();
                            strRefPage = strRefPage.Substring(strRefPage.LastIndexOf('/'));

                            if (strRefPage.IndexOf("education.aspx") > -1)
                            {
                                pVidWelcome.Visible = false;
                                //spVidCongratulations.Visible = true;
                            }
                        }

                        patstep.UpdateNotificationSteps((long)PatientStep.CompletedVideos);
                        mvPortalSteps.SetActiveView(vCompletedVideos);
                        return;
                    }
                    else
                    {
                        patstep.UpdateNotificationSteps((long)PatientStep.CompletedVideos);
                        GetListPendingQuestionnaires(dsIntakes, bHasPendingQuestionnaires);
                        mvPortalSteps.SetActiveView(vPendingQuestionnaires);
                        return;
                    }
                }
                else if ((Master.PatientTxStep & (long)PatientStep.CompletedVideos) > 0
                    && (Master.NotificationTxStep & (long)PatientStep.CompletedVideos) > 0)
                {
                    if (bHasPendingQuestionnaires)
                    {
                        GetListPendingQuestionnaires(dsIntakes, bHasPendingQuestionnaires);
                        mvPortalSteps.SetActiveView(vPendingQuestionnaires);
                        return;
                    }
                }
            }

            if (bHasPendingQuestionnaires) 
            {
                GetListPendingQuestionnaires(dsIntakes, bHasPendingQuestionnaires);
                mvPortalSteps.SetActiveView(vPendingQuestionnaires);
                return;
            }

            //Redirect patient to treatment results page
            Response.Redirect("portal_revamp.aspx");
        }
    }

    protected void CheckPAPDataStatus() 
    {
        CPatientEvent evt = new CPatientEvent(Master);
        evt.CheckPAPEvent();
    }

    protected void GetListPendingQuestionnaires(DataSet ds, bool bPendingQ) 
    {
        //check if patient have peding follow-up questionnaires
        string strQuestionnaireList = String.Empty;

        if (ds != null)
        {
            if (bPendingQ)
            {
                DataRow[] drs = ds.Tables[0].Select("STATUS = 0");
                
                string strList = String.Empty;
                string strTitle = String.Empty;
                int a = 0;

                foreach (DataRow dr in drs) 
                {
                    if (strTitle != dr["MODULE_GROUP_DESCR"].ToString())
                    {
                        strTitle = dr["MODULE_GROUP_DESCR"].ToString();
                        strList += strTitle + "|";
                    }
                }

                if (strList.Length > 1) 
                {
                    strList = strList.Substring(0, strList.Length - 1);
                }

                string[] strQList = strList.Split('|');

                if (strQList.GetLength(0) > 0)
                {
                    strQuestionnaireList += "<p><ul style=\"list-style: disc; margin-left: 45px; margin-top: 15px;\">";
                    foreach (string str in strQList)
                    {
                        strQuestionnaireList += String.Format("<li>{0}</li>", str);
                    }
                    strQuestionnaireList += "</ul></p>";
                    litQuestionnaireList.Text = strQuestionnaireList;
                }
            }
        }
    }
}