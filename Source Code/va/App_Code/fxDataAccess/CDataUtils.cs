using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for CDataUtils
/// </summary>
public class CDataUtils
{
    /// <summary>
    /// empty constructor
    /// </summary>
    public CDataUtils()
    {
        
    }

    /// <summary>
    /// used to retrieve a date/time in a string format for display
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public string GetDateTimeAsString(DateTime dt)
    {
        string strMonth = Convert.ToString(dt.Month);
        if (strMonth.Length < 2)
            strMonth = "0" + strMonth;

        string strDay = Convert.ToString(dt.Day);
        if (strDay.Length < 2)
            strDay = "0" + strDay;

        string strYear = Convert.ToString(dt.Year);

        string strHours = Convert.ToString(dt.Hour);
        if (strHours.Length < 2)
            strHours = "0" + strHours;

        string strMinutes = Convert.ToString(dt.Minute);
        if (strMinutes.Length < 2)
            strMinutes = "0" + strMinutes;

        return strMonth + "/" + strDay + "/" + strYear + " " + strHours + ":" + strMinutes;
    }

    /// <summary>
    /// helper to get one value from a dataset as a string
    /// we do this throughout the app so it makes sense to wrap it
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="strField"></param>
    /// <returns></returns>
    public long GetDSLongValue(DataSet ds, string strField)
    {
        long lValue = -1;
        if (IsEmpty(ds))
        {
            return -1;
        }

        string strValue = GetDSStringValue(ds, strField);
        try
        {
            lValue = Convert.ToInt32(strValue);
        }
        catch (Exception)
        {
            return -1;
        }

        return lValue;
    }

    public string GetDSStringValue(DataSet ds, string strField)
    {
        if (IsEmpty(ds))
        {
            return null;
        }

        foreach (DataTable table in ds.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (!row.IsNull(strField))
                {
                    return Convert.ToString(row[strField]);
                }
            }
        }

