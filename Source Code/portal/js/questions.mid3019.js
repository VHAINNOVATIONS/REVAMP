questions.checkSkipPatterns = function () {
            var _me = this,
                _checks = $('input[type="checkbox"]', $('div[id$="divQuestions"]')),
                _radios = $('input[type="radio"]', $('div[id$="divQuestions"]'));

            //return if there is not a pattern defined
            if (typeof (_me.opts.skipPatterns) != "undefined") {
                $.each(_me.opts.skipPatterns, function (i, o) {
                    var mObj = $('[id="rid_' + o.rid + '"]')[0];
                    if (mObj.getAttribute('type') == "radio" || mObj.getAttribute('type') == "checkbox") {
                        if (mObj.checked) {

                            if (typeof (o.checked.show) != "undefined") {
                                $.each(o.checked.show, function (n, v) {
                                    $('tr[id="' + v + '"]').show().removeAttr('skipped');
                                });
                            }

                            if (typeof (o.checked.hide) != "undefined") {
                                $.each(o.checked.hide, function (n, v) {
                                    _me.clearQuestionControls(v);
                                    $('tr[id="' + v + '"]').hide().attr('skipped', 'skipped');
                                });
                            }
                        }
                        else {
                            if (typeof (o.checked.show) != "undefined") {
                                $.each(o.checked.show, function (n, v) {
                                    _me.clearQuestionControls(v);
                                    $('tr[id="' + v + '"]').hide().attr('skipped', 'skipped');
                                });
                            }

                            if (typeof (o.checked.hide) != "undefined") {
                                $.each(o.checked.hide, function (n, v) {
                                    $('tr[id="' + v + '"]').show().removeAttr('skipped');
                                });
                            }
                        }
                    }

                    if (mObj.tagName.toLowerCase() == "select") {
                        if (mObj.selectedIndex > 0) {

                            if (typeof (o.checked.show) != "undefined") {
                                $.each(o.checked.show, function (n, v) {
                                    $('tr[id="' + v + '"]').show().removeAttr('skipped');
                                });
                            }

                            if (typeof (o.checked.hide) != "undefined") {
                                $.each(o.checked.hide, function (n, v) {
                                    _me.clearQuestionControls(v);
                                    $('tr[id="' + v + '"]').hide().attr('skipped', 'skipped');
                                });
                            }
                        }
                        else {
                            if (typeof (o.checked.show) != "undefined") {
                                $.each(o.checked.show, function (n, v) {
                                    _me.clearQuestionControls(v);
                                    $('tr[id="' + v + '"]').hide().attr('skipped', 'skipped');
                                });
                            }

                            if (typeof (o.checked.hide) != "undefined") {
                                $.each(o.checked.hide, function (n, v) {
                                    $('tr[id="' + v + '"]').show().removeAttr('skipped');
                                });
                            }
                        }
                    }
                });
            }

            return;
        };

questions.checkRequiredAnswers = function () {
            var _me = this,
                _allTRs = $('tr[id^="TID3"]'),
                _trs = $('tr[id^="TID3"]').not('[skipped]'),
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
                    if (this.selectedIndex > 0) {
                        ansCount = ansCount + 1;
                    }
                });

                //if count of responses < 1 then display error message
                if (ansCount < 1) {

                    _validate = false;

                    $('td:first', t).each(function (z, x) {
                        $(x).prepend('<div class="err-caption"><span style="color:Red;">A response for this question is required!</span></div>');
                        $('td', $(x).parent('tr')).css({
                            'background-color': '#F9FC9D'
                        });
                    });
                }
            });

            return _validate;
        };
        
// ***********************************************************

saqli.opts.skipToTx = false;

