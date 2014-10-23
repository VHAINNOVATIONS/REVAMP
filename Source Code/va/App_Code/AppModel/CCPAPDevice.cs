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
/// Summary description for CCPAPDevice
/// </summary>
public class CCPAPDevice
{
    protected BaseMaster m_BaseMstr;

    public CCPAPDevice(BaseMaster BaseMstr)
	{
        m_BaseMstr = BaseMstr;
	}

    //add new cpap device to the system
    public bool AddUpdateDevice(
        string strDeviceName,
        long lDeviceType,
        string strDeviceSerial,
        string strLowPressure,
        string strHighPressure,
        string strMaskType,
        string strMaskDetails,
        string strStudyDate,
        string strBaselineAHI,
        long lPAPType)
    {
        //status info
        long lStatusCode = 0;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vKey", m_BaseMstr.Key);
        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);
        plist.AddInputParameter("pi_vDeviceName", strDeviceName);
        plist.AddInputParameter("pi_nDeviceTypeID", lDeviceType);
        plist.AddInputParameter("pi_vSerialNumber", strDeviceSerial);

        plist.AddInputParameter("pi_vLowPressure", strLowPressure);
        plist.AddInputParameter("pi_vHighPressure", strHighPressure);
        plist.AddInputParameter("pi_vMaskType", strMaskType);
        plist.AddInputParameter("pi_vMaskDetails", strMaskDetails);
        plist.AddInputParameter("pi_nPAPType", lPAPType);
        plist.AddInputParameter("pi_vStudyDate", strStudyDate);
        plist.AddInputParameter("pi_nBaselineAHI", strBaselineAHI);

        //Execute Stored Procedure Call
        m_BaseMstr.DBConn.ExecuteOracleSP("PCK_CPAP_DEVICE.AddUpdateDevice",
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

    public DataSet GetPatientDeviceDS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_CPAP_DEVICE.GetPatientDeviceRS",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

        if (lStatusCode == 0)
        {
            return ds;
        }
        else
        {
            return null;
        }
    }

    public DataSet GetOtherDevicesDS()
    {
        //status info
        long lStatusCode = -1;
        string strStatusComment = "";

        //create a new parameter list with standard params from basemstr
        CDataParameterList plist = new CDataParameterList(m_BaseMstr);

        //add params for the DB stored procedure call
        plist.AddInputParameter("pi_vKey", m_BaseMstr.Key);
        plist.AddInputParameter("pi_vPatientID", m_BaseMstr.SelectedPatientID);

        //use helper to get a dataset from the sp call
        CDataSet cds = new CDataSet();
        DataSet ds = cds.GetOracleDataSet(m_BaseMstr.DBConn,
                                          "PCK_CPAP_DEVICE.GetOtherDevicesRS",
                                          plist,
                                          out lStatusCode,
                                          out strStatusComment);

        //set the base master status code and status for display
        m_BaseMstr.StatusCode = lStatusCode;
        m_BaseMstr.StatusComment = strStatusComment;

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