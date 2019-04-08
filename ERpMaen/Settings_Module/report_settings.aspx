<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="report_settings.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.report_settings" Theme="Theme5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager  ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/report_settings.asmx" />
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

                <script type="text/javascript">
                        function UploadComplete2(sender, args) {
                            var fileLength = args.get_length();
                            var fileType = args.get_contentType();
                            var Sender_id = $(sender.get_element()).attr("id");
                            if (Sender_id == "fuPhoto_header") {
                                document.getElementById('imgItemURL_header').src = 'images_reports/' + args.get_fileName();
                             } else if (Sender_id == "fuPhoto_footer") {
                                document.getElementById('imgItemURL_footer').src = 'images_reports/' + args.get_fileName();
                              } 
                           
                            
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
        
function ClearMe(sender) {
                            sender.value = '';
                        }
                        function clearContents(sender) {
                            {
                                $(sender._element).find('input').val('');
                            }
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
                        <asp:Label ID="lblFormName" runat="server" Text="اعدادات التقارير" SkinID="page_title"></asp:Label>
                    </div>
                    <div class="strip_menu">
                        <asp:Panel ID="pnlOps" runat="server" Style="text-align: right">
                         <asp:Panel ID="pnlFunctions" runat="server" CssClass="row" Enabled="true">
                                <div class="col-md-9 col-sm-12">
                                    <ul>
                                        
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
                          
                            <asp:Panel ID="pnlForm" runat="server">
                                <div class="col-md-6">
                                
               
                                        <div class="col-md-6">
                                            <div>
                                                <asp:Image ID="imgItemURL_header" ClientIDMode="Static" runat="server" Width="114px" ImageUrl="~/App_Themes/images/add-icon.jpg" />
                                                
                                            </div>
                                            <div class="clear">
                                            </div>
                                            <div class="photo-upload-box">
                                                <span> تحميل صورة الهيدر</span>
                                                <asp:AsyncFileUpload ID="fuPhoto_header" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                                    OnClientUploadComplete="UploadComplete2" 
                                                    FailedValidation="False" />
                                                <asp:TextBox ID="photo_nm1" runat="server" ClientIDMode="Static" type="text" class="form-control" Style="display: none;"></asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div>
                                                <asp:Image ID="imgItemURL_footer" ClientIDMode="Static" runat="server" Width="114px" ImageUrl="~/App_Themes/images/add-icon.jpg" />
                                               
                                            </div>
                                            <div class="clear">
                                            </div>
                                            <div class="photo-upload-box">
                                                <span> تحميل صورة الفوتر</span>
                                                <asp:AsyncFileUpload ID="fuPhoto_footer" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                                    OnClientUploadComplete="UploadComplete2" 
                                                    FailedValidation="False" />
                                                <asp:TextBox ID="photo_nm_footer" runat="server" ClientIDMode="Static" type="text" class="form-control" Style="display: none;"></asp:TextBox>

                                            </div>
                                        </div>

                                   
                                </div>
                               
                                <div class="clearfix"></div>
                            </asp:Panel>
                        </div>
                   
                    </div>
                     
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>