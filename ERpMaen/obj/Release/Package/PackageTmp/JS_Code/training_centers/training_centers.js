
var deleteWebServiceMethod = "training_centers.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "training_centers.asmx/Edit";
var formAutoCodeControl = "lblmainid";

$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").show();

    try {
       // drawDynamicTable();
        form_load();
        //aslah.checkUser(function (val) {
            
        //    if (val == "Superadmin") {
        //        form_load();
        //        //$("#pnlConfirm").show();
        //        $("#divData").show();
        //        $("#SavedivLoader").hide();
        //        $("#comp_id").css("display", "");
        //        $("#Dyntabel").css("display", "");
        //    } else if (val != "0") {
                
        //        sms_setting.get_data(val, function (val1) {
        //            edit(val1);
        //            $("#ddlComps").val(val);
        //        });
        //    }
        //});

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
        if ($("#txtNumber").val().length != 10) {
            showErrorMessage("رقم الهوية  يجب أن يكون 10 ارقام");
            return;

        }


        if ($("#txttel").val().length != 10) {
            showErrorMessage("رقم الهاتف  يجب أن يكون 10 ارقام");
            return;

        }
        ////// email validation

        function isValidEmailAddress(emailAddress) {
            var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
            return pattern.test(emailAddress);
        }
        if (!isValidEmailAddress($("#TextEmail").val())) {
            showErrorMessage("من فضلك ادخل البريد الاليكترونى صحيح");
            return;
        }
            //////end email
        if (Page_ClientValidate("vgroup")) {
            $("#pnlConfirm").hide();
            $("#SavedivLoader").show();
            var basicData = generateJSONFromControls("divForm");
            var PosId = $("#lblmainid").html();
            training_centers.Save(PosId, basicData, function (val) {
                if (val == true) {
                    debugger;
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
    debugger
    resetAll();
 
        if (val[0] == "1") {
            var data = JSON.parse(val[1]);
            fillControlsFromJson(data[0]);
           
        }
        $("#pnlConfirm").hide();
        $("#divData").show();
        $("#SavedivLoader").hide();
}

function drawDynamicTable() {
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
        loadDynamicTable('Advisors', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch (err) {
        alert(err);
    }
}




function add() {
    try {
        prepareAdd();
        resetAll();
      //  getcode();
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