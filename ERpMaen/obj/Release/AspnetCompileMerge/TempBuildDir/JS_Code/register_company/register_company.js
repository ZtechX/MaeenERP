/************************************/
// Created By : Ahmed Nayl
// Create Date : 14/10/2018 3:30 PM
// Description : This file contains all javaScript functions related to Companies form 
/************************************/


//public variables
var deleteWebServiceMethod = "register_company.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "register_company.asmx/Edit";
var formAutoCodeControl = "lblmainid";
// load function set defualt values
var index_i =1;
var clean = true;
$(function () {

    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").show();
    try {
        form_load();
        //  get_groups();
       

    } catch (err) {
        alert(err);
    }
});

// draw dynamic table for existing travel_agencies
function drawDynamicTable() {
    try {
        var tableSortingColumns = [
                { orderable: false }, null, null, null,
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" },{ type: "text" }, { type: "text" },
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 1;
      //  loadDynamicTable('contact_group', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch (err) {
        alert(err);
    }
}

function delete_row() {
    if (index_i > 1) {
        index_i--;
        alert(index_i);
        $("#addr" + (index_i)).remove();

    }
}
function add_row(){
    
    var rowCount = $('#tab_logic tr').length;
        var str = "";
        var str2 = "";
    var strdiv = "";

    strdiv="<td>" + (index_i+1) + "</td><td><input dbcolumn='' required name='reason" + index_i + "' type='text' id='details" + index_i + "' placeholder='' class='form-control moneyValue' ></td><td>" + strdiv + "</td><td> <select  class='chosen-select form-control'   id='type" + index_i + "'><option value='0'>شرط</option><option value='1'> حكم</option></select></td>";
    $('#draw_items').append('<tr id="addr' + (index_i) + '">' + strdiv + '</tr>');
    index_i++;
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
        debugger;
        $("input").removeClass('error');
        $("select").removeClass('error');
        objects_arr = getDetailsArrJson();
        var atch_files = get_files_ArrJson();
        var group_id = $("#ddlcategory_id2").val();
        if (Page_ClientValidate("vgroup")) {
            var PosId = $("#lblmainid").html();
            register_company.Save(PosId, objects_arr, atch_files, function (val) {
                if (val == true) {
                    $('#invoices input:checkbox').prop("checked", false);
                    $('#invoices tr').css("background", "transparent");
                    $("#ddlcategory_id2").val("0");
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

function getDetailsArrJson() {
    try {

        var arrData = [];
        var index = 0;
        var category_id = $("#ddlcategory_id2").val();
        var arr_validation = [];
        $("#draw_items tr").each(function () {
            var details = $(this).find("#details" + index).val();
            var type = $(this).find("#type" + index).val();
            var data = { "category_id": category_id, "details": details, "type": type };
            arrData.push(data);
            index = index + 1;
        });
            return arrData;

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
    var dep_Id = $("#ddlcategory_id2").val();
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
function get_category() {
    var category = $("#ddlcategory_id2").val();
    register_company.get_category(category, function (val) {
        console.log(val);
        var arr = [];
            if (val[0] != "0") {
                var finance = JSON.parse(val[0]);
                var result_finance = "";
                for (var z = 0; z < finance.length; z++) {
                    var strdiv = "";
                    result_finance = result_finance + "<tr id='addr" + z + "'>" + "<td>" + (z + 1) + "</td> <td><input dbcolumn='' required  value='" + finance[z].details + "'  type='text' id='details" + z + "' placeholder='' class='form-control moneyValue' ></td><td>" + strdiv + "</td><td> <select value='" + finance[z].type + "' class='chosen-select form-control'   id='type" + z + "'><option value='0'>شرط</option><option value='1'> حكم</option></select></td>" +
                        "</tr >";
                    index_i = z + 1;
                    arr.push(finance[z].type)
                }
                $("#draw_items").html(result_finance);
            } else {
                $("#draw_items").html("");
                index_i = 0;
                add_row();
        }
        if (arr != "") {
            for (var h = 0; h < arr.length; h++) {

                $("#type" + h).val(arr[h]);
            }

        }
        console.log(val[1])
        if (val[1] != "") {
            fillImages(val[1]);
        }
      

        });

}
function get_files_ArrJson() {
    try {
        var arrData = [];
        $("#tblUploadedFiles tbody tr").each(function () {
            if ($(this).find('#cmdDelete').val() != "Deleted") {
                var data = { "Id": $(this).find("#lblFileId").html(), "Name": $(this).find("#txtTitle").val(), "Type": $(this).find('select').val(), "url": $(this).find('img').prop("id"), "MainImage": $(this).find("#chkMainImage").prop("checked"), "LogoImage": $(this).find("#chkLogoImage").prop("checked"), "deleted": 0, "orderid": $(this).find("#lblOrderId").html() };
                arrData.push(data);
            }
            else {
                if ($(this).find("#lblFileId").html() != "") {
                    var data = { "Id": $(this).find("#lblFileId").html(), "Name": $(this).find("#txtTitle").val(), "Type": $(this).find('select').html(), "url": $(this).find('img').prop("id"), "MainImage": $(this).find("#chkMainImage").prop("checked"), "LogoImage": $(this).find("#chkLogoImage").prop("checked"), "deleted": 1, "orderid": $(this).find("#lblOrderId").html() };
                    arrData.push(data);
                }
            }
        });
        return arrData;
    } catch (err) {
        alert(err);
    }
}
        function fillImages(data) {
            try {
                if (data != "") {
                    console.log(data);
                    var files = JSON.parse(data);
                    if (files.length > 0) {
                        //  $('#tblUploadedFiles').remove();
                          createTblUploadedFiles(["Index", "File", "Name", "Delete"]);
                        for (i = 0; i < files.length; i++) {
                            appendFileTR(files[i], i);
                        }
                        $("#txtUploadedFiles").val(files.length);
                    }
                }
            } catch (error) {
                alert(error);
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