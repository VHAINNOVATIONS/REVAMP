// SOAP.OBJECTIVE.MEASURE
if (typeof (soap) == "undefined") {
    soap = {};
}
if (typeof (soap.objective) == "undefined") {
    soap.objective = {};
}
soap.objective.measure = {
    //options
    opts:{
        probtbls:['tblOMProblemsAxis1', 'tblOMProblemsAxis2', 'tblOMProblemsAxis3', 'tblOMProblemsAxis4', 'tblOMProblemsAxis5', 'tblOMProblemsAxis10'],
        hiddenProbID:'htxtOMProblemID'
    },

    //paint selected problem's row
    markSelectedProblem:function () {
        var me = this, omSelProblem = $('input[id$="' + me.opts.hiddenProbID + '"]').get(0);
        if (omSelProblem) {
            $.each(me.opts.probtbls, function (i, v) {
                if (omSelProblem.value != '') {
                    $('tbody tr', $('table[id$="' + v + '"]')).each(function (a, row) {
                        var rowid = row.id.replace(/\D/gi, '');
                        if (rowid == omSelProblem.value) {
                            $(row).addClass('selected');
                        } else {
                            $(row).removeClass('selected');
                        }
                    });
                }
            });
        }
    },

    //init
    init:function () {
        var me = this;
        $(document).ready(function () {
            me.markSelectedProblem();
        });
    }
};

