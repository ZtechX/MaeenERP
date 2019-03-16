/************************************/
// Created By : Ahmed Nayl
// Create Date : 16/6/2015 10:00 PM
// Description : This file contains all javaScript functions resopsable for uploading multi files
/************************************/

// called when file uploaded success
function onSuccessFileUpload(file) {
    try {
        
        var val = file.split("|")[0];
        var k = file.split("|")[1];
        var container = document.createElement('div');
        var cmdDelete = document.createElement('input');
        var img = document.createElement('img');
        var x = val;
        if (val == '') { alert('Error uploading photos'); return false; }
        //Here I create the Button cmdDelete
        cmdDelete.id = 'cmd' + val;
        cmdDelete.type = 'button';
        cmdDelete.value = 'x';
        cmdDelete.className += 'delete-photo';
        cmdDelete.addEventListener('click', function () {
            deleteUploadedFile(file);
        });
        img.id = 'img' + val;
        img.src = val;
        img.className += 'uploadedPhoto';
        container.id = 'divPhotos' + val;
        container.className += 'photoWrap';
        container.appendChild(cmdDelete);
        container.appendChild(img);
        var div = document.getElementById('MainDiv');
        div.appendChild(container);
       // setIcon(img, val);
        setTimeout(function () { document.getElementById('MainDiv').style.display = 'block'; }, 5000);
    } catch (err) {
        alert(err);
    }
}

function onSuccessFileUploadNew(file) {
    try {
        var val = file.split("|")[0];
        var k = file.split("|")[1];
        var container = document.createElement('div');
        var cmdDelete = document.createElement('input');
        var img = document.createElement('img');
        var x = val;
        if (val == '') { alert('Error uploading photos'); return false; }
        //Here I create the Button cmdDelete
        cmdDelete.id = 'cmd' + val;
        cmdDelete.type = 'button';
        cmdDelete.value = 'x';
        cmdDelete.className += 'delete-photo';
        cmdDelete.addEventListener('click', function () {
            deleteUploadedFile(file);
        });
        img.id = 'img' + val;
        img.src = val;
        img.className += 'uploadedPhoto';
        container.id = 'divPhotos' + val;
        container.className += 'photoWrapNew';
        container.appendChild(cmdDelete);
        container.appendChild(img);
        var div = document.getElementById('MainDivNew');
        div.appendChild(container);
        // setIcon(img, val);
        setTimeout(function () { document.getElementById('MainDivNew').style.display = 'block'; }, 5000);
    } catch (err) {
        alert(err);
    }
}
// called when delete file success
function onSuccessDeletedFile(file) {
    try {
        var val = file.split("|")[0];
        var k = file.split("|")[1];
        if (val != '') {
            var par = document.getElementById('MainDiv');
            var child = document.getElementById('divPhotos' + val);
            par.removeChild(child);
            $("#AjaxFileUpload1_FileItemContainer_" + k + "").remove();

            return false;
        }
    } catch (err) {
        alert(err);
    }
}

// delete file row from uploaded files table
function deleteFileFromGrid(control) {
    try {
        deleteRowFromTable(control);
        countFilesNumber();
    }
    catch (error) {
        alert(error);
    }
}

// delete row from table
function deleteRowFromTable(control) {
    try {
        var r = confirm("are you sure you want to delete the records");
        if (r == true) {
            $(control).closest("tr").hide();
            $(control).closest("tr").find('td .index').removeClass('index');
            orderUploadedFilesIndex();
            $(control).val('Deleted');
        }
    }
    catch (error) {
        alert(error);
    }
}

// order uploaded files indecies
function orderUploadedFilesIndex() {
    try {
        $('#tblUploadedFiles tbody tr td .index').each(function (i) {
            $(this).html(i + 1);
        });
    } catch (err) {
        alert(err);
    }
    
}

// count number of files uploaded
function countFilesNumber() {
    try {
        countNotDeletedRows("tblUploadedFiles", "txtUploadedFiles");
    } catch (err) {
        alert(err);
    }
}


// count number of files uploaded
function countFilesNumberNew() {
    try {
        countNotDeletedRows("tblUploadedFilesNew", "txtUploadedFilesNew");
    } catch (err) {
        alert(err);
    }
}

// count number of not deleted rows
function countNotDeletedRows(tableId, txtId) {
    try {
        var count = 0;
        $("#" + tableId + " tbody tr").each(function () {
            if ($(this).find('#cmdDelete').val() != "Deleted") {
                count = count + 1;
            }
        });
        $("#" + txtId).val(count);
    } catch (err) {
        alert(err);
    }
}

