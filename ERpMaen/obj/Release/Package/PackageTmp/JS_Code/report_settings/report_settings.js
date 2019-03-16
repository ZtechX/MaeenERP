/************************************/
// Created By : Ahmed Nayl
// Create Date : 14/10/2018 3:30 PM
// Description : This file contains all javaScript functions related to Companies form 
/************************************/


//public variables
var deleteWebServiceMethod = "report_settings.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "report_settings.asmx/Edit";
var formAutoCodeControl = "lblmainid";
// load function set defualt values
var clean = true;
$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").show();
    try {
        get_report();


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
            { type: "null" }, { type: "text" },{ type: "text" },
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 1;
        loadDynamicTable('report_settings', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch (err) {
        alert(err);
    }
}
function get_report() {
    report_settings.get_report(function (val) {
        if (val != 0) {
            var data = JSON.parse(val[0]);

            $("#ddltype_id").val(data[0].type_id);
            $("#lblmainid").html(data[0].id);
            if (data[0].type_id != 6) {
                $("#type_images").css("display", "block");
                $("#imgItemURL").attr("src", data[0].image);
            } else {
                $("#type_images").css("display", "none");
            }
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
        }

    });

}

// empty pnlform and show it and hide function panel


function resetAll() {
    try {
        resetFormControls();
        $("#lblmainid").html("");
        $("#divuploadimage img").prop("src", "");
        $("#imgItemURL").prop("src", "");
    } catch (err) {
        alert(err);
    }
}

function save() {
    
    try {
        if (Page_ClientValidate("vgroup")) {
            debugger;
            var saveType = $("#cmdSave").attr("CommandArgument");
            var mainImag = $("#imgItemURL").prop("src");
            var PosId = $("#lblmainid").html();
            var basicData = generateJSONFromControls("divForm");
            report_settings.save( PosId, basicData, mainImag, function (val) {
                if (val != '0') {
                    showSuccessMessage('تم تسجيل البيانات بنجاح');
                  //  drawDynamicTable();
                    cancel();
                    get_report();
                } else {
                    showErrorMessage('لم يتم الحفظ');
                }
            });
        }
        else {
            showErrorMessage("يرجى ادخال البيانات المطلوبه");
            $("#pnlConfirm").show();
            $("#SavedivLoader").hide();
        }
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
            if (data[0].type_id != 6) {
                $("#type_images").css("display", "block");
                $("#imgItemURL").attr("src", data[0].image);
            } else {
                $("#type_images").css("display", "none");
            }
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');

            //showSuccessMessage("");
            //if (formOperation == "update") {
            //    setformforupdate();
            //    formOperation = "";
            //}
        } else {
            showErrorMessage("No data found !!");
        }
    } catch (err) {
        alert(err);
    }
}
function get_types() {
    if ($("#ddltype_id").val() != '6') {
        $("#type_images").css("display", "block");

    } else {
        $("#type_images").css("display", "none");
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