<%@ Page Language="vb" AutoEventWireup="false" EnableEventValidation="false" CodeBehind="cases.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.cases" Theme="Theme5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/ImageSlider.ascx" TagPrefix="uc1" TagName="ImageSlider" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>
<%@ Register Src="~/UserControls/Person.ascx" TagPrefix="uc1" TagName="Person" %>
<%@ Register Src="~/UserControls/Appraisal.ascx" TagPrefix="uc1" TagName="Appraisal" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">

    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/cases.asmx" />
           <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/MultiFileUploader.asmx" />
        </Services>
    </asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="update-progress02">
                <asp:UpdateProgress ID="upg" runat="server" AssociatedUpdatePanelID="up">
                    <ProgressTemplate>
                        <asp:Image ID="imgProgress" runat="server" ImageUrl="~/App_Themes/Images/ajax-loader.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="wraper">
                <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
                <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>

                <script src="../js/cases/cases.js"></script>
                <script src="../js_code/cases/cases.js"></script>
                  <script src="../JS_Code/cases/cases_Upload.js"></script>
                <script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
                <script type="text/javascript" src="../Clock/ng_all.js"></script>
<script type="text/javascript" src="../Clock/ng_ui.js"></script>
<script type="text/javascript" src="../Clock/components/timepicker.js"></script>

<script type="text/javascript">
    ng.ready(function () {
      
        var tp = new ng.TimePicker({
            input: 'txtreceiving_time', // the input field id
            start: '12:00 am',  // what's the first available hour
            end: '11:00 pm',  // what's the last avaliable hour
            top_hour: 12,  // what's the top hour (in the clock face, 0 = midnight)
            name: 'txtreceiving_time',
            
        });
          var tp1 = new ng.TimePicker({
            input: 'txtdelivery_time',  // the input field id
            start: '12:00 am',  // what's the first available hour
            end: '11:00 pm',  // what's the last avaliable hour
              top_hour: 12,  // what's the top hour (in the clock face, 0 = midnight)
        });
            var tp2 = new ng.TimePicker({
            input: 'txtentry_time',  // the input field id
            start: '12:00 am',  // what's the first available hour
            end: '11:00 pm',  // what's the last avaliable hour
            top_hour: 12  // what's the top hour (in the clock face, 0 = midnight)
        });
        //   var tp3 = new ng.TimePicker({
        //    input: 'txtentry_time',  // the input field id
        //    start: '12:00 am',  // what's the first available hour
        //    end: '11:00 pm',  // what's the last avaliable hour
        //    top_hour: 12  // what's the top hour (in the clock face, 0 = midnight)
        //});
         var tp4= new ng.TimePicker({
            input: 'txtexite_time',  // the input field id
            start: '12:00 am',  // what's the first available hour
            end: '11:00 pm',  // what's the last avaliable hour
            top_hour: 12  // what's the top hour (in the clock face, 0 = midnight)
        });
        
    });
