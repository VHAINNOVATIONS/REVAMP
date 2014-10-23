if (typeof (cms) == "undefined") {
    var cms = {};
}

cms.video = {
    //general settings and options
    opts: {
        videoElementID: 'revamp_video',
        videos: [
            { video_src: 'sleep_study_setup.mp4', event_id: 3 },
            { video_src: 'what_is_sleep_apnea.mp4', event_id: 4 }
        ],
        _video: null,
        htxtEventID: 'htxtEventID',
        htxtOPT0: 'htxtOPT0',
        videoDurationPercent: .99
    },

    //check video properties/status
    checkVideo: function () {

        var me = this,
            htxtEventID = $('input[id$="' + me.opts.htxtEventID + '"]')[0],
            htxtOPT0 = $('input[id$="' + me.opts.htxtOPT0 + '"]')[0],
            _video = $('[id$="' + me.opts.videoElementID + '"]')[0];

        if (_video && htxtOPT0.value.length > 1) {

            me.opts._video = _video;

            //set the event id that correspond to the displayed video
            var reSrc = new RegExp(_video.currentSrc, 'gi');

            $.each(me.opts.videos, function (i, o) {
                if (reSrc.test(o.video_src))
                {
                    htxtEventID.value = o.event_id;
                }
            });

            //pass the video element to the global scope
            window.revamp_video = _video;

            window.video_interval = setInterval(checkVideoStatus, 500);

        }

    },

    //trigger the video event
    triggerVideoEvent: function () {
        var me = this,
            htxtEventID = $('input[id$="' + me.opts.htxtEventID + '"]')[0];

        $.ajax({
            url: 'cms_contents.aspx',
            data: { opt1: htxtEventID.value },
            success: function (data) {
                return;
            }
        });
    },

    //initializing functions
    init: function () {
        var me = this;
        $(document).ready(function () {
            me.checkVideo();
        });
    }
}

var video_event_flag = false,

    checkVideoStatus = function () {

    var _video = $('[id$="' + cms.video.opts.videoElementID + '"]')[0];

    if (!_video.paused) {
        if ((_video.ended > 0 || (_video.currentTime / _video.duration) > cms.video.opts.videoDurationPercent) && !video_event_flag) {

            video_event_flag = true;
            window.clearInterval(video_interval);

            cms.video.triggerVideoEvent();

        }
    }
};