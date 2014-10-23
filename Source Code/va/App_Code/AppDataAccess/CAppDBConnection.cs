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
/// Summary description for CAppDBConnection
/// </summary>
public class CAppDBConnection
{   
    /// <summary>
    /// empty constructor
    /// </summary>
    public CAppDBConnection()
	{
	}

     /// <summary>
    /// get a data connection
    /// </summary>
    public CDataConnection GetDataConnection()
    {
        //get the connection string from the web.config file
        //connection string is encrypted in the file using MS recommended procedures
        //
        //cd\
        //cd windows
        //cd microsoft.net
        //cd framework
        //cd v2.0.50727
        //aspnet_regiis -pe "connectionStrings" -app "/PrimeCarePlus" -prov "RsaProtectedConfigurationProvider"
        //
        //look for connection strings in connection strings and app settings
        string strConnectionString = "";
        try
        {
            //try to get the connection string from the encrypted connectionstrings section
            strConnectionString = ConfigurationManager.ConnectionStrings["DBConnString"].ConnectionString;
        }
        catch (Exception eee)
        {
            //pull from appsettings if failed, this lets developers connect from local boxes.
            //strConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBConnString"];
            string strStatus = eee.Message;
        }

        bool bAudit = false;
        string strAudit = "";
        if (System.Configuration.ConfigurationManager.AppSettings["AUDIT"] != null)
        {   strAudit = System.Configuration.ConfigurationManager.AppSettings["AUDIT"].ToString();
            if (strAudit == "1")
            {
                bAudit = true;
            }
        }
        
        CDataConnection DBConnection;

        //create a new dataconnection object
        DBConnection = new CDataConnection();

        //Connect to the database, connection is housed in the master page 
        //so that all pages that use the master have access to it.
        if (!DBConnection.Connect(strConnectionString, (int)DataConnectionType.Oracle, bAudit))
        {
            return null;
        }

        return DBConnection;
    }
}
