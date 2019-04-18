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


    <div class="wrap">
         <div>
                    <script src="../JS_Code/acadmies/diplomaCourses.js"></script>

                </div>

        <section>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 ">
                    <div class="white-block row">
                        <div class="col-md-4">
                            <h3>
                                <i class="fa fa-book"></i>

                                <span id="diplome_title">  </span>
                            </h3>
                        </div>
                        <div class="col-md-6" >
                            <input  id="txt_Search" onkeypress="searchCourses();" type="text" class="form-control" placeholder="بحث عن مادة" />
                        </div>
                        <div class="col-md-2">

                            <%--<div class="btn-group pull-left">
                                <button type="button" class="btn btn-info " data-toggle="modal" data-target="#addsubject">اضافة مادة <i class="fa fa-plus"></i></button>

                            </div>--%>
                            
                            <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
                            <div class="btn-group pull-left">
                                <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    الخيارات
                                  <i class="fa fa-cogs"></i>
                                </button>
                                <ul class="dropdown-menu">
                                     <li><a data-toggle="modal" href="#addsubject"> اضافه مادة
                                    </a></li>
                                    <li>
                                        <a onclick="DiplomeView();">تعديل
                                        </a></li>
                                    <li><a data-toggle="modal" href="#semester_archiveModal">  ارشيف بالفصل الدراسى
                                    </a></li>

                                    <li><a data-toggle="modal" href="#order_addcondition"> اضافه شرط
                                    </a></li>
                                  
                                    <li><a   onclick=" deleteDiplome();"  > حذف
                                    </a></li>
                                    <li><a data-toggle="modal" href="#addStudentModal" > الطلاب
                                    </a></li>
                                     <li><a   onclick=" Archive();"  > الارشيف
                                    </a></li>
                                   
                                </ul>
                               
                            </div>
                             <% End If %>
                            <% if ERpMaen.LoginInfo.getUserType = 8 Then   %>
                        
                            <div class="btn-group pull-left">
                                <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    الخيارات
                                  <i class="fa fa-cogs"></i>
                                </button>
                                <ul class="dropdown-menu">
                                     <li><a data-toggle="modal" href="#add_Financial">  اضافة مالية
                                    </a></li>
                                  
                                    <li><a data-toggle="modal" href="#studentDegreesDiplome" onclick="studentDegreesIN_Diplome();">درجات الدبلوم
                                    </a></li>

                                    
                                   
                                </ul>
                               
                            </div>
                             <% End If %>

                          

                        </div>
                    </div>
                </div>

                 <%--<div class="col-md-12 col-sm-12 col-xs-12 ">
                        <div class="col-md-4">

                            <div class="btn-group pull-right">
                                <button type="button" class="btn btn-danger " onclick="deleteDiplome();"> حذف الدبلومه  <i class="fa fa-trash"></i></button>

                            </div>
                        </div>

                     </div>--%>
            </div>
        </section>
        <section>
            <div class="row">
                <label  style="display:none" id="Lbldeploma_id" runat="server" ></label>
                  <label style="display:none" id="lbldiplomeCode" runat="server" ></label>
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

                          
                    </div>
                    <div class="modal-body">
                        <div class="col-md-12" id="divForm">
                             <div class="col-md-6">
                          
                            <div class="row form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label>المادة  </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <asp:DropDownList dbcolumn="subject_id" class="form-control" ClientIDMode="Static" ID="ddlcourse" runat="server">
                                    </asp:DropDownList>
                                   
                                </div>
                            </div>
                             <div class="row form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label > مدة المحاضره      </label>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="lecture_time" type="text" id="Text4"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    
                                        </div>
                            </div>

                                  <div class="row form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label>الفصل الدراسى  </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <asp:DropDownList dbcolumn="semster_id" class="form-control" ClientIDMode="Static" ID="ddlsemster" runat="server">
                                    </asp:DropDownList>
                                   
                                </div>
                            </div>
                                    <div class="row form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label >   الدرجة النهائية      </label>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="final_exam_degrees" type="text" id="Text2"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    <br />
                                        </div>
                            </div>


                                   <div class="row form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label > درجة نصف العام  </label>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="activity_degrees" type="text" id="Text3"
                                                class="form-control" runat="server" clientidmode="Static" />
                                 
                                        </div>
                            </div>
                                               
                            </div>
                                
                             <div class="col-md-6">
                                 <div class="row form-group">
                                 <div class="btn-group pull-left">
                                <button type="button" class="btn btn-info " data-toggle="modal" data-target="#newsubject"> new subject <i class="fa fa-plus"></i></button>

                            </div>
                                     </div>


                                 <div class=" row form-group">
                                     <div class="col-md-3 col-sm-12">
                                         <label class="label-required">
                                             التاريخ  
                                         </label>
                                     </div>

                                     <div class="col-md-9 col-sm-12">

                                         <div class="fancy-form" id="divdate1">
                                             <input dbcolumn="created_at_hj" type="hidden" id="date_hj" />
                                             <input dbcolumn="created_at_m" type="hidden" id="date_m" />
                                             <uc1:HijriCalendar runat="server" ID="HijriCalendar" />
                                         </div>

                                     </div>
                                 </div>


                                 <div class=" row form-group ">
                                     <div class="col-md-3 col-sm-12">
                                         <label class="label-required">المدرب</label>
                                     </div>

                                     <div class="col-md-9 col-sm-12">
                                         <asp:DropDownList dbcolumn="trainer_id" required class="form-control" ClientIDMode="Static" ID="ddltrainer" runat="server">
                                         </asp:DropDownList>

                                     </div>
                                 </div>

                                 <div class=" row form-group">
                                     <div class="col-md-3 col-sm-12">
                                         <label for="Name">هدف  المادة </label>

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
            <div class="modal-dialog">
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
                            <h4 class="modal-title">اضافة شرط</h4>
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


        <div class="modal" id="order_addcondition" data-easein="perspectiveRightIn" tabindex="-1" role="dialog">
                <div class="modal-dialog">
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
                        <h4 class="modal-title">اضافة دبلوم</h4>
                    </div>
                    <div class="modal-body">
                        <div id="divFormDiplome">
                             <div class="col-md-6"> <%--col first--%>
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
                                    <label>القسم </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <asp:DropDownList dbcolumn="category_id" class="form-control" ClientIDMode="Static" ID="ddlcategory" runat="server">
                                    </asp:DropDownList>
                                    <br />
                                </div>
                            </div>


                         
           
                                 </div>
                              <div class="col-md-6"> <%--second col--%>
                           
                                 
                           
                          
                              <div class="form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label > عدد الطلاب    </lable>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="student_number" type="text" id="studentnum"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    <br />
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
                            <h4 class="modal-title"> درجات مواد الدبلوم   </h4>
                        </div>
                        <%--  جدول الطلاب--%>
                        <div class="table-responsive" >
                            <table class="table table-bordered table-hover"  id="newitem">
                               <thead>
                                <tr>
                                    <th>المادة </th>
                                    <th>درجة النشاط</th>
                                     <th>الدرجة النهائية</th>
                                    
                                  
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


            <div class="modal fade" id="add_Financial" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title"> مالية الطلاب   </h4>
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
                                        <asp:TextBox SkinID="form-control" TextMode="multiline" required class="form-control" dbColumn="notes" ClientIDMode="Static" ID="TextBox8" runat="server">
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
                                    <asp:AsyncFileUpload ID="fufile7" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                        OnClientUploadComplete="UploadComplete8" />
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
                        <div class="table-responsive" id="allStudentlist">
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

        </div>


    <%-- end form--%>
</asp:Content>

<%--select acd_diplome_subjects.id as 'AutoCodeHide' ,acd_diplome_subjects.subject_id ,acd_diplome_subjects.price ,acd_diplome_subjects.student_number , acd_diplome_subjects.subject_goal from acd_diplome_subjects--%>