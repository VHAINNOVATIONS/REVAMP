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

public partial class portal_revamp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack && Master.SelectedPatientID == "") 
        {
            GetPatientID();
        }

        CDataUtils utils = new CDataUtils();
        CCPAPResults cpap = new CCPAPResults(Master);
        CIntake intake = new CIntake();

        if (!IsPostBack)
        {
            //load questionnaires dropdown
            //cpap.LoadQuestionnaireCombo(cboQuestionnaireScores);

            //get graphic raw data
            htxtTxAdherence.Value = cpap.GetTxAdherence();
            htxtAHI.Value = cpap.GetAHI();
            htxtMaskLeak.Value = cpap.GetMaskLeak();
            htxtQuestionnaires.Value = utils.GetJSONString(intake.GetPatIntakeScoresDS(Master));
        }

        cboSummaryTimeWindow.Attributes.Add("onchange", "patient.summary.timewindow(this);");
        //cboQuestionnaireScores.Attributes.Add("onchange", "patient.summary.renderQuestionnaires(this);");

        ShowSysFeedback();
    }
    protected void ShowSysFeedback()
    {
        if (Master.StatusCode > 0 && !String.IsNullOrEmpty(Master.StatusComment))
        {
            HtmlContainerControl div = (HtmlContainerControl)this.Master.FindControl("divSysFeedback");
            div.InnerHtml = Master.StatusComment;
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "showAlert", "Ext.onReady(function(){winSysFeedback.show();})", true);
            Master.ClearStatus();
        }
    }


    protected void GetPatientID() {
        CPatient pat = new CPatient();
        CDataUtils utils = new CDataUtils();
        DataSet dsPat = pat.GetPatientIDRS(Master, Master.FXUserID);
        string strPatientID = utils.GetDSStringValue(dsPat, "PATIENT_ID");
        Master.SelectedPatientID = strPatientID;
    }
}
