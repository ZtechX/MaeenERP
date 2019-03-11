/************************************/
// Created By : Ahmed Nayl
// Create Date : 14/10/2018 3:30 PM
// Description : This file contains all javaScript functions related to Companies form 
/************************************/


//public variables
var deleteWebServiceMethod = "cases.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "cases.asmx/Edit";
var formAutoCodeControl = "lblmainid";
// load function set defualt values
var index_i =1;
var clean = true;
$(function () {

    //$('select').select2({
    //    placeholder: 'This is my placeholder',
    //    allowClear: true
    //});
    try {
        get_tabs();
        get_option_cases();
     // show_all(21);
        getSerial(); 
        getSerial_conciliation()
        getSerial_correspondencesn()
        getSerial_sessions()
  
    } catch (err) {
        alert(err);
    }
});

function getSerial() {
    cases.getSerial(function (val) {
        $("#txtcode").val(val);
    });
}

function getSerial_conciliation() {
    cases.getSerial_conciliation(function (val) {
        $("#txtcode_conciliation").val(val);
    });
}
function getSerial_correspondencesn() {
    cases.getSerial_correspondencesn(function (val) {
        $("#txtcode_correspondences").val(val);
    });
}
function getSerial_sessions() {
    cases.getSerial_sessions(function (val) {
        $("#txtcode_sessions").val(val);
    });
}

function get_persons(id,delivery_data) {
    cases.get_persons(id,function (val) {
        if (val[0] != "0") {
            var data = JSON.parse(val[0])
            var result_person1 = "";
            var div_data = "";
            var div_data2 = "";
            for (var x = 0; x < data.length; x++) {
                result_person1 = result_person1 + "<option value=" + data[x].person_id + ">" + data[x].person_name + "</option>";
            }
            $("#ddldeliverer_id").html(result_person1);
            $("#ddlreciever_id").html(result_person1);
            $("#ddlowner_id").html(result_person1);
            $("#ddlsecond_party_id").html(result_person1);
            $("#ddlperson_id").html(result_person1);
            $("#ddlowner_id_sessions").html(result_person1);
            $("#ddlsecond_party_id_sessions").html(result_person1);
            $("#ddldeliverer_id_expense").html(result_person1);
            $("#ddlrciever_id_expense").html(result_person1);

            for (var y = 0; y < data.length; y++) {
                div_data = div_data + "<tr id='" + data[y].person_id + "' class='row-content' ><td>" + (y + 1) + "</td> " +
                    "<td id='tab_num'>" + data[y].person_name + "</td>" +
                    "<td class='check'><input id='persons_checked" + data[y].person_id + "' type='checkbox'></td>" +
                    "</tr > ";

            }
            $('#persons_sessions').html(div_data);
            console.log(delivery_data);
            if (delivery_data != "") {
                $("#ddlreciever_id").val(delivery_data.reciever_id)
                $("#ddldeliverer_id").val(delivery_data.deliverer_id)
            }
        } 
    });
}
$("#combobox").on("change", function () { debugger; });


$(document).on("change", "#combobox", function () {
}); 
function get_option_cases() {

    cases.get_option_cases(function (val) {
        if (val[0] != "") {
            var data = JSON.parse(val[0]);
            console.log(data);
            var div_data = "";
            for (var x = 0; x < data.length; x++) {
                div_data = div_data + "<option value=" + data[x].case_id + ">حالة#" + data[x].cases + " " + "تاريخ" + data[x].date + "  " + "مقدمة:" + data[x].person + " </option> ";
            }
            $('#combobox').append(div_data);
            $('#ddlcase_id').append(div_data);
            //$(".select2").select2({
            //    allowClear: true,
            //    placeholder: 'Position'
            //});
        }


    });

}

