// ------------------------------------------
//   CHECKBOXLIST CLASS
// ------------------------------------------

function _CCheckBoxList()
{
    this.CheckAllNone = function(sender)
    {
        var grpPrefix = sender.id.substring(0, sender.id.lastIndexOf('_') + 1);
        var chkGroup = [];
        var IsAll = false;
        var IsNone = false;
        var objAll;
        var objNone;
        var x = document.getElementsByTagName('input');
        if (x)
        {
            for (var i in x)
            {
                if (x[i].id)
                {
                    if (x[i].id.indexOf(grpPrefix) != -1)
                    {
                        chkGroup.push(x[i]);

                        var cIndex = parseInt(x[i].id.substring(x[i].id.lastIndexOf('_') + 1));
                        var cLabel = this.getLabelFor(x[i].id);

                        //look for 'all' obj
                        if ((cIndex == 0 || cIndex == 1) && cLabel == 'ALL')
                        {
                            objAll = x[i];
                            if (sender.id == x[i].id)
                            {
                                IsAll = true;
                            }
                        }

                        //look for 'none' obj
                        if ((cIndex == 0 || cIndex == 1) && cLabel == 'None')
                        {
                            objNone = x[i];
                            if (sender.id == x[i].id)
                            {
                                IsNone = true;
                            }
                        }
                    }
                }
            }
        }
        //change state [un]checked 
        if (IsAll)
        {
            for (var i in chkGroup)
            {
                chkGroup[i].checked = true;
                if (objNone)
                {
                    objNone.checked = false;
                }
            }
        }
        else if (IsNone)
        {
            for (var i in chkGroup)
            {
                chkGroup[i].checked = false;
            }
        }
        else
        {
            if (!sender.checked && objAll)
            {
                objAll.checked = false;
            }
        }
    };

    this.getLabelFor = function(chkId)
    {
        var mLabel = '';
        var x = document.getElementsByTagName('label');
        if (x)
        {
            for (var i in x)
            {
                //if (x[i].getAttribute('for'))
                if (x[i].htmlFor)
                {
                    if (x[i].htmlFor == chkId)
                    {
                        mLabel = x[i].innerHTML;
                        break;
                    }
                }
            }
        }
        return mLabel;
    };
}

var CCheckBoxList = new _CCheckBoxList();