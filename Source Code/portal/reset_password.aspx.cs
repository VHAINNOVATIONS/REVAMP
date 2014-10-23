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

public partial class reset_password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        CSec sec = new CSec();
        CSecQuestions secquest = new CSecQuestions(Master);

        if (pnlAccntDetails.Visible)
        {

            ViewState["FX_USER_ID"] = null;
            bool bIsLocked = false;
            bool bIPLocked = false;

            if (txtUserName.Text.Trim().Length < 1)
            {
                return;
            }

            string strUsername = sec.Enc(txtUserName.Text.Trim(), String.Empty);

            DataSet dsQuest = secquest.GetUserQuestions(strUsername);

            if (dsQuest != null)
            {
                foreach (DataTable dt in dsQuest.Tables)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!dr.IsNull("QUESTION_1"))
                        {
                            lblQuestion1.Text = dr["QUESTION_1"].ToString();
                            txtAnswer1.Text = String.Empty;
                        }

                        if (!dr.IsNull("QUESTION_2"))
                        {
                            lblQuestion2.Text = dr["QUESTION_2"].ToString();
                            txtAnswer2.Text = String.Empty;
                        }

                        if (!dr.IsNull("QUESTION_2"))
                        {
                            lblQuestion2.Text = dr["QUESTION_2"].ToString();
                            txtAnswer2.Text = String.Empty;
                        }

                        if (!dr.IsNull("FX_USER_ID"))
                        {
                            ViewState["FX_USER_ID"] = Convert.ToInt32(dr["FX_USER_ID"]);
                        }

                        if (!dr.IsNull("IS_LOCKED"))
                        {
                            bIsLocked = Convert.ToInt32(dr["IS_LOCKED"]) == 1;
                        }

                        if (!dr.IsNull("IP_LOCKED"))
                        {
                            bIPLocked = Convert.ToInt32(dr["IP_LOCKED"]) == 1;
                        }
                    }
                }

                if (dsQuest.Tables[0].Rows.Count > 0)
                {
                    if (!bIsLocked)
                    {
                        bool bConfirmedAccnt = true;

                        if (Convert.ToInt32(ViewState["FX_USER_ID"]) == 0)
                        {
                            Master.StatusCode = 1;
                            Master.StatusComment = "The Username you entered is incorrect.";
                            bConfirmedAccnt = false;
                        }
                        else if (Convert.ToInt32(ViewState["FX_USER_ID"]) > 0
                            && (lblQuestion1.Text.Length < 1 ||
                            lblQuestion2.Text.Length < 1))
                        {
                            Master.StatusCode = 1;
                            Master.StatusComment = "You have not yet selected security questions for your portal account.";
                            bConfirmedAccnt = false;
                        }

                        if (bConfirmedAccnt)
                        {
                            pnlAccntDetails.Visible = false;
                            pnlSecQuestions.Visible = true;
                        }
                        else
                        {
                            pnlAccntDetails.Visible = true;
                            pnlSecQuestions.Visible = false;
                            ShowSysFeedback();
                        }
                    }
                    else
                    {
                        pnlAccntDetails.Visible = true;
                        pnlSecQuestions.Visible = false;
                        Master.StatusCode = 9; //9: account is locked
                        Master.StatusComment = "Your account has been locked. Please contact the system administrator to reactivate your login.";
                        ShowSysFeedback();
                    }
                }
                else
                {
                    Master.StatusCode = 1;
                    Master.StatusComment = "The Username you entered is incorrect.";
                    ShowSysFeedback();
                }
            }
            else
            {
                ShowSysFeedback();
                return;
            }
        }
        else if (pnlSecQuestions.Visible)
        {

            ///long lValidate = 0;
            long lFXUserID = 0;

            if (txtAnswer1.Text.Trim().Length > 0
                && txtAnswer2.Text.Trim().Length > 0)
            {
                if (ViewState["FX_USER_ID"] != null)
                {
                    lFXUserID = Convert.ToInt32(ViewState["FX_USER_ID"]);
                }

                string strAnswer1 = sec.Enc(txtAnswer1.Text.Trim().ToLower(), String.Empty);
                string strAnswer2 = sec.Enc(txtAnswer2.Text.Trim().ToLower(), String.Empty);
                secquest.ValidateAnswers(lFXUserID, strAnswer1, strAnswer2, String.Empty);

                if (Master.StatusCode == 0) // good to continue to reset password
                {
                    pnlSecQuestions.Visible = false;
                    pnlNewPassword.Visible = true;
                }
                else if (Master.StatusCode == 1) //1: invalid answer
                {
                    ShowSysFeedback();
                }
                else if (Master.StatusCode == 9) //9: account is locked
                {
                    btnSubmit.Visible = false;
                    divAccLocked.InnerText = Master.StatusComment;
                    divAccLocked.Visible = true;
                }
            }
            else
            {
                Master.StatusCode = 1;
                Master.StatusComment = "Please answer all the questions to continue.";
                ShowSysFeedback();
            }
        }
        else if (pnlNewPassword.Visible)
        {
            if (txtPassword.Text.Trim().Length < 1 || txtConfirmPassword.Text.Trim().Length < 1)
            {
                Master.StatusCode = 1;
                Master.StatusComment = "Pasword and Password Confirmation are required.";
                ShowSysFeedback();
                return;
            }
            
            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim()) {
                Master.StatusCode = 1;
                Master.StatusComment = "Pasword and Password Confirmation are different.";
                ShowSysFeedback();
                return;
            }
            
            //change password and login
            long lFXUserID = 0;
            if (ViewState["FX_USER_ID"] != null)
            {
                lFXUserID = Convert.ToInt32(ViewState["FX_USER_ID"]);
            }

            string strUserName = txtUserName.Text.Trim();
            if (sec.ValidatePasswordRules(Master, txtPassword.Text.Trim()))
            {
                if (secquest.ResetPassword(lFXUserID, strUserName, txtPassword.Text.Trim()))
                {
                    long lStatusCode = 0;
                    string strStatusComment = String.Empty;

                    if (sec.Login(Master, txtUserName.Text.Trim(), txtPassword.Text.Trim()) != 0)
                    {
                        Master.StatusCode = lStatusCode;
                        Master.StatusComment = strStatusComment;
                        ShowSysFeedback();
                        return;
                    }

                    //set a session variable with the login time
                    Session["SESSION_INITIATED"] = DateTime.Now;

                    //redirect, we are now logged in
                    //Master.Response.Redirect("portal_revamp.aspx");
                    Master.Response.Redirect("portal_start.aspx");

                    return;
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
            ScriptManager.RegisterStartupScript(upChangePWD, typeof(string), "showAlert", "Ext.onReady(function(){winSysFeedback.show();})", true);
            Master.ClearStatus();
        }
    }

}