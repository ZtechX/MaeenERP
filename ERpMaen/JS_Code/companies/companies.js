/************************************/
// Created By : Ahmed Nayl
// Create Date : 10/10/2018 3:30 PM
// Description : This file contains all javaScript functions related to travel_agencies form 
/************************************/


//public variables
var deleteWebServiceMethod = "companies.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "companies.asmx/Edit";
var formAutoCodeControl = "lblunitsid";
// load function set defualt values
var index_i = 0;
var w = 0;
var arr_id = [];
var Date_m = "";
var Date_hj = "";

$(function () {
    try {
        setDate();
        get_admin();
        get_form_for_permtion();
   
    } catch (err) {
        alert(err);
    }
});
function setDate() {
 
    Date_m =  Pub_date_m;
    Date_hj = Pub_date_hj;
}
function get_admin() {

    companies.get_admin( function (val) {
        debugger
        if (val[1] != "0") {
            var comp = JSON.parse(val[1]);
            var comp_admin = JSON.parse(val[0]);
         
            fillControlsFromJson(comp[0], "divComp");
            fillControlsFromJson(comp_admin[0], "divAdminComp");
            $("#compLogo").prop("src", comp[0].image_path);
            $("#Comp_imgItemURL").prop("src", comp_admin[0].User_Image);

            //fillControlsFromJson(comp[0],"divForm");
            //$("#imgItemURL").attr("src", comp[0].image_path);
            $("#user_name").val(comp_admin[0].User_Name);
            $("#user_password").val(comp_admin[0].User_Password);
            $("#Li2").css("display", "none");
            $("#Li1").css("display", "none");
            $("#panel-1").css('display', "none");

            $("#divdate2 #txtDatem").val(comp[0].deal_start_date_m);
            $("#divdate2 #txtDateh").val(comp[0].deal_start_date_hj);
            $("#divdate3 #txtDatem").val(comp[0].deal_end_date_m);
            $("#divdate3 #txtDateh").val(comp[0].deal_end_date_hj);
            $("#divdate4 #txtDatem").val(comp[0].maintainance_start_date_m);
            $("#divdate4 #txtDateh").val(comp[0].maintainance_start_date_hj);
            $("#divdate5 #txtDatem").val(comp[0].maintainance_end_date_m);
            $("#divdate5 #txtDateh").val(comp[0].maintainance_end_date_hj);
            $("#ddlcomp_id").html(comp[0].id);
            $("#lbluser_id").html(comp_admin[0].id);
            $("#lblgroup_id").html(comp_admin[0].group_id);
            get_form_for_permtion_for_edit(comp[0].id, comp_admin[0].group_id)
            $("#maintance").prop('checked', comp[0].maintainance);
            get_maintance();
            if (val[2] != "0") {
                debugger
                var Ac_admin = JSON.parse(val[2]);
                var Acadyme = JSON.parse(val[3]);
                fillControlsFromJson(Ac_admin[0], "Ac_adminDiv");
                fillControlsFromJson(Acadyme[0], "AcadmeyDiv");
                Date_m = Acadyme[0].date_m;
                Date_hj = Acadyme[0].date_hj;

                $("#Ac_imgItemURL").prop("src", Ac_admin[0].User_Image);

            }
            if (val[4] != "0") {
                var Cen_admin = JSON.parse(val[4]);
                var center = JSON.parse(val[5]);
                fillControlsFromJson(Cen_admin[0], "Cen_adminDiv");
                fillControlsFromJson(center[0], "CenterDiv");
                Date_m = center[0].date_m;
                Date_hj = center[0].date_hj;

                $("#Cen_imgItemURL").prop("src", Cen_admin[0].User_Image);
            }
        }
            if (val[6] == 1) {
                $("#Li2").css("display", "block");
                $("#Li1").css("display", "block");
                $("#clearBtn").css("display", "inline-block");
                $("#acPanal").hide();
                $("#CenPanal").hide();
                // form_load();
                drawDynamicTable();
            }
     
    });

}


