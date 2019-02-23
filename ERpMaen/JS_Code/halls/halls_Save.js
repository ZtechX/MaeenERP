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
                halls.Savehalls(saveType, PosId, basicData,images,features_data, function (val) {
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