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

public partial class ucUserAdmin : System.Web.UI.UserControl
{
    public BaseMaster BaseMstr { get; set; }
    public bool bAllowUpdate { set; get; }

    protected CDataUtils utils = new CDataUtils();
    public CUserAdmin useradmin = new CUserAdmin();
    
    const int cnLocked = 1;
    const int cnNotLocked = 0;

    //User
    const int cnName = 401;
    const int cnTitle = 403;
    const int cnAddress = 408;
    const int cnCity = 409;
    const int cnState = 410;
    const int cnPostalCode = 411;
    const int cnPhone = 412;
    const int cnEmail = 413;

    string strNewProviderID = "";
    string strMessage = "";

    bool blnFXUserIDExists;

    protected void Page_Load(object sender, EventArgs e)
    {
        //add js onclick event listener
        //to "reset password" checkbox
        chkResetPasswd.Attributes.Add("onclick","admin.user.resetPasswd(this);");

        if (!IsPostBack)
        {
            BaseMstr.ClosePatient();

            LoadUsersData();

            GetRightsTemplate();

            //User Types
            useradmin.LoadUserTypesRadioButtonList(BaseMstr,
                                                  rblUserType,
                                                  "");

            //User Rights
            DataSet dsUsrRights = useradmin.GetUserRightsDS(BaseMstr);
            repUserRights.DataSource = dsUsrRights;
            repUserRights.DataBind();

            CMilitaryRender MilitaryRender = new CMilitaryRender();
            
            //military duty station
            MilitaryRender.LoadMilDutyStationDropDownList(BaseMstr,
                                                       cboSite,
                                                       "");
        }

        //if the user pushed the master save button
        //then save the form
        if (BaseMstr.OnMasterSAVE())
        {
            bool bSaved = false;
            Save(out bSaved);
            if (bSaved) 
            {
                divStatus.InnerHtml = "<font color=\"green\"><img alt=\"\" src=\"Images/tick.png\">&nbsp;User's data was saved!</font>";
                ScriptManager.RegisterClientScriptBlock(upUserAdmin, typeof(string), "saved", "clearStatusDiv(4);", true);
            }
        }

        CheckUsrRightsMode();
    }

