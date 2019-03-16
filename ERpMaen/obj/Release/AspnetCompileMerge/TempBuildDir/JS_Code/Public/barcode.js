/************************************/
// Created By : Ahmed Nayl 
// Create Date : 11-11-2015
// Description : This file contains all javaScript functions related to barcode
/************************************/

//generate barcode
function generateBarcode() {
    try {
        if ($("#Textbarcode").val() != "") {
            Barcode.GenerateBarcode($("#Textbarcode").val(), function (val) {
                console.log(val);
                $("#divBarcodeDraw").append(val);
            });
           }
        
    } catch (err) {
    alert(err);
    }
}

function generateBarcode() {
    var barcodeType = $("input:radio[name='Barcode']:checked").val();
    if (barcodeType == "Small") {
        getSmallBaecode();
    } else if(barcodeType=="Meduim"){
       // getMeduimBarcode();
        getBarBarcode();
    } else if(barcodeType=="Bars"){
    getBarBarcode();
    } else {
    getSmallBaecode();
    }
}

//get small barcode
function getSmallBaecode() {
    try {
        $("#divMenuimLables").hide();
        $("#divMenuimLables1").hide();
        $("#meduimBarcode").show();
        $("#meduimBarcode").barcode(
       $("#Textbarcode").val(), // Value barcode (dependent on the type of barcode)
       "code128" // type (string)
);
} catch(err) {
alert(err);
}
}
//get small barcode
function getMeduimBarcode(){
    try {
        $("#divMenuimLables").show();
        $("#divMenuimLables1").show();
        $("#lblBarcodeVisitNO").html("VN : " + $("#txtVisitId").val());
        $("#lblBarcodeDB").html("DB : " + $("#tblSubTrans tbody tr:first").find("#lblTestType").html());
        $("#lblBarcodeSp").html("SP : " + $("#txtVisitId").val());
        $("#lblBarcodeSR").html("PID : " + $("#txtPatientId").val());
        $("#lblBarcodePatientName").html("PN : " + $("#txtPatientName").val());
        $("#lblBarcodeCollectedDate").html("Collected : " + $("#txtVisitDateTime").val());
        $("#lblBArcodeCollectedBY").html("Collected BY : " + $("#lblUserName").html());
        $("#meduimBarcode").show();
        $("#meduimBarcode").barcode(
        $("#Textbarcode").val(), // Value barcode (dependent on the type of barcode)
       "code128" // type (string)
);
} catch(err) {
alert(err);
}
}

//get small barcode
function getBarBarcode(){
try{
    $("#divBarcodeDraw").barcode(
     $("#Textbarcode").val(), // Value barcode (dependent on the type of barcode)
       "code128" // type (string)
);
} catch(err) {
alert(err);
}
}


//$(function () {
//    $("#cmdPrint").click(function () {
//        var contents = $("#divMainReport").html();
//        var frame1 = $('<iframe />');
//        frame1[0].name = "frame1";
//        frame1.css({ "position": "absolute", "top": "-1000000px" });
//        $("body").append(frame1);
//        var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
//        frameDoc.document.open();
//       // Create a new HTML document.
//        frameDoc.document.write('<html><head><title>DIV Contents</title>');
//        frameDoc.document.write('</head><body>');
//        //Append the external CSS file.
//        frameDoc.document.write('<link href="Style.css" rel="stylesheet" type="text/css" />');
//        frameDoc.document.write('<link href="Site.css" rel="stylesheet" type="text/css" />');
//       // Append the DIV contents.
//        frameDoc.document.write(contents);
//        frameDoc.document.write('</body></html>');
//        frameDoc.document.close();
//        setTimeout(function () {
//            window.frames["frame1"].focus();
//            window.frames["frame1"].print();
//            frame1.remove();
//        }, 500);
//    });
//});

	function ClientSidePrint(idDiv) 
{
$("#meduimBarcode div").css("height","30");
	    var mywindow = window.open('', 'PRINT', 'height=300,width=600');
	   // mywindow.document.write('<html><head><title></title>');
	  
	    mywindow.document.write('</head><body style="font-family: DroidKufi !important; border:1px solid; color:black !important; dir:rtl; text-align:center !important; margin:4px !important; padding:2px !important;">');
	   // mywindow.document.write('<h1>' + document.title + '</h1>');
	    mywindow.document.write(document.getElementById(idDiv).innerHTML);
	    mywindow.document.write('</body></html>');

	    mywindow.document.close(); // necessary for IE >= 10
	    mywindow.focus(); // necessary for IE >= 10*/

	    mywindow.print();
	    mywindow.close();

	    return true;
	    //window.print();
	        //var contents = window.document.getElementById(idDiv).innerHTML;
	        //var frame1 = $('<iframe />');
	        //frame1[0].name = "frame1";
	        //$("body").append(frame1);
	        //var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
	        //frameDoc.document.open();
	        //frameDoc.document.write('</head><body style="width:100%; font-family: DroidKufi !important;  border:1px solid; dir:rtl; padding:0px; color:black; font-size:10px !important; text-align:right;">');
	      
	        //frameDoc.document.write(contents);
	        //frameDoc.document.write('</body></html>');
	        //frameDoc.document.close();
	        //setTimeout(function () {
	        //    window.frames["frame1"].focus();
	        //    window.frames["frame1"].print();
	        //    frame1.remove();
	        //}, 1000);
        }         