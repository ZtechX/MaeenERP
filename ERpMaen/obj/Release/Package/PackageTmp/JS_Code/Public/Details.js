/************************************/
// Created By : Ahmed  Nayl
// Create Date : 7/7/2015 04:40 PM
// Description : This file contains all javaScript functions for getting and reseting details of Contact and Property
/************************************/

//##########################  Contact Details  ##########################
// get details of selected contact
function getContactDetails(sender, colName) {
    try {
        var condition = {"ColName": colName, "Value": getControlValue(sender) };
        WebService.GetContactDetails(condition, function (contactDetails) {
            if (contactDetails != "") {
                var detailsJson = JSON.parse(contactDetails);
                $('#lblContactId').html(detailsJson[0].id);
                $('#txtContactFirstName').val(detailsJson[0].fname);
                $('#txtContactmiddleName').val(detailsJson[0].mname);
                $('#txtContactLastName').val(detailsJson[0].lname);
                $('#txtContactApproveNo').val(detailsJson[0].approve_no);
                $('#txtContactTel').val(detailsJson[0].tel);
                $('#btnContactReset').show();
                enableContactControls(false);
            }
        });
    } catch (err) {
        alert(err);
    }
}

// get details of selected contact
function getcompanyDetails(sender, colName) {
    try {
        var condition = { "ColName": colName, "Value": getControlValue(sender) };
        WebService.GetcompanyDetails(condition, function (companyDetails) {
            if (companyDetails != "") {
                var detailsJson = JSON.parse(companyDetails);
                $('#lblcompanyId').html(detailsJson[0].id);
                $('#txtcompanyFirstName').val(detailsJson[0].name);
                $('#txtcompanytel').val(detailsJson[0].tel);
                $('#btncompanyReset').show();
                enablecompanyControls(false);
            }
        });
    } catch (err) {
        alert(err);
    }
}

// reset contact controls
function resetContactDetails() {
    try {
        $('#lblContactId').html("");
        $('#lblContactCode').html("");
        $("#txtContactFirstName").val("");
        $("#txtContactLastName").val("");
        $('#txtContactMobile').val("");
        $('#txtContactEmail').val("");
        $('#txtContactPhone').val("");
        $('#txtContactFax').val("");
        enableContactControls(true);
        $('#btnContactReset').hide();
    } catch (err) {
        alert(err);
    }
}

// reset contact controls
function resetcompanyDetails() {
    try {
        $('#lblcompanyId').html("");
        $("#txtcompanyFirstName").val("");
        $("#txtcompanytel").val("");
        enablecompanyControls(true);
        $('#btncompanyReset').hide();
    } catch (err) {
        alert(err);
    }
}

// enable or disable contact controls
function enableContactControls(b) {
    try {
        $('#txtContactFirstName').prop("disabled", !b);
        $('#txtContactLastName').prop("disabled", !b);
        $('#txtContactmiddleName').prop("disabled", !b);
        $('#txtContactEmail').prop("disabled", !b);
        $('#txtContactApproveNo').prop("disabled", !b);
        $('#txtContactTel').prop("disabled", !b);
    } catch (err) {
        alert(err);
    }
}

// enable or disable contact controls
function enablecompanyControls(b) {
    try {
        $('#txtcompanyFirstName').prop("disabled", !b);
        $('#txtcompanytel').prop("disabled", !b);
    } catch (err) {
        alert(err);
    }
}

// set contact type for contact user control
function setContactTypeForUserControl(contactType) {
    try {
        $("#lblContactType").html(contactType);
        setContactContextKey(contactType);
    } catch (err) {
        alert(err);
    }
}

// set contact type as context key of autocomplete controls
function setContactContextKey(contactType) {
    try {
        $find("aceContactFirstName").set_contextKey(contactType);
        $find("aceContactLastName").set_contextKey(contactType);
        $find("aceContactMobile").set_contextKey(contactType);
        $find("aceContactEmail").set_contextKey(contactType);
        $find("aceContactPhone").set_contextKey(contactType);
        $find("aceContactFax").set_contextKey(contactType);
    } catch (err) {
        alert(err);
    }
}

// save new contact to db
function saveNewContact() {
    try {
        if (Page_ClientValidate("vgNewContact")) {
            var basicData = generateJSONFromControls("divNewContact");
            WebService.SaveNewContact(basicData, function (val) {
                if (val == "True") {
                    cancelAddNewContact();
                    showSuccessMessage("تمت اضافة العميل بنجاح");
                } else {
                    alert(val);
                }
            });
        }
    } catch (err) {
        alert(err);
    }
}


