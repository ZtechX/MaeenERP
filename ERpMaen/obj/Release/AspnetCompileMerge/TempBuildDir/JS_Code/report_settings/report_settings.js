
$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").show();
    try {
        get_Data();
        
    } catch (err) {
        alert(err);
    }
});

function get_Data() {
    report_settings.get_Data(function (val) {
        
            if (val[0] != "") {
                $("#imgItemURL_header").attr("src", val[0]);
            } else {
                $("#imgItemURL_header").attr("src", "../App_Themes/images/add-icon.jpg");
            }
        if (val[1] != "") {
            $("#imgItemURL_footer").attr("src", val[1]);
            } else {
                $("#imgItemURL_footer").attr("src", "../App_Themes/images/add-icon.jpg");
            }
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
        

    });

}

function resetAll() {
    try {
        debugger
        $("#cmdUpdate").removeAttr('disabled');
       
    } catch (err) {
        alert(err);
    }
}

function save() {
    
    try {
        $("#SavedivLoader").show();
        var headerImag = $("#imgItemURL_header").prop("src");
        var footerImag = $("#imgItemURL_footer").prop("src");
        if (headerImag.indexOf("App_Themes/images/add-icon.jpg") != -1) {
            headerImag = "";
        }
        if (footerImag.indexOf("App_Themes/images/add-icon.jpg") != -1) {
            footerImag = "";
        }
        if (headerImag != "" || footerImag!= "") {
            report_settings.save(headerImag, footerImag, function (val) {
                $("#SavedivLoader").hide();
                if (val ) {
                    showSuccessMessage('تم الحفظ بنجاح');
                    cancel();
                    get_Data();
                } else {
                    showErrorMessage('لم يتم الحفظ');
                    $("#pnlConfirm").show();
                }
            });
        }
        else {
            showErrorMessage("يرجى أختيار الصور");
            $("#pnlConfirm").show();
            $("#SavedivLoader").hide();
        }
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