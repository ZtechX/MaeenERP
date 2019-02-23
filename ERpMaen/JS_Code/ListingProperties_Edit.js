/************************************/
// Created By : Mostafa Abdelghffar
// Create Date : 16/6/2015 10:00 PM
// Description : This file contains all javaScript functions resopsable for editing listing properties
/************************************/

// global variables used in deleteItem function
var deleteWebServiceMethod = "ListingProperties.asmx/DeleteProperties";

// global variable used in row_click and update functions
var editWebServiceMethod = "ListingProperties.asmx/EditProperties";
var formAutoCodeControl = "lblPropertyId";

// enable pnl form for update
function setformforupdate() {
    try {
        var PropCode = $('#lblPropertyId').val();
        ListingProperties.IsHisPropery(PropCode,OnCheckSucess)
        
    } catch (err) {
        alert(err);
    }
}
//
function OnCheckSucess(val) {
    if (val["0"] == 0) {
        showErrorMessage(" You don't have permission to edit this property")
    }
    else {
        setformforupdate_all();
        $('#txtFacilites').attr('readonly', true);
        $('#txtUploadedFiles').attr('readonly', true);
        $("#lblFormMode").html("(Edit)");
        $(".btnMap").removeAttr("disabled");
    }
}
// called after update function success
function edit(val) {
    try {
        var data = JSON.parse(val[1]);
        cancel();
        if (val[0] != "0") {
            fillControlsFromJson(data[0]);
            FilterShareDropdown();
            $("#txtcurrentcommunity").val(data[0]["Community"]);
            $("#txtcurrentsubcommunity").val(data[0]["SubCommunity"]);
            $("#txtcurrentbuilding").val(data[0]["landMark"]);
            $("#lblSalesman").html(data[0]["SalesMan"]);
            getDllDataAfterEdit();
            calcsqmt();
           calComissionAmount();
           // fillFiles();
            getFacility();
            getPortals();
            filterEnquiry();
           // getContactDetails($("#OwnerAutoCodeNo"), "AutoCode");
           // getSalesmanDetails($("#txtSalesmanCode"), "Code");
            showSuccessMessage("Record Selected")
            $("#cmdSave").prop("CommandArgument", "Update");
            $("#cmdUpdate").removeAttr('disabled');
            $("#cmdDelete").removeAttr('disabled');
            $("#lblFormMode").html("(Show)");
            if (formOperation == "update") {
                setformforupdate();
                formOperation = "";

            }
           

        } else {
            showErrorMessage("No data found !!");
        }
    }
    catch (err) {
        alert(err);
    }
}

// get facilities from db
function getFacility() {
    ListingProperties.getFacility($("#ddltype option:selected").text(), function (data) {
        var facilities = JSON.parse(data);
        $("#pnlFacility").empty();
        $("#pnlFacility").append("<table>");
        if (facilities.length > 0) {
            for (i = 0; i < facilities.length; i++) {
                $("#pnlFacility").append("<tr><td>");
                $("#pnlFacility").append('<input type="checkbox" onclick="getFacilitiesNumber();" id=' + facilities[i].ID + ' value=' + facilities[i].Description + '>' + facilities[i].Description + '</input>');
                $("#pnlFacility").append("</td></tr>");
            }
        }
        $("#pnlFacility").append("</table>");
        fillFacilities();
    });
}

// get selected facilities from db and select them
function fillFacilities() {
    var count = 0;
    var autocode = $("#lblPropertyId").val();
    ListingProperties.GetPropertyFacilities(autocode, function (data) {
        var PropertyFacilities = JSON.parse(data);
        if (PropertyFacilities.length > 0) {
            for (i = 0; i < PropertyFacilities.length; i++) {
                $("#" + PropertyFacilities[i].Facility).prop("checked", true);
                count = count + 1;
            }
            getFacilitiesNumber();
        }
    });
}
// get portals from db
function getPortals() {
    ListingProperties.getPortals("", function (data) {
        var Portals = JSON.parse(data);
        $("#pnlPortals").empty();
        $("#pnlPortals").append("<table>");
        if (Portals.length > 0) {
            for (i = 0; i < Portals.length; i++) {
                $("#pnlPortals").append("<tr><td>");
                $("#pnlPortals").append('<input type="checkbox" onclick="getPortalsNumber();" id=' + Portals[i].ID + ' value=' + Portals[i].Description + '>' + Portals[i].Description + '</input>');
                $("#pnlPortals").append("</td></tr>");
            }
        }
        $("#pnlPortals").append("</table>");
        fillPortals();
    });
}
// get selected portals from db and select them
function fillPortals() {
    var count = 0;
    var autocode = $("#lblPropertyId").val();
    ListingProperties.GetPropertyPortals(autocode, function (data) {
        var PropertyPortals = JSON.parse(data);
        if (PropertyPortals.length > 0) {
            for (i = 0; i < PropertyPortals.length; i++) {
                $("#" + PropertyPortals[i].Facility).prop("checked", true);
                count = count + 1;
            }
            getPortalsNumber();
        }
    });
}


