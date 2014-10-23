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
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for CFormValidation
/// </summary>
public class CFormValidation
{
	public CFormValidation()
	{}

    public bool IsRequired(TextBox tb) 
    {
        if (!String.IsNullOrEmpty(tb.Text))
        {
            return true;
        }
        return false;
    }

    public bool MinLength(TextBox tb, int iLength) 
    {
        //TODO:
        return false;
    }

    public bool MaxLength(TextBox tb, int iLength)
    {
        //TODO:
        return false;
    }

    public bool RangeLength(TextBox tb, int iMinLength, int iMaxLength)
    {
        //TODO:
        return false;
    }

    public bool IsAlphanumeric(TextBox tb)
    {
        //TODO:
        return false;
    }

    public bool LettersOnly(TextBox tb)
    {
        //TODO:
        return false;
    }

    public bool NumbersOnly(TextBox tb)
    {
        //TODO:
        return false;
    }

    public bool HasSpaces(TextBox tb)
    {
        //TODO:
        return false;
    }

    public bool HasNumbers(TextBox tb)
    {
        //TODO:
        return false;
    }

    public bool HasSpecialChars(TextBox tb)
    {
        //TODO:
        return false;
    }

    public bool HasLowercaseLetters(TextBox tb)
    {
        //TODO:
        return false;
    }

    public bool HasUppercaseLetters(TextBox tb)
    {
        //TODO:
        return false;
    }
    
    public bool AllowedChraracters(TextBox tb, string strAllowed)
    {
        //TODO:
        return false;
    }

    public bool HasFormat(TextBox tb, string strFormat)
    {
        //TODO:
        return false;
    }

    public bool MatchRegExpPattern(TextBox tb, string strRegExp)
    {
        //TODO:
        return false;
    }







}
