using System;
using System.Collections.Generic;
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

public enum SUATUserType : long
{
    PROVIDER = 1,
    PROVIDER_INTERN = 2,
    CASE_MANAGER = 4,
    FRONT_OFFICE = 8,
    ADMINISTRATOR = 16,
    OFFICE_MANAGER = 32,
    PATIENT = 32768
};

public enum IntakeType : long
{
    PATIENT = 1,
    COUNSELOR = 2,
}

//ReVamp Encounter Types
public enum ReVamp_EncounterType : long
{
    INITIAL_EVALUATION = 0,
    INITIAL_PHONE_CALL = 1,
    ONE_WEEK_FU = 2,
    ONE_MONTH_FU = 3,
    AFTER_1_MONTH_FU = 4,
    THREE_MONTHS_FU = 5,
    AFTER_THREE_MONTHS_FU = 6,
    PHONE_CALL_FU = 7,
    SELF_MANAGEMENT = 99
};

public enum EncounterType : long
{
    INITIAL_VISIT = 0,
    THREE_MONTH = 1,
    SIX_MONTH = 3,
    NINE_MONTH = 4,
    TWELVE_MONTH = 5,
    OTHER = 6,
    ADMIN_NOTE = 7,
    GROUP_NOTE = 8,
    TT_MEETING = 9,
    SELF_MANAGEMENT = 99
};

public enum SUATUserRight : long
{
    NoneUR = 0x00000000,
	NoteSubjectiveUR = 0x00000001,
	NoteObjectiveUR = 0x00000002,
	NoteAssessmentUR = 0x00000004,
	NotePlanUR = 0x00000008,
	ReferralsUR = 0x00000010,
	NoteFlagsToDoUR = 0x00000020,
	LockNoteUR = 0x00000040,
	AdminNoteUR = 0x00000080,
	SignAdminNoteUR = 0x00000100,
	GroupNoteUR = 0x00000200,
	CloseCaseUR = 0x00000400,
	CaseManagementUR = 0x00000800,
	ProcessNewPatientsUR = 0x00001000,
	ReviewNotesUR = 0x00002000,
	ReviewAllNotesUR = 0x00004000,
	DataManagementUR = 0x00008000,
	PopulationReportingUR = 0x00010000,
	AdministratorUR = 0x00020000,
    SubstanceHxUR = 0x00040000,
    AggregateRptsUR = 0x00080000,
    ActionRptsUR = 0x00100000
};

public enum RightMode : long
{
    NoAccess    = 0,
    ReadOnly    = 1,
    ReadWrite   = 2
}

public enum Branch : long 
{ AirForce      = 1,
  Army          = 2,
  Marines       = 3,
  Navy          = 4,
  Commercial    = 5
};

/// <summary>
/// Summary description for AppMaster
/// </summary>
public class AppMaster
{
    public BaseMaster BaseMstr;

    //
    public AppMaster()
	{
   	}

    // SOAP Note related combined user rights
    public long lSOAPNoteRights
    {
        get
        {
            return  (long)SUATUserRight.NoteSubjectiveUR +
                    (long)SUATUserRight.NoteObjectiveUR +
                    (long)SUATUserRight.NoteAssessmentUR +
                    (long)SUATUserRight.NotePlanUR +
                    (long)SUATUserRight.ReferralsUR +
                    (long)SUATUserRight.NoteFlagsToDoUR +
                    (long)SUATUserRight.LockNoteUR +
                    (long)SUATUserRight.AdminNoteUR +
					(long)SUATUserRight.SignAdminNoteUR +
                    (long)SUATUserRight.CloseCaseUR;
        }
    }
    
