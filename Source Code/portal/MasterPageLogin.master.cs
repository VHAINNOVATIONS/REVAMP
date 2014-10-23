using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPageLogin : BaseMaster
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnFeedbackOK_OnClick(object sender, EventArgs e)
    {
        winSysFeedback.Hide();
    }
}
