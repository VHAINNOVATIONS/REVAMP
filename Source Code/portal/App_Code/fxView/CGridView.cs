using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DataAccess;

/// <summary>
/// Summary description for CGridView
/// </summary>
public enum DisplayType : int
{
    Literal = 1,
    HyperLink = 2,
    CheckBox = 3,
    CheckBoxWithLabel = 4,
    RadioButton = 5,
    RadioButtonWithLabel = 6,
    TextBox = 7
};


public class CGridView
{
    protected string m_strStatus;
    protected long m_lStatusCode;
    int m_nControlCount;

    /// <summary>
    /// get/set status
    /// </summary>
    public string Status
    {
        get
        {
            return m_strStatus;
        }
        set
        {
            m_strStatus = value;
        }
    }

    /// <summary>
    /// get/set status code
    /// </summary>
    public long StatusCode
    {
        get
        {
            return m_lStatusCode;
        }
        set
        {
            m_lStatusCode = value;
        }
    }

    /// <summary>
    /// constructor
    /// </summary>
    public CGridView()
    {
        m_strStatus = "";
        m_lStatusCode = 0;
        m_nControlCount = 0;
    }

    //start, end and selected are zero based
    public string GetPagerHTML( string strControlID, 
                                int nStartPage, 
                                int nEndPage, 
                                int nSelectedPage)
    {
        string strPagerHTML = "";
        if (nStartPage < 0 || nEndPage < 0)
        {
            return strPagerHTML;
        }
              
        for (int i = nStartPage; i < nEndPage; i++)
        {
            if (i == nSelectedPage)
            {
                strPagerHTML += "<a href=\"javascript:OnPagerClick('";
                strPagerHTML += strControlID;
                strPagerHTML += "'";
                strPagerHTML += ", ";
                strPagerHTML += "'";
                strPagerHTML += Convert.ToString(i);
                strPagerHTML += "'";
                strPagerHTML += ")\">[";
                strPagerHTML += Convert.ToString(i + 1) + "]</a>&nbsp;";
            }
            else
            {
                strPagerHTML += "<a href=\"javascript:OnPagerClick('";
                strPagerHTML += strControlID;
                strPagerHTML += "'";
                strPagerHTML += ", ";
                strPagerHTML += "'";
                strPagerHTML += Convert.ToString(i);
                strPagerHTML += "'";
                strPagerHTML += ")\">";
                strPagerHTML += Convert.ToString(i + 1) + "</a>&nbsp;";
            }
        }

        return strPagerHTML;
    }

    //removes all data from a dataset except the 
    //current page worth of data
    public void PageData( DataSet ds,
                          int nPage,          
                          int nItemsPerPage)  
    {
        //get the number of records in the dataset
        int nCount = 0;
        foreach (DataTable table in ds.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                nCount++;
            }
        }

        //nothing to do
        if (nCount <= nItemsPerPage)
            return;

        //calc pages
        int lRecords = nCount;                          
        int lPages = (int)(lRecords / nItemsPerPage);   
        int lRemainder = lRecords % nItemsPerPage;      
        if (lRemainder > 0)
        {
            lPages = lPages + 1;                        
        }
        
        long lPageTotal = lPages;
        long lCurrentPage = nPage;
        long lItemsPerPage = nItemsPerPage;

        //calc a zero based end index
        int nEndIndex = 0;
        if ((nPage - 1) > 0)
        {
            nEndIndex = (nPage * nItemsPerPage) - 1;    
        }
        else
        {
            nEndIndex = nItemsPerPage;
        }

        //calc a zero based start index
        int nStartIndex = (nEndIndex - nItemsPerPage) + 1;

