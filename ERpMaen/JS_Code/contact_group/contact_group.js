/************************************/
// Created By : Ahmed Nayl
// Create Date : 14/10/2018 3:30 PM
// Description : This file contains all javaScript functions related to Companies form 
/************************************/


//public variables
var deleteWebServiceMethod = "contact_group.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "contact_group.asmx/Edit";
var formAutoCodeControl = "lblmainid";
// load function set defualt values
var clean = true;
$(function () {
    try {
        $("#pnlConfirm").hide();

    } catch (err) {
        alert(err);
    }
});


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
        objects_arr = [];
        var group_id = $("#ddlgroup_id").val();
        if (!checkRequired()) {
            $('#invoices').find('tr').each(function () {
                var row = $(this);

                if (row.find('input[type="checkbox"]').is(':checked')) {
                    var data = { 'contact_id': row.find("#contact_id").html(), 'group_id': group_id }
                    objects_arr.push(data);
                }
            });
            var PosId = $("#lblmainid").html();
            contact_group.Save( PosId, objects_arr, function (val) {
                if (val == true) {
                    $('#invoices input:checkbox').prop("checked", false);
                    $('#invoices tr').css("background", "transparent");
                    $("#ddlgroup_id").val("0");
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
            implementers.GetWorkerInfo(data[0].id, function (val) {
                if (val[0] == "1") {
                    var data = JSON.parse(val[1]);
                    $("#user_nm").val(data[0].User_Name);
                    $("#password").val(data[0].User_Password);
                } else {
                    $("#user_nm").val("");
                    $("#password").val("");
                }
            });
        } else {
            showErrorMessage("No data found !!");
        }
    } catch (err) {
        alert(err);
    }
}
function get_groups() {
    var dep_Id = $("#ddlgroup_id").val();
    // alert(dep_Id);
    contact_group.get_groups(dep_Id, function (val) {
        var div_data = "";
        if (val[0] != "") {
            var data = JSON.parse(val[0]);
            for (var x = 0; x < data.length; x++) {

                var obj = JSON.stringify(data[x]);
                var check = '';
                var style = "style=background:transparent";
                if (val[1] != 0) {
                    var data2 = JSON.parse(val[1]);
                    for (var y = 0; y < data2.length; y++) {
                        if (data[x].id == data2[y].contact_id) {
                            check = 'checked';
                            style = "style=background:#afcce6";
                        }
                    }

                }
                div_data = div_data + "<tr " + style + " id='tr_" + data[x].id + "' class='row-content' data-obj='" + obj + "' ><td>" + (x + 1) + "</td> " +
                    "<td id='contact_name'>" + data[x].name_ar + "</td>" +
                    "<td id='contact_id' style='display:none'>" + data[x].id + "</td>" +
                    "<td>" + data[x].tel1 + "</td>" +
                    "<td class='check'> <label><input type='checkbox' " + check + "   onchange='check_row(this," + data[x].id + ");' value=''></label></td></tr>";

            }
            $('#invoices').html(div_data);
        }

    });
}
function mark_all() {
    $('#invoices input:checkbox').prop("checked", "checked");
    $('#invoices tr').css("background", "#afcce6");
}
function unmark_all() {
    $('#invoices input:checkbox').prop("checked", false);
    $('#invoices tr').css("background", "transparent");
}
function check_row(e, id) {
    if ($(e).is(':checked')) {

        $('#tr_' + id).css("background", "#afcce6");
    }
    else { $('#tr_' + id).css("background", "transparent"); }

}
function search_table() {
    var value = $("#lblsearch").val();

    $("#invoices tr").each(function () {


        var id = $(this).find("#contact_name").text();

        if (id.indexOf(value) == -1) {

            $(this).hide();
        }else {

            $(this).show();
        }

    });
}