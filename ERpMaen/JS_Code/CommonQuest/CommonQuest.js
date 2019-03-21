var formAutoCodeControl = "lblmainid";
$(function () {
    try {
        form_load();
        resetAll();
    } catch (err) {
        alert(err);
    }
});
function resetAll() {
    try {
        $("#divRes").hide();
       
        $("#lblmainid").html("");
        $("#tbl_questions tbody").html("");
        addRow();
    } catch (err) {
        alert(err);
    }
}
function drawDynamicTable() {
    try {
        var tableSortingColumns = [
            { orderable: false }, null, null, null,null
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" },
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 0;
        loadDynamicTable('CommonQuest', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch (err) {
        alert(err);
    }
}

function save() {
    $(".error").removeClass("error");
    $("#SavedivLoader").show();
    $("#tbl_questions tbody tr").removeClass("error");
    var arr = [];
    var error = 0;
    var tr = $("#tbl_questions tbody tr").length;
    for (var i = 0; i<tr; i++) {
        var ques = $("#question_" + i).val();
        var ans = $("#answer_" + i).val();
        var order = $("#order_" + i).val();
        if (ques == "" ) {
            showErrorMessage("إدخل السوال");
            error = 1;
            $("#question_" + i).addClass("error");
            break;
        }
        if (ans == "" ) {
            showErrorMessage("إدخل الجواب");
            error = 1;
            $("#answer_" + i).addClass("error");
            break;
        }
        if (order == "") {
            order=1;
        }
        arr.push({ "question": ques, "answer": ans, "q_order": order});
      
    }
    
    if (!error) {
        CommonQuest.Save(arr,function (val) {
            if (val) {
                showSuccessMessage("تم الحفظ بنجاح");
                resetAll();
                drawDynamicTable()
            } else {
                showErrorMessage("لم يتم الحفظ");
            }
            $("#SavedivLoader").hide();
        });
    } else {
        $("#SavedivLoader").hide();
    }
}

function addRow() {
    var row_index = ($("#tbl_questions tbody tr").length);
    var str = ` <tr id="tr_${row_index}">
<td><textarea  rows="2" cols="20" class="textbox icon-common"  id="question_${row_index}" runat="server" ></textarea></td>
<td><textarea  rows="2" cols="20"  class="textbox icon-common" id="answer_${row_index}" runat="server" ></textarea></td>
<td><input style="border:1px solid #C09C67;margin-top:5px;" onkeypress="return isNumber(event);" type="text" id="order_${row_index}" class="form-control" runat="server" clientidmode="Static" /></td>
<td><div class="hidden-sm hidden-xs action-buttons"><button style="margin-left:20%;" class="btn btn-xs btn-danger btn-quick"  onclick ="deleteRow(${row_index}); return false;" > <i class="fa fa-times"></i></button ></div></td>
</tr>`;
    $("#tbl_questions tbody").append(str);
    addstyle(); 
}

function deleteRow(id) {
    $("#tr_" + id).remove();
    addstyle();
}

function addstyle() {
  var  index = 0;
    $("#tbl_questions tbody tr").each(function () {
     
        if (index % 2 != 0) {
            $(this).css('background-color', '#ddd');
        } else {
            $(this).css('background-color', '#fff');
        }
        index++;
    });
} 
function deleteQuestion(id) {
    try {
        var r = confirm("هل تريد الحذف");
        if (r == true) {
            CommonQuest.Delete(id, function (val) {
                if (val) {
                    showSuccessMessage("تم الحذف بنجاح");
                    drawDynamicTable();
                } else {
                    showErrorMessage("لم يتم الحذف");
                }
            });
        }
    }
    catch (err) {
        alert(err);
    }
}
function editQuestion(id) {
    if (val != "") {
        var arr = JSON.parse(val);
        var str = `<tr id="tr_0">
<td><textarea rows="2" cols="20" class="textbox icon-common"  id="question_0" runat="server" >${arr.question}</textarea></td>
<td><textarea rows="2" cols="20" class="textbox icon-common" id="answer_0" runat="server" >${arr.answer}</textarea></td>
<td><input value="${arr.q_order}" style="border:1px solid #C09C67;margin-top:5px;" onkeypress="return isNumber(event);" type="text" id="order_0" class="form-control" runat="server" clientidmode="Static" /></td>
<td><div class="hidden-sm hidden-xs action-buttons"><button style="margin-left:20%;" class="btn btn-xs btn-danger btn-quick"  onclick ="deleteRow(0); return false;" > <i class="fa fa-times"></i></button ></div></td>
</tr>`;
        $("#tbl_questions tbody").html(str);
    }
}