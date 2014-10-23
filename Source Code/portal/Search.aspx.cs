using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["q"] != null) 
        {
            string strSearch = Server.UrlDecode(Request.QueryString["q"]);
            lblSearchTopic.Text = strSearch;

            ShowResults(strSearch);
        }
    }

    protected void ShowResults(string strSearch) {
        CContentManagement cms = new CContentManagement(Master);
        DataSet dsResults = cms.SearchPagesDS(strSearch);

        string strResults = String.Empty;

        if (dsResults != null) {
            strResults += "<ol>";
            foreach (DataTable dt in dsResults.Tables) {
                foreach (DataRow dr in dt.Rows) {
                    strResults += "<li><div><a href=\"cms_contents.aspx?id=" + dr["PAGE_ID"].ToString();
                    strResults += "\" style=\"font-weight: bold; color: blue;\" \">";
                    strResults += dr["TITLE"].ToString();
                    strResults += "</a><br/>";

                    //show matches inside the contents ------------------------------------------
                    strResults += "<ul>";

                    Regex regex = new Regex("</?(.*)>", RegexOptions.IgnoreCase | RegexOptions.Multiline);

                    string strLSearch = strSearch.ToLower();
                    string strContents = dr["CONTENTS"].ToString();
                    string strLContents = strContents.ToLower();

                    int iStringIndex = strLContents.IndexOf(strLSearch);
                    int iStartIndex = 0;
                    int iLeftPad = 35;
                    int iMaxLen = 400;

                    if (iStringIndex >= iLeftPad)
                    {
                        iStartIndex = iStringIndex - iLeftPad;
                    }

                    string strSub1 = strContents.Substring(iStartIndex);
                    string strSub2 = String.Empty;

                    if(strSub1.Length > iMaxLen){
                        strSub2 = strSub1.Substring(0, iMaxLen);
                    }
                    else
                    {
                        strSub2 = strSub1;
                    }

                    if (strSub2.Trim().Length > 0)
                    {
                        Regex reSearch = new Regex("(" + strSearch + ")", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        //reSearch.Replace(strSub2, "<span style=\"background-color: yellow;\">$1</span>")

                        strResults += "<li>";
                        strResults += "<p>[...]";
                        strResults += reSearch.Replace(strSub2, "<span style=\"background-color: yellow;\">$1</span>");
                        strResults += "[...]</p>";
                        strResults += "</li>"; 
                    }
                    
                    strResults += "</ul>";
                    // --------------------------------------------------------------------------

                    strResults += "</div></li>";
                }
            }
            strResults += "</ol>";
        }

        divResults.InnerHtml = strResults;
    }
}