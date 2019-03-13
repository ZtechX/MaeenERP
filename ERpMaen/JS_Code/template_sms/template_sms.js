/************************************/
// Created By : Ahmed Nayl
// Create Date : 14/10/2018 3:30 PM
// Description : This file contains all javaScript functions related to Companies form 
/************************************/


//public variables
var deleteWebServiceMethod = "template_sms.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "template_sms.asmx/Edit";
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
                { orderable: false }, null, null,
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" },{ type: "text" },
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 1;
        loadDynamicTable('template_sms', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
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

          //  var letters=getnum($("#letter_count").html());
            //template_sms.get_Limit(function (val) {
            //    if (val != "") {
            //        var arr = JSON.parse(val);
            //        var L_count = Number(arr[0].max_char);
            //        if (L_count < letters) {
            //            showErrorMessage("أقصى حد للحروف " + L_count);
            //            return;
            //        }
            //    }
                template_sms.Save(PosId, basicData, function (val) {
                    if (val == true) {
                        cancel();
                        resetAll();
                        drawDynamicTable();

                        showSuccessMessage("تم الحفظ بنجاح");
                    } else {
                        showErrorMessage(val.split("|")[1]);
                    }
                });
            //});
           
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
 
        } else {
            showErrorMessage("No data found !!");
        }
    } catch (err) {
        alert(err);
    }
}



function SetLetterCount() {
    $("#letter_count").html($("#txtbody").val().length + " حرف");
}
function getnum(str) {
    var num = str.match(/\d/g);
    num = num.join("");
    return num;
}