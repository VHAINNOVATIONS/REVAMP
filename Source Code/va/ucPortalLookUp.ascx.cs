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

public partial class ucPortalLookUp : System.Web.UI.UserControl
{
    public BaseMaster BaseMstr { get; set; }
    public bool bAllowUpdate { set; get; }

    protected CDataUtils utils = new CDataUtils();
    public CPatient patadmin = new CPatient();
    
    const int cnLocked = 1;
    const int cnNotLocked = 0;

    //Patient 
    const int cnName =  401;
    const int cnPhone = 412;
    const int cnEmail = 413;

    const long cnChgPSWDEventID = 10000;

    bool blnFXUserIDExists;

    CDataUtils m_utils = new CDataUtils();

    protected void Page_Load(object sender, EventArgs e)
    {        
        //add js onclick event listener
        //to "reset password" checkbox
        chkResetPasswd.Attributes.Add("onclick","portal.user.resetPasswd(this);");

        if (!IsPostBack)
        {
            BaseMstr.ClosePatient();
            CheckUsrRightsMode();
        }

        //if the user pushed the master save button
        //then save the form
        if (BaseMstr.OnMasterSAVE())
        {
            bool bSaved = false;
            Save(out bSaved);
            if (bSaved) 
            {
                divStatus.InnerHtml = "<font color=\"green\"><img alt=\"\" src=\"Images/tick.png\">&nbsp;Patient's data was saved!</font>";
                ScriptManager.RegisterClientScriptBlock(upPatPortal, typeof(string), "saved", "clearStatusDiv(4);", true);
            }
        }

        LoadPatientsData();

        
    }

    protected long getFXUserID()
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

