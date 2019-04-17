
//var deleteWebServiceMethod = "coursatCls.asmx/Delete";
// global variable used in row_click and update functions
//var editWebServiceMethod = "coursatCls.asmx/Edit";
var formAutoCodeControl = "lblmainid";
var diplomasList = [];
var records_per_page = 6;
var numPages = 0;
var current_page = 1;

$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").show();
    drawAllCourses();

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
                    drawCourses();
                    $("#addCourse").modal('hide');
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
    var colors = ["#5cb85c", "#428bca", "#000"];
    for (var i = (page - 1) * records_per_page; i < (page * records_per_page) && i < diplomasList.length; i++) {
        var element = diplomasList[i];
        data = data + `<div class="col-md-4 col-sm-12">
                    <div class="block"  >
                        <div class="block-title" style="background:${colors[element.status]}">
                            <h5><a href="DiplomaCourses.aspx?code=${element.code}">${element.name}</a></h5>
                        </div>
                        <div class="block-desc">
                            <p class="desc">${element.description}</p>
                            <div class="row desc-inner">
                                <div class="bock-trainee pull-right">
                                  
                                    <img  class="avatar" src="${element.userImage}" />
                                 
                                      <span>${element.username}</span>
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

function drawAllCourses(){
    try {
        DiplomasCls.get_deplomas("", "",function (val) {

        
            var data = "";
            console.log(val);
            var arr1 = JSON.parse(val[1]);
            diplomasList = arr1;
            numPages = Math.ceil(diplomasList.length / records_per_page);

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


function drawCourses(filter) {
    try {
        DiplomasCls.get_deplomas(filter, "", function (val) {
            debugger

            //var data = "";
            //console.log(val);
            if (val[0] == "0") {

                $("#diplomas-list").html("لا يوجد كورسات تم التسجيل بها ");


            }
            else {
                var arr1 = JSON.parse(val[1]);
                diplomasList = arr1;
                numPages = Math.ceil(diplomasList.length / records_per_page);

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


function searchDiploma() {
    try {
        
            
            var diplomaName = $("#txt_Search").val();
        DiplomasCls.get_deplomas(diplomaName, function (val) {
                debugger

                var data = "";
                //console.log(val);
                if (val[0] == "1") {
                    var arr1 = JSON.parse(val[1]);

                    arr1.forEach(function (element) {


                        data = data + `<div class="col-md-4 col-sm-12">
                    <div class="block"  >
                        <div class="block-title">
                            <h5><a href="DiplomaCourses.aspx?deploma_id=${element.id}">${element.name}</a></h5>
                        </div>
                        <div class="block-desc">
                            <p class="desc">${element.description}</p>
                            <div class="row desc-inner">
                                <div class="bock-trainee pull-right">
                                  
                                    <img  class="avatar" src="${element.userImage}" />
                                 
                                      <span>${element.username}</span>
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
                   
                    $("#diplomas-list").html("لا يوجد دبلومة بهذا الاسم");
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