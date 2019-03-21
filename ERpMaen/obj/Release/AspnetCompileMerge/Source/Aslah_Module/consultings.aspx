<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="consultings.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.consultings" Theme="Theme5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            
            <asp:ServiceReference Path="~/ASMX_WebServices/Testwebservice.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/consulting.asmx" />
        </Services>
    </asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="update-progress02">
                <asp:UpdateProgress ID="upg" runat="server" AssociatedUpdatePanelID="up">
                    <ProgressTemplate>
                        <asp:Image ID="imgProgress" runat="server" ImageUrl="~/App_Themes/images/ajax-loader.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="wraper">

                <div>
                    <script src="../JS_Code/consultings/consulting.js"></script>
                    <link href="../css/cases/cases.css" rel="stylesheet" />
                    
                     <script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="رسائل الاستشارة" ClientIDMode="Static" SkinID="page_title"></asp:Label>
                    </div>
                                                    <input id="loginUser"  style="display: none" runat="server" />
                                  
                    <div class="strip_menu">
                        
                        <uc1:PnlConfirm runat="server" ID="PnlConfirm" />
                    </div>
                    <uc1:Result runat="server" ID="Result" />
                    <div id="divForm" class="newformstyle form_continer">
                        
                          <asp:Label ID="consulting_id" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="consulting_id"></asp:Label>
                         <div class="panel panel-default" style="width: 75%; margin-left: auto;margin-right: auto;">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">بيانات الاستشارة</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse3" class="panel-collapse collapse">
                                                            <div class="panel-body" id="person_against" style="direction: rtl;text-align: right;">
                                                                <%-- start group3--%>
 <label class="col-md-12" style="margin:0px;text-align:center;margin-bottom:20px;border-bottom:1px solid #ddd;padding-bottom: 10px;" id="Ctitle"></label>
                                                                
                                              <div class="col-md-6">
                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label >اسم الاستشارة   </lable>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <label id="consult_nm" >  </lable>
                                        </div>
                                          <br />
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label> رقم الاستشارة  </lable>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <label id="code"></label>
                                        </div>
                                        <br />
                                    </div>
                                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label >مصدر الاحالة</label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <label id="source_id" > </label>

                                        </div>
                                        <br />
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label >تصنيف الحالة </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <label id="category_id" > </label>

                                        </div>
                                        <br />
                                    </div>
                               
                                       </div>
<div class="col-md-6">
                                  
                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label > حالة الاستشارة </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <label id="status" > </label>

                                        </div>
                                        <br />
                                    </div>
                                           <div class="col-md-12 form-group ">
                                        <div class="col-md-3 col-sm-12">
                                            <label >تاريخ التقديم</lable>
                                        </div>
                                        <br />
                                        <div class="col-md-3 col-sm-12"></div>
                                        <div class="col-md-9 col-sm-12">

                                                <label id="lblstart_date"></label>
                                            <br />
                                                <label id="lblstart_date_hj"></label>
                                               
                                        </div>
                                    </div>
                                </div>   
                                        
                                                                    <div class="col-md-12" style="margin-top:10px;">
                                                                        <label>ملاحظات  حول الاستشارة</label>

                                                                    </div>
                                                                <br />
                                                                    <div class="col-md-9">
                                                                        <asp:TextBox Enabled="false" TextMode="multiline" class="form-control"  ClientIDMode="Static" ID="income_notes" runat="server" >
                                                                        </asp:TextBox>


                                                                    </div>
                                                                
                                                                    <%--end group3--%>
                                                            
                                                        </div>
                                                    </div>
                             </div>
                       <div class="panel panel-default" style="width: 75%; margin-left: auto;margin-right: auto;">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">رسائل سابقة</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse2" class="panel-collapse collapse">
                                                            <div class="panel-body" id="oldMassages" style="direction:rtl;">
                                                                <%-- start group3--%>
                                              
                                                                    <%--end group3--%>
                                                            </div>
                                                        </div>
                                                    </div>

                          <div class="col-md-12" style="direction:rtl;" id="newMess">
                                                                   
                                                                        <label>رسالة جديدة</label>
                                                                       <br />
                                                                    <div class="col-md-9" >
                                                                        <asp:TextBox  TextMode="multiline" Rows="5" class="form-control"  ClientIDMode="Static" ID="txtmessage" runat="server" style="margin-right: 0px;">
                                                                        </asp:TextBox>
                                                                        <input type="button" onclick="save();" value="إرســـال" style="float:left;font-size:medium;margin-top:10px;padding: 6px;"/>

                                                                    </div>
                                                                </div>

                    </div>
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                   
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

