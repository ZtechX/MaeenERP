/************************************/
// Created By : Ahmed Nayl
// Create Date : 10/10/2018 3:30 PM
// Description : This file contains all javaScript functions related to travel_agencies form 
/************************************/


//public variables
var deleteWebServiceMethod = "sessions.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "sessions.asmx/Edit";
var formAutoCodeControl = "lblmainid";
// load function set defualt values
var index_i = 0;
var w = 0;
var arr_id = [];
$(function () {
    try {
     
        form_load();
   
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
        loadDynamicTable('sessions', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
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
    } catch (err) {
        alert(err);
    }
}

function save() {
    try {
        if (Page_ClientValidate("vgroup")) {
            $("#pnlConfirm").hide();
            $("#SavedivLoader").show();
            var PosId = $("#lblmainid").html();
            $("#lbldate_m").html($("#divdate2 #txtDatem").val());
            $("#lbldate_hj").html($("#divdate2 #txtDateh").val());
            var atch_files = get_files_ArrJson();
            var basicData = generateJSONFromControls("divForm");
            sessions.save_sessions( PosId, basicData, atch_files, function (val) {
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
        var data = JSON.parse(val[0]);
        if (val[0] != "0") {
            fillControlsFromJson(data[0]);
            $("#divdate2 #txtDatem").val(data[0].date_m);
            $("#divdate2 #txtDateh").val(data[0].date_hj);
            fillImages(val[1]);
            //  $("#tblUploadedFiles tr").remove();

            // alert(val[2].Image_name);
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
            $('.action-btns').css('display', 'none');

            showSuccessMessage("Record Selected");
            if (formOperation == "update") {
                setformforupdate();
                formOperation = "";
            }
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
