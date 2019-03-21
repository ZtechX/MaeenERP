<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FollowCasesRep.aspx.vb" Inherits="ERpMaen.FollowCasesRep" %>

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
           
        </div>
    </form>
</body>
</html>
