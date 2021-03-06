﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="cases.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.cases" Theme="Theme5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/ImageSlider.ascx" TagPrefix="uc1" TagName="ImageSlider" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>
<%@ Register Src="~/UserControls/Person.ascx" TagPrefix="uc1" TagName="Person" %>


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
                        <asp:Image ID="imgProgress" runat="server" ImageUrl="~/Images/ajax-loader.gif" />
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
            top_hour: 12  // what's the top hour (in the clock face, 0 = midnight)
        });
          var tp1 = new ng.TimePicker({
            input: 'txtdelivery_time',  // the input field id
            start: '12:00 am',  // what's the first available hour
            end: '11:00 pm',  // what's the last avaliable hour
            top_hour: 12  // what's the top hour (in the clock face, 0 = midnight)
        });
               var tp2 = new ng.TimePicker({
            input: 'txtNafqatime',  // the input field id
            start: '12:00 am',  // what's the first available hour
            end: '11:00 pm',  // what's the last avaliable hour
            top_hour: 12  // what's the top hour (in the clock face, 0 = midnight)
        });
           var tp3 = new ng.TimePicker({
            input: 'txtentry_time',  // the input field id
            start: '12:00 am',  // what's the first available hour
            end: '11:00 pm',  // what's the last avaliable hour
            top_hour: 12  // what's the top hour (in the clock face, 0 = midnight)
        });
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
                    <div class="strip_menu">
                        <asp:Panel ID="pnlOps" runat="server" Style="text-align: right">
                            <asp:Panel ID="pnlFunctions" runat="server" CssClass="row" Enabled="true">
                                <div class="col-md-9 col-sm-12">
                                    <ul>
                                        <li>
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

                    <div class="container">
                        <div class="row">
                            <div class="ui-widget" dir="rtl">
                                <label>الحالات: </label>
                                <select  id="combobox">
                                    <option></option>

                                </select>
                            </div>
                        </div>
                    </div>

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
                                            <a href="#" id="case_3" style="display: none" class="list-group-item text-center">
                                                <h4 class="fa fa-clock-o"></h4>
                                                <br />
                                                تواريخ التسليم والاستلام
                </a>
                                            <a href="#" id="case_4" style="display: none" class="list-group-item text-center">
                                                <h4 class="fa fa-history"></h4>
                                                <br />
                                                جلسات التهيئة والتدرج
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
                                            <a href="#" id="case_7" style="display: none" class="list-group-item text-center">
                                                <h4 class="fa fa-clock-o"></h4>
                                                <br />
                                                تواريخ استلام النفقة
                                            </a>
                                            <a href="#" id="case_8" style="display: none" class="list-group-item text-center">
                                                <h4 class="fa fa-tasks"></h4>
                                                <br />
                                                اجرائات العضو المباشر للحالة
                </a>
                                        </div>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9 bhoechie-tab" dir="rtl">
                                        <!-- بيانات الحالة -->
                                        <div class="bhoechie-tab-content active">
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
                                                                        التاريخ   </lable>
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
                                                                        <label>وارد من</label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="court_id" required SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlcourt_id" runat="server">
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                </div>

                                                                <div class=" col-md-12 form-group ">

                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        رقم الصك  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" required dbcolumn="instrument_no" type="text" id="Text9"
                                                                            class="form-control" runat="server" clientidmode="Static" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        تاريخ الصك  </lable>
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

                                                                <%--stsrt group2--%>

                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">المنفذ</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" required class="form-control" onchange="get_name(1);" dbColumn="name" ClientIDMode="Static" ID="txtname" runat="server">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label>صلة القرابة للمنفذ</label>
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
                                                                        <input onkeypress="return isNumber(event);" required dbcolumn="indenty" type="text" id="txtindenty"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        رقم الوكالة  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" dbcolumn="authorization_no" type="text" id="txtauthorization_no"
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
                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label for="Name" class="label-required">المنفذ</label>

                                                                        </div>
                                                                        <div class="col-md-9 col-sm-12">
                                                                            <asp:TextBox SkinID="form-control" required onchange="get_name(2);" class="form-control" dbColumn="name" ClientIDMode="Static" ID="txtname2" runat="server">
                                                                            </asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label>صلة القرابة للمنفذ ضده</label>
                                                                        </div>

                                                                        <div class="col-md-9 col-sm-12">
                                                                            <asp:DropDownList dbcolumn="relationship_id" required SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddrelationship_id2" runat="server">
                                                                            </asp:DropDownList>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label class="label-required">
                                                                            رقم هوية المنفذ  </lable>
                                                                        </div>

                                                                        <div class="col-md-9 col-sm-12">
                                                                            <input onkeypress="return isNumber(event);" required  dbcolumn="indenty" type="text" id="txtindenty2"
                                                                                class="form-control" runat="server" clientidmode="Static" />

                                                                            <br />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label class="label-required">
                                                                            رقم الوكالة  </lable>
                                                                        </div>

                                                                        <div class="col-md-9 col-sm-12">
                                                                            <input onkeypress="return isNumber(event);" dbcolumn="authorization_no" type="text" id="txtauthorization_no2"
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
                                                                            <label>حضانة الاطفال</label>
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
                                                                            <input type="text" onchange="calc_total();" required dbcolumn="boys_no" id="txtboys_no"
                                                                                class="form-control" />
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label class="label-required">عدد الاناث</label>

                                                                        </div>

                                                                        <div class="col-md-9 col-sm-12">
                                                                            <input type="text" onchange="calc_total();" required dbcolumn="girls_no" id="txtgirls_no"
                                                                                class="form-control" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label class="label-required">الاجمالى</label>

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
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse5">يانات الحاله الحالية</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse5" class="panel-collapse collapse">
                                                            <div class="panel-body" id="get_status">
                                                                <%-- start group5--%>


                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label>الحالة الحالية</label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="status" required SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlstatus" runat="server">
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label>الخدمة المقدمة</label>
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
                                                                        <label for="Name" class="label-required">الحكم الصدار من المحكمة</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="court_details" ClientIDMode="Static" ID="txtcourt_details" runat="server">
                                                                        </asp:TextBox>


                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">ملاحظات على الحالة</label>

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
                                                                        <label for="Name" class="label-required">اسم الابن</label>

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
                                                                        <label for="Name" class="label-required">ملاحظات المشرف</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="details" ClientIDMode="Static" ID="txtdetails_child" runat="server">
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
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse7">تسليم واستلام الاولاد </a>
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
                                                                        <asp:TextBox SkinID="form-control" required onkeypress="return isNumber(event);" class="form-control" dbColumn="delivery_period" ClientIDMode="Static" ID="txtdelivery_period" runat="server">
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
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="first_date_received_m" ID="lblfirst_date_received_m"></asp:Label>
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="first_date_received_h" ID="lblfirst_date_received_h"></asp:Label>
                                                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar1" />
                                                                        </div>

                                                                    </div>
                                                                </div>


                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">يوم التسليم </label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList SkinID="form-control"  class="form-control" dbColumn="day_nam" ClientIDMode="Static" ID="ddlday_nam1" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        ساعة التسليم  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" dbcolumn="receiving_time" type="time" id="txtreceiving_time"
                                                                            class="form-control" runat="server" clientidmode="Static" />
        
                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        وقت الجلسة </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" required dbcolumn="session_time" type="text" id="txtsession_time"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        ساعة الاستلام </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" dbcolumn="delivery_time" type="time" id="txtdelivery_time"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
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


                                        </div>
                                        <!-- تواريخ التسليم والاستلام -->
                                        <div class="bhoechie-tab-content">
                                            <%-- start--%>
                                            <div class="col-md-12" id="receiving_delivery_details">

                                                <div class="panel-group" id="accordion4">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse8">بيانات التسليم والاستلام   </a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse8" class="panel-collapse collapse in">
                                                            <div class="panel-body">
                                                                <%-- start group 1--%>

                                                                <asp:Label ID="lbldelivery_details" runat="server" Style="display: none" dbcolumn="id"></asp:Label>
                                                                <label id="lbldelivery_date" runat="server" style="display: none"></label>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        التاريخ    </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">

                                                                        <div class="fancy-form" id="divdate_delivery">
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_m" ID="lbldate_m"></asp:Label>
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_h" ID="lbldate_h"></asp:Label>
                                                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar3" />
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">بيانات المسلم </label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="deliverer_id" required SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddldeliverer_id" runat="server">
                                                                            <asp:ListItem Value="0">اختر</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                         <button class="btn btn-primary" type="button" onclick="find_persons('ddldeliverer_id',1)">اضافة مسلم </button>
                                                                    </div>
                                                                </div>
                                                                              <uc1:Person runat="server" ID="Person" />
                                                            
                                                                <%--end of group1--%>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">بيانات المستلم </label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="reciever_id" required SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlreciever_id" runat="server">
                                                                            <asp:ListItem Value="0">اختر</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                              <button class="btn btn-primary" type="button" onclick="find_persons('ddlreciever_id',1)">اضافة مستلم </button>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">بيانات الاولاد </label>
                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12  tbl_auto">
                                                                        <table dir="rtl" class="table table-borderd">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>#</th>
                                                                                    <th>اسم الولد</th>
                                                                                    <th class="checkbox-all"><span>
                                                                                        <input type="checkbox" id="check_child" onchange="mark_all(this,'tab_children')">الكل</span></th>
                                                                                </tr>

                                                                            </thead>
                                                                            <tbody id="tab_children">
                                                                            </tbody>

                                                                        </table>
                                                                    </div>



                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">الموظف المسؤل</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList SkinID="form-control" class="form-control" dbColumn="employee_id" ClientIDMode="Static" ID="ddlemployee_id" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>



                                                                </div>
                                                                <%--end of group1--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-group" id="accordion5">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse9">التاكد من التسليم والاستلام   </a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse9" class="panel-collapse collapse in">
                                                            <div class="panel-body">
                                                                <%-- start group 1--%>

                                                                <div class="col-md-9 col-sm-12">
                                                                    <asp:LinkButton OnClientClick="state_setting(); return false;" ID="LinkButton1" runat="server"
                                                                        SkinID="btn-top" CausesValidation="false">
                                                                  <i class="fa fa-print"></i>
                                                                   طباعة محضر الاستلام
                                                                    </asp:LinkButton>
                                                                </div>

                                                                <div class="col-md-9 col-sm-12" id="receiving_done">

                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="email">تم الاستلام والتسليم </label>
                                                                    </div>
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <input dbcolumn="receiving_delivery_done" type="checkbox" id="doneId" style="width: 40px;" class="form-control" />
                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-9 col-sm-12">
                                                                    <br />
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="email">تمت الموافقة بالجوال   </label>
                                                                    </div>
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <input dbcolumn="moble_app_accept" type="checkbox" id="txtmoble_app_accept" style="width: 40px;" class="form-control" />

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <br />
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">ملاحظات </label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="TextBox20" runat="server">
                                                                        </asp:TextBox>


                                                                    </div>
                                                                </div>

           

                                                                <div class="col-md-9 col-sm-12" id="receiving_delay">
                                                                    <br />
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="email">تاجيل   </label>
                                                                    </div>
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <input dbcolumn="Delay" type="checkbox" onclick="get_receiving(2)" id="Delay" style="width: 40px;" class="form-control" />
                                                                    </div>
                                                                </div>
                                                                <div id="delay_reason" style="display: none">
                                                                    <div class="col-md-12 form-group ">
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label class="label-required">
                                                                            التاريخ    </lable>
                                                                        </div>
                                                                        <div class="col-md-9 col-sm-12">

                                                                            <div class="fancy-form" id="div_new_date">
                                                                                <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="new_date_m" ID="lblnew_date_m"></asp:Label>
                                                                                <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="new_date_h" ID="lblnew_date_h"></asp:Label>
                                                                                <uc1:HijriCalendar runat="server" ID="HijriCalendar6" />
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12 form-group ">
                                                                        <br />
                                                                        <div class="col-md-3 col-sm-12">
                                                                            <label for="Name" class="label-required">السبب </label>

                                                                        </div>
                                                                        <div class="col-md-9 col-sm-12">
                                                                            <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="delay_reason" ClientIDMode="Static" ID="txtdelay_reason" runat="server">
                                                                            </asp:TextBox>


                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="col-md-12 form-group" id="save_delivery_details" style="display: none">
                                                                    <button onclick="save_delivery_details();" class="btn btn-success">حفظ</button>
                                                                </div>

                                                                <div class="col-md-12 form-group">
                                                                    <button onclick="get_archive(1); return false;" class="btn btn-success">ارشفة</button>
                                                                    <button onclick="get_archive(2); return false;" class="btn btn-success">جديد</button>
                                                                </div>

                                                                <div class="col-md-12 form-group">
                                                                    <table id="table_delivery_data" dir="rtl" class="table table-bordered">
                                                                        <thead>
                                                                            <tr>

                                                                                <th>التاريخ</th>
                                                                                <th>الحالة</th>

                                                                            </tr>
                                                                        </thead>
                                                                        <tbody id="delivery_details_archive" style="display: none">
                                                                            <tr>
                                                                            </tr>
                                                                        </tbody>

                                                                        <tbody id="delivery_details">
                                                                            <tr>
                                                                            </tr>
                                                                        </tbody>


                                                                    </table>
                                                                </div>


                                                                <%--end of group1--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>


                                        </div>

                                        <!--  جلسات التهيئة والتدرج -->
                                        <div class="bhoechie-tab-content">
                                            <%--START--%>
                                            <div class="col-md-12" id="case_sessions">

                                                <div class="panel-group" id="accordion6">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse10">بيانات جلسات التهيئه   </a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse10" class="panel-collapse collapse in">
                                                            <div class="panel-body">

                                                                <asp:Label ID="lbl_sessions_id" ClientIDMode="static" runat="server" Style="display: none" dbcolumn="id"></asp:Label>

                                                                <%-- start group 1--%>

                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label>
                                                                        رقم الجلسة  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" disabled  dbcolumn="code" type="text" id="txtcode_sessions"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">طالب التنفيذ</label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="owner_id" SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlowner_id_sessions" runat="server">
                                                                        </asp:DropDownList>
                                                                        <button class="btn btn-primary" type="button" onclick="find_persons('ddlowner_id_sessions',1)">اضافة منفذ </button>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">المنفذ ضده </label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="second_party_id" SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlsecond_party_id_sessions" runat="server">
                                                                        </asp:DropDownList>
                                                                         <button class="btn btn-primary" type="button" onclick="find_persons('ddlsecond_party_id_sessions',1)">اضافة منفذ ضده </button>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">الاطفال المقيدين </label>
                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12 tbl_auto">
                                                                        <table dir="rtl" class="table table-bordered ">
                                                                            <thead>
                                                                                <tr>

                                                                                    <th>#</th>
                                                                                    <th>الاسم</th>
                                                                                    <th class="checkbox-all"><span>
                                                                                        <input type="checkbox" id="check_child1" onchange="mark_all(this,'children_sessions')">الكل</span></th>

                                                                                </tr>
                                                                            </thead>
                                                                            <tbody id="children_sessions">
                                                                                <tr>
                                                                                </tr>
                                                                            </tbody>


                                                                        </table>
                                                                    </div>

                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">مكان الجلسة</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" class="form-control" dbColumn="place" ClientIDMode="Static" ID="txtplace" runat="server">
                                                                        </asp:TextBox>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        تاريخ الجلسة    </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">

                                                                        <div class="fancy-form" id="divdate_sessions">
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_m" ID="lbldate_m_sessions"></asp:Label>
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_h" ID="lbldate_h_sessions"></asp:Label>
                                                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar9" />
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        وقت الدخول </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" dbcolumn="entry_time" type="time" id="txtentry_time"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        وقت الخروج </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" dbcolumn="exite_time" type="time" id="txtexite_time"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>


                                                                <%--end of group1--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-group" id="accordion7">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse11">بيانات نتيجة الجلسة   </a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse11" class="panel-collapse collapse in">
                                                            <div class="panel-body">
                                                                <%-- start group 1--%>

                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">الموظف المختص </label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="employee_id" SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlemployee_id2" runat="server">
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">نتيجة الجلسة</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" class="form-control" dbColumn="result" ClientIDMode="Static" ID="txtresult" runat="server">
                                                                        </asp:TextBox>

                                                                    </div>
                                                                </div>
                                               

                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">المرافقيين</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12 tbl_auto">
                                                                        <table dir="rtl" class="table table-bordered">

                                                                            <thead>
                                                                                <tr>

                                                                                    <th>#</th>
                                                                                    <th>الاسم</th>
                                                                                    <th class="checkbox-all"><span>
                                                                                        <input type="checkbox" id="check_child2" onchange="mark_all(this,'persons_sessions')">الكل</span></th>

                                                                                </tr>
                                                                            </thead>
                                                                            <tbody id="persons_sessions">
                                                                                <tr>
                                                                                </tr>
                                                                            </tbody>


                                                                        </table>
                                                                    </div>
                                                                            <button class="btn btn-primary" type="button" onclick="find_persons('persons_sessions',2)">اضافة مرافق </button>
                                                                </div>

                                                                <div class="col-md-12 form-group">
                                                                    <button onclick="save_sessions();" class="btn btn-success">حفظ</button>
                                                                </div>
                                                                <div class="col-md-12 form-group">
                                                                    <table dir="rtl" class="table table-bordered">
                                                                        <thead>
                                                                            <tr>

                                                                                <th>كود الجلسة</th>
                                                                                <th>تاريخ الجلسة</th>
                                                                                <th>حذف/مشاهدة</th>

                                                                            </tr>
                                                                        </thead>
                                                                        <tbody id="sessions">
                                                                            <tr>
                                                                            </tr>
                                                                        </tbody>


                                                                    </table>
                                                                </div>


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
                                                                        <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="txtnotes" runat="server">
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
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_m" ID="lbldate_m_expenses_basic"></asp:Label>
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_h" ID="lbldate_h_expenses_basic"></asp:Label>
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
                                                                        <input onkeypress="return isNumber(event);" required dbcolumn="amount" type="text" id="Text11"
                                                                            class="form-control" runat="server" clientidmode="Static" />
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


                                        </div>
                                        <!-- تواريخ استلام النفقة -->
                                        <div class="bhoechie-tab-content">
                                            <div class="col-md-12">
                                                <div class="panel-group" id="accordion10">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse14">تاكيد الاستلام والتسليم   </a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse14" class="panel-collapse collapse in">
                                                            <div class="panel-body" id="expense_details">
                                                                <asp:Label ID="lblexpenses_details" runat="server" Style="display: none" dbcolumn="id"></asp:Label>
                                                                <label id="lblexpense_date" runat="server" style="display: none"></label>
                                                                <%-- start group 2--%>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        التاريخ  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">

                                                                        <div class="fancy-form" id="date_expense_details">
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_m" ID="lbldate_m_expense_details"></asp:Label>
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_h" ID="lbldate_h_expense_details"></asp:Label>
                                                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar4" />
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">المسلم </label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="deliverer_id" required class="form-control" SkinID="form-control" ClientIDMode="Static" ID="ddldeliverer_id_expense" runat="server">
                                                                            <asp:ListItem Value="0">اختر</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <button class="btn btn-primary" type="button" onclick="find_persons('ddldeliverer_id_expense',1)">اضافة مسلم </button>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">المستلم </label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="rciever_id" required SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlrciever_id_expense" runat="server">
                                                                            <asp:ListItem Value="0">اختر</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                      <button class="btn btn-primary" type="button" onclick="find_persons('ddlrciever_id_expense',1)">اضافة مستلم </button>

                                                                    </div>
                                                                </div>
                                                                        <div class=" col-md-12 form-group ">

                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        المبلغ    </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);"  dbcolumn="amount" type="text" id="Text1"
                                                                            class="form-control" runat="server" clientidmode="Static" />
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">الموظف المسؤل</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList SkinID="form-control" class="form-control" dbColumn="employee_id" ClientIDMode="Static" ID="ddlemployee_id4" runat="server">
                                                                        </asp:DropDownList>



                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        وقت التسليم </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" dbcolumn="time" type="text" id="txtNafqatime"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-9 col-sm-12">

                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="email">تم التسليم </label>
                                                                    </div>
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <input dbcolumn="done" type="checkbox" id="doneid" style="width: 40px;" class="form-control" />
                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group" id="save_expense_details" style="display: none">
                                                                    <button onclick="save_expense_details();" class="btn btn-success">حفظ</button>
                                                                </div>

                                                                <div class="col-md-12 form-group">
                                                                    <button onclick="get_archive_expense(1); return false;" class="btn btn-success">ارشفة</button>
                                                                    <button onclick="get_archive_expense(2); return false;" class="btn btn-success">جديد</button>
                                                                </div>

                                                                <div class="col-md-12 form-group">
                                                                    <table id="table_expense_data_archive" dir="rtl" class="table table-bordered">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>التاريخ</th>
                                                                                <th>الحالة</th>

                                                                            </tr>
                                                                        </thead>
                                                                        <tbody id="expense_details_archive" style="display: none">
                                                                            <tr>
                                                                            </tr>
                                                                        </tbody>

                                                                        <tbody id="expense_details_new" >
                                                                        </tbody>
                                                                    </table>
                                                                </div>

                                                                <%--end of group1--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>

                                        </div>

                                        <!--   اجرائات العضوالمباشر للحالة -->
                                        <div class="bhoechie-tab-content">
                                            <%-- start--%>


                                            <div class="col-md-12">

                                                <div class="panel-group" id="accordion11">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse15" class="fa-fa-plus">اجرائات العضو المباشر للحالة   </a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse15" class="panel-collapse collapse in">
                                                            <div class="panel-body" id="case_correspondences">
                                                                <%-- start group 2--%>
                                                                <asp:Label ID="lblcorrespondences_id" ClientIDMode="static" runat="server" Style="display: none" dbcolumn="id"></asp:Label>
                                                                <div class=" col-md-12 form-group ">

                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        رقم الاجراء  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return isNumber(event);" disabled dbcolumn="code" type="text" id="txtcode_correspondences"
                                                                            class="form-control" runat="server" clientidmode="Static" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        تاريخ الاجراء   </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">

                                                                        <div class="fancy-form" id="divdate_correspondences">
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_m" ID="lblcorrespondences_date_m"></asp:Label>
                                                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_h" ID="lblcorrespondences_date_h"></asp:Label>
                                                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar7" />
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">الطرف الاخر </label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="person_id" SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlperson_id" runat="server">
                                                                        </asp:DropDownList>
                                                                         <button class="btn btn-primary" type="button" onclick="find_persons('ddlperson_id',1)">اضافة طرف اخر </button>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">نوع الاجراء </label>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:DropDownList dbcolumn="type" SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddltype" runat="server">
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group ">
                                                                    <br />
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">ملاحظات على الاجراء </label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="TextBox14" runat="server">
                                                                        </asp:TextBox>


                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 form-group">
                                                                    <button onclick="save_correspondences();" class="btn btn-success">حفظ</button>
                                                                </div>
                                                                <div class="col-md-12 form-group">
                                                                    <table dir="rtl" class="table table-bordered">
                                                                        <thead>
                                                                            <tr>

                                                                                <th>كود الاجراء</th>
                                                                                <th>تاريخ الاجراء</th>
                                                                                <th>حذف/مشاهدة</th>

                                                                            </tr>
                                                                        </thead>
                                                                        <tbody id="correspondences">
                                                                            <tr>
                                                                            </tr>
                                                                        </tbody>


                                                                    </table>
                                                                </div>

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
                        <%--  <uc1:ImageSlider runat="server" ID="ImageSlider" />
                    <uc1:MultiPhotoUpload runat="server" ID="MultiPhotoUpload" />
                    <uc1:DynamicTable runat="server" ID="DynamicTable" />
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />--%>
                    </div>
                </div>
            </label>
            </label>
            </label>
            </label>
            </label>
                
    
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
