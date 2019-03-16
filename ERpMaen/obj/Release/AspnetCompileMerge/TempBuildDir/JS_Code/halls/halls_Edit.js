/************************************/
// Created By : Ahmed Nayl
// Create Date : 16/11/2015 12:00 PM
// Description : This file contains all javaScript functions resopsable for editing travel_agencies
/************************************/

// global variables used in deleteItem function
var deleteWebServiceMethod = "halls.asmx/Deletehalls";

// global variable used in row_click and update functions
var editWebServiceMethod = "halls.asmx/Edithalls";
var formAutoCodeControl = "lblhallsId";

// enable pnl form for update
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

            if (formOperation== "update") {
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