if (typeof patient == "undefined") {
    patient = {};
}

patient.summary = {

    opts: {
        month_name: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        selectedGraphIndex: 0,
        adherenceData: null,
        ahiData: null,
        baselineAHIData: null,
        leakData: null,
        qolData: null,
        qosData: null,
        chartPlaceholder: 'divChartPlaceholder',
        graphTitleID: 'hdGraphTitle',
        YAxisLabel: 'YAxisLabel',
        XAxisLabel: 'XAxisLabel',
        txAdherenceVal: 'htxtTxAdherence',
        AHIVal: 'htxtAHI',
        BaselineAHI: 'htxtBaselineAHI',
        maskLeakVal: 'htxtMaskLeak',
        leakType: 'htxtLeakType',
        dateRangeID: 'divDateRange',
        fromDateID: 'txtDateFrom',
        toDateID: 'txtDateTo',
        tabularData: 'tblCPAPRawData',
        valueHeader: 'thValue',

        cboQuestionnaire: 'cboQuestionnaireScores',
        rawScores: 'htxtQuestionnaires',
        scoresDataObj: null,
        scoresData: null,

        scoreMarkings: [{
            mid: 3002,
            markings: [{
                yaxis: {
                    from: 11,
                    to: 11
                },
                color: "#ff0000"
            }]
        }]
    },

    //interacting with Treatment Adherence points
    showTooltip: function (x, y, contents) {
        $('<div id="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 5,
            border: '1px solid #fdd',
            padding: '2px',
            'background-color': '#fee',
            opacity: 0.80
        }).appendTo("body").fadeIn(200);
    },

    //get series data
    getSeriesData: function () {
        var me = this,
            adherenceData = eval($('[id$="' + me.opts.txAdherenceVal + '"]').val()),
            ahiData = eval($('[id$="' + me.opts.AHIVal + '"]').val()),
            baselineAHIData = eval($('[id$="' + me.opts.BaselineAHI + '"]').val()),
            maskLeakData = eval($('[id$="' + me.opts.maskLeakVal + '"]').val());

        //convert adherenceData
        var newAdherenceData = [];
        $.each(adherenceData, function (i, a) {
            var iDate = Date.parse(a[0]),
                m_point = [iDate, a[1]];
            newAdherenceData.push(m_point);
        });
        me.opts.adherenceData = newAdherenceData;

        //convert baseline AHI
        var newBaselineAHI = [];
        $.each(baselineAHIData, function (i, a) {
            var iDate = Date.parse(a[0]),
                m_point = [iDate, a[1]];
            newBaselineAHI.push(m_point);
        });
        me.opts.baselineAHIData = newBaselineAHI;

        //convert ahiData
        var newAhiData = [];
        $.each(ahiData, function (i, a) {
            var iDate = Date.parse(a[0]),
                m_point = [iDate, a[1]];
            newAhiData.push(m_point);
        });
        me.opts.ahiData = newAhiData;

        //convert ahiData
        var newMaskLeakData = [];
        $.each(maskLeakData, function (i, a) {
            var iDate = Date.parse(a[0]),
                m_point = [iDate, a[1]];
            newMaskLeakData.push(m_point);
        });
        me.opts.leakData = newMaskLeakData;
    },

    // render treatment adherence graph
    renderTxAdherence: function () {
        var me = this,
            greenData = [],
            yellowData = [],
            redData = [],
            plotData = [],
            txData = me.opts.adherenceData,
            cboQuestionnaire = $('[id$="' + me.opts.cboQuestionnaire + '"]')[0];

        //enable raw data tab
        me.enableRawDataTab(true);

        cboQuestionnaire.selectedIndex = 0;

        me.opts.selectedGraphIndex = 0;

        /*
        $.each(txData, function (i, a) {
            if (a[1] >= 4) {
                greenData.push(a);
            }
            else if (a[1] > 2 && a[1] <= 4) {
                yellowData.push(a);
            }
            else {
                redData.push(a);
            }
        });
        */

        // set graph title
        $('[id="' + me.opts.graphTitleID + '"]').text('Treatment Adherence');

        //set y-axis label
        $('[id="' + me.opts.YAxisLabel + '"]').text('Hours of PAP use');

        var options = {
            xaxis: {
                mode: "time",
                timeformat: "%b %e",
                minTickSize: [1, "day"]
            },

            yaxis: {
                min: 0
                //max: 15
            },

            series: {
                lines: { show: false },
                points: { show: true },
                bars: {
                    barWidth: (23 * 60 * 60 * 1000),
                    align: "center",
                    show: true
                },

            },

            colors: ['#0aff0a', '#FFB90F', '#ff0000'],

            grid: {
                markings: [{ yaxis: { from: 4, to: 4 }, color: "#000000" }],
                labelMargin: 10,
                hoverable: true,
                clickable: true
            }

        };

        $('[id="' + me.opts.chartPlaceholder + '"]').html('');

        //if (greenData.length > 0) {
        //    plotData.push({ data: greenData });
        //}

        //if (yellowData.length > 0) {
        //    plotData.push({ data: yellowData });
        //}

        //if (redData.length > 0) {
        //    plotData.push({ data: redData });
        //}

        plotData.push({ data: txData });

        if (txData.length < 1) {
            options.xaxis = {
                mode: null,
                ticks: 0
            };
        }

        //var iTicks = greenData.length + yellowData.length + redData.length;
        //if (iTicks < 10) {
        //    options.xaxis.ticks = iTicks;
        //}

        me.plot = $.plot($('[id="' + me.opts.chartPlaceholder + '"]'), plotData, options);
        me.plot.setupGrid();

        strValueHeader = 'Time Connected (hrs.)';

        //update tabular data
        me.updateTabularData(me.opts.adherenceData, strValueHeader);

    },

    // render apnea-hypopnea index graph
    renderAHI: function () {
        var me = this,
            greenData = [],
            yellowData = [],
            redData = [],
            plotData = [],
            txData = me.opts.ahiData,
            baselineData = me.opts.baselineAHIData,
            cboQuestionnaire = $('[id$="' + me.opts.cboQuestionnaire + '"]')[0],
            cboTimeWindow = $('select[id$="cboSummaryTimeWindow"]')[0],
            count = 0;

        //enable raw data tab
        me.enableRawDataTab(true);

        cboQuestionnaire.selectedIndex = 0;

        me.opts.selectedGraphIndex = 1;

        //setgraph title
        $('[id="' + me.opts.graphTitleID + '"]').text('Apnea-Hypopnea Index');

        //set y-axis label
        $('[id="' + me.opts.YAxisLabel + '"]').text('Events per Hour');

        var options = {
            xaxis: {
                mode: "time",
                timeformat: "%b %e",
                minTickSize: [1, "day"]
            },

            series: {
                lines: { show: false },
                points: { show: true },
                bars: {
                    barWidth: (23 * 60 * 60 * 1000),
                    align: "center",
                    show: true
                },

            },

            colors: [],

            grid: {
                markings: [{ yaxis: { from: 10, to: 10 }, color: "#000000" }],
                labelMargin: 10,
                hoverable: true,
                clickable: true
            }

        };

        $('[id="' + me.opts.chartPlaceholder + '"]').html('');

        if (cboTimeWindow.selectedIndex <= 0) {
            plotData.push({ label: 'Baseline AHI', data: baselineData });

            options.colors.push('#0000ff');

            if (baselineData.length > 0) {
                count = count + 1;
            }
        }

        plotData.push({ label: 'Treatment AHI', data: txData });
        options.colors.push('#0AFF0A');

        if (txData.length > 0) {
            count = count + 1;
        }

        if (count < 1)
        {
            options.xaxis = {
                mode: null,
                ticks: 0
            };
        }

        me.plot = $.plot($('[id="' + me.opts.chartPlaceholder + '"]'), plotData, options);
        me.plot.setupGrid();

        strValueHeader = 'AHI';

        //update tabular data
        me.updateTabularData(me.opts.ahiData, strValueHeader);
    },

    // render mask leak graph
    renderMaskLeak: function () {
        var me = this,
            greenData = [],
            yellowData = [],
            redData = [],
            plotData = [],
            txData = me.opts.leakData,
            leakType = $('[id$="' + me.opts.leakType + '"]')[0],
            cboQuestionnaire = $('[id$="' + me.opts.cboQuestionnaire + '"]')[0];

        //enable raw data tab
        me.enableRawDataTab(true);

        cboQuestionnaire.selectedIndex = 0;

        me.opts.selectedGraphIndex = 2;

        //push values greater than 120 to a different series (red)
        $.each(txData, function (i, a) {
            if (a[1] > 120) {
                redData.push(a);
            }
            else {
                greenData.push(a);
            }
        });

        //set graph title
        $('[id="' + me.opts.graphTitleID + '"]').text('Mask Leak' + leakType.value);

        //set y-axis label
        $('[id="' + me.opts.YAxisLabel + '"]').text('Liters per Minute');

        var options = {
            xaxis: {
                mode: "time",
                timeformat: "%b %e",
                minTickSize: [1, "day"]
            },

            series: {
                lines: { show: false },
                points: { show: true },
                bars: {
                    barWidth: (23 * 60 * 60 * 1000),
                    align: "center",
                    show: true
                },

            },

            colors: ['#0AFF0A', '#FF0000'],

            grid: {
                markings: [{ yaxis: { from: 40, to: 40 }, color: "#000000" }],
                labelMargin: 10,
                hoverable: true,
                clickable: true
            }

        },
        count = 0;

        if (redData.length > 0) {
            options.yaxis = { min: 0, max: 120 };
        }

        $('[id="' + me.opts.chartPlaceholder + '"]').html('');

        if (greenData.length > 0) {
            plotData.push({ data: greenData });
        }

        if (redData.length > 0) {
            plotData.push({ label: 'Leak Out of Range', data: redData });
        }

        if (redData.length + greenData.length < 1)
        {
            options.xaxis = {
                mode: null,
                ticks: 0
            };
        }

        plotData.push({ data: plotData });

        //var iTicks = greenData.length + yellowData.length + redData.length;
        //var iTicks = txData.length;

        //if (iTicks < 10) {
        //    options.xaxis.ticks = iTicks;
        //}

        me.plot = $.plot($('[id="' + me.opts.chartPlaceholder + '"]'), plotData, options);
        me.plot.setupGrid();

        strValueHeader = 'Value';

        //update tabular data
        me.updateTabularData(me.opts.leakData, strValueHeader);

    },

    // render quality of sleep graph
    renderQuestionnaires: function (obj) {
        var me = this,
            rawScores = $('input[id$="' + me.opts.rawScores + '"]')[0],
            scoresDataObj = JSON.parse(rawScores.value),
            mid = parseInt($(obj).val()),
            strHeader = $('option:selected', obj).text();

        //-----------------
        var midData = [];

        m_markings = false;

        me.opts.selectedGraphIndex = 4;

        if (mid < 0)
        {
            me.renderTxAdherence();
            return;
        }

        //enable raw data tab
        me.enableRawDataTab(false);

        //build scoresData
        $.each(scoresDataObj, function (a, b) {
            if (b.mid == mid)
            {
                midData = b.mid_data;
            }
        });

        $.each(midData, function (a, b) {
            $.each(b.data, function (c, d) {
                d[0] = Date.parse(d[0]);
                d[1] = parseFloat(d[1]);
            });
        });

        $('[id="' + me.opts.graphTitleID + '"]').text(strHeader);
        $('[id="' + me.opts.XAxisLabel + '"]').text('Questionnaire Dates');
        $('[id="' + me.opts.YAxisLabel + '"]').text('Score');

        // --------------------------------------------------------------
        var options = {
            xaxis: {
                mode: "time",
                timeformat: "%b %e"
                //minTickSize: [1, "day"]
            },

            //yaxis: {
            //    min: 0
            //},

            series: {
                lines: { show: true },
                points: { show: true }
            },

            grid: {
                labelMargin: 20,
                hoverable: true,
                clickable: true
            }

        };

        $('[id="' + me.opts.chartPlaceholder + '"]').html('');
        me.plot = $.plot($('[id="' + me.opts.chartPlaceholder + '"]'), midData, options);
        me.plot.setupGrid();
        // --------------------------------------------------------------

    },

    // show/hide the time range selection controls
    timewindow: function (obj) {
        var me = this,
            fromDate = $('[id$="' + me.opts.fromDateID + '"]')[0],
            toDate = $('[id$="' + me.opts.toDateID + '"]')[0],
            cboQuestionnaires = $('select[id$="'+ me.opts.cboQuestionnaire +'"]')[0];

        fromDate.value = '';
        toDate.value = '';

        if (obj.value != "4") {
            $('[id="' + me.opts.dateRangeID + '"]').hide();
        }

        if (obj.value == "0") {
            me.getSeriesData();
        }
        else if (obj.value == "1") {
            var myDateOne = new Date(),
                sunday = 0,
                dateTwo;

            //get sunday of the current week
            while (myDateOne.getDay() > sunday) {
                myDateOne.setDate(myDateOne.getDate() - 1);
            }

            //substract 7 days to get begining 
            myDateOne.setDate(myDateOne.getDate() - 7);
            dateTwo = new Date(myDateOne.valueOf());
            dateTwo.setDate(dateTwo.getDate() + 6);

            strDate1 = (myDateOne.getMonth() + 1) + '/' + myDateOne.getDate() + '/' + myDateOne.getFullYear();
            strDate2 = (dateTwo.getMonth() + 1) + '/' + dateTwo.getDate() + '/' + dateTwo.getFullYear();

            fromDate.value = strDate1;
            toDate.value = strDate2;

            me.updateGraph();
            return;

        }
        else if (obj.value == "2") {
            var myDateOne = new Date(),
                dateTwo;

            //get 1st day of current month
            while (myDateOne.getDate() > 1) {
                myDateOne.setDate(myDateOne.getDate() - 1);
            }

            //set the las day of previous month
            myDateOne.setDate(myDateOne.getDate() - 1);

            //
            dateTwo = new Date(myDateOne.valueOf());

            //rollback to first day of month
            while (dateTwo.getDate() > 1) {
                dateTwo.setDate(dateTwo.getDate() - 1);
            }

            strDate2 = (myDateOne.getMonth() + 1) + '/' + myDateOne.getDate() + '/' + myDateOne.getFullYear();
            strDate1 = (dateTwo.getMonth() + 1) + '/' + dateTwo.getDate() + '/' + dateTwo.getFullYear();

            fromDate.value = strDate1;
            toDate.value = strDate2;

            me.updateGraph();
            return;
        }
        else if (obj.value == "3") {
            me.getSeriesData();
        }
        else if (obj.value == "4") {
            $('[id="' + me.opts.dateRangeID + '"]').show();
        }

        //re-render the graphs
        if (me.opts.selectedGraphIndex == 0) {
            me.renderTxAdherence();
        }
        else if (me.opts.selectedGraphIndex == 1) {
            me.renderAHI();
        }
        else if (me.opts.selectedGraphIndex == 2) {
            me.renderMaskLeak();
        }
        else if (me.opts.selectedGraphIndex == 4) {
            //me.renderQuestionnaires(cboQuestionnaires);
        }
    },

    //update tabular data
    updateTabularData: function (objData, strValueHeader) {
        var me = this,
            tbody = $('tbody', $('table[id$="' + me.opts.tabularData + '"]'))[0],
            strRow = '<tr><td>%date%</td><td>%value%</td></tr>',
            strValueHd = $('[id$="' + me.opts.valueHeader + '"]')[0];

        if (typeof (strValueHeader) != "undefined") {
            $(strValueHd).text(strValueHeader);
        }

        $('tbody tr', $('table[id$="' + me.opts.tabularData + '"]')).remove();

        $.each(objData, function (i, o) {
            newDate = new Date(o[0]);

            newRow = strRow.replace(/%date%/gi, me.opts.month_name[newDate.getMonth()] + ' ' + newDate.getDate());
            newRow = newRow.replace(/%value%/gi, o[1]);
            $(newRow).appendTo(tbody);
        });
    },

    //update graphs
    updateGraph: function (obj) {
        var me = this,
            fromDate = $('[id$="' + me.opts.fromDateID + '"]')[0],
            toDate = $('[id$="' + me.opts.toDateID + '"]')[0],
            iFromDate = null,
            iToDate = null,
            newTxAdherenceData = [],
            newAHIData = [],
            newMaskLeakData = [],
            newQOSData = [],
            newQOLData = [],
            cboQuestionnaires = $('select[id$="'+ me.opts.cboQuestionnaire +'"]')[0];

        me.getSeriesData();

        if (fromDate.value.length > 0) {
            iFromDate = Date.parse(fromDate.value);
        }

        if (toDate.value.length > 0) {
            iToDate = Date.parse(toDate.value);
        }

        //filter tratment adherence data by time window
        $.each(me.opts.adherenceData, function (i, a) {
            if (iFromDate && iToDate) {
                if (a[0] >= iFromDate && a[0] <= iToDate) {
                    newTxAdherenceData.push(a);
                }
            }

            if (!iFromDate && !iToDate) {
                newTxAdherenceData = me.opts.adherenceData;
            }

            if (iFromDate && !iToDate) {
                if (a[0] >= iFromDate) {
                    newTxAdherenceData.push(a);
                }
            }

            if (!iFromDate && iToDate) {
                if (a[0] <= iToDate) {
                    newTxAdherenceData.push(a);
                }
            }
        });

        me.opts.adherenceData = newTxAdherenceData;

        //filter AHI data by time window
        $.each(me.opts.ahiData, function (i, a) {
            if (iFromDate && iToDate) {
                if (a[0] >= iFromDate && a[0] <= iToDate) {
                    newAHIData.push(a);
                }
            }

            if (!iFromDate && !iToDate) {
                newAHIData = me.opts.ahiData;
            }

            if (iFromDate && !iToDate) {
                if (a[0] >= iFromDate) {
                    newAHIData.push(a);
                }
            }

            if (!iFromDate && iToDate) {
                if (a[0] <= iToDate) {
                    newAHIData.push(a);
                }
            }
        });

        me.opts.ahiData = newAHIData;

        //filter MaskLeak data by time window
        $.each(me.opts.leakData, function (i, a) {
            if (iFromDate && iToDate) {
                if (a[0] >= iFromDate && a[0] <= iToDate) {
                    newMaskLeakData.push(a);
                }
            }

            if (!iFromDate && !iToDate) {
                newMaskLeakData = me.opts.leakData;
            }

            if (iFromDate && !iToDate) {
                if (a[0] >= iFromDate) {
                    newMaskLeakData.push(a);
                }
            }

            if (!iFromDate && iToDate) {
                if (a[0] <= iToDate) {
                    newMaskLeakData.push(a);
                }
            }
        });

        me.opts.leakData = newMaskLeakData;

        //render the graphs
        if (me.opts.selectedGraphIndex == 0) {
            me.renderTxAdherence();
        }
        else if (me.opts.selectedGraphIndex == 1) {
            me.renderAHI();
        }
        else if (me.opts.selectedGraphIndex == 2) {
            me.renderMaskLeak();
        }
        else if (me.opts.selectedGraphIndex == 4) {
            me.renderQuestionnaires(cboQuestionnaires);
        }

    },

    //display datepicker for the date range text boxes
    rptDatepicker: function () {
        var me = this,
            fromDateMax = "",
            toDateMin = "";

        var fromDate = $('input[id$="' + me.opts.fromDateID + '"]').datepicker({
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            dateFormat: 'mm/dd/yy',
            contrainInput: true,
            duration: 'fast',
            showAnim: 'slideDown',
            onSelect: function (selectedDate) {
                instance = $(this).data("datepicker");
                date = $.datepicker.parseDate(
                        instance.settings.dateFormat ||
                        $.datepicker._defaults.dateFormat,
                        selectedDate, instance.settings);
                toDate.datepicker("option", "minDate", date);
            }
        });

        var toDate = $('input[id$="' + me.opts.toDateID + '"]').datepicker({
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            dateFormat: 'mm/dd/yy',
            constrainInput: true,
            duration: 'fast',
            showAnim: 'slideDown',
            onSelect: function (selectedDate) {
                instance = $(this).data("datepicker");
                date = $.datepicker.parseDate(
                        instance.settings.dateFormat ||
                        $.datepicker._defaults.dateFormat,
                        selectedDate, instance.settings);
                fromDate.datepicker("option", "maxDate", date);
            }
        });
    },

    //reset date range text boxes
    resetDateRange: function () {
        var me = this;
        $('input[id$="' + me.opts.fromDateID + '"], input[id$="' + me.opts.toDateID + '"]').val('');
        $('input[id$="' + me.opts.toDateID + '"]').datepicker("option", "minDate", null);
        $('input[id$="' + me.opts.fromDateID + '"]').datepicker("option", "maxDate", null);
    },

    //get Timewindow status
    getTimewindowStatus: function () {
        var me = this,
            fromDate = $('[id$="' + me.opts.fromDateID + '"]')[0],
            toDate = $('[id$="' + me.opts.toDateID + '"]')[0],
            status = 0;

        if (isNaN(Date.parse(fromDate.value)) && isNaN(Date.parse(toDate.value)))
        {
            status = 0;
        }
        else if (!isNaN(Date.parse(fromDate.value)) && isNaN(Date.parse(toDate.value))) {
            status = 1;
        }
        else if (isNaN(Date.parse(fromDate.value)) && !isNaN(Date.parse(toDate.value))) {
            status = 2;
        }
        else if (!isNaN(Date.parse(fromDate.value)) && !isNaN(Date.parse(toDate.value))) {
            status = 3;
        }

        return status;
    },

    //enable raw data tab
    enableRawDataTab: function (bEnabled) {
        var me = this,
            a = $('[id$="tcCPAPSummary"]:eq(0)').attr('id'),
            b = $find(a),
            cbo = $('select[id$="cboSummaryTimeWindow"]')[0];
        b.get_tabs()[1].set_enabled(bEnabled);

        //disable the time window dropdown
        if (bEnabled) {
            $(cbo).css({ visibility: 'visible' }).removeAttr('disabled').show();
        }
        else
        {
            $(cbo).css({ visibility: 'hidden' }).attr('disabled', 'disabled');
        }
    },

    //initial functions
    init: function () {
        var me = this,
            leakType = $('[id$="' + me.opts.leakType + '"]')[0];

        me.getSeriesData();
        me.rptDatepicker();

        //for interaction with graph's points
        me.previousPoint = null;
        $('[id$="' + me.opts.chartPlaceholder + '"]').bind("plothover", function (event, pos, item) {
            //$("#x").text(pos.x.toFixed(2));
            //$("#y").text(pos.y.toFixed(2));

            if (true) {
                if (item) {
                    if (me.previousPoint != item.dataIndex) {
                        me.previousPoint = item.dataIndex;

                        $("#tooltip").remove();
                        var x = item.datapoint[0],
                            y = item.datapoint[1],
                            pointDate = new Date(parseInt(item.datapoint[0])),
                            strDate = me.opts.month_name[pointDate.getMonth()] + ' ' + pointDate.getDate();

                        strTooltipText = strDate + ' : ' + y;

                        if (me.opts.selectedGraphIndex == 0) {
                            strTooltipText = '[' + strDate + '] connected time ' + y + ' hrs.';
                        }
                        else if (me.opts.selectedGraphIndex == 1) {
                            strTooltipText = '[' + strDate + '] AHI: ' + y.toFixed(4);
                        }
                        else if (me.opts.selectedGraphIndex == 2) {
                            strTooltipText = '[' + strDate + '] Mask Leak' + leakType.value + ' : ' + y.toFixed(4);
                        }
                        else if (me.opts.selectedGraphIndex == 4) {
                            strTooltipText = '[' + strDate + '] Score: ' + y;
                        }

                        me.showTooltip(item.pageX, item.pageY, strTooltipText);
                    }
                }
                else {
                    $("#tooltip").remove();
                    me.previousPoint = null;
                }
            }
        });
    }
};