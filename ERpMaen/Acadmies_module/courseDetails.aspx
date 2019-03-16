<%@ Page Language="vb" AutoEventWireup="false"   CodeBehind="CourseDetails.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.CourseDetails" EnableEventValidation="false" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>

<%--<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="ucmf" TagName="MultiPhotoUpload" %>--%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/courseDetailsCls.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/MultiFileUploader.asmx" />
        </Services>
    </asp:ScriptManager>
<%--        <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" crossorigin="anonymous">--%>

<%--    <link rel="stylesheet" href="../libs/bower/font-awesome/css/font-awesome.min.css">--%>
<%--    <link rel="stylesheet" href="../libs/bower/material-design-iconic-font/dist/css/material-design-iconic-font.css">--%>
    <link rel="stylesheet" href="../assets/css/app.min.css">
<%--    <script src="../libs/bower/breakpoints.js/dist/breakpoints.min.js"></script>--%>
 <%--   <link rel="stylesheet" type="text/css" href="../libs/bower/switchery/dist/switchery.min.css">
    <link rel="stylesheet" type="text/css" href="../libs/bower/lightbox2/dist/css/lightbox.min.css">
    <link rel="stylesheet" type="text/css" href="../assets/css/jquery.mCustomScrollbar.min.css">--%>
    <%--<link rel="stylesheet" href="../libs/bower/bootstrap-fileInput/fileinput.min.css">
    <link rel="stylesheet" href="../libs/bower/bootstrap-fileInput/explorer/theme.css">--%>
<%--    <link rel="stylesheet" href="../assets/css/owl.carousel.css">--%>
<%--    <link rel="stylesheet" href="../assets/css/star-rating.min.css">--%>
    <link rel="stylesheet" type="text/css" href="../assets/css/ontime.css"/>
    <link rel="stylesheet" type="text/css" href="../assets/css/ontime-rtl.css"/>

  <%--  <script>
        Breakpoints();
    </script>--%>
    <style>
        .btn-group {
            margin: 0px !important;
            width: auto;
        }
    </style>
 <%-- <script src="../assets/js/core.min.js"></script>
    <script src="../assets/js/app.min.js"></script>
    <script src="../libs/bower/moment/moment.js"></script>
    <script src="../libs/bower/fullcalendar/dist/fullcalendar.min.js"></script>
    <script src="../assets/js/fullcalendar.js"></script>
    <script src="../libs/bower/switchery/dist/switchery.min.js"></script>
    <script src="../assets/js/jquery.mCustomScrollbar.concat.min.js"></script>--%>
<%--    <script src="../assets/js/ontime.js"></script>--%>
<%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>--%>

<!-- Latest compiled and minified CSS -->


<!-- Latest compiled JavaScript -->

    <style>
        .app-main {
            opacity: 1;
        }

        .pull-right .dropdown-menu {
            right: auto;
            left: 0;
        }


        #student {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
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

            #student td img {
                width: 100%;
                height: 100%;
            }

       .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9 {
            float: right;
        }
      
.col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9 {
    float: right;
}



    </style>

    <style type="text/css" >
    .Empty
    {
     
        background: #FFFECB
            url("images/empty_star.png") ;
            background-size: contain;
    }
    .Empty:hover
    {
        background: #FED352
            /*url("Acadmies_module/images/fill_star.png") no-repeat right top;*/
    }
    .Filled
    {
        background: #F49818
            url("images/fill_star.png") ;
            background-size: contain;
    }
