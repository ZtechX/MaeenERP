/************************************/
// Created By : Mostafa Abdelghffar
// Create Date : 23/5/2015 9:30 AM
// Description : This file contains all javaScript functions in Map form
/************************************/
// define global variables
var coordinates = [];
var polygons = [];
var map; var infowindow;
var markers = [];
var passed_x_coordinate = "", passed_y_coordinate = "", passed_prop_code = "";

$(document).ready(function () {
    try {
        $('.carousel').carousel({
            interval: 2000
        });

        checkMapFormURL();
        checkLanguage();
    } catch (err) {
        alert(err);
    }
});

// check opration of form when load
function checkMapFormURL() {
    try {
        var par = getUrlVars();
        if (jQuery.isEmptyObject(par) == false) {
            if (par.hasOwnProperty('purpose')) {
                var purpose = par.purpose;
                if (purpose == "rent") {
                    purpose = "Rent";
                } else if (purpose == "sale") {
                    purpose = "Sale";
                } else if (purpose = "short+term") {
                    purpose = "Short Term";
                }
                setSelectedDdlOptionByText("ddlPurpose", purpose);
                initialize();
            } else if (par.hasOwnProperty('propertycode')) {
                var propCode = par.propertycode;
                WebService.GetMapPropertyDdlValues(propCode, setDdlValuesBasedOnPassedPropCode);
            }
        } else {
            initialize();
        }
    } catch (err) {
        alert(err);
    }
}

// set ddl Values based on passed propCode
function setDdlValuesBasedOnPassedPropCode(ddlValues) {
    try {
        if (ddlValues != "") {
            var valuesJSON = JSON.parse(ddlValues);
            if (getValueOrEmpty(valuesJSON[0].Purpose) != "") {
                $("#ddlPurpose").val(valuesJSON[0].Purpose);
            }
            if (getValueOrEmpty(valuesJSON[0].City) != "") {
                $("#ddlCity").val(valuesJSON[0].City);
                if (getValueOrEmpty(valuesJSON[0].Community) != "") {
                    GetDataWithSelectid($("#ddlCity").val(), 'ddlCommunity', 'UC', 'C', valuesJSON[0].Community);
                } else {
                    var ddlControls = ["ddlCommunity"]; resetDll(ddlControls);
                }
            }
            if (getValueOrEmpty(valuesJSON[0].UnitType) != "") {
                $("#ddlType").val(valuesJSON[0].UnitType);
            }
            if (getValueOrEmpty(valuesJSON[0].Category) != "") {
                $("#ddlCategory").val(valuesJSON[0].Category);
            }
            passed_x_coordinate = valuesJSON[0].GeoPoint1;
            passed_y_coordinate = valuesJSON[0].GeoPoint2;
            passed_prop_code = valuesJSON[0].Code;
            initialize();
        }
    } catch (err) {
        alert(err);
    }
}

// initialize map with default zoom and center points
function initialize() {
    try {
        //var latlng = new google.maps.LatLng();
        var mapOptions =
        {
            zoom: 14,
            // The below line is equivalent to writing:
            // center: new google.maps.LatLng(25.2048, 55.2708)
            center: { lat: 25.2048, lng: 55.2708 }
        };

        //Create a Google map.
        map = new google.maps.Map(document.getElementById('map'), mapOptions);

        getPropertyBySearch();
    } catch (err) {
        alert(err);
    }   
}

// center map based on search values
function getPropertyBySearch() {
    try {
        var values = $("#ddlCity").val() + "|" + $("#ddlCommunity").val() + "|" + $("#ddlType").val() + "|" + $("#ddlCategory").val() + "|" + $("#ddlPurpose").val();
        WebService.GetPropertiesBySearch(values, function (points) {
            clearMarkers();
            if (points != "") {
                var pointsArrJson = JSON.parse(points);
                markIcons(pointsArrJson);
                if (getValueOrEmpty(passed_x_coordinate) != "" && getValueOrEmpty(passed_y_coordinate) != "") {
                    map.setCenter(new google.maps.LatLng(passed_x_coordinate, passed_y_coordinate));
                    passed_x_coordinate = ""; passed_y_coordinate = "";
                } else {
                    map.setCenter(new google.maps.LatLng(pointsArrJson[0].GeoPoint1, pointsArrJson[0].GeoPoint2));
                }
            } else {
                alert("No Property Found");
            }
        });
    } catch (err) {
        alert(err);
    }
}

// Sets the map on all markers in the array.
function setAllMap(map) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}

// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
    setAllMap(null);
}

// draw marker for each property
function markIcons(pointsArrJson) {
    try {
        infowindow = new google.maps.InfoWindow();
        for (var i = 0; i <= pointsArrJson.length - 1; i++) {
            var marker = new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(pointsArrJson[i].GeoPoint1, pointsArrJson[i].GeoPoint2),
                icon: getMarkerIcon(pointsArrJson[i].Code, pointsArrJson[i].CategoryName)
            });
            markers.push(marker);
            showPropDetails(marker, infowindow, pointsArrJson[i].Id);
        }
    } catch (err) {
        alert(err);
    }
}

