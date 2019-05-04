
var deleteWebServiceMethod = "Pub_InstructionsCls.asmx/Delete";
// global variable used in row_click and update functions
var editWebServiceMethod = "Pub_InstructionsCls.asmx/Edit";
var formAutoCodeControl = "lblmainid";
var CoursesList = [];
var records_per_page = 4;
var numPages = 0;
var current_page = 1;
$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
   

    try {

        drawCourses(0);
      
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



function uploadClick(id) {
    section_id = id;
    $("#fuPhoto1").find('input[type="file"]').click();
}



function saveInstruction() {


    try {
        
        if (checkRequired("divForm") == 1) {

            alert("يرجى ادخال البيانات المطلوبة");
        } else {
            debugger
                    var basicData = generateJSONFromControls("divForm");

            var instructionId = $("#lblInstruction_id").html();
                   // $("#SavedivLoader").show();
            Pub_InstructionsCls.Save(instructionId, Pub_date_m, Pub_date_hj,basicData, function (val) {
                        if (val === true) {

                            $("#addInstruction").modal('hide');
                            resetDivControls("divForm");
                            drawAllCourses();
                          //  $("#SavedivLoader").hide();
                            alert("تم الحفظ بنجاح");
                            getcousreCode();

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
    //  var colors = ["#5cb85c", "#428bca", "#000"];


    for (var i = (page - 1) * records_per_page; i < (page * records_per_page) && i < CoursesList.length; i++) {
        debugger
        var element = CoursesList[i];
        var file_nm = "";
        var path = element.image;
        if (path != "" && path != null) {
            if (path.indexOf("Acadmies_module/images/") != -1) {
                file_nm = path.split("Acadmies_module/images/")[1];
            }
        }

        var date = element.Created_at_m;

        var arr1 = date.split('/');
        var edit = "";
        var del = "";
        var view = "";
        var img = element.image;
        if (img.endsWith(".pdf")) {
            img = "../images/pdf_img.png";
        }
        if (img.endsWith(".xls") || img.endsWith(".xlsx")) {
            img = "../images/excel_img.png";
        }
        if (img.endsWith(".ppt") || img.endsWith(".pptx")) {
            img = "../images/powerPoint_img.png";
        }
        if (img.endsWith(".doc") || img.endsWith(".docx")) {
            img = "../images/word_img.png";
        }
        if ($("#inst_edit").html() == "True") {
            edit = `<li style="width:33%;"><a href="#" class="bg-warning"  onclick="EditInstruct(${element.id});"><span class="fa fa-edit text-warning"></span></a></li>`;

        } if ($("#inst_del").html() == "True") {
            del = `	<li  style="width:33%;"><a href="#" class="bg-danger" onclick="deleteInstruct(${element.id});"><span class="fa fa-trash text-danger"></span></a></li>`;
        }
        if (element.status == 1) {
            view = ` <li  style="width:33%;"><a href="#" class="bg-info" id="vw_inst" onclick="viewed(${element.id});"><span class="fa fa-eye-slash text-info"></span></a></li>`;
        }
            data = data + `

                        <li>
						<time datetime="${element.Created_at_m}">
							<span class="day">${arr1[0]}</span>
							<span class="month">${arr1[1]}</span>
							<span class="year">${arr1[2]}</span>
							
						</time>
                          
                   
                    	<a target="_blank" href="../${element.image}"><img  src="../${img}"/></a>
                              
                                  
                            
						<div class="info">
							<h2 class="title">${element.title}</h2>
							<p class="desc">${element.description}</p>
							<ul>
								<li style="width:50%;"><a href="../${element.image}"><span class="fa fa-file"></span> ${file_nm}</a></li>
							</ul>
						</div>
                           <div class="social">
							<ul>
${view}
							${edit}
							${del}

							</ul>
						</div>
						
					</li>
`;
    }
    $("#Instructions-list").html(data);

}


 
function drawAllCourses() {
    try {
        debugger
        Pub_InstructionsCls.get_Courses("", "",function (val) {
           
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

        //if (filter == 1) {
        //    document.getElementById("vwId").style.visibility = "hidden";
        //}
        //else {
        //    document.getElementById("vwId").style.visibility = "hidden";
        //}
        Pub_InstructionsCls.get_Courses("", filter, function (val) {
            debugger
            var data = "";
            if (val[0] == "0") {
                
                $("#Instructions-list").html("لايوجد تعاميم");
                  
           
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

        debugger
        var courseName = $("#txt_Search").val();
        Pub_InstructionsCls.get_Courses(courseName, "", function (val) {
            var data = "";
            if (val[0] == 1) {
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
            }
            else {
                $("#Instructions-list").html(" لا يوجد تعاميم بهذا الاسم!");

            }
        });


    } catch (err) {
        alert(err);
    }
}

function deleteInstruct(instructId) {
    var f = confirm("هل  تريد الحذف");
    if (f == true) {
        $("#SavedivLoader").show();
       
        Pub_InstructionsCls.Delete_instruction(instructId, function (val) {
            if (val[0] == 1) {
                $("#SavedivLoader").hide();
                alert("تم الحذف بنجاح");
                drawAllCourses();

            } else {
                alert('لم يتم الحذف');
            }

        });
    }

}

function viewed(instructId) {
    
    Pub_InstructionsCls.Seen_instruction(instructId, Pub_date_m, Pub_date_hj, function (val) {
        if (val == true) {
            drawCourses(0);
                $("#SavedivLoader").hide();
              //  alert("تم الحذف بنجاح");
            //  document.getElementById("vw_inst").style.visibility = "hidden";
            $(this).closest("li").remove();

            } else {
                alert('لم يتم ');
            }

        });
    

}



function EditInstruct(instID) {
    debugger
    $("#lblInstruction_id").html(instID);
    Pub_InstructionsCls.Edit_Instruction(instID, function (val) {
        if (val[0] == "1") {
            

            var data = JSON.parse(val[1]);
            fillControlsFromJson(data[0]);
           

            $("#addInstruction").modal();
            

        }


    });

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

function ClearMe(sender) {
    sender.value = '';
}
function clearContents(sender) {
    { $(sender._element).find('input').val(''); }
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