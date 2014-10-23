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
/// Summary description for CEncounterIntake
/// </summary>
public class CEncounterIntakePP : CBaseDataClass
{

    public CEncounterIntakePP(BaseMaster bm)
    {
        m_BaseMstr = bm;
        m_DBConn = m_BaseMstr.DBConn;
        m_strPatientID = m_BaseMstr.SelectedPatientID;
        m_nMID = m_BaseMstr.ModuleID;
    }


    //----------------------------------------------------------
    public bool GetEncounterIntakeAltLang(string strEncounterID, long lEncounterIntakeID)
    {
        long lOutLang = 0;

        CDataParameterList pList = ItemList();
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nEncounterIntakeID", lEncounterIntakeID);
        pList.AddOutputParameter("po_nOutAltLang", lOutLang);

        //execute the stored procedure
        m_DBConn.ExecuteOracleSP("PCK_ENCOUNTER_INTAKE.getIntakeAltLang",
                              pList,
                              out m_lErrorStatus,
                              out m_strErrorComment);

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (m_lErrorStatus == 0)
        {
            CDataParameter param = new CDataParameter();
            param = pList.GetItemByName("po_nOutAltLang");
            if (param != null)
            {
                lOutLang = param.LongParameterValue;
            }

            if (lOutLang > 0)
            {
                return true;
            }
        }

        return ErrorHandler();
    }

    //----------------------------------------------------------
    public string GetEncounterIntakePatientID( string strEncounterID, long lEncounterIntakeID)
    {
        string strPatientID = "";

        //add params for the DB call in paramaters
        //these will always be passed in to all sp calls
        CDataParameterList pList = ItemList();
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nEncounterIntakeID", lEncounterIntakeID);
        pList.AddOutputParameter("po_vPatientID", strPatientID);

        //execute the stored procedure
        m_DBConn.ExecuteOracleSP("PCK_ENCOUNTER_INTAKE.getIntakePatientID",
                              pList,
                              out m_lErrorStatus,
                              out m_strErrorComment);

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (m_lErrorStatus == 0)
        {
            CDataParameter param = new CDataParameter();
            param = pList.GetItemByName("po_vPatientID");
            if (param != null)
            {
                strPatientID = param.StringParameterValue;
            }
        }

        ErrorHandler();
        return strPatientID;
    }

    //----------------------------------------------------------
    public bool OnEncounterIntakeEnd( long lMID, string strEncounterID, long lEncounterIntakeID)
    {
        //add params for the DB call in paramaters
        //these will always be passed in to all sp calls
        CDataParameterList pList = ItemList();
        pList.AddInputParameter("pi_nMID", lMID);
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nEncounterIntakeID", lEncounterIntakeID);

        //execute the stored procedure
        m_DBConn.ExecuteOracleSP("PCK_ENCOUNTER_INTAKE.OnEncounterIntakeEnd",
                              pList,
                              out m_lErrorStatus,
                              out m_strErrorComment);

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        return ErrorHandler();
    }

    //----------------------------------------------------------
    public DataSet GetEncounterFlags( string strEncounterID )
    {
        //add params for the DB call in paramaters
        //these will always be passed in to all sp calls
        CDataParameterList pList = ItemList();
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_DBConn,
                                          "PCK_ENCOUNTER_INTAKE.GetEncounterFlags",
                                          pList,
                                          out m_lErrorStatus,
                                          out m_strErrorComment);


        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (m_lErrorStatus == 0)
        {
            return ds;
        }

