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
/// Summary description for CRadioButtonList
/// </summary>
public class CRadioButtonList
{
	public CRadioButtonList()
	{
		
	}

    //set the selected item in a radio button list...
    public void SetSelectedValue( string strValue,
                                  RadioButtonList rbl)
    {
        for (int i = 0; i < rbl.Items.Count; i++)
        {
            if (rbl.Items[i].Value == strValue)
            {
                rbl.Items[i].Selected = true;
            }
        }
    }

    //render a dataset as a radio button list
    public void RenderDataSet( BaseMaster BaseMstr,
                               DataSet ds,
                               RadioButtonList rbl,
                               string strTextFields,  //comma delimeted / LastName,FirstName
                               string strIDField,     //field used to uniquely id a row
                               string strSelectedID)
    {
        //clear exisiting Items, set properties
        try
        {
            rbl.DataSource = null;
            rbl.Items.Clear();
        }
        catch (Exception ew)
        {
            string str = ew.Message;
        }

       
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
                //build the rbl text
                string strRBLText = "";
                foreach (string txtField in splitTextFields)
                {
                    if (!row.IsNull(txtField))
                    {
                        string strValue = Convert.ToString(row[txtField]);
                        strRBLText += strValue;
                        strRBLText += " - ";
                    }
                }

                //strip last " - "
                if (strRBLText.Length > 4)
                {
                    strRBLText = strRBLText.Substring(0, strRBLText.Length - 3);
                }

                //set item properties
                ListItem RBLItm = new ListItem();
                if (!row.IsNull(strIDField))
                {
                    string strValue = Convert.ToString(row[strIDField]);
                    RBLItm.Value = strValue;
                }
                RBLItm.Text = strRBLText;
                rbl.Items.Add(RBLItm);

                //set the selected item if necessary
                if (RBLItm.Value == strSelectedID)
                {
                    nSelectedIndex = rbl.Items.Count - 1;
                }
            }

            //set the selected index
            try
            {
                rbl.SelectedIndex = nSelectedIndex;
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
