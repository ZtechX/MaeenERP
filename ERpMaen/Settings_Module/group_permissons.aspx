<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="group_permissons.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.group_permissons" Theme="Theme5"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/ImageSlider.ascx" TagPrefix="uc1" TagName="ImageSlider" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager  ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/group_permissons.asmx" />
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
                    .panel {
    width: 100%;
    position: relative;
    padding: 0px;
    bottom: auto;
}
                    table.table{direction:rtl;}
                    .btn-group {
                        width: auto;
                          margin: 0 !important;
                    }
                    .panel input[type=checkbox] {
                        margin:0px;
                        padding:0px;
                        width:18px;
                        height:18px;
                    }
        table#divAreaList td {
    border: 1px solid gainsboro;
}
                table#divAreaList th {
    border: 1px solid gainsboro;
}
        #divAreaList1{    max-height: 200px; overflow: auto;}
       .card .btn-link{width:100%;}
    </style>

                <div>
<%--                    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">--%>
<%--                    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>--%>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
                    <script src="../JS_Code/group_permissons/group_permissons.js"></script>
                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="صلاحيات المجموعات" SkinID="page_title"></asp:Label>
                    </div>
                     <div id="SavedivLoader" class="loader" style="display: none; text-align: center;">
                                            <asp:Image ID="img" runat="server" ImageUrl="../App_Themes/images/loader.gif" />
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
                       <asp:Label ID="lbluser_id" ClientIDMode="Static" Style="display: none" runat="server" dbColumn=""></asp:Label>
                        <asp:Label ID="lblcomp_id" ClientIDMode="Static" Style="display: none" runat="server" dbColumn=""></asp:Label>
                        
                        <div class="cp_margin pad10">
                            <div class="clear"></div>
                            <asp:Panel ID="pnlForm" runat="server">
                                <div class="col-md-6">
                                <div class="row">
                                      <button onclick="addNewGroup(); return false;" class="btn btn-info btn-lg pull-right"  >إنشاء مجموعة</button>
       
                                </div>
                                        
                                            <div class="row">
                                                  <div class="col-md-3 col-sm-12">
                                                 <label for="Name" class="label-required">المجموعة </label>

                                                  </div>
                                                 <div class="col-md-9 col-sm-12">
                                                   <asp:DropDownList dbcolumn="type_id" onchange="fillPerm();" class="form-control" ClientIDMode="Static" ID="ddlgroup_id" runat="server">
                                                        </asp:DropDownList>
                                              
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlgroup_id"
                                                    ErrorMessage="من فضلك أدخل اختر المجموعة " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                                  </div>
                                                  </div>
                                           
                                              
                                   
                                                       </div>
                               
                                <div class="clearfix"></div>
                            </asp:Panel>
                        </div>
                        <div class="row">
                           <div id="tablePrint">
                                </div>

                        </div>
                   
                    </div>

                    <div  id="groupModal" dir="rtl" style="display:none;">
    <div class="card card-body">
            <div class="form-group">
                
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">
                                             اسم ألمجموعة  </lable>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <input  dbcolumn="consult_nm" type="text" id="txtGroup_nm"
                                                class="form-control" runat="server" clientidmode="Static" />

                                            <br />
                                        </div>
                                    </div>
            <div class="row">
                                      <button onclick="savegroup(); return false;" class="btn btn-info btn-lg pull-left"  >حفظ</button>
       
                                </div>
    
        </div>
                  </div>      
                 
                          <%--<uc1:ImageSlider runat="server" ID="ImageSlider" />
                       <uc1:MultiPhotoUpload runat="server" id="MultiPhotoUpload" />
                    <uc1:DynamicTable runat="server" ID="DynamicTable" />--%>
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />
                </div>
            </div>
             
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>