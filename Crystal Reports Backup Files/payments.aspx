<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="payments.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.payments" Theme="Theme5"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/ImageSlider.ascx" TagPrefix="uc1" TagName="ImageSlider" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<%@ Register Src="~/UserControls/HijriCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager  ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/payments.asmx" />
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
                    <script src="../JS_Code/payments/payments.js"></script>
  <%--                  <script src="../JS_Code/payments/halls_Save.js"></script>
                    <script src="../JS_Code/payments/halls_Edit.js"></script>
                    <script src="../JS_Code/payments/halls_Upload.js"></script>--%>
                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="الدفعات" SkinID="page_title"></asp:Label>
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
                              <asp:TextBox runat="server" ID="txtTemId" Style="display: none"></asp:TextBox>
                                <asp:Label ID="ddlcomp_id" ClientIDMode="Static" Style="display: none" runat="server" dbcolumn="project_id"></asp:Label>
                                <asp:Label ID="lbluser_id" ClientIDMode="Static" Style="display: none" runat="server" dbcolumn=""></asp:Label>
                                <asp:Label ID="check_userName" ClientIDMode="Static" Style="display: none" runat="server" dbcolumn=""></asp:Label>
                                <asp:Label ClientIDMode="Static" runat="server" Style="display: none" ID="lblDelete">1</asp:Label>
                                <asp:Label ClientIDMode="Static" runat="server" Style="display: none" ID="lblUpdate">1</asp:Label>

                        <div class="cp_margin pad10">
                            <div class="clear"></div>
                            <asp:Panel ID="pnlForm" runat="server">
                                <div class="col-md-6">
                                
                                        
                                            <div class="row">
                                                  <div class="col-md-3 col-sm-12">
                                                 <label for="Name" class="label-required">  اعضاء الجمعية</label>

                                                  </div>
                                                 <div class="col-md-9 col-sm-12">
                                                      <asp:DropDownList dbcolumn="member_id" onchange="get_member_money()" required class="form-control" ClientIDMode="Static" ID="member_id" runat="server">
                                            </asp:DropDownList>
                                                          
                                              
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="member_id"
                                                    ErrorMessage="من فضلك اختار الاعضاء " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                                  </div>
                                                  </div>
                                            <div class="row">
                                                <br />
                                                <div class="col-md-3 col-sm-12">
                                                    <label for="TextBox1" class="label-required"> الدفعات المالية  </label>
                                                    </div>
                                                <div class="col-md-9 col-sm-12">
                                                <asp:TextBox  class="form-control"  dbcolumn="amount_money" type="text" id="amount_money" ClientIDMode="Static"  runat="server">
                                                </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="amount_money"
                                                    ErrorMessage="من فضلك  ادخل الدفعات " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                                    </div>
                                                
                                           </div>

                                    <div class="row">
                                                <br />
                                                <div class="col-md-4 col-sm-12">
                                                   
                                                <label for="TextBox1">تاريخ استحقاق الدفعة المالية    </label>

                                                    </div>
                                                <div class="col-md-4 col-sm-12 fancy-form" id="divdate2">
                                                  
                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_m" ID="lbldate_m"></asp:Label>
                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_hj" ID="lbldate_hj"></asp:Label>
                                        <uc1:HijriCalendar runat="server" ID="HijriCalendar2" />
                                                    </div>
                                                 </div>

                                           

                                           
                                              
                                   
                                </div>


                                <div class="col-md-6">
                                  
                                               <div class="row">
                                               
                                                <div class="col-md-3 col-sm-12">
                                                <label class="required">طريقة الدفع</label>

                                                    </div>
                                                <div class="col-md-9 col-sm-12">
                                                <asp:DropDownList dbcolumn="payment_method"  required class="form-control" ClientIDMode="Static" ID="payment_method" runat="server">
                                            </asp:DropDownList>
                                                    </div>
                                               </div>   
                                            
                                     <div class="row">
                                               
                                                <div class="col-md-3 col-sm-12">
                                                <label class="required">الحالة</label>
                                                    </div>
                                                <div class="col-md-3 col-sm-12">
                                                <select  dbcolumn="paid" id="paid">
                                                <option value="0">دفعت</option>
                                                <option value="1">لم تدفع</option>
                                                      </select>
                                                    </div>
                                               </div> 
                                            
                                            
                                            
                                       
                                    <div class="row" style ="display:none">

                                            <div class="col-md-12 col-sm-12" >
                                                <asp:Button ID="cmdPOP" runat="server" SkinID="uploadBtn_plus" Text="+" CausesValidation="False" />
                                                <asp:TextBox SkinID="txt80percentage" ID="txtUploadedFiles" ReadOnly="true" onclick="showUploadedFilesTable(this);" runat="server" MaxLength="20"
                                                    ClientIDMode="Static"></asp:TextBox>
                                                <label for="txtUploadedFiles">الملفات</label>
                                            </div>
                                        </div>

                                        
                                </div>
                          
                                <div class="clearfix"></div>
                            </asp:Panel>
                        </div>
                   
                    </div>
                          
                       <uc1:MultiPhotoUpload runat="server" id="MultiPhotoUpload" />
                    <uc1:DynamicTable runat="server" ID="DynamicTable" />
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>