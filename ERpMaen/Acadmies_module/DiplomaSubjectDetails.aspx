<%@ Page Language="vb" AutoEventWireup="false"   CodeBehind="DiplomaSubjectDetails.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.DiplomaSubjectDetails" EnableEventValidation="false" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>

<%--<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="ucmf" TagName="MultiPhotoUpload" %>--%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/DiplomaSubjectDetailsCls.asmx" />
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

     <script type="text/javascript" src="../Clock/ng_all.js"></script>
<script type="text/javascript" src="../Clock/ng_ui.js"></script>
<script type="text/javascript" src="../Clock/components/timepicker.js"></script>
    <script type="text/javascript">
    ng.ready(function () {
      
        var tp = new ng.TimePicker({
            input: 'startTime', // the input field id
            start: '12:00 am',  // what's the first available hour
            end: '11:00 pm',  // what's the last avaliable hour
            top_hour: 12,  // what's the top hour (in the clock face, 0 = midnight)
            name: 'startTime',
            
        });
         
        
    });
</script>

  <style>
           .btn-group {
            margin: 0px !important;
            width: auto;
        }
        .modal-content .table-responsive{max-height: 430px;}
        .add-hint .table-responsive{max-height: 1000px;}
        .app-main {
            opacity: 1;
        }

        .pull-right .dropdown-menu {
            right: auto;
            left: 0;
        }
        /*studentlist*/
      

        #newitem tbody td img {
            

              width:50px;
                height: 50px;
        }

        #student {
            border-collapse: collapse;
            width: 100%;
        }

            #student td:nth-child(2){
                width: 25%;
            }

            #student td:last-child {
                width: 15%;
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

            #student tbody td img  {
               
              width:50px;
                height: 50px;
            }
           #publicDeg td input{
         
             width:70px;
          padding:5px; 
    border:2px solid #ccc; 
    -webkit-border-radius: 5px;
    border-radius: 5px;
    margin-right:10px;
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
                    <script src="../JS_Code/acadmies/DiplomaSubjectDetails.js"></script>


                </div>
            <section class="app-content">
                <div class="row">
                     <label style="display:none" id="Lblsubject_id" runat="server" ></label>
                       <label style="display:none" id="lblcode" runat="server" ></label>
                       <label style="display:none" id="LblDiplome_id" runat="server"></label>
                        <label style="display:none" id="subject_code" runat="server"></label>
                      <label style="display:none" id="lbldiplomeCode" runat="server"></label>
                     <label style="display:none" id="LblLecture_id" ></label>
                     <label style="display:none" id="lblsub_FinalDegree" ></label>
                     <label style="display:none" id="lblsub_ActivityDegree" ></label>
                      <label style="display:none" id="LblHomework_id" ></label>
                       <label style="display:none" id="LblExam_id" ></label>
                       <label style="display:none" id="LblDegree_id" ></label>
                      <label style="display:none" id="lect_time" ></label>
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
                                    <span id="subject_title" > </span>
                                  </h3>
                                  <% if ERpMaen.LoginInfo.getUserType <> 8 Then
                                          If ERpMaen.LoginInfo.get_form_operation("11") = True Or ERpMaen.LoginInfo.get_form_operation_group("11") Then
                                          %>
                                <div class="btn-group pull-left">
  <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
    الخيارات
      <i class="fa fa-cogs"></i>
  </button>
  <ul class="dropdown-menu">
       <% if ERpMaen.LoginInfo.get_form_operation("12") = True Then   %>
      <li>
          <a  onclick="subjectView();">
              تعديل بيانات المادة
          </a></li>
      <% End If %>
        <% if ERpMaen.LoginInfo.get_form_operation("14") = True Then   %>
      <li><a data-toggle="modal"  href="#exams">
          الاختبارات العامة
      </a></li>
      <% End If %>
        <% if ERpMaen.LoginInfo.get_form_operation("15") = True Then   %>
      <li><a data-toggle="modal" href="#homeworks">
          الواجبات العامة
      </a></li>
      <% End If %>
        <% if ERpMaen.LoginInfo.get_form_operation("32") = True Then   %>
      <li><a data-toggle="modal" href="#courseEvalution" onclick="viewEvaluation();">
          تقييم المادة
      </a></li>
      <% End If %>
        <% if ERpMaen.LoginInfo.get_form_operation("16") = True Then   %>
       <li><a data-toggle="modal" href="#publicStudentDegree" onclick="drawpublicDegreeTable();">
           رصد درجات الطلاب
      </a></li>
      <% End If %>
        <% if ERpMaen.LoginInfo.get_form_operation("17") = True Then   %>
        <li><a data-toggle="modal" href="#publicStudentDegree_Admin" onclick="drawApproved_StudentDegree();">
            اعتماد درجات الطلاب
      </a></li>
      <% End If %>
        
      

 
  </ul>
