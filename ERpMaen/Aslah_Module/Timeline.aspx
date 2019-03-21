<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Timeline.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.Timeline" Theme="Theme5"%>
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
                <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css" />
<script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>
<script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
                <link href="../css/Timeline/Timeline.css" rel="stylesheet" />
                <script src="../JS_Code/Timeline/Timeline.js"></script>
            </div>
          <div class="container" style="direction:rtl; margin-top:2%;background:#fff;width:100%;padding-left:15%;padding-right:15%;">
  <header class="page-header">
    <h1>الجدول الزمنى للحالة</h1>
  </header>
  <asp:Label ID="case_id" runat="server" ClientIDMode="Static" style="display:none;"></asp:Label>
              <style>
                  .timeline li .timeline-panel {
                      padding-top:10px !important;
                  }
              </style>
  <ul id="data" class="timeline">

  </ul>
              </div>
 </ContentTemplate>
            </asp:UpdatePanel>
            
</asp:Content>
