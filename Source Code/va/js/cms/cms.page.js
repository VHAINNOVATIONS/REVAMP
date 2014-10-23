if (typeof (cms) == "undefined") {
    var cms = {};
}

cms.page = {

    //general settings and options
    opts: {
        editMode: null,
        editModeRadios: 'rblPageEditMode',
        editDivID: 'divEditPage',
        addDivID: 'divAddPage',
        divEditControls: 'divEditControls',
        editControls: [
            {id: 'txtPageTitle', value: ''},
            {id: 'cboPageStatus', value: '0'}
        ],
        pageContents: 'txtPageContents',
        authorsCombo: 'cboAuthors',
        selectedPageID: 'htxtSelectedPageID',
        preSelectedPageID: 'htxtPreSelPageID',
        pagesListTable: 'tblStaticPagesList',
        spanDelete: 'spDeletePage',
        cboAuthors: 'cboAuthorsPopup',
        cboStatus: 'cboStatusPopup'
    },

    //set the page edit mode
    setEditMode: function (obj) {
        var me = this,
            addDiv = $('div[id$="' + me.opts.addDivID + '"]')[0],
            editDiv = $('div[id$="' + me.opts.editDivID + '"]')[0],
            selectedPage = $('input[id$="' + me.opts.selectedPageID + '"]')[0],
            spDelete = $('[id$="' + me.opts.spanDelete + '"]')[0],
            divControls = $('div[id$="' + me.opts.divEditControls + '"]')[0];

        me.opts.editMode = obj.value;

        if (me.opts.editMode == "0") {
            /*
            $(addDiv).show();
            $(editDiv).hide();
            $(selectedPage).val('-1');
            me.resetPage();
            $(spDelete).hide();
            $(divControls).show();
            */
            window.location = "cms_page_edit.aspx";
        }
        else if (me.opts.editMode == "1") {
            $(addDiv).hide();
            $(editDiv).show();

            if (parseInt(selectedPage.value) < 1) {
                $(divControls).hide();
            }
        }
    },

    //reset page's controls
    resetPage: function () {
        var me = this,
            ed = tinyMCE.activeEditor;

        $.each(me.opts.editControls, function (i, o) {
            $('[id$="'+ o.id +'"]').val(o.value);
        });

        //set author to current user
        $('[id$="' + me.opts.authorsCombo + '"]').val(me.getAuthorID());

        //reset contents
        $('[id$="' + me.opts.pageContents + '"]').html('');
        $('[id$="' + me.opts.pageContents + '"]').val('');

        ed.save();
        me.initTextEditor();
    },

    //show page selection popup
    editPage: function () {
        var me = this,
            cboAuthors = $('select[id$="' + me.opts.cboAuthors + '"]')[0],
            cboStatus = $('select[id$="' + me.opts.cboStatus + '"]')[0],
            tbl = $('table[id$="' + me.opts.pagesListTable + '"]')[0],
            rows = $('tbody tr', tbl);

        cboAuthors.selectedIndex = 0;
        cboStatus.selectedIndex = 0;

        me.selectPage(null);
        me.filterPages();

        if (typeof (winSelectPage) != "undefined") {
            winSelectPage.show();
        }
    },

    //show template selection popup
    selectTemplate: function () {
        var me = this;

        me.selectPage(null);
        
        alert('Feature under construction!');
    },


    //get authorID for current user
    getAuthorID: function () {
        var me = this;
        if (me.opts.authorID) {
            return me.opts.authorID;
        }
        return 0;
    },

    //initialize text editor
    initTextEditor: function () {
        $('textarea.tinymce').tinymce({
            // Location of TinyMCE script
            script_url: 'js/tiny_mce/tiny_mce.js',

            // General options
            theme: "advanced",
            plugins: "advhr,advimage,advlink,advlist,autolink,contextmenu,directionality,emotions,fullscreen,inlinepopups,insertdatetime,layer,lists,media,nonbreaking,noneditable,pagebreak,paste,preview,print,searchreplace,style,table,visualchars,xhtmlxtras",

            // Theme options
            theme_advanced_buttons1: "newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,formatselect,|,fontselect,|,fontsizeselect",
            theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
            theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
            theme_advanced_buttons4: "styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,pagebreak,|,mybutton",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: true,
			
			setup: function (ed) {
			    // Add a custom button
			    ed.addButton('mybutton', {
			        title: 'Template',
			        image: 'Images/layout.png',
			        onclick: function () {
			            // Add you own code to execute something on click
			            ed.focus();
			            if (typeof (winSelectTemplate) != "undefined")
			            {
			                Ext.onReady(function () {
			                    winSelectTemplate.show();
			                });
			            }
			        }
			    });
			}
        });
    },

    //select page to edit
    selectPage: function (obj) {
        var me = this,
            preSel = $('[id$="' + me.opts.preSelectedPageID + '"]')[0],
            selProb = $('[id$="' + me.opts.selectedPageID + '"]')[0],
            rows = $('tbody tr', $('table[id$="' + me.opts.pagesListTable + '"]'));

        $(preSel).val('');

        $.each(rows, function (i, r) {
            $(r).removeClass('selected');
        });

        if (obj) {
            $(obj).addClass('selected');
            $(preSel).val($(obj).attr('pageid'));
        }
    },

    //bind onclick event to pages list table
    bindPageListOnClick: function () {
        var me = this,
            preSel = $('[id$="' + me.opts.preSelectedPageID + '"]')[0],
            selProb = $('[id$="' + me.opts.selectedPageID + '"]')[0],
            rows = $('tbody tr', $('table[id$="' + me.opts.pagesListTable + '"]'));

        $.each(rows, function (i, r) {
            $(r).css({cursor: 'pointer'}).bind({
                click: function () {
                    me.selectPage(this);
                }
            });
        });

    },

    //check edit mode
    checkEditMode: function () {
        var me = this,
            rbl = $('input[type="radio"][name$="' + me.opts.editModeRadios + '"]:checked')[0];
        me.setEditMode(rbl);
    },

    //check for delete button
    checkDeleteBtn: function () {
        var me = this,
            selPage = $('[id$="'+ me.opts.selectedPageID +'"]')[0],
            spDelete = $('[id$="' + me.opts.spanDelete + '"]')[0];
        
        if ($(selPage).val() > 0) {
            $(spDelete).show();
        }
        else {
            $(spDelete).hide();
        }
    },

    //modify master save action
    setMasterSave: function () {
        var ed = tinyMCE.activeEditor.save();
        clickedMasterSave();
    },

    // --------------------------------------------
    //      Pages Popup
    // --------------------------------------------

    // bind onchange actions to authors and status dropdowns
    bindFilterOnChange: function () {
        var me = this,
            cboAuthors = $('select[id$="' + me.opts.cboAuthors + '"]')[0],
            cboStatus = $('select[id$="' + me.opts.cboStatus + '"]')[0],
            cboArr = [cboAuthors, cboStatus];
        $.each(cboArr, function (i, c) {
            $(c).bind({
                change: function () {
                    me.filterPages();
                }
            });
        });
    },

    //filter pages popup by author or status
    filterPages: function () {
        var me = this,
            cboAuthors = $('select[id$="' + me.opts.cboAuthors + '"]')[0],
            cboStatus = $('select[id$="' + me.opts.cboStatus + '"]')[0],
            tbl = $('table[id$="' + me.opts.pagesListTable + '"]')[0],
            rows = $('tbody tr', tbl),
            objData = [],
            f1Data = [],
            f2Data = [];

        //build a data object with all pages details
        $.each(rows, function (i, r) {
            var newObj = {},
                pageid = $(this).attr('pageid'),
                authorid = $(this).attr('authorid'),
                statusid = $(this).attr('statusid');

            newObj.pageid = pageid;
            newObj.authorid = authorid;
            newObj.statusid = statusid;

            objData.push(newObj);
        });

        //console.log(objData);

        //initially hide all rows and reset filtered data objects (f1Data, f2Data)
        $(rows).hide();
        f1Data = [];
        f2Data = [];

        //build a filtered data object by author
        if ($(cboAuthors).val() == "-1") {
            $.each(objData, function (i, o) {
                f1Data.push(o);
            });
        }
        else {
            var m_authorid = $(cboAuthors).val();
            $.each(objData, function (i, o) {
                if (o.authorid == m_authorid) {
                    f1Data.push(o);
                }
            });
        }

        //build a filtered data object by status
        if ($(cboStatus).val() == "-1") {
            $.each(f1Data, function (i, o) {
                f2Data.push(o);
            });
        }
        else {
            var m_statusid = $(cboStatus).val();
            $.each(f1Data, function (i, o) {
                if (o.statusid == m_statusid) {
                    f2Data.push(o);
                }
            });
        }

        //loop through f2Data and show matching pages' rows
        $.each(f2Data, function (i, o) {
            $.each(rows, function (n, r) {
                if ($(this).attr('pageid') == o.pageid) {
                    $(r).show();
                }
            });
        });

        //console.log($(cboAuthors).val());
        //console.log($(cboStatus).val());

        //console.log(f1Data);
        //console.log(f2Data);
    },

    //remove editor instance
    removeEditorInstance: function () {
        if (typeof ((tinyMCE.activeEditor)) != undefined) {
            var m_err1 = null,
                m_err2 = null;

            try {
                (tinyMCE.activeEditor).remove();
            }
            catch (err1) {
                m_err1 = "An exception occurred in the script.  Error message: " + err1.message;
            }

            try {
                (tinyMCE.activeEditor).destroy();
            }
            catch (err2) {
                m_err2 = "An exception occurred in the script. Error message: " + err2.message;
            }

            if (typeof (console) != "undefined" && console.log) {
                if (m_err1) {
                    console.log(m_err1);
                }

                if (m_err2) {
                    console.log(m_err2);
                }
            }
        }
        return true;
    },

    //force hide dropdowns
    forceHideDropdowns: function(){
        var me = this,
            n = 0;

        //Format Select
        $('div[id$="formatselect_menu"]').each(function (i, d) {
            var d1 = true;
            if (d1 && ($(d).css('display') == "block")) {
                $(d).hide();
                d1 = false;
                n = n + 1;

                $('div[id$="formatselect_menu_co"]', $(this)).each(function (a, b) {
                    var d2 = true;
                    if (d2 && ($(b).css('display') == "block")) {
                        $(b).hide();
                        d1 = false;
                        n = n + 1;
                    }
                });
            }
        });

        //Font Select
        $('div[id$="fontselect_menu"]').each(function (i, d) {
            var d1 = true;
            if (d1 && ($(d).css('display') == "block")) {
                $(d).hide();
                d1 = false;
                n = n + 1;

                $('div[id$="fontselect_menu_co"]', $(this)).each(function (a, b) {
                    var d2 = true;
                    if (d2 && ($(b).css('display') == "block")) {
                        $(b).hide();
                        d1 = false;
                        n = n + 1;
                    }
                });
            }
        });

        //Font Size Select
        $('div[id$="fontsizeselect_menu"]').each(function (i, d) {
            var d1 = true;
            if (d1 && ($(d).css('display') == "block")) {
                $(d).hide();
                d1 = false;
                n = n + 1;

                $('div[id$="fontsizeselect_menu_co"]', $(this)).each(function (a, b) {
                    var d2 = true;
                    if (d2 && ($(b).css('display') == "block")) {
                        $(b).hide();
                        d1 = false;
                        n = n + 1;
                    }
                });
            }
        });

        if(n > 0){
            me.removeEditorInstance();
            me.initTextEditor();
            __doPostBack();
        }

        return true;
    },

    //initialization function
    init: function () {
        var me = this;
        $(document).ready(function () {
            me.initTextEditor();
            me.bindPageListOnClick();
            me.checkDeleteBtn();

            //me.bindFilterOnChange();
        });
    }
};