// show property details when click on the marker (icon of each property)
function showPropDetails(marker, infowindow, propId) {
    try {
        google.maps.event.addListener(marker, 'mouseover', function () {
            infowindow.open(map, marker);
            infowindow.setContent("<p>Loading ...</p>");
            WebService.GetMapPropertyDetails(propId, function (details) {
                contentHTMLString = getContentHTMLString(details);
                infowindow.setContent(contentHTMLString);
            });
        });

        google.maps.event.addListener(marker, 'click', function () {
            infowindow.close();
        });
    } catch (err) {
        alert(err);
    }
}

// get HTML string based on property details
function getContentHTMLString(propDetails) {
    try {
        var contentHTMLString = '<div class="container-fluid">' +
            '<div class="max-width"><h4 class="popover_title">' + "<a target='_blank' href='http://always.linkinsoft.com/property/" + propDetails[1] + "/" + propDetails[0] + "' >" + propDetails[1] + '</a></h4>';
        if (propDetails[3] != '') {
            contentHTMLString += '<small class="popover_property_det">' + parseFloat(propDetails[3]).toFixed(2) + 'AED</small>';
        }
        //contentHTMLString += '' + '<br>' +
        //    'City: ' + propDetails[4] + '<br>' +
        //    'Community: ' + propDetails[5] + '<br>' +
        //    'Type: ' + propDetails[6] + '<br>' +
        //    'Category: ' + propDetails[7] + '<br>' +
        //    'Purpose: ' + propDetails[8] + '</div><br>' +
        //    '<span style="display:none" id="lblPropCode">' + propDetails[0] + '</span>' +
        //    '<span style="display:none" id="lblPropTitle">' + propDetails[1] + '</span>' +
        //    '<span style="display:none" id="lblPropCustomerImagesCount">' + propDetails[9] + '</span>' +
        //    '<div id="carousel-example-generic" class="carousel slide" data-interval="3000">' +
        //    '<div class="carousel-inner">';
        contentHTMLString += '<br>' +'Address:' + propDetails[4] + '/' + '' + propDetails[5]
                           + '<br>' +'Type:' + propDetails[6] + '/' +'' + propDetails[7]
                           + '<br>' +'Purpose: ' + propDetails[8] + '</div><br>' +
                           '<span style="display:none" id="lblPropCode">' + propDetails[0] + '</span>' +
                           '<span style="display:none" id="lblPropTitle">' + propDetails[1] + '</span>' +
                           '<span style="display:none" id="lblPropCustomerImagesCount">' + propDetails[9] + '</span>' +
                           '<div id="carousel-example-generic" class="carousel slide" data-interval="3000">' +
                           '<div class="carousel-inner">';

        if (propDetails.length > 10) {
            for (var i = 10; i < propDetails.length; i++) {
                var className;
                if (i == 10) {
                    className = 'item active';
                } else {
                    className = 'item';
                }
                contentHTMLString +=
                    '<div class="' + className + '">' +
                    '<img class="popover_image"  src="' + propDetails[i].replace("http://lscrm.blueberry.software/",'') + '" class="img-responsive"/>' +
                    '</div>';
            }
        }
        contentHTMLString +=
            '</div>';
        if (propDetails.length > 10) {
            contentHTMLString += '<a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">' +
                '<span class="fa fa-caret-left"></span>' +
                '</a>' +
                '<a class="right carousel-control" href="#carousel-example-generic" data-slide="next">' +
                '<span class="fa fa-caret-right"></span>' +
                '</a>';
        }
        contentHTMLString += '</div>' +
             '<div class="well">' +
             '<p>' + propDetails[2] + '</p>' +
             '</div></div>';
        return contentHTMLString;
    } catch (err) {
        alert(err);
    }
}

// open property in website to show its details
function openPropertyInWebSite() {
    try {
        window.location = "always.linkinsoft.com/property/" + $("#lblPropTitle").html() + "/" + $("#lblPropCode").html();
    } catch (err) {
        alert(err);
    }
}

// get the marker icon based on property category
function getMarkerIcon(propCode, propertyCategory) {
    try {
        if (getValueOrEmpty(passed_prop_code) != "") {
            if (propCode == passed_prop_code)
            return "";
        }
        var markerIcon = "../App_Themes/images/land.png";
        if (propertyCategory.indexOf("Apartment") != -1) {
            markerIcon = "../App_Themes/images/appartments.png";
        }
        if (propertyCategory == "Villa") {
            markerIcon = "../App_Themes/images/villas.png";
        }
        if (propertyCategory.indexOf("Commercial") != -1) {
            markerIcon = "../App_Themes/images/commercial.png";
        }
        return markerIcon;
    } catch (err) {
        alert(err);
    }
}