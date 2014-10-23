//define the soap assessment namespace
soap.assessment =
{
    //define some constants
    _stat: {
        axesRadGrpName: 'rblDiagAxes',
        axesDivId: ['divDiagAxis1', 'divDiagAxis2', 'divDiagAxis3', 'divDiagAxis4', 'divDiagAxis5', 'divDiagAxis10']
    },

    //get selected axis (from radio btns. list)
    getSlectedAxis: function()
    {
        var me = this;
        var iAxis = 0;
        $('input[type="radio"][name$="' + me._stat.axesRadGrpName + '"]').each(function()
        {
            var rad = this;
            if (rad.checked)
            {
                iAxis = parseInt(rad.value);
            }
        });
        return iAxis;
    },

    //show div for the selected axis
    showAxisDiv: function()
    {
        var me = this,
            selAxis = me.getSlectedAxis() - 1;
        $.each(me._stat.axesDivId, function(i, v)
        {
            $('div[id$="' + v + '"]').hide();
        });
        $('div[id$="' + me._stat.axesDivId[selAxis] + '"]').show();
        me.toggleCriteriaDefs();
    },

    //list of diagnosis axes tables id
    tblAxis: ['tblDiagAxis1', 'tblDiagAxis2', 'tblDiagAxis3', 'tblDiagAxis4', 'tblDiagAxis5', 'tblDiagAxis10'],

    //list of possible problems tables id
    tblPosProb: ['tblPosProbA1', 'tblPosProbA2', 'tblPosProbA3', 'tblPosProbA4', 'tblPosProbA5', 'tblPosProbA10'],

    //paint selected axis row
    markSelectedRow: function()
    {
        var me = this;

        //check actual problem grids
        $.each(me.tblAxis, function(i, v)
        {
            me.resetTableRows(v);
        });

        //check potential problem grids
        $.each(me.tblPosProb, function(i, v)
        {
            me.resetTableRows(v);
        });
    },

    //clear table rows selection
    resetTableRows: function(id)
    {
        $('tbody tr', $('table[id$="' + id + '"]')).each(function()
        {
            var mrow = this;
            $('[type="radio"]', $(this)).each(function()
            {
                if (this.checked)
                {
                    $(mrow).addClass('selected');
                } else
                {
                    $(mrow).removeClass('selected');
                }
            });
        });
    },

    //shows and initialize discontinue window
    discontinueDiagnosis: function(obj)
    {
        //get problemID
        var problemID = parseInt($(obj).attr('problemid'));

        //clear error label text
        $('span[id$="lblDiscDiagItemError"]').text('');

        //clear discontinue reason text
        $('textarea[id$="txtDiscDiag"]').val('');

        //set the problem id value into hidden field
        $('input[id$="htxtSelectedActualProblem"]').val(problemID);

        //display the popup
        setTimeout('winDiscDiagItem.show()', 100);

        $(document).ready(function () {
            setTimeout(function () {
                wp.adjustMain();
            }, 1);
        });
    },

    //check text lenrth to enable/disable discontinue button
    checkTextLength: function(obj)
    {
        var strDisc = obj.value;
        strDisc = strDisc.replace(/\s/gi, '');

        if (strDisc.length > 2)
        {
            $('input[id$="btnDiscDiagItem"]').removeAttr('disabled');
        }
        else
        {
            $('input[id$="btnDiscDiagItem"]').attr('disabled', 'disabled');
        }
    },

    //call add diagnosis popup
    //but also creates a hidden field for the intake_problem_id
    addFromPotential: function(axis, obj)
    {
        winAxis = [];
        winAxis[1] = winAxis1;
        winAxis[2] = winAxis2;
        winAxis[3] = null;
        winAxis[4] = winAxis4;
        winAxis[5] = winAxis5;

        var diagprobid = $(obj).attr('diagprobid'),
            input1 = document.createElement("input");

        input1.setAttribute("type", "hidden");
        input1.setAttribute("name", "htxtPotDiagID");
        input1.setAttribute("id", "htxtPotDiagID");
        input1.setAttribute("value", diagprobid);

        document.forms[0].appendChild(input1);

        if (axis != 3)
        {
            Ext.onReady(function() { setTimeout('winAxis' + axis + '.show();', 0); });
        }
    },

    //erase temp diag_id hidden field
    eraseHtxtDiag: function()
    {
        $('[id$="htxtPotDiagID"]').remove();
        $('[id$="htxtPotIntkProbID"]').remove();
    },

    //show/hide Criteria/Definitions Div
    toggleCriteriaDefs: function()
    {
        var me = this;
        
        $.each(me._stat.axesDivId, function(i, v)
        {
            if ($('div[id$="' + v + '"]').css('display') != "none")
            {
                if ($('tbody tr input[type="radio"]:checked', $('table[id$="'+ me.tblAxis[i] +'"]')).length > 0 || $('tbody tr input[type="radio"]:checked', $('table[id$="'+ me.tblPosProb[i] +'"]')).length > 0)
                {
                    $('div[id$="divCriteriaDefs"]').show();
                }
                else
                {
                    $('div[id$="divCriteriaDefs"]').hide();
                }
            }
        });
    },

    //init
    init: function()
    {
        var me = this;
        me.markSelectedRow();
        me.toggleCriteriaDefs();
    }

};

//initial calls
