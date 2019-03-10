<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="courseDetailsStudent.aspx.vb" MasterPageFile="~/Site.Master" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
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
    <link rel="stylesheet" type="text/css" href="../assets/css/ontime.css">
    <link rel="stylesheet" type="text/css" href="../assets/css/ontime-rtl.css">

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
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: right;
                background-color: #4CAF50;
                color: white;
            }

            #student td img {
                width: 100%;
                height: 100%;
            }
    </style>

    <%--  start form--%>

    <main id="app-main" class="app-main" style="margin-top:50px;">
        <div class="wrap">
            <section class="app-content">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 pull-right">
                        <div class="p-md clearfix widget-orders widget-def widget-panels widget-users widget-financial widget-request">
                            <div class="widget-header">
                                <h3>
                                    <i class="menu-icon zmdi zmdi-layers zmdi-hc-lg"></i>
                                    <span>دورة علوم الحاسب</span>
                                </h3>
                                <div class="btn-group pull-left">
  
  
</div>
                              <%--  <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#order_edit">
                                    تعديل الدورة
                               
                                </button>--%>

                            </div>
                            <div class="order-card col-xs-12">
                                <div class="order-panel order col-md-8 col-xs-12">
                                    <%--  ده اول عمود وفيه تفاصيل الكورس ومساحته 8 من 12--%>
                                    <div class="inner">
                                        <%--ارشادات عن الكورس--%>
                                        <div class="order-head order_wid">
                                            <%--هنا تفاصيل بسيطه عن الكورس--%>
                                            <p class="order-title ">تفاصيل الدورة تفاصيل الدورة تفاصيل الدورة تفاصيل الدورة تفاصيل الدورة </p>
                                        </div>
                                        <div class="order-extra">
                                            <ul>

                                                <li>
                                                    <span>
                                                        <%--هنا تاريخ الكورس--%>
                                                        <i class="zmdi zmdi-calendar-note"></i>
                                                        <%--كتب كود التاريخ بالهجرى asp--%>
                                                        7 من رمضان 1440  
                                                </span>
                                                </li>
                                                <%--  <li>
                                                    <span>رقم الدورة : #155</span>
                                                    <%--رقم الكورس من الداتا بيز asp--%>
                                                <%--</li>--%>
                                                <li>
                                                    <span class="price">
                                                        <i class="zmdi zmdi-money"></i>
                                                        200 دولار
                                                       
                                                        <%--سعر الكورس من الداتا بيز asp--%>
                                                </span>
                                                </li>
                                                <li>
                                                    <span>
                                                        <i class="zmdi zmdi-time"></i>
                                                        5 أيام
                                                      
                                                        <%--مده ا لكورس نحسبها من تاريخ البداية والنهاية--%>
                                                </span>
                                                </li>
                                                <li>
                                                    <span class="price">قسم برمجة
                                                      
                                                        <%--قسم الدورة--%>
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
                                              
                                                
                                            </div>
                                            <div class="desc-body">


                                                <%-- الجدول بداية--%>

                                                <section class="app-content">
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12 col-xs-12 pull-right">

                                                            <div class="widget-body">
                                                                <div class="trans-data col-xs-12">
                                                                   <%-- <h3>جدول المحاضرات </h3>--%>
                                                                    <div class="table-responsive">
                                                                        <table class="table table-bordered table-hover">
                                                                            <tr>
                                                                                <th>المحاضرة </th>
                                                                                <th>التاريخ</th>
                                                                                <th>الوقت </th>
                                                                                <th>القاعه </th>
                                                                                 <th>الغياب </th>
                                                                               

                                                                            </tr>
                                                                            <tr>
                                                                                <td>#01 </td>
                                                                                <td>11 ديسمبر 2017</td>
                                                                                <td>9:30 صباحا الى11:30 صباحا  </td>
                                                                                <td>القاعة 4 </td>
                                                                                 <td> غايب </td>
                                                                     

                                                                            </tr>
                                                                               <tr>
                                                                                <td>#02 </td>
                                                                                <td>25 ديسمبر 2018</td>
                                                                                <td>9:30 صباحا الى11:30 صباحا  </td>
                                                                                <td>القاعة 3 </td>
                                                                                <td> حاضر </td>
                                                                                

                                                                            </tr>
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

                                   <%-- جدول الواجبات--%>
                                    <div class="inner">
                                        <div class="order-desc row">
                                            <div class="desc-head order_wid col-md-12"">
                                                <div class=" pull-right">
                                                <i class="zmdi zmdi-file-text zmdi-hc-lg"></i>
                                                <h3>جدول الواجبات </h3>
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
                                                                                <th>اسم الواجب </th>
                                                                                <th>تفاصيل الواجب</th>
                                                                                <th>مرفق الواجب </th>
                                                                                <th>رفع حل </th>
                                                                                 <th>درجة الواجب </th>
                                                                               

                                                                            </tr>
                                                                            <tr>
                                                                                <td> os assigment  </td>
                                                                                <td>define CORBA  </td>
                                                                                <td> 
                                                        <a href="#">
                                                            <i class="zmdi zmdi-cloud-download"></i>   ارفاق الواجب 

                                                        </a>
                                                        <span>(8كيلوبايت)</span>
                                                       </td>
                                                                                <td> 
                                   <button type="button" class="btn btn-upload" data-toggle="modal" data-target="#file_upload">
                                                                إرفاق الحل
                                                           
                                                            </button>
                                                       </td>
                                                                                 <td> 5 </td>
                                                                     

                                                                            </tr>
                                                                               <tr>
                                                                                <td> os assigment  </td>
                                                                                <td>define CORBA  </td>
                                                                                <td> 
                                                        <a href="#">
                                                            <i class="zmdi zmdi-cloud-download"></i>   ارفاق الواجب 

                                                        </a>
                                                        <span>(8كيلوبايت)</span>
                                                       </td>
                                                                               <td> 
                                   <button type="button" class="btn btn-upload" data-toggle="modal" data-target="#file_upload">
                                                                إرفاق الحل
                                                           
                                                            </button>
                                                       </td>
                                                                                 <td> 5 </td>
                                                                                

                                                                            </tr>
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
                                    <%--end ASSIGNMENT table--%>
                                    <%--جدول الاختبارات--%>
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
                                                                                <th>رفع حل </th>
                                                                                 <th>درجة الاختبار </th>
                                                                               

                                                                            </tr>
                                                                            <tr>
                                                                                <td> os EXAM  </td>
                                                                                <td>define CORBA  </td>
                                                                               <td> 
                                                        <a href="#">
                                                            <i class="zmdi zmdi-cloud-download"></i>   ارفاق الاختبار 

                                                        </a>
                                                        <span>(8كيلوبايت)</span>
                                                       </td>
                                                                               <td> 
                                   <button type="button" class="btn btn-upload" data-toggle="modal" data-target="#file_upload">
                                                                إرفاق الحل
                                                           
                                                            </button>
                                                       </td>
                                                                                 <td> 20 </td>
                                                                     

                                                                            </tr>
                                                                               <tr>
                                                                                <td> os EXAM  </td>
                                                                                <td>define CORBA  </td>
                                                                                <td> 
                                                        <a href="#">
                                                            <i class="zmdi zmdi-cloud-download"></i>   ارفاق الاختبار 

                                                        </a>
                                                        <span>(8كيلوبايت)</span>
                                                       </td>
                                                                               <td>
                                                                                 
                                   <button type="button" class="btn btn-upload" data-toggle="modal" data-target="#file_upload">
                                                                إرفاق الحل
                                                           
                                                            </button>
                                                       </td>
                                                                                 <td> 20 </td>
                                                                                

                                                                            </tr>
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
                                   <%-- END EXAMS TABLE--%>
                                    <div class="inner">
                                        <div class="attached-files">
                                            <div class="attach-head order_wid">
                                                <i class="zmdi zmdi-attachment-alt zmdi-hc-lg"></i>
                                                <h3>ملفات الدورة</h3>
                                            </div>
                                            <div class="attach-body">
                                                <%-- كود مرفقات مع الكورس من الداتا بيز برضو asp--%>
                                                <ul>
                                                    <li>
                                                        <a href="#">
                                                            <i class="zmdi zmdi-cloud-download"></i>تحميل ملف مرفق مع الطلب

                                                        </a>
                                                        <span>(2كيلوبايت)</span>
                                                    </li>
                                                    <li>
                                                        <a href="#">
                                                            <i class="zmdi zmdi-cloud-download"></i>تحميل ملف مرفق مع الطلب

                                                        </a>
                                                        <span>(2كيلوبايت)</span>
                                                    </li>
                                                </ul>
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
                                            <button type="button" class="btn btn_rate btn-warning" data-toggle="modal" data-target="#order_rate">
                                                    <i class="zmdi zmdi-star"></i>تقييم الدورة
                                             
                                                <button  type="button" class="btn btn_com btn-purple" data-toggle="modal" data-target="#order_com">
                                                    <i class="zmdi zmdi-headset-mic"></i> تواصل مع المدرب
                                                </button>
                                                   
                                                </button>
                                                <button type="button" class="btn btn_com btn-primary" data-toggle="modal" data-target="#order_com">
                                                    <i class="zmdi zmdi-headset-mic"></i> تواصل مع الادراة
                                                </button>

                                            </div>
                                          <%--  <div class="edit-footer order_wid">
                                                
                                            </div>--%>
                                        </div>
                                    </div>
                                    <div class="inner">
                                        <div class="comments">
                                         
                                            <div class="comment-body">
                                                <%-- التعليقات على الدورة ان وجدت--%>
                                                <div class="comment-users">
                                                    <ul>
                                                        <li>
                                                            <div class="user">
                                                                <div class="usr-img">
                                                                    <img src="../assets/images/104.jpg">
                                                                </div>
                                                                <div class="usr-data">
                                                                    <h3>
                                                                        <a href="#">امير ناجح</a>
                                                                    </h3>
                                                                    <span>
                                                                        <i class="zmdi zmdi-calendar"></i>
                                                                        11 ديسمبر 2017
                                                                </span>
                                                                    <p>هناك حقيقة مثبتة منذ زمن طويل وهي أن المحتوى المقروء لصفحة ما سيلهي القارئ عن التركيز على الشكل الخارجي للنص أو شكل توضع الفقرات في الصفحة التي يقرأها. هناك </p>
                                                                </div>
                                                            </div>
                                                        </li>

                                                        <li>
                                                            <div class="user">
                                                                <div class="usr-img">
                                                                    <img src="../assets/images/105.jpg">
                                                                </div>
                                                                <div class="usr-data">
                                                                    <h3>
                                                                        <a href="#">امير ناجح</a>
                                                                    </h3>
                                                                    <span>
                                                                        <i class="zmdi zmdi-calendar"></i>
                                                                        11 ديسمبر 2017
                                                                </span>
                                                                    <p>هناك حقيقة مثبتة منذ زمن طويل وهي أن المحتوى المقروء لصفحة ما سيلهي القارئ عن التركيز على الشكل الخارجي للنص أو شكل توضع الفقرات في الصفحة التي يقرأها. هناك حقيقة مثبتة منذ زمن طويل وهي أن المحتوى المقروء لصفحة ما سيلهي القارئ عن التركيز عل</p>
                                                                </div>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="comment-form">
                                                    <form action="#" method="get">
                                                        <div class="form-group">
                                                            <label for="comment_ad">اكتب تعليقك</label>
                                                            <textarea class="form-control" id="comment_ad"></textarea>
                                                        </div>
                                                        <div class="form-group">
                                                            <button type="submit" class="btn">ارسال</button>
                                                           <%-- <button type="button" class="btn btn-upload" data-toggle="modal" data-target="#file_upload">
                                                                <i class="zmdi zmdi-cloud-upload"></i>إرفاق ملفات
                                                           
                                                            </button>--%>
                                                        </div>
                                                    </form>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="order-sidebar order col-md-4 col-xs-12">
                                    <%-- العمود الثانى وفيه بيانات صاحب الكورس والطلاب والمحاضرات ومساحته 4 --%>
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
                                                        <%--  صوره المدرب--%>
                                                        <img src="../assets/images/218.jpg" alt="">
                                                    </div>
                                                    <div class="usr-data">
                                                        <h3>
                                                            <%-- اسم المدرب وبيكون لينك لو ليه صفحه ع الموقع--%>
                                                            <a href="#">حمزة محمود  </a>
                                                        </h3>

                                                        <span>
                                                            <%--عنوان المدرب ان وجد--%>
                                                            <i class="zmdi zmdi-pin"></i>
                                                            السعودية , الرياض
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>




                                    


                                    <div class="inner">
                                        <div class="add-hint">
                                            <div class="hint-head side_head">
                                                <h3>
                                                    <i class="zmdi zmdi-storage zmdi-hc-lg"></i>
                                                     ملاحظات
                                                </h3>
                                            </div>
                                            <div class="hint-body">
                                                <p>
                                                    لا توجد ملاحظات                                                 </p>
                                               
                                            </div>
                                        </div>
                                    </div>

                                       <div class="inner">
                                        <div class="add-hint">
                                            <div class="hint-head side_head">
                                                <h3>
                                                    <i class="zmdi zmdi-storage zmdi-hc-lg"></i>
                                                     الماليات
                                                </h3>
                                            </div>
                                            <div class="hint-body">
                                                <p>
                                                    لا توجد ماليات الان

 
                                                </p>
                                                <button type="button" class="btn btn-purple btn-hint" data-toggle="modal" data-target="#add_Financial">اضافة مالية جديدة</button>
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
                                                <p>
                                                    لا توجد انشطة حاليه

 
                                                </p>
                                               
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            
            <%--<div class="modal fade" id="activity_edit" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">اضافة نشاط</h4>
                        </div>
                        <div class="modal-body">
                            <form action="#">
                                <div class="form-group">
                                    <label for="p_p">عنوان الشاط</label>
                                    <input type="text" class="form-control" id="p_p">
                                </div>
                                <div class="form-group">
                                    <label for="p_dd">محتوي النشاط</label>
                                    <textarea class="form-control"></textarea>
                                </div>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">اضافة</button>
                        </div>
                    </div>
                </div>
            </div>--%>

           <div class="modal fade" id="add_Financial" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">اضافة مالية</h4>
                        </div>
                        <div class="modal-body">
                            <form action="#">
                                <div class="form-group">
                                    <label for="p_p">عنوان المالية</label>
                                    <input type="text" class="form-control" id="p_p">
                                </div>
                                <div class="form-group">
                                    <label for="p_dd"> المبلغ</label>
                                    <label class="form-control"></label>
                                </div>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">اضافة</button>
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
                            <form action="#">
                                <div class="form-group">
                                    <label for="p_p"> اسم الاختبار</label>
                                    <input type="text" class="form-control" id="p_p">
                                </div>

                               
                                <div class="form-group">
                                    <label for="p_ee">تاريخ الاختبار </label>
                                    <input type="text" class="form-control" id="p_ee">
                                </div>
                                <button type="button" class="btn btn-upload" data-toggle="modal" data-target="#file_upload">
                                    <i class="zmdi zmdi-cloud-upload"></i>إرفاق ملفات
                                                           
                                </button>
                              
                            </form>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="assignment" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">الواجبات </h4>
                        </div>
                        <div class="modal-body">
                            <form action="#">
                                <div class="form-group">
                                    <label for="p_p"> اسم الواجب</label>
                                    <input type="text" class="form-control" id="p_p">
                                </div>

                               
                                <div class="form-group">
                                    <label for="p_ee">تاريخ الواجب </label>
                                    <input type="text" class="form-control" id="p_ee">
                                </div>
                                <button type="button" class="btn btn-upload" data-toggle="modal" data-target="#file_upload">
                                    <i class="zmdi zmdi-cloud-upload"></i>إرفاق ملفات
                                                           
                                </button>
                              
                            </form>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="degrees" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">درجات الطالب </h4>
                        </div>
                        <div class="modal-body">
                            <form action="#">
                                <div class="form-group">
                                    <label for="p_p">  درجة نصف العام</label>
                                    <input type="text" class="form-control" id="p_p">
                                </div>

                               
                                <div class="form-group">
                                    <label for="p_ee">الدرجة النهائية  </label>
                                    <input type="text" class="form-control" id="p_ee">
                                </div>
                             
                              
                            </form>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>
            <%--<div class="modal fade" id="add_note" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">ملاحظة  </h4>
                        </div>
                        <div class="modal-body">
                            <form action="#">
                                <div class="form-group">
                                    <label for="p_p">  ملاحظه  </label>
                                    <input type="textarea" class="form-control" id="p_p">
                                </div>

                            </form>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">حفظ </button>
                        </div>
                    </div>
                </div>
            </div>--%>

        </div>
    </main>

    <%-- end form--%>
</asp:Content>
