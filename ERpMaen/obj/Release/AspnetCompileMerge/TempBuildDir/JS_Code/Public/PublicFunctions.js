/************************************/
// Created By : Mostafa Abdelghffar
// Create Date : 23/5/2015 12:00 PM
// Description : This file contains all public javaScript functions for each form
/************************************/
var Pub_date_m;
var Pub_date_hj;
$(function () {
    GetCurrentDate_m_hj();
    $("#UlMenu").css("height", document.documentElement.clientHeight);
});
// enable pnl form for update for each form
function setformforupdate_all() {
    try {
        enableDivFormControls(true);
        $("#pnlConfirm").show();
        $("#pnlFunctions").hide();
    } catch (err) {
        alert(err);
    }
}

// called when row click for each form
function row_click(event) {
    try {
        checlAllRowsSelecte();

        var row = $(event.target).closest("tr");
        var chk = $(row).find('input[type="checkbox"]');

        if (event.target.type != 'checkbox' || $(chk).prop("checked") == true) {
            var row = $(event.target).closest("tr");
            $(chk).prop("checked", true);

            FilterShareDropdown();

            if (typeof editWebServiceMethod != 'undefined') // Any scope
            {
                $.ajax({
                    type: "POST",
                    url: "../ASMX_WebServices/" + editWebServiceMethod,
                    data: "{'editItemId': '" + $(row).prop("id") + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (val) {
                        edit(val.d);
                    },
                    error: function (err) {
                        console.log(err);
                    }
                });
            }

        }
        else {

            var selectedcode = getControlValueById(formAutoCodeControl);
            if (selectedcode == $(row).prop("id")) {
                $(chk).prop("checked", false);
                resetControlValueById(formAutoCodeControl);
                cancel();
                FilterShareDropdown();

            }
            else {
                if ($(chk).prop("checked")) {
                    $(chk).prop("checked", true);
                    FilterShareDropdown();

                }
                else {
                    $(chk).prop("checked", false);
                    FilterShareDropdown();

                }
            }
        }
    } catch (err) {
        alert(err);
    }
}

// update item for each form
function update() {
    try {
        var updateItemId = '';
        var rowscount = 0;
        $('#tablelist tbody tr').each(function () {
            if ($(this).find('input[type="checkbox"]').is(':checked')) {
                var chk = $(this).find('input[type="checkbox"]');
                var n = $(chk).prop("id").indexOf("&");
                if (n != -1) {
                    var autocode = $(chk).prop("id").split("&")[1];
                    updateItemId = autocode;
                    rowscount = rowscount + 1;
                }
            }
        });
        if (rowscount == 1) {
            $.ajax({
                type: "POST",
                url: "../ASMX_WebServices/" + editWebServiceMethod,
                data: "{'editItemId': '" + updateItemId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (val) {
                    edit(val.d);
                },
                error: function (err) {
                    alert(err);
                }
            });
        }
        else {
            showErrorMessage("Select one row to update");
        }
    }
    catch (err) {
        alert(err);
    }
}

// delete item for each form
function deleteItem() {
    try {
        var r = confirm("are you sure you want to delete the records");
        if (r == true) {
            var deleteBool = false;
            var autoCodeValue = getControlValueById(formAutoCodeControl);
            if (autoCodeValue != "") {
                $("#" + autoCodeValue).hide();
                deletedItems = autoCodeValue;
                deleteBool = true;
            }
            else {
                showErrorMessage("No records selected");
            }
        }
        if (deleteBool == true) {
            $.ajax({
                type: "POST",
                url: "../ASMX_WebServices/" + deleteWebServiceMethod,
                data: "{'deleteItem': '" + deletedItems + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (val) {
                    onSuccessDeleteItem(val.d);
                },
                error: function (err) {
                    console.log(err);
                    alert(err);
                }
            });
        }
    }
    catch (err) {
        alert(err);
    }
}

// called after deleteItem function success
function onSuccessDeleteItem(val) {
    try {
        console.log(val);
        if (val[0] != "0") {
            if (val[0] == "1") {
                showSuccessMessage("Deleted Successfully!");
                cancel();
                drawDynamicTable();
            } else {
                showAlarmMessage("Some rows Can`t be deleted it used in the system!");
                cancel();
                drawDynamicTable();
            }
        } else {
            showErrorMessage("Failed To Delete");
        }
    }
    catch (err) {
        alert(err);
    }
}

var formOperation = "";

// called when each form is loaded
function form_load() {
    try {
        $("#pnlConfirm").hide();
        $(".search_continer").hide();
        cancel();
        drawDynamicTable();
        checkFormOperation();

    } catch (err) {
        alert(err);
    }
}

// Get Logged In user Id From Cookies
function getLoggedInUser() {
    WebService.GetSalesmanFromCoockies("", function (value) {
        $("#ddlUsers").val(value);
        return value;
    });
}

