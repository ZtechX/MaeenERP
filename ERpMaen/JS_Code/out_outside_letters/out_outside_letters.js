/************************************/
// Created By : Ahmed Nayl
// Create Date : 10/10/2018 3:30 PM
// Description : This file contains all javaScript functions related to travel_agencies form 
/************************************/


//public variables
var deleteWebServiceMethod = "out_outside_letters.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "out_outside_letters.asmx/Edit";
var formAutoCodeControl = "lblmianid";
// load function set defualt values
$(function () {
    try {
        setMaxBarcode();
        form_load();
        $("#div_print").hide();
      //  $("#cmdSave").hide();
        $("#clearBtn").hide();
      //  $("#txtname_ar").prop('disabled', true);
    } catch (err) {
        alert(err);
    }
});

// draw dynamic table for existing travel_agencies
function drawDynamicTable() {
    try {
        var tableSortingColumns = [
            { orderable: false },null, null, null, null, null, null, null,
        ];
        var tableFilteringColumns = [
            { type: "null" },{ type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" },
        ];

        var tableColumnDefs = [

        ];
        //select tblletter.id as 'AutoCodeHide', tblletter.title as 'عنوان الخطاب ',tblletter.details as 'تفاصيل الخطاب', tbldepartmen.name as 'صادر من',to_d.name as 'صادر الي',tblUsers.User_Name as 'الراسل', 1 as ' تعديل /حذف' from tblletter inner join  tbldepartmen on tblletter.from_dep=tbldepartmen.id  inner join tbldepartmen to_d on to_d.id=tblletter.to_dep  inner join tblUsers on tblletter.add_by=tblUsers.id where tblletter.type=2 and ISNUll(tblletter.deleted,0)=0
        var initialSortingColumn = 0;
        loadDynamicTable('out_outside_letters', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch (err) {
        alert(err);
    }
}

function setMaxBarcode() {
    out_outside_letters.getBarCode(function (val) {
        $("#txtcode").val(val);
    });
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
        $("#lblmianid").html("");
        setMaxBarcode();
    } catch (err) {
        alert(err);
    }
}

function save() {
    try {
        if (!checkRequired()) {
            $("#lbldate").html($("#divdate1 #txtDatem").val());
            $("#lbldate_hj").html($("#divdate1 #txtDateh").val());
            var basicData = generateJSONFromControls("divForm");
            var saveType = $("#cmdSave").attr("CommandArgument");
            var PosId = $("#lblmianid").html();
            var atch_files = get_files_ArrJson();
            out_outside_letters.Save(saveType, PosId, basicData, atch_files, function (val) {
                if (val == true) {
                    cancel();
                    resetAll();
                    drawDynamicTable();
                    showSuccessMessage('تم تسجيل البيانات بنجاح');
                    $("#div_print").hide();
                   // $("#txtname_ar").prop('disabled', true);

                } else {
                    showErrorMessage(val.split("|")[1]);
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



// called after update function success
function edit(val) {
    try {
        var data = JSON.parse(val[1]);
        cancel();
        if (val[0] != "0") {

            $("input[name=radio-btn][value=" + data[0].from_type_id + "]").prop('checked', true);
            // get_fromdep(data[0].from_type_id);
            get_fromdep_replay(data[0].from_type_id, data[0].from_dep);
            $("input[name=radio-btn2][value=" + data[0].to_type_id + "]").prop('checked', true);
           // get_todep(data[0].to_type_id);
            get_todepreplay(data[0].to_type_id, data[0].to_dep);

            fillControlsFromJson(data[0]);
            fillImages(val[2]);
            $("#txtTemId").val($("#lblyearId").html());
            $("#divdate1 #txtDateh").val(data[0].date_hj);
            $("#divdate1 #txtDatem").val(data[0].date);
            $("#imgItemURL").prop("src", data[0].logo);
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
            $('.action-btns').css('display', 'none');
            if (data[0].type == 1) {
                $("#div_print").show();
               // $("#txtname_ar").prop('disabled', false);
            }
            else {
                $("#div_print").hide();
               // $("#txtname_ar").prop('disabled', false);
            }
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

function fillImages(data) {
    try {
        if (data != "") {
            var files = JSON.parse(data);
            if (files.length > 0) {
             //   $('#tblUploadedFiles').remove();
            //    createTblUploadedFiles(["Index", "File", "Name", "Delete"]);
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


function get_letter_data_for_replay() {

    var letter_id = $("#lblmianid").html();
 if (letter_id > 0) {
        // var url = "../Reports_Module/emp_apprisal_rep.aspx?ddlemp_id=" + ddlemp_id + "&app_date=" + app_date + "&app_year=" + app_year + "&app_id=" + app_id;
        //  alert(app_id);
        var url = "out_inside_letters.aspx?letter_id=" + letter_id;

        window.open(url, '_blank');
    }
    // alert(url);
}



function get_fromdep(dep_id) {
    try {
        $("#from_type_id").html(dep_id);
        out_inside_letters.get_fromdep(dep_id, function (val) {

            if (val[0] != "0") {
                $("#ddlfrom_dep").empty();
                $("#ddlfrom_dep").append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
                for (var i = 1; i <= val.length - 1; i++) {
                    $("#ddlfrom_dep").append($("<option ></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
                }

            } else {
                $("#ddlfrom_dep").append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
            }

        });

    } catch (error) {
        alert(error);
    }
}


function get_todep(dep_id) {
    try {
        $("#to_type_id").html(dep_id);

        out_inside_letters.get_fromdep(dep_id, function (val) {

            if (val[0] != "0") {
                $("#ddlto_dep").empty();
                $("#ddlto_dep").append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
                for (var i = 1; i <= val.length - 1; i++) {
                    $("#ddlto_dep").append($("<option ></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
                }

            } else {
                $("#ddlto_dep").append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
            }

        });

    } catch (error) {
        alert(error);
    }
}



function get_fromdep_replay(dep_type_id, dep_id) {
    try {
        $("#from_type_id").html(dep_id);
        out_inside_letters.get_fromdep(dep_type_id, function (val) {
            // alert(dep_id);
            if (val[0] != "0") {
                $("#ddlfrom_dep").empty();
                for (var i = 1; i <= val.length - 1; i++) {
                    if (val[i].split("|")[1] == dep_id) {
                        //  alert(val[i].split("|")[1] + "   " + dep_id);
                        $("#ddlfrom_dep").append($("<option selected='selected' ></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
                    } else {
                        $("#ddlfrom_dep").append($("<option ></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
                    }
                }

            } else {
                $("#ddlfrom_dep").append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
            }

        });

    } catch (error) {
        alert(error);
    }
}


function get_todepreplay(dep_type_id, dep_id) {
    try {
        $("#to_type_id").html(dep_id);

        out_inside_letters.get_fromdep(dep_type_id, function (val) {

            if (val[0] != "0") {
                $("#ddlto_dep").empty();
                $("#ddlto_dep").append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
                for (var i = 1; i <= val.length - 1; i++) {
                    if (val[i].split("|")[1] == dep_id) {
                        $("#ddlto_dep").append($("<option selected='selected' ></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
                    } else {
                        $("#ddlto_dep").append($("<option ></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
                    }
                }

            } else {
                $("#ddlto_dep").append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
            }

        });

    } catch (error) {
        alert(error);
    }
}