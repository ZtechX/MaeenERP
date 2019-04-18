
var deleteWebServiceMethod = "coursatCls.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "coursatCls.asmx/Edit";
var formAutoCodeControl = "lblmainid";
var CoursesList = [];
var records_per_page = 6;
var numPages = 0;
var current_page = 1;
$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
    //$("#SavedivLoader").show();

    try {

        drawAllCourses();
       // getcousreCode();
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

        setRequired_Date("divdate1");
        setRequired_Date("divdate2");

        if (checkRequired("divForm") == 1) {

            alert("يرجى ادخال البيانات المطلوبة");
        } else {



            $("#end_datem").val($("#divdate2 #txtDatem").val());
            $("#end_datehj").val($("#divdate2 #txtDateh").val());

            $("#start_date_m").val($("#divdate1 #txtDatem").val());
            $("#start_date_hj").val($("#divdate1 #txtDateh").val());

            var enddate_m = document.getElementById("end_datem").value;

            var startdate_m = document.getElementById("start_date_m").value;

            // console.log(enddate_m + "     " + startdate_m);

            if (startdate_m >= Pub_date_m) {
                if (enddate_m > startdate_m) {
                    var basicData = generateJSONFromControls("divForm");

                    var Id = "";
                    $("#SavedivLoader").show();
                    coursatCls.Save(Id, basicData, function (val) {
                        if (val === true) {

                            $("#addCourse").modal('hide');
                            resetDivControls("divForm");
                            drawAllCourses();
                            $("#SavedivLoader").hide();
                            alert("تم الحفظ بنجاح");
                            getcousreCode();

                        } else {
                            alert("لم يتم الحفظ");
                        }

                    });
                }
                else {
                    alert("تاريخ البداية اكبر من تاريخ النهاية")
                }

            }

            else {
                alert("تاريخ البداية اقل من تاريخ اليوم ")
            }
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
    //debugger
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
    var colors = ["#5cb85c", "#428bca", "#000"];
   

        for (var i = (page - 1) * records_per_page; i < (page * records_per_page) && i < CoursesList.length; i++) {
            var element = CoursesList[i];
            var costdiv = `<i class="fa fa-money" style="color:brown;"></i>
                <span style="color:brown;">${element.price}   </span>`;
            if (element.price == 0) {
                 costdiv = ` <span style="color:blue;">مجاناً</span>`;
            }
            data = data + `<div class="col-md-4 col-sm-12" >
<div class="block">
                        <div class="block-title" style="background:${colors[element.status]}">
                            <h5><a href="courseDetails.aspx?code=${element.code}">${element.name}</a></h5>
                        </div>
                        <div class="block-desc">
<b>${element.department}: <b/>
                            <p class="desc" style="height:100px;">${element.description.substring(0, 200)}....</p>
                            <div class="row desc-inner">
                                <div class="bock-trainee pull-right">
                                    <img class="avatar" src="${element.trImage}" />
                                    <span>${element.full_name}</span>
                                </div>
                                <div class="block-date pull-left">
                                    <i class="fa fa-calendar-check-o"></i>
                                    <span>${element.start_dt_m}   </span>
<br>
${costdiv}
                                </div>
                            </div>
</div>
                    </div>
                </div>`;
        }
        $("#courses-list").html(data);
     
    }

 
function drawAllCourses() {
    try {

        debugger

        coursatCls.get_Courses("", "", Pub_date_m,function (val) {
            //debugger
            var data = "";
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

function drawCourses(filter){
    try {

        debugger
        coursatCls.get_Courses(filter, "", Pub_date_m, function (val) {
            //debugger
            var data = "";
            if (val[0] == "0") {
                
                     $("#courses-list").html("لا يوجد كورسات تم التسجيل بها ");
                  
           
            }
            else {
                var arr1 = JSON.parse(val[1]);
                CoursesList = arr1;

               // debugger
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

            }
           
           
        });
           } catch (err) {
        alert(err);
    }
}

function searchCourses() {
    try {


        var courseName = $("#txt_Search").val();
        coursatCls.get_Courses("", courseName, Pub_date_m, function (val) {
            var data = "";
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