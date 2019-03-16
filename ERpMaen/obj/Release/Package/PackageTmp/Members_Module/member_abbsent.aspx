<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="member_abbsent.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.member_abbsent" Theme="Theme5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/ImageSlider.ascx" TagPrefix="uc1" TagName="ImageSlider" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/members_abbsent.asmx" />
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
                <div>
                    <script src="../JS_Code/members_abbsent/members_abbsent.js"></script>
                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="حضور الاجتماعات" SkinID="page_title"></asp:Label>
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

                        <asp:Label ID="Label1" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="id"></asp:Label>
                        <asp:TextBox runat="server" ID="TextBox1" Style="display: none"></asp:TextBox>
                        <asp:Label ID="Label2" ClientIDMode="Static" Style="display: none" runat="server" dbcolumn="project_id"></asp:Label>
                        <asp:Label ID="Label3" ClientIDMode="Static" Style="display: none" runat="server" dbcolumn=""></asp:Label>
                        <asp:Label ID="Label4" ClientIDMode="Static" Style="display: none" runat="server" dbcolumn=""></asp:Label>
                        <asp:Label ClientIDMode="Static" runat="server" Style="display: none" ID="Label5">1</asp:Label>
                        <asp:Label ClientIDMode="Static" runat="server" Style="display: none" ID="Label6">1</asp:Label>
                        <div class="cp_margin pad10">
                            <div class="clear"></div>
                            <asp:Panel ID="pnlForm" runat="server">
                                <div class="col-md-4">


                                    <div class="row">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" class="label-required">المجلس</label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList dbcolumn="board_id" onchange="get_board_session() ;" required class="form-control" ClientIDMode="Static" ID="board_id" runat="server">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="board_id"
                                                ErrorMessage="من فضلك المجلس " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>


                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="TextBox1">الاجتماعات </label>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList required dbcolumn="dep_type" onchange="get_member_session() ;" class="form-control" ClientIDMode="Static" ID="ddlsession" runat="server">
                                            </asp:DropDownList>
                                        </div>

                                    </div>

                                    <div class="row">
                                        
                                        <div class="col-md-3 col-sm-12">
                                        </div>
                                        
                                    </div>




                                </div>
                                <div class="col-md-8">


                                    <div class="row">
                                        <div class="col-md-6 col-sm-12">
                                            <div style="max-height: 300px; overflow: auto;">
                                                <label>الحضور</label>
                                                <table class="table table-striped table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>الكل</th>
                                                            <th>المتدرب</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="approved-body">
                                                    </tbody>


                                                           </table>
                                                        </div>
                                            <br />
                                            <button class="btn btn-primary" id="all_member" onclick="abbsent(); return false;">
                                            غياب العضو                           
                                        </button>
                                        </div>
                                        <div class="col-md-6 col-sm-12">

                                            <div style="max-height: 300px; overflow: auto;">
                                                <label>الغياب</label>
                                                <table class="table table-striped table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>الكل</th>
                                                            <th>المتدرب</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="trainner-body">
                                                    </tbody>




                                                </table>
                                                <button class="btn btn-danger" id="some_member" onclick="disapproved(); return false;">حذف العضو من الغياب</button>

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


                                <div class="clearfix"></div>
                            </asp:Panel>
                        </div>

                    </div>
                    <uc1:ImageSlider runat="server" ID="ImageSlider" />
                    <uc1:MultiPhotoUpload runat="server" ID="MultiPhotoUpload" />
                    <uc1:DynamicTable runat="server" ID="DynamicTable" />
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