</div>
                               <% 
                                       End If
                                   End If
                                   %>

                            </div>
                            <div class="order-card col-xs-12">
                                <div class="order-panel order col-md-8 col-xs-12">
                                   
                                    <div class="inner">
                                     
                                        <div class="order-head order_wid">
                                            
                                            <p class="order-title " id="subjectGoal"></p>
                                        </div>
                                        <div class="order-extra">
                                            <ul>

                                                <li>
                                                    <span id="course_date" class="fa fa-calendar-check-o">
                                                     
                                                        <i class="zmdi zmdi-calendar-note" ></i>
                                                     
                                                        
                                                </span>
                                                </li>
                                             
                                                <li>
                                                    <span id="">
                                                         <i class="fa fa-clock-o"></i>
                                                        <b>مدة المحاضرة</b>
                                                       <b id="lectureDuration"></b>
                                                           <b>دقيقة </b>
                                                     
                                                     
                                                </span>
                                              </li>
                                              
                                                <li>
                                                    <span class="price" id="subject_semster">
                                                      
                                                  
                                                </span>
                                                </li>
                                        

                                            </ul>
                                        </div>

                                    </div>
                                    <% if ERpMaen.LoginInfo.getUserType <> 8 Then
                                If ERpMaen.LoginInfo.get_form_operation("18") = True Or ERpMaen.LoginInfo.get_form_operation_group("18") Then
                                          %>
                                    <div class="inner">
                                        <div class="order-desc row">
                                            <div class="desc-head order_wid col-md-12"">
                                                <div class=" pull-right">
                                                <i class="zmdi zmdi-file-text zmdi-hc-lg"></i>
                                                <h3>جدول المحاضرات </h3>
                                                    </div>
                                               <div class=" pull-left">
                                                     <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
                                                    <% if ERpMaen.LoginInfo.get_form_operation("19") = True Then   %>
                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#order_addLec">
                                                    اضافة محاضرة   <i class="fa fa-plus"></i>

                                                </button>
                                                     <% End if   %>
                                                     <% End if   %>
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
                                                          
                                                           
                                                      
                                                    </div>
                                                </section>

                                                <%-- نهايةالجدول--%>
                                            </div>
                                        </div>
                                    </div>
                                    <% End if
                                        End If %> 
                                    <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
                                    <div class="inner" style="display:none;">
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
                                                          
                                                           
                                                      
                                                    </div>
                                                </section>

                                           
                                            </div>
                                        </div>
                                    </div>
                                      <% End If   %>



                                     <% if ERpMaen.LoginInfo.getUserType = 8 Then   %>
                                         
                                    <div class="inner">
                                        <div class="order-desc row">
                                            <div class="desc-head order_wid col-md-12"">
                                                <div class=" pull-right">
                                                <i class="zmdi zmdi-file-text zmdi-hc-lg"></i>
                                                <h3>جدول الواجبات </h3>
                                                    </div>
                                              
                                                
                                            </div>
                                            <div class="desc-body">


                                              

                                                <section class="app-content">
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12 col-xs-12 pull-right">

                                                            <div class="widget-body">
                                                                <div class="trans-data col-xs-12">
                                                                 
                                                                    <div class="table-responsive">
                                                                        <table class="table table-bordered table-hover">
                                                                            <tr>
                                                                                <th>اسم الواجب </th>
                                                                                <th>تفاصيل الواجب</th>
                                                                                <th>مرفق الواجب </th>
                                                                                <th>رفع حل </th>
                                                                                 <th>درجة الواجب </th>
                                                                                
                                                                               

                                                                            </tr>

                                                                            <tbody id="studenthomeworktable">
                                                                                   </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>


                                                            </div>
                                                           
                                                        </div>
                                                    </div>
                                                </section>

                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="inner" style="display:none;">
                                        <div class="order-desc row">
                                            <div class="desc-head order_wid col-md-12"">
                                                <div class=" pull-right">
                                                <i class="zmdi zmdi-file-text zmdi-hc-lg"></i>
                                                <h3>جدول الاختبارات </h3>
                                                    </div>
                                              
                                                
                                            </div>
                                            <div class="desc-body">


                                             

                                                <section class="app-content">
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12 col-xs-12 pull-right">

                                                            <div class="widget-body">
                                                                <div class="trans-data col-xs-12">
                                                                  
                                                                    <div class="table-responsive">
                                                                        <table class="table table-bordered table-hover">
                                                                            <tr>
                                                                                <th>اسم الاختبار </th>
                                                                                <th>تفاصيل الاختبار</th>
                                                                                <th>مرفق الاختبار </th>
                                                                                <th>رفع حل </th>
                                                                                 <th>درجة الاختبار </th>
                                                                               

                                                                            </tr>
                                                                            <tbody id="studentExamstable">
                                                                          
                                                                               
                                                                                </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>


                                                            </div>
                                                          
                                                        </div>
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
                                                <h3> درجات المادة </h3>
                                                    </div>
                                              
                                                
                                            </div>
                                            <div class="desc-body">



                                                <section class="app-content">
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12 col-xs-12 pull-right">

                                                            <div class="widget-body">
                                                                <div class="trans-data col-xs-12">
                                                                  
                                                                    <div class="table-responsive">
                                                                        <table class="table table-bordered table-hover">
                                                                            <tr>
                                                                                <th> الدرجة النهائية </th>
                                                                                <th> درجة النشاط</th>
                                                                               

                                                                            </tr>

                                                                            <tbody id="studentcourseDegreestable">

                     
                                                                                   </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>


                                                            </div>
                                                           
                                                        </div>
                                                    </div>
                                                </section>

                                                <%-- نهايةالجدول--%>
                                            </div>
                                        </div>
                                    </div>

                               <% End If %>
       <% 
           If ERpMaen.LoginInfo.get_form_operation("28") = True Or ERpMaen.LoginInfo.get_form_operation_group("28") Then
                                          %>
                                    <div class="inner">
                                        <div class="order-desc row">
                                            <div class="desc-head order_wid col-md-12"">
                                                <div class=" pull-right">
                                                <i class="zmdi zmdi-file-text zmdi-hc-lg"></i>
                                                <h3>مرفقات المادة  </h3>
                                                    </div>
                                               <div class=" pull-left">
                                                     <% if ERpMaen.LoginInfo.getUserType <> 8 And ERpMaen.LoginInfo.get_form_operation("29") = True Then   %>
                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#order_addfiles">
                                                     اضاقة مرفق   <i class="fa fa-plus"></i>
                                                    
                                                </button>
                                                     <%End If  %>
                                                    <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
                                                    <input type="hidden" id="checkuser"  value="1"/>
                                                   
                                                    <% Else  %>
                                                     <input type="hidden" id="checkuser"  value="2"/>
                                                    <% End If %>
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
                            <th>عنوان المرفق </th>
                            <th>تحميل</th>
                              <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
                            <th>الاجراء</th>
                              <% End If   %>
                            

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
                                     <% End If %>

                                    <% 
           If ERpMaen.LoginInfo.get_form_operation("37") = True Or ERpMaen.LoginInfo.get_form_operation_group("37") Then
                                          %>
                                    <div class="inner">
                                        <div class="order-desc row">
                                            <div class="desc-head order_wid col-md-12"">
                                                <div class=" pull-right">
                                                <i class="zmdi zmdi-file-text zmdi-hc-lg"></i>
                                                <h3>روابط مفيده  </h3>
                                                    </div>
                                               <div class=" pull-left">
                                                  <% if  ERpMaen.LoginInfo.get_form_operation("38") = True Then   %>

                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#addLinks_modal">
                                                    اضافة رابط   <i class="fa fa-plus"></i>
                                                    
                                                </button>
                                                     <% End If %>
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
                          
                            <th>الرابط</th>
                            <th>الوصف</th>
                             <th>الاجراء</th>
                            

                        </tr>
                        <tbody id="course_Links">

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
                                     <% End If %>
                                    <%
                                            If ERpMaen.LoginInfo.get_form_operation("31") = True Or ERpMaen.LoginInfo.get_form_operation_group("31") Then
