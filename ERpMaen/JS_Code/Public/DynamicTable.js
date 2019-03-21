/************************************/
// Created By : Mostafa Abdelghffar
// Create Date : 16/6/2015 09:30 AM
// Description : This file contains all javaScript functions for generating dynamic table
/************************************/

// define global variables
var keys = [];
var columns = [];
var filterColumns = [];
var vColumnDefs = [];
var initialColumn, res, formName, formAutoCode, formOrReport, Archived;
var body = document.createElement("tbody");

// generate a table based for passed parameter that have all features of DataTable plugin
function loadDynamicTable(frmName, autoCode, tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, frmorReport) {
    
    try {
        $("#divLoader").show();
        formName = frmName;
        formAutoCode = autoCode;
        if (formName == "emails" || formName == "SMS" || formName == "Files") {
            formAutoCode = autoCode.split("|")[0];
            Archived = autoCode.split("|")[1];
        }
        columns = tableSortingColumns;
        initialColumn = initialSortingColumn;
        filterColumns = tableFilteringColumns;
        vColumnDefs = tableColumnDefs;
        formOrReport = frmorReport;
        
        $.ajax({
            type: "POST",
            url: "../ASMX_WebServices/WebService.asmx/GetFormQuaryString",
            data: "{'formName': '" + formName + "|" + Archived + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (quary) {
                generateDynamicTable(quary["d"]);
            },
            error: function (err) {
                console.log(err);
                alert("err");
            }
        });
    } catch (err) {
        alert(err);
    }
}

// generate a tables based on the passed quary string
function generateDynamicTable(quaryStr) {
    
    try {
        $.ajax({
            type: "POST",
            url: "../ASMX_WebServices/WebService.asmx/GetListData",
            data: "{'formName':'" + formName +"'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (quary) {
                onSuccessGetTableData(quary["d"]);
            },
            error: function (err) {
                console.log(err);
                alert("err1");
            }
        });
       // WebService.GetListData(quaryStr, formName, onSuccessGetTableData);
    } catch (err) {
        alert(err);
    }
}

// success function for getting table data from db
function onSuccessGetTableData(val) {
    try {
        var userType = "Admin";
        var salesmanCode = "";
        body.innerHTML = "";
        $("#tableDiv").empty();
        $(".ColVis_collection").remove();
        $("#tableDiv").append('<table id="tablelist" runat="server" class="report_table"></table>');

        var ar = val[0].split('|');
        appendHeader(ar);
        appendFooter(ar);
        applyDatatable(val);

        enableDateSorting();
        enableRowsReordering();
        $.datepicker.regional[""].dateFormat = 'dd/mm/yy';
        $.datepicker.setDefaults($.datepicker.regional['']);
        $("#tablelist_wrapper").append('<div class="table_scroll" id="divTableScroll"></div>');
        $("#divTableScroll").append($('#tablelist'));
        $("#divTableScroll").insertAfter($("#tablelist_filter"));
        $("#tablelist_wrapper .clear").remove();
        $("#divLoader").hide();
    } catch (err) {
        alert(err);
    }
}

// append header to the table
function appendHeader(arr) {
    try {
        var hrow = "<thead><tr>";
        if (formOrReport == "Form" || formOrReport == "Publish/UnPublish") {
            hrow += "<th><input type='checkbox' onclick='selectAll(this);'></th>";
        }
        for (var k = 0; k < arr.length - 1; k++) {
            if (arr[k].indexOf("Hide") == -1 && arr[k].indexOf("hide") == -1) {
                hrow += "<th>" + arr[k] + "</th>";
            }
        }
        hrow += "</tr></thead>";
        $(tablelist).append(hrow);
    } catch (err) {
        alert(err);
    }
}

// append footer to the table
function appendFooter(arr) {
    try {
        var tfoot = "<tfoot><tr>";
        if (formOrReport == "Form" || formOrReport == "Publish/UnPublish") {
            tfoot += "<th><input type='button' title='Reset' onclick='ClearFilterSearch(this);' class='filter-icon' /></th>";
        }
        for (var k = 0; k < arr.length - 1; k++) {
            if (arr[k].indexOf("Hide") == -1 && arr[k].indexOf("hide") == -1) {
                tfoot += "<th>" + arr[k] + "</th>";
            }
        }
        tfoot += "</tr></tfoot>";
        $(tablelist).append(tfoot);
    } catch (err) {
        alert(err);
    }
}

