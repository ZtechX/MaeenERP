﻿
var deleteWebServiceMethod = "course_registerCls.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "course_registerCls.asmx/Edit";
var formAutoCodeControl = "lblmainid";

$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
    //$("#SavedivLoader").show();
    GetCourses();
    //drawCourseFiles();
    drawConditionsTable();
    checkstudent();

    try {

       
      
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

function GetCourses() {
    try {
        $("#SavedivLoader").show();
        var status = ["جديدة", "حالية", "مكتملة", "معلقة"];
        var badges = ["badge-success", "badge-info", "badge-dark", "badge-warning"];
        var CourseId = $("#Lblcourse_id").html();
      //  alert(CourseId);
        course_registerCls.get_course(CourseId, function (val) {

            //   console.log(val);

            var arr1 = JSON.parse(val[1]);
            $("#course_title").html(arr1[0].name);
            $("#course_details").html(arr1[0].description);
            $("#course_date").html(arr1[0].start_dt_hj);
            if (arr1[0].price > 0) {
                $("#course_price").parent().css("display", "block");
                $("#course_price").html(arr1[0].price);
            }
            else { $("#course_price").parent().css("display", "none"); }
            $("#lect_time").html(arr1[0].lect_duration);
            $("#course_category").html(arr1[0].course_category);
            $("#course_stat").html(status[arr1[0].StatusCourse]);
            $("#course_stat").attr("class", badges[arr1[0].StatusCourse]);

            $("#course_trainer").html(arr1[0].trainer_name);
            $("#course_duration").html(arr1[0].duration);
            $("#tr_Image").attr('src', arr1[0].trainerImage);
            $("#SavedivLoader").hide();

        });
    }
    catch (err) {
        alert(err);
    }
}

function checkstudent() {
    try {
        $("#SavedivLoader").show();
     
        var CourseId = $("#Lblcourse_id").html();
        var code = ($("#lblcode").html());
       
    
        course_registerCls.checkstudentregister( CourseId ,function (val) {
            

            if (val[0] == 5) {
                document.getElementById('btnregister').style.visibility = 'hidden';
                document.getElementById('checkstudentregister').innerHTML = " الدورة مكتملة";

            }
         

                if (val[0] == 4) {
                    window.location.replace("courseDetails?code=" + code)

                }
                if (val[0] == 3) {
                    document.getElementById('btnregister').style.visibility = 'hidden';
                    document.getElementById('checkstudentregister').innerHTML = "  تم حذف الطالب من الدورة";

                }

                //if (val[0] == 1) {
                //    document.getElementById('btnregister').style.visibility = 'hidden';
                //    document.getElementById('checkstudentregister').innerHTML = "طلبك قيد المراجعه";

                //}
                if (val[0] == 2) {
                    document.getElementById('btnregister').style.visibility = 'visible';
                    //document.getElementById('checkstudentregister').innerHTML = " ارسل طلب الالتحاق من جديد ";

                }
            

        });
    }
    catch (err) {
        alert(err);
    }
}


function drawConditionsTable() {
    try {
        var CourseId = ($("#Lblcourse_id").html());
        course_registerCls.get_courseFiles(CourseId, function (val) {


            var data = "";
            console.log(val);
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {
                    //debugger
                    var file_nm = "";
                    var path = element.image;
                    if (path != "" && path != null) {
                        if (path.indexOf("Acadmies_module/images/") != -1) {
                            file_nm = path.split("Acadmies_module/images/")[1];
                        }
                    }
                    data = data + `
                                                     <tr>
                                                      <td>${element.condition} </td>
                                                     <td>
                                                       <li>
                                                        <a href="../${element.image}" download>
                                                            <i class="fa fa-download"></i> 

                                                        </a>
                                                        <span>${file_nm}</span>
                                                    </li>

                                                     </td>

                          
                                                       </tr>
                 
                                                                               
                                                                      
`;
                });


            }
            $("#conditions-table").html(data);
        });
    } catch (err) {
        alert(err);
    }
}
function setconditionID(condID) {
    $("#Lblcondition_id").html(condID);

}

function drawcourseConditions() {
    try {
        debugger
        var CourseId = ($("#Lblcourse_id").html());
        course_registerCls.get_courseFiles(CourseId, function (val) {


            var data = "";
            console.log(val);
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {
                    //debugger
                    //var file_nm = "";
                    //var path = element.upImg;
                    //if (path != "" && path != null) {
                    //    if (path.indexOf("Acadmies_module/images/") != -1) {
                    //        file_nm = path.split("Acadmies_module/images/")[1];
                    //    }
                    //}
                    data = data + `
                                                     <tr>
                                                      <td>${element.condition} </td>
                                                     
                                                <td>
                                   <button type="button" class="btn btn-upload" data-toggle="modal" data-target="#upload_conditionFiles" onclick="setconditionID(${element.id});">
                                                                 upload file
                                                           
                                                            </button>
                                                       </td>
                                 
 
  
                                                      
                                                                            </tr>
                 
                                                                               
                                                                      
`;
                });


              
            }
            $("#action_courseStudents").html(data);
        });
    } catch (err) {
        alert(err);
    }
}


function addcondFile() {

    try {

        debugger
            $("#SavedivLoader").show();

        var conditionId = ($("#Lblcondition_id").html());
            var CourseId = ($("#Lblcourse_id").html());
          
            var fURL = document.getElementById("fileURL4").value;
            var fName = document.getElementById("FName4").value;
        


            var Id = "";

        course_registerCls.addcondFile(Id, CourseId, conditionId, fURL, fName, function (val) {
                if (val == true) {
                    $("#SavedivLoader").hide();
                    alert("تم الحفظ بنجاح");
                    resetDivControls("divFormuploadHMfiles");
                    $("#upload_conditionFiles").modal('hide');
                   //drawcourseConditions();

                  

                } else {
                    alert("لم يتم الحفظ");
                }


            });
      


    } catch (err) {
        alert(err);
    }
}


function sendRequest() {

    try {

        debugger

        if (checkRequired("divformsignin") == 1) {
            alert("ادخل البيانات المطلوبة")
        }

        else {
            $("#SavedivLoader").show();
           
          
            var CourseId = ($("#Lblcourse_id").html());
            var code = ($("#lblcode").html());
          
            var stdREq = $("#studentRequest").val();
           


            var Id = "";
          
            course_registerCls.SaveRegister(Id, CourseId, code, stdREq,  function (val) {
                if (val == true) {
                    $("#SavedivLoader").hide();
                    alert("تم الحفظ بنجاح");
                    $("#register_Course").modal('hide');
                   
                    window.location.replace("coursat.aspx")
                  
                    resetAll();
                    prepareAdd();




                } else {
                    alert("لم يتم الحفظ");
                }


            });
        }


    } catch (err) {
        alert(err);
    }
}

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


function UploadComplete4(sender, args) {

    var fileLength = args.get_length();
    var fileType = args.get_contentType();
    //alert(sender);
    $('#fileURL4').val('Acadmies_module/images/' + args.get_fileName());
    $("#FName4").val(args.get_fileName());

    switch (true) {
        case (fileLength > 1000000):

            fileLength = fileLength / 1000000 + 'MB';
            break;

        case (fileLength < 1000000):

            fileLength = fileLength / 1000000 + 'KB';
            break;

        default:
            fileLength = '1 MB';
            break;
    }
    clearContents(sender);
}

function ClearMe(sender) {
    sender.value = '';
}
function clearContents(sender) {
    { $(sender._element).find('input').val(''); }
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