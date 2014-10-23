if (typeof (management) == "undefined")
{
    var management = {};
}

management.plandata = {

    _opts: {
        levels: [0, 0, 250, 500, 750, 750],
        currLevelTxt: 'htxtLevel',
        mainDivWrapper: 'divDiagItmsWrapper',
        htxtDiagID: 'htxtDiagID',
        htxtDiagDesc: 'htxtDiagDesc'
    },

    selParent: function(obj)
    {
        var _me = this,
			divDiagItmsWrapper = $('div[id$="' + _me._opts.mainDivWrapper + '"]')[0],
			grplevel = parseInt(obj.getAttribute('grplevel')),
			diagID = obj.getAttribute('diagnosisid'),
            htxtCurrLevel = $('input[id$="' + _me._opts.currLevelTxt + '"]')[0];

        //check if is needed to scroll back levels
        if(parseInt(htxtCurrLevel.value) < parseInt(obj.getAttribute('grplevel'))){

            if (grplevel <= 3)
            {
                $('input[id$="' + _me._opts.currLevelTxt + '"]').val(grplevel);
            } else
            {
                $('input[id$="' + _me._opts.currLevelTxt + '"]').val('3');
            }

            divDiagItmsWrapper.scrollLeft = _me._opts.levels[parseInt(obj.getAttribute('grplevel'))];
        }

        //clear levels greater than the currently selected
        for(var i = (grplevel + 1); i < 6; i++){
            $('div.diag-item', $('div[id$="divLvl_' + i + '"]')).each(function()
            {
                $(this).hide().removeClass('selected');
            });
        }

        $('div.diag-item', $('div[id$="divLvl_' + (grplevel + 1) + '"]')).each(function()
        {
            if ($(this).attr('parentid') == diagID)
            {
                $(this).show();
            }
            else
            {
                $(this).hide();
            }
        });

        $('div.diag-item', $('div[id$="divLvl_' + (grplevel) + '"]')).each(function()
        {
            if ($(this).attr('diagnosisid') == $(obj).attr('diagnosisid'))
            {
                $(this).addClass('selected');
            }
            else
            {
                $(this).removeClass('selected');
            }
        });
    },

    selDiagnosis: function(obj)
    {
        var _me = this;
        grplevel = parseInt(obj.getAttribute('grplevel')),
		    diagID = obj.value;

        if (obj.checked)
        {
            $('div.diag-item', $('div[id$="divLvl_' + grplevel + '"]')).each(function()
            {
                $(this).removeClass('selected');
            });

            //clear levels greater then actual one
            for (var i = (grplevel + 1); i < 6; i++)
            {
                $('div.diag-item', $('div[id$="divLvl_' + i + '"]')).each(function()
                {
                    $(this).removeClass('selected').hide();
                });
            }

            //alert('You\'ve selected: ' + $('label[for="'+ obj.id +'"]').text());

            $('input[id$="' + _me._opts.htxtDiagID + '"]').val(obj.value);
            $('input[id$="' + _me._opts.htxtDiagDesc + '"]').val(function(){
                return $(obj).parent('td').next('td').children('label').text();
            });

        }
    },

    moveBack: function()
    {
        var _me = this,
		    divDiagItmsWrapper = $('div[id$="' + _me._opts.mainDivWrapper + '"]')[0],
		    currLevel = parseInt($('input[id$="' + _me._opts.currLevelTxt + '"]').val());

        if (currLevel > 0)
        {
            currLevel = currLevel - 1;
            $('input[id$="' + _me._opts.currLevelTxt + '"]').val(currLevel);
            divDiagItmsWrapper.scrollLeft = _me._opts.levels[currLevel];
        }
    },

    moveForward: function()
    {
        var _me = this,
		    divDiagItmsWrapper = $('div[id$="' + _me._opts.mainDivWrapper + '"]')[0],
		    currLevel = parseInt($('input[id$="' + _me._opts.currLevelTxt + '"]').val());

        if (currLevel < 4)
        {
            if (currLevel == 0)currLevel = 1;
            if (currLevel >= 3)currLevel = 2;

            currLevel = currLevel + 1;
            var nNextLvlVisibleItems = 0;
            $('div.diag-item', $('div[id$="divLvl_'+ currLevel +'"]')).each(function(){
                if($(this).css('display') != 'none'){
                    nNextLvlVisibleItems = nNextLvlVisibleItems +1;
                }
            });
            if(nNextLvlVisibleItems < 1){
                return false;
            }

            $('input[id$="' + _me._opts.currLevelTxt + '"]').val(currLevel);
            divDiagItmsWrapper.scrollLeft = _me._opts.levels[currLevel];
        }
    }

};