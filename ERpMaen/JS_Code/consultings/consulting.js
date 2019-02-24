
$(function () {
   
    try {

        $("#pnlConfirm").hide();
        $("#divData").hide();
        $("#SavedivLoader").show();
        consulting.isSuperAdmin(function (val) {
            if (val) {
                $("#newMess").hide();
            } else {
                $("#newMess").show();
            }
            consulting.getConslute($("#consulting_id").html(), function (val) {
                
                if (val[0] == "1") {
                    var conslut = JSON.parse(val[1]);
                    $("#Ctitle").html("هذة الاستشارة مقدمة من " + conslut[0].From + " <br/>إلى المستشار " + conslut[0].To);
                    $("#consult_nm").html(conslut[0].consult_nm);
                    $("#code").html(conslut[0].code);
                    $("#lblstart_date").html("الميلادى : " + conslut[0].start_dt);
                    $("#lblstart_date_hj").html("الهجرى : " + conslut[0].start_dt_hj);
                    $("#source_id").html(conslut[0].source_id);
                    $("#category_id").html(conslut[0].category_id);
                    $("#income_notes").val(conslut[0].income_notes);
                    $("#status").html(conslut[0].status);
                    drawOldMess();
                }
            });
        });
       
    } catch (err) {
        alert(err);
    }
});

function drawOldMess() {
    consulting.getConsluteMess($("#consulting_id").html(), function (val) {
        debugger
        if (val != "") {
            var conslut_mess = JSON.parse(val);
            var str = "";
            conslut_mess.forEach(function (mess) {
                str += `<div class="col-md-12">
<label style="float:right;">من : ${mess.full_name}</label ><label style="float:left;">التاريخ : ${mess.message_date}</label>
<textarea  rows="5" cols="20"  class="textbox icon-common" style="margin-right: 0px;">${mess.message}</textarea>
</div><br/>
`
            });
            $("#oldMassages").html(str);
        }
    });
}
function resetAll() {
    try {
        resetFormControls();
        $("#lblmainid").html("");
        getConsultNum();
    } catch (err) {
        alert(err);
    }
}

function save() {
    consulting.Save($("#consulting_id").html(), $("#txtmessage").val(), function (val) {
        if (val) {
            showSuccessMessage("تم الارسال بنجاح");
            $("#txtmessage").val("");
            drawOldMess();
        } else {
            showErrorMessage("لم يتم الارسال");
        }
    });
    
}

function edit(val) {
   
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