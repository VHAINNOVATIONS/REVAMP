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
/// Summary description for CBaseDataClass
/// </summary>
public class CBaseDataClass
{
    protected BaseMaster m_BaseMstr = null;
    protected CDataConnection m_DBConn = null;
    protected long m_lErrorStatus = 0;
    protected string m_strErrorComment="";
    protected string m_strPatientID = "";
    protected string m_strEncounterID = "";
    protected long   m_lEncounterIntakeID = 0;
    protected int    m_nMID = 0;

    //----------------------------------------------------------
    // Base Class for the data layer classess
    public CBaseDataClass()
	{
	}

    //----------------------------------------------------------
    // 0 = success if strStatus is populated it will show on the screen
    // 1 to n are errors and we always show errors
    protected bool ErrorHandler()
    {
        m_BaseMstr.StatusCode = m_lErrorStatus;
        m_BaseMstr.StatusComment = m_strErrorComment;

        if (m_lErrorStatus > 0)
        {
            return false;
        }

        return true;
    }

    //----------------------------------------------------------
    // These are repetitive paramenters that must be used to
    // ensure HIPAA compliance -
    protected CDataParameterList ItemList()
    {
        //create a new parameter list
        CDataParameterList pList = new CDataParameterList();

        //add params for the DB call in paramaters
        //these will always be passed in to all sp calls
        pList.AddInputParameter("pi_vSessionID", m_BaseMstr.DBSessionID);
        pList.AddInputParameter("pi_vSessionClientIP", m_BaseMstr.ClientIP);
        pList.AddInputParameter("pi_nUserID", m_BaseMstr.FXUserID);

        return pList;
    }

}