// check opration of form when load
function checkFormOperation() {
    try {
        var par = getUrlVars();
        if (jQuery.isEmptyObject(par) == false) {
            var operation = par.operation;
            if (operation.toLowerCase() == "add") {
                add();
                if (par.hasOwnProperty('code')) {
                    var lblPropertyCode = $("<span>" + par.code + "</span>");
                    getPropertyDetails(lblPropertyCode, "Code");
                    getPropertyOwner(lblPropertyCode, "Code");
                }
                if (par.hasOwnProperty('clientcode')) {
                    var lblClientCode = $("<span>" + par.clientcode + "</span>");
                    var formName = getFormName();
                    if (formName == "Correspondance.aspx" || formName == "Leads.aspx" || formName == "ListingSale.aspx" || formName == "ListingRent.aspx" || formName == "ListingShort.aspx") {
                        getContactName(lblClientCode, "Code");
                    } else if (formName == "Deals.aspx") {
                        getDealContactName(lblClientCode, "Code");
                    }
                }
                if (par.hasOwnProperty('leadcode')) {
                    var lblLeadCode = $("<span>" + par.leadcode + "</span>");
                    getLeadDetails(lblLeadCode, "Code");
                }
                if (par.hasOwnProperty('dealcode')) {
                    var lblDealCode = $("<span>" + par.dealcode + "</span>");
                    getDealDetails(lblDealCode, "Code");
                    getDealPropertyAndContactDetails(lblDealCode, "Code");
                }
                if (par.hasOwnProperty('propertycode')) {
                    showProperties();
                    var lblPropertyCode = $("<span>" + par.propertycode + "</span>");
                    getPropertyDetails(lblPropertyCode, "Code");
                }
                if (par.hasOwnProperty('callid')) {
                    $("#txtCall").val(par.callid);
                }
            } else if (operation.toLowerCase() == "search") {
                var code = par.code;
                $("#txtSearchAll").val(code);
                search();
            } else if (operation.toLowerCase() == "update") {
                var code = par.code;
                $("#txtSearchAll").val(code);
                search();
                formOperation = "update";

            }
            else if (operation.toLowerCase() == "convert") {
                var code = par.code;
                getLeadsPropertyDetails(code)
                formOperation = "convert";
            }
        }
    } catch (err) {
        alert(err);
    }
}

// get current form name
function getFormName() {
    try {
        var formName = $("#form1").prop("action").split("?")[0].split("/").pop();
        return formName;
    } catch (err) {
        alert(err);
    }
}

// Read a page's GET URL variables and return them as an associative array.
function getUrlVars() {
    item = {};
    if ($("#form1").prop("action").indexOf("?") >= 0 && $("#form1").prop("action").indexOf("=") >= 0) {
        var hashes = $("#form1").prop("action").slice($("#form1").prop("action").indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            var hash = hashes[i].split('=');
            item[hash[0].toLowerCase()] = hash[1].toLowerCase();
        }
    }
    return item;
}

// prepare form to add
function prepareAdd() {
    try {
        $('#divForm').find('input').removeAttr('disabled');
        $('#divForm').find('select').removeAttr('disabled');
        $('#divForm').find('textarea').removeAttr('disabled');
        $('#divForm').find('div').removeAttr('disabled');
        $('#divForm').find('a').removeAttr('disabled');
        $("#pnlConfirm").show();
        $("#pnlFunctions").hide();
        $("#cmdSave").prop("CommandArgument", "New");

    } catch (err) {
        alert(err);
    }
}

// disable controls
function disableControls(controlsId) {
    try {
        $.each(controlsId, function (i, contId) {
            $("#" + contId).attr("disabled", "disabled");
        });
    } catch (err) {
        alert(err);
    }
}

// reset drop down list controls
function resetDll(ddlControls) {
    try {
        $.each(ddlControls, function (i, val) {
            $("#" + val).empty();
            $("#" + val).append($("<option selected='selected'></option>").val("0").html("-- Select -- "));
        });
    } catch (err) {
        alert(err);
    }
}

// reset form controls
function resetFormControls() {
    try {
        resetDivControls("divForm")
        $("#txtSearch").val("");
        $("#lblResError").hide();
    } catch (err) {
        alert(err);
    }
}

// reset div controls
function resetDivControls(divId) {
    try {
        $("#" + divId).find('input:text').val('');
        $("#" + divId).find('input:checkbox').attr("checked", false);
        $("#" + divId).find('textarea').val('');
        $("#" + divId).find('select').each(function () {
            if ($(this).attr("id") != "ddlPurpose") {
                $(this).val(0);
            }
        });
    } catch (err) {
        alert(err);
    }
}

