
//var deleteWebServiceMethod = "coursatCls.asmx/Delete";
// global variable used in row_click and update functions
//var editWebServiceMethod = "coursatCls.asmx/Edit";
var formAutoCodeControl = "lblmainid";


$(function () {

    try {
        $("#pnlConfirm").hide();
        $("#divData").hide();
      //  $("#SavedivLoader").show();


        getlectureCode();
        GetCourses();
        drawFinanceStudent();
        drawLecturesTable();
        drawStudentTable();
        drawNotes();
        drawActivity();
        drawConditionsTable();
        Studentlistview();
        drawHomeworks();
        drawExams();
        drawCourseFile();
        drawStudentCertificateTable();
     
        drawstudentHomeworkTable();
        drawStudentfinanceforAdmin();
        drawstudentExamsTable();
        drawstudentcourseDegreesTable();
       
        setInterval(function () {
            drawCourseComments();
        }, 10000);

    } catch (err) {
        alert(err);
    }
});

function saveCourse() {
    
    try {
      //debugger

        if (checkRequired("divformEditCourse")) {
            showErrorMessage("يرجى ادخال البيانات المطلوبة");
        }

        
          
        else {

            $("#SavedivLoader").show();

            $("#strdate_m").val($("#divdate6 #txtDatem").val());
            $("#strdate_hj").val($("#divdate6 #txtDateh").val());

            $("#enddate_m").val($("#divdate7 #txtDatem").val());
            $("#enddate_hj").val($("#divdate7 #txtDateh").val());

             

                var basicData = generateJSONFromControls("divformEditCourse");

                var CourseId = ($("#Lblcourse_id").html());
               // console.log(basicData);
                courseDetailsCls.SaveCourse(CourseId, basicData, function (val) {
                    if (val == true) {
                        
                        $("#SavedivLoader").hide();
                        //debugger;
                        alert("تم الحفظ بنجاح");
                        GetCourses();
                        $("#EditCourseModal").modal('hide');
                        resetAll();
                        prepareAdd();

                        //drawDynamicTable();


                    } else {
                        alert("لم يتم الحفظ");
                    }

                    //  $("#SavedivLoader").hide();
                });
            }

        
       

    }
catch (err) {
        alert(err);
    }
}


function SaveExam() {

    try {
        setRequired_Date("divdateExam");
        // debugger
        if (checkRequired("divformExams") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");

        }
        else {
            $("#SavedivLoader").show();

            $("#date2_m").val($("#divdateExam #txtDatem").val());
            $("#date2_hj").val($("#divdateExam #txtDateh").val());

            var basicData = generateJSONFromControls("divformExams");

          //  var degreeID = $("#LblDegree_id").html();
            var CourseId = ($("#Lblcourse_id").html());
            var ExamID = $("#LblExam_id").html();
            var code = ($("#lblcode").html());
            //console.log($("#fileURL3").val());
            //console.log(basicData);
            //console.log($("#LblLecture_id").html());
            courseDetailsCls.SaveExam(ExamID, $("#LblLecture_id").html(), code,CourseId, basicData, function (val) {
                if (val == true) {
                    //debugger;
                    $("#SavedivLoader").hide();
                    alert("تم الحفظ بنجاح");
                    $("#exams").modal('hide');
                    resetDivControls("divformExams");
                    drawExams();
                    $("#LblExam_id").html("");
                    $("#LblLecture_id").html("")






                } else {
                    alert("لم يتم الحفظ");
                }


            });
        }


    }
    catch (err) {
        alert(err);
    }
}




function sendMsg() {
    try {



        if (checkRequired("divformconTr") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");

        }
        else {

            $("#SavedivLoader").show();

          

            var basicData = generateJSONFromControls("divformconTr");
            var Id = "";

            var CourseId = ($("#Lblcourse_id").html());
            var code = ($("#lblcode").html());
            var today = new Date();
           // var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
            var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();

            var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
         
            console.log(basicData);
            courseDetailsCls.sendMesg(Id, code, CourseId, Pub_date_m, Pub_date_hj, time, basicData, function (val) {
                if (val == true) {
                    //debugger;
                    $("#SavedivLoader").hide();
                    alert("تم الحفظ بنجاح");
                    $("#contact_Trainer").modal('hide');
                    resetDivControls("divformconTr");
               

                } else {
                    alert("لم يتم الحفظ");
                }


            });
        }


    }
    catch (err) {
        alert(err);
    }

}

function sendMsgtoAdmin() {
    try {



        if (checkRequired("divformconAdmin") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");

        }
        else {

            $("#SavedivLoader").show();



            var basicData = generateJSONFromControls("divformconAdmin");
            var Id = "";

            var CourseId = ($("#Lblcourse_id").html());
            var code = ($("#lblcode").html());
            var today = new Date();
            // var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
            var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();

            var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();

            console.log(basicData);
            courseDetailsCls.sendMesgtoAdmin(Id, code,CourseId, Pub_date_m, Pub_date_hj, time, basicData, function (val) {
                if (val == true) {
                    //debugger;
                    $("#SavedivLoader").hide();
                    alert("تم الحفظ بنجاح");
                    $("#contact_admin").modal('hide');
                    resetDivControls("divformconAdmin");


                } else {
                    alert("لم يتم الحفظ");
                }


            });
        }


    }
    catch (err) {
        alert(err);
    }

}


function SaveHomework() {

    try {
        setRequired_Date("divdateHomework");

        if (checkRequired("divformHomework") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");
        }
        else {
            $("#SavedivLoader").show();

            $("#date3_m").val($("#divdateHomework #txtDatem").val());
            $("#date3_hj").val($("#divdateHomework #txtDateh").val());



            var basicData = generateJSONFromControls("divformHomework");
            

            var CourseId = ($("#Lblcourse_id").html());
            var lectureID = $("#LblLecture_id").html();


            var HomewID = $("#LblHomework_id").html();
            var code = ($("#lblcode").html());
            console.log(basicData);
            courseDetailsCls.SaveHomeWork(HomewID, lectureID, code,CourseId, basicData, function (val) {
                if (val == true) {
                    $("#SavedivLoader").hide();
                    //debugger;
                    alert("تم الحفظ بنجاح");
                    $("#homeworks").modal('hide');
                    resetDivControls("divformHomework");
                    drawHomeworks();
                    $("#LblHomework_id").html("");
                    $("#LblLecture_id").html("");


                }
                else {
                    alert("لم يتم الحفظ");
                }


            });
        }


    }
    catch (err) {
        alert(err);
    }
}