</script>


                <!------ Include the above in your HEAD tag ---------->

                <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
                <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

                <link href="../css/cases/cases.css" rel="stylesheet" />
                <style>
                   
                    .tbl_auto {
                        max-height: 200px;
                        overflow: auto;
                    }

                    .checkbox-all {
                        text-align: right !important;
                    }

                        .checkbox-all input[type=checkbox] {
                            margin: 0px 0px 0px 10px;
                        }
                </style>

                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="الحالات" SkinID="page_title"></asp:Label>
                    </div>
                        <div id="SavedivLoader" class="loader" style="display: none; text-align: center;">
                                            <asp:Image ID="img" runat="server" ImageUrl="../App_Themes/images/loader.gif" />
                                        </div>
                    <div class="strip_menu">
                        <asp:Panel ID="pnlOps" runat="server" Style="text-align: right">
                            <asp:Panel ID="pnlFunctions" runat="server" CssClass="row" Enabled="true">
                                <div class="col-md-9 col-sm-12">
                                    <ul>
                                        <li id="li1">
                                            <asp:LinkButton OnClientClick="add(); return false;" ID="cmdAdd" runat="server"
                                                SkinID="btn-top" CausesValidation="false">
                                     <i class="fa fa-plus"></i>
                                           جديد
                                            </asp:LinkButton>
                                        </li>
                                 
                                    </ul>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                        <uc1:PnlConfirm runat="server" ID="PnlConfirm" />
                    </div>
                    <uc1:Result runat="server" ID="Result" />
                    <%--compopox--%>
                      
                    
                        <div class="row">
                             <button class="btn btn-info btn-lg pull-left" onclick="GetMeasurementSatisfaction(); return false;"> <i class="fa fa-print"></i> نموذج قياس رضا المستفيد </button>
                              
                            <div class="ui-widget" dir="rtl">
                                <label>الحالات: </label>
                                <select  id="combobox">
                                    <option></option>

                                </select>
                            </div>
                        </div>
                    
                    <input type ="text"  style="display:none;" id="userLoginType" runat="server"/>
                    <div id="divForm" class="newformstyle form_continer">
                        <div class="container">


                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tab-container">
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 bhoechie-tab-menu">
                                        <div class="list-group" id="list_group">
                                            <a href="#" class="list-group-item active text-center">
                                                <h4 class="fa fa-user"></h4>
                                                <br />
                                                بيانات الحالة
                </a>
                                             <a href="#" id="case_0" style="display: none" class="list-group-item text-center">
                                                <h4 class="fa fa-list-alt"></h4>
                                                <br />
                                                الجدول الزمنى للحالة
                </a>

                                            <a href="#" id="case_1" style="display: none" class="list-group-item text-center">
                                                <h4 class="fa fa-child"></h4>
                                                <br />
                                                بيانات الاولاد
                </a>
                                            <a href="#" id="case_2" style="display: none" class="list-group-item text-center">
                                                <h4 class="fa fa-calendar"></h4>
                                                <br />
                                                تسليم واستلام الاولاد
                </a>

                                  
                                            <a href="#" id="case_5" style="display: none" class="list-group-item text-center">
                                                <h4 class="fa fa-user-circle-o"></h4>
                                                <br />
                                                محضر صلح خاص بالحالة
                </a>
                                            <a href="#" id="case_6" style="display: none" class="list-group-item text-center">
                                                <h4 class="fa fa-handshake-o"></h4>
                                                <br />
                                                تسليم واستلام النفقة
                </a>
   
                                        </div>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 bhoechie-tab" dir="rtl">
                                        <!-- بيانات الحالة -->
                                        <div id='case_active'  class="bhoechie-tab-content active">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <button class="btn btn-success btn-lg pull-left" onclick="save(); return false;"> حفظ بيانات الحالة</button>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="panel-group" id="accordion">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">بياناتا الحاله</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse1" class="panel-collapse collapse in">

                                                            <label id="lblcase_id" clientidmode="static" runat="server" style="display: none" dbcolumn=""></label>
                                                            <div class="panel-body" id="cases_info">
                                                                <asp:Label ID="lblmainid" runat="server" ClientIDMode="static" Style="display: none" dbcolumn="id"></asp:Label>
                                                                <%-- start group 1--%>
                                   <div id="caseReportDive" class="col-md-12 form-group" style="display:none;">
                                            <button class="btn btn-info btn-lg pull-right" onclick="getCaseReport(); return false;"> <i class="fa fa-print"></i> تقرير عن الحالة</button>
                                              
                                            <button class="btn btn-info btn-lg pull-left" onclick="getCaseDetails(); return false;"> <i class="fa fa-print"></i> بيانات الحالة الأولية</button>
                                           <button class="btn btn-info btn-lg" onclick="GetReferral(); return false;"> <i class="fa fa-print"></i> إحالة دعوى </button>
          
                                   </div>
                                                                <div class=" col-md-12 form-group ">

                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        رقم الحالة  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" required disabled dbcolumn="code" type="text" id="txtcode"
                                                                            class="form-control" runat="server" clientidmode="Static" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        التاريخ   </label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">

                                                                        <div class="fancy-form" id="divdate3">
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_m" ID="lblstart_date"></asp:Label>
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_h" ID="lblstart_date_hj"></asp:Label>
                                                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar2" />
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">وارد من</label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="court_id" required SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlcourt_id" runat="server">
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                </div>

                                                                <div class=" col-md-12 form-group ">

                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label >
                                                                        رقم الصك  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);"  dbcolumn="instrument_no" type="text" id="Text9"
                                                                            class="form-control" runat="server" clientidmode="Static" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label >
                                                                        تاريخ الصك  </label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">

                                                                        <div class="fancy-form" id="instrum_date">
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="instrument__date_m" ID="txtinstrument_date_m"></asp:Label>
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="instrument_date_h" ID="txtinstrument_date_h"></asp:Label>
                                                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar8" />
                                                                        </div>

                                                                    </div>
                                                                </div>
