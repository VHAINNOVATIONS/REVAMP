//define the SOAP.PLAN namespace
if (window.soap !== undefined) {
	soap.plan = {
		
		editGrid : {
			
			//edit grid methods
			//edit this row
			editRow : function (obj, opts) {
				var _me = this;
				var thisRow = $(obj).parent().parent().parent('tr');
				var thisTable = $(thisRow).parent('table');
				$('.is-open').each(function () {
					var btn = $('img[src$="cross.png"]', this).get(0);
					_me.cancelEdit(btn, opts);
				});
				
				_me._editRow(thisRow);
			},
			_editRow : function (thisRow) {
				var _me = this;
				$('.grid-read', $(thisRow)).hide();
				$('.grid-edit', $(thisRow)).show();
				$(thisRow).addClass('is-open');
			},
			
			//cancel edit this row
			cancelEdit : function (obj, opts) {
				var thisRow = thisRow = $(obj).parent().parent().parent('tr'),
				thisTable = $(thisRow).parent('table');
				
				$('.grid-edit', $(thisRow)).hide().removeClass('is-open');
				$('.grid-read', $(thisRow)).show();
				$.each(opts.editFields, function (i, v) {
					var _edit = $('[id$="' + v[0] + '"]').get(0);
					var _orig = $('[id$="' + v[1] + '"]').get(0);
					_edit.value = _orig.value;
				});
				
				$.each(opts.checkBoxes, function (i, v) {
					var _edit = $('[id$="' + v[0] + '"]').get(0);
					var _orig = $('[id$="' + v[1] + '"]').get(0);
					_edit.checked = (_orig.value == "1");
				});
			},
			
			//add rows
			addRows : function (opts) {
				var _me = this;
				var addTable = $('table[id$="' + opts.tableAddId + '"]').get(0);
				
				//clear all controls in the table
				//textboxes
				$('input[type="text"]', addTable).each(function () {
					this.value = '';
				});
				
				//combos
				$('select', addTable).each(function () {
					this.selectedIndex = -1;
				});
				
				//checkboxes
				$('input[type="checkbox"]', addTable).each(function () {
					this.checked = false;
				});
				
				//show the add_rows table
				$(addTable).show();
			},
			
			//cancel add rows
			cancelAddRows : function (opts) {
				var _me = this;
				var addTable = $('table[id$="' + opts.tableAddId + '"]').get(0);
				
				//hide the add_rows table
				$(addTable).hide();
			},
			
			//discontinue -> show popup
			discontinueItem : function (obj, opts) {
				//clear discontinue text field
				$('[id$="' + opts.discFieldId + '"]').val('');
				
				var $mtr = $(obj).parent().parent().parent('tr');
				var trID = ($mtr.attr('id')).replace(/\D/gi, '');
				
				//create a temporary hidden field to store row id
				var tmpHtxt = document.getElementById(opts.tmpField);
				if (tmpHtxt != null) {
					tmpHtxt.value = trID;
				} else {
					var input = document.createElement("input");
					input.setAttribute("type", "hidden");
					input.setAttribute("name", opts.tmpField);
					input.setAttribute("id", opts.tmpField);
					input.setAttribute("value", trID);
					document.forms[0].appendChild(input);
				}
				
				//show discontinue popup
				eval(opts.discWindowId + '.show();');
			}
			
		},
		
		// plan mode DIVs
		planModeDiv : ['vwPlan', 'vwNote'],
		
		//show stat modality popup
		showStatModality: function(){
			Ext.onReady(function(){
				$('input[type="checkbox"]', $('table[id$="tblStatModality"]')).each(function(){
					var chk = this;
					chk.checked = false;
					$(chk).parent().parent('tr').css({'background-color':''});
					
					//disable checkboxes for already selected modalities
					if(typeof($('[id$="htxtModalities"]').get(0)) !== undefined){
						if($('[id$="htxtModalities"]').get(0).value.length > 2){
							var modData = eval($('[id$="htxtModalities"]').get(0).value);
							$.each(modData, function(i,o){
								if(o.stat_modality_id == chk.value)
								{
									chk.disabled = true;
									$(chk).parent().parent('tr').css({'background-color':'#f1f1f1'});
								}
							});
						}
					}
				});
				
				winStatModality.show();
			});
		},
		
		// enable/disable Discontinue button
		enableDiscButton: function(ele, btnID)
		{
			var btn 	= 	$('[id$="' + btnID + '"]')[0],
			    strVal	=	ele.value;
			strVal = strVal.replace(/\s/gi, '');
			if(strVal.length > 2)
			{
				$(btn).removeAttr('disabled');
			}
			else
			{
				$(btn).attr('disabled','disabled');
			}
			return true;
		}
	};
}

