//define the soap objective namespace
if (typeof(soap) != "undefined") {
	soap.objective = {
		
		//add listener to Suicide Risk Assessment's dropdowns
		setChangeEvt : function () {
			var me = this,
			cboIDs = [
				'cboSuicideIntensity',
				'cboSuicidePlan',
				'cboSuicideMeans',
				'cboSuicideRehearsed',
				'cboSuicideAttempts'];
			$.each(cboIDs, function (i, v) {
				$('select[id$="' + v + '"]').each(function () {
					var obj = this;
					$(this).bind({
						change : function () {
							me.levelOfRisk(obj);
						}
					});
				});
			});
		},
		
		// enable/disable level of risk
		levelOfRisk : function (obj) {
			var me = this;
			me.getConditions();
		},
		
		// get values of dropdowns
		getConditions : function () {
			var Q1 = (function () {
				var val;
				$('option', $('select[id$="cboSuicideIntensity"]'))
				.each(function () {
					var opt = this;
					if (opt.selected) {
						val = opt.text;
					}
				});
				return val;
			}),
			
			Q2 = (function () {
				var val;
				$('option', $('select[id$="cboSuicidePlan"]'))
				.each(function () {
					var opt = this;
					if (opt.selected) {
						val = opt.text;
					}
				});
				return val;
			}),
			
			Q3 = (function () {
				var val;
				$('option', $('select[id$="cboSuicideMeans"]'))
				.each(function () {
					var opt = this;
					if (opt.selected) {
						val = opt.text;
					}
				});
				return val;
			}),
			
			Q4 = (function () {
				var val;
				$('option', $('select[id$="cboSuicideRehearsed"]'))
				.each(function () {
					var opt = this;
					if (opt.selected) {
						val = opt.text;
					}
				});
				return val;
			}),
			
			Q5 = (function () {
				var val;
				$('option', $('select[id$="cboSuicideAttempts"]'))
				.each(function () {
					var opt = this;
					if (opt.selected) {
						val = opt.value;
					}
				});
				return val;
			});
			
			if (typeof(Q1()) != "undefined"
				 && typeof(Q2()) != "undefined"
				 && typeof(Q3()) != "undefined"
				 && typeof(Q4()) != "undefined"
				 && typeof(Q5()) != "undefined") {
				var c1 = ((Q1()).match(/(moderate|high)/gi)) ? 1 : 0,
				c2 = ((Q2()).match(/(yes)/gi)) ? 1 : 0,
				c3 = ((Q3()).match(/(yes)/gi)) ? 1 : 0,
				c4 = ((Q4()).match(/(yes)/gi)) ? 1 : 0,
				c5 = parseInt(Q5()),
				cSum = c1 + c2 + c3 + c4;
				
				var opts = $('option', $('select[id$="cboSuicideSeverity"]')),
				bAlertUser = false;
				
				/*console.log('c1: %s, c2: %s, c3: %s, c4: %s, c5: %s, cSum: %s',
					c1, c2, c3, c4, c5, cSum);*/
				
				if (c5 > 0) {
					if (cSum == 0) {
						$.each(opts, function (i, opt) {
							var reGrayed = /(no significant risk)/gi;
							if (opt.text.match(reGrayed)) {
								if (opt.selected) {
									opts[0].selected = true;
									bAlertUser = true;
								}
								$(opt).attr('disabled', 'disabled');
							} else {
								$(opt).removeAttr('disabled');
							}
						});
					} else if (cSum == 1) {
						$.each(opts, function (i, opt) {
							var reGrayed = /(no significant risk|mild)/gi;
							if (opt.text.match(reGrayed)) {
								if (opt.selected) {
									opts[0].selected = true;
									bAlertUser = true;
								}
								$(opt).attr('disabled', 'disabled');
							} else {
								$(opt).removeAttr('disabled');
							}
						});
					} else if (cSum >= 2) {
						$.each(opts, function (i, opt) {
							var reGrayed = /(no significant risk|mild|moderate)/gi;
							if (opt.text.match(reGrayed)) {
								if (opt.selected) {
									opts[0].selected = true;
									bAlertUser = true;
								}
								$(opt).attr('disabled', 'disabled');
							} else {
								$(opt).removeAttr('disabled');
							}
						});
					}
				} else {
					if (cSum == 0) {
						$.each(opts, function (i, opt) {
							$(opt).removeAttr('disabled');
						});
					} else if (cSum == 1) {
						$.each(opts, function (i, opt) {
							var reGrayed = /(no significant risk)/gi;
							if (opt.text.match(reGrayed)) {
								if (opt.selected) {
									opts[0].selected = true;
									bAlertUser = true;
								}
								$(opt).attr('disabled', 'disabled');
							} else {
								$(opt).removeAttr('disabled');
							}
						});
					} else if (cSum >= 2) {
						$.each(opts, function (i, opt) {
							var reGrayed = /(no significant risk|mild)/gi;
							if (opt.text.match(reGrayed)) {
								if (opt.selected) {
									opts[0].selected = true;
									bAlertUser = true;
								}
								$(opt).attr('disabled', 'disabled');
							} else {
								$(opt).removeAttr('disabled');
							}
						});
					}
				}
			}
			
			if(bAlertUser)
			{
				alert('Please make a new selection on the "Current Level of Risk".');
			}
		},
		
		//init
		init : function () {
			var me = this;
			me.setChangeEvt();
			me.levelOfRisk(null);
		}
		
	};
}
