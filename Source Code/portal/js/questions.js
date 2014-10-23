var questions = {
    //general settings and properties
    opts: {
        validation: {
            dropdown: {
                minIndex: 0
            }
        }
    },

    //bind Type99 action
    bindType99Actions: function () {

        var _me = this;

        $('input[clearabove]').each(function (i, o) {
            var patt = /TID\d+_QID\d+/g,
                trID = patt.exec(o.name);

            //$('td', $('tr[id="' + trID + '"]')).each(function () {
            $('td', $(o).parent().parent().parent('tr')).each(function () {
                $('input', this).each(function () {
                    if (this.getAttribute('type') == 'radio' || this.getAttribute('type') == 'checkbox') {

                        var _onclick = '';
                        if (this.hasAttribute('onclick')) {
                            _onclick = this.getAttribute('onclick') + ' ';
                        }
                        this.setAttribute('onclick', _onclick + 'questions.checkClearAbove(this);');

                    } else if (this.getAttribute('type') == 'text') {
                        var _onkeyup = '';
                        if (this.hasAttribute('onkeyup')) {
                            _onkeyup = this.getAttribute('onkeyup') + ' ';
                        }
                        if (_onkeyup.indexOf('questions.checkClearAbove(this)') < 0) {
                            this.setAttribute('onkeyup', _onkeyup + 'questions.checkClearAbove(this);');
                        }
                    }
                });
            });
        });
    },

    //perform Type99 actions
    checkClearAbove: function (obj) {
        var _me = this,
            patt = /TID\d+_QID\d+/g,
            _tr = $(obj).parent().parent().parent('tr'),
            _name = patt.exec(_tr.id),
            _obj99 = $('input[clearabove]', $(_tr)),

            _chks = $('input[type="checkbox"]', $(_tr)).not('[clearabove]'),
            _radios = $('input[type="radio"]', $(_tr)).not('[clearabove]'),
            _texts = $('input[type="text"]', $(_tr)),
            _dropdowns = $('select', $(_tr));

        //if obj99 clicked
        $.each(_obj99, function (i, ctrl) {
            if (obj.id == ctrl.id) {
                if (obj.checked) {

                    //clear radios and checkboxes
                    $(_chks, _radios).each(function () {
                        this.checked = false;
                    });

                    //clear textboxes
                    $(_texts).val('');

                    //clear dropdowns
                    $(_dropdowns).each(function () {
                        this.selectedIndex = -1;
                    });
                }
            }


            if (obj.tagName == "INPUT") {
                if (obj.getAttribute('type') == 'text') {
                    if (obj.value.length > 0) {
                        ctrl.checked = false;
                    }
                } else if (obj.id != ctrl.id && (obj.getAttribute('type') == 'radio' || obj.getAttribute('type') == 'checkbox')) {
                    ctrl.checked = false;
                }
            }

            if (obj.tagName == "SELECT") {
                if (obj.selectedIndex > -1) {
                    ctrl.checked = false;
                }
            }

        });


    },

    //build text response -prefix RID-
    buildTextResponses: function () {

        //clear leftovers
        $('input[type="text"][name^="grpText_"]').each(function () {
            this.value = '';
        });

        //build the text responses
        $('input[type="text"][name^="grpCtrlText_"]').each(function () {
            var rid = this.id.replace(/\D/gi, ''),
                txtResp = $('input[type="hidden"][id="rid_' + rid + '"]')[0],
                respValue = this.value;

            //replace ',' in the response's text
            this.value = respValue.replace(/\,/gi, '^')

            //build the response pre-fixing the rid
            txtResp.value = rid + '|0|' + this.value;
        });

        return true;
    },

    //bind check skip patterns to answers
    bindCheckSkipPatterns: function () {
        var _me = this;

        if (typeof (_me.opts.skipPatterns) != "undefined") {
            $.each(_me.opts.skipPatterns, function (i, o) {
                if (!isNaN(o.rid)) {
                    var _obj = $('[id="rid_' + o.rid + '"]')[0],
                    patt = /TID\d+_QID\d+/gi,
                    _name = patt.exec(_obj.name),
                    _tr = $('tr', $('div[id$="divQuestions"]')).has('input[id*="rid_' + o.rid + '"]');

                    $('input[type="radio"], input[type="checkbox"]', $(_tr)).each(function () {
                        $(this).attr('onclick', 'questions.checkSkipPatterns();');
                    });
                }
                else {
                    var rids = o.rid.split('|');
                    $.each(rids, function (a, b) {
                        var _obj = $('[id="rid_' + b + '"]')[0],
                            patt = /TID\d+_QID\d+/gi,
                            _name = patt.exec(_obj.name),
                            _tr = $('tr', $('div[id$="divQuestions"]')).has('input[id*="rid_' + b + '"]');

                        $('input[type="radio"], input[type="checkbox"]', $(_tr)).each(function () {
                            $(this).attr('onclick', 'questions.checkSkipPatterns();');
                        });
                    });
                }
            });
        }
    },

    //check skip patterns
    checkSkipPatterns: function () {
        var _me = this;

        //return if there is not a pattern defined
        if (typeof (_me.opts.skipPatterns) != "undefined") {
            $.each(_me.opts.skipPatterns, function (i, o) {
                if (isNaN(o.rid)) {
                    _me.multipleRIDSkipPattern(o);
                }
                else {
                    _me.simpleSkipPattern(o);
                }
            });
        }
        return;
    },

    //single RID skip pattern
    simpleSkipPattern: function (o) {
        var _me = this,
            _checks = $('input[type="checkbox"]', $('div[id$="divQuestions"]')),
            _radios = $('input[type="radio"]', $('div[id$="divQuestions"]')),
            mObj = $('[id="rid_' + o.rid + '"]')[0],
            bIgnoreRule = false;

        if (typeof (o.checked.ignore_when) != "undefined") {
            var ignore_obj = $('[id$="rid_' + o.checked.ignore_when.rid + '"]')[0];
            if (ignore_obj) {
                bIgnoreRule = (ignore_obj.checked);
            }
        }

        if (mObj.getAttribute('type') == "radio" || mObj.getAttribute('type') == "checkbox") {
            //---------------
            if (mObj.checked) {

                if (typeof (o.checked.show) != "undefined") {
                    $.each(o.checked.show, function (n, v) {
                        if (!bIgnoreRule) {
                            $('tr[id="' + v + '"]').show().removeAttr('skipped');
                        }
                    });
                }

                if (typeof (o.checked.hide) != "undefined") {
                    $.each(o.checked.hide, function (n, v) {
                        if (!bIgnoreRule) {
                            _me.clearQuestionControls(v);
                            $('tr[id="' + v + '"]').hide().attr('skipped', 'skipped');
                        }
                    });
                }

            } else {

                if (typeof (o.checked.show) != "undefined") {
                    $.each(o.checked.show, function (n, v) {
                        if (!bIgnoreRule) {
                            _me.clearQuestionControls(v);
                            $('tr[id="' + v + '"]').hide().attr('skipped', 'skipped');
                        }
                    });
                }

                if (typeof (o.checked.hide) != "undefined") {
                    $.each(o.checked.hide, function (n, v) {
                        if (!bIgnoreRule) {
                            $('tr[id="' + v + '"]').show().removeAttr('skipped');
                        }
                    });
                }

            }
            //---------------
        }
    },

    multipleRIDSkipPattern: function (o) {
        var _me = this,
            _checks = $('input[type="checkbox"]', $('div[id$="divQuestions"]')),
            _radios = $('input[type="radio"]', $('div[id$="divQuestions"]')),
            objs = [],
            bGrpChecked = false,
            bIgnoreRule = false,
            nCount = 0;

        //mObj = $('[id="rid_' + o.rid + '"]')[0]
        var rids = o.rid.split('|');
        $.each(rids, function (x, y) {
            var ele = $('[id="rid_' + y + '"]')[0];
            if (ele) {
                objs.push(ele);
            }
        });

        $.each(objs, function () {
            if (this.checked) nCount = nCount + 1;
        });

        bGrpChecked = (nCount > 0);

        if (typeof (o.checked.ignore_when) != "undefined") {
            var ignore_obj = $('[id$="rid_' + o.checked.ignore_when.rid + '"]')[0];
            if (ignore_obj) {
                bIgnoreRule = (ignore_obj.checked);
            }
        }

        $.each(objs, function (n, mObj) {
            if (mObj.getAttribute('type') == "radio" || mObj.getAttribute('type') == "checkbox") {
                //---------------
                if (mObj.checked || bGrpChecked) {

                    if (typeof (o.checked.show) != "undefined") {
                        $.each(o.checked.show, function (n, v) {
                            if (!bIgnoreRule) {
                                $('tr[id="' + v + '"]').show().removeAttr('skipped');
                            }
                        });
                    }

                    if (typeof (o.checked.hide) != "undefined") {
                        $.each(o.checked.hide, function (n, v) {
                            if (!bIgnoreRule) {
                                _me.clearQuestionControls(v);
                                $('tr[id="' + v + '"]').hide().attr('skipped', 'skipped');
                            }
                        });
                    }

                } else if (!mObj.checked || !bGrpChecked) {

                    if (typeof (o.checked.show) != "undefined") {
                        $.each(o.checked.show, function (n, v) {
                            if (!bIgnoreRule) {
                                _me.clearQuestionControls(v);
                                $('tr[id="' + v + '"]').hide().attr('skipped', 'skipped');
                            }
                        });
                    }

                    if (typeof (o.checked.hide) != "undefined") {
                        $.each(o.checked.hide, function (n, v) {
                            if (!bIgnoreRule) {
                                $('tr[id="' + v + '"]').show().removeAttr('skipped');
                            }
                        });
                    }

                }
                //---------------
            }
        });

    },

    //clear all question controls
    clearQuestionControls: function (qID) {
        var _me = this,
            tr = $('tr[id$="' + qID + '"]')[0];

        //clear radios and checkboxes
        $('input[type="radio"], input[type="checkbox"]', tr).removeAttr('checked');

        //clear textboxes
        $('input[type="text"]', tr).val('');

        //clear dropdowns
        $('select', tr).each(function () {
            this.selectedIndex = -1;
        });
    },

    //check that all non-skipped questions were answered
    checkRequiredAnswers: function () {
        var _me = this,
            _allTRs = $('tr[id^="TID"]'),
            _trs = $('tr[id^="TID"]').not('[skipped]'),
            _validate = true;

        //clear previous error messages
        $('td', _allTRs).css({
            'background-color': '#fff'
        });
        $('div.err-caption', _allTRs).remove();

        $.each(_trs, function (i, t) {
            var ansCount = 0;

            $('input[type="radio"], input[type="checkbox"]', t).each(function () {
                if (this.checked) {
                    ansCount = ansCount + 1;
                }
            });

            $('input[type="text"]', t).each(function () {
                if (this.value.length > 0) {
                    ansCount = ansCount + 1;
                }
            });

            $('select', t).each(function () {

                if (this.selectedIndex >= _me.opts.validation.dropdown.minIndex) {
                    ansCount = ansCount + 1;
                }

                //conditional added to support SAQLI rank dropdowns (if there are more than 5 symptoms)
                /*
				if (this.getAttribute('skip') != null)
                {
                    ansCount = ansCount + 1;
                }
				*/
            });

            //if count of responses < 1 then display error message
            if (ansCount < 1) {
                if (!t.getAttribute("bypass"))
                {
                    _validate = false;

                    $('td:first', t).each(function (z, x) {
                        $(x).prepend('<div class="err-caption"><span style="color:Red;">A response for this question is required!</span></div>');
                        $('td', $(x).parent('tr')).css({
                            'background-color': '#F9FC9D'
                        });
                    });
                }
            }

        });

        return _validate;
    },

    //validate responses
    validateResponses: function () {
        var _me = this,
            _validateRequired = true,
            _validateRules = true;

        //added to support saqli symptoms rank dropdowns
        saqli.skipRank();

        //first, build the text responses
        //_me.buildTextResponses();

        _validateRequired = _me.checkRequiredAnswers();

        if (typeof (_me.opts.validationRules) != "undefined") {
            //
        }

        if (!(_validateRequired && _validateRules)) {
            var _pos = $('div.err-caption').eq(0).position();
            window.scrollTo(0, _pos.top);
            alert('Please review the responses for the highlighted questions!');
        }

        return (_validateRequired && _validateRules);

    },

    //apply masks to textboxes
    applyMask: function () {
        var _me = this;

        if (typeof (_me.opts.txtMasks) != "undefined") {
            $.each(_me.opts.txtMasks, function (i, o) {
                var _txt = $('input[type="text"][id="rid_txt_' + o.rid + '"]')[0];

                if (typeof (o.maxlength) != "undefined") {
                    $(_txt).attr('maxlength', o.maxlength);
                }

                if (typeof (o.mask) != "undefined") {
                    if (o.mask.toLowerCase() == "numbersonly") {
                        var _onkeyup = $(_txt).attr('onkeyup');
                        $(_txt).attr('onkeyup', 'questions.mask.numbersOnly(this); ' + _onkeyup);
                    }
                }

                if (typeof (o.mask) != "undefined") {
                    if (o.mask.toLowerCase() == "hhmm24h") {
                        var _onkeyup = $(_txt).attr('onkeyup');
                        $(_txt).attr('onkeyup', 'questions.mask.HHmm24h(this); ' + _onkeyup);
                    }
                }

            });
        }
    },

    //MASKS
    mask: {
        numbersOnly: function (obj) {
            var p = questions,
            me = this,
            id = obj.id.replace(/\D/gi, ''),
            m = null,
            dec = false,
            reNum = /[^0-9.]/gi;
            if (typeof (p.opts.txtMasks) != "undefined") {
                m = p.opts.txtMasks;
                $.each(m, function (i, o) {
                    if (o.rid == id) {
                        if (typeof (o.allowDecimal) != "undefined") {
                            dec = o.allowDecimal;
                            if (o.allowDecimal) {
                                reNum = /[^0-9.]/gi;
                            }
                            else {
                                reNum = /\D/gi;
                            }
                        }
                    }
                });
            }
            obj.value = obj.value.replace(reNum, '');

            if (dec) {
                var val = obj.value,
                    val2 = val.replace(/[^.]/gi, '');
                if (val2.length > 1) {
                    obj.value = val.substr(0, val.length - 1);
                }
            }
        },

        //apply HHmm24h
        HHmm24h: function (obj) {
            var me = this,
				test = [
					'(\\d)',
					'([01]\\d|2[0-3])|([1-9]:)',
					'([01]\\d:|2[0-3]:)|([1-9]:[0-5])',
					'(([01]\\d|2[0-3]):([0-5]))|([1-9]:[0-5]\\d)',
					'(([01]\\d|2[0-3]):([0-5]\\d))'
				];

            if (obj.value.length > test.length) {
                obj.value = obj.value.substr(0, obj.value.length - 1);
                return false;
            }
            else if (obj.value.length == 0) {
                return true;
            }
            else {
                var reTest = new RegExp(test[obj.value.length - 1]);
                if (reTest.test(obj.value)) {
                    return true;
                }
                else {
                    obj.value = obj.value.substr(0, obj.value.length - 1);
                    return false;
                }
            }
        }
    },

    //initializing functions
    init: function () {
        var _me = this;
        _me.bindCheckSkipPatterns();
        _me.bindType99Actions();
        _me.checkSkipPatterns();
        _me.applyMask();
    }
};

