<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm2.aspx.vb" Inherits="ERpMaen.WebForm2" %>
<%@ Register Src="~/UserControls/MultiFileUpload.ascx" TagPrefix="uc1" TagName="MultiFileUpload" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-2.1.4.min.js"></script>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
           <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/MultiFileUploader.asmx" />
        </Services>
    </asp:ScriptManager>
        <div>
            <uc1:MultiFileUpload runat="server" id="MultiFileUpload" />
        </div>
    </form>
</body>
</html>
