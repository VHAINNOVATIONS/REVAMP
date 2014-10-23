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

public partial class ucMessagesRead : System.Web.UI.UserControl
{
    public BaseMaster BaseMstr { set; get; }
    protected CMessages msg;

    protected void Page_Load(object sender, EventArgs e)
    {
        msg = new CMessages(BaseMstr);

        if (!IsPostBack) {
            GetUserMessages();
            LoadPatientsList();
            LoadProvidersList();
        }

        if (IsPostBack) {

            //get name of postback generating control
            string EvtSender = Request.Params["__EVENTTARGET"];
            string EvtArgs = Request.Params["__EVENTARGUMENT"];
        }

        SetJsAttributes_Recipients(chklstPatients, "chklstPatients");
        SetJsAttributes_Recipients(chklstProviders, "chklstProviders");
    }

    protected void GetUserMessages() {
        DataSet dsMsg = msg.GetUserMessagesDS();
        repMsgList.DataSource = dsMsg;
        repMsgList.DataBind();

        DataSet dsDeletedMsg = msg.GetDeletedMessagesDS();
        repDeletedMsg.DataSource = dsDeletedMsg;
        repDeletedMsg.DataBind();

        DataSet dsSentMsg = msg.GetSentMessagesDS();
        repSentMsg.DataSource = dsSentMsg;
        repSentMsg.DataBind();
    }

    protected DataSet GetMergedDataset() 
    {
        DataSet dsMsg = msg.GetUserMessagesDS();
        DataSet dsDeletedMsg = msg.GetDeletedMessagesDS();
        DataSet dsSentMsg = msg.GetSentMessagesDS();

        DataSet dsResult = new DataSet();
        DataSet[] dsSrc = { dsMsg, dsDeletedMsg, dsSentMsg };

        dsResult.Tables.Add(dsSrc[0].Tables[0].Clone());
        foreach (DataSet ds in dsSrc) 
        {
            foreach (DataTable dt in ds.Tables) 
            {
                foreach (DataRow dr in dt.Rows) 
                {
                    dsResult.Tables[0].ImportRow(dr);
                }
            }
        }

        return dsResult;
    }

    protected void ReadMessage(object sender, EventArgs e) {
        long lMsgID = -1;
        if (htxtSelectedMsg.Value.Length > 0) {
            lMsgID = Convert.ToInt32(htxtSelectedMsg.Value);
        }

        if (lMsgID > -1)
        {
            bool bUnread = false;
            long lIsSentMsg = (tcMessages.ActiveTabIndex == 1) ? 1 : 0;

            DataSet dsMsg = GetMergedDataset();

            DataRow[] drMsg = dsMsg.Tables[0].Select("MESSAGE_ID = " + lMsgID.ToString() + " AND IS_SENDER = " + lIsSentMsg.ToString());
            foreach (DataRow dr in drMsg)
            {
                if (!dr.IsNull("RECIPIENTS"))
                {
                    lblTo.Text = dr["RECIPIENTS"].ToString();
                }
                
                if (!dr.IsNull("SENDER_NAME"))
                {
                    lblFrom.Text = dr["SENDER_NAME"].ToString();
                }

                if (!dr.IsNull("SUBJECT"))
                {
                    lblSubject.Text = Server.HtmlDecode(dr["SUBJECT"].ToString());
                }

                if (!dr.IsNull("BODY"))
                {
                    txtMsgBodyContents.Text = Server.HtmlDecode(dr["BODY"].ToString());
                }

                if (!dr.IsNull("UNREAD")) {
                    bUnread = (Convert.ToInt32(dr["UNREAD"]) > 0);
                }
            }

            if (bUnread) {
                if (MarkAsRead(lMsgID))
                {
                    GetUserMessages();
                }
            }

            divMsgContents.Visible = true;
            divMsgContents.Style.Add("display","block");

            if (tcMessages.ActiveTabIndex == 2)
            {
                btnDeleteMsg.Visible = false;
                //btnReplyMsg.Visible = false;
            }
            else
            {
                btnDeleteMsg.Visible = true;
                //btnReplyMsg.Visible = true;
            }
        }
        else 
        {
            lblFrom.Text = String.Empty;
            lblSubject.Text = String.Empty;
            txtMsgBodyContents.Text = String.Empty;
            divMsgContents.Visible = false;
            htxtSelectedMsg.Value = String.Empty;
        }
    }

