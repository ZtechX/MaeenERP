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
                { orderable: false }, null, null,
        ];
        var tableFilteringColumns = [
            { type: "null" },{ type: "text" }, { type: "text" },
        ];
        
        var tableColumnDefs = [
            
        ];
        var initialSortingColumn = 1;
        loadDynamicTable('stors', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
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
        $("#lblstorsId").html("");
    } catch (err) {
        alert(err);
    }
}


