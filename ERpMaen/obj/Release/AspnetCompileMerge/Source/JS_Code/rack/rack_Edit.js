/************************************/
// Created By : Ahmed Nayl
// Create Date : 16/11/2015 12:00 PM
// Description : This file contains all javaScript functions resopsable for editing travel_agencies
/************************************/

// global variables used in deleteItem function
var deleteWebServiceMethod = "rack.asmx/Deleterack";

// global variable used in row_click and update functions
var editWebServiceMethod = "rack.asmx/Editrack";
var formAutoCodeControl = "lblrackId";

// enable pnl form for update
function setformforupdate() {
    try {
        setformforupdate_all();
    } catch (err) {
        alert(err);
    }
}

// called after update function success
function edit(val) {
    try {
        var data = JSON.parse(val[1]);
        cancel();
        if (val[0] != "0") {
            fillControlsFromJson(data[0]);
            showSuccessMessage("Record Selected");
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
            if (formOperation== "update") {
                setformforupdate();
                formOperation = "";
            }
        } else {
            showErrorMessage("No data found !!");
        }
    } catch (err) {
        alert(err);
    }
}

function getPayments() {
    try {

    } catch (err) {
        alert(err);
    }
}