/************************************/
// Created By : Mostafa Abdelghffar
// Create Date : 4/6/2015 11:30 AM
// Description : This file contains all javaScript functions in Widgets form
/************************************/
 
// sort the widgets based on stored sorting in DB
$(function () {
    try {
        Widgets.GetSortedWidgets(function (sortedWidgetsStr) {
            if (sortedWidgetsStr == "") {
                var defaultSortedWidgets = [
                    { "DivId": "Stats", "ColumnNo": 0, "Height": $("#Stats .portlet").height() + 36, "Width": 665 },
                    { "DivId": "User", "ColumnNo": 0, "Height": $("#User .portlet").height() + 36, "Width": 665 },
                    { "DivId": "ExpiredRentContracts", "ColumnNo": 0, "Height": $("#ExpiredRentContracts .portlet").height() + 36, "Width": 665 },
                    { "DivId": "MyContacts", "ColumnNo": 1, "Height": $("#MyContacts .portlet").height() + 36, "Width": 665 },
                    { "DivId": "MyLeads", "ColumnNo": 1, "Height": $("#MyLeads .portlet").height() + 36, "Width": 665 }
                ];
                appendSortedWidgetsToColumns(defaultSortedWidgets);
            } else {
                var sortedWidgetsArrJson = JSON.parse(sortedWidgetsStr);
                appendSortedWidgetsToColumns(sortedWidgetsArrJson);
            }
        });
    } catch (err) {
        alert(err);
    }
});

// append widgets to columns based on the sorting arr Json
function appendSortedWidgetsToColumns(sortedWidgetsArrJson) {
    try {
        $(".column").empty();
        $.each(sortedWidgetsArrJson, function (index, obj) {
            $("#" + obj.DivId).find(".portlet").css("height", obj.Height);
            $("#" + obj.DivId).find(".portlet").css("width", obj.Width);
            $(".column").eq(obj.ColumnNo).append($("#" + obj.DivId));
        });
        $(".column").append("<div class='portlet'></div>");
    } catch (err) {
        alert(err);
    }
}

// save sortedWidgets arrOfJson into DB
function saveSortedWidgetsArrJsonToLocalStorage() {
    try {
        var sortedWidgetsArrJson = getCurrentSortedWidgetsArrJson();
        Widgets.SaveSortedWidgets(sortedWidgetsArrJson);
    } catch (err) {
        alert(err);
    }
}

// get arrOfJson that contains the order and sorting of the current widgets
function getCurrentSortedWidgetsArrJson() {
    try {
        var sortedWidgetsArrJson = [];
        $(".column").each(function (index) {
            $(this).find(".portlet").not(":last").each(function () {
                sortedWidgetsArrJson.push({
                    "DivId": $(this).closest(".divcol").attr("id"), "ColumnNo": index,
                    "Height": $(this).height(), "Width": $(this).width()
                });
            });
        });
        return sortedWidgetsArrJson;
    } catch (err) {
        alert(err);
    }
}