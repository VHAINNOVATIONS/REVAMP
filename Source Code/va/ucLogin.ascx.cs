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

public partial class ucLogin : System.Web.UI.UserControl
{
    //basemaster property
    public BaseMaster BaseMstr { get; set; }

    //page load
    protected void Page_Load(object sender, EventArgs e)
    {
        CSec sec = new CSec();
        CDataUtils utils = new CDataUtils();

        if (!IsPostBack)
        {
            //if we are already logged in put us in change pwd mode
            if (BaseMstr.IsLoggedIn())
            {
                //we are in change pwd mode since we are already logged in
                SetMode(2);

                //get the username from db
                DataSet dsSecData = sec.GetFXUsernamePasswordDS(BaseMstr);
                txtUN.Text = sec.dec(utils.GetStringValueFromDS(dsSecData, "USER_NAME"), "");
                txtUN.ReadOnly = true;
            }
            else
            {
                //we are in login mode because we have not logged in yet
                SetMode(1);
              
                //when we time out session wise we dont want to ask the user 
                //for  a user name/password if they have a valid cert...
                if (sec.CertLogin(BaseMstr))
                {
                    BaseMstr.StatusCode = 0;
                    BaseMstr.StatusComment = "";
                    Response.Redirect("revamp.aspx");
                }
                
                BaseMstr.StatusCode = 0;
                BaseMstr.StatusComment = "";
            }
        }
        else //not a post back
        {
            //clear the divs html on the postback
            PopupPostLogin.InnerHtml = "";

            //only if not logged in set the un on the change pwd dive = to the u
            //this is so we dont have to re-type it if forced to change pwd
            if (!BaseMstr.IsLoggedIn())
            {
                txtUN.Text = txtU.Text;
            }
        }
    }

    public void SetMode(int nMode)
    {
        if (nMode == 2)//change pwd
        {
            divChangePassword.Visible = true;
            divLogin.Visible = false;
        }
        else//login
        {
            if (txtOldP.Text == "")//check to make sure we are not currently changing the password
            {
                divChangePassword.Visible = false;
                divLogin.Visible = true;
            }
        }
    }

    //login or change password
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        long lStatus = 0;
        CSec sec = new CSec();

        #region Login
        if (divLogin.Visible)//logging in
        {
            lStatus = sec.Login(BaseMstr, txtU.Text, txtP.Text);
            if (lStatus == 0)
            {
                //good to go so cleanup and redirect
                PopupPostLogin.InnerHtml = "";
                                
                //put us in change password mode
                divChangePassword.Visible = true;
                txtUN.ReadOnly = false;//lest them re-type there user name
                txtUN.Text = "";
                txtOldP.Text = "";
                txtNewP.Text = "";
                txtVNewP.Text = "";
                
                txtU.Text = "";
                txtP.Text = "";
                divLogin.Visible = false;

                //set a session variable with the login time
                Session["SESSION_INITIATED"] = DateTime.Now;

                //redirect, we are now logged in
                BaseMstr.Response.Redirect("revamp.aspx");
            }
            else
            {
                //
                //4 = change password
                //
                if (lStatus == 4)
                {
                    divLogin.Visible = false;
                    divChangePassword.Visible = true;

                    string strMsg = "<div style=\"padding: 10px; \">";
                    strMsg += "<span class=\"login-alert\"><img src=\"Images/error.png\" alt=\"Transaction Failed\" />&nbsp;";
                    strMsg += "Please change your password.";
                    strMsg += "</span>";
                    strMsg += "</div>";
                    divLoginStatus.InnerHtml = strMsg;

                    txtUN.Text = txtU.Text;
                    txtUN.ReadOnly = true;
                    return;
                }

                //////////////////////////////////////////////////////
                //following are handled below
                //
                //1 = invalid pwd
                //7 = invalid pwd and locked it
                //2 = account locked
                //3 = account inactive
                //6 = ip address locked
                //
                //show error and try again

                string strErr = "<div style=\"padding: 10px; \">";
                strErr += "<span class=\"login-error\"><img src=\"Images/cancel.png\" alt=\"Transaction Failed\" />&nbsp;";
                strErr += BaseMstr.StatusComment;
                strErr += "</span>";
                strErr += "</div>";

                divLoginStatus.InnerHtml = strErr;

                Session["SESSION_INITIATED"] = null;
            }
        }
#endregion

        #region ChangePassword
        //are we changing the password?
        if (divChangePassword.Visible)//changing password
        {
            //only if not logged in
            if (!BaseMstr.IsLoggedIn())
            {
                txtUN.Text = txtU.Text;
            }

            //new pwd and verify new pwd must match
            if (txtNewP.Text != txtVNewP.Text)
            {
                string strErr = "<div style=\"padding: 10px; \">";
                strErr += "<span class=\"login-error\"><img src=\"Images/cancel.png\" alt=\"Transaction Failed\" />&nbsp;";
                strErr += "New Password and Verify Password do not match";
                strErr += "</span>";
                strErr += "</div>";

                divLoginStatus.InnerHtml = strErr;
                return;
            }

            //check all the account rules for the account...
            if (!sec.ValidateUserAccountRules(BaseMstr,
                                               txtUN.Text,
                                               txtNewP.Text))
            {
                //Note: will set StatusCode/StatusComment info
                string strErr = "<div style=\"padding: 10px; \">";
                strErr += "<span class=\"login-error\"><img src=\"Images/cancel.png\" alt=\"Transaction Failed\" />&nbsp;";
                strErr += BaseMstr.StatusComment;
                strErr += "</span>";
                strErr += "</div>";

                if (BaseMstr.StatusComment.Length < 48)
                {
                    divLoginStatus.InnerHtml = strErr;
                    PopupPostLogin.InnerHtml = String.Empty;
                }
                else
                {
                    divLoginStatus.InnerHtml = String.Empty;
                    ScriptManager.RegisterStartupScript(upLogin, typeof(string), "loginmsg", "sysfeedback('" + BaseMstr.StatusComment + "')", true);
                    //PopupPostLogin.InnerHtml = "<script type=\"text/javascript\">alert('" + BaseMstr.StatusComment + "');</script>";
                }

                return;
            }

            //change the users password, this will also log the user in
            lStatus = sec.ChangePassword(BaseMstr,
                                         txtUN.Text,
                                         txtOldP.Text,
                                         txtNewP.Text);


            if (lStatus != 0)
            {
                //Note: will set StatusCode/StatusComment info
                string strErr = "<div style=\"padding: 10px; \">";
                strErr += "<span class=\"login-error\"><img src=\"Images/cancel.png\" alt=\"Transaction Failed\" />&nbsp;";
                strErr += BaseMstr.StatusComment;
                strErr += "</span>";
                strErr += "</div>";

                divLoginStatus.InnerHtml = strErr;

                Session["SESSION_INITIATED"] = null;
                return;
            }
            else
            {
                //successfully logged in!
                divLoginStatus.InnerHtml = "";


                //clear the user id and pwd
                txtU.Text = "";
                txtP.Text = "";
                txtUN.Text = "";
                txtOldP.Text = "";
                txtNewP.Text = "";
                txtVNewP.Text = "";
                txtUN.ReadOnly = false;

                Session["SESSION_INITIATED"] = DateTime.Now;

                BaseMstr.Response.Redirect("revamp.aspx");
                //redirect...
            }
        }
        #endregion
    }
}