// fill photos uploaded drawings when click edit button
function fillFiles() {
    try {
        ListingProperties.getFiles($("#lblPropertyId").val(), function (data) {
            var files = JSON.parse(data);
            if (files.length > 0) {
                createTblUploadedFiles(["Index", "File", "Name", "File Type", "Category", "Show in xml ", "Main Image", "Water Mark", "Contanct Image", "Brochure Image", "WebSite Image", "Delete"]);
                for (i = 0; i < files.length; i++) {
                    appendFileTR(files[i], i);
                }
                $("#txtUploadedFiles").val(files.length);
            }
        });
    }
    catch (error) {
        alert(error);
        return false;
    }
}

// count number of selected facilities
function getFacilitiesNumber() {
    var count = 0;
    $("#pnlFacility input").each(function () {
        if ($(this).prop("checked")) {
            count = count + 1;
        }
    });
    $("#txtFacilites").val(count);
}

// fill ddls with data when get data for update
function getDllDataAfterEdit() {
    try {
        GetDataWithSelectid($("#ddlUnitCity").val(), 'ddlCommunity', 'UC', 'C', $("#txtcurrentcommunity").val());
        GetDataWithSelectid($("#txtcurrentcommunity").val(), 'ddlSubCommunity', 'US', 'UC', $("#txtcurrentsubcommunity").val());
        GetDataWithSelectid($("#txtcurrentsubcommunity").val(), 'ddlBuilding', 'BN', 'US', $("#txtcurrentbuilding").val());
    } catch (err) {
        alert(err);
    }
}

function filterEnquiry() {
    try {
        $("#tblFilteredEnquiry").remove();
        $("#FilteredPEnquiry").empty();
        var category = $("#ddlProkDetCategory").val();
        var purpose = $("#ddlPurpose").val();
        var city = $("#ddlUnitCity").val();
        var community = $("#ddlCommunity").val();
        var rooms = $("#ddlNumRooms").val();
        var bathrooms = $("#ddlBathRooms").val();
        var price = $("#txtProkDetPrice").val();
        var data = {
            "category": category, "purpose": purpose, "city": city, "community": community, "rooms": rooms, "bathrooms": bathrooms, "price": price
        }
        ListingProperties.GetFilterEnquiry(data, fillFilterdEnquiry);
    } catch (err) {
        alert(err);
    }
}
function fillFilterdEnquiry(data) {
    try {
        $("#FilteredPEnquiry").empty();
        var files = JSON.parse(data);
        if (files.length > 0) {
            createTable("tblFilteredEnquiry", "FilteredEnquiry", ["Enquiry Code", "Enquiry Date", "Contact", "City", "Community", "Bedrooms", "Bathrooms", "Price From", "Price To", "Agent"]);
            for (i = 0; i < files.length; i++) {
                appendFilteredEnquiryTR(files[i], i);
            }
            $("#txtFilterdEnquiry").val(files.length);
        }
    }
    catch (error) {
        $("#txtFilterdEnquiry").val(0);
    }
}
// draw Filtered Properties 
function appendFilteredEnquiryTR(fileDetails, i) {
    try {
        var index = $('#tblFilteredEnquiry tbody tr').length;
        var tr = document.createElement('tr');
        $(tr).append('<td><span id="lblCode" >' + fileDetails.Code + '</span></td>');
        $(tr).append('<td><span id="lblName" >' + fileDetails.EnquiryDate + '</span></td>');
        $(tr).append('<td><span id="lblContact" >' + fileDetails.contact + '</span></td>');
        $(tr).append('<td><span id="lblCity" >' + fileDetails.CityName + '</span></td>');
        $(tr).append('<td><span id="lblCommunity" >' + fileDetails.CommunityName + '</span></td>');
        $(tr).append('<td><span id="lblNumOfRoom" >' + fileDetails.NoOFRooms + '</span></td>');
        $(tr).append('<td><span id="lblNumOfBathRoom" >' + fileDetails.NoOfBathRooms + '</span></td>');
        $(tr).append('<td><span id="lblPriceFrom">' + fileDetails.PriceFrom + '</span></td>');
        $(tr).append('<td><span id="lblPriceTo">' + fileDetails.PriceTo + '</span></td>');
        $(tr).append('<td><span id="lblSalesmanName" >' + fileDetails.SalesmanName + '</span></td>');
        $('#tblFilteredEnquiry tbody').append(tr);
        //orderUploadedFilesIndex();
    } catch (err) {
        alert(err);
    }
}

