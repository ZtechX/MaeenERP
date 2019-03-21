<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="register_companies.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.register_companies" Theme="Theme5" %>

<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="result" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/pnlConfirm.ascx" TagPrefix="uc1" TagName="pnlconfirm" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/ImageSlider.ascx" TagPrefix="uc1" TagName="ImageSlider" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">

    <asp:ScriptManager  ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/register_company.asmx" />
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
  .completionListElement 
{  
    visibility : hidden; 
    margin : 0px! important; 
    background-color : inherit; 
    color : black; 
    border : solid 1px gray; 
    cursor : pointer; 
    text-align : left; 
    list-style-type : none; 
    font-family : Verdana; 
    font-size: 11px; 
    padding : 0; 
    z-index: 1100 !important;
} 
.listItem 
{ 
    background-color: white; 
    padding : 1px; 
    text-align:center;
    margin:2%;
    border-radius:2px;
   z-index: 1100;
}      
.highlightedListItem 
{ 
    background-color: #64c5b1; 
      text-align:center;
      font-size:medium;
       border-radius:3px;
    padding : 1px; 
}
        #div_credit,#errormessage {
            display:none;
        }
    </style>      

                <div>
                    <script src="../JS_Code/register_company/register_company.js"></script>
                    <script src="../JS_Code/register_company/register_company_Upload.js"></script>
                     
                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="اعدادات التسجيل" SkinID="page_title"></asp:Label>
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
                                                 <label for="Name" class="label-required">الفئة</label>

                                                  </div>
                                                 <div class="col-md-9 col-sm-12">
                                                      <asp:DropDownList dbcolumn="category_id" onchange="get_category();"  class="form-control" ClientIDMode="Static" ID="ddlcategory_id2" runat="server">
                                                    </asp:DropDownList>
                                                     
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlcategory_id2"
                                                    ErrorMessage="من فضلك اختر الفئة " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                                  </div>
                                                  </div>
                                            <div class="row">
                                                 <table id="tab_logic" class="table table-hover table-vertical-middle nomargin">
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th class="table-header">الشرط /الحكم
                                                </th>
                                                <th class="table-header">النوع
                                                </th>
                                      

                                            </tr>

                                        </thead>
                                        <tbody id="draw_items">
                                            <tr id='addr0'>
                                                <td>1</td>

                                                <td>
                                                    <input type="text" dbcolumn=""  id="details0"  placeholder="" class="form-control moneyValue" />
                                                </td>
                                                <td>
                                        
                                                </td>
                             
                                                <td>
                                                    <select class="form-control" required id="type0">
                                                        <option value="0">شرط</option>
                                                        <option value="1">  حكم</option>
                                                    </select>
                                                </td>


                                            </tr>

                                        </tbody>
                                    </table>
                                <a id="add_row" onclick="add_row();" class="btn btn-success pull-right">اضافة سجل<i class="ace-icon fa fa-arrow-down align-top bigger-125"></i></a><a id='delete_row' onclick="delete_row();" class="pull-left btn btn-danger">حذف سجل<i class="ace-icon fa fa-arrow-up align-top bigger-125"></i> </a>
                                <br />
                                <br />
                                <br />
                                          <label for="txtUploadedFiles">الملفات</label>
                                <div class="fancy-file-upload">


                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-3d" Style="padding: 0 20px 0 20px;" Text="+" CausesValidation="False" />
                                    <asp:TextBox ID="TextBox1" CssClass="form-control" Style=" display: inline-block;width: 40%;" ReadOnly="true" onclick="showUploadedFilesTable(this);" runat="server"
                                        ClientIDMode="Static"></asp:TextBox>


                                </div>
                                                
                                           </div>
                                            

                                           
                                              
                                   
                                </div>
                              
                                <div class="clearfix"></div>
                            </asp:Panel>
                        </div>
                   
                    </div>
<%--                          <uc1:ImageSlider runat="server" ID="ImageSlider" />--%>
<%--                    <uc1:MultiPhotoUpload runat="server" ID="MultiPhotoUpload" />--%>
                    <uc1:DynamicTable runat="server" ID="DynamicTable" />
                   <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>