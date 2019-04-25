<%@ Page Language="vb" AutoEventWireup="false"   CodeBehind="course_register.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.course_register" EnableEventValidation="false" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>

<%--<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="ucmf" TagName="MultiPhotoUpload" %>--%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/course_registerCls.asmx" />
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
      
     

        @media screen and (min-width: 800px) {
            .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9 {
                float: right;
            }

            .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9 {
                float: right;
            }
        }

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

    </style>

    

    <%--  start form--%>

    <main id="app-main" class="app-main" style="margin-top:50px;">
        <div class="wrap">
             <div>

                    <script src="../JS_Code/acadmies/course_register.js"></script>
                 <script type="text/javascript">
                      function UploadComplete2(sender, args) {
                           debugger
                            var fileLength = args.get_length();
                          var fileType = args.get_contentType();
                          if (fileType.indexOf("image/") != -1) {
                              $('#img_' + section_id).css('background', 'url("images/' + args.get_fileName() + '")');
                          }
                           else if (fileType.indexOf("presentation") != -1  || fileType.indexOf("ms-powerpoint") !=-1 || fileType.indexOf(".pptx") != -1) {
                              $('#img_' + section_id).css('background', 'url("../images/powerPoint_img.png")');
                          }
                          else if (fileType.indexOf("sheet") != -1  || fileType.indexOf("vnd.ms-excel") !=-1 || fileType.indexOf(".xlsx") != -1) {
                              $('#img_' + section_id).css('background', 'url("../images/excel_img.png")');
                          }
                          else if (fileType.indexOf("document") != -1  || fileType.indexOf("msword") !=-1 || fileType.indexOf(".docx") != -1) {
                              $('#img_' + section_id).css('background', 'url("../images/word_img.png")');
                          }
                          else if (fileType.indexOf("application/pdf") != -1) {
                              $('#img_' + section_id).css('background', 'url("../images/pdf_img.png")');
                          }
                          var file_nm = args.get_fileName();
                          $('#lblSec_' + section_id).html("Acadmies_module/images/" + file_nm);
                          $('#lblSec_' + section_id).attr("name",file_nm.split('.').slice(0, -1).join('.'));
                           
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
            <section class="app-content">
                <div class="row">
                     <label style="display:none" id="Lblcourse_id" runat="server" ></label>
                     <label style="display:none" id="lblcode" runat="server" ></label>
                     <label style="display:none" id="Lblcondition_id" ></label>
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

                                <div class="btn-group pull-left" id="checkstudentregister" runat="server" >
                                      <button type="button" id="btnregister" class="btn btn-info " data-toggle="modal" data-target="#register_Course" onclick="drawcourseConditions();" > التسجيل فى الدورة <i class="fa fa-sign-in"></i></button>

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
                                                    <span class="price " >
                                                        <b id="course_price"></b>
                                                          <i class="fa fa-money"> </i>
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
            
          </div>

            <div class="modal fade" id="register_Course" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">طلب التقديم  </h4>
                        </div>
                        <div class="modal-body" style="padding-left:25px;padding-right:25px;height:400px;overflow-y:scroll;">
                            <div id="divformsignin" class="row">
                               <div class="table-responsive" id="allStudentlist">
                            <table class="table table-bordered table-hover"  id="newitem">
                               <thead>
                                <tr>
                                    <th>الشرط </th>
                                   
                                     <th>ارفاق الملف</th>
                                   
                                </tr>
                                 </thead>
                                <tbody  id="action_courseStudents">
                                       
                                </tbody>
                                    

                            </table>
                        </div>

                                    <div class="row form-group" style="margin-top:15px;margin-bottom:0px;">
                                        <div class="col-md-2 col-sm-12">
                                            <label class="label-required" for="Name">الطلب </label>

                                        </div>
                                        <div class="col-md-10 col-sm-12">
                                            <asp:TextBox SkinID="form-control" TextMode="multiline" placeholder="طلب التقديم"  required class="form-control" dbColumn="notes" ClientIDMode="Static" ID="studentRequest" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                  
                                    
                                

                            </div>
                        </div>
                        <div class="modal-footer">
                            
                            <button type="button"  class="btn btn-primary" onclick="sendRequest();">ارسال </button>
                        </div>
                    </div>
                </div>
            </div>
           
        <asp:AsyncFileUpload ID="fuPhoto1" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                                    OnClientUploadComplete="UploadComplete2" style="display:none;"
                                                    FailedValidation="False" />
    </main>

    <%-- end form--%>
</asp:Content>
