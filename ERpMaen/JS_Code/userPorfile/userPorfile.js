$(function () {
    try {
        $("input").attr("class", "");
        $("select").attr("class", "");
        $("input").addClass("form-control");
        $("select").addClass("form-control");
        $("#pnlConfirm").hide();
        $("#divData").hide();
        $("#SavedivLoader").show();
        $("#cmdAdd").closest("li").remove();
        $("#cmdDelete").closest("li").remove();
        userPorfile.GetUserData( function (val) {
            edit(val);
         });
    } catch (err) {
        alert(err);
    }
});


function save() {
    try {
    $("input").removeClass('error');
    $("select").removeClass('error');
    if ($("#txtindenty").val().length != 10) {
        showErrorMessage("رقم الهوية  يجب أن يكون 10 ارقام");
        return;
    }

    if ($("#phone").val().length != 10) {
        showErrorMessage("رقم الجوال  يجب أن يكون 10 ارقام");
        return;
    }
    if (!checkRequired("divForm")) {
        //   $("#pnlConfirm").hide();
        $("#SavedivLoader").show();
        var arrData = new Array;
        var basicData = generateJSONFromControls("divForm");
        var UserId = $("#lblmainid").html();
        var permarrData = [];
        var mainImag = $("#imgItemURL").prop("src");
        var researchArea_arrData = [];
            $("#LiPlaces").find('input[type=checkbox]:checked').each(function () {
                var data = { "area_id": $(this).attr("id") };
                researchArea_arrData.push(data);
            });
            User.SaveUser(UserId, basicData, researchArea_arrData, mainImag, function (val) {
                $("#SavedivLoader").hide();
                if (val.split("|")[0] == "True") {
                    showSuccessMessage("تم الحفظ بنجاح");
                } else {
                    showErrorMessage(val.split("|")[1]);
                    $("#pnlConfirm").show();
                    $("#SavedivLoader").hide();
                }
            });
         }

    } catch (err) {
        alert(err);
    }
}

function edit(val) {
    try {
        cancel();
        resetAll();
        if (val[0] != "") {
            var data = JSON.parse(val[1]);
            fillControlsFromJson(data[0]);
            
            changeResearcher(function () {

                if (val[1] != "") {
                    var P_data = JSON.parse(val[1]);
                    for (var i = 0; i < P_data.length; i++) {
                        $("#LiPlaces").find("#" + P_data[i].area_id).prop("checked", true);
                    }
                }
            });


            if (data[0].User_Image != "") {
                $("#divuploadimage img").prop("src", data[0].User_Image);
                $("#imgItemURL").prop("src", data[0].User_Image);
                $("#imagemain").prop("src", data[0].User_Image);
                $("#lblMainUserName").html(data[0].User_FullNameEn);

            } else {
                $("#divuploadimage img").prop("src", "");
                $("#imgItemURL").prop("src", "");
            }
            //showSuccessMessage("Record Selected1");
            $("#bg-purple").addClass("in");
            $("#bg-purple").attr("style", "");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
            
            $("#pnlConfirm").hide();
            $("#divData").show();
            $("#SavedivLoader").hide();

        } else {
            showErrorMessage("No data found !!");
        }
    } catch (err) {
        alert(err);
    }
}


function edit(val) {
    cancel();
    resetAll();
        if (val != "") {
            var data = JSON.parse(val);
            fillControlsFromJson(data[0],"divForm");
        }
        $("#pnlConfirm").hide();
        $("#divData").show();
    $("#SavedivLoader").hide();
    $("#cmdUpdate").removeAttr("disabled");
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
function changeResearcher(callback) {

    var Researcher = $("#Researcher").is(':checked');
    if (Researcher) {
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
            for (var i = 0; i < data.length; i++) {
                str += "<tr><td> <input onchange='changearea(" + data[i].id + ");' type='checkbox' id='" + data[i].id + "'/> </td>";
                str += "<td>" + data[i].description + "</td></tr>";
            }
            $("#CTY tbody").html(str);
        }
        if (val[1] != "") {
            var data = JSON.parse(val[1]);
            var str = "";
            for (var i = 0; i < data.length; i++) {
                str += "<tr><td> <input onchange='changearea(" + data[i].id + ");' type='checkbox' id='" + data[i].id + "'/>  </td>";
                str += "<td>" + data[i].description + "</td></tr>";
            }
            $("#CEN tbody").html(str);
        }
        if (val[2] != "") {
            var data = JSON.parse(val[2]);
            var str = "";
            for (var i = 0; i < data.length; i++) {
                str += "<tr><td> <input  type='checkbox' id='" + data[i].id + "'/> </td>";
                str += "<td>" + data[i].description + "</td></tr>";
            }
            $("#VILL tbody").html(str);
        }
        if (val[3] != "") {
            var data = JSON.parse(val[3]);
            var str = "";
            for (var i = 0; i < data.length; i++) {
                str += "<tr><td> <input type='checkbox' id='" + data[i].id + "'/>  </td>";
                str += "<td>" + data[i].description + "</td></tr>";
            }
            $("#BIO tbody").html(str);
        }
        callback();
    });
}

function changearea(id) {

    User.getRelatedTo(id, function (val) {
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
