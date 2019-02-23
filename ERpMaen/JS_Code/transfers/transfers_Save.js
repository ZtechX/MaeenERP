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
                var PosId = $("#lbltransfersId").html();
                transfers.Savetransfers(saveType, PosId, basicData, function (val) {
                    if (val == true) {
                        cancel();
                        resetAll();
                        drawDynamicTable();
                        showSuccessMessage("تم الحفظ بنجاح");
                    } else {
                        showErrorMessage(val);
                    }
                });
        }
    } catch (err) {
        alert(err);
    }
}