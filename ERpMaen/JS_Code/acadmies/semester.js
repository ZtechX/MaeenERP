
var deleteWebServiceMethod = "semesterCls.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "semesterCls.asmx/Edit";
var formAutoCodeControl = "lblmainid";

$(function () {
    $("#pnlConfirm").hide();


    try {
        form_load();
        //drawDynamicTable();
    
    } catch (err) {
       // alert(err);
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
        debugger
        
        $("input").removeClass('error');
        $("select").removeClass('error');

        setRequired_Date("divdate1");
        setRequired_Date("divdate2");

        $("#end_datem").val($("#divdate2 #txtDatem").val());
        $("#end_datehj").val($("#divdate2 #txtDateh").val());

        $("#start_date_m").val($("#divdate1 #txtDatem").val());
        $("#start_date_hj").val($("#divdate1 #txtDateh").val());

        var enddate_m = document.getElementById("end_datem").value;

        var startdate_m = document.getElementById("start_date_m").value;

        if (enddate_m > startdate_m) {

            if (Page_ClientValidate("vgroup")) {
                $("#pnlConfirm").hide();
                $("#SavedivLoader").show();
                var basicData = generateJSONFromControls("divForm");


                var PosId = $("#lblmainid").html();



                // console.log(enddate_m + "     " + startdate_m);




                semesterCls.SaveSemester(PosId, basicData, function (val) {



                    $("#SavedivLoader").hide();
                    if (val.split("|")[0] == "True") {
                        drawDynamicTable();
                        cancel();
                        showSuccessMessage("تم الحفظ بنجاح");


                    } else {
                        showErrorMessage(val.split("|")[1]);
                        $("#pnlConfirm").show();
                        $("#SavedivLoader").hide();
                    }
                });


            }
          
        }
        else {
            alert("تاريخ البداية اكبر من تاريخ النهاية")
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

            $("#divdate1 #txtDatem").val(data[0].start_date_m);
            $("#divdate1 #txtDateh").val(data[0].start_date_hj);
            $("#divdate2 #txtDatem").val(data[0].end_date_m);
            $("#divdate2 #txtDateh").val(data[0].end_date_hj);
           
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
           
        }
        $("#pnlConfirm").hide();
        $("#divData").show();
        $("#SavedivLoader").hide();
}

function drawDynamicTable() {
    try {
        debugger
        var tableSortingColumns = [
            { orderable: false }, null, null, null, null,
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" },
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 0;
        loadDynamicTable('Semester', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch (err) {
        //alert(err);
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