</style>
  

    <%--  start form--%>

    <main id="app-main" class="app-main" style="margin-top:50px;">
        <div class="wrap">
             <div>

                   <script type="text/javascript">
                      
                    </script>
                    <script src="../JS_Code/acadmies/Course_Details.js"></script>


                </div>
            <section class="app-content">
                <div class="row">
                     <label style="display:none" id="Lblcourse_id" runat="server" ></label>
                     <label style="display:none" id="LblLecture_id" ></label>
                      <label style="display:none" id="LblHomework_id" ></label>
                       <label style="display:none" id="LblExam_id" ></label>
                    <label style="display:none" id="LblAbsence_id" ></label>
                    <label style="display:none" id="lblStudentID" runat="server" ></label>
                    <div id="SavedivLoader" class="loader" style="display: none; text-align: center;">
                        <asp:Image ID="img" runat="server" ImageUrl="../App_Themes/images/loader.gif" />
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12 pull-right">
                        <div class="p-md clearfix widget-orders widget-def widget-panels widget-users widget-financial widget-request">
                            <div class="widget-header">
                                <h3 >

                                  
                                    <i class="menu-icon zmdi zmdi-layers zmdi-hc-lg"></i>
                                    <span id="course_title" > </span>
                                  </h3>
                                <div class="btn-group pull-left">
  <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
    الخيارات
      <i class="fa fa-cogs"></i>
  </button>
  <ul class="dropdown-menu">
      <li>
          <a  onclick="CourseView();">
              تعديل
          </a></li>
      
      <li><a data-toggle="modal"  href="#exams">
          الاختبارات
      </a></li>
      <li><a data-toggle="modal" href="#homeworks">
          الواجبات
      </a></li>
      <li><a data-toggle="modal" href="#courseEvalution">
          تقييم الدورة
      </a></li>
      
 
 
  </ul>
</div>
                             

                            </div>
                            <div class="order-card col-xs-12">
                                <div class="order-panel order col-md-8 col-xs-12">
                                   
                                    <div class="inner">
                                     
                                        <div class="order-head order_wid">
                                            
                                            <p class="order-title " id="course_details"></p>
                                        </div>
                                        <div class="order-extra">
                                            <ul>

                                                <li>
                                                    <span id="course_date" class="fa fa-calendar-check-o">
                                                     
                                                        <i class="zmdi zmdi-calendar-note" ></i>
                                                     
                                                        
                                                </span>
                                                </li>
                                             
                                                <li>
                                                    <span class="price fa-fa-dollar" id="course_price">
                                                      
                                                        <i class="zmdi zmdi-money"> 
                                                  
                                                        </i>
                                                
                                                </span>
                                                            <span> ريال</span>
                                                </li>
                                                <li>
                                                    <span id="course_duration">
                                                        <i class="zmdi zmdi-time"></i>
                                                     
                                                      
                                                     
                                                </span>
<span>
    ايام
    
