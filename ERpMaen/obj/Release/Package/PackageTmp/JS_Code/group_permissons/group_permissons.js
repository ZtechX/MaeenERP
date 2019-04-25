
var deleteWebServiceMethod = "group_permissons.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "group_permissons.asmx/Edit";
var formAutoCodeControl = "lblmainid";
var superAdmin = false;
$(function () {
    $("#pnlConfirm").show();
    $("#divData").hide();
    $("#ddlgroup_id").addClass("form-control");
    
    $("#pnlOps").hide();
    try {
        get_form_operations();
        //alert = function () { };
        group_permissons.superAdmin(function (val) {
            //alert(val);
            superAdmin = val;
            //form_load();
            get_form_for_permtion();
          
             $("#pnlConfirm").show();
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

        var UserId = $("#ddlgroup_id").val();
        if ($("#ddlgroup_id").val() > 0) {

            $("#pnlConfirm").hide();
            $("#SavedivLoader").show();
            var arrData = new Array;
           var saveType = $("#cmdSave").attr("CommandArgument");
            var permarrData = [];
            var oparrData = [];
            group_permissons.menulength("1", function (val) {
                menu = JSON.parse(val[1]);
                for (i = 0; i < menu.length; i++) {
                    $("#perm_table" + menu[i].Id + " tbody tr").each(function () {
                        if ($(this).attr("formid") != undefined) {
                            var data = { "permid": $(this).attr("permid"), "UserId": "", "form_id": $(this).attr("formid"), "f_access": $(this).find("#" + $(this).prop("id") + "PAccess").prop("checked"), "f_add": $(this).find('#' + $(this).prop("id") + 'PAdd').prop("checked"), "f_update": $(this).find('#' + $(this).prop("id") + 'PEdite').prop("checked"), "f_delete": $(this).find('#' + $(this).prop("id") + 'PDelete').prop("checked") };
                            permarrData.push(data);
                        }
                    });
                }

              
            $("#operations_tbl").find("tr td input:checkbox").each(function () {
                if ($(this).is(":checked")) {
                    var value = $(this).attr("op_id");
                    oparrData.push(value);
                }
                });
                console.log(oparrData);
                group_permissons.SaveUser(UserId, permarrData, oparrData, function (val) {
               $("#SavedivLoader").hide();
                    if (val.split("|")[0] == "True") {
                       // cancel();
                        resetAll();
                        $("#pnlConfirm").show();
                       // drawDynamicTable();
                        showSuccessMessage("تم الحفظ");
                    } else {
                        showErrorMessage(val.split("|")[1]);
                    }
                });


            });


        }

    } catch (err) {
        alert(err);
    }
}


function fillPerm(){
    try {
        $("#input[type='checkbox'").prop("checked", false);
        var str = "";
       
        var ddlgroup_id = $("#ddlgroup_id").val();
        group_permissons.Edit_permissons(ddlgroup_id,function (val) {
           
            var data = JSON.parse(val[1]);
            console.log(val);

        if (data != "") {
            var files = JSON.parse(val[1]);
            var menu = JSON.parse(val[2]);
            var operations = JSON.parse(val[3]);
           
            if (files.length > 0) {
                for (i = 0; i < files.length; i++) {
                 //   alert(files[i].ID + "          " + files[i].formid + "          " + files[i].paccess);    
                    var formid = files[i].formid;
                    for (y = 0; y < menu.length; y++) {
                        $("#perm_table" + menu[y].Id).find("#" + formid).attr("permid", files[i].ID);
                        $("#perm_table" + menu[y].Id).find("#" + formid).attr("formid", files[i].formid);
                        var Access = $("#perm_table" + menu[y].Id).find("#" + formid).find("#" + formid + "PAccess");
                        var Add = $("#perm_table" + menu[y].Id).find("#" + formid).find("#" + formid + "PAdd");
                        var Edit = $("#perm_table" + menu[y].Id).find("#" + formid).find("#" + formid + "PEdite");
                        var Delete = $("#perm_table" + menu[y].Id).find("#" + formid).find("#" + formid + "PDelete");
                        Access.prop("checked", files[i].paccess);
                        //checkBoxChange(Access, files[i].paccess);
                        Add.prop("checked", files[i].padd);
                       // checkBoxChange(Add, files[i].padd);
                        Edit.prop("checked", files[i].PEdite);
                       // checkBoxChange(Edit, files[i].PEdite);
                        Delete.prop("checked", files[i].pdelete);
                       // checkBoxChange(Delete, files[i].pdelete);
                        

                    }
                }
            }
            $("#operations_tbl tr td input").prop("checked", 0);
            if (operations.length > 0) {
                
                for (i = 0; i < operations.length; i++) {
                    $("#operations_tbl tr td input[op_id=" + operations[i].form_operation_id + "]").prop("checked", 1);

                }
            }
        }
    });
    } catch (error) {
        alert(error);
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


function get_form_for_permtion() {
    try {
        group_permissons.get_main_menu(function (val) {
            var data = JSON.parse(val[1]);
            var data2 = JSON.parse(val[2]);
            var div_show = "<div id='accordion' syle='width:100%;'>";
            if (val[0] != "0") {
                for (var x = 0; x < data.length; x++) {

                    div_show += " <div class='card'> <div class='card-header' id ='headingOne' ><h5 class='mb-0'><a class='btn btn-link collapsed' data-toggle='collapse' data-target='#collapseThree" + data[x].Id + "' aria-expanded='false' aria-controls='collapseThree" + data[x].Id + "'><i class='fa fa-caret-down'></i> " + data[x].ArMenuName +"</a></h5></div> \
                                        <div id='collapseThree" + data[x].Id + "' class='collapse' aria-labelledby='headingTwo' data-parent='#accordion'> <div class='card-body'>\
                                            <table class='table table-bordered ' id='perm_table" + data[x].Id + "'><tbody>" +
                        "<tr><td>الصفحة</td><td>الكل</td>" +
                        " <td>  <input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='2' class='ace input-lg' style='margin-left: 12px' /> دخول  </td>" +
                        " <td>  <input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='3' class='ace input-lg' style='margin-left: 12px'/>اضافة  </td>" +
                        " <td>  <input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='4' class='ace input-lg' style='margin-left: 12px'/>    تعديل</td>" +
                        " <td>  <input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='5' class='ace input-lg' style='margin-left: 12px'/>   حذف</td>" +

                        "</tr>";
                    for (var y = 0; y < data2.length; y++) {

                        if (data[x].Id == data2[y].MenueId) {
                            // alert(data[x].Id + "       " + data2[y].MenueId + "                 " + data[x].ArMenuName + "           " + data2[y].ArFormTitle);
                            div_show += " <tr id='" + data2[y].Id + "' permid=''  formid='" + data2[y].Id + "' >\
                                                  <td>   "+ data2[y].ArFormTitle +
                                "</td><td> <input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='0' class='ace input-lg' /></label></td><td> \
                                                  <div class='[ form-group ]' style='margin-bottom: 0px;'> \
           <input type='checkbox' id='" + data2[y].Id + "PAccess' name='fancy-checkbox-primary' autocomplete='off' onclick='checkBoxChange(this,this.checked)' /> \
        </div>\
                                                               </td>\
                                                               <td>   <div class='[ form-group ]' style='margin-bottom: 0px;'> \
           <input type='checkbox' id='" + data2[y].Id + "PAdd' name='fancy-checkbox-success' id='fancy-checkbox-success' autocomplete='off' onclick='checkBoxChange(this,this.checked)'/> \
        </div>\</td>\
                                                                   <td>   <div class='[ form-group ]' style='margin-bottom: 0px;'> \
           <input type='checkbox' id='" + data2[y].Id + "PEdite' name='fancy-checkbox-info'  autocomplete='off' onclick='checkBoxChange(this,this.checked)'/> \
        </div>\</td>\
                                                                <td>  <div class='[ form-group ]' style='margin-bottom: 0px;'> \
           <input type='checkbox' id='" + data2[y].Id + "PDelete' name='fancy-checkbox-danger' id='fancy-checkbox-danger' autocomplete='off' onclick='checkBoxChange(this,this.checked)'/> \
        </div>\ </td>\
                                                </ul> </tr>";
                        }
                    }
                    div_show += "</tbody></table> </div></div></div>\    ";

                }
                div_show += "</div>";
                document.getElementById('tablePrint').innerHTML = div_show;

            } else {
                showErrorMessage("No data found !!");
            }
        });
    } catch (err) {
        alert(err);
    }
}

function select_unselectAll(obj) {

    debugger;
    var tdNum = Number($(obj).attr("value"));

    var ckeckedVal = obj.checked
    if (tdNum != 0) {
        var tabelId = $(obj).closest('table').attr('id');
        var trArr = $("#" + tabelId + " tr");
        for (var i = 1; i < trArr.length; i++) {

            tds = trArr[i].getElementsByTagName("td");
            var input = $(tds[tdNum]).find("input");
            input.prop("checked", ckeckedVal);
            //checkBoxChange(input, ckeckedVal);
        }
    } else {
        tds = $(obj).closest('tr').find("td");
        for (var i = 2; i < tds.length; i++) {
            var input = $(tds[i]).find("input");
            input.prop("checked", ckeckedVal);
           // checkBoxChange(input, ckeckedVal);
        }
    }
}
function check_all_group(obj) {

    debugger;
    var tdNum = Number($(obj).attr("value"));

    var ckeckedVal = obj.checked
  
         $("#operations_tbl tr td input[value=" + tdNum + "]").prop("checked", ckeckedVal);
     
  
}

function addNewGroup() {
    $('#groupModal').modal({ backdrop: 'static', keyboard: false });
    
    $("#groupModal").dialog({
        title: "إضافة محجموعة" 
    });
}
function savegroup() {
    var group_nm = $("#txtGroup_nm").val();
    if (group_nm != "") {
        $("#SavedivLoader").show();
        group_permissons.savegroup(group_nm, function (val) {
            if (val == "True") {
                showSuccessMessage("تم الحفظ بنجاح");
                location.reload();
            } else {
                showErrorMessage(val.split("|")[1]);
                $("#SavedivLoader").hide();
            }
           
        });
    } else {
        showErrorMessage("إدخل أسم المجموعة");
    }

}

function get_form_operations() {
    group_permissons.get_form_operations(function (val) {
        var data = JSON.parse(val[1]);
      
        var div_show = "";
        if (val[0] != "0") {
            var parent = 0;
            data.forEach(function (element) {
             
                if (element.id == element.parent) {
                    parent = element.id;
                    div_show = div_show + `
 <tr>
               
                <td colspan="2"> ${element.action_name} </td>
               <td>  <input name="form-field-checkbox" onclick="check_all_group(this)" op_id="${element.id}" value="${element.parent}" type="checkbox"  class="ace input-lg" >  </td> 
               </tr>

`;
                }
                if (element.id != element.parent & parent==element.parent) {
                    div_show = div_show + `
    <tr>
                <td></td>
                <td>  ${element.action_name}</td>
          <td>  <input name="form-field-checkbox" type="checkbox" op_id="${element.id}" value="${element.parent}"   class="ace input-lg" >  </td> 
        </tr>

`;
                }
            })
        }
        $("#operations_tbl").html(div_show);
    });
}
