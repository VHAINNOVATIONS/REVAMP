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

public partial class keyboard : System.Web.UI.Page
{
    public void DrawKey(string strKey, string strValue)
    {
	    Response.Write("<table style=\"cursor:pointer;cursor:hand;\" width=\"45\" height=\"50\" >");
	    Response.Write("<tr>");
	    Response.Write("<td onclick=\"javascript:OnKeyPress('");
	    Response.Write(strKey);
	    Response.Write("', '");
        Response.Write(strValue);
	    Response.Write("');\" align=\"center\" valign=\"center\" bgcolor=\"CEDAE2\" ><font face=verdana size=+1>");
	    Response.Write(strKey);
	    Response.Write("</font></td>");
	    Response.Write("</tr>");
	    Response.Write("</table>");

    }
    public void DrawNumberKey(string strKey, string strValue)
    {
	    Response.Write("<table style=\"cursor:pointer;cursor:hand;\" width=\"45\" height=\"50\" >");
	    Response.Write("<tr>");
	    Response.Write("<td onclick=\"javascript:OnKeyPress('");
	    Response.Write(strKey);
	    Response.Write("', '");
	    Response.Write(strValue);
	    Response.Write("');\" align=\"center\" valign=\"center\" bgcolor=\"LightSteelBlue\" ><font face=verdana size=+1>");
	    Response.Write(strKey);
	    Response.Write("</font></td>");
	    Response.Write("</tr>");
	    Response.Write("</table>");
    }

    public void DrawKey2(string strKey, string strValue)
    {
	    Response.Write("<table style=\"cursor:pointer;cursor:hand;\" width=\"45\" height=\"45\" >");
	    Response.Write("<tr>");
	    Response.Write("<td onclick=\"javascript:OnKeyPress('");
	    Response.Write(strKey);
	    Response.Write("', '");
	    Response.Write(strValue);
	    Response.Write("');\" align=\"center\" valign=\"center\" bgcolor=\"LightGrey\" ><font face=verdana size=+0>");
	    Response.Write(strKey);
	    Response.Write("</font></td>");
	    Response.Write("</tr>");
	    Response.Write("</table>");

    }

    public void DrawEnterBar(string strKey, string strValue)
    {
	    Response.Write("<table cellpadding=\"6\" cellspacing=\"5\" style=\"cursor:pointer;cursor:hand;\" width=\"100%\" >");
	    Response.Write("<tr>");
	    Response.Write("<td onclick=\"javascript:CloseForm();\"");
	    Response.Write(" align=\"center\" valign=\"center\" bgcolor=\"LightGrey\" ><font face=verdana size=+0>");
    	
	    Response.Write(strKey);
    	
	    Response.Write("</font></td>");
	    Response.Write("</tr>");
	    Response.Write("</table>");
    }

    public void DrawSpaceBar(string strKey, string strValue)
    {
	    Response.Write("<table cellpadding=\"10\" cellspacing=\"5\" style=\"cursor:pointer;cursor:hand;\" width=\"100%\" >");
	    Response.Write("<tr>");
	    Response.Write("<td onclick=\"javascript:OnKeyPress('");
	    Response.Write(strKey);
	    Response.Write("', '");
	    Response.Write(strValue);
	    Response.Write("');\" align=\"center\" valign=\"center\" bgcolor=\"LightGrey\" ><font face=verdana size=+0>");
	    Response.Write(strKey);
	    Response.Write("</font></td>");
	    Response.Write("</tr>");
	    Response.Write("</table>");

    }

    public void DrawClearBar(string strKey, string strValue)
    {
	    Response.Write("<table cellpadding=\"6\" cellspacing=\"5\" style=\"cursor:pointer;cursor:hand;\" width=\"100%\" >");
	    Response.Write("<tr>");
	    Response.Write("<td onclick=\"javascript:OnKeyPress('");
	    Response.Write(strKey);
	    Response.Write("', '");
	    Response.Write(strValue);
	    Response.Write("');\" align=\"center\" valign=\"center\" bgcolor=\"LightGrey\" ><font face=verdana size=+0>");
	    Response.Write(strKey);
	    Response.Write("</font></td>");
	    Response.Write("</tr>");
	    Response.Write("</table>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
