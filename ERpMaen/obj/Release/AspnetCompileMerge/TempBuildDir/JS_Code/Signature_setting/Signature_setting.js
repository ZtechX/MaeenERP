
var deleteWebServiceMethod = "Signature_setting.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "Signature_setting.asmx/Edit";
var formAutoCodeControl = "lblmainid";

$(function () {
    
    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").show();

    try {

        Signature_setting.checkUser(function (val) {
            
            if (val == "Superadmin") {
                form_load();
               // $("#pnlConfirm").show();
                $("#divData").show();
                $("#SavedivLoader").hide();
                $("#comp_id").css("display", "");
                $("#Dyntabel").css("display", "");
            } else if (val != "0") {
                
                Signature_setting.get_data(val, function (val1) {
                    edit(val1);
                    $("#ddlComps").val(val);
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
        $("#divuploadimage img").prop("src", "");

        $("#imgItemURL").attr("src", "../App_Themes/images/add-icon.jpg");
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
            var mainImag = $("#imgItemURL").prop("src");
            if (mainImag.indexOf("images") != -1) {
                Signature_setting.Save(PosId, basicData,mainImag, function (val) {
                    if (val == true) {
                        showSuccessMessage("تم الحفظ بنجاح");
                        drawDynamicTable();
                        cancel();
                    } else {
                        showErrorMessage("لم يتم الحفظ");
                    }
                   $("#SavedivLoader").hide();
                });
            } else {
                showErrorMessage("من فضل أدخل صورة التوقيع");
                $("#pnlConfirm").show();
                $("#SavedivLoader").hide();
            }
        }
    } catch (err) {
        alert(err);
    }
}

function edit(val) {
    debugger
        resetAll();
        if (val[0] == "1") {
            var data = JSON.parse(val[1]);
            fillControlsFromJson(data[0]);
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
            if (data[0].image != "") {
                $("#divuploadimage img").prop("src", data[0].image);
                $("#imgItemURL").prop("src", data[0].image);
                $("#imagemain").prop("src", data[0].image);
                $("#lblMainUserName").html(data[0].image);

            } else {
                $("#divuploadimage img").prop("src", "");
                $("#imgItemURL").prop("src", "");
            }
    }

    //resetAll();

    //if (val[0] == "1") {
    //    var data = JSON.parse(val[1]);
    //    fillControlsFromJson(data[0]);
    //    showSuccessMessage("Record Selected");
    //    $("#cmdSave").prop("CommandArgument", "Update");
    //    $("#cmdUpdate").removeAttr('disabled');
    //    $("#cmdDelete").removeAttr('disabled');
    //    if (formOperation == "update") {
    //        setformforupdate();
    //        formOperation = "";
    //    }
    //}
    $("#pnlConfirm").hide();
    $("#divData").show();
    $("#SavedivLoader").hide();
   
}

function drawDynamicTable() {
    debugger
    try {
        var tableSortingColumns = [
            { orderable: false }, null, null, null,  
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" },
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 0;
        loadDynamicTable('Signature_setting', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
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