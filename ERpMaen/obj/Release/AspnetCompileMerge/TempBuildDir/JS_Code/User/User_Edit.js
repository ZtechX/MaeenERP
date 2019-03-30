/************************************/
// Created By : Ahmed Nayl
// Create Date : 16/11/2015 12:00 PM
// Description : This file contains all javaScript functions resopsable for editing departments
/************************************/

// global variables used in deleteItem function
var deleteWebServiceMethod = "User.asmx/Delete";

// global variable used in row_click and update functions
var editWebServiceMethod = "User.asmx/EditUser";
var formAutoCodeControl = "lblmainid";

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
        debugger
        var data = JSON.parse(val[1]);
        cancel();
        resetAll();
       // $("#lbldistuser").html("");
        if (val[0] != "0") {
            fillControlsFromJson(data[0]);
        //    alert(data[0].id);
            $("#lblmainid").html(data[0].id);
            if (data[0].id == 1) {
                $("#divcomp_id").css('display', "none");
                $("#ddlcomp_id").prop("required", false);
            } else {
                $("#divcomp_id").css('display', "block");
                $("#ddlcomp_id").prop("required", true);
            }
            fillPerm(val[2], val[3]);
            //  fillDist(val[3]);
           
            changeResearcher(function () {

                if (val[4] != "0") {
                    var P_data = JSON.parse(val[4]);
                    for (var i = 0; i < P_data.length; i++) {
                        $("#LiPlaces").find("#" + P_data[i].area_id).prop("checked", true);

                    }
                }
            });
          
           
            if (data[0].User_Image != "") {
                $("#divuploadimage img").prop("src", data[0].User_Image);
                $("#imgItemURL").prop("src", data[0].User_Image);
                $("#imagemain").prop("src", data[0].User_Image);
                $("#lblMainUserName").html(data[0].User_FullNameEn);

            } else {
                $("#divuploadimage img").prop("src", "");
                $("#imgItemURL").prop("src", "");
            }
            //showSuccessMessage("Record Selected1");
            $("#bg-purple").addClass("in");
            $("#bg-purple").attr("style", "");
            $("#cmdSave").attr("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
            if (formOperation== "update") {
                setformforupdate();
                formOperation = "";
            }
            $("#pnlConfirm").hide();
            $("#divData").show();
            $("#SavedivLoader").hide();
            
        } else {
            showErrorMessage("No data found !!");
        }
    } catch (err) {
        alert(err);
    }
}


function fillPerm(data,data2) {
    try {
        
        var str = "";
        if (data != "") {
            var files = JSON.parse(data);
            var menu = JSON.parse(data2);
            if (files.length > 0) {            
                for (i = 0; i < files.length; i++) {
               //     alert(files[i].ID + "          " + files[i].formid);
                    var formid = files[i].formid;
                    for (y = 0; y < menu.length; y++) {
                        $("#perm_table" + menu[y].Id).find("#" + formid).attr("permid", files[i].ID);
                        $("#perm_table" + menu[y].Id).find("#" + formid).attr("formid", files[i].formid);
                        var Access = $("#perm_table" + menu[y].Id).find("#" + formid).find("#" + formid + "PAccess");
                        var Add= $("#perm_table" + menu[y].Id).find("#" + formid).find("#" + formid + "PAdd");
                        var Edit = $("#perm_table" + menu[y].Id).find("#" + formid).find("#" + formid + "PEdite");
                        var Delete =  $("#perm_table" + menu[y].Id).find("#" + formid).find("#" + formid + "PDelete");
                        Access.prop("checked", files[i].paccess);
                        checkBoxChange(Access, files[i].paccess);
                        Add.prop("checked", files[i].padd);
                        checkBoxChange(Add, files[i].padd);
                        Edit.prop("checked", files[i].PEdite);
                        checkBoxChange(Edit, files[i].PEdite);
                        Delete.prop("checked", files[i].pdelete);
                        checkBoxChange(Delete, files[i].pdelete);

                    }
                }
            }
        }
    } catch (error) {
        alert(error);
    }
}


function fillDist(data) {
    try {
        var str = "";
        if (data != "") {
            var files = JSON.parse(data);
            if (files.length > 0) {
                for (i = 0; i < files.length; i++) {
                    var distid = files[i].Dist_id;
                    if (distid != "") {
                        $("#ulbasictree").find("#" + distid).prop("checked", true);
                        $("#lbldistuser").html($("#lbldistuser").html() + files[i].Dist_id + "|");
                    }
                }
            }
        }
    } catch (error) {
        alert(error);
    }
}