
var deleteWebServiceMethod = "ConsultationDetails.asmx/Delete";
// global variable used in row_click and update functions

var formAutoCodeControl = "lblmainid";
var consulat_id = "";
var oldAdvisor = "";
$(function () {
    
    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").show();

    try {
        form_load();
        
    } catch (err) {
        alert(err);
    }
});


function resetAll() {
    try {
        resetFormControls();
        $("#consult_id").html("");
        $("#pnlConfirm").hide();
    } catch (err) {
        alert(err);
    }
}
function viewConslute(id) {
    
    ConsultationDetails.Edit(id, function (val) {
        debugger
        prepareAdd();
        resetAll();
        if (val[0] == "1") {
            debugger
            var data = JSON.parse(val[1]);
            fillControlsFromJson(data[0]);
            $("#divdate2").find("#txtDatem").val($("#lblstart_date").html());
            $("#divdate2").find("#txtDateh").val($("#lblstart_date_hj").html());
            $("#divdate4").find("#txtDatem").val($("#lbl_dob_m").html());
            $("#divdate4").find("#txtDateh").val($("#lbl_dob_h").html());
            $("#appear_data").prop('checked', data[0].appear_data);
            if (data[0].created_By == $("#loginUser").val()) {
                $("#divCollapse2").show();
                $("#divCollapse3").show();
                $("#gander").hide();
            } else {
                if (data[0].appear_data) {
                    $("#divCollapse2").show();
                    $("#divCollapse3").show();
                    $("#gander").hide();
                } else {
                    $("#divCollapse2").hide();
                    $("#divCollapse3").hide();
                    $("#gander").show();
                    $("#gander").html("صاحب الاستشارة " + $("#ddlgender option:selected").html());
                }
            }
            $("#ConslutModal").modal();
        }
    });

}

function assignAdvisor(id) {
    consulat_id = id;
    ConsultationDetails.getAdvisor(consulat_id, function (val) {
        oldAdvisor = val;
        $("#ddlAdvisors").val(val);
        $("#assignAdvisorModal").modal();
    });
   
}
function clearConslut_id() {
    consulat_id = "";
}

function addAdvisor() {
    debugger
    advisor_id =$("#ddlAdvisors").val();
    if (advisor_id != "0") {
        debugger
        ConsultationDetails.addAdvisor(consulat_id, advisor_id, oldAdvisor, function (val) {
            if (val) {
                alert(" تم الاسناد بنجاح");
                $('#assignAdvisorModal').modal('toggle');
            } else {
                alert("لم يتم الاسناد");
            }
        });
    } else {
        alert("لم يتم اختيار المستشار");
    }
}
function save() {
    try {
        $("input").removeClass('error');
        $("select").removeClass('error');
        if ($("#TextNUMId").val().length != 10) {
            showErrorMessage("رقم الهوية  يجب أن يكون 10 ارقام");
            return;
        }
        if ($("#tel").val().length != 10) {
            showErrorMessage("رقم الهاتف  يجب أن يكون 10 ارقام");
            return;
        }
        if (Page_ClientValidate("vgroup")) {
            $("#lblstart_date").html($("#divdate2").find("#txtDatem").val());
            $("#lblstart_date_hj").html($("#divdate2").find("#txtDateh").val());
            $("#lbl_dob_m").html($("#divdate4").find("#txtDatem").val());
            $("#lbl_dob_h").html($("#divdate4").find("#txtDateh").val());
            var basicData = generateJSONFromControls("divForm");
            basicData["appear_data"] = $("#appear_data").is(":checked");
            var PosId = $("#consult_id").html();
            ConsultationDetails.Save(PosId, basicData, function (val) {
                if (val == true) {
                    showSuccessMessage("تم الحفظ بنجاح");
                    cancel();
                    drawDynamicTable();
                    getConsultNum();
                    $('#ConslutModal').modal('toggle');
                } else {
                    showErrorMessage("لم يتم الحفظ");
                }
            });
            
        }
    } catch (err) {
        alert(err);
    }
   
}



function drawDynamicTable() {
    try {
        var tableSortingColumns = [
            { orderable: false }, null, null, null, null, null, null, 
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" },{ type: "text" }, { type: "text" },{ type: "text" }, 
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 0;
        loadDynamicTable('ConsultationDetails', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch (err) {
        alert(err);
    }
}




function add() {
    try {
        
          prepareAdd();
        resetAll();
        getConsultNum();
       
        $("#divCollapse2").show();
        $("#divCollapse3").show();
        $("#gander").hide();
        $('#ConslutModal').modal({ backdrop: 'static', keyboard: false });
        $("#ConslutModal").modal();
        $("#ddlstatus").val("100");
        $("#ddlstatus").attr("disabled", "disabled");
    } catch (err) {
        alert(err);
    }
}
function CloseConslutModal() {
    $('#ConslutModal').modal('toggle');
}
function setformforupdate() {
    try {
        setformforupdate_all();
    } catch (err) {
        alert(err);
    }
}
function getConsultNum() {
    ConsultationDetails.getConsultNum(function (val) {
        debugger
        $("#txtNumber").val(Number(val) + 1);
    });
}
function getconsultation(Consult_id) {
    window.open("../report_Module/Aslah/consultationRep?Consult_id=" + Consult_id, "_blank");
}