<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DynamicTable.ascx.vb" Inherits="ERpMaen.DynamicTable" %>
<div id="divLoader" class="loader" style="display: none">
    <asp:Image ID="img" runat="server" ImageUrl="~/Images/ajax-loader.gif" />
</div>

<div class="col-md-12 pad10" id="tableDiv">
</div>