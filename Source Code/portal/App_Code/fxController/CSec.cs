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
using System.Collections;
using System.Data.Common;
using System.Data.OracleClient;
using System.Security;
using System.Security.Cryptography;
using DataAccess;

/// <summary>
/// Summary description for CSec
/// </summary>
public class CSec
{
    //user name and password options
    const int cnMinUserIDLength = 3;
    const int cnMaxUserIDLength = 30;
    const int cnMinPWDLength = 8;
    const int cnMaxPWDLength = 20;
    const int cnPWDUpperCaseCount = 1;
    const int cnPWDLowerCaseCount = 1;
    const int cnPWDNumberCount = 1;
    const int cnPWDSpecialCharCount = 1;
    
	public CSec()
	{
		
	}

    //does the user name already exist?
    public bool UserNameExists( BaseMaster BaseMstr,
                                string strUserName)
    {
        //check for dupe user name, to do this encrypt the 
        //username and call an sp to check for a dupe user name
        //user name is not case sensitive so always store lower case
        string strUName = strUserName;
        strUName = strUName.ToLower();
        strUName = Enc(strUName, "");

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);
        plist.AddInputParameter("pi_vEncUID", strUName);

        //get and return a dataset
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_FX_SEC.GetFXUserRS",
                                            plist,
                                            out lStatusCode,
                                            out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
            if (ds != null)
            {
                //loop and set roles on the base master
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            //call failed but tell user that name exists
            //to prevent dupes...
            return true;
        }
    

        return false;
    }