    protected long getFXUserID()
    {
        long Value = 0;

        CSec fxSec = new CSec();

        DataSet SecSet = new DataSet();
        SecSet = fxSec.GetFXUserIdDS(BaseMstr);

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

    protected bool CheckIfFXUserRecExists()
    {
        CSec sec = new CSec();
        DataSet secDataChk = new DataSet();

        //attempt to grab the user's profile
        secDataChk = sec.CheckFXUserRecDS(BaseMstr);

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
        long lUserType = -1;
        if(rblUserType.SelectedIndex > -1)
        {
            lUserType = Convert.ToInt32(rblUserType.SelectedValue);
        }

        if (lUserType < 0)
        {
            strErrMessage += "Please select a User Type.<br/>";
        }
       
        long lUserRights = 0;
        long lReadOnly = 0;
        GetUserRights(out lUserRights, out lReadOnly);

        if (lUserRights == 0)
        {
            strErrMessage += "Please select User Rights.<br/>";
        }

        if (cboSite.SelectedItem.Value == "-1")
        {

            strErrMessage += "Please select a Clinic.<br/>";
        }

        string strStatus = "";
        if (!usrdemo.ValidateUserDemographicRules(BaseMstr, cnName, txtName.Text, out strStatus))
        {
            strErrMessage += strStatus;
        }

        strStatus = "";
        if (!usrdemo.ValidateUserDemographicRules(BaseMstr, cnEmail, txtEmail.Text, out strStatus))
        {
            strErrMessage += strStatus;
        }

        strStatus = "";
        if (!usrdemo.ValidateUserDemographicRules(BaseMstr, cnPhone, txtPhone.Text, out strStatus))
        {
            strErrMessage += strStatus;
        }

        return strErrMessage;
    }

    protected void Save(out bool bSaved)
    {
        bSaved = false;
        if (bAllowUpdate)
        {
            if (!String.IsNullOrEmpty(txtUserId.Text) && txtUserId.Text.Trim().Length > 0)
            {
                #region SaveUser
                strMessage = "";

                //////////////////////////////////////////////////////
                //user account is valid so press on with the saves... 
                CUserAdmin usrSave = new CUserAdmin();
                int iUserType = 0;

                //get the user rights for storing
                long lUserRights = 0;
                long lReadOnly = 0;
                GetUserRights(out lUserRights, out lReadOnly);

                strMessage = CheckUserDemoErrors();

                if (strMessage != "")
                {
                    BaseMstr.StatusCode = 1;
                    BaseMstr.StatusComment = strMessage;
                    ShowSysFeedback();
                    return;
                }

                //User Type Selected
                iUserType = Convert.ToInt32(rblUserType.SelectedItem.Value);

                int lService = 0;

                //fx sec helper
                CSec sec = new CSec();

                if (chkResetPasswd.Checked || String.IsNullOrEmpty(htxtProviderID.Value))
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


                ////////////////////////////////////////////////////////
                ////user account is valid so press on with the saves... 

                //is a user is looked up, then SelectedProviderID will be set
                //otherwise we are creating a new user
                if (htxtProviderID.Value == "")//this is a new user
                {
                    if (BaseMstr.StatusCode == 0)
                    {

                        //note: user name and pwd are valid so do the inserts

                        //make sure user name does not already exist since 
                        //we are doing an insert
                        if (sec.UserNameExists(BaseMstr, txtUserId.Text))
                        {
                            BaseMstr.StatusCode = 1;
                            BaseMstr.StatusComment = "Please choose a different user name!";
                            ShowSysFeedback();
                            return;
                        }

                        //get a new provider id
                        strNewProviderID = BaseMstr.APPMaster.GetNewProviderID();

                        //insert a record into the suat user table
                        usrSave.InsertSuatUser(BaseMstr,
                                               strNewProviderID,
                                               0,       //n/a now that we have fx-user - Locked
                                               txtName.Text,
                                               "N/A",   //Not Used Rank
                                                0,      //Not Used Service,
                                               txtTitle.Text,
                                               "N/A",   //Not Used Corps,
                                               "N/A",   //Not Used Squadron,
                                               "N/A",   //Not Used OfficeSymbol,
                                               txtPhone.Text,
                                               txtEmail.Text,
                                               cboSite.SelectedValue,
                                               "N/A",   //- Not Used UIDPWD
                                               0,       //n/a now that we have fx_user - Not Used MustChgPWD
                                              "N/A");   //Not Used SupervisorID);

                        if (BaseMstr.StatusCode == 0)
                        {
                            BaseMstr.SelectedProviderID = strNewProviderID;
                            htxtProviderID.Value = strNewProviderID;

                            //insert a record into the fx_user table and update
                            //the fx_user_id in the suat user table
                            long lFXUserID = 0;

                            sec.InsertFXUser(BaseMstr,
                                             BaseMstr.SelectedProviderID,
                                             txtUserId.Text,
                                             txtPassword.Text,
                                             chkbxAccountLocked.Checked,
                                             chkbxAccountInactive.Checked,
                                             out lFXUserID);

                            if (BaseMstr.StatusCode == 0)
                            {
                                sec.UpdateFXUserRights(BaseMstr,
                                                       lFXUserID,
                                                       iUserType,
                                                       lUserRights,
                                                       lReadOnly);


                                txtUserId.Enabled = false;
                                chkResetPasswd.Checked = false;
                                txtPassword.Text = String.Empty;
                                txtVerifyPassword.Text = String.Empty;
                                txtPassword.Enabled = false;
                                txtVerifyPassword.Enabled = false;

                            }

                            bSaved = (BaseMstr.StatusCode == 0);
                        }
                    }
                }
                else //if is existing user
                {
                    //user is selected for edit
                    if (htxtProviderID.Value != "")
                    {
                        BaseMstr.SelectedProviderID = htxtProviderID.Value;

                        if (BaseMstr.StatusCode == 0)
                        {
                            //update the users record in the SUAT_USER table
                            usrSave.UpdateSuatUser(BaseMstr,
                                                   0,       //n.a now that we have fx_user - Locked
                                                   txtName.Text,
                                                   "N/A",   //Not Used Rank
                                                   0,       //Not Used Service,
                                                   txtTitle.Text,
                                                   "N/A",   //Not Used Corps
                                                   "N/A",   //Not Used Squadron
                                                   "N/A",   //Not Used OfficeSymbol
                                                   txtPhone.Text,
                                                   txtEmail.Text,
                                                   cboSite.SelectedValue,
                                                   "N/A",   //Not Used UIDPWD
                                                   0,       //n/a now that we have fx_user -Not Used MustChgPWD
                                                   "N/A");  //Not Used SupervisorID);

                            if (BaseMstr.StatusCode == 0)
                            {

                                long lFXUserID = 0;

                                BaseMstr.SetVSValue("FXUserIDExists", CheckIfFXUserRecExists());

                                blnFXUserIDExists = BaseMstr.GetVSBoolValue("FXUserIDExists");

                                if (!blnFXUserIDExists)
                                {
                                    //insert a record into the fx_user table and update
                                    //the fx_user_id in the patient table

                                    sec.InsertFXUser(BaseMstr,
                                                     BaseMstr.SelectedProviderID,
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
                                    sec.UpdateFXUserPWD(BaseMstr,
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
                                }
                                else
                                {
                                    bSaved = sec.UpdateFXUserOptions(BaseMstr,
                                                            lFXUserID,
                                                            chkbxAccountLocked.Checked,
                                                            chkbxAccountInactive.Checked
                                                           );
                                }

                                //update fx_user_rights
                                bSaved = sec.UpdateFXUserRights(BaseMstr,
                                                       lFXUserID,
                                                       iUserType,
                                                       lUserRights,
                                                       lReadOnly);
                            }
                            bSaved = (BaseMstr.StatusCode == 0);
                        }
                    }
                }

                LoadUsersData();
                #endregion
            }
            else
            {
                BaseMstr.StatusCode = 1;
                BaseMstr.StatusComment = "<b>Username</b> and <b>Password</b> are required for the new user account.";
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

    protected void LoadUsersData() 
    {
        //load users list
        CUserAdmin usradmin = new CUserAdmin();
        DataSet dsUsers = usradmin.GetSUATUserListDS(BaseMstr);

        repUsersList.DataSource = dsUsers;
        repUsersList.DataBind();

        //generates user data JSON string
        ProcessUserData(dsUsers);
        htxtUserData.Value = utils.GetJSONString(dsUsers);
    }

    protected void ProcessUserData(DataSet ds) 
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

    protected void GetUserRights(out long lRights, out long lReadOnly) 
    {
        lRights = 0;
        lReadOnly = 0;

        foreach (RepeaterItem ri in repUserRights.Items) 
        {
            HtmlInputCheckBox chkRight = (HtmlInputCheckBox)ri.FindControl("chkUsrRight");
            HtmlInputCheckBox chkReadOnly = (HtmlInputCheckBox)ri.FindControl("chkReadOnly");

            long lRightVal = Convert.ToInt32(chkRight.Value);

            if (chkRight.Checked) 
            {
                lRights += lRightVal;
            }

            if (chkReadOnly.Checked)
            {
                lReadOnly += lRightVal;
            }
        }
    }

    protected void repUserRights_OnItemDataBound(object sender, RepeaterItemEventArgs e) 
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            DataRow dr = drv.Row;

            HtmlInputCheckBox chkro = (HtmlInputCheckBox)e.Item.FindControl("chkReadOnly");
            HtmlGenericControl lblro = (HtmlGenericControl)e.Item.FindControl("lblRO");
            HtmlContainerControl divMode = (HtmlContainerControl)e.Item.FindControl("divMode");

            Label lbl = (Label)e.Item.FindControl("lblUsrRight");
            Label lblDesc = (Label)e.Item.FindControl("lblRightComment");

            if (!dr.IsNull("RIGHT_DESC")) 
            {
                lbl.Text = dr["RIGHT_DESC"].ToString();
            }

            if (lblro != null) 
            {
                lblro.Attributes.Add("for", chkro.ClientID);
            }

            if (!dr.IsNull("HAS_MODE"))
            {
                divMode.Visible = (dr["HAS_MODE"].ToString().Equals("1"));
            }

            if (!dr.IsNull("COMMENTS"))
            {
                lblDesc.Text = dr["COMMENTS"].ToString();
            }
        }
    }

    protected void GetRightsTemplate() 
    {
        CDataUtils utils = new CDataUtils();
        CUserAdmin usradmin = new CUserAdmin();
        DataSet ds = usradmin.GetRightsTemplateDS(BaseMstr);
        htxtRightsTemplate.Value = utils.GetJSONString(ds);
    }

    protected void SaveRightsTemplate(object sender, EventArgs e) 
    {
        if (bAllowUpdate)
        {
            long lUserType = 0;
            long lUserRights = 0;
            long lRightsMode = 0;

            if (!String.IsNullOrEmpty(htxtSelUsrType.Value))
            {
                lUserType = Convert.ToInt32(htxtSelUsrType.Value);
                GetUserRights(out lUserRights, out lRightsMode);
                if (useradmin.UpdateRightsTemplate(BaseMstr, lUserType, lUserRights, lRightsMode))
                {
                    GetRightsTemplate();
                    foreach (RepeaterItem ri in repUserRights.Items) 
                    {
                        long lTempRight = 0;
                        long lTempMode = 0;

                        HtmlInputCheckBox chkR = (HtmlInputCheckBox)ri.FindControl("chkUsrRight");
                        HtmlInputCheckBox chkM = (HtmlInputCheckBox)ri.FindControl("chkReadOnly");
                        HtmlContainerControl div = (HtmlContainerControl)ri.FindControl("divMode");

                        lTempRight = Convert.ToInt32(chkR.Value);
                        lTempMode = Convert.ToInt32(chkM.Value);

                        if ((lUserRights & lTempRight) > 0) 
                        {
                            chkR.Checked = true;
                            div.Style.Add("display","block");
                        }

                        if ((lRightsMode & lTempMode) > 0)
                        {
                            chkM.Checked = true;
                        }
                    }

                    rblUserType.SelectedValue = lUserType.ToString();
                }
            }
            //htxtSavedTemplate.Value = "1";
        }
    }

    protected void CheckUsrRightsMode() 
    {
        if (!bAllowUpdate) 
        {
            btnAddUser.Visible = false;
        }

        foreach (RepeaterItem ri in repUserRights.Items) 
        {
            HtmlInputCheckBox chkRight = (HtmlInputCheckBox)ri.FindControl("chkUsrRight");
            HtmlInputCheckBox chkMode = (HtmlInputCheckBox)ri.FindControl("chkReadOnly");
            chkRight.Disabled = !bAllowUpdate;
            chkMode.Disabled = !bAllowUpdate;
        }

        txtName.Enabled = bAllowUpdate;
        txtTitle.Enabled = bAllowUpdate;
        txtPhone.Enabled = bAllowUpdate;
        txtEmail.Enabled = bAllowUpdate;
        cboSite.Enabled = bAllowUpdate;
        txtUserId.Enabled = bAllowUpdate;
        txtPassword.Enabled = bAllowUpdate;
        txtVerifyPassword.Enabled = bAllowUpdate;
        chkbxAccountLocked.Enabled = bAllowUpdate;
        chkbxAccountInactive.Enabled = bAllowUpdate;
        chkResetPasswd.Enabled = bAllowUpdate;
        lnkSaveTemplate.Visible = bAllowUpdate;
    }

    protected void ShowSysFeedback()
    {
        if (BaseMstr.StatusCode > 0 && !String.IsNullOrEmpty(BaseMstr.StatusComment))
        {
            HtmlContainerControl div = (HtmlContainerControl)this.BaseMstr.FindControl("divSysFeedback");
            div.InnerHtml = BaseMstr.StatusComment;
            ScriptManager.RegisterStartupScript(upUserAdmin, typeof(string), "showAlert", "Ext.onReady(function(){winSysFeedback.show();})", true);
            BaseMstr.ClearStatus();
        }
    }
}
