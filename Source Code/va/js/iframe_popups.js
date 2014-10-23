// ----------------------------------------
// REPORT LIST ARRAY

var report_list = [{
    "name": "patAssessmentsRpt",
    "url": "rpt_assessments.aspx",
    "title": "Assessments Report"
}, {
    "name": "aggregatesettings",
    "url": "aggregate_settings.aspx",
    "title": "Aggregate Reports / Dashboard Settings"
}, {
    "name": "messagescenter",
    "url": "message_center.aspx",
    "title": "Messages Center"
}];

// ----------------------------------------
(function()
{
    var iframepopup = function()
    {

        var self = this;

        this.opts = {
            hidden: true,
            modal: true,
            collapsible: false,
            maximizable: false,
            width: 850,
            height: 600,
            renderTo: '#aspnetForm'
        };

        this.winpopup;
        this.rptargs;

        this.renderWindow = function(rpt)
        {
            var m_rpturl = rpt.url;
            if (typeof self.rptargs[1] == "object")
            {
                //build a query string to append to the url if 
                //more parameters were passed when calling the renderer function

                var qs = '?';
                for (var a = 0; a < self.rptargs[1].length; a++)
                {
                    qs += 'op' + a + '=' + self.rptargs[1][a] + '&';
                }
                qs = qs.substr(0, qs.length - 1);
                m_rpturl = m_rpturl + qs;
            }

            //render the report popup 
            self.winpopup = new Ext.Window({
                hidden: true,
                title: rpt.title,
                id: 'popupreport',
                modal: self.opts.modal,
                collapsible: self.opts.collapsible,
                maximizable: self.opts.maximizable,
                height: self.opts.height,
                width: self.opts.width,
                closeAction: 'destroy',
                renderTo: Ext.get('divPPRpts'),
                layout: 'anchor',
                listeners: {
                    hide: {
                        fn: function(item)
                        {
                            $(window).trigger('resize');
                            return true;
                        }
                    }
                },
                items: [{
                    id: 'iframe-report',
                    xtype: 'component',
                    autoEl: {
                        tag: 'iframe',
                        src: m_rpturl,
                        width: '100%',
                        height: '100%'
                    }
}]
                }).show();


            $(document).ready(function () {
                setTimeout(function () {
                    wp.adjustMain();
                }, 1);
            });

                return self.winpopup;
            };

            this.getReportList = function()
            {
                if (self.opts.reportList)
                {
                    var m_list = self.opts.reportList;

                    for (var i = 0, v; (v = self.opts.reportList[i]) != null; i++)
                    {
                        for (var k in v)
                        {
                            console.log(k + ' : ' + v[k]);
                        }
                    }
                }
            };

            this.getReportParameters = function(args)
            {
                if (self.opts.reportList)
                {
                    var rpt = null;
                    var m_list = self.opts.reportList;
                    $.each(m_list, function(i, o)
                    {
                        if (o.name == args)
                        {
                            rpt = o;
                        }
                    });
                    return rpt;
                }
                else
                {
                    return null;
                }
            };

            this.showReport = function()
            {
                var args = arguments[0];
                self.rptargs = arguments;

                if (typeof (arguments[2]) != "undefined") {
                    $.extend(self.opts, arguments[2]);
                }

                if (self.getReportParameters(args))
                {
                    return self.renderWindow(self.getReportParameters(args));
                }
                else
                {
                    return false;
                }
            };
        };


        window.iframepopup = iframepopup;
    })(window);

winrpt = new iframepopup();
winrpt.opts.reportList = report_list;