function edit(val) {
   
    
    resetAll();
    if (val[0] == "1") {
        var data = JSON.parse(val[1]);
        $("#divdate2 #txtDatem").val(data[0].deal_start_date_m);
        $("#divdate2 #txtDateh").val(data[0].deal_start_date_hj);
        $("#divdate3 #txtDatem").val(data[0].deal_end_date_m);
        $("#divdate3 #txtDateh").val(data[0].deal_end_date_hj);
        $("#divdate4 #txtDatem").val(data[0].maintainance_start_date_m);
        $("#divdate4 #txtDateh").val(data[0].maintainance_start_date_hj);
        $("#divdate5 #txtDatem").val(data[0].maintainance_end_date_m);
        $("#divdate5 #txtDateh").val(data[0].maintainance_end_date_hj);
        $("#ddlcomp_id").html(data[0].id);
        $("#lbluser_id").html(data[0].user_id);
        $("#lblgroup_id").html(data[0].group_id);

        get_form_for_permtion_for_edit(data[0].id, data[0].group_id);

        $("#maintance").prop('checked', data[0].maintainance);
        get_maintance();
      
        fillControlsFromJson(data[0],"divForm");
        if (data[0].image_path != "") {
            $("#imgItemURL").prop("src", data[0].image_path);

            $("#divuploadimage img").prop("src", data[0].image);
            
            $("#imagemain").prop("src", data[0].image);
            $("#lblMainUserName").html(data[0].image);

        } else {
            $("#divuploadimage img").prop("src", "");
            $("#imgItemURL").prop("src", "");
        }
    }
}

