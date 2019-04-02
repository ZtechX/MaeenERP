
var deleteWebServiceMethod = "orders.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "orders.asmx/Edit";
var formAutoCodeControl = "lblmainid";
var superAdmin = false;
$(function () {

    try {
        $("#SavedivLoader").show();
                form_load();
    } catch (err) {
        alert(err);
    }
});

function ChangeOrderStatus(id,Set_val) {

    try {
        if (Set_val && $("#userloginType").html() == "6") {

        }
        $("#SavedivLoader").show();
        orders.Save(id, Set_val, function (val) {
            debugger
            if (val =="True") {
                drawDynamicTable();
                if (Set_val) {
                    showSuccessMessage("تمت الموافقة على الطلب");

                } else {
                    showSuccessMessage("تم رفض الطلب");
                }
            } else {
                showErrorMessage(val.split("|")[1]);
            }
                      
                $("#SavedivLoader").hide();
            });
        
    } catch (err) {
        alert(err);
    }
}

function drawDynamicTable() {
    try {
        var tableSortingColumns = [
            { orderable: false }, null, null, null, null, null, null, null
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" },{ type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 0;
        loadDynamicTable('orders', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch (err) {
        alert(err);
    }
}

function add() {
    try {
        prepareAdd();
        resetAll();
    } catch (err) {
        alert(err);
    }
}

function resetAll() {
    try {
        resetFormControls();
    } catch (err) {
        alert(err);
    }
}