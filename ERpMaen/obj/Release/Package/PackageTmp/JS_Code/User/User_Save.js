/************************************/
// Created By : Ahmed Nayl
// Create Date : 16/11/2015 10:30 PM
// Description : This file contains all javaScript functions resopsable for saving departments
/************************************/

function save() {

    try {
        
        $("input").removeClass('error');
        $("select").removeClass('error');
        if ($("#txtindenty").val().length != 10) {
            showErrorMessage("رقم الهوية  يجب أن يكون 10 ارقام");
            return;

        }
        
        if ($("#phone").val().length != 10) {
            showErrorMessage("رقم الجوال  يجب أن يكون 10 ارقام");
            return;

        }
        if (!checkRequired("divForm")) {
         //   $("#pnlConfirm").hide();
            $("#SavedivLoader").show();
                var arrData = new Array;
                var basicData = generateJSONFromControls("divForm");
                var saveType = $("#cmdSave").prop("CommandArgument");

                var UserId = $("#lblmainid").html();
               var permarrData = [];
               var mainImag = $("#imgItemURL").prop("src");
               var researchArea_arrData = [];
                User.menulength("1", function (val) { 
                    menu = JSON.parse(val[1]);
                    for (i = 0; i < menu.length;i++){
                        $("#perm_table" + menu[i].Id + " tbody tr").each(function () {
                            if ($(this).attr("formid") != undefined) {
                                var data = { "permid": $(this).attr("permid"), "UserId": "", "formid": $(this).attr("formid"), "PAccess": $(this).find("#" + $(this).prop("id") + "PAccess").prop("checked"), "PAdd": $(this).find('#' + $(this).prop("id") + 'PAdd').prop("checked"), "PEdite": $(this).find('#' + $(this).prop("id") + 'PEdite').prop("checked"), "PDelete": $(this).find('#' + $(this).prop("id") + 'PDelete').prop("checked") };
                                permarrData.push(data);
                            }
                    });
                    }
                    $("#LiPlaces").find('input[type=checkbox]:checked').each(function () {
                        var data = { "area_id": $(this).attr("id")};
                        researchArea_arrData.push(data);
                    });
                    User.SaveUser( UserId, basicData, permarrData,researchArea_arrData, mainImag, function (val) {
                        $("#SavedivLoader").hide();
                        if (val.split("|")[0] == "True") {                            
                            drawDynamicTable();
                            cancel();
                            showSuccessMessage("تم الحفظ بنجاح");
                           // $("#pnlConfirm").show();
                           // $("#SavedivLoader").hide();
                        } else {
                            showErrorMessage(val.split("|")[1]);
                            $("#pnlConfirm").show();
                            $("#SavedivLoader").hide();
                        }
                    });


                });
                
                
            }
        
    } catch (err) {
        alert(err);
    }
}


// check if fields have valid values or not
function checkFields() {
    try {
        var ddlControls = [
            { "ControlId": "ddlUserType", "ControlTitle": "User Type" },
        ];
        if (checkDdlControls(ddlControls) == false) {
            return false;
        }
        if ($("#ddlUserType").val()=="2" && $("#lbldistuser").html() == "") {
            showErrorMessage("Please Select Distributer ");
            return false;
        }
        return true;
    }
    catch (err) {
        alert(err);
    }
}

function getPermArrJson() {
    try {
        var arrData = [];
        for (y = 1; y < 6; y++) {
            $("#perm_table"+y +" tbody tr").each(function () {
            var data = { "permid": $(this).attr("permid"), "formid": $(this).attr("formid"), "access": $(this).find("#" + $(this).prop("id") + "access").prop("checked"), "add": $(this).find('#' + $(this).prop("id") + 'add').prop("checked"), "update": $(this).find('#' + $(this).prop("id") + 'update').prop("checked"), "delete": $(this).find('#' + $(this).prop("id") + 'delete').prop("checked") };
            arrData.push(data);
        });

    }
        return arrData;
    } catch (err) {
        alert(err);
    }
}