    protected void btnReply_OnClick(object sender, EventArgs e) {
        long lMsgID = -1;
        if (htxtSelectedMsg.Value.Length > 0)
        {
            lMsgID = Convert.ToInt32(htxtSelectedMsg.Value);
        }

        long lRecipientID = 0;

        if (lMsgID > -1)
        {
            DataSet dsMsg = GetMergedDataset();
            DataRow[] drMsg = dsMsg.Tables[0].Select("message_id = " + lMsgID.ToString());
            foreach (DataRow dr in drMsg)
            {
                if (!dr.IsNull("FROM_USR_ID"))
                {
                    lRecipientID = Convert.ToInt32(dr["FROM_USR_ID"]);
                }
                
                string strHeader = String.Empty;
                if (!dr.IsNull("SENDER_NAME"))
                {
                    //lblFrom.Text = dr["SENDER_NAME"].ToString();
                    strHeader += "From: " + dr["SENDER_NAME"].ToString() + "\n";
                }

                if (!dr.IsNull("DATE_SENT"))
                {
                    //lblFrom.Text = dr["DATE_SENT"].ToString();
                    strHeader += "Date: " + dr["DATE_SENT"].ToString() + "\n";
                }

                if (!dr.IsNull("SUBJECT"))
                {
                    strHeader += "Subject: " + dr["SUBJECT"].ToString() + "\n\n\n";

                    if (dr["SUBJECT"].ToString().ToLower().IndexOf("re:") < 0)
                    {
                        txtSubject.Text = "RE: " + dr["SUBJECT"].ToString();
                    }
                    else
                    {
                        txtSubject.Text = dr["SUBJECT"].ToString();
                    }
                }

                strHeader = "\n\n---------------------------------------------------\n" + strHeader;

                if (!dr.IsNull("BODY"))
                {
                    txtMsgBody.Text = strHeader + dr["BODY"].ToString();
                    //divMsgBodyContents.InnerHtml = dr["BODY"].ToString();
                }

            }

            SetReplyTo(lRecipientID);
            winComposeMsg.Show();
            ScriptManager.RegisterStartupScript(upMsgActions, typeof(string), "restorelist", "setTimeout(function(){messages.recipients.restoreList();}, 100);", true);
        }

    }
    
    protected void btnRToAll_OnClick(object sender, EventArgs e)
    {
        long lMsgID = -1;
        if (htxtSelectedMsg.Value.Length > 0)
        {
            lMsgID = Convert.ToInt32(htxtSelectedMsg.Value);
        }

        if (lMsgID > -1)
        {
            DataSet dsMsg = GetMergedDataset();
            DataRow[] drMsg = dsMsg.Tables[0].Select("message_id = " + lMsgID.ToString());
            foreach (DataRow dr in drMsg)
            {
                string strHeader = String.Empty;
                if (!dr.IsNull("SENDER_NAME"))
                {
                    //lblFrom.Text = dr["SENDER_NAME"].ToString();
                    strHeader += "From: " + dr["SENDER_NAME"].ToString() + "\n";
                }

                if (!dr.IsNull("DATE_SENT"))
                {
                    //lblFrom.Text = dr["DATE_SENT"].ToString();
                    strHeader += "Date: " + dr["DATE_SENT"].ToString() + "\n";
                }

                if (!dr.IsNull("SUBJECT"))
                {
                    strHeader += "Subject: " + dr["SUBJECT"].ToString() + "\n\n\n";

                    if (dr["SUBJECT"].ToString().ToLower().IndexOf("re:") < 0)
                    {
                        txtSubject.Text = "RE: " + dr["SUBJECT"].ToString();
                    }
                    else
                    {
                        txtSubject.Text = dr["SUBJECT"].ToString();
                    }
                }

                strHeader = "\n\n---------------------------------------------------\n" + strHeader;

                if (!dr.IsNull("BODY"))
                {
                    txtMsgBody.Text = strHeader + dr["BODY"].ToString();
                    //divMsgBodyContents.InnerHtml = dr["BODY"].ToString();
                }

            }

            SetRecipientsList(lMsgID);
            winComposeMsg.Show();
            ScriptManager.RegisterStartupScript(upMsgActions, typeof(string), "restorelist", "setTimeout(function(){messages.recipients.restoreList();}, 100);", true);
        }
    }

    protected void btnSendMsg_OnClick(object sender, EventArgs e) {
        long lMessageID = -1;
        string strRecipient = GetRecipientsList();
        
        if (strRecipient.Length < 1) {
            //DAVID: (todo) send error feedback: "Please select one or more recipients!"
            return;
        }

        if (msg.SendMessage(strRecipient, txtSubject.Text, txtMsgBody.Text, out lMessageID)) 
        {
            tcMessages.ActiveTabIndex = 1;
            htxtSelectedMsg.Value = lMessageID.ToString();
        }

        winComposeMsg.Hide();
        GetUserMessages();
        ScriptManager.RegisterStartupScript(upWrapper, typeof(string), "sendMsg", "__doPostBack('" + upSelectMsg.ClientID + "','');", true);
    }

    protected string GetRecipientsList() {
        string strRecipients = String.Empty;

        CheckBoxList[] chklist = {chklstProviders, chklstPatients };

        chklstPatients.Items[0].Selected = false;
        chklstProviders.Items[0].Selected = false;

        foreach (CheckBoxList chklst in chklist) {
            foreach (ListItem li in chklst.Items) {
                if (li.Selected) {
                    strRecipients += li.Value + ",";
                }
            }
        }

        return strRecipients;
    }