%>
                                    <div class="inner">
                                        <div class="edit-order">
                                            <div class="edit-head order_wid">
                                                <i class="zmdi zmdi-wrench zmdi-hc-lg"></i>
                                                <h3>التحكم في الدورة</h3>
                                              
                                            </div>
                                            <div class="edit-body">
                                                 <% If ERpMaen.LoginInfo.get_form_operation("32") = True Then   %>
                                            <button type="button" class="btn btn_rate btn-warning" data-toggle="modal" data-target="#courseEvalution" onclick="viewEvaluation();">
                                                    <i class="zmdi zmdi-star"></i>تقييم المادة
                                               
                                              </button>
                                                    <% End If %>
                                                  <% If ERpMaen.LoginInfo.get_form_operation("36") = True Then   %>

                                                <button type="button" class="btn btn_com btn-purple" data-toggle="modal" data-target="#contact_Trainer">
                                                    <i class="zmdi zmdi-headset-mic"></i> تواصل مع المدرب
                                                </button>
                                                <% End If %>
                                                    <% If ERpMaen.LoginInfo.get_form_operation("33") = True Then   %>
                                                    <button type="button" class="btn btn_rec btn-danger" onclick="deleteCourse();">
                                                    <i class="zmdi zmdi-headset-mic"></i>
                                                       حذف المادة</button>
                                                
                                                 <% End If %>
                                             
                                                 <% If ERpMaen.LoginInfo.get_form_operation("34") = True Then   %>
                                                   <button type="button" class="btn btn_rec btn-dark" onclick="archiveCourse();">
                                                    <i class="zmdi zmdi-headset-mic"></i>
                                                        اضافة الى الارشيف</button>
                                               
                                                <% End If %>

                                            </div>
                                        
                                        </div>
                                    </div>
                                      <% End If %>
                                    <%--<div class="inner">
                                        <div class="comments">
                                             <div class="edit-head order_wid">
                                                <i class="zmdi zmdi-wrench zmdi-hc-lg"></i>
                                                <h3> روابط مفيدة </h3>
                                              
                                            </div>
                                         
                                            <div class="comment-body">
                                               
                                                <div class="comment-users">
                                                   
                                                    <div id="divformLinks">
                                                        
                                              
                                                                
                                                        </div>
                                                    
                                                </div>
                                                <div class="comment-form">
                                                    
                                                        <div class="form-group">

                                                         <button type="button" class="btn btn-purple btn-hint" data-toggle="modal" data-target="#addLinks_modal">اضافة رابط </button>
                                                           
                                                        </div>
                                                     
                                                   
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                                                      <%
                                            If ERpMaen.LoginInfo.get_form_operation("40") = True Or ERpMaen.LoginInfo.get_form_operation_group("40") Then