// create table for uploaded files
function createTblUploadedFiles(cols) {
    try {
        if ($("#tblUploadedFiles").length >= 1) {
            $('#tblUploadedFiles tbody tr').remove();
        } else {
            var tableId = "tblUploadedFiles";
            var divId = "divUploadedFiles";
            var tbl = document.createElement('table');
            tbl.border = 3;
            tbl.id = tableId;
            tbl.className = 'table table-bordered table-condensed table-fixed-head';
            var header = "<thead><tr>";
            for (i = 0; i < cols.length; i++) {
                if (cols[i] == "Show in xml" || cols[i] == "Water Mark" || cols[i] == "Contanct Image" || cols[i] == "Brochure Image" || cols[i] == "WebSite Image") {
                    header += "<th><input colName=" + cols[i] + " type='checkbox' onchange='checkAllForUploadedFiles(this);'></input> " + cols[i] + "</th>";
                } else {
                    header += "<th>" + cols[i] + "</th>";
                }
            }
            header += "</tr></thead>"
            $(tbl).append(header);
            $(tbl).append('<tbody></tbody>');
            $("#" + divId).append(tbl);
        }
    }
    catch (error) {
        alert(error);
        return false;
    }
}

// create table for uploaded files
function createTblUploadedFilesNew(cols) {
    try {

        var tableId = "tblUploadedFilesNew";
        var divId = "divUploadedFilesNew";
        var tbl = document.createElement('table');
        tbl.border = 3;
        tbl.id = tableId;
        tbl.className = 'table table-bordered table-condensed table-fixed-head';
        var header = "<thead><tr>";
        for (i = 0; i < cols.length; i++) {
            if (cols[i] == "Show in xml" || cols[i] == "Water Mark" || cols[i] == "Contanct Image" || cols[i] == "Brochure Image" || cols[i] == "WebSite Image") {
                header += "<th><input colName=" + cols[i] + " type='checkbox' onchange='checkAllForUploadedFiles(this);'></input> " + cols[i] + "</th>";
            } else {
                header += "<th>" + cols[i] + "</th>";
            }
        }
        header += "</tr></thead>"
        $(tbl).append(header);
        $(tbl).append('<tbody></tbody>');
        $("#" + divId).append(tbl);
    }
    catch (error) {
        alert(error);
        return false;
    }
}
// create table
function createTable(tableId, divId, cols) {
    try {
        var tbl = document.createElement('table');
        tbl.border = 3;
        tbl.id = tableId;
        tbl.className = 'table table-bordered table-condensed table-fixed-head';
        var header = "<thead><tr>";
        for (i = 0; i < cols.length; i++) {
            header += "<th>" + cols[i] + "</th>";
        }
        header += "</tr></thead>"
        $(tbl).append(header);
        $(tbl).append('<tbody></tbody>');
        $("#" + divId).append(tbl);
    }
    catch (error) {
        alert(error);
    }
}

// show the table of uploaded files
function showUploadedFilesTable(sender) {
    try {
        showTable("tblUploadedFiles", "divUploadedFiles", "Uploaded Files", sender);
    } catch (err) {
        alert(err);
    }
}

// show the table of uploaded files
function showUploadedFilesTableNew(sender) {
    try {
        showTable("tblUploadedFilesNew", "divUploadedFilesNew", "Uploaded Files", sender);
    } catch (err) {
        alert(err);
    }
}

// show table
function showTable(tableId, divId, title, sender) {
    try {
       
        if ($(sender).val() != "" && $(sender).val() != "0") {
            $("#" + divId).dialog({ modal: true, title: title, show: 'slide', width: 1000 });
            $("#" + tableId + " tbody").sortable({
                //helper: sortableHelper,
                //stop: updateIndex
            });
        }
    } catch (err) {
        alert(err);
    }
}

// show suitable icon to file uploaded
function setIcon(img, val) {
    try {
        var type = val.split(".").pop();
        switch (type) {
            case "doc": case "docx":
                img.src = '../images/word.png'; break;
            case "xls": case "xlsx":
                img.src = '../images/icon-xlsx.png'; break;
            case "pdf":
                img.src = '../images/pdf_icon.jpg'; break;
            case "txt":
                img.src = '../images/icon-text.gif'; break;
            case "jpg": case "jpeg": case "png":
                img.src = val; break;
            case "mp4": case "MP4":
                img.src = '../images/mp4.jpg'; break;
            case "":
                img.src = '../images/noDoc.jpg'; break;
            default:
                img.src = val; alert("Error at document type"); break;
        }
    } catch (err) {
        alert(err);
    }
}

// show images slider 
function showImageSlider() {
    try {
        $("#dvImageSlider").empty();
        $("#olImageSlider").empty();
        var i = 0;
        $("#tblUploadedFiles tbody tr").each(function () {
            if ($(this).find('#cmdDelete').val() != "Deleted") {
                if (i == 0) {
                    $("#olImageSlider").append("<li data-target='#carousel-example-generic1' data-slide-to=" + i + " class='active'></li>");
                    $("#dvImageSlider").append("<div class='item active'><img src=" + $(this).find('img').attr("id") + " alt='...'></img></div>");
                    i = i + 1;
                }
                else {
                    $("#olImageSlider").append("<li data-target='#carousel-example-generic1' data-slide-to=" + i + "></li>");
                    $("#dvImageSlider").append("<div class='item'><img src=" + $(this).find('img').attr("id") + " alt='...'></img></div>");
                    i = i + 1;
                }
            }
        });
        $("#pnlImageSlider").dialog({ modal: true, title: 'Uploaded Photos', show: 'slide', width: 950 });
    } catch (err) {
        alert(err);
    }
}