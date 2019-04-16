
//var deleteWebServiceMethod = "coursatCls.asmx/Delete";
// global variable used in row_click and update functions
//var editWebServiceMethod = "coursatCls.asmx/Edit";
var formAutoCodeControl = "lblmainid";


$(function () {

    try {
        $("#pnlConfirm").hide();
        $("#divData").hide();
      //  $("#SavedivLoader").show();


       
        GetCourses();
        drawLecturesTable();
        drawStudentTable();
        drawNotes();
        drawActivity();
        drawConditionsTable();
      //  Studentlistview();

        drawCourseComments();
        drawHomeworks();
        drawExams();
        drawCourseFile();
    
        drawstudentHomeworkTable();
        drawstudentExamsTable();
       
    

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


function drawCourseComments() {
    try {
        $("#SavedivLoader").show();
        var CourseId = ($("#Lblcourse_id").html());
        courseDetails_archivedCls.get_courseComments(CourseId, function (val) {


            var data = "";
            console.log(val);
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {



                    data = data + `
                 <ul>

                                                        <li>
                                                            <div class="user">
                                                                <div class="usr-img">
                                                                    <img src="../assets/images/104.jpg">
                                                                </div>
                                                                <div class="usr-data">
                                                                    <h3>
                                                                        <a href="#"> ${element.username}</a>
                                                                    </h3>
                                                                    <span>
                                                                        <i class="zmdi zmdi-calendar"></i>
                                                                      ${element.date_hj}
                                                                </span>
                                                                    <p>${element.comment} </p>
                                                                </div>
                                                            </div>
                                                        </li>
</u>
                                                                                                             
                                                                      
`;
                });

            }
            $("#divformComments").html(data);
            $("#SavedivLoader").hide();
        });
    } catch (err) {
        alert(err);
    }
}



function drawpublicDegreeTable() {
    try {
        debugger
        var CourseId = ($("#Lblcourse_id").html());
        

        courseDetails_archivedCls.get_pub_StudentsDegree(CourseId, function (val) {

            console.log(val);
            var data = "";
            if (val[0] == 1) {
                var arrstudent = JSON.parse(val[1]);

                arrstudent.forEach(function (element) {


                    data = data + `   <tr>
                                    <td>
                                        <label id=" ${element.student_id}"> ${element.studentname}</label>
                                    </td>
                                    <td>
               <input id="finaldegee" type="text" value=" ${element.final}"   />
                                   
                                    </td>
                                    <td>
               <input id="activitydegee" type="text" value=" ${element.activityDegree}" />
                                    
                                    </td>
                                </tr> `


                });
            }

            $("#pblcstudentdegrees").html(data);
            $("#publicStudentDegree").modal();



        });

    }



    catch (err) {
        alert(err);
    }
}





function drawstudenthomeworkanswers(homeworkId) {
    try {
        //debugger
        var CourseId = ($("#Lblcourse_id").html());
        ($("#LblHomework_id").html(homeworkId));


        courseDetails_archivedCls.get_StudentsHomeworkAnswers(homeworkId, CourseId, function (val) {

            console.log(val);
            var data = "";
            if (val[0] == 1) {
                //  debugger
                var arrstudent = JSON.parse(val[1]);

                arrstudent.forEach(function (element) {
                    //    debugger

                    var filename = "";
                    var path = element.homeworkanswer;
                    if (path != "" && path != null) {
                        // debugger
                        if (path.indexOf("Acadmies_module/coursefiles/") != -1) {
                            filename = path.split("Acadmies_module/coursefiles/")[1];
                        }
                    }

                    data = data + `   <tr>
                                    <td>
                                        <label id=" ${element.student_id}"> ${element.studentname}</label>

                                    </td>
                                  
                                                <td>
                                                       <li>
                                                        <a id="homeworkfile" href="../${element.homeworkanswer}" download>
                                                            <i class="fa fa-download"></i>   

                                                        </a>
                                                        <span>${filename}</span>
                                      
                                                    </li>

                                                     </td>

                                           <td>
                         <input id="Homeworkdegee" type="text" value=" ${element.HMWDegree}" />
                                    
                                    </td>
                                </tr> `


                });
            }

            $("#studentHWAnswers").html(data);
            $("#studentHWAnswers").modal();



        });

    }



    catch (err) {
        alert(err);
    }
}


function drawstudentExamsanswers(ExamId) {
    try {
        //debugger
        var CourseId = ($("#Lblcourse_id").html());
        ($("#LblExam_id").html(ExamId));


        courseDetails_archivedCls.get_StudentsExamsAnswers(ExamId, CourseId, function (val) {

            console.log(val);
            var data = "";
            if (val[0] == 1) {
                debugger
                var arrstudent = JSON.parse(val[1]);

                arrstudent.forEach(function (element) {
                    debugger

                    var filename = "";
                    var path = element.homeworkanswer;
                    if (path != "" && path != null) {
                        debugger
                        if (path.indexOf("Acadmies_module/coursefiles/") != -1) {
                            filename = path.split("Acadmies_module/coursefiles/")[1];
                        }
                    }

                    data = data + `   <tr>
                                    <td>
                                        <label id=" ${element.student_id}"> ${element.studentname}</label>

                                    </td>
                                  
                                                <td>
                                                       <li>
                                                        <a id="homeworkfile" href="../${element.homeworkanswer}" download>
                                                            <i class="fa fa-download"></i>   

                                                        </a>
                                                        <span>${filename}</span>
                                      
                                                    </li>

                                                     </td>

                                           <td>
                         <input id="Homeworkdegee" type="text" value=" ${element.HMWDegree}" />
                                    
                                    </td>
                                </tr> `


                });
            }

            $("#studentExamAnswers").html(data);
            $("#StudentExamskAnswers").modal();



        });

    }



    catch (err) {
        alert(err);
    }
}


function unarchivecourse() {
    debugger
    if (checkRequired("divformunarchive") == 1) {
        alert("يرجى ادخال سبب ازالة الدورة من الارشيف ");
    }
    else {
        $("#SavedivLoader").show();

        var basicData = generateJSONFromControls("divformunarchive");

        var f = confirm("  هل  تريد ازالةالدورة من الارشيف");
        if (f == true) {
            var CourseId = ($("#Lblcourse_id").html());


            courseDetails_archivedCls.unArchive_course(CourseId, basicData, function (val) {

                if (val[0] == 1) {
                    $("#SavedivLoader").hide();
                    alert("تم حذف الدورة من الارشيف    ");
                    window.location.replace("archived_courses.aspx");
                    $("#unarchivecourse_modal").modal('hide');

                } else {
                    alert("لم يتم الحذف");
                    $("#unarchivecourse_modal").modal('hide');
                }

            });
        }

    }
}


function CourseView() {
    var CourseId = ($("#Lblcourse_id").html());
    courseDetails_archivedCls.Edit(CourseId, function (val) {
        if (val[0] == "1") {
            

            
            var data = JSON.parse(val[1]);
            fillControlsFromJson(data[0]);
            //start
            $("#divdate6 #txtDatem").val(data[0].start_dt_m);
            $("#divdate6 #txtDateh").val(data[0].start_dt_hj);
            //end
            $("#divdate7 #txtDatem").val(data[0].end_dt_m);
            $("#divdate7 #txtDateh").val(data[0].end_dt_hj);
          
            $("#EditCourseModal").modal();
          

           
            
        }


    });
    
}




function viewstudDegrees(studentID) {
    debugger
    var CourseId = $("#Lblcourse_id").html(); 

    courseDetails_archivedCls.EditstudDegree(CourseId, studentID, function (val) {
        if (val[0] == "1") {
            var data = JSON.parse(val[1]);
            fillControlsFromJson(data[0]);


            //$("#order_addLec").modal();

        }


    });
}



function studentSearch() {

    try {

        var CourseId = ($("#Lblcourse_id").html()); 
        var StudentName = $("#txt_Search").val();
        courseDetails_archivedCls.get_student(StudentName, CourseId, function (val) {
           
            var data = "";
            //console.log(val);
            if (val[0] == "1") {
                var arr1 = JSON.parse(val[1]);
                
                arr1.forEach(function (element) {
                    
                    data = data + `
                                   <table id="student">

                                                    <tr>
                                                        <td><img src="${element.srImage}"/></td>

                                                        <td>

                                                            <label>${element.studentName}</label>
                                                                          


                                                        </td>
                                                        <td>
                                                             <div class="btn-group pull-left"  style="position:absolute; margin-bottom:5px;">
                                                                                        <button type="button" class="btn btn-info dropdown-toggle btn-xs" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                         
                                                                                            <i class="fa fa-cogs"></i>
      
                                                                                        </button>
                                                                                        <ul class="dropdown-menu">
                                                                                           
                                     
                                                                                               
                                                                                                <li>
                                                                                            <a   data-toggle="modal" href="#studentDegrees" onclick= "Setstudentid(${element.student_id});">
                                                                                                الدرجات 
                                                                                            </a>
                                                                                                    </li>
                                                                                              
                                                                                                </ul>
                                                                                           
                                                                                        </div>
                                                                                          

                                                        </td>

                                                    </tr>
                                                  
                                                </table>`;


                });


                $("#StudentTable").html(data);
            }
            else {
            $("#StudentTable").html("لا يوجد طالب بهذا الاسم");
            }

        });


    } catch (err) {
        alert(err);
    }
}

function searchAbsence() {

    try {

        var lec_id = ($("#LblLecture_id").html());
       
        var CourseId = ($("#Lblcourse_id").html());
        var StudentName = ($("#txtAbsen_Search").val());
        courseDetails_archivedCls.get_srchstudentABS(StudentName, lec_id,CourseId, function (val) {

         

            //console.log(val);
            var data = "";
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);


                if (val[2] != "") {
                    var arr = JSON.parse(val[2]);
                }
                arr1.forEach(function (element) {
                    var check = "";
                    if (val[2] != "") {
                        $.map(arr, function (elementOfArray) {
                            if (elementOfArray.student_id == element.student_id) {
                                check = "checked";
                            }
                            //  else { check = ""; }
                        });
                    }
                    data = data + `   <tr >
                                    <td><label> ${element.CourseStudents}</label> </td>
                                    <td>  <input type="checkbox" ${check} studentAbs="${element.student_id}" style="width: 50px; height:20px;"  /></td>
                                </tr>                                                       `;
                });
                
                $("#absenscetable").html(data);
            }
        
            else {
                $("#absenscetable").html("لا يوجد طالب بهذا الاسم");
            }
            
        });


    } catch (err) {
        alert(err);
    }
}


