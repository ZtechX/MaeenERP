﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="ERpMaen.Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>نظام معين</title>
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link href="css/BluebLogin.css" rel="stylesheet" />
    <link rel="Shortcut Icon" href="../images/favicon.png" />
    <link href="css/GridSystem.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <script src="../js/jquery-1.2.6.min.js"></script>


    <%-- Slider --%>
    <script src="../JS_Code/Public/PublicFunctions.js"></script>
      <script src="../js/customCalender/CustomerCalendar.js"></script>
    <script>
        $(function () {
            var curr_date = new Datepicker();
            $("#date_m").val(curr_date.getPickedDate().getDateString());
    curr_date.setHijriMode = true;
            $("#date_h").val(curr_date.getOppositePickedDate().getDateString());
           
        });
      
    </script>
    <script type="text/javascript">

        /*** 
            Simple jQuery Slideshow Script
            Released by Jon Raasch (jonraasch.com) under FreeBSD license: free to use or modify, not responsible for anything, etc.  Please link out to me if you like it :)
        ***/

        function slideSwitch() {
            var $active = $('#slideshow IMG.active');

            if ($active.length == 0) $active = $('#slideshow IMG:last');

            // use this to pull the images in the order they appear in the markup
            var $next = $active.next().length ? $active.next()
                : $('#slideshow IMG:first');

            // uncomment the 3 lines below to pull the images in random order

            // var $sibs  = $active.siblings();
            // var rndNum = Math.floor(Math.random() * $sibs.length );
            // var $next  = $( $sibs[ rndNum ] );


            $active.addClass('last-active');

            $next.css({ opacity: 0.0 })
                .addClass('active')
                .animate({ opacity: 1.0 }, 9000, function () {
                    $active.removeClass('active last-active');
                });
        }

        $(function () {
            setInterval("slideSwitch()", 9000);
        });
        
</script>



    <%-- Slider --%>
    <style>
        @font-face {
    font-family: DroidKufi;
    src: url(../fonts/DroidKufi-Regular.ttf);
}


p,span,h1,h2,h3,h4,h5,h6,a,lablel{ font-family:DroidKufi !important;}

        * {
margin:0;
padding:0;
outline:none;
box-sizing:border-box;
    font-family: DroidKufi !important;
}
        i {
    font-family: FontAwesome!important;
}
    </style>
</head>
<body>
   
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
        </Services>
    </asp:ScriptManager>
       <%-- <div id="slideshow">
    <img src="images/bg3.jpg" alt="Slideshow Image 1" class="active" />
    <img src="images/bg4.jpg" alt="Slideshow Image 2" />
    <img src="images/bg3.jpg" alt="Slideshow Image 1" />
    <img src="images/bg4.jpg" alt="Slideshow Image 2" />
   
</div>--%>

           <div class="container">
           
            <header>

            </header>
            <section class="main">	
                <div class="row">
                    <input type="hidden" id="date_m" runat="server" />
                    <input type="hidden" id="date_h" runat="server"/>
                    <div class="hint">
                    
<img alt="" src="images/name.png" style="width:60%;" />

                      
                    </div>
                    <div class="row">
                    <div class="col-lg-4 col-md-3 col-sm-1"></div>
                    <div class="col-lg-4 col-md-6 col-sm-10" id="login">
                        <div class="content_wrapper">
                               <div class="col-xs-12">
                                <h1><img alt="" src="images/logo1.png" style="width:140px;"/></h1>
                                   </div>
                        <div class="row">
                            <div class="col-xs-12">                                
<div class="txt_wrap">
    <i class="fa fa-phone icon"></i>
<input class="textbox" id="txtUserName" type="text"  placeholder="رقم الهوية أو رقم الجوال" required="required" onkeypress="return cust_chkNumber(event,this,10);"
                                        runat="server"  />
                            
</div>
                       <div class="txt_wrap">
                            <i class="fa fa-lock icon"></i>
<asp:TextBox  ID="txtPassword" placeholder=" كلمة المرور" required="required" 
     runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox>
                      
                        </div>
                             
<asp:Button ID="Login" runat="server" CssClass="button" Text="&#xf061;" style="font-family:FontAwesome !important" ToolTip="Start">
</asp:Button>
                            </div>
   <div>
                                    <asp:CheckBox ID="cklogin" runat="server" Text="تذكرنى " />
           <asp:Label ID="lblFail" runat="server" Visible="False" style="color:red;font-weight: bolder;font-size: 16px;"></asp:Label>

                                </div>
                             <a  href="#" runat="server" onclick="resetPassword();">إستعادة كلمة السر</a>
                        </div>
                    </div>
                        <footer>
                            <ul>   
</ul>
                        </footer>
                        </div>
                    <div class="col-lg-4 col-md-3 col-sm-1"></div>
</div>
                </div>
                			
                    <a class="hiddenanchor" id="toregister"></a>
                    <a class="hiddenanchor" id="tologin"></a>
                    <div id="wrapper" class="login_wrapper">
                       
                        
                         <div >
                        
                        </div>

                    </div>
               
            </section>


        </div>
    </form>
</body>
</html>

