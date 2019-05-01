<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DiplomaCourses.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.DiplomaCourses" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services> 
            <asp:ServiceReference Path="~/ASMX_WebServices/Diploma_CoursesCls.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/MultiFileUploader.asmx" />
        </Services>
    </asp:ScriptManager>

    <style>
        .comp-logo {
    width: 100%;
    height: auto;
    min-height: 70px;
    max-height:70px;
    background-color: #efefef;
    overflow-y: hidden;
    border:2px solid #efefef;
}

.up-btn:hover {
    background-color: #0b4d73;
}
.up-btn {
    width: 100%;
    height: 30px;
    background: #0b669a;
    color: #fff;
    border: none;
    border-radius: 0px 0px 10px 10px;
    transition: all ease 01s;
    display: inline-block;
    vertical-align: middle;
    line-height: 30px;
    text-align: center;
    cursor: pointer;
}

.comp-logo
{
   background-size: contain!important;
    background-repeat: no-repeat !important;
    background-position: center !important;
}
        .btn-group {
            margin: 0px !important;
            width: auto;
        }
    </style>

    <style>
         .modal-content .table-responsive{max-height: 430px; overflow:auto}
        .wrap {
            margin-top: 50px;
            direction: rtl;
        }
        .btn {
            float:left;
        }

        .widget-navigation {
            text-align: center;
        }

        .white-block {
            background: white;
            border: 2px solid white;
            border-radius: 4px;
            padding: 10px;
            margin: 10px 20px 10px 20px;
        }

        .block-title {
            color: #fff;
            background-color: #3b3e47;
            border-color: #f5f7f9;
            padding: 16px;
            border-bottom: 1px solid transparent;
            border-top-right-radius: 3px;
            border-top-left-radius: 3px;
        }

        .block {
            margin-bottom: 15px;
        }

        .block-title a {
            color: white;
        }

            .block-title a:hover {
                color: #428bca;
                cursor: pointer;
            }

        .block-desc {
            background: white;
            border: 2px solid white;
            border-bottom-right-radius: 4px;
            border-bottom-left-radius: 4px;
            padding: 10px;
        }

        .desc-inner {
            margin: 10px
        }

        p.desc {
            border-bottom: 1px dashed;
            padding: 5px;
            font-weight: normal;
            font-size: 12px;
        }

        img.avatar {
            border-radius: 100% !important;
            width: 30px;
            height: 30px;
            margin-left: 9px;
        }

        .btn-group {
            margin: 0px !important;
            width: auto;
        }

          #student {
         
            border-collapse: collapse;
            width: 100%;
        }

            #student td:first-child {
                width: 35%;
            }

            #student td:last-child {
                width: 20%;
            }

            #student td {
                border: 1px solid #ddd;
                padding: 8px;
                height: 60px;
            }

            #student tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #student tr:hover {
                background-color: #ddd;
            }

            #student th {
                padding-top: 10px;
                padding-bottom: 10px;
                text-align: center;
                background-color: #3A364A;
                color: white;
            }


        /*popup edit*/
        .modal-dialog {
            
    position: absolute;
    width: 70%; 
    height: auto;
    left:15%;
   
    border: solid 2px #cccccc;
    background-color: #ffffff;
  
        }
       


