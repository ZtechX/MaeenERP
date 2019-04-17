<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="trainers.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.trainers" Theme="Theme5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/trainersCls.asmx" />
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
                    .form-control { direction:rtl;
                    }
                </style>
                <div>
                    <script src="../JS_Code/acadmies/trainers.js"></script>
                     <script type="text/javascript">
                         function UploadComplete2(sender, args) {
                             debugger;
                            var fileLength = args.get_length();
                            var fileType = args.get_contentType();
                            //alert(sender);
                            document.getElementById('imgItemURL').src = 'images/' + args.get_fileName();
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
                        function UploadStarted2() {
                            //var fileName = args.get_fileName();
                            var img = document.getElementById('imgLoader');
                            img.style.display = 'block';
                        }
              
                        function ClearMe(sender) {
                            sender.value = '';
                        }
                        function clearContents(sender) {
                            { $(sender._element).find('input').val(''); }
                        }
                    </script>
                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="المدربين" SkinID="page_title"></asp:Label>
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

                                    
                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" class="label-required">اسم المدرب</label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" class="form-control" dbColumn="full_name" ClientIDMode="Static" ID="Name" runat="server">
                                            </asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Name"
                                                ErrorMessage="من فضلك أدخل اسم المدرب " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                        <label  class="label-required">رقم الجوال</label>

                                        </div>

                                         <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isMobilePhoneFax(event);" dbcolumn="User_PhoneNumber" runat="server" type="text" id="txttel" class="form-control" />
                                            <asp:RequiredFieldValidator CssClass="validator" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttel"
                                                ErrorMessage="من فضلك ادخل رقم الجوال " ValidationGroup="vgroup" ForeColor="red"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                      <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                        <label class="label-required"> رقم الهوية  </lable>
                                            </div>

                                      <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="user_indenty" type="text" id="txtNumber"
                                                class="form-control" runat="server" clientidmode="Static" />
                                          
                                    <br />
                                        </div>
                                    </div>
                                    

                                   


                                </div>
                                <div class="col-md-6">


                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label  for="TextBox1">البريد الالكتروني</label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" class="form-control" dbColumn="User_Email" ClientIDMode="Static" ID="TextEmail" runat="server">
                                            </asp:TextBox>
                                            <br />
                                        </div>
                                    </div>

                                   

                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="TextBox1">كلمة المرور </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" class="form-control" dbColumn="User_Password" ClientIDMode="Static" ID="TextBox3" runat="server">
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
                                            <input dbcolumn="Active" type="checkbox" id="active" style="width: 40px;" class="form-control" />

                                        </div>
                                    </div>


                                                      <div class="form-group row">
<div class="col-md-6 col-md-offset-3">
                                    <div >
                                        <asp:Image ID="imgItemURL" ClientIDMode="Static" runat="server" Width="114px" ImageUrl="~/App_Themes/images/add-icon.jpg" />
                                        <div class="update-progress-img">
                                             <asp:Image ID="imgLoader" runat="server" ClientIDMode ="Static" style="display: none;"  ImageUrl="../App_Themes/images/loader.gif" />
                                        </div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <div class="photo-upload-box">
                                        <span>تحميل صورة</span>
                                        <asp:AsyncFileUpload ID="fuPhoto1" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                            OnClientUploadComplete="UploadComplete2" OnClientUploadStarted="UploadStarted2"
                                            FailedValidation="False"  />
                                    </div>
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