// set selected option for ddl by text
function setSelectedDdlOptionByText(ddlId, text) {
    try {
        $("#" + ddlId + " option").each(function () {
            if ($(this).text() == text) {
                $(this).attr('selected', 'selected');
            }
        });
    } catch (err) {
        alert(err);
    }
}

// disable the form
function disablepnlform() {
    try {
        enableDivFormControls(false);
        $("#cmdUpdate").removeAttr("disabled");
        $("#cmdDelete").removeAttr("disabled");
        $("#cmdReplay").removeAttr("disabled");
    } catch (err) {
        alert(err);
    }
}

// cancel add/edit operation
function cancel() {
    try {
        resetAll();
        enableDivFormControls(false);
        $("#cmdUpdate").attr("disabled", "disabled");
        $('#cmdDelete').attr("disabled", "disabled");
        $('#cmdReplay').attr("disabled", "disabled");
        $("#pnlConfirm").hide();
        $("#pnlFunctions").show();
        $("#lblResult").hide();
        $("#txtSearchAll").val('');
        try {
            Page_ClientValidate('');
        } catch (err) {
        }
        $(".ajax__fileupload_fileItemInfo").empty();
        $(".photoWrap").empty();
    } catch (err) {
        alert(err);
    }
}
// handle click event of cancel button
function cancelClick() {
    try {
        if (confirm("هل تريد الالغاء ؟")) {
            cancel();
        }
    }
    catch (err) {
        alert(err);
    }
}
// enable or disable div controls
function enableDivFormControls(bool) {
    try {
        var b = !bool;
        $('#divForm').find('input').prop("disabled", b);
        $("#txtUploadedFiles").prop("disabled", false);
        $('#divForm').find('select').prop("disabled", b);
        $('#divForm').find('textarea').prop("disabled", b);
        $('#divForm').find('div').prop("disabled", b);
        $('#divForm').find('#timepicker_input').prop("disabled", false);
        $('#divForm').find('#timepicker_input2').prop("disabled", false);
        if (b) {
            $('#divForm').find('a').attr('disabled', "disabled");
        } else {
            $('#divForm').find('a').removeAttr("disabled");
        }
    }
    catch (err) {
        alert(err);
    }
}

// clear appeared validation errors
function clearValidationErrors() {
    try {
        var i;
        for (i = 0; i < Page_Validators.length; i++) {
            Page_Validators[i].style.display = "none";
        }
    } catch (err) {
        alert(err);
    }
}

// after call update function and get data of selected item
function fillControlsFromJson(data, divId) {
    try {
        $.each(data, function (key, value) {
            if (value != null) {
                var $control;
                if (jQuery.isEmptyObject(divId) == false) {
                    $control = $("#" + divId).find("[dbColumn='" + key + "']");
                } else {
                    $control = $("[dbColumn='" + key + "']");
                }
                var tagName = $control.prop('tagName');
                if (tagName == "SPAN" || tagName == "DIV") {
                    $control.html(data[key]);
                } else if (tagName == "INPUT") {
                    var type = $control.prop('type');
                    if (type == "checkbox") {
                        $control.prop("checked", data[key]);
                    } else {
                        $control.val(data[key]);
                    }
                } else if (tagName == "TEXTAREA" || tagName == "SELECT") {
                    $control.val(data[key]);
                } else if (tagName == "TABLE") {
                    $control.find("tbody tr input:radio").each(function () {
                        if ($(this).val() == data[key])
                            $(this).prop("checked", true);
                    });
                }
            }
        });
    } catch (err) {
        alert(err);
    }
}

// fill json object with data
function fillJSONFromControls(data) {
    try {
        $.each(data, function (key, value) {
            if (value == null) {
                var tagName = $("#" + key).prop('tagName');
                if (tagName == "SPAN" || tagName == "DIV") {
                    data["" + key + ""] = $("#" + key).html();
                } else if (tagName == "INPUT") {
                    var type = $("#" + key).prop('type');
                    if (type == "checkbox") {
                        data["" + key + ""] = $("#" + key).prop("checked");
                    } else {
                        data["" + key + ""] = $("#" + key).val();
                    }
                } else if (tagName == "TEXTAREA" || tagName == "SELECT") {
                    data["" + key + ""] = $("#" + key).val();
                }
            }
        });
        return data;
    } catch (err) {
        alert(err);
    }
}

