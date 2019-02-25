﻿
//var deleteWebServiceMethod = "coursatCls.asmx/Delete";
// global variable used in row_click and update functions
//var editWebServiceMethod = "coursatCls.asmx/Edit";
var formAutoCodeControl = "lblmainid";
var CoursesList = [];
var records_per_page = 2;
var numPages = 0;
var current_page = 1;
$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").show();
    drawCourses();

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

function saveCourse() {

    try {
        
       
        if (!checkRequired()) {
            debugger
            $("#Label1").html($("#txtDatem").val());
            $("#Label2").html($("#txtDateh").val());
            var basicData = generateJSONFromControls("divForm");
      
            var Id = "";
            console.log(basicData);
            DiplomasCls.Save(Id, basicData, function (val) {
                if (val == true) {
                    debugger;
                    alert("تم الحفظ بنجاح");
                    resetAll();
                    prepareAdd();
                    drawCourses();
                    
                 

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
    debugger
    current_page = page;
    if (page < 1) {
        page = 1;
    }
    else if (page > numPages) {
        page = numPages;
    }
    $(".pagination").find("li").removeClass("active");
    $("#li_" + page).addClass("active");
    $("#diplomas-list").html("");
    var data = "";
    for (var i = (page - 1) * records_per_page; i < (page * records_per_page) && i < CoursesList.length; i++) {
        var element = CoursesList[i];
        data = data + `<div class="col-md-4 col-sm-12">
                    <div class="block"  >
                        <div class="block-title">
                            <h5><a href="DiplomaCourses.aspx?deploma_id=${element.id}">${element.name}</a></h5>
                        </div>
                        <div class="block-desc">
                            <p class="desc">${element.description}</p>
                            <div class="row desc-inner">
                                <div class="bock-trainee pull-right">
                                    <img class="avatar" src="../assets/images/101.jpg" />
                                    <span>${element.add_by}</span>
                                </div>
                               
                                     <div class="block-date pull-left">
                                    <i class="fa fa-price"></i>
                                    <span>${element.price}   </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`;
    }
    $("#diplomas-list").html(data);


}
function drawCourses(){
    try {
        DiplomasCls.get_deplomas( "",function (val) {

        
            var data = "";
            console.log(val);
            var arr1 = JSON.parse(val[1]);
            CoursesList = arr1;
            numPages = Math.ceil(CoursesList.length / records_per_page);

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
        DiplomasCls.get_Courses(courseName, function (val) {
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
                                    <span>${element.add_by}</span>
                                </div>
                               
                                     <div class="block-date pull-left">
                                    <i class="fa fa-price"></i>
                                    <span>${element.price}   </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`;
                        

                    });


                    $("#diplomas-list").html(data);
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