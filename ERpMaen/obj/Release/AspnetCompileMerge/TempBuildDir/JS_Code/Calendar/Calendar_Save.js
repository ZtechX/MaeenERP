/************************************/
// Created By : Ahmed Nayl
// Create Date : 16/11/2015 10:30 PM
// Description : This file contains all javaScript functions resopsable for saving departments
/************************************/

function save() {
    try {
        if (checkFields()) {
            $("#divLoader").show();
            var arrData = new Array;
            var Manager_id = $("#ddlUsers").val();
            var shops = getSelectedShops();
            if (shops == "") {
                showErrorMessage("Please Select Shops!!!");
            } else {
                Calendar.SaveShop("New", Manager_id, shops, function (val) {
                    if (val) {
                        cancel();
                        resetAll();
                        drawDynamicTable();
                        showSuccessMessage("Record Saved Successfully");
                        $("#divLoader").hide();
                        $("#ddlUsers").val(0);
                        getManagerDetails();
                    } else {
                        showErrorMessage(val.split("|")[1]);
                    }
                });
            }
        }
    } catch (err) {
        alert(err);
    }
}


// check if fields have valid values or not
function checkFields() {
    try {
        if ($("#ddlUsers").val() == "0" || $("#ddlUsers").val() == "") {
            showErrorMessage("Please Select Manager!!!");
            return false;
        }
        return true;
    }
    catch (err) {
        alert(err);
    }
}

//get selected shops
function getSelectedShops() {
    try {
        var shops = "";
        $("#tblShops tbody tr").each(function () {
            if ($(this).find("#chkShops").prop("checked")) {
                shops = shops + $(this).prop("id")+",";
            }
        });
        return shops.slice(0, -1);
    } catch (err) {
        alert(err);
    }
}

