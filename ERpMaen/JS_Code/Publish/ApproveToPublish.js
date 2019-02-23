/************************************/
// Created By : Mostafa Abdelghffar 
// Create Date : 8/7/2015 3:30 PM
// Description : This file contains all javaScript functions related to approve to publish 
/************************************/

// load function set defualt values
$(function () {
    try {
        checkLanguage();
        drawDynamicTable();
    } catch (err) {
        alert(err);
    }
});

// draw dynamic table for ApproveToPublish
function drawDynamicTable() {
    try {
        var tableSortingColumns = [
                { orderable: false }, null, null, null, null,
                null, null, null, null, null,
        ];
        var tableFilteringColumns = [
            { type: "null" },{ type: "text" }, { type: "text" }, { type: "number-range" },
            { type: "number-range" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" },
        ];
        var tableColumnDefs = [
        ];
        var initialSortingColumn = 0;
        loadDynamicTable("ApproveToPublish", "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Publish/UnPublish");
    } catch(err) {
        alert(err);
    }
}