// save new contact to db
function saveNewcompany() {
    try {
        if (Page_ClientValidate("vgNewcompany")) {
            var basicData = generateJSONFromControls("divNewcompany");
            WebService.SaveNewcompany(basicData, function (val) {
                if (val == "True") {
                    cancelAddNewcompany();
                    showSuccessMessage("تم اضافة الشركة بنجاح");
                } else {
                    alert(val);
                }
            });
        }
    } catch (err) {
        alert(err);
    }
}

// cancel adding new contact
function cancelAddNewContact() {
    try {
        Page_ClientValidate('');
        resetDivControls("divNewContact");
        $("#lblContactErrorRes").hide();
        $('#divNewContact').hide();
    } catch (err) {
        alert(err);
    }
}

// cancel adding new contact
function cancelAddNewcompany() {
    try {
        Page_ClientValidate('');
        resetDivControls("divNewcompany");
        $("#lblcompanyErrorRes").hide();
        $('#divNewcompany').hide();
    } catch (err) {
        alert(err);
    }
}

// get contact full name for correspondence form
function getContactName(sender, colName) {
    try {
        var condition = { "ColName": colName, "Value": getControlValue(sender) };
        WebService.GetContactName(condition, function (ContactDetails) {
            if (ContactDetails != "") {

                //coresspndance form
                var detailsJson = JSON.parse(ContactDetails);
                $("#txtFullName").val(detailsJson[0]["FullName"]);
                $("#lblContId").html(detailsJson[0]["Id"]);
                // leads form
                $("#txtContactName").val(detailsJson[0]["FullName"]);
                $("#lblLeadContactId").html(detailsJson[0]["Id"]);
                setSelectedDdlOptionByText('ddlContactType', detailsJson[0]["Type"])

                //Listing Forms
                $("#txtProkDetOwnerName").val(detailsJson[0]["FullName"]);
                $("#lblLandlordId").html(detailsJson[0]["Id"]);

            }
        });
    } catch (err) {
        alert(err);
    }
}

// get contact full name for deal form
function getDealContactName(sender, colName) {
    try {
        var condition = { "ColName": colName, "Value": getControlValue(sender) };
        WebService.GetContactName(condition, function (ContactDetails) {
            if (ContactDetails != "") {
                var detailsJson = JSON.parse(ContactDetails);
                if (detailsJson[0]["Type"] == "Landlord") {

                    $("#txtLandlordFullName").val(detailsJson[0]["FullName"]);
                    setSelectedDdlOptionByText("ddlDealType", "Rent");
                    $("#lblLandlordId").html(detailsJson[0]["Id"]);

                } else if (detailsJson[0]["Type"] == "Tenant") {

                    $("#txtTenantFullName").val(detailsJson[0]["FullName"]);
                    setSelectedDdlOptionByText("ddlDealType", "Rent");
                    $("#lblTenantId").html(detailsJson[0]["Id"]);

                } else if (detailsJson[0]["Type"] == "Buyer") {

                    $("#txtBuyerFullName").val(detailsJson[0]["FullName"]);
                    setSelectedDdlOptionByText("ddlDealType", "Sale");
                    $("#lblBuyerId").html(detailsJson[0]["Id"]);

                } else {

                    $("#txtSellerFullName").val(detailsJson[0]["FullName"]);
                    setSelectedDdlOptionByText("ddlDealType", "Sale");
                    $("#lblSellerId").html(detailsJson[0]["Id"]);

                }
                showHideLandlordTenant();
            }
        });
    } catch (err) {
        alert(err);
    }
}

