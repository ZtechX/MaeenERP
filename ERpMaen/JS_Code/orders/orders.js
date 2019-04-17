
var deleteWebServiceMethod = "orders.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "orders.asmx/Edit";
var formAutoCodeControl = "lblmainid";
var superAdmin = false;
var selected_id = "";
$(function () {
  //  window.alert = function () { };
    try {
        $("#SavedivLoader").show();
               form_load();
    } catch (err) {
        alert(err);
    }
});
function saveNewDate() {
    var date_m = $("#newDate").find("#txtDatem").val();
    var date_h = $("#newDate").find("#txtDateh").val();
    if (date_m != "" && date_h != "") {
        saveAction(true, date_m, date_h);
    } else {
        showErrorMessage("يجب اختيار تاريخ جديد");
    }
}
function ChangeOrderStatus(id,Set_val) {
    selected_id = id;
    try {
        if (Set_val && $("#userloginType").html() == "6") {
            $("#newDate").dialog({
                width: "800px",
            });
            return;
        }
        $("#SavedivLoader").show();
        saveAction(Set_val,"","");
        
    } catch (err) {
        alert(err);
    }
}
function saveAction(Set_val, dt_m, dt_h) {
    
    orders.Save(selected_id, Set_val, dt_m, dt_h, function (val) {
        
        if (val == "True") {
            drawDynamicTable();
            if (Set_val) {
                if (dt_m != "") {
                    alert("تمت الموافقة على الطلب");
            }
                else {
                    showSuccessMessage("تمت الموافقة على الطلب");
                }

                showSuccessMessage("تم رفض الطلب");
            }
        } else {
            if (dt_m != "") {
                alert(val.split("|")[1]);
            } else {
                showErrorMessage(val.split("|")[1]);
            }
        }

        $("#SavedivLoader").hide();
    });
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