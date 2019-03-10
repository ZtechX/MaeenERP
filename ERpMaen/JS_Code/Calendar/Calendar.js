/************************************/
// Created By : Ahmed Nayl
// Create Date : 16/11/2015 11:30 AM
// Description : This file contains all javaScript functions related to POS form 
/************************************/

// load function set defualt values
$(function () {
    try {
        // getTree();
        // form_load();
      //  $(".SearchDiv").hide();
       // selectAll();
    } catch (err) {
        alert(err);
    }
});

// draw dynamic table for existing POS
function drawDynamicTable() {
    try {
        var tableSortingColumns = [
                { orderable: false }, null, null, null, null, null, null,
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" },
        ];
        var tableColumnDefs = [
            
        ];
        var initialSortingColumn = 1;
        var querystr = "Select Shop_PK_ID as 'Shop_PK_IDHide',Shop_Name_Ar as 'Arabic Name',Shop_Name_En as 'English Name',Shop_Phone as 'Shop Phone',Shop_Email as 'Shop Email',Dist_code as 'Distributor Code', 1 as 'Action' from tblShop ";
        loadDynamicTable(querystr, "Shop_PK_ID", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch(err) {
        alert(err);
    }
}

// draw dynamic table for existing POS
function drawDynamicTableWithFilter() {
    try {
        var tableSortingColumns = [
                { orderable: false }, null, null, null, null, null, null,
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" },
        ];
        var tableColumnDefs = [

        ];
        var filter = $("#lbldistuser").html().replace(/\|/g, ',').slice(0, -1);
        var initialSortingColumn = 1;
        var querystr = "Select Shop_PK_ID as 'Shop_PK_IDHide',Shop_Name_Ar as 'Arabic Name',Shop_Name_En as 'English Name',Shop_Phone as 'Shop Phone',Shop_Email as 'Shop Email',Dist_code as 'Distributor Code', 1 as 'Action' from tblShop where Dist_code in (" + filter + ")";
        loadDynamicTable(querystr, "Shop_PK_ID", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch (err) {
        alert(err);
    }
}

// empty pnlform and show it and hide function panel
function add() {
    try {
        prepareAdd();
        resetAll();
    } catch (err) {
        alert(err);
    }
}

// reset all controls when add or cancel
function resetAll() {
    try {
        resetFormControls();
        $("#lblShopId").html("");
    } catch (err) {
        alert(err);
    }
}

//get shops for selected regions
function getRegionManagers() {
    try {
        $("#lblRegion").html("");
        $("#ddlUsers").empty();
        $("#divManager ul").empty();
        setTimeout(function () {
            var selectednames = $("#divRegion .filter-option").html();
            var selectedvalues = "";
            var selectednames = $("#divRegion .filter-option").html().split(',');
            for (var i = 0; i < selectednames.length; i++) {
                $("#ddlRegions option").each(function () {
                    if ($(this).html() == selectednames[i]) {
                        selectedvalues = selectedvalues + $(this).val() + ",";
                    }
                });
            }
            $("#lblRegion").html(selectedvalues.slice(0, -1));
            Calendar.GetManagers(selectedvalues.slice(0, -1), function (val) {
                if (val[0] != "0") {
                    var Promoterdata = JSON.parse(val[1]);
                    for (y = 0; y < Promoterdata.length; y++) {
                        $("#ddlUsers").append($("<option></option>").val(Promoterdata[y].manager_id).html(Promoterdata[y].User_Name));
                        $("#divManager ul").append('<li data-original-index="' + y + '"><a tabindex="' + y + '" class="" style="" data-tokens="null"><span class="text">' + Promoterdata[y].User_Name + '</span><span class="glyphicon glyphicon-ok check-mark"></span></a></li>');
                    }
                    //drawShops(val[1]);
                } else {
                    //showErrorMessage("No data found !!");
                }
            });
            getShopNo();
        },2000);
    } catch (err) {
        alert(err);
    }
}
//get shops for selected regions
function getDateShop(datestr,sts) {
    try {
        var region = $("#lblRegion").html();
        var manager = $("#lblManager").html();
        var selectedvalues = "";
            var selectednames = $("#divPromoter .filter-option").html().split(',');
            for (var i = 0; i < selectednames.length; i++) {
                $("#promoter_id option").each(function () {
                    if ($(this).html() == selectednames[i]) {
                        selectedvalues = selectedvalues + $(this).val() + ",";
                    }
                });
            }
        var promoter = selectedvalues.slice(0, -1)
        Calendar.GetShops(region, manager,promoter,datestr,sts, function (val) {
                if (val[0] != "0") {
                    drawShops(val[1]);
                } else {
                    //showErrorMessage("No data found !!");
                }
            });
        getTree(region, manager, promoter, datestr, sts);
    } catch (err) {
        alert(err);
    }
}

function drawShops(data) {
    try {
        var str = "";
        if (data != "") {
            var files = JSON.parse(data);
            if (files.length > 0) {
                $('#tblShops').remove();
                createTable("tblShops", "divShops", ["<div class='checkbox checkbox-success'><input onchange='selectAllShops(this);' id='AllchkShops' type='checkbox' > <label for='AllchkShops' style='color:white'>Select</label> </div> ", "English Name ", "Arabic Name", "Code", "Region", "Area", "Partner", "Status"]);
                var tr = document.createElement('tr');
                $('#tblShops tbody').append(tr);
                for (i = 0; i < files.length; i++) {
                    appendShops(files[i], i);
                }
                if ($("#lblManagerDetails").html() != "") {                  
                        var managershops = $("#lblManagerDetails").html().split(',');
                        for (var m = 0; m < managershops.length; m++) {
                            $("#tblShops tbody tr").each(function () {
                                if ($(this).prop("id") == managershops[m]) {
                                    $(this).find("#chkShops").prop("checked", true);
                                }
                            });
                        }
                }
            }
        }
    } catch (error) {
        alert(error);
    }
}


function selectAllShops(sender) {
    try {
        $('#tblShops tbody tr').each(function () {
            if ($(this).is(":visible")) {
                $(this).find("#chkShops").prop('checked', $(sender).prop('checked'));
                var dist_code = $(this).find("#lblDistCode").html();
                $("#ulbasictree").find("#dist_code").prop('checked', $(sender).prop('checked'));
            }
        });
    }
catch (error) {
    alert(error);
}
}

function appendShops(fileDetails, i) {
    try {
        var index = parseFloat($('#tblShops tbody tr').length) + 1;
        var tr = document.createElement('tr');
        $(tr).prop("id",fileDetails.Shop_PK_ID);
        $(tr).append("<td><div class='checkbox checkbox-success'><input onchange='selectdistfromtree(this);' id='chkShops' type='checkbox' > <label for='checkbox6'></label><label id='lblDistCode' style='display:none'>" + fileDetails.Dist_code + "</label> </div> </td>");
        $(tr).append('<td><span id="lblStock_Name" class="index">' + fileDetails.Shop_Name_En + '</span>');
        $(tr).append('<td><span id="lblStock_ItemCode" class="index">' + fileDetails.Shop_Name_Ar + '</span>');
        $(tr).append('<td><span id="lblStock_ItemNo" class="index">' + fileDetails.Shop_Code + '</span>');
        $(tr).append('<td><span id="lblStock_Amount" class="index">' + fileDetails.Region + '</span>');
        $(tr).append('<td><span id="lblStock_ItemCode" class="index">' + fileDetails.Area + '</span>');
        $(tr).append('<td><span id="lblStock_ItemNo" class="index">' + fileDetails.Partner + '</span>');
        $(tr).append('<td><span id="lblStock_Amount" class="index">' + fileDetails.Status + '</span>');
        $('#tblShops tbody').append(tr);
    } catch (err) {
        alert(err);
    }
}

//get manager details

function getManagerDetails() {
    try {
        $("#lblManagerDetails").html("");
         $("#divShops").empty();
    
        $("#ulbasictree").empty();
        $(".filter-option").html("");
        Calendar.GetManagerDetails($("#ddlUsers").val(), function (val) {
            if (val[0] != "0") {
                var areaNames = "";
                var shopsId=""
                var managerdata = JSON.parse(val[1]);
                for (x = 0; x < managerdata.length; x++) {
                    if (notExist(areaNames, managerdata[x].name)) {
                        areaNames = areaNames + managerdata[x].name + ",";
                        $(".dropdown-menu .inner li").each(function () {
                            if ($(this).find(".text").html() == managerdata[x].name) {
                                $(this).addClass("selected");
                            }
                        });
                    }
                    shopsId = shopsId + managerdata[x].Shop_PK_ID + ",";
                }
                $(".filter-option").html(areaNames);
                $("#lblManagerDetails").html(shopsId);
                getRegionShops();
                $(".SearchDiv").show();
              
            } else {
               // showErrorMessage("No data found !!");
            }
        });
    } catch (err) {
        alert(err);
    }
}

//selectdistfromtree
function selectdistfromtree(sender) {
    try {
        var code = $(sender).closest("tr").find("#lblDistCode").html();
        $("#ulbasictree").find("#" + code).prop("checked", $(sender).prop("checked"));
        $("#ulbasictree li").each(function () {
            var id = $(this).prop("id");
            if ($("#"+id+" #" + code).length) {
                $("#"+id+" ul").show();
            }
        });
        
    } catch (err) {
        alert(err);
    }
}
function notExist(bigstr, smallstr) {
    try {
        var selectednames = bigstr.split(',');
        for (var i = 0; i < selectednames.length; i++) {
            if (selectednames[i] == smallstr) {
                return false;
            }
        }
        return true;
    } catch (err) {

    }
}


function myFunction() {
    var treechecked = 0;
    $("#ulbasictree").find('input[type=checkbox]').each(function () {
        if ($(this).prop("checked")) {
            treechecked = 1;
        }
    });
    input = $("#myInput").val();
    $("#tblShops tbody tr").each(function () {
        var i = 0;
        $(this).find("td").each(function () {
            if ($(this).find(".index").length) {
                if ($(this).find(".index").html().toLowerCase().indexOf(input.toLowerCase()) >= 0) {
                    i = 1;
                }
            }
        });
        if (i == 1) {
            var distid = $(this).find("#lblDistCode").html();
            if (treechecked == 1 && $("#ulbasictree #" + distid).prop("checked")) {
                $(this).show();
            } else if (treechecked == 0) {
                $(this).show();
            } else {
                $(this).hide();
            }
        } else {
            $(this).hide();
        }
    });
}

//get shops for selected regions
function getManagerPromoters() {
    try {
        $("#promoter_id").empty();
        $("#divPromoter ul").empty();
        $("#lblManager").html("");
        setTimeout(function () {
            var selectednames = $("#divPromoter .filter-option").html();
            var selectedvalues = "";
            var selectednames = $("#divManager .filter-option").html().split(',');
            for (var i = 0; i < selectednames.length; i++) {
                $("#ddlUsers option").each(function () {
                    if ($(this).html() == selectednames[i]) {
                        selectedvalues = selectedvalues + $(this).val() + ",";
                    }
                });
            }
            $("#lblManager").html(selectedvalues.slice(0, -1));
            Calendar.GetManagerPromoter(selectedvalues.slice(0, -1), function (val) {
                if (val[0] != "0") {
                    var Promoterdata = JSON.parse(val[1]);
                    for (y = 0; y < Promoterdata.length; y++) {
                        $("#promoter_id").append($("<option></option>").val(Promoterdata[y].Promoter_id).html(Promoterdata[y].User_Name));
                        $("#divPromoter ul").append('<li data-original-index="' + y + '"><a tabindex="' + y + '" class="" style="" data-tokens="null"><span class="text">' + Promoterdata[y].User_Name + '</span><span class="glyphicon glyphicon-ok check-mark"></span></a></li>');
                    }
                    //drawShops(val[1]);
                } else {
                    //showErrorMessage("No data found !!");
                }
            });
            getShopNo();
        }, 2000);
    } catch (err) {
        alert(err);
    }
}

function getShopNo() {
    try {
        var region=$("#lblRegion").html();
        var manager = $("#lblManager").html();
        var selectedvalues = "";
        setTimeout(function () {           
            var selectednames = $("#divPromoter .filter-option").html().split(',');
            for (var i = 0; i < selectednames.length; i++) {
                $("#promoter_id option").each(function () {
                    if ($(this).html() == selectednames[i]) {
                        selectedvalues = selectedvalues + $(this).val() + ",";
                    }
                });
            }
        
            var promoter=selectedvalues.slice(0, -1)
            Calendar.getShopNo(region,manager,promoter, function (val) {
                drawCalendar(val);
            });
        }, 2000);
    } catch (err) {
        alert(err);
    }
}

//draw calendar
function drawCalendar_old(val) {
    try {
        for (i = 0; i < 30; i++) {
            var date = val[i].split("|")[0];
            var value = val[i].split("|")[1];
            $('#calendar .fc-rigid table thead tr td').each(function () {
                if ($(this).attr("data-date") == "2017-10-10") {
                        $('#calendar .fc-rigid table tbody tr td:eq(' + $(this).index() + ') ').html('<a class="fc-day-grid-event fc-h-event fc-event fc-start fc-end bg-success fc-draggable"><div class="fc-content"><span class="fc-title">' + value + '</span></div></a>');
                }
            });
        }
    } catch (err) {
        alert(err);
    }
}

//draw calendar
function drawCalendar(val) {
    try {
        jq223('#calendar').fullCalendar('removeEvents');
        for (i = 0; i < 30; i++) {
            var date = val[i].split("|")[0];
            var value = val[i].split("|")[1];
            var value1 = val[i].split("|")[2];
            var value2 = val[i].split("|")[3];
            var myEvent = {
                title: value,
                allDay: true,
                start: new Date(date),
                end: new Date(date),
                datestr: date,
                color: 'green',
                sts:3,
            };
            var myEvent1 = {
                title: value1,
                allDay: true,
                start: new Date(date),
                end: new Date(date),
                datestr: date,
                color: 'red',
                sts:4,
            };
            var myEvent2 = {
                title: value2,
                allDay: true,
                start: new Date(date),
                end: new Date(date),
                datestr: date,
                color: 'orange',
                sts:5,
            };
            if (value != 0) {
                jq223('#calendar').fullCalendar('renderEvent', myEvent, true);
            }
            if (value1 != 0) {
                jq223('#calendar').fullCalendar('renderEvent', myEvent1, true);
            }
            if (value2 != 0) {
                jq223('#calendar').fullCalendar('renderEvent', myEvent2, true);
            }
        }
    } catch (err) {
        alert(err);
    }
}