//##########################  Property Details  ##########################
// get property details based on selected property
function getPropertyDetails(sender, colName) {
    try {
        var condition = { "ColName": colName, "Value": getControlValue(sender) };
        WebService.GetPropertyDetails(condition, function (propertyDetails) {
            if (propertyDetails != "") {
                var detailsJson = JSON.parse(propertyDetails);
                $('#lblListingID').html(detailsJson[0].Id);
                $('#txtPropCode').val(detailsJson[0].Code);
                $('#txtPropCategory').val(detailsJson[0].Category);
                $('#txtBeds').val(detailsJson[0].Beds);
                $("#txtCommunity").val(detailsJson[0]["Community"]);
                $("#txtSubCommunity").val(detailsJson[0]["SubCommunity"]);
                $('#ddlProkDetCategory').val(detailsJson[0].CategoryId);
                $('#ddlUnitType').val(detailsJson[0].UnitType);
                $('#ddlUnitCity').val(detailsJson[0].City);
                $("#txtcurrentcommunity").val(detailsJson[0]["CommunityId"]);
                $("#txtcurrentsubcommunity").val(detailsJson[0]["SubCommunityId"]);
                $("#txtcurrentBuildingName").val(detailsJson[0]["BuildingNameId"]);
                $('#ddlMinBeds').val(detailsJson[0].Beds);
                $('#ddlMaxBeds').val(detailsJson[0].Beds);
                $('#txtMinPrice').val(detailsJson[0].Price);
                $('#txtMaxPrice').val(detailsJson[0].Price);
                $('#txtMinArea').val(detailsJson[0].BUA);
                $('#txtMaxArea').val(detailsJson[0].BUA);
                $('#btnPropertyReset').show();
                enablePropertyControls(false);
                getDllDataAfterEdit();
            }
            else {
                $('#lblListingID').html('');
            }
        });
    } catch (err) {
        alert(err);
    }
}

// get property owner
function getPropertyOwner(sender, colName) {
    try {
        var condition = { "ColName": colName, "Value": getControlValue(sender) };
        WebService.GetPropertyOwner(condition, function (propertyDetails) {
            if (propertyDetails != "") {
                var detailsJson = JSON.parse(propertyDetails);
                $("#txtOwner").val(detailsJson[0]["ContactName"]);
            }
            else {
                $('#lblListingID').html('');
            }
        });
    } catch (err) {
        alert(err);
    }
}

// Get Deal Id OR Deal Code
function getDealDetails(sender, colName) {
    try {
        var condition = { "ColName": colName, "Value": getControlValue(sender) };
        WebService.GetDealDetails(condition, function (DealsDetails) {
            if (DealsDetails != "") {
                var detailsJson = JSON.parse(DealsDetails);
                $('#lblDealId').html(detailsJson[0].Id);
                $('#txtDealCode').val(detailsJson[0].Code);
            }
            else {
                $('#lblDealId').html('');
            }
           
        });
    } catch (err) {
        alert(err);
    }
}

// Get Deal Property Details and contacts details
function getDealPropertyAndContactDetails(sender, colName) {
    try {
        var condition = { "ColName": colName, "Value": getControlValue(sender) };
        WebService.GetDealPropertyAndContact(condition, function (DealsDetails) {
            if (DealsDetails != "") {
                var detailsJson = JSON.parse(DealsDetails);
                $("#txtOwner").val(detailsJson[0]["OwnerName"]);
                $("#txtClient1Name").val(detailsJson[0]["TenantName"]);
                $('#lblListingID').html(detailsJson[0]["PropId"]);
                $('#txtPropCode').val(detailsJson[0]["PropCode"]);
                $("#txtCommunity").val(detailsJson[0]["Community"]);
                $("#txtSubCommunity").val(detailsJson[0]["SubCommunity"]);
                $('#txtPropCategory').val(detailsJson[0].Category);
            }
            else {
                $('#lblDealId').html('');
            }

        });
    } catch (err) {
        alert(err);
    }
}

// Get Lead Id OR Deal Code
function getLeadDetails(sender, colName) {
    try {
        var condition = { "ColName": colName, "Value": getControlValue(sender) };
        WebService.GetLeadDetails(condition, function (LeadDetails) {
            if (LeadDetails != "") {
                var detailsJson = JSON.parse(LeadDetails);
                $('#lblLeadId').html(detailsJson[0].Id);
                $('#txtLeadCode').val(detailsJson[0].Code);
            }
            else {
                $('#lblLeadID').html('');
            }

        });
    } catch (err) {
        alert(err);
    }
}

//##########################  Property Details  ##########################
// get property details for enquiry from 
function getEnquiryPropertyDetails(sender, colName) {
    try {
        var condition = { "ColName": colName, "Value": getControlValue(sender) };
        WebService.GetEnquiryPropertyDetails(condition, function (propertyDetails) {
            var detailsJson = JSON.parse(propertyDetails);
            $('#ddlUnitCity').val(detailsJson[0].City);
            $("#txtcurrentcommunity").val(detailsJson[0]["Community"]);
            getDllDataAfterEdit();
            $('#ddlNumRooms').val(detailsJson[0].Unit);
            $('#ddlBathRooms').val(detailsJson[0].NumOfRoom);
            $('#txtProkDetPriceFrom').val(detailsJson[0].SellOrLeasePrice); 
            $('#txtProkDetPriceTo').val(detailsJson[0].SellOrLeasePrice);
            $('#lblPropertyCode').html(detailsJson[0].AutoCode);
        });
    } catch (err) {
        alert(err);
    }
}