<%--                                                                                             <a class="btn btn-primary" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">add</a>--%>
                                                    
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">بيانات المنفذ</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse2" class="panel-collapse collapse">
                                                            <div class="panel-body" id="person_owner">
                                                                <input  dbcolumn="id" type="text" style="display:none;"/>

                                                                <%--stsrt group2--%>

                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">اسم المنفذ</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" required class="form-control" onchange="get_name();" dbColumn="name" ClientIDMode="Static" ID="txtname" runat="server">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label  class="label-required">صلة القرابة للمنفذ</label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="relationship_id" required class="form-control" SkinID="form-control" ClientIDMode="Static" ID="ddlrelationship_id" runat="server">
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        رقم هوية المنفذ  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input placeholder="رقم الهوية يتكون من 10 رقم"  onkeypress="return cust_chkNumber(event,this,10);"  required dbcolumn="indenty" type="text" id="txtindenty"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                                                       <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        رقم جوال المنفذ  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input placeholder="رقم الجوال يتكون من 10 رقم"  onkeypress="return cust_chkNumber(event,this,10);"  required dbcolumn="phone" type="text" id="tel"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label >
                                                                        رقم الوكالة  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" dbcolumn="authorization_no" type="text" id="txtauthorization_no"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                      <div style="display:none" class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        الرقم السرى </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);"  dbcolumn="User_Password" type="text" id="txtpassword"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>



                                                                <%--            end group2--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">بيانات المنفذ ضده</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse3" class="panel-collapse collapse">
                                                            <div class="panel-body" id="person_against">
                                                                <%-- start group3--%>
                                                                <fieldset>
                                                                    <legend>بيانات المنفذ ضده</legend>
                                                                    <input  dbcolumn="id" type="text" style="display:none;"/>
                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label for="Name" class="label-required">اسم المنفذ ضده</label>

                                                                        </div>
                                                                        <div class="col-md-9 col-sm-12">
                                                                            <asp:TextBox SkinID="form-control" required onchange="get_name();" class="form-control" dbColumn="name" ClientIDMode="Static" ID="txtname2" runat="server">
                                                                            </asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label  class="label-required">صلة القرابة للمنفذ ضده</label>
                                                                        </div>

                                                                        <div class="col-md-9 col-sm-12">
                                                                            <asp:DropDownList dbcolumn="relationship_id" required SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddrelationship_id2" runat="server">
                                                                            </asp:DropDownList>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label class="label-required">
                                                                            رقم هوية المنفذ ضدده  </lable>
                                                                        </div>

                                                                        <div class="col-md-9 col-sm-12">
                                                                            <input placeholder="رقم الهوية يتكون من 10 رقم" onkeypress="return cust_chkNumber(event,this,10);" required  dbcolumn="indenty" type="text" id="txtindenty2"
                                                                                class="form-control" runat="server" clientidmode="Static" />

                                                                            <br />
                                                                        </div>
                                                                    </div>
                                                                      <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        رقم جوال المنفذ ضدده  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input placeholder="رقم الجوال يتكون من 10 رقم" onkeypress="return cust_chkNumber(event,this,10);"  required dbcolumn="phone" type="text" id="tel1"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label >
                                                                            رقم الوكالة  </lable>
                                                                        </div>

                                                                        <div class="col-md-9 col-sm-12">
                                                                            <input onkeypress="return isNumber(event);" dbcolumn="authorization_no" type="text" id="txtauthorization_no2"
                                                                                class="form-control" runat="server" clientidmode="Static" />

                                                                            <br />
                                                                        </div>
                                                                    </div>

                                                                    <div style="display:none" class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label class="label-required">
                                                                            الرقم السرى </lable>
                                                                        </div>

                                                                        <div class="col-md-9 col-sm-12">
                                                                            <input onkeypress="return isNumber(event);"  dbcolumn="User_Password" type="text" id="txtpassword2"
                                                                                class="form-control" runat="server" clientidmode="Static" />

                                                                            <br />
                                                                        </div>
                                                                    </div>

                                                                    <fieldset />

                                                                    <%--end group3--%>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse4">بيانات الاطفال</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse4" class="panel-collapse collapse">
                                                            <div class="panel-body" id="get_children">
                                                                <%-- start group4--%>
                                                                <fieldset>
                                                                    <legend>الاطفال</legend>
                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label class="label-required">حضانة الاطفال</label>
                                                                        </div>

                                                                        <div class="col-md-9 col-sm-12">
                                                                            <asp:DropDownList dbcolumn="child_custody" required SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlchild_custody" runat="server">
                                                                            </asp:DropDownList>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label class="label-required">عدد الذكور</label>

                                                                        </div>

                                                                        <div class="col-md-9 col-sm-12">
                                                                            <input type="text" onchange="calc_total();" onkeypress="return isNumber(event);" required dbcolumn="boys_no" id="txtboys_no"
                                                                                class="form-control" />
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label class="label-required">عدد الاناث</label>

                                                                        </div>

                                                                        <div class="col-md-9 col-sm-12">
                                                                            <input type="text" onchange="calc_total();" onkeypress="return isNumber(event);" required dbcolumn="girls_no" id="txtgirls_no"
                                                                                class="form-control" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label >الاجمالى</label>

                                                                        </div>

                                                                        <div class="col-md-9 col-sm-12">
                                                                            <input type="text" id="txtchildrens_no" disabled dbcolumn="childrens_no" class="form-control" runat="server" clientidmode="Static" />

                                                                        </div>
                                                                    </div>

                                                                    <fieldset />

                                                                    <%--end group4--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse5">ييانات الحاله الحالية</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse5" class="panel-collapse collapse">
                                                            <div class="panel-body" id="get_status">
                                                                <%-- start group5--%>


                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label  class="label-required">الحالة الحالية</label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="status" required SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlstatus" runat="server">
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label  class="label-required">الخدمة المقدمة</label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="depart" class="form-control" required SkinID="form-control" ClientIDMode="Static" ID="ddldepart" runat="server">
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label>المستشار المسؤل عن الحالة</label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="advisor_id" SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlAdvisor" runat="server">
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                </div>

                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" >الحكم الصدار من المحكمة</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="court_details" ClientIDMode="Static" ID="txtcourt_details" runat="server">
                                                                        </asp:TextBox>


                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" >ملاحظات على الحالة</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="details" ClientIDMode="Static" ID="txtdetails" runat="server">
                                                                        </asp:TextBox>


                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">التبوبات</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12 tbl_auto">
                                                                        <table dir="rtl" class="table table-bordered">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>التبويب</th>
                                                                                    <th class="checkbox-all"><span>
                                                                                        <input type="checkbox" id="check_tab" onchange="mark_all(this,'tabs')">الكل</span></th>

                                                                                </tr>
                                                                            </thead>
                                                                            <tbody id="tabs">
                                                                                <tr>
                                                                                </tr>
                                                                            </tbody>


                                                                        </table>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group">
                                                                    <div class="col-md-3 col-sm-12">
                                                                    <label for="txtUploadedFiles">الملفات</label>
                                                                        </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <div class="fancy-file-upload">


                                                                            <asp:Button ID="cmdPOP" runat="server" CssClass="btn btn-primary btn-3d" Style="padding: 0 20px 0 20px;" Text="+" CausesValidation="False" />
                                                                            <asp:TextBox ID="txtUploadedFiles" CssClass="form-control" Style="display: inline-block; width: 40%;" ReadOnly="true" onclick="showUploadedFilesTable(this);" runat="server"
                                                                                ClientIDMode="Static"></asp:TextBox>


                                                                        </div>
                                                                        </div>
                                                                </div>





                                                                <%--end group5--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                           <!-- الجدول الزمنى للحالة -->
                                        <div   class="bhoechie-tab-content ">
                                           
                                            <div class="col-md-12">
                                                <div class="panel-group" id="accordion0">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion1" href="#collapse_1">الجدول الزمنى للحالة</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse_1" class="panel-collapse collapse in">

                                                            <div class="panel-body" >
                                                 
                                            <button class="btn btn-info btn-lg pull-right" onclick="getTimeline(true); return false;">أحداث تمت بالفعل</button>
                                               
                                            <button class="btn btn-info btn-lg pull-left" onclick="getTimeline(false); return false;">أحداث لم تحدث بعد</button>
                                    
                                                                <div  style="margin-top:70px;">
                                                                <label style="display:block;float:right;margin-bottom:20px;">خلال فترة محددة</label>
                                                                    <br />
                                                                   <div class="col-md-12 form-group ">
                                      <div class="col-md-6 form-group ">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">
                                            تاريخ بداية الفترة   </lable>
                                        </div>

                                        <div class="col-md-9 col-sm-12">

                                            <div class="fancy-form" id="divdate_start">
                                              <uc1:HijriCalendar runat="server" ID="HijriCalendar3" />
                                            </div>

                                        </div>
                                    </div>
                            <div class="col-md-6 form-group ">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">
                                            تاريخ نهاية الفترة   </lable>
                                        </div>

                                        <div class="col-md-9 col-sm-12">

                                            <div class="fancy-form" id="divdate_end">
                                              <uc1:HijriCalendar runat="server" ID="HijriCalendar1" />
                                            </div>

                                        </div>
                                    </div>
                                                                       </div>
                                             </div>
   <div class="col-md-6 form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">نوع الاحداث </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList class="form-control" ClientIDMode="Static" ID="ddldone" runat="server">
                                                <asp:ListItem Value="True|" Selected="True" Text="تمت بالفعل"></asp:ListItem>
                                                <asp:ListItem Value="False|" Text="لم تحدث بعد"></asp:ListItem>
                                                  <asp:ListItem Value="True|False" Text="جميع الاحداث"></asp:ListItem>
                                                </asp:DropDownList>
                                            <br />
                                        </div>
                                    </div>
                                                                 <div class="col-md-12 form-group ">
                                                                <button class="btn btn-info btn-lg " onclick="getTimelinePeriod(); return false;" >الجدول الزمنى خلال الفترة</button>
                                               </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                  
                                                </div>
                                            </div>
                                        </div>

                                        <!-- بيانات الاولاد -->
                                        <div class="bhoechie-tab-content">


                                            <%-- start--%>

                                            <div class="col-md-12">
                                                <div class="panel-group" id="accordion2">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse6">بيانات الاولاد</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse6" class="panel-collapse collapse in">
                                                            <div class="panel-body" id="children_info">

                                                                <asp:Label runat="server" ClientIDMode="static"  ID="lblchild" Style="display: none" dbcolumn="id"></asp:Label>


                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">الأسم</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" required class="form-control" dbColumn="name" ClientIDMode="Static" ID="txtname_child" runat="server">
                                                                        </asp:TextBox>

                                                                    </div>
                                                                </div>

                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label>النوع </label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="gender" SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlGender" runat="server">
                                                                            <asp:ListItem Value="1">ذكر</asp:ListItem>
                                                                            <asp:ListItem Value="2">انثى</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        العمر  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" required dbcolumn="age" type="text" id="age_son"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label>الحالة الصحية </label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList SkinID="form-control" dbcolumn="health_status" class="form-control" ClientIDMode="Static" ID="ddlhealth_status" runat="server">
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                </div>
                                                                <%-- <div class="col-md-12 form-group ">
                                                                           <label for="ddlLanguage"  class="col-md-3 col-sm-12 control-label no-padding-right">صورة شخصية </label>
                                                                        <div class="col-sm-8">
                                                                        <div class="photo-upload-box">
                                                                                        <asp:AsyncFileUpload ID="fuPhoto1" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                                                                            OnClientUploadComplete="UploadComplete2" OnClientUploadStarted="UploadStarted2"
                                                                                            FailedValidation="False" />
                                                                                    </div>
                                                                        </div>
                                                                    </div>--%>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" >ملاحظات المشرف</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control"  TextMode="multiline" class="form-control" dbColumn="details" ClientIDMode="Static" ID="txtdetails_child" runat="server">
                                                                        </asp:TextBox>


                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group">
                                                                    <button onclick="save_children();" class="btn btn-success">حفظ</button>
                                                                </div>
                                                                <div class="col-md-12 form-group">
                                                                    <table dir="rtl" class="table table-bordered">
                                                                        <thead>
                                                                            <tr>

                                                                                <th>الاسم</th>
                                                                                <th>العمر</th>
                                                                                <th>حذف/مشاهدة</th>

                                                                            </tr>
                                                                        </thead>
                                                                        <tbody id="children">
                                                                            <tr>
                                                                            </tr>
                                                                        </tbody>


                                                                    </table>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <%-- end--%>
                                        </div>


                                        <!-- تسليم واستلام الاولاد -->
                                        <div class="bhoechie-tab-content">

                                            <div class="col-md-12">
                                                <div class="panel-group" id="accordion3">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse7">تسليم واستلام الاولاد (الفترة الاساسية) </a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse7" class="panel-collapse collapse in">
                                                            <div class="panel-body" id="children_receive">
                                                                <%-- start group 1--%>

                                                                <asp:Label ID="lblchild_recive_id" ClientIDMode="static" runat="server" Style="display: none" dbcolumn="id"></asp:Label>

                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">(يوم)التسليم كل</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control"  required onkeypress="return isNumber(event);" class="form-control" dbColumn="delivery_period" ClientIDMode="Static" ID="txtdelivery_period" runat="server">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        تاريخ اول تسليم    </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">

                                                                        <div class="fancy-form" id="divdate_received">
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="first_date_m" ID="lblfirst_date_received_m"></asp:Label>
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="first_date_h" ID="lblfirst_date_received_h"></asp:Label>
                                                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar12" />
                                                                        </div>
                                                                       
                                                                    </div>
                                                                </div>

                                                                 <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                       لمدة (شهر) </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" required  dbColumn="month_number" type="text" id="num_months"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group" id="time_receiving_time" style="margin-bottom:15px;">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        ساعة التسليم  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" required dbcolumn="receiving_time" type="time" id="txtreceiving_time"
                                                                            class="form-control" runat="server" readonly clientidmode="Static" />
        
                                                                       
                                                                    </div>
                                                                </div>
                                                              
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        ساعة الاستلام </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12" id="time_delivery_time">
                                                                        <input onkeypress="return isNumber(event);" required dbcolumn="delivery_time" readonly type="time" id="txtdelivery_time"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                                  <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">(يوم)الإرجاع بعد</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" dbcolumn="back_period" required onkeypress="return isNumber(event);" class="form-control"  ClientIDMode="Static" ID="txtreturn_period" runat="server">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group">
                                                               <button onclick="save_children_receive(); return false" class="btn btn-success">حفظ</button>
                                                                    
                                                                </div>


                                                                <%--end of group1--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="panel-group" id="">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse8">بيانات الفترات الاضافية </a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse8" class="panel-collapse collapse in">
                                                            <div class="panel-body" >
                                                                <%-- start group 1--%>
                                                                       <button id="addAnother_receive" onclick="addAnother_children_receive(); return false" class="btn btn-success">إضافة مدة آخرى</button>
                                                              
