/************************************/
// Created By : Ahmed Nayl
// Create Date : 10/10/2018 3:30 PM
// Description : This file contains all javaScript functions related to travel_agencies form 
/************************************/


//public variables
var deleteWebServiceMethod = "out_inside_letters.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "out_inside_letters.asmx/Edit";
var formAutoCodeControl = "lblmianid";
// load function set defualt values

$(function () {
    try {
        setMaxBarcode();
        form_load();
        var related_letter_id = $("#related_letter_id").html();
        
        if (related_letter_id > 0) {
           // alert(related_letter_id);
            letter_replay();
            $("#lbl_rep").hide();
        }

        $("#ddlContinue").prop("disabled", true);

    } catch (err) {
        alert(err);
    }
});

function setMaxBarcode() {
    out_inside_letters.getBarCode(function (val) {
        $("#txtcode").val(val);
    });
}

// draw dynamic table for existing travel_agencies
function drawDynamicTable() {
    debugger;
    try {
        var tableSortingColumns = [
            { orderable: false }, null,null, null, null, null, null, 
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" },
        ];

        var tableColumnDefs = [

        ];
       // select tblletter.id as 'AutoCodeHide', tblletter.title as 'عنوان الخطاب ',tblletter.details as 'تفاصيل الخطاب', tbllock_up.description as 'صادر من',to_d.description as 'صادر الي',tblUsers.User_Name as 'الراسل', 1 as ' تعديل /حذف' from tblletter inner join  tbllock_up on tblletter.from_dep=tbllock_up.id  inner join tbllock_up to_d on to_d.id=tblletter.to_dep inner join tblUsers on tblletter.add_by=tblUsers.id where tblletter.type=1 and tbllock_up.type='MG' and ISNUll(tblletter.deleted,0)=0

        var initialSortingColumn = 0;
        loadDynamicTable('out_inside_letters', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
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

function resetAll() {
    try {
        resetFormControls();
        $("#lblmianid").html("");
        setMaxBarcode();
    } catch (err) {
        alert(err);
    }
}

function save() {
    try {
        if (!checkRequired()) {
            $("#lbldate").html($("#divdate1 #txtDatem").val());
            $("#lbldate_hj").html($("#divdate1 #txtDateh").val());
            var basicData = generateJSONFromControls("divForm");
            var PosId = $("#lblmianid").html();
            var atch_files = get_files_ArrJson();
            out_inside_letters.Save(PosId, basicData,atch_files, function (val) {
                if (val == true) {
                    cancel();
                    resetAll();
                    drawDynamicTable();
                    $("#lbl_replaye").html("");
                    showSuccessMessage('تم تسجيل البيانات بنجاح');

                } else {
                    showErrorMessage(val.split("|")[1]);
                }
            });
        }
        else {
            showErrorMessage("يرجى ادخال البيانات المطلوبه");
        }
    } catch (err) {
        alert(err);
    }
}



// called after update function success
function edit(val) {
    try {
        var data = JSON.parse(val[1]);
        cancel();
        if (val[0] != "0") {

           
            fillControlsFromJson(data[0]);
            $("input[name=radio-btn][value=" + data[0].from_type_id + "]").prop('checked', true);
            //get_fromdep(data[0].from_type_id);
            //get_fromdep_replay(data[0].from_type_id, data[0].from_dep);

            $("input[name=radio-btn2][value=" + data[0].to_type_id + "]").prop('checked', true);
            // get_todep(data[0].to_type_id);
            //get_todepreplay(data[0].to_type_id, data[0].to_dep);

            fillImages(val[2]);

            $("#divdate1 #txtDateh").val(data[0].date_hj);
            $("#divdate1 #txtDatem").val(data[0].date);
            $("#imgItemURL").prop("src", data[0].logo);
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
            $('.action-btns').css('display', 'none');
            
            var dd = data[0].related_id;
            var letter_name ="رد علي الخطاب :" + data[0].title ;
            if (dd > 0) {
                $("#lbl_replaye").html(letter_name);
            }
            else {
                $("#lbl_replaye").html("");
            }
            
           
            showSuccessMessage("");
            if (formOperation == "update") {
                setformforupdate();
                formOperation = "";
            }
        } else {
            showErrorMessage("No data found !!");
        }
    } catch (err) {
        alert(err);
    }
}

function letter_replay() {
    try {
        var related_letter_id = $("#related_letter_id").html();
        out_inside_letters.letter_replay(related_letter_id, function (val) {
            

            var data = JSON.parse(val[1]);
            cancel();
            if (val[0] != "0") {


                fillControlsFromJson(data[0]);

                $("input[name=radio-btn][value=" + data[0].to_type_id  + "]").prop('checked', true);
                get_fromdep_replay(data[0].to_type_id, data[0].to_dep);
                $('input[name="radio-btn"]').attr('disabled', 'disabled');
                $("input[name=radio-btn2][value=" + data[0].from_type_id + "]").prop('checked', true);
                get_todepreplay(data[0].from_type_id, data[0].from_dep);
                $('input[name="radio-btn2"]').attr('disabled', 'disabled');
                //fillImages(val[2]);
                $("#divdate1 #txtDateh").val(data[0].date_hj);
                $("#divdate1 #txtDatem").val(data[0].date);
                $("#imgItemURL").prop("src", data[0].logo);
                $("#cmdSave").prop("CommandArgument", "Update");
                $("#cmdUpdate").removeAttr('disabled');
                $("#cmdDelete").removeAttr('disabled');
                $('.action-btns').css('display', 'none');
                $("#ddlContinue").val(2);
                var letter_name ="رد علي الخطاب :" +data[0].title ;
                $("#lbl_replaye").html(letter_name);
                document.getElementById('lbl_replaye').style.color = "red";
                $("#lbl_rep").html(related_letter_id);
                $("#lblmianid").html("");

                $("#txtcode").val("");
                showSuccessMessage('تم جلب بيانات الخطاب للعمل الرد');
              
            } else {
                showErrorMessage("No data found !!");
            }

        });


    } catch (err) {
        alert(err);
    }
}

function fillImages(data) {
    try {
        if (data != "") {
            var files = JSON.parse(data);
            if (files.length > 0) {
              //  $('#tblUploadedFiles').remove();
              //  createTblUploadedFiles(["Index", "File", "Name", "Delete"]);
                for (i = 0; i < files.length; i++) {
                    appendFileTR(files[i], i);
                }
                $("#txtUploadedFiles").val(files.length);
            }
        }
    } catch (error) {
        alert(error);
    }
}

function get_files_ArrJson() {
    try {
        var arrData = [];
        $("#tblUploadedFiles tbody tr").each(function () {
            if ($(this).find('#cmdDelete').val() != "Deleted") {
                var data = { "Id": $(this).find("#lblFileId").html(), "Name": $(this).find("#txtTitle").val(), "Type": $(this).find('select').val(), "url": $(this).find('img').prop("id"), "MainImage": $(this).find("#chkMainImage").prop("checked"), "LogoImage": $(this).find("#chkLogoImage").prop("checked"), "deleted": 0, "orderid": $(this).find("#lblOrderId").html() };
                arrData.push(data);
            }
            else {
                if ($(this).find("#lblFileId").html() != "") {
                    var data = { "Id": $(this).find("#lblFileId").html(), "Name": $(this).find("#txtTitle").val(), "Type": $(this).find('select').html(), "url": $(this).find('img').prop("id"), "MainImage": $(this).find("#chkMainImage").prop("checked"), "LogoImage": $(this).find("#chkLogoImage").prop("checked"), "deleted": 1, "orderid": $(this).find("#lblOrderId").html() };
                    arrData.push(data);
                }
            }
        });
        return arrData;
    } catch (err) {
        alert(err);
    }
}

function get_fromdep(dep_id) {
    try {
        $("#from_type_id").html(dep_id);
        out_inside_letters.get_fromdep(dep_id, function(val) {
         
            if (val[0] != "0") {
                $("#ddlfrom_dep").empty();
               $("#ddlfrom_dep").append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
               for (var i = 1; i <= val.length - 1; i++) {
                   $("#ddlfrom_dep").append($("<option ></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
                    //if (val[i] == dep_id) {
                    //    $("#ddlfrom_dep").append($("<option selected='selected ></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
                    //} else {
                    //    $("#ddlfrom_dep").append($("<option ></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
                    //}
                }

            } else {
                $("#ddlfrom_dep").append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
            }

        });

    } catch (error) {
        alert(error);
    }
}


function get_todep(dep_id) {
    try {
        $("#to_type_id").html(dep_id);

        out_inside_letters.get_fromdep(dep_id, function (val) {

            if (val[0] != "0") {
                $("#ddlto_dep").empty();
                $("#ddlto_dep").append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
                for (var i = 1; i <= val.length - 1; i++) {
                    $("#ddlto_dep").append($("<option ></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
                }

            } else {
                $("#ddlto_dep").append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
            }

        });

    } catch (error) {
        alert(error);
    }
}


function get_fromdep_replay(dep_type_id, dep_id) {
    try {
        $("#from_type_id").html(dep_type_id);
        out_inside_letters.get_fromdep(dep_type_id, function (val) {
           // alert(dep_id);
            if (val[0] != "0") {
                $("#ddlfrom_dep").empty();
               for (var i = 1; i <= val.length - 1; i++) {
                    if (val[i].split("|")[1] == dep_id) {
                      //  alert(val[i].split("|")[1] + "   " + dep_id);
                        $("#ddlfrom_dep").append($("<option selected='selected' ></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
                    } else {
                        $("#ddlfrom_dep").append($("<option ></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
                    }
                }

            } else {
                $("#ddlfrom_dep").append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
            }

        });

    } catch (error) {
        alert(error);
    }
}


function get_todepreplay(dep_type_id, dep_id) {
    try {
        $("#to_type_id").html(dep_type_id);

        out_inside_letters.get_fromdep(dep_type_id, function (val) {

            if (val[0] != "0") {
                $("#ddlto_dep").empty();
                $("#ddlto_dep").append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
                for (var i = 1; i <= val.length - 1; i++) {
                    if (val[i].split("|")[1] == dep_id) {
                        $("#ddlto_dep").append($("<option selected='selected' ></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
                    } else {
                        $("#ddlto_dep").append($("<option ></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
                    }
                }

            } else {
                $("#ddlto_dep").append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
            }

        });

    } catch (error) {
        alert(error);
    }
}
function setformforupdate() {
    try {
        setformforupdate_all();
    } catch (err) {
        alert(err);
    }
}
