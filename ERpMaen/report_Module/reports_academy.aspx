<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="reports_academy.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.reports_academy" Theme="Theme5"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/reports_academy.asmx" />
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

                <div>
                    <script src="../JS_Code/reports_academy/reports_academy.js"></script>

                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="تقارير الاكاديمية" SkinID="page_title"></asp:Label>
                    </div>
                    <div class="strip_menu">
                        <asp:Panel ID="pnlOps" runat="server" Style="text-align: right">
                            <asp:Panel ID="pnlFunctions" runat="server" CssClass="row" Enabled="true">
                     
                            </asp:Panel>
                        </asp:Panel>
                    </div>

                    
            <div class="tabbable">
                <!-- #  بداية التبويب  -->
                <ul class="nav nav-tabs" id="myTab">
                    <li runat="server" id="ladd" class="active" ClientIDMode="Static">
                        <a data-toggle="tab" href="#subj_degree">
                            <i class="green ace-icon fa fa-home bigger-120"></i>
                            تقرير درجات المادة

                        </a>

                    </li>
                       <li runat="server" id="Li2" class=""  ClientIDMode="Static">
                        <a data-toggle="tab" href="#academy_session">
                            <i class="green ace-icon fa fa-bars bigger-120"></i>
                           تقرير السجل الاكاديمى

                        </a>

                    </li>
                    <li runat="server" id="Li1" class="">
                        <a data-toggle="tab" href="#students_money">
                            <i class="green ace-icon fa fa-bars bigger-120"></i>
                          التقرير المالى للطلبة

                        </a>

                    </li>
                        <li runat="server" id="Li3" class="">
                        <a data-toggle="tab" href="#details_student_money">
                            <i class="green ace-icon fa fa-bars bigger-120"></i>
                            تقرير التفاصيل المالية لكل طالب

                        </a>

                    </li>
                       
                </ul>

                

                 <div  class="tab-content">
                    <!-- # بداية التاب الاول -->
                       <uc1:result runat="server" ID="result1" />
                                   
                    <div id="subj_degree" class="tab-pane fade in active">

                       
                    <!-- # بداية محتويات التبويب -->
                        <div class="form-horizontal" role="form">
                             <div class="row">
                                 <div class="widget-box">
                                     <div class="widget-body">
                                           <div class="newformstyle form_continer" style="direction: rtl;">
                                            <div class="clear"></div>
                                            <div class="cp_margin pad10">

                                                 <div class="col-md-6">

                                                     <div class="form-group">
                                                         <div class="col-md-3 col-sm-12">
                                                             <label class="label-required">الدبلومة</label>
                                                         </div>

                                                         <div class="col-md-9 col-sm-12">
                                                             <asp:DropDownList required dbcolumn="specialty" class="form-control" ClientIDMode="Static" ID="ddldiplomes" runat="server">
                                                             </asp:DropDownList>

                                                         </div>
                                                     </div>

                                                     <div class="form-group">
                                                         <div class="col-md-3 col-sm-12">
                                                             <label class="label-required">الترم</label>
                                                         </div>

                                                         <div class="col-md-9 col-sm-12">
                                                             <asp:DropDownList required dbcolumn="semster" onchange="get_deplomes_subjects();" class="form-control" ClientIDMode="Static" ID="ddlsemster" runat="server">
                                                             </asp:DropDownList>

                                                         </div>
                                                     </div>

                                                     <div class="form-group">
                                                         <div class="col-md-3 col-sm-12">
                                                             <label class="label-required">المواد</label>
                                                         </div>

                                                         <div class="col-md-9 col-sm-12">
                                                             <select id="ddlsubject" class="form-control">
                                                                 <option value="0">اختر</option>

                                                             </select>

                                                         </div>
                                                     </div>

                                                     <div class="form-group">
                                                         <div class="col-md-9 col-sm-12">
                                                             <button onclick="get_report_degree(); return false" class="btn btn-success">التقرير</button>

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
                    <!-- # بداية التاب الثاني -->
                     <div id="academy_session" class="tab-pane fade  ">
                         
                     <div class="form-horizontal" role="form">
                            <!-- #بداية النموذج -->

                            <div class="row">
                                <div class="widget-box">
                                    <div class="widget-body">
                                        <div class="newformstyle form_continer" style="direction: rtl;">
                                            <div class="clear"></div>
                                            <div class="cp_margin pad10">

                                                <asp:Panel ID="Panel1" runat="server">

                                                    <div class="col-md-6">

                                                        <div class="form-group">
                                                            <div class="col-md-3 col-sm-12">
                                                                <label class="label-required">الدبلومة</label>
                                                            </div>

                                                            <div class="col-md-9 col-sm-12">
                                                                <asp:DropDownList required dbcolumn="specialty" onchange="get_deplome_students(1)" class="form-control" ClientIDMode="Static" ID="ddldiplomes1" runat="server">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-3 col-sm-12">
                                                                <label class="label-required">الطلاب</label>
                                                            </div>

                                                            <div class="col-md-9 col-sm-12">
                                                                <select id="ddlstudents" class="form-control">
                                                                    <option value="0">اختر</option>

                                                                </select>

                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <div class="col-md-9 col-sm-12">
                                                                <button onclick="get_report_student(); return false" class="btn btn-success">التقرير</button>

                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="clearfix"></div>
                                                </asp:Panel>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>

                    </div>

                     </div>
                     <!-- # بداية التاب الثالث -->

                     <div id="students_money" class="tab-pane fade  ">
                         
                     <div class="form-horizontal" role="form">
                            <!-- #بداية النموذج -->

                            <div class="row">
                                <div class="widget-box">
                                    <div class="widget-body">
                                        <div class="newformstyle form_continer" style="direction: rtl;">
                                            <div class="clear"></div>
                                            <div class="cp_margin pad10">

                                                <asp:Panel ID="Panel2" runat="server">

                                                    <div class="col-md-6">

                                                        <div class="form-group">
                                                            <div class="col-md-3 col-sm-12">
                                                                <label class="label-required">الدبلومة</label>
                                                            </div>

                                                            <div class="col-md-9 col-sm-12">
                                                                <asp:DropDownList required dbcolumn="specialty" onchange="get_deplome_students()" class="form-control" ClientIDMode="Static" ID="ddldiplomes2" runat="server">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-9 col-sm-12">
                                                                <button onclick="get_report_diplome_money(); return false" class="btn btn-success">التقرير</button>

                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="clearfix"></div>
                                                </asp:Panel>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>

                    </div>

                     </div>

                       <!-- # بداية التاب الرابع -->

                     <div id="details_student_money" class="tab-pane fade  ">
                         
                     <div class="form-horizontal" role="form">
                            <!-- #بداية النموذج -->

                            <div class="row">
                                <div class="widget-box">
                                    <div class="widget-body">
                                        <div class="newformstyle form_continer" style="direction: rtl;">
                                            <div class="clear"></div>
                                            <div class="cp_margin pad10">

                                                <asp:Panel ID="Panel3" runat="server">

                                                    <div class="col-md-6">

                                                        <div class="form-group">
                                                            <div class="col-md-3 col-sm-12">
                                                                <label class="label-required">الدبلومة</label>
                                                            </div>

                                                            <div class="col-md-9 col-sm-12">
                                                                <asp:DropDownList required dbcolumn="specialty" onchange="get_deplome_students(2)" class="form-control" ClientIDMode="Static" ID="ddldiplomes3" runat="server">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-3 col-sm-12">
                                                                <label class="label-required">الطلاب</label>
                                                            </div>

                                                            <div class="col-md-9 col-sm-12">
                                                                <select id="ddlstudents1" class="form-control">
                                                                    <option value="0">اختر</option>

                                                                </select>

                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <div class="col-md-9 col-sm-12">
                                                                <button onclick="get_report_details_student_money(); return false" class="btn btn-success">التقرير</button>

                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="clearfix"></div>
                                                </asp:Panel>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>

                    </div>

                     </div>
            <!-- /نهاية التبويب   -->

                         

        </div>
        <!-- /.نهاية الصف -->

        <div class="space"></div>

    </div>

                    
                    <uc1:Result runat="server" ID="Result" />
                   
                    <uc1:DynamicTable runat="server" ID="DynamicTable" />
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
