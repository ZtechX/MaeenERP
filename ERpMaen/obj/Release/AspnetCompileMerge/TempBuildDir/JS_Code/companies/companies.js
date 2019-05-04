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

    companies.get_admin(function (val) {
        
        if (val[0] == "admin") {
            $("#clearBtn").css("display", "inline-block");
            $("#ac_cen_Panal").hide();
            
            drawDynamicTable();
        } else {
            $("#liStep2").hide();
            $("#liStep3").hide();
            $("#liStep4").hide();
            $("#activate-step-2").hide();
            $("#Save").show();
            if (val[1] != "") {
           debugger
       
            var comp = JSON.parse(val[1]);
            var comp_admin = JSON.parse(val[0]);

            fillControlsFromJson(comp[0], "divComp");
            fillControlsFromJson(comp_admin[0], "divAdminComp");
            $("#compLogo").prop("src", comp[0].image_path);
            $("#Comp_imgItemURL").prop("src", comp_admin[0].User_Image);
            
            $("#user_password").val(comp_admin[0].User_Password);
            $("#Li2").css("display", "none");
            $("#Li1").css("display", "none");
            $("#panel-1").css('display', "none");

            $("#divdate2 #txtDatem").val(comp[0].deal_start_date_m);
            $("#divdate2 #txtDateh").val(comp[0].deal_start_date_hj);
            $("#divdate3 #txtDatem").val(comp[0].deal_end_date_m);
            $("#divdate3 #txtDateh").val(comp[0].deal_end_date_hj);
            $("#maintance").prop('checked', comp[0].maintainance);
            $("#chkManual").prop('checked', comp[0].active);
            if (comp[0].maintainance) {
                $("#divdate4 #txtDatem").val(comp[0].maintainance_start_date_m);
                $("#divdate4 #txtDateh").val(comp[0].maintainance_start_date_hj);
                $("#divdate5 #txtDatem").val(comp[0].maintainance_end_date_m);
                $("#divdate5 #txtDateh").val(comp[0].maintainance_end_date_hj);
            }
            $("#ddlcomp_id").html(comp[0].id);
            $("#lbluser_id").html(comp_admin[0].id);
            $("#lblgroup_id").html(comp_admin[0].group_id);
            get_form_for_permtion_for_edit(comp[0].id, comp_admin[0].group_id)
           
            if (val[2] != "") {
                var Acadyme = JSON.parse(val[2]);
                fillControlsFromJson(Acadyme[0], "AcadmeyDiv");
                Date_m = Acadyme[0].date_m;
                Date_hj = Acadyme[0].date_hj;
            }
            if (val[3] != "") {
                var center = JSON.parse(val[3]);
                fillControlsFromJson(center[0], "CenterDiv");
                Date_m = center[0].date_m;
                Date_hj = center[0].date_hj;
                
            }
        }
    }
     
    });

}

function edit(val) {
    resetAll();
    if (val[0] == "1") {
        debugger
        var data = JSON.parse(val[1]);
        $("#lblmainid").html(data[0].id);
        $("#divdate2 #txtDatem").val(data[0].deal_start_date_m);
        $("#divdate2 #txtDateh").val(data[0].deal_start_date_hj);
        $("#divdate3 #txtDatem").val(data[0].deal_end_date_m);
        $("#divdate3 #txtDateh").val(data[0].deal_end_date_hj);
        if (data[0].maintainance) {
            $("#divdate4 #txtDatem").val(data[0].maintainance_start_date_m);
            $("#divdate4 #txtDateh").val(data[0].maintainance_start_date_hj);
            $("#divdate5 #txtDatem").val(data[0].maintainance_end_date_m);
            $("#divdate5 #txtDateh").val(data[0].maintainance_end_date_hj);
            $("#maintance_company").show();
        } else {
            $("#divdate4 #txtDatem").val("");
            $("#divdate4 #txtDateh").val("");
            $("#divdate5 #txtDatem").val("");
            $("#divdate5 #txtDateh").val("");
            $("#maintance_company").hide();
        }
        $("#ddlcomp_id").html(data[0].id);
        $("#lbluser_id").html(data[0].user_id);
        $("#lblgroup_id").html(data[0].group_id);
       
        $("#maintance").prop('checked', data[0].maintainance);
        $("#chkManual").prop('checked', data[0].active);
        fillControlsFromJson(data[0], "divComp");
        if (val[2] != "") {
            var comp_admin = JSON.parse(val[2]);
            fillControlsFromJson(comp_admin[0], "divAdminComp");
        }
        get_form_for_permtion_for_edit(data[0].id, comp_admin[0].group_id);
        $("#compLogo").prop("src", data[0].image_path);
        $("#Comp_imgItemURL").prop("src", comp_admin[0].User_Image);
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
    } catch (err) {
        alert(err);
    }
}