function getLeadsPropertyDetails(AutoCode) {
    try {
        WebService.EditLeadProperties(AutoCode, function (propertyDetails) {
            var detailsJson = JSON.parse(propertyDetails);
            $('#ddlUnitCity').val(detailsJson[0].City);
            $("#txtcurrentcommunity").val(detailsJson[0]["Community"]);
            getDllDataAfterEdit();
            $('#ddlNumRooms').val(detailsJson[0].Unit);
            $('#ddlBathRooms').val(detailsJson[0].NumOfRoom);
            $('#txtProkDetPriceFrom').val(detailsJson[0].SellOrLeasePrice);
            $('#txtProkDetPriceTo').val(detailsJson[0].SellOrLeasePrice);
            $('#txtPropName').val(detailsJson[0].Name);
            $('#lblPropertyCode').html(detailsJson[0].AutoCode);
            setformforupdate_all();
        });
    } catch (err) {
        alert(err);
    }
}

// get property details based on selected property
function getLeadPropertDetails(sender, colName) {
    try {
        var condition = { "ColName": colName, "Value": getControlValue(sender) };
        WebService.GetLeadDetails(condition, function (propertyDetails) {
            var detailsJson = JSON.parse(propertyDetails);
            $('#lblUnitAutoCode').html(detailsJson[0].AutoCode);
            $('#txtUnitCodeSubCommuniuty').val(detailsJson[0].Code + "|" + detailsJson[0].CodeSubCommunity);
            $('#txtUnitTitle').val(detailsJson[0].Name);
            $('#btnPropertyReset').show();
            enablePropertyControls(false);
        });
    } catch (err) {
        alert(err);
    }
}

// reset property controls
function resetPropertyDetails() {
    try {
        $('#lblUnitAutoCode').html('');
        $('#txtUnitCodeSubCommuniuty').val('');
        $('#txtUnitTitle').val('');
        enablePropertyControls(true);
        $('#btnPropertyReset').hide();
    } catch (err) {
        alert(err);
    }
}

// enable or disable property controls
function enablePropertyControls(b) {
    try {
        $('#txtUnitCodeSubCommuniuty').prop("disabled", !b);
        $('#txtUnitTitle').prop("disabled", !b);
    } catch (err) {
        alert(err);
    }
}

// get login agent details
function getAgentDetails() {
    try {

        WebService.getAgentDetails("", function (agentDetails) {
            var detailsJson = JSON.parse(agentDetails);
            $('#txtSalesmanCode').val(detailsJson[0].Code);
            $('#lblSalesman').html(detailsJson[0].Code);
            $('#txtSalesmanName').val(detailsJson[0].Name);
            $('#btnSalesmanReset').show();
        });
    } catch (err) {
        alert(err);
    }
}

// reset salesman controls
function resetAgentDetails() {
    try {
        $('#txtAgentCode').val('');
        $('#txtAgentName').val('');
        enableAgentControls(true);
        $('#btnAgentReset').hide();
        $('#btnAssignAgent').hide();
        $('#lblAgent').html('');
    } catch (err) {
        alert(err);
    }
}

//function to set additional data to ACL 
function SetCustomerContextKey(colName, ACLId) {
    var customerType = $("#CustomerType").html();
    var value = customerType + "|" + colName;
    $find(ACLId).set_contextKey(value);
}

//##########################  Salesman Details  ##########################
// get salesman details based on selected salesman
function getSalesmanAllDetails(sender, colName) {
    try {
       
        var condition = { "ColName": colName, "Value": getControlValue(sender) };
        WebService.GetSalesmanDetails(condition, function (salesmanDetails) {
            var detailsJson = JSON.parse(salesmanDetails);
            $('#txtSalesmanCode').val(detailsJson[0].Code);
            $('#lblSalesman').html(detailsJson[0].Code);
            $('#txtSalesmanName').val(detailsJson[0].Name);
            $('#txtFirstName').val(detailsJson[0].Name);
            $('#txtLastName').val(detailsJson[0].Name);
        });
    } catch (err) {
        alert(err);
    }
}