        ErrorHandler();
        return null;
    }

    //----------------------------------------------------------
    public DataSet GetEncounterIntakeFlags( string strEncounterID, long lEncounterIntakeID )
    {
        //add params for the DB call in paramaters
        //these will always be passed in to all sp calls
        CDataParameterList pList = ItemList();
        pList.AddInputParameter("pi_vEncounterID", strEncounterID);
        pList.AddInputParameter("pi_nEncounterIntakeID", lEncounterIntakeID);

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_DBConn,
                                          "PCK_ENCOUNTER_INTAKE.GetEncounterIntakeFlags",
                                          pList,
                                          out m_lErrorStatus,
                                          out m_strErrorComment);

        // 0 = success if strStatus is populated it will show on the screen
        // 1 to n are errors and we always show errors
        if (m_lErrorStatus == 0)
        {
            return ds;
        }

        ErrorHandler();
        return null;
    }

    //----------------------------------------------------------
   // public bool GetEncounterIDFromForModule(Int32 nModuleID, string strPatientID, long lTreatmentID, out string strEncounterID)
    public bool GetEncounterIDFromForModule(long lTreatmentID, out string strEncounterID )
    {
        strEncounterID = "";

        //add params for the DB call in paramaters
        //these will always be passed in to all sp calls
        CDataParameterList pList = ItemList();
        pList.AddInputParameter("pi_nModuleID", m_nMID);
        pList.AddInputParameter("pi_vPatientID", m_strPatientID);
        pList.AddInputParameter("pi_nTreatmentID", lTreatmentID);
        pList.AddOutputParameter("po_vEncounterID", strEncounterID);

        //execute the stored procedure
        m_DBConn.ExecuteOracleSP("PCK_ENCOUNTER_INTAKE.GetEncounterIDFromModule",
                                pList,
                                out m_lErrorStatus,
                                out m_strErrorComment);

        if(m_lErrorStatus > 0)
        {
            return ErrorHandler();
        }

        CDataParameter paramValue = pList.GetItemByName("po_vEncounterID");
        strEncounterID = paramValue.StringParameterValue;
        m_strEncounterID = strEncounterID;

        return true;
    }

    //----------------------------------------------------------
    //public bool NewEncounterIntake(Int32 lModuleID, bool bAltLang, string strPatientID, string strEncounterID, out int nEncounterIntakeID)
    public bool NewEncounterIntake(bool bAltLang, out int nEncounterIntakeID)
    {
        nEncounterIntakeID = 0;
        long lOutAltLang = 0;
        if (bAltLang)
        {
            lOutAltLang = 1;
        }
        else
        {
            lOutAltLang = 0;
        }

        //add params for the DB call in paramaters
        //these will always be passed in to all sp calls
        CDataParameterList pList = ItemList();
        pList.AddInputParameter("pi_nModuleID", m_nMID);
        pList.AddInputParameter("pi_vEncounterID", m_strEncounterID);
        pList.AddInputParameter("pi_nAltLang", lOutAltLang);
        pList.AddOutputParameter("po_nEncounterIntakeID", nEncounterIntakeID);

        //execute the stored procedure
        m_DBConn.ExecuteOracleSP("PCK_ENCOUNTER_INTAKE.NewEncounterIntake",
                                pList,
                                out m_lErrorStatus,
                                out m_strErrorComment);

        if (m_lErrorStatus > 0)
        {
            return ErrorHandler();
        }

        CDataParameter paramValue = pList.GetItemByName("po_nEncounterIntakeID");
        nEncounterIntakeID = (int) paramValue.LongParameterValue;
        m_lEncounterIntakeID = nEncounterIntakeID;

        return true;
    }
    
    //----------------------------------------------------------
    public DataSet GetAllEncounterFlagsDS( Int32 lPatientID, Int32 lModuleID, long lEncounterID)
    {
        DataSet ds = null;

        //add params for the DB call in paramaters
        //these will always be passed in to all sp calls
        CDataParameterList pList = ItemList();
        pList.AddInputParameter("pi_nModuleID", lModuleID);
        pList.AddInputParameter("pi_nEncounterID", lEncounterID);

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        ds = cds.GetOracleDataSet(m_DBConn,
                                  "PCK_ENCOUNTER_INTAKE.getEncounterFlagsRS",
                                  pList,
                                  out m_lErrorStatus,
                                  out m_strErrorComment);


        ErrorHandler();
        return ds;
    }




    //----------------------------------------------------------
    public DataSet GetIntakesForEducationRS( string strPatientID )
    {

        //add params for the DB call in paramaters
        //these will always be passed in to all sp calls
        CDataParameterList pList = ItemList();
        pList.AddInputParameter("pi_vPatientID", strPatientID);

        //get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_DBConn,
                                          "PCK_ENCOUNTER_INTAKE.GetIntakesForEducationRS",
                                          pList,
                                          out m_lErrorStatus,
                                          out m_strErrorComment);

        ErrorHandler();
        return ds;
    }


} //END CLASS