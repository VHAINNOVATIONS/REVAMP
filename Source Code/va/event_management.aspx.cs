using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class event_management : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        Master.ClosePatient();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            CPatientEvent evt = new CPatientEvent(Master);
            evt.CheckPAPEventALL();
        }

        ucEvtManagement.BaseMstr = Master;
    }
}