// **************************************************************
// **************************************************************
//define the SOAP.PLAN.PROBLEM namespace
if (window.soap.plan !== undefined) {
    soap.plan.problem = {

        //problems grids ids
        arrProblemsDataTables: ['tblProblemsAxis1', 'tblProblemsAxis2',
				'tblProblemsAxis3', 'tblProblemsAxis4', 'tblProblemsAxis5', 'tblProblemsAxis10'],

        //show goal, strenght, weakness for selected problem
		showProblemChildren: function(){
			var me = this,
			    tbls = ['tblGoals','tblStrength','tblWeakness'],
				problemid = $('input[id$="htxtProblemID"]').val();
				
			
			$.each(tbls, function(i, v){
				$('tbody tr', $('table[id$="'+ v +'"]')).each(function(){
					if(this.getAttribute('problemid') == problemid){
						$(this).show();
					}
					else
					{
						$(this).hide();
					}
				});
			});
		},
		
		//initialize problem list
        init: function()
        {
            var me = this;
            //paint selected problem row
            $.each(me.arrProblemsDataTables, function(i, k)
            {
                $('tbody tr', $('table[id$="' + k + '"]')).each(function()
                {
                    var trID = parseInt(this.id.replace(/\D/gi, ''));
                    if (parseInt($('input[id$="htxtProblemID"]').val()) == trID)
                    {
                        $('tr', $('#divProblemTbls')).removeClass('selected');
                        $('div', $('#divProblemTbls')).removeClass('selected');
                        $(this).addClass('selected');
                    }

                    //bind onclick event
                    $(this).bind({
                        click: function()
                        {
                            $('a', $(this)).each(function()
                            {
                                var m_onclick = this.href.replace('javascript:', '');
                                eval(m_onclick);
                            });
                        }
                    }).css({
                        cursor: 'pointer'
                    });
                });
            });
            
            //tplan.ProbAxes.init();
            me.setProblemAlert();
			
			me.showProblemChildren();
        },

        setProblemAlert: function()
        {
            $(function()
            {
                var mProblemSummary;
                var tblAxis = [
				'tblOMProblemsAxis1', 
				'tblOMProblemsAxis2', 
				'tblOMProblemsAxis3', 
				'tblOMProblemsAxis4', 
				'tblOMProblemsAxis5',
				'tblOMProblemsAxis10'];

                var oProblemFlag = $('input[id$="htxtProblemSummary"]').get(0);
                if (oProblemFlag)
                {
                    mProblemSummary = eval(oProblemFlag.value)
                    if (mProblemSummary !== undefined)
                    {
                        try
                        {
                            for (var i = 0, tn; (tn = tblAxis[i]) != null; i++)
                            {
                                for (var o = 0, probjson; (probjson = mProblemSummary[o]) != null; o++)
                                {
                                    $('tbody tr', $('table[id$="' + tn + '"]')).each(function()
                                    {
                                        var prob_id = $(this).attr('id');
                                        prob_id = prob_id.replace(/\D/gi, '');
                                        var flag_td = $('td:first', $(this)).get(0);

                                        if (probjson == prob_id)
                                        {
                                            $(flag_td).addClass('problem-alert');
                                        } else
                                        {
                                            $(flag_td).removeClass('problem-alert');
                                        }
                                    });
                                }
                            }
                        } catch (err)
                        {
                            txt = "There was an error on this page.\n";
                            txt += "Error description: " + err.description;
                        }
                    }
                }
            });
        }
    };
}

