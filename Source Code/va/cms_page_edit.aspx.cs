using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cms_page_edit : System.Web.UI.Page
{
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
        ucPageEdit.BaseMstr = Master;
    }
}