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

public partial class change_password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            LoadSecQuestions();
            lblUID.Text = (string)Session["USER_NAME"];

        }
    }

    protected void LoadSecQuestions()
    {
        CSecQuestions sec = new CSecQuestions(Master);

        DataSet dsQuest1 = sec.GetSecQuestionsRS(1);
        DataSet dsQuest2 = sec.GetSecQuestionsRS(2);

        cboQuestion1.DataSource = dsQuest1;
        cboQuestion1.DataTextField = "QUESTION";
        cboQuestion1.DataValueField = "QUESTION_ID";
        cboQuestion1.DataBind();

        cboQuestion1.Items.Insert(0, new ListItem("--Select Question--", "-1"));

        cboQuestion2.DataSource = dsQuest2;
        cboQuestion2.DataTextField = "QUESTION";
        cboQuestion2.DataValueField = "QUESTION_ID";
        cboQuestion2.DataBind();

        cboQuestion2.Items.Insert(0, new ListItem("--Select Question--", "-1"));
    }

    protected void btnChangePWD_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtNewP.Text)
            || string.IsNullOrEmpty(txtVNewP.Text)
            || string.IsNullOrEmpty(txtOldP.Text))
        {
            Master.StatusCode = 1;
            Master.StatusComment = "Password entries are empty!";
            ShowSysFeedback();
            return;
        }

        if (txtNewP.Text != txtVNewP.Text)
        {
            Master.StatusCode = 1;
            Master.StatusComment = "New Password and Verify Password do not match!";
            ShowSysFeedback();
            return;
        }

        if (pnlSecQuestions.Visible)
        {

            if (cboQuestion1.SelectedIndex < 1
                || cboQuestion2.SelectedIndex < 1
                || txtAnswer1.Text.Trim().Length < 1
                || txtAnswer2.Text.Trim().Length < 1)
            {
                Master.StatusCode = 1;
                Master.StatusComment = "Please select two challenge questions and enter the corresponding answers!";
                ShowSysFeedback();
                return;
            }
        }

        long lStatusCode = 0;
        string strStatusComment = string.Empty;

        //validate the password rules
        CSec sec = new CSec();
        if (!sec.ValidateUserAccountRules(Master, (string)Session["USER_NAME"], txtNewP.Text))
        {
            Master.StatusCode = lStatusCode;
            Master.StatusComment = strStatusComment;
            ShowSysFeedback();
            return;
        }

        //all good so far, change the pwd, login and redirect
        lStatusCode = sec.ChangePassword(Master, (string)Session["USER_NAME"], txtOldP.Text, txtNewP.Text); 

        if (lStatusCode != 0)
        {
            Master.StatusCode = lStatusCode;
            Master.StatusComment = strStatusComment;
            ShowSysFeedback();
            return;
        }

        //update security challenge questions & answers
        CSecQuestions secquest = new CSecQuestions(Master);
        if (!secquest.UpdateSecQuestions(Convert.ToInt32(cboQuestion1.SelectedValue),
                                    txtAnswer1.Text.Trim(),
                                    Convert.ToInt32(cboQuestion2.SelectedValue),
                                    txtAnswer2.Text.Trim(),
                                    -1,
                                    String.Empty))
        {
            Master.StatusCode = lStatusCode;
            Master.StatusComment = strStatusComment;
            ShowSysFeedback();
            return;
        }

        //if we get here we have successfully changed the password
        //now login with the new account
        if (sec.Login(Master, (string)Session["USER_NAME"], txtNewP.Text) != 0)
        {
            Master.StatusCode = lStatusCode;
            Master.StatusComment = strStatusComment;
            ShowSysFeedback();
            return;
        }

        Master.StatusCode = lStatusCode;
        Master.StatusComment = strStatusComment;

        CPatient pat = new CPatient();
        CDataUtils utils = new CDataUtils();
        DataSet dsPat = pat.GetPatientIDRS(Master, Master.FXUserID);
        Master.SelectedPatientID = utils.GetDSStringValue(dsPat, "PATIENT_ID");

        CPatientEvent evt = new CPatientEvent(Master);
        evt.CompletedEvent(1);


        ShowSysFeedback();

        //successful login so clear txt boxes
        lblUID.Text = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        txtOldP.Text = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        txtNewP.Text = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        txtVNewP.Text = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        lblUID.Text = string.Empty;
        txtOldP.Text = string.Empty;
        txtNewP.Text = string.Empty;
        txtVNewP.Text = string.Empty;
        Session["USER_NAME"] = null;

        //set a session variable with the login time
        Session["SESSION_INITIATED"] = DateTime.Now;

        //redirect, we are now logged in
        //Master.Response.Redirect("portal_revamp.aspx");
        Master.Response.Redirect("portal_start.aspx");

    }

    protected void ShowSysFeedback()
    {
        if (Master.StatusCode > 0 && !String.IsNullOrEmpty(Master.StatusComment))
        {
            HtmlContainerControl div = (HtmlContainerControl)Master.FindControl("divSysFeedback");
            div.InnerHtml = Master.StatusComment;
            ScriptManager.RegisterStartupScript(upChangePWD, typeof(string), "showAlert", "Ext.onReady(function(){winSysFeedback.show();})", true);
            Master.ClearStatus();
        }
    }
}