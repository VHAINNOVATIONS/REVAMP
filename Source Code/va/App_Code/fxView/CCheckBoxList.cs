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
/// Summary description for CCheckBoxList
/// </summary>
public class CCheckBoxList
{
    public CCheckBoxList()
    {

    }

    public void SelectALL(CheckBoxList chklst)
    {
        for (int i = 0; i < chklst.Items.Count; i++)
        {
            chklst.Items[i].Selected = true;
        }
    }

    public void DeSelectALL(CheckBoxList chklst)
    {
        for (int i = 0; i < chklst.Items.Count; i++)
        {
            chklst.Items[i].Selected = false;
        }
    }

    public string strJSClass = "CCheckBoxList";

    public void AddALLOption(CheckBoxList chklst)
    {
        if (chklst == null)
        {
            return;
        }

        if (chklst.Items.Count > 0)
        {
            if (chklst.Items[0].Value != "-1")
            {
                chklst.Items.Insert(0, "ALL");
                chklst.Items[0].Value = "-1";  //all 
            }
        }

        string strNewEvent = strJSClass + ".CheckAllNone(this);";
        foreach (ListItem li in chklst.Items)
        {
            string strCurrOnClick = String.Empty;
            if (li.Attributes["onclick"] != null)
            {
                strCurrOnClick = li.Attributes["onclick"];
                //we do not want to duplicate event
                strCurrOnClick = strCurrOnClick.Replace(strNewEvent, "");
            }
            li.Attributes.Add("onclick", strCurrOnClick + strNewEvent);
        }

    }

    public void AddNoneOption(CheckBoxList chklst)
    {
        int nNoneIndex = 0;
        if (chklst.Items.Count > 0)
        {
            if (chklst.Items[0].Value == "-1")
            {
                nNoneIndex = 1; 
            }
        }

        if (chklst.Items.Count > nNoneIndex)
        {
            if (chklst.Items[nNoneIndex].Value != "-2")
            {
                chklst.Items.Insert(nNoneIndex, "None");
                chklst.Items[nNoneIndex].Value = "-2"; 
            }
        }

        string strNewEvent = strJSClass + ".CheckAllNone(this);";
        foreach (ListItem li in chklst.Items)
        {
            string strCurrOnClick = String.Empty;
            if (li.Attributes["onclick"] != null)
            {
                strCurrOnClick = li.Attributes["onclick"];
                //we do not want to duplicate event
                strCurrOnClick = strCurrOnClick.Replace(strNewEvent, "");
            }
            li.Attributes.Add("onclick", strCurrOnClick + strNewEvent);
        }
    }

    public void SetSelectedValues(string txtValues,
                                   CheckBoxList chklst)
    {
        string[] splitText = txtValues.Split(new Char[] { '|' });
        if (splitText.Length < 1)//nothing to do
        {
            return;
        }

        foreach (string txt in splitText)
        {
            for (int i = 0; i < chklst.Items.Count; i++)
            {
                if (chklst.Items[i].Value == txt)
                {
                    chklst.Items[i].Selected = true;
                }
            }
        }
    }

