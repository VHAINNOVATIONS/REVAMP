function renderFMP(value, meta, record)
{
    var pat_id = record.data.patient_id;
    var fmp = record.data.fmpssnlast4;

    return String.format(
    '<a href="#null" onclick=\'OnGVLinkClick("gvPatients", "FMP Last 4", "fmplast4", "{2}", "{1}")\'; return false;>{0}</a>',
    value, pat_id, fmp);
}

function renderLastName(value, meta, record)
{
    var pat_id = record.data.patient_id;
    var lastname = record.data.last_name;

    return String.format(
    '<a href="#null" onclick=\'OnGVLinkClick("gvPatients", "Last Name", "last_name", "{2}", "{1}")\'; return false;>{0}</a>',
    value, pat_id, lastname);
}

//set properties for txtSearch depending on radio button selection
function auditRadButton(element)
{
    txtSearch = $('input[id$="txtSearch"]').get(0);
    
    if (element.value != radValue)
    {
        if (element.value == "3") 
        {
            var keyPressAttribute = $(txtSearch).attr("onkeypress");
            if (keyPressAttribute)
            {
                //onkeypress attribute is already there
            }
            else
            {
                //txtSearch.setAttribute("onkeypress", "return maskFMPSSN2(event, this.value, this)");
                //txtSearch.setAttribute("onkeyup", "this.value = this.value.replace(/[^0-9\\/\\-]/, '')");
                txtSearch.setAttribute("maxlength", "6");
            }
        }
        else if (element.value == "2")
        {
            txtSearch.removeAttribute("maxlength");
            txtSearch.setAttribute("maxlength", "10");
            txtSearch.setAttribute("onkeypress", "return onlyNumbers(event)");
            txtSearch.setAttribute("onkeyup", "this.value = this.value.replace(/[^0-9]/, '')");
        }
        else if (element.value == "4") {
            txtSearch.removeAttribute("maxlength");
            txtSearch.setAttribute("maxlength", "8");
            txtSearch.setAttribute("onkeypress", "return onlyNumbers(event)");
            txtSearch.setAttribute("onkeyup", "this.value = this.value.replace(/[^0-9]/, '')");
        }
        else
        {
            txtSearch.removeAttribute("onkeypress");
            txtSearch.removeAttribute("onkeyup");
            txtSearch.setAttribute("maxlength", "10");
        }
        txtSearch.value = "";
        radValue = element.value;
    }
}

//get search radio buttons and textbox
var txtSearch,
    allInpt,
    radBtnList = [],
    radValue = null;


//check state of radio buttons on page load and postbacks
function InitRadios()
{

    txtSearch = $('input[id$="txtSearch"]').get(0);
    $('input[name$="rblSearchType"]').each(function()
    {
        $(this).bind({
            click: function()
            {
                auditRadButton(this);
            }
        });

        if (this.checked)
        {
            auditRadButton(this);
        }
    });
    
}

InitRadios();