function setLectId(lecutreId) {
   $("#LblLecture_id").html(lecutreId);
}

function setHomWID(hwID) {
    $("#LblHomework_id").html(hwID);
}

//

function setExamID(examID) {
    $("#LblExam_id").html(examID);
}

function setFLectId(lecID) {
    $("#LblLecture_id").html(lecID);
}
function Setstudentid(StudentId) {
    $("#lblStudentID").html(StudentId);
    //$("#note_edit").modal();
}

function setHomewId(hmwId) {
    $("#LblHomework_id").html(hmwId);
}
function setExamtId(examId) {
    $("#LblExam_id").html(examId);
}

function edit(val) {
   
    resetAll();
 
        if (val[0] == "1") {
            var data = JSON.parse(val[1]);
            fillControlsFromJson(data[0]);
           
        }
        $("#pnlConfirm").hide();
        $("#divData").show();
        $("#SavedivLoader").hide();
}

function GetCourses(){
    try {
        $("#SavedivLoader").show();
        var status = ["جديدة","حالية","مكتملة","معلقة"];
        var badges = [ "badge-dark"];
        var CourseId = $("#Lblcourse_id").html(); 
        //alert(CourseId);
        courseDetails_archivedCls.get_course(CourseId, function (val) {

         //   console.log(val);
            
            var arr1 = JSON.parse(val[1]);
            $("#course_title").html(arr1[0].name);
            $("#course_details").html(arr1[0].description);
            $("#course_date").html(arr1[0].start_dt_hj);
            if (arr1[0].price > 0) {
                $("#course_price").parent().css("display", "block");
                $("#course_price").html(arr1[0].price);
            }
            else { $("#course_price").parent().css("display", "none");}
            $("#lect_time").html(arr1[0].lect_duration);
            $("#course_category").html(arr1[0].course_category);
            $("#course_stat").html("ارشيف");
            $("#course_stat").attr("class","badge-dark");

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
function drawAbsenceTable(CoursLecID) {
    try {
        var CourseId = ($("#Lblcourse_id").html());
        $("#LblLecture_id").html(CoursLecID);
        $("#SavedivLoader").show();
        courseDetails_archivedCls.get_CourseStudents(CoursLecID,CourseId, function (val) {

            console.log(val);
            var data = "";
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);
              
             
                if (val[2] != "") {
                    var arr = JSON.parse(val[2]);
                }
                arr1.forEach(function (element) {
                    var check = "";
                        if (val[2] != "") {
                            $.map(arr, function (elementOfArray) {
                                if (elementOfArray.student_id == element.student_id) {
                                    check = "checked";
                                }
                              //  else { check = ""; }
                            });
                        }
                    data = data + `   <tr >
                                    <td><label> ${element.CourseStudents}</label> </td>
                                    <td>  <input type="checkbox" ${check} studentAbs="${element.student_id}" style="width: 50px; height:20px;"  /></td>
                                </tr>                                                       `;
                });


            }
            $("#absenscetable").html(data);
            $("#absenscetable").modal();
            $("#SavedivLoader").hide();
        });
    } catch (err) {
        alert(err);
    }
}
Number.prototype.padDigit = function () {
    return (this < 10) ? '0' + this : this;
}
function count_endtime(start_time) {
    
    var lect_time = $("#lect_time").html();
    var t1 = start_time.split(":");
    console.log(t1);
    var t2 = t1[1].split(" ");
    console.log(t2);
    mins = Number(t2[0]) + Number(lect_time);
    minhrs = Math.floor(parseInt(mins / 60));
    hrs = Number(t1[0]) + minhrs;
    mins = mins % 60;
    console.log(lect_time);
    console.log(mins);
    console.log(minhrs);
    console.log(hrs);
    var tm = t2[1];
    if (hrs > 12) {
        hrs = hrs - 12;
        if (t2[1] == "am")
            tm = "pm";
        else
            tm = "am";
    }
    return ( hrs.padDigit() + ':' + mins.padDigit() + " " + tm);
}
function drawLecturesTable() {
    try {
       // $("#SavedivLoader").show();
        var CourseId = $("#Lblcourse_id").html();
        courseDetails_archivedCls.get_LectureTable(CourseId, function (val) {
          
            var data = "";
            console.log(val);
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {
                    var end_time = count_endtime(element.start_time);
                 
                    data = data + `<tr>
                                                      <td>${element.lecture_code} </td>
                                                     <td>${element.date_hj}  </td>
                                                    <td>${element.start_time} الى  ${end_time}   </td>
                                                       <td> ${element.hallNum} </td> `;
                    action = ` <td>

                        <div class="btn-group pull-left" style="position:absolute">
                            <button type="button" class="btn btn-info dropdown-toggle btn-xs" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                <i class="fa fa-cogs"></i>

                            </button>
                            <ul class="dropdown-menu">

                                    <li>
                                        <a data-toggle="modal" href="#absenceModal" onclick="drawAbsenceTable(${element.id});">
                                            الغياب
                                                                           </a>
                                    </li>
                                    
                                                                       </ul>
                                                                                           
                                                                         </div>
                                                                                           
                                                                          </td>`;
                    if (typeof element.absence === 'undefined') {
                        data = data + action + '</tr>';
                    }
                    else {
                        switch (element.absence ) {
                            default:
                                text = "--";
                                break;
                            case true:
                                text = "<i class=' success fa fa-check'><i/>";
                                break;
                            case false:
                                text = "<i class='danger fa fa-times'><i/>";
                        }
                        data = data + '<td align="center">' + text +'</td> </tr>';
                    }
                   
                });

              
            }
            $("#lectures-table").html(data);
            $("#SavedivLoader").hide();
        });
    } catch (err) {
        alert(err);
    }
}



