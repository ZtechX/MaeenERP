var doRedirect = false;
$(function () {
    try {
        debugger
        $("input").attr("class", "");
        $("select").attr("class", "");
        $("input").addClass("form-control");
        $("select").addClass("form-control");
        $("#SavedivLoader").show();
      
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
        $("#SavedivLoader").show();
        var arrData = new Array;
        var basicData = generateJSONFromControls("divForm");
        var UserId = $("#lblmainid").html();
        var permarrData = [];
        var mainImag = $("#imgItemURL").prop("src");
        
        userPorfile.SaveUser(UserId, basicData, mainImag, function (val) {
                $("#SavedivLoader").hide();
                if (val.split("|")[0] == "True") {
                    showSuccessMessage("تم الحفظ بنجاح");
                    if (doRedirect) {
                        location.replace("/Dashboard.aspx");
                    }
                    
                } else {
                    showErrorMessage(val.split("|")[1]);
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
        //cancel();
        debugger
        resetAll();
        if (val[0] != "") {
            var data = JSON.parse(val[0]);
            fillControlsFromJson(data[0]);
            $("#userType").html(data[0].userType);
            
            if (data[0].User_Image != "") {
                $("#divuploadimage img").prop("src", data[0].User_Image);
                $("#imgItemURL").prop("src", data[0].User_Image);
                $("#imagemain").prop("src", data[0].User_Image);
                $("#lblMainUserName").html(data[0].User_FullNameEn);
            } else {
                $("#divuploadimage img").prop("src", "");
                $("#imgItemURL").prop("src", "");
            }
           
            $("#SavedivLoader").hide();
            if (data[0].User_Type == 9 && !(data[0].password_changed)) {
                doRedirect = true;
                $("#cssmenu li:not(:first-child)").remove();
                $("#txtUserPassword").val("");
                alert("يرجى تغيير كلمة المرور الأفتراضية");
                
            }
        } else {
            $("#userType").html("");
            showErrorMessage("No data found !!");
        }
    } catch (err) {
        alert(err);
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

// reset all controls when add or cancel
function resetAll() {
    try {
        resetFormControls();
        $("#lblmainid").html("");
        $("#imgItemURL").attr("src", "../App_Themes/images/add-icon.jpg");
        $("#tablePrint").find('input:checkbox').attr("checked", false);
        $(".btn-group").find("span:first-child").css("display", "none");
        $(".btn-group").find("span:last-child").css("display", "inline-block");
        setformforupdate();
        $("#pnlFunctions").show();
    } catch (err) {
        alert(err);
    }
}
