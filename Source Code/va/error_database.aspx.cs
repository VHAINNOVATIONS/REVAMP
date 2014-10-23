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
using System.Net;
using System.Net.Mail;

public partial class error_database : System.Web.UI.Page
{
    private string DBError
    {
        get
        {
            object obj = Session["DB_ERROR"];
            return (obj == null) ? string.Empty : obj.ToString();
        }
    }

    private string DBErrorCode
    {
        get
        {
            object obj = Session["DB_ERROR_CODE"];
            return (obj == null) ? string.Empty : obj.ToString();
        }
    }

    private string strError = String.Empty;

    /// <summary>
    /// Show Error Code and Error description
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Master.IsLoggedIn()) 
        {
            strError = "<strong>FX_USER_ID:</strong> " + Master.FXUserID + "<br/>";
            strError += "<strong>CLIENT_IP:</strong> " + Master.ClientIP + "<br/>";
            strError += "<strong>DB_SESSION_ID:</strong> " + Master.DBSessionID + "<br/>";
            strError += "<strong>ASP_SESSION_ID:</strong> " + Master.ASPSessionID + "<br/>";
            strError += "<strong>TIMESTAMP:</strong> " + DateTime.Now.ToString() + "<br/>";
            if (Request.UrlReferrer != null)
            {
                strError += "<strong>REFERRER_PAGE:</strong> " + Request.UrlReferrer.ToString() + "<br/>";
            }
        }

        if (strError.Length > 0) 
        {
            divErrMain.Visible = true;
            divErr.InnerHtml = "<p>" + strError + "</p>";
        }

        if (!IsPostBack) 
        {
            //SendErrorEmail();
        }
    }

    protected void btnGoHome_OnClick(object sender, EventArgs e) 
    {
        Response.Redirect("revamp.aspx");
    }

    protected void SendErrorEmail()
    {

        //Message Body
        string txtBody = "";
        txtBody += "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
        txtBody += "<html xmlns=\"http://www.w3.org/1999/xhtml\">";
        txtBody += "<head>";
        txtBody += "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />";
        txtBody += "<title>ContactUs :: Intellica Corporation</title>";
        txtBody += "<style type=\"text/css\">";
        txtBody += ".varLabel ";
        txtBody += "{";
        txtBody += "color:#3366CC; ";
        //txtBody += "font-family:Arial, Helvetica, sans-serif; ";
        txtBody += "text-align:right";
        txtBody += "}";
        txtBody += "</style>";
        txtBody += "";
        txtBody += "</head>";
        txtBody += "<body>";
        txtBody += "<div id=\"contactEmail\" style=\"width:400px; text-align: left;\">";
        txtBody += "<p style=\"text-align:left;\"><img alt=\"Intellica Corporation\" src=\"http://www.intellicacorp.com/intellicacorp/images/intellicahdB.jpg\" /></p>";
        txtBody += "<p>An error occured in WebPSAM Application.</p>";
        txtBody += "<div style=\"font-size: 14px; margin: 15px; width: 400px; padding: 10px; border: 1px dashed #808080; \">";
        txtBody += "<p>"+ strError +"</p>";
        txtBody += "</div>";
        txtBody += "</div>";
        txtBody += "</body>";
        txtBody += "</html>";

        SmtpClient client = new SmtpClient();
        MailMessage message = new MailMessage();

        try
        {
            //message.To.Add(new MailAddress("sales@intellicacorp.com", "Sales.IntellicaCorp"));
            message.To.Add(new MailAddress("david.santana@intellicacorp.com", "David Santana"));
            message.From = new MailAddress("web.intellicacorp@gmail.com", "WebSUAT App");
            //message.Bcc.Add(new MailAddress("david.santana@intellicacorp.com", "David Santana"));
            message.Subject = "WebSUAT Error";
            message.IsBodyHtml = true;
            message.Body = txtBody;
            client.EnableSsl = true;
            client.Send(message);
        }
        catch (Exception)
        {
            divErrMain.Visible = true;
            divErr.InnerHtml += "<p>An error occurred when trying to send the email to Intellica's application manager.</p>";
        }
    }
}
