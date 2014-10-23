using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class pat_portal_account : System.Web.UI.Page
{
    public long lPortalAccRightsMode;
    public bool bShowReadOnlyAlert = false;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ucPatientPortalAccount.BaseMstr = Master;
        lPortalAccRightsMode = ucPatientPortalAccount.lUsrRightMode;
        bShowReadOnlyAlert = (Master.APPMaster.UserType == (long)SUATUserType.ADMINISTRATOR);

        if (!IsPostBack)
        {
            ucPatientPortalAccount.loadPatientPortalAccount();
        }

         //if the user pushed the master save button
        //then save the form
        if (Master.OnMasterSAVE())
        {
            Save();
        }
    }

    protected void Save()
    {
        if (lPortalAccRightsMode > (long)RightMode.ReadOnly)
        {
            ucPatientPortalAccount.SavePatientPortalAccount();
        }
        else 
        {
            Master.StatusCode = 1;
            Master.StatusComment = "<img alt=\"\" src=\"Images/lock16x16.png\" /> You have <b>Read-Only Access</b> to this section.";
        }
    }
}