saqli.opts.responses_map1 = [{
								rid: 2002,
								tr: 'TID1_QID1'
							}, {
								rid: 2004,
								tr: 'TID1_QID2'
							}, {
								rid: 2006,
								tr: 'TID1_QID3'
							}, {
								rid: 2008,
								tr: 'TID1_QID4'
							}, {
								rid: 2010,
								tr: 'TID1_QID5'
							}, {
								rid: 2012,
								tr: 'TID1_QID6'
							}, {
								rid: 2014,
								tr: 'TID1_QID7'
							}, {
								rid: 2016,
								tr: 'TID1_QID8'
							}, {
								rid: 2018,
								tr: 'TID1_QID9'
							}, {
								rid: 2020,
								tr: 'TID1_QID10'
							}, {
								rid: 2022,
								tr: 'TID1_QID11'
							}, {
								rid: 2024,
								tr: 'TID1_QID12'
							}, {
								rid: 2026,
								tr: 'TID1_QID13'
							}, {
								rid: 2028,
								tr: 'TID1_QID14'
							}, {
								rid: 2030,
								tr: 'TID1_QID15'
							}, {
								rid: 2032,
								tr: 'TID1_QID16'
							}, {
								rid: 2034,
								tr: 'TID1_QID17'
							}, {
								rid: 2036,
								tr: 'TID1_QID18'
							}, {
								rid: 2038,
								tr: 'TID1_QID19'
							}, {
								rid: 2040,
								tr: 'TID1_QID20'
							}, {
								rid: 2042,
								tr: 'TID1_QID21'
							}];
							
saqli.opts.responses_map2 = [{
								rid: 6002,
								tr: 'TID5_QID1'
							}, {
								rid: 6004,
								tr: 'TID5_QID2'
							}, {
								rid: 6006,
								tr: 'TID5_QID3'
							}, {
								rid: 6008,
								tr: 'TID5_QID4'
							}, {
								rid: 6010,
								tr: 'TID5_QID5'
							}, {
								rid: 6012,
								tr: 'TID5_QID6'
							}, {
								rid: 6014,
								tr: 'TID5_QID7'
							}, {
								rid: 6016,
								tr: 'TID5_QID8'
							}, {
								rid: 6018,
								tr: 'TID5_QID9'
							}, {
								rid: 6020,
								tr: 'TID5_QID10'
							}, {
								rid: 6022,
								tr: 'TID5_QID11'
							}, {
								rid: 6024,
								tr: 'TID5_QID12'
							}, {
								rid: 6026,
								tr: 'TID5_QID13'
							}, {
								rid: 6028,
								tr: 'TID5_QID14'
							}, {
								rid: 6030,
								tr: 'TID5_QID15'
							}, {
								rid: 6032,
								tr: 'TID5_QID16'
							}, {
								rid: 6034,
								tr: 'TID5_QID17'
							}, {
								rid: 6036,
								tr: 'TID5_QID18'
							}, {
								rid: 6038,
								tr: 'TID5_QID19'
							}, {
								rid: 6040,
								tr: 'TID5_QID20'
							}, {
								rid: 6042,
								tr: 'TID5_QID21'
							}, {
								rid: 6044,
								tr: 'TID5_QID22'
							}, {
								rid: 6046,
								tr: 'TID5_QID23'
							}, {
								rid: 6048,
								tr: 'TID5_QID24'
							}, {
								rid: 6050,
								tr: 'TID5_QID25'
							}, {
								rid: 6052,
								tr: 'TID5_QID26'
							}];							

saqli.goBack = function(nStage){
	
	var me = this;
	
	$('div[id$="divStage'+ (nStage + 1) +'"]').hide();
	$('div[id$="divStage'+ nStage +'"]').show();
	
	if(nStage == 1 && me.opts.skipToTx == true){
		$('div[id$="divStage4"]').hide();
	}
	
	return true;
};

saqli.gotoStage2 = function(){

	var me = this,
	    txSymptoms = 0;
	
	if(saqli.validateStage1())
	{
		$.each(saqli.opts.responses, function(a, b){
			if(parseInt(b.rid) >= 6000 && parseInt(b.rid) <= 6999){
				txSymptoms = txSymptoms + 1;
			}
		});
		
		if(txSymptoms > 0){
			$('[id$="btnBack4"]').attr('onclick','saqli.goBack(1);');
			me.opts.skipToTx = true;
			me.gotoStage4();
			return;
		}
		
		me.checkStage2opts();
		
		$('div[id$="divStage1"]').hide();
		$('div[id$="divStage2"]').show();
		return true;
	}
	
	return false;
};

