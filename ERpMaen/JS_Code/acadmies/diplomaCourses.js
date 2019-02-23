
//var deleteWebServiceMethod = "coursatCls.asmx/Delete";
// global variable used in row_click and update functions
//var editWebServiceMethod = "coursatCls.asmx/Edit";
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


function saveCourse(){

    try {
        var diplomeID = ($("#Lbldeploma_id").html());      
        //alert($("#Lbldeploma_id").html());
        if (!checkRequired()) {
            debugger
           // alert($("#txtDatem").val());
            $("#date_m").val($("#txtDatem").val());
            $("#date_hj").val($("#txtDateh").val());
          //  alert($("#date_m").val());
            var basicData = generateJSONFromControls("divForm");
      
            var Id = "";
            console.log(basicData);
            Diploma_CoursesCls.Save(Id, diplomeID, basicData, function (val) {
                if (val == true) {
                    debugger;
                    alert("تم الحفظ بنجاح");
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

function drawCourses(){
    try {
        var diplomeID = ($("#Lbldeploma_id").html());
        Diploma_CoursesCls.get_Courses(diplomeID,"",function (val) {
         
            var data = "";
            console.log(val);
            var arr1 = JSON.parse(val[1]);
           
            arr1.forEach(function (element) {
                data = data + `<div class="col-md-4 col-sm-12">
                    <div class="block">
                        <div class="block-title">
                            <h5><a href="courseDetails.aspx?course_id=${element.id}">${element.subjectName}</a></h5>
                        </div>
                        <div class="block-desc">
                            <p class="desc">${element.subject_goal}</p>
                            <div class="row desc-inner">
                                <div class="bock-trainee pull-right">
                                    <img class="avatar" src="../assets/images/101.jpg" />
                                    <span>${element.full_name}</span>
                                </div> 
                                    <div class="block-date pull-left">
                                    <i class="fa fa-list-alt"></i>
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
                            <h5><a href="courseDetails.aspx?course_id=${element.id}">${element.courseName}</a></h5>
                        </div>
                        <div class="block-desc">
                            <p class="desc">${element.subject_goal}</p>
                            <div class="row desc-inner">
                                <div class="bock-trainee pull-right">
                                    <img class="avatar" src="../assets/images/101.jpg" />
                                    <span>${element.full_name}</span>
                                </div>
                                <div class="block-date pull-left">
                                    <i class="fa fa-calendar-check-o"></i>
                                    <span>${element.semster_id}   </span>
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