// *********************************************************************************
// js object for the SAQLI questionnaire
var saqli = {
    opts: {
        ddPrefix: 'grpCombo_',
        ddIds: function () {
            var me = this,
                ids = [],
                id1 = [],
                id2 = [];
            for (a = 22; a < 43; a++) {
                var strID = me.ddPrefix + a;
                id1.push(strID);
            }

            for (a = 90; a < 116; a++) {
                var strID = me.ddPrefix + a;
                id2.push(strID);
            }

            ids.push(id1);
            ids.push(id2);

            return ids;
        },
        rankVaues: []
    },

    bindOnChangeEvt: function () {
        var me = this,
            ids = me.opts.ddIds();

        $.each(ids[0], function (i, s) {
            $('select[name$="' + s + '"]').bind({
                change: function () {
                    me.changedRank(this, 0);
                }
            });
        });

        $.each(ids[1], function (i, s) {
            $('select[name$="' + s + '"]').bind({
                change: function () {
                    me.changedRank(this, 1);
                }
            });
        });
    },

    changedRank: function (obj, grp) {
        var me = this,
            cbo = [],
            selValues = [],
            ids = me.opts.ddIds();

        $.each(ids[grp], function (i, s) {
            $('select[name$="' + s + '"]').each(function (i, o) {
                if (o != obj) {
                    cbo.push(o);
                }
            });
        });

        //get list of currently selected values
        $.each(cbo, function (i, c) {
            $('option:selected', c).each(function (n, op) {
                if (c.value != '') {
                    var m_val = c.value.split('|')[0];
                    selValues.push(m_val);
                }
            });

            //first remove "disabled"attribute from all options
            $('option', c).each(function (a, b) {
                $(b).removeAttr('disabled');
            });
        });

        if (obj.value != '') {
            selValues.push(obj.value.split('|')[0]);
        }

        //loop through the dropdowns
        $.each(cbo, function (i, c) {
            $('option:not(:selected)', c).each(function (n, op) {
                if (op.value != '') {

                    var o_val = op.value.split('|')[0];

                    $.each(selValues, function (a, v) {
                        if (o_val == v) {
                            $(op).attr('disabled', 'disabled');
                        }
                    });
                }
            });
        });

        questions.checkSkipPatterns();
        me.defaultComboSelection();
    },

    defaultComboSelection: function (objs) {
        var me = this,
            cbo = [];

        $.each(me.opts.ddIds(), function (i, s) {
            $('select[name$="' + s + '"]').each(function (i, o) {
                cbo.push(o);
            });
        });

        $.each(cbo, function (a, b) {
            if (b.selectedIndex < 0) {
                b.selectedIndex = 0;
            }
        });
    },

    init: function () {
        var me = this;
        me.bindOnChangeEvt();
    }
};

