﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="contact.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.contact1" Theme="Theme5"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager  ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/contact.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
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
                <style>
        table#divAreaList td {
    border: 1px solid gainsboro;
}
                table#divAreaList th {
    border: 1px solid gainsboro;
}
        #divAreaList1{    max-height: 200px; overflow: auto;}
    </style>
                <div>
                    <script src="../JS_Code/contact/contact.js"></script>
                         </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="رسائل الجوال" SkinID="page_title"></asp:Label>
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
                                        <li>
                                            <asp:LinkButton ID="cmdDelete" OnClientClick="deleteItem(); return false;" ToolTip="Delete Community" runat="server" SkinID="btn-top">
                                               <i class="fa fa-trash-o"></i>
                                           حذف
                                            </asp:LinkButton>
                                            <asp:Panel ID="pnlDelete" runat="server">
                                            </asp:Panel>
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="cmdUpdate" OnClientClick="setformforupdate(); return false;" runat="server" CommandArgument="1"
                                                SkinID="btn-top">
                                               <i class="fa fa-pencil-square-o"></i>
                                           تعديل
                                            </asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                        <uc1:PnlConfirm runat="server" ID="PnlConfirm" />
                    </div>
                    <uc1:Result runat="server" ID="Result" />
                    <div id="divForm" class="newformstyle form_continer">
                        <div class="clear"></div>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="vgroup" />
                      <asp:Label ID="lblmainid" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="id"></asp:Label>
                            <div class="cp_margin pad10">
                            <div class="clear"></div>
                            <asp:Panel ID="pnlForm" runat="server">
                                <div class="col-md-6">


                                    <div class="row">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" class="label-required">الاسم</label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                        <input dbcolumn="name_ar" required type="text" id="name_ar" placeholder="" class="form-control" />


                                        </div>
                                    </div>


                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="TextBox1">رقم التليفون </label>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                              <input dbcolumn="tel1" required type="text" id="tele" placeholder="" class="form-control" onkeypress="return isNumber(event);" />
                                        </div>

                                    </div>

                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label>رقم التليفون(2)</label>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                           <input dbcolumn="tel2" type="text" id="Text8" placeholder="" class="form-control" onkeypress="return isNumber(event);" />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label class="required">البريد الالكترونى</label>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <input dbcolumn="email" required type="email" id="email" placeholder="example@example.com" class="form-control" />
                                        </div>

                                    </div>

                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label>الموقع الالكترونى</label>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                              <input dbcolumn="site" type="text" id="txtsite" placeholder="" class="form-control" />
                                        </div>

                                    </div>
                                       <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                      <label>ملاحظات</label>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                                 <input dbcolumn="notes" type="text" id="txtnotes" placeholder="" class="form-control" />
                                        </div>

                                    </div>

                                 
                                </div>


                                <div class="clearfix"></div>
                            </asp:Panel>
                        </div>
                   
                    </div>
                        <uc1:DynamicTable runat="server" ID="DynamicTable" />
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>