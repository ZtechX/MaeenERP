<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Diplomas.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.Diplomas" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services> 
            <asp:ServiceReference Path="~/ASMX_WebServices/DiplomasCls.asmx" />
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
        /*.modal-dialog {
          
    position: absolute;
    width: 700px; height: 300px;
    left: 50%; top: 30%;
    margin: -155px 0 0 -300px;
    border: solid 2px #cccccc;
    background-color: #ffffff;
   
        }*/
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
                    <script src="../JS_Code/acadmies/Diplomas.js"></script>

                </div>

        <section>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 ">
                    <div class="white-block row">
                        <div class="col-md-4">
                            <h3>
                                <i class="fa fa-book"></i>
                                <span>الدبلومات </span>
                            </h3>
                        </div>
                        <div class="col-md-6" >
                            <input  id="txt_Search" onkeypress="searchCourses();" type="text" class="form-control" placeholder="بحث عن دبلومه" />
                        </div>
                        <div class="col-md-2">

                            <div class="btn-group pull-left">
                                <button type="button" class="btn btn-info " data-toggle="modal" data-target="#addCourse">اضافة دبلومه <i class="fa fa-plus"></i></button>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 " id="diplomas-list">

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
                        <h4 class="modal-title">اضافة دبلومه</h4>
                    </div>
                    <div class="modal-body">
                        <div id="divForm">
                             <div class="col-md-6"> <%--col first--%>
                            <div class=" form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label for="Name" class="label-required">عنوان الدبلومه </label>

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
                                        <label> سعر الدبلومه </label>
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
                                    <label for="Name" >تفاصيل الدورة </label>

                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="description" ClientIDMode="Static" ID="description" runat="server">
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
                        <button type="button" class="btn btn-primary"  onclick="saveCourse();">حفظ </button>

                      <%--  <button type="button" data-dismiss="modal" id="savepop" class="btn btn-primary">حفظ </button>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- end form--%>
</asp:Content>
