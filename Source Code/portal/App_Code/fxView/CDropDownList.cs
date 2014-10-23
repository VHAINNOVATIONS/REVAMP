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

using System.Collections.Generic;
using System.Collections;
using System.Text;
using DataAccess;

/// <summary>
/// Summary description for CDropDownList
/// </summary>
public class CDropDownList
{
	public CDropDownList()
	{
		
	}

    //select a value in the dropdownlist
    public void SelectValue( DropDownList cbo, 
                             string strValue)
    {
        cbo.ClearSelection();
        for (int i = 0; i < cbo.Items.Count; i++)
        {
            if (cbo.Items[i] != null)
            {
                if (cbo.Items[i].Value == strValue)
                {
                    cbo.Items[i].Selected = true;
                    return;
                }
            }
        }
    }

    //select a value in the dropdownlist
    public void SelectValue( DropDownList cbo,
                             long lValue)
    {
        string strValue = Convert.ToString(lValue);
        SelectValue(cbo, strValue);
    }

    //render a dataset as a drop down list
    public void RenderDataSet(BaseMaster BaseMstr,
                               DataSet ds,
                               DropDownList cbo,
                               string strTextFields,  //comma delimeted / LastName,FirstName
                               string strIDField,     //field used to uniquely id a row
                               string strSelectedID)
    {
        //clear exisiting Items, set properties
        try
        {
            cbo.DataSource = null;
            cbo.Items.Clear();
        }
        catch (Exception ew)
        {
            string str = ew.Message;
        }

        //our cbo's always have an empty item
        ListItem itm = new ListItem();
        itm.Value = "-1";
        itm.Text = "";
        cbo.Items.Add(itm);
        int nSelectedIndex = -1;

        if (ds == null)
        {
            return;
        }

        //split text fields used to load
        string[] splitTextFields = strTextFields.Split(new Char[] { ',' });
        if (splitTextFields.Length < 1)//nothing to do
        {
           BaseMstr.StatusComment = "";
           BaseMstr.StatusCode = 0;
           return;
        }

        //loop over the dataset and load the dropdownlist
        foreach (DataTable table in ds.Tables)
        {
           foreach (DataRow row in table.Rows)
           {
               //build the cbo text
               string strCBOText = "";
               foreach (string txtField in splitTextFields)
               {
                   if (!row.IsNull(txtField))
                   {
                       string strValue = Convert.ToString(row[txtField]);
                       strCBOText += strValue;
                       strCBOText += " - ";
                   }
               }

               //strip last " - "
               if (strCBOText.Length > 4)
               {
                   strCBOText = strCBOText.Substring(0, strCBOText.Length - 3);
               }

               //set item properties
               ListItem cboItm = new ListItem();
               if (!row.IsNull(strIDField))
               {
                   string strValue = Convert.ToString(row[strIDField]);
                   cboItm.Value = strValue;
               }
               cboItm.Text = strCBOText;
               cbo.Items.Add(cboItm);

               //set the selected item if necessary
               if (cboItm.Value == strSelectedID)
               {
                   nSelectedIndex = cbo.Items.Count - 1;
               }
           }
           
           //set the selected index
           try
           {
               cbo.SelectedIndex = nSelectedIndex;
           }
           catch (Exception e)
           {
               string strE = e.Message;
               //BaseMstr.StatusComment = "";
               //BaseMstr.StatusCode = 0;
           }
        }
    }

    //render a dataset as a drop down list
    public void RenderDataSetByMatch( BaseMaster BaseMstr,
                                      DataSet ds,
                                      DropDownList cbo,
                                      string strTextFields,  //comma delimeted / LastName,FirstName
                                      string strIDField,     //field used to uniquely id a row
                                      string strSelectedID,
                                      string strMatchFieldName,
                                      string strMatchValue)
    {
        //clear exisiting Items, set properties
        try
        {
            cbo.DataSource = null;
            cbo.Items.Clear();
        }
        catch (Exception ew)
        {
            string str = ew.Message;
        }

        //our cbo's always have an empty item
        ListItem itm = new ListItem();
        itm.Value = "-1";
        itm.Text = "";
        cbo.Items.Add(itm);
        int nSelectedIndex = -1;

        if (ds == null)
        {
            return;
        }

        //split text fields used to load
        string[] splitTextFields = strTextFields.Split(new Char[] { ',' });
        if (splitTextFields.Length < 1)//nothing to do
        {
            BaseMstr.StatusComment = "";
            BaseMstr.StatusCode = 0;
            return;
        }

        //loop over the dataset and load the dropdownlist
        foreach (DataTable table in ds.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (!row.IsNull(strMatchFieldName))
                {
                    if (Convert.ToString(row[strMatchFieldName]) == strMatchValue)
                    {
                        //build the cbo text
                        string strCBOText = "";
                        foreach (string txtField in splitTextFields)
                        {
                            if (!row.IsNull(txtField))
                            {
                                string strValue = Convert.ToString(row[txtField]);
                                strCBOText += strValue;
                                strCBOText += " - ";
                            }
                        }

                        //strip last " - "
                        if (strCBOText.Length > 4)
                        {
                            strCBOText = strCBOText.Substring(0, strCBOText.Length - 3);
                        }

                        //set item properties
                        ListItem cboItm = new ListItem();
                        if (!row.IsNull(strIDField))
                        {
                            string strValue = Convert.ToString(row[strIDField]);
                            cboItm.Value = strValue;
                        }
                        cboItm.Text = strCBOText;
                        cbo.Items.Add(cboItm);

                        //set the selected item if necessary
                        if (cboItm.Value == strSelectedID)
                        {
                            nSelectedIndex = cbo.Items.Count - 1;
                        }
                    }
                }
            }
            //set the selected index
            try
            {
                cbo.SelectedIndex = nSelectedIndex;
            }
            catch (Exception e)
            {
                string strE = e.Message;
                //BaseMstr.StatusComment = "";
                //BaseMstr.StatusCode = 0;
            }
        }
    }

}
