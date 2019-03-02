<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="training_centers.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.training_centers" Theme="Theme5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/ImageSlider.ascx" TagPrefix="uc1" TagName="ImageSlider" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<%@ Register Src="~/UserControls/HijriCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/training_centers.asmx" />
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

                <div>
                    <script src="../JS_Code/acadmies/training_centers.js"></script>

                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="كورسات" SkinID="page_title"></asp:Label>
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
                               

                                <div class="col-md-12 form-group ">
                                    <div class="col-md-3 col-sm-12">
                                        <label for="Name" class="label-required">اسم المركز</label>

                                    </div>
                                    <div class="col-md-9 col-sm-12">
                                        <asp:TextBox SkinID="form-control" class="form-control" dbColumn="name" ClientIDMode="Static" ID="Name" runat="server">
                                        </asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Name"
                                            ErrorMessage="من فضلك أدخل اسم المركز " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                    </div>
                                </div>

                                <div class="col-md-12 form-group ">
                                    <div class="col-md-3 col-sm-12">
                                        <label>المدير</label>
                                    </div>

                                    <div class="col-md-9 col-sm-12">
                                        <asp:DropDownList dbcolumn="admin" class="form-control" ClientIDMode="Static" ID="ddladmin" runat="server">
                                        </asp:DropDownList>

                                    </div>
                                </div>

                                <div class="col-md-12 form-group ">
                                    <div class="col-md-3 col-sm-12">
                                        <label class="label-required">
                                        التاريخ   </lable>
                                    </div>

                                    <div class="col-md-9 col-sm-12">

                                        <div class="fancy-form" id="divdate3">
                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_m" ID="lblstart_date"></asp:Label>
                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_hj" ID="lblstart_date_hj"></asp:Label>
                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar" />
                                        </div>
                                        <br />
                                    </div>
                                          <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">ملاحظات </label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="TextNotes" runat="server">
                                                                        </asp:TextBox>


                                                                    </div>
                                                                </div>
                                    <%--</div>--%>

                                                                      
                                </div>

                                

                                <div class="clearfix"></div>
                            </asp:Panel>
                        </div>

                    </div>
<%--                    <uc1:ImageSlider runat="server" ID="ImageSlider" />
                    <uc1:MultiPhotoUpload runat="server" ID="MultiPhotoUpload" />--%>
                    <uc1:DynamicTable runat="server" ID="DynamicTable" />
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%--select acd_training_centers.id as 'AutoCodeHide' ,acd_training_centers.name as 'اسم المركز' ,acd_training_centers.admin as 'المدير ' from acd_training_centers--%>