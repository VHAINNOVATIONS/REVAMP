if (typeof (management) == "undefined") {
    management = {};
}

management.event = {

    opts: {
        evtRowsID: 'divEvtRows',
        mainContainer: 'div-page-contents',
        rowsTblClass: 'tbl_evt_rows'
    },

    dimensionRowsDiv: function () {
        var me = this,
            evtRows = $('div[id$="' + me.opts.evtRowsID + '"]')[0],
            mContainer = $('div[id$="'+ me.opts.mainContainer +'"]')[0],
            tblRows = $('table.' + me.opts.rowsTblClass)[0],
            nWidth = $('table.tbl_evt_headings').width() + 40,
            mPos1 = $(mContainer).position(),
            mPos2 = $(evtRows).position();

        nHeight = $(mContainer).height() - (mPos2.top - mPos1.top) - 10;
        $(tblRows).width($('table.tbl_evt_headings').width());

        $(evtRows).css({
            'width': nWidth + 'px',
            'height': nHeight + 'px',
            'overflow': 'auto'
        });
    },

    rptDatepicker: function ()
    {
        $(function()
        {
            fromDateMax = "";
            toDateMin = "";

            var fromDate = $('input[id$="txtFromDate"]').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'mm/dd/yy',
                contrainInput: true,
                duration: 'fast',
                showAnim: 'slideDown',
                onSelect: function(selectedDate)
                {
                    instance = $(this).data("datepicker");
                    date = $.datepicker.parseDate(
                    instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
                    toDate.datepicker("option", "minDate", date);
                }
            });

            var toDate = $('input[id$="txtToDate"]').datepicker({
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'mm/dd/yy',
                constrainInput: true,
                duration: 'fast',
                showAnim: 'slideDown',
                onSelect: function(selectedDate)
                {
                    instance = $(this).data("datepicker");
                    date = $.datepicker.parseDate(
                    instance.settings.dateFormat || $.datepicker._defaults.dateFormat, selectedDate, instance.settings);
                    fromDate.datepicker("option", "maxDate", date);
                }
            });

        });
    },

    filterPatients: function (obj) {
        var me = this,
            patdat = window.patdata,
            val = obj.value,
            patt = new RegExp(val, "gi");

        $('.tbl_evt_rows tr').show();

        if(obj.value.length > 0){
            $.each(patdat, function (i, o) {
                if (o.name.toLowerCase().indexOf(obj.value.toLowerCase()) != -1) {
                    $('tr[id$="' + o.id + '"]').show();
                }
                else
                {
                    $('tr[id$="' + o.id + '"]').hide();
                }
            });
        }
    },

    init: function () {
        var me = this;
        me.dimensionRowsDiv();
        me.rptDatepicker();
    }
};