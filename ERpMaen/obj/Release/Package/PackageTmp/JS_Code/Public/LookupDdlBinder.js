/************************************/
// Created By : Mostafa Abdelghffar
// Create Date : 16/6/2015 10:00 PM
// Description : This file contains all javaScript functions resopsable for binding lookup ddl
/************************************/

// get options  of drobdownlist from db by calling weservice function ListingProperties.GetData filtered by  value of other control
function GetData(Value, nextcontrolid, type, relatedtype) {
    try {
        var data = Value + "|" + nextcontrolid + "|" + type + "|" + relatedtype;
        LookupDdlBinder.GetData(data, SetData);
    } catch (err) {
        alert(err);
    }
}

// get options of building related to community for building only
function Getcupbord() {
    try {
        var data = $("#ddlstore_id").val() + "|ddlpbord_id";
        LookupDdlBinder.Getcupbord(data, SetData);
    } catch (err) {
        alert(err);
    }
}

function Getitems() {
    try {
       // var data = $("#ddlcategory_id").val();
        var data = $("#ddlcategory_id").val() + "|ddlitem_id";
    //    alert(data);
        LookupDdlBinder.Getitems_id(data, SetData);
    } catch (err) {
        alert(err);
    }
}


// get options of building related to community for building only
function GetReck() {
    try {
        var data = $("#ddlpbord_id").val() + "|ddlrack_id";
        LookupDdlBinder.GetReck(data, SetData);
    } catch (err) {
        alert(err);
    }
}




// copy of GetData function but will select is of ddl
function GetDataWithSelectid(Value, nextcontrolid, type, relatedtype, selectedval) {
    try {
        var data = Value + "|" + nextcontrolid + "|" + type + "|" + relatedtype + "|" + selectedval;
        LookupDdlBinder.GetDataWithSelectid(data, SetDataWithSelectid);
    } catch (err) {
        alert(err);
    }
}

// copy of GetData function but will select is of ddl
function GetDataWithSelectidForBuilding(Community, subCommunity,nextcontrolid, selectedval) {
    try {
        var data = Community + "|" + subCommunity + "|" + nextcontrolid +"|" +selectedval;
        LookupDdlBinder.GetDataWithSelectidForBuilding(data, SetDataWithSelectid);
    } catch (err) {
        alert(err);
    }
}

// set  options  to  drobdownlist with selested id
function SetDataWithSelectid(val) {
    try {
        controlid = val[0].split("|")[0];
        selectedid = val[0].split("|")[1];
        $("#" + controlid).empty();
        if (currentLanguage == "Arabic") {
            $("#" + controlid).append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
        } else {
            $("#" + controlid).append($("<option selected='selected'></option>").val("0").html("-- Select -- "));
        }
        for (var i = 1; i <= val.length - 1; i++) {
            if (val[i].split("|")[1] == selectedid) {
                $("#" + controlid).append($("<option selected='selected'></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
            }
            else {

                $("#" + controlid).append($("<option></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
            }
        }
    } catch (err) {
        alert(err);
    }
}

// set  options  to  drobdownlist
function SetData(val) {
    try {
       // alert(controlid);
        controlid = val[0];
        $("#" + controlid).empty();
            $("#" + controlid).append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
        for (var i = 1; i <= val.length - 1; i++) {
            $("#" + controlid).append($("<option ></option>").val(val[i].split("|")[1]).html(val[i].split("|")[0]));
        }
        $("#" + controlid).change();
    }
    catch (err) {
        controlid = val[0];
        $("#" + controlid).empty();
            $("#" + controlid).append($("<option selected='selected'></option>").val("0").html("-- اختر -- "));
        $("#" + controlid).change();
    }
}

// bind options to drobdownlist from lookup table and select value 
function bindDdlWithSelectedValue(ddlID, lookupType, val) {
    try {
        LookupDdlBinder.GetLookupData(lookupType, function (options) {
            setDdlOptionsWithSelectedValue(ddlID, options, val);
        });
    } catch (err) {
        alert(err);
    }
}

// set options to drobdownlist and select value 
function setDdlOptionsWithSelectedValue(ddlID, options, val) {
    try {
        $('#' + ddlID).find('option').remove();
        if (currentLanguage == "Arabic") {
            $('#' + ddlID).append($("<option></option>").val("0").html("-- اختر -- "));
        } else {
            $('#' + ddlID).append($("<option></option>").val("0").html("-- Select -- "));
        }
        var optionsJSON = JSON.parse(options);
        for (var i = 0; i <= optionsJSON.length - 1; i++) {
            if (optionsJSON[i].ID == val) {
                $('#' + ddlID).append($("<option selected='selected'></option>")
                .val(optionsJSON[i].ID)
                .html(optionsJSON[i].Description));
            }
            else {
                $('#' + ddlID).append($("<option></option>")
                .val(optionsJSON[i].ID)
                .html(optionsJSON[i].Description));
            }
        }
    } catch (err) {
        if (currentLanguage == "Arabic") {
            $('#' + ddlID).append($("<option></option>").val("0").html("-- اختر -- "));
        } else {
            $('#' + ddlID).append($("<option></option>").val("0").html("-- Select -- "));
        }
    }
}

// select subcommunity when selet building
function getSubCommunity(building) {
    try {
        if ((building) != "0") {
            LookupDdlBinder.GetSubCommunity(building, function (subCommmunity) {
                $("#ddlSubCommunity").val(subCommmunity);
            });
        }
    } catch (err) {
        alert(err);
    }
}