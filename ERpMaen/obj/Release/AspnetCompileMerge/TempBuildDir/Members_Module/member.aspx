<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="member.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.member" Theme="Theme5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/members.asmx" />
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

                    #divAreaList1 {
                        max-height: 200px;
                        overflow: auto;
                    }
                   
                </style>

                <script type="text/javascript">
                    function UploadComplete2(sender, args) {

                        var fileLength = args.get_length();
                        var fileType = args.get_contentType();
                        alert(sender);
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
                    function UploadStarted2(sender, args) {



                    }

                    function ClearMe(sender) {
                        sender.value = '';
                    }
                    function clearContents(sender) {
                        { $(sender._element).find('input').val(''); }
                    }
                </script>
                <div>
                    <script src="../JS_Code/members/members.js"></script>
                </div>


                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="اعضاء الجمعية" SkinID="page_title"></asp:Label>
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
                    <div id="divForm" class="newformstyle form_continer" >
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
                            <asp:Panel ID="pnlForm" runat="server" >
                                <div class="col-md-6">


                                    <div class="row">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" class="label-required">الاسم بالعربية</label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" CssClass="form-control" dbColumn="name"  ClientIDMode="Static" ID="Name" runat="server">
                                            </asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Name"
                                                ErrorMessage="من فضلك أدخل الاسم بالعربية " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="TextBox1">الصفة  </label>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" CssClass="form-control" dbColumn="adjective" ClientIDMode="Static" ID="Textdetails" runat="server">
                                            </asp:TextBox>
                                        </div>

                                    </div>


                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="ddlType">المنصب </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList SkinID="form-control" CssClass="form-control" dbColumn="job_id" ClientIDMode="Static" ID="job_id" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>




                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="email">عضو جمعية عمومية </label>
                                        </div>
                                        <div class="col-md-3 col-sm-12">
                                            <input id="member_main" onchange="get_main();" dbcolumn="main" type="checkbox" style="width: 40px;" class="form-control" />

                                        </div>
                                    </div>

                                    <div id="money" style="display: none">
                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                        
                                            <label>الدفعات المالية</label>
                                            </div>
                                        <div class="col-md-9 col-sm-12">
                                            <div class="fancy-form">
                                                <asp:TextBox dbcolumn="amount_money" ID="amount_money" CssClass="form-control" />
                                            </div>

                                            <label class="required">تاريخ استحقاق الدفعة المالية   </label>


                                            <select style="height: 50px" id="date_hj_day" dbcolumn="date_hj_day">
                                                <option value="0">اختر اليوم</option>

                                            </select>
                                            <select style="height: 50px" id="date_hj_month" dbcolumn="date_hj_month">
                                                <option value="0">اختر الشهر</option>
                                            </select>
                                        </div>
                                        
                                        </div>
                                            </div>
                                   


                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="email">دخول على النظام  </label>
                                        </div>
                                        <div class="col-md-3 col-sm-12">
                                            <input id="maintance" onchange="get_enter();" dbcolumn="system_enter" type="checkbox" style="width: 40px;" class="form-control" />

                                        </div>
                                    </div>
                                     <div id="entering" style="display: none">
                                    <div class="row">
                                       <br />
                                            <div class="col-md-3 col-sm-12">
                                            <label>اسم المستخدم</label>
                                                 </div>
                                            <div class="col-md-6 col-sm-12">
                                            <div class="fancy-form">
                                                <input dbcolumn="User_Name" type="text" onchange="check_user();" id="user_name" placeholder="" class="form-control" />
                                            </div>
                                            </div>
                                            </div>

                                    <div class="row">
                                        <br />
                                                <div class="col-md-3 col-sm-12">
                                            <label>كلمة المرور</label>
                                                    </div>
                                        <div class="col-md-6 col-sm-12">
                                            <div class="fancy-form">
                                                <input dbcolumn="User_Password" type="password" id="user_password" placeholder="" class="form-control" />
                                            </div>
                                             </div>
                                        </div>

                                    
                                    </div>



                                </div>
                                <div class="col-md-6">

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




                                    <div class="col-md-12 col-sm-12" style="display: none">
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
                    <%--<uc1:ImageSlider runat="server" ID="ImageSlider" />
                       <uc1:MultiPhotoUpload runat="server" id="MultiPhotoUpload" />--%>
                    <uc1:DynamicTable runat="server" ID="DynamicTable" />
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