// clear search filters
function ClearFilterSearch(sender) {
    try {
        $(sender).closest("tr").find('input[type=text],textarea,select').each(function () {
            $(this).val('');
        });
        var table = $('#tablelist').dataTable();
        // Remove all filtering
        table.fnFilterClear('');

    } catch (err) {
        alert(err);
    }
}

// append body row to the table body
function appendBodyRow(arr, rownum, userType, salesmanCode) {
    try {
        var row;
        if (formOrReport == "Form") {
            if ((formName == "ListingSale" || formName == "ListingRent" || formName == "ListingShort")
                && userType != "Admin" && arr[getColNameIndex("AgentHide")] != salesmanCode) {
                row = "<tr class='rowDisabled' disabled='disabled' title='u cant modify or delete this listing' onclick='alert(this.title);' id=" + arr[getColNameIndex(formAutoCode)] + " >";
            } else {
                row = "<tr onclick='row_click(event);' class='RowStyle' id=" + arr[getColNameIndex(formAutoCode)] + " >";
            }
        } else if (formOrReport == "Report" || formOrReport == "Publish/UnPublish") {
            row = "<tr class='RowStyle' id=" + arr[getColNameIndex(formAutoCode)] + " >";
        }
        if (formOrReport == "Form" || formOrReport == "Publish/UnPublish") {
            row += "<td><input id='cb" + rownum + "&" + arr[getColNameIndex(formAutoCode)] + "' type='checkbox'/></td>";
        }
        row += "<td>" + (rownum + 1) + "</td>";
        for (var j = 0; j < arr.length - 1; j++) {
            if (res[0].split('|')[j] != null) {
                if (res[0].split('|')[j].indexOf("Hide") == -1 && res[0].split('|')[j].indexOf("hide") == -1) {
                    var td = "<td>" + arr[j] + "</td>";
                    for (k = 0; k < keys.length; k++) {
                        var kName = res[0].split('|')[j];
                        if (keys[k].KeyName == kName) {
                            var href = "";
                            if (formName == "ListingSale" || formName == "ListingRent" || formName == "ListingShort") {
                                href = getListingPropertiesKeyHref(res[0].split('|')[j], keys[k].KeyForm, arr);
                            } else if (formName == "Leads") {
                                href = getLeadsKeyHref(res[0].split('|')[j], keys[k].KeyForm, arr);
                            } else if (formName == "Enquiry") {
                                href = getEnquiryKeyHref(res[0].split('|')[j], keys[k].KeyForm, arr);
                            } else if (formName == "Contract") {
                                href = getContractKeyHref(res[0].split('|')[j], keys[k].KeyForm, arr);
                            } else if (formName == "RentContract") {
                                href = getRentContractKeyHref(res[0].split('|')[j], keys[k].KeyForm, arr);
                            } else if (formName == "Contacts") {
                                href = getContactKeyHref(res[0].split('|')[j], keys[k].KeyForm, arr);
                            } else if (formName == "Correspondance") {
                                href = getCorrespondanceKeyHref(res[0].split('|')[j], keys[k].KeyForm, arr);
                            } else if (formName == "LeadsCorrespondance") {
                                href = getLeadsCorrespondanceKeyHref(res[0].split('|')[j], keys[k].KeyForm, arr);
                            } else if (formName == "PropertyCorrespondance") {
                                href = getPropertyCorrespondanceKeyHref(res[0].split('|')[j], keys[k].KeyForm, arr);
                            }
                            td = "<td><a href ='http://lscrm.blueberry.software/" + href + "' target='_blank'>" + arr[j] + "</a></td>"
                            if (formName == "LeadsCorrespondance") {
                                if (keys[k].KeyName == 'Lead Code') {
                                    var hh = arr[getColNameIndex("LeadCodeHide")];
                                    td = "<td><a target='_blank' type='" + hh + "' onclick='getLeadPropertyDetails(this.type);'>" + arr[j] + "</a></td>";
                                }
                            }
                        }
                    }
                    row += td;
                }
            }



        }
        row += "</tr>";
        $(body).append(row);
    } catch (err) {
        alert(err);
    }
}

