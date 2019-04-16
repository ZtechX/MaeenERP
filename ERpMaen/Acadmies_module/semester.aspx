<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="semester.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.semester" Theme="Theme5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/semesterCls.asmx" />
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
                    <script src="../JS_Code/acadmies/semester.js"></script>
                     <script type="text/javascript">
                      
                    </script>
                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="الفصول الدراسية" SkinID="page_title"></asp:Label>
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
                               
                                  <div class="col-md-12">
                                    
                                    <div class=" row form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" class="label-required"> اسم الفصل الدراسى</label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <asp:TextBox SkinID="form-control" class="form-control" dbColumn="name" ClientIDMode="Static" ID="Name" runat="server">
                                            </asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Name"
                                                ErrorMessage="من فضلك أدخل اسم الفصل الدراسى " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>

                                  <div class="row form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label> السنة الدراسية</label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <asp:DropDownList dbcolumn="year_id" class="form-control" ClientIDMode="Static" ID="ddlyear" runat="server">
                                    </asp:DropDownList>
                                    <br />
                                </div>
                            </div>

                                      

                                      </div>

                                  <div class="col-md-6">

                                    <div class=" row form-group">
                                     <div class="col-md-3 col-sm-12">
                                         <label class="label-required">
                                             تاريخ البداية  
                                         </label>
                                     </div>

                                     <div class="col-md-9 col-sm-12" >

                                         <div class="fancy-form" id="divdate1" style="float:right" >
                                             <input dbcolumn="start_date_m" type="hidden" id="start_date_m" />
                                             <input dbcolumn="start_date_hj" type="hidden" id="start_date_hj" />
                                             <uc1:hijricalendar runat="server" id="HijriCalendar" />
                                         </div>

                                     </div>
                                 </div>

                                        </div>
                                
                                 <div class="col-md-6">
                                 <div class="form-group">
                                     <div class="col-md-3 col-sm-12">
                                         <label class="label-required">
                                             تاريخ النهاية  
                                         </label>
                                     </div>

                                     <div class="col-md-9 col-sm-12">

                                         <div class="fancy-form" id="divdate2">
                                             <input dbcolumn="end_date_m" type="hidden" id="end_datem" />
                                             <input dbcolumn="end_date_hj" type="hidden" id="end_datehj" />
                                             <uc1:hijricalendar runat="server" id="HijriCalendar1" />
                                         </div>
                                         <br />
                                     </div>
                                 </div>

                         </div>
                               

                            
                               <%-- <div class="clearfix"></div>--%>
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