saqli.gotoStage3 = function(){
	
	var me = this;
	
	if(saqli.validateStage2())
	{
		me.checkStage3opts();
		me.checkMaxFive();
		
		$('div[id$="divStage2"]').hide();
		$('div[id$="divStage3"]').show();
		return true;
	}
	
	return false;
};

saqli.gotoStage4 = function(){

	var me = this;
	
	if(saqli.validateStage3())
	{
		me.checkStage4opts();
		
		$('div[id$="divStage3"]').hide();
		$('div[id$="divStage4"]').show();
		
		if(me.opts.skipToTx == true){
			$('div[id$="divStage1"]').hide();
		}
		
		return true;
	}
	
	return false;
};

saqli.validateStage1 = function(){
	var me = this,
	    _tr = $('tr', $('table[id$="tblTID1"]')).not('[skipped]'),
	    emptyResp = 0,
	    sliderTR = $('tr[id$="TID2_QID1"]')[0];
	    
	    _tr.push(sliderTR);
	 
	//remove previous validation errors
	$.each(_tr, function(a, b){
		$('.err-caption', $(b)).remove();
		$(b).css({'background-color': '#FFFFFF'});
		$('td:first', $(b)).css({'background-color': '#FFFFFF'});
	});
	
	$('td', $(sliderTR)).css({'background-color': '#FFFFFF'});
	    
	$.each(_tr, function(a, b){
		var sel = $('select', $(b))[0];
		if(sel.selectedIndex < 1){
			emptyResp = emptyResp + 1;
						
			$('td:first', $(b)).each(function (z, x) {
				$(x).prepend('<div class="err-caption"><span style="color:Red;">A response for this question is required!</span></div>');
				$('td', $(x).parent('tr')).css({
					'background-color': '#F9FC9D'
				});
			});
		}
	});
	
	
	
	return (emptyResp == 0);
};

saqli.validateStage2 = function(){
	var me = this,
	    symptoms = $('input[type="checkbox"]:checked', $('#divStage2'));
	
	//remove previous validation messages
	$('.err-caption', $('div[id$="divStage2"]')).remove();
	$('td', $('div[id$="divStage2"]')).css({'background-color':'#FFFFFF'});
	
	if (symptoms.length < 1) {
		$('td:first', $('tr[id="TID3_QID1"]')).each(function (z, x) {
			$(x).prepend('<div class="err-caption"><span style="color:Red;">A response for this question is required!</span></div>');
			$('td', $(x).parent('tr')).css({
				'background-color': '#F9FC9D'
			});
		});

		var _pos = $('div.err-caption').eq(0).position();
		window.scrollTo(0, _pos.top);
		alert('Please review the responses for the highlighted question!');

		return false;
	}
	
	return true;
};

saqli.validateStage3 = function(){
	var me = this,
		Symptoms = $('input[type="checkbox"][maxfive="maxfive"]', $('#divStage3')),
		selSymptoms = $('input[type="checkbox"][maxfive="maxfive"]:checked', $('#divStage3'));

	//remove previous validation messages
	$('div.err-caption', $('tr[id="TID4_QID0"]')).remove();
	$('td', $('tr[id="TID4_QID0"]')).css({'background-color':'#FFFFFF'});

	if(me.opts.skipToTx == false){
		if (selSymptoms.length < 1) {
			$('td:first', $('tr[id="TID4_QID0"]')).each(function (z, x) {
				$(x).prepend('<div class="err-caption"><span style="color:Red;">A response for this question is required!</span></div>');
				$('td', $(x).parent('tr')).css({
					'background-color': '#F9FC9D'
				});
			});

			var _pos = $('div.err-caption').eq(0).position();
			window.scrollTo(0, _pos.top);
			alert('Please review the responses!');

			return false;
		}

		if (Symptoms.length > 5 && selSymptoms.length < 5) {
			$('td:first', $('tr[id="TID4_QID0"]')).each(function (z, x) {
				$(x).prepend('<div class="err-caption"><span style="color:Red;">Please select 5 symptoms!</span></div>');
				$('td', $(x).parent('tr')).css({
					'background-color': '#F9FC9D'
				});
			});

			var _pos = $('div.err-caption').eq(0).position();
			window.scrollTo(0, _pos.top);
			alert('Please review the responses!');

			return false;
		}
		else if (selSymptoms.length < Symptoms.length && Symptoms.length <= 5) {
			$('td:first', $('tr[id="TID4_QID0"]')).each(function (z, x) {
				$(x).prepend('<div class="err-caption"><span style="color:Red;">If your list only has 5 or fewer symptoms, please select all of them.</span></div>');
				$('td', $(x).parent('tr')).css({
					'background-color': '#F9FC9D'
				});
			});

			var _pos = $('div.err-caption').eq(0).position();
			window.scrollTo(0, _pos.top);
			alert('Please review the responses for the highlighted question!');

			return false;
		}
	}
	
	return true;
};

