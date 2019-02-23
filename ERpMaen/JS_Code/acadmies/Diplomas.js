
//var deleteWebServiceMethod = "coursatCls.asmx/Delete";
// global variable used in row_click and update functions
//var editWebServiceMethod = "coursatCls.asmx/Edit";
var formAutoCodeControl = "lblmainid";

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

function drawCourses(){
    try {
        DiplomasCls.get_deplomas( "",function (val) {

        
            var data = "";
            console.log(val);
            var arr1 = JSON.parse(val[1]);
            arr1.forEach(function (element) {
                data = data + `<div class="col-md-4 col-sm-12">
                    <div class="block">
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
            });
            //for (var i = 0; i < arr1.length; i++) {

            //data = data + '<div class="col-md-4 col-sm-12"> <div class="block">'+
            //    '<div class="block-title"><h5><a href="courseDetails.aspx">' + arr1[i].name +'</a></h5></div>' +
            //    '<div class="block-desc"> <p class="desc">' + arr1[i].description+'</p>'+
            //            '<div class="row desc-inner"><div class="bock-trainee pull-right">' +
            //                    '<img class="avatar" src="../assets/images/101.jpg" />' +
            //        '<span>'+ arr1[i].trainer_id +' </span>'+' </div>'+
            //               ' <div class="block-date pull-left">'+
            //                   ' <i class="fa fa-calendar-check-o"></i>'+
            //            ' <span>' + arr1[i].start_date_hj+' </span>'+
            //               ' </div></div> </div></div></div>';
            //}
            $("#diplomas-list").html(data);
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