// open form with passed href
function openForm(formHref) {
    try {
        var myWindow = window.open(formHref, "", "width=800, height=600");
        //window.location.href = formHref;
    } catch (err) {
        alert(err);
    }
}

// get keys href for listing properties forms 
function getListingPropertiesKeyHref(colName, formName, row) {
    try {
        var href = "";
        if (colName == "New Contact") {
            href = formName + "?operation=add";
        } else if (colName == "New Corres.") {
            href = formName + '?code=' + $(row).attr('id') + '&clientCode=' + $(row).attr('keysattr').split("|")[0] + '&operation=add';
        } else if (colName == "Agent") {
            href = formName + "?code=" + $(row).attr('keysattr').split("|")[1] + "&operation=search";
        } else if (colName == "Print Brochure") {
            // href = formName + "?code=" + arr[getColNameIndex("AutoCodeHide")];

            href = "Property/" + $(row).attr('keysattr').split("|")[3] + "/" + $(row).attr('keysattr').split("|")[2];
        } else if (colName == "Owner Name") {
            href = formName + "?code=" + $(row).attr('keysattr').split("|")[0] + "&operation=search";
        }
        return href;
    } catch (err) {
        alert(err);
    }

}

// get keys href for enquiry form
function getEnquiryKeyHref(colName, formName, row) {
    try {
        var href = "";
        if (colName == "New Corres") {
            href = formName + '?ClientCode=' + $(row).attr('keysattr').split("|")[0] + '&Operation=add';
        } else if (colName == "Agent Name") {
            href = formName + '?Code=' + $(row).attr('keysattr').split("|")[1] + '&Operation=search';
        } else if (colName == "Contact Name") {
            href = formName + '?Code=' + $(row).attr('keysattr').split("|")[0] + '&Operation=search';
        }

        return href;
    } catch (err) {
        alert(err);
    }
}

// get keys href for sales contract form
function getContractKeyHref(colName, formName, row) {
    try {
        var href = "";
        if (colName == "First Buyer/Tenant Name") {
            href = formName + '?Code=' + $(row).attr('keysattr').split("|")[2] + '&Operation=search';
        } else if (colName == "Second Buyer/Tenant Name") {
            href = formName + '?Code=' + $(row).attr('keysattr').split("|")[3] + '&Operation=search';
        } else if (colName == "Agent Name") {
            href = formName + '?Code=' + $(row).attr('keysattr').split("|")[1] + '&Operation=search';
        } else if (colName == "LandLord Name") {
            href = formName + '?Code=' + $(row).attr('keysattr').split("|")[0] + '&Operation=search';
        } else if (colName == "Deal Code") {
            href = formName + '?dealCode=' + $(row).attr('keysattr').split("|")[1] + '&Operation=search';
        }
        else if (colName == "Listing Code") {
            var purpose = $(row).attr('keysattr').split("|")[0];
            if (purpose == "Sale") {
                formName = "Listing_Module/ListingSale.aspx";
            } else if (purpose == "Rent") {
                formName = "Listing_Module/ListingRent.aspx";
            } else if (purpose == "Short term") {
                formName = "Listing_Module/ListingShort.aspx";
            }
            href = formName + "?Code=" + $(row).attr('keysattr').split("|")[2] + "&Operation=Search";
        }

        return href;
    } catch (err) {
        alert(err);
    }
}

// get keys href for leads form
function getLeadsKeyHref(colName, formName, row) {
    try {
        var href = "";
        if (colName == "Agent Name") {
            href = formName + '?Code=' + $(row).attr('keysattr').split("|")[1] + '&Operation=search';
        } else if (colName == "Contact Name") {
            href = formName + "?Code=" + $(row).attr('keysattr').split("|")[0] + "&Operation=Search";
        }
        return href;
    } catch (err) {
        alert(err);
    }
}

// get keys href for contacts form
function getContactKeyHref(colName, formName, row) {
    try {
        var href = "";
        if (colName == "Assigned To") {
            href = formName + '?Code=' + $(row).attr('keysattr') + '&Operation=search';
        }
        return href;
    } catch (err) {
        alert(err);
    }
}

