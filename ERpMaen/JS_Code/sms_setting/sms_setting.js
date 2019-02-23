
var deleteWebServiceMethod = "sms_setting.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "sms_setting.asmx/Edit";
var formAutoCodeControl = "lblmainid";

$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").show();

    try {

        sms_setting.checkUser(function (val) {
            
            if (val == "Superadmin") {
                form_load();
                //$("#pnlConfirm").show();
                $("#divData").show();
                $("#SavedivLoader").hide();
                $("#comp_id").css("display", "");
                $("#Dyntabel").css("display", "");
            } else if (val != "0") {
                
                sms_setting.get_data(val, function (val1) {
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

                    drawDynamicTable();
                    cancel();
                   
                } else {
                    showErrorMessage("لم يتم الحفظ");
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
            showSuccessMessage("Record Selected");
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
            if (formOperation == "update") {
                setformforupdate();
                formOperation = "";
            }
        }
        $("#pnlConfirm").hide();
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