<table class="table table-bordered">
    <thead>
       <tr>
            <th >#</th>
            <th >تاريخ بداية الفترة</th>
            <th >مدة التسليم كل (يوم)</th>
            <th >لمدة (شهر)</th>
           <th >حذف الفترة ومواعيدها</th>
        </tr>
    </thead>
    <tbody id="tb_other_period">
        
    </tbody>
</table>

                                                                <%--end of group1--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>


                                        <!--   محضر صلح خاص بالحالة -->
                                        <div class="bhoechie-tab-content">


                                            <div class="col-md-12">

                                                <div class="panel-group" id="accordion8">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse12">بيانات محضر الصلح الخاص بالجلسة   </a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse12" class="panel-collapse collapse in">
                                                            <div class="panel-body" id="case_conciliation">
                                                                <%-- start group 1--%>
                                                                <asp:Label ID="lblconciliation_id" ClientIDMode="static" runat="server" Style="display: none" dbcolumn="id"></asp:Label>
                                                                <div class="col-sm-12 form-group">
    <button class="btn btn-info btn-lg pull-right" onclick="getRegistAggregation(); return false;"><i class="fa fa-print"></i> توثيق إتفاق </button>
                                                                
                                                                    <button class="btn btn-info btn-lg pull-left" onclick="getConciliation(); return false;"><i class="fa fa-print"></i> محضر الصلح </button>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label>
                                                                        رقم الصلح  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" disabled dbcolumn="code" type="text" id="txtcode_conciliation"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        تاريخ الصلح  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">

                                                                        <div class="fancy-form" id="Date_reconciliation">
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_m" ID="lblconciliation_date_m"></asp:Label>
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_h" ID="lblconciliation_date_h"></asp:Label>
                                                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar5" />
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label>الطرف الاول  </label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="owner_id" SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlowner_id" runat="server">
                                                                        </asp:DropDownList>
                                                                        <button class="btn btn-primary" type="button" onclick="find_persons('ddlowner_id',1)">اضافة طرف اول </button>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label>الطرف الثانى  </label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="second_party_id" SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlsecond_party_id" runat="server">
                                                                        </asp:DropDownList>
                                                                        <button class="btn btn-primary" type="button" onclick="find_persons('ddlsecond_party_id',1)">اضافة طرف ثانى </button>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <br />
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">بنود الاتفاق </label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" required TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="txtnotes" runat="server">
                                                                        </asp:TextBox>


                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label>الموظف المسؤل  </label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="employee_id" SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlemployee_id3" runat="server">
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group">
                                                                    <button onclick="save_conciliation(); return false" class="btn btn-success">حفظ</button>
                                                                </div>


                                                                <%--end of group1--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>



                                        </div>
                                        <!-- تسليم واستلام النفقة -->
                                        <div class="bhoechie-tab-content">
                                            <div class="col-md-12">

                                                <div class="panel-group" id="accordion9">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse13">بيانات تسليم واستلام النفقة   </a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse13" class="panel-collapse collapse in">
                                                            <div class="panel-body" id="expense_basic">
                                                                <asp:Label ID="lblexpense_basic" ClientIDMode="static" runat="server" Style="display: none" dbcolumn="id"></asp:Label>
                                                                <%-- start group 1--%>

                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">(يوم)التسليم كل</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" required onkeypress="return isNumber(event);" class="form-control" dbColumn="delivery_period" ClientIDMode="Static" ID="txtdelivery_period_expenses" runat="server">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        تاريخ اول تسليم  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">

                                                                        <div class="fancy-form" id="date_expenses_basic">
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="first_date_m" ID="lbldate_m_expenses_basic"></asp:Label>
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="first_date_h" ID="lbldate_h_expenses_basic"></asp:Label>
                                                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar10" />
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                                <div class=" col-md-12 form-group ">

                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        المبلغ    </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" required dbcolumn="amount" type="text" id="expense_amount"
                                                                            class="form-control" runat="server" clientidmode="Static" />
                                                                    </div>
                                                                </div>

                                                                 <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                       لمدة (شهر) </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" required  dbColumn="month_number" type="text" id="expense_month"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                                                               
                                                                <div class="col-md-12 form-group">
                                                                    <button onclick="save_expense_basic();" class="btn btn-success">حفظ</button>
                                                                </div>


                                                                <%--end of group1--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>
                                            
                                            <div class="col-md-12">
                                                <div class="panel-group" id="">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse11">بيانات الفترات الاضافية </a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse11" class="panel-collapse collapse in">
                                                            <div class="panel-body" >
                                                                <%-- start group 1--%>
                                                                       <button id="addAnother_receiveExpense" onclick="addAnother_expense_receive(); return false" class="btn btn-success">إضافة مدة آخرى</button>
                                                              
