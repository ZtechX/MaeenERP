﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="lecture_attendence_rep.aspx.vb" Inherits="ERpMaen.lecture_attendence_rep" %>

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
                <a href="diplome_money_rep.aspx">diplome_money_rep.aspx</a>
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