// **************************************************************
// **************************************************************
// define the SOAP.PLAN.SUMMARY namespace
if (window.soap.plan !== undefined) {
	soap.plan.summary = {
		
		//put an alert on Intervention and Self-Management
		//summary grid if they have values with bad adherence
		checkAdherenceGrids : function () {
			"use strict";
			var tblNames = ['tblInterventionSummary', 'tblSelfMgntSummary'];
			for (var i = 0, tbl; (tbl = tblNames[i]) != null; i++) {
				$('tbody tr', $('table[id$="' + tbl + '"]')).each(function () {
					var c = parseInt($(this).attr('adherencecode'));
					switch (c) {
					case 0:
						$('td:first', $(this))
						.addClass('problem-noinfo')
						.removeClass('problem-alert')
						.removeClass('problem-targetmet');
						break;
					case 1:
						$('td:first', $(this)).removeClass('problem-noinfo')
						.removeClass('problem-alert')
						.addClass('problem-targetmet');
						break;
					default:
						$('td:first', $(this))
						.removeClass('problem-noinfo')
						.addClass('problem-alert')
						.removeClass('problem-targetmet');
					}
				});
			}
		}
	};
}

// **************************************************************
// **************************************************************
//define the SOAP.PLAN.TRACKER namespace
if (window.soap.plan !== undefined) {
	soap.plan.tracker = {
		
		//MEASURES
		measures : {
			
			opts : {
				//general options
				tableID : 'tblMeasureValues',
				tableAddId : 'tblMeasureValues_Add',
				openRow : null,
				editFields : [
					['txtObjectiveValDate', 'htxtObjectiveValDateOrig'],
					['cboObjValue', 'htxtObjectiveValOrig'],
					['txtObjectiveValComment', 'htxtObjectiveValCommentOrig']
				],
				checkBoxes : []
			},
			
			//edit row
			editRow : function (obj) {
				var _me = this;
				soap.plan.editGrid.editRow(obj, _me.opts);
			},
			
			//cancel edit row
			cancelEdit : function (obj) {
				var _me = this;
				soap.plan.editGrid.cancelEdit(obj, _me.opts);
			},
			
			//add rows
			addRows : function () {
				var _me = this;
				soap.plan.editGrid.addRows(_me.opts);
			},
			
			//cancel add rows
			cancelAddRows : function () {
				var _me = this;
				soap.plan.editGrid.cancelAddRows(_me.opts);
			}
			
		},
		
		//INTERVENTIONS
		interventions : {
			
			//general options
			opts : {
				tableID : 'tblInterventionValues',
				tableAddId : 'tblInterventionValues_Add',
				openRow : null,
				editFields : [
					['txtInterventionValDate', 'htxtInterventionValDateOrig'],
					['cboInterventionValue', 'htxtInterventionValOrig'],
					['txtInterventionValComment', 'htxtInterventionValCommentOrig']
				],
				checkBoxes : []
			},
			
			//edit row
			editRow : function (obj) {
				var _me = this;
				soap.plan.editGrid.editRow(obj, _me.opts);
			},
			
			//cancel edit row
			cancelEdit : function (obj) {
				var _me = this;
				soap.plan.editGrid.cancelEdit(obj, _me.opts);
			},
			
			//add rows
			addRows : function () {
				var _me = this;
				soap.plan.editGrid.addRows(_me.opts);
			},
			
			//cancel add rows
			cancelAddRows : function () {
				var _me = this;
				soap.plan.editGrid.cancelAddRows(_me.opts);
			}
		},
		
		//SELF_MANAGEMENT
		homeworks : {
			
			//general options
			opts : {
				tableID : 'tblHomeworkValues',
				tableAddId : 'tblHomeworkValues_Add',
				openRow : null,
				editFields : [
					['txtHomeworkValDate', 'htxtHomeworkValDateOrig'],
					['cboHomeworkValue', 'htxtHomeworkValOrig'],
					['txtHomeworkValComment', 'htxtHomeworkValCommentOrig']
				],
				checkBoxes : []
			},
			
			//edit row
			editRow : function (obj) {
				var _me = this;
				soap.plan.editGrid.editRow(obj, _me.opts);
			},
			
			//cancel edit row
			cancelEdit : function (obj) {
				var _me = this;
				soap.plan.editGrid.cancelEdit(obj, _me.opts);
			},
			
			//add rows
			addRows : function () {
				var _me = this;
				soap.plan.editGrid.addRows(_me.opts);
			},
			
			//cancel add rows
			cancelAddRows : function () {
				var _me = this;
				soap.plan.editGrid.cancelAddRows(_me.opts);
			}
		}
	};
}

