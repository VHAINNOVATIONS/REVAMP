using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;

public partial class MasterPageError : BaseMaster
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //doesn't cache the page so users cannot back into sensitive information after logging off
        //Response.AddHeader("X-UA-Compatible", "IE=8");
        //Response.Cache.SetAllowResponseInBrowserHistory(false);
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        //Response.Cache.SetNoStore();
        //Response.AddHeader("Pragma", "no-cache");
    }
}
