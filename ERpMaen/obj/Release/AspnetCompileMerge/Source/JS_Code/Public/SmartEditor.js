/************************************/
// Created By : Mostafa Abdelghffar
// Create Date : 23/5/2015 12:00 PM
// Description : This file contains all javaScript functions for smart editor
/************************************/

// set the smart editor control to description control
bkLib.onDomLoaded(function () {
    area1 = new nicEditor({ fullPanel: true }).panelInstance('txtPropDesc', { hasPanel: true });
    area2 = new nicEditor({ fullPanel: true }).panelInstance('txtPropInfo', { hasPanel: true });
});

//  take the value of description and set it in smart editor and  show popup
function showSmartEditorPopup(control) {
    //alert($(".nicEdit-main").length);
    if ($(".nicEdit-main").length == 0) {
        area1 = new nicEditor({ fullPanel: true }).panelInstance('txtPropDesc', { hasPanel: true });
    }
    var ctrl = "#" + control;
    var isDisabled = $(ctrl).prop('disabled');
    if (isDisabled == false) {
        $("#txtpropedit").dialog({ modal: true, title: 'Smart Editor', show: 'slide', width: 600 });
        $(".nicEdit-main").html($("#divDescription").html());
    }
}

function showMultiSmartEditorPopup(control,divId) {
    //alert($(".nicEdit-main").length);
    if ($(".nicEdit-main").length == 0) {
        area1 = new nicEditor({ fullPanel: true }).panelInstance('txtPropDesc', { hasPanel: true });
    }
    var ctrl = "#" + control;
    var isDisabled = $(ctrl).prop('disabled');
    if (isDisabled == false) {
        $("#lblCurrentEditorDiv").html(divId);
        $("#txtpropedit").dialog({ modal: true, title: 'Smart Editor', show: 'slide', width: 600 });
        $(".nicEdit-main").html($("#"+ divId).html());
    }
}

//  take value of smart editor and set it in description and hide popup
function hideSmartEditorPopup() {
    var x = $("#txtpropedit .nicEdit-main").html();
    document.getElementById("divDescription").innerHTML = x;
    $("#txtpropedit").dialog("close");
}
//  take value of smart editor and set it in description and hide popup
function hideMultiSmartEditorPopup() {
    var x = $("#txtpropedit .nicEdit-main").html();
    var currentid = $("#lblCurrentEditorDiv").html();
    document.getElementById(currentid).innerHTML = x;
    $("#txtpropedit").dialog("close");
}

function showSmartEditorPopupInfo(control) {
    var ctrl = "#" + control;
    var isDisabled = $(ctrl).prop('disabled');
    if (isDisabled == false) {
        $("#txtpropedit1").dialog({ modal: true, title: 'Smart Editor', show: 'slide', width: 600 });
        $(".nicEdit-main").html($("#txtProkDetMainInfo").html());
    }
}

//  take value of smart editor and set it in description and hide popup
function hideSmartEditorPopupInfo() {
    var x = $("#txtpropedit1 .nicEdit-main").html();
    document.getElementById("txtProkDetMainInfo").innerHTML = x;
    $("#txtpropedit1").dialog("close");
}