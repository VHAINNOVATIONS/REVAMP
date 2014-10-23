if(typeof(cms) == "undefined"){
    var cms = {};
}

cms.education = {
    
    //general options and settings
    opts: {
        htxtEduID: 'htxtEduID',
    },

    //select education page
    selectPage: function (pageID) {
        var me = this,
            htxtEduID = $('input[id$="' + me.opts.htxtEduID + '"]')[0];

        $(htxtEduID).val(pageID);

        $('div[id$="divEduInfo"]').hide();

        if (typeof (cms.video) != "undefined") {
            cms.video.opts.autoplay = true;
        }

        __doPostBack(me.opts.contentsPanelID,'');
    },

    //initializing functions
    init: function () {
        var me = this;
    }
};