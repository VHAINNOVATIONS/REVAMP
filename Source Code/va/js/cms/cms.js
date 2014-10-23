var cms = {
    //general options and settings
    opts: {},

    //allow only numbers in textbox
    onlyNumbers: function (obj) {
        var me = this,
        strValue = obj.value;

        strValue = strValue.replace(/\D/gi, '');
        obj.value = strValue;
    }
};