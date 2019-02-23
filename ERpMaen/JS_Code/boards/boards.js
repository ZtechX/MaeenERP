/************************************/
// Created By : Ahmed Nayl
// Create Date : 10/10/2018 3:30 PM
// Description : This file contains all javaScript functions related to travel_agencies form 
/************************************/


//public variables
var deleteWebServiceMethod = "boards.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "boards.asmx/Edit";
var formAutoCodeControl = "lblunitsid";
// load function set defualt values
var index_i = 0;
var w = 0;
var arr_id = [];
$(function () {
    
    try {
        form_load();
        $("#pnlConfirm").hide();
        $("#divData").show();
        $("#SavedivLoader").hide();
     
        
   
    } catch (err) {
        alert(err);
    }
});
function get_admin() {
    companies.get_admin(function (val) {
        console.log(val);
        if (val[0] != 0) {
            var project = JSON.parse(val[0]);
            var users = JSON.parse(val[1]);
            fillControlsFromJson(project[0]);
            $("#user_name").val(users[0].User_Name);
            $("#user_password").val(users[0].User_Password);
        }
        if (val[2] == 2) {
            $("#Li2").css("display", "block");
            $("#Li1").css("display", "block");
            $("#clearBtn").css("display", "inline-block");
            form_load();
        }

    });

}

// draw dynamic table for existing travel_agencies
function drawDynamicTable() {
    try {
        var tableSortingColumns = [
            { orderable: false }, null, null,
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" }, 
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 1;
        loadDynamicTable('boards', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch (err) {
        alert(err);
    }
}

// empty pnlform and show it and hide function panel
//function add() {
//    try {
//        prepareAdd();
//        resetAll();
//    } catch (err) {
//        alert(err);
//    }
//}

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
        debugger;
        if (Page_ClientValidate("vgroup")) {
            var saveType = $("#cmdSave").attr("CommandArgument");
            var PosId = $("#lblmainid").html();
            var basicData = generateJSONFromControls("divForm");
            boards.save( PosId, basicData, function (val) {
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
            $("#imgItemURL").attr("src", data[0].image);
            //  $("#tblUploadedFiles tr").remove();

            // alert(val[2].Image_name);
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
            $('.action-btns').css('display', 'none');

            showSuccessMessage("");
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