// generate JSON from controls
function generateJSONFromControls(div = 'divForm') {
    try {
        item = {};
        $("#" + div + " input, #" + div + " span, #" + div + " select, #" + div + "  textarea, #" + div + "  table,#" + div + "  div").each(function () {
            var id = $(this).attr("dbColumn");
            if (id != "") {
                var tagName = $(this).prop('tagName');
                var value = "";
                if (tagName == "SPAN" || tagName == "DIV") {
                    if (tagName == "SPAN" && $(this).find('input:checkbox').length > 0) {
                        value = $(this).find('input:checkbox').prop("checked");
                    } else {
                        value = $(this).html();
                    }
                } else if (tagName == "INPUT") {
                    var type = $(this).prop('type');
                    if (type == "checkbox") {
                        value = $(this).prop("checked");
                    } else {
                        value = $(this).val();

                    }
                } else if (tagName == "TEXTAREA" || tagName == "SELECT") {
                    value = $(this).val();
                    if (tagName == "SELECT") {
                        if ($(this).val() == 0 || $(this).val() == -1) {
                            item[id + "_selTXT"] = "";
                        } else {
                            item[id + "_selTXT"] = $(this).find("option:selected").text();
                        }
                    }
                } else if (tagName == "TABLE") {
                    $(this).find("tbody tr input:radio").each(function () {
                        if ($(this).prop("checked") == true)
                            value = $(this).val();
                    });
                }

                item[id] = value;
            }
        });
        return item;
    } catch (err) {
        alert(err);
    }
}

// check chkAll if all rows checkboxes are checked
// and uncheck chkAll if one row checkbox is unchecked
function checlAllRowsSelecte() {
    try {
        var allChecked = 1;
        $('#tablelist tbody tr').each(function () {
            if ($(this).find('input[type="checkbox"]').is(':checked') == false) {
                allChecked = 0;
            }
        });
        if (allChecked == 1) {
            $('#tablelist thead tr').each(function () {
                if ($(this).find('input[type="checkbox"]').prop("checked", true));
            });
        }
        else {
            $('#tablelist thead tr').each(function () {
                if ($(this).find('input[type="checkbox"]').prop("checked", false));
            });
        }
    } catch (err) {
        alert(err);
    }
}

// get the value of control based on its tagname
function getControlValue(control) {
    try {
        var value = "";
        if ($(control).prop('tagName') == "INPUT") {
            value = $(control).val();
        } else if ($(control).prop('tagName') == "SPAN") {
            value = $(control).html();
        }
        return value;
    } catch (err) {
        alert(err);
    }
}

// get the value of control by id based on its tagname
function getControlValueById(controlId) {
    try {
        var value = "";
        if ($("#" + controlId).prop('tagName') == "INPUT") {
            value = $("#" + controlId).val();
        } else if ($("#" + controlId).prop('tagName') == "SPAN") {
            value = $("#" + controlId).html();
        }
        return value;
    } catch (err) {
        alert(err);
    }
}

// reset the value of control based on its tagname
function resetControlValue(control) {
    try {
        if ($(control).prop('tagName') == "INPUT") {
            $(control).val("");
        } else if ($(control).prop('tagName') == "SPAN") {
            $(control).html("");
        }
    } catch (err) {
        alert(err);
    }
}

// reset the value of control id based on its tagname
function resetControlValueById(controlId) {
    try {
        if ($("#" + controlId).prop('tagName') == "INPUT") {
            $("#" + controlId).val("");
        } else if ($("#" + controlId).prop('tagName') == "SPAN") {
            $("#" + controlId).html("");
        }
    } catch (err) {
        alert(err);
    }
}

// calc square meter based on square feet
function calcsqmt() {
    try {
        var unitAreasqft = $("#txtAreasqft").val().replace(/\,/g, '').replace(",", "");
        var unitAreaM2 = parseFloat(unitAreasqft) * 0.09290304;
        $("#txtAreaM2").val(unitAreaM2.toFixed(2));
        $("#txtAreasqft").val(parseFloat($("#txtAreasqft").val().replace(",", "")).toFixed(2));
    } catch (err) {
        alert(err);
    }
}

// calc square feet based on square meter
function calcsqft() {
    try {
        var unitAreaM2 = $("#txtAreaM2").val().replace(/\,/g, '').replace(",", "");
        var unitAreaSQF = parseFloat(unitAreaM2) / 0.09290304;
        $("#txtAreasqft").val(unitAreaSQF.toFixed(2));
        $("#txtAreaM2").val(parseFloat($("#txtAreaM2").val().replace(",", "")).toFixed(2));
    } catch (err) {
        alert(err);
    }
}

