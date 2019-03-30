<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Timeline.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.Timeline" Theme="Theme5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/Timeline.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
        </Services>
    </asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
           <div>
               <script src="../JS_Code/Timeline/Timeline.js"></script>
               <link href="../css/Timeline/Timeline.css" rel="stylesheet" />
            <%--  <script src="../../js/JSpdf/jspdf.min.js"></script>
             --%> 
                 <script src="https://cdnjs.cloudflare.com/ajax/libs/dom-to-image/2.6.0/dom-to-image.min.js"></script>
             
               <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>--%>

            </div>
             <asp:Label ID="case_id" runat="server" ClientIDMode="Static" Style="display: none;"></asp:Label>
            <asp:Label ID="start_dt" runat="server" ClientIDMode="Static" Style="display: none;"></asp:Label>
            <asp:Label ID="end_dt" runat="server" ClientIDMode="Static" Style="display: none;"></asp:Label>
            <asp:Label ID="done" runat="server" ClientIDMode="Static" Style="display: none;"></asp:Label>

            <style>
                .timeline li .timeline-panel {
                    padding-top: 10px !important;
                }
            </style>
            <div id="SavedivLoader" class="loader" style="display: none; text-align: center;">
                <asp:Image ID="img" runat="server" ImageUrl="../App_Themes/images/loader.gif" />
            </div>
            <button runat="server" onclick="SaveImage(); return false;" class="btn btn-info pull-left btn-lg"  style="margin-top: 5%; margin-left: 5%;">ScreenShoot</button>

            <div runat="server" id="divContainer" class="container" style="direction: rtl; margin-top: 2%; background: #fff; width: 100%; padding-left: 15%; padding-right: 15%;">
                <header class="page-header">
                    <h1>الجدول الزمنى للحالة</h1>
                </header>

                <ul id="data" class="timeline">
                </ul>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