// -----------------------------------
// enhance to saqli to render sliders

if (typeof (saqli) == "undefined") {
    var saqli = {};
}

saqli.renderSliders = function (args) {
    $.each(args, function (i, v) {
        var select = $('select[id$="' + v + '"]')[0],
            slider = $('div[id$="div_' + v + '"]').slider({
                min: 0,
                max: 10,
                range: "min",
                value: (function () {
                    if (select.selectedIndex < 1) {
                        return 0;
                    } else {
                        return select.selectedIndex - 1;
                    }
                })(),
                slide: function (event, ui) {
                    $('option', select).each(function (i, o) {
                        var optVal = o.value.split('|')[0];
                        if (optVal == ui.value) {
                            o.selected = true;
                        }
                    });
                    $('.ui-slider-range').css({
                        margin: '0'
                    });
                }
            });

        $(select).change(function () {
            var optVal = 0;
            if (this.value != "") {
                optVal = this.value.split('|')[0];
            }
            slider.slider("value", optVal);
            $('.ui-slider-range').css({
                margin: '0'
            });
        });

        $('.ui-slider-range').css({
            margin: '0'
        });
        select.selectedIndex = -1;
    });
};

saqli.skipRank = function () {
    var me = this,
        ids = me.opts.ddIds(),
        _trs = $('tr[id^="TID"]').not('[skipped]'),
        _dd = [],
        _skip = [];

    $('[SKIP="SKIP"]').removeAttr('SKIP');

    for (var n = 0; n < 2; n++) {
        var grp = [];
        $.each(ids[n], function (a, b) {
            $.each(_trs, function (index, element) {
                var dd = $('[name$="' + b + '"]', $(element))[0];
                if (dd) {
                    grp.push(dd);
                }
            });
        });
        _dd.push(grp);
    }

    for (var n = 0; n < 2; n++) {
        var ddCount = _dd[n].length,
            ddSelected = [],
            ddBlank = [];
        $.each(_dd[n], function (i, o) {
            if (o.selectedIndex > 0) {
                ddSelected.push(o);
            }
            else {
                ddBlank.push(o);
            }
        });

        if (ddCount > ddSelected.length) {
            if (ddSelected.length >= 5) {
                $.each(ddBlank, function (a, b) {
                    _skip.push(b);
                });
            }
        }
    }

    $(_skip).attr('SKIP', 'SKIP');

    //return (_skip.length > 0) ? _skip : null;
};

