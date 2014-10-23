using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ucPAPDevice : System.Web.UI.UserControl
{
    public BaseMaster BaseMstr { set; get; }
    
    /// <summary>
    /// page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //set js attributes
        txtLowPressure.Attributes.Add("onkeyup", "onlyNumbers(this);");
        txtHighPressure.Attributes.Add("onkeyup", "onlyNumbers(this);");
        txtBaselineAHI.Attributes.Add("onkeyup", "onlyNumbers(this);");

        //add confirm javascript
        AddJSSaveConfirm();
     
    }
    
    /// <summary>
    /// load the patient device
    /// </summary>
    public void LoadPatientDevice() 
    {
        CCPAPDevice dev = new CCPAPDevice(BaseMstr);
        DataSet dsDev = dev.GetPatientDeviceDS();
        
        if (dsDev != null) 
        {
            foreach (DataRow row in dsDev.Tables[0].Rows) 
            {
                //device data

                if (!row.IsNull("serial_number")) 
                {
                    txtSerialNumber.Text = row["serial_number"].ToString();
                }

                if (!row.IsNull("device_name"))
                {
                    txtUnitType.Text = row["device_name"].ToString();
                }

                if (!row.IsNull("pap_type"))
                {
                    foreach (ListItem li in rblPAPType.Items)
                    {
                        li.Selected = (li.Value == row["pap_type"].ToString());
                    }
                }

                if (!row.IsNull("device_type_id"))
                {
                    foreach (ListItem li in cboPAPManufacturer.Items)
                    {
                        li.Selected = (li.Value == row["device_type_id"].ToString());
                    }
                }

                if (!row.IsNull("low_pressure"))
                {
                    txtLowPressure.Text = row["low_pressure"].ToString();
                }

                if (!row.IsNull("high_pressure"))
                {
                    txtHighPressure.Text = row["high_pressure"].ToString();
                }

                if (!row.IsNull("mask_type"))
                {
                    foreach (ListItem li in rblMaskType.Items)
                    {
                        li.Selected = (li.Value == row["mask_type"].ToString());
                    }
                }

                if (!row.IsNull("mask_details"))
                {
                    txtMaskDetails.Text = row["mask_details"].ToString();
                }

                // sleep study data
                if (!row.IsNull("sleep_study_date"))
                {
                    txtStudyDate.Text = Convert.ToDateTime(row["sleep_study_date"]).ToShortDateString();
                }

                if (!row.IsNull("baseline_ahi"))
                {
                    txtBaselineAHI.Text = row["baseline_ahi"].ToString();
                }
            }
        }

        //if we have a serial number and they delete it 
        //later we need to confirm that its ok...
        if (txtSerialNumber.Text.Length > 0)
        {
            hfHiddenConfirm.Value = "1";
        }
    }

    /// <summary>
    /// add a confirm to the save button click
    /// </summary>
    protected void AddJSSaveConfirm()
    {
        //get mastersave control
        Button btnMasterSave = (Button)BaseMstr.FindControl("btnMasterSave");
        if (btnMasterSave == null)
        {
            return;
        }

        string strConfirm = "";

        strConfirm += "if(document.getElementById('";
        strConfirm += txtSerialNumber.ClientID;
        strConfirm += "').value != '')";
        strConfirm += "{";
            strConfirm += "   __doPostBack('";
            strConfirm += btnMasterSave.ClientID;
            strConfirm += "', ''); ";
            //if you dont return true you lose the please wait!
            strConfirm += "   return true;";
        strConfirm += "}";
        strConfirm += "else";
        strConfirm += "{";

            strConfirm += "if(document.getElementById('";
            strConfirm += hfHiddenConfirm.ClientID;
            strConfirm += "').value != '')";
            strConfirm += "{";
            
                strConfirm += "if( confirm('";
                strConfirm += "You are about to delete the patient\\'s PAP serial number. ";
                strConfirm += "If you continue, all data imported to the patient PAP ";
                strConfirm += "Machine graphs will be removed.\\n\\n";
        
                strConfirm += "If you want to change the patient’s serial ";
                strConfirm += "number and keep the data from the previous ";
                strConfirm += "device, click cancel and enter the serial ";
                strConfirm += "number for the new device and then SAVE.\\n\\n";

                strConfirm += "Do you want to continue?";
                strConfirm += "') )";
                strConfirm += "{";
                    strConfirm += "   __doPostBack('";
                    strConfirm += btnMasterSave.ClientID;
                    strConfirm += "', ''); ";
                    //if you dont return true you lose the please wait!
                    strConfirm += "   return true;";
                strConfirm += "}";
                strConfirm += "else";
                strConfirm += "{";
                    strConfirm += "  return false;";
                strConfirm += "}";

            strConfirm += "}";
            strConfirm += "else{";
                strConfirm += "   __doPostBack('";
                strConfirm += btnMasterSave.ClientID;
                strConfirm += "', ''); ";
                //if you dont return true you lose the please wait!
                strConfirm += "   return true;";
            strConfirm += "}";
       
        strConfirm += "}";

        //add the JS to the button click
        btnMasterSave.OnClientClick = strConfirm;
    }

    /// <summary>
    /// save the device
    /// </summary>
    /// <returns></returns>
    public bool SaveDevice() 
    {
        string strPatientName = String.Empty;
        if (ValidateSerialNumber(txtSerialNumber.Text.Trim(), out strPatientName))
        {
            CCPAPDevice dev = new CCPAPDevice(BaseMstr);
            string strMaskType = String.Empty;
            long lPAPManufacturer = 0,
                lPAPType = 0;

            if(rblPAPType.SelectedIndex > -1)
            {
                lPAPType = Convert.ToInt32(rblPAPType.SelectedValue);
            }

            if(rblMaskType.SelectedIndex > -1)
            {
                strMaskType = rblMaskType.SelectedValue;
            }

            if(cboPAPManufacturer.SelectedIndex > -1)
            {
                lPAPManufacturer = Convert.ToInt32(cboPAPManufacturer.SelectedValue);
            }

            bool bSave = dev.AddUpdateDevice(txtUnitType.Text.Trim(), 
                                            lPAPManufacturer, 
                                            txtSerialNumber.Text.Trim(),
                                            txtLowPressure.Text.Trim(), 
                                            txtHighPressure.Text.Trim(), 
                                            strMaskType, 
                                            txtMaskDetails.Text.Trim(), 
                                            txtStudyDate.Text.Trim(), 
                                            txtBaselineAHI.Text.Trim(),
                                            lPAPType);

            //signal whether we show the confirm
            if (bSave)
            {
                if (txtSerialNumber.Text.Length > 0)
                {
                    hfHiddenConfirm.Value = "1";
                }
                else
                {
                    hfHiddenConfirm.Value = "";
                }
            }

            return bSave;
        }
        else
        {
            BaseMstr.StatusCode = 1;
            BaseMstr.StatusComment = "Please enter a different Serial Number for the PAP machine. There is another device registered with this number to "+ strPatientName +".";
            return false;
        }
    }

    /// <summary>
    /// validate the serial number
    /// </summary>
    /// <param name="strSerialNumber"></param>
    /// <param name="strPatientName"></param>
    /// <returns></returns>
    protected bool ValidateSerialNumber(string strSerialNumber, out string strPatientName) 
    {
        strPatientName = String.Empty;
        if (strSerialNumber.Length > 0) 
        {
            CCPAPDevice dev = new CCPAPDevice(BaseMstr);
            DataSet dsSerials = dev.GetOtherDevicesDS();
            if (dsSerials != null)
            {
                foreach (DataRow row in dsSerials.Tables[0].Rows) 
                {
                    if (!row.IsNull("serial_number")) 
                    {
                        if (strSerialNumber == row["serial_number"].ToString()) 
                        { 
                            strPatientName = row["patient_name"].ToString();
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}