//make sure username and password follow the rules...
    public bool ValidateUserAccountRules( BaseMaster BaseMstr,
                                          string strUserName,
                                          string strPassword)
    {   
        //set the status to good to start
        BaseMstr.StatusCode = 0;
        BaseMstr.StatusComment = "";

        //user names and passwords cannot have spaces
        if (GetNumberSpacesCount(strUserName) > 0 ||
            GetNumberSpacesCount(strPassword) > 0)
        {
            BaseMstr.StatusCode = 899;
            BaseMstr.StatusComment = "User name/password cannot contain spaces!";
        }

        //user name must be at least cnMinUserIDLength long
        if (strUserName.Length < cnMinUserIDLength)
        {
            BaseMstr.StatusCode = 900;
            BaseMstr.StatusComment = "User name must be at least " + Convert.ToString(cnMinUserIDLength) + " characters!";
        }

        //user name must be less than cnMaxUserIDLength long
        if (strUserName.Length > cnMaxUserIDLength)
        {
            BaseMstr.StatusCode = 901;
            BaseMstr.StatusComment = "User name must be less than " + Convert.ToString(cnMaxUserIDLength) + " characters!";
        }
        
        //pwd must be between cnMinPWDLength and cnMaxPWDLength
        if (strPassword.Length < cnMinPWDLength)
        {
            BaseMstr.StatusCode = 902;
            BaseMstr.StatusComment = "Password must be at least " + Convert.ToString(cnMinPWDLength) + " characters!";
        }
        if (strPassword.Length > cnMaxPWDLength)
        {
            BaseMstr.StatusCode = 903;
            BaseMstr.StatusComment = "Password must be less than " + Convert.ToString(cnMaxPWDLength) + " characters!";
        }

         // ---------------------------
        long lPWDSpecial = 0;
        long lPWDUpper = 0;
        long lPWDLower = 0;
        long lPWDNumber = 0;

        string strErrMsg =  "Password must contain at least three of the following kinds of characters: <br/>";
               strErrMsg += "<ul>";
               strErrMsg += "<li>Upper case letters (ABC...)<li>";
               strErrMsg += "<li>Lower case letters (abc...)<li>";
               strErrMsg += "<li>Numbers (0123456789)<li>";
               strErrMsg += "<li>\"Special characters,\" such as " + HttpUtility.HtmlEncode("(!@#$%^&*()_-+=|\":;?/>.,~`") + "&#39)" + "<li>";
               strErrMsg += "</ul>";
        
        //password must contain ar least 1 special chars
        //!@#$%^&*()_-+=|\"':;?/>.<,~`
        if (GetSpecialCharCount(strPassword) >= cnPWDSpecialCharCount)
        {
            lPWDSpecial = 1;
            //BaseMstr.StatusCode = 904;
            //BaseMstr.StatusComment = "Password must contain at least " + Convert.ToString(cnPWDSpecialCharCount) + " special characters such as " + HttpUtility.HtmlEncode("(!@#$%^&*()_-+=|\":;?/>.,~`") + "&#39)!";
        }

        //pwd must have cnPWDUpperCaseCount upper case chars
        if (GetUpperCharCount(strPassword) >= cnPWDUpperCaseCount)
        {
            lPWDUpper = 1;
            //BaseMstr.StatusCode = 905;
            //BaseMstr.StatusComment = "Password must contain at least " + Convert.ToString(cnPWDUpperCaseCount) + " upper case characters!";
        }

        //pwd must have cnPWDLowerCaseCount lower case chars
        if (GetLowerCharCount(strPassword) >= cnPWDLowerCaseCount)
        {
            lPWDLower = 1;
            //BaseMstr.StatusCode = 906;
            //BaseMstr.StatusComment = "Password must contain at least " + Convert.ToString(cnPWDLowerCaseCount) + " lower case characters!";
        }

        //pwd must have cnPWDNumberCount numbers
        if (GetNumberCharCount(strPassword) >= cnPWDNumberCount)
        {
            lPWDNumber = 1;
            //BaseMstr.StatusCode = 907;
            //BaseMstr.StatusComment = "Password must contain at least " + Convert.ToString(cnPWDNumberCount) + " numbers!";
        } 

        if((lPWDSpecial + lPWDUpper + lPWDLower + lPWDNumber) < 3)
        {
            BaseMstr.StatusCode = 907;
            BaseMstr.StatusComment = strErrMsg;
        }

        //false is status is not 0
        if (BaseMstr.StatusCode != 0)
        {
            return false;
        }

        //good
        return true;
    }

    public bool ValidatePasswordRules(BaseMaster BaseMstr,
                                  string strPassword)
    {
        //set the status to good to start
        BaseMstr.StatusCode = 0;
        BaseMstr.StatusComment = "";

        //user names and passwords cannot have spaces
        if (GetNumberSpacesCount(strPassword) > 0)
        {
            BaseMstr.StatusCode = 899;
            BaseMstr.StatusComment = "Password cannot contain spaces!";
        }

        //pwd must be between cnMinPWDLength and cnMaxPWDLength
        if (strPassword.Length < cnMinPWDLength)
        {
            BaseMstr.StatusCode = 902;
            BaseMstr.StatusComment = "Password must be at least " + Convert.ToString(cnMinPWDLength) + " characters!";
        }
        if (strPassword.Length > cnMaxPWDLength)
        {
            BaseMstr.StatusCode = 903;
            BaseMstr.StatusComment = "Password must be less than " + Convert.ToString(cnMaxPWDLength) + " characters!";
        }

        //password must contain ar least 2 special chars
        //!@#$%^&*()_-+=|\"':;?/>.<,~`
        if (GetSpecialCharCount(strPassword) < cnPWDSpecialCharCount)
        {
            BaseMstr.StatusCode = 904;
            BaseMstr.StatusComment = "Password must contain at least " + Convert.ToString(cnPWDSpecialCharCount) + " special characters such as " + HttpUtility.HtmlEncode("(!@#$%^&*()_-+=|\":;?/>.,~`") + "&#39)!";
        }

        //pwd must have cnPWDUpperCaseCount upper case chars
        if (GetUpperCharCount(strPassword) < cnPWDUpperCaseCount)
        {
            BaseMstr.StatusCode = 905;
            BaseMstr.StatusComment = "Password must contain at least " + Convert.ToString(cnPWDUpperCaseCount) + " upper case characters!";
        }

        //pwd must have cnPWDLowerCaseCount lower case chars
        if (GetLowerCharCount(strPassword) < cnPWDLowerCaseCount)
        {
            BaseMstr.StatusCode = 906;
            BaseMstr.StatusComment = "Password must contain at least " + Convert.ToString(cnPWDLowerCaseCount) + " lower case characters!";
        }

        //pwd must have cnPWDNumberCount numbers
        if (GetNumberCharCount(strPassword) < cnPWDNumberCount)
        {
            BaseMstr.StatusCode = 907;
            BaseMstr.StatusComment = "Password must contain at least " + Convert.ToString(cnPWDNumberCount) + " numbers!";
        }

        //false is status is not 0
        if (BaseMstr.StatusCode != 0)
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

    //gets the number of upper case chars in a string
    public int GetUpperCharCount(string str)
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
        }

        return nCount;
    }

    //gets the number of lower case chars in a string
    public int GetLowerCharCount(string str)
    {
        int nCount = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string strC = str.Substring(i, 1);
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
            if(strC == "!"){ nCount++; }
            if(strC == "@"){ nCount++; }
            if(strC == "#"){ nCount++; }
            if(strC == "$"){ nCount++; }
            if(strC == "%"){ nCount++; }
            if(strC == "^"){ nCount++; }
            if(strC == "&"){ nCount++; }
            if(strC == "*"){ nCount++; }
            if(strC == "("){ nCount++; }
            if(strC == ")"){ nCount++; }
            if(strC == "_"){ nCount++; }
            if(strC == "-"){ nCount++; }
            if(strC == "+"){ nCount++; }
            if(strC == "="){ nCount++; }
            if(strC == "|"){ nCount++; }
            if(strC == "\\"){ nCount++; }
            if(strC == "\""){ nCount++; }
            if(strC == "'"){ nCount++; }
            if(strC == ":"){ nCount++; }
            if(strC == ";"){ nCount++; }
            if(strC == "?"){ nCount++; }
            if(strC == "/"){ nCount++; }
            if(strC == ">"){ nCount++; }
            if(strC == "."){ nCount++; }
            if(strC == "<"){ nCount++; }
            if(strC == ","){ nCount++; }
            if(strC == "~"){ nCount++; }
            if(strC == "`"){ nCount++; }
        }

        return nCount;
    }

    //create a framework user
    public bool InsertPatientFXUser(BaseMaster BaseMstr,
                              string strPatientID,
                              string strUserName,
                              string strPassword,
                              bool bAccountLocked,
                              bool bAccountInactive,
                              out long lUserID)
    {
        lUserID = -1;

        //do the actual insert, at this point the username/pwd etc...
        //has already been validated

        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vKey", BaseMstr.Key);
        plist.AddInputParameter("pi_vPatientID", strPatientID);

        //user name and password are encrypted in the db, case does not
        //matter for user name...
        plist.AddInputParameter("pi_vUserName", Enc(strUserName.ToLower(), ""));
        plist.AddInputParameter("pi_vPassword", Enc(strPassword, ""));

        plist.AddInputParameter("pi_nAccountLocked", Convert.ToInt32(bAccountLocked));
        plist.AddInputParameter("pi_nAccountInactive", Convert.ToInt32(bAccountInactive));

        plist.AddInputParameter("pi_vCOldPassword", strPassword);
        plist.AddInputParameter("pi_vCPassword", strPassword);
        plist.AddInputParameter("pi_vCUserName", strUserName);

        //long lFXUserID = -1;
        plist.AddOutputParameter("po_nFXUserID", lUserID);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_FX_SEC_PATIENT.InsertPatientFXUser",
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
            //BaseMstr.StatusComment = "Patient - Saved. ";
            //set the out param
            //Orig CDataParameter paramValue = plist.GetItemByName("po_vDBSessionID");
            CDataParameter paramValue = plist.GetItemByName("po_nFXUserID");
            lUserID = paramValue.LongParameterValue;

            return true;
        }

        return false;
    }

    //update a framework user
    public bool UpdateFXUser(BaseMaster BaseMstr,
                              long lFXUserID,
                              string strUserName,
                              string strPassword,
                              bool bAccountLocked,
                              bool bAccountInactive)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_nFXUserID", lFXUserID);
        plist.AddInputParameter("pi_vUserName", Enc(strUserName.ToLower(), ""));
        plist.AddInputParameter("pi_vPassword", Enc(strPassword, ""));
        plist.AddInputParameter("pi_nAccountLocked", Convert.ToInt32(bAccountLocked));
        plist.AddInputParameter("pi_nAccountInactive", Convert.ToInt32(bAccountInactive));

        BaseMstr.DBConn.ExecuteOracleSP("PCK_FX_SEC.UpdateFXUser",
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

    /*
        pi_nFxUserID        in number,
        pi_nUserType        in number,
        pi_nUserRights      in number,
        pi_nUserReadOnly    in number
    */

    //update fx_user_rights
    public bool UpdateFXUserRights(BaseMaster BaseMstr,
                                    long lFXUserID,
                                    long lUserType,
                                    long lUserRights,
                                    long lReadOnly)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_nFxUserID", lFXUserID);
        plist.AddInputParameter("pi_nUserType", lUserType);
        plist.AddInputParameter("pi_nUserRights", lUserRights);
        plist.AddInputParameter("pi_nUserReadOnly", lReadOnly);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_FX_SEC.UpdateFXUserRights",
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

    //update a framework user
    public bool UpdateFXUserOptions(BaseMaster BaseMstr,
                              long lFXUserID,
                              bool bAccountLocked,
                              bool bAccountInactive)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_nFXUserID", lFXUserID);
        plist.AddInputParameter("pi_nAccountLocked", Convert.ToInt32(bAccountLocked));
        plist.AddInputParameter("pi_nAccountInactive", Convert.ToInt32(bAccountInactive));

        BaseMstr.DBConn.ExecuteOracleSP("PCK_FX_SEC.UpdateFXUserOptions",
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

    //update a framework user
    public bool UpdatePatientFXUserPWD(BaseMaster BaseMstr,
                              long lFXUserID,
                              string strUserName,
                              string strPassword,
                              bool bAccountLocked,
                              bool bAccountInactive)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vKey", BaseMstr.Key);
        plist.AddInputParameter("pi_nFXUserID", lFXUserID);
        plist.AddInputParameter("pi_vUserName", Enc(strUserName.ToLower(), ""));
        plist.AddInputParameter("pi_vPassword", Enc(strPassword, ""));
        plist.AddInputParameter("pi_nAccountLocked", Convert.ToInt32(bAccountLocked));
        plist.AddInputParameter("pi_nAccountInactive", Convert.ToInt32(bAccountInactive));

        plist.AddInputParameter("pi_vCPassword", strPassword);
        plist.AddInputParameter("pi_vCUserName", strUserName);

        BaseMstr.DBConn.ExecuteOracleSP("PCK_FX_SEC_PATIENT.UpdatePatientFXUserPWD",
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
            BaseMstr.StatusComment = "Patient - Updated.";
            return true;
        }

        return false;
    }

    //update a framework user
    public bool UpdatePatientFXUserOptions(BaseMaster BaseMstr,
                              long lFXUserID,
                              bool bAccountLocked,
                              bool bAccountInactive)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add params for the DB stored procedure call

        plist.AddInputParameter("pi_nFXUserID", lFXUserID);
        plist.AddInputParameter("pi_nAccountLocked", Convert.ToInt32(bAccountLocked));
        plist.AddInputParameter("pi_nAccountInactive", Convert.ToInt32(bAccountInactive));

        BaseMstr.DBConn.ExecuteOracleSP("PCK_FX_SEC_PATIENT.UpdatePatientFXUserOptions",
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

    //logoff
    public void LogOff(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //execute the sp
        BaseMstr.DBConn.ExecuteOracleSP("PCK_FX_SEC.LogOff",
                                         plist,
                                         out lStatusCode,
                                         out strStatusComment);

        //set the base master status code and status for display
        BaseMstr.StatusCode = lStatusCode;
        BaseMstr.StatusComment = strStatusComment;
        if (lStatusCode == 0)
        {
        }
    }

    //audit page access
    public bool AuditPageAccess(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //get the page name
        plist.AddInputParameter("pi_vPageName", BaseMstr.GetPageName());

        //execute the sp
        BaseMstr.DBConn.ExecuteOracleSP( "PCK_FX_SEC.AuditPageAccess",
                                         plist,
                                         out lStatusCode,
                                         out strStatusComment);

        //set the base master status code and status for display
        if (lStatusCode == 0)
        {
            return true;
        }
        
        return false;
    }

    //auto login with cert
    public bool CertLogin(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";
        
        //get the cert unique id from the certificate
        string strCert = "";
        if (!GetClientCertUniqueID(BaseMstr.Request.ClientCertificate, out strCert))
        {
            lStatusCode = 1;
            strStatusComment = "Error retrieving information from CAC certificate, please try again!";
            return false;
        }    
        
        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add in additional params for the DB call
        plist.AddInputParameter("pi_vCert", strCert);
       
        //user id
        long lFXUserID = -1;
        plist.AddOutputParameter("po_nUserID", lFXUserID);

        //dbsession id
        string strDBSessionID = "";
        plist.AddOutputParameter("po_vDBSessionID", strDBSessionID);

        //time out
        long lTimeout = -1;
        plist.AddOutputParameter("po_nTimeout", lTimeout);

        //execute the sp call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_FX_SEC.CertLogin",
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
            //get the dbsession id and save it as a session var, this is passed to all other calls
            CDataParameter paramValueDBSess = plist.GetItemByName("po_vDBSessionID");
            BaseMstr.DBSessionID = paramValueDBSess.StringParameterValue;
        
            //get the framework user id
            CDataParameter paramValueUID = plist.GetItemByName("po_nUserID");
            BaseMstr.FXUserID = paramValueUID.LongParameterValue;
            BaseMstr.SetSessionValue("FX_USER_ID", Convert.ToString(paramValueUID.LongParameterValue));

            //get the timeout
            CDataParameter paramT = plist.GetItemByName("po_nTimeout");
            lTimeout = paramT.LongParameterValue;

            //adjust the users timeout based on their permissions....
            BaseMstr.Session.Timeout = (int)lTimeout;

            //redirect back to default, default will now know we are logged in
            BaseMstr.Response.Redirect("portal_revamp.aspx");
            return true;
        }

        return false;
    }

    //gets a unique id from the client cert
    public bool GetClientCertUniqueID( HttpClientCertificate cert,
                                       out string strUID)
    {
        strUID = "";
        if (cert != null)
        {
            //only if there is a client cert present!
            if (cert.IsPresent)
            {
                //choosing the following to uniquely id the CAC
                strUID += cert.Issuer;
                strUID += cert.SerialNumber;
                strUID += cert.ServerSubject;
                strUID += cert.Subject;

                //if no cert then return false
                if (strUID == "")
                {
                    return false;
                }
       
                //encrypting the unique id
                strUID = Enc(strUID, "");

                //sucess
                return true;
            }
        }

        return false;
    }

    //login - returns status info about the login
    public long Login( BaseMaster BaseMstr,
                       string strUserName,
                       string strPassword)
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //encrypt the password
        string strEPasssword = Enc(strPassword, "");

        //encrypt the username too
        string strEUserName = Enc(strUserName.ToLower(), "");

        //get a unique id from the certificate
        string strCert = "";
        if (!GetClientCertUniqueID(BaseMstr.Request.ClientCertificate, out strCert))
        {
            lStatusCode = 1;
            strStatusComment = "Error retrieving information from CAC certificate, please try again!";

            if (BaseMstr.DEV_MODE)//DEV ONLY
            {
                //dummy up the CAC if we are in dev mode...
                strCert  = strUserName;
                strCert  = strCert.ToUpper();
                strCert += "136096198228179118189014240108148223068209177130233128171013234184206077248078218168008009250163034145175109071219000084015189023213155132097220085129063159115198225148209029244072207022183227214042031118137133055221078096159030141231222149112081032081156204077044222005058221019020201067048110179019012048177046243072194139064206093251051140058151085182080176168048094034004102175009163198144218107132159197120093018014092181018140082211183163230156210216133162194091233103172189000031188200105012121125052233031013000219029023154106000158101013186151027000150243033069018012254209111160095077128060012032078089151249217007137032186045212122207081008019034180108173077246078122021214156016090228121149130082204100158208107108091178098068106090003112153222074079060146";
                lStatusCode = 0;
                strStatusComment = "";
            }
            else
            {
                return 1;
            }
        }        
       
        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add in additional params for the DB call
        plist.AddInputParameter("pi_vUserName", strEUserName);
        plist.AddInputParameter("pi_vPassword", strEPasssword);
        plist.AddInputParameter("pi_vCert", strCert);

        //user id
        long lFXUserID = -1;
        plist.AddOutputParameter("po_nUserID", lFXUserID);

        //dbsession id
        string strDBSessionID = "";
        plist.AddOutputParameter("po_vDBSessionID", strDBSessionID);

        long lTimeout = -1;
        plist.AddOutputParameter("po_nTimeout", lTimeout);

        //get a dataset from the sp call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_FX_SEC.Login",
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
            //get the dbsession id and save it as a session var, this is passed to all other calls
            CDataParameter paramValueDBSess = plist.GetItemByName("po_vDBSessionID");
            BaseMstr.DBSessionID = paramValueDBSess.StringParameterValue;
        
            //get the framework user id
            CDataParameter paramValueUID = plist.GetItemByName("po_nUserID");
            BaseMstr.FXUserID = paramValueUID.LongParameterValue;
            BaseMstr.SetSessionValue("FX_USER_ID", Convert.ToString(paramValueUID.LongParameterValue));
            
            CDataParameter paramT = plist.GetItemByName("po_nTimeout");
            lTimeout = paramT.LongParameterValue;

            //adjust the users timeout based on their permissions....
            BaseMstr.Session.Timeout = (int)lTimeout;

            return lStatusCode;
        }

        return lStatusCode;
    }

    //Sign
    public bool Sign( BaseMaster BaseMstr,
                      string strUserName,
                      string strPassword,
                      out string strProviderID,
                      out long lUserType)
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";
        strProviderID = "";
        lUserType = -1;

        //encrypt the password
        string strEPasssword = Enc(strPassword, "");

        //encrypt the username too
        string strEUserName = Enc(strUserName.ToLower(), "");

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add in additional params for the DB call
        plist.AddInputParameter("pi_vUserName", strEUserName);
        plist.AddInputParameter("pi_vPassword", strEPasssword);
       
        //provider id
        long lFXUserID = -1;
        plist.AddOutputParameter("po_vProviderID", strProviderID);
        plist.AddOutputParameter("po_nUserType", lUserType);

       
        //get a dataset from the sp call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_FX_SEC.Sign",
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
            //get the dbsession id and save it as a session var, this is passed to all other calls
            CDataParameter paramValue = plist.GetItemByName("po_vProviderID");
            strProviderID = paramValue.StringParameterValue;

            CDataParameter paramValue2 = plist.GetItemByName("po_nUserType");
            lUserType = paramValue2.LongParameterValue;
            
            return true;
        }

        return false;
    }

    //Change Password - returns status info about the login
    public long ChangePassword(BaseMaster BaseMstr,
                               string strUserName,
                               string strOldPassword,
                               string strPassword)
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //encrypt the password
        string strEPasssword = Enc(strPassword, "");
        string strEOldPassword = Enc(strOldPassword, "");

        if (strEPasssword == strEOldPassword)
        {
            lStatusCode = 1;
            strStatusComment = "New password cannot be the same as the old password!";
            return lStatusCode;
        }

        //get a unique id from the certificate
        string strCert = "";
        if (!GetClientCertUniqueID(BaseMstr.Request.ClientCertificate, out strCert))
        {
            lStatusCode = 1;
            strStatusComment = "Error retrieving information from CAC certificate, please try again!";

            if (BaseMstr.DEV_MODE)//DEV ONLY
            {
                //dummy up the CAC if we are in dev mode...
                strCert = strUserName;
                strCert = strCert.ToUpper();
                strCert += "136096198228179118189014240108148223068209177130233128171013234184206077248078218168008009250163034145175109071219000084015189023213155132097220085129063159115198225148209029244072207022183227214042031118137133055221078096159030141231222149112081032081156204077044222005058221019020201067048110179019012048177046243072194139064206093251051140058151085182080176168048094034004102175009163198144218107132159197120093018014092181018140082211183163230156210216133162194091233103172189000031188200105012121125052233031013000219029023154106000158101013186151027000150243033069018012254209111160095077128060012032078089151249217007137032186045212122207081008019034180108173077246078122021214156016090228121149130082204100158208107108091178098068106090003112153222074079060146";
                lStatusCode = 0;
                strStatusComment = "";
            }
            else
            {
                return 1;
            }
        }

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //add in additional params for the DB call
        plist.AddInputParameter("pi_vKey", BaseMstr.Key);
        string strEUserName = Enc(strUserName.ToLower(), "");
        plist.AddInputParameter("pi_vUserName", strEUserName);
        plist.AddInputParameter("pi_vOldPassword", strEOldPassword);
        plist.AddInputParameter("pi_vPassword", strEPasssword);
        plist.AddInputParameter("pi_vCert", strCert);

        //added 04/16/2012 Security updates
        plist.AddInputParameter("pi_vCOldPassword", strOldPassword);
        plist.AddInputParameter("pi_vCPassword", strPassword);
        plist.AddInputParameter("pi_vCUserName", strUserName);
        
        //user id
        long lFXUserID = -1;
        plist.AddOutputParameter("po_nUserID", lFXUserID);

        //dbsession id
        string strDBSessionID = "";
        plist.AddOutputParameter("po_vDBSessionID", strDBSessionID);

        long lTimeout = -1;
        plist.AddOutputParameter("po_nTimeout", lTimeout);

        //get a dataset from the sp call
        BaseMstr.DBConn.ExecuteOracleSP("PCK_FX_SEC.ChangePassword",
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
            //get the dbsession id and save it as a session var, this is passed to all other calls
            CDataParameter paramValueDBSess = plist.GetItemByName("po_vDBSessionID");
            BaseMstr.DBSessionID = paramValueDBSess.StringParameterValue;
        
            //get the framework user id
            CDataParameter paramValueUID = plist.GetItemByName("po_nUserID");
            BaseMstr.FXUserID = paramValueUID.LongParameterValue;
            BaseMstr.SetSessionValue("FX_USER_ID", Convert.ToString(paramValueUID.LongParameterValue));

            CDataParameter paramT = plist.GetItemByName("po_nTimeout");
            lTimeout = paramT.LongParameterValue;

            //adjust the users timeout based on their permissions....
            BaseMstr.Session.Timeout = (int)lTimeout;

            return lStatusCode;
        }

        return lStatusCode;
    }
    
    /// <summary>
    /// encrypt a string using the key in the web.config
    /// </summary>
    /// <param name="strClear"></param>
    /// <returns></returns>
    public string Enc(string strClear, string strInitVector)
    {
        //triple des
        TripleDES des = new TripleDESCryptoServiceProvider();

        //key and interrupt vector
        //CR 07/18/2011 modified to pull from the connection strings so that they can be encrypted
        //string strKey = System.Configuration.ConfigurationManager.AppSettings["KEY"].ToString();
        //string strIV = System.Configuration.ConfigurationManager.AppSettings["IV"].ToString();
        //
        //set the IV = to the IV passed in
        string strIV = strInitVector;
        
        //get the key from the config file
        string strKey = "";
        try
        {
            //try to get the connection string from the encrypted connectionstrings section
            strKey = ConfigurationManager.ConnectionStrings["SEC"].ConnectionString;
            strKey = strKey.Substring(0, 24);
        }
        catch (Exception eee)
        {
            //pull from appsettings if failed, this lets developers connect from local boxes.
            //strKey = System.Configuration.ConfigurationManager.AppSettings["KEY"];
            string strStatus = eee.Message;
        }

        //if no IV then use the IV from the config file
        if (strIV == "")
        {
            try
            {
                //try to get the connection string from the encrypted connectionstrings section
                strIV = ConfigurationManager.ConnectionStrings["SEC"].ConnectionString;
                strIV = strIV.Substring(strIV.Length - 8);
            }
            catch (Exception eee)
            {
                //pull from appsettings if failed, this lets developers connect from local boxes.
                //strIV = System.Configuration.ConfigurationManager.AppSettings["IV"];
                string strStatus = eee.Message;
            }
        }

        //set the key and vector
        System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        des.Key = enc.GetBytes(strKey);
        des.IV = enc.GetBytes(strIV);

        //encrypt
        ICryptoTransform ctEncrypt = des.CreateEncryptor();
        byte[] bCClear = System.Text.Encoding.Unicode.GetBytes(strClear);
        byte[] bCEncrypted = ctEncrypt.TransformFinalBlock(bCClear, 0, bCClear.Length);

        //convert the encrypted to string
        string strEnc = "";
        for (int i = 0; i < bCEncrypted.Length; i++)
        {
            string str = bCEncrypted[i].ToString();
            while (str.Length < 3)
            {
                str = "0" + str;
            }

            strEnc += str;
        }

        strKey = "xtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtx";
        strIV = "xtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtxtx";

        //return the string
        return strEnc;
    }

    /// <summary>
    /// decrypt a string using the key in the web.config
    /// </summary>
    /// <param name="strEnc"></param>
    /// <returns></returns>
    public string dec(string strEnc, string strInitVector)
    {
        string strEClear = "";

        byte[] bCEncrypted = new byte[strEnc.Length / 3];
        int noffset = 0;
        for (int j = 0; j < strEnc.Length; j += 3)
        {
            bCEncrypted[noffset++] = Convert.ToByte(strEnc.Substring(j, 3));
        }

        //if nothing to decrypt just return ""
        if (bCEncrypted.Length < 1)
        {
            return strEClear;
        }

        TripleDES des = new TripleDESCryptoServiceProvider();

        //key and interrupt vector
        //
        //CR 07/18/2011 modified to pull from the connection strings so that they can be encrypted
        //string strKey = System.Configuration.ConfigurationManager.AppSettings["KEY"].ToString();
        //string strIV = System.Configuration.ConfigurationManager.AppSettings["IV"].ToString();
        //

        //set the IV = to the IV passed in
        string strIV = strInitVector;

        //get the key from the config file
        string strKey = "";
        try
        {
            //try to get the connection string from the encrypted connectionstrings section
            strKey = ConfigurationManager.ConnectionStrings["SEC"].ConnectionString;
            strKey = strKey.Substring(0, 24);
        }
        catch (Exception eee)
        {
            //pull from appsettings if failed, this lets developers connect from local boxes.
            //strKey = System.Configuration.ConfigurationManager.AppSettings["KEY"];
            string strStatus = eee.Message;
        }

        //if no IV then use the IV from the config file
        if (strIV == "")
        {
            try
            {
                //try to get the connection string from the encrypted connectionstrings section
                strIV = ConfigurationManager.ConnectionStrings["SEC"].ConnectionString;
                strIV = strIV.Substring(strIV.Length - 8);
            }
            catch (Exception eee)
            {
                //pull from appsettings if failed, this lets developers connect from local boxes.
                //strIV = System.Configuration.ConfigurationManager.AppSettings["IV"];
                string strStatus = eee.Message;
            }
        }

        //set the key and vector
        System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        des.Key = enc.GetBytes(strKey);
        des.IV = enc.GetBytes(strIV);

        //decrypt       
        ICryptoTransform ctDecrypt = des.CreateDecryptor();
        byte[] bEClear = ctDecrypt.TransformFinalBlock(bCEncrypted, 0, bCEncrypted.Length);
        strEClear = System.Text.Encoding.Unicode.GetString(bEClear);

        strKey = "xyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxxyx";
        strIV = "xyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyxyx";

        return strEClear;
    }

    //get a dataset of FX_User Username and Password
    public DataSet GetFXUsernamePasswordDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vProviderID", BaseMstr.SelectedProviderID);
        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_FX_SEC.getFXUsernamePasswordRS",
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

    //get a dataset of FX_User Username and Password
    public DataSet GetPatientFXUsernamePasswordDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vKey", BaseMstr.Key);
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_FX_SEC_PATIENT.getPatientFXUsernamePasswordRS",
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

    //get a dataset of FXUserID
    public DataSet GetFXUserIdDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vProviderID", BaseMstr.SelectedProviderID);
        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_FX_SEC.getFXUserIdRS",
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

    //get user access permissions
    public long GetRightMode(BaseMaster BaseMstr, long lUR)
    {
        long lRightMode = 0; // no access
        long lUsrRights = BaseMstr.APPMaster.UserRights;
        long lUsrReadOnlyRights = BaseMstr.APPMaster.UserReadOnly;

        if (BaseMstr.APPMaster.HasUserRight(lUR))
        {
            if ((lUR & lUsrReadOnlyRights) > 0)
            {
                lRightMode = 1; // read-only
            }
            else
            {
                lRightMode = 2; // read,write
            }
        }
        return lRightMode;
    }

    //get a dataset of FXUserID
    public DataSet GetPatientFXUserIdDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vKey", BaseMstr.Key);
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);

        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(
            BaseMstr.DBConn,
            "PCK_FX_SEC_PATIENT.getPatientFXUserIdRS",
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

    //get a dataset of FXUserID
    public DataSet CheckPatientFXUserRecDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        plist.AddInputParameter("pi_vKey", BaseMstr.Key);
        plist.AddInputParameter("pi_vPatientID", BaseMstr.SelectedPatientID);
        
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_FX_SEC_PATIENT.CheckPatientFXUserRecRS",
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

    //get a dataset of FXUserID
    public DataSet CheckFXUserRecDS(BaseMaster BaseMstr)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(BaseMstr);

        //todo: add in additional params for the DB call

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vProviderID", BaseMstr.SelectedProviderID);
        //
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(BaseMstr.DBConn,
                                           "PCK_FX_SEC.CheckFXUserRecRS",
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
}
