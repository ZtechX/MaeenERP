
//var deleteWebServiceMethod = "coursatCls.asmx/Delete";
// global variable used in row_click and update functions
//var editWebServiceMethod = "coursatCls.asmx/Edit";
var formAutoCodeControl = "lblmainid";


var subjectList = [];
var records_per_page = 6;
var numPages = 0;
var current_page = 1;

$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").show();

    try {
        drawCourses();
        Studentlistview();
       // drawFinanceStudent();
       
     

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



function drawFinanceStudent() {
    try {
        
        var diplomeId = ($("#Lbldeploma_id").html());
        Diploma_CoursesCls.get_StudentFnance(diplomeId, function (val) {

            var data = "";

            if (val[0] === "1") {
                debugger
                var check = ""
                var arr1 = JSON.parse(val[1]);
                var total = 0;
                var rest = arr1[0].price;
            
                
                arr1.forEach(function (element) {

                    var diplome_Price = element.price;
                  
                    if (element.approved == 1) {
                        check = "  تم تاكيد المبلغ "
                        total = total + element.amount;
                        rest = diplome_Price - total;
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
            $("#Rest_money").html(rest);
        });
    }
    catch (err) {
        alert(err);
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

            var diplome_Price = ($("#lbldiplomePrice").html());
            var rest_money = ($("#Rest_money").html());
            var studentAmount = ($("#amount").val());

            if (studentAmount <= diplome_Price && studentAmount <= rest_money) {
                $("#SavedivLoader").show();

                var diplomeID = ($("#Lbldeploma_id").html());

                var basicData = generateJSONFromControls("divformstudentFinanc");

                var Id = "";

                Diploma_CoursesCls.Savefinanc(Id, diplomeID, Pub_date_m, Pub_date_hj, basicData, function (val) {
                    if (val == true) {
                        $("#SavedivLoader").hide();
                        // debugger;
                        drawFinanceStudent();
                        alert("تم الحفظ بنجاح");
                        $("#add_Financial").modal('hide');

                        resetDivControls("divformstudentFinanc");



                    } else {
                        alert("لم يتم الحفظ");
                    }


                });

            }
            else {
                alert("المبلغ المضاف اكبر من سعر الدبلوم")
            }
        }


    } catch (err) {
        alert(err);
    }
}


function drawStudentfinanceforAdmin() {
    try {
        debugger
        var diplomeId = ($("#Lbldeploma_id").html());
        Diploma_CoursesCls.get_StudentFinanceAdmin(diplomeId, function (val) {


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
                        status = "قيد المراجعة"
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

                                                  <label>${element.date_hj} </label>
                                                     
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


function approv_finance(Studentid, payId) {

    var f = confirm("هل  تريد تاكيد المبلغ ");
    if (f == true) {

        $("#SavedivLoader").show();
        var diplomeID = ($("#Lbldeploma_id").html());
        var code = $("#lbldiplomeCode").html();
        Diploma_CoursesCls.approv_finance(Studentid, diplomeID, code, payId, function (val) {


            if (val[0] == 1) {

                $("#SavedivLoader").hide();

                alert("تم التاكيد بنجاح");
                drawStudentfinanceforAdmin();



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
        var diplomeID = ($("#Lbldeploma_id").html());
        var code = $("#lbldiplomeCode").html();
        Diploma_CoursesCls.refuse_finance(Studentid, diplomeID, code, payId, function (val) {


            if (val[0] == 1) {

                $("#SavedivLoader").hide();

                alert("تم الرفض ");
                drawStudentfinanceforAdmin();



            } else {
                showErrorMessage('لم يتم الرفض ');
            }

        });
    }

}


function drawConditionsTable() {
    try {
        debugger
        var diplomeID = ($("#Lbldeploma_id").html());
        Diploma_CoursesCls.get_Condition(diplomeID, function (val) {

            
            var data = "";
            var del_op = $("#dplm_delete_condtion").html();
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);
                arr1.forEach(function (element) {
                    var del_btn = "";
                    if (del_op == "True") {
                        del_btn = `      <button type="button" class="btn btn-danger  btn-s" onclick="DeleteCondition(${element.id});" >

                                                   <i class="fa fa-trash"></i>

                                                                </button>`;
                    }
                    var file_nm = "";
                    var path = element.image;
                    if (path != "" && path != null) {
                        if (path.indexOf("Acadmies_module/images/") != -1) {
                            file_nm = path.split("Acadmies_module/images/")[1];
                        }
                    }
                    var icon = "";
                    if (file_nm != "") {
                        icon = `   <li> <a href="../${element.image}" download>
                                                        <i class="fa fa-download"></i>  

                                                        </a>
                                                        <span>${file_nm}</span>
                                                    </li>  `
                    }
                   
                    data = data + `
                                                     <tr>
                                                      <td>${element.condition} </td>
                                                     <td>
                                                      ${icon}

                                                     </td>
                                                       <td>

                                                          <div class="btn-group pull-left" style="align:center; display: block; margin: auto;">
                                                  ${del_btn}
                                                      
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


function DeleteCondition(condID) {
    var f = confirm("هل  تريد الحذف");
    if (f == true) {
        $("#SavedivLoader").show();
        // debugger
        Diploma_CoursesCls.Delete_condition(condID, function (val) {
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

           // console.log(basicData);
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
        debugger
        setRequired_Date("divdate1");
        //setRequired_Date("divdate2");
        var diplomeID = ($("#Lbldeploma_id").html());  
        var final_Degree = parseInt($("#finaldegree").val());
        var activityDegree = parseInt($("#activityDegree").val());
        var sum = (final_Degree + activityDegree);
        if (checkRequired("divForm") == 1) {
            alert("يرجى ادخال البيانات المطلوبة");
        }
        else if (sum !=100) {
            alert("مجموع درجة الاختبار النهائي زدرجة نصف العام يجب ان يكون 100 درجة")

        }
        else {
          

                var basicData = generateJSONFromControls("divForm");

                var Id = "";
               
            Diploma_CoursesCls.Save(Id, diplomeID, Pub_date_m, Pub_date_hj,basicData, function (val) {
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
                            <p class="desc" style="height:100px;">${element.subject_goal.substring(0, 200)}....</p>
                            <div class="row desc-inner">
                                <div class="bock-trainee pull-right">
                                    <img class="avatar" src="${element.trainerImage}" />
                                    <span>${element.full_name}</span>
                                </div> 
                                    <div class="block-date pull-left">
                                  <i class="fa fa-calendar-check-o"></i>
                                    <span> ${element.created_at_hj} </span>
                                  <br>
                                     <i class="fa fa-book"></i>
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
        var diplomeID = ($("#Lbldeploma_id").html());
        Diploma_CoursesCls.get_StudentList(diplomeID, function (val) {
            var data = "";
            var studid = -1;

            arr1.forEach(function (element) {
                //طلبات الطلاب
                var stdID = element.student_id;
               
                var td_student = "";
                var td_files = "";
                var td_request = "";
                var td_action = "";
                var file_nm = "";
                var path = element.registerFiles;
                if (path != "" && path != null) {
                    if (path.indexOf("Acadmies_module/images/") != -1) {
                        file_nm = path.split("Acadmies_module/images/")[1];
                    }
                }
                if (stdID != studid) {
                    data += td_student + td_request + td_files + td_action;
                    data = data + " </tr >";
                    studid = stdID;
                    data = data + " <tr >";
                    td_student = `<td><label id="${ element.student_id } > ${element.studentName}</label> </td>`;
                    td_request = ` <td><label id='notes_student'> ${element.notes}</label> </td>`;
                    td_action = `<td><select  style="width: 100px; " id="action" > <option value="0">رفض</option><option value="1">قبول</option> </select></td>`;
                    td_files = "<td>";
                } else {
                    td_files += `<li><a id="registerFiles" href="../${element.registerFiles}" download>
                            <i class="fa fa-download"></i></a><span>${file_nm}</span> </li> `;
                }
               
                

                                                           

                         
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


function UploadComplete3(sender, args) {

    var fileLength = args.get_length();
    var fileType = args.get_contentType();
    //alert(sender);
    $('#fileurlfinanc').val('Acadmies_module/coursefiles/' + args.get_fileName());
    $("#FnameFinanc").val(args.get_fileName());


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
                    var grad = "";
                   
                    var finalDegree = element.final_exam_degrees;
                    //var activityDegree = element.activity_degrees;
                    var finalGrad = ((element.final_degree) / (finalDegree)) * 100;
                    if (finalGrad >= 95) {
                        grad = "A+"
                    }
                    else if (finalGrad >= 90) {
                        grad = "A"
                    }
                    else if (finalGrad >= 85) {
                        grad = "B+"
                    }
                    else if (finalGrad >= 80) {
                        grad = "B"
                    }
                    else if (finalGrad >= 75) {
                        grad = "C+"
                    }
                    else if (finalGrad >= 70) {
                        grad = "C"
                    }
                    else if (finalGrad >= 65) {
                        grad = "D+"
                    }
                    else if (finalGrad >= 60) {
                        grad = "D"
                    }
                    else {
                        grad = "E"
                    }
                  
                    



                    data = data + `
                                                           <tr>
                                                            <td> ${element.subjectName}  </td>
                                                      <td>${element.activity_degree}   </td>
                                                          <td> ${element.final_degree}  </td>
                                                          <td>  ${grad} </td>   
                                                                            
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


function fillsubject() {

    var str = "";
  
    Diploma_CoursesCls.get_DiplomeSubject("", function (val) {
        console.log(val);
        debugger
        if (val[0] == 1) {
            var arr1 = JSON.parse(val[1]);
            arr1.forEach(function (element) {
                str = str + `<option value="${element.id}">${element.Description}</option>`;
            });
        }
        $("#ddlcourse").html(str);
    });
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
            //console.log(basicData);
            Diploma_CoursesCls.addsubject(Id, basicData, function (val) {
                if (val == true) {
                    debugger;
                    alert("تم الحفظ بنجاح");
                    fillsubject();
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
           
          
            var arr1 = JSON.parse(val[1]);
            $("#diplome_title").html(arr1[0].dpname);
            $("#lbldiplomePrice").html(arr1[0].diplomePrice);
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
        debugger
        var diplomeID = ($("#Lbldeploma_id").html());
            
        var courseName = $("#txt_Search").val();
        //alert(courseName);
        Diploma_CoursesCls.get_Courses(diplomeID,courseName, function (val) {
               

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
                        <div class="block-desc" >
                            <p class="desc" style="height:100px;">${element.subject_goal.substring(0, 200)}....</p>
                            <div class="row desc-inner">
                                <div class="bock-trainee pull-right">
                                    <img class="avatar" src="${element.trainerImage}" />
                                    <span>${element.full_name}</span>
                                </div> 
                                    <div class="block-date pull-left">
                                  <i class="fa fa-calendar-check-o"></i>
                                    <span> ${element.created_at_hj} </span>
<br>
                                     <i class="fa fa-book"></i>
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