/************************************/
// Created By : Mostafa Abdelghffar
// Create Date : 23/5/2015 12:00 PM
// Description : This file contains all javaScript functions for showing messages
/************************************/

// show default success message
function showSuccessMessage(message) {
    try {
        debugger
        showMessage(message, "divRes", "lblRes", "green");
        $("#lblResError").hide();
    } catch (err) {
        alert(err);
    }
}
function showAlarmMessage(message) {
    try {
        showMessage(message, "divRes", "lblRes", "yellow");

    } catch (err) {
        alert(err);
    }
}
// show default error message
function showErrorMessage(message) {
    try {
        showMessage(message, "divRes", "lblRes", "red");
        $("#lblResError").removeClass();
        $("#lblResError").addClass("res-label-error");
        $("#lblResError").html(message);
        $("#lblResError").show();
    } catch (err) {
        alert(err);
    }
}

// show success message for specific controls
function showModifiedSuccessMessage(message, divId, lblResId) {
    try {
        showMessage(message, divId, lblResId, "green");
    } catch (err) {
        alert(err);
    }
}

// show error message for specific controls
function showModifiedErrorMessage(message, divId, lblResId, lblResErrorId) {
    try {
        showMessage(message, divId, lblResId, "red");

        $("#" + lblResErrorId).removeClass();
        $("#" + lblResErrorId).addClass("res-label-error");
        $("#" + lblResErrorId).html(message);
        $("#" + lblResErrorId).show();
    } catch (err) {
        alert(err);
    }
}

// show message
function showMessage(message, divId, lblResId, color) {
    try {
        $("#" + divId).show();
        $("#" + divId).css("background-color", color);
        $("#" + lblResId).html(message);
        $("#" + divId).fadeIn(3000);
        $("#" + divId).fadeOut(3000);
        $("#" + lblResId).show();
    } catch (err) {
        alert(err);
    }
}