    // protected bool CheckIfFXUserRecExists()
    protected bool CheckIfFXUserRecExists()
    {
        CSec sec = new CSec();
        DataSet secDataChk = new DataSet();

        //attempt to grab the user's profile
        secDataChk = sec.CheckPatientFXUserRecDS(BaseMstr);

        //load all of the user's available fields
        if (secDataChk != null)
        {
            foreach (DataTable secTable in secDataChk.Tables)
            {
                foreach (DataRow secRow in secTable.Rows)
                {
                    if (!secRow.IsNull("FXUSERCOUNT"))
                    {
                        if (secRow["FXUSERCOUNT"].ToString() != "0")
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    protected string CheckUserDemoErrors()
    {
        string strErrMessage = "";

        CUserAdmin usrdemo = new CUserAdmin();
        
        string strStatus = "";
        if (!usrdemo.ValidateUserDemographicRules(BaseMstr, cnName, txtName.Text, out strStatus))
        {
            strErrMessage += strStatus;
        }

        return strErrMessage;
    }

    protected void Save(out bool bSaved)
    {
        bSaved = false;
        bool bNotify = false;

        if (bAllowUpdate)
        {
            if (!String.IsNullOrEmpty(txtUserId.Text) && txtUserId.Text.Trim().Length > 0)
            {
                #region SaveUser

                //////////////////////////////////////////////////////
                //user account is valid so press on with the saves... 
                CPatient patSave = new CPatient();

                //fx sec helper
                CSec sec = new CSec();

                if (chkResetPasswd.Checked || String.IsNullOrEmpty(htxtPatientID.Value))
                {
                    //make sure pwd and verify pwd match
                    if (txtPassword.Text != txtVerifyPassword.Text)
                    {
                        BaseMstr.StatusCode = 1;
                        BaseMstr.StatusComment = "Password and verify password must match!";
                        ShowSysFeedback();
                        return;
                    }

                    //check all the account rules for the account...
                    if (!sec.ValidateUserAccountRules(BaseMstr,
                                                       txtUserId.Text,
                                                       txtPassword.Text))
                    {
                        BaseMstr.StatusCode = 1;
                        //Note: ValidateUserAccountRules will set StatusCode/StatusComment info
                        ShowSysFeedback();
                        return;
                    }
                }

                //if a user is looked up, then SelectedPatientID will be set
                //otherwise we are creating a new user
                if (htxtPatientID.Value == "")//this is a new user
                {
                    return;
                }
                else if (htxtPatientID.Value != "")
                {
                    BaseMstr.SelectedPatientID = htxtPatientID.Value;

                    long lFXUserID = 0;

                    BaseMstr.SetVSValue("FXUserIDExists", CheckIfFXUserRecExists());

                    blnFXUserIDExists = BaseMstr.GetVSBoolValue("FXUserIDExists");

                    if (!blnFXUserIDExists)
                    {
                        //insert a record into the fx_user table and update
                        //the fx_user_id in the patient table

                        sec.InsertPatientFXUser(BaseMstr,
                                            BaseMstr.SelectedPatientID,
                                            txtUserId.Text,
                                            txtPassword.Text,
                                            chkbxAccountLocked.Checked,
                                            chkbxAccountInactive.Checked,
                                            out lFXUserID);

                        if (BaseMstr.StatusCode == 0)
                        {
                            txtUserId.Enabled = false;
                            chkResetPasswd.Checked = false;
                            txtPassword.Text = String.Empty;
                            txtVerifyPassword.Text = String.Empty;
                            txtPassword.Enabled = false;
                            txtVerifyPassword.Enabled = false;

                            bSaved = true;
                        }
                        bSaved = (BaseMstr.StatusCode == 0);

                        if (bSaved)
                        {
                            bNotify = true;
                        }

                    }
                    else
                    {
                        //update a record into the fx_user table and update
                        //the fx_user_id in the suat user table
                        lFXUserID = getFXUserID();
                    }

                    //ONLY if we changed the users account info, update the
                    //record in the FX_USER table

                    if (chkResetPasswd.Checked)
                    {
                        sec.UpdatePatientFXUserPWD(BaseMstr,
                                            lFXUserID,
                                            txtUserId.Text,
                                            txtPassword.Text,
                                            chkbxAccountLocked.Checked,
                                            chkbxAccountInactive.Checked
                                            );

                        if (BaseMstr.StatusCode == 0)
                        {
                            txtUserId.Enabled = false;
                            chkResetPasswd.Checked = false;
                            txtPassword.Text = String.Empty;
                            txtVerifyPassword.Text = String.Empty;
                            txtPassword.Enabled = false;
                            txtVerifyPassword.Enabled = false;  
                        }
                        bSaved = (BaseMstr.StatusCode == 0);

                        if (bSaved)
                        {
                            bNotify = true; 
                        }
                    }
                    else
                    {
                        bSaved = sec.UpdatePatientFXUserOptions(BaseMstr,
                                                lFXUserID,
                                                chkbxAccountLocked.Checked,
                                                chkbxAccountInactive.Checked
                                                );
                    }
                    bSaved = (BaseMstr.StatusCode == 0);
                }

                if (bNotify)
                {
                    //Add all patient events
                    CPatientEvent evt = new CPatientEvent(BaseMstr);
                    evt.AddSpecificEvent(cnChgPSWDEventID); //Password Changed

                    if (BaseMstr.StatusCode == 0)
                    {
                        evt.CompletedSpecificEvent(cnChgPSWDEventID); //Password Changed Event ID
                    }
                }

                LoadPatientsData();
                #endregion
            }
            else
            {
                return;
            }
        }
        else
        {
            BaseMstr.StatusCode = 1;
            BaseMstr.StatusComment = "<img alt=\"\" src=\"Images/lock16x16.png\" /> You have <b>Read-Only Access</b> to this section.";
        }

        ShowSysFeedback();
        return;
    }

    protected void LoadPatientsData() 
    {
        //load users list
        CPatient patadmin = new CPatient();
        DataSet dsPatients = patadmin.GetPatientPortalListDS(BaseMstr);

        storePatAccount.DataSource = m_utils.GetDataTable(dsPatients);
        storePatAccount.DataBind();

        //generates user data JSON string
        ProcessPatientData(dsPatients);
        htxtPatientData.Value = utils.GetJSONString(dsPatients);

    }

    protected void ProcessPatientData(DataSet ds)
    {
        CSec sec = new CSec();
        if (ds != null)
        {
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull("USER_NAME"))
                    {
                        dr["USER_NAME"] = sec.dec(dr["USER_NAME"].ToString(), "");
                    }
                }
            }
            ds.AcceptChanges();
        }
    }

    protected void CheckUsrRightsMode() 
    {
        // Not Editable
        txtName.Enabled = false;
        txtPhone.Enabled = false;
        txtEmail.Enabled = false;
        txtUserId.Enabled = false;
        txtPassword.Enabled = false;
        txtVerifyPassword.Enabled = false; 
    }

    protected void ShowSysFeedback()
    {
        if (BaseMstr.StatusCode > 0 && !String.IsNullOrEmpty(BaseMstr.StatusComment))
        {
            HtmlContainerControl div = (HtmlContainerControl)this.BaseMstr.FindControl("divSysFeedback");
            div.InnerHtml = BaseMstr.StatusComment;
            ScriptManager.RegisterStartupScript(upPatPortal, typeof(string), "showAlert", "Ext.onReady(function(){winSysFeedback.show();})", true);
            BaseMstr.ClearStatus();
        }
    }
}
