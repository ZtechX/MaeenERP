/************************************/
// Created By : Ahmed Nayl
// Create Date : 10/10/2018 3:30 PM
// Description : This file contains all javaScript functions related to travel_agencies form 
/************************************/


//public variables
var deleteWebServiceMethod = "members.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "members.asmx/Edit";
var formAutoCodeControl = "lblunitsid";
// load function set defualt values
var index_i = 0;
var w = 0;
var arr_id = [];
$(function () {
    try {
        var result = "<option value='0'>اختراليوم</option>";
        for (var i = 1; i <= 30; i++) {
            result = result + "<option value="+i+">"+ i+"</option >";
        }
        $("#date_hj_day").html(result);
 var monthNames = [ "المحرم", "صفر", "ربيع الاول", "ربيع الاخر", "جماد الاول", "جماد الاخر",
            "رجب", "شعبان", "رمضان", "شوال", "ذى القعدة", "ذى الحجة"];
        var row = "<option value='0'>اخترالشهر</option>";

        for (var x = 0; x < monthNames.length; x++) {
            row = row + "<option value=" +parseInt(x+1)+ ">" + monthNames[x] + "</option >";
        }
        $("#date_hj_month").html(row);

     
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
            { orderable: false }, null, null,null,
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" },
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 1;
        loadDynamicTable('members', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
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
        debugger;
        if (Page_ClientValidate("vgroup")) {
            var saveType = $("#cmdSave").attr("CommandArgument");
            var mainImag = $("#imgItemURL").prop("src");
            var PosId = $("#lblmainid").html();
            var userId = $("#lbluser_id").html();
            var basicData = generateJSONFromControls("divForm");
            members.save_members( PosId, basicData, mainImag, userId, function (val) {
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
            debugger;
            fillControlsFromJson(data[0]);

            $("#imgItemURL").attr("src", data[0].image);
            //  $("#tblUploadedFiles tr").remove();

            // alert(val[2].Image_name);
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
            $('.action-btns').css('display', 'none');

            showSuccessMessage("تم جلب البيانات بنجاح");
            if (data[0].main == "1") {
                $("#divdate2 #txtDatem").val(data[0].date_m);
                $("#divdate2 #txtDateh").val(data[0].date_hj);
                $("#money").css("display", "block");
                $("#amount_money").prop('required', true);
            } else {
                $("#money").css("display", "none");
                $("#amount_money").prop('required', false);

            }
            if (data[0].system_enter == "1") {
                var data2 = JSON.parse(val[2]);
                $("#entering").css("display", "block");
                $("#user_name").prop('required', true);
                $("#user_password").prop('required', true);
                $("#user_name").val(data2[0].User_Name);
                $("#lbluser_id").html(data2[0].id);
                $("#user_password").val(data2[0].User_Password);

            }
            else {
                $("#entering").css("display", "none");
                $("#user_name").prop('required', false);
                $("#user_password").prop('required', false);
                $("#user_name").val("");
                $("#lbluser_id").html("");
                $("#user_password").val("");

            }
            //if (formOperation == "update") {
            //    setformforupdate();
            // //   formOperation = "";
            //}
        } else {
            showErrorMessage("No data found !!");
        }
        $("#pnlConfirm").hide();
        $("#divData").show();
        $("#SavedivLoader").hide();
    } catch (err) {
        alert(err);
    }
}
function get_main() {
    var maintance = $('#member_main').val();
    if ($('#member_main').is(":checked")) {
        $("#money").css("display", "block");
        $("#amount_money").prop('required', true);
        
    } else {
        $("#money").css("display", "none");
        $("#amount_money").prop('required', false);

    }
}

function get_enter() {
    var maintance = $('#maintance').val();
    if ($('#maintance').is(":checked")) {
        $("#entering").css("display", "block");
        $("#user_name").prop('required', true);
        $("#user_password").prop('required', true);
    } else {
        $("#entering").css("display", "none");
        $("#user_name").prop('required', false);
        $("#user_password").prop('required', false);

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