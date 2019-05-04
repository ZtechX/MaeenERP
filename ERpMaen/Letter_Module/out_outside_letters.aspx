<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="out_outside_letters.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.out_outside_letters" Theme="Theme5"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/ImageSlider.ascx" TagPrefix="uc1" TagName="ImageSlider" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>



<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager  ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/out_inside_letters.asmx" />
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
                    <script src="../JS_Code/out_inside_letters/out_inside_letters.js"></script>
                    <script src="../js/customCalender/CustomerCalendar.js"></script>
                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="الخطابات الواردة" SkinID="page_title"></asp:Label>
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
                               <asp:Label ID="lblmianid" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="id"></asp:Label>
                                    <asp:Label ID="Label2" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="type">5</asp:Label>
                                    <asp:Label ID="user_id" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="add_by"></asp:Label>
                           <asp:Label ID="related_letter_id" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="related_id">0</asp:Label>
                        <div class="cp_margin pad10">
                            <div class="clear"></div>
                            <asp:Panel ID="pnlForm" runat="server">
                                <div class="col-md-6">


                                    <div class="row">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" class="label-required">الباركود</label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox runat="server"  ReadOnly="true" ClientIDMode="Static" dbColumn="parcode" ID="txtcode" placeholder="" SkinID="form-control" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="TextBox1" class="label-required">عنوان الخطاب  </label>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox runat="server" required dbColumn="title" ID="txtname_ar" placeholder="" SkinID="form-control" CssClass="form-control"></asp:TextBox>

                                        </div>

                                    </div>

           
                                       <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="TextBox1">التاريخ</label>
                                        </div>
                                        <div class="col-md-9 col-sm-12 fancy-form"  id="divdate1">
                                      <asp:Label runat=server ClientIDMode="static" style="display:none" dbColumn="date" id="lbldate"></asp:Label>
                                      <asp:Label runat=server ClientIDMode="static" style="display:none" dbColumn="date_hj" id="lbldate_hj"></asp:Label>
                                     <uc1:HijriCalendar runat="server" ID="HijriCalendar1" />
         
                                        </div>
                                    </div>
                                           
                                            <br />
         

                      <%--          <asp:Label ID="from_type_id" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="from_type_id"></asp:Label>--%>
                                <br />
                                    <div class="row">
                                             <div class="col-md-3 col-sm-12">
                                            <label for="TextBox1" class="label-required">صادر من  </label>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList dbcolumn="from_dep" SkinID="form-control" CssClass="form-control" ClientIDMode="Static" ID="ddlfrom_dep" runat="server">
                                            </asp:DropDownList>
                                        </div>

                                    </div>

                                          <br />

                                <!--radio-->
                               <%-- <asp:Label ID="to_type_id" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="to_type_id"></asp:Label>--%>
                                <!--/radio $("input[name=radio-btn][value=2]").prop('checked', true); --> 
                                <br />
                                    <div class="row">
                                         <div class="col-md-3 col-sm-12">
                                            <label for="TextBox1" class="label-required">صادر الي  </label>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList dbcolumn="to_dep" SkinID="form-control" CssClass="form-control" ClientIDMode="Static" ID="ddlto_dep" runat="server">
                                            </asp:DropDownList>
                                        </div>

                                    </div>

                                        <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="TextBox1" class="label-required">نوع الخطاب </label>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                           <asp:DropDownList dbcolumn="Continue_id" SkinID="form-control" CssClass="form-control" ClientIDMode="Static" ID="ddlContinue" runat="server"></asp:DropDownList>

                                        </div>

                                    </div>
                                    <div class="row">
                                          <div class="col-md-9 col-sm-12">
                                        
                                     <asp:Label ID="lbl_replaye" ClientIDMode="Static"  runat="server"  ></asp:Label>
                                     <br />
                                    <asp:Label ID="lbl_rep" ClientIDMode="Static"  runat="server" dbcolumn="related_id" ></asp:Label>

                                    </div>
                                        </div>
                                </div>


                                <div class="col-md-6">

                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="ddlType"> تفاصيل الخطاب</label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <textarea dbcolumn="details" required runat="server" class="form-control" id="Textarea1"></textarea>
                                        </div>
                                    </div>

                                        <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="ddlType">درجة الاهمية</label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList dbcolumn="priority_id" SkinID="form-control" CssClass="form-control" ClientIDMode="Static" ID="ddlpriority" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                        <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="ddlType">درجة السرية</label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                          <asp:DropDownList dbcolumn="secrecy_id" SkinID="form-control" CssClass="form-control" ClientIDMode="Static" ID="ddlsecrecy" runat="server">
                                    </asp:DropDownList>
                                        </div>
                                    </div>

                                           <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="ddlType"> حالة الخطاب</label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                       <asp:DropDownList dbcolumn="stutse_id" SkinID="form-control" CssClass="form-control" ClientIDMode="Static" ID="ddlstutse" runat="server">
                                    </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">

                                            <label for="TextBox1">ملاحظات  </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12 fancy-form">
                                            <textarea dbcolumn="note" runat="server" class="form-control" id="txtdetails"></textarea>
                                        </div>
                                    </div>

                                            
                                            
                                            
                                       
                                    <div class="row">
                                         <br />
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