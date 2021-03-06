﻿<%@ Page Language="vb" AutoEventWireup="false"   CodeBehind="CourseDetails.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.CourseDetails" EnableEventValidation="false" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/courseDetailsCls.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/MultiFileUploader.asmx" />
                   <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />

        </Services>
    </asp:ScriptManager>
 <link rel="stylesheet" href="../assets/css/app.min.css"/>
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
    </style>

    <style>
        .app-main {
            opacity: 1;
        }

        .pull-right .dropdown-menu {
            right: auto;
            left: 0;
        }

        #newitem tbody td img {
            width: 50px;
            height: 50px;
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

        #publicDeg td input {
            width: 70px;
            padding: 5px;
            border: 2px solid #ccc;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            margin-right: 10px;
        }

        #student tbody td img {
            width: 50px;
            height: 50px;
        }

        @media screen and (min-width: 800px) {
            .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9 {
                float: right;
            }

            .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9 {
                float: right;
            }
        }

        .Empty {
            background: #FFFECB url("images/empty_star.png");
            background-size: contain;
        }

            .Empty:hover {
                background: #FED352
            }

        .Filled {
            background: #F49818 url("images/fill_star.png");
            background-size: contain;
        }
    </style>

    <%--  start form--%>
    <main id="app-main" class="app-main" style="margin-top:50px;">
        <div class="wrap">
             <div>
<script src="../JS_Code/acadmies/Course_Details.js"></script>
                 <script src="../js/customCalender/CustomerCalendar.js"></script>
                </div>
            <section class="app-content">
                <div class="row">
                     <label style="display:none" id="Lblcourse_id" runat="server" ></label>
                     <label style="display:none" id="lblcode" runat="server" ></label>
                     <label style="display:none" id="LblLecture_id" ></label>
                      <label style="display:none" id="LblHomework_id" ></label>
                       <label style="display:none" id="LblExam_id" ></label>
                    <label style="display:none" id="LblAbsence_id" ></label>
                    <label style="display:none" id="lect_time" ></label>
                    <label style="display:none" id="lblStudentID" runat="server" ></label>
                    <div id="SavedivLoader" class="loader" style="display: none; text-align: center;">
                        <asp:Image ID="img" runat="server" ImageUrl="../App_Themes/images/loader.gif" />
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12 pull-right">
                        <div class="p-md clearfix widget-orders widget-def widget-panels widget-users widget-financial widget-request">
                            <div class="widget-header">
                                <h3 >
                                    <i class="fa fa-book"></i>
                                    <span id="course_title" > </span>
                                  </h3>
                                <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
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
          اختبارات عامة
      </a></li>
      <li><a data-toggle="modal" href="#homeworks">
          واجبات عامة
      </a></li>
      <li><a data-toggle="modal" href="#courseEvalution">
          تقييم الدورة
      </a></li>
       <li><a data-toggle="modal" href="#publicStudentDegree" onclick="drawpublicDegreeTable();">
           درجات الطلاب
      </a></li>
  </ul>
                                    </div>