<table class="table table-bordered">
    <thead>
       <tr>
            <th >#</th>
            <th >تاريخ بداية الفترة</th>
            <th >مدة التسليم كل (يوم)</th>
            <th >لمدة (شهر)</th>
           <th >حذف الفترة ومواعيدها</th>
        </tr>
    </thead>
    <tbody id="tb_other_period_expense">
        
    </tbody>
</table>

                                                                <%--end of group1--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
   

                                    </div>
                                    <!-- Modal -->



                                </div>
                            </div>


                        </div>
                        </button>
                                              <uc1:ImageSlider runat="server" ID="ImageSlider" />
                     <uc1:MultiPhotoUpload runat="server" ID="MultiPhotoUpload" />
                        <uc1:Person runat="server" ID="Person" />
                           <uc1:Appraisal runat="server" ID="Appraisal1" />
                        <%--  <uc1:ImageSlider runat="server" ID="ImageSlider" />
                    <uc1:MultiPhotoUpload runat="server" ID="MultiPhotoUpload" />
                    <uc1:DynamicTable runat="server" ID="DynamicTable" />
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />--%>
                    </div>
                </div>
        
           <div class="collapse" id="anotherPeriod" dir="rtl" style="display :none;">

               
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">(يوم)التسليم كل</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control"  required onkeypress="return isNumber(event);" class="form-control"  ClientIDMode="Static" ID="delivery_period" runat="server">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                         

                                                                 <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                       لمدة (شهر) </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" required  type="text" id="month_number"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">(يوم)الإرجاع بعد</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" dbcolumn="back_period" required onkeypress="return isNumber(event);" class="form-control"  ClientIDMode="Static" ID="another_return_period" runat="server">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>

                  <div class="col-md-12 form-group ">
                           <button onclick="save_anotherPeriod()" style="font-family: DroidKufi !important;" class="btn btn-success pull-left">حفظ</button>
        
                  </div>             

                      </div>

                  <div class="collapse" id="anotherPeriod_expense" dir="rtl" style="display :none;">

               
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">(يوم)التسليم كل</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control"  required onkeypress="return isNumber(event);" class="form-control"  ClientIDMode="Static" ID="TextBox1" runat="server">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                         

                                                                 <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                       لمدة (شهر) </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" required  type="text" id="Text1"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">(يوم)الإرجاع بعد</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" dbcolumn="back_period" required onkeypress="return isNumber(event);" class="form-control"  ClientIDMode="Static" ID="TextBox2" runat="server">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>

                  <div class="col-md-12 form-group ">
                           <button onclick="save_anotherPeriod()" style="font-family: DroidKufi !important;" class="btn btn-success pull-left">حفظ</button>
        
                  </div>             

                      </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