function drawstudentHomeworkTable() {
    // draw homework table for student
    try {
        //debugger
        // $("#SavedivLoader").show();
        var CourseId = $("#Lblcourse_id").html();
        courseDetails_archivedCls.get_homeworkTable(CourseId, function (val) {

            var data = "";
            console.log(val);
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {
                    var degree = element.degree;
                    if (degree == 0) {
                        degree = "--";
                    }
                    var file_nm = "";
                    var path = element.image;
                    if (path != "" && path != null) {
                        if (path.indexOf("Acadmies_module/homework/") != -1) {
                            file_nm = path.split("Acadmies_module/homework/")[1];
                        }
                    }
                   
                    data = data + `
                                                    <tr>
                                                                                <td> ${element.title}  </td>
                                                                                <td>${element.details}   </td>
                                                                                <td>
                                                    <a href="../${element.image}" download>
                                                            <i class="fa fa-download"></i>  ${file_nm}

                                                        </a>
                                                       
                                                       </td>
                                                                                <td>
                                   <button type="button" class="btn btn-upload" data-toggle="modal" data-target="#file_upload_hw_answers" onclick="setHomWID(${element.id});">
                                                                إرفاق الحل
                                                           
                                                            </button>
                                                       </td>
                                                                                 <td> ${degree} </td>
                                                                     

                                                                            </tr>
                                                        `;
                   

                });


            }
            $("#studenthomeworktable").html(data);
            $("#SavedivLoader").hide();
        });
    } catch (err) {
        alert(err);
    }
}