// get keys href for Correspondance form
function getCorrespondanceKeyHref(colName, formName, row) {
    try {
        var href = "";
        if (colName == "Agent Name") {
            href = formName + '?Code=' + $(row).attr('keysattr').split("|")[1] + '&Operation=search';
        } else if (colName == "Contact Name") {
            href = formName + "?Code=" + $(row).attr('keysattr').split("|")[0] + "&Operation=Search";
        }
        return href;
    } catch (err) {
        alert(err);
    }
}

// get keys href for leads Correspondance form
function getLeadsCorrespondanceKeyHref(colName, formName, row) {
    try {
        var href = "";
        if (colName == "Agent Name") {
            href = formName + '?Code=' + $(row).attr('keysattr').split("|")[1] + '&Operation=search';
        } else if (colName == "Contact Name") {
            href = formName + "?Code=" + $(row).attr('keysattr').split("|")[0] + "&Operation=Search";
        } else if (colName == "Lead Code") {
            href = formName + "?Code=" + $(row).attr('keysattr').split("|")[2] + "&Operation=Search";
        }
        return href;
    } catch (err) {
        alert(err);
    }
}

// get keys href for property Correspondance form
function getPropertyCorrespondanceKeyHref(colName, formName, row) {
    try {
        var href = "";
        if (colName == "Agent Name") {
            href = formName + '?Code=' + $(row).attr('keysattr').split("|")[1] + '&Operation=search';
        } else if (colName == "Contact Name") {
            href = formName + "?Code=" + $(row).attr('keysattr').split("|")[0] + "&Operation=Search";
        } else if (colName == "Property Code") {
            var purpose = $(row).attr('keysattr').split("|")[3];
            if (purpose == "Sale") {
                formName = "Listing_Module/ListingSale.aspx";
            } else if (purpose == "Rent") {
                formName = "Listing_Module/ListingRent.aspx";
            } else if (purpose == "Short term") {
                formName = "Listing_Module/ListingShort.aspx";
            }
            href = formName + "?Code=" + $(row).attr('keysattr').split("|")[2] + "&Operation=Search";
        }
        return href;
    } catch (err) {
        alert(err);
    }
}

// get column index that have the passed name
function getColNameIndex(colName) {
    try {
        var colIndex = "";
        $("#tablelist thead tr:first th").each(function (indx) {
            if ($(this).html() == colName) {
                colIndex = indx;
            }
        });
        return colIndex;
    } catch (err) {
        alert(err);
    }
}

// get column name
function getColName(colIndex) {
    try {
        var colName = "";
        $("#tablelist thead tr:first th").each(function (indx) {
            if (indx == colIndex && colIndex != 0) {
                colName = $(this).html();
            }
        });
        return colName;
    } catch (err) {
        alert(err);
    }
}

// Apply Keys
function applyKeys(idx, row,cell) {
    var columnName = getColName(idx);
    for (k = 0; k < keys.length; k++) {
        if (keys[k].KeyName == columnName) {
            var href = "";
            if (formName == "ListingSale" || formName == "ListingRent" || formName == "ListingShort") {
                href = getListingPropertiesKeyHref(columnName, keys[k].KeyForm, row);
            } else if (formName == "Leads") {
                href = getLeadsKeyHref(columnName, keys[k].KeyForm, row);
            } else if (formName == "Contract") {
                href = getContractKeyHref(columnName, keys[k].KeyForm, row);
            } else if (formName == "Contacts") {
                href = getContactKeyHref(columnName, keys[k].KeyForm, row);
            }
            $('td:eq("' + idx + '")', row).html('<a href="/' + href + '"  target="_blank">' + $(cell).html() + '</b>');

            //td = "<td><a href ='http://lscrm.blueberry.software/" + href + "' target='_blank'>" + arr[j] + "</a></td>"
            //if (formName == "LeadsCorrespondance") {
            //    if (keys[k].KeyName == 'Lead Code') {
            //        var hh = $(row).attr('keysattr').split("|")[2];
            //        td = "<td><a target='_blank' type='" + hh + "' onclick='getLeadPropertyDetails(this.type);'>" + arr[j] + "</a></td>";
            //    }
            //}
        }
    }
}