function add() {
    try {
        resetDivControls("divForm");
        getSerial(); 
        getSerial_conciliation()
        getSerial_correspondencesn()
        getSerial_sessions()
        for (var i = 1; i <= 8; i++) {
            $("#case_" + i).css("display", "none");
        }
        $("#ddlchild_custody").html("");
        $("#case_active").addClass("active")
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
// save_cases
function save() {

    try {
        $("input").removeClass('error');
        $("select").removeClass('error');
        $("#lblstart_date").html($("#divdate3 #txtDatem").val());
        $("#lblstart_date_hj").html($("#divdate3 #txtDateh").val());

        $("#txtinstrument_date_m").html($("#instrum_date #txtDatem").val());
        $("#txtinstrument_date_h").html($("#instrum_date #txtDateh").val());
        
        //objects_arr = getDetailsArrJson();
       var cases_info= generateJSONFromControls("cases_info");
        var persons_owner = generateJSONFromControls("person_owner"); 
      var person_against=  generateJSONFromControls("person_against");
      var children=  generateJSONFromControls("get_children");
        var status = generateJSONFromControls("get_status");
       var tabs= getTabsJson();
        var atch_files = get_files_ArrJson();
        if (!checkRequired("cases_info") && !checkRequired("person_owner") && !checkRequired("person_against") && !checkRequired("get_children") && !checkRequired("get_status")  ) {
            var PosId = $("#lblmainid").html();
        cases.Save(PosId, cases_info, persons_owner, person_against, children, status, tabs, atch_files, function (val) {
            if (val !=0) {
                $("#lblcase_id").html(val);
                getSerial();
                get_checked_tab(val);
                show_all(val,1);
                get_persons(val,"");
                resetDivControls("cases_info");
                resetDivControls("person_owner");
                resetDivControls("person_against");
                resetDivControls("get_children");
                resetDivControls("get_status");
                    showSuccessMessage("تم الحفظ بنجاح");
                } else if(val==-1) {
                alert("عفوا رقم هوية المنفذ اغو المنفذ ضده موجود من قبل");
                }
            });
        } else {
            alert("يرجى ادخال البيانات المطلوبه");
        }
    } catch (err) {
        alert(err);
    }
}
// end save_cases
/////////////////////////////////
// save_children
function save_children() {
    var children = generateJSONFromControls("children_info");
    if (!checkRequired("children_info")) {
    var case_id = $("#lblcase_id").html();
    var PosId = $("#lblchild").html();
    cases.save_children(PosId, case_id ,children, function (val) {
        if (val) {
            if (val == -1) {
                alert("عفوا عدد الاولاد اكبر ")
            } else if (val == -2) {
                alert("عفوا عدد البنات اكبر ")
            } else {
                showSuccessMessage("تم الحفظ بنجاح");
            }
            resetDivControls("children_info");
            show_all(case_id,1);
   
        } else {
            showErrorMessage(val.split("|")[1]);
        }
    });
} else {
    alert("يرجى ادخال البيانات المطلوبه");
}


}
// end save_children
////////////////////////////////////////////
// save_children_receive
function save_children_receive() {

    if (!checkRequired("children_receive")) {
    $("#lblfirst_date_received_m").html($("#divdate_received #txtDatem").val());
    $("#lblfirst_date_received_h").html($("#divdate_received #txtDateh").val());
    var children_receive = generateJSONFromControls("children_receive");
    var case_id = $("#lblcase_id").html();
    var PosId = $("#lblchild_recive_id").html();

 
    cases.save_children_receive(PosId, case_id, children_receive, function (val) {
        if (val) {
            show_all(case_id,1);
            showSuccessMessage("تم الحفظ بنجاح");

        } else {
            showErrorMessage(val.split("|")[1]);
        }
    });
} else {
    alert("يرجى ادخال البيانات المطلوبه");
}


}
// end save_children_receive
//////////////////////////////
// save_delivery_details
function save_delivery_details() {

    if (!checkRequired("receiving_delivery_details")) {
    //$("#lbldate_m").html($("#divdate_delivery #txtDatem").val());
    //$("#lbldate_h").html($("#divdate_delivery #txtDateh").val());

    //$("#lblnew_date_m").html($("#div_new_date #txtDatem").val());
    //$("#lblnew_date_h").html($("#div_new_date #txtDateh").val());
    var children_delivery_details = generateJSONFromControls("receiving_delivery_details");
       var case_id = $("#ddlcase_id").val();
        var ddltype = $("#ddltype").val();
        var PosId = $("#lbldelivery_details").html();
        var new_date = $("#lbldelivery_date_h").html();
        var new_date_m = $("#lbldelivery_date_m").html();
        var res = new_date.substring(3, 10);
        var children_gson = getChildrenJson();
        if (ddltype == 1) {
            if (children_gson == "") {
                alert("يجب ان تختار الاولاد");
                return;
            }
        }
        cases.save_delivery_details(PosId, case_id, children_delivery_details, children_gson, res, function (val) {
            if (val) {
                $("#Appraisal").dialog({
                    width: "800px",
                });
                var res_id = val.split("|")[0]
                var res_type = val.split("|")[1]
                $("#lbltype").html(res_type)
                $("#lbldetail_id").html(res_id)
                $("#lblcase_apprisal_id").html(case_id)
                $("#lblapprisal_date_m").html(new_date_m)
                $("#lblapprisal_date_h").html(new_date)
                if (res_type == 1) {
                    $("#dateval[date*='" + new_date_m + "']").parent().css("background-color", "#039ae4");
                } else {
                    $("#dateval[date*='" + new_date_m + "']").parent().css("background-color", "red");
                }
                $("#dateval[date*='" + new_date_m + "']").parent().css("color", "#ffffff");
                $("#dateval[date*='" + new_date_m + "']").parent().prop('id', res_id);
            resetDivControls("receiving_delivery_details");
            $('#receiving_delivery_details').dialog("close");
            showSuccessMessage("تم الحفظ بنجاح");
        } else {
            showErrorMessage(val.split("|")[1]);
        }
    });
} else {
    alert("يرجى ادخال البيانات المطلوبه");
}


}
// end save_delivery_details
//////////////////////////
// save_conciliation
function save_conciliation() {


    if (!checkRequired("case_conciliation")) {
    $("#lblconciliation_date_m").html($("#Date_reconciliation #txtDatem").val());
    $("#lblconciliation_date_h").html($("#Date_reconciliation #txtDateh").val());
    var case_conciliation = generateJSONFromControls("case_conciliation");
    var case_id = $("#lblcase_id").html();
    var PosId = $("#lblconciliation_id").html();
    var new_date_m = cal_days();
    var children_gson = getChildrenJson();
    cases.save_conciliation(PosId, case_id, case_conciliation, function (val) {
        if (val) {
            resetDivControls("case_conciliation");
            getSerial_conciliation()
            show_all(case_id,1);
            showSuccessMessage("تم الحفظ بنجاح");
        } else {
            showErrorMessage(val.split("|")[1]);
        }
    });
} else {
    alert("يرجى ادخال البيانات المطلوبه");
}


}

// end save_conciliation
////////////////////////
// save_correspondences
function save_correspondences() {


    if (!checkRequired("case_correspondences")) {
    $("#lblcorrespondences_date_m").html($("#divdate_correspondences #txtDatem").val());
    $("#lblcorrespondences_date_h").html($("#divdate_correspondences #txtDateh").val());
    var case_correspondences = generateJSONFromControls("case_correspondences");
    var case_id = $("#lblcase_id").html();
    var PosId = $("#lblcorrespondences_id").html();
    cases.save_correspondences(PosId, case_id, case_correspondences, function (val) {
        if (val) {
            resetDivControls("case_correspondences");
            getSerial_correspondencesn();
            show_all(case_id,1);
            showSuccessMessage("تم الحفظ بنجاح");
        } else {
            showErrorMessage(val.split("|")[1]);
        }
        });
    } else {
        alert("يرجى ادخال البيانات المطلوبه");
    }



}
// end save_correspondences
/////////////////////
// save_sessions
function save_sessions() {

    if (!checkRequired("case_sessions")) {
    $("#lbldate_m_sessions").html($("#divdate_sessions #txtDatem").val());
    $("#lbldate_h_sessions").html($("#divdate_sessions #txtDateh").val());
    var sessions = generateJSONFromControls("case_sessions");
    var case_id = $("#lblcase_id").html();
    var PosId = $("#lbl_sessions_id").html();
    var children_gson = getChildren_sessionsJson();
    var persons_gson = getPersons_sessionsJson();

 

    cases.save_sessions(PosId, case_id, sessions, children_gson, persons_gson, function (val) {
        if (val) {
            $("#Appraisal").dialog({
                width: "800px",
            });
            $("#lbltype").html(3)
            $("#lbldetail_id").html(val)
            $("#lblcase_apprisal_id").html(case_id)
            $("#lblapprisal_date_m").html($("#divdate_sessions #txtDatem").val())
            $("#lblapprisal_date_h").html($("#divdate_sessions #txtDateh").val())
            resetDivControls("case_sessions");
            getSerial_sessions()
            show_all(case_id,1);
        } else {
            showErrorMessage(val.split("|")[1]);
        }
    });
} else {
    alert("يرجى ادخال البيانات المطلوبه");
}



}
// end save_sessions
///////////////////
// save_expense_basic
function save_expense_basic() {

    if (!checkRequired("expense_basic")) {
    $("#lbldate_m_expenses_basic").html($("#date_expenses_basic #txtDatem").val());
    $("#lbldate_h_expenses_basic").html($("#date_expenses_basic #txtDateh").val());
    var expense_basic = generateJSONFromControls("expense_basic");
    var case_id = $("#lblcase_id").html();
    var PosId = $("#lblexpense_basic").html();

    cases.save_expense_basic(PosId, case_id, expense_basic, function (val) {
        if (val) {
            show_all(case_id,1);
            showSuccessMessage("تم الحفظ بنجاح");
        } else {
            showErrorMessage(val.split("|")[1]);
        }
    });
} else {
    alert("يرجى ادخال البيانات المطلوبه");
}




}
// end save_expense_basic
////////////////////////
// save_expense_details
function save_expense_details() {
    if (!checkRequired("expense_basic")) {
    $("#lbldate_m_expense_details").html($("#date_expense_details #txtDatem").val());
    $("#lbldate_h_expense_details").html($("#date_expense_details #txtDateh").val());
    var expense_details = generateJSONFromControls("expense_details");
    var case_id = $("#lblcase_id").html();
        var PosId = $("#lblexpenses_details").html();

    var new_date_m = cal_days_expense();
    var d = cal_days_expense();
    var dateParts = new Date((Number(d.split("/")[2])), (Number(d.split("/")[1]) - 1), (Number(d.split("/")[0])));
    var new_date_h = kuwaiticalendar(dateParts);

    cases.save_expense_details(PosId, case_id, expense_details, new_date_m, new_date_h, function (val) {
        if (val) {
            resetDivControls("expense_details");
            show_all(case_id,1);
            get_date_expenses(case_id);
            showSuccessMessage("تم الحفظ بنجاح");

        } else {
            showErrorMessage(val.split("|")[1]);
        }
    });
} else {
    alert("يرجى ادخال البيانات المطلوبه");
}


}
// end save_expense_details
/////////////////////////
//save new_person
function find_persons(flag,flag2) {
    $("#lbldiv_person").html(flag);
    $("#lbldiv_type").html(flag2);
    $("#collapseExample").dialog({
        width: "600px",
    });
}
function save_new_person(flag) {
    if (!checkRequired("collapseExample")) {
    var id = $("#lbldiv_person").html();
  var type_draw=  $("#lbldiv_type").html();
    var children_gson = generateJSONFromControls("collapseExample");
        var person = $("#person_name").val();
            var case_id = $("#lblcase_id").html();
      

    var PosId = 0;
    cases.Save_person(children_gson, PosId, case_id,  function (val) {
        if (val) {
            var result_person1 = "";
            if (type_draw == 1) {
                result_person1 = result_person1 + "<option selected value=" + val + ">" + person + "</option>";
            } else {
                result_person1 = result_person1 + "<tr id='" + val + "' class='row-content' ><td> </td> " +
                    "<td id='tab_num'>" +person+ "</td>" +
                    "<td class='check'><input id='persons_checked" + val+ "' type='checkbox' checked></td>" +
                    "</tr > ";
            }
            $("#" + id).append(result_person1);
            resetDivControls("collapseExample");
            $('#collapseExample').dialog("close");
        } else {
            showErrorMessage(val.split("|")[1]);
        }


    });
} else {
    alert("يرجى ادخال البيانات المطلوبه");
}
}
// end save_person
// save_apprisal
function save_apprisal() {
    var apprisal_gson = generateJSONFromControls("Appraisal");
    var PosId = $("#lblitem_id").html();
    var PosId = 0;
    cases.save_apprisal(PosId,apprisal_gson, function (val) {
        if (val) {
            $('#Appraisal').dialog("close");
        }
    });

}
//end save_apprisal
function rate(sender, rate, control_id) {
    var i = 1;
    $(".star_test").val(rate);
    $("#Appraisal span").each(function () {
        $(this).removeClass("check");
        var id = $(this).attr("id");
        var new_id = id.replace("star", "");
        if (new_id <= rate && new_id != 0) {
            $(this).addClass("check");
        }


        // $(id).;

    });





}
// cal_days
function cal_days() {
   
    var check = $("#lbldelivery_date").html();
    var Apt = check.split("/");
    var newdate = Apt[1] + "/" + Apt[0] + "/" + Apt[2];

    var days = $("#txtdelivery_period").val();
    var date = new Date(newdate);

    days = parseInt(days, 10);


    if (!isNaN(date.getTime())) {
        date.setDate(date.getDate() + days);

        return date.toInputFormat();
    } else {
        alert("Invalid Date");
    }
}
Date.prototype.toInputFormat = function () {
    var yyyy = this.getFullYear().toString();
    var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
    var dd = this.getDate().toString();
    return (dd[1] ? dd : "0" + dd[0]) + "/" + (mm[1] ? mm : "0" + mm[0]) + "/" + yyyy; // padding
    alert((dd[1] ? dd : "0" + dd[0]) + "/" + (mm[1] ? mm : "0" + mm[0]) + "/" + yyyy)
};

function cal_days_expense() {
    var check = $("#lblexpense_date").html();
    var Apt = check.split("/");
    var newdate = Apt[1] + "/" + Apt[0] + "/" + Apt[2];

    var days = $("#txtdelivery_period_expenses").val();
    var date = new Date(newdate);

    days = parseInt(days, 10);


    if (!isNaN(date.getTime())) {
        date.setDate(date.getDate() + days);

        return date.toInputFormat();
    } else {
        alert("Invalid Date");
    }
}
Date.prototype.toInputFormat = function () {
    var yyyy = this.getFullYear().toString();
    var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
    var dd = this.getDate().toString();
    return (dd[1] ? dd : "0" + dd[0]) + "/" + (mm[1] ? mm : "0" + mm[0]) + "/" + yyyy; // padding
};

// end call_days


// called after update function success

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
function get_name(flag) {
    var name = "";
    if (flag == 1) {
      name=  $("#txtname").val();
    } else {
       name= $("#txtname2").val();
    }
    var result = "";
    result = result + "<option id=" + name + ">" + name + "</option>";
    $("#ddlchild_custody").append(result);
}
function calc_total() {
    var boys = $("#txtboys_no").val();
    var girls = $("#txtgirls_no").val();
    if (boys == "") {
        boys = 0;
    }
    if (girls == "") {
        girls = 0;
    }
    $("#txtchildrens_no").val(parseFloat(boys) +parseFloat(girls));

}

function get_tabs() {
    // alert(dep_Id);
    cases.get_tabs(function (val) {
        var div_data = "";
        if (val[0] != "") {
            var data = JSON.parse(val[0]);
            for (var x = 0; x < data.length; x++) {
                var obj = JSON.stringify(data[x]);
                div_data = div_data + "<tr id='" + data[x].id + "' class='row-content' data-obj='" + obj + "' ><td>" + (x + 1) + "</td> " +
                    "<td id='tab_num'>" + data[x].name + "</td>" +
                    "<td class='check'><input id=tab_case_"+ data[x].id+" type='checkbox'></td>" +
                    "</tr > ";
            }
            $('#tabs').html(div_data);
        }
      
    });
}
function getTabsJson() {
    var objects_arr = [];
    $("#tabs tr").each(function () {
        var row = $(this);
        if (row.find('input[type="checkbox"]').is(':checked')) {
            // alert('You must fill the text area!');
            var id = $(this).closest("tr").attr("id");
            var data = { "tab_id": id}
            objects_arr.push(data);
        }

    });
    return objects_arr;
}
function getChildrenJson() {
    var objects_arr = [];
    $("#tab_children tr").each(function () {
        var row = $(this);
        if (row.find('input[type="checkbox"]').is(':checked')) {
            // alert('You must fill the text area!');
            var id = $(this).closest("tr").attr("id");
            var data = { "children_id": id }
            objects_arr.push(data);
        }

    });
    return objects_arr;
}
function getChildren_sessionsJson() {
    var objects_arr = [];
    $("#children_sessions tr").each(function () {
        var row = $(this);
        if (row.find('input[type="checkbox"]').is(':checked')) {
            // alert('You must fill the text area!');
            var id = $(this).closest("tr").attr("id");
            var data = { "children_id": id }
            objects_arr.push(data);
        }

    });
    return objects_arr;
}
function getPersons_sessionsJson() {
    var objects_arr = [];
    $("#persons_sessions tr").each(function () {
        var row = $(this);
        if (row.find('input[type="checkbox"]').is(':checked')) {
            // alert('You must fill the text area!');
            var id = $(this).closest("tr").attr("id");
            var data = { "person_id": id }
            objects_arr.push(data);
        }

    });
    return objects_arr;
}

function get_checked_tab(id) {
    cases.get_checked_tab(id,function (val) {
        var div_data = "";
        var cases = [];
        if (val[0] != "") {
            var data = JSON.parse(val[0]);
            $("#list_group a").each(function () {
                var id = $(this).attr("id");
                if (id != undefined) {
                    var new_id = id.replace("case_", "");
                    cases.push(new_id);

                }

            });
            for (var x = 0; x < data.length; x++) {
   
                $("#tab_case_" + data[x].tab_id + "").prop("checked", "checked");
                for (var y = 0; y < cases.length; y++) {
                    if (data[x].tab_id == cases[y]) {
                        $("#case_" + cases[y]).css("display", "block");


                    }
                }
            }
        } else {
            $("#tabs tr input:checkbox").prop("checked", false);

        }
    });


}

function get_archive(flag) {
    if (flag == 1) {
        $("#delivery_details").css("display", "none");
        $("#delivery_details_archive").show();
    } else {
        $("#delivery_details_archive").css("display", "none");
        $("#delivery_details").show();
    }
}

function get_archive_expense(flag) {
    if (flag == 1) {
        $("#expense_details_new").css("display", "none");
        $("#expense_details_archive").show();
    } else {
        $("#expense_details_archive").css("display", "none");
        $("#expense_details_new").show();
    }
}

function get_receiving() {
   
    if ($('#Delay').is(":checked")) {
            $("#delay_reason").css("display", "block");
        } else {
            $("#delay_reason").css("display", "none");

        }

}
function show_all(id, flag) {
    if (flag == 1) {
        var case_id = id
            get_persons(case_id);
    } else {
        var case_id = $("#ddlcase_id").val();
    }
    $("#caseReportDive").show();
    $("lblcase_id").html(case_id);
    //get_date_expenses(case_id);
    //get_date_delivery(case_id)
    get_checked_tab(case_id);
    //get_persons(case_id);
    cases.show_all(case_id, function (val) {

        if (val[0] != "0") {
            var data = JSON.parse(val[0])
            var data_owner = JSON.parse(val[1])
            var data_against = JSON.parse(val[2])
            $("#divdate3 #txtDatem").val(data[0].date_m);
            $("#divdate3 #txtDateh").val(data[0].date_h);
            $("#instrum_date #txtDatem").val(data[0].instrument__date_m);
            $("#instrum_date #txtDateh").val(data[0].instrument_date_h);
            $("#lblcase_id").html(data[0].id)
            fillControlsFromJson(data[0], "cases_info")
            fillControlsFromJson(data_owner[0], "person_owner")
            fillControlsFromJson(data_against[0], "person_against")
            fillControlsFromJson(data[0], "get_children")
            fillControlsFromJson(data[0], "get_status")
        }
            if (val[3] != 0) {
                var children = JSON.parse(val[3]);
                var result_children = ""
                for (var y = 0; y < children.length; y++) {
                    result_children = result_children + '<tr>' + '<td>' + children[y].name + '</td>' +
                        '<td>' + children[y].age + '</td>' +
                        '<td><button class="btn btn-xs btn-primary btn-quick" title="view Row" onclick="show_cases_details(' + children[y].id + ',1); return false; "><i class="fa fa-eye"></i></button><button class="btn btn-xs btn-danger btn-quick" title = "Delete" onclick = "delete_details(' + children[y].id + ',1); return false;" > <i class="fa fa-times"></i></button ></td>' +
                        '</tr >';
                }
                $("#children").html(result_children);

                var div_data = "";
                var div_data2 = "";
                for (var x = 0; x < children.length; x++) {
                    div_data = div_data + "<tr id='" + children[x].id + "' class='row-content' ><td>" + (x + 1) + "</td> " +
                        "<td id='tab_num'>" + children[x].name + "</td>" +
                        "<td class='check'><input id='children_checked" + children[x].id+"' type='checkbox'></td>" +
                            "</tr > ";

                }
                $('#tab_children').html(div_data);
                for (var x = 0; x < children.length; x++) {
                    div_data2 = div_data2 + "<tr id='" + children[x].id + "' class='row-content'><td>" + (x + 1) + "</td> " +
                        "<td id='tab_num'>" + children[x].name + "</td>" +
                        "<td class='check'><input id='children_sessions_checked" + children[x].id + "' type='checkbox'></td>" +
                        "</tr > ";
                }

                $('#children_sessions').html(div_data2);
            } else {
                $("#children").html("");
                $('#tab_children').html("");
               
            }

            if (val[4] != 0) {
                var receiving_delivery_basic = JSON.parse(val[4]);
                $("#divdate_received #txtDatem").val(receiving_delivery_basic[0].first_date_received_m);
                $("#divdate_received #txtDateh").val(receiving_delivery_basic[0].first_date_received_h);
                fillControlsFromJson(receiving_delivery_basic[0], "children_receive");
                $("#time_receiving_time input").val(receiving_delivery_basic[0].receiving_time);
                $("#time_delivery_time input").val(receiving_delivery_basic[0].delivery_time);
             //   alert($("#txtdelivery_time").parent().prop("id"));

            }
        if (val[5] != 0) {
            var receiving_delivery_details = JSON.parse(val[5]);
            get_date_children(receiving_delivery_details[0].case_id);
            get_persons(receiving_delivery_details[0].case_id, receiving_delivery_details[0])
              fillControlsFromJson(receiving_delivery_details[0], "receiving_delivery_details");
            show_cases_details(receiving_delivery_details[0].id, 2);
           
            if (receiving_delivery_details[0].type == 1) {
                $("#child_info").css("display", "block");
                $("#money_data").css("display", "none");
            } else {
                $("#child_info").css("display", "none");
                $("#money_data").css("display", "block");
            }
            } else {
                $("#delivery_details").html("");
                $("#delivery_details_archive").html("");

            }
            if (val[6] != 0) {
                var conciliation= JSON.parse(val[6]);
                $("#Date_reconciliation #txtDatem").val(conciliation[0].date_m);
                $("#Date_reconciliation #txtDateh").val(conciliation[0].date_h);
                fillControlsFromJson(conciliation[0], "case_conciliation");
            }

            if (val[7] != 0) {
                var correspondences = JSON.parse(val[7]);
                var result_correspondences= "";
                for (var y = 0; y < correspondences.length; y++) {
                    result_correspondences = result_correspondences + '<tr>' + '<td>' + correspondences[y].code + '</td>' +
                        '<td>' + correspondences[y].date_h + '</td>' +
                        '<td><button class="btn btn-xs btn-primary btn-quick" title="view Row" onclick="show_cases_details(' + correspondences[y].id + ',3); return false; "><i class="fa fa-eye"></i></button><button class="btn btn-xs btn-danger btn-quick" title = "Delete" onclick = "delete_details(' + correspondences[y].id + ',3); return false;" > <i class="fa fa-times"></i></button ></td>' +
                        '</tr >';
                }
                $("#correspondences").html(result_correspondences);

            } else {
                $("#correspondences").html("");
            }

            if (val[8] != 0) {
                var sessions = JSON.parse(val[8]);
                var result_sessions = "";
                for (var y = 0; y < sessions.length; y++) {
                    result_sessions = result_sessions + '<tr>' + '<td>' + sessions[y].code + '</td>' +
                        '<td>' + sessions[y].date_h + '</td>' +
                        '<td><button class="btn btn-xs btn-primary btn-quick" title="view Row" onclick="show_cases_details(' + sessions[y].id + ',4); return false; "><i class="fa fa-eye"></i></button><button class="btn btn-xs btn-danger btn-quick" title = "Delete" onclick = "delete_details(' + sessions[y].id + ',4); return false;" > <i class="fa fa-times"></i></button ></td>' +
                        '</tr >';
                }
                $("#sessions").html(result_sessions);

            } else {
                $("#sessions").html("");
            }
            if (val[9] != 0) {
                var expenses_basic = JSON.parse(val[9]);
                $("#date_expenses_basic #txtDatem").val(expenses_basic[0].date_m);
                $("#date_expenses_basic #txtDateh").val(expenses_basic[0].date_h);
                fillControlsFromJson(expenses_basic[0], "expense_basic");
            }
            if (val[10] != 0) {
                var expenses_details = JSON.parse(val[10]);
                var result_delivery_details = ""
                var result_delivery_details_archive = ""
                for (var h = 0; h < expenses_details.length; h++) {
                    if (expenses_details[h].done == 0) {
                        result_delivery_details = result_delivery_details + '<tr>' + '<td>' + expenses_details[h].date_h + '</td>' +
                            '<td><button class="btn btn-xs btn-primary btn-quick" title="view Row" onclick="show_cases_details(' + expenses_details[h].id + ',5); return false; "><i class="fa fa-eye"></i></button><button class="btn btn-xs btn-danger btn-quick" title = "Delete" onclick = "delete_details(' + expenses_details[h].id + ',5); return false;" > <i class="fa fa-times"></i></button ></td>' +
                            '</tr >';
                    } else {
                        result_delivery_details_archive = result_delivery_details_archive + '<tr>' + '<td>' + expenses_details[h].date_h + '</td>' +
                            '<td><button class="btn btn-xs btn-primary btn-quick" title="view Row" onclick="show_cases_details(' + expenses_details[h].id + ',5); return false; "><i class="fa fa-eye"></i></button></td>' +
                            '</tr >';

                    }
                }
                $("#expense_details_new").html(result_delivery_details);
                $("#expense_details_archive").html(result_delivery_details_archive);
            } else {
                $("#expense_details_new").html("");
                $("#expense_details_archive").html("");

            }

    });

}
function show_calender_details() {
    var case_id = $("#ddlcase_id").val();
    get_date_children(case_id)
    get_persons(case_id,"")
    $("#save_delivery_details").css("display", "block");
}

function show_cases_details(flag1,flag3) {

    cases.show_cases_details(flag1,flag3, function (val) {
        if (flag3 == 1) {
            //$("#worker_panel").css('display', 'block');
            //$("#worker_collapse").removeClass("plus");
            var children_details = JSON.parse(val[0]);
            fillControlsFromJson(children_details[0], "children_info")


        } else if (flag3 == 2) {
            var delivery_details = JSON.parse(val[0]);
            if (delivery_details[0].receiving_delivery_done == 1) {
                $("#save_delivery_details").css("display", "none");
            } else {
                $("#save_delivery_details").css("display", "block");
            }
            if (val[1] != 0) {
                var delivery_childrens = JSON.parse(val[1]);
                for (var x = 0; x < delivery_childrens.length; x++) {
                    $("#children_checked" + delivery_childrens[x].children_id + "").prop("checked", "checked");
                }
            } else {
                $('#tab_children tr input:checkbox').prop("checked", false);
            }

        } else if (flag3 == 3) {
            var case_correspondences = JSON.parse(val[0]);
            $("#divdate_correspondences #txtDatem").val(case_correspondences[0].date_m);
            $("#divdate_correspondences #txtDateh").val(case_correspondences[0].date_h);
            fillControlsFromJson(case_correspondences[0], "case_correspondences")
        } else if (flag3 == 4) {
            var sessions = JSON.parse(val[0]);

            $("#divdate_sessions #txtDatem").val(sessions[0].date_m);
            $("#divdate_sessions #txtDateh").val(sessions[0].date_h);
            fillControlsFromJson(sessions[0], "case_sessions");
            $("#time_entry_time input").val(sessions[0].entry_time);
            $("#time_exite_time input").val(sessions[0].exite_time);
            if (val[1] != 0) {
                var childrens = JSON.parse(val[1]);
                for (var y = 0; y < childrens.length; y++) {
                    $("#children_sessions_checked" + childrens[y].children_id + "").prop("checked", "checcked");
                }
            } else {
                $('#children_sessions tr input:checkbox').prop("checked", false);
            }
            if (val[2] != 0) {
                var persons = JSON.parse(val[2]);
                for (var y = 0; y < persons.length; y++) {
                    $("#persons_checked" + persons[y].person_id + "").prop("checked", "checcked");

                }
            } else {
                $('#persons_sessions tr input:checkbox').prop("checked", false);
            }
        } else if (flag3 == 5) {
            var case_expense = JSON.parse(val[0]);
            $("#date_expense_details #txtDatem").val(case_expense[0].date_m);
            $("#date_expense_details #txtDateh").val(case_expense[0].date_h);
            fillControlsFromJson(case_expense[0], "expense_details")
            if (case_expense[0].done == 1) {
                $("#save_expense_details").css("display", "none");
            } else {
                $("#save_expense_details").css("display", "block");
            }


        }
          
    });

}

function delete_details(flag1, flag2, flag3) {
    var r = confirm("هل بالفعل تريد الحذف");
    if (r == true) {
        projects.delete_details(flag1, flag2, flag3, function (val) {
            if (val == true) {
                show_project(flag2, 2);
                showSuccessMessage("تم الحذف بنجاح");
            } else {
                showErrorMessage('لم يتم الحفظ');
            }

        });
    }
}
function get_date_children(case_id) {
  
    cases.get_date_children(case_id, function (val) {
        if (val[0] != "0") {
            var children = JSON.parse(val[0]);
            var div_data = ""
            for (var x = 0; x < children.length; x++) {
                var obj = JSON.stringify(children[x]);
                div_data = div_data + "<tr id='" + children[x].id + "' class='row-content' data-obj='" + obj + "' ><td>" + (x + 1) + "</td> " +
                    "<td id='tab_num'>" + children[x].name + "</td>" +
                    "<td class='check'><input id='children_checked" + children[x].id + "' type='checkbox'></td>" +
                    "</tr > ";

            }
            $('#tab_children').html(div_data);
        } else {
            $('#tab_children').html("");
                }

        });
 
}
function check_user() {
    var user_name = $("#user_name").val();
    companies.check_user(user_name, function (val) {
        if (val == true) {
            $("#check_userName").val('1')
        } else {
            $("#check_userName").val('0')
        }

    });


}
function mark_all(event,table_id) {
   var id=$(event).attr("id");
    //if (event.attr("checked")) {
    //    alert("44")
    //} else {
    //    alert("33")
    //}
    if ($('#'+id).is(":checked")) {
        $('#'+table_id+' input:checkbox').prop("checked", "checked");
        $('#' + table_id +'  tr').css("background", "#afcce6");
    } else {
        $('#' + table_id +'  input:checkbox').prop("checked", false);
        $('#' + table_id +'  tr').css("background", "transparent");
    }
}
function define_type() {
    var type = $("#ddltype").val();
    if (type == 1) {
        $("#child_info").css("display","block")
        $("#money_data").css("display","none")
    } else {
        $("#child_info").css("display", "none")
        $("#money_data").css("display", "block")
    }
}
function gmod(n, m) {
    return ((n % m) + m) % m;
}

function kuwaiticalendar(today) {
   // var today = new Date();
    //if (adjust) {
    //    adjustmili = 1000 * 60 * 60 * 24 * adjust;
    //    todaymili = today.getTime() + adjustmili;
    //    today = new Date(todaymili);
    //}
    debugger;;
    day = today.getDate();
    month = today.getMonth();
    year = today.getFullYear();
    m = month + 1;
    y = year;
    if (m < 3) {
        y -= 1;
        m += 12;
    }

    a = Math.floor(y / 100.);
    b = 2 - a + Math.floor(a / 4.);
    if (y < 1583) b = 0;
    if (y == 1582) {
        if (m > 10) b = -10;
        if (m == 10) {
            b = 0;
            if (day > 4) b = -10;
        }
    }

    jd = Math.floor(365.25 * (y + 4716)) + Math.floor(30.6001 * (m + 1)) + day + b - 1524;

    b = 0;
    if (jd > 2299160) {
        a = Math.floor((jd - 1867216.25) / 36524.25);
        b = 1 + a - Math.floor(a / 4.);
    }
    bb = jd + b + 1524;
    cc = Math.floor((bb - 122.1) / 365.25);
    dd = Math.floor(365.25 * cc);
    ee = Math.floor((bb - dd) / 30.6001);
    day = (bb - dd) - Math.floor(30.6001 * ee);
    month = ee - 1;
    if (ee > 13) {
        cc += 1;
        month = ee - 13;
    }
    year = cc - 4716;


    wd = gmod(jd + 1, 7) + 1;

    iyear = 10631. / 30.;
    epochastro = 1948084;
    epochcivil = 1948085;

    shift1 = 8.01 / 60.;

    z = jd - epochastro;
    cyc = Math.floor(z / 10631.);
    z = z - 10631 * cyc;
    j = Math.floor((z - shift1) / iyear);
    iy = 30 * cyc + j;
    z = z - Math.floor(j * iyear + shift1);
    im = Math.floor((z + 28.5001) / 29.5);
    if (im == 13) im = 12;
    id = z - Math.floor(29.5001 * im - 29);

    var myRes = new Array(3);

    //myRes[0] = day; //calculated day (CE)
    //myRes[1] = month - 1; //calculated month (CE)
    //myRes[2] = year; //calculated year (CE)
    //myRes[3] = jd - 1; //julian day number
    //myRes[4] = wd - 1; //weekday number
    myRes[1] = id; //islamic date
    myRes[2] = im - 1; //islamic month
    myRes[3] = iy; //islamic year
  return (id >= 10 ? id : "0" + id) + "/" + ((im) >= 10 ? (im) : "0" + (im) ) + "/" + iy; // padding
}
function getCaseReport() {
    window.open("../report_Module/Aslah/CaseReportRep?Case_id=" + $("#combobox").val(),"_blank");
}
function getCaseDetails() {
    window.open("../report_Module/Aslah/CaseDetailsRep?Case_id=" + $("#combobox").val(), "_blank");
}
function getReceive_and_deliver() {
    window.open("../report_Module/Aslah/Receive_and_deliverRep?Case_id=" + $("#receiving_delivery_details").find("#ddlcase_id").val() + "&details_id=" + $("#receiving_delivery_details").find("#lbldelivery_details").html(), "_blank");
}