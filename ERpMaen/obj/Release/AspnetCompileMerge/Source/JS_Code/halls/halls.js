/************************************/
// Created By : Ahmed Nayl
// Create Date : 16/11/2015 11:30 AM
// Description : This file contains all javaScript functions related to travel_agencies form 
/************************************/

// load function set defualt values
var deleteWebServiceMethod = "halls.asmx/Deletehalls";

// global variable used in row_click and update functions
var editWebServiceMethod = "halls.asmx/Edithalls";
var formAutoCodeControl = "lblhallsId";
$(function () {
    try {
        form_load();
       // get_features();
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
            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" }, 
        ];
        
        var tableColumnDefs = [
            
        ];
        var initialSortingColumn = 1;
        loadDynamicTable('Halls', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch(err) {
        alert(err);
    }
}

// empty pnlform and show it and hide function panel
function add() {
    try {
        prepareAdd();
        resetAll();
        getcode();
    } catch (err) {
        alert(err);
    }
}

// reset all controls when add or cancel
function resetAll() {
    try {
        resetFormControls();
        $("#lblhallsId").html("");
    } catch (err) {
        alert(err);
    }
}

function getcode() {
    try {
        halls.GetNewCode("", function (val) {
            $("#txtBarcode").val(val);
        });
    } catch (err) {
        alert(err);
    }
}

function get_features() {
    try {

       halls.get_features(function (data) {
            var areas = JSON.parse(data);
            $("#divAreaList").empty();
            if (areas.length > 0) {
                $("#divAreaList").append('<tr style="background-color: #89c3eb;height:35px" ><th>اختر</th><th>الميزة</th></tr>');
                for (i = 0; i < areas.length; i++) {
                    $("#divAreaList").append('<tr><td align="right" "><input type="checkbox" id=' + areas[i].ID + ' value=' + areas[i].ID + '></input></td><td><label>' + areas[i].aDescription + '</label></td></tr>');
                }
            }
            //  fillAreas();
        });
    } catch (err) {
        alert(err)
    }
}
/************************************/
// Created By : Ahmed Nayl
// Create Date : 16/11/2015 10:30 PM
// Description : This file contains all javaScript functions resopsable for saving travel_agencies
/************************************/

// save Area
function save() {
    try {
        if (Page_ClientValidate("vgroup")) {
            var arrData = new Array;
            var basicData = generateJSONFromControls("divForm");
            var saveType = $("#cmdSave").prop("CommandArgument");
            var PosId = $("#lblhallsId").html();
            var images = getImagesArrJson();
            var features_data = getfeatures_data_dataJson();
            halls.Savehalls(saveType, PosId, basicData, images, features_data, function (val) {
                if (val.split("|")[0] == "True") {
                    cancel();
                    resetAll();
                    drawDynamicTable();
                    showSuccessMessage("تم الحفظ بنجاح");
                } else {
                    showErrorMessage(val.split("|")[1]);
                }
            });
        }
    } catch (err) {
        alert(err);
    }
}
function getfeatures_data_dataJson() {
    try {
        var arrData = [];
        $("#divAreaList input").each(function () {
            if ($(this).prop("checked")) {
                var data = { "Id": $(this).prop("id") };
                arrData.push(data);
            }
        });
        return arrData;
    }
    catch (err) {
        alert(err);
    }

}
function getImagesArrJson() {
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

/************************************/
function setformforupdate() {
    try {
        setformforupdate_all();
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
            showSuccessMessage("Record Selected");
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
            get_featuresForEdit(data[0].id);

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
                $('#tblUploadedFiles').remove();
                createTblUploadedFiles(["Index", "File", "Name", "Main Image", "Delete"]);
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

function get_featuresForEdit(hall_id) {
    try {
        halls.get_features(function (data) {
            var areas = JSON.parse(data);
            $("#divAreaList").empty();
            if (areas.length > 0) {
                $("#divAreaList").append('<tr style="background-color: #89c3eb;height:35px" ><th>اختر</th><th>الميزة</th></tr>');
                for (i = 0; i < areas.length; i++) {
                    $("#divAreaList").append('<tr><td align="right" "><input type="checkbox" id=' + areas[i].ID + ' value=' + areas[i].ID + '></input></td><td><label>' + areas[i].aDescription + '</label></td></tr>');
                }
            }
            fillAreas(hall_id);
        });
    } catch (err) {
        alert(err)
    }
}


// get selected areas from db and select them
function fillAreas(hall_id) {
    try {
        var count = 0;
        halls.get_featuresForEdit(hall_id, function (data) {
            var DriverAreas = JSON.parse(data);
            if (DriverAreas.length > 0) {
                for (i = 0; i < DriverAreas.length; i++) {
                    $("#divAreaList #" + DriverAreas[i].ID).prop("checked", true);
                    count = count + 1;
                }
            }
        });
    } catch (err) {
        alert(err);
    }
}