saqli.validateStage4 = function(){
	var me = this,
	    _tr = $('tr', $('table[id$="tblTID5"]')).not('[skipped]'),
	    emptyResp = 0,
	    sliderTR = $('tr[id$="TID6_QID1"]')[0];
	    
	    _tr.push(sliderTR);
	 
	//remove previous validation errors
	$.each(_tr, function(a, b){
		$('.err-caption', $(b)).remove();
		$(b).css({'background-color': '#FFFFFF'});
		$('td:first', $(b)).css({'background-color': '#FFFFFF'});
	});
	
	$('td', $(sliderTR)).css({'background-color': '#FFFFFF'});
	    
	$.each(_tr, function(a, b){
		var sel = $('select', $(b))[0];
		if(sel.selectedIndex < 1){
			emptyResp = emptyResp + 1;
						
			$('td:first', $(b)).each(function (z, x) {
				$(x).prepend('<div class="err-caption"><span style="color:Red;">A response for this question is required!</span></div>');
				$('td', $(x).parent('tr')).css({
					'background-color': '#F9FC9D'
				});
			});
		}
	});
	
	return (emptyResp == 0);
};

saqli.checkStage1opts = function(){
	var me = this;
	
	//initially hide all symptom rows
	$.each(me.opts.responses_map1, function(a, b){
		var m_tr = $('tr[id$="'+ b.tr +'"]')[0],
			sel = $('select', $(m_tr))[0];
			
			sel.selectedIndex = -1;
			$(m_tr).attr('skipped','skipped').hide();
	});
	
	$.each(saqli.opts.responses, function(a, b){
		$.each(me.opts.responses_map1, function(c, d){
			
			if(b.rid == d.rid){
				var m_tr = $('tr[id$="'+ d.tr +'"]')[0];
				$(m_tr).removeAttr('skipped').show();
				
				$('option', $(m_tr)).each(function(){
					var m_val = this.value.substr(this.value.lastIndexOf('|') + 1);
					if(m_val == b.score_value){
						this.selected = true;
					}
				});
			}
		});
	});
	
	//check the impact's slider
	$.each(me.opts.responses, function(a, b){
		if(b.rid == 3002){
			$('option', $('select[id$="rid_3002"]')).each(function(){
				var m_val = this.value.substr(this.value.lastIndexOf('|') + 1);
				if(m_val == b.score_value){
					this.selected = true;
				}
			});
			$('select[id$="rid_3002"]').change();
		}
	});
};

saqli.checkStage2opts = function(){
	//remove previous validation messages
	$('.err-caption', $('div[id$="divStage2"]')).remove();
	$('td', $('div[id$="divStage2"]')).css({'background-color':'#FFFFFF'});
	
	//get previous symptoms
	saqli.getPrevTxSymptoms();
};

