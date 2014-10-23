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
/// ----------------------------------------------------------
/// CTreatment interface to the PCK_TREATMENT package.
/// ----------------------------------------------------------
/// </summary>
public class CTreatmentPP : CBaseDataClass
{
    //----------------------------------------------------------
    public CTreatmentPP(BaseMaster bm)
	{
        m_BaseMstr = bm;
        m_DBConn = m_BaseMstr.DBConn;
        m_strPatientID = m_BaseMstr.SelectedPatientID;
        m_nMID = m_BaseMstr.ModuleID;
    }

    //----------------------------------------------------------
    // returns the current treatment ID -
    //----------------------------------------------------------
    public bool GetCurrentTreatmentID(out long lTreatmentID)
    {
        lTreatmentID = 0;

        //add params for the DB call in paramaters
        //these will always be passed in to all sp calls
        CDataParameterList pList = ItemList();
        pList.AddInputParameter("pi_vPatientID", m_strPatientID);
        pList.AddOutputParameter("po_nTreatmentID", lTreatmentID);

        //execute the stored procedure
        if (m_DBConn.ExecuteOracleSP("PCK_TREATMENT.GetCurrentTreatmentID",
                                pList,
                                out m_lErrorStatus,
                                out m_strErrorComment) == false)
        {
            return ErrorHandler();
        }

        CDataParameter paramValue = pList.GetItemByName("po_nTreatmentID");
        lTreatmentID = paramValue.LongParameterValue;

        return true;
    }

} //END CLASS
