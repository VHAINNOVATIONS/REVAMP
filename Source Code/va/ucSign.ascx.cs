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

public partial class ucSign : System.Web.UI.UserControl
{
    //basemaster property
    public BaseMaster BaseMstr { get; set; }
  
    private string m_strSignedProviderID;
    public string SignedProviderID
    {
        get { return m_strSignedProviderID; }
        set { m_strSignedProviderID = value; }
    }
    public string strLogAddendum { get; set; }

    private long m_lSignedUserType;
    public long SignedUserType
    {
        get { return m_lSignedUserType; }
        set { m_lSignedUserType = value; }
    }

    private long m_lCloseEncounter;
    public long CloseEncounter
    {
        get { return m_lCloseEncounter; }
        set { m_lCloseEncounter = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        m_strSignedProviderID = "";
        m_lSignedUserType = 0;
    }

    //check credentials and load the provider id property
    protected void btnSignSOAPP_Click(object sender, EventArgs e)
    {
        strLogAddendum = htxtLogAddendum.Value;
        
        //clear current provider id
        SignedProviderID = "";
        SignedUserType = 0;
        CloseEncounter = 0;

        //check account
        CSec sec = new CSec();
        if (sec.Sign(BaseMstr,
                      txtProvUsername.Text,
                      txtUPassword.Text,
                      out m_strSignedProviderID,
                      out m_lSignedUserType))
        {
            //if account was ok the we are good
            SignedProviderID = m_strSignedProviderID;
            SignedUserType = m_lSignedUserType;

            if (BaseMstr.APPMaster.UserType == (long)SUATUserType.PROVIDER) 
            {
                CloseEncounter = 1;
                chkClosed.Checked = true;
            }

            //clear the text
            txtProvUsername.Text = "";
            txtUPassword.Text = "";

            //bubble up the event so someone using 
            //the control can check to see if we signed
            RaiseBubbleEvent(this, e);
        }
      
        //clear the text
        txtProvUsername.Text = "";
        txtUPassword.Text = "";

        //Hide Sign popup
        winSignNote.Hide();

        //Show system feedback
        ShowSysFeedback();
    }

    protected void ShowSysFeedback()
    {
        if (BaseMstr.StatusCode == 9 && !String.IsNullOrEmpty(BaseMstr.StatusComment))
        {
            string strScript = "sysfeedback('" + BaseMstr.StatusComment + "');";
            ScriptManager.RegisterStartupScript(upSign, typeof(string), "feedback", strScript, true);
            BaseMstr.ClearStatus();
        }
    }

}
