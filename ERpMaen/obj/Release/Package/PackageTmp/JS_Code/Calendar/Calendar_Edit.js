/************************************/
// Created By : Ahmed Nayl
// Create Date : 16/11/2015 12:00 PM
// Description : This file contains all javaScript functions resopsable for editing departments
/************************************/

// global variables used in deleteItem function
var deleteWebServiceMethod = "Shop.asmx/DeleteUser";

// global variable used in row_click and update functions
var editWebServiceMethod = "Shop.asmx/EditShop";
var formAutoCodeControl = "lblShopId";

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
        resetAll();
        $("#lblShopId").html("");
        if (val[0] != "0") {
            fillControlsFromJson(data[0]);
            
            if (data[0].User_Image != "") {
                $("#divuploadimage img").prop("src", data[0].User_Image);
                $("#imgItemURL").prop("src", data[0].User_Image);
                $("#imagemain").prop("src", data[0].User_Image);
                $("#lblMainUserName").html(data[0].User_FullNameEn);
            } else {
                $("#divuploadimage img").prop("src", "");
                $("#imgItemURL").prop("src", "");
            }
            showSuccessMessage("Record Selected1");
            $("#bg-purple").addClass("in");
            $("#cmdSave").attr("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
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