function drawstudentExamsTable() {
   
    try {
        
        var CourseId = $("#Lblcourse_id").html();
        courseDetails_archivedCls.get_ExamsTable(CourseId, function (val) {

            var data = "";
            console.log(val);
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {
                    //  debugger
                    var dgr = element.degree;
                    if (dgr == 0) {
                        dgr = "--";
                    }
                    else {
                        dgr = element.degree;
                    }
                    var file_nm = "";
                    var path = element.image;
                    if (path != "" && path != null) {
                        if (path.indexOf("Acadmies_module/exams/") != -1) {
                            file_nm = path.split("Acadmies_module/exams/")[1];
                        }
                    }

                    data = data + `
                                                    <tr>
                                                                                <td> ${element.title}  </td>
                                                                                <td>${element.details}   </td>
                                                                                <td>
                                                        <a href="../${element.image}" download>
                                                            <i class="fa fa-download"></i>  ${file_nm}

                                                        </a>
                                                       
                                                       </td>
                                                                                <td>
                                   <button type="button" class="btn btn-upload" data-toggle="modal" data-target="#file_upload_exam_answers" onclick="setExamID(${element.id})">
                                                                إرفاق الحل
                                                           
                                                            </button>
                                                       </td>
                                                                                 <td> ${dgr}</td>
                                                                     

                                                                            </tr>
                                                        `;


                });


            }
            $("#studentExamstable").html(data);
            $("#SavedivLoader").hide();
        });
    } catch (err) {
        alert(err);
    }
}

