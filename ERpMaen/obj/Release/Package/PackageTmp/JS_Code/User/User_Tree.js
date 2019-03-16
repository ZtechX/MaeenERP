/************************************/
// Created By : Ahmed Nayl
// Create Date : 16/11/2015 12:00 PM
// Description : This file contains all javaScript functions resopsable for editing Distributer
/************************************/
// called after update function success
function getTree() {
    try {
        $("#ulbasictree").empty();
        User.GetTree("", function (val) {
            var data = JSON.parse(val[1]);
            if (val[0] != "0") {
                drawMainTree(data);
            } else {
                showErrorMessage("No data found !!");
            }
        });
    } catch (err) {
        alert(err);
    }
}

function drawMainTree(data) {
    try {
        for (m = 0; m < data.length; m++) {
          //  $("#ulbasictree").append('<li id="dist' + data[m].Distributor_PK_ID + '" data-jstree={opened:true}><a href="#" onclick="getSubDistributer(\'' + data[m].Distributor_PK_ID + '\');"><i class="icon-wrench"></i>' + data[m].Distributor_NameEn + '</a></li>');
            $("#ulbasictree").append('<li id="dist' + data[m].Distributor_PK_ID + '" class="parent_li" ><div class="checkbox checkbox-success checkbox-circle"><input onchange="setdistuser(\'' + data[m].Distributor_PK_ID + '\',this);" id="' + data[m].Distributor_PK_ID + '" type="checkbox"/><label><span onclick="getSubDistributer(\'' + data[m].Distributor_PK_ID + '\');"> <img src="' + data[m].Distributor_Logo + '" alt="user-img" class="img-circle user-img"> ' + data[m].Distributor_NameEn + '</span></label></div></li>');
        }
    } catch (err) {
        alert(err);
    }
    $(".ulbasictree").on("click", function (e) {
        e.stopPropagation();
        var clickedLi = $(this);
        $("> ul", clickedLi).slideToggle();
        clickedLi.toggleClass("current");
    });
}

function getSubDistributer(distid) {
    try {
        if ($("#dist" + distid + " ul").length) {
            $("#dist" + distid + " ul").toggle();
        } else {
            User.GetSubTree(distid, function (val) {
                var data = JSON.parse(val[1]);
                // var data1 = JSON.parse(val[2]);
                if (val[0] != "0") {
                    drawSubTree(data, distid);
                    drawChart(distid);
                } else {
                    showErrorMessage("No data found !!");
                }
            });
        }
    } catch (err) {
        alert(err);
    }
}

function drawSubTree(data,distid) {
    try {
        var str = "<ul>"
        for (m = 0; m < data.length; m++) {
           // str = str + '<li id="dist' + data[m].Distributor_PK_ID + '" data-jstree={opened:true}><a href="#" onclick="getSubDistributer(\'' + data[m].Distributor_PK_ID + '\');">' + data[m].Distributor_NameEn + '</a></li>';
            str = str + '<li id="dist' + data[m].Distributor_PK_ID + '" class="parent_li" ><div class="checkbox checkbox-success checkbox-circle"><input onchange="setdistuser(\'' + data[m].Distributor_PK_ID + '\');" id="' + data[m].Distributor_PK_ID + '" type="checkbox"/><label><span onclick="getSubDistributer(\'' + data[m].Distributor_PK_ID + '\');"> <img src="' + data[m].Distributor_Logo + '" class="img-circle user-img"> ' + data[m].Distributor_NameEn + '</span></label></div></li>';
        }
        str=str+"</ul>"
        $("#dist" + distid).append(str);
    } catch (err) {
        alert(err);
    }
}

function getDistDetails(distid) {
    try {
        User.GetSubTree(distid, function (val) {
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

function setdistuser(id,sender) {
    try {
        if ($(sender).prop("checked")) {
            $("#lbldistuser").html($("#lbldistuser").html() + id + "|");
        } else {
            var newstr ="";
            var valNew = $("#lbldistuser").html().split('|');
            for (var i = 0; i < valNew.length; i++) {
                if (valNew[i] != id && valNew[i] !="" ) {
                    newstr = newstr + valNew[i] + "|";
                }
            }
            $("#lbldistuser").html(newstr);
        }
    } catch (err) {
        alert(err);
    }
}