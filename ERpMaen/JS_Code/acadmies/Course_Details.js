
//var deleteWebServiceMethod = "coursatCls.asmx/Delete";
// global variable used in row_click and update functions
//var editWebServiceMethod = "coursatCls.asmx/Edit";
var formAutoCodeControl = "lblmainid";

$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").show();
    //drawCourses();
    GetCourses();
    drawLecturesTable();
    drawStudentTable();

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

function addLecture() {

    try {
        
       
        if (!checkRequired()) {
            debugger
            $("#date_m").val($("#txtDatem").val());
            $("#date_hj").val($("#txtDateh").val());
          //  alert($("#date_m").val());
            var CourseId = ($("#Lblcourse_id").html()); 
            var basicData = generateJSONFromControls("divFormTable");
      
            var Id = "";
            //console.log(basicData);
            courseDetailsCls.Save(Id, CourseId , basicData, function (val) {
                if (val == true) {
                    debugger;
                    alert("تم الحفظ بنجاح");
                    resetAll();
                    prepareAdd();
                    //drawCourses();
                    
                 

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


        if (!checkRequired()) {
            debugger
           
            var basicData = generateJSONFromControls("divformNote");


            var Id = "";
            console.log(basicData);
            courseDetailsCls.SaveNote(Id,basicData, function (val) {
                if (val == true) {
                    debugger;
                    alert("تم الحفظ بنجاح");
                    resetAll();
                    prepareAdd();
                    //drawCourses();



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

function GetCourses(){
    try {

        var CourseId = ($("#Lblcourse_id").html()); 
        //alert(CourseId);
        courseDetailsCls.get_course(CourseId, function (val) {

             //alert(val);
        
            //var data = "";
            console.log(val);
            
            var arr1 = JSON.parse(val[1]);
            $("#course_title").html(arr1[0].name);
            $("#course_details").html(arr1[0].description);
            $("#course_date").html(arr1[0].start_dt_hj);
            $("#course_price").html(arr1[0].price);
            //$("#course_duration").html(arr1[0].course_name);
            $("#course_category").html(arr1[0].course_category);
            $("#course_stat").html(arr1[0].StatusCourse);
            $("#course_trainer").html(arr1[0].trainer_name);
            $("#course_duration").html(arr1[0].duration);

            

        });
    }
    catch (err) {
        alert(err);
    }
}

function drawLecturesTable() {
    try {
        var CourseId = ($("#Lblcourse_id").html());
        courseDetailsCls.get_LectureTable(CourseId, function (val) {


            var data = "";
            console.log(val);
            if (val[0] == 1) {
                var arr1 = JSON.parse(val[1]);

                arr1.forEach(function (element) {

                    data = data + `
                                                     <tr>
                                                      <td>${element.lecture_code} </td>
                                                     <td>${element.dt_hj}  </td>
                                                    <td>${element.start_time}   </td>
                                                       <td> ${element.hall_id} </td>
                                                           <td>
                                                                                  
                                                          <div class="btn-group pull-left" style="position:absolute">
                                                        <button type="button" class="btn btn-info dropdown-toggle btn-xs" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                          
                                                   <i class="fa fa-cogs"></i>
      
                                                                </button>
                                                                <ul  class="dropdown-menu">
                                                                                            
                                                              <li><a  data-toggle="modal" href="#order_edit">
                                                                      تعديل 
                                                                       </a></li>
                                                                            <li>
                                                                      <a data-toggle="modal" href="#">
                                                                         حذف
                                                                             </a>
                                                                               </li>
                                                                                  <li>
                                                                        <a data-toggle="modal" href="#absence">
                                                                            الغياب
                                                                           </a>
                                                                                 </li>
                                                                       </ul>
                                                                                           
                                                                         </div>
                                                                                           
                                                                          </td>

                                                                            </tr>
                 
                                                                               
                                                                      
`;
                });

              
            }
            $("#lectures-table").html(data);
        });
    } catch (err) {
        alert(err);
    }
}

/// students////


function drawStudentTable() {
    try {
        var CourseId = ($("#Lblcourse_id").html());
        courseDetailsCls.get_StudentTable(CourseId, function (val) {


            var data = "";

            console.log(val);
            var arr1 = JSON.parse(val[1]);

            arr1.forEach(function (element) {

                data = data + `<table id="student">

                                                    <tr>
                                                        <td>

                                                            <img src="../assets/images/210.jpg" />

                                                        </td>

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
                                                                                            <a data-toggle="modal" href="#order_edit">
                                                                                                تعديل 
                                                                                            </a>

                                                                                                </li>
                                                                                                <li>
                                                                                            <a  data-toggle="modal" href="#">
                                                                                                حذف
                                                                                            </a>
                                                                                                    </li>
                                                                                                <li>
                                                                                            <a  data-toggle="modal" href="#degrees">
                                                                                                الدرجات 
                                                                                            </a>
                                                                                                    </li>
                                                                                                <li>
                                                                                            <a  data-toggle="modal" href="#add_note">
                                                                                                اضافة ملاحظة
                                                                                            </a>
                                                                                                    </li>
                                                                                                </ul>
                                                                                           
                                                                                        </div>
                                                                                          

                                                        </td>

                                                    </tr>
                                                  
                                                </table>
                 
                                                                               
                                                                      
`;
            });
            //data += "</table></div></div ></div >";
            //console.log(data);
            $("#StudentTable").html(data);
        });
    } catch (err) {
        alert(err);
    }
}

//function searchCourses() {
//    try {
        
            
//            var courseName = $("#txt_Search").val();
//        courseDetailsCls.get_Courses(courseName, function (val) {
//                debugger

//                var data = "";
//                //console.log(val);
//                if (val[0] == "1") {
//                    var arr1 = JSON.parse(val[1]);

//                    arr1.forEach(function (element) {
//                       debugger
//                        data = data + `<div class="col-md-4 col-sm-12">
//                    <div class="block">
//                        <div class="block-title">
//                            <h5><a href="courseDetails.aspx?course_id=${element.id}">${element.name}</a></h5>
//                        </div>
//                        <div class="block-desc">
//                            <p class="desc">${element.description}</p>
//                            <div class="row desc-inner">
//                                <div class="bock-trainee pull-right">
//                                    <img class="avatar" src="../assets/images/101.jpg" />
//                                    <span>${element.add_by}</span>
//                                </div>
                               
//                                     <div class="block-date pull-left">
//                                    <i class="fa fa-price"></i>
//                                    <span>${element.price}   </span>
//                                </div>
//                            </div>
//                        </div>
//                    </div>
//                </div>`;
                        

//                    });


//                    $("#diplomas-list").html(data);
//                } else {
//                    showErrorMessage("Not Found");
//                }

//            });
       
    
//    } catch (err) {
//        alert(err);
//    }
//}




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