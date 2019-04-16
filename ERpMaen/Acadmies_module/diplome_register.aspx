<%@ Page Language="vb" AutoEventWireup="false"   CodeBehind="diplome_register.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.diplome_register" EnableEventValidation="false" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>

<%--<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="ucmf" TagName="MultiPhotoUpload" %>--%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/diplome_registerCls.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/MultiFileUploader.asmx" />
             <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
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
    //ng.ready(function () {
      
    //    var tp = new ng.TimePicker({
    //        input: 'startTime', // the input field id
    //        start: '12:00 am',  // what's the first available hour
    //        end: '11:00 pm',  // what's the last avaliable hour
    //        top_hour: 12,  // what's the top hour (in the clock face, 0 = midnight)
    //        name: 'startTime',
            
    //    });
         
        
    //});
</script>

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
        /*studentlist*/
      
     

        @media screen and (min-width: 800px) {
            .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9 {
                float: right;
            }

            .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9 {
                float: right;
            }
        }



    </style>

    

    <%--  start form--%>

    <main id="app-main" class="app-main" style="margin-top:50px;">
        <div class="wrap">
             <div>

                   <script type="text/javascript">
                      
                    </script>
                    <script src="../JS_Code/acadmies/diplome_register.js"></script>


                </div>
            <section class="app-content">
                <div class="row">
                     <label style="display:none" id="Lbldiplome_id" runat="server" ></label>
                         <label style="display:none" id="lblcode" runat="server" ></label>
                  <%--   <label style="display:none" id="LblLecture_id" ></label>
                      <label style="display:none" id="LblHomework_id" ></label>
                       <label style="display:none" id="LblExam_id" ></label>
                    <label style="display:none" id="LblAbsence_id" ></label>
                    <label style="display:none" id="lect_time" ></label>--%>
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

                                <div class="btn-group pull-left" id="checkstudentregister">
                                      <button type="button" id="btnregister" class="btn btn-info " data-toggle="modal" data-target="#register_Course" > التسجيل فى الدبلوم <i class="fa fa-sign-in"></i></button>

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

                                              <%--  <li>
                                                    <span id="course_date" class="fa fa-calendar-check-o">
                                                     
                                                        <i class="zmdi zmdi-calendar-note" ></i>
                                                     
                                                        
                                                </span>
                                                </li>--%>
                                             
                                                <li>
                                                    <span class="price " >
                                                        <b id="course_price"></b>
                                                          <i class="fa fa-money"> </i>
                                                </span>
                                                        

                                                </li>
                                               <%-- <li>
                                                    <span id="">
                                                        <i class="fa fa-clock-o"></i>
                                                     
                                                       <b id="course_duration"></b>
                                                        <b>يوم</b>
                                                     
                                                </span>
                                              </li>--%>
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
                                                <h3> شروط الدورة </h3>
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
                          <%--  <th>الاجراء</th>--%>
                            

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

                                                       <%-- <span>
                                                            
                                                            <i class="zmdi zmdi-pin"></i>
                                                            السعودية , الرياض
                                                        </span>--%>
                                                    </div>
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
            
          


            <div class="modal fade" id="register_Course" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">طلب التقديم  </h4>
                        </div>
                        <div class="modal-body">
                            <div id="divformsignin" class="row">
                                <%-- <div class="col-md-12">
                                        <uc1:Result runat="server"  ID ="Result2" />
                                    </div>--%>

                                    <div class="row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name">الطلب </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" TextMode="multiline" placeholder="طلب التقديم"  required class="form-control" dbColumn="notes" ClientIDMode="Static" ID="notes2" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                  <div class="form-group ">

                                       
                                        <%--     <label id="fileupload">  </label>--%>
                                            <input id="fileURL4" required type="hidden" dbcolumn="Image_path" runat="server" />
                                            <input id="FName4" type="text" readonly="readonly" runat="server" />

                                      
                                        <div class="clear">
                                        </div>
                                        <asp:AsyncFileUpload ID="fuFile3" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                            OnClientUploadComplete="UploadComplete4" />
                                       
                                    </div>
                                    
                                

                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button"  class="btn btn-primary" onclick="sendRequest();">ارسال </button>
                        </div>
                    </div>
                </div>
            </div>
           
           
    </main>

    <%-- end form--%>
</asp:Content>
