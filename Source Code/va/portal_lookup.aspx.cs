using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class portal_lookup : System.Web.UI.Page
{
    public bool bAllowUpdate;
    
    protected void Page_Init(object sender, EventArgs e)
    {
        Master.ClosePatient();
        
        //get mastersave control
        Button btnMasterSave = (Button)Master.FindControl("btnMasterSave");
        AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
        trigger.ControlID = btnMasterSave.ID;
        upWrapperUpdatePanel.Triggers.Add(trigger);
    }

    protected void Page_Load(object sender, EventArgs e) 
    {
        CSec usrsec = new CSec();
        
        bAllowUpdate = (usrsec.GetRightMode(Master, (long)SUATUserRight.AdministratorUR) > (long)RightMode.ReadOnly);

        ucPortalLookUp.BaseMstr = Master;
        ucPortalLookUp.bAllowUpdate = bAllowUpdate;
    }
}
