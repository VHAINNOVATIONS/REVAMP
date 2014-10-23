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
/// Summary description for CListBox
/// </summary>
public class CListBox
{
	public CListBox()
	{
		
	}
    
    public void CopySelItemIgnoreDupe( ListBox lstFrom,
                                       ListBox lstTo)
    {
        //nothing to do
        if(lstFrom.SelectedItem == null)
        {
            return;
        }

        for (int i=0; i<lstTo.Items.Count; i++)
        {
            if (lstTo.Items[i].Value == lstFrom.SelectedItem.Value)
            {
                if (lstTo.Items[i].Text == lstFrom.SelectedItem.Text)
                {
                    //ignore the dupe
                    return;
                }
            }
        }

        ListItem li = new ListItem();
        li.Value = lstFrom.SelectedItem.Value;
        li.Text = lstFrom.SelectedItem.Text;
        lstTo.Items.Add(li);
    }

    public void SaveSelectedValuesToTextBox(ListBox lst,
                                             TextBox txt)
    {
        //get the text
        string strText = txt.Text;

        //loop over the items in the list and add the selected ones to the text box
        for (int i = 0; i < lst.Items.Count; i++)
        {
            //get the value of the checkbox item
            string strItemValue = lst.Items[i].Value;

            //is this item in the list of items in the text box
            string strFind = strItemValue + "|";
            int pos = strText.IndexOf(strFind);

            if (pos != -1)
            {
                //already in the list nothng to do
            }
            else
            {
                //add it to the list
                strText += strItemValue + "|";
            }
        }

        txt.Text = strText;
    }

    //render a dataset as a check list
    public void RenderDataSet(BaseMaster BaseMstr,
                               DataSet ds,
                               ListBox lst,
                               string strTextFields,  //comma delimeted / LastName,FirstName
                               string strIDField)
    {
        //clear exisiting Items, set properties
        try
        {
            lst.DataSource = null;
            lst.Items.Clear();

        }
        catch (Exception ew)
        {
            string str = ew.Message;
        }

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
                string strLstText = "";
                foreach (string txtField in splitTextFields)
                {
                    if (!row.IsNull(txtField))
                    {
                        string strValue = Convert.ToString(row[txtField]);
                        strLstText += strValue;
                        strLstText += " - ";
                    }
                }

                //strip last " - "
                if (strLstText.Length > 4)
                {
                    strLstText = strLstText.Substring(0, strLstText.Length - 3);
                }

                //set item properties
                ListItem lstItm = new ListItem();
                if (!row.IsNull(strIDField))
                {
                    string strValue = Convert.ToString(row[strIDField]);
                    lstItm.Value = strValue;
                }
                lstItm.Text = strLstText;
                lst.Items.Add(lstItm);
            }
        }
    }
}
