using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class message_center : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            Session["USER_MESSAGES_DS"] = null;
        }

        ucMsg.BaseMstr = Master;
    }
}