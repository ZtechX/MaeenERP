/************************************/
// Created By : Ahmed Nayl
// Create Date : 16/11/2015 12:00 PM
// Description : This file contains all javaScript functions resopsable for editing Distributer
/************************************/
// called after update function success
function getTree(region, manager, promoter, datestr, sts) {
    try {
        $("#ulbasictree").empty();
        Calendar.GetTree(region, manager, promoter, datestr,sts, function (val) {
            var data = JSON.parse(val[1]);
            var data1 = JSON.parse(val[2]);
            if (val[0] != "0") {
                drawMainTree(data, data1);
            } else {
                showErrorMessage("No data found !!");
            }
        });
    } catch (err) {
        alert(err);
    }
}

var allitemsstr = '';
function drawMainTree(data, data1) {
    try {
        for (m = 0; m < data.length; m++) {
            allitemsstr = '';
            allitemsstr = '<li id="dist' + data[m].Distributor_PK_ID + '" class="parent_li" ><div class="checkbox checkbox-success checkbox-circle"><input onchange="setdistuser(\'' + data[m].Distributor_PK_ID + '\',this);" id="' + data[m].Distributor_PK_ID + '" type="checkbox"/><label><span onclick="getSubDistributer(\'' + data[m].Distributor_PK_ID + '\');"> <img src="' + data[m].Distributor_Logo + '" alt="user-img" class="img-circle user-img"> ' + data[m].Distributor_NameEn + '</span></label></div>'
            var stritems = getsubItems(data[m].Distributor_PK_ID, data1, 0) + '</li>';
            $("#ulbasictree").append(allitemsstr);
        }
    } catch (err) {
        alert(err);
    }
}

//draw sub items 
function getsubItems(maindistid, subdata, startpos) {
    try {
        for (s = startpos; s < subdata.length; s++) {
            if (subdata[s].Distributor_ParentID == maindistid) {
                allitemsstr = allitemsstr + "<ul style='display:none' id='distsubdist'>";
                allitemsstr = allitemsstr + '<li id="dist' + subdata[s].Distributor_PK_ID + '" class="parent_li" ><div class="checkbox checkbox-success checkbox-circle"><input onchange="setdistuser(\'' + subdata[s].Distributor_PK_ID + '\',this);" id="' + subdata[s].Distributor_PK_ID + '" type="checkbox"/><label><span onclick="getSubDistributer(\'' + subdata[s].Distributor_PK_ID + '\');"> <img src="' + subdata[s].Distributor_Logo + '" class="img-circle user-img"> ' + subdata[s].Distributor_NameEn + '</span></label></div>';
                getsubItems(subdata[s].Distributor_PK_ID, subdata, s + 1);
                allitemsstr = allitemsstr + '</li></ul>';
            }
        }
    } catch (err) {
        alert(err);
    }
}
function getSubDistributer(distid) {
    try {
        if ($("#dist" + distid + " .parent_li").length >= 1) {
            $("#dist" + distid + " #distsubdist").toggle();
        } else {
            Distributer.GetSubTree(distid, function (val) {
                var data = JSON.parse(val[1]);
                // var data1 = JSON.parse(val[2]);
                if (val[0] != "0") {
                    drawSubTree(data, distid);
                    // drawChart(distid);
                } else {
                    showErrorMessage("No data found !!");
                }
            });
        }
    } catch (err) {
        alert(err);
    }
}

function getDistDetails(distid) {
    try {
        Shop.GetSubTree(distid, function (val) {
            edit(val);
        });      
    } catch (err) {
        alert(err);
    }
}

function deleteDist(distid) {
    try {
        alert(distid);
    } catch (err) {
        alert(err);
    }
}
function clearData(distid) {
    try {
        cancel();
        $("#divFlowChart").empty();
        $("#divStock").empty();
        $("#divlogs").empty();
        $("#cmdSave").prop("CommandArgument", "New");
        $("#lblDistributor_ParentID").html(distid);
        resetAll();

    } catch (err) {
        alert(err);
    }
}

function showHideCol(type) {
    try {
        var searchtree = 0;
        $("#tblShops tbody tr").each(function () {
            if (type == 0) {
                $(this).show();
            } else {
                var distcode = $(this).find("#lblDistCode").html();
                if ($("#ulbasictree").find("#" + distcode).prop("checked")) {
                    $(this).show();
                } else {
                    $(this).hide();
                }
            }
        });
    } catch (err) {
        alert(err);
    }
}


function setdistuser(id, sender) {
    try {
        if ($(sender).prop("checked")) {
            $("#lbldistuser").html($("#lbldistuser").html() + "'" + id + "'|");
            $("#dist" + id).find('input[type=checkbox]').prop("checked", true);
            $("#dist" + id).find('input[type=checkbox]').each(function () {
                $("#lbldistuser").html($("#lbldistuser").html() + "'" + $(this).prop("id") + "'|");
            });
            getSubDistributer(id);
        } else {
            $("#dist" + id).find('input[type=checkbox]').prop("checked", false);
            var newstr = "";
            var valNew = $("#lbldistuser").html().split('|');
            for (var i = 0; i < valNew.length; i++) {
                if (valNew[i] != "'" + id + "'" && valNew[i] != "") {
                    var existid = 0;
                    $("#dist" + id).find('input[type=checkbox]').each(function () {                      
                        if (valNew[i] != "'" + $(this).prop("id") + "'") {
                        } else {
                            existid = 1;
                        }
                    });
                    if (existid == 0) {
                        newstr = newstr + valNew[i] + "|";
                    }
                }

            }
            $("#lbldistuser").html(newstr);
            getSubDistributer(id);
        }
        if ($("#lbldistuser").html() == "") {
            showHideCol(0);
        } else {
            showHideCol(1);
        }
    } catch (err) {
        alert(err);
    }
}