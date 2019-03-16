/************************************/
// Created By : Ahmed Nayl & Mostafa Abdelghffar
// Create Date : 16/6/2015 10:00 PM
// Description : This file contains all javaScript functions resopsable submit,edite ,delete  html table
/************************************/

var allArrJsonDetails = [];

// create json array from controls and reset form controls
function submitDetails(controlsDivId, columnsArrayJson, dbcolumnsArrayJson, divDrawId, tableDrawId,submitButtonId, requiredControls, passedLblResId) {
    try {
        var lblResId = "";
        if (jQuery.isEmptyObject(passedLblResId) == false) {
            lblResId = passedLblResId;
        }
        if (checkRequiredControls(requiredControls, lblResId)) {
            var arrJSON = getDivArrJSON(controlsDivId);
            var details = generateJSONFromControls(controlsDivId);
            var rowIndex = "";
            if ($("#" + submitButtonId).attr("saveType") == "Update") {
                rowIndex = $("#" + submitButtonId).attr("rowIndex")
                arrJSON[rowIndex] = details;
            } else {
                arrJSON.push(details);
            }
            setDivArrJSON(controlsDivId, arrJSON);
            drawDetailsTable(controlsDivId, divDrawId, tableDrawId, columnsArrayJson, dbcolumnsArrayJson, arrJSON, submitButtonId);
            resetDivControls(controlsDivId);
            $("#" + submitButtonId).attr("saveType", "Add");
            $("#" + passedLblResId).hide();
        }
    } catch (err) {
        alert(err);
    }
}

// check if required controls is filled with data or not
function checkRequiredControls(controls, lblResId) {
    try {
        var message = "";
        var control;
        $.each(controls, function (index, obj) {
            control = $("#" + obj.ControlId);
            var tagName = $(control).prop('tagName');
            var value = "";
            if (tagName == "SPAN" || tagName == "DIV") {
                if (tagName == "SPAN" && $(control).find('input:checkbox').length > 0) {
                    if ($(control).find('input:checkbox').prop("checked") == false) {
                        message = obj.Message;
                        return false;
                    }
                } else {
                    if ($(control).html() == "") {
                        message = obj.Message;
                        return false;
                    }
                }
            } else if (tagName == "INPUT") {
                var type = $(control).prop('type');
                if (type == "checkbox") {
                    if ($(control).prop("checked") == false) {
                        message = obj.Message;
                        return false;
                    }
                } else {
                    if ($(control).val() == "") {
                        message = obj.Message;
                        return false;
                    }
                }
            } else if (tagName == "TEXTAREA") {
                if ($(control).val() == "") {
                    message = obj.Message;
                    return false;
                }
            } else if (tagName == "SELECT") {
                if ($(control).val() == "0") {
                    message = obj.Message;
                    return false;
                }
            } else if (tagName == "TABLE") {
                var checked = false;
                $(control).find("tbody tr input:radio").each(function () {
                    if ($(this).prop("checked") == true)
                        checked = true;
                });
                if (checked == false) {
                    message = obj.Message;
                    return false;
                }
            }
        });
        if (message != "") {
            if (lblResId != "") {
                showPopUpErrorMessage(lblResId, message);
            } else {
                showErrorMessage(message);
            }
            $(control).focus();
            return false;
        } else {
            return true;
        }
    } catch (err) {
        alert(err);
    }
}

// draw html table 
function drawDetailsTable(controlsDivId, divDrawId, tableDrawId, columnsArrayJson, dbColumnsArrayJson, arrJSON, submitButtonId, lblResId) {
    try {
        $("#"+divDrawId).empty();
        if (arrJSON.length > 0) {
            createTable(tableDrawId, divDrawId, columnsArrayJson);
            for (i = 0; i < arrJSON.length; i++) {
                if (arrJSON[i].IsDeleted != "True") {
                    appendDetailsTR(arrJSON[i], i, tableDrawId, dbColumnsArrayJson, controlsDivId, submitButtonId);
                }
            }
        }
    } catch (err) {
        alert(err);
    }
}