    /////////////////////////////////////////////////////////////////////////////////////
    //check a user right...
    public bool HasUserRight(long ur, long urs)
    {
        long lUserRights = urs;
        long lCompare = lUserRights & ur;
        if (lCompare > 0)
        {
            return true;
        }

        return false;
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //does the user have a specific user right
    public bool HasUserRight(long ur)
    {
        long lUserRights = (long)UserRights;
        long lCompare = lUserRights & ur;
        if (lCompare > 0)
        {
            return true;
        }

        return false;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    //set the base master
    public void SetBaseMaster(BaseMaster basemstr)
	{
        BaseMstr = basemstr;
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //get the user type
    public long UserType
    {
        get
        {
            long lValue = -1;
            string strValue = "";
            BaseMstr.GetSessionValue("USER_TYPE", out strValue);
            if (strValue != "")
            {
                lValue = Convert.ToInt32(strValue);
            }

            return lValue;
        }
        set { BaseMstr.SetSessionValue("USER_TYPE", Convert.ToString(value)); }

    }

    ////////////////////////////////////////////////////////////
    //does the patient have a reason for referral
    public bool PatientHasOpenCase
    {
        get
        {
            //if no patient is looked up then we need to return false.
            if (BaseMstr.SelectedPatientID == "")
            {
                BaseMstr.SetSessionValue("PAT_HAS_OPENCASE", "0");
                return false;
            }

            long lValue = -1;
            string strValue = "";
            BaseMstr.GetSessionValue("PAT_HAS_OPENCASE", out strValue);
            if (strValue != "")
            {
                lValue = Convert.ToInt32(strValue);
            }
            else
            {
                return false;
            }

            if (lValue > 0)
            {
                return true;
            }

            return false;
        }
        set
        {
            if (value)
            {
                BaseMstr.SetSessionValue("PAT_HAS_OPENCASE", "1");
            }
            else
            {
                BaseMstr.SetSessionValue("PAT_HAS_OPENCASE", "0");
            }
        }
    }

    public bool InitialEncounter()
    {
        CPatient pat = new CPatient();
        return pat.InitialEncounter(BaseMstr);
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //get user rights
    public long UserRights
    {
        get
        {
            long lValue = 0;
            string strValue = "";
            BaseMstr.GetSessionValue("USER_RIGHTS", out strValue);
            if (strValue != "")
            {
                lValue = Convert.ToInt32(strValue);
            }

            return lValue;
        }
        set { BaseMstr.SetSessionValue("USER_RIGHTS", Convert.ToString(value)); }

    }

    /////////////////////////////////////////////////////////////////////////////////////
    //get read-only mode for the user rights
    public long UserReadOnly
    {
        get
        {
            long lValue = 0;
            string strValue = "";
            BaseMstr.GetSessionValue("USER_READONLY", out strValue);
            if (strValue != "")
            {
                lValue = Convert.ToInt32(strValue);
            }

            return lValue;
        }
        set { BaseMstr.SetSessionValue("USER_READONLY", Convert.ToString(value)); }
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //get the users dmis
    public string UserDMISID
    {
        get
        {
            string strValue = "";
            BaseMstr.GetSessionValue("USER_DMIS", out strValue);
            return strValue;
        }
        set { BaseMstr.SetSessionValue("USER_DMIS", Convert.ToString(value)); }

    }

    /////////////////////////////////////////////////////////////////////////////////////
    //get the users provider id
    public string UserProviderID
    {
        get
        {
            string strValue = "";
            BaseMstr.GetSessionValue("USER_PROVIDER_ID", out strValue);
            return strValue;
        }
        set { BaseMstr.SetSessionValue("USER_PROVIDER_ID", Convert.ToString(value)); }

    }

    /////////////////////////////////////////////////////////////////////////////////////
    //get the user type
    public long PasswordExpires
    {
        get
        {
            long lValue = -1;
            string strValue = "";
            BaseMstr.GetSessionValue("USER_PWD_EXPDAYS", out strValue);
            if (strValue != "")
            {
                lValue = Convert.ToInt32(strValue);
            }

            return lValue;
        }
        set { BaseMstr.SetSessionValue("USER_PWD_EXPDAYS", Convert.ToString(value)); }

    }

    /////////////////////////////////////////////////////////////////////////////////////
    //load the users details after they login
    public bool LoadUserDetails()
    {
        long lUserType = 0;
        long lUserRights = 0;
        long lUserReadOnly = 0;
        string strDMISID = "";
        string strProviderID = "";
        long lPWDExpiresIn = 0;

        CUser usr = new CUser();
        if(usr.GetLoginUserDetails( BaseMstr,
                                    BaseMstr.FXUserID,
                                    out lUserType,
                                    out lUserRights,
                                    out lUserReadOnly,
                                    out strDMISID,
                                    out strProviderID,
                                    out lPWDExpiresIn))
        {
            UserType = lUserType;
            UserRights = lUserRights;
            UserReadOnly = lUserReadOnly;
            UserDMISID = strDMISID;
            UserProviderID = strProviderID;
            PasswordExpires = lPWDExpiresIn;
            return true;
        }
                
        return false;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    //get a new patient id
    public string GetNewPatientID()
    {
        string strPatientID = "";

        //users site id is part of the patient id
        string strSiteID = UserDMISID;

        //used to be nicid, but just use 6 random chars now
        string strRand = BaseMstr.GenerateRandomChars();
        while (strRand.Length < 6)//always longer but just in case...
        {
            strRand += "0";
        }
        strRand = strRand.Substring(0, 6);
        strRand = strRand.ToUpper();

        //date time as string
        DateTime dtNow = DateTime.Now;
        string strNow = "";
        strNow = Convert.ToString(dtNow.Month)  +
                 Convert.ToString(dtNow.Day)    +
                 Convert.ToString(dtNow.Year)   +
                 Convert.ToString(dtNow.Hour)   +
                 Convert.ToString(dtNow.Minute) +
                 Convert.ToString(dtNow.Second);

        //site id + nic id + date time
        strPatientID = strSiteID + strRand + strNow;

        return strPatientID;
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //get a new provider id
    public string GetNewProviderID()
    {
        string strProviderID = "";

        //users site id logged in is part of the provider id
        string strSiteID = UserDMISID;

        //used to be nicid, but just use 6 random chars now
        string strRand = BaseMstr.GenerateRandomChars();
        while (strRand.Length < 6)//always longer but just in case...
        {
            strRand += "0";
        }
        strRand = strRand.Substring(0, 6);
        strRand = strRand.ToUpper();

        //date time as string
        DateTime dtNow = DateTime.Now;
        string strNow = "";
        strNow = Convert.ToString(dtNow.Month)  +
                 Convert.ToString(dtNow.Day)    +
                 Convert.ToString(dtNow.Year)   +
                 Convert.ToString(dtNow.Hour)   +
                 Convert.ToString(dtNow.Minute) +
                 Convert.ToString(dtNow.Second);

        //site id + nic id + date time
        strProviderID = strSiteID + strRand + strNow;

        return strProviderID;
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //get a new patient id
    public string GetNewEncounterID()
    {
        string strEncounterID = "";

        //just use 6 random chars now
        string strRand = BaseMstr.GenerateRandomChars();
        while (strRand.Length < 6)//always longer but just in case...
        {
            strRand += "0";
        }
        strRand = strRand.Substring(0, 6);
        strRand = strRand.ToUpper();

        //date time as string
        DateTime dtNow = DateTime.Now;
        string strNow = "";
        strNow = Convert.ToString(dtNow.Month)  +
                 Convert.ToString(dtNow.Day)    +
                 Convert.ToString(dtNow.Year)   +
                 Convert.ToString(dtNow.Hour)   +
                 Convert.ToString(dtNow.Minute) +
                 Convert.ToString(dtNow.Second);

        //rand + date time
        strEncounterID = strRand + strNow;

        return strEncounterID;
    }

    /// <summary>
    /// TIU SUPPORT: is support for TIU turned on?
    /// </summary>
    public bool TIU
    {
        get
        {
            bool bTIU = false;
            string strTIU = "";
            if (System.Configuration.ConfigurationManager.AppSettings["AUDIT"] != null)
            {
                strTIU = System.Configuration.ConfigurationManager.AppSettings["TIU"].ToString();
                if (strTIU == "1")
                {
                    bTIU = true;
                }
            }

            return bTIU;
        }
    }

}