function SearchStudent() {

    try {

        var CourseId = ($("#Lblcourse_id").html());
        var StudentName = ($("#txtstud_Search").val());
        courseDetails_archivedCls.search_pub_Students(StudentName, CourseId, function (val) {
            var data = "";
            console.log(val);

            if (val[0] == 1) {
                var arrstudent = JSON.parse(val[1]);

                arrstudent.forEach(function (element) {


                    data = data + `   <tr>
                                    <td>
                                        <label id=" ${element.student_id}"> ${element.studentname}</label>
                                    </td>
                                    <td>
               <input id="finaldegee" type="text" value=" ${element.final}"   />
                                   
                                    </td>
                                    <td>
               <input id="activitydegee" type="text" value=" ${element.activityDegree}" />
                                    
                                    </td>
                                </tr> `


                });

                $("#pblcstudentdegrees").html(data);
                $("#pblcstudentdegrees").modal();
            }




            else {
                $("#pblcstudentdegrees").html("لا يوجد طالب بهذا الاسم");
            }

        });


    } catch (err) {
        alert(err);
    }
}



function drawConditionsTable() {
    try {
        var CourseId = ($("#Lblcourse_id").html());
        courseDetails_archivedCls.get_Condition(CourseId, function (val) {


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
                                                            <i class="zmdi zmdi-cloud-download"></i>تحميل ملف  

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
function drawCourseFile() {
    try {
        var CourseId = ($("#Lblcourse_id").html());
        //var LecID= $("#LblLecture_id").html();
        
        courseDetails_archivedCls.get_CourseFiles(CourseId, function (val) {
            var data = "";
            console.log(val);
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {

                    var file_nm = "";
                    var path = element.image;
                    if (path != "" && path != null) {
                        if (path.indexOf("Acadmies_module/coursefiles/") != -1) {
                            file_nm = path.split("Acadmies_module/coursefiles/")[1];
                        }
                    }

                    data = data + `
                                                     <tr>
                                                      <td>${element.notes} </td>
                                                     <td>
                                                       <li>
                                                        <a href="../${element.image}" download>
                                                            <i class="zmdi zmdi-cloud-download"></i>تحميل ملف  

                                                        </a>
                                                        <span>${file_nm}</span>
                                                    </li>

                                                        </td>
                                                  
                                                      
                                                                            </tr>
                                                                      
`;
                });


            }
            $("#courseFiles-table").html(data);
        });
    } catch (err) {
        alert(err);
    }
}

function drawStudentTable() {
    try {
        var CourseId = ($("#Lblcourse_id").html());
        courseDetails_archivedCls.get_StudentTable(CourseId, function (val) {


            var data = "";

            console.log(val);
          
            if (val[0] === "1") {
                var arr1 = JSON.parse(val[1]);
                arr1.forEach(function (element) {

                    data = data + `<table id="student">

                                                    <tr>
 
                                                <td><img src="${element.studImag}"> </td>

                                                        <td>

                                                            <label>${element.studentName}  </label>


                                                        </td>
                                                        <td>
                                                             <div class="btn-group pull-left"  style="position:absolute; margin-bottom:5px;">
                                                                                        <button type="button" class="btn btn-info dropdown-toggle btn-xs" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                         
                                                                                            <i class="fa fa-cogs"></i>
      
                                                                                        </button>
                                                                                        <ul class="dropdown-menu">
                                                                                           
                                                                                                <li>
                                                                                            <a   data-toggle="modal" href="#studentDegrees"  onclick=" viewstudDegrees(${element.student_id});Setstudentid(${element.student_id});">
                                                                                                الدرجات 
                                                                                            </a>
                                                                                                    </li>
                                                                                             
                                                                                                </ul>
                                                                                           
                                                                                        </div>
                                                                                          

                                                        </td>

                                                    </tr>
                                                  
                                                </table>
                 
                                                                               
                                                                      
`;
                });
            }
            $("#StudentTable").html(data);
        });
    } catch (err) {
        alert(err);
    }
}

function drawHomeworks() {
    try {
        var CourseId = ($("#Lblcourse_id").html());
        courseDetails_archivedCls.get_Homework(CourseId, function (val) {


            var data = "";

            if (val[0] === "1") {

                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {

                    data = data + `<table id="student">

                                                    <tr>
                                                      
                                                        <td>

                                                       <li>
                                                        <a href="../${element.image}" download>
                                                            <i class="zmdi zmdi-cloud-download"></i> ${element.title}   

                                                        </a>
                                                       
                                                       </li>

                                                                  </td>
                                                              <td>
                                                             <label>${element.date_hj}  </label>
                            
                                                        </td>
                                                        <td>
                                                             <div class="btn-group pull-left"  style="position:absolute; margin-bottom:5px;">
                                                                                        <button type="button" class="btn btn-info dropdown-toggle btn-xs" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                         
                                                                                            <i class="fa fa-cogs"></i>
      
                                                                                        </button>
                                                                                        <ul class="dropdown-menu">
                                                                                   
                                                                                              
                                                                                                 <li>
                                                                                            <a   data-toggle="modal" href="#StudenthomeworkAnswers"  onclick="drawstudenthomeworkanswers(${element.id}) ">
                                                                                              حلول الواجب  
                                                                                            </a>
                                                                                                    </li>
                                                                                             
                                                                                                </ul>
                                                                                           
                                                                                        </div>
                                                                                          

                                                        </td>

                                                    </tr>
                                                  
                                                </table>
                 
                                                                               
                                                                      
`;
                });
            }
            $("#homework").html(data);
        });
    } catch (err) {
        alert(err);
    }
}
function drawExams() {
    try {
        var CourseId = ($("#Lblcourse_id").html());
        courseDetails_archivedCls.get_Exams(CourseId, function (val) {


            var data = "";

            console.log(val);
            if (val[0] === "1") {

                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {

                    data = data + `<table id="student">

                                                    <tr>
                                                      
                                                        <td>

                                                                                <li>
                                                        <a href="../${element.image}" download>
                                                            <i class="zmdi zmdi-cloud-download"></i> ${element.title}   

                                                        </a>
                                                       
                                                       </li>
                                                                  </td>
                                                              <td>
                                                             <label>${element.date_hj}  </label>
                            
                                                        </td>
                                                        <td>
                                                             <div class="btn-group pull-left"  style="position:absolute; margin-bottom:5px;">
                                                                                        <button type="button" class="btn btn-info dropdown-toggle btn-xs" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                         
                                                                                            <i class="fa fa-cogs"></i>
      
                                                                                        </button>
                                                                                        <ul class="dropdown-menu">
                                                                                   
                                                                                               
                                                                                              
                                                                                                     <li>
                                                                                            <a   data-toggle="modal" href="#StudentExamskAnswers"  onclick="drawstudentExamsanswers(${element.id}) ">
                                                                                              حلول الاختبارات  
                                                                                            </a>
                                                                                                    </li>
                                                                                             
                                                                                                </ul>
                                                                                           
                                                                                        </div>
                                                                                          

                                                        </td>

                                                    </tr>
                                                  
                                                </table>
                 
                                                                               
                                                                      
`;
                });
            }
            $("#courseExams").html(data);
        });
    } catch (err) {
        alert(err);
    }
}