.modal-body {
    padding: 15px;
    display: -webkit-box;
}
          
    </style>


    <div class="wrap" >
        <label hidden runat="server"  id="dplm_delete_condtion"></label>
         <style>
                    .form-control { direction:rtl;
                    }
                </style>
                   <div>
                    <script src="../JS_Code/acadmies/diplomaCourses.js"></script>


                          <script type="text/javascript">
                      function UploadComplete2(sender, args) {
                          
                            var fileLength = args.get_length();
                          var fileType = args.get_contentType();
                           var Sender_id = $(sender.get_element()).attr("id");
                          if (Sender_id == "fuPhoto1") {
                              debugger
                              if (fileType.indexOf("image/") != -1) {
                                  $('#img_' + section_id).css('background', 'url("images/' + args.get_fileName() + '")');
                              }
                              else if (fileType.indexOf("presentation") != -1 || fileType.indexOf("ms-powerpoint") != -1 || fileType.indexOf(".pptx") != -1) {
                                  $('#img_' + section_id).css('background', 'url("../images/powerPoint_img.png")');
                              }
                              else if (fileType.indexOf("sheet") != -1 || fileType.indexOf("vnd.ms-excel") != -1 || fileType.indexOf(".xlsx") != -1) {
                                  $('#img_' + section_id).css('background', 'url("../images/excel_img.png")');
                              }
                              else if (fileType.indexOf("document") != -1 || fileType.indexOf("msword") != -1 || fileType.indexOf(".docx") != -1) {
                                  $('#img_' + section_id).css('background', 'url("../images/word_img.png")');
                              }
                              else if (fileType.indexOf("application/pdf") != -1) {
                                  $('#img_' + section_id).css('background', 'url("../images/pdf_img.png")');
                              }
                              var file_nm = args.get_fileName();
                              $('#lblSec_' + section_id).html("Acadmies_module/images/" + file_nm);
                              $('#lblSec_' + section_id).attr("name", file_nm.split('.').slice(0, -1).join('.'));

                          } 
                         
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

                 </script>
                </div>

        <section>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 ">
                    <div class="white-block row">
                        <div class="col-md-4">
                            <h3>
                                <i class="fa fa-book"></i>

                                <span id="diplome_title" runat="server">  </span>
                            </h3>
                        </div>
                        <div class="col-md-6" >
                            <input  id="txt_Search" onkeyup="searchCourses();" type="text" class="form-control" placeholder="بحث عن مادة" />
                        </div>
                        <div class="col-md-2">

                          
                            
                            <% if ERpMaen.LoginInfo.getUserType <> 8 Then
                                    If ERpMaen.LoginInfo.get_form_operation("2") = True Or ERpMaen.LoginInfo.get_form_operation_group("2") Then
                                    %>
                            <div class="btn-group pull-left">
                                <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    الخيارات
                                  <i class="fa fa-cogs"></i>
                                </button>
                                <ul class="dropdown-menu">
                                    <% if ERpMaen.LoginInfo.get_form_operation("3") = True Then   %>
                                     <li><a data-toggle="modal" href="#addsubject"> اضافه مادة
                                    </a></li>
                                   <% End If %>
                                     <% if ERpMaen.LoginInfo.get_form_operation("42") = True Then   %>
                                    <li><a data-toggle="modal" href="#DiplomeConditionsModal" onclick="drawConditionsTable();">  شروط القبول
                                    </a></li>
                                     <% End If %>
                                     <% if ERpMaen.LoginInfo.get_form_operation("6") = True Then   %>
                                     <li><a data-toggle="modal" href="#StudentFinanceModal" onclick ="drawStudentfinanceforAdmin();">   الدفعات المالية للطلاب
                                    </a></li>
                                     <% End If %>
                                   <% if ERpMaen.LoginInfo.get_form_operation("7") = True Then   %>
                                    <li><a data-toggle="modal" href="#addStudentModal" > تقديمات الطلاب
                                    </a></li>
                                     <% End If %>

                                    <% if ERpMaen.LoginInfo.get_form_operation("7") = True Then   %>
                                    <li><a data-toggle="modal" href="#certificateStudentModal" >  ارفاق شهادة
                                    </a></li>
                                     <% End If %>
                                     <% if ERpMaen.LoginInfo.get_form_operation("8") = True Then   %>
                                     <li><a data-toggle="modal" href="#semester_archiveModal">  ارشفه بالفصل الدراسى
                                    </a></li>
                                     <% End If %>
                                     <% if ERpMaen.LoginInfo.get_form_operation("41") = True Then   %>
                                     <li><a   onclick=" Archive();"  > ارشيف الدبلوم
                                    </a></li>
                                     <% End If %>
                                     <% if ERpMaen.LoginInfo.get_form_operation("9") = True Then   %>
                                     <li>
                                        <a onclick="DiplomeView();">تعديل الدبلوم
                                        </a></li>
                                     <% End If %>
                                     <% if ERpMaen.LoginInfo.get_form_operation("10") = True Then   %>
                                          <li><a   onclick=" deleteDiplome();"  > حذف الدبلوم
                                    </a></li>
                                    <% End If %>
                                </ul>
                               
                            </div>
                             <% End If
                                 End If %>
                            <% if ERpMaen.LoginInfo.getUserType = 8 Then   %>
                        
                            <div class="btn-group pull-left">
                                <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    الخيارات
                                  <i class="fa fa-cogs"></i>
                                </button>
                                <ul class="dropdown-menu">
                                     <li><a data-toggle="modal" href="#FinancialModal" onclick="drawFinanceStudent();"> الدفعات المالية
                                    </a></li>
                                  
                                    <li><a data-toggle="modal" href="#studentDegreesDiplome" onclick="studentDegreesIN_Diplome();">السجل الاكاديمى 
                                    </a></li>

                                    
                                   
                                </ul>
                               
                            </div>
                             <% End If %>

                          

                        </div>
                    </div>
                </div>

                 
            </div>
        </section>
        <section>
            <div class="row">
                <label  style="display:none" id="Lbldeploma_id" runat="server" ></label>
                  <label style="display:none" id="lbldiplomeCode" runat="server" ></label>
                 <label style="display:none" id="lbldiplomePrice"  ></label>
                <div class="col-md-12 col-sm-12 col-xs-12 " id="courses-list">

                </div>
            </div>
        </section>
        <div class="widget-navigation">
            <ul class="pagination">
                <li class="paginate_button previous"><a>السابق</a></li>
                <li class="paginate_button next" id="default-datatable_next"><a>التالي</a></li>
            </ul>
        </div>

        <div class="modal" id="addsubject" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">اضافة مادة</h4>
                       
                                 <div class="btn-group pull-left">
                                     
                            </div>
                                     
                           
                    </div>
                    <div class="modal-body">
                        <div class="row" id="divForm">
                             <div class="col-md-6">
                          
                            <div class=" form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label class="label-required">المادة  </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <div class="row">
                                    <div class="col-md-10">
                                    <asp:DropDownList dbcolumn="subject_id" class="form-control" required ClientIDMode="Static" ID="ddlcourse" runat="server">
                                    </asp:DropDownList>
                                        </div>
                                    <div class="col-md-2">
                                                                   <button type="button" class="btn btn-info " data-toggle="modal"  data-target="#newsubject">    <i class="fa fa-plus"></i></button>
                                            </div>
                                        </div>
                                </div>
                            </div>
                             <div class=" form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label > مدة المحاضره      </label>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="lecture_time"  placeholder="مدة المحاضرة بالدقيقة" id="Text4"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    
                                        </div>
                            </div>


                                             <div class=" form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label  class="label-required">  عدد الوحدات </label>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="Units_Num" required  placeholder=" عدد وحدات المادة" id="subject_units"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    
                                        </div>
                            </div>

                                  <div class=" form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label class="label-required">الفصل الدراسى  </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <asp:DropDownList dbcolumn="semster_id" class="form-control" required ClientIDMode="Static" ID="ddlsemster" runat="server">
                                    </asp:DropDownList>
                                   
                                </div>
                            </div>

                                 <div class="  form-group ">
                                     <div class="col-md-3 col-sm-12">
                                         <label class="label-required">المدرب</label>
                                     </div>

                                     <div class="col-md-9 col-sm-12">
                                         <asp:DropDownList dbcolumn="trainer_id" required class="form-control" ClientIDMode="Static" ID="ddltrainer" runat="server">
                                         </asp:DropDownList>

                                     </div>
                                 </div>
                                  
                                               
                            </div>
                                
                             <div class="col-md-6">
                                 
                                 <div class=" form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label class="label-required" > الرمز </label>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input  placeholder="رمز المادة" dbcolumn="sub_code"  required type="text" id="sub_code" class="form-control" runat="server" clientidmode="Static" />
                                        </div>
                            </div>

                                 
                                   <div class=" form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label class="label-required" >  الاختبار النهائي </label>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" placeholder="درجة الاختبار النهائى" dbcolumn="final_exam_degrees"  required type="text" id="finaldegree" class="form-control" runat="server" clientidmode="Static" />
                                        </div>
                            </div>

                                   <div class=" form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label class="label-required">  اعمال السنة</label>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);"  placeholder="درجة اعمال السنة" dbcolumn="activity_degrees" required type="text" id="activityDegree" class="form-control" runat="server" clientidmode="Static" />
                                 
                                        </div>
                            </div>

                                 <div class="  form-group">
                                     <div class="col-md-3 col-sm-12">
                                         <label for="Name">اهداف  المادة </label>

                                     </div>
                                     <div class="col-md-9 col-sm-12">
                                         <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="subject_goal" ClientIDMode="Static" ID="description" runat="server">
                                         </asp:TextBox>


                                     </div>
                                 </div>
                                  </div>
                          
                                 </div>



                        </div>
                  
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary"  onclick="savesubject();">حفظ </button>

                    </div>
                      </div>
                </div>
            </div>

        <div class="modal" id="newsubject" tabindex="-1" role="dialog">
            <div class="modal-dialog" style="width: 350px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title"> مادة جديدة</h4>
                    </div>
                    <div class="modal-body">
                        <div class="col-md-12" id="divFormnewsub">
                          
                          
                            <div class="row form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label>المادة  </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                            <input dbcolumn="Description" type="text" id="subject"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    <br />
                                        </div>
                            </div>

                                     
                          
                                 </div>



                        </div>
                  
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary"  onclick="addsubject();">حفظ </button>

                    </div>
                      </div>
                </div>
            </div>

        <div class="modal" id="semester_archiveModal" data-easein="perspectiveRightIn" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title"> ارشفه الفصول الدراسية</h4>
                        </div>
                        <div class="modal-body">
                            <div id="divFormArchiveSemester">

                                
                                     <div class="row form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label>الفصل الدراسى  </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <asp:DropDownList dbcolumn="semster_id" class="form-control" ClientIDMode="Static" ID="ddlsemster2" runat="server">
                                    </asp:DropDownList>
                                   
                                </div>
                            </div>


                            </div>


                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="archiveSemester();">اضافه </button>
                        </div>
                    </div>
                </div>
            </div>

        <div class="modal" id="DiplomeConditionsModal"  tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">   شروط القبول فى الدبلوم</h4>
                        </div>
                        <div class="modal-body">
                           
                      <div class="trans-data col-xs-12" >
                           <% if ERpMaen.LoginInfo.get_form_operation("4") = True Then   %>
                            <button type="button" class="btn btn-info " data-toggle="modal" data-target="#order_addcondition"> اضافة شرط  <i class="fa fa-plus"></i></button>
                                           <% End If %>                       
                <div class="table-responsive">
                    <table class="table table-bordered table-hover">
                        <tr>
                            <th>الشرط </th>
                            <th>الملف</th>
                            <th>الاجراء</th>
                            

                        </tr>
                        <tbody id="conditions-table">

                        </tbody>
                        </table>
                    </div>
                    </div>
                             
                            </div>


                        
                        <div class="modal-footer">
                           
                        </div>
                        </div>
                    </div>
                </div>
            

        <div class="modal" id="order_addcondition"  tabindex="-1" role="dialog">
                <div class="modal-dialog" style="width:450px;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">اضافة شرط</h4>
                        </div>
                        <div class="modal-body">
                            <div id="divFormcondition">

                                <div class=" row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label>الشرط      </label>
                                    </div>

                                   <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control"  TextMode="multiline"  class="form-control" dbColumn="condition" required  ClientIDMode="Static" ID="TextBox5" runat="server">
                                            </asp:TextBox>
                                        </div>
                                </div>

                                <div class="form-group row">

                                    <div>
                                        <input id="fileURL" type="hidden"  dbcolumn="image" runat="server" />
                                        <input id="FName" type="text" required readonly="readonly" runat="server" />

                                    </div>
                                    <div class="clear">
                                    </div>
                                    <asp:AsyncFileUpload ID="fuFile1" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                        OnClientUploadComplete="UploadComplete2" />
                                </div>
                            </div>


                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="addCondition();">اضافه </button>
                        </div>
                    </div>
                </div>
            </div>

        <div class="modal" id="addCourse" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">تعديل الدبلوم </h4>
                    </div>
                    <div class="modal-body">
                        <div id="divFormDiplome">
                             <div class="col-md-6"> 
                            <div class=" form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label for="Name" class="label-required">عنوان الدبلوم </label>

                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <asp:TextBox SkinID="form-control" required class="form-control" dbColumn="name" ClientIDMode="Static" ID="courseTitle" runat="server">
                                    </asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="courseTitle"
                                        ErrorMessage="من فضلك أدخل عنوان الدبلومه  " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                             <div class="form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label> سعر الدبلوم </label>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="price" type="text" id="price"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    <br />
                                        </div>
                            </div>
                                   <div class=" form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label class="label-required">القسم </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <asp:DropDownList dbcolumn="category_id" class="form-control" ClientIDMode="Static" ID="ddlcategory" runat="server">
                                    </asp:DropDownList>
                                    <br />
                                </div>
                            </div>

                                   <div class=" form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label class="label-required">المنسق </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <asp:DropDownList dbcolumn="coordinator_id" required class="form-control" ClientIDMode="Static" ID="ddlcoordinator" runat="server">
                                    </asp:DropDownList>
                                    <br />
                                </div>
                            </div>
                         
           
                                 </div>




                              <div class="col-md-6"> 
                           
                              <div class="form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label>  سعة الدبلوم </lable>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="student_number" placeholder="اجمالى عدد الطلاب" type="text" id="studentnum"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    <br />
                                        </div>
                            </div>
                          
                                  <div class=" form-group">
                                              <div class="col-md-3 col-sm-12">
                                                <label class="">الحالة     </label>
                                            </div>

                                         <div class="col-md-9 col-sm-12">
                                                <select class="form-control" dbcolumn="status">
                                                    <option value="0">جديدة</option>
                                                    <option value="1">حالية </option>
                                                    <option value="2">مكتملة</option>
                                                    <option value="3">معلقة</option>
                                                </select>


                                            </div>
                                        </div>

                            <div class="form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label for="Name" >تفاصيل الدبلوم </label>

                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="description" ClientIDMode="Static" ID="TextBox1" runat="server">
                                    </asp:TextBox>


                                </div>
                            </div>
                            <div class="row">
                                <br />
                                <div class="col-md-3 col-sm-12">
                                    <label for="email">مميزة </label>
                                </div>
                                <div class="col-md-3 col-sm-12">
                                    <input type="checkbox" dbcolumn="features" id="features" style="width: 40px;" class="form-control" />

                                </div>
                            </div>
                                 </div>



                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary"  onclick="savediplome();">حفظ </button>

                      <%--  <button type="button" data-dismiss="modal" id="savepop" class="btn btn-primary">حفظ </button>--%>
                    </div>
                </div>
            </div>
        </div>



            <% if ERpMaen.LoginInfo.getUserType = 8 Then   %>

             <div class="modal fade" id="studentDegreesDiplome" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">السجل الاكاديمى</h4>
                        </div>
                        <br /> 
                       
                        <div class="col-md-12">

                        <div class="  form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label  class="label-required">اختر الفصل الدراسى</label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <asp:DropDownList dbcolumn="semster_id" required class="form-control" ClientIDMode="Static" onchange="studentDegreesIN_Diplome();" ID="ddlsubject_Semester" runat="server">
                                    </asp:DropDownList>
                                    
                                </div>
                            </div>
                      </div>
                        <div class="table-responsive" >
                            <table class="table table-bordered table-hover"  id="newitem">
                               <thead>
                                <tr>
                                    <th>المادة </th>
                                    <th>  درجة اعمال السنة </th>
                                     <th> درجة الاختبار النهائي</th>
                                     <th> المجموع</th>
                                      <th> التقدير</th>
                                    
                                  
                                </tr>
                                 </thead>
                                <tbody  id="studentDiplomeDegreestable">
                                         
                                </tbody>
                                    

                            </table>
                        </div>


                        <div class="modal-footer">
                            <button type="button"   data-dismiss="modal" class="btn btn-primary" >close </button>
                        </div>
                    </div>
                </div>
            </div>

        

            <div class="modal fade" id="FinancialModal" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">   الماليات   </h4>
                        </div>
                        <div class="modal-body" style="direction:rtl;">
                            
                            <div class="trans-data col-xs-12" >

                                   <button type="button" class="btn btn-info " data-toggle="modal" data-target="#add_Financial"> اضافة دفعة مالية  <i class="fa fa-plus"></i></button>
                                      <div class="table-responsive" >
                                             
                                                <table class="table table-bordered">
                                                     <tr>
                                                       
                                                        <th>المبلغ</th>
                                                        <th>الحاله </th>
                                                     

                                                    </tr>
                                                    <tbody id="financestudent">

                                                    </tbody>
                                                    <tfoot>
                                                        <tr>
                                                            <td>اجمالى المبالغ المؤكدة</td>
                                                            <td><label id="total_money"></label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>  المبلغ المتبقى</td>
                                                            <td><label id="Rest_money"></label></td>
                                                        </tr>
                                                    </tfoot>

                                                 
                                                </table>
                                            </div>

                               
                                </div>
                           
                            
                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" data-dismiss="modal" >close </button>
                        </div>
                    </div>
                </div>
            </div>
        <div class="modal fade" id="add_Financial" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">  اضافة دفعة مالية   </h4>
                        </div>
                        <div class="modal-body">
                           
                            <div id="divformstudentFinanc">

                                <div class="row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label class="label-required"> المبلغ   </label>
                                    </div>

                                    <div class="col-md-9 col-sm-12">
                                        <input onkeypress="return isNumber(event);" placeholder="  قيمة المبلغ بالريال" required dbcolumn="amount" type="text" id="amount"
                                            class="form-control" runat="server" clientidmode="Static" />


                                    </div>
                                </div>

                                <div class=" row form-group ">
                                    <div class="col-md-3 col-sm-12">
                                        <label class="label-required">طريقة الدفع </label>
                                    </div>

                                    <div class="col-md-9 col-sm-12">
                                        <asp:DropDownList dbcolumn="payment_type" required class="form-control" ClientIDMode="Static" ID="ddlpayment_type" runat="server">
                                        </asp:DropDownList>

                                    </div>
                                </div>

                                <div class=" row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label>ملاحظات      </label>
                                    </div>

                                    <div class="col-md-9 col-sm-12">
                                        <asp:TextBox SkinID="form-control" TextMode="multiline"  class="form-control" dbColumn="notes" ClientIDMode="Static" ID="TextBox8" runat="server">
                                        </asp:TextBox>
                                    </div>
                                </div>


                                <div class="form-group row">

                                    <div>
                                        <input id="fileurlfinanc" type="hidden" dbcolumn="image" runat="server" />
                                        <input id="FnameFinanc" type="text" required readonly="readonly" runat="server" />

                                    </div>
                                    <div class="clear">
                                    </div>
                                    <asp:AsyncFileUpload ID="fufile3" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                        OnClientUploadComplete="UploadComplete3" />
                                </div>
                                 </div>
                             
                             
                        </div>
                      
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="addfinancial();">حفظ </button>
                        </div>
                    </div>
                    </div>
                </div>
            
            


             <% End If %>

             <div class="modal fade" id="addStudentModal" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">تقديمات الطلاب   </h4>
                        </div>
                        <%--  جدول الطلاب--%>
                        <style>
                            #allStudentlist th {
                                text-align:right;
                            }
                        </style>
                        <div class="table-responsive" id="allStudentlist" style="direction:rtl;">
                            <table class="table table-bordered table-hover"  id="newitem">
                               <thead>
                                <tr>
                                    <th>الطالب </th>
                                    <th>الطلب</th>
                                     <th>الملف</th>
                                     <th>الاجراء</th>
                                  
                                </tr>
                                 </thead>
                                <tbody  id="action_courseStudents">
                                         
                                </tbody>
                                    

                            </table>
                        </div>


                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="AddDiplome_Student();">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>

        <div class="modal fade" id="certificateStudentModal" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">ارافاق شهادة    </h4>
                        </div>
                        <%--  جدول الطلاب--%>
                        <style>
                            #StudentlistCertificates th {
                                text-align:right;
                            }
                        </style>
                        <div class="table-responsive" id="StudentlistCertificates" style="direction:rtl;">
                            <table class="table table-bordered table-hover"  id="newitem">
                               <thead>
                                <tr>
                                    <th>الطالب </th>
                                    <th> كود الشهادة</th>
                                     <th>ارفاق شهادة</th>
                                   
                                  
                                </tr>
                                 </thead>
                                <tbody  id="Students_Certificates">
                                         
                                </tbody>
                                    

                            </table>
                        </div>


                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="AddStudent_Certificate();">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>

    <div class="modal fade" id="StudentFinanceModal" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title"> الدفعات المالية   </h4>
                        </div>
                      
                       <div class="table-responsive" style="direction:rtl;" >
                                             

                                                <table id="student">
                                                     <tr>
                                                        <th>الطالب </th>
                                                        <th>المبلغ</th>
                                                           <th>التاريخ</th>
                                                        <th>الصورة </th>
                                                           <th>الحالة </th>
                                                         <th>الاجراء </th>

                                                    </tr>
                                                    <tbody id="Student_Finance">

                                                    </tbody>


                                                 <%-- here--%>
                                                </table>
                                            </div>


                        <div class="modal-footer">
                          
                        </div>
                    </div>
                </div>
            </div> 
        
    </div>

         <asp:AsyncFileUpload ID="fuPhoto1" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                                    OnClientUploadComplete="UploadComplete2" style="display:none;"
                                                    FailedValidation="False" />
 
</asp:Content>
