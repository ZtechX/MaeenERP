<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Halls.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.Hall" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/ImageSlider.ascx" TagPrefix="uc1" TagName="ImageSlider" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager  ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/halls.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/Testwebservice.asmx" />
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
                    <script src="../JS_Code/halls/halls.js"></script>
  <%--                  <script src="../JS_Code/halls/halls_Save.js"></script>
                    <script src="../JS_Code/halls/halls_Edit.js"></script>
                    <script src="../JS_Code/halls/halls_Upload.js"></script>--%>
                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="Type" SkinID="page_title"></asp:Label>
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
                        <asp:Label ID="lblhallsId" dbColumn="id" runat="server" Style="display: none"></asp:Label>
                        <div class="cp_margin pad10">
                            <div class="clear"></div>
                            <asp:Panel ID="pnlForm" runat="server">
                                <div class="col-md-4">
                                    <ul class="form_list">
                                        <li>
                                        <div class="col-md-12 col-sm-12">
                                                <asp:TextBox Enabled="false" ReadOnly="true"  SkinID="txt80percentage"  dbColumn="barcode" ClientIDMode="Static" ID="txtBarcode" runat="server">
                                                </asp:TextBox>
                                                <label for="txtBarcode">كود القاعة </label>
                                           </div>
                                            <div class="col-md-12 col-sm-12">
                                                <asp:TextBox SkinID="txt80percentage"  dbColumn="name" ClientIDMode="Static" ID="Name" runat="server">
                                                </asp:TextBox>
                                                <label for="Name" class="label-required">اسم القاعة</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Name"
                                                    ErrorMessage="من فضلك أدخل اسم القاعة " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-12 col-sm-12">
                                                <asp:TextBox SkinID="txt80percentage"  dbColumn="address" ClientIDMode="Static" ID="Textdetails" runat="server">
                                                </asp:TextBox>
                                                <label for="TextBox1">مكان القاعة </label>
                                           </div>
                                            <div class="col-md-12 col-sm-12">
                                                <asp:TextBox SkinID="txt80percentage"  dbColumn="description" ClientIDMode="Static" ID="TextBox1" runat="server">
                                                </asp:TextBox>
                                                <label for="TextBox1">الوصف </label>
                                           </div>
                                           <div class="col-md-12 col-sm-12">
                                                <asp:DropDownList SkinID="drop-down80"  dbColumn="type" ClientIDMode="Static" ID="ddlType" runat="server">
                                                </asp:DropDownList>
                                                <label for="ddlType">النوع</label>
                                           </div>
                                              <div class="col-md-12 col-sm-12">
                                                <asp:TextBox SkinID="txt80percentage"  dbColumn="email" ClientIDMode="Static" ID="email" runat="server">
                                                </asp:TextBox>
                                                <label for="email">البريد الالكترونى </label>
                                           </div>
                                        </li>                                      
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="form_list">
                                        <li>
                                                                                       <div class="col-md-12 col-sm-12">
                                                <asp:DropDownList SkinID="drop-down80"  dbColumn="user_id" ClientIDMode="Static" ID="ddlStaff" runat="server">
                                                </asp:DropDownList>
                                                <label for="ddlStaff">الموظف المسئول</label>
                                           </div>
                                             <div class="col-md-12 col-sm-12">
                                                <asp:DropDownList SkinID="drop-down80"  dbColumn="status" ClientIDMode="Static" ID="ddlStatus" runat="server">
                                                </asp:DropDownList>
                                                <label for="ddlStaff">حالة القاعة</label>
                                           </div>
                                            <div class="col-md-12 col-sm-12">
                                                <asp:TextBox SkinID="txt80percentage"  dbColumn="price" ClientIDMode="Static" ID="TextBox2" runat="server">
                                                </asp:TextBox>
                                                <label for="TextBox1">السعر  </label>
                                           </div>
                                            <div class="col-md-12 col-sm-12">
                                                <asp:TextBox SkinID="txt80percentage"  dbColumn="phone" ClientIDMode="Static" ID="TextBox3" runat="server">
                                                </asp:TextBox>
                                                <label for="TextBox1">تليفون القاعة </label>
                                           </div>
                                            <div class="col-md-12 col-sm-12">
                                                <asp:Button ID="cmdPOP" runat="server" SkinID="uploadBtn_plus" Text="+" CausesValidation="False" />
                                                <asp:TextBox SkinID="txt80percentage" ID="txtUploadedFiles" ReadOnly="true" onclick="showUploadedFilesTable(this);" runat="server" MaxLength="20"
                                                    ClientIDMode="Static"></asp:TextBox>
                                                <label for="txtUploadedFiles">الملفات</label>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                                
                                <div class="col-md-4">
                                      <div id="divAreaList1" class="col-md-12 col-sm-12" runat="server">
                                       <table dir="rtl"  style="border:1px solid; text-align:center; width:100%" id="divAreaList"></table>

                                            </div>
                                </div>
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