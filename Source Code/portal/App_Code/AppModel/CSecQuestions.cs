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
/// Summary description for CSecQuestions
/// </summary>
public class CSecQuestions
{
    protected BaseMaster m_BaseMstr { set; get; }
    protected CSec sec;

    public CSecQuestions(BaseMaster BaseMstr)
	{
        m_BaseMstr = BaseMstr;
        sec = new CSec();
	}

    public DataSet GetSecQuestionsRS(long lQuestionGroup)
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        // store procedure specific params
        plist.AddInputParameter("pi_nQuestionGrp", lQuestionGroup);

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_FX_SEC.GetSecurityQuestions",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public DataSet GetUserQuestions(string strUsername)
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        // store procedure specific params
        plist.AddInputParameter("pi_vUsername", strUsername);


        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_FX_SEC.GetUserQuestions",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode != 0)
        {
            return null;
        }

        return ds;
    }

    public bool UpdateSecQuestions(long lQ1ID, 
                                    string strAnswer1,
                                    long lQ2ID,
                                    string strAnswer2,
                                    long lQ3ID,
                                    string strAnswer3)
    {

        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //create a new parameter list
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        /*
            pi_nQuestionID_1 in number,
            pi_vAnswer_1     in varchar2,
                               
            pi_nQuestionID_2 in number,
            pi_vAnswer_2     in varchar2,
                               
            pi_nQuestionID_3 in number,
            pi_vAnswer_3     in varchar2,
         */

        //encrypt answers
        strAnswer1 = sec.Enc(strAnswer1.ToLower(), String.Empty);
        strAnswer2 = sec.Enc(strAnswer2.ToLower(), String.Empty);
        //strAnswer3 = sec.Enc(strAnswer3, String.Empty);
        
        
        plist.AddInputParameter("pi_nQuestionID_1", lQ1ID);
        plist.AddInputParameter("pi_vAnswer_1", strAnswer1);

        plist.AddInputParameter("pi_nQuestionID_2", lQ2ID);
        plist.AddInputParameter("pi_vAnswer_2", strAnswer2);

        plist.AddInputParameter("pi_nQuestionID_3", lQ3ID);
        plist.AddInputParameter("pi_vAnswer_3", strAnswer3);
        

        //get a dataset from the sp call
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_FX_SEC.UpdateSecQuestions",
                                         plist,
                                         out lStatusCode,
                                         out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }

        return false;

    }

    public bool ValidateAnswers(long lFXUserID,
                                string strAnswer1,
                                string strAnswer2,
                                string strAnswer3)
    {

        //status info
        long lStatusCode = 0;
        string strStatusComment = "";
        //lValidate = 0;

        m_BaseMstr.FXUserID = lFXUserID;
        
        //create a new parameter list
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        //plist.AddInputParameter("pi_nQuestionID_1", lQ1ID);
        plist.AddInputParameter("pi_vAnswer_1", strAnswer1);

        //plist.AddInputParameter("pi_nQuestionID_2", lQ2ID);
        plist.AddInputParameter("pi_vAnswer_2", strAnswer2);

        //plist.AddInputParameter("pi_nQuestionID_3", lQ3ID);
        plist.AddInputParameter("pi_vAnswer_3", strAnswer3);

        //plist.AddOutputParameter("po_nValidate", lValidate);

        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_FX_SEC.CheckSecurityQuestions",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);


        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            //CDataParameter paramValue = plist.GetItemByName("po_nValidate");
            //lValidate = paramValue.LongParameterValue;

            return true;
        }
        return false;
    }

    public bool ResetPassword(long lFXUserID,
                            string strUserName,
                            string strPassword)
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        m_BaseMstr.FXUserID = lFXUserID;
        
        //create a new parameter list
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vKey", m_BaseMstr.Key);
        plist.AddInputParameter("pi_nFXUserID", lFXUserID);
        plist.AddInputParameter("pi_vUserName", sec.Enc(strUserName.ToLower(), ""));
        plist.AddInputParameter("pi_vPassword", sec.Enc(strPassword, ""));

        plist.AddInputParameter("pi_vCPassword", strPassword);
        plist.AddInputParameter("pi_vCUserName", strUserName);

        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_FX_SEC.ResetPassword",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (lStatusCode == 0)
        {
            return true;
        }

        return false;
    }



}