        return null;
    }


    /// <summary>
    /// helper to determine if a dataset is empty
    /// </summary>
    /// <param name="ds"></param>
    /// <returns></returns>
    public bool IsEmpty(DataSet ds)
    {
        foreach (DataTable table in ds.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// helper to get a datetime value from a record
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="strField"></param>
    /// <returns></returns>
    public DateTime GetDSDateTimeValue(DataSet ds, string strField)
    {
        DateTime dt = new DateTime(0001, 01, 01, 0, 0, 0);
        if (IsEmpty(ds))
        {
            return dt;
        }

        try
        {

            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (!row.IsNull(strField))
                    {
                        dt = Convert.ToDateTime(row[strField]);
                    }
                }
            }
        }
        catch (Exception)
        {
            return dt;
        }

        return dt;
    }

    /// <summary>
    /// helper to get a datetime from a data row
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="strField"></param>
    /// <returns></returns>
    public DateTime GetDSDateTimeValue(DataRow dr, string strField)
    {
        DateTime dt = new DateTime(0001, 01, 01, 0, 0, 0);
        if (dr == null)
        {
            return dt;
        }

        try
        {
            if (!dr.IsNull(strField))
            {
                dt = Convert.ToDateTime(dr[strField]);
                return dt;
            }
        }
        catch (Exception)
        {
            return dt;
        }

        return dt;
    }

    public string GetMonthAsText(int nMonth)
    {
        string strMonth = "";
        
        if (nMonth == 1)  {strMonth = "JAN";}
        if (nMonth == 2)  { strMonth = "FEB"; }
        if (nMonth == 3)  { strMonth = "MAR"; }
        if (nMonth == 4)  { strMonth = "APR"; }
        if (nMonth == 5)  { strMonth = "MAY"; }
        if (nMonth == 6)  { strMonth = "JUN"; }
        if (nMonth == 7)  { strMonth = "JUL"; }
        if (nMonth == 8)  { strMonth = "AUG"; }
        if (nMonth == 9)  { strMonth = "SEP"; }
        if (nMonth == 10) { strMonth = "OCT"; }
        if (nMonth == 11) { strMonth = "NOV"; }
        if (nMonth == 12) { strMonth = "DEC"; }

        return strMonth;
    }

    //helper to get 1 value from a dataset as a string
    //we do this throughout the app so it makes sense to wrap it
    public string GetStringValueFromDS(DataSet ds, string strField)
    {
        string strValue = "";
        if (ds == null)
        {
            return strValue;
        }

        foreach (DataTable table in ds.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (!row.IsNull(strField))
                {
                    strValue = Convert.ToString(row[strField]);
                }
            }
        }

        return strValue;
    }

    //helper to get 1 value from a dataset as a string
    //we do this throughout the app so it makes sense to wrap it
    public string GetDateValueAsStringFromDS(DataSet ds, string strField)
    {
       
        string strValue = "";
        if (ds == null)
        {
            return strValue;
        }

        foreach (DataTable table in ds.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (!row.IsNull(strField))
                {
                    DateTime dtValue = Convert.ToDateTime(row[strField]);
                    return GetDateAsString(dtValue);
                }
            }
        }

        return strValue;
    }

    //helper to get 1 value from a dataset as a string
    //we do this throughout the app so it makes sense to wrap it
    public long GetLongValueFromDS(DataSet ds, string strField)
    {
        long lValue = -1;
        string strValue = GetStringValueFromDS(ds, strField);
        if (strValue != "")
        {
            lValue = Convert.ToInt32(strValue);
        }

        return lValue;
    }

    //helper to get 1 value from a dataset as a string
    //we do this throughout the app so it makes sense to wrap it
    public int GetIntValueFromDS(DataSet ds, string strField)
    {
        int nValue = -1;
        string strValue = GetStringValueFromDS(ds, strField);
        if (strValue != "")
        {
            nValue = Convert.ToInt32(strValue);
        }

        return nValue;
    }

    public string GetSQLInString1(string strValues)
    {
        string strRet = strValues;
        string[] splitRet = strRet.Split(new Char[] { '|' });
        strRet = "";
        for (int i = 0; i < splitRet.Length; i++)
        {
            if (splitRet[i].Length > 0)
            {
                strRet += "'";
                strRet += splitRet[i];
                strRet += "'";
                strRet += ",";
            }
        }

        if (strRet.Length > 1)
        {
            if (strRet.Substring(strRet.Length - 1, 1) == ",")
            {
                strRet = strRet.Substring(0, strRet.Length - 1);
            }
        }

        return strRet;
    }

    public string GetSQLInString2(string strValues)
    {
        string strRet = strValues;
        string[] splitRet = strRet.Split(new Char[] { '|' });
        strRet = "";
        for (int i = 0; i < splitRet.Length; i++)
        {
            if (splitRet[i].Length > 0)
            {
                // strRet += "'";
                strRet += splitRet[i];
                //strRet += "'";
                strRet += ",";
            }
        }

        if (strRet.Length > 1)
        {
            if (strRet.Substring(strRet.Length - 1, 1) == ",")
            {
                strRet = strRet.Substring(0, strRet.Length - 1);
            }
        }

        return strRet;
    }

    /// <summary>
    /// gets the values from a dataset and splits them with commas ','
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="strColumnName"></param>
    /// <returns></returns>
    public string GetCommaDelimitedValues(DataSet ds,
                                          string strColumnName)
    {
        string strColumnList = "";

        //loop and get a full list of 
        if (ds != null)
        {
            //loop and add
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (!row.IsNull(strColumnName))
                    {
                        strColumnList += row[strColumnName].ToString();
                        strColumnList += ",";
                    }
                }
            }

            //remove last comma
            if (strColumnList.Length > 1)
            {
                strColumnList = strColumnList.Substring(0, strColumnList.Length - 1);
            }
        }

        return strColumnList;
    }

    /// <summary>
    /// gets the values from a dataset and splits them with pipes '|'
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="strColumnName"></param>
    /// <returns></returns>
    public string GetPipeDelimitedValues(DataSet ds,
                                          string strColumnName)
    {
        string strColumnList = "";

        //loop and get a full list of 
        if (ds != null)
        {
            //loop and add
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (!row.IsNull(strColumnName))
                    {
                        strColumnList += row[strColumnName].ToString();
                        strColumnList += "|";
                    }
                }
            }

            //remove last pipe
            if (strColumnList.Length > 1)
            {
                strColumnList = strColumnList.Substring(0, strColumnList.Length - 1);
            }
        }

        return strColumnList;
    }

    /// <summary>
    /// load a .net checkboxlist from a dataset
    /// </summary>
    /// <param name="chklst"></param>
    /// <param name="ds"></param>
    /// <param name="sb"></param>
    public void LoadCheckListBox(CheckBoxList chklst, DataSet ds, StateBag sb)
    {
        if (ds == null)
            return;

        //bind list to data
        chklst.DataSource = ds;
        chklst.DataTextField = "control_name";
        chklst.DataValueField = "control_id";
        chklst.DataBind();

        //enable/disable and preload
        foreach (DataTable table in ds.Tables)
        {
            int i = 0;
            foreach (DataRow row in table.Rows)
            {
                
                //disable rows if necessary
                if (!row.IsNull("control_enabled"))
                {
                    if (Convert.ToInt32(row["control_enabled"]) == 0)
                    {
                        chklst.Items[i].Enabled = false;
                    }
                }               


                //select value if necessary
                if (!row.IsNull("control_value"))
                {
                    if (Convert.ToInt32(row["control_value"]) > 0)
                    {
                        //item is selected
                        chklst.Items[i].Selected = true;

                        //if its an all check then set the viewstate
                        if (!row.IsNull("control_id"))
                        {
                            if (Convert.ToString(row["control_id"]) == "0")
                            {
                                sb[chklst.ClientID + "_CHECK_ALL"] = true;
                            }
                        }
                    }
                }
                else
                {
                    chklst.Items[i].Selected = false;
                }

                i++;
            }
        }
    }

    /// <summary>
    /// load a .net combo box
    /// </summary>
    /// <param name="rbllst"></param>
    /// <param name="ds"></param>
    public void LoadComboBox(ListBox cboList, DataSet ds)
    {
        if (ds == null)
            return;

        //bind list to data
        cboList.DataSource = ds;
        cboList.DataTextField = "control_name";
        cboList.DataValueField = "control_id";
        cboList.DataBind();

        //enable/disable and preload
        foreach (DataTable table in ds.Tables)
        {
            int i = 0;
            foreach (DataRow row in table.Rows)
            {
                //disable rows if necessary
                if (!row.IsNull("control_enabled"))
                {
                    if (Convert.ToInt32(row["control_enabled"]) == 0)
                    {
                        cboList.Items[i].Enabled = false;
                    }
                }

                //select value if necessary
                if (!row.IsNull("control_value"))
                {
                    if (Convert.ToInt32(row["control_value"]) > 0)
                    {
                        cboList.Items[i].Selected = true;
                    }
                }

                i++;
            }
        }

        ListItem itm = new ListItem();
        itm.Text = "";
        itm.Value = "-1";
        //if nothing is selected then select this item...
        if (cboList.SelectedValue == "")
        {
            itm.Selected = true;
        }
        itm.Enabled = true;
        cboList.Items.Insert(0, itm);
    }
    public void LoadComboBox2(DropDownList cbo, DataSet ds, string strTextField, string strValueField)
    {
        cbo.Items.Clear();
        if (ds != null)
        {
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!dr.IsNull(strTextField))
                    {
                        ListItem li = new ListItem();
                        li.Text = dr[strTextField].ToString();
                        if (!dr.IsNull(strValueField))
                        {
                            li.Value = dr[strValueField].ToString();
                        }
                        cbo.Items.Add(li);
                    }
                }
            }
            ListItem liDefault = new ListItem();
            liDefault.Text = "";
            liDefault.Value = "-1";
            //liDefault.Selected = true;
            cbo.Items.Insert(0, liDefault);
        }
    }


    /// <summary>
    /// load a .net radio button list box
    /// </summary>
    /// <param name="rbllst"></param>
    /// <param name="ds"></param>
    public void LoadRadioButtonListBox(RadioButtonList rblList, DataSet ds)
    {
        if (ds == null)
            return;

        //bind list to data
        rblList.DataSource = ds;
        rblList.DataTextField = "control_name";
        rblList.DataValueField = "control_id";
        rblList.DataBind();

        //enable/disable and preload
        foreach (DataTable table in ds.Tables)
        {
            int i = 0;
            foreach (DataRow row in table.Rows)
            {
                //disable rows if necessary
                if (!row.IsNull("control_enabled"))
                {
                    if (Convert.ToInt32(row["control_enabled"]) == 0)
                    {
                        rblList.Items[i].Enabled = false;
                    }
                }

                //select value if necessary
                if (!row.IsNull("control_value"))
                {
                    if (Convert.ToInt32(row["control_value"]) > 0)
                    {
                        rblList.Items[i].Selected = true;
                    }
                }

                i++;
            }
        }
    }

    /// <summary>
    /// get parameter chunk...
    /// </summary>
    /// <param name="strText"></param>
    /// <param name="strLeftOver"></param>
    /// <returns></returns>
    public string GetDataParameterChunk( string strText,
                                         out string strLeftOver)
    {
        string strRet = "";
        int nMaxSize = 3999;

        strLeftOver = "";
        
        //if we are less than or equal to max size then just return
        // the full text
        if (strText.Length <= nMaxSize)
        {
            return strText;
        }

        //split and return
        string[] arstrValues = strText.Split(new Char[] { ',' });
        for (int i = 0; i < arstrValues.Length; i++)
        {
            string strTemp = strRet + arstrValues[i] + ",";
            if (strTemp.Length <= nMaxSize)
            {
                strRet = strTemp;
            }
            else
            {
                for (int j = i; j < arstrValues.Length; j++)
                {
                    strLeftOver += arstrValues[j];
                    strLeftOver += ",";
                }

                break; 
            }
        }

        //remove last comma
        if (strLeftOver.Length > 1)
        {
            strLeftOver = strLeftOver.Substring(0, strLeftOver.Length - 1);
        }
        if (strRet.Length > 1)
        {
            strRet = strRet.Substring(0, strRet.Length - 1);
        }

        return strRet;
    }

    /// <summary>
    /// handle a .net checklistbox change
    /// </summary>
    /// <param name="chklst"></param>
    /// <param name="sb"></param>
    public void HandleCheckBoxListChange(CheckBoxList chklst, StateBag sb)
    {
        int nCount = chklst.Items.Count;

        //did the user previously check all
        bool bPreviouslyCheckedAll = false;
        if (sb[chklst.ClientID + "_CHECK_ALL"] != null)
        {
            if (Convert.ToBoolean(sb[chklst.ClientID + "_CHECK_ALL"]))
            {
                bPreviouslyCheckedAll = true;
            }
        }

        //is all clicked?
        bool bALLChecked = false;
        for (int i = 0; i < nCount; i++)
        {
            if (chklst.Items[i].Text.ToLower().Trim() == "all")
            {
                if (chklst.Items[i].Selected)
                {
                    bALLChecked = true;
                    sb[chklst.ClientID + "_CHECK_ALL"] = true;
                }
            }
        }
        if (!bALLChecked)
        {
            sb[chklst.ClientID + "_CHECK_ALL"] = false;
        }


        //is everything except all clicked?
        bool bAllOthersChecked = true;
        for (int i = 0; i < nCount; i++)
        {
            if (chklst.Items[i].Text.ToLower().Trim() == "all")
            {
                //ignore this one;
            }
            else
            {
                if (!chklst.Items[i].Selected)
                {
                    bAllOthersChecked = false;
                }
            }
        }

        //is something other than all clicked?
        bool bOtherThenAllChecked = false;
        for (int i = 0; i < nCount; i++)
        {
            if (chklst.Items[i].Text.ToLower().Trim() == "all")
            {
                //ignore this one;
            }
            else
            {
                if (chklst.Items[i].Selected)
                {
                    bOtherThenAllChecked = true;
                }
            }
        }
        
        if (bALLChecked)
        {
            if (bPreviouslyCheckedAll)
            {
                if (bOtherThenAllChecked)
                {
                    //remove all check and allow user to sect the option
                    for (int i = 0; i < nCount; i++)
                    {
                        if (chklst.Items[i].Text.ToLower().Trim() == "all")
                        {
                            chklst.Items[i].Selected = false;
                        }
                    }

                    sb[chklst.ClientID + "_CHECK_ALL"] = false;
                }
            }
            else
            {
                //uncheck all the others
                for (int i = 0; i < nCount; i++)
                {
                    if (chklst.Items[i].Text.ToLower().Trim() == "all")
                    {
                        //ignore this one;
                    }
                    else
                    {
                        chklst.Items[i].Selected = false;
                    }
                }

                sb[chklst.ClientID + "_CHECK_ALL"] = true;
            }
        }
        else
        {
            if (bAllOthersChecked)
            {
                //check all and uncheck everything else
                for (int i = 0; i < nCount; i++)
                {
                    if (chklst.Items[i].Text.ToLower().Trim() == "all")
                    {
                        chklst.Items[i].Selected = true;
                    }
                    else
                    {
                        chklst.Items[i].Selected = false;
                    }
                }

                sb[chklst.ClientID + "_CHECK_ALL"] = true;
            }
        }
    }

    /// <summary>
    /// set .net radio button list
    /// </summary>
    /// <param name="rbllst"></param>
    /// <param name="strValue"></param>
    /// <returns></returns>
    public bool SetSelectedRadioListValue(RadioButtonList rblList, string strValue)
    {
        int nCount = rblList.Items.Count;
        for (int i = 0; i < nCount; i++)
        {
            if (rblList.Items[i].Value == strValue)
            {
                rblList.Items[i].Selected = true;
            }
        }
                
        return true;
    }

    /// <summary>
    /// get slected radiobox value
    /// </summary>
    /// <param name="rbllst"></param>
    /// <param name="nValuePieceIndex"></param>
    /// <returns></returns>
    public string GetRadioListBoxValue(RadioButtonList rblList, int nValuePieceIndex)
    {
        string strValue = "";
        int nCount = rblList.Items.Count;
        for (int i = 0; i < nCount; i++)
        {
            if (rblList.Items[i].Selected)
            {
                string[] arstrValues = rblList.Items[i].Value.Split(new Char[] { '_' });

                strValue += arstrValues[nValuePieceIndex];//rbllst.Items[i].Value;
                return strValue;
            }
        }
                
        return strValue;
    }

    /// <summary>
    /// sets the selected checkbox values
    /// </summary>
    /// <param name="chklst"></param>
    /// <param name="strValues"></param>
    /// <returns></returns>
    public bool SetSelectedCheckListboxValues(CheckBoxList chklst, string strValues)
    {
        string[] arstrValues = strValues.Split(new Char[] { ',' });
        foreach (string strValue in arstrValues)
        {
            if (strValue.Length > 0)
            {
                int nCount = chklst.Items.Count;
                for (int i = 0; i < nCount; i++)
                {
                    if (chklst.Items[i].Value == strValue)
                    {
                        chklst.Items[i].Selected = true;
                        break;
                    }
                }
            }
        }

        return true;
    }

    
    /// <summary>
    /// hyung - 20090501 to fix group by order from saved report
    /// sets the selected checkbox values
    /// </summary>
    /// <param name="chklst"></param>
    /// <param name="lst"></param>
    /// <param name="strValues"></param>
    /// <returns></returns>
    public bool SetSelectedCheckListboxValues(CheckBoxList chklst, ListBox lst, string strValues)
    {
        string[] arstrValues = strValues.Split(new Char[] { ',' });
        foreach (string strValue in arstrValues)
        {
            if (strValue.Length > 0)
            {
                int nCount = chklst.Items.Count;
                for (int i = 0; i < nCount; i++)
                {
                    if (chklst.Items[i].Value == strValue)
                    {
                        chklst.Items[i].Selected = true;

                        ListItem itm = new ListItem();
                        itm.Value = chklst.Items[i].Value;
                        itm.Text = chklst.Items[i].Text;
                        lst.Items.Add(itm);

                        break;
                    }
                }
            }
        }

        return true;
    }

    /// <summary>
    /// gets the selected checkbox values
    /// </summary>
    /// <param name="chklst"></param>
    /// <param name="nValuePieceIndex"></param>
    /// <returns></returns>
    public string GetUnSelectedCheckListBoxValues(CheckBoxList chklst, int nValuePieceIndex)
    {
        string strValues = "";
        int nCount = chklst.Items.Count;
        for (int i = 0; i < nCount; i++)
        {
            if (!chklst.Items[i].Selected)
            {
                string[] arstrValues = chklst.Items[i].Value.Split(new Char[] { '_' });

                strValues += arstrValues[nValuePieceIndex];//chklst.Items[i].Value;

                //add comma 
                strValues += ",";
            }
        }

        //remove last comma
        if (strValues.Length > 1)
        {
            strValues = strValues.Substring(0, strValues.Length - 1);
        }

        return strValues;
    }

    /// <summary>
    /// gets the selected checkbox values
    /// </summary>
    /// <param name="chklst"></param>
    /// <param name="nValuePieceIndex"></param>
    /// <returns></returns>
    public string GetSelectedCheckListBoxValues(CheckBoxList chklst, int nValuePieceIndex)
    {
        string strValues = "";
        int nCount = chklst.Items.Count;
        for (int i = 0; i < nCount; i++)
        {
            if (chklst.Items[i].Selected)
            {
                string[] arstrValues = chklst.Items[i].Value.Split(new Char[] { '_' });

                strValues += arstrValues[nValuePieceIndex];//chklst.Items[i].Value;
                
                //add comma 
                strValues += ",";
            }
        }

        //remove last comma
        if (strValues.Length > 1)
        {
            strValues = strValues.Substring(0, strValues.Length - 1);
        }
                
        return strValues;
    }

    public string GetSelectedCheckListBoxText(CheckBoxList chklst)
    {
        string strValues = "";
        int nCount = chklst.Items.Count;
        for (int i = 0; i < nCount; i++)
        {
            if (chklst.Items[i].Selected)
            {
                strValues += chklst.Items[i].Text;
                
                //add comma 
                strValues += ", ";
            }
        }

        //remove last comma
        if (strValues.Length > 1)
        {
            strValues = strValues.Substring(0, strValues.Length - 2);
        }

        return strValues;
    }

    /// <summary>
    /// gets the selected checkbox values
    /// </summary>
    /// <param name="lst"></param>
    /// <param name="nValuePieceIndex"></param>
    /// <returns></returns>
    public string GetListBoxValues(ListBox lst, int nValuePieceIndex)
    {
        string strValues = "";
        int nCount = lst.Items.Count;
        for (int i = 0; i < nCount; i++)
        {
            string[] arstrValues = lst.Items[i].Value.Split(new Char[] { '_' });
            strValues += arstrValues[nValuePieceIndex];//chklst.Items[i].Value;
            strValues += ",";
        }

        //remove last comma
        if (strValues.Length > 1)
        {
            strValues = strValues.Substring(0, strValues.Length - 1);
        }

        return strValues;
    }

    /// <summary>
    /// gets the selected checkbox values
    /// </summary>
    /// <param name="lst"></param>
    /// <param name="nValuePieceIndex"></param>
    /// <returns></returns>
    public string GetListBoxText(ListBox lst)
    {
        string strValues = "";
        int nCount = lst.Items.Count;
        for (int i = 0; i < nCount; i++)
        {
            strValues += lst.Items[i].Text;
            strValues += ", ";
        }

        //remove last comma
        if (strValues.Length > 1)
        {
            strValues = strValues.Substring(0, strValues.Length - 2);
        }

        return strValues;
    }

    public string GetCheckYesNoText(CheckBox chk)
    {
        string strValue = "No";

        if (chk.Checked)
        {
            strValue = "Yes";
        }

        return strValue;
    }

    /// <summary>
    /// get a long value as a string
    /// </summary>
    /// <param name="lValue"></param>
    /// <returns></returns>
    public string GetLongValueAsString(long lValue)
    {
        return Convert.ToString(lValue);
    }

    /// <summary>
    /// get currency value
    /// </summary>
    /// <param name="dblPct"></param>
    /// <param name="dblValue"></param>
    /// <returns></returns>
    public double GetCurrencyValue(double dblPct, double dblValue)
    {
        if (dblPct <= 0)
        {
            return 0;
        }

        if (dblValue <= 0)
        {
            return 0;
        }

        double dblDecimal = dblPct / 100;

        return dblValue * dblDecimal;
    }

    /// <summary>
    /// get percent value
    /// </summary>
    /// <param name="strPercent"></param>
    /// <returns></returns>
    public double GetPercentValue(string strPercent)
    {
        strPercent = strPercent.Replace("$", "");
        strPercent = strPercent.Replace(",", "");
        strPercent = strPercent.Replace("%", "");

        try
        {
            return Convert.ToDouble(strPercent);
        }
        catch (Exception e)
        {
            string strError = e.Message;
            return -1;
        }
    }

    /// <summary>
    /// get int value
    /// </summary>
    /// <param name="strInt"></param>
    /// <returns></returns>
    public int GetIntValue(string strInt)
    {
        try
        {
            return Convert.ToInt32(strInt);
        }
        catch (Exception e)
        {
            string strError = e.Message;
            return -1;
        }
    }

    /// <summary>
    /// get long value
    /// </summary>
    /// <param name="strLong"></param>
    /// <returns></returns>
    public long GetLongValue(string strLong)
    {
        try
        {
            return Convert.ToInt32(strLong);
        }
        catch (Exception e)
        {
            string strError = e.Message;
            return -1;
        }
    }

    /// <summary>
    /// get double value
    /// </summary>
    /// <param name="strDouble"></param>
    /// <returns></returns>
    public double GetDoubleValue(string strDouble)
    {
        strDouble = strDouble.Replace("$", "");
        strDouble = strDouble.Replace(",", "");

        try
        {
            return Convert.ToDouble(strDouble);
        }
        catch (Exception e)
        {
            string strError = e.Message;
            return -1;
        }
    }

    /// <summary>
    /// get currency value
    /// </summary>
    /// <param name="strCurrency"></param>
    /// <returns></returns>
    public double GetCurrencyValue(string strCurrency)
    {
        strCurrency = strCurrency.Replace("$", "");
        strCurrency = strCurrency.Replace(",", "");

        try
        {
            return Convert.ToDouble(strCurrency);
        }
        catch (Exception e)
        {
            string strError = e.Message;
            return 0;
        }
    }

    /// <summary>
    /// get a long value
    /// </summary>
    /// <param name="bValue"></param>
    /// <returns></returns>
    public long GetLongValue(bool bValue)
    {
        if (bValue)
            return 1;

        return 0;
    }

    /// <summary>
    /// get percent as string
    /// </summary>
    /// <param name="dblPercent"></param>
    /// <returns></returns>
    public string GetPercentAsString(double dblPercent)
    {
        string strPercent = "";
        if (dblPercent == 0)
        {
            return strPercent;
        }

        strPercent = Convert.ToString(dblPercent);

        return strPercent;
    }

    /// <summary>
    /// Get a random number, good for forcing the browser to refresh a page
    /// also used to help generate our session id
    /// </summary>
    /// <returns></returns>
    public string GenerateRandomNumber()
    {
        string strRand = "";
        Random r = new Random();
        strRand = Convert.ToString(r.NextDouble());
        strRand = strRand.Replace(".", "");

        return strRand;
    }

    /// <summary>
    /// Get a random chars, good for forcing the browser to refresh a page
    /// also used to help generate our session id
    /// </summary>
    /// <returns></returns>
    public string GenerateRandomChars()
    {
        string strRand = "";
        Random r = new Random();
        strRand = Convert.ToString(r.NextDouble());
        strRand = strRand.Replace(".", "");

        string strRandChars = "";

        for (int i = 0; i < strRand.Length; i++)
        {
            string strC = "";
            strC = strRand.Substring(i, 1);
            if (strC == "0")
            {
                strRandChars += "a";
            }
            else if (strC == "1")
            {
                strRandChars += "b";
            }
            else if (strC == "2")
            {
                strRandChars += "c";
            }
            else if (strC == "3")
            {
                strRandChars += "d";
            }
            else if (strC == "4")
            {
                strRandChars += "e";
            }
            else if (strC == "5")
            {
                strRandChars += "f";
            }
            else if (strC == "6")
            {
                strRandChars += "g";
            }
            else if (strC == "7")
            {
                strRandChars += "h";
            }
            else if (strC == "8")
            {
                strRandChars += "i";
            }
            else if (strC == "9")
            {
                strRandChars += "j";
            }
            else
            {
                strRandChars += "z";
            }
        }

        return strRandChars;
    }

    /// <summary>
    /// set .net checkbox value
    /// </summary>
    /// <param name="chk"></param>
    /// <param name="lValue"></param>
    public void SetCheckBoxValue(CheckBox chk, long lValue)
    {
        if (lValue > 0)
        {
            chk.Checked = true;
        }
        else
        {
            chk.Checked = false;
        }
    }

    /// <summary>
    /// get ssn in pieces
    /// </summary>
    /// <param name="strSSN"></param>
    /// <param name="strSSN1"></param>
    /// <param name="strSSN2"></param>
    /// <param name="strSSN3"></param>
    /// <returns></returns>
    public bool GetSSNElements(string strSSN,
                                out string strSSN1,
                                out string strSSN2,
                                out string strSSN3)
    {
        strSSN1 = "";
        strSSN2 = "";
        strSSN3 = "";
        if (strSSN.Length == 9)
        {
            strSSN1 = strSSN.Substring(0, 3);
            strSSN2 = strSSN.Substring(3, 2);
            strSSN3 = strSSN.Substring(5, 4);

            return true;
        }

        string[] arstrSSN = strSSN.Split(new Char[] { '-' });
        if (arstrSSN.Length < 3)
        {
            return false;
        }

        strSSN1 = arstrSSN[0];
        strSSN2 = arstrSSN[1];
        strSSN3 = arstrSSN[2];

        return true;
    }

    /// <summary>
    /// used to turn a mm/dd/yyyy string into a DateTime
    /// </summary>
    /// <param name="strDateTime"></param>
    /// <returns></returns>
    public DateTime GetDate(string strDateTime)
    {
        string[] arstrDate = strDateTime.Split(new Char[] { '/' });
        if (arstrDate.Length < 3)
        {
            return new System.DateTime(1899, 12, 31, 0, 0, 0);
        }

        int nMonth = Convert.ToInt32(arstrDate[0]);
        int nDay = Convert.ToInt32(arstrDate[1]);
        int nYear = Convert.ToInt32(arstrDate[2]);

        return new System.DateTime(nYear, nMonth, nDay, 0, 0, 0);
    }

    /// <summary>
    /// used to retrieve a date in a string format for display
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public string GetDateAsString(DateTime dt)
    {
        string strMonth = "";
        string strDay = "";
        string strYear = "";

        strMonth = Convert.ToString(dt.Month);
        if (strMonth.Length < 2)
            strMonth = '0' + strMonth;

        strDay = Convert.ToString(dt.Day);
        if (strDay.Length < 2)
            strDay = '0' + strDay;

        strYear = Convert.ToString(dt.Year);

        return strMonth + '/' + strDay + '/' + strYear;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dtDOB"></param>
    /// <returns></returns>
    public string GetCurrentAgeAsString(DateTime dtDOB)
    {
        string strAge = "";
        
        int years = 0;
        int months = 0;
        int days = 0;

        GetAge(dtDOB, DateTime.Now, out years, out months, out days);

        if (years > 0)
        {
            strAge = Convert.ToString(years) + " years";
        }
        else
        {
            if (months > 0)
            {
                strAge = Convert.ToString(months) + " months";
            }
            else
            {
                if (days > 0)
                {
                    strAge = Convert.ToString(days) + " days";
                }

            }
        }

        return strAge;

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dtDOB"></param>
    /// <param name="years"></param>
    /// <param name="months"></param>
    /// <param name="days"></param>
    public void GetCurrentAge(  DateTime dtDOB, 
                                out int years, 
                                out int months, 
                                out int days)
    {
        years = 0;
        months = 0;
        days = 0;

        GetAge(dtDOB, DateTime.Now, out years, out months, out days);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dtDOB"></param>
    /// <param name="dtAsOf"></param>
    /// <param name="years"></param>
    /// <param name="months"></param>
    /// <param name="days"></param>
    public void GetAge( DateTime dtDOB, 
                        DateTime dtAsOf, 
                        out int years, 
                        out int months, 
                        out int days)
    {
        years = 0;
        months = 0;
        days = 0;

        DateTime tmpDOB = new DateTime(dtDOB.Year, dtDOB.Month, 1);

        DateTime tmpFutureDate = new DateTime(dtAsOf.Year, dtAsOf.Month, 1);

        while (tmpDOB.AddYears(years).AddMonths(months) < tmpFutureDate)
        {
            months++;
            if (months > 12)
            {
                years++;
                months = months - 12;
            }
        }

        if (dtAsOf.Day >= dtDOB.Day)
        {
            days = days + dtAsOf.Day - dtDOB.Day;
        }
        else
        {
            months--;
            if (months < 0)
            {
                years--;
                months = months + 12;
            }
            days +=
                DateTime.DaysInMonth(
                    dtAsOf.AddMonths(-1).Year, dtAsOf.AddMonths(-1).Month
                ) + dtAsOf.Day - dtDOB.Day;

        }

        //add an extra day if the dob is a leap day 
        if (DateTime.IsLeapYear(dtDOB.Year) && dtDOB.Month == 2 && dtDOB.Day == 29)
        {
            //but only if the future date is less than 1st March 
            if (dtAsOf >= new DateTime(dtAsOf.Year, 3, 1))
                days++;
        } 
    }

    // Converts an integer value into Roman numerals
    public string NumberToRoman(int number, bool bUppercase)
    {
        // limit the number to 3999 (geater than 4000 require special characters to represent 5000, 10000 etc)
        if (number >= 0 && number < 4000)
        {
            if (number == 0) return "N";

            //array of decimals and roman pairs
            int[] values = new int[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string[] romans = new string[] { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            // Initialize roman number string
            StringBuilder strRoman = new StringBuilder();

            // Loop through arrays elements
            for (int i = 0; i < 13; i++)
            {
                // If the number being converted is less than the test value, append
                // the corresponding numeral or numeral pair to the resultant string
                while (number >= values[i])
                {
                    number -= values[i];
                    strRoman.Append(romans[i]);
                }
            }

            // Return converted value
            if (!bUppercase)
            {
                return strRoman.ToString().ToLower();
            }
            else
            {
                return strRoman.ToString();
            }
        }
        return String.Empty;
    }

    //get letter for outlining
    public string GetLetterOutline(int number, bool bUpercase)
    {
        if (number <= 701) //the farthest letter outline will get is "ZZ"
        {
            string strChar = "";
            string strPos1 = "";
            string strPos0 = "";
            int iPos0;
            int iPos1;
            if (bUpercase)
            {
                iPos0 = 65;
                iPos1 = 64;
            }
            else
            {
                iPos0 = 97;
                iPos1 = 96;
            }
            int reminder = number % 26;
            int quotient = (number - reminder) / 26;
            if (quotient > 0)
            {
                char pos1 = (char)(iPos1 + quotient);
                strPos1 = pos1.ToString();
            }
            char pos0 = (char)(iPos0 + reminder);
            strPos0 = pos0.ToString();
            strChar = strPos1 + strPos0;
            return strChar;
        }
        return null;
    }

    public string GetJSArray(DataSet ds, string strFields)
    {
        string strArr = String.Empty;
        if (!String.IsNullOrEmpty(strFields))
        {
            strFields = strFields.Replace(", ", ",");
        }

        if (strFields.IndexOf(",") > 0)
        {
            if (strFields.LastIndexOf(",") == strFields.Length - 1)
            {
                strFields = strFields.Substring(0, strFields.Length - 1);
            }
            string[] arrFields = strFields.Split(',');
            string strLastField = arrFields[arrFields.GetLength(0) - 1];
            strLastField = strLastField.Replace(' ', '_');
            if (ds != null)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        foreach (string fld in arrFields)
                        {
                            string mField = fld.Replace(" ", "_");
                            if (!dr.IsNull(mField))
                            {
                                if (dr[mField].GetType() == typeof(DateTime))
                                {
                                    strArr += Convert.ToDateTime(dr[mField]).ToShortDateString();
                                }
                                else
                                {
                                    strArr += dr[mField].ToString();
                                }
                            }
                            else
                            {
                                strArr += "";
                            }

                            if (mField != strLastField)
                            {
                                strArr += "|";
                            }
                        }
                        strArr += "^";
                    }
                }

                if (strArr.Length > 0)
                {
                    strArr = strArr.Substring(0, strArr.Length - 1);
                    strArr = "{" + strFields + "}" + strArr;
                }
            }
        }
        return strArr;
    }

    //Validates two dates (string) and compares them ("before", "equal", "after")
    public bool ValidateCompareDates(string strDate1,
                                        string strDate2,
                                        string strRelation,
                                        out bool bIsValidDate1,
                                        out bool bIsValidDate2,
                                        out bool bComparison)
    {

        bIsValidDate1 = false;
        bIsValidDate2 = false;
        bComparison = false;

        DateTime dtDate1;
        DateTime dtDate2;
        int iDateRel;

        if (DateTime.TryParse(strDate1, out dtDate1))
        {
            bIsValidDate1 = true;
        }

        if (DateTime.TryParse(strDate2, out dtDate2))
        {
            bIsValidDate2 = true;
        }

        if (bIsValidDate1 && bIsValidDate2)
        {
            iDateRel = DateTime.Compare(dtDate2, dtDate1);
            if (strRelation == "before")
            {
                bComparison = (iDateRel < 0) ? true : false;
            }
            else if (strRelation == "equal")
            {
                bComparison = (iDateRel == 0) ? true : false;
            }
            else if (strRelation == "after")
            {
                bComparison = (iDateRel > 0) ? true : false;
            }
        }

        if (bIsValidDate1 && bIsValidDate2 && bComparison)
        {
            return true;
        }

        return false;
    }

    public string GetJSONString(DataSet ds)
    {
        string strJSON = String.Empty;

        if (ds != null)
        {

            bool bArrOpen = false;

            foreach (DataTable dt in ds.Tables)
            {
                if (!bArrOpen)
                {
                    strJSON += "[";
                    bArrOpen = true;
                }
                foreach (DataRow dr in dt.Rows)
                {
                    strJSON += "{";
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dr[dc.ColumnName].GetType() == typeof(System.Decimal))
                        {
                            strJSON += "\"" + dc.ColumnName.ToLower() + "\":" + dr[dc.ColumnName].ToString() + ",";
                        }
                        else if (dr[dc.ColumnName].GetType() == typeof(System.DateTime))
                        {
                            if (!dr.IsNull(dc.ColumnName))
                            {
                                strJSON += "\"" + dc.ColumnName.ToLower() + "\":\"" + Convert.ToDateTime(dr[dc.ColumnName]).ToShortDateString() + "\",";
                            }
                            else
                            {
                                strJSON += "\"" + dc.ColumnName.ToLower() + "\":\"" + dr[dc.ColumnName].ToString().Replace("\"","\\\"") + "\",";
                            }
                        }
                        else
                        {
                            string strContents = dr[dc.ColumnName].ToString();
                            strContents = HttpUtility.HtmlEncode(strContents);

                            strJSON += "\"" + dc.ColumnName.ToLower() + "\":\"" + strContents + "\",";
                        }
                    }
                    strJSON += "},";

                }
                if (bArrOpen)
                {
                    strJSON += "],";
                }
            }
            if (!String.IsNullOrEmpty(strJSON))
            {
                strJSON = strJSON.Replace(",}", "}");
                strJSON = strJSON.Replace(",]", "]");
            }
        }
        if (strJSON.Length > 2)
        {
            strJSON = strJSON.Substring(0, strJSON.Length - 1); 
        }
        return strJSON;
    }

    public DataTable GetDataTable(DataSet ds) 
    {
        if (ds != null) 
        {
            DataTable tbl = new DataTable();
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                tbl.Columns.Add(dc.ColumnName.ToLower(), dc.DataType);
            }

            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow mrow = tbl.NewRow();
                    foreach (DataColumn mdc in dt.Columns)
                    {
                        mrow[mdc.ColumnName] = dr[mdc.ColumnName];
                    }
                    tbl.Rows.Add(mrow);
                }
            }
            tbl.AcceptChanges();
            return tbl;
        }
        return null;
    }

    public bool OptionExists(DropDownList ctrl, string strOptionValue) 
    {
        if (ctrl.Items.Count > 0) 
        {
            foreach (ListItem li in ctrl.Items) 
            {
                if (li.Value != null) 
                {
                    if (li.Value.Equals(strOptionValue)) 
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool OptionExists(CheckBoxList ctrl, string strOptionValue)
    {
        if (ctrl.Items.Count > 0)
        {
            foreach (ListItem li in ctrl.Items)
            {
                if (li.Value != null)
                {
                    if (li.Value.Equals(strOptionValue))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool OptionExists(RadioButtonList ctrl, string strOptionValue)
    {
        if (ctrl.Items.Count > 0)
        {
            foreach (ListItem li in ctrl.Items)
            {
                if (li.Value != null)
                {
                    if (li.Value.Equals(strOptionValue))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool OptionExists(ListBox ctrl, string strOptionValue)
    {
        if (ctrl.Items.Count > 0)
        {
            foreach (ListItem li in ctrl.Items)
            {
                if (li.Value != null)
                {
                    if (li.Value.Equals(strOptionValue))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool OptionExists(ListControl ctrl, string strOptionValue)
    {
        if (ctrl.Items.Count > 0)
        {
            foreach (ListItem li in ctrl.Items)
            {
                if (li.Value != null)
                {
                    if (li.Value.Equals(strOptionValue))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }




}