// **************************************************************
// **************************************************************
//define the SOAP.PLAN.BUILDER namespace
if (window.soap.plan !== undefined) {
	soap.plan.builder = {
		
		//COMMON
		section: {
			measure:{},
			intervention:{},
			homework:{},
			modality:{
				winDisc: 'winModalityDisc',
				btnDisc: 'btnDiscModality',
				txtDisc: 'txtDiscModality',
				keyattr: 'modalityid',
				htxt: 'htxtTmpModality'
			},
			goal:{},
			strength:{},
			weakness:{},
			referral:{}
		},
		
		//get discontinue reason's text length to enable/disable discontinue button
		getDiscTextLength: function(obj, section)
		{
			var me = this;
			var strDisc= obj.value;
			var txtlen = parseInt(strDisc.replace(/\s/gi,'').length);
			
			var sect = me.section[section];
			//get discontinue button
			var btn = $('input[id$="'+ sect.btnDisc +'"]').get(0);
				
			if(txtlen > 2)
			{
				$(btn).removeAttr('disabled');
			}
			else
			{
				$(btn).attr('disabled', 'disabled');
			}
		},
		
		//MEASURES
		measure : {
			//general options
			opts : {
				tableID : 'tblMeasureBuilder',
				tableAddId : 'tblMeasureBuilder_Add',
				openRow : null,
				editFields : [
					['txtOutcomeDesc', 'htxtOutcomeDescOrig'],
					['txtOutcomeTargetDate', 'htxtOutcomeTargetDateOrig'],
					['cboOutcomeBaseline', 'htxtOutcomeBaselineOrig'],
					['cboOutcomeTargetOp', 'htxtOutcomeTargetOpOrig'],
					['cboOutcomeTargetValue', 'htxtOutcomeTargetValueOrig'],
					['txtOutcomeComment', 'htxtOutcomeComentOrig']
				],
				checkBoxes : [
					['chkOutcomeIsDone', 'htxtOutcomeIsDoneOrig']
				],
				discFieldId : 'txtDiscMeasureBuilder', //discontinue reason text field id
				discWindowId : 'winMeasureDisc', //discontinue reason popup id
				tmpField : 'htxtTmpMeasureBuilder' //temporary hidden field to hold item id
			},
			
			//edit row
			editRow : function (obj) {
				var _me = this;
				soap.plan.editGrid.editRow(obj, _me.opts);
			},
			
			//cancel edit row
			cancelEdit : function (obj) {
				var _me = this;
				soap.plan.editGrid.cancelEdit(obj, _me.opts);
			},
			
			//add rows
			addRows : function () {
				var _me = this;
				soap.plan.editGrid.addRows(_me.opts);
			},
			
			//cancel add rows
			cancelAddRows : function () {
				var _me = this;
				soap.plan.editGrid.cancelAddRows(_me.opts);
			},
			
			//discontinue item
			discontinueItem : function (obj) {
				var _me = this;
				soap.plan.editGrid.discontinueItem(obj, _me.opts);
			}
			
		},
		
		//INTERVENTIONS
		intervention : {
			//general options
			opts : {
				tableID : 'tblIntervention',
				tableAddId : 'tblIntervention_Add',
				openRow : null,
				editFields : [['txtIntervention', 'htxtInterventionOrig']],
				checkBoxes : [],
				discFieldId : 'txtDiscIntervention', //discontinue reason text field id
				discWindowId : 'winInterventionDisc', //discontinue reason popup id
				tmpField : 'htxtTmpIntervention' //temporary hidden field to hold item id
			},
			
			//edit row
			editRow : function (obj) {
				var _me = this;
				soap.plan.editGrid.editRow(obj, _me.opts);
			},
			
			//cancel edit row
			cancelEdit : function (obj) {
				var _me = this;
				soap.plan.editGrid.cancelEdit(obj, _me.opts);
			},
			
			//add rows
			addRows : function () {
				var _me = this;
				soap.plan.editGrid.addRows(_me.opts);
			},
			
			//cancel add rows
			cancelAddRows : function () {
				var _me = this;
				soap.plan.editGrid.cancelAddRows(_me.opts);
			},
			
			//discontinue item
			discontinueItem : function (obj) {
				var _me = this;
				soap.plan.editGrid.discontinueItem(obj, _me.opts);
			}
			
		},
		
		//HOMEWORKS
		homework : {
			
			//general options
			opts : {
				tableID : 'tblSelfManagement',
				tableAddId : 'tblSelfManagement_Add',
				openRow : null,
				editFields : [['txtManagement', 'htxtManagementOrig']],
				checkBoxes : [],
				discFieldId : 'txtDiscManagement', //discontinue reason text field id
				discWindowId : 'winSelfManagementDisc', //discontinue reason popup id
				tmpField : 'htxtTmpManagement' //temporary hidden field to hold item id
			},
			
			//edit row
			editRow : function (obj) {
				var _me = this;
				soap.plan.editGrid.editRow(obj, _me.opts);
			},
			
			//cancel edit row
			cancelEdit : function (obj) {
				var _me = this;
				soap.plan.editGrid.cancelEdit(obj, _me.opts);
			},
			
			//add rows
			addRows : function () {
				var _me = this;
				soap.plan.editGrid.addRows(_me.opts);
			},
			
			//cancel add rows
			cancelAddRows : function () {
				var _me = this;
				soap.plan.editGrid.cancelAddRows(_me.opts);
			},
			
			//discontinue item
			discontinueItem : function (obj) {
				var _me = this;
				soap.plan.editGrid.discontinueItem(obj, _me.opts);
			}
			
		},
		
		//MODALITIES
		modality : {},
		
		//GOALS
		goal : {
			
			//general options
			opts : {
				tableID : 'tblGoals',
				tableAddId : 'tblGoals_Add',
				openRow : null,
				editFields : [['txtGoal', 'htxtGoalOrig'], ['txtGComment', 'htxtGCommentOrig']],
				checkBoxes : [],
				discFieldId : 'txtDiscGoal', //discontinue reason text field id
				discWindowId : 'winGoalsDisc', //discontinue reason popup id
				tmpField : 'htxtTmpGoal' //temporary hidden field to hold item id
			},
			
			//edit row
			editRow : function (obj) {
				var _me = this;
				soap.plan.editGrid.editRow(obj, _me.opts);
			},
			
			//cancel edit row
			cancelEdit : function (obj) {
				var _me = this;
				soap.plan.editGrid.cancelEdit(obj, _me.opts);
			},
			
			//add rows
			addRows : function () {
				var _me = this;
				soap.plan.editGrid.addRows(_me.opts);
			},
			
			//cancel add rows
			cancelAddRows : function () {
				var _me = this;
				soap.plan.editGrid.cancelAddRows(_me.opts);
			},
			
			//discontinue item
			discontinueItem : function (obj) {
				var _me = this;
				soap.plan.editGrid.discontinueItem(obj, _me.opts);
			}
			
		},
		
		//STRENGTHS
		strength : {
			
			//general options
			opts : {
				tableID : 'tblStrength',
				tableAddId : 'tblStrength_Add',
				openRow : null,
				editFields : [
					['txtStrength', 'htxtStrengthOrig'],
					['txtStrengthComment', 'htxtStrengthCommentOrig']
				],
				checkBoxes : [],
				discFieldId : 'txtDiscStrength', //discontinue reason text field id
				discWindowId : 'winStrengthDisc', //discontinue reason popup id
				tmpField : 'htxtTmpStrength' //temporary hidden field to hold item id
			},
			
			//edit row
			editRow : function (obj) {
				var _me = this;
				soap.plan.editGrid.editRow(obj, _me.opts);
			},
			
			//cancel edit row
			cancelEdit : function (obj) {
				var _me = this;
				soap.plan.editGrid.cancelEdit(obj, _me.opts);
			},
			
			//add rows
			addRows : function () {
				var _me = this;
				soap.plan.editGrid.addRows(_me.opts);
			},
			
			//cancel add rows
			cancelAddRows : function () {
				var _me = this;
				soap.plan.editGrid.cancelAddRows(_me.opts);
			},
			
			//discontinue item
			discontinueItem : function (obj) {
				var _me = this;
				soap.plan.editGrid.discontinueItem(obj, _me.opts);
			}
			
		},
		
		//WEAKNESSES
		weakness : {
			
			//general options
			opts : {
				tableID : 'tblWeakness',
				tableAddId : 'tblWeakness_Add',
				openRow : null,
				editFields : [
					['txtWeakness', 'htxtWeaknessOrig'],
					['txtWeaknessComment', 'htxtWeaknessCommentOrig']
				],
				checkBoxes : [],
				discFieldId : 'txtDiscWeakness', //discontinue reason text field id
				discWindowId : 'winWeaknessDisc', //discontinue reason popup id
				tmpField : 'htxtTmpWeakness' //temporary hidden field to hold item id
			},
			
			//edit row
			editRow : function (obj) {
				var _me = this;
				soap.plan.editGrid.editRow(obj, _me.opts);
			},
			
			//cancel edit row
			cancelEdit : function (obj) {
				var _me = this;
				soap.plan.editGrid.cancelEdit(obj, _me.opts);
			},
			
			//add rows
			addRows : function () {
				var _me = this;
				soap.plan.editGrid.addRows(_me.opts);
			},
			
			//cancel add rows
			cancelAddRows : function () {
				var _me = this;
				soap.plan.editGrid.cancelAddRows(_me.opts);
			},
			
			//discontinue item
			discontinueItem : function (obj) {
				var _me = this;
				soap.plan.editGrid.discontinueItem(obj, _me.opts);
			}
			
		},
		
		//REFERRALS
		referral : {}
		
	};
}

// **************************************************************
// **************************************************************
//define the SOAP.PLAN.NOTE namespace
if (window.soap.plan !== undefined) {
	soap.plan.note = {};
}

//STANDALONE functions for the plan section

function showTrendChart() {
	setTimeout("renderTrendChart()", 100);
}

function autoAdjustMainDiv() {
    $(document).ready(function () {
        setTimeout(function () {
            origWidth = $('input[id$="htxtMainDivWidth"]').val();
            wp.adjustMain();

            if ($('div[id$="mainContents"]').css('width') != origWidth) {
                $('div[id$="mainContents"]').css({
                    width: origWidth
                });
                $('input[id$="htxtMainDivWidth"]').val(origWidth);
            }
            $('input[type="button"], input[type="submit"]').css({
                padding: '1px 6px'
            });
        }, 1);
    });
}