// show filtered properties
function showFilterdEnquiry() {
    try {
        if ($("#txtFilterdEnquiry").val() != 0 && $("#txtFilterdEnquiry").val() != "") {
            $("#FilteredEnquiry").dialog({ modal: true, title: 'Related Enquiry', show: 'slide', width: 900 });
        }
    }
    catch (err) {
        alert(err);
    }
}

function showAssignAgentpnl() {
    var assignedItems = '';
    $('#tablelist tbody').find('tr').each(function () {
        var row = $(this);
        if (row.find('input[type="checkbox"]').is(':checked')) {
            var chk = row.find('input[type="checkbox"]');
            var n = chk.attr("id").indexOf("&");
            if (n != -1) {
                // hide the row of deledted property
                var autocode = chk.attr("id").split("&")[1];
                assignedItems = assignedItems + autocode + "|";
            }
        }
    });
    if (assignedItems != '') {
        resetAgentDetails();
        $("#pnlAssignAgent").dialog({ modal: true, title: 'Assign Agent', show: 'slide', width: 900 });
    } else {
        showErrorMessage("No records selected");
    }
}
function assignAgent() {
    var assignedItems = '';
    $('#tablelist tbody').find('tr').each(function () {
        var row = $(this);
        if (row.find('input[type="checkbox"]').is(':checked')) {
            var chk = row.find('input[type="checkbox"]');
            var n = chk.attr("id").indexOf("&");
            if (n != -1) {
                // hide the row of deledted property
                var autocode = chk.attr("id").split("&")[1];
                assignedItems = assignedItems + autocode + "|";
            }
        }
    });
    if (assignedItems != '') {
        if (assignedItems, $("#txtAgentCode").val() != '') {
            $("#lblAssignAgentResult").hide();
            ListingProperties.AssignAgent(assignedItems, $("#txtAgentCode").val(), function (val) {
                try {
                    if (val[0] == "1") {
                        showSuccessMessage("Assigned Successfully!");
                        $("#pnlAssignAgent").dialog("close");
                        cancel();
                        drawDynamicTable();
                    } else {
                        showErrorMessage("Failed To Assign");
                        $("#pnlAssignAgent").dialog("close");
                    }
                }
                catch (err) {
                    alert(err);
                }
            });
        } else {
            $("#lblAssignAgentResult").removeClass();
            $("#lblAssignAgentResult").addClass("res-label-error");
            $("#lblAssignAgentResult").html("please select agent");
            $("#lblAssignAgentResult").show();
        }
    } else {
        showErrorMessage("No records selected");
        $("#pnlAssignAgent").dialog("close");
    }
}
function printBrochure() {
    var code = $("#txtPropCode").val();
    var title = $("#txtPropName").val().split(' ').join('-');
    if (code != '' && title != '') {
        window.open("http://lscrm.blueberry.software/Property/" + title + "/" + code);
    } else {
        showErrorMessage("No records selected");
    }
}
function addEnquiry() {
    var code = $("#lblPropertyId").val();
    if (code != '') {
        window.open("http://lscrm.blueberry.software/Work_Module/Enquiry.aspx?operation=add&propertycode=" + code);
    } else {
        showErrorMessage("No records selected");
    }
}


