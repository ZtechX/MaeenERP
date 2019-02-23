﻿
var deleteWebServiceMethod = "coursatCls.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "coursatCls.asmx/Edit";
var formAutoCodeControl = "lblmainid";

$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").show();

    try {

        drawCourses();
       
      

    } catch (err) {
        alert(err);
    }
});

function resetAll() {
    try {
        resetFormControls();
        $("#lblmainid").html("");
    
    } catch (err) {
        alert(err);
    }
}

function saveCourse() {

    try {
        
       
        if (!checkRequired()) {
            debugger
            $("#Label1").html($("#txtDatem").val());
            $("#Label2").html($("#txtDateh").val());

            $("#date_m").val($("#txtDatem").val());
            $("#date_hj").val($("#txtDateh").val());
            
            var basicData = generateJSONFromControls("divForm");
      
            var Id = "";
            console.log(basicData);
            coursatCls.Save(Id, basicData, function (val) {
                if (val == true) {
                    debugger;
                    alert("تم الحفظ بنجاح");
                    resetAll();
                    prepareAdd();

                    //drawDynamicTable();
                 

                } else {
                    alert("لم يتم الحفظ");
                }

                //  $("#SavedivLoader").hide();
            });
        }
           
       
    } catch (err) {
        alert(err);
    }
}
//function savefile() {

//    try {

//        $("input").removeClass('error');
//        $("select").removeClass('error');

//        if (Page_ClientValidate("vgroup")) {
//            $("#pnlConfirm").hide();
//            $("#SavedivLoader").show();
//            var basicData = generateJSONFromControls("divForm");
//            var PosId = $("#lblmainid").html();
//            coursat.Save(PosId, basicData, function (val) {
//                if (val == true) {
//                    debugger;
//                    showSuccessMessage("تم الحفظ بنجاح");

//                    //drawDynamicTable();
//                    cancel();

//                } else {
//                    showErrorMessage("لم يتم الحفظ");
//                }

//                $("#SavedivLoader").hide();
//            });


//        }
//    } catch (err) {
//        alert(err);
//    }
//}

function edit(val) {
    debugger
    resetAll();
 
        if (val[0] == "1") {
            var data = JSON.parse(val[1]);
            fillControlsFromJson(data[0]);
           
        }
        $("#pnlConfirm").hide();
        $("#divData").show();
        $("#SavedivLoader").hide();
}

function drawCourses(){
    try {
        coursatCls.get_Courses( "",function (val) {

            
            var data = "";
            console.log(val);
            var arr1 = JSON.parse(val[1]);
      
            arr1.forEach(function (element) {
                data = data + `<div class="col-md-4 col-sm-12">
                    <div class="block">
                        <div class="block-title">
                            <h5><a href="courseDetails.aspx?course_id=${element.id}">${element.name}</a></h5>
                        </div>
                        <div class="block-desc">
                            <p class="desc">${element.description}</p>
                            <div class="row desc-inner">
                                <div class="bock-trainee pull-right">
                                    <img class="avatar" src="../assets/images/101.jpg" />
                                    <span>${element.full_name}</span>
                                </div>
                                <div class="block-date pull-left">
                                    <i class="fa fa-calendar-check-o"></i>
                                    <span>${element.start_dt_m}   </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`;
            });
            //for (var i = 0; i < arr1.length; i++) {

            //data = data + '<div class="col-md-4 col-sm-12"> <div class="block">'+
            //    '<div class="block-title"><h5><a href="courseDetails.aspx">' + arr1[i].name +'</a></h5></div>' +
            //    '<div class="block-desc"> <p class="desc">' + arr1[i].description+'</p>'+
            //            '<div class="row desc-inner"><div class="bock-trainee pull-right">' +
            //                    '<img class="avatar" src="../assets/images/101.jpg" />' +
            //        '<span>'+ arr1[i].trainer_id +' </span>'+' </div>'+
            //               ' <div class="block-date pull-left">'+
            //                   ' <i class="fa fa-calendar-check-o"></i>'+
            //            ' <span>' + arr1[i].start_date_hj+' </span>'+
            //               ' </div></div> </div></div></div>';
            //}
            $("#courses-list").html(data);
        });
           } catch (err) {
        alert(err);
    }
}
function searchCourses() {
    try {
        
            
            var courseName = $("#txt_Search").val();
            coursatCls.get_Courses(courseName, function (val) {
                debugger

                var data = "";
                //console.log(val);
                if (val[0] == "1") {
                    var arr1 = JSON.parse(val[1]);

                    arr1.forEach(function (element) {
                       debugger
                        data = data + `<div class="col-md-4 col-sm-12">
                    <div class="block">
                        <div class="block-title">
                            <h5><a href="courseDetails.aspx?course_id=${element.id}">${element.name}</a></h5>
                        </div>
                        <div class="block-desc">
                            <p class="desc">${element.description}</p>
                            <div class="row desc-inner">
                                <div class="bock-trainee pull-right">
                                    <img class="avatar" src="../assets/images/101.jpg" />
                                    <span>${element.full_name} </span>
                                </div>
                                <div class="block-date pull-left">
                                    <i class="fa fa-calendar-check-o"></i>
                                    <span>${element.end_date_m}   </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`;
                        

                    });


                    $("#courses-list").html(data);
                } else {
                    showErrorMessage("Not Found");
                }

            });
       
    
    } catch (err) {
        alert(err);
    }
}




function add() {
    try {
        prepareAdd();
        resetAll();
      //  getcode();
    } catch (err) {
        alert(err);
    }
}

function setformforupdate() {
    try {
        setformforupdate_all();
    } catch (err) {
        alert(err);
    }
}