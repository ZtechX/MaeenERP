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
var sms_num = 1;
var confirm_save = true;
var arrObjects = [];
$(function () {
    try {
       
        //showHideCalendar($("#txtDateh"));
        //showHideCalendar($("#txtDatem"));
        //add_date();
        //$(".txtDateh").val(cal2.getDate().getDateStringHigri()); 
        //$(".txtDatem").val(cal2.getDate().getDateString()); 
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
        loadDynamicTable('contact_group', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
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
        $("#letter_count").html("0 حرف");
        $("#recieved_count").html("عدد المرسل اليهم 0");
        sms_num = 1;
    } catch (err) {
        alert(err);
    }
}

function save() {
  //  confirm_save = true;
    sms_num = 1;
    try {
        $("input").removeClass('error');
        $("select").removeClass('error');
        objects_arr = [];
        var group_id = $("#ddlgroup_id").val();
        var title = $("#txttitle").val();
        var body = $("#txtbody").val();
        var j =  $('#invoices').find('input[type="checkbox"]:checked').length;

        if (!checkRequired()) {
            if (j == 0) {
                $("#lblsearch").addClass('error');
                showErrorMessage("يرجى اختيار المرسل اليهم");
                return;
            }
            $('#invoices').find('tr').each(function () {
                var row = $(this);

                if (row.find('input[type="checkbox"]').is(':checked')) {
                    var data = { 'to_id': row.find("#contact_id").html(), 'title': title, 'body': body, 'date_h': $("#txtDateh").val(),'date':$("#txtDatem").val() }
                    objects_arr.push(data);
                }
            });
            var PosId = $("#lblmainid").html();
            //var letters=getnum($("#letter_count").html());
            //var persons = getnum($("#recieved_count").html());
          
                //sms.get_Limit(function (val) {
                //    if (val != "") {
                //        var arr = JSON.parse(val);
                //        var L_count = Number(arr[0].max_char);
                //        var P_count = Number(arr[0].max_message_no);
                //        if (L_count < letters) {
                //            confirm_save = false;
                //            sms_num = Math.ceil(letters / L_count);
                //            $("#MessageContent").html("أقصى عدد للحروف " + L_count + "  حرف هل تريد الارسال على اكثر من رسالة");
                //            arrObjects = objects_arr;
                //            $('#confirmModal').modal('toggle');
                          
                //        }
                //        if (P_count < persons) {
                //            showErrorMessage("أقصى حد للمرسل اليهم " + P_count);
                //            return;
                //        }
                //    }

                    //if (confirm_save) {
                        sms.Save(PosId, objects_arr, function (val) {
                            if (val == true) {
                                $('#invoices input:checkbox').prop("checked", false);
                                $('#invoices tr').css("background", "transparent");
                                $("#ddlgroup_id").val("0");
                                showSuccessMessage("تم الحفظ بنجاح");
                            } else {
                                showErrorMessage(val.split("|")[1]);
                            }
                        });
                    //}
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
function get_group(flag) {
    $("#recieved_count").html("عدد المرسل اليهم 0");
    var ddl_group = $("#ddlgroup_id").val();
    var type = $("#ddl_type").val();

    // alert(dep_Id);
    if (flag == 1) {
        if (type == 3) {
            $("#group_contact").css('display', 'block');
            $('#invoices').html("");
            return;
        } else {
            $("#group_contact").css('display', 'none');
        }
    }
    sms.get_groups(flag, ddl_group,type, function (val) {
        var div_data = "";
        if (val[0] != "") {
            var data = JSON.parse(val[0]);
           // console.log(data);
            for (var x = 0; x < data.length; x++) {
                var obj = JSON.stringify(data[x]);
                div_data = div_data + "<tr  id='tr_" + data[x].id + "' class='row-content' data-obj='" + obj + "' ><td>" + (x + 1) + "</td> " +
                    "<td id='contact_name'>" + data[x].name + "</td>" +
                    "<td id='contact_id' style='display:none'>" + data[x].id + "</td>" +
                    "<td>" + data[x].tel + "</td>" +
                    "<td class='check'> <label><input type='checkbox'    onchange='check_row(this," + data[x].id + ");' value=''></label></td></tr>";
            }
        }

        $('#invoices').html(div_data);
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
     
    //var num = getnum($("#recieved_count").html());
    if ($(e).is(':checked')) {

        $('#tr_' + id).css("background", "#afcce6");
        //$("#recieved_count").html("عدد المرسل اليهم "+(Number(num)+1));
    }
    else {
        $('#tr_' + id).css("background", "transparent");
        //$("#recieved_count").html("عدد المرسل اليهم " + (Number(num) - 1));
    }

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
function get_template() {
    var template = $("#ddltemplate").val();
    sms.get_template(template, function (val) {
        if (val[0] != 0) {
            var data = JSON.parse(val[0]);
            $("#txttitle").val(data[0].name);
            $("#txtbody").val(data[0].body);
            $("#letter_count").html($("#txtbody").val().length +" حرف");
        } else {
            $("#txttitle").val("");
            $("#txtbody").val("");
        }

    });

}

function SetLetterCount() {
    $("#letter_count").html($("#txtbody").val().length + " حرف");
}
function getnum(str) {
    var num = str.match(/\d/g);
    num = num.join("");
    return num;
}

function SetValue(val) {
    if (val) {
        var PosId = $("#lblmainid").html();

            sms.Save(PosId, arrObjects, $("#ddl_type").val(), sms_num, function (val) {
                if (val == true) {
                    $('#invoices input:checkbox').prop("checked", false);
                    $('#invoices tr').css("background", "transparent");
                    $("#ddlgroup_id").val("0");
                    showSuccessMessage("تم الحفظ بنجاح");
                } else {
                    showErrorMessage(val.split("|")[1]);
                }
            });
        
    }
    $('#confirmModal').modal('toggle');
};
