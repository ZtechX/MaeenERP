/************************************/
// Created By : Ahmed Nayl
// Create Date : 16/11/2015 11:30 AM
// Description : This file contains all javaScript functions related to Students form 
/************************************/

// load function set defualt values
$(function () {
    try {
       // getStudentPointData();
    } catch (err) {
        alert(err);
    }
});
var studentchartdata;
function getReportData() {
    try {
        getLetters();
    } catch (err) {

    }
}


function getLetters() {
    try {
        var type = $("#ddlType").val();
        var managment = $("#ddlmanagment_id").val();
        var fromDate = $("#txtDateFrom").val();
        var toDate = $("#txtDateTo").val();
        $("#reportDiv").show();
        $('#tblStudentPoints').remove();
        Reports.GetLetters(type,managment,fromDate,toDate, function (val) {
            fillPoints(val[0]);
        });
    } catch (err) {
        alert(err);
    }
}

// 
function fillPoints(data) {
    try {
        var str = "";
        if (data != "") {
            var files = JSON.parse(data);
            console.log(files);
            if (files.length > 0) {
                $('#tblStudentPoints').remove();
                createTable("tblStudentPoints", "divStudentPoints", ["#", "الادارة", "الكود","العنوان", "النوع ","تاريخ الانشاء"]);
                for (i = 0; i < files.length; i++) {
                    appendFileTR(files[i], i);
                }
            }
        }
    } catch (error) {
        alert(error);
    }
}

// 
function appendFileTR(fileDetails, i) {
    try {
       // alert(fileDetails.week);
        var index = parseFloat($('#tblStudentPoints tbody tr').length) + 1;
        var tr = document.createElement('tr');
        $(tr).append('<td><span id="lblPointId" class="index">' + index + '</span>');
        $(tr).append('<td><span id="lblLesson_Name" class="index">' + fileDetails.managmentname + '</span>');
        $(tr).append('<td><span id="lblStage_Name" class="index">' + fileDetails.barcode + '</span>');
        $(tr).append('<td><span id="lblStudent_Name" class="index">' + fileDetails.title + '</span>');
        $(tr).append('<td><span id="lblStudent_Name" class="index">' + fileDetails.typename + '</span>');
        $(tr).append('<td><span id="lblStudent_Name" class="index">' + fileDetails.create_date + '</span>');

        $('#tblStudentPoints tbody').append(tr);
    } catch (err) {
        alert(err);
    }
}

function drawChart() {
    var dataArray = [['الطلاب', 'النقاط المكتسبة','النقاط المخصومة', 'مجموع النقاط']];
    for (g = 0; g < studentchartdata.length; g++) {
        dataArray.push([studentchartdata[g].Student_Name,parseFloat(studentchartdata[g].Add),parseFloat(studentchartdata[g].sub),parseFloat(studentchartdata[g].Total)]);
    }
    var data = google.visualization.arrayToDataTable(dataArray);
        var options = {
            width: 900,
            height: 400,
            legend: { position: 'top', maxLines: 3 },
            bar: { groupWidth: '75%' },
            isStacked: true,
        };
        var chart = new google.visualization.ColumnChart(document.getElementById("columnchart_values"));
        chart.draw(data, options);
    }



function getTeacherName() {
    try {
        Reports.GetTeacherName($("#ddlLessons").val(), function (val) {
            $("#lblteacherName").html(val);
        });
    } catch (err) {
        alert(err);
    }
}

function printReport() {
    try {
        window.print();
    } catch (err) {
        alert(err);
    }
}

function ClientSidePrint1(idDiv) {

    var contents = window.document.getElementById(idDiv).innerHTML;
    var frame1 = $('<iframe />');
    frame1[0].name = "frame1";
    frame1.css({ "position": "absolute", "top": "-1000000px" });
    $("body").append(frame1);
    var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
    frameDoc.document.open();
    // Create a new HTML document.
    //frameDoc.document.write('<html><head><title>DIV Contents</title>');
    //frameDoc.document.write('</head><body>');
    //Append the external CSS file.
    frameDoc.document.write('<link href="../UserControls/style.css" rel="stylesheet" type="text/css" />');
    //frameDoc.document.write('<link href="Site.css" rel="stylesheet" type="text/css" />');
    // Append the DIV contents.
    frameDoc.document.write(contents);
    //frameDoc.document.write('</body></html>');
    frameDoc.document.close();
    setTimeout(function () {
        window.frames["frame1"].focus();
        window.frames["frame1"].print();
        frame1.remove();
    }, 500);
}

function fnExcelReport() {
    var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
    var textRange; var j = 0;
    tab = document.getElementById('tblStudentPoints'); // id of table

    for (j = 0 ; j < tab.rows.length ; j++) {
        tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
        //tab_text=tab_text+"</tr>";
    }

    tab_text = tab_text + "</table>";
    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
    tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
    {
        txtArea1.document.open("txt/html", "replace");
        txtArea1.document.write(tab_text);
        txtArea1.document.close();
        txtArea1.focus();
        sa = txtArea1.document.execCommand("SaveAs", true, "Say Thanks to Sumit.xls");
    }
    else                 //other browser not tested on IE 11
        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

    return (sa);
}