// American Numbering System
// var th = ['', 'thousand', 'million', 'billion', 'trillion'];
// uncomment this line for English Number System
var th = ['', 'thousand', 'million', 'milliard', 'billion'];
var dg = ['zero', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine'];
var tn = ['ten', 'eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen'];
var tw = ['twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];
function convertNumberToWords(s) {
    s = s.toString();
    s = s.replace(/[\, ]/g, '');
    if (s != parseFloat(s))
        return 'not a number';
    var x = s.indexOf('.');
    if (x == -1) x = s.length; if (x > 15) return 'too big';
    var n = s.split(''); var str = ''; var sk = 0;
    for (var i = 0; i < x; i++) {
        if ((x - i) % 3 == 2) {
            if (n[i] == '1') {
                str += tn[Number(n[i + 1])] + ' '; i++; sk = 1;
            } else if (n[i] != 0) { str += tw[n[i] - 2] + ' '; sk = 1; }
        } else if (n[i] != 0) {
            str += dg[n[i]] + ' ';
            if ((x - i) % 3 == 0) str += 'hundred '; sk = 1;
        }
        if ((x - i) % 3 == 1) {
            if (sk) str += th[(x - i - 1) / 3] + ' '; sk = 0;
        }
    } if (x != s.length) {
        var y = s.length; str += 'point '; for (var i = x + 1; i < y; i++) str += dg[n[i]] + ' ';
    } return str.replace(/\s+/g, ' ');
}

// check if endDate is greater than startDate or not
function isDateGreaterThan(endDate, startDate) {
    try {
        var regExp = /(\d{1,2})\/(\d{1,2})\/(\d{2,4})/;
        var startDateInt = parseInt(startDate.replace(regExp, "$3$2$1"));
        var endDateInt = parseInt(endDate.replace(regExp, "$3$2$1"));
        if (startDateInt >= endDateInt) {
            return false;
        } else {
            return true;
        }
    } catch (err) {
        alert(err);
    }
}

// check if endDate is greater than or equal to startDate or not
function isDateGreaterThanOrEqual(endDate, startDate) {
    try {
        var regExp = /(\d{1,2})\/(\d{1,2})\/(\d{2,4})/;
        var startDateInt = parseInt(startDate.replace(regExp, "$3$2$1"));
        var endDateInt = parseInt(endDate.replace(regExp, "$3$2$1"));
        if (startDateInt > endDateInt) {
            return false;
        } else {
            return true;
        }
    } catch (err) {
        alert(err);
    }
}

// check if the float value is valid float or not
function isValidFloat(floatVlaue) {
    try {
        if ((floatVlaue.length - floatVlaue.replace(/\./g, '').length) > 1) {
            return false;
        } else {
            return true;
        }
    } catch (err) {
        alert(err);
    }
}

// check if the float controls have a valid float values or not
function checkFloatControls(controls) {
    try {
        var valid = true;
        $.each(controls, function () {
            var value = $("#" + this.ControlId).val().replace(",", "");
            value = value.replace(",", "");
            if (value != "") {
                if (checkFloatingPoint(value) == false) {
                    showErrorMessage("Please Insert Valid " + this.ControlTitle);
                    valid = false;
                    return false;
                }
                else {
                    $("#" + this.ControlId).val(value);
                }
            }
        });
        return valid;
    } catch (err) {
        alert(err);
    }
}

function checkFloatingPoint(valueNumeric) {
    try {
        var objRegex = /(^-?\d\d*\.\d\d*$)|(^-?\.\d\d*$)/;
        if (valueNumeric.toString().indexOf('.') == -1) {
            valueNumeric = valueNumeric.toString() + ".00";
        }
        if (objRegex.test(valueNumeric)) {
            return true;
        } else {
            return false;
        }
    } catch (err) {
        alert(err);
    }
}

// check if the ddl controls have a selected value or not
function checkDdlControls(controls) {
    try {
        var valid = true;
        $.each(controls, function () {
            if ($("#" + this.ControlId).val() == 0) {
                showErrorMessage("Please Select " + this.ControlTitle);
                valid = false;
                return false;
            }
        });
        return valid;
    } catch (err) {
        alert(err);
    }
}

// check owner data
function checkOwnerData() {
    try {
        if ($('#OwnerAutoCodeNo').html() == "") {
            if ($('#txtOwnerName').val() != "" || $('#txtOwnerMobile').val() != "" || $('#txtOwnerEmail').val() != "") {
                if ($('#txtOwnerName').val() == "") {
                    showErrorMessage("Please Insert Owner Name ");
                    return false;
                }
                if ($('#txtOwnerMobile').val() == "") {
                    showErrorMessage("Please Insert Owner Mobile ");
                    return false;
                }
                if ($('#txtOwnerEmail').val() == "") {
                    showErrorMessage("Please Insert Owner Email ");
                    return false;
                }
            }
            return true;
        }
        return true;
    } catch (err) { alert(err); }

}


//##########################  Share Options  ##########################

//Filters the dropdown list of ShareOptions
function FilterShareDropdown() {
    try {
        if ($('#tablelist').find('input[type="checkbox"]:checked').length > 0) {
            $('#liDownloadPDF').show();
            $('#liDownloadA3').show();
            $('#liDownloadExcelTable').show();
            $('#liDownloadPDFTable').show();
            $('#liDownloadAllPDFTable').show();
            $('#lbdownloadxlsAllTbl').show();
            //$('#liHTML').show();
            //$('#lblPropertyIds').html(chk.id);

            setPropertyIds();
        }
        else {
            $('#liDownloadPDF').hide();
            $('#liDownloadA3').hide();
            $('#liDownloadExcelTable').hide();
            $('#liDownloadPDFTable').hide();
            $('#liDownloadAllPDFTable').hide();
            $('#lbdownloadxlsAllTbl').hide();
            //$('#liHTML').hide();
        }
    } catch (err) {
        alert(err);
    }
}

// set the Property Title .Also sets the user and Agent details on the modal div
function getUserAndAgentDetails(btn) {
    try {
        $("#myModalLabel").html(btn.innerHTML);
        getUserDetails();
        getAgentDetails();
        $("#myModal").show();
    } catch (err) {
        alert(err);
    }
}

// set the user details on the modal div
function getUserDetails() {
    try {
        ListingProperties.getUserInfo($("#lblPropertyId").val(), function (data) {
            var details = JSON.parse(data);
            $('#userEmail').html(details[0]["Email"]);
            $('#userPhone').html(details[0]["Mob"]);
            $('#userName').html(details[0]["Title"] + "." + details[0]["FirstName"] + " " + details[0]["LastName"]);
        });
    } catch (err) {
        alert(err);
    }
}

// set the agent details on the modal div
function getAgentDetails() {
    try {
        ListingProperties.getAgentInfo($("#lblPropertyId").val(), function (data) {
            var details = JSON.parse(data);
            $('#agentName').html(details[0]["SalesmanName"]);
            $('#agentPhone').html(details[0]["Mob"]);
            $('#agentEmail').html(details[0]["Email"]);
            $('#propertyTitle').html(details[0]["PropertyName"]);
        });
    } catch (err) {
        alert(err);
    }
}

// close the div
function closePopUpDiv() {
    try {
        $('#myModal').hide();
        $('#lblPropertyIds').html("");
    } catch (err) {
        alert(err);
    }
}

// download property -Goes to the Property Brochure Form
function exportAsA3Poster() {
    try {
        var agentOrUser = $("input[name=useragent]:checked").val();
        var Title = "exportPDF" + agentOrUser;
        //var url = "http://localhost:57805/lsCRM-1/Property/";
        var url = "http://lscrm.blueberry.software/Property/";

        setPropertyIds();
        var propIds = $("#lblPropertyIds").html().split(",");
        $.each(propIds, function (i, propId) {
            if (propId != "") {
                var src = url + Title + "/" + propId + ",";
                $("<iframe style='display:none' src='" + src + "'></iframe>").appendTo("body");
            }
        });

        $("#lblPropertyIds").html("");
    } catch (err) {
        alert(err);
    }
}

// show/hide the share div
function showHideShareDiv() {
    try {
        var div = document.getElementById('divShare');
        if (div.style.display == 'none') {
            div.style.display = 'block';
        } else if (div.style.display == 'block') {
            div.style.display = 'none';
        }
    } catch (err) {
        alert(err);
    }
}

// set the Property ids of the selected property
function setPropertyIds() {
    try {
        if (document.getElementById("lblPropertyIds")) {
            document.getElementById("lblPropertyIds").innerHTML = "";
            $('#tablelist').find('input[type="checkbox"]:checked').each(function () {
                var id = this.id;
                var str = document.getElementById("lblPropertyIds").innerHTML.toString();
                if (str.indexOf(id.toString().split("&")[1]) == -1) {
                    document.getElementById("lblPropertyIds").innerHTML += id.toString().split("&")[1] + ",";
                }
            });
        }
    } catch (err) {
        alert(err);
    }
}

$(document).mouseup(function (e) {
    var container = $("#divShare");

    if (!container.is(e.target) // if the target of the click isn't the container...
        && container.has(e.target).length === 0) // ... nor a descendant of the container
    {
        container.hide();
    }
});
$(document).mouseup(function (e) {
    var container = $(".ColVis_collection");

    if (!container.is(e.target) // if the target of the click isn't the container...
        && container.has(e.target).length === 0) // ... nor a descendant of the container
    {
        container.hide();
    }
});

// export all
function ExportAll() {
    try {
        var table = $("#tablelist th:first-child, #tablelist td:first-child").remove();
        table = $("#tablelist").tableToJSON();
        $("#tblH").val("");
        $("#tblH").val(JSON.stringify(table).toString());
        drawDynamicTable();
        $("#divShare").hide();
    } catch (err) {
        alert(err);
    }
}

// filter table and export
function FilterTableAndExport() {
    try {
        var CheckedIDs = new Array();
        $('#tablelist').find('input[type="checkbox"]:checked').each(function () {
            var id = this.id;
            if (CheckedIDs.indexOf(id.toString().split("&")[1]) == -1) {
                CheckedIDs.push(id.toString().split("&")[1]);
            }
        });
        $('#tablelist').find("tbody tr").each(function () {
            if (CheckedIDs.indexOf(this.id) == -1) {
                $(this).remove();
            }
        });
        var table = $("#tablelist th:first-child, #tablelist td:first-child").remove();
        table = $('#tablelist').tableToJSON();
        $("#tblH").val("");
        $("#tblH").val(JSON.stringify(table).toString());
        drawDynamicTable();
        $("#divShare").hide();
    } catch (err) {
        alert(err);
    }
}
function cust_chkNumber(evt, element, len) {
    debugger
    if ($(element).val().length >= len) {
        return false;
    } else {
        return isNumber(evt);
    }
}
// accept only entering numbers
function isNumber(evt) {
    try {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    } catch (err) {
        alert(err);
        return false;
    }
}

// accept only entering decimal (numbers with one dot and only two digits)
function isDecimal(sender) {
    try {
        if (event.keyCode > 47 && event.keyCode < 58 || event.keyCode == 46) {
            var amount = $(sender).val();
            var present = 0;
            var count = 0;

            do {
                present = amount.indexOf(".", present);
                if (present != -1) {
                    count++;
                    present++;
                }
            } while (present != -1);

            if (present == -1 && amount.length == 0 && event.keyCode == 46) {
                event.keyCode = 0;
                //alert("Wrong position of decimal point not  allowed !!");
                return false;
            }

            if (count >= 1 && event.keyCode == 46) {
                event.keyCode = 0;
                //alert("Only one decimal point is allowed !!");
                return false;
            }

            if (count == 1) {
                var lastdigits = amount.substring(amount.indexOf(".") + 1, amount.length);
                if (lastdigits.length >= 2) {
                    //alert("Two decimal places only allowed");
                    event.keyCode = 0;
                    return false;
                }
            }
            return true;
        } else {
            event.keyCode = 0;
            //alert("Only Numbers with dot allowed !!");
            return false;
        }
    } catch (err) {
        alert(err);
        return false;
    }
}

// accept only entering phone & mobile & fax numbers
function isMobilePhoneFax(evt) {
    try {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && charCode != 45 && charCode != 42 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    } catch (err) {
        alert(err);
        return false;
    }
}

// accept only entering date (numbers with slash)
function isDate(evt) {
    try {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && charCode != 47 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    catch (err) {
        alert(err);
        return false;
    }
}

// accept only valid characters for name / firstName/ lasName
function isName(evt) {
    try {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        var reg = /^[a-zA-Z'.\s]{1,40}$/;
        return (reg.test(String.fromCharCode(charCode)));
    } catch (err) {
        alert(err);
        return false;
    }
}

// get Today Date
function getTodayDate() {
    try {
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = d.getFullYear() + '/' +
            (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day;
        return output;
    } catch (err) {
        alert(err);
    }
}

// get Today Date
function returnDate(date1) {
    try {
        var d = new Date(date1);

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = d.getFullYear() + '/' +
            (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day;
        alert(output);
        return output;
    } catch (err) {
        alert(err);
    }
}

//show message in alabel inside the popup
function showPopUpErrorMessage(lblResId, messageText) {
    try {
        $("#" + lblResId).html(messageText);
        $("#" + lblResId).css("background-color", "red");
        $("#" + lblResId).show();
    } catch (err) {
        alert(err);
    }
}

// check if value is null then return "", else return value
function getValueOrEmpty(value) {
    try {
        if (value == "undefined" || value == null || value == "") {
            return "";
        } else {
            return value.toString().trim();
        }
    } catch (err) {
        alert(err);
    }
}

// get Lockup value for dynamic table select
function getLockupValues(Type) {
    try {
        var LockupValues = "";
        WebService.GetlockupValues(Type, function (Values) {
            LockupValues = Values;
        });
        return LockupValues;
    } catch (err) {
        alert(err);
    }
}

// check language of app and load from with this language
function checkLanguage() {
    try {
        //        if (currentLanguage == "Arabic") {
        //            $("#HDasboard").find("span").remove();
        //            var data = allLanguage;
        //            var numberOfMngrName = 0;
        //            for (var i = 0; i < allLanguage.length; i++) {
        //                if (allLanguage[i].arabic != null) {
        //                    var arabic = allLanguage[i].arabic;
        //                    var english = allLanguage[i].english;
        //                    numberOfMngrName++;
        //                    var pageTitle = document.title;
        //                    english = english.replace(/\s/g, '');
        //                    english = english.toLowerCase();
        //                    pageTitle = pageTitle.replace(/\s/g, '');
        //                    pageTitle = pageTitle.replace(/\-/g, '');
        //                    pageTitle = pageTitle.toLowerCase();
        //                    if (pageTitle == english) {
        //                        document.title = arabic;
        //                    }
        //                    $("span ,label,:button,a,legend,option, :button,table tbody tr td div a span,table thead tr th ").each(function () {
        //                        var str = $(this).html();
        //                        str = str.replace(/<\/?span[^>]*>/g, "");
        //                        str = str.replace(/<\/?i[^>]*>/g, "");
        //                        str = str.replace(/<\/?pseudo[^>]*>/g, "");
        //                        var englishText = str;
        //                        str = str.replace(/(\r\n|\n|\r)/gm, "");
        //                        
        //                        str = str.replace(/\s/g, '');
        //                        str = str.replace(/\-/g, '');
        //                        str = str.toLowerCase();
        //                        if (str == english) {
        //                            $(this).html(arabic)
        //                            //$(this).html($(this).html().replace(/(\r\n|\n|\r)/gm, "").replace(/  +/g, ' ').replace(englishText.replace(/  +/g, ' '), arabic));
        //                        }
        //                    });
        //                    $(":submit,ul li :button").each(function () {
        //                        var str = $(this).val();
        //                        str = str.replace(/<\/?span[^>]*>/g, "");
        //                        str = str.replace(/<\/?i[^>]*>/g, "");
        //                        str = str.replace(/<\/?pseudo[^>]*>/g, "");
        //                        //str = element.html();
        //                        str = str.replace(/(\r\n|\n|\r)/gm, "");
        //                        str = str.replace(/\s/g, '');
        //                        str = str.replace(/\-/g, '');
        //                        str = str.replace(/\&/g, '');
        //                        str = str.toLowerCase();
        //                        if (str == english) {
        //                            alert(str);
        //                            $(this).val(arabic);
        //                        }
        //                        $("#lnbootstrapmin").attr("href", "App_ThemesEn/styles/bootstrap.min.css");
        //                        $("#lnStyleSheet").attr("href", "App_ThemesEn/Theme1/StyleSheet.css");
        //                        $("#lnawesome").attr("href", "App_ThemesEn/Theme1/font-awesome.min.css");
        //                        $("#lnvmenu").attr("href", "App_ThemesEn/Theme1/vmenu.css");
        //                        $("#lnstylesheetscc").attr("href", "cssEn/StyleSheet.css");
        //                        $("#lnanimate").attr("href", "App_ThemesEn/Theme1/animate.css");
        //                        $("#lnform").attr("href", "App_ThemesEn/styles/form.css");
        //                        $("#lnDatatable").attr("href", "cssEn/plugins/datatable/jquery.dataTables.css");
        //                        $("#lnDatatableTool").attr("href", "cssEn/plugins/datatable/tableTools.css");
        //                       
        //                    });
        //                }
        //            }
        //            $("#spnLanguage").html("English");
        //           
        //        } else {
        //            $("#spnLanguage").html("عربى");
        //        }
    } catch (err) {
        alert(err);
    }
}

//change language function 
function changeLanguage() {
    try {
        if (currentLanguage == "English") {
            setCookie("Language", "Arabic", 1000)
            location.reload();
        } else {
            setCookie("Language", "English", 1000)
            location.reload();
        }
    } catch (err) {
        alert(err);
    }
}

// set language coockie
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}

function checkRequired(div = 'divForm') {

    var requiredall = 0;
    var notrequiredall = 1;

    $(" #" + div + " input[dbcolumn], #" + div + "  select[dbcolumn], #" + div + "  textarea[dbcolumn]").each(function () {
        var value = $(this).val();
        if ($(this).prop('tagName') == "SELECT" && value == "0") {
            value = "";
        }
        if ($(this).prop('required') && value == "") {
            requiredall = 1;
            $(this).addClass('error');

        } else {
            notrequiredall = 0;
            $(this).removeClass('error');          
        }

    });

    return requiredall;

}
function setRequired_Date(div) {

    $("#" + div + " #txtDatem").prop("required", true);
    $("#" + div + " #txtDateh").prop("required", true);
    //$("#Text9").prop("required", true);
}

function setRequired_time(div) {
    $("#" + div + " input:text").first().prop("required", true);
    $("#" + div + " input:text").first().prop("required", true);
    $("#" + div + " input:text").first().attr("dbcolumn", "");
    //$("#Text9").prop("required", true);
}
function GetCurrentDate_m_hj() {
    var arr_date = [];
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd;
    }

    if (mm < 10) {
        mm = '0' + mm;
    }

    today = dd + '/' + mm + '/' + yyyy;
    $("#CurrentDate").find("#txtDatem").val(today);
    showHideCalendar($("#CurrentDate").find("#txtDatem"));
    cal2.callback();
    Pub_date_m = today;
    Pub_date_hj = $("#CurrentDate").find("#txtDateh").val();
 

}