saqli.checkStage3opts = function(){
	var me = this,
	    chkStage2checked = $('input[type="checkbox"]:checked', $('div[id$="divStage2"]')),
	    chkStage3 = $('input[type="checkbox"]', $('div[id$="divStage3"]'));
	    
	//remove previous validation messages
	$('div.err-caption', $('tr[id="TID4_QID0"]')).remove();
	$('td', $('tr[id="TID4_QID0"]')).css({'background-color':'#FFFFFF'});
	
	$.each(chkStage3, function(a, b){
		$(b).removeAttr('maxfive').removeAttr('checked').attr('skipped','skipped').hide();
		$('label[for="'+ b.id +'"]').hide();
	});
	
	$.each(chkStage2checked, function(a, b){
		$.each(chkStage3, function(c, d){
			var m_rid = parseInt(b.id.replace(/\D/gi, '')) + 1000;
			if(d.id == 'rid_' + m_rid){
				$(d).attr('maxfive','maxfive').removeAttr('skipped').show();
				$('label[for="'+ d.id +'"]').show();
			}
		});
	});
	
	//bind click event listener
	$('input[type="checkbox"][maxfive="maxfive"]', $('div[id$="divStage3"]')).each(function(){
		$(this).bind({
			click: function(){
				me.checkMaxFive();
			}
		});
	});
	
};

saqli.checkStage4opts = function(){
	var me = this,
	    bIgnoreSkipPattern = false;
	
	//remove previous validation messages
	$('div.err-caption', $('div[id="divStage4"]')).remove();
	$('td', $('div[id="divStage4"]')).css({'background-color':'#FFFFFF'});
	
	//initially hide all symptom rows
	$.each(me.opts.responses_map2, function(a, b){
		var m_tr = $('tr[id$="'+ b.tr +'"]')[0],
			sel = $('select', $(m_tr))[0];
			
			sel.selectedIndex = -1;
			$(m_tr).attr('skipped','skipped').hide();
	});
	
	$.each(saqli.opts.responses, function(a, b){
		$.each(me.opts.responses_map2, function(c, d){
			
			if(b.rid == d.rid){
				var m_tr = $('tr[id$="'+ d.tr +'"]')[0];
				$(m_tr).removeAttr('skipped').show();
				
				$('option', $(m_tr)).each(function(){
					var m_val = this.value.substr(this.value.lastIndexOf('|') + 1);
					if(m_val == b.score_value){
						this.selected = true;
						bIgnoreSkipPattern = true;
					}
				});
			}
		});
	});
	
	//check the impact's slider
	$.each(me.opts.responses, function(a, b){
		if(b.rid == 7002){
			$('option', $('select[id$="rid_7002"]')).each(function(){
				var m_val = this.value.substr(this.value.lastIndexOf('|') + 1);
				if(m_val == b.score_value){
					this.selected = true;
				}
			});
			$('select[id$="rid_7002"]').change();
		}
	});
	
	if(!bIgnoreSkipPattern){
		questions.checkSkipPatterns();
	}
};

saqli.getPrevTxSymptoms = function(){
	var me = this,
		prevTxSymptoms = [];
		
	$.each(saqli.opts.responses, function(a, b){
		if(parseInt(b.rid) >= 4000 && parseInt(b.rid) <= 4999){
			prevTxSymptoms.push(b);
		}
	});
	
	
	if(prevTxSymptoms.length > 0){
		
		//initially hide all checkboxes
		$('input[type="checkbox"], label', $('div[id$="divStage2"]')).each(function(a,b){
			$(this).hide();
		});
		
		//check which symptom to show
		$.each(prevTxSymptoms, function(c, d){
			$('input[id$="rid_'+ d.rid +'"]').each(function(){
				$(this).attr('checked','checked').show();
				$(this).next('label').show();
			});
		});
	}
};

saqli.checkMaxFive = function () {
	var me = this,
		Symptoms = $('input[type="checkbox"]', $('#divStage3')),
		selSymptoms = $('input[type="checkbox"]:checked', $('#divStage3'));

	if (selSymptoms.length > 4) {
		//disable remaining unselected symptoms
		$.each(Symptoms, function (i, o) {
			if (!o.checked) {
				$(o).attr('disabled', 'disabled');
			}
		});
	}
	else {
		//re-enable remaining unselected symptoms
		$.each(Symptoms, function (i, o) {
			if (!o.checked) {
				$(o).removeAttr('disabled');
			}
		});
	}

};