%>
                                    <div class="inner">
                                        <div class="comments">
                                         
                                            <div class="comment-body">
                                               
                                                <div class="comment-users">
                                                   
                                                    <div id="divformComments">
                                                        
                                           
                                                                
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
                                                     <% If ERpMaen.LoginInfo.get_form_operation("43") = True Then   %>
                                                        <div class="form-group">

                                                            <button type="button" class="btn" onclick="addComment();">ارسال</button>
                                                           
                                                        </div>
                                                      <% End If %>
                                                   
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                  <% End If %>
                                </div>
                                <div class="order-sidebar order col-md-4 col-xs-12">
                                   
                                    <div class="inner">
                                        <div class="owner">
                                            <div class="owner-head side_head">
                                                <h3 >
                                                    <i class="zmdi zmdi-account zmdi-hc-lg"></i>
                                                    مدرس المادة 
                                                </h3>
                                            </div>
                                            <div class="owner-body">
                                                <div class="owner-usr">
                                                    <div class="usr-img">
                                                       
                                                        <img src="" alt="" id="tr_Image"/>
                                                    </div>
                                                    <div class="usr-data">
                                                        <h3 id="course_trainer">
                                                           
                                                          
                                                        </h3>

                                                      
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    
                                      <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>

                                    <div class="inner">
                                        <div class="add-hint">
                                            
                                            <div class="hint-head side_head">
                                                    <div>
                                                        <h3>
                                                            <i class="zmdi zmdi-storage zmdi-hc-lg"></i>
                                                            الطلاب  
                                                        </h3>
                                              
                                                    
                                                       
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


                                               
                                               
                                                </table>
                                            </div>

                                         </div>
                                             </div>
                                       
                                        </div>
                                     


                                    <div class="inner" >
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


                                                                        
                                    <div class="inner" style="display:none;">
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
                                                  <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
                                                <button type="button" class="btn btn-purple btn-hint" data-toggle="modal" data-target="#exams">اضافة اختبار </button>
                                           <% End If   %>
                                                </div>
                                        </div>
                                    </div>
                                      <% End If %>


                                     

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
                                                 <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
                                                <button type="button" class="btn btn-purple btn-hint" data-toggle="modal" data-target="#addNote">اضافة ملاحظة </button>
                                             <% End If %>
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
                                                 <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
                                                <button type="button" class="btn btn-purple btn-hint" data-toggle="modal" data-target="#activity_edit">اضافة انشطة </button>
                                             <% End If %>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                 </section>
            
            <div class="modal" id="editDiplomaSubject" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title"> تعديل المادة</h4>
                        </div>
                       
                        <div class="modal-body">
                            
                            <div class="row" id="divFormsubjectEdit">
                             <div class="col-md-6">
                          
                            <div class="row form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label class="label-required">المادة  </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <asp:DropDownList dbcolumn="subject_id" class="form-control" ClientIDMode="Static" ID="ddlsubject" runat="server">
                                    </asp:DropDownList>
                                   
                                </div>
                            </div>
                             <div class="row form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label > مدةالمحاضره      </label>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="lecture_time" placeholder=" مدة المحاضرة بالدقيقة " type="text" id="lecture_time"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    
                                        </div>
                            </div>

                                  <div class="row form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label class="label-required">الفصل الدراسى  </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <asp:DropDownList dbcolumn="semster_id" class="form-control" ClientIDMode="Static" ID="ddlsemster" runat="server">
                                    </asp:DropDownList>
                                   
                                </div>
                            </div>

                                   <div class=" row form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label  class="label-required">  المدرب</label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <asp:DropDownList dbcolumn="trainer_id" required class="form-control" ClientIDMode="Static" ID="ddlsubTrainer" runat="server">
                                    </asp:DropDownList>
                                    
                                </div>
                            </div>

                                    
                                               
                            </div>
                                
                             <div class="col-md-6">
                                
                                 <div class="row form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label class="label-required" >    درجة الاختبار النهائي      </label>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="final_exam_degrees" type="text" id="Text2"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    <br />
                                        </div>
                            </div>

                                   <div class="row form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label class="label-required" >   درجة اعمال السنة  </label>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="activity_degrees" type="text" id="Text3"
                                                class="form-control" runat="server" clientidmode="Static" />
                                 
                                        </div>
                            </div>
                            <div class=" row form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label for="Name" >هدف  المادة </label>

                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="subject_goal" ClientIDMode="Static" ID="subject_goal1" runat="server">
                                    </asp:TextBox>


                                </div>
                            </div>
                                  </div>
                          
                                 </div>
                            
                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="saveCourse();">اضافه </button>
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
                                        <input onkeypress="return isNumber(event);"  readonly="readonly" dbcolumn="lecture_code" type="text" id="lecture_code"
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

                                       <div class=" row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label class="label-required">وقت بداية المحاضرة</label>
                                    </div>

                                    <div class="col-md-9 col-sm-12" id="divstartTime">
                                        <input onkeypress="return isNumber(event);" required dbcolumn="start_time" type="text" id="startTime"
                                            class="form-control" runat="server" clientidmode="Static" />


                                    </div>
                                </div>

                                       </div>
                                   <div class="col-md-6">
                                           <div class="row form-group">
                                 <div class="btn-group pull">
                                <button type="button" class="btn btn-info " style="margin-right :100px;" data-toggle="modal" data-target="#AddHall"> اضافة قاعة  <i class="fa fa-plus"></i></button>

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
                                                <label for="Name">الملاحظات </label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox SkinID="form-control" TextMode="multiline"   class="form-control" dbColumn="notes" ClientIDMode="Static" ID="lecNotes" runat="server">
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
                                            <asp:TextBox SkinID="form-control"  TextMode="multiline"  class="form-control" dbColumn="condition" required  ClientIDMode="Static" ID="conditionsub" runat="server">
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
                                            <label for="Name" class="label-required"> عنوان الملف </label>

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
                                    <th>غياب</th>


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

               <div class="modal fade" id="publicStudentDegree" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">جدول الدرجات  </h4>
                            <div class="col-md-6" >
                            <input  id="txtstud_Search" onkeyup="SearchStudent();" type="text" class="form-control" placeholder="بحث عن طالب" />
                        </div>
                        </div>
                       
                        <div class="table-responsive">
                            <table class="table table-bordered table-hover" id="publicDeg">
                                <tr>
                                    <th>الاسم </th>
                                    <th> درجة الاختبار النهائي</th>
                                     <th>   درجة اعمال السنة</th>



                                </tr>
                                   
                                <tbody id="pblcstudentdegrees">
                                 
                              
                              
                                         </tbody>  


                            </table>
                        </div>


                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="addStudentdegree();">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="publicStudentDegree_Admin" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">جدول الدرجات  </h4>
                            <div class="col-md-6" >
                            <input  id="txtstud_Search_Admin" onkeyup="SearchStudent();" type="text" class="form-control" placeholder="بحث عن طالب" />
                        </div>
                        </div>
                       
                        <div class="table-responsive">
                            <table class="table table-bordered table-hover" id="publicDeg">
                                <tr>
                                    <th>الاسم </th>
                                    <th>الدرجة النهائية</th>
                                     <th> درجة نصف العام</th>



                                </tr>
                                   
                                <tbody id="pblcstudentdegrees_Admin">
                                 
                              
                              
                                         </tbody>  


                            </table>
                        </div>


                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="Approve_Studentdegree();">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>

              <div class="modal" id="AddHall" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title"> قاعة جديدة</h4>
                    </div>
                    <div class="modal-body">
                        <div class="col-md-12" id="divFormnewHall">
                          
                          
                            <div class="row form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label>القاعة  </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                            <input dbcolumn="Description" type="text" id="hall"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    <br />
                                        </div>
                            </div>

                                     
                          
                                 </div>



                        </div>
                  
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary"  onclick="addHalls();">حفظ </button>

                    </div>
                      </div>
                </div>
            </div>
            <div class="modal" id="addLinks_modal" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">   رابط جديد</h4>
                        </div>
                        <div class="modal-body">
                            <div id="divFormAddLinks">

                                <div class="row form-group">
                                        <div class="col-md-3 col-sm-12">
                                    <label class="label-required" >العنوان  </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                            <input dbcolumn="title" required type="text" id="Text1"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                   
                                        </div>
                                    </div>

                                <div class="form-group row">

                                    <div class="col-md-3 col-sm-12">
                                    <label class="label-required">الرابط  </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                            <input dbcolumn="URL" type="text" required id="Text4"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    <br />
                                        </div>
                                                                                                                   
                                </div>

                                <div class=" form-group row">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" > الوصف  </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control"  TextMode="multiline"  class="form-control" dbColumn="notes"   ClientIDMode="Static" ID="TextBox9" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                            </div>


                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="addNewlink();">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>

            <%--<div class="modal" id="addLinks_modal" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">  رابط جديد</h4>
                    </div>
                    <div class="modal-body">
                        <div class="col-md-12" id="divFormAddLinks">
                          
                          
                            <div class="row form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label class="label-required" >العنوان  </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                            <input dbcolumn="title" required type="text" id="link_title"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    <br />
                                        </div>
                            </div>

                            <div class="row form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label class="label-required">الرابط  </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                            <input dbcolumn="URL" type="text" required id="link_url"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    <br />
                                        </div>
                            </div>

                             <div class="row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" > الوصف  </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control"  TextMode="multiline"  class="form-control" dbColumn="notes"   ClientIDMode="Static" ID="linkNote" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                     
                          
                                 </div>



                        </div>
                  
                    <div class="modal-footer">
                        <button type="button"  class="btn btn-primary" onclick="addNewlink();">حفظ </button>

                    </div>
                      </div>
                </div>
            </div>--%>

              <div class="modal" id="file_upload_hw_answers" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title"> ارفاق حل الواجب</h4>
                        </div>
                        <div class="modal-body">
                            <div id="divFormuploadHMfiles">

                                             <div class="row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" > ملاحظات  </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control"  TextMode="multiline"  class="form-control" dbColumn="notes" required  ClientIDMode="Static" ID="TextBox6" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                <div class="form-group row">

                                    <div>
                                        <input id="hwansfileurl" type="hidden" dbcolumn="image" runat="server" />
                                        <input id="fnamehwans" type="text"   readonly="readonly" required runat="server" />

                                    </div>
                                    <div class="clear">
                                    </div>
                                    <asp:AsyncFileUpload ID="fuFile5" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                        OnClientUploadComplete="UploadComplete6" />
                                                                                                                   
                                </div>
                            </div>


                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="saveHWanswer();">اضافه </button>
                        </div>
                    </div>
                </div>
            </div>


            <div class="modal" id="file_upload_exam_answers" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title"> ارفاق حل الاختبار</h4>
                        </div>
                        <div class="modal-body">
                            <div id="divFormuploadexamfiles">

                                             <div class="row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" class="label-required"> ملاحظات  </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control"  TextMode="multiline"  class="form-control" dbColumn="notes" required  ClientIDMode="Static" ID="TextBox7" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                <div class="form-group row">

                                    <div>
                                        <input id="examansfileurl" type="hidden" dbcolumn="image" runat="server" />
                                        <input id="fnameExamans" type="text"   readonly="readonly" required runat="server" />

                                    </div>
                                    <div class="clear">
                                    </div>
                                    <asp:AsyncFileUpload ID="fuFile6" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                        OnClientUploadComplete="UploadComplete7" />
                                                                                                                   
                                </div>
                            </div>


                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="saveExamanswer();">اضافه </button>
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
                                            <asp:TextBox SkinID="form-control" required class="form-control" dbColumn="title" ClientIDMode="Static" ID="examTitle" runat="server">
                                            </asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="examTitle"
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
                                    <asp:AsyncFileUpload ID="fuFile2"  required SkinID="image-upload" runat="server"  OnUploadedComplete="PhotoUploaded"
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
                                            <asp:TextBox SkinID="form-control" required class="form-control" dbColumn="title" ClientIDMode="Static" ID="HMtitle" runat="server">
                                            </asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="HMtitle"
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
                            <h4 class="modal-title">تقييم المادة </h4>
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
                                            <label for="Name">تعليق </label>

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


            <div class="modal fade" id="StudenthomeworkAnswers" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">حلول الواجبات   </h4>
                            <div class="col-md-6" >
                            <input  id="txtstudhw_Search" onkeyup="SearchStudent();" type="text" class="form-control" placeholder="بحث عن طالب" />
                        </div>
                        </div>
                        <%--  جدول الغياب--%>
                        <div class="table-responsive">
                            <table class="table table-bordered table-hover" id="publicDeg">
                                <tr>
                                    <th>الاسم </th>
                                    <th> الحل</th>
                                     <th> الدرجة  </th>



                                </tr>
                                   
                                <tbody id="studentHWAnswers">
                                 
                              
                               <%-- <tr>
                                    <td>
                                        <label> ahmed mohamed</label>
                                    </td>
                                    <td>
               <input id="finaldegee" type="text"   />
                                        55
                                    </td>
                                    <td>
               <input id="activitydegee" type="text"  />
                                        66
                                    </td>
                                </tr>--%>
                                         </tbody>  


                            </table>
                        </div>


                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="saveHomeworkDegree();">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>


            <div class="modal fade" id="StudentExamskAnswers" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">حلول الاختبارات   </h4>
                            <div class="col-md-6" >
                            <input  id="txtstudExam_Search" onkeyup="SearchStudent();" type="text" class="form-control" placeholder="بحث عن طالب" />
                        </div>
                        </div>
                        <%--  جدول الغياب--%>
                        <div class="table-responsive">
                            <table class="table table-bordered table-hover" id="publicDeg">
                                <tr>
                                    <th>الاسم </th>
                                    <th> الحل</th>
                                     <th> الدرجة  </th>



                                </tr>
                                   
                                <tbody id="studentExamAnswers">
                                 
                              
                               <%-- <tr>
                                    <td>
                                        <label> ahmed mohamed</label>
                                    </td>
                                    <td>
               <input id="finaldegee" type="text"   />
                                        55
                                    </td>
                                    <td>
               <input id="activitydegee" type="text"  />
                                        66
                                    </td>
                                </tr>--%>
                                         </tbody>  


                            </table>
                        </div>


                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="saveExamkDegree();">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>
       
         

           
    </main>

    <%-- end form--%>
</asp:Content>
