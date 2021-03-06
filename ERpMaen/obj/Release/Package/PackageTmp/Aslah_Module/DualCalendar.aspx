﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DualCalendar.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.DualCalendar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/DeualCalendar1.ascx" TagPrefix="uc1" TagName="DeualCalendar1" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/advisor.asmx" />
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
                    <script src="../JS_Code/advisors/advisor.js"></script>

                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="مستشارين" SkinID="page_title"></asp:Label>
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
                        <uc1:DeualCalendar1 runat="server" ID="DeualCalendar11" />
                        <div class="clear"></div>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="vgroup" />
                        <asp:Label ID="lblmainid" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="id"></asp:Label>
                        <div class="cp_margin pad10">
                            <div class="clear"></div>
                            <asp:Panel ID="pnlForm" runat="server">
                                <div class="col-md-6">

                                     <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">
                                            رقم المستشار  </lable>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" readonly="readonly" dbcolumn="code" type="text" id="code"
                                                class="form-control" runat="server" clientidmode="Static" />

                                            <br />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" class="label-required">اسم المستشار</label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" class="form-control" dbColumn="name" ClientIDMode="Static" ID="Name" runat="server">
                                            </asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Name"
                                                ErrorMessage="من فضلك أدخل اسم المستشار " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                        <label  class="label-required">رقم الهاتف</label>

                                        </div>

                                         <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isMobilePhoneFax(event);" dbcolumn="tel" runat="server" type="text" id="txttel" class="form-control" />
                                            <asp:RequiredFieldValidator CssClass="validator" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttel"
                                                ErrorMessage="من فضلك ادخل رقم الهاتف " ValidationGroup="vgroup" ForeColor="red"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                      <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                        <label class="label-required"> رقم الهوية  </lable>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="advisor_identiy" type="text" id="txtNumber"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    <br />
                                        </div>
                                    </div>
                                    

                                   <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                        <label>التخصص</label>
                                            </div>

                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList dbcolumn="specialty" class="form-control" ClientIDMode="Static" ID="ddlspecial_id" runat="server">
                                            </asp:DropDownList>

                                        </div>
                                    </div>


                                </div>
                                <div class="col-md-6">


                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required" for="TextBox1">البريد الالكتروني</label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" class="form-control" dbColumn="email" ClientIDMode="Static" ID="TextEmail" runat="server">
                                            </asp:TextBox>
                                            <br />
                                        </div>
                                    </div>

                                <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="TextBox1">اسم المستخدم </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" class="form-control" dbColumn="user_nm" ClientIDMode="Static" ID="TextBox1" runat="server">
                                            </asp:TextBox>
                                            <br />
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="TextBox1">كلمة المرور </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" class="form-control" dbColumn="password" ClientIDMode="Static" ID="TextBox3" runat="server">
                                            </asp:TextBox>
                                            <br />
                                        </div>

                                    </div>


                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="email">نشط </label>
                                        </div>
                                        <div class="col-md-3 col-sm-12">
                                            <input dbcolumn="active" type="checkbox" id="active" style="width: 40px;" class="form-control" />

                                        </div>
                                    </div>


                                    <div class="col-md-12 col-sm-12" style="display: none">
                                        <asp:Button ID="cmdPOP" runat="server" SkinID="uploadBtn_plus" Text="+" CausesValidation="False" />
                                        <asp:TextBox SkinID="txt80percentage" ID="txtUploadedFiles" ReadOnly="true" onclick="showUploadedFilesTable(this);" runat="server" MaxLength="20"
                                            ClientIDMode="Static"></asp:TextBox>
                                        <label for="txtUploadedFiles">الملفات</label>
                                    </div>


                                </div>

                            
                                <div class="clearfix"></div>
                            </asp:Panel>
                        </div>

                    </div>
                  
                    <uc1:MultiPhotoUpload runat="server" ID="MultiPhotoUpload" />
                    <uc1:DynamicTable runat="server" ID="DynamicTable" />
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
