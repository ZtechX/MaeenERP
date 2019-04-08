<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Notification.aspx.vb" MasterPageFile="~/Site.Master"  Inherits="ERpMaen.Notification1" Theme="Theme5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>


<asp:Content runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
        </Services>
    </asp:ScriptManager>

    <asp:UpdatePanel ID="up" runat="server">

        <ContentTemplate>
          <uc1:DynamicTable runat="server" ID="DynamicTable" />

          <div>
              <script>
                  $(function () {
                       form_load();
                  });
                  function resetAll() { }
                  function drawDynamicTable() {
    try {
        var tableSortingColumns = [
            { orderable: false }, null, null, null, null,null
        ];
        var tableFilteringColumns = [
            { type: "null" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }, { type: "text" }
        ];

        var tableColumnDefs = [

        ];
        var initialSortingColumn = 0;
        loadDynamicTable('Notification', "AutoCodeHide", tableColumnDefs, tableFilteringColumns, tableSortingColumns, initialSortingColumn, "Form");
    } catch (err) {
        alert(err);
    }
}


              </script>
          </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
