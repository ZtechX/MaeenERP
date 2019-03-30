
//var deleteWebServiceMethod = "coursatCls.asmx/Delete";
// global variable used in row_click and update functions
//var editWebServiceMethod = "coursatCls.asmx/Edit";
var formAutoCodeControl = "lblmainid";


var subjectList = [];
var records_per_page = 1;
var numPages = 0;
var current_page = 1;

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
    }
    $("#courses-list").html(data);

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
        debugger
        var diplomeID = ($("#Lbldeploma_id").html());
        Diploma_CoursesCls.get_Courses(diplomeID,"",function (val) {
            debugger
          //  var data = "";
            console.log(val);
            var arr1 = JSON.parse(val[1]);
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