<% End If %>
                            </div>
                            <div class="order-card col-xs-12">
                                <div class="order-panel order col-md-8 col-xs-12">                                    <div class="inner">
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
                                                    <span class="price " >
                                                        <b id="course_price"></b>
                                                          <i class="fa fa-money"> </i>
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
                                                    <span id="">
                                                        <i class="fa fa-clock-o"></i>
                                                       <b id="course_duration"></b>
                                                        <b>يوم</b>
                                                </span>
                                              </li>
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
                                                      <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#order_addLec" onclick="getlectureCode();">
                                                    اضافة محاضرة   <i class="fa fa-plus"></i>
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
                            <th>المحاضرة </th>
                            <th>التاريخ</th>
                            <th>الوقت </th>
                            <th>القاعه </th>
                             <% if ERpMaen.LoginInfo.getUserType = 8 Then   %>
                            <th>الحاله </th>
                             <% Else    %>
                               <th>الاجراء </th>
                             <%end if   %>
                        </tr>
                        <tbody id="lectures-table"> </tbody>
                        </table>
                    </div>
                    </div>
                                                                 </div>   

                                                        </div>
                                                            <!-- end widget-body -->
                                                            <!-- end widget -->
                                                    </div>
                                                </section>
                                                <%-- نهايةالجدول--%>
                                            </div>
                                        </div>
                                    </div>
                            <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
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
                        <tbody id="conditions-table"> </tbody>
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
                                    <% End If %>

                                     <% if ERpMaen.LoginInfo.getUserType = 2 Then   %>
                                    <div class="inner">
                                        <div class="order-desc row">
                                            <div class="desc-head order_wid col-md-12"">
                                                <div class=" pull-right">
                                                <i class="zmdi zmdi-file-text zmdi-hc-lg"></i>
                                                <h3>   الدفعات المالية </h3>
                                                    </div>
                                            </div>
                                            <div class="desc-body">
                                                <section class="app-content">
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12 col-xs-12 pull-right"  >
                                                             <div class="widget-body">
                <div class="trans-data col-xs-12" >
                                                      <%--finance new--%> 
                    <div class="table-responsive" >
                                                <table id="student">
                                                     <tr>
                                                        <th>الطالب </th>
                                                        <th>المبلغ</th>
                                                        <th>التاريخ </th>
                                                         <th>المرفق </th>
                                                           <th>الحالة </th>
                                                         <th>الاجراء </th>
                                                    </tr>
                                                    <tbody id="Student_Finance"> </tbody>
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
                                    <% End If %>

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
                                                                   <%-- <h3>جدول الواجبات </h3>--%>
                                                                    <div class="table-responsive">
                                                                        <table class="table table-bordered table-hover">
                                                                            <tr>
                                                                                <th>اسم الواجب </th>
                                                                                <th>تفاصيل الواجب</th>
                                                                                <th>مرفق الواجب </th>
                                                                                  <th>درجة الواجب </th>
                                                                                <th>رفع حل </th>
                                                                            </tr>
                                                                            <tbody id="studenthomeworktable"></tbody>
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
                                                <h3>جدول الاختبارات </h3>
                                                    </div>
                                            </div>
                                            <div class="desc-body">
                                                <%-- الجدول بداية--%>
                                                <section class="app-content">
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12 col-xs-12 pull-right">
                                                            <div class="widget-body">
                                                                <div class="trans-data col-xs-12">
                                                                   <%-- <h3>جدول الواجبات </h3>--%>
                                                                    <div class="table-responsive">
                                                                        <table class="table table-bordered table-hover">
                                                                            <tr>
                                                                                <th>اسم الاختبار </th>
                                                                                <th>تفاصيل الاختبار</th>
                                                                                <th>مرفق الاختبار </th>
                                                                                  <th>درجة الاختبار </th>
                                                                                <th>رفع حل </th>
                                                                            </tr>
                                                                            <tbody id="studentExamstable"></tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!-- end widget-body -->
                                                            <!-- end widget -->
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
                                                <h3> درجات الدورة </h3>
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
                                                                            <tbody id="studentcourseDegreestable"></tbody>
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
                                    
                                    <div class="inner">
                                        <div class="order-desc row">
                                            <div class="desc-head order_wid col-md-12"">
                                                <div class=" pull-right">
                                                <i class="zmdi zmdi-file-text zmdi-hc-lg"></i>
                                                <h3>روابط مفيده  </h3>
                                                    </div>
                                               <div class=" pull-left">
                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#addLinks_modal">
                                                    اضافة رابط   <i class="fa fa-plus"></i>
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
                            <th>الرابط</th>
                            <th>الوصف</th>
                             <th>الاجراء</th>
                        </tr>
                        <tbody id="course_Links"></tbody>
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
                                   
                                    <div class="inner">
                                        <div class="order-desc row">
                                            <div class="desc-head order_wid col-md-12"">
                                                <div class=" pull-right">
                                                <i class="zmdi zmdi-file-text zmdi-hc-lg"></i>
                                                <h3>ملفات الدورة  </h3>
                                                    </div>
                                               <div class=" pull-left">
                                                    <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#order_addfiles">
                                                     اضاقة ملف   <i class="fa fa-plus"></i>
                                                </button>
                                                      <% End If %>
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
                            <th>الملاحظة </th>
                            <th>الملف</th>
                              <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
                            <th>الاجراء</th>
                             <% End If %>
                        </tr>
                        <tbody id="courseFiles-table"></tbody>
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
                                                </button>
                                                <% if ERpMaen.LoginInfo.getUserType <> 4 Then   %>
                                                <button type="button" class="btn btn_com btn-purple" data-toggle="modal" data-target="#contact_Trainer">
                                                    <i class="zmdi zmdi-headset-mic"></i>تواصل مع المدرب
                                                </button>
                                                <% End If %>
                                                <% if ERpMaen.LoginInfo.getUserType = 2 Then   %>
                                                <button type="button" class="btn btn_rec btn-danger" onclick="deleteCourse();">
                                                    <i class="zmdi zmdi-headset-mic"></i>
                                                    حذف الدورة</button>
                                                <% End If %>
                                                <% if ERpMaen.LoginInfo.getUserType <> 2 Then   %>
                                                <button type="button" class="btn btn_com btn-primary" data-toggle="modal" data-target="#contact_admin">
                                                    <i class="zmdi zmdi-headset-mic"></i>تواصل مع الادراة
                                                </button>
                                                <% End If %>

                                                <% if ERpMaen.LoginInfo.getUserType = 2 Then   %>
                                                <button type="button" class="btn btn_rec btn-dark" id="archiveBtn" onclick="archiveCourse();" runat="server">
                                                    <i class="zmdi zmdi-headset-mic"></i>
                                                    اضافة الى الارشيف</button>
                                                <% End If %>
                                            </div>
                                        </div>
                                    </div>

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
                                                    <div class="form-group">
                                                        <button type="button" class="btn" onclick="addComment();">ارسال</button>
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
                                                <h3>
                                                    <i class="zmdi zmdi-account zmdi-hc-lg"></i>
                                                    مدرب الدورة
                                                </h3>
                                            </div>
                                            <div class="owner-body">
                                                <div class="owner-usr">
                                                    <div class="usr-img">
                                                        <img src="" alt="" id="tr_Image" />
                                                    </div>
                                                    <div class="usr-data">
                                                        <h3 id="course_trainer"></h3>
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
                                                    <div class=" pull-left">
                                                        <% if ERpMaen.LoginInfo.getUserType = 2 Then   %>
                                                        <button style="margin-top: -20px;" type="button" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#addStudentModal">
                                                            اضافة طلاب   <i class="fa fa-plus zmdi zmdi-storage zmdi-hc-lg"></i>
                                                        </button>

                                                        <% End If    %>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <input id="txt_Search" onkeyup="studentSearch();" type="text" class="form-control" placeholder="بحث عن طالب" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="table-responsive">
                                                <div id="StudentTable">
                                                    <table id="student"></table>
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
                                                <div class="table-responsive" style="padding-right:10px;padding-left:10px;">
                                                    <table id="student">
                                                        <tr>
                                                            <th>الواجب </th>
                                                            <th>التاريخ</th>
                                                            <th>الاجراء </th>
                                                        </tr>
                                                        <tbody id="homework"></tbody>
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
                                                        <tbody id="courseExams"></tbody>
                                                    </table>
                                                </div>
                                                <button type="button" class="btn btn-purple btn-hint" data-toggle="modal" data-target="#exams">اضافة اختبار </button>
                                            </div>
                                        </div>
                                    </div>
                                    <% End If %>
                                    <div class="inner">
                                        <div class="add-hint">
                                            <div class="hint-head side_head">
                                                <h3>
                                                    <i class="zmdi zmdi-storage zmdi-hc-lg"></i>
                                                    الملاحظات
                                                </h3>
                                            </div>
                                            <div class="hint-body">
                                                <div id="StudentNote">
                                                    <p>لا توجد ملاحظات</p>
                                                </div>
                                                <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
                                                <button type="button" class="btn btn-purple btn-hint" data-toggle="modal" data-target="#addNote">اضافة ملاحظة </button>
                                                <% End If %>
                                            </div>
                                        </div>
                                    </div>
                                    <% if ERpMaen.LoginInfo.getUserType = 8 Then   %>
                                    <div class="inner" id="finance_div" runat="server">
                                        <div class="add-hint">
                                            <div class="hint-head side_head">
                                                <h3>
                                                    <i class="zmdi zmdi-storage zmdi-hc-lg"></i>
                                                    الدفعات المالية
                                                </h3>
                                            </div>
                                            <div class="hint-body">
                                                <div class="table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th>المبلغ</th>
                                                            <th>الحاله </th>
                                                        </tr>
                                                        <tbody id="financestudent"></tbody>
                                                        <tfoot>
                                                            <tr>
                                                                <td>اجمالى المبالغ المؤكدة</td>
                                                                <td>
                                                                    <label id="total_money"></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>المبلغ المتبقى</td>
                                                                <td>
                                                                    <label id="Rest_money"></label>
                                                                </td>
                                                            </tr>
                                                        </tfoot>
                                                        <%-- here--%>
                                                    </table>
                                                </div>
                                                <button type="button" class="btn btn-purple btn-hint" data-toggle="modal" data-target="#add_Financial">اضافة مالية جديدة</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="inner" id="Div1" runat="server">
                                        <div class="add-hint">
                                            <div class="hint-head side_head">
                                                <h3>
                                                    <i class="zmdi zmdi-storage zmdi-hc-lg"></i>
                                                    شهادة الدورة
                                                </h3>
                                            </div>
                                            <div class="hint-body">
                                                <div class="table-responsive">
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th>اصل الشهادة</th>
                                                        </tr>
                                                        <tbody id="student_Certificate"></tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <% End If %>
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
                                                    <p>لا توجد انشطة حاليه</p>
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
          </div>

        <div class="modal fade" id="EditCourseModal" tabindex="-1" role="dialog">
            <div class="modal-dialog" style="width: 900px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">تعديل بيانات الدورة</h4>
                    </div>
                    <div class="modal-body" style="height:500px; overflow-y:scroll">
                        <div class="modal-body">
                            <div id="divformEditCourse" class="row">

                                <div class="col-md-12">
                                    <uc1:Result runat="server" ID="res1" />
                                </div>
                                <div class="col-md-9">
                                    <div class="row form-group">

                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" class="label-required">عنوان الدورة </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control " required class="form-control" dbColumn="name" ClientIDMode="Static" ID="courseTitle" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name">وصف الدورة </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control " dbColumn="description" ClientIDMode="Static" ID="description" runat="server">
                                            </asp:TextBox>


                                        </div>
                                    </div>
                                </div>
                                                  <div class="col-md-3">
                                
                                <div class="col-md-3 col-sm-12">
                                    <label for="email">مميزة </label>
                                </div>
                                <div class="col-md-3 col-sm-12">
                                    <input type="checkbox" dbcolumn="features" id="features" style="width: 40px;" class="form-control" />

                                </div>
                            </div>
                                <div class="col-md-6">
                                    <div class="row  form-group ">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">القسم </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList required dbcolumn="category_id" class="form-control " ClientIDMode="Static" ID="ddlcategory" runat="server">
                                            </asp:DropDownList>

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
                                    <div class="row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">مدة المحاضرة   </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" placeholder="مدة المحاضرة بالدقيقة" required dbcolumn="lect_duration" type="text" id="lecture_duration"
                                                class="form-control" runat="server" clientidmode="Static" />


                                        </div>
                                    </div>


                                </div>
                                <div class="col-md-6">
                                    <div class=" row form-group ">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">المدرب </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList dbcolumn="trainer_id" required class="form-control" ClientIDMode="Static" ID="ddltrainer" runat="server">
                                            </asp:DropDownList>

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

                                    <div class=" row form-group">
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
                                    <div class=" row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="">السعر     </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="price" type="text" id="price"
                                                class="form-control " runat="server" clientidmode="Static" />


                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" onclick="saveCourse();">حفظ </button>
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
                                    <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control " required dbColumn="activity" ClientIDMode="Static" ID="TextBox4" runat="server">
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
                        <button type="button" class="btn btn-primary" onclick="AddActivity();">اضافة</button>
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
                                        <input onkeypress="return isNumber(event);" readonly="readonly" dbcolumn="lecture_code" type="text" id="lecture_code"
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
                                            <input dbcolumn="date_hj" type="hidden" id="dateLec_hj" />
                                            <input dbcolumn="date_m" type="hidden" id="dateLec_m" />
                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class=" row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label class="label-required">وقت بداية المحاضرة</label>
                                    </div>
                                    <div class="col-md-9 col-sm-12" id="divstartTime">
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
                                        <label for="Name">الملاحظات </label>
                                    </div>
                                    <div class="col-md-9 col-sm-12">
                                        <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="lecNotes" runat="server">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="SaveLec();">اضافه </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="addStudentModal" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">تقديمات الطلاب   </h4>
                    </div>
                    <%--  جدول الطلاب--%>
                    <div class="table-responsive" id="allStudentlist">
                        <table class="table table-bordered table-hover" id="newitem">
                            <thead>
                                <tr>
                                    <th>الطالب </th>
                                    <th>الطلب</th>
                                    <th>الملفات</th>
                                    <th>الاجراء</th>
                                </tr>
                            </thead>
                            <tbody id="action_courseStudents"></tbody>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="AddStudent();">حفظ </button>
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
                                    <label>الشرط</label>
                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="condition" required ClientIDMode="Static" ID="TextBox5" runat="server">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div>
                                    <input id="fileURL" type="hidden" dbcolumn="image" runat="server" />
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
                        <button type="button" class="btn btn-primary" onclick="addCondition();">اضافه </button>
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
                                    <label for="Name" class="label-required">وصف الملف </label>
                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="notes" required ClientIDMode="Static" ID="notes3" runat="server">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div>
                                    <input id="coursefileURL" type="hidden" dbcolumn="image" runat="server" />
                                    <input id="EXFName" type="text" readonly="readonly" required runat="server" />
                                </div>
                                <div class="clear">
                                </div>
                                <asp:AsyncFileUpload ID="fuFile4" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                    OnClientUploadComplete="UploadComplete5" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="addFiles();">اضافه </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal" id="file_upload_hw_answers" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">ارفاق حل الواجب</h4>
                    </div>
                    <div class="modal-body">
                        <div id="divFormuploadHMfiles">
                            <div class="row form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label for="Name">ملاحظات</label>
                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="TextBox6" runat="server">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div>
                                    <input id="hwansfileurl" type="hidden" dbcolumn="image" runat="server" />
                                    <input id="fnamehwans" type="text" readonly="readonly" required runat="server" />
                                </div>
                                <div class="clear">
                                </div>
                                <asp:AsyncFileUpload ID="fuFile5" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                    OnClientUploadComplete="UploadComplete6" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="saveHWanswer();">اضافه </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal" id="file_upload_exam_answers" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">ارفاق حل الاختبار</h4>
                    </div>
                    <div class="modal-body">
                        <div id="divFormuploadexamfiles">
                            <div class="row form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label for="Name">ملاحظات</label>
                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="TextBox7" runat="server">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div>
                                    <input id="examansfileurl" type="hidden" dbcolumn="image" runat="server" />
                                    <input id="fnameExamans" type="text" readonly="readonly" required runat="server" />
                                </div>
                                <div class="clear">
                                </div>
                                <asp:AsyncFileUpload ID="fuFile6" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                    OnClientUploadComplete="UploadComplete7" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="saveExamanswer();">اضافه </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal" id="addLinks_modal" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">رابط جديد</h4>
                    </div>
                    <div class="modal-body">
                        <div id="divFormAddLinks">
                            <div class="row form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label class="label-required">العنوان</label>
                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <input dbcolumn="title" required type="text" id="Text1"
                                        class="form-control" runat="server" clientidmode="Static" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-md-3 col-sm-12">
                                    <label class="label-required">الرابط</label>
                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <input dbcolumn="URL" type="text" required id="Text2"
                                        class="form-control" runat="server" clientidmode="Static" />
                                    <br />
                                </div>
                            </div>
                            <div class=" form-group row">
                                <div class="col-md-3 col-sm-12">
                                    <label for="Name">الوصف</label>
                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="TextBox9" runat="server">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="addNewlink();">حفظ </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="absenceModal" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">جدول الغياب</h4>
                        <div class="col-md-6">
                            <input id="txtAbsen_Search" onkeyup="searchAbsence();" type="text" class="form-control" placeholder="بحث عن طالب" />
                        </div>
                    </div>
                    <%--  جدول الغياب--%>
                    <div class="table-responsive">
                        <table class="table table-bordered table-hover">
                            <tr>
                                <th>الاسم </th>
                                <th>غياب</th>
                            </tr>
                            <tbody id="absenscetable"></tbody>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="SaveAbsenceStudent();">حفظ </button>
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
                                <uc1:Result runat="server" ID="Result1" />
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
                                            <input dbcolumn="date_hj" type="hidden" id="date2_hj" />
                                            <input dbcolumn="date_m" type="hidden" id="date2_m" />
                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar4" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div>
                                        <label id="examfile"></label>
                                        <input id="fileURL3" required type="hidden" dbcolumn="image" runat="server" />
                                        <input id="FName3" type="text" readonly="readonly" runat="server" />
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <asp:AsyncFileUpload ID="fuFile2" required SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                        OnClientUploadComplete="UploadComplete3" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label for="Name" class="label-required">تفاصيل الاختبار </label>
                                    </div>
                                    <div class="col-md-9 col-sm-12">
                                        <asp:TextBox SkinID="form-control" TextMode="multiline" required class="form-control" dbColumn="details" ClientIDMode="Static" ID="details" runat="server">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="SaveExam();">حفظ </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="homeworks" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">الواجبات</h4>
                    </div>
                    <div class="modal-body">
                        <div id="divformHomework" class="row">
                            <div class="col-md-12">
                                <uc1:Result runat="server" ID="Result2" />
                            </div>
                            <div class="col-md-6">
                                <div class="row form-group">
                                    <div class="col-md-3 col-sm-12">
                                        <label for="Name" class="label-required">العنوان</label>
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
                                            <input dbcolumn="date_hj" type="hidden" id="date3_hj" />
                                            <input dbcolumn="date_m" type="hidden" id="date3_m" />
                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar5" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div id="divfileupload">
                                        <label id="fileupload"></label>
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
                                        <label for="Name" class="label-required">تفاصيل الواجب </label>
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
                        <button type="button" class="btn btn-primary" onclick="SaveHomework();">حفظ </button>
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
                            <button type="button" class="btn btn-primary" onclick="SaveDegree();">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="certificateModal" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">الشهادات</h4>
                    </div>
                    <div class="modal-body">
                        <div id="divFormcertificate">
                            <div class=" row form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label>كود الشهادة</label>
                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <input dbcolumn="certificate_code" required type="text" id="certif_code"
                                        class="form-control" runat="server" clientidmode="Static" />
                                </div>
                            </div>
                            <div class=" row form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label class="label-required">
                                        التاريخ  
                                    </label>
                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <div class="fancy-form" id="divdate_certif">
                                        <input dbcolumn="date_hj" type="hidden" id="dtcf_hj" />
                                        <input dbcolumn="date_m" type="hidden" id="dtcf_m" />
                                        <uc1:HijriCalendar runat="server" ID="HijriCalendar7" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div>
                                    <input id="FURL_CERTIF" type="hidden" dbcolumn="image" runat="server" />
                                    <input id="FnameCertif" type="text" required readonly="readonly" runat="server" />
                                </div>
                                <div class="clear">
                                </div>
                                <asp:AsyncFileUpload ID="fuFile9" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                    OnClientUploadComplete="UploadComplete9" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="saveCertificate();">حفظ </button>
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
                    <div class="modal-body" >
                        <div id="divformNote">
                            <div class=" row form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label>محتوى الملاحظة</label>
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
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="AddNote();">حفظ </button>
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
                    <div class="modal-body" >
                        <div id="divformCourseEvalution" class="row" style="padding-right:10px;padding-left:10px;">
                            <div class="col-md-12">
                                <uc1:Result runat="server" ID="Result3" />
                            </div>
                            <div class="row form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label for="Name">التقييم </label>

                                </div>
                                <div class="col-md-9 col-sm-12">
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
                                        <asp:Label ID="lblRate" hidden dbColumn="evalution" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label for="Name">تعليق </label>

                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <asp:TextBox SkinID="form-control" TextMode="multiline" required class="form-control" dbColumn="notes" ClientIDMode="Static" ID="evaluteNotes" runat="server">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="SaveEvalution();">حفظ </button>
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
                                    <label>الرسالة      </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <asp:TextBox SkinID="form-control" TextMode="multiline" required class="form-control" dbColumn="message" ClientIDMode="Static" ID="conmsg" runat="server">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="sendMsg();">ارسال </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="reject_Reason" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">التعليق   </h4>
                    </div>
                    <div class="modal-body">
                        <div id="divformReject_Reason">
                            <div class=" row form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label>التعليق  </label>
                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control " required dbColumn="comment" ClientIDMode="Static" ID="rejectId" runat="server">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="saveResponse();">ارسال </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="contact_admin" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">تواصل مع الادارة</h4>
                    </div>
                    <div class="modal-body">
                        <div id="divformconAdmin">
                            <div class=" row form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label>الرسالة</label>
                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <asp:TextBox SkinID="form-control" TextMode="multiline" required class="form-control" dbColumn="message" ClientIDMode="Static" ID="message" runat="server">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="sendMsgtoAdmin();">ارسال </button>
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
                        <div class="col-md-6">
                            <input id="txtstud_Search" onkeyup="SearchStudent();" type="text" class="form-control" placeholder="بحث عن طالب" />
                        </div>
                    </div>
                    <%--  جدول الغياب--%>
                    <div class="table-responsive" style="padding-right:10px;padding-left:10px;">
                        <table class="table table-bordered table-hover" id="publicDeg">
                            <tr>
                                <th>الاسم </th>
                                <th>الدرجة النهائية</th>
                                <th>درجة نصف العام</th>
                            </tr>

                            <tbody id="pblcstudentdegrees"></tbody>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="addStudentdegree();">حفظ </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="StudenthomeworkAnswers" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">حلول الواجبات</h4>
                        <div class="col-md-6">
                            <input id="txtstudhw_Search" onkeyup="SearchStudent();" type="text" class="form-control" placeholder="بحث عن طالب" />
                        </div>
                    </div>
                    <%--  جدول الغياب--%>
                    <div class="table-responsive">
                        <table class="table table-bordered table-hover" id="publicDeg">
                            <tr>
                                <th>الاسم</th>
                                <th>الحل</th>
                                <th>الدرجة</th>
                            </tr>
                            <tbody id="studentHWAnswers"></tbody>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="saveHomeworkDegree();">حفظ </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="studentFilesModal" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">ملفات الطالب</h4>
                    </div>
                    <div class="table-responsive">
                        <table class="table table-bordered table-hover">
                            <tr>
                                <th>الشرط</th>
                                <th>ملفات الطالب </th>
                            </tr>
                            <tbody id="studentFiles"></tbody>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" data-dismiss="modal" class="btn btn-primary">close </button>
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
                        <div class="col-md-6">
                            <input id="txtstudExam_Search" onkeyup="SearchStudent();" type="text" class="form-control" placeholder="بحث عن طالب" />
                        </div>
                    </div>
                    <%--  جدول الغياب--%>
                    <div class="table-responsive">
                        <table class="table table-bordered table-hover" id="publicDeg">
                            <tr>
                                <th>الاسم </th>
                                <th>الحل</th>
                                <th>الدرجة  </th>
                            </tr>
                            <tbody id="studentExamAnswers"></tbody>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="saveExamkDegree();">حفظ </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="add_Financial" tabindex="-1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">مالية الطلاب   </h4>
                    </div>
                    <div class="modal-body">
                        <div id="divformstudentFinanc">
                            <div class="row form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label class="label-required">المبلغ   </label>
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
                                    <label>ملاحظات</label>
                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="TextBox8" runat="server">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div>
                                    <input id="fileurlfinanc" type="hidden" required dbcolumn="image" runat="server" />
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
                        <button type="button" class="btn btn-primary" onclick="addfinancial();">حفظ </button>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <%-- end form--%>
</asp:Content>
