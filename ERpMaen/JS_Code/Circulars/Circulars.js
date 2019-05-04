/************************************/
// Created By : Ahmed Nayl
// Create Date : 10/10/2018 3:30 PM
// Description : This file contains all javaScript functions related to travel_agencies form 
/************************************/


//public variables
var deleteWebServiceMethod = "Circulars.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "Circulars.asmx/Edit";
var formAutoCodeControl = "lblmianid";
// load function set defualt values
$(function () {
    try {
        setMaxBarcode();
        form_load();
    } catch (err) {
        alert(err);
    }
});

function setMaxBarcode() {
    Circulars.getBarCode(function (val) {
        $("#txtcode").val(val);
    });
}

// draw dynamic table for existing travel_agencies
function drawDynamicTable() {
    try {
        var tableSortingColumns = [
            { orderable: false }, null,null, null, null, null, 
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" },  { type: "text" }, { type: "text" },  { type: "text" },{ type: "text" },
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 0;
        loadDynamicTable('Circulars', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
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
            //var saveType = $("#cmdSave").attr("CommandArgument");
            var PosId = $("#lblmianid").html();
    
            var atch_files = get_files_ArrJson();
            Circulars.Save(PosId, basicData, atch_files, function (val) {
                if (val == true) {
                    cancel();
                    resetAll();
                    drawDynamicTable();
                    showSuccessMessage('تم تسجيل البيانات بنجاح');
                  

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
               // $('#tblUploadedFiles').remove();
               // createTblUploadedFiles(["Index", "File", "Name", "Delete"]);
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

    //var ddlemp_id = $("#ddlemp_id").val();
    //var app_date = $("#app_date").val().replace(/\//g, '-');
    //var app_year = $("#app_year").val();
    if (letter_id > 0) {
        // var url = "../Reports_Module/emp_apprisal_rep.aspx?ddlemp_id=" + ddlemp_id + "&app_date=" + app_date + "&app_year=" + app_year + "&app_id=" + app_id;
        //  alert(app_id);
        var url = "out_inside_letters.aspx?letter_id=" + letter_id;

        window.open(url, '_blank');
    }
    // alert(url);
}
function setformforupdate() {
    try {
        setformforupdate_all();
    } catch (err) {
        alert(err);
    }
}
