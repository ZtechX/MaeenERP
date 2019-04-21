
//var deleteWebServiceMethod = "coursatCls.asmx/Delete";
// global variable used in row_click and update functions
//var editWebServiceMethod = "coursatCls.asmx/Edit";
var formAutoCodeControl = "lblmainid";


var subjectList = [];
var records_per_page = 4;
var numPages = 0;
var current_page = 1;

$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").show();

    try {
        drawCourses();
        Studentlistview();
       
     

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



function archiveSemester() {

    debugger
    var f = confirm(" عند ارشفة الكورس لن تتمكن من التعديل هل تريد الارشفة");

    if (f == true) {

        var semester_id = $("#ddlsemster2").val();
        var diplomeId = ($("#Lbldeploma_id").html());


        Diploma_CoursesCls.checkSemesterDate(semester_id, diplomeId, Pub_date_m, function (val) {
            if (val[0] == 1) {

                var f = confirm("الفصل الدراسى لم ينتهى  هل تريد الارشفة");

                if (f == true) {


                    var diplomeId = ($("#Lbldeploma_id").html());
                    var diplomeCode = ($("#lbldiplomeCode").html());
                    var semester_id = $("#ddlsemster2").val();

                    Diploma_CoursesCls.Archive_Semester(semester_id, diplomeId, function (val) {


                        if (val[0] == 1) {
                            $("#SavedivLoader").hide();
                            alert("تمت الارشفه بنجاح  ");
                            window.location.replace("DiplomaCourses?code=" + diplomeCode);
                            //window.location.reload();

                        }

                        else {
                            alert('  لم يتم الارشيف  ');
                        }

                    });
                }
                else {
                    $("#semester_archiveModal").modal('hide')
                }
            }
            else if (val[0] == 2) {

                var f = confirm("هل تريد الارشفة");

                if (f == true) {


                    var diplomeId = ($("#Lbldeploma_id").html());
                    var diplomeCode = ($("#lbldiplomeCode").html());
                    var semester_id = $("#ddlsemster2").val();

                    Diploma_CoursesCls.Archive_Semester(semester_id, diplomeId, function (val) {


                        if (val[0] == 1) {
                            $("#SavedivLoader").hide();
                            alert("تمت الارشفه بنجاح  ");
                            window.location.replace("DiplomaCourses?code=" + diplomeCode);
                            //window.location.reload();

                        }
                        //else if (val[0] == 2) {
                        //    alert('  الفصل الدراسى لم ينتهى بعد ');
                        //    $("#SavedivLoader").hide();
                        //    alert("تمت الارشفه بنجاح  ");
                        //    window.location.replace("DiplomaCourses?code=" + diplomeCode);
                        //}
                        else {
                            alert('  لم يتم الارشيف  ');
                        }

                    });
                }
                else {
                    alert(" لم يتم الارشيف")
                }

            }
            else {
                alert(" No results found");
                $("#semester_archiveModal").modal('hide')

            }


        });
    }
    else {

        $("#semester_archiveModal").modal('hide')
    }

    }



function addfinancial() {
    //add financial

    try {
        debugger

        if (checkRequired("divformstudentFinanc") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");

        }
        else {
            $("#SavedivLoader").show();
         
            var diplomeID = ($("#Lbldeploma_id").html());

            var basicData = generateJSONFromControls("divformstudentFinanc");

            var Id = "";

            Diploma_CoursesCls.Savefinanc(Id, diplomeID, Pub_date_m, Pub_date_hj, basicData, function (val) {
                if (val == true) {
                    $("#SavedivLoader").hide();
                    // debugger;
                    alert("تم الحفظ بنجاح");
                    $("#add_Financial").modal('hide');
                  
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

function savediplome() {

    try {

        //setRequired_Date("divdate1");
        //setRequired_Date("divdate2");
        var diplomeID = ($("#Lbldeploma_id").html());

        if (checkRequired("divFormDiplome") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");
        }
        else {
            //$("#date_m").val($("#txtDatem").val());
            //$("#date_hj").val($("#txtDateh").val());

            var basicData = generateJSONFromControls("divFormDiplome");

            console.log(basicData);
            Diploma_CoursesCls.SaveDiplome(diplomeID, basicData, function (val) {
                if (val == true) {

                    alert("تم الحفظ بنجاح");
                   // drawCourses();

                    $("#addCourse").modal('hide');
                    resetDivControls("divFormDiplome");


                } else {
                    alert("لم يتم الحفظ");
                }


            });
        }


    } catch (err) {
        alert(err);
    }
}

function savesubject(){

    try {

        setRequired_Date("divdate1");
        //setRequired_Date("divdate2");
        var diplomeID = ($("#Lbldeploma_id").html());      
        
        if (checkRequired("divForm") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");
        }
        else {
            $("#date_m").val($("#txtDatem").val());
            $("#date_hj").val($("#txtDateh").val());
        
            var basicData = generateJSONFromControls("divForm");
      
            var Id = "";
            console.log(basicData);
            Diploma_CoursesCls.Save(Id, diplomeID, basicData, function (val) {
                if (val == true) {
                   
                    alert("تم الحفظ بنجاح");
                    drawCourses(); 
                  
                    $("#addsubject").modal('hide');
                    resetDivControls("divForm");
                   

                } else {
                    alert("لم يتم الحفظ");
                }

               
            });
        }
           
       
    } catch (err) {
        alert(err);
    }
}

////// pagination /////////////
function nextPage() {
    debugger
    if (current_page < numPages) {
        current_page++;
        changePage(current_page);
    }
}
function prevPage() {
    debugger
    if (current_page > 1) {
        current_page--;
        changePage(current_page);
    }
}
function changePage(page) {

    current_page = page;
    if (page < 1) {
        page = 1;
    }
    else if (page > numPages) {
        page = numPages;
    }
    $(".pagination").find("li").removeClass("active");
    $("#li_" + page).addClass("active");
    $("#courses-list").html("");
    var data = "";
    for (var i = (page - 1) * records_per_page; i < (page * records_per_page) && i < subjectList.length; i++) {
        var element = subjectList[i];
        data = data + `<div class="col-md-4 col-sm-12">
                    <div class="block">
                        <div class="block-title">
                            <h5><a href="DiplomaSubjectDetails.aspx?code=${element.code}">${element.subjectName}</a></h5>
                        </div>
                        <div class="block-desc">
                            <p class="desc">${element.subject_goal}</p>
                            <div class="row desc-inner">
                                <div class="bock-trainee pull-right">
                                    <img class="avatar" src="${element.trainerImage}" />
                                    <span>${element.full_name}</span>
                                </div> 
                                    <div class="block-date pull-left">
                                 
                                    <span> ${element.created_at_hj} </span>
                                     <i class="fa fa-calendar-check-o"></i>
                                    <span>${element.semster}</span>

                                </div>
                             
                                   
                            </div>
                        </div>
                    </div>
                </div>`;
    }
    $("#courses-list").html(data);

    }



function Studentlistview() {
    try {
        debugger

        var diplomeID = ($("#Lbldeploma_id").html());
        Diploma_CoursesCls.get_StudentList(diplomeID, function (val) {


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
                                                   <li>
                                                        <a id="registerFiles" href="../${element.registerFiles}" download>
                                                            <i class="fa fa-download"></i>   

                                                        </a>
                                                        <span>${file_nm}</span>
                                                    </li> </td>
                                                            <td>
                                               
                                                  <select  style="width:100px;" id="action" >
                                                    <option value="0">رفض</option>
                                                    <option value="1">قبول</option>
                                                      </select>
                                                     
                                                            </td>
                                 
                                </tr>

                                                                     
`;
            });

            $("#action_courseStudents").html(data);
            //var xx = document.getElementById("action").value;
            //console.log(xx);

        });
    } catch (err) {
        alert(err);
    }
}


function Archive() {
    debugger
    
    var diplomecode = ($("#lbldiplomeCode").html());
    window.location.replace("Archived_DiplomaCourses?code=" + diplomecode)

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
}


function ClearMe(sender) {
    sender.value = '';
}
function clearContents(sender) {
    { $(sender._element).find('input').val(''); }
}

function addCondition() {

    try {


        if (checkRequired("divFormcondition") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");

        }
        else {
            $("#SavedivLoader").show();
            debugger

            var diplomeID = ($("#Lbldeploma_id").html());
            var basicData = generateJSONFromControls("divFormcondition");

            var Id = "";
            //console.log(basicData);

           // console.log($("#fileURL").val());
            Diploma_CoursesCls.SaveCondition(Id, diplomeID, basicData, function (val) {
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



function AddDiplome_Student() {

   // debugger;
    try {
       
            var student_arr = [];
            $("#action_courseStudents tr").each(function () {
                var obj = {};
                obj["id"] = $(this).find("label").attr("id");
                obj["std_notes"] = $(this).find("#notes_student").text();
                //obj["file"] = $(this).find("#registerFiles").attr("href");
                obj["action_Student"] = $(this).find("#action").val();
                student_arr.push(obj);

            });

        var diplomeID = ($("#Lbldeploma_id").html());
        var code = ($("#lbldiplomeCode").html());

            //var x = student_arr.length;
            //if (x != 0) {

            Diploma_CoursesCls.SaveStudent(diplomeID, code,student_arr, function (val) {
                if (val == true) {
                    debugger
                    //  $("#SavedivLoader").hide();
                    alert("تم الحفظ بنجاح");
                    $("#addStudentModal").modal('hide');
                    // drawStudentTable();
                    window.location.reload();
                    // drawAbsenceTable();




                } else {
                    alert("لم يتم الحفظ");
                }


            });


      


    }
    catch (err) {
        alert(err);
    }
}


function DiplomeView() {
    var diplomeID = ($("#Lbldeploma_id").html());
    Diploma_CoursesCls.Edit(diplomeID, function (val) {
        if (val[0] == "1") {



            var data = JSON.parse(val[1]);
            fillControlsFromJson(data[0]);
           
           

            $("#addCourse").modal();




        }


    });

}



function studentDegreesIN_Diplome() {
    //السجل الاكاديمي
    debugger
    try {
        //debugger
        // $("#SavedivLoader").show();
        var diplomeID = ($("#Lbldeploma_id").html());
        Diploma_CoursesCls.get_diplomeDegree(diplomeID, function (val) {


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
                                                            <td> ${element.subjectName}  </td>
                                                      <td>${element.activity_degree}   </td>
                                                                                <td> ${element.final_degree}  </td>
                                                                              
                                                                            
                                                                            </tr>
                                                        `;




                });


            }
            $("#studentDiplomeDegreestable").html(data);
            $("#SavedivLoader").hide();
        });
    } catch (err) {
        alert(err);
    }
}


function addsubject() {

    try {

        debugger
        if (checkRequired("divFormnewsub") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");
        }
        else {
           

            var basicData = generateJSONFromControls("divFormnewsub");

            var Id = "";
            console.log(basicData);
            Diploma_CoursesCls.addsubject(Id, basicData, function (val) {
                if (val == true) {
                    debugger;
                    alert("تم الحفظ بنجاح");

                    $("#newsubject").modal('hide');
                    resetDivControls("divFormnewsub");
                    $("#newsubject").modal('hide');
                   
                    

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

function drawCourses(){
    try {
        //debugger
        var diplomeID = ($("#Lbldeploma_id").html());
        Diploma_CoursesCls.get_Courses(diplomeID,"",function (val) {
           
          //  var data = "";
           // var arr1 = JSON.parse(val[1]);
         
          

            var arr1 = JSON.parse(val[1]);
            $("#diplome_title").html(arr1[0].dpname);
            subjectList = arr1;
            numPages = Math.ceil(subjectList.length / records_per_page);

            var str = "";
            for (var i = 0; i < (numPages + 2); i++) {

                if (i == 0) {
                    str += '<li class="paginate_button previous"><a onclick="prevPage();">السابق</a></li>';
                }
                else if (i == (numPages + 1)) {
                    str += '<li class="paginate_button next" id="default-datatable_next"><a onclick="nextPage();">التالي</a></li>';

                } else {
                    str += '<li id="li_' + i + '" class="paginate_button"><a onclick="changePage(' + i + ');">' + i + '</a></li>';

                }
            }
            $(".pagination").html(str);
            changePage(1);
           
        });
           } catch (err) {
        alert(err);
    }
}
function searchCourses() {
    try {
        
            
            var courseName = $("#txt_Search").val();
        Diploma_Courses.get_Courses(diplomeID,courseName, function (val) {
                debugger

                var data = "";
               
                if (val[0] == "1") {
                    var arr1 = JSON.parse(val[1]);

                    arr1.forEach(function (element) {
                       debugger
                        data = data + `<div class="col-md-4 col-sm-12">
                    <div class="block">
                        <div class="block-title">
                            <h5><a href="DiplomaSubjectDetails.aspx?subject_id=${element.id}">${element.subjectName}</a></h5>
                        </div>
                        <div class="block-desc">
                            <p class="desc">${element.subject_goal}</p>
                            <div class="row desc-inner">
                                <div class="bock-trainee pull-right">
                                    <img class="avatar" src="${element.trainerImage}" />
                                    <span>${element.full_name}</span>
                                </div> 
                                    <div class="block-date pull-left">
                                 
                                    <span> ${element.created_at_hj} </span>
                                     <i class="fa fa-calendar-check-o"></i>
                                    <span>${element.semster}</span>
                                </div>
                             
                                   
                            </div>
                        </div>
                    </div>
                </div>`;
                    });


                    $("#courses-list").html(data);
                } else {
                    $("#courses-list").html("لا يوجد مواد بهذا الاسم");
                  
                }

            });
       
    
    } catch (err) {
        alert(err);
    }
}

//delete deploma



function deleteDiplome() {
    debugger
    var diplomeID = ($("#Lbldeploma_id").html()); 
    
    var f = confirm("هل  تريد الحذف");
    if (f == true) {
        Diploma_CoursesCls.Delete_deploma(diplomeID, function (val) {
            debugger
            if (val[0] == 1) {
                $("#SavedivLoader").hide();
                alert("تم الحذف بنجاح");
                window.location.replace("Diplomas.aspx");
            




            } else {
                alert('لم يتم الحذف');
            }

        });
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