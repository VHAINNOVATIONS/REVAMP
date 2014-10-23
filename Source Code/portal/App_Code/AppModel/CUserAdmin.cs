using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DataAccess;

/// <summary>
/// Summary description for CUserAdmin
/// </summary>
public class CUserAdmin
{

    //User
    const int cnName = 401;
    const int cnRank = 402;
    const int cnTitle = 403;
    const int cnCorps = 404;
    const int cnSquadron = 405;
    const int cnOfficeSymbol = 406;
    const int cnAddress = 408;
    const int cnCity = 409;
    const int cnState = 410;
    const int cnPostalCode = 411;
    const int cnPhone = 412;
    const int cnEmail = 413;

	public CUserAdmin()
	{
		
	}

    public DataSet GetUserLookupDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);
        plist.AddInputParameter("pi_vDMISID", BaseMstr.APPMaster.UserDMISID);
        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_USER_ADMIN.getUserLookupRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public DataSet GetUserLookupBySearchDS(BaseMaster BaseMstr,
                                             string strSearchValue)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);
        plist.AddInputParameter("pi_vSearchValue", strSearchValue);
        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_USER_ADMIN.getUserLookupBySearchRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public DataSet GetSuatUserDS(BaseMaster BaseMstr) 
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);
    
        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_vProviderID", BaseMstr.SelectedProviderID ); 
        
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_USER_ADMIN.GetSuatUserRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }

    }
    //
    public DataSet GetSuatUserNameDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_nFXUserID", BaseMstr.FXUserID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_USER_ADMIN.GetSuatUserNameRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }

    }
    //
    public bool InsertSuatUser(BaseMaster BaseMstr,
                                     string strProviderId,
                                     int    intLocked,
                                     string strName,
                                     string strRank,
                                     int    intServiceID,
                                     string strTitle,
                                     string strCorps,
                                     string strOfficeSymbol,
                                     string strSquadron,
                                     string strPhone,
                                     string strEmail,
                                     string strDimsID,
                                     string strUIDPWD,
                                     int intMustChangePWD,
                                     string strSupervisorID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vProviderID", strProviderId);
        plist.AddInputParameter("pi_nLocked", intLocked);
        plist.AddInputParameter("pi_vName", strName);
        plist.AddInputParameter("pi_vRank", strRank);
        plist.AddInputParameter("pi_nServiceID", intServiceID);
        plist.AddInputParameter("pi_vTitle", strTitle);
        plist.AddInputParameter("pi_vCorps", strCorps);
        plist.AddInputParameter("pi_vOfficeSymbol", strOfficeSymbol);
        plist.AddInputParameter("pi_vSquadron", strSquadron);
        plist.AddInputParameter("pi_vPhone", strPhone);
        plist.AddInputParameter("pi_vEmail", strEmail);
        plist.AddInputParameter("pi_vDimsID", strDimsID);
        plist.AddInputParameter("pi_vUIDPWD", strUIDPWD);
        plist.AddInputParameter("pi_nMustChgPwd", intMustChangePWD);
        plist.AddInputParameter("pi_vSupervisorID", strSupervisorID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_AP_USERADMIN.InsertSuatUser",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base mastekr status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }

        return false;
    }

    public bool UpdateSuatUser(BaseMaster BaseMstr,
                                     int    intLocked,
                                     string strName,
                                     string strRank,
                                     int intServiceID,
                                     string strTitle,
                                     string strCorps,
                                     string strOfficeSymbol,
                                     string strSquadron,
                                     string strPhone,
                                     string strEmail,
                                     string strDimsID,
                                     string strUIDPWD,
                                     int intMustChangePWD,
                                    string strSupervisorID)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_vProviderID", BaseMstr.SelectedProviderID);
        plist.AddInputParameter("pi_vCurrentDimsID", BaseMstr.APPMaster.UserDMISID); //** Note - Need to get the DMIS of the Selected Provider ID.
        plist.AddInputParameter("pi_nLocked", intLocked);
        plist.AddInputParameter("pi_vName", strName);
        plist.AddInputParameter("pi_vRank", strRank);
        plist.AddInputParameter("pi_nServiceID", intServiceID);
        plist.AddInputParameter("pi_vTitle", strTitle);
        plist.AddInputParameter("pi_vCorps", strCorps);
        plist.AddInputParameter("pi_vOfficeSymbol", strOfficeSymbol);
        plist.AddInputParameter("pi_vSquadron", strSquadron);
        plist.AddInputParameter("pi_vPhone", strPhone);
        plist.AddInputParameter("pi_vEmail", strEmail);
        plist.AddInputParameter("pi_vDimsID", strDimsID);
        plist.AddInputParameter("pi_vUIDPWD", strUIDPWD);
        plist.AddInputParameter("pi_nMustChgPwd", intMustChangePWD);
        plist.AddInputParameter("pi_vSupervisorID", strSupervisorID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_AP_USERADMIN.UpdateSuatUser",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }

        return false;
    }

    public DataSet GetFacilityInfoDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call
       
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_USER_ADMIN.GetFacilityInfoRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }

    }

    public bool InsertFacilityInfo(BaseMaster BaseMstr,
                                      string strSiteID,
                                      string strSiteName,
                                      string strSiteAddress,
                                      string strSiteCity,
                                      string strSiteState,
                                      string strSitePostalCode)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_vSiteID", strSiteID);
        plist.AddInputParameter("pi_vSiteName", strSiteName);
        plist.AddInputParameter("pi_vSiteAddress1", strSiteAddress);
        plist.AddInputParameter("pi_vSiteCity", strSiteCity);
        plist.AddInputParameter("pi_vSiteState", strSiteState);
        plist.AddInputParameter("pi_vSitePostalCode", strSitePostalCode);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_USER_ADMIN.InsertFacilityInfo",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }

        return false;
    }

    public bool UpdateFacilityInfo(BaseMaster BaseMstr,
                                      string strSiteID,
                                      string strSiteName,
                                      string strSiteAddress,
                                      string strSiteCity,
                                      string strSiteState,
                                      string strSitePostalCode)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_vSiteID", strSiteID);
        plist.AddInputParameter("pi_vSiteName",strSiteName);
        plist.AddInputParameter("pi_vSiteAddress1",strSiteAddress);
        plist.AddInputParameter("pi_vSiteCity", strSiteCity);
        plist.AddInputParameter("pi_vSiteState",strSiteState);
        plist.AddInputParameter("pi_vSitePostalCode",strSitePostalCode);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_USER_ADMIN.UpdateFacilityInfo",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }

        return false;
    }

    //get a dataset of User Rights
    public DataSet GetUserRightsDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_AP_USERADMIN.GetUserRightsRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }

    }
    
    //get Supervisors Dataset
    public DataSet GetInternSupervisorsDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_AP_USERADMIN.GetInternSupervisorsRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }

    }

    //Load Supervisors Combo
    public void LoadSupervisorCombo(BaseMaster BaseMstr, DropDownList ddlst) 
    {
        DataSet ds = GetInternSupervisorsDS(BaseMstr);
        if(ds != null)
        {
            ddlst.DataSource = ds;
            ddlst.DataTextField = "NAME";
            ddlst.DataValueField = "PROVIDER_ID";
            ddlst.DataBind();
            ddlst.Items.Insert(0, new ListItem(String.Empty, "-1"));
        }
    }

    //load a checkbox list of User Rights
    public void LoadUserRightsCheckboxList(BaseMaster BaseMstr,
                                            CheckBoxList chklst)
    {
        //get the data to load
        DataSet ds = GetUserRightsDS(BaseMstr);

        //load the combo
        CCheckBoxList cl = new CCheckBoxList();
        cl.RenderDataSet(BaseMstr,
                          ds,
                          chklst,
                          "RIGHT_DESC",
                          "RIGHT_DEC"
                          );
    }

 
    //get a dataset of User Types
    public DataSet GetUserTypesDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_AP_USERADMIN.GetUserTypesRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }

    }

    //load a radio button list of user types
    public void LoadUserTypesRadioButtonList(BaseMaster BaseMstr,
                                            RadioButtonList rbllst,
                                            string strSelectedID)
    {
        //get the data to load
        DataSet ds = GetUserTypesDS(BaseMstr);

        //load the combo
        CRadioButtonList rl = new CRadioButtonList();
        rl.RenderDataSet(BaseMstr,
                          ds,
                          rbllst,
                          "USERTYPE_DESC",
                          "STAT_USERTYPE_ID",
                          strSelectedID
                          );
    }


    //get a dataset of Suat Users
    public DataSet GetSUATUserListDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_AP_USERADMIN.GetSUATUsersRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }


    public bool ValidateUserDemographicRules(BaseMaster BaseMstr,
                                                int iFieldCode,
                                                string strData,
                                                out string strStatusOut)
    {
        //set the status to good to start
        strStatusOut = "";

        //********************* Patient Demographics *********************

        if ((iFieldCode == cnName) && (string.IsNullOrEmpty(strData)))
        {
            strStatusOut = "Name cannot be blank.<br />";
        }

        if ((iFieldCode == cnRank) && (string.IsNullOrEmpty(strData)))
        {
            strStatusOut = "Rank cannot be blank.<br />";
        }

        if ((iFieldCode == cnEmail) && (string.IsNullOrEmpty(strData)))
        {
            strStatusOut = "Email cannot be blank.<br />";
        }
        
        if ((iFieldCode == cnCity) && (GetCitySpecialCharCount(strData) > 0))
        {
            strStatusOut = "City must not contain Special Characters.<br />";
        }
        
        if ((iFieldCode == cnName) && (GetNumberCharCount(strData) > 0))
        {
            strStatusOut = "Name must not contain Numeric Characters.<br />";
        }

        if ((iFieldCode == cnCity) && (GetNumberCharCount(strData) > 0))
        {
            strStatusOut = "City must not contain Numeric Characters.<br />";
        }

        if ((iFieldCode == cnPostalCode) && (GetAlphaCharCount(strData) > 0))
        {
            strStatusOut = "Postal Code must not contain Alpha Characters.<br />";
        }

        if ((iFieldCode == cnPhone) && (GetAlphaCharCount(strData) > 0))
        {
            strStatusOut = "Phone must not contain Alpha Characters.<br />";
        }

        if ((iFieldCode == cnPhone) && (GetPhoneSpecialCharCount(strData) > 0))
        {
            strStatusOut = "Phone '-' and '( )' only Special Characters accepted.<br />";
        }

        //false is status is not empty 
        if (strStatusOut != "")
        {
            return false;
        }

        //good
        return true;
    }

    //gets the number of numbers in a string
    public int GetNumberSpacesCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == " ")
            {
                nCount++;
            }
        }

        return nCount;
    }

    //gets the number of numbers in a string
    public int GetNumberCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "0") { nCount++; }
            if (strC == "1") { nCount++; }
            if (strC == "2") { nCount++; }
            if (strC == "3") { nCount++; }
            if (strC == "4") { nCount++; }
            if (strC == "5") { nCount++; }
            if (strC == "6") { nCount++; }
            if (strC == "7") { nCount++; }
            if (strC == "8") { nCount++; }
            if (strC == "9") { nCount++; }
        }

        return nCount;
    }

    public int GetAlphaCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "A") { nCount++; }
            if (strC == "B") { nCount++; }
            if (strC == "C") { nCount++; }
            if (strC == "D") { nCount++; }
            if (strC == "E") { nCount++; }
            if (strC == "F") { nCount++; }
            if (strC == "G") { nCount++; }
            if (strC == "H") { nCount++; }
            if (strC == "I") { nCount++; }
            if (strC == "J") { nCount++; }
            if (strC == "K") { nCount++; }
            if (strC == "L") { nCount++; }
            if (strC == "M") { nCount++; }
            if (strC == "N") { nCount++; }
            if (strC == "O") { nCount++; }
            if (strC == "P") { nCount++; }
            if (strC == "Q") { nCount++; }
            if (strC == "R") { nCount++; }
            if (strC == "S") { nCount++; }
            if (strC == "T") { nCount++; }
            if (strC == "U") { nCount++; }
            if (strC == "V") { nCount++; }
            if (strC == "W") { nCount++; }
            if (strC == "X") { nCount++; }
            if (strC == "Y") { nCount++; }
            if (strC == "Z") { nCount++; }
            if (strC == "a") { nCount++; }
            if (strC == "b") { nCount++; }
            if (strC == "c") { nCount++; }
            if (strC == "d") { nCount++; }
            if (strC == "e") { nCount++; }
            if (strC == "f") { nCount++; }
            if (strC == "g") { nCount++; }
            if (strC == "h") { nCount++; }
            if (strC == "i") { nCount++; }
            if (strC == "j") { nCount++; }
            if (strC == "k") { nCount++; }
            if (strC == "l") { nCount++; }
            if (strC == "m") { nCount++; }
            if (strC == "n") { nCount++; }
            if (strC == "o") { nCount++; }
            if (strC == "p") { nCount++; }
            if (strC == "q") { nCount++; }
            if (strC == "r") { nCount++; }
            if (strC == "s") { nCount++; }
            if (strC == "t") { nCount++; }
            if (strC == "u") { nCount++; }
            if (strC == "v") { nCount++; }
            if (strC == "w") { nCount++; }
            if (strC == "x") { nCount++; }
            if (strC == "y") { nCount++; }
            if (strC == "z") { nCount++; }

        }

        return nCount;
    }

    //gets the number of special chars in a string...
    public int GetSpecialCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "!") { nCount++; }
            if (strC == "@") { nCount++; }
            if (strC == "#") { nCount++; }
            if (strC == "$") { nCount++; }
            if (strC == "%") { nCount++; }
            if (strC == "^") { nCount++; }
            if (strC == "&") { nCount++; }
            if (strC == "*") { nCount++; }
            if (strC == "(") { nCount++; }
            if (strC == ")") { nCount++; }
            if (strC == "_") { nCount++; }
            //if (strC == "-") { nCount++; } Permitting dash hyphen in name ???
            if (strC == "+") { nCount++; }
            if (strC == "=") { nCount++; }
            if (strC == "|") { nCount++; }
            if (strC == "\\") { nCount++; }
            if (strC == "\"") { nCount++; }
            if (strC == "'") { nCount++; }
            if (strC == ":") { nCount++; }
            if (strC == ";") { nCount++; }
            if (strC == "?") { nCount++; }
            if (strC == "/") { nCount++; }
            if (strC == ">") { nCount++; }
            if (strC == ".") { nCount++; }
            if (strC == "<") { nCount++; }
            if (strC == ",") { nCount++; }
            if (strC == "~") { nCount++; }
            if (strC == "`") { nCount++; }
        }

        return nCount;
    }

    //gets the number of special chars in a string...
    public int GetDateSpecialCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "!") { nCount++; }
            if (strC == "@") { nCount++; }
            if (strC == "#") { nCount++; }
            if (strC == "$") { nCount++; }
            if (strC == "%") { nCount++; }
            if (strC == "^") { nCount++; }
            if (strC == "&") { nCount++; }
            if (strC == "*") { nCount++; }
            if (strC == "(") { nCount++; }
            if (strC == ")") { nCount++; }
            if (strC == "_") { nCount++; }
            if (strC == "-") { nCount++; }
            if (strC == "+") { nCount++; }
            if (strC == "=") { nCount++; }
            if (strC == "|") { nCount++; }
            if (strC == "\\") { nCount++; }
            if (strC == "\"") { nCount++; }
            if (strC == "'") { nCount++; }
            if (strC == ":") { nCount++; }
            if (strC == ";") { nCount++; }
            if (strC == "?") { nCount++; }
            //if (strC == "/") { nCount++; } //Character Allowed
            if (strC == ">") { nCount++; }
            if (strC == ".") { nCount++; }
            if (strC == "<") { nCount++; }
            if (strC == ",") { nCount++; }
            if (strC == "~") { nCount++; }
            if (strC == "`") { nCount++; }
        }

        return nCount;
    }

    //gets the number of special chars in a string...
    public int GetPhoneSpecialCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "!") { nCount++; }
            if (strC == "@") { nCount++; }
            if (strC == "#") { nCount++; }
            if (strC == "$") { nCount++; }
            if (strC == "%") { nCount++; }
            if (strC == "^") { nCount++; }
            if (strC == "&") { nCount++; }
            if (strC == "*") { nCount++; }
            //if (strC == "(") { nCount++; } Character Allowed
            //if (strC == ")") { nCount++; } Character Allowed
            if (strC == "_") { nCount++; }
            //if (strC == "-") { nCount++; } Character Allowed
            if (strC == "+") { nCount++; }
            if (strC == "=") { nCount++; }
            if (strC == "|") { nCount++; }
            if (strC == "\\") { nCount++; }
            if (strC == "\"") { nCount++; }
            if (strC == "'") { nCount++; }
            if (strC == ":") { nCount++; }
            if (strC == ";") { nCount++; }
            if (strC == "?") { nCount++; }
            if (strC == "/") { nCount++; }
            if (strC == ">") { nCount++; }
            if (strC == ".") { nCount++; }
            if (strC == "<") { nCount++; }
            if (strC == ",") { nCount++; }
            if (strC == "~") { nCount++; }
            if (strC == "`") { nCount++; }
        }

        return nCount;
    }

    //gets the number of special chars in a string...
    public int GetCitySpecialCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "!") { nCount++; }
            if (strC == "@") { nCount++; }
            if (strC == "#") { nCount++; }
            if (strC == "$") { nCount++; }
            if (strC == "%") { nCount++; }
            if (strC == "^") { nCount++; }
            if (strC == "&") { nCount++; }
            if (strC == "*") { nCount++; }
            if (strC == "(") { nCount++; }
            if (strC == ")") { nCount++; }
            if (strC == "_") { nCount++; }
            //if (strC == "-") { nCount++; } Character Allowed
            if (strC == "+") { nCount++; }
            if (strC == "=") { nCount++; }
            if (strC == "|") { nCount++; }
            if (strC == "\\") { nCount++; }
            if (strC == "\"") { nCount++; }
            if (strC == "'") { nCount++; }
            if (strC == ":") { nCount++; }
            if (strC == ";") { nCount++; }
            if (strC == "?") { nCount++; }
            if (strC == "/") { nCount++; }
            if (strC == ">") { nCount++; }
            if (strC == ".") { nCount++; }
            if (strC == "<") { nCount++; }
            if (strC == ",") { nCount++; }
            if (strC == "~") { nCount++; }
            if (strC == "`") { nCount++; }
        }

        return nCount;
    }

    //gets the number of special chars in a string...
    public int GetPostalCodeSpecialCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "!") { nCount++; }
            if (strC == "@") { nCount++; }
            if (strC == "#") { nCount++; }
            if (strC == "$") { nCount++; }
            if (strC == "%") { nCount++; }
            if (strC == "^") { nCount++; }
            if (strC == "&") { nCount++; }
            if (strC == "*") { nCount++; }
            if (strC == "(") { nCount++; }
            if (strC == ")") { nCount++; }
            if (strC == "_") { nCount++; }
            //if (strC == "-") { nCount++; } Character Allowed
            if (strC == "+") { nCount++; }
            if (strC == "=") { nCount++; }
            if (strC == "|") { nCount++; }
            if (strC == "\\") { nCount++; }
            if (strC == "\"") { nCount++; }
            if (strC == "'") { nCount++; }
            if (strC == ":") { nCount++; }
            if (strC == ";") { nCount++; }
            if (strC == "?") { nCount++; }
            if (strC == "/") { nCount++; }
            if (strC == ">") { nCount++; }
            if (strC == ".") { nCount++; }
            if (strC == "<") { nCount++; }
            if (strC == ",") { nCount++; }
            if (strC == "~") { nCount++; }
            if (strC == "`") { nCount++; }
        }

        return nCount;
    }

    //gets the number of special chars in a string...
    public int GetNameSpecialCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
            if (strC == "!") { nCount++; }
            if (strC == "@") { nCount++; }
            if (strC == "#") { nCount++; }
            if (strC == "$") { nCount++; }
            if (strC == "%") { nCount++; }
            if (strC == "^") { nCount++; }
            if (strC == "&") { nCount++; }
            if (strC == "*") { nCount++; }
            if (strC == "(") { nCount++; }
            if (strC == ")") { nCount++; }
            if (strC == "_") { nCount++; }
            //if (strC == "-") { nCount++; } Character Allowed
            if (strC == "+") { nCount++; }
            if (strC == "=") { nCount++; }
            if (strC == "|") { nCount++; }
            if (strC == "\\") { nCount++; }
            if (strC == "\"") { nCount++; }
            if (strC == "'") { nCount++; }
            if (strC == ":") { nCount++; }
            if (strC == ";") { nCount++; }
            if (strC == "?") { nCount++; }
            if (strC == "/") { nCount++; }
            if (strC == ">") { nCount++; }
            if (strC == ".") { nCount++; }
            if (strC == "<") { nCount++; }
            if (strC == ",") { nCount++; }
            if (strC == "~") { nCount++; }
            if (strC == "`") { nCount++; }
        }

        return nCount;
    }

    //---------------------------------------------------
    //get Supervisors Dataset
    public DataSet GetRightsTemplateDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_AP_USERADMIN.GetRightsTemplateRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }

    }

    public bool UpdateRightsTemplate(BaseMaster BaseMstr,
                                    long lUserType,
                                    long lUserRights,
                                    long lRightsMode)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_nUserType", lUserType);
        plist.AddInputParameter("pi_nUserRights", lUserRights);
        plist.AddInputParameter("pi_nRightsMode", lRightsMode);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_AP_USERADMIN.InsertRightsTemplate",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base mastekr status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }

        return false;
    }

}
