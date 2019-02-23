/************************************/
// Created By : Ahmed Nayl
// Create Date : 10/10/2018 3:30 PM
// Description : This file contains all javaScript functions related to travel_agencies form 
/************************************/


//public variables
var deleteWebServiceMethod = "members_abbsent.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "members_abbsent.asmx/Edit";
var formAutoCodeControl = "lblunitsid";
// load function set defualt values
var index_i = 0;
var w = 0;
var arr_id = [];
$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").hide(); pnlFunctions
    $("#pnlFunctions").hide();
    try {
    
    } catch (err) {
        alert(err);
    }
});
function get_member_session() {
    var session = $("#ddlsession").val();
    members_abbsent.get_member_session(session,function (val) {
        if (val[0] != 0) {
            var data = JSON.parse(val[0]);
            var results = "";
            var row = "";
            var results2 = "";
            var j = 1;
            var i = 1;
            $("#approved-body").html(results);
            for (var x = 0; x < data.length; x++) {

                if (data[x].abbsent == 1) {
                    //    alert('444444');
                    row = "<tr>" +
                        "<td>" + parseInt(i, 10) + "</td>" +
                        "<td>" +
                        "<input type='checkbox' name='trainer_id" +
                        "" +
                        "' value='" + data[x].member_id + "'>" +
                        "</td>" +
                        "<td>" + data[x].name + "</td>" +
                        "</tr>";
                    results2 = results2 + row;
                    i++;
                } else {
                    row = "<tr>" +
                        "<td>" + parseInt(j, 10) + "</td>" +
                        "<td>" +
                        "<input type='checkbox' name='trainer_id" +
                        "" +
                        "' value='" + data[x].member_id + "'>" +
                        "</td>" +
                        "<td>" + data[x].name
                    "</td>" +
                        "</tr>";
                    results = results + row;
                    j++;
                }
            }
            $("#trainner-body").html(results2);
            $("#approved-body").html(results);
        } else {
            $("#trainner-body").html("");
            $("#approved-body").html("");
        }

    });

}

function get_board_session() {
    var board_id = $("#board_id").val();
    members_abbsent.get_board_session(board_id, function (val) {
        if (val[0] != 0) {
            var data = JSON.parse(val[0]);
            var result = "<option value='0'>اختر</option>";
            for (var x = 0; x < data.length; x++) {
                result = result + "<option value=" + data[x].id+">"+data[x].name+"</option>";
            }
            $("#ddlsession").html(result);
        } else {
            $("#ddlsession").html("");
        }

    });

}
function abbsent() {
    if (Page_ClientValidate("vgroup")) {
        $("#SavedivLoader").show();
        $("#all_member").css("display", "none");
        var each_li = [];
        var session = $("#ddlsession").val();
        var attend = $('#approved-body > tr input[type="checkbox"][name="trainer_id"]:checked').length;
        if (attend == 0) {
            showErrorMessage("عفوا يجب ان تختار الاعضاء");
            $("#all_member").css("display", "block");
            $("#lblResError").hide();
            return;
        }
        $('#approved-body > tr input[type="checkbox"][name="trainer_id"]:checked').each(function () {
            var data = { "member_id": $(this).val() };
            each_li.push(data);
        });
        console.log(each_li);
        members_abbsent.abbsent(each_li, session, function (val) {
            if (val) {
                get_member_session();
                $("#all_member").css("display", "block");
                $("#lblResError").hide();
                showErrorMessage("تم بنجاح");
            }
            else {
                $("#all_member").css("display", "block");
                $("#lblResError").hide();
                showErrorMessage("فشل");
            }
        });
    } else {
        showErrorMessage("يرجى ادخال البيانات المطلوبه");
        $("#all_member").css("display", "block");
        $("#lblResError").hide();
    }

}
 
function disapproved() {
    if (Page_ClientValidate("vgroup")) {
        $("#some_member").css("display", "none");
        var each_li = [];
        var session = $("#ddlsession").val();
        var attend = $('#trainner-body > tr input[type="checkbox"][name="trainer_id"]:checked').length;
        if (attend == 0) {
            showErrorMessage("عفوا يجب ان تختار الاعضاء");
            $("#some_member").css("display", "block");
            $("#lblResError").hide();
            return;
        }
        $('#trainner-body > tr input[type="checkbox"][name="trainer_id"]:checked').each(function () {
            var data = { "member_id": $(this).val() };
            each_li.push(data);
        });
        members_abbsent.disapproved(each_li, session, function (val) {
            if (val) {
                get_member_session();
                $("#some_member").css("display", "block");
                showErrorMessage("تم بنجاح");
                $("#lblResError").hide();
            }
                   else {
                $("#some_member").css("display", "block");
                    showErrorMessage("فشل");
                    $("#lblResError").hide();
                }
            
        });
    } else {
        showErrorMessage("يرجى ادخال البيانات المطلوبه");
        $("#some_member").css("display", "block");
        $("#lblResError").hide();
    }


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

function save_members() {
    try {
        if (!checkRequired()) {
            var saveType = $("#cmdSave").attr("CommandArgument");
            var mainImag = $("#imgItemURL").prop("src");
            var PosId = $("#lblmainid").html();
            var basicData = generateJSONFromControls("divForm");
            members.save_members(saveType, PosId, basicData, mainImag, function (val) {
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

