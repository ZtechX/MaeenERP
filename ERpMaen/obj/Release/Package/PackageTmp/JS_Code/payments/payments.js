/************************************/
// Created By : Ahmed Nayl
// Create Date : 10/10/2018 3:30 PM
// Description : This file contains all javaScript functions related to travel_agencies form 
/************************************/


//public variables
var deleteWebServiceMethod = "payments.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "payments.asmx/Edit";
var formAutoCodeControl = "lblunitsid";
// load function set defualt values
var index_i = 0;
var w = 0;
var arr_id = [];
$(function () {
    try {
        form_load();
    
    } catch (err) {
        alert(err);
    }
});


function get_member_money() {
    var member_id = $("#member_id").val();
    payments.get_member_money(member_id, function (val) {
        if (val[0] != 0) {
            var data = JSON.parse(val[0]);
            $("#amount_money").val(data[0].amount_money)
        }

    });

}



// draw dynamic table for existing travel_agencies
function drawDynamicTable() {
    try {
        var tableSortingColumns = [
            { orderable: false }, null, null,null,null,
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, 
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 1;
        loadDynamicTable('payments', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch (err) {
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

function resetAll() {
    try {
        resetFormControls();
        $("#lblunitsid").html("");
        $("#divuploadimage img").prop("src", "");
        $("#imgItemURL").prop("src", "");
    } catch (err) {
        alert(err);
    }
}

function save() {
    try {
        if (Page_ClientValidate("vgroup")) {
            var saveType = $("#cmdSave").attr("CommandArgument");
            $("#lbldate_m").html($("#divdate2 #txtDatem").val());
            $("#lbldate_hj").html($("#divdate2 #txtDateh").val());
            var PosId = $("#lblmainid").html();
            var basicData = generateJSONFromControls("divForm");
            payments.save( PosId, basicData,function (val) {
                if (val != '0') {
                    cancel();
                    resetAll();
                    drawDynamicTable();
                    showSuccessMessage('تم تسجيل البيانات بنجاح');

                } else {
                    showErrorMessage('لم يتم الحفظ');
                }
            });
        }
        else {
            showErrorMessage("يرجى ادخال البيانات المطلوبه");
        }
    } catch (err) {
        alert(err);
    }
}

function edit(val) {
    try {
        var data = JSON.parse(val[1]);
        cancel();
        if (val[0] != "0") {
            fillControlsFromJson(data[0]);
          $("#divdate2 #txtDatem").val(data[0].date_m);
            $("#divdate2 #txtDateh").val(data[0].date_hj);
            //  $("#tblUploadedFiles tr").remove();

            // alert(val[2].Image_name);
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
            $('.action-btns').css('display', 'none');

            showSuccessMessage("data select");
            if (formOperation == "update") {
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

function add() {
    try {
        prepareAdd();
        resetAll();
    } catch (err) {
        alert(err);
    }
}

function setformforupdate() {
    try {
        setformforupdate_all();
    } catch (err) {
        alert(err);
    }
}