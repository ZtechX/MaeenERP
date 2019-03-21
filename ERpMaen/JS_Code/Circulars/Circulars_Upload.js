/************************************/
// Created By : Mostafa Abdelghffar
// Create Date : 22/9/2015 10:00 AM
// Description : This file contains all javaScript functions resopsable for uploading files for deals
/************************************/

// called when uploaded complete
function onUploadFileComplete(sender, args) {
    try {
        document.getElementById('MainDiv').style.display = 'none';
        var folders = "Letter_Module/Images"
        MultiFileUploader.UploadFile(args, folders, onSuccessFileUpload);
    } catch (err) {
        alert(err);
    }
}

// called when uploaded complete
function onUploadFileCompleteNew(sender, args) {
    try {
        document.getElementById('MainDivNew').style.display = 'none';
        var folders = "Letter_Module/Images"
        MultiFileUploader.UploadFile(args, folders, onSuccessFileUploadNew);
    } catch (err) {
        alert(err);
    }
}

// delete file
function deleteUploadedFile(val) {
    try {
        MultiFileUploader.DeleteFile(val, "Letter_Module", onSuccessDeletedFile);
    } catch (err) {
        alert(err);
    }
}

// append uploaded files to html table after click ok
function appendUploadedFiles() {
    try {
        var count = 0;
        if ($('.photoWrap img').length > 0) {
            if ($('#tblUploadedFiles').length == 0) {
                createTblUploadedFiles(["Index", "File", "Name", "Delete"]);
            }
            $('.photoWrap img').each(function (i) {
                var fileDetails = {
                    "Index": i, "Image_id": "", "Image_path": this.src, "Image_name": this.id.substring(this.id.lastIndexOf('/') + 1).split(".")[0],
                    //  "MainImage": false
                };
                appendFileTR(fileDetails, i);
            });
            // remove all .photoWrap div that contain files preview
            $('.photoWrap').remove();
            countFilesNumber();
        }
    } catch (err) {
        alert(err);
    }
}

// append uploaded files to html table after click ok
function appendUploadedFilesNew() {
    try {
        var count = 0;
        if ($('.photoWrapNew img').length > 0) {
            if ($('#tblUploadedFilesNew').length == 0) {
                createTblUploadedFilesNew(["Index", "File", "Name", "Delete"]);
            }
            $('.photoWrapNew img').each(function (i) {
                var fileDetails = {
                    "Index": i, "Image_id": "", "Image_path": this.src, "Image_name": this.id.substring(this.id.lastIndexOf('/') + 1).split(".")[0],
                    // "MainImage": false
                };
                appendFileTRNew(fileDetails, i);
            });
            // remove all .photoWrap div that contain files preview
            $('.photoWrapNew').remove();
            countFilesNumberNew();
        }
    } catch (err) {
        alert(err);
    }
}

// append new uploaded images to image table
function appendFileTR(fileDetails, i) {
    try {
        var index = $('#tblUploadedFiles tbody tr').length;
        var tr = document.createElement('tr');
        $(tr).append('<td><span id="lblOrderId" class="index">' + i + '</span>');
        $(tr).append('<td><span id="lblFileId" style="display:none;">' + fileDetails.Image_id + '</span><img  onclick="showImageSlider();" id="' + fileDetails.Image_path + '" src="' + fileDetails.Image_path + '" width="40"></img></td>');
        $(tr).append('<td><input id="txtTitle" value="' + fileDetails.Image_name + '"/></td>');
        //if (fileDetails.MainImage) {
        //    $(tr).append('<td><input type="checkbox" onchange="changeMainImage(this);" id="chkMainImage" checked="checked"></input></td>');
        //} else {
        //    $(tr).append('<td><input type="checkbox" onchange="changeMainImage(this);" id="chkMainImage"></input></td>');
        //}
        $(tr).append('<td><input id="cmdDelete" onclick="deleteFileFromGrid(this); return false;"  type="button" value="Delete" class="glyphicon glyphicon-trash btn btn-primary"></input></td>');
        $('#tblUploadedFiles tbody').append(tr);
    } catch (err) {
        alert(err);
    }
}

// append new uploaded images to image table
function appendFileTRNew(fileDetails, i) {
    try {
        var index = $('#tblUploadedFilesNew tbody tr').length;
        var tr = document.createElement('tr');
        $(tr).append('<td><span id="lblOrderId" class="index">' + fileDetails.Index + '</span>');
        $(tr).append('<td><span id="lblFileId" style="display:none;">' + fileDetails.Image_id + '</span><img  onclick="showImageSlider();" id="' + fileDetails.Image_path + '" src="' + fileDetails.Image_path + '" width="40"></img></td>');
        $(tr).append('<td><input id="txtTitle" value="' + fileDetails.Image_name + '"/></td>');
        //if (fileDetails.MainImage) {
        //    $(tr).append('<td><input type="checkbox" onchange="changeMainImage(this);" id="chkMainImage" checked="checked"></input></td>');
        //} else {
        //    $(tr).append('<td><input type="checkbox" onchange="changeMainImage(this);" id="chkMainImage"></input></td>');
        //}
        $(tr).append('<td><input id="cmdDelete" onclick="deleteFileFromGrid(this); return false;"  type="button" value="Delete" class="glyphicon glyphicon-trash btn btn-primary"></input></td>');
        $('#tblUploadedFilesNew tbody').append(tr);

    } catch (err) {
        alert(err);
    }
}
// change main image
function changeMainImage(sender) {
    try {
        var value = $(sender).prop('checked');
        $("#tblUploadedFiles #chkMainImage").each(function () {
            $(this).prop('checked', false);
        });
        if (value == true) {
            $(sender).closest('#chkMainImage').prop('checked', true);
        }
    } catch (err) {
        alert(err);
    }
}