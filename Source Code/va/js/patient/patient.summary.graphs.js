if (typeof patient.summary == "undefined") {
    patient.summary = {};
}

patient.summary.graphs = {

    opts: {
        month_name: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        selectedGraphIndex: 0,
        adherenceData: null,
        ahiData: null,
        baselineAHIData: null,
        leakData: null,
        qolData: null,
        qosData: null,
		
        chartPlaceholder0: 'divChartPlaceholder0',
		chartPlaceholder1: 'divChartPlaceholder1',
		chartPlaceholder2: 'divChartPlaceholder2',
		chartPlaceholder3: 'divChartPlaceholder3',
		
        graphTitleID0: 'hdGraphTitle0',
		graphTitleID1: 'hdGraphTitle1',
		graphTitleID2: 'hdGraphTitle2',
		graphTitleID3: 'hdGraphTitle3',		
		
        txAdherenceVal: 'htxtTxAdherence',
        AHIVal: 'htxtAHI',
        BaselineAHI: 'htxtBaselineAHI',
        maskLeakVal: 'htxtMaskLeak',
        leakType: 'htxtLeakType',
		
        dateRangeID: 'divDateRange2',
        fromDateID: 'txtDateFrom2',
        toDateID: 'txtDateTo2',
		
        tabularData: 'tblCPAPRawData',
        valueHeader: 'thValue'
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
            txData = me.opts.adherenceData;

        me.opts.selectedGraphIndex = 0;

        /*
        $.each(txData, function (i, a) {
            if (a[1] > 4) {
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

        $('[id="' + me.opts.graphTitleID0 + '"]').text('Treatment Adherence');

        var options = {
            xaxis: {
                mode: "time",
                timeformat: "%b %e"
            },

            yaxis: {
                min: 0
                //max: 15
            },

            series: {
                lines: { show: false },
                points: { show: true },
                bars: {
                    barWidth: (24 * 60 * 60 * 1000),
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

        $('[id="' + me.opts.chartPlaceholder0 + '"]').html('');

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

        //var iTicks = greenData.length + yellowData.length + redData.length;
        //if (iTicks < 10) {
        //    options.xaxis.ticks = iTicks;
        //}

        me.plot = $.plot($('[id="' + me.opts.chartPlaceholder0 + '"]'), plotData, options);
        me.plot.setupGrid();

        strValueHeader = 'Time Connected (hrs.)';

        //update tabular data
        //me.updateTabularData(me.opts.adherenceData, strValueHeader);

    },

    // render apnea-hypopnea index graph
    renderAHI: function () {
        var me = this,
            greenData = [],
            yellowData = [],
            redData = [],
            plotData = [],
            baselineData = me.opts.baselineAHIData,
            txData = me.opts.ahiData;

        me.opts.selectedGraphIndex = 1;

        /*
        $.each(txData, function (i, a) {
            if (a[1] < 10) {
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

        $('[id="' + me.opts.graphTitleID1 + '"]').text('Apnea-Hypopnea Index');

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

            colors: ['#0000ff', '#0AFF0A', '#FFB90F', '#FF0000'],

            grid: {
                markings: [{ yaxis: { from: 10, to: 10 }, color: "#000000" }],
                labelMargin: 10,
                hoverable: true,
                clickable: true
            }

        };

        $('[id="' + me.opts.chartPlaceholder1 + '"]').html('');

        //if (greenData.length > 0) {
        //    plotData.push({ data: greenData });
        //}

        //if (yellowData.length > 0) {
        //    plotData.push({ data: yellowData });
        //}

        //if (redData.length > 0) {
        //    plotData.push({ data: redData });
        //}

        plotData.push({ label: 'Baseline AHI', data: baselineData });
        plotData.push({ label: 'Treatment AHI', data: txData });

        //var iTicks = greenData.length + redData.length;

        //if (iTicks < 10) {
        //    options.xaxis.ticks = iTicks;
        //}

        me.plot = $.plot($('[id="' + me.opts.chartPlaceholder1 + '"]'), plotData, options);
        me.plot.setupGrid();

        strValueHeader = 'AHI';

        //update tabular data
        //me.updateTabularData(me.opts.ahiData, strValueHeader);
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
        $('[id="' + me.opts.graphTitleID2 + '"]').text('Mask Leak' + leakType.value);

        //set y-axis label
        //$('[id="' + me.opts.YAxisLabel + '"]').text('Liters per Minute');

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

        };

        if (redData.length > 0) {
            options.yaxis = { min: 0, max: 120 };
        }

        $('[id="' + me.opts.chartPlaceholder2 + '"]').html('');

        if (greenData.length > 0) {
            plotData.push({ data: greenData });
        }

        if (redData.length > 0) {
            plotData.push({ label: 'Leak Out of Range', data: redData });
        }

        plotData.push({ data: plotData });

        //var iTicks = greenData.length + yellowData.length + redData.length;
        //var iTicks = txData.length;

        //if (iTicks < 10) {
        //    options.xaxis.ticks = iTicks;
        //}

        me.plot = $.plot($('[id="' + me.opts.chartPlaceholder2 + '"]'), plotData, options);
        me.plot.setupGrid();

        strValueHeader = 'Value';

        //update tabular data
        //me.updateTabularData(me.opts.leakData, strValueHeader);

    },

    // render quality of sleep graph
    renderQOSleep: function () {
        var me = this;
        $('[id="' + me.opts.graphTitleID3 + '"]').text('Quality of Sleep');
        $('[id="' + me.opts.chartPlaceholder3 + '"]').html('');
    },

    // render quality of life graph
    renderQOLife: function () {
        var me = this;
        $('[id="' + me.opts.graphTitleID3 + '"]').text('Quality of Life');
        $('[id="' + me.opts.chartPlaceholder3 + '"]').html('');
    },

    // show/hide the time range selection controls
    timewindow: function (obj) {
        var me = this,
            fromDate = $('[id$="' + me.opts.fromDateID + '"]')[0],
            toDate = $('[id$="' + me.opts.toDateID + '"]')[0];

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
        else if (me.opts.selectedGraphIndex == 3) {
            me.renderQOSleep();
        }
        else if (me.opts.selectedGraphIndex == 4) {
            me.renderQOLife();
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
            newQOLData = [];

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

        //filter QOS data by time window
        //DAVID: TODO

        //filter QOL data by time window
        //DAVID: TODO

        //render the graphs
        /*
        if (me.opts.selectedGraphIndex == 0) {
            me.renderTxAdherence();
        }
        else if (me.opts.selectedGraphIndex == 1) {
            me.renderAHI();
        }
        else if (me.opts.selectedGraphIndex == 2) {
            me.renderMaskLeak();
        }
        else if (me.opts.selectedGraphIndex == 3) {
            me.renderQOSleep();
        }
        else if (me.opts.selectedGraphIndex == 4) {
            me.renderQOLife();
        }
        */
        me.renderTxAdherence();
        me.renderAHI();
        me.renderMaskLeak();

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

    //initial functions
    init: function () {
        var me = this,
            leakType = $('[id$="' + me.opts.leakType + '"]')[0];

        me.getSeriesData();
        me.rptDatepicker();

        //for interaction with graph's points
        me.previousPoint = null;
        $.each([me.opts.chartPlaceholder0, me.opts.chartPlaceholder1, me.opts.chartPlaceholder2, me.opts.chartPlaceholder3], function (i, t) {

            $('[id$="' + t + '"]').bind("plothover", function (event, pos, item) {
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

                            if (i == 0) {
                                strTooltipText = '[' + strDate + '] connected time ' + y + ' hrs.';
                            }
                            else if (i == 1) {
                                strTooltipText = '[' + strDate + '] AHI: ' + y.toFixed(4);
                            }
                            else if (i == 2) {
                                strTooltipText = '[' + strDate + '] Mask Leak' + leakType.value + ' : ' + y.toFixed(4);
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
        });


        /*
        $('[id$="' + me.opts.chartPlaceholder + '"]').bind("plotclick", function (event, pos, item) {
            if (item) {
                me.plot.highlight(item.series, item.datapoint);
            }
        });
        */
    }
};