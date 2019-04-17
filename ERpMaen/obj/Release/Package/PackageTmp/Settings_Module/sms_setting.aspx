<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="sms_setting.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.sms_setting" Theme="Theme5"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/ImageSlider.ascx" TagPrefix="uc1" TagName="ImageSlider" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager  ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/sms_setting.asmx" />
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
                    <script src="../JS_Code/sms_setting/sms_setting.js"></script>
  <%--                  <script src="../JS_Code/sms_setting/halls_Save.js"></script>
                    <script src="../JS_Code/sms_setting/halls_Edit.js"></script>
                    <script src="../JS_Code/sms_setting/halls_Upload.js"></script>--%>
                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="اعدادات الرسائل" SkinID="page_title"></asp:Label>
                    </div>
                    <div class="strip_menu">
                        <asp:Panel ID="pnlOps" runat="server" Style="text-align: right">
                         <asp:Panel ID="pnlFunctions" runat="server" CssClass="row" Enabled="true">
                                <div class="col-md-9 col-sm-12">
                                    <ul id="DivAction">
                                        <li >
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
                                                 <label for="Name" class="label-required">اسم الشركة</label>

                                                  </div>
                                                 <div class="col-md-9 col-sm-12">
                                                      <asp:TextBox SkinID="form-control" class="form-control" dbColumn="company_name" ClientIDMode="Static" ID="Name" runat="server">
                                                </asp:TextBox>
                                              
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Name"
                                                    ErrorMessage="من فضلك أدخل اسم الشركة " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                                  </div>
                                                  </div>
                                            <div class="row">
                                                <br />
                                                <div class="col-md-3 col-sm-12">
                                                    <label for="TextBox1"> اسم المستخدم </label>
                                                    </div>
                                                <div class="col-md-9 col-sm-12">
                                                <asp:TextBox SkinID="form-control" class="form-control"  dbColumn="user_name" ClientIDMode="Static" ID="Textdetails" runat="server">
                                                </asp:TextBox>
                                                    </div>
                                                
                                           </div>
                                            <div class="row">
                                                <br />
                                                <div class="col-md-3 col-sm-12">
                                                <label for="TextBox1">اقصي عدد حروف </label>
                                                    </div>
                                                <div class="col-md-9 col-sm-12">
                                                  <asp:TextBox SkinID="form-control" class="form-control"  dbColumn="max_char" ClientIDMode="Static" ID="TextBox1" runat="server">
                                                </asp:TextBox>
                                                    </div>
                                                   </div>

                                           <div class="row" id="Comp_div">
                                               <br />
                                                <div class="col-md-3 col-sm-12">
                                                <label for="ddlType">اسم الشركة</label>

                                                    </div>
                                                <div class="col-md-9 col-sm-12">
                                                <asp:DropDownList SkinID="form-control" class="form-control" dbColumn="comp_id" ClientIDMode="Static" ID="ddlComps" runat="server">
                                                </asp:DropDownList>
                                                    </div>
                                               </div>
                                              
                                   
                                </div>
                                <div class="col-md-6">
                                  
                                                  
                                            <div class="row">
                                                <div class="col-md-3 col-sm-12">
                                                <label for="TextBox1">الموقع الالكتروني  </label>

                                                    </div>
                                                <div class="col-md-9 col-sm-12">
                                                    <asp:TextBox SkinID="form-control" class="form-control"  dbColumn="url" ClientIDMode="Static" ID="TextBox2" runat="server">
                                                </asp:TextBox>
                                                    </div>
                                                 </div>

                                            <div class="row">
                                                <br />
                                                <div class="col-md-3 col-sm-12">
                                                      <label for="TextBox1"> كلمة المرور </label>

                                                    </div>
                                                <div class="col-md-9 col-sm-12">
                                                <asp:TextBox SkinID="form-control" class="form-control"  dbColumn="password" ClientIDMode="Static" ID="TextBox3" runat="server">
                                                </asp:TextBox>
                                                    </div>

                                           </div>
                                            
                                            <div class="row">
                                                <br />
                                                <div class="col-md-3 col-sm-12">
                                                     <label for="TextBox1">  اقصي عدد رسائل </label>
                                                    </div>
                                                <div class="col-md-9 col-sm-12">
                                                    <asp:TextBox SkinID="form-control" class="form-control"  dbColumn="max_message_no" ClientIDMode="Static" ID="TextBox4" runat="server">
                                                </asp:TextBox>
                                                    </div>
                                                  </div>
                                        <div class="row">
                                            <br />
                                                <div class="col-md-3 col-sm-12">
                                             <label for="email"> نشط </label>
                                                    </div>
                                                <div class="col-md-3 col-sm-12">
                                        <input dbcolumn="active"  type="checkbox" id="active" style="width: 40px;" class="form-control" />
                                
                                                    </div>
                                                 </div>


                                            <div class="col-md-12 col-sm-12" style="display:none">
                                                <asp:Button ID="cmdPOP" runat="server" SkinID="uploadBtn_plus" Text="+" CausesValidation="False" />
                                                <asp:TextBox SkinID="txt80percentage" ID="txtUploadedFiles" ReadOnly="true" onclick="showUploadedFilesTable(this);" runat="server" MaxLength="20"
                                                    ClientIDMode="Static"></asp:TextBox>
                                                <label for="txtUploadedFiles">الملفات</label>
                                            </div>

                                        
                                </div>
                                
                                <%--<div class="col-md-4">
                                      <div id="divAreaList1" class="col-md-12 col-sm-12" runat="server">
                                       <table dir="rtl"  style="border:1px solid; text-align:center; width:100%" id="divAreaList"></table>

                                            </div>
                                </div>--%>
                                <div class="clearfix"></div>
                            </asp:Panel>
                        </div>
                   
                    </div>
                          <uc1:ImageSlider runat="server" ID="ImageSlider" />
                       <uc1:MultiPhotoUpload runat="server" id="MultiPhotoUpload" />
                    <uc1:DynamicTable runat="server" ID="DynamicTable" />
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>