function drawNotes() {
    try {
        var CourseId = ($("#Lblcourse_id").html());
        courseDetails_archivedCls.get_StudentNote(CourseId, function (val) {


            var data = "";

           //console.log(val);
            if (val[0] ==="1") {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {

                    data = data + `<p>${element.comment}</p>
                                                                           
`;
                });
            }
            $("#StudentNote").html(data);
        });
    } catch (err) {
        alert(err);
    }
}

function drawActivity() {
    try {
        var CourseId = ($("#Lblcourse_id").html());
        courseDetails_archivedCls.get_StudentActivity(CourseId, function (val) {


            var data = "";

            //console.log(val);
            if (val[0] === "1") {

                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {

                    data = data + `<p>${element.activity}</p>
                                                                           
`;
                });
            }
            $("#studentActivity").html(data);
        });
    } catch (err) {
        alert(err);
    }
}



function saveExamkDegree() {

    try {


        if (!checkRequired()) {


            var students_degree = [];
            $("#studentExamAnswers tr").each(function () {
                debugger
                var obj = {};
                obj["id"] = $(this).find("label").attr("id");
                obj["hwmstuddegree"] = $(this).find("#Homeworkdegee").val();
                obj["file"] = $(this).find("#homeworkfile").attr("href");
                students_degree.push(obj);

            });

            console.log(students_degree);
            var CourseId = ($("#Lblcourse_id").html());
            var ExamId = ($("#LblExam_id").html());
            // $("#SavedivLoader").show();

            courseDetailsCls.saveExamDegree(ExamId, CourseId, students_degree, function (val) {
                if (val == true) {

                    $("#SavedivLoader").hide();
                    alert("  تم الحفظ  ");
                    $("#StudentExamskAnswers").modal('hide');

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

