using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class ucPatientPortalAccount : System.Web.UI.UserControl
{
    bool m_bNewPatPortalAcct = false;
    public long lUsrRightMode { get; set; }
    bool bControlsEnabled;
    CSec usrsec = new CSec();

    public BaseMaster BaseMstr { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void EnablePasswordFields()
    {
        txtPassword.Enabled = true;
        txtVerifyPassword.Enabled = true;
    }

    protected void DisablePasswordFields()
    {
        txtPassword.Enabled = false;
        txtVerifyPassword.Enabled = false;
    }

    protected void btnResetPassword_click(object sender, EventArgs e)
    {
        //Delete if (divPassword.Visible)
        if (txtPassword.Enabled)
        {
            txtPassword.Text = "";
            txtVerifyPassword.Text = "";
            //Delete divPassword.Visible = false;
            DisablePasswordFields();
        }
        else 
        {
            //Delete divPassword.Visible = true;
            EnablePasswordFields();
            txtPassword.Focus();
            //btnResetPassword.Enabled = false; //xxx
        }
    }

    public void loadPatientPortalAccount()
    {
        //Delete divPassword.Visible = false;
        txtPassword.Enabled = false;
        txtVerifyPassword.Enabled = false;

        BaseMstr.SetVSValue("NewPatientPortalAcct", false);
        
        BaseMstr.SetVSValue("PatientFXUserIDExists", false);

        BaseMstr.SetVSValue("PatientFXUserIDExists", CheckIfPatientFXUserRecExists());

        bool bFXUserIDExists = BaseMstr.GetVSBoolValue("PatientFXUserIDExists");

        if (!bFXUserIDExists)
        {
            //Delete divPassword.Visible = true;
            txtPassword.Enabled = true;
            txtVerifyPassword.Enabled = true;
            txtUserId.Enabled = true;
            //btnResetPassword.Enabled = false;
            chkResetPassword.Enabled = false;

            BaseMstr.SetVSValue("NewPatientPortalAcct", true);
        }
        else
        {
            CDataUtils utils = new CDataUtils();
            CSec sec = new CSec();

            DataSet dsSecData = new DataSet();

            //attempt to grab the user's profile
            dsSecData = sec.GetPatientFXUsernamePasswordDS(BaseMstr);

            //load FXUser Username and Password fields
            if (dsSecData != null)
            {
                txtUserId.Text = sec.dec(utils.GetStringValueFromDS(dsSecData, "USER_NAME"),"");
                //chkbxAccountLocked.Checked = Convert.ToBoolean(utils.GetLongValueFromDS(dsSecData, "IS_LOCKED"));
                //chkbxAccountInactive.Checked = Convert.ToBoolean(utils.GetLongValueFromDS(dsSecData, "IS_INACTIVE"));

                txtUserId.Enabled = false;
                divResetPWDButton.Visible = true;
                chkResetPassword.Enabled = true;
                txtPassword.Enabled = false;
                txtVerifyPassword.Enabled = false;
            }
        }

        Page_Load(null, EventArgs.Empty);
    }

    protected bool CheckIfPatientFXUserRecExists()
    {
        CSec sec = new CSec();
        DataSet secDataChk = sec.CheckPatientFXUserRecDS(BaseMstr);

        //load all of the patient's available fields
        if (secDataChk != null)
        {
            foreach (DataTable secTable in secDataChk.Tables)
            {
                foreach (DataRow secRow in secTable.Rows)
                {
                    if (!secRow.IsNull("FXUSERCOUNT"))
                    {
                        long lFXUserCount = Convert.ToInt32(secRow["FXUSERCOUNT"]);
                        if (lFXUserCount > 0)
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;

    }

    protected void ConvertPatientPortal()
    {

        
    }

    protected long getPatientFXUserID()
    {
        long Value = 0;

        CSec fxSec = new CSec();

        DataSet SecSet = new DataSet();
        SecSet = fxSec.GetPatientFXUserIdDS(BaseMstr);

        //load all of the user's available fields
        if (SecSet != null)
        {
            foreach (DataTable secTable in SecSet.Tables)
            {
                foreach (DataRow secRow in secTable.Rows)
                {
                    if (!secRow.IsNull("FX_USER_ID"))
                    {
                        Value = Convert.ToInt64(secRow["FX_USER_ID"]);
                    }
                }
            }
        }

        return Value;
    }

    public bool SavePatientPortalAccount()
    {
        bool bSaved = false;

        //fx sec Patient helper
        CSec sec = new CSec();
        long lFXUserID = 0;

        bool bUserExists = CheckIfPatientFXUserRecExists();
        bool bChkInsPatPortalAccount = CheckPatientFXUser(sec, !bUserExists);

        if (bChkInsPatPortalAccount)
        {
            bool m_bFXUserIDExists = bUserExists;

            if (!m_bFXUserIDExists)
            {
                bSaved = sec.InsertPatientFXUser(BaseMstr,
                                         BaseMstr.SelectedPatientID,
                                         txtUserId.Text,
                                         txtPassword.Text,
                                         false, //acount locked
                                         false, //account inactive
                                         out lFXUserID);

                if (BaseMstr.StatusCode == 0)
                {
                    txtUserId.Enabled = false;
                    //Delete divPassword.Visible = false; 
                    divPassword.Visible = true;
                    divResetPWDButton.Visible = true;
                }
            }
            else
            {
                //long lFXUserID = 0;
                //update a record into the fx_user table and update
                //the fx_user_id in the patient table
                lFXUserID = getPatientFXUserID();
            }

            //ONLY if we changed the users account info, update the
            //record in the FX_USER table
            //Delete if (divPassword.Visible)
            if (txtPassword.Enabled)
            {
                bSaved = sec.UpdatePatientFXUserPWD(BaseMstr,
                                            lFXUserID,
                                            txtUserId.Text,
                                            txtPassword.Text,
                                            false, //account locked
                                            false //account inactive
                                             );

                if (BaseMstr.StatusCode == 0)
                {
                    //Delete divPassword.Visible = false;
                    txtPassword.Enabled = false;
                    txtVerifyPassword.Enabled = false;
                    //btnResetPassword.Enabled = true;
                }
            }
            else
            {
                bSaved = sec.UpdatePatientFXUserOptions(BaseMstr,
                                                lFXUserID,
                                                false, //account locked
                                                false //account inactive
                                               );
            }

        }
        loadPatientPortalAccount();
        //upPortalAccount.Update();

        if (!bSaved) 
        {
            chkResetPassword.Checked = false;
        }

        return bSaved;

    }

    protected bool CheckPatientFXUser(CSec secx, bool bIsNewPatPortalAcct)
    {
        //Delete if (divPassword.Visible)
        if (txtPassword.Enabled)
        {
            //make sure pwd and verify pwd match
            if (txtPassword.Text != txtVerifyPassword.Text)
            {
                BaseMstr.StatusCode = 501;
                BaseMstr.StatusComment = "Password and verify password must match!";
                return false;
            }

            //check all the account rules for the account...
            if (!secx.ValidateUserAccountRules(BaseMstr,
                                               txtUserId.Text,
                                               txtPassword.Text))
            {
                //Note: ValidateUserAccountRules will set StatusCode/StatusComment info
                return false;
            }
        }

        //make sure user name does not already exist since 
        //we are doing an insert
        if (bIsNewPatPortalAcct)
        {
            if (secx.UserNameExists(BaseMstr, txtUserId.Text))
            {
                BaseMstr.StatusCode = 500;
                BaseMstr.StatusComment = "Please choose a different user name!";
                return false;
            }
        }
        return true;
    }

    protected string CheckPatientPortalInputErrors()
    {
        string strErrMessage = "";
        string strStatus = "";

        return strErrMessage;
    }

    //chkResetPassword_click
    protected void chkResetPassword_click(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        if (chk.Checked)
        {
            btnResetPassword_click(null, EventArgs.Empty);
        }
        else 
        {
            txtPassword.Text = String.Empty;
            txtVerifyPassword.Text = String.Empty;
            DisablePasswordFields();
        }
    }

    //show save confirmation
    public void showSaveConfirmation() 
    {
        divStatus.InnerHtml = "<font color=\"green\"><img alt=\"\" src=\"Images/tick.png\">&nbsp;User's data was saved!</font>";
        ScriptManager.RegisterClientScriptBlock(upPortalAccount, typeof(string), "saved", "clearStatusDiv(4);", true);
    }
}