</span>                                                </li>
                                                <li>
                                                    <span class="price" id="course_category">
                                                      
                                                  
                                                </span>
                                                </li>
                                                <li>
                                                    <span class="price" id="course_stat">
                                                                                                            
                                                </span>
                                                </li>

                                            </ul>
                                        </div>

                                    </div>

                                    <div class="inner">
                                        <div class="order-desc row">
                                            <div class="desc-head order_wid col-md-12"">
                                                <div class=" pull-right">
                                                <i class="zmdi zmdi-file-text zmdi-hc-lg"></i>
                                                <h3>جدول المحاضرات </h3>
                                                    </div>
                                               <div class=" pull-left">
                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#order_addLec">
                                                    اضافة محاضرة   <i class="fa fa-plus"></i>

                                                </button>
                                                   </div>
                                              
                                            </div>
                                            <div class="desc-body">

                                                <section class="app-content">
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12 col-xs-12 pull-right"  >
                                                             <div class="widget-body">
                <div class="trans-data col-xs-12" >
                                                                  
                <div class="table-responsive">
                    <table class="table table-bordered table-hover">
                        <tr>
                            <th>المحاضرة </th>
                            <th>التاريخ</th>
                            <th>الوقت </th>
                            <th>القاعه </th>
                            <th>الاجراء </th>

                        </tr>
                        <tbody id="lectures-table">

                        </tbody>
                        </table>
                    </div>
                    </div>
                                                                 </div>    </div>
                                                          
                                                            <!-- end widget-body -->
                                                            <!-- end widget -->
                                                      
                                                    </div>
                                                </section>

                                                <%-- نهايةالجدول--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="inner">
                                        <div class="order-desc row">
                                            <div class="desc-head order_wid col-md-12"">
                                                <div class=" pull-right">
                                                <i class="zmdi zmdi-file-text zmdi-hc-lg"></i>
                                                <h3>جدول الشروط </h3>
                                                    </div>
                                               <div class=" pull-left">
                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#order_addcondition">
                                                    اضافة شرط   <i class="fa fa-plus"></i>
                                                    
                                                </button>
                                                   </div>
                                              
                                            </div>
                                            <div class="desc-body">

                                                <section class="app-content">
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12 col-xs-12 pull-right"  >
                                                             <div class="widget-body">
                <div class="trans-data col-xs-12" >
                                                                  
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
                                                                 </div>    </div>
                                                          
                                                            <!-- end widget-body -->
                                                            <!-- end widget -->
                                                      
                                                    </div>
                                                </section>

                                           
                                            </div>
                                        </div>
                                    </div>

                                    <div class="inner">
                                        <div class="order-desc row">
                                            <div class="desc-head order_wid col-md-12"">
                                                <div class=" pull-right">
                                                <i class="zmdi zmdi-file-text zmdi-hc-lg"></i>
                                                <h3>ملفات الدورة  </h3>
                                                    </div>
                                               <div class=" pull-left">
                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#order_addfiles">
                                                     اضاقة ملف   <i class="fa fa-plus"></i>
                                                    
                                                </button>
                                                   </div>
                                              
                                            </div>
                                            <div class="desc-body">

                                                <section class="app-content">
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12 col-xs-12 pull-right"  >
                                                             <div class="widget-body">
                <div class="trans-data col-xs-12" >
                                                                  
                <div class="table-responsive">
                    <table class="table table-bordered table-hover">
                        <tr>
                            <th>الملاحظة </th>
                            <th>الملف</th>
                            <th>الاجراء</th>
                            

                        </tr>
                        <tbody id="courseFiles-table">

                        </tbody>
                        </table>
                    </div>
                    </div>
                                                                 </div>    </div>
                                                      
                                                      
                                                    </div>
                                                </section>

                                           
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="inner">
                                        <div class="edit-order">
                                            <div class="edit-head order_wid">
                                                <i class="zmdi zmdi-wrench zmdi-hc-lg"></i>
                                                <h3>التحكم في الدورة</h3>
                                                <%--نكتب عنوان مناسب غير ده--%>
                                            </div>
                                            <div class="edit-body">
                                            <button type="button" class="btn btn_rate btn-warning" data-toggle="modal" data-target="#courseEvalution">
                                                    <i class="zmdi zmdi-star"></i>تقييم الدورة
                                               
                                              
                                                 

                                                <button type="button" class="btn btn_com btn-purple" data-toggle="modal" data-target="#contact_Trainer">
                                                    <i class="zmdi zmdi-headset-mic"></i> تواصل مع المدرب
                                                </button>
                                                   <button type="button" class="btn btn_rec btn-danger" onclick="deleteCourse();">
                                                    <i class="zmdi zmdi-headset-mic"></i>
                                                       حذف الدورة</button>
                                                </button>
                                                <button type="button" class="btn btn_com btn-primary" data-toggle="modal" data-target="#contact_admin">
                                                    <i class="zmdi zmdi-headset-mic"></i> تواصل مع الادراة
                                                </button>

                                            </div>
                                        
                                        </div>
                                    </div>
                                    <div class="inner">
                                        <div class="comments">
                                         
                                            <div class="comment-body">
                                               
                                                <div class="comment-users">
                                                   
                                                    <div id="divformComments">
                                                        
                                                <%--  //comments--%>
                                                                
                                                        </div>
                                                    
                                                </div>
                                                <div class="comment-form">
                                                    
                                                        <div class="form-group">
                                                            <label for="comment_ad">اكتب تعليقك</label>
                                 <div id="newdivcomment">
                                                <asp:TextBox SkinID="form-control" TextMode="multiline" required class="form-control" dbColumn="comment" ClientIDMode="Static" ID="userComments" runat="server">
                                                </asp:TextBox>
                                     </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <button type="submit" class="btn" onclick="addComment();">ارسال</button>
                                                           
                                                        </div>
                                                   
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="order-sidebar order col-md-4 col-xs-12">
                                   
                                    <div class="inner">
                                        <div class="owner">
                                            <div class="owner-head side_head">
                                                <h3 >
                                                    <i class="zmdi zmdi-account zmdi-hc-lg"></i>
                                                    مدرب الدورة
                                                </h3>
                                            </div>
                                            <div class="owner-body">
                                                <div class="owner-usr">
                                                    <div class="usr-img">
                                                        <%--  صوره المدرب--%>
                                                        <img src="../assets/images/218.jpg" alt="">
                                                    </div>
                                                    <div class="usr-data">
                                                        <h3 id="course_trainer">
                                                           
                                                          
                                                        </h3>

                                                       <%-- <span>
                                                            
                                                            <i class="zmdi zmdi-pin"></i>
                                                            السعودية , الرياض
                                                        </span>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>




                                    <div class="inner">
                                        <div class="add-hint">
                                            
                                            <div class="hint-head side_head">
                                                    <div>
                                                        <h3>
                                                            <i class="zmdi zmdi-storage zmdi-hc-lg"></i>
                                                            الطلاب  
                                                        </h3>
                                              
                                                    
                                                        <div class=" pull-left">
                                                        <button style="margin-top: -20px;" type="button" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#addStudentModal">
                                                            اضافة طلاب   <i class="fa fa-plus zmdi zmdi-storage zmdi-hc-lg"></i>

                                                        </button>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <input id="txt_Search" onkeyup="studentSearch();" type="text" class="form-control" placeholder="بحث عن طالب" />
                                                    </div>
                                                </div>

                                                        </div>
                                                
                                            <div class="table-responsive" >
                                                <div id="StudentTable">

                                              

                                                <table id="student">


                                               
                                                 <%-- here--%>
                                                </table>
                                            </div>

                                         </div>
                                             </div>
                                       
                                        </div>
                                     


                                    <div class="inner">
                                        <div class="add-hint">
                                            <div class="hint-head side_head">
                                                <h3>
                                                    <i class="zmdi zmdi-storage zmdi-hc-lg"></i>
                                                    الواجبات
                                                </h3>
                                            </div>
                                            <div class="hint-body">

                                            <div class="table-responsive">

                                                <table id="student">
                                                    <tr>
                                                        <th>الواجب </th>
                                                        <th>التاريخ</th>
                                                        <th>الاجراء </th>

                                                    </tr>
                                                    <tbody id="homework">
                                                    </tbody>
                                                </table>


                                            </div>

                                            
                                            <button type="button" class="btn btn-purple btn-hint" data-toggle="modal" data-target="#homeworks">اضافة واجب </button>
                                        </div>
                                            </div>
                                    </div>


                                    <div class="inner">
                                        <div class="add-hint">
                                            <div class="hint-head side_head">
                                                <h3>
                                                    <i class="zmdi zmdi-storage zmdi-hc-lg"></i>
                                                    الاختبارات
                                                </h3>
                                            </div>
                                            <div class="hint-body">
                                                <div class="table-responsive">

                                                <table id="student">
                                                    <tr>
                                                        <th>الاختبار </th>
                                                        <th>التاريخ</th>
                                                        <th>الاجراء </th>

                                                    </tr>
                                                    <tbody id="courseExams">
                                                    </tbody>
                                                </table>


                                            </div>
                                                <button type="button" class="btn btn-purple btn-hint" data-toggle="modal" data-target="#exams">اضافة اختبار </button>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="inner">
                                        <div class="add-hint">
                                            <div class="hint-head side_head">
                                                <h3>
                                                    <i class="zmdi zmdi-storage zmdi-hc-lg"></i>
                                                    اضافة ملاحظات
                                                </h3>
                                            </div>
                                            <div class="hint-body">
                                                <div id="StudentNote">
                                                    <p>
                                                        لا توجد ملاحظات على هذا الطلب بعد


                                                    </p>
                                                </div>
                                                <button type="button" class="btn btn-purple btn-hint" data-toggle="modal" data-target="#addNote">اضافة ملاحظة </button>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="inner">
                                        <div class="add-hint">
                                            <div class="hint-head side_head">
                                                <h3>
                                                    <i class="zmdi zmdi-storage zmdi-hc-lg"></i>
                                                    الانشطة الحالية 
                                                </h3>
                                            </div>
                                            <div class="hint-body">
                                                <div id="studentActivity">
                                                    <p>
                                                        لا توجد انشطة حاليه


                                                    </p>
                                                </div>
                                                <button type="button" class="btn btn-purple btn-hint" data-toggle="modal" data-target="#activity_edit">اضافة انشطة </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                 </section>
            
          

            <div class="modal fade" id="EditCourseModal" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">تعديل الدورة</h4>
                        </div>
                        <div class="modal-body">
                            <div class="modal-body">
                                <div id="divformEditCourse" class="row">
                                    <%--  <label style="display:none" id="Label1" runat="server" ></label>--%>
                                    <div class="col-md-12">
                                        <uc1:Result runat="server"  ID ="res1" />
                                    </div>
                                    <div class="col-md-6">
                                        <div class="row form-group">

                                            <div class="col-md-3 col-sm-12">
                                                <label for="Name" class="label-required">عنوان الدورة </label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox SkinID="form-control " required class="form-control" dbColumn="name" ClientIDMode="Static" ID="courseTitle" runat="server">
                                                </asp:TextBox>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="courseTitle"
                                                    ErrorMessage="من فضلك أدخل عنوان الدورة  " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class=" row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">
                                                    تاريخ البداية   
                                                </label>
                                            </div>

                                            <div class="col-md-9 col-sm-12">

                                                <div class="fancy-form" id="divdate6">
                                                    <input dbcolumn="start_dt_m" type="hidden" id="strdate_m" />
                                                    <input dbcolumn="start_dt_hj" type="hidden" id="strdate_hj" />
                                                    <uc1:HijriCalendar runat="server" ID="HijriCalendar2" />
                                                </div>

                                            </div>
                                        </div>

                                            <div class=" row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">
                                                    تاريخ النهاية   
                                                </label>
                                            </div>

                                            <div class="col-md-9 col-sm-12">

                                                <div class="fancy-form" id="divdate7">
                                                    <input dbcolumn="end_dt_m" type="hidden" id="enddate_m" />
                                                    <input dbcolumn="end_dt_hj" type="hidden" id="enddate_hj" />
                                                    <uc1:HijriCalendar runat="server" ID="HijriCalendar6" />
                                                </div>

                                            </div>
                                        </div>
                                        
                                    </div>
                                    <div class="col-md-6">

                                        <div class=" row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">السعر     </label>
                                            </div>

                                            <div class="col-md-9 col-sm-12">
                                                <input onkeypress="return isNumber(event);"  required  dbcolumn="price" type="text" id="price"
                                                    class="form-control " runat="server" clientidmode="Static" />


                                            </div>
                                        </div>
                                        <div class="row  form-group ">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">القسم </label>
                                            </div>

                                            <div class="col-md-9 col-sm-12">
                                                <asp:DropDownList  required dbcolumn="category_id" class="form-control " ClientIDMode="Static" ID="ddlcategory" runat="server">
                                                </asp:DropDownList>

                                            </div>

                                        </div>
                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label for="Name">تفاصيل الدورة </label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control " dbColumn="description" ClientIDMode="Static" ID="description" runat="server">
                                                </asp:TextBox>


                                            </div>
                                        </div>
                                        <div class=" row form-group ">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">مدرب الدورة</label>
                                            </div>

                                            <div class="col-md-9 col-sm-12">
                                                <asp:DropDownList dbcolumn="trainer_id" required class="form-control" ClientIDMode="Static" ID="ddltrainer" runat="server">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button"  class="btn btn-primary" onclick="saveCourse();">حفظ </button>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            

            <div class="modal" id="activity_edit" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">اضافة نشاط</h4>
                        </div>
                        <div class="modal-body">
                            <div id="divformactivity">

                                <div class=" row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label class="label-required">محتوى النشاط       </label>
                                    </div>

                                   <div class="col-md-9 col-sm-12">
                                                <asp:TextBox SkinID="form-control"  TextMode="multiline" class="form-control " required  dbColumn="activity" ClientIDMode="Static" ID="TextBox4" runat="server">
                                                </asp:TextBox>

                                                </div>
                                </div>

                                <div class=" row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label class="label-required">
                                            التاريخ   
                                        </label>
                                    </div>

                                    <div class="col-md-9 col-sm-12">

                                        <div class="fancy-form" id="divdate3">
                                            <input dbcolumn="date_hj" type="hidden" id="date_hj1" />
                                            <input dbcolumn="date_m" type="hidden" id="date_m1" />
                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar3" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="AddActivity();">اضافة</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal" id="order_addLec" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">اضافة محاضره</h4>
                        </div>
                       
                        <div class="modal-body">
                            
                            <div id="divFormTable" class="row">
                                   <div class="col-md-6">


                                <div class=" row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label class="label-required">كود المحاضرة       </label>
                                    </div>

                                    <div class="col-md-9 col-sm-12">
                                        <input onkeypress="return isNumber(event);"  required dbcolumn="lecture_code" type="text" id="Text1"
                                            class="form-control" runat="server" clientidmode="Static" />

                                        <br />
                                    </div>
                                </div>
                                <div class=" row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label class="label-required">
                                            تاريخ المحاضرة  
                                        </label>
                                    </div>

                                    <div class="col-md-9 col-sm-12">

                                        <div class="fancy-form" id="divdate1">
                                            <input dbcolumn="date_hj"   type="hidden" id="dateLec_hj" />
                                            <input dbcolumn="date_m" type="hidden" id="dateLec_m" />
                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar" />
                                        </div>

                                    </div>
                                </div>
                                       </div>
                                   <div class="col-md-6">

                                <div class=" row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label class="label-required">وقت المحاضرة      </label>
                                    </div>

                                    <div class="col-md-9 col-sm-12">
                                        <input onkeypress="return isNumber(event);" required dbcolumn="start_time" type="text" id="startTime"
                                            class="form-control" runat="server" clientidmode="Static" />


                                    </div>
                                </div>
                                   <div class="row  form-group ">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">القاعة </label>
                                            </div>

                                            <div class="col-md-9 col-sm-12">
                                                <asp:DropDownList dbcolumn="hall_id" required class="form-control" ClientIDMode="Static" ID="ddlhallNum" runat="server">
                                                </asp:DropDownList>

                                            </div>
                                        </div>

                                
                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label for="Name" class="label-required">الملاحظات </label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox SkinID="form-control" TextMode="multiline" required  class="form-control" dbColumn="notes" ClientIDMode="Static" ID="lecNotes" runat="server">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                       </div>
                            </div>
                            
                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="SaveLec();">اضافه </button>
                        </div>
                    </div>
                </div>
            </div>
              <div class="modal fade" id="addStudentModal" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">الطلاب   </h4>
                        </div>
                        <%--  جدول الطلاب--%>
                        <div class="table-responsive" id="allStudentlist">
                            <table class="table table-bordered table-hover"  >
                               <thead>
                                <tr>
                                    <th>الاسم </th>
                                    <th>الصورة</th>
                                     <th>اضافة</th>
                                  
                                </tr>
                                 </thead>
                                <tbody  id="courseStudents">
                          
                                         
                                </tbody>
                                    

                            </table>
                        </div>


                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="AddStudent();">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal" id="order_addcondition" tabindex="-1" role="dialog">
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
                                        <input dbcolumn="condition" required type="text" id="condition"
                                            class="form-control" runat="server" clientidmode="Static" />


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

            <div class="modal" id="order_addfiles" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">اضافة ملف</h4>
                        </div>
                        <div class="modal-body">
                            <div id="divFormfiles">

                                             <div class="row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" class="label-required"> الملاحظات </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control"  TextMode="multiline"  class="form-control" dbColumn="notes" required  ClientIDMode="Static" ID="notes3" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                <div class="form-group row">

                                    <div>
                                        <input id="coursefileURL" type="hidden" dbcolumn="image" runat="server" />
                                        <input id="EXFName" type="text"   readonly="readonly" required runat="server" />

                                    </div>
                                    <div class="clear">
                                    </div>
                                    <asp:AsyncFileUpload ID="fuFile4" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                        OnClientUploadComplete="UploadComplete5" />
                                                                                                                   
                                </div>
                            </div>


                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="addFiles();">اضافه </button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="absenceModal" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">جدول الغياب  </h4>
                            <div class="col-md-6" >
                            <input  id="txtAbsen_Search" onkeyup="searchAbsence();" type="text" class="form-control" placeholder="بحث عن طالب" />
                        </div>
                        </div>
                        <%--  جدول الغياب--%>
                        <div class="table-responsive">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>الاسم </th>
                                    <th>حضور</th>


                                </tr>
                                   
                                <tbody id="absenscetable">
                                 
                                </tbody>
                                         


                            </table>
                        </div>


                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="SaveAbsenceStudent();">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="exams" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">الاختبارات </h4>
                        </div>
                        <div class="modal-body">
                            <div id="divformExams" class="row">
                                 <div class="col-md-12">
                                        <uc1:Result runat="server"  ID ="Result1" />
                                    </div>

                                <div class="col-md-6">

                                    <div class="row form-group">

                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" class="label-required">العنوان  </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" required class="form-control" dbColumn="title" ClientIDMode="Static" ID="TextBox1" runat="server">
                                            </asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="courseTitle"
                                                ErrorMessage="من فضلك أدخل عنوان الاختبار  " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>

                                    <div class=" row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">
                                                تاريخ الاختبار  
                                            </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">

                                            <div class="fancy-form" id="divdateExam">
                                                <input dbcolumn="date_hj"  type="hidden" id="date2_hj" />
                                                <input dbcolumn="date_m" type="hidden" id="date2_m" />
                                                <uc1:HijriCalendar runat="server" ID="HijriCalendar4" />
                                            </div>

                                        </div>
                                    </div>

                                    <div class="form-group row">

                                    <div>
                                          <label id="examfile">  </label>
                                        <input id="fileURL3" required type="hidden" dbcolumn="image" runat="server" />
                                        <input id="FName3" type="text" readonly="readonly" runat="server" />

                                    </div>
                                    <div class="clear">
                                    </div>
                                    <asp:AsyncFileUpload ID="fuFile2"  required SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                        OnClientUploadComplete="UploadComplete3" />
                                </div>

                                </div>
                                <div class="col-md-6">

                                    <div class="row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name">تفاصيل الاختبار </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" TextMode="multiline"  required class="form-control" dbColumn="details" ClientIDMode="Static" ID="details" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name">الملاحظات </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" required  TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="notes" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    
                          
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="SaveExam();">حفظ </button>
                        </div>

                    </div>

                </div>
            </div>

            <div class="modal fade" id="homeworks" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">الواجبات </h4>
                        </div>
                        <div class="modal-body">
                            <div id="divformHomework" class="row">
                                 <div class="col-md-12">
                                        <uc1:Result runat="server"  ID ="Result2" />
                                    </div>

                                <div class="col-md-6">

                                    <div class="row form-group">

                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" class="label-required">العنوان  </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" required class="form-control" dbColumn="title" ClientIDMode="Static" ID="TextBox2" runat="server">
                                            </asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="courseTitle"
                                                ErrorMessage="من فضلك أدخل عنوان الواجب  " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>

                                    <div class=" row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">
                                                تاريخ الواجب  
                                            </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">

                                            <div class="fancy-form" id="divdateHomework">
                                                <input dbcolumn="date_hj"   type="hidden" id="date3_hj" />
                                                <input dbcolumn="date_m" type="hidden" id="date3_m" />
                                                <uc1:HijriCalendar runat="server" ID="HijriCalendar5" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group row">

                                        <div id="divfileupload">
                                             <label id="fileupload">  </label>
                                            <input id="fileURL4" required type="hidden" dbcolumn="image" runat="server" />
                                            <input id="FName4" type="text" readonly="readonly" runat="server" />

                                        </div>
                                        <div class="clear">
                                        </div>
                                        <asp:AsyncFileUpload ID="fuFile3" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                            OnClientUploadComplete="UploadComplete4" />
                                       
                                    </div>
                                </div>
                                <div class="col-md-6">

                                    <div class="row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name">تفاصيل الواجب </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" required TextMode="multiline" class="form-control" dbColumn="details" ClientIDMode="Static" ID="TextBox3" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name">الملاحظات </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" TextMode="multiline"  required class="form-control" dbColumn="notes" ClientIDMode="Static" ID="notes2" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="SaveHomework();">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="studentDegrees" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">درجات الطالب </h4>
                        </div>
                        <div class="modal-body">
                           <div id="divFormDegrees">

                                <div class=" row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label>درجة نصف العام       </label>
                                            </div>

                                            <div class="col-md-9 col-sm-12">
                                                <input onkeypress="return isNumber(event);" dbcolumn="activity_degree" required type="text" id="activityDegree"
                                                    class="form-control" runat="server" clientidmode="Static" />


                                            </div>
                                        </div>


                                 <div class=" row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label>الدرجة النهائية  </label>
                                            </div>

                                            <div class="col-md-9 col-sm-12">
                                                <input onkeypress="return isNumber(event);" dbcolumn="final_degree" required type="text" id="finaldegree"
                                                    class="form-control" runat="server" clientidmode="Static" />


                                            </div>
                                        </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="SaveDegree();">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>
                 </div>
            <div class="modal fade" id="addNote" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">ملاحظة  </h4>
                        </div>
                        <div class="modal-body">
                            <div id="divformNote">
                                <div class=" row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label>محتوى الملاحظة      </label>
                                    </div>

                        
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control " required dbColumn="comment" ClientIDMode="Static" ID="txtnote" runat="server">
                                                </asp:TextBox>

                                                </div>
                                            </div>
                                </div>
                                <div class=" row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label class="label-required">
                                            تاريخ الملاحظة  
                                        </label>
                                    </div>

                                    <div class="col-md-9 col-sm-12">

                                        <div class="fancy-form" id="divdate2">
                                            <input dbcolumn="date_hj" type="hidden" id="dt_hj1" />
                                            <input dbcolumn="date_m" type="hidden" id="dt_m1" />
                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar1" />
                                        </div>

                                    </div>
                                </div>


                            </div>
                            
                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="AddNote();">حفظ </button>
                        </div>
                    </div>
                </div>
            </div> 
            <div class="modal fade" id="courseEvalution" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">تقييم الدورة </h4>
                        </div>
                        <div class="modal-body">
                            <div id="divformCourseEvalution" class="row">
                                 <div class="col-md-12">
                                        <uc1:Result runat="server"  ID ="Result3" />
                                    </div>
                                <div id="evalute" runat="server">
                                    <asp:Button BorderStyle="None" ID="Rating1" onmouseover="return Decide(1);" OnClientClick="return Decide(1);"
                                        Height="30px" Width="36px" CssClass="Empty" runat="server" />
                                    <asp:Button BorderStyle="None" ID="Rating2" onmouseover="return Decide(2);" OnClientClick="return Decide(2);"
                                        Height="30px" Width="36px" CssClass="Empty" runat="server" />
                                    <asp:Button BorderStyle="None" ID="Rating3" onmouseover="return Decide(3);" OnClientClick="return Decide(3);"
                                        Height="30px" Width="36px" CssClass="Empty" runat="server" />
                                    <asp:Button BorderStyle="None" ID="Rating4" onmouseover="return Decide(4);" OnClientClick="return Decide(4);"
                                        Height="30px" Width="36px" CssClass="Empty" runat="server" />
                                    <asp:Button BorderStyle="None" ID="Rating5" onmouseover="return Decide(5);" OnClientClick="return Decide(5);"
                                        Height="30px" Width="36px" CssClass="Empty" runat="server" />
                                    <br />
                                    <br />
                                    <asp:Label ID="lblRate"  hidden dbColumn="evalution" runat="server" Text=""></asp:Label>
                                </div>

                                    <div class="row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name">الملاحظات </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" TextMode="multiline"  required class="form-control" dbColumn="notes" ClientIDMode="Static" ID="evaluteNotes" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    
                              

                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="SaveEvalution();">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="contact_Trainer" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">تواصل مع المدرب  </h4>
                        </div>
                        <div class="modal-body">
                            <div id="divformconTr">
                                <div class=" row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label> الرسالة      </label>
                                    </div>

                                    <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" TextMode="multiline"  required class="form-control" dbColumn="message" ClientIDMode="Static" ID="conmsg" runat="server">
                                            </asp:TextBox>
                                        </div>
                                </div>
                                


                            </div>
                            
                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="sendMsg();">ارسال </button>
                        </div>
                    </div>
                </div>
            </div>

           <div class="modal fade" id="contact_admin" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">تواصل مع الادارة  </h4>
                        </div>
                        <div class="modal-body">
                            <div id="divformconAdmin">
                                <div class=" row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label> الرسالة      </label>
                                    </div>

                                    <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" TextMode="multiline"  required class="form-control" dbColumn="message" ClientIDMode="Static" ID="message" runat="server">
                                            </asp:TextBox>
                                        </div>
                                </div>
                                


                            </div>
                            
                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="sendMsgtoAdmin();">ارسال </button>
                        </div>
                    </div>
                </div>
            </div>


       
           
    </main>

    <%-- end form--%>
</asp:Content>
