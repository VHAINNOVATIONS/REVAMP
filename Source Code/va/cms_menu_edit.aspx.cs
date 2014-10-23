using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class cms_menu_edit : System.Web.UI.Page
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
        ucMenuEdit.BaseMstr = Master;
    }
}