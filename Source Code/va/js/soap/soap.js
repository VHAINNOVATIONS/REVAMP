var soap = {

    //editGrid
    editGrid: {

        //static data for elements
        _stat: {
            readCls: 'grid-read',
            editCls: 'grid-edit',
            axes: [
				{
				    axisType: 1,
				    tableId: 'tblDiagAxis1',
				    editCtrls: [
						['cboA1Specifier', 'htxtSpecifierIdOrig'],
						['txtA1Comment', 'htxtA1CommentOrig'],
						['cboA1SortOrder', 'htxtA1SortOrderOrig']
					]
				},
				{
				    axisType: 2,
				    tableId: 'tblDiagAxis2',
				    editCtrls: [
						['txtA2Comment', 'htxtA2CommentOrig'],
						['cboA2SortOrder', 'htxtA2SortOrderOrig']
					]
				},
				{
				    axisType: 3,
				    tableId: 'tblDiagAxis3',
				    editCtrls: [
						['txtA3DiagText', 'htxtA3DiagTextOrig'],
						['txtA3Comment', 'htxtA3CommentOrig'],
						['cboA3SortOrder', 'htxtA3SortOrderOrig']
					]
				},
				{
				    axisType: 4,
				    tableId: 'tblDiagAxis4',
				    editCtrls: [
						['txtA4Comment', 'htxtA4CommentOrig'],
						['cboA4SortOrder', 'htxtA4SortOrderOrig']
					]
				},
				{
				    axisType: 5,
				    tableId: 'tblDiagAxis5',
				    editCtrls: [
						['txtA5Comment', 'htxtA5CommentOrig'],
						['cboA5SortOrder', 'htxtA5SortOrderOrig']
					]
				},
				{
				    axisType: 10,
				    tableId: 'tblDiagAxis10',
				    editCtrls: [
						['txtA10Comment', 'htxtA10CommentOrig'],
						['cboA10SortOrder', 'htxtA10SortOrderOrig']
					]
				}
			]
        },

        //edit current row
        editThisRow: function(obj)
        {
            var thisrow = $(obj).parent().parent().parent('tr');
            var thistbl = $(thisrow).parent().parent('table'); //used to enter edit mode in all rows
            $('.grid-edit', $(thistbl)).show();
            $('.grid-read', $(thistbl)).hide();
            $(thistbl).css({ 'background-color': '#E9FFE9' });
            return true;
        },

        //cancel edit row
        cancelEditThisRow: function(obj, axisIndex)
        {
            var thisAxisStat = this._stat.axes[axisIndex];
            var thistbl = $('table[id$="' + thisAxisStat.tableId + '"]').get(0);
            var thisrows = $('tbody tr', $(thistbl));
            $.each(thisrows, function(a, row)
            {
                $.each(thisAxisStat.editCtrls, function(i, v)
                {
                    $('[id$="' + v[0] + '"]', $(row)).val($('[id$="' + v[1] + '"]', $(row)).val());
                    $('.grid-edit', $(row)).hide();
                    $('.grid-read', $(row)).show();
                });
                $(thistbl).css({ 'background-color': '#FFF' });
            });
        }

    },

    //close and clear signature popup
    closeSign: function()
    {
        Sys.onDomReady(function()
        {
            var ctrls = ['txtProvUsername', 'txtUPassword'];
            $.each(ctrls, function(i, v)
            {
                $('[id$="' + v + '"]').val('');
            });
            $('[id$="chkClosed"]').get(0).checked = false;
            winSignNote.hide();
        });
    },

    //allow only numbers in textbox
    onlyNumbers: function(obj)
    {
        var me = this,
        strValue = obj.value;

        strValue = strValue.replace(/\D/gi, '');
        obj.value = strValue;
    },

    //resize SOAP's note's texboxes
    resizeTxtSOAP: function()
    {
        var k_offset = 78;
        $('textarea[id$="txtSubjective"]').css({ height: $(window).height() - 233 - k_offset + 'px' });
        $('textarea[id$="txtObjective"]').css({ height: $(window).height() - 277 - k_offset + 'px' });
        $('textarea[id$="txtAssessment"]').css({ height: $(window).height() - 270 - k_offset + 'px' });
    },

    //init
    init: function()
    {
        var me = this;
        me.resizeTxtSOAP();
    }

};

//namespace for SUBJECTIVE
// soap.subjective.js

//namespace for OBJECTIVE
// soap.objective.js

//namespace for ASSESSMENT
// soap.assessment.js

//namespace for PLAN
// soap.plan.js

//namespace for FLAGS
// soap.flags.js
