<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DiplomaCourses.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.DiplomaCourses" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>


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
                                <span>مواد الدبلومه </span>
                            </h3>
                        </div>
                        <div class="col-md-6" >
                            <input  id="txt_Search" onkeypress="searchCourses();" type="text" class="form-control" placeholder="بحث عن دورة" />
                        </div>
                        <div class="col-md-2">

                            <div class="btn-group pull-left">
                                <button type="button" class="btn btn-info " data-toggle="modal" data-target="#addCourse">اضافة مادة <i class="fa fa-plus"></i></button>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section>
            <div class="row">
                <label id="Lbldeploma_id" runat="server" ></label>
                <div class="col-md-12 col-sm-12 col-xs-12 " id="courses-list">

                </div>
            </div>
        </section>
        <div class="widget-navigation">
            <ul class="pagination">
                <li class="paginate_button previous"><a href="#">السابق</a></li>
                <li class="paginate_button active"><a href="#">1</a></li>
                <li class="paginate_button"><a href="#">2</a></li>
                <li class="paginate_button"><a href="#">3</a></li>
                <li class="paginate_button"><a href="#">4</a></li>
                <li class="paginate_button"><a href="#">5</a></li>
                <li class="paginate_button"><a href="#">6</a></li>
                <li class="paginate_button next" id="default-datatable_next"><a href="#">التالي</a></li>
            </ul>
        </div>

        <div class="modal" id="addCourse" tabindex="-1" role="dialog">
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
                                        <label >  درجات الامتحان      </label>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="final_exam_degrees" type="text" id="Text2"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    <br />
                                        </div>
                            </div>
                                                <div class="row form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label > درجات النشاط    </label>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="activity_degrees" type="text" id="Text1"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                   
                                        </div>
                            </div>
                                 </div>
                             <div class="col-md-6">
                                  <div class=" row form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label class="label-required">
                                     التاريخ   </label>
                                </div>

                                <div class="col-md-9 col-sm-12">

                                    <div class="fancy-form"   id="divdate1">
                                        <input dbColumn="created_at_hj"  type="hidden" id="date_hj" />
                                        <input dbColumn="created_at_m"  type="hidden" id="date_m" />
                                     <uc1:hijricalendar runat="server" id="HijriCalendar" />
                                    </div>
                                  
                                </div>
                            </div>


                                    <div class="row form-group">
                                 <div class="col-md-3 col-sm-12">
                                        <label > درجات النشاط    </label>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="activity_degrees" type="text" id="Text3"
                                                class="form-control" runat="server" clientidmode="Static" />
                                 
                                        </div>
                            </div>

                                
                               <%--second col--%>
                            <div class=" row form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label  class="label-required">  المدرب</label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <asp:DropDownList dbcolumn="trainer_id" required class="form-control" ClientIDMode="Static" ID="ddltrainer" runat="server">
                                    </asp:DropDownList>
                                    
                                </div>
                            </div>
                           
                            <div class=" row form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label for="Name" >هدف  المادة </label>

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
                        <button type="button" class="btn btn-primary"  onclick="saveCourse();">حفظ </button>

                    </div>
                      </div>
                </div>
            </div>
        </div>
 

    <%-- end form--%>
</asp:Content>

<%--select acd_diplome_subjects.id as 'AutoCodeHide' ,acd_diplome_subjects.subject_id ,acd_diplome_subjects.price ,acd_diplome_subjects.student_number , acd_diplome_subjects.subject_goal from acd_diplome_subjects--%>