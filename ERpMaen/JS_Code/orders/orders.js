
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
        $("#SavedivLoader").show();
        orders.Save(id, Set_val, function (val) {
            debugger
                if (val) {
                    if (Set_val) {
                        showSuccessMessage("تمت الموافقة على الطلب");
                    } else {
                        showSuccessMessage("تم رفض الطلب");
                    }
                        drawDynamicTable();
                } else {
                    showErrorMessage("حدث خطا ولم يتم تسجيل الاستجابة");
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
            { orderable: false }, null, null, null, null, null, null
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" },{ type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }
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