    public void SaveSelectedValuesToTextBox(CheckBoxList chklst,
                                             TextBox txt)
    {
        //get the text
        string strText = txt.Text;

        //loop over the items in the list and add the selected ones to the text box
        for (int i = 0; i < chklst.Items.Count; i++)
        {
            //get the value of the checkbox item
            string strItemValue = chklst.Items[i].Value;

            //is this item in the list of items in the text box
            string strFind = strItemValue + "|";
            int pos = strText.IndexOf(strFind);

            if (chklst.Items[i].Selected)
            {
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
            else
            {
                if (pos != -1)
                {
                    //in the list so delete it since its not selectd
                    strText = strText.Replace(strFind, "");
                }
            }
        }

        txt.Text = strText;
    }

    //get selected values 
    public string GetSelectedValues(CheckBoxList chklst)
    {
        //get the text
        string strText = "";

        //loop over the items in the list and add the selected ones to the text box
        for (int i = 0; i < chklst.Items.Count; i++)
        {
            //get the value of the checkbox item
            string strItemValue = chklst.Items[i].Value;

            if (chklst.Items[i].Selected)
            {
                //add it to the list
                strText += strItemValue + "|";
            }
        }

        if (strText.Length > 2)
        {
            if (strText.Substring(strText.Length - 1, 1) == "|")
            {
                strText = strText.Substring(0, strText.Length - 1);
            }
        }

        return strText;
    }

    //get selected values 
    public string GetSelectedValues(CheckBoxList chklst,
                                     string strDelimeter,
                                     string strSurround)
    {
        //get the text
        string strText = "";

        //loop over the items in the list and add the selected ones to the text box
        for (int i = 0; i < chklst.Items.Count; i++)
        {
            //get the value of the checkbox item
            string strItemValue = chklst.Items[i].Value;

            if (chklst.Items[i].Selected)
            {
                //add it to the list
                strText += strSurround + strItemValue + strSurround + strDelimeter;
            }
        }

        if (strText.Length > 2)
        {
            if (strText.Substring(strText.Length - 1, 1) == strDelimeter)
            {
                strText = strText.Substring(0, strText.Length - 1);
            }
        }

        return strText;
    }

    public void SelectAll(CheckBoxList chklst)
    {
        if (chklst == null)
            return;

        for (int i = 0; i < chklst.Items.Count; i++)
        {
            chklst.Items[i].Selected = true;
        }

    }

    public void DeSelectAll(CheckBoxList chklst)
    {
        if (chklst == null)
            return;

        for (int i = 0; i < chklst.Items.Count; i++)
        {
            chklst.Items[i].Selected = false;
        }

    }

    public void CheckValuesFromDataSet(DataSet ds,
                                        CheckBoxList chklst,
                                        string strIDField,
                                        string strCompareField,
                                        string strCompareValue)
    {
        if (chklst == null)
        {
            return;
        }

        if (ds == null)
        {
            return;
        }

        //ignore case for the compare
        string strCompareV = strCompareValue.ToUpper();

        foreach (DataTable table in ds.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (!row.IsNull(strIDField))
                {
                    //unique id
                    string strIDValue = Convert.ToString(row[strIDField]);

                    if (!row.IsNull(strCompareField))
                    {
                        //value 
                        string strValue = Convert.ToString(row[strCompareField]);

                        //loop over the checklist, find the item and check or uncheck it
                        for (int i = 0; i < chklst.Items.Count; i++)
                        {
                            if (chklst.Items[i].Value == strIDValue)
                            {
                                if (strValue.ToUpper() == strCompareV)
                                {
                                    chklst.Items[i].Selected = true;
                                }
                                else
                                {
                                    chklst.Items[i].Selected = false;
                                }

                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    public int GetIndexJustSelected(HttpRequest req,
                                    CheckBoxList chklst)
    {
        //per microsoftm the checkbox item just selected is in the
        //_eventtarget: ctl00$ContentPlaceHolder1$chklstMAJCOM$1
        //              ctl00$ContentPlaceHolder1$chklstMAJCOM$2 etc...
        string strIndex = "";

        string strET = req.Form["__EVENTTARGET"];
        string strCtlID = chklst.ID;
        int nIndex = strET.IndexOf(strCtlID + "$");
        if (nIndex > -1)
        {
            strIndex = strET.Substring(nIndex + strCtlID.Length + 1);
            if (strIndex != "")
            {
                return Convert.ToInt32(strIndex);
            }
        }

        return -1;
    }

    //render a dataset as a check list
    public void RenderDataSet(BaseMaster BaseMstr,
                               DataSet ds,
                               CheckBoxList chklst,
                               string strTextFields,  //comma delimeted / LastName,FirstName
                               string strIDField)
    {
        //clear exisiting Items, set properties
        try
        {
            chklst.DataSource = null;
            chklst.Items.Clear();

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
                string strChkLstText = "";
                foreach (string txtField in splitTextFields)
                {
                    if (!row.IsNull(txtField))
                    {
                        string strValue = Convert.ToString(row[txtField]);
                        strChkLstText += strValue;
                        strChkLstText += " - ";
                    }
                }

                //strip last " - "
                if (strChkLstText.Length > 4)
                {
                    strChkLstText = strChkLstText.Substring(0, strChkLstText.Length - 3);
                }

                //set item properties
                ListItem chklstItm = new ListItem();
                if (!row.IsNull(strIDField))
                {
                    string strValue = Convert.ToString(row[strIDField]);
                    chklstItm.Value = strValue;
                }
                chklstItm.Text = "&nbsp;" + strChkLstText;
                chklst.Items.Add(chklstItm);
            }
        }
    }
}