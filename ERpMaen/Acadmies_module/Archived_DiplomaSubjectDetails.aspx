﻿<%@ Page Language="vb" AutoEventWireup="false"   CodeBehind="Archived_DiplomaSubjectDetails.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.Archived_DiplomaSubjectDetails" EnableEventValidation="false" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/Archived_DiplomaSubjectDetailsCls.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/MultiFileUploader.asmx" />
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
        /*studentlist*/
      

        #newitem tbody td img {
            

              width:50px;
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
                    <script src="../JS_Code/acadmies/Archived_DiplomaSubjectDetails.js"></script>
                 <script src="../js/customCalender/CustomerCalendar.js"></script>

                </div>
            <section class="app-content">
                <div class="row">
                     <label style="display:none" id="Lblsubject_id" runat="server" ></label>
                       <label style="display:none" id="LblDiplome_id" runat="server"></label>
                        <label style="display:none" id="lbldiplomecode" runat="server"></label>
                     <label style="display:none" id="LblLecture_id" ></label>
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
                                  <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
                                <div class="btn-group pull-left">
  <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
    الخيارات
      <i class="fa fa-cogs"></i>
  </button>
  <ul class="dropdown-menu">
    
       <li><a data-toggle="modal" href="#publicStudentDegree" onclick="drawpublicDegreeTable();">
           درجات الطلاب
      </a></li>
       <li><a   onclick="unarchivecourse();">
           ازالة من الارشيف
      </a></li>
        
      

 
  </ul>
</div>
                               <%  End If    %>

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
                                                    <span class="price" id="subject_semster">
                                                      
                                                  
                                                </span>
                                                </li>
                                              <%--  <li>
                                                    <span class="price" id="course_stat">
                                                                                                            
                                                </span>
                                                </li>--%>

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
                                      <% if ERpMaen.LoginInfo.getUserType <> 8 Then   %>
                                    <div class="inner">
                                        <div class="order-desc row">
                                            <div class="desc-head order_wid col-md-12"">
                                                <div class=" pull-right">
                                                <i class="zmdi zmdi-file-text zmdi-hc-lg"></i>
                                                <h3>جدول الشروط </h3>
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

                                                                            <tbody id="studenthomeworktable">

                     
                                                                        <%--    <tr>
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
                                                                     

                                                                            </tr>--%>
                                                                            



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
                                                                            <tbody id="studentExamstable">
                                                                            <%--<tr>
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
                                                                     

                                                                            </tr>--%>
                                                                               
                                                                                </tbody>
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
                                  
                               <% End If %>

                                    <div class="inner">
                                        <div class="order-desc row">
                                            <div class="desc-head order_wid col-md-12"">
                                                <div class=" pull-right">
                                                <i class="zmdi zmdi-file-text zmdi-hc-lg"></i>
                                                <h3>ملفات الدورة  </h3>
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
                                        <div class="comments">
                                         
                                            <div class="comment-body">
                                               
                                                <div class="comment-users">
                                                   
                                                    <div id="divformComments">
                                                        
                                                <%--  //comments--%>
                                                                
                                                        </div>
                                                    
                                                </div>
                                                <div class="comment-form">
                                                    
                                                        <div class="form-group">
                                                            <label for="comment_ad"> التعليقات</label>
                                 <div id="newdivcomment">
                                                <asp:TextBox SkinID="form-control" TextMode="multiline" required class="form-control" dbColumn="comment" ClientIDMode="Static" ID="userComments" runat="server">
                                                </asp:TextBox>
                                     </div>
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
                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                 </section>
            
          

            

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
                        <%--  جدول الغياب--%>
                        <div class="table-responsive">
                            <table class="table table-bordered table-hover" id="publicDeg">
                                <tr>
                                    <th>الاسم </th>
                                    <th>الدرجة النهائية</th>
                                     <th> درجة نصف العام</th>



                                </tr>
                                   
                                <tbody id="pblcstudentdegrees">
                                 
                             
                                         </tbody>  


                            </table>
                        </div>


                        <div class="modal-footer">
                         
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
                           
                        </div>
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
                           
                        </div>
                    </div>
                </div>
            </div>
       
         

           
    </main>

    <%-- end form--%>
</asp:Content>
