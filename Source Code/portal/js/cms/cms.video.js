if (typeof (cms) == "undefined") {
    var cms = {};
}

cms.video = {
    //general settings and options
    opts: {
        videoElementID: 'revamp_video',
        videos: [
            { video_src: 'sleep_study_setup.mp4', event_id: 4 },
            { video_src: 'what_is_sleep_apnea.mp4', event_id: 3 },
			{ video_src: 'Nox_T3_Tutorial_video_by_CS.m4v', event_id: 4 }
        ],
        _video: null,
        htxtEventID: 'htxtEventID',
        htxtOPT0: 'htxtOPT0',
        videoDurationPercent: .80,
        autoplay: true
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
            var strVideoSrc = $('source', $(_video)).attr('src');

            $.each(me.opts.videos, function (i, o) {
                if (strVideoSrc.indexOf(o.video_src) > -1)
                {
                    htxtEventID.value = o.event_id;
                }
            });

            //pass the video element to the global scope
            window.revamp_video = _video;

            window.video_interval = setInterval(checkVideoStatus, 500);
        }

        if (me.opts.autoplay == false)
        {
            $(document).ready(function () {

                window.mcheck = setInterval(function () {
                    if (!window.revamp_video.paused) {

                        window.revamp_video.pause();
                        window.clearInterval(window.mcheck);
                    }
                }, 500);

            });
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
                if (typeof (me.opts.upCheckVideosStatus) != "undefined") {
                    //__doPostBack(me.opts.upCheckVideosStatus, '');
                }
                else {
                    return;
                }
            }
        });
    },

    //initializing functions
    init: function () {
        var me = this;
        $(document).ready(function () {
            window.video_event_flag = false;
            me.checkVideo();
        });
    }
}

    window.video_event_flag = false,

    checkVideoStatus = function () {

    var _video = $('[id$="' + cms.video.opts.videoElementID + '"]')[0];

    if (!_video.paused) {
        if ((_video.ended > 0 || (_video.currentTime / _video.duration) > cms.video.opts.videoDurationPercent) && !video_event_flag) {

            video_event_flag = true;
            window.clearInterval(video_interval);
            cms.video.opts.autoplay = false;

            cms.video.triggerVideoEvent();
        }
    }
};