<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="orders.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.orders" Theme="Theme5"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="result" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/orders.asmx" />
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

                <div>
                    <script src="../JS_Code/orders/orders.js"></script>
                    <script src="../js/customCalender/CustomerCalendar.js"></script>
                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="طلبات التاجيل والإلغاء" SkinID="page_title"></asp:Label>
                    </div>
                    <label id="userloginType" runat ="server" style="display:none;" />
                 
                    <uc1:result runat="server" id="res1"/>
                    <uc1:DynamicTable runat="server" ID="DynamicTable" />
                    <asp:HiddenField ID="tblH" runat="server" />
                </div>
            </div>
             <div class="collapse" id="newDate" dir="rtl" style="display :none;">
                         <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        التاريخ الجديد   </lable>
                                                                    </div>

                                                                    <div class="col-md-6 col-sm-12">
                                                                        <div class="fancy-form" id="date_div">
                                                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar2" />
                                                                       </div> 
                                                                    </div>
                             
                                                                </div>
                  <div class="col-md-12 form-group ">
                           <button onclick="saveNewDate()" style="font-family: DroidKufi !important;" class="btn btn-success pull-left">حفظ</button>
        
                  </div>             

                      </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
