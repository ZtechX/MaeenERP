/************************************/
// Created By : Ahmed Nayl
// Create Date : 14/10/2018 3:30 PM
// Description : This file contains all javaScript functions related to Companies form 
/************************************/


//public variables
var deleteWebServiceMethod = "contact.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "contact.asmx/Edit";
var formAutoCodeControl = "lblmainid";
// load function set defualt values
var clean = true;
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
                { orderable: false }, null, null, null, null, null,
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" },{ type: "text" }, { type: "text" }, { type: "text" }, { type: "text" },
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 1;
        loadDynamicTable('contact', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
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
        $("#lblmainid").html("");
        $("#divuploadimage img").prop("src", "");
        $("#imgItemURL").prop("src", "");
    } catch (err) {
        alert(err);
    }
}

function save() {
    
    try {
        $("input").removeClass('error');
        $("select").removeClass('error');
        if (!checkRequired()) {
            var basicData = generateJSONFromControls("divForm");
            var saveType = $("#cmdSave").attr("CommandArgument");
            var PosId = $("#lblmainid").html();
            contact.Save(PosId, basicData, function (val) {
                if (val == true) {
                        cancel();
                        resetAll();
                        drawDynamicTable();

                    showSuccessMessage("تم الحفظ بنجاح");
                } else {
                    showErrorMessage(val.split("|")[1]);
                }
            });
        } else {
            showErrorMessage("يرجى ادخال البيانات المطلوبه");
        }
    } catch (err) {
        alert(err);
    }
}



// called after update function success
function edit(val) {
    resetAll();
    try {
        var data = JSON.parse(val[1]);
        if (val[0] != "0") {
            fillControlsFromJson(data[0]);
            
            if (data[0].image != ""  && data[0].image != null) {
                $("#divuploadimage img").prop("src", data[0].image);
                $("#imgItemURL").prop("src", data[0].image);
            } else {
                $("#divuploadimage img").prop("src", "");
                $("#imgItemURL").prop("src", "");
            }
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#lblmainid").val( data[0].id);
            if (formOperation == "update") {
                setformforupdate();
                formOperation = "";
            }
            //implementers.GetWorkerInfo(data[0].id, function (val) {
            //    if (val[0] == "1") {
            //        var data = JSON.parse(val[1]);
            //        $("#user_nm").val(data[0].User_Name);
            //        $("#password").val(data[0].User_Password);
            //    } else {
            //        $("#user_nm").val("");
            //        $("#password").val("");
            //    }
            //});
        } else {
            showErrorMessage("No data found !!");
        }
    } catch (err) {
        alert(err);
    }
}
