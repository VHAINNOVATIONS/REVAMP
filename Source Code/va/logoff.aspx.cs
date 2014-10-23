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

public partial class logoff : System.Web.UI.Page
{
    /// <summary>
    /// Logs user out of Website
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //remove some session variables
        Session["USERLOGGEDON"] = null;
        Session["SYSSETTINGS"] = null;
        Master.ClosePatient();
        
        //logs the user off and re-directs...
        Master.LogOff();
    }
}
