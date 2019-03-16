/************************************/
// Created By : Mostafa Abdelghffar 
// Create Date : 28/7/2015 2:00 PM
// Description : This file contains all javaScript functions related to listing forms 
/************************************/

// load function set defualt values
$(function () {
    try {
        form_load();
    } catch (err) {
        alert(err);
    }
});

// empty pnlform and show it and hide function panel
function add() {
    try {
        prepareAdd();
        resetAll();
        $("#txtPropName").removeAttr('disabled');
        $("#lblPropertyId").val('');
        $("#txtPropCode").attr("disabled", "disabled");
        $('#txtFacilites').attr('readonly', true);
        $('#txtUploadedFiles').attr('readonly', true);
        $('#txtGeopintx').attr('readonly', true);
        $('#txtGeopinty').attr('readonly', true);
        $('#ddlPurpose').attr("disabled", "disabled");
        $(".btnMap").removeAttr("disabled");
        getFacility();
        getPortals();
        if ($("#lblFormType").html() == "Rent") {
            $("#ddlPurpose option:contains(Rent)").attr('selected', 'selected');
        } else if ($("#lblFormType").html() == "Sale") {
            $("#ddlPurpose option:contains(Sale)").attr('selected', 'selected');
        } if ($("#lblFormType").html() == "Short Term") {
            $("#ddlPurpose option:contains(Short Term)").attr('selected', 'selected');
        }
        $("#lblFormMode").html("(اضافة)");
    } catch (err) {
        alert(err);
    }
}

// reset all controls when add or cancel
function resetAll() {
    // reset using public reset functions
    var ddlControls = ["ddlCommunity", "ddlSubCommunity", "ddlBuilding"];
    resetDll(ddlControls);
    resetFormControls();

    // reset specific controls for this form
    $('#tblUploadedFiles').remove();
    $('#divDescription').html('');
    $('#txtProkDetMainInfo').html('');
    resetOwnerDetails();
    getAgentDetails();
    $("#lblFormMode").html("");
    $(".btnMap").attr("disabled", "disabled");
}

// show facilities
function showFacilities() {
    try {
        if (event.target.type == 'text') {
            if ($("#txtFacilites").val() != 0 && $("#txtFacilites").val() != "") {
                $("#pnlFacility").dialog({ modal: true, title: 'Facilities', show: 'slide', width: 600 });
            }
        }
        else {
            $("#pnlFacility").dialog({ modal: true, title: 'Facilities', show: 'slide', width: 600 });
        }
    } catch (err) {
        alert(err);
    }
}
// show facilities
function showPortals() {
    try {
        if (event.target.type == 'text') {
            if ($("#txtPortals").val() != 0 && $("#txtPortals").val() != "") {
                $("#pnlPortals").dialog({ modal: true, title: 'Portals', show: 'slide', width: 600 });
            }
        }
        else {
            $("#pnlPortals").dialog({ modal: true, title: 'Portals', show: 'slide', width: 600 });
        }
    } catch (err) {
        alert(err);
    }
}

// search function
function search() {
    var value = $("#txtSearchAll").val();
    if (value != "") {
        ListingProperties.checksearchvalue(value,'Rent', function (val) {
            if (val.split("|")[0] == "True") {
                ListingProperties.EditProperties(val.split("|")[1], edit);
            }
            else {
                showErrorMessage("No data found !!");
            }
        });
    } else {
        cancel();
        resetAll();
    }
}

// add template based on selected agent
function addAgentTemplate() {
    try {
        if ($("#lblSalesman").html() == "") {
            alert("No Agent Selected");
        } else {
            ListingProperties.GetAgentTemplate($("#lblSalesman").html(), function (template) {
                if (template == "") {
                    alert("There is no templates for the selected agent");
                } else {
                    appendTemplateToDescription(template);
                }
            });
        }
    } catch (err) {
        alert(err);
    }
}

// add template based on the company
function addCompanyTemplate() {
    try {
        ListingProperties.GetCompanyTemplate(function (template) {
            if (template == "") {
                alert("There is no templates for the company");
            } else {
                appendTemplateToDescription(template);
            }
        });
    } catch (err) {
        alert(err);
    }
}

// add template based on selected city and community
function addLocationTemplate() {
    try {
        if ($("#ddlUnitCity").val() == "0" || $("#ddlCommunity").val() == "0") {
            alert("No City or Community Selected");
        } else {
            ListingProperties.GetLocationTemplate($("#ddlUnitCity").val(), $("#ddlCommunity").val(), function (template) {
                if (template == "") {
                    alert("There is no templates for the selected city and community");
                } else {
                    appendTemplateToDescription(template);
                }
            });
        }
    } catch (err) {
        alert(err);
    }
}

// append template to description 
function appendTemplateToDescription(template) {
    try {
        if ($(".nicEdit-main").html() == "") {
            $(".nicEdit-main").html(template);
        } else {
            $(".nicEdit-main").html($(".nicEdit-main").html() + "</br>" + template);
        }
    } catch (err) {
        alert(err);
    }
}
// update properties set updated date with current date 
function updateSelectedProperties(updateType) {
    try {
        var updateItemId = '';
        var rowscount = 0;
        $('#tablelist tbody tr').each(function () {
            if ($(this).find('input[type="checkbox"]').is(':checked')) {
                var chk = $(this).find('input[type="checkbox"]');
                var n = $(chk).prop("id").indexOf("&");
                if (n != -1) {
                    var autocode = $(chk).prop("id").split("&")[1];
                    updateItemId = updateItemId + autocode + "|";
                    rowscount = rowscount + 1;
                }
            }
        });
        if (rowscount != 0) {
            var r = confirm("Are you sure you want to update selected records");
            if (r == true) {
                ListingProperties.UpdateSelectedProperties(updateType, updateItemId, function (val) {
                    if (val[0] == "1") {
                        showSuccessMessage("Update  Successfully!");
                        drawDynamicTable();
                    } else {
                        showErrorMessage("Failed To Update");
                    }
                });
            }
        } else {
            if (updateType == "collectionUpdate") {
                showErrorMessage("Please select rows to collection update");
            } else if (updateType == "showInWebsite") {
                showErrorMessage("Please select rows to show in website");
            } else if (updateType == "deleteFromWebsite") {
                showErrorMessage("Please select rows to delete from website");
            }
        }
    } catch (err) {
        alert(err);
    }
}