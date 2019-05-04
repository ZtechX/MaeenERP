<%@ Page Language="vb" AutoEventWireup="false"  EnableEventValidation="false"CodeBehind="sms.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.sms" Theme="Theme5"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager  ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/sms.asmx" />
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
                    .tbl_details thead tr {
    background: #148083;
    color: #fff;
    font-size: 12px;
    font-weight: normal;
}
                    .tbl_details thead th {
                        text-align:center;
                        padding:10px;
                    }
                    .tbl_details td {
                        text-align:center;
                        padding-top:10px;
                    }
                    .tbl_details {
                        width:100%;
                            margin: .5% 1%;
                        direction:rtl;
                    }
                </style>
                <div>
                    <script src="../JS_Code/sms/sms.js"></script>
                         </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="رسائل الجوال" SkinID="page_title"></asp:Label>
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
                                            <label for="Name" class="label-required">اختر نموذج</label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList dbcolumn="" onchange="get_template();" SkinID="form-control" CssClass="form-control" ClientIDMode="Static" ID="ddltemplate" runat="server">
                                            </asp:DropDownList>


                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddltemplate"
                                                ErrorMessage="من فضلك أدخل اختر نموذج " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>


                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="TextBox1">العنوان </label>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox dbcolumn="title" required  SkinID="form-control" 
                                                ID="txttitle" runat="server" CssClass="form-control" />
                                        </div>

                                    </div>

                                    <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="email">الرسالة </label>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" TextMode="MultiLine" dbcolumn="body"  required ID="txtbody" class="form-control" runat="server">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                            <div class="row">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="TextBox1">اسم المجموعة </label>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                        <select id="ddl_type" dbcolumn="" required class="form-control"  onchange="get_group(1);">
                                      <option value="0">اختر</option>
                                      <option value="_3">المجموعات</option>
                                  </select>
                                        </div>

                                    </div>

                                            <div class="row" id="group_contact" style="display:none">
                                        <br />
                                        <div class="col-md-3 col-sm-12">
                                            <label for="TextBox1">اسم المجموعة </label>
                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                                   <asp:DropDownList dbcolumn="group_id" SkinID="form-control" onchange="get_group(2);"  CssClass="form-control" ClientIDMode="Static" ID="ddlgroup_id" runat="server">
                                    </asp:DropDownList>
                                        </div>

                                    </div>


                                 
                                </div>


                                <div class="clearfix"></div>
                            </asp:Panel>
                        </div>
                   
                    </div>
                <div class="row">
                 

                            <table id="table_sms" class="tbl_details">
                              
                      
                        <thead>
                            <tr class="row-name">

                                <th>#</th>
                                <th>الاسم </th>
                                <th>رقم التليفون</th>
                                <th style="width: 12%">تحديد</th>
                            </tr>
                        </thead>
                        <tbody id="invoices" style="text-align: center">
                        </tbody>
                        </table>
                                        <%--                    <uc1:DynamicTable runat="server" ID="DynamicTable" />--%>
                    </div>
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />
                </div>
           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>