﻿
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
                    if (conslut[0].created_By == $("#loginUser").val()) {
                        $("#newMess").show();
                    }
                    $("#Ctitle").html("هذة الاستشارة مقدمة من " + conslut[0].From + " <br/>إلى المستشار " + conslut[0].To);
                    $("#consult_nm").html(conslut[0].consult_nm);
                    $("#code").html(conslut[0].code);
                    $("#lblstart_date").html("الميلادى : " + conslut[0].start_date);
                    $("#lblstart_date_hj").html("الهجرى : " + conslut[0].start_date_hj);
                    $("#source_id").html(conslut[0].source_id);
                    $("#category_id").html(conslut[0].category_id);
                    $("#income_notes").val(conslut[0].income_notes);
                    $("#status").html(conslut[0].status);
                    drawOldMess();
                }
            });
        });
        //setInterval(drawOldMess, 10000);
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
            debugger
            conslut_mess.forEach(function (mess) {
                debugger
                var date_time = "<pre style='margin-bottom: 0px;'>"+"التاريخ : "+ mess.message_date;
                var time =mess.messageTime;
                date_time += "     التوقيت : " + (time == null ? "" : time) +"</pre>";
                str += `<div class="col-md-9">
<label style="float:right;margin-right: 5px;"><pre style='margin-bottom: 0px;'>من : ${mess.name}</pre></label ><label style="float:left;">${date_time}</label>
<textarea readonly="readonly"  rows="5" cols="20"  class="textbox icon-common" style="margin-right: 0px;margin-top: 0px; margin-bottom: 10px;">${mess.message}</textarea>
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