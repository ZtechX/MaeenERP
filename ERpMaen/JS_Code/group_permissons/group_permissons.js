
var deleteWebServiceMethod = "group_permissons.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "group_permissons.asmx/Edit";
var formAutoCodeControl = "lblmainid";
var superAdmin = false;
$(function () {
    $("#pnlConfirm").show();
    $("#divData").hide();
    //$("#SavedivLoader").hide();
    
    $("#pnlOps").hide();
    try {

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
              
                group_permissons.SaveUser( UserId, permarrData, function (val) {
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

        var str = "";
       
        var ddlgroup_id = $("#ddlgroup_id").val();
        group_permissons.Edit_permissons(ddlgroup_id,function (val) {
            debugger;
            var data = JSON.parse(val[1]);
            

        if (data != "") {
            var files = JSON.parse(val[1]);
            var menu = JSON.parse(val[2]);
           
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
        }
    });
    } catch (error) {
        alert(error);
    }
}

//function drawDynamicTable() {
//    try {
//        var tableSortingColumns = [
//                { orderable: false }, null, null, null, null, null,null,null
//        ];
//        var tableFilteringColumns = [
//            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" },
//        ];

//        var tableColumnDefs = [

//        ];
//        var initialSortingColumn = 0;
//      //  loadDynamicTable('group_permissons', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
//    } catch (err) {
//        alert(err);
//    }
//}




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

                    div_show += " <div class='card'> <div class='card-header' id ='headingOne' ><h5 class='mb-0'><button type='button' class='btn btn-link collapsed' data-toggle='collapse' data-target='#collapseThree" + data[x].Id + "' aria-expanded='false' aria-controls='collapseThree" + data[x].Id + "'>" + data[x].ArMenuName +"</button></h5></div> \
                                        <div id='collapseThree" + data[x].Id + "' class='collapse' aria-labelledby='headingTwo' data-parent='#accordion'> <div class='card-body'>\
                                            <table class='table table-bordered ' id='perm_table" + data[x].Id + "'><tbody>" +
                        "<tr><td>الصفحة</td><td>الكل</td>" +
                        " <td>  <input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='2' class='ace input-lg' /> دخول  </td>" +
                        " <td>  <input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='3' class='ace input-lg' />اضافة  </td>" +
                        " <td>  <input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='4' class='ace input-lg' />    تعديل</td>" +
                        " <td>  <input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='5' class='ace input-lg' />   حذف</td>" +

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
//function get_form_for_permtion() {
//    try {
//        group_permissons.get_main_menu(function (val) {
//            var data = JSON.parse(val[1]);
//            var data2 = JSON.parse(val[2]);
//            var div_show = "<div class='accordion panel-group' id='accordion'>";
//            if (val[0] != "0") {
//                for (var x = 0; x < data.length; x++) {

//                    div_show += "<div class='panel panel-default'>\
//                                        <div class='panel-heading'> \
//                                            <h4 class='panel-title'> \
//                                                <a data-toggle='collapse' data-parent='#accordion-test' href='#collapseThree" + data[x].Id + "' class='collapsed' aria-expanded='false'><i class='fa fa-check'></i>" + data[x].ArMenuName
//                        + "</a> \
//                                            </h4> \
//                                           </div>\
//                                        <div id='collapseThree" + data[x].Id + "' class='panel-collapse collapse' aria-expanded='false' style='height: 0px;'>\
//                                            <div class='panel-body'> <table class='table table-bordered table-vertical-middle nomargin' id='perm_table" + data[x].Id + "'><tbody>" +
//                        "<tr><td></td><td></td>" +
//                        " <td>  <label class='checkbox' style='margin-bottom: 30px;padding-right: 10px;'><input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='2' class='ace input-lg' /><i style='width:25px;height:25px;'></i></label></td>" +
//                        " <td>  <label class='checkbox' style='margin-bottom: 30px;padding-right: 10px;'><input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='3' class='ace input-lg' /><i style='width:25px;height:25px;'></i></label></td>" +
//                        " <td>  <label class='checkbox' style='margin-bottom: 30px;padding-right: 10px;'><input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='4' class='ace input-lg' /><i style='width:25px;height:25px;'></i></label></td>" +
//                        " <td>  <label class='checkbox' style='margin-bottom: 30px;padding-right: 10px;'><input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='5' class='ace input-lg' /><i style='width:25px;height:25px;'></i></label></td>" +

//                        "</tr>";
//                    for (var y = 0; y < data2.length; y++) {

//                        if (data[x].Id == data2[y].MenueId) {
//                            // alert(data[x].Id + "       " + data2[y].MenueId + "                 " + data[x].ArMenuName + "           " + data2[y].ArFormTitle);
//                            div_show += " <tr id='" + data2[y].Id + "' permid=''  formid='" + data2[y].Id + "' ><ul class='list-group'>\
//                                                  <td>   <li class='list-group-item'>"+ data2[y].ArFormTitle +
//                                "</td><td>  <label class='checkbox' style='margin-bottom: 30px;'><input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='0' class='ace input-lg' /><i style='width:25px;height:25px;'></i></label></td><td> \
//                                                  <div class='[ form-group ]' style='margin-bottom: 0px;'> \
//           <input type='checkbox' id='" + data2[y].Id + "PAccess' name='fancy-checkbox-primary' autocomplete='off' onclick='checkBoxChange(this,this.checked)' /> \
//            <div class='[ btn-group ]'> \
//                <label for='" + data2[y].Id + "PAccess' class='[ btn btn-primary ]'> \
//                    <span class='[ glyphicon glyphicon-ok ]'></span> \
//                    <span> </span> \
//                </label> \
//                <label for='" + data2[y].Id + "PAccess' class='[ btn btn-default active ]'>\
//                    دخول\
//                </label>\
//            </div>\
//        </div>\
//                                                               </td>\
//                                                               <td>   <div class='[ form-group ]' style='margin-bottom: 0px;'> \
//           <input type='checkbox' id='" + data2[y].Id + "PAdd' name='fancy-checkbox-success' id='fancy-checkbox-success' autocomplete='off' onclick='checkBoxChange(this,this.checked)'/> \
//            <div class='[ btn-group ]'> \
//                <label for='" + data2[y].Id + "PAdd' class='[ btn btn-success ]'> \
//                    <span class='[ glyphicon glyphicon-ok ]'></span> \
//                    <span> </span> \
//                </label> \
//                <label for='" + data2[y].Id + "PAdd' class='[ btn btn-default active ]'>\
//                    اضافة\
//                </label>\
//            </div>\
//        </div>\</td>\
//                                                                   <td>   <div class='[ form-group ]' style='margin-bottom: 0px;'> \
//           <input type='checkbox' id='" + data2[y].Id + "PEdite' name='fancy-checkbox-info'  autocomplete='off' onclick='checkBoxChange(this,this.checked)'/> \
//            <div class='[ btn-group ]'> \
//                <label for='" + data2[y].Id + "PEdite' class='[ btn btn-info ]'> \
//                    <span class='[ glyphicon glyphicon-ok ]'></span> \
//                    <span> </span> \
//                </label> \
//                <label for='" + data2[y].Id + "PEdite' class='[ btn btn-default active ]'>\
//                    تعديل\
//                </label>\
//            </div>\
//        </div>\</td>\
//                                                                <td>  <div class='[ form-group ]' style='margin-bottom: 0px;'> \
//           <input type='checkbox' id='" + data2[y].Id + "PDelete' name='fancy-checkbox-danger' id='fancy-checkbox-danger' autocomplete='off' onclick='checkBoxChange(this,this.checked)'/> \
//            <div class='[ btn-group ]'> \
//                <label for='" + data2[y].Id + "PDelete' class='[ btn btn-danger ]'> \
//                    <span class='[ glyphicon glyphicon-ok ]'></span> \
//                    <span> </span> \
//                </label> \
//                <label for='" + data2[y].Id + "PDelete' class='[ btn btn-default active ]'>\
//                    حذف\
//                </label>\
//            </div>\
//        </div>\ </td>\
//                                                </ul> </tr>";
//                        }
//                    }
//                    div_show += "</body></table> </div>\
//                                                 </div> \
//                                     </div> \
//                              ";

//                }
//                div_show += "</div>";
//                document.getElementById('tablePrint').innerHTML = div_show;

//            } else {
//                showErrorMessage("No data found !!");
//            }
//        });
//    } catch (err) {
//        alert(err);
//    }
//}