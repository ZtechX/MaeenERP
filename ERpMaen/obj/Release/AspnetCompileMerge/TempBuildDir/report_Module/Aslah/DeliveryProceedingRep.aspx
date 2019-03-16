<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DeliveryProceedingRep.aspx.vb" Inherits="ERpMaen.DeliveryProceedingRep" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script>
        function showpdf(pdfname) {
            try {
                window.location.replace("../../Report/" + pdfname + ".pdf");
            } catch (err) {
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
        </div>
    </form>
</body>
</html>
