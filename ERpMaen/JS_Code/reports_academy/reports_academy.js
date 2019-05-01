
var deleteWebServiceMethod = "advisor.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "advisor.asmx/Edit";
var formAutoCodeControl = "lblmainid";
var superAdmin = false;
$(function () {
      $("input").attr("class","");
    $("select").attr("class", "");
    $("input").addClass("form-control");
    $("select").addClass("form-control");
    //$("#pnlConfirm").hide();
    //$("#divData").hide();
    //$("#SavedivLoader").show();

    try {
        //advisor.checkUser(function (val) {
            
        //    if (val == "Superadmin") {
        //        form_load();
        //        //getAdvisorCode();
        //        superAdmin = true;
        //    } else if (val != "0") {
        //        $("#cmdAdd").closest("li").remove();
        //        $("#cmdDelete").closest("li").remove();
        //        superAdmin = false;
        //        advisor.Edit(val, function (val1) {
        //            edit(val1);
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
function isValidEmailAddress(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    return pattern.test(emailAddress);
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
        
        if ($("#TextEmail").val() != "") {
            if (!isValidEmailAddress($("#TextEmail").val())) {
                showErrorMessage("من فضلك ادخل البريد الاليكترونى صحيح");
                return;
            }
        }
            //////end email
        if (!checkRequired()) {
            $("#pnlConfirm").hide();
            $("#SavedivLoader").show();
            var basicData = generateJSONFromControls("divForm");
            var PosId = $("#lblmainid").html();
            advisor.Save(PosId, basicData, function (val) {
               
                if (val == "True") {
                    showSuccessMessage("تم الحفظ بنجاح");
                    if (superAdmin) {
                        drawDynamicTable();
                        cancel();
                       // getAdvisorCode();
                    } else {
                        advisor.Edit(PosId, function (val1) {
                            edit(val1);
                        });
                    }
                } else {
                    showErrorMessage(val.split("|")[1]);

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
    cancel();
 
        if (val != "") {
            var data = JSON.parse(val);
            fillControlsFromJson(data[0],"divForm");
            $("#lblmainid").html(data[0].id);
        }
        $("#pnlConfirm").hide();
        $("#divData").show();
    $("#SavedivLoader").hide();
    $("#cmdUpdate").removeAttr("disabled");
}

function drawDynamicTable() {
    try {
        var tableSortingColumns = [
            { orderable: false }, null, null, null, null
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }
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
       // getAdvisorCode();
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
function get_deplomes_subjects() {
    var ddldiplomes = $("#ddldiplomes").val();
    var ddlsemster = $("#ddlsemster").val();

    reports_academy.get_deplomes_subjects(ddldiplomes, ddlsemster,function (val) {
        if (val[0] != 0) {
            var data = JSON.parse(val[0]);
            var result = "";
            for (var x = 0; x < data.length; x++) {
                result = result + "<option value=" + data[x].id + '|' + data[x].subject_id+">" + data[x].subject+"</option>";

            }
            $("#ddlsubject").html(result);
        }


    });
}

function get_deplome_students(flag) {
    if (flag == 1) {
        var ddldiplomes = $("#ddldiplomes1").val();
    } else {
        var ddldiplomes = $("#ddldiplomes3").val();
    }
    reports_academy.get_deplome_students(ddldiplomes,function (val) {
        if (val[0] != "0") {
            var data = JSON.parse(val[0]);
            var result = "";
            for (var x = 0; x < data.length; x++) {
                result = result + "<option value=" + data[x].user_id + ">" + data[x].full_name + "</option>";

            }
            if (flag == 1) {
                $("#ddlstudents").html(result);
            } else {
                $("#ddlstudents1").html(result);
            }
        } else {
            $("#ddlstudents").html("");
            $("#ddlstudents1").html("");
        }


    });
}
function get_report_degree() {

    if ($("#ddldiplomes").val() == "0") {
        alert("من فضلك اختار الدبلومة");
        return;
    }
    if ($("#ddlsemster").val() == "0") {
        alert("من فضلك اختار الترم");
        return;
    }
    var ddlsemster = $("#ddlsemster").val();
    var diplome_subject = $("#ddlsubject").val().split("|")[0];
    var subject = $("#ddlsubject").val().split("|")[1];
    window.open("Academy/subj_degree_rep.aspx?subject_id=" + subject + "&diplome_subject=" + diplome_subject + "&semster_id=" + ddlsemster, '_blank');

}

function get_report_student() {

    if ($("#ddldiplomes1").val() == "0") {
        alert("من فضلك اختار الدبلومة");
        return;
    }
    var diplome_subject = $("#ddldiplomes1").val();
    var students = $("#ddlstudents").val();
    window.open("Academy/academy_session_rep.aspx?diplome_id=" + diplome_subject + "&diplome_user=" + students, '_blank');

}

function get_report_diplome_money() {
    if ($("#ddldiplomes2").val() == "0") {
        alert("من فضلك اختار الدبلومة");
        return;
    }
    var diplome = $("#ddldiplomes2").val();
    var students = $("#ddlstudents").val();
    window.open("Academy/diplome_money_rep.aspx?diplome_id=" + diplome, '_blank' );

}

function get_report_details_student_money() {

    if ($("#ddldiplomes3").val() == "0") {
        alert("من فضلك اختار الدبلومة");
        return;
    }
    var diplome_subject = $("#ddldiplomes3").val();
    var students = $("#ddlstudents1").val();
    window.open("Academy/student_money_rep.aspx?diplome_id=" + diplome_subject + "&diplome_user=" + students, '_blank');

}


//function getAdvisorCode() {
//    advisor.getAdvisorCode(function (val) {
//        debugger
//        $("#code").val(Number(val) + 1);
//    });
//}