// draw dynamic table for existing travel_agencies
function drawDynamicTable() {
    try {
        var tableSortingColumns = [
            { orderable: false }, null, null, null, null, 
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" },
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 1;
        loadDynamicTable('companies', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
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
        $("#imgItemURL").prop("src", "../App_Themes/images/add-icon.jpg");
    } catch (err) {
        alert(err);
    }
}

function save_companies() {
    try {
        var arr_json = [];
        if ($("#loginUser").val() == "1") {
            if (Page_ClientValidate("vgroup1")) {
                var basicData_divComp = generateJSONFromControls("divComp");
                basicData_divComp["image_path"] = $("#compLogo").prop("src");
                var basicData_divAdminComp = generateJSONFromControls("divAdminComp");
                basicData_divAdminComp["User_Image"] = $("#Comp_imgItemURL").prop("src");
                arr_json = [basicData_divComp, basicData_divAdminComp];
                companies.save_companies(arr_json, Date_m, Date_hj, function (val) {

                    if (val[0] == '1') {
                        showSuccessMessage('تم تسجيل البيانات بنجاح');
                        cancel2();
                        get_admin();
                    }
                    else {
                        showErrorMessage(val[0]);
                    }
                });
            } else {
                showErrorMessage("يرجى ادخال البيانات المطلوبه");
            }
        } else {
            if (Page_ClientValidate("vgroup") && Page_ClientValidate("vgroup1")) {

                var basicData_divComp = generateJSONFromControls("divComp");
                basicData_divComp["image_path"] = $("#compLogo").prop("src");
                var basicData_divAdminComp = generateJSONFromControls("divAdminComp");
                basicData_divAdminComp["User_Image"] = $("#Comp_imgItemURL").prop("src");
                var basicData_AcadmeyDiv = generateJSONFromControls("AcadmeyDiv");
                var basicData_Ac_adminDiv = generateJSONFromControls("Ac_adminDiv");
                basicData_Ac_adminDiv["User_Image"] = $("#Ac_imgItemURL").prop("src");
                var basicData_CenDiv = generateJSONFromControls("CenterDiv");
                var basicData_Cen_adminDiv = generateJSONFromControls("Cen_adminDiv");
                basicData_Cen_adminDiv["User_Image"] = $("#Cen_imgItemURL").prop("src");
                 arr_json = [basicData_divComp, basicData_divAdminComp, basicData_AcadmeyDiv, basicData_Ac_adminDiv, basicData_CenDiv, basicData_Cen_adminDiv];
                companies.save_companies(arr_json, Date_m, Date_hj, function (val) {

                    if (val[0] == '1') {
                        showSuccessMessage('تم تسجيل البيانات بنجاح');
                        cancel2();
                        get_admin();
                    }
                    else {
                        showErrorMessage(val[0]);
                    }
                });
            }
            else {
                showErrorMessage("يرجى ادخال البيانات المطلوبه");
            }
        }
    } catch (err) {
        alert(err);
    }
}
// save_payments
function save_modules() {
    if (Page_ClientValidate("vgroup")) {
        var comp_id = $("#ddlcomp_id").html();
        var PosId = $("#lblmainid").html();
        var objects_arr = [];

        if (comp_id == '0' || comp_id == "") {
            $("#divfailed").css('display', "block");
            $("#divfailed").html("عفوا يجب ان تختار الجمعية<br>");
            showErrorMessage("عفوا يجب ان تختار الجمعية");
            return;
        }
        $("#pnlConfirm").hide();
        $("#SavedivLoader").show();
        $('#perm_table tbody tr').each(function () {
            var row = $(this);

            if (row.find('input[type="checkbox"]').is(':checked')) {
                var module_id = $(this).find("#contact_id").html();
                var data = { "comp_id": comp_id, "user_id": 0, "module_id": module_id };
                objects_arr.push(data);
            }
        });

        var comp_name = $("#txtname_ar").val();
        var userId = $("#lbluser_id").html();
        var group_id = $("#lblgroup_id").html();
        debugger;
        companies.save_modules(PosId, comp_name, userId, group_id, objects_arr, function (val) {
            if (val != 0) {
               
                //$("#SavedivLoader").hide();
                //$("#divRes").css("display", "none");
                //$("#divfailed").html("");
                $("#lblResError").hide();
                showSuccessMessage("تم الحفظ بنجاح");
                
            } else {
                showErrorMessage('لم يتم الحفظ');
            }
        });
    } else {
        showErrorMessage("يرجى ادخال البيانات المطلوبه");
    }

}

function save_contract() {
    debugger;
    var comp_id = $("#ddlcomp_id").html(); 
    if (comp_id == '0' || comp_id == "") {
        $("#divfailed").css('display', "block");
       // $("#divfailed").html("عفوا يجب ان تختار الجمعية<br>");
        showErrorMessage("عفوا يجب ان تختار الجمعية");
        return;
    }
            $("#lblstart_date").html($("#divdate2 #txtDatem").val());
            $("#lblstart_date_hj").html($("#divdate2 #txtDateh").val());
            $("#lblend_date").html($("#divdate3 #txtDatem").val());
            $("#lblend_date_hj").html($("#divdate3 #txtDateh").val());
            $("#lblmaintaianace_start_date").html($("#divdate4 #txtDatem").val());
            $("#lblmaintaianace_start_date_hj").html($("#divdate4 #txtDateh").val());
            $("#lblmaintaianace_end_date").html($("#divdate5 #txtDatem").val());
          $("#lblmaintaianace_end_date_hj").html($("#divdate5 #txtDateh").val());
    var basicData = generateJSONFromControls("div_contract");
    var atch_files = get_files_ArrJson();

    companies.save_contract(comp_id, basicData, atch_files, function (val) {
        if (val != 0) {
            showSuccessMessage("تم الحفظ بنجاح");
            $("#divdate2 #txtDatem").val("");
            $("#divdate2 #txtDateh").val("");
           $("#divdate3 #txtDatem").val("");
           $("#divdate3 #txtDateh").val("");
          $("#divdate4 #txtDatem").val("");
            $("#divdate4 #txtDateh").val("");
            $("#divdate5 #txtDatem").val("");
            $("#divdate5 #txtDateh").val("");
            $("#maintance").prop('checked', false);
            get_maintance();
        } else {

            showErrorMessage('لم يتم الحفظ');
        }
        });


}



function show_project(event, flag) {
        var row = $(event.target).closest("tr");
        var id = $(row).prop("id");
    $("#ddlcomp_id").html(id);
    $("#lblmainid").html(id);
    companies.show_all(id, function (val) {
        var project = JSON.parse(val[0]);
        var users = JSON.parse(val[3]);
        $("#user_name").val(users[0].User_Name);
        $("#user_password").val(users[0].User_Password);
        $("#lbluser_id").html(users[0].id);
        fillControlsFromJson(project[0]);
        $("#divdate2 #txtDatem").val(project[0].deal_start_date_m);
        $("#divdate2 #txtDateh").val(project[0].deal_start_date_hj);
        $("#divdate3 #txtDatem").val(project[0].deal_end_date_m);
        $("#divdate3 #txtDateh").val(project[0].deal_end_date_hj);
        $("#divdate4 #txtDatem").val(project[0].maintainance_start_date_m);
        $("#divdate4 #txtDateh").val(project[0].maintainance_start_date_hj);
        $("#divdate5 #txtDatem").val(project[0].maintainance_end_date_m);
        $("#divdate5 #txtDateh").val(project[0].maintainance_end_date_hj);
        $("#imgItemURL").attr("src", project[0].image_path);
        if (project[0].maintainance == '1') {
            $("#maintance_company").css("display", "block");

        } else {

            $("#maintance_company").css("display", "none");
        }
        if (val[1] != '0') {
            var modules = JSON.parse(val[1]);
            for (var x = 0; x < modules.length; x++) {

                $('#tr_' + modules[x].module_id).find('input[type="checkbox"]').prop("checked", "checked");
                $('#tr_' + modules[x].module_id).css("background", "#afcce6");

            }
            //if ($(e).is(':checked')) {

            //    $('#tr_' + id).css("background", "#afcce6");
            //}
            //else { $('#tr_' + id).css("background", "transparent"); }

        } else {
            $('#perm_table tbody tr input:checkbox').prop("checked", false);
            $('#perm_table tbody tr').css("background", "transparent");
        }
        fillImages(val[2]);
    });

}

function get_form_for_permtion() {
    try {
        companies.get_main_menu(1, function (val) {
            var data = JSON.parse(val[0]);
            console.log(data);
            var obj = JSON.stringify(data[x]);
            var div_show = " <table class='table ' id='perm_table' ><thead>" +
                "<tr><th>#</th><th>الموديول</th>" +
                " <th>  <input name='form-field-checkbox' type='checkbox' id='check_form' onchange='mark_all()' value='2' class='ace input-lg' /></th> <tbody>";
            for (var x = 0; x < data.length; x++) {
                div_show = div_show + "<tr  id='tr_" + data[x].Id + "' class='row-content' data-obj='" + obj + "' ><td>" + (x + 1) + "</td> " +
                    "<td id='contact_name'>" + data[x].ArMenuName + "</td>" +
                    "<td id='contact_id' style='display:none'>" + data[x].Id + "</td>" +
                    "<td class='check'> <label><input type='checkbox'    onchange='check_row(this," + data[x].Id + ");' value=''></label></td></tr>";
            }
               $('#tablePrint').html("</tbody>"+div_show);

            //} else {
            //    showErrorMessage("No data found !!");
            //}
        });
    } catch (err) {
        alert(err);
    }
}

function get_form_for_permtion_for_edit(comp_id,group_id) {
    try {

        companies.get_main_menu_for_edit(comp_id, group_id, function (val) {
            var data = JSON.parse(val[0]);
            var data2 = JSON.parse(val[1]);

            var obj = JSON.stringify(data[x]);
            var div_show = " <table class='table ' id='perm_table' ><thead>" +
                "<tr><th>#</th><th>الموديول</th>" +
                " <th>  <input name='form-field-checkbox' type='checkbox' id='check_form' onchange='mark_all()' value='2' class='ace input-lg' /></th> <tbody>";
            for (var x = 0; x < data.length; x++) {
                var flage = 0;
                for (var y = 0; y < data2.length; y++) {
                    if (data[x].Id == data2[y].module_id) {
                        flage = 1;
                    }
                  }
                
                if (flage == 1) {
                    div_show = div_show + "<tr  id='tr_" + data[x].Id + "' class='row-content' data-obj='" + obj + "' ><td>" + (x + 1) + "</td> " +
                        "<td id='contact_name'>" + data[x].ArMenuName + "</td>" +
                        "<td id='contact_id' style='display:none'>" + data[x].Id + "</td>" +
                        "<td class='check'> <label><input  type='checkbox'  checked  onchange='check_row(this," + data[x].Id + ");' value=''></label></td></tr>";

                 }
                else {
                    div_show = div_show + "<tr  id='tr_" + data[x].Id + "' class='row-content' data-obj='" + obj + "' ><td>" + (x + 1) + "</td> " +
                        "<td id='contact_name'>" + data[x].ArMenuName + "</td>" +
                        "<td id='contact_id' style='display:none'>" + data[x].Id + "</td>" +
                        "<td class='check'> <label><input  type='checkbox'    onchange='check_row(this," + data[x].Id + ");' value=''></label></td></tr>";

                }


            }
            $('#tablePrint').html("</tbody>" + div_show);

            //} else {
            //    showErrorMessage("No data found !!");
            //}
        });
    } catch (err) {
        alert(err);
    }
}


function check_row(e, id) {
    if ($(e).is(':checked')) {

        $('#tr_' + id).css("background", "#afcce6");
    }
    else { $('#tr_' + id).css("background", "transparent"); }

}
function get_maintance() {
    var maintance = $('#maintance').val();
    if ($('#maintance').is(":checked")) {
        $("#maintance_company").css("display", "block");
    } else {
        $("#maintance_company").css("display", "none");

    }
}
function mark_all() {
    if ($('#check_form').is(":checked")) {
        $('#perm_table input:checkbox').prop("checked", "checked");
        $('#perm_table tr').css("background", "#afcce6");
    } else {
        $('#perm_table input:checkbox').prop("checked", false);
        $('#perm_table tr').css("background", "transparent");
    }
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
function cancel2() {
    resetDivControls("divForm");
    resetDivControls("AcadmeyDataDiv")
    resetDivControls("CenterDataDiv")

    var basicData_divComp = generateJSONFromControls("divComp");
    basicData_divComp["image_path"] = $("#compLogo").prop("src");
    var basicData_divAdminComp = generateJSONFromControls("divAdminComp");
    basicData_divAdminComp["User_Image"] = $("#Comp_imgItemURL").prop("src");
    var basicData_AcadmeyDiv = generateJSONFromControls("AcadmeyDiv");
    var basicData_Ac_adminDiv = generateJSONFromControls("Ac_adminDiv");
    basicData_Ac_adminDiv["User_Image"] = $("#Ac_imgItemURL").prop("src");
    var basicData_CenDiv = generateJSONFromControls("CenterDiv");
    var basicData_Cen_adminDiv = generateJSONFromControls("Cen_adminDiv");
    basicData_Cen_adminDiv["User_Image"] = $("#Cen_imgItemURL").prop("src");
    setDate();

    $('#perm_table input:checkbox').prop("checked", false);
    $('#perm_table tr').css("background", "transparent");
    $("#divdate2 #txtDatem").val("");
    $("#divdate2 #txtDateh").val("");
    $("#divdate3 #txtDatem").val("");
    $("#divdate3 #txtDateh").val("");
    $("#divdate4 #txtDatem").val("");
    $("#divdate4 #txtDateh").val("");
    $("#divdate5 #txtDatem").val("");
    $("#divdate5 #txtDateh").val("");
    $("#ddlcomp_id").html("");
    $("#lblmainid").html("");
    $("#lbluser_id").html("");
    $("#check_userName").html("");
    add();
   
}
//function seteditid() {
//    $("#lblmainid").html($("#txtTemId").val());
//}

function add() {
    try {
        prepareAdd();
        resetAll();
    } catch (err) {
        alert(err);
    }
}


