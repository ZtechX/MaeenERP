<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="report_settings.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.report_settings" Theme="Theme5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager  ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/report_settings.asmx" />
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

                <script type="text/javascript">
                    function UploadComplete2(sender, args) {
                       
                       var fileLength = args.get_length();
                       var fileType = args.get_contentType();
                       //alert(sender);
                       document.getElementById('imgItemURL').src = 'images_reports/' + args.get_fileName();
                       var img = document.getElementById('imgLoader');
                       img.style.display = 'none';
                       switch (true) {
                           case (fileLength > 1000000):

                               fileLength = fileLength / 1000000 + 'MB';
                               break;

                           case (fileLength < 1000000):

                               fileLength = fileLength / 1000000 + 'KB';
                               break;

                           default:
                               fileLength = '1 MB';
                               break;
                       }
                       clearContents(sender);
                   }
                    function UploadStarted2(sender, args) {
                        
                       
                       
                   }
                
                   function ClearMe(sender) {
                       sender.value = '';
                   }
                   function clearContents(sender) {
                       { $(sender._element).find('input').val(''); }
                   }
                </script>

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
                    <script src="../JS_Code/report_settings/report_settings.js"></script>
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
                      <asp:Label ID="lblmainid" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="id"></asp:Label>
                            <div class="cp_margin pad10">
                            <div class="clear"></div>
                            <asp:Panel ID="pnlForm" runat="server">
                                <div class="col-md-6">
                                
                                        
                                            <div class="row">
                                                  <div class="col-md-3 col-sm-12">
                                                 <label for="Name" class="label-required">نوع الترويسة</label>

                                                  </div>
                                                 <div class="col-md-9 col-sm-12">
                                                      <asp:DropDownList dbcolumn="type_id" onchange="get_types();" required class="form-control" ClientIDMode="Static" ID="ddltype_id" runat="server">
                                                        </asp:DropDownList>

                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddltype_id"
                                                    ErrorMessage="من فضلك أختر الترويسة " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                                  </div>
                                                  </div>
                                            <%--<div class="row">
                                        <br />
                                              <label class="required">اختار الصورة </label>
                                          <asp:Image ID="imgItemURL" ClientIDMode="Static" runat="server" Width="114px" ImageUrl="~/images/defualtPic.png" />

                                       <div class="fancy-file-upload">

                                    <i class="fa fa-upload"></i>
                                    <asp:AsyncFileUpload class="form-control " ID="fuPhoto1" SkinID="image-upload" runat="server"  OnClientUploadStarted="UploadStarted2" OnUploadedComplete="PhotoUploaded"
                                        OnClientUploadComplete="UploadComplete2" FailedValidation="False" />

                                        <asp:TextBox id="photo_nm" runat="server" ClientIDMode="Static" type="text" class="form-control" style="margin-top: -40px;"  ></asp:TextBox>

                                    
                                             </div>
                                      
                                
                                               </div>--%>
                                    <div class="form-group row">
                                        <div class="col-md-6 col-md-offset-3">
                                            <div>
                                                <asp:Image ID="imgItemURL" ClientIDMode="Static" runat="server" Width="114px" ImageUrl="~/App_Themes/images/add-icon.jpg" />
                                                <div class="update-progress-img">
                                                    <asp:Image ID="imgLoader" runat="server" ClientIDMode="Static" Style="display: none;" ImageUrl="../App_Themes/images/loader.gif" />
                                                </div>
                                            </div>
                                            <div class="clear">
                                            </div>
                                            <div class="photo-upload-box">
                                                <span>تحميل صورة</span>
                                                <asp:AsyncFileUpload ID="fuPhoto1" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                                    OnClientUploadComplete="UploadComplete2" OnClientUploadStarted="UploadStarted2"
                                                    FailedValidation="False" />
                                                <asp:TextBox ID="photo_nm" runat="server" ClientIDMode="Static" type="text" class="form-control" Style="display: none;"></asp:TextBox>

                                            </div>
                                        </div>

                                    </div>

                                          
                                   
                                </div>
                               
                                <div class="clearfix"></div>
                            </asp:Panel>
                        </div>
                   
                    </div>
                         <%-- <uc1:ImageSlider runat="server" ID="ImageSlider" />
                       <uc1:MultiPhotoUpload runat="server" id="MultiPhotoUpload" />--%>
                    <uc1:DynamicTable runat="server" ID="DynamicTable" />
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>