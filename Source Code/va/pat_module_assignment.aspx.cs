using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class pat_Module_Assignment : System.Web.UI.Page
{
    public BaseMaster BaseMstr { get; set; }
    public bool bReadOnly;

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
        CIntake intake = new CIntake();
        CSec usrsec = new CSec();
        bReadOnly = (usrsec.GetRightMode(Master, (long)SUATUserRight.ProcessNewPatientsUR) < (long)RightMode.ReadWrite);
        if (Master.IsPatientLocked) 
        {
            bReadOnly = true;
        }

        ucIntakeModules.BaseMstr = Master;
        ucIntakeModules.bReadOnly = bReadOnly;

        if (!Master.APPMaster.PatientHasOpenCase)
        {
            Master.StatusCode = 1;
            Master.StatusComment = "Patient Does Not Have An Open Case! An Open Case Is Required To Assign Assessments.";
            return;
        }

        if (!IsPostBack) 
        {
            ucIntakeModules.LoadModuleGroups();
            htxtSelectedModules.Value = ucIntakeModules.GetPatientModules();
        }

        if (Master.OnMasterSAVE())
        {
            string strSelectedModules = ucIntakeModules.GetAssignedModules();
            if (!bReadOnly && !Master.IsPatientLocked)
            {
                if (intake.AssignPatientModules(Master, Master.SelectedPatientID, Master.SelectedProviderID, strSelectedModules))
                {
                    htxtSelectedModules.Value = strSelectedModules;
                    divStatus.InnerHtml = "<font color=\"green\"><img alt=\"\" src=\"Images/tick.png\">&nbsp;Assigned modules were saved for the patient!</font>";
                    ScriptManager.RegisterClientScriptBlock(upWrapperUpdatePanel, typeof(string), "saved", "clearStatusDiv(4);", true);
                }
            }
            else
            {
                if (bReadOnly)
                {
                    Master.StatusCode = 1;
                    Master.StatusComment = "<img alt=\"\" src=\"Images/lock16x16.png\" /> You have <b>Read-Only Access</b> to this section."; 
                }
                else if(Master.IsPatientLocked)
                {
                    Master.StatusCode = 1;
                    Master.StatusComment = "<img alt=\"\" src=\"Images/lock16x16.png\" /> <b>Read-Only Access</b>: The patient's record is in use by " + Session["PAT_LOCK_PROVIDER"].ToString() + ".";
                }
            }
        }

        ShowSysFeedback();
    }

    protected void ShowSysFeedback()
    {
        if (Master.StatusCode > 0 && !String.IsNullOrEmpty(Master.StatusComment))
        {
            HtmlContainerControl div = (HtmlContainerControl)Master.FindControl("divSysFeedback");
            div.InnerHtml = Master.StatusComment;
            ScriptManager.RegisterStartupScript(upWrapperUpdatePanel, typeof(string), "showAlert", "Ext.onReady(function(){winSysFeedback.show();})", true);
            Master.ClearStatus();
        }
    }

}