// appen rows to table
function appendDetailsTR(trDetails, i, tableDrawId, dbColumnsArrayJson, controlsDivId, submitButtonId) {
    try {
        var index = $('#'+tableDrawId+' tbody tr').length;
        var tr = document.createElement('tr');
        tr.id = trDetails.rowId;
        $.each(dbColumnsArrayJson, function (index, obj) {
            if (trDetails[obj] == null) {
                trDetails[obj] = '';
            }
            $(tr).append('<td><span id="lbl' + obj + '" >' + trDetails[obj] + '</span></td>');
        });
        $(tr).append("<td><input id='cmdEditDetails' submitButtonId='" + submitButtonId+"' divId='" + controlsDivId + "' onclick='editDetails(this); return false;' type='button' value='Edit'></input></td>");
        $(tr).append("<td><input id='cmdDeleteDetails' submitButtonId='" + submitButtonId + "' divId='" + controlsDivId + "'  onclick='deleteDetails(this); return false;' type='button' value='Delete'></input></td>");
        $(tr).append('<span style="display:none" id="lblIsDeleted" ></span>');
        $('#' + tableDrawId + ' tbody').append(tr);
    } catch (err) {
        alert(err);
    }
}

// delete Details row from html table
function deleteDetails(sender) {
    try {
        var index = $(sender).closest("tr")[0].rowIndex;
        var rowId = $(sender).closest("tr").attr("id");
        var arrJSON = getDivArrJSON($(sender).attr("divId"));
        submitButtonId = $(sender).attr("submitButtonId");
        $("#" + submitButtonId).attr("saveType", "Add");
        resetDivControls($(sender).attr("divId"));
        if (rowId == "") {
            arrJSON.splice(index - 1, 1);
            $(sender).closest("tr").remove();
        } else {
            $(sender).closest("tr").find("#lblIsDeleted").html("True");
            $(sender).closest("tr").css("display", "none");
            arrJSON[index - 1].IsDeleted = "True";
        }
        setDivArrJSON($(sender).attr("divId"), arrJSON);
        
    } catch (err) {
        alert(err);
    }
}

// edit Details row from html table into controls
function editDetails(sender) {
    try {
        var index = $(sender).closest("tr")[0].rowIndex;
        var arrJSON = getDivArrJSON($(sender).attr("divId"));
        submitButtonId = $(sender).attr("submitButtonId");
        $("#" + submitButtonId).attr("rowIndex", index - 1);
        $("#" + submitButtonId).attr("saveType", "Update");
        //$("#txtcurrentcommunity").val(arrJSON[index - 1].Community);
        //$("#txtcurrentsubcommunity").val(arrJSON[index - 1].SubCommunity);
        //$("#txtcurrentBuildingName").val(arrJSON[index - 1].BuildingName);
        //getDllDataAfterEdit();
        fillControlsFromJson(arrJSON[index - 1], $(sender).attr("divId"));
        GetDataWithSelectid($("#ddlUnitCity").val(), 'ddlCommunity', 'UC', 'C', arrJSON[index - 1].Community);
        GetDataWithSelectid(arrJSON[index - 1].Community, 'ddlSubCommunity', 'US', 'UC', arrJSON[index - 1].SubCommunity);
        GetDataWithSelectidForBuilding(arrJSON[index - 1].Community, arrJSON[index - 1].SubCommunity, 'ddlBuilding', arrJSON[index - 1].BuildingName);

    } catch (err) {
        alert(err);
    }
}

// get json array by divid
function getDivArrJSON(controlsDivId) {
    try {
        var arrJSON = [];
        $.each(allArrJsonDetails, function (index, obj) {
            if (obj.ControlsDivId == controlsDivId) {
                arrJSON = obj.ArrJSON;
                return;
            }
        });
        return arrJSON;
    } catch (err) {
        alert(err);
    }
}

// fill div with data by divid
function setDivArrJSON(controlsDivId, arrJSON) {
    try {
        $.each(allArrJsonDetails, function (index, obj) {
            if (obj.ControlsDivId == controlsDivId) {
                obj.ArrJSON = arrJSON;
                return;
            }
        });
    } catch (err) {
        alert(err);
    }
}

// empty div json array
function emptyDivArrJSON(controlsDivId) {
    try {
        var arrJSON = [];
        $.each(allArrJsonDetails, function (index, obj) {
            if (obj.ControlsDivId == controlsDivId) {
                obj.ArrJSON = arrJSON;
                return;
            }
        });
    } catch (err) {
        alert(err);
    }
}