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

public partial class revamp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.ClosePatient();
        ShowSysFeedback();
    }

    protected void ShowSysFeedback()
    {
        if (Master.StatusCode > 0 && !String.IsNullOrEmpty(Master.StatusComment))
        {
            HtmlContainerControl div = (HtmlContainerControl)this.Master.FindControl("divSysFeedback");
            div.InnerHtml = Master.StatusComment;
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "showAlert", "Ext.onReady(function(){winSysFeedback.show();})", true);
            Master.ClearStatus();
        }
    }
}