function addStudentdegree() {

    try {
        //درجات الطلاب
       
        if (!checkRequired()) {
          

            var students_degree = [];
            $("#pblcstudentdegrees tr").each(function () {
                var obj = {};
                obj["id"] = $(this).find("label").attr("id");
                obj["fdegree"] = $(this).find("#finaldegee").val();
                obj["Acdegree"] = $(this).find("#activitydegee").val();
                students_degree.push(obj);

            });

            console.log(students_degree);
            var CourseId = ($("#Lblcourse_id").html());
            var code = ($("#lblcode").html());

            // $("#SavedivLoader").show();

            courseDetailsCls.addStudentdegree(CourseId, code,students_degree, function (val) {
                if (val == true) {

                    $("#SavedivLoader").hide();
                    alert("  تم الحفظ  ");
                    $("#publicStudentDegree").modal('hide');

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


function saveHomeworkDegree() {

    try {


        if (!checkRequired()) {


            var students_degree = [];
            $("#studentHWAnswers tr").each(function () {
               
                var obj = {};
                obj["id"] = $(this).find("label").attr("id");
                obj["hwmstuddegree"] = $(this).find("#Homeworkdegee").val();
                obj["file"] = $(this).find("#homeworkfile").attr("href");
                students_degree.push(obj);

            });

            console.log(students_degree);
           var CourseId = ($("#Lblcourse_id").html());
            var homeworkID = ($("#LblHomework_id").html());
            // $("#SavedivLoader").show();

            courseDetailsCls.savehomeworkDegree(homeworkID, CourseId ,students_degree, function (val) {
                if (val == true) {

                    $("#SavedivLoader").hide();
                    alert("  تم الحفظ  ");
                    $("#StudenthomeworkAnswers").modal('hide');

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

function saveExamkDegree() {

    try {


        if (!checkRequired()) {


            var students_degree = [];
            $("#studentExamAnswers tr").each(function () {
                
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

function resetAll() {
    try {
        resetFormControls();
        $("#lblmainid").html("");
    
    } catch (err) {
        alert(err);
    }
}



function SaveAbsenceStudent() {

    try {


        if (!checkRequired()) {

            var student_arr = [];
            $("#absenscetable").find("tr td input:checkbox").each(function () {
                if ($(this).is(":checked")) {
                    var value = $(this).attr("studentAbs");
                    student_arr.push(value)
                }
            });
            
            var CourseId = ($("#Lblcourse_id").html());
            $("#SavedivLoader").show();
           // var x= student_arr.length;
           
                courseDetailsCls.SaveStudentAbsence(CourseId, $("#LblLecture_id").html(), student_arr, function (val) {
                    if (val == true) {

                        $("#SavedivLoader").hide();
                        alert("تم اخذ الغياب ");
                        $("#absenceModal").modal('hide');
                        //drawAbsenceTable();
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






function AddStudent() {
    //قبول الطلاب 

    try {

        debugger

        if (!checkRequired()) {

            var CourseId = ($("#Lblcourse_id").html());
            var code = ($("#lblcode").html());
            
            var student_arr = [];
            $("#action_courseStudents tr").each(function () {
                var obj = {};
                obj["id"] = $(this).find("label").attr("id");
                obj["std_notes"] = $(this).find("#notes_student").text();
                //obj["file"] = $(this).find("#registerFiles").attr("href");
                obj["action_Student"] = $(this).find("#action").val();
                student_arr.push(obj);
             

            });

            var CourseId = ($("#Lblcourse_id").html());
            var code = ($("#lblcode").html());
          
           
            courseDetailsCls.SaveStudent(CourseId, code, student_arr, function (val) {
                if (val == true) {
                        //  $("#SavedivLoader").hide();
                        alert("تم الحفظ بنجاح");
                        $("#addStudentModal").modal('hide');
                        drawStudentTable();
                        window.location.reload();
                        // drawAbsenceTable();




                    } else {
                        alert("لم يتم الحفظ");
                    }


                });
            
           
        }


    } catch (err) {
        alert(err);
    }
}


function drawpublicDegreeTable() {
    try {
        var CourseId = ($("#Lblcourse_id").html());
        

        courseDetailsCls.get_pub_StudentsDegree(CourseId, function (val) {

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
            $("#pblcstudentdegrees").modal();



        });

    }



    catch (err) {
        alert(err);
    }
}

//draw_student-homework - answers-degrees- hasnaa

function drawstudenthomeworkanswers(homeworkId) {
    try {
        //debugger
        var CourseId = ($("#Lblcourse_id").html());
        ($("#LblHomework_id").html(homeworkId));


        courseDetailsCls.get_StudentsHomeworkAnswers(homeworkId, CourseId , function (val) {

            console.log(val);
            var data = "";
            if (val[0] == 1) {
             
                var arrstudent = JSON.parse(val[1]);

                arrstudent.forEach(function (element) {
                
                    var filename = "";
                    var path = element.homeworkanswer;
                    if (path != "" && path != null) {
                      
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


        courseDetailsCls.get_StudentsExamsAnswers(ExamId, CourseId, function (val) {

            console.log(val);
            var data = "";
            if (val[0] == 1) {
                
                var arrstudent = JSON.parse(val[1]);

                arrstudent.forEach(function (element) {
                    

                    var filename = "";
                    var path = element.homeworkanswer;
                    if (path != "" && path != null) {
                        
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


function SaveLec() {

    try {

        setRequired_time("divstartTime");
        setRequired_Date("divdate1");
        //var check = checkRequired("divFormTable");

        if (checkRequired("divFormTable") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");

        }
        else {
          // $("#SavedivLoader").show();

            $("#dateLec_m").val($("#divdate1 #txtDatem").val());
            $("#dateLec_hj").val($("#divdate1 #txtDateh").val());


            var CourseId =$("#Lblcourse_id").html();
            var lectureID = $("#LblLecture_id").html();
            //var id = "";

            var basicData = generateJSONFromControls("order_addLec");

            ////console.log(basicData);
            courseDetailsCls.Save(lectureID, CourseId, basicData, function (val) {


                $("#SavedivLoader").hide();
                if (val == true) {
                   
                    alert("تم الحفظ بنجاح");
                    getlectureCode();
              
                    $("#order_addLec").modal('hide');
                    resetDivControls("divFormTable");
                    drawLecturesTable();
                    $("#LblLecture_id").html('');

                } else {
                    //$("#SavedivLoader").hide();
                    alert("لم يتم الحفظ");
                }

              
            });
        }
    }


     catch (err) {
        alert(err);
    }
}

function SaveEvalution() {

    try {

       
        //var check = checkRequired("divFormTable");

        if (checkRequired("divformCourseEvalution") == 1) {
            showErrorMessage("يرجى ادخال البيانات المطلوبة");

        }
        else {

            $("#SavedivLoader").show();


            var CourseId = ($("#Lblcourse_id").html());
            //var studentId = ($("#lblStudentID").html());
           
            var id = "";

            var basicData = generateJSONFromControls("divformCourseEvalution");


            console.log(basicData);
            courseDetailsCls.SaveEvalution(id, CourseId,basicData, function (val) {
                if (val == true) {
                    $("#SavedivLoader").hide();
                    //debugger;
                    alert("تم الحفظ بنجاح");
                    $("#courseEvalution").modal('hide');
                    resetDivControls("divformCourseEvalution");
                    drawLecturesTable();





                } else {
                    alert("لم يتم الحفظ");
                }


            });
        }
    }


    catch (err) {
        alert(err);
    }
}
//addFiles


function saveHWanswer() {

    try {

        
        if (checkRequired("divFormuploadHMfiles") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");

        }
        else {
            $("#SavedivLoader").show();
            var HomeWorkId = ($("#LblHomework_id").html());
            var CourseId = ($("#Lblcourse_id").html());
            var code = ($("#lblcode").html());
           
            var basicData = generateJSONFromControls("divFormuploadHMfiles");

            var Id = "";
          
            courseDetailsCls.saveHWanswer(Id, HomeWorkId, code, CourseId ,basicData, function (val) {
                if (val == true) {
                    $("#SavedivLoader").hide();
                    // debugger;
                    alert("تم الحفظ بنجاح");
                    $("#file_upload_hw_answers").modal('hide');
                  
                    resetDivControls("divFormuploadHMfiles");



                } else {
                    alert("لم يتم الحفظ");
                }


            });
        }


    } catch (err) {
        alert(err);
    }
}



function saveExamanswer() {

    try {

        
        if (checkRequired("divFormuploadexamfiles") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");

        }
        else {
            $("#SavedivLoader").show();
            var examID = ($("#LblExam_id").html());
            var CourseId = ($("#Lblcourse_id").html());
            var code = ($("#lblcode").html());
            var basicData = generateJSONFromControls("divFormuploadexamfiles");

            var Id = "";

            courseDetailsCls.saveExamanswer(Id, code,examID, CourseId,basicData, function (val) {
                if (val == true) {
                    $("#SavedivLoader").hide();
                    // debugger;
                    alert("تم الحفظ بنجاح");
                    $("#file_upload_exam_answers").modal('hide');

                    resetDivControls("divFormuploadexamfiles");



                } else {
                    alert("لم يتم الحفظ");
                }


            });
        }


    } catch (err) {
        alert(err);
    }
}

function addFiles() {

    try {


        if (checkRequired("divFormfiles") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");
       
        }
        else {
            $("#SavedivLoader").show();
            var CourseId = ($("#Lblcourse_id").html());
            var lecID= $("#LblLecture_id").html();
            var basicData = generateJSONFromControls("divFormfiles");

            var Id = "";
            //console.log(basicData);
            console.log($("#coursefileURL").val());
            courseDetailsCls.Savefiles(Id, lecID ,CourseId, basicData, function (val) {
                if (val == true) {
                    $("#SavedivLoader").hide();
                   // debugger;
                    alert("تم الحفظ بنجاح");
                    $("#order_addfiles").modal('hide');
                    drawCourseFile();
                    resetDivControls("divFormfiles");



                } else {
                    alert("لم يتم الحفظ");
                }


            });
        }


    } catch (err) {
        alert(err);
    }
}



function addfinancial() {
    //add financial

    try {
        

        if (checkRequired("divformstudentFinanc") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");

        }
        else {
            $("#SavedivLoader").show();
            var CourseId = ($("#Lblcourse_id").html());
           
            var basicData = generateJSONFromControls("divformstudentFinanc");

            var Id = "";
           
            courseDetailsCls.Savefinanc(Id, CourseId, Pub_date_m, Pub_date_hj, basicData, function (val) {
                if (val == true) {
                    $("#SavedivLoader").hide();
                    // debugger;
                    alert("تم الحفظ بنجاح");
                    drawFinanceStudent();
                    $("#add_Financial").modal('hide');
                    drawCourseFile();
                    resetDivControls("divformstudentFinanc");



                } else {
                    alert("لم يتم الحفظ");
                }


            });
        }


    } catch (err) {
        alert(err);
    }
}


function addCondition() {

    try {


        if (checkRequired("divFormcondition") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");

        }
        else {
            $("#SavedivLoader").show();
           
            var CourseId = ($("#Lblcourse_id").html());
            var basicData = generateJSONFromControls("divFormcondition");

            var Id = "";
            //console.log(basicData);
        
            console.log( $("#fileURL").val());
            courseDetailsCls.SaveCondition(Id, CourseId, basicData, function (val) {
                if (val == true) {
                    $("#SavedivLoader").hide();
                   // debugger;
                    alert("تم الحفظ بنجاح");
                    $("#order_addcondition").modal('hide');
                    drawConditionsTable();
                    resetDivControls("divFormcondition");



                } else {
                    alert("لم يتم الحفظ");
                }


            });
        }


    } catch (err) {
        alert(err);
    }
}

///// note/////
function AddNote() {

    try {
      
      

        if (checkRequired("addNote") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");
        }
        else {
            $("#SavedivLoader").show();
            
            $("#dt_m1").val($("#divdate2 #txtDatem").val());
            $("#dt_hj1").val($("#divdate2 #txtDateh").val());
           
            var CourseId = ($("#Lblcourse_id").html()); 
            var studentId = $("#lblStudentID").html();
            var code = ($("#lblcode").html());
           
            var basicData = generateJSONFromControls("divformNote");


            var Id = "";
            //console.log(basicData);
            courseDetailsCls.Savenote(Id,studentId, code,CourseId,basicData, function (val) {
                if (val == true) {
                    $("#SavedivLoader").hide();
                   // debugger;
                    alert("تم الحفظ بنجاح");
                    $("#addNote").modal('hide');
                    drawNotes();
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


function saveCertificate() {

    try {

        debugger

        if (checkRequired("divFormcertificate") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");
        }
        else {
            $("#SavedivLoader").show();

            $("#dtcf_m").val($("#divdate_certif #txtDatem").val());
            $("#dtcf_hj").val($("#divdate_certif #txtDateh").val());

            var CourseId = ($("#Lblcourse_id").html());
            var studentId = $("#lblStudentID").html();
            var code = ($("#lblcode").html());

            var basicData = generateJSONFromControls("divFormcertificate");


            var Id = "";
           
            courseDetailsCls.saveCertificate(Id, studentId, code, CourseId, basicData, function (val) {
                if (val == true) {
                    $("#SavedivLoader").hide();
                    // debugger;
                    alert("تم الحفظ بنجاح");
                    $("#certificateModal").modal('hide');
                   
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

function drawCourseLinks() {
    debugger
    //روابط مفيدة
    try {

        var CourseId = ($("#Lblcourse_id").html());
        courseDetailsCls.get_courseLinks(CourseId, function (val) {


            var data = "";

            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {



                    data = data + `
                     <ul>

                                                            <li>
                                                                <div class="user">
                                                                    <div class="usr-img">
                                                                        <img  class="avatar" src="${element.suerImage}">
                                                                    </div>
                                                                    <div class="usr-data">
                                                                        <h3>
                                                                            <a href="#"> ${element.username}</a>
                                                                        </h3>
                                                                          <span>
                                                                        <i class="zmdi zmdi-calendar"></i>
                                                                      ${element.date_hj}
                                                               
                                                                </span>
                                                            <span> <h3>
                                                                <a href="${element.URL}"> ${element.title}</a>
                                                                        </h3></span>
                                                                        <p>${element.notes} </p>
                                                                    </div>
                                                                </div>
                                                            </li>
    </u>


    `;
                });

            }
            $("#divformLinks").html(data);
        });
    } catch (err) {
        alert(err);
    }
}

function addNewlink() {

    try {

        //   debugger
        if (checkRequired("divFormAddLinks") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");
        }
        else {

            var CourseId = ($("#Lblcourse_id").html());
            var basicData = generateJSONFromControls("divFormAddLinks");

            var Id = "";
            var today = new Date();

            var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();

            courseDetailsCls.saveCourseLinks(Id, CourseId, Pub_date_m, Pub_date_hj, time, basicData, function (val) {
                if (val == true) {
                    debugger
                    alert("تم الحفظ بنجاح");

                    resetDivControls("divFormAddLinks");
                    $("#addLinks_modal").modal('hide');
                    drawCourseLinks();



                } else {
                    alert("لم يتم الحفظ");
                }


            });
        }


    } catch (err) {
        alert(err);
    }
}



function addComment() {
    try {

        

        if (checkRequired("newdivcomment") == 1) {
            alert("اكتب تعليق")

        }
        else {
            

            var CourseId = ($("#Lblcourse_id").html());
        

            var basicData = generateJSONFromControls("newdivcomment");


            var Id = "";
        
            var today = new Date();
           
            var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
            courseDetailsCls.SaveComment(Id, CourseId, Pub_date_m, Pub_date_hj,  time , basicData, function (val) {
                if (val == true) {
                   
                  
                    drawCourseComments();
                    resetDivControls("newdivcomment");
                  
                } else {
                    alert("لم يتم الحفظ");
                }


            });
        }


    } catch (err) {
        alert(err);
    }


}


//////SaveaActivity////////
function AddActivity() {

    try {


        if (checkRequired("divformactivity") == 1) {
            alert("ادخل البيانات المطلوبة")
        }
        else {
            $("#SavedivLoader").show();
            $("#date_hj1").val($("#txtDatem").val());
            $("#date_m1").val($("#txtDateh").val());

            var CourseId = ($("#Lblcourse_id").html());

            var basicData = generateJSONFromControls("divformactivity");
            var code = ($("#lblcode").html());


            var Id = "";
            console.log(basicData);
            courseDetailsCls.SaveActivit(Id, code,CourseId, basicData, function (val) {
                if (val == true) {
                    $("#SavedivLoader").hide();
                   
                    alert("تم الحفظ بنجاح");
                    $("#activity_edit").modal('hide');
                    drawActivity();
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


function CourseView() {
    var CourseId = ($("#Lblcourse_id").html());
    courseDetailsCls.Edit(CourseId, function (val) {
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

function LectureView(LecID) {

    setLectId(LecID);
    
    
    courseDetailsCls.EditLec(LecID, function (val) {
        if (val[0] == "1") {
            var data = JSON.parse(val[1]);
            fillControlsFromJson(data[0]);
         
            $("#divdate1 #txtDatem").val(data[0].date_m);
            $("#divdate1 #txtDateh").val(data[0].date_hj);
            $("#divstartTime input").val(data[0].start_time);
           //$("#order_addLec").modal();

        }


    });

}
//get lecture code
function getlectureCode() {
      
    var CourseId = $("#Lblcourse_id").html(); 
    courseDetailsCls.getlectureCode(CourseId,function (val) {
        
        $("#lecture_code").val(Number(val) + 1);
    });
}                                                                                                                                                                                                                 
                                                                                                                                             

      function viewHomework(HMWID) {

    setHomewId(HMWID);

   

    courseDetailsCls.EditHmw(HMWID, function (val) {
        if (val[0] == "1") {
            var data = JSON.parse(val[1]);
            fillControlsFromJson(data[0]);
           
            var filename = "";
            var path = data[0].image;
            if (path != "" && path != null) {
                if (path.indexOf("Acadmies_module/homework/") != -1) {
                    filename = path.split("Acadmies_module/homework/")[1];
                }
            }
            
            $("#divdateHomework #txtDatem").val(data[0].date_m);
            $("#divdateHomework #txtDateh").val(data[0].date_hj);
            document.getElementById('fileupload').innerHTML = (" اسم الملف المرفق"+ " : " +filename);

      

            

        }


    });

}                                                              

function viewExam(examID) {

    setExamtId(examID);
   

    courseDetailsCls.EditExam(examID, function (val) {
        if (val[0] == "1") {
            var data = JSON.parse(val[1]);
            fillControlsFromJson(data[0]);
            var filename = "";
            var path = data[0].image;
            if (path != "" && path != null) {
                if (path.indexOf("Acadmies_module/exams/") != -1) {
                    filename = path.split("Acadmies_module/exams/")[1];
                }
            }
            document.getElementById('examfile').innerHTML = (" اسم الملف المرفق" + " : " + filename);


            $("#divdateExam #txtDatem").val(data[0].date_m);
            $("#divdateExam #txtDateh").val(data[0].date_hj);



        }


    });

}
//searchAbsence

function studentSearch() {

    try {

        var CourseId = ($("#Lblcourse_id").html()); 
        var StudentName = $("#txt_Search").val();
        courseDetailsCls.get_student(StudentName, CourseId, function (val) {
           
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
                                                                                            <a   onclick="DeleteStudent(${element.student_id});">
                                                                                                حذف
                                                                                            </a>
                                                                                                    </li>
                                                                                                <li>
                                                                                            <a   data-toggle="modal" href="#studentDegrees" onclick="Setstudentid(${element.student_id});">
                                                                                                الدرجات 
                                                                                            </a>
                                                                                                    </li>
                                                                                                <li>
                                                                                            <a   data-toggle="modal" href="#addNote" onclick="viewstudDegrees(${element.student_id});Setstudentid(${element.student_id});">
                                                                                                اضافة ملاحظة
                                                                                            </a>
                                                                                                    </li>
                                                                                              <li>
                                                                                            <a   data-toggle="modal" href="#certificateModal" onclick="Setstudentid(${element.student_id});">
                                                                                                اضافة شهادة 
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
        courseDetailsCls.get_srchstudentABS(StudentName, lec_id,CourseId, function (val) {

         

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
        var badges = ["badge-success", "badge-info", "badge-dark","badge-warning"];
        var CourseId = $("#Lblcourse_id").html(); 
        //alert(CourseId);
        courseDetailsCls.get_course(CourseId, function (val) {

         //   console.log(val);
            
            var arr1 = JSON.parse(val[1]);
            $("#course_title").html(arr1[0].name);
            $("#lectureDuration").html(arr1[0].lect_duration);
            $("#course_details").html(arr1[0].description);
            $("#course_date").html(arr1[0].start_dt_hj);
            if (arr1[0].price > 0) {
                $("#course_price").parent().css("display", "block");
                $("#course_price").html(arr1[0].price);
            }
            else { $("#course_price").parent().css("display", "none");}
            $("#lect_time").html(arr1[0].lect_duration);
            $("#course_category").html(arr1[0].course_category);
            $("#course_stat").html(status[arr1[0].StatusCourse]);
            $("#course_stat").attr("class",badges[arr1[0].StatusCourse]);

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
        courseDetailsCls.get_CourseStudents(CoursLecID,CourseId, function (val) {

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
//function count_endtime(start_time) {
    
//    var lect_time = $("#lect_time").html();
   
//    var t1 = start_time.split(":");
//    console.log(t1);
//    var t2 = t1[1].split(" ");
//    console.log(t2);
//    mins = Number(t2[0]) + Number(lect_time);
//    minhrs = Math.floor(parseInt(mins / 60));
//    hrs = Number(t1[0]) + minhrs;
//    mins = mins % 60;
//    console.log(lect_time);
//    console.log(mins);
//    console.log(minhrs);
//    console.log(hrs);
//    var tm = t2[1];
//    if (hrs > 12) {
//        hrs = hrs - 12;
//        if (t2[1] == "am")
//            tm = "pm";
//        else
//            tm = "am";
//    }
//    return ( hrs.padDigit() + ':' + mins.padDigit() + " " + tm);
//}
function drawLecturesTable() {
    try {
       // $("#SavedivLoader").show();
        var CourseId = $("#Lblcourse_id").html();
        courseDetailsCls.get_LectureTable(CourseId, function (val) {
          
            var data = "";
            console.log(val);
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {
                    //var end_time = count_endtime(element.start_time);
                 
                    data = data + `<tr>
                                                      <td>${element.lecture_code} </td>
                                                     <td>${element.date_hj}  </td>
                                                    <td>${element.start_time}</td>
                                                       <td> ${element.hallNum} </td> `;
                    action = ` <td>

                        <div class="btn-group pull-left" style="position:absolute">
                            <button type="button" class="btn btn-info dropdown-toggle btn-xs" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                <i class="fa fa-cogs"></i>

                            </button>
                            <ul class="dropdown-menu">

                                <li><a data-toggle="modal" href="#order_addLec" onclick="LectureView(${element.id});">
                                    تعديل
                                                                       </a></li>
                                <li><a onclick="DeleteLec(${element.id});">
                                    حذف
                                                                       </a></li>
                                <li>
                                    <li>
                                        <a data-toggle="modal" href="#absenceModal" onclick="drawAbsenceTable(${element.id});">
                                            الغياب
                                                                           </a>
                                    </li>
                                    <li>
                                        <a data-toggle="modal" href="#order_addfiles" onclick="setFLectId(${element.id});">
                                            اضافة ملف
                                                                           </a>
                                    </li>

                                                                            <li>
                                                                            <a data-toggle="modal" href="#exams"  onclick="setLectId(${element.id});">
                                                                                اضافة اختبار
                                                                               </a>
                                                                                     </li>
                                                                                    <li>
                                                                            <a data-toggle="modal" href="#homeworks"  onclick="setLectId(${element.id});">
                                                                                اضافة واجب
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
                                text = "<i class='success fa fa-check'><i/>";
                                break;
                            case true:
                                text = "<i class=' danger fa fa-times'><i/>";
                                break;
                            //case false:
                            //    text = "<i class='danger fa fa-times'><i/>";
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
        courseDetailsCls.get_homeworkTable(CourseId, function (val) {

            var data = "";
            console.log(val);
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {
                    var degree = element.degree;
                    var file_nm = "";
                    var path = element.image;
                    if (path != "" && path != null) {
                        if (path.indexOf("Acadmies_module/homework/") != -1) {
                            file_nm = path.split("Acadmies_module/homework/")[1];
                        }
                    }
                    if (degree == 0) {
                        degree = "--";



                        data = data + `
                                                    <tr>
                                                                                <td> ${element.title}  </td>
                                                                                <td>${element.details}   </td>
                                                                                <td>
                                                    <a href="../${element.image}" download>
                                                            <i class="fa fa-download"></i>  ${file_nm}

                                                        </a>
                                                       
                                                       </td>
                                                        <td> ${degree} </td>
                                                                     
                                                                                <td>
                                   <button type="button" class="btn btn-upload" data-toggle="modal" data-target="#file_upload_hw_answers" onclick="setHomWID(${element.id});">
                                                                إرفاق الحل
                                                           
                                                            </button>
                                                       </td>
                                                   

                                                                            </tr>
                                                        `;
                    }
                    else {


                        data = data + `
                                                    <tr>
                                                                                <td> ${element.title}  </td>
                                                                                <td>${element.details}   </td>
                                                                                <td>
                                                    <a href="../${element.image}" download>
                                                            <i class="fa fa-download"></i>  ${file_nm}

                                                        </a>
                                                       
                                                       </td>
                                                        <td> ${degree} </td>
                                                                     
                                                                                <td> done </td>
                                                   

                                                                            </tr>
                                                        `;
                    }
                   

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
        courseDetailsCls.get_ExamsTable(CourseId, function (val) {

            var data = "";
            console.log(val);
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {
                    //  debugger
                    var file_nm = "";
                    var path = element.image;
                    if (path != "" && path != null) {
                        if (path.indexOf("Acadmies_module/exams/") != -1) {
                            file_nm = path.split("Acadmies_module/exams/")[1];
                        }
                    }
                    var dgr = element.degree;
                    if (dgr == 0) {
                        dgr = "--";

                        data = data + `
                                                    <tr>
                                                                                <td> ${element.title}  </td>
                                                                                <td>${element.details}   </td>
                                                                                <td>
                                                        <a href="../${element.image}" download>
                                                            <i class="fa fa-download"></i>  ${file_nm}

                                                        </a>
                                                       
                                                       </td>
                                                  <td> ${dgr}</td>
                                                                                <td>
                                   <button type="button" class="btn btn-upload" data-toggle="modal" data-target="#file_upload_exam_answers" onclick="setExamID(${element.id})">
                                                                إرفاق الحل
                                                           
                                                            </button>
                                                       </td>
                                                                        
                                                                     

                                                                            </tr>
                                                        `;

                    }
                    else {
                        dgr = element.degree;



                        data = data + `
                                                    <tr>
                                                                                <td> ${element.title}  </td>
                                                                                <td>${element.details}   </td>
                                                                                <td>
                                                        <a href="../${element.image}" download>
                                                            <i class="fa fa-download"></i>  ${file_nm}

                                                        </a>
                                                       
                                                       </td>
                                                  <td> ${dgr}</td>
                                                                                <td> done</td>
                                                                        
                                                                     

                                                                            </tr>
                                                        `;
                    }

                });


            }
            $("#studentExamstable").html(data);
            $("#SavedivLoader").hide();
        });
    } catch (err) {
        alert(err);
    }
}

function drawstudentcourseDegreesTable() {
    // draw homework table for student
    try {
        //debugger
        // $("#SavedivLoader").show();
        var CourseId = $("#Lblcourse_id").html();
        courseDetailsCls.get_courseDegreesTable(CourseId, function (val) {
            

            var data = "";
            console.log(val);
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {
                    var degree = element.degree;
                   
                    if (degree == 0) {
                        degree = "--";
                    }



                        data = data + `
                                                           <tr>
                                                                                <td> ${element.final_degree}  </td>
                                                                                <td>${element.activity_degree}   </td>
                                                                            
                                                                            </tr>
                                                        `;
                    
                    


                });


            }
            $("#studentcourseDegreestable").html(data);
            $("#SavedivLoader").hide();
        });
    } catch (err) {
        alert(err);
    }
}



function drawCourseComments() {
    try {
      
       // $("#SavedivLoader").show();
        var CourseId = ($("#Lblcourse_id").html());
        courseDetailsCls.get_courseComments(CourseId, function (val) {
         

            var data = "";
          
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {



                    data = data + `
                 <ul>

                                                        <li>
                                                            <div class="user">
                                                                <div class="usr-img">
                                                                    <img src="${element.image}">
                                                                </div>
                                                                <div class="usr-data">
                                                                    <h3>
                                                                        <a href="#"> ${element.username}</a>
                                                                    </h3>
                                                                    <span>
                                                                        <i class="zmdi zmdi-calendar"></i>
                                                                      ${element.date_hj}
                                                            <i class="zmdi zmdi-calendar"></i>
                                                                      ${element.time}
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

//serchstudent in student degrees

function SearchStudent() {

    try {

        var CourseId = ($("#Lblcourse_id").html());
        var StudentName = ($("#txtstud_Search").val());
        courseDetailsCls.search_pub_Students(StudentName, CourseId, function (val) {
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



function drawStudentCertificateTable() {
    try {
        debugger
        var CourseId = ($("#Lblcourse_id").html());
        courseDetailsCls.get_certificate(CourseId, function (val) {


            var data = "";
            console.log(val);
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {
                    //debugger
                    var file_nm = "";
                    var path = element.image;
                    if (path != "" && path != null) {
                        if (path.indexOf("Acadmies_module/coursefiles/") != -1) {
                            file_nm = path.split("Acadmies_module/coursefiles/")[1];
                        } 
                    }
                    data = data + `
                                                     <tr>
                                                   
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
            $("#student_Certificate").html(data);
        });
    } catch (err) {
        alert(err);
    }
}




function drawConditionsTable() {
    try {
        var CourseId = ($("#Lblcourse_id").html());
        courseDetailsCls.get_Condition(CourseId, function (val) {


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
                                                       <td>

                                                          <div class="btn-group pull-left" style="position:absolute">
                                                        <button type="button" class="btn btn-info dropdown-toggle btn-xs" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                                   <i class="fa fa-cogs"></i>

                                                                </button>
                                                                <ul  class="dropdown-menu">

                                                                 <li>
                                                                                            <a   onclick="DeleteCondition(${element.id});">
                                                                                                حذف
                                                                                            </a>
                                                                                                    </li>
                                                  
                                                      </ul>
                                                     </div>
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

       // debugger
        var CourseId = ($("#Lblcourse_id").html());
        //var LecID= $("#LblLecture_id").html();
        
        courseDetailsCls.get_CourseFiles(CourseId, function (val) {
            var data = "";
            var condition_check = "";
          // console.log(val);
            if (val[0] == 1) {
               //debugger
             
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {

                    var file_nm = "";
                    var path = element.image;
                    if (path != "" && path != null) {
                        if (path.indexOf("Acadmies_module/coursefiles/") != -1) {
                            file_nm = path.split("Acadmies_module/coursefiles/")[1];
                        }
                    }
                    if (document.getElementById("checkuser").value ==1) {

                        data = data + `<tr>
                                                      <td>${element.notes} </td>
                                                     <td>
                                                       <li>
                                                        <a href="../${element.image}" download>
                                                            <i class="zmdi zmdi-cloud-download"></i>تحميل ملف  

                                                        </a>
                                                        <span>${file_nm}</span>
                                                    </li>

                                                        </td> 
                                                        <td>

                                                          <div class="btn-group pull-left" style="position:absolute">
                                                        <button type="button" class="btn btn-info dropdown-toggle btn-xs" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                                   <i class="fa fa-cogs"></i>

                                                                </button>
                                                                <ul  class="dropdown-menu">

                                                                 <li>
                                                                                            <a   onclick="DeleteFile(${element.id});">
                                                                                                حذف
                                                                                            </a>
                                                                                                    </li>
                                                  
                                                      </ul>
                                                     </div>
                                                           </td> 
</tr>`;
                    }

                    else {


                        data = data + `<tr>
                                                      <td>${element.notes} </td>
                                                     <td>
                                                       <li>
                                                        <a href="../${element.image}" download>
                                                            <i class="fa fa-download"></i>   

                                                        </a>
                                                        <span>${file_nm}</span>
                                                    </li>

                                                        </td> 
                                                       
</tr>`;

                    }
                   
                });


            }
            $("#courseFiles-table").html(data);
        });
    } catch (err) {
        alert(err);
    }
}

function viewstudDegrees(studentID) {
    

    var CourseId = ($("#Lblcourse_id").html());
    $("#lblStudentID").html(studentID);
    var stdid = $("#lblStudentID").html();
    courseDetailsCls.EditstudDegree(CourseId, studentID, function (val) {
        if (val[0] == "1") {
            var data = JSON.parse(val[1]);
            fillControlsFromJson(data[0]);


        }


    });
}

function drawStudentTable() {
    try {
        var CourseId = ($("#Lblcourse_id").html());
        courseDetailsCls.get_StudentTable(CourseId, function (val) {


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
                                                                                            <a   onclick="DeleteStudent(${element.student_id});">
                                                                                                حذف
                                                                                            </a>
                                                                                                    </li>
                                                                                                <li>
                                                                                            <a   data-toggle="modal" href="#studentDegrees"  onclick=" viewstudDegrees(${element.student_id});Setstudentid(${element.student_id});">
                                                                                                الدرجات 
                                                                                            </a>
                                                                                                    </li>
                                                                                                <li>
                                                                                            <a   data-toggle="modal" href="#addNote" onclick="Setstudentid(${element.student_id});">
                                                                                                اضافة ملاحظة
                                                                                            </a>
                                                                                                    </li>

                                                                                               <li>
                                                                                            <a   data-toggle="modal" href="#certificateModal" onclick="Setstudentid(${element.student_id});">
                                                                                                اضافة شهادة 
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


function drawStudentfinanceforAdmin() {
    try {
        debugger
        var CourseId = ($("#Lblcourse_id").html());
        courseDetailsCls.get_StudentFinanceAdmin(CourseId, function (val) {


            var data = "";

            console.log(val);

            if (val[0] === "1") {
                var arr1 = JSON.parse(val[1]);
                arr1.forEach(function (element) {
                    var status = "";
                    if (element.approved == 1) {
                        status = "تم التاكيد"
                    }
                    else if (element.approved == 2) {
                        status = "تم الرفض"
                    }
                    else {
                        status="قيد المراجعة"
                    }
                    var file_nm = "";
                    var path = element.image;
                    if (path != "" && path != null) {
                        if (path.indexOf("Acadmies_module/coursefiles/") != -1) {
                            file_nm = path.split("Acadmies_module/coursefiles/")[1];
                        }
                    }


                    data = data + `<table id="student">

                                                    <tr>
 
                                                        <td>

                                                            <label>${element.name}  </label>
                                                        </td>
                                                      <td>
                                               
                                                  <label>${element.amount} </label>
                                                     
                                                            </td>
                                                      <td>
                                                       <li>
                                                        <a href="../${element.image}" download>
                                                            <i class="fa fa-download"></i>   

                                                        </a>
                                                        <span>${file_nm}</span>
                                                    </li>
                                    <td> ${status}</td>

                                                     </td>
                                                         <td>
                                                             <div class="btn-group pull-left"  style="position:absolute; margin-bottom:5px;">
                                                                                        <button type="button" class="btn btn-info dropdown-toggle btn-xs" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                                                                            <i class="fa fa-cogs"></i>

                                                                                        </button>
                                                                                        <ul class="dropdown-menu">


                                                                                                <li>
                                                                                            <a   onclick="approv_finance(${element.student_id} , ${element.id});">
                                                                                                تاكيد
                                                                                            </a>
                                                                                                    </li>
                                                                                                <li>
                                                                                            <a   onclick="refuse_finance(${element.student_id},${element.id});">
                                                                                                رفض
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
            $("#Student_Finance").html(data);
        });
    } catch (err) {
        alert(err);
    }
}


function approv_finance(Studentid,payId) {

    var f = confirm("هل  تريد تاكيد المبلغ ");
    if (f == true) {

        $("#SavedivLoader").show();
        var CourseId = ($("#Lblcourse_id").html());
        var code = $("#lblcode").html();
        courseDetailsCls.approv_finance(Studentid, CourseId, code, payId, function (val) {


            if (val[0] == 1) {

                $("#SavedivLoader").hide();

                alert("تم التاكيد بنجاح");
               
             

            } else {
                showErrorMessage('لم يتم التاكيد');
            }

        });
    }

}


function refuse_finance(Studentid, payId) {

    var f = confirm("هل  تريد تاكيد المبلغ ");
    if (f == true) {

        $("#SavedivLoader").show();
        var CourseId = ($("#Lblcourse_id").html());
        var code = $("#lblcode").html();
        courseDetailsCls.refuse_finance(Studentid, CourseId,  code ,payId, function (val) {


            if (val[0] == 1) {

                $("#SavedivLoader").hide();

                alert("تم الرفض ");



            } else {
                showErrorMessage('لم يتم الرفض ');
            }

        });
    }

}

function drawHomeworks() {
    try {
        var CourseId = ($("#Lblcourse_id").html());
        courseDetailsCls.get_Homework(CourseId, function (val) {


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
                                                                                            <a   onclick="DeleteHomework(${element.id});">
                                                                                                حذف
                                                                                            </a>
                                                                                                    </li>
                                                                                                  <li>
                                                                                            <a href="../${element.image}" download>
                                                                                                تحميل
                                                                                            </a>
                                                                                                    </li>
                                                                                                <li>
                                                                                            <a   data-toggle="modal" href="#homeworks"  onclick="viewHomework(${element.id});">
                                                                                              تعديل 
                                                                                            </a>
                                                                                                    </li>
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
        courseDetailsCls.get_Exams(CourseId, function (val) {


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
                                                                                            <a   onclick="DeleteExam(${element.id});">
                                                                                                حذف
                                                                                            </a>
                                                                                                    </li>
                                                                                                     <li>
                                                                                            <a href="../${element.image}" download>
                                                                                                تحميل
                                                                                            </a>
                                                                                                    </li>
                                                                                                <li>
                                                                                            <a   data-toggle="modal" href="#exams"  onclick="viewExam(${element.id});">
                                                                                              تعديل 
                                                                                            </a>
                                                                                                    </li>
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
        courseDetailsCls.get_StudentNote(CourseId, function (val) {


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
        courseDetailsCls.get_StudentActivity(CourseId, function (val) {


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


function drawFinanceStudent() {
    try {
        debugger
        var CourseId = ($("#Lblcourse_id").html());
        courseDetailsCls.get_StudentFnance(CourseId, function (val) {

            var data = "";

            if (val[0] === "1") {
                var check = ""
                var arr1 = JSON.parse(val[1]);
                var total = 0;
                arr1.forEach(function (element) {
                    if (element.approved == 1) {
                        check = "  تم تاكيد المبلغ "
                        total = total + element.amount;
                    }
                    else if (element.approved == 2) {
                        check = "  لم يتم تاكيد المبلغ "
                    }

                    else {
                        check = "  قيد المراجعه "
                    }

                    data = data + `
                                                    <tr>
                                                      
                                                        <td>
                                            <label>${element.amount} </label>
                                                                  </td>
                                                              <td>
                                                             <label>${check}  </label>
                            
                                                        </td>
                                                     
                                                    </tr>
                                             
                 
                                                                               
                                                                      
`;

                });
            }

            $("#financestudent").html(data);
            $("#total_money").html(total);
        });
    }
    catch (err) {
        alert(err);
    }
}


function SaveDegree() {

    try {
       

        if (checkRequired("divFormDegrees") == 1) {
            alert("ادخل البيانات المطلوبة")
        }

        else {
            $("#SavedivLoader").show();
            
          
            var StudentId = $("#lblStudentID").html();
            var CourseId = ($("#Lblcourse_id").html());
            var code = ($("#lblcode").html());

            var basicData = generateJSONFromControls("divFormDegrees");


            var Id = "";
            console.log(basicData);
            courseDetailsCls.SaveDegree(Id, code,StudentId,CourseId, basicData, function (val) {
                if (val == true) {
                    $("#SavedivLoader").hide();
                    alert("تم الحفظ بنجاح");
                    $("#studentDegrees").modal('hide');
                    resetDivControls("divFormDegrees");
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

function Studentlistview() {
    try {
        debugger
       var CourseId = ($("#Lblcourse_id").html());
        courseDetailsCls.get_StudentList( CourseId,function (val) {


            var data = "";

            console.log(val);
            var arr1 = JSON.parse(val[1]);

            arr1.forEach(function (element) {
                //طلبات الطلاب

                var file_nm = "";
                var path = element.registerFiles;
                if (path != "" && path != null) {
                    if (path.indexOf("Acadmies_module/images/") != -1) {
                        file_nm = path.split("Acadmies_module/images/")[1];
                    }
                }

                data = data + `
                                <tr >
                                    <td><label id="${element.student_id}" > ${element.studentName}</label> </td>
                                    <td><label id="notes_student"> ${element.notes}</label> </td>
                                       
                                          <td>
                                   <button type="button" class="btn btn-upload" data-toggle="modal" data-target="#studentFilesModal" onclick="drawstudentFiles(${element.student_id});">
                                                                 student files
                                                           
                                                            </button>
                                                       </td>

                                                         <td>
                                               
                                                  <select  onchange="checkReason();" onclick="setStudentId(${element.student_id});" style="width:100px;" id="action" >
                                                     <option value="-1">اختر</option>                                                    
                                                    <option value="0">رفض</option>
                                                    <option value="1">قبول</option>
                                                      </select>
                                                     
                                                            </td>
                                 
                                </tr>

                                                                     
`;
            });
            
            $("#action_courseStudents").html(data);
           
            
        });
    } catch (err) {
        alert(err);
    }
}

function drawstudentFiles(studentId) {
    try {
        debugger
        var CourseId = ($("#Lblcourse_id").html());
        courseDetailsCls.get_studentFiles(CourseId, studentId,function (val) {

            var data = "";

            if (val[0] === "1") {
              
                var arr1 = JSON.parse(val[1]);
              
                arr1.forEach(function (element) {
                  

                    data = data + `   <tr>
                                    
                                                            <td>
                                                              <label>${element.conditionName}</label>
                                                            </td>
                                  

                                                <td>
                                                       <li>
                                                        <a id="homeworkfile" href="../${element.file}" download>
                                                            <i class="fa fa-download"></i>   
                                                     <span>${element.Name}</span>
                                                        </a>
                                                      
                                      
                                                    </li>
                                         </td>

                                                    
                           </tr>
                                             
                 
                                                                               
                                                                      
`;
                
                });
            }

            $("#studentFiles").html(data);
          
        });
    }
    catch (err) {
        alert(err);
    }
}

function setStudentId(studentID) {
    ($("#lblStudentID").html(studentID));
}

function checkReason() {
    
   
        $("#reject_Reason").modal();
    
}

function saveResponse() {
    try {
        debugger

        if (checkRequired("divformReject_Reason") == 1) {
            alert("ادخل البيانات المطلوبة")
        }

        else {
            $("#SavedivLoader").show();


            var StudentId = $("#lblStudentID").html();
            var CourseId = ($("#Lblcourse_id").html());
            var code = ($("#lblcode").html());
            var comment = $("#rejectId").val();
         
            
            courseDetailsCls.saveResponse(code, StudentId, CourseId, comment , function (val) {
                if (val == true) {
                    $("#SavedivLoader").hide();
                    alert("تم الحفظ بنجاح");
                    $("#reject_Reason").modal('hide');
                  
                } else {
                    alert("لم يتم الحفظ");
                }


            });
        }


    } catch (err) {
        alert(err);
    }

    
}



function UploadComplete2(sender, args) {
   
    var fileLength = args.get_length();
    var fileType = args.get_contentType();
    //alert(sender);
    $('#fileURL').val('Acadmies_module/images/' + args.get_fileName());
    $("#FName").val(args.get_fileName());

   
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
    ClearMe(sender);
}

function UploadComplete3(sender, args) {
 
    var fileLength = args.get_length();
    var fileType = args.get_contentType();
    //alert(sender);
    $('#fileURL3').val('Acadmies_module/exams/' + args.get_fileName());
    $("#FName3").val(args.get_fileName());



    //var img = document.getElementById('imgLoader');
    //img.style.display = 'none';
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
function UploadComplete4(sender, args) {
  
    var fileLength = args.get_length();
    var fileType = args.get_contentType();
    //alert(sender);
    $('#fileURL4').val('Acadmies_module/homework/' + args.get_fileName());
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

function UploadComplete5(sender, args) {

    var fileLength = args.get_length();
    var fileType = args.get_contentType();
    //alert(sender);
    $('#coursefileURL').val('Acadmies_module/coursefiles/' + args.get_fileName());
    $("#EXFName").val(args.get_fileName());



    //var img = document.getElementById('imgLoader');
    //img.style.display = 'none';
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


function UploadComplete6(sender, args) {

    var fileLength = args.get_length();
    var fileType = args.get_contentType();
    //alert(sender);
    $('#hwansfileurl').val('Acadmies_module/coursefiles/' + args.get_fileName());
    $("#fnamehwans").val(args.get_fileName());



    //var img = document.getElementById('imgLoader');
    //img.style.display = 'none';
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


function UploadComplete7(sender, args) {

    var fileLength = args.get_length();
    var fileType = args.get_contentType();
    //alert(sender);
    $('#examansfileurl').val('Acadmies_module/coursefiles/' + args.get_fileName());
    $("#fnameExamans").val(args.get_fileName());



    //var img = document.getElementById('imgLoader');
    //img.style.display = 'none';
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

function UploadComplete8(sender, args) {

    var fileLength = args.get_length();
    var fileType = args.get_contentType();
    //alert(sender);
    $('#fileurlfinanc').val('Acadmies_module/coursefiles/' + args.get_fileName());
    $("#FnameFinanc").val(args.get_fileName());



    //var img = document.getElementById('imgLoader');
    //img.style.display = 'none';
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

function UploadComplete9(sender, args) {

    var fileLength = args.get_length();
    var fileType = args.get_contentType();
    //alert(sender);
    $('#FURL_CERTIF').val('Acadmies_module/coursefiles/' + args.get_fileName());
    $("#FnameCertif").val(args.get_fileName());



    //var img = document.getElementById('imgLoader');
    //img.style.display = 'none';
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

function DeleteLec(LecID) {
    var f = confirm("هل  تريد الحذف");
    if (f == true) {
        $("#SavedivLoader").show();
        courseDetailsCls.Delete_Lecture(LecID, function (val) {
          
            if (val[0] == 1) {
                $("#SavedivLoader").hide();
                //debugger
                
                alert("تم الحذف بنجاح");

                drawLecturesTable();
              

            } else {
                showErrorMessage('لا يمكن الحذف');
            }

        });
    }

}

function DeleteStudent(Studentid) {

    var f = confirm("هل  تريد الحذف");
    if (f == true) {
        
        $("#SavedivLoader").show();
        var CourseId = ($("#Lblcourse_id").html());
        courseDetailsCls.Delete_Student(Studentid, CourseId, function (val) {

            
            if (val[0]==1) {

                $("#SavedivLoader").hide();

                alert("تم الحذف بنجاح");
                drawStudentTable();
                Studentlistview();

            } else {
                showErrorMessage('لم يتم الحذف');
            }

        });
    }

}

function DeleteCondition(condID) {
    var f = confirm("هل  تريد الحذف");
    if (f == true) {
        $("#SavedivLoader").show();
       // debugger
        courseDetailsCls.Delete_condition(condID, function (val) {
            if (val[0] == 1) {
                $("#SavedivLoader").hide();
                alert("تم الحذف بنجاح");
                drawConditionsTable();
               
            } else {
                alert('لم يتم الحذف');
            }

        });
    }

}

function DeleteFile(fileID) {
    var f = confirm("هل  تريد الحذف");
    if (f == true) {
        $("#SavedivLoader").show();
       // debugger
        courseDetailsCls.Delete_File(fileID, function (val) {
            if (val[0] == 1) {
                $("#SavedivLoader").hide();
                alert("تم الحذف بنجاح");
                drawCourseFile();

             

            } else {
                alert('لم يتم الحذف');
            }

        });
    }

}

function DeleteHomework(HWID) {
    var f = confirm("هل  تريد الحذف");
    if (f == true) {
        $("#SavedivLoader").show();
        courseDetailsCls.Delete_Homework(HWID, function (val) {
            if (val[0] == 1) {
                
                $("#SavedivLoader").hide();
                drawHomeworks();
                alert("تم الحذف بنجاح");
               // drawHomeworks();
            
               

            } else {
                alert('لم يتم الحذف');
            }

        });
    }

}

function DeleteExam(ExamID) {
    var f = confirm("هل  تريد الحذف");
    if (f == true) {
        $("#SavedivLoader").show();
        courseDetailsCls.Delete_Exam(ExamID, function (val) {
            
            if (val[0] == 1) {
                $("#SavedivLoader").hide();
                alert("تم الحذف بنجاح");
                drawExams();


            } else {
                alert('لم يتم الحذف');
            }

        });
    }

}
function deleteCourse() {

    var CourseId = ($("#Lblcourse_id").html());
    var f = confirm("هل  تريد الحذف");
    if (f == true) {
        courseDetailsCls.Delete_course(CourseId, function (val) {
            
            if (val[0]==1) {
                $("#SavedivLoader").hide();
                alert("تم الحذف بنجاح");
                window.location.replace("coursat.aspx");
               
               


            } else {
                alert('لم يتم الحذف');
            }

        });
    }
}



function archiveCourse() {

    var f = confirm("عند ارشفة الدورة لن تستطيع التعديل عليها");
    if (f == true) {
        

        var CourseId = ($("#Lblcourse_id").html());

        courseDetailsCls.checkDate(CourseId, Pub_date_m, function (val) {
            if (val[0] == 1) {
                var f = confirm(" الكورس  لم ينتهى  هل تريد الارشفة");

                if (f == true) {

                    courseDetailsCls.Archive_course(CourseId, function (val) {

                        if (val[0] == 1) {
                            $("#SavedivLoader").hide();
                            alert("تمت الارشفه بنجاح  ");
                            window.location.replace("coursat.aspx");

                        } else {
                            alert('لم يتم الارشيف');
                        }

                    });
                }
            }
            else {

                var f = confirm(" هل تريد الارشفة");

                if (f == true) {

                    courseDetailsCls.Archive_course(CourseId, function (val) {

                        if (val[0] == 1) {
                            $("#SavedivLoader").hide();
                            alert("تمت الارشفه بنجاح  ");
                            window.location.replace("coursat.aspx");

                        } else {
                            alert('لم يتم الارشيف');
                        }

                    });
                }

            }

            });

    }
    else {

        alert('لم يتم الارشيف');
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


function Decide(option) {
    var temp = "";
    document.getElementById('lblRate').innerText = "";
    if (option == 1) {
        document.getElementById('Rating1').className = "Filled";
        document.getElementById('Rating2').className = "Empty";
        document.getElementById('Rating3').className = "Empty";
        document.getElementById('Rating4').className = "Empty";
        document.getElementById('Rating5').className = "Empty";
        temp = "1-ضعيف";
    }
    if (option == 2) {
        document.getElementById('Rating1').className = "Filled";
        document.getElementById('Rating2').className = "Filled";
        document.getElementById('Rating3').className = "Empty";
        document.getElementById('Rating4').className = "Empty";
        document.getElementById('Rating5').className = "Empty";
        temp = "2-مقبول";

    }
    if (option == 3) {
        document.getElementById('Rating1').className = "Filled";
        document.getElementById('Rating2').className = "Filled";
        document.getElementById('Rating3').className = "Filled";
        document.getElementById('Rating4').className = "Empty";
        document.getElementById('Rating5').className = "Empty";
        temp = "3-حسنا";
    }
    if (option == 4) {
        document.getElementById('Rating1').className = "Filled";
        document.getElementById('Rating2').className = "Filled";
        document.getElementById('Rating3').className = "Filled";
        document.getElementById('Rating4').className = "Filled";
        document.getElementById('Rating5').className = "Empty";
        temp = "4-جيد";
    }
    if (option == 5) {
        document.getElementById('Rating1').className = "Filled";
        document.getElementById('Rating2').className = "Filled";
        document.getElementById('Rating3').className = "Filled";
        document.getElementById('Rating4').className = "Filled";
        document.getElementById('Rating5').className = "Filled";
        temp = "5-ممتاز";
    }
    document.getElementById('lblRate').innerText = temp;
    return false;
}