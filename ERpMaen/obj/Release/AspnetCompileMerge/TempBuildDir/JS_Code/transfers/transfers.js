/************************************/
// Created By : Ahmed Nayl
// Create Date : 16/11/2015 11:30 AM
// Description : This file contains all javaScript functions related to travel_agencies form 
/************************************/

// load function set defualt values
$(function () {
    try {
        form_load();
    } catch (err) {
        alert(err);
    }
});

// draw dynamic table for existing travel_agencies
function drawDynamicTable() {
    try {
        var tableSortingColumns = [
                { orderable: true}, null, null, null, null, null, null, null,
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" },
        ];
        
        var tableColumnDefs = [
            
        ];
        var initialSortingColumn = 0;
        loadDynamicTable('transfers', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch(err) {
        alert(err);
    }
}

// empty pnlform and show it and hide function panel
function add() {
    try {
        prepareAdd();
        resetAll();
    } catch (err) {
        alert(err);
    }
}

// reset all controls when add or cancel
function resetAll() {
    try {
        resetFormControls();
        $("#lbltransfersId").html("");
    } catch (err) {
        alert(err);
    }
}


// showInvoice
function showInvoice() {
    try {
        if ($("#lbltransfersId").html() != "") {
            window.open("../Invoice.aspx?code=" + $("#lbltransfersId").html() + "&noteid=" + $("#lbltransfersId").html() + "&operation=search", "", "width=800,height=500");
        } else {
            showErrorMessage("من فضلك اختار الحركة !!!");
        }
    } catch (err) {
        alert(err);
    }
}

