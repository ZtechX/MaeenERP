﻿
var deleteWebServiceMethod = "sms_setting.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "sms_setting.asmx/Edit";
var formAutoCodeControl = "lblmainid";
var Superadmin = false;
$(function () {
    
    $("#divData").hide();
    $("#SavedivLoader").show();

    try {

        sms_setting.checkUser(function (val) {
            
            if (val == "Superadmin") {
                Superadmin = true;
                $("#pnlConfirm").hide();
                form_load();
                //$("#pnlConfirm").show();
                $("#divData").show();
                $("#SavedivLoader").hide();
                $("#comp_id").css("display", "");
                $("#Dyntabel").css("display", "");
                $("#Comp_div").show();
            } else if (val != "0") {
                $("#Comp_div").hide();
                sms_setting.get_data(val, function (val1) {
                    edit(val1);
                    $("#ddlComps").val(val);
                    $("#DivAction").remove();
                });
            }
        });

    } catch (err) {
        alert(err);
    }
});

function resetAll() {
    try {
        resetFormControls();
        $("#lblmainid").html("");
    
    } catch (err) {
        alert(err);
    }
}

function save() {

    try {
        
        $("input").removeClass('error');
        $("select").removeClass('error');
        if (Page_ClientValidate("vgroup")) {
            $("#pnlConfirm").hide();
            $("#SavedivLoader").show();
            var basicData = generateJSONFromControls("divForm");
            var PosId = $("#lblmainid").html();
            sms_setting.Save(PosId, basicData, function (val) {
                
                if (val == true) {
                  
                    showSuccessMessage("تم الحفظ بنجاح");
                    if (Superadmin) {
                        drawDynamicTable();
                        cancel();
                        $("#pnlConfirm").hide();
                    } else {
                        $("#pnlConfirm").show();
                    }
                   
                } else {
                    showErrorMessage("لم يتم الحفظ");
                    $("#pnlConfirm").show();
                }
               
                $("#SavedivLoader").hide();
            });
        }
    } catch (err) {
        alert(err);
    }
}

function edit(val) {
    resetAll();
 
        if (val[0] == "1") {
            var data = JSON.parse(val[1]);
            fillControlsFromJson(data[0]);
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
            if (formOperation == "update") {
                setformforupdate();
                formOperation = "";
            }
        }
        $("#divData").show();
        $("#SavedivLoader").hide();
}

function drawDynamicTable() {
    try {
        var tableSortingColumns = [
                { orderable: false }, null, null, null, null, null,null,
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" },  { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" },
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 0;
        loadDynamicTable('sms_setting', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch (err) {
        alert(err);
    }
}




function add() {
    try {
        prepareAdd();
        resetAll();
    } catch (err) {
        alert(err);
    }
}

function setformforupdate() {
    try {
        setformforupdate_all();
    } catch (err) {
        alert(err);
    }
}