function save_companies() {
    try {
        var arr_json = [];
        $("#SavedivLoader").show();
        if ($("#loginUser").val() == "1") {

            if ($("#txtindenty").val().length != 10) {
                $("#txtindenty").addClass("error");
                showErrorMessage("رقم الهوية يجب أن يكون 10 ارقام");
                return;
            }
            if ($("#tele").val().length != 10) {
                $("#tele").addClass("error");
                showErrorMessage("رقم الجوال يجب أن يكون 10 ارقام");
                return;
            }
            if ($("#comp_AdminNum").val().length != 10) {
                $("#comp_AdminNum").addClass("error");
                showErrorMessage("رقم الجوال يجب أن يكون 10 ارقام");
                return;
            }
                var FormId = $("#lblmainid").html();
                ///////////////basic
                var basicData_divComp = generateJSONFromControls("divComp");
                basicData_divComp["maintainance"] = $("#maintance").is(":checked");
                basicData_divComp["active"] =$("#chkManual").prop('checked'); 
                basicData_divComp["image_path"] = $("#compLogo").prop("src");
                var basicData_divAdminComp = generateJSONFromControls("divAdminComp");
                basicData_divAdminComp["User_Image"] = $("#Comp_imgItemURL").prop("src");
                ///////////modules
                var objects_arr = [];
                $('#perm_table tbody tr').each(function () {
                    var row = $(this);

                    if (row.find('input[type="checkbox"]').is(':checked')) {
                        var module_id = $(this).find("#contact_id").html();
                        var data = {  "module_id": module_id };
                        objects_arr.push(data);
                    }
                });
                //////////contract
                $("#lblstart_date").html($("#divdate2 #txtDatem").val());
                $("#lblstart_date_hj").html($("#divdate2 #txtDateh").val());
                $("#lblend_date").html($("#divdate3 #txtDatem").val());
                $("#lblend_date_hj").html($("#divdate3 #txtDateh").val());
                if ($("#maintance").is(":checked")) {
                    $("#lblmaintaianace_start_date").html($("#divdate4 #txtDatem").val());
                    $("#lblmaintaianace_start_date_hj").html($("#divdate4 #txtDateh").val());
                    $("#lblmaintaianace_end_date").html($("#divdate5 #txtDatem").val());
                    $("#lblmaintaianace_end_date_hj").html($("#divdate5 #txtDateh").val());
                }
                var contractData = generateJSONFromControls("div_contract");
                var atch_files = get_files_ArrJson();
                ///////////////////
                arr_json = [basicData_divComp, basicData_divAdminComp, objects_arr, contractData, atch_files];
                companies.save_companies(FormId,arr_json, Date_m, Date_hj, function (val) {
                    $("#SavedivLoader").hide();
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
            if (!checkRequired("divForm") && !checkRequired("Acadmey_CenterDataDiv")) { 
                if ($("#txtindenty").val().length != 10) {
                    $("#txtindenty").addClass("error");
                    showErrorMessage("رقم الهوية يجب أن يكون 10 ارقام");
                    return;
                }
                if ($("#tele").val().length != 10) {
                    $("#tele").addClass("error");
                    showErrorMessage("رقم الجوال يجب أن يكون 10 ارقام");
                    return;
                }
                if ($("#comp_AdminNum").val().length != 10) {
                    $("#comp_AdminNum").addClass("error");
                    showErrorMessage("رقم الجوال يجب أن يكون 10 ارقام");
                    return;
                }
                var FormId = $("#lblmainid").html();
                var basicData_divComp = generateJSONFromControls("divComp");
                basicData_divComp["image_path"] = $("#compLogo").prop("src");
                var basicData_divAdminComp = generateJSONFromControls("divAdminComp");
                basicData_divAdminComp["User_Image"] = $("#Comp_imgItemURL").prop("src");
                var basicData_AcadmeyDiv = generateJSONFromControls("AcadmeyDiv");
                var basicData_CenDiv = generateJSONFromControls("CenterDiv");
               arr_json = [basicData_divComp, basicData_divAdminComp, basicData_AcadmeyDiv, basicData_CenDiv];

                companies.save_companies(FormId,arr_json, Date_m, Date_hj, function (val) {
                    $("#SavedivLoader").hide();
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
                $("#SavedivLoader").hide();
                showErrorMessage("يرجى ادخال البيانات المطلوبه");
            }
        }
    } catch (err) {
        alert(err);
    }
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

        });
    } catch (err) {
        alert(err);
    }
}

function check_row(e, id) {
    if ($(e).is(':checked')) {
        $('#tr_' + id).css("background", "#afcce6");
    }
    else {
        $('#tr_' + id).css("background", "transparent");
    }

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


function cancel2() {
    resetDivControls("divForm");
    resetDivControls("AcadmeyDataDiv")
    resetDivControls("CenterDataDiv")

    $("#compLogo").prop("src","../App_Themes/images/add-icon.jpg");
    $("#Comp_imgItemURL").prop("src","../App_Themes/images/add-icon.jpg");
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
    setDate();
    $("#maintance").prop('checked', false);
    $("#maintance_company").hide();
    $("#chkManual").prop('checked', true); 

    $("#liStep1 a").click();
}


function add() {
    try {
        prepareAdd();
        resetAll();
    } catch (err) {
        alert(err);
    }
}