        //remove items from the front of the dataset
        if (nPage > 1)
        {
            int i = 0;
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (i < nStartIndex)
                    {
                        row.Delete();
                        i++;
                    }
                }
            }
        }

        //remove items from the back of the dataset
        if (nCount > nItemsPerPage)
        {
            int i = -1;
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    i++;

                    if (i > nItemsPerPage-1)
                    {
                        row.Delete();
                        i++;
                    }
                }
            }
        }
    }

    /// <summary>
    /// renders dataset
    /// </summary>
    /// <param name="gv"></param>
    /// <param name="strHeaderNames"></param>
    /// <param name="strListPropertyNames"></param>
    /// <returns></returns>
    public bool RenderDataSet( System.Web.HttpRequest req,
                                     StateBag vs,
                                     DataSet ds,
                                     GridView gv, 
                                     string strFieldNames,      //comma delimeted / Last Name,First Name
                                     string strFieldWidths,     //comma delimeted / 100px,100%
                                     string strIDProperty,       //property used to uniquely id a row
                                     string strHeaderNames,
                                     string strFieldTypes,
                                     int    nItemsPerPage
                                        )   
    {
        //
        //TODO: paging of data is shelved till next release.
        //
        //PageData(ds, 1, nItemsPerPage);
        string strPagerPageID = gv.ID.ToString() + "_pager_current_page";

        //clear rows
        for(int k=0; k<gv.Rows.Count; k++)
        {
            try { gv.DeleteRow(k); }
            catch (Exception e) { string strMsg = e.Message; }
        }

        //clear cloumns
        gv.Columns.Clear();

        //set sorting and paging
        gv.AllowSorting = false;
        gv.AllowPaging = false;
        
        //show footer/header
        gv.ShowFooter = true;
        gv.ShowHeader = true;
            
        gv.CellPadding = 3;
        gv.CaptionAlign = TableCaptionAlign.Top;
        gv.EmptyDataText = "No results found.";
            
        //headers etc...
        string strHeaders = strHeaderNames;
        if (strHeaders.Length < 2)
        {
            strHeaders = strFieldNames;
        }

        string[] arstrHeaderNames = strHeaders.Split(new Char[] { ',' });
        string[] arstrPropertyNames = strFieldNames.Split(new Char[] { ',' });
        string[] arstrHeaderWidths = strFieldWidths.Split(new Char[] { ',' });
        string[] arstrFieldTypes = strFieldTypes.Split(new Char[] { ',' });


        for (int j = 0; j < arstrHeaderWidths.Length; j++)
        {
            arstrHeaderWidths[j] = arstrHeaderWidths[j].ToLower();
            arstrHeaderWidths[j] = arstrHeaderWidths[j].Replace(" ", "");
        }

        if (arstrHeaderNames.Length != arstrPropertyNames.Length)
        {   
            m_strStatus = "Header names and property names are not the same length";
            m_lStatusCode = 1;
        }

        if (arstrHeaderNames.Length < 1)//nothing to do
        {
            m_strStatus = "";
            m_lStatusCode = 0;
            return true;
        }

        string strPagerHTML = "";
        if (nItemsPerPage > 0)
        {   
            ////////////////////////////////////////////////////////////////
            //paging - overriden by alpha
            ///////////////////////////////////////////////////////////////
            int nPg = 1;//always start with page 1

            if (req.Form[strPagerPageID] != null)
            {
                vs[strPagerPageID] = req.Form[strPagerPageID];
                nPg = Convert.ToInt32(req.Form[strPagerPageID]);
                nPg++;
            }
            else
            {
                //read from viewstate if necessary
                if (vs[strPagerPageID] != null)
                {
                    nPg = Convert.ToInt32(vs[strPagerPageID]);
                    nPg++;
                }
            }

            //page the data by removing unnecessary records.
      //      if (strAlpha == "")
        //    {
          //      lst.PageData(nPg, nItemsPerPage);
    //        }
    //        else
   //         {
   //             lst.CalulatePageData(nPg, nItemsPerPage);
   //             nPg = -1;
//
  //              lst.AlphaPageData(strAlphaSortProperty, strAlpha);
    //        }

            //render the pager
   //         strPagerHTML = GetPagerHTML( gv.ID,
     //                                    0,
       //                                  lst.PageTotal,
         //                                nPg - 1);
           
        //    gv.AllowPaging = true;

            //set the page size to the number of items in the list
      //      if (lst.Count < 1)
        //    {
          //      gv.PageSize = 1;
   //         }
     //       else
       //     {
          //      gv.PageSize = lst.Count;
 //           }
        }

        int i = -1;
        foreach (string strHeaderName in arstrHeaderNames)
        {
            i++;

            //BoundField bf;
            TemplateField tf = new TemplateField();

            //header test
            tf.HeaderText = strHeaderName;

            //set width of column
            //
            //get the unit type
            UnitType ut;
            if (arstrHeaderWidths[i].IndexOf("%") > -1)
            {
                ut = UnitType.Percentage;
            }
            else
            {
                ut = UnitType.Pixel;
            }
            arstrHeaderWidths[i] = arstrHeaderWidths[i].Replace("px", "");
            arstrHeaderWidths[i] = arstrHeaderWidths[i].Replace("%", "");
            //
            //make a new unit of measurements
            Unit u = new Unit(Convert.ToDouble(arstrHeaderWidths[i]), ut);

            //set the template fields width
            tf.ItemStyle.Width = u;

            m_nControlCount++;
            tf.ItemTemplate = new CGridViewTemplate( gv,
                                                     ListItemType.EditItem,
                                                     strHeaderName,
                                                     arstrPropertyNames[i],
                                                     strIDProperty,
                                                     Convert.ToInt32(arstrFieldTypes[i]),
                                                     u,
                                                     m_nControlCount);

            //vertical align is top
            tf.ItemStyle.VerticalAlign = VerticalAlign.Top;
            tf.SortExpression = arstrPropertyNames[i];

            //horizontal align is right
            tf.ItemStyle.HorizontalAlign = HorizontalAlign.Left;

            //add the column to the grid
            gv.Columns.Add(tf);
            // gv.Columns[0].HeaderStyle.

            tf = null;
        }
        
        //all our gridviews do these properties
        gv.AutoGenerateColumns = false;
        gv.EnableViewState = false;
        gv.DataSource = ds;
        gv.DataBind();

        //draw pager if paging
        if (strPagerHTML != "")
        {
            GridViewRow pagerRow = gv.BottomPagerRow;

            Label pagerLabel = new Label();
            pagerRow.Visible = true;

            //pagerRow.Cells.Add(new TableCell());
            pagerRow.Cells[0].Text = strPagerHTML;
            pagerRow.Cells[0].CssClass = "gv_pagerstyle";
        }

        return true;
    }
    
    /// <summary>
    /// grid view template
    /// </summary>
    public class CGridViewTemplate : ITemplate
    {
        GridView m_gv;
        ListItemType m_ltType;
        string m_strColHeader;
        string m_strColName;
        int m_nTemplateType;
        string m_strIDPropertyName;
        string m_strColValue;
        Unit m_Width;
        int m_nControlCount;

        /// <summary>
        /// binds literal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BindLiteral(object sender, EventArgs e)
        {
            Literal l = (Literal)sender;
            GridViewRow container = (GridViewRow)l.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, m_strColName);
            m_strColValue = dataValue.ToString();

            object dataValueID = DataBinder.Eval(container.DataItem, m_strIDPropertyName);
            if (dataValue != DBNull.Value)
            {
                l.Text = "<font class=\"gv_itemstyle\">" + dataValue.ToString() + "</font>";
            }
        }

        /// <summary>
        /// binds checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bShowText"></param>
        private void BindCheckBox(object sender, EventArgs e, bool bShowText)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow container = (GridViewRow)chk.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, m_strColName);
            m_strColValue = dataValue.ToString();
            object dataValueID = DataBinder.Eval(container.DataItem, m_strIDPropertyName);

            //set the id to something unique
            chk.ID = m_gv.ID.ToString() + "_" + dataValue.ToString() + "_" + Convert.ToString(m_nControlCount);

            if (dataValue != DBNull.Value)
            {
                if (bShowText)
                    chk.Text = dataValue.ToString();
                else
                    chk.Text = "";

                //set the check box if the underlying data item is true/////////////////////////
                string strChecked = dataValue.ToString();
                strChecked = strChecked.ToUpper();
                strChecked = strChecked.Trim();
                if (strChecked == "1")
                {
                    chk.Checked = true;
                }
                else if (strChecked == "Y")
                {
                    chk.Checked = true;
                }
                else if (strChecked == "T")
                {
                    chk.Checked = true;
                }
                else if (strChecked == "TRUE")
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }

                //generate onclick javascript code//////////////////////////////
                string strOnClick = "";
                strOnClick = "OnGVCheckClick(";

                //id of gridview control
                strOnClick += "'";
                strOnClick += m_gv.ID.ToString();
                strOnClick += "'";

                strOnClick += ", ";

                //property name / column header
                strOnClick += "'";
                strOnClick += m_strColName;
                strOnClick += "'";

                strOnClick += ", ";

                //property value
                strOnClick += "'";
                strOnClick += dataValue.ToString();
                strOnClick += "'";

                strOnClick += ", ";

                //rowid value
                strOnClick += "'";
                strOnClick += dataValueID.ToString();
                strOnClick += "'";

                strOnClick += ");";

                chk.Attributes["onclick"] = strOnClick;
            }
        }

        /// <summary>
        /// binds radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="bShowText"></param>
        private void BindRadioButton(object sender, EventArgs e, bool bShowText)
        {
            Literal l = (Literal)sender;
            GridViewRow container = (GridViewRow)l.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, m_strColName);
            m_strColValue = dataValue.ToString();
            object dataValueID = DataBinder.Eval(container.DataItem, m_strIDPropertyName);

            if (dataValue != DBNull.Value)
            {
                string strOnClick = "";
                strOnClick = "OnGVRadioClick(";

                //id of gridview control
                strOnClick += "'";
                strOnClick += m_gv.ID.ToString();
                strOnClick += "'";

                strOnClick += ", ";

                //property name / column header
                strOnClick += "'";
                strOnClick += m_strColName;
                strOnClick += "'";

                strOnClick += ", ";

                //property value
                strOnClick += "'";
                strOnClick += dataValue.ToString();
                strOnClick += "'";

                strOnClick += ", ";

                //rowid value
                strOnClick += "'";
                strOnClick += dataValueID.ToString();
                strOnClick += "'";

                strOnClick += ");";

                l.Text = "<input type=\"radio\" name=\"";
                l.Text += m_gv.ID.ToString() + "_" + "RDO_GROUP";
                l.Text += "\" onclick=\"";
                l.Text += strOnClick;
                l.Text += "\" id=\"";
                l.Text += m_gv.ID.ToString() + "_" + dataValue.ToString() + "_" + Convert.ToString(m_nControlCount);
                l.Text += "\" ";

                string strChecked = dataValue.ToString();
                strChecked = strChecked.ToUpper();
                strChecked = strChecked.Trim();
                if (strChecked == "1")
                {
                    l.Text += "CHECKED";
                }
                else if (strChecked == "Y")
                {
                    l.Text += "CHECKED";
                }
                else if (strChecked == "T")
                {
                    l.Text += "CHECKED";
                }
                else if (strChecked == "TRUE")
                {
                    l.Text += "CHECKED";
                }
                else
                {
                    l.Text += "";
                }

                l.Text += " >";

                if (bShowText)
                {
                    l.Text += " ";
                    l.Text += dataValue.ToString();
                }
            }
        }

        /// <summary>
        /// binds hyperlink
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BindHyperLink(object sender, EventArgs e)
        {
            HyperLink lnk = (HyperLink)sender;
            GridViewRow container = (GridViewRow)lnk.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, m_strColName);
            m_strColValue = dataValue.ToString();
            object dataValueID = DataBinder.Eval(container.DataItem, m_strIDPropertyName);

            if (dataValue != DBNull.Value)
            {
                //lnk.Text = "<font class=\"gv_itemstyle\">" + dataValue.ToString() + "</font>";
                lnk.Text = dataValue.ToString();
                lnk.CssClass = "gv_link";

                string strOnClick = "";
                strOnClick = "OnGVLinkClick(";

                //id of gridview control
                strOnClick += "'";
                strOnClick += m_gv.ID.ToString().Replace("'", "");
                strOnClick += "'";

                strOnClick += ", ";

                //column header
                strOnClick += "'";
                strOnClick += m_strColHeader.Replace("'", "");
                strOnClick += "'";

                strOnClick += ", ";

                //property name / column header
                strOnClick += "'";
                strOnClick += m_strColName.Replace("'", "");
                strOnClick += "'";

                strOnClick += ", ";

                //property value
                strOnClick += "'";
                strOnClick += dataValue.ToString().Replace("'", "");
                strOnClick += "'";

                strOnClick += ", ";

                //rowid value
                strOnClick += "'";
                strOnClick += dataValueID.ToString().Replace("'", "");
                strOnClick += "'";

                strOnClick += ");";

                lnk.NavigateUrl = "javascript:" + strOnClick;
                lnk.Attributes["onclick"] = strOnClick + " return false;";
            }
        }

        /// <summary>
        /// binds textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BindTextBox(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            GridViewRow container = (GridViewRow)txt.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, m_strColName);
            m_strColValue = dataValue.ToString();
            object dataValueID = DataBinder.Eval(container.DataItem, m_strIDPropertyName);

            //set the id to something unique
            txt.ID = m_gv.ID.ToString() + "_" + dataValue.ToString() + "_" + Convert.ToString(m_nControlCount);

            if (dataValue != DBNull.Value)
            {
                txt.Text = dataValue.ToString();
            }
        }

        /// <summary>
        /// Create a public method that will handle the
        /// DataBinding event called in the InstantiateIn method. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BindData(object sender, EventArgs e)
        {
            m_strColValue = "";

            switch (m_nTemplateType)
            {
                case 1://text
                    BindLiteral(sender, e);
                    break;

                case 2://hyper link
                    BindHyperLink(sender, e);
                    break;

                case 3://check box, w/o text
                    BindCheckBox(sender, e, false);
                    break;

                case 4: //check box with text
                    BindCheckBox(sender, e, true);
                    break;

                case 5: //radio button no text
                    BindRadioButton(sender, e, false);
                    break;

                case 6: //radio button with text
                    BindRadioButton(sender, e, true);
                    break;

                case 7: //radio button with text
                    BindTextBox(sender, e);
                    break;
            }
        }

        /// <summary>
        /// sets gridview template
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="type"></param>
        /// <param name="strColHeader"></param>
        /// <param name="strColName"></param>
        /// <param name="strIDPropertyName"></param>
        /// <param name="nTemplateType"></param>
        /// <param name="u"></param>
        /// <param name="nControlCount"></param>
        public CGridViewTemplate(GridView gv, ListItemType type, string strColHeader, string strColName, string strIDPropertyName, int nTemplateType, Unit u, int nControlCount)
        {
            m_gv = gv;
            m_ltType = type;
            m_strColName = strColName;
            m_nTemplateType = nTemplateType;
            m_strIDPropertyName = strIDPropertyName;
            m_strColValue = "";
            m_Width = u;
            m_nControlCount = nControlCount;
            m_strColHeader = strColHeader;
        }

        /// <summary>
        /// instantiates template
        /// </summary>
        /// <param name="container"></param>
        void ITemplate.InstantiateIn(System.Web.UI.Control container)
        {
            switch (m_ltType)
            {
                case ListItemType.EditItem:

                    switch (m_nTemplateType)
                    {
                        case 1: //cell text

                            Literal l = new Literal();
                            l.DataBinding += new EventHandler(this.BindData);
                            container.Controls.Add(l);
                            break;

                        case 2: //hyper link, post back and pass id

                            HyperLink ht = new HyperLink();
                            //ht.Target = "_blank";
                            ht.DataBinding += new EventHandler(this.BindData);
                            container.Controls.Add(ht);
                            break;

                        case 3: //check box post back and pass id

                            CheckBox chk = new CheckBox();
                            chk.Text = String.Empty;
                            chk.DataBinding += new EventHandler(this.BindData);
                            container.Controls.Add(chk);
                            break;

                        case 4: //check box with next next to it post back and pass id

                            CheckBox chk2 = new CheckBox();
                            chk2.DataBinding += new EventHandler(this.BindData);
                            container.Controls.Add(chk2);
                            break;

                        case 5: //radio button 

                            Literal lbtn = new Literal();
                            lbtn.DataBinding += new EventHandler(this.BindData);
                            container.Controls.Add(lbtn);
                            break;


                        case 6: //radio button w/text 

                            Literal lbtn2 = new Literal();
                            lbtn2.DataBinding += new EventHandler(this.BindData);
                            container.Controls.Add(lbtn2);
                            break;

                        case 7: //text

                            TextBox txt = new TextBox();
                            txt.DataBinding += new EventHandler(this.BindData);
                            txt.Width = m_Width;
                            container.Controls.Add(txt);
                            break;

                        default:

                            Literal l2 = new Literal();
                            l2.DataBinding += new EventHandler(this.BindData);
                            container.Controls.Add(l2);
                            break;
                    }

                    break;
            }
        }
    }
}

