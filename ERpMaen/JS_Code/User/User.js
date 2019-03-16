/************************************/
// Created By : Ahmed Nayl
// Create Date : 16/11/2015 11:30 AM
// Description : This file contains all javaScript functions related to POS form 
/************************************/
var superAdmin = false;

// load function set defualt values
$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").show();

   // alert("load");
    try {
      //  window.alert = function () { };
        // getTree();
        
        alert = function () { };
        User.superAdmin(function (val) {
            alert(val);
            superAdmin = val;
            form_load();
           get_form_for_permtion();
          // $("#pnlConfirm").show();
        });
        
    } catch (err) {
        alert(err);
    }
});

// draw dynamic table for existing POS
function drawDynamicTable() {
    try {
        var tableSortingColumns = [
                { orderable: false }, null, null, null, null, 
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" },
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 0;
        if (superAdmin) {
            $("#ddlcomp_id").prop("required",false);
            loadDynamicTable('Users', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
        } else {
            CustloadDynamicTable('Users', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
        }
    } catch (err) {
        alert(err);
    }
}

function CustloadDynamicTable(frmName, autoCode, tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, frmorReport) {
    try {
        
        $("#divLoader").show();
        formName = frmName;
        formAutoCode = autoCode;

        columns = tableSortingColumns;
        initialColumn = initialSortingColumn;
        filterColumns = tableFilteringColumns;
        vColumnDefs = tableColumnDefs;
        formOrReport = frmorReport;
        WebService.GetFormQuaryString(formName + "|" + Archived, function (quaryStr) {
            var quary = quaryStr + " and isNull(superAdmin,0) =0 and tblUsers.comp_id = " + $("#ddlcomp_id").val();
          
            WebService.GetFormKeys(formName, function (formKeys) {
                if (formKeys != "") {
                    keys = JSON.parse(formKeys);
                }
                generateDynamicTable(quary);
            });
        });
    } catch (err) {
        alert(err);
    }
}
// empty pnlform and show it and hide function panel
function add() {
    try {
        prepareAdd();
        resetAll();
       
    } catch (err) {
        alert(err);
    }
}

// reset all controls when add or cancel
function resetAll() {
    try {
        resetFormControls();
        $("#lblmainid").html("");
        $('#ulbasictree').find('input[type=checkbox]:checked').removeAttr('checked');
        $('#perm_table').find('input[type=checkbox]:checked').removeAttr('checked');
        $("#places").find('input[type=checkbox]:checked').removeAttr('checked');
        $("#imgItemURL").attr("src", "../App_Themes/images/add-icon.jpg");
        $("#tablePrint").find('input:checkbox').attr("checked", false);
        $(".btn-group").find("span:first-child").css("display", "none");
        $(".btn-group").find("span:last-child").css("display", "inline-block");
        $("#pnlConfirm").show();
        $("#ddlcomp_id").val($("#comp").html());
        $("#Researcher").attr("checked", false);
        changeResearcher();
    } catch (err) {
        alert(err);
    }
}
function checkBoxChange(obj, ckeckedVal) {
    if (ckeckedVal) {
        $(obj).closest('div').find("span:first-child").css("display", "inline-block");
        $(obj).closest('div').find("span:last-child").css("display", "none");
    } else {
        $(obj).closest('div').find("span:first-child").css("display", "none");
        $(obj).closest('div').find("span:last-child").css("display", "inline-block");
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
            checkBoxChange(input, ckeckedVal);
        }
    } else {
        tds = $(obj).closest('tr').find("td");
        for (var i = 2; i < tds.length; i++) {
            var input = $(tds[i]).find("input");
            input.prop("checked", ckeckedVal);
            checkBoxChange(input, ckeckedVal);
        }
    }
}

// get forms for permtion
function get_form_for_permtion() {
    try {
        User.get_main_menu( function (val) {
            var data = JSON.parse(val[1]);
            var data2 = JSON.parse(val[2]);
            var div_show = "";
             div_show = "<div class='accordion panel-group' id='accordion'>";
            if (val[0] != "0") {
                for (var x = 0; x < data.length; x++) {
                  
                    div_show += "<div class='panel panel-default'>\
                                        <div class='panel-heading'> \
                                            <h4 class='panel-title'> \
                                                <a data-toggle='collapse' data-parent='#accordion-test' href='#collapseThree" + data[x].Id + "' class='collapsed' aria-expanded='false'><i class='fa fa-check'></i>" + data[x].ArMenuName
                                                + "</a> \
                                            </h4> \
                                           </div>\
                                        <div id='collapseThree" + data[x].Id + "' class='panel-collapse collapse' aria-expanded='false' style='height: 0px;'>\
                                            <div class='panel-body'> <table class='table table-bordered table-vertical-middle nomargin' id='perm_table" + data[x].Id + "'><tbody>"+
                                               "<tr><td></td><td></td>" +
                                        " <td>  <label class='checkbox' style='margin-bottom: 30px;padding-right: 10px;'><input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='2' class='ace input-lg' /><i style='width:25px;height:25px;'></i></label></td>" +
                                         " <td>  <label class='checkbox' style='margin-bottom: 30px;padding-right: 10px;'><input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='3' class='ace input-lg' /><i style='width:25px;height:25px;'></i></label></td>" +
 " <td>  <label class='checkbox' style='margin-bottom: 30px;padding-right: 10px;'><input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='4' class='ace input-lg' /><i style='width:25px;height:25px;'></i></label></td>" +
 " <td>  <label class='checkbox' style='margin-bottom: 30px;padding-right: 10px;'><input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='5' class='ace input-lg' /><i style='width:25px;height:25px;'></i></label></td>" +

                                       "</tr>";
                    for (var y = 0; y < data2.length; y++) {
                        
                        if (data[x].Id == data2[y].MenueId) {
                           // alert(data[x].Id + "       " + data2[y].MenueId + "                 " + data[x].ArMenuName + "           " + data2[y].ArFormTitle);
                            div_show += " <tr id='" + data2[y].Id + "' permid=''  formid='" + data2[y].Id + "' ><ul class='list-group'>\
                                                  <td>   <li class='list-group-item'>"+ data2[y].ArFormTitle +
                                                  "</td><td>  <label class='checkbox' style='margin-bottom: 30px;'><input name='form-field-checkbox' type='checkbox'  onclick='select_unselectAll(this)' value='0' class='ace input-lg' /><i style='width:25px;height:25px;'></i></label></td><td> \
                                                  <div class='[ form-group ]' style='margin-bottom: 0px;'> \
           <input type='checkbox' id='" + data2[y].Id + "PAccess' name='fancy-checkbox-primary' autocomplete='off' onclick='checkBoxChange(this,this.checked)' /> \
            <div class='[ btn-group ]'> \
                <label for='" + data2[y].Id + "PAccess' class='[ btn btn-primary ]'> \
                    <span class='[ glyphicon glyphicon-ok ]'></span> \
                    <span> </span> \
                </label> \
                <label for='" + data2[y].Id + "PAccess' class='[ btn btn-default active ]'>\
                    دخول\
                </label>\
            </div>\
        </div>\
                                                               </td>\
                                                               <td>   <div class='[ form-group ]' style='margin-bottom: 0px;'> \
           <input type='checkbox' id='" + data2[y].Id + "PAdd' name='fancy-checkbox-success' id='fancy-checkbox-success' autocomplete='off' onclick='checkBoxChange(this,this.checked)'/> \
            <div class='[ btn-group ]'> \
                <label for='" + data2[y].Id + "PAdd' class='[ btn btn-success ]'> \
                    <span class='[ glyphicon glyphicon-ok ]'></span> \
                    <span> </span> \
                </label> \
                <label for='" + data2[y].Id + "PAdd' class='[ btn btn-default active ]'>\
                    اضافة\
                </label>\
            </div>\
        </div>\</td>\
                                                                   <td>   <div class='[ form-group ]' style='margin-bottom: 0px;'> \
           <input type='checkbox' id='" + data2[y].Id + "PEdite' name='fancy-checkbox-info'  autocomplete='off' onclick='checkBoxChange(this,this.checked)'/> \
            <div class='[ btn-group ]'> \
                <label for='" + data2[y].Id + "PEdite' class='[ btn btn-info ]'> \
                    <span class='[ glyphicon glyphicon-ok ]'></span> \
                    <span> </span> \
                </label> \
                <label for='" + data2[y].Id + "PEdite' class='[ btn btn-default active ]'>\
                    تعديل\
                </label>\
            </div>\
        </div>\</td>\
                                                                <td>  <div class='[ form-group ]' style='margin-bottom: 0px;'> \
           <input type='checkbox' id='" + data2[y].Id + "PDelete' name='fancy-checkbox-danger' id='fancy-checkbox-danger' autocomplete='off' onclick='checkBoxChange(this,this.checked)'/> \
            <div class='[ btn-group ]'> \
                <label for='" + data2[y].Id + "PDelete' class='[ btn btn-danger ]'> \
                    <span class='[ glyphicon glyphicon-ok ]'></span> \
                    <span> </span> \
                </label> \
                <label for='" + data2[y].Id + "PDelete' class='[ btn btn-default active ]'>\
                    حذف\
                </label>\
            </div>\
        </div>\ </td>\
                                                </ul> </tr>";
                        }
                    }
                    div_show += "</body></table> </div></div> </div>";

                }
                div_show = div_show + "</div>";
                $('#tablePrint').html( div_show);

            } else {
                showErrorMessage("No data found !!");
            }
        });
    } catch (err) {
        alert(err);
    }
}

// check user type 
function checkUserType() {
    try {
        var usertype = $("#ddlUserType").val();
        if (usertype != 1) {
            $("#ddlDepartment").attr("disabled", "disabled");
        } else {
            $("#ddlDepartment").removeAttr('disabled');
        }
    } catch (err) {
    }
}


function print_grid() {
    //var id = $("#Textreat_id").val();
    //alert("fffffffffffffffS");
    window.open('User_grid.aspx', '_blank');

}

function changeResearcher(callback) {
    
    var Researcher=  $("#Researcher").is(':checked') ;
    if (Researcher)
    {
        $("#LiPlaces").css("display", "");
        drawPleaces(callback);
    } else {
        $("#LiPlaces").css("display", "none");
    }
 
}

function drawPleaces(callback) {
    User.GetPlaces(function (val) {
        
        if (val[0] != "") {
            var data = JSON.parse(val[0]);
            var str = "";
            for (var i = 0; i < data.length ; i++) {
                str += "<tr><td> <input onchange='changearea(" + data[i].id + ");' type='checkbox' id='" + data[i].id + "'/> </td>";
                str += "<td>" + data[i].description + "</td></tr>";
            }
            $("#CTY tbody").html(str);
        }
        if (val[1] != "") {
            var data = JSON.parse(val[1]);
            var str = "";
            for (var i = 0; i < data.length ; i++) {
                str += "<tr><td> <input onchange='changearea(" + data[i].id + ");' type='checkbox' id='" + data[i].id + "'/>  </td>";
                str += "<td>" + data[i].description + "</td></tr>";
            }
            $("#CEN tbody").html(str);
        }
        if (val[2] != "") {
            var data = JSON.parse(val[2]);
            var str = "";
            for (var i = 0; i < data.length ; i++) {
                str += "<tr><td> <input  type='checkbox' id='" + data[i].id + "'/> </td>";
                str += "<td>" + data[i].description + "</td></tr>";
            }
            $("#VILL tbody").html(str);
        }
        if (val[3] != "") {
            var data = JSON.parse(val[3]);
            var str = "";
            for (var i = 0; i < data.length ; i++) {
                str += "<tr><td> <input type='checkbox' id='" + data[i].id + "'/>  </td>";
                str += "<td>" + data[i].description + "</td></tr>";
            }
            $("#BIO tbody").html(str);
        }
        callback();
    });
}

function changearea(id) {
  
        User.getRelatedTo( id, function (val) {
            if (val != "") {
                var data = JSON.parse(val);
                if ($("#" + id).prop("checked")) {
                    for (var i = 0; i < data.length; i++) {
                        $("#" + data[i].id).prop("checked", true);
                        if (data[i].type == "CEN") {
                            User.getRelatedTo(data[i].id, function (val1) {
                                if (val1 != "") {
                                    var data1 = JSON.parse(val1);
                                        for (var k = 0; k < data1.length; k++) {
                                            $("#" + data1[k].id).prop("checked", true);
                                        }
                                }
                            });
                        }
                    }

                } else {
                    for (var i = 0; i < data.length; i++) {
                        $("#" + data[i].id).prop("checked", false);
                        if (data[i].type == "CEN") {
                            User.getRelatedTo(data[i].id, function (val1) {
                                if (val1 != "") {
                                    var data1 = JSON.parse(val1);
                                    for (var k = 0; k < data1.length; k++) {
                                        $("#" + data1[k].id).prop("checked", false);
                                    }
                                }
                            });
                        }
                    }
                }
            }

        });
    
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