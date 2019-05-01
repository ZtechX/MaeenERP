<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AccessDenied.aspx.vb" Inherits="ERpMaen.AccessDenied" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>غير مسموح بالدخول</title>
    
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <style>
        * {
  margin: 0;
  padding: 0;
  font-family:'Droid Arabic Kufi';
}

a {
  text-decoration: none;
}

body {
  font-weight: 600;
  color: #343434;
}

.error_section {
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  height: 100vh;
  background-image: linear-gradient(-225deg, #1A1A1A, #343434);
}
.error_section_subtitle {
  color: #25F193;
  text-transform: uppercase;
  letter-spacing: 1.5pt;
  font-weight: 600;
  font-size: 1rem;
  margin-bottom: 3em;
}
.error_section .error_title {
  --x-shadow: 0;
  --y-shadow: 0;
  --x:50%;
  --y:50%;
  font-size: 7rem;
  transition: all 0.2s ease;
  position: relative;
  padding: 2rem;
}
.error_section .error_title:hover {
  transition: all 0.2s ease;
  text-shadow: var(--x-shadow) var(--y-shadow) 10px #1A1A1A;
}

.btn {
  padding: 0.8em 1.5em;
  border-radius: 99999px;
  background-image: linear-gradient(to top, #03A9F4, #00BCD4);
  box-shadow: 0px 2px 5px 0px rgba(0, 0, 0, 0.2), inset 0px -2px 5px 0px rgba(0, 0, 0, 0.2);
  border: none;
  cursor: pointer;
  text-shadow: 0px 1px #343434;
  color: white;
  text-transform: uppercase;
  letter-spacing: 1.5pt;
  font-size: 0.8rem;
  font-weight: 700;
  transition: ease-out 0.2s all;
    margin-top: 5em;
}
.btn:hover {
  text-shadow: 0px 1px 1px #ffffff;
  transform: translateY(-5px);
  box-shadow: 0px 4px 15px 2px rgba(0, 0, 0, 0.1), inset 0px -3px 7px 0px rgba(0, 0, 0, 0.2);
  transition: ease-out 0.2s all;
}

    </style>
    <script>
        const title = $(".error_title");


//////// Light //////////
        document.onmousemove = function (e) {
    
  let x = e.pageX - window.innerWidth/2;
  let y = e.pageY - window.innerHeight/2;
  
            title.css('--x', x + 'px');
            title.css('--y', y + 'px');
}

////////////// Shadow ///////////////////
title.onmousemove = function(e) {
  let x = e.pageX - window.innerWidth/2;
  let y = e.pageY - window.innerHeight/2;

  let rad = Math.atan2(y, x).toFixed(2); 
  let length = Math.round(Math.sqrt((Math.pow(x,2))+(Math.pow(y,2)))/10); 

  let x_shadow = Math.round(length * Math.cos(rad));
  let y_shadow = Math.round(length * Math.sin(rad));

    title.css('--x-shadow', - x_shadow + 'px');
    title.css('--y-shadow', - y_shadow + 'px');

}
    </script>
</head>
<body>
     <section class="error_section">
      <p class="error_section_subtitle">لـيـس لـديـك صـلاحـيـة لـدخـول هـذة الـصـفـحـة</p>
      <h1 class="error_title">
        غير مسموح لك بالدخول
      </h1>
      <a href="Dashboard.aspx" class="btn">العودة إلى الصفحة الرئيسية</a>
    </section>
</body>
</html>