    protected void SetReplyTo(long lRecipientID)
    {
        chklstPatients.ClearSelection();
        chklstProviders.ClearSelection();

        string strRecipientID = lRecipientID.ToString();

        //for (int a = 0; a < chklstPatients.Items.Count; a++)
        //{
        //    if (chklstPatients.Items[a].Value == strRecipientID)
        //    {
        //        chklstPatients.Items[a].Selected = true; ;
        //    }
        //}

        for (int b = 0; b < chklstProviders.Items.Count; b++)
        {
            if (chklstProviders.Items[b].Value == strRecipientID)
            {
                chklstProviders.Items[b].Selected = true;
            }
        }
    }
    
    protected void SetRecipientsList(long lMsgID){
        chklstPatients.ClearSelection();
        chklstProviders.ClearSelection();
        DataSet dsRecipients = msg.GetRecipientsListDS(lMsgID);
        
        foreach (DataTable dt in dsRecipients.Tables) {
            foreach (DataRow dr in dt.Rows) {
                string strRecipientID = "0";
                if(!dr.IsNull("RECIPIENT_ID")){
                    strRecipientID = dr["RECIPIENT_ID"].ToString();
                }
                
                for (int a = 0; a < chklstPatients.Items.Count; a++) 
                {
                    if (chklstPatients.Items[a].Value == strRecipientID)
                    {
                        if (BaseMstr.FXUserID == Convert.ToInt32(strRecipientID))
                        {
                            chklstPatients.Items[a].Selected = true; 
                        }
                    }
                }

                for (int b = 0; b < chklstProviders.Items.Count; b++)
                {
                    if (chklstProviders.Items[b].Value == strRecipientID)
                    {
                        chklstProviders.Items[b].Selected = true;
                    }
                }
            }
        }
    }

    protected void btnDeleteMsg_OnClick(object sender, EventArgs e)
    {
        CMessages mmsg = new CMessages(BaseMstr);
        long lIsSentMsg = 0;
        long lMsgID = -1;

        if (tcMessages.ActiveTabIndex == 1) {
            lIsSentMsg = 1;
        }

        if (htxtSelectedMsg.Value.Length > 0) {
            lMsgID = Convert.ToInt32(htxtSelectedMsg.Value);
        }

        if (mmsg.MarkAsDeleted(lMsgID, lIsSentMsg))
        {
            //clear message contents preview area
            lblTo.Text = String.Empty;
            lblFrom.Text = String.Empty;
            lblSubject.Text = String.Empty;
            txtMsgBodyContents.Text = String.Empty;
            htxtSelectedMsg.Value = String.Empty;

            divMsgContents.Visible = false;

            GetUserMessages();
        }
    }

    protected void repMsgList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            DataRow dr = drv.Row;
            Image img = (Image)e.Item.FindControl("imgMsgStatus");
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trMsg");

            if (!dr.IsNull("UNREAD")) {
                if (Convert.ToInt32(dr["UNREAD"]) > 0) {
                    img.ImageUrl = "Images/email.png";
                    img.Visible = true;

                    tr.Attributes.Add("class", "unread");
                }
            }

            tr.Attributes.Add("messageid", dr["MESSAGE_ID"].ToString());
        }
    }

    protected bool MarkAsRead(long lMessageID) {
        bool bMarked = false;
        bMarked = msg.MarkAsRead(lMessageID);

        return bMarked;
    }

    protected void LoadPatientsList() {
        chklstPatients.Items.Add(new ListItem(UserLoggedOn(), BaseMstr.FXUserID.ToString()));
        chklstPatients.Items.Insert(0, new ListItem("ALL", "-1"));
    }

    protected void LoadProvidersList()
    {
        DataSet dsProviders = msg.GetAllProviderDS();
        chklstProviders.DataSource = dsProviders;
        chklstProviders.DataTextField = "NAME";
        chklstProviders.DataValueField = "FX_USER_ID";
        chklstProviders.DataBind();

        chklstProviders.Items.Insert(0, new ListItem("ALL", "-1"));
        //SetJsAttributes_Recipients(chklstProviders, "chklstProviders");
    }

    protected void SetJsAttributes_Recipients(CheckBoxList chkRecipients, string strListName) {
        for (int a = 0; a < chkRecipients.Items.Count; a++) {
            string strMaster = "false";
            if (a == 0) {
                strMaster = "true"; 
            }
            chkRecipients.Items[a].Attributes.Add("onclick", "messages.chkSelectAll(this, {name: '" + strListName + "', master: "+ strMaster +"});");
        }
    }

    protected string UserLoggedOn()
    {
        string strUserName = String.Empty;
        string[] strUserNameParts;
        CDataUtils utils = new CDataUtils();
        CUser user = new CUser();
        DataSet dsUser = user.GetLoginUserDS(BaseMstr, BaseMstr.FXUserID);
        strUserName = utils.GetDSStringValue(dsUser, "NAME");
        strUserNameParts = strUserName.Split(' ');
        strUserName = strUserNameParts[1] + ", " + strUserNameParts[0];
        return strUserName;
    }
}