// apply dataTable features for the table
function applyDatatable(val) {
    try {
        
         val.splice(0, 1);
        $('#tablelist').dataTable({
                 dom: 'CT<"">BRlfrtip',
        buttons: [
            { extend: 'copy', text: 'نسخ' }, { extend: 'excel', text: 'اكسل' }, { extend: 'print', text: 'طباعة' }, { extend: 'pdf', text: 'PDF' }
        ],Defs: vColumnDefs,
            "colVis": {
                "exclude": [0],
                "align": "left",
                "restore": "Restore",
                "showAll": "Show all",
                "showNone": "Show none"
            },
            destroy: true,
            "data": val,
            "aoColumns": columns,      
           "aaSorting": [[initialColumn, 'desc']],
            "fnDrawCallback": function (oSettings) {
                addCheckBoxToTableRows();
            }, 
            "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                
                if (getFormName() == "targets.aspx") {
                    $('td:eq(5)', nRow).html('<a href="targetdetails.aspx?operation=search&Code=' + aData[1] + '">التفاصيل</a>');
                    return nRow;
                }
                else if (getFormName() == "CommonQuest") {
                    $('td:last-child', nRow).html('<div class="hidden-sm hidden-xs action-buttons"><button class="btn btn-xs btn-danger btn-quick" onclick="deleteQuestion(' + aData[0] + '); return false;" > <i class="fa fa-times"></i></button></div>');
                    // $('td:last-child', nRow).html('<div class="hidden-sm hidden-xs action-buttons"><button class="btn btn-xs btn-danger btn-quick" onclick="deleteQuestion(' + aData[0] + '); return false;" > <i class="fa fa-times"></i></button><button class="btn btn-xs btn-warning btn-quick" onclick="editQuestion(' + aData[0] + '); return false;" > <i class="fa fa-pencil"></i></button ></div>');

                }
                else if (getFormName() == "ConsultationDetails") {
                    ConsultationDetails.isSuperAdmin(function (val) {
                        if (val) {
                            $('td:last-child', nRow).html('<div class="hidden-sm hidden-xs action-buttons"><a onclick="viewConslute(' + aData[0] + '); return 0;"   style="color:green;margin-left:10px;" ><i class="ace-icon fa fa-pencil fa-lg"></i></a><a target="_blank" href="consultings.aspx?id=' + aData[0] + '" style="color:#89c3ea;margin-left:10px;"  ><i class="ace-icon fa fa-envelope fa-lg"></i></a><a onclick="assignAdvisor(' + aData[0] + '); return 0;" style="color:black;margin-left:10px;" ><i class="ace-icon fa fa-hand-pointer-o fa-lg"></i></a><a onclick="getconsultation(' + aData[0] + '); return 0;"   style="color:#4db4b6;margin-left:10px;" ><i class="ace-icon fa fa-print fa-lg"></i></a></div>');

                        } else {
                            $('td:last-child', nRow).html('<div class="hidden-sm hidden-xs action-buttons"><a onclick="viewConslute(' + aData[0] + '); return 0;"   style="color:green;margin-left:10px;" ><i class="ace-icon fa fa-pencil fa-lg"></i></a><a target="_blank" href="consultings.aspx?id=' + aData[0] + '" style="color:#89c3ea;margin-left:10px;"  ><i class="ace-icon fa fa-envelope fa-lg"></i></a><a onclick="getconsultation(' + aData[0] + '); return 0;"   style="color:#4db4b6;margin-left:10px;" ><i class="ace-icon fa fa-print fa-lg"></i></a></div>');

                        }
                    });

                }
            },
        }).columnFilter({
            destroy: true,
            aoColumns: filterColumns
        });
        $(".DTTT_container").hide();
    } catch (err) {
        alert(err);
    }
}

// add checkbox to each row in table
function addCheckBoxToTableRows() {
    try {
        $("#tablelist tbody tr").each(function (index) {
            if ($(this).find("td:first").html() == "No matching records found" || 
                $(this).find("td:first").html() == "No data available in table") {
                return;
            } else {
                if ($(this).find("td:first").find("input").length == 0 && (formOrReport == "Form" || formOrReport == "Publish/UnPublish")) {
                    var tdCode = $(this).find("td:eq(0)").html();
                    if (tdCode.indexOf("|") == -1) {
                        $(this).attr("id", tdCode);
                    }
                    else {
                        $(this).attr("keysAttr", tdCode.substring(tdCode.indexOf("|") + 1));
                        tdCode = tdCode.split("|")[0];
                        $(this).attr("id", tdCode);
                    }
                    $(this).find("td:eq(0)").html("<input type='checkbox' id='cb" + index + "&" + tdCode + "'/>");
                    $(this).click(function (event) {
                        row_click(event)
                    });
                }
            
                var row = this;
                $(row).find("td").each(function (idx) {
                    if ($(this).html() == "01/01/1900") {
                        var tdValue = $(this).html();
                        $(this).html("<label style='display:none;'>" + tdValue + "</label>");
                    }
                    //applyKeys(idx, row, this);
                });
            }
        });
    } catch (err) {
        alert(err);
    }
}

// enable date sorting for date columns in the table
function enableDateSorting() {
    try {
        jQuery.extend(jQuery.fn.dataTableExt.oSort, {
            "date-uk-pre": function (a) {
                var ukDatea = a.split('/');
                return (ukDatea[2] + ukDatea[1] + ukDatea[0]) * 1;
            },

            "date-uk-asc": function (a, b) {
                return ((a < b) ? -1 : ((a > b) ? 1 : 0));
            },

            "date-uk-desc": function (a, b) {
                return ((a < b) ? 1 : ((a > b) ? -1 : 0));
            }
        });
    } catch (err) {
        alert(err);
    }
}

// enable table rows reodering
function enableRowsReordering() {
    //$("#tablelist tbody").sortable({
    //    helper: sortableHelper,
    //    stop: updateIndex
    //}).disableSelection();
}

// handle onchange of checkbox in the header of the table
function selectAll(sender) {
    try {
        $('#tablelist tbody tr').each(function () {
            var chk = $(this).find('input[type="checkbox"]');
            $(chk).prop("checked", $(sender).prop("checked"));
        });
        if ($(sender).prop("checked") == true) {
            $('#cmdDelete').removeAttr('disabled');

        }
        else {
            $('#cmdUpdate').attr('disabled', 'disabled');
            $('#cmdDelete').attr('disabled', 'disabled');
        }
    } catch (err) {
        alert(err);
    }
}

//get lead properties details
function getLeadPropertyDetails(leadPropertyCode) {
    Correspondance.GetLeadPropertyDetails(leadPropertyCode, function (val) {
        var data = JSON.parse(val);
        $("#LeadPropertiesDetails input").prop("disabled", true);
        $("#LeadPropertiesDetails textarea").prop("disabled", true);
        $("#txtPropCode").val(data[0]["Code"]);
        $("#txtPropTitle").val(data[0]["Name"]);
        $("#txtPropPurpose").val(data[0]["PurposeName"]);
        $("#txtPropType").val(data[0]["TypeName"]);
        $("#txtPropCity").val(data[0]["CityNam"]);
        $("#txtCommunity").val(data[0]["CommunityName"]);
        $("#txtSubCommunity").val(data[0]["SubCommunityName"]);
        $("#LeadPropertiesDetails").dialog({ modal: true, title: 'Lead Property Details', show: 'slide', width: 600 });
    });
}

// apply dataTable features for the table
function applyDatatable1(val) {
    try {
        val.splice(0, 1);
        $('#tablelist').dataTable({
            destroy: true,
            "data": val,
            "aoColumns": columns,
            "dom": 'CT<"clear">BRlfrtip',
            buttons: [
     { extend: 'copy', text: 'نسخ' }, { extend: 'excel', text: 'اكسل' }, { extend: 'print', text: 'طباعة' }, { extend: 'pdf', text: 'PDF' }
            ], Defs: vColumnDefs,
            "colVis": {
                "exclude": [0],
                "align": "left",
                "restore": "Restore",
                "showAll": "Show all",
                "showNone": "Show none"
            },
            "fnDrawCallback": function (oSettings) {
                addCheckBoxToTableRows();
            },
        }).columnFilter({
            destroy: true,
            aoColumns: filterColumns
        });
    } catch (err) {
        alert(err);
    }
}