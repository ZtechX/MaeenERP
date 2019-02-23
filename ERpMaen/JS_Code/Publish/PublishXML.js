/************************************/
// Created By : Hamada Mohamed
// Create Date : 29/6/2015 10:30 AM
// Description : This file contains all javaScript functions related to Publish form 
/************************************/

// load function set defualt values
$(function () {
    try {
        //drawPublicListing();
    }
    catch (err) {
        alert(err);
    }
});
// Show Websites
function PublishProp() {
    try {
        var r = confirm("are you sure you want to Publish the selected records");
        if (r == true) {
            PublishXML.CheckPublishApprove(onSuccessCheckPublish)
        }
    }
    catch (err) {
        alert(err);
    }
}
function ClearXML(val) {
    alert(val)
}

// Show web site for unPublish

function UnPublishProp() {
    try {
        var r = confirm("are you sure you want to UnPublish the selected records");
        if (r == true) {
            PublishXML.CheckPublishApprove(onSuccessCheckUnPublish)
        }
    }
    catch (err) {
        alert(err);
    }
}
// Publish  item
function PublishItems() {
    try {
        var i = 0;
        var r = confirm("are you sure you want to Publish the selected records");
        if (r == true) {
            var PublishItems = '';
            $('#tablelist tr').each(function () {
                if ($(this).find('input[type="checkbox"]').is(':checked')) {
                    var chk = $(this).find('input[type="checkbox"]');
                    var n = $(chk).prop("id").indexOf("&");
                    if (n != -1) {
                        var autocode = $(chk).prop("id").split("&")[1];
                        PublishItems = PublishItems + autocode + "|";
                    }
                }
            });
            if (PublishItems!=''){
                PublishXML.PublishItems(PublishItems, onSuccessPublish);
            }
            else {
                showErrorMessage("No records selected");
            }
        }
    }
    catch (err) {
        alert(err);
    }
}

//Reject Publish Itmes
function RejectPublish() {
    try {
        var i = 0;
        var r = confirm("are you sure you want to Reject Publish the records");
        if (r == true) {
            var PublishItems = '';
            $('#tablelist tr').each(function () {
                if ($(this).find('input[type="checkbox"]').is(':checked')) {
                    var chk = $(this).find('input[type="checkbox"]');
                    var n = $(chk).prop("id").indexOf("&");
                    if (n != -1) {
                        var autocode = $(chk).prop("id").split("&")[1];
                        PublishItems = PublishItems + autocode + "|";
                    }
                }
            });
            if (PublishItems != '') {
                PublishXML.RejectPublishItems(PublishItems, onSuccessRejectPublish);
            }
            else {
                showErrorMessage("No records selected");
            }
        }
    }
    catch (err) {
        alert(err);
    }
}

//Unblish Items
function UnPublishItems() {
    try {
        var i = 0;
        var r = confirm("are you sure you want to unPublish the Selected  records");
        if (r == true) {
            var PublishItems = '';
            $('#tablelist tr').each(function () {
                if ($(this).find('input[type="checkbox"]').is(':checked')) {
                    var chk = $(this).find('input[type="checkbox"]');
                    var n = $(chk).prop("id").indexOf("&");
                    if (n != -1) {
                        var autocode = $(chk).prop("id").split("&")[1];
                        PublishItems = PublishItems + autocode + "|";
                    }
                }
            });
            if (PublishItems != '') {
                PublishXML.UnPublishItems(PublishItems, onSuccessUnPublish);
            }
            else {
                showErrorMessage("No records selected");
            }
        }
    }
    catch (err) {
        alert(err);
    }
}

// Reject UnpublishItmes
function RejectUnPublish() {
    try {
        var i = 0;
        var r = confirm("are you sure you want to Reject UnPublish the records");
        if (r == true) {
            var PublishItems = '';
            $('#tablelist tr').each(function () {
                if ($(this).find('input[type="checkbox"]').is(':checked')) {
                    var chk = $(this).find('input[type="checkbox"]');
                    var n = $(chk).prop("id").indexOf("&");
                    if (n != -1) {
                        var autocode = $(chk).prop("id").split("&")[1];
                        PublishItems = PublishItems + autocode + "|";
                    }
                }
            });
            if (PublishItems != '') {
                PublishXML.RejectUnPublishItems(PublishItems, onSuccessRejectPublish);
            }
            else {
                showErrorMessage("No records selected");
            }
        }
    }
    catch (err) {
        alert(err);
    }
}
//called after reject unPublish
function onSuccessRejectPublish(val) {
    if (val["0"] == false) {
        showErrorMessage("failed");
    }
    else {
        showSuccessMessage("Done  successfully");
        drawDynamicTable();
    }
}

// called after delete function success
function onSuccessPublish(val) {
    ///////// if deleted success ////////
    try{

        if (val[0] == "1") {
            //location.reload();
            // form_load();
           // $("#pnlWebSites").hide()
            showSuccessMessage("Published  successfully");
            drawDynamicTable();
        }
            ///////// if  failed  ////////
        else if (val["0"] == "0") {
            showErrorMessage("Published Failed")
        }
        else {
            showErrorMessage("Published Failed for Properties "+ val+ " becuase missing Required fields")
        }
    }
    catch (err) {
         alert(err)
    }
}

//onSuccessUnPublish
function onSuccessUnPublish(val) {
    ///////// if  success ////////
    if (val[0] != "0") {
        showSuccessMessage("UnPublished  successfully");
        drawDynamicTable();
    }
        ///////// if  failed  ////////
    else {
        showErrorMessage("UnPublished Failed")
    }
}

function onSuccessCheckPublish(val) {
    ///////// if need Approval ////////
    try{
        var PublishItems = '';
        $('#tablelist tr').each(function () {
            if ($(this).find('input[type="checkbox"]').is(':checked')) {
                var chk = $(this).find('input[type="checkbox"]');
                var n = $(chk).prop("id").indexOf("&");
                if (n != -1) {
                    // hide the row of deledted property
                    var autocode = $(chk).prop("id").split("&")[1];
                    PublishItems = PublishItems + autocode + "|";
                    $(chk).prop("checked", false);
                }
            }
        });
        if (val[0] != "0") {
            if (PublishItems != '') {
                PublishXML.RequestPublish(PublishItems)
                drawDynamicTable();
                showSuccessMessage("Publish Requested Successfully")
            }
            else {
                showErrorMessage("No records selected");
            }
       
        }
        else {
            if (PublishItems != '') {
                PublishXML.PublishItems(PublishItems, onSuccessPublish);
            }
            else {
                showErrorMessage("No records selected");
            }
        }
    }
    catch (err) {
        alert(err)
    }
}

function onSuccessCheckUnPublish(val) {
    var PublishItems = '';
    $('#tablelist tr').each(function () {
        if ($(this).find('input[type="checkbox"]').is(':checked')) {
            var chk = $(this).find('input[type="checkbox"]');
            var n = $(chk).prop("id").indexOf("&");
            if (n != -1) {
                // hide the row of deledted property
                var autocode = $(chk).prop("id").split("&")[1];
                PublishItems = PublishItems + autocode + "|";
                $(chk).prop("checked", false);
            }
        }
    });
    if (val[0] != "0") {
        //////// if need Approval ////////
        if (PublishItems != '') {
            PublishXML.RequestUnPublish(PublishItems)
            drawDynamicTable();
            showSuccessMessage("UnPublish Requested successfully");
        }
        else {
            showErrorMessage("No records selected");
        }
    }
    else {
        if (PublishItems != '') {
            PublishXML.UnPublishItems(PublishItems, onSuccessUnPublish);
        }
        else {
            showErrorMessage("No records selected");
        }
    }
}