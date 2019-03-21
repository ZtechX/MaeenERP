<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Dashboard.aspx.vb" Inherits="ERpMaen.Dashboard" Theme="Theme1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/jquery-1.11.2.min.js"></script>

    <link href="css/clock.css" rel="stylesheet" />
    <script type="text/javascript">
        function clock() {
            var now = new Date();
            var outStr = now.getHours() + ':' + now.getMinutes() + ':' + now.getSeconds();
            document.getElementById('clockDiv').innerHTML = outStr;
            setTimeout('clock()', 1000);
        }
        function SetDateDetails() {
            try {
                var today = new Date();
                ////////////////////////// Day Details////////////////////////////
                var weekday = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
                var dd = today.getDate();
                var dayName = weekday[today.getDay()];
                document.getElementById("day").innerHTML = dd;
                document.getElementById("dayName").innerHTML = dayName;
                ///////////////////////////////////////////////////////////////////

                ///////////////////////// Month Details////////////////////////////
                var monthNames = ["January", "February", "March", "April", "May", "June",
                  "July", "August", "September", "October", "November", "December"
                ];
                var mm = today.getMonth() + 1; //January is 0!
                var monthName = monthNames[today.getMonth()];
                document.getElementById("month").innerHTML = monthName;
                ///////////////////////////////////////////////////////////////////

                ///////////////////////// Year Details////////////////////////////
                var yyyy = today.getFullYear();
                document.getElementById("year").innerHTML = yyyy;
                /////////////////////////////////////////////////////////////////

                ///////////////////////// Time Details////////////////////////////
                var hours = today.getHours();
                var minutes = today.getMinutes();
                var ampm = hours >= 12 ? 'pm' : 'am';
                hours = hours % 12;
                hours = hours ? hours : 12; // the hour '0' should be '12'
                minutes = minutes < 10 ? '0' + minutes : minutes;
                var strTime = hours + ':' + minutes + ' ' + ampm;

                document.getElementById("time").innerHTML = strTime;
                /////////////////////////////////////////////////////////////////
            }
            catch (err) {
                alert(err);
            }
        }
        clock();

    </script>
    <script>
        $(document).ready(function () {
            $(".card-container ul").each(function () {
               
                if ($(this).html().indexOf("<li>")== -1)
                {
                    $(this).closest(".card-container").parent().hide();
                }
            });
        
            $('.collapsible-panels h2 a').click(function (e) {
                // slide down the corresponding div if hidden, or slide up if shown  
                $(this).parent().next('.collapsible-panels div').slideToggle('slow');
                // set the current item as active  
                $(this).parent().toggleClass('active');
                e.preventDefault();
            });
            // set Date details
            SetDateDetails();
        });
    </script>

    <%-- Calendar --%>
   <%-- <script src="http://jquery-ui.googlecode.com/svn/trunk/ui/i18n/jquery.ui.datepicker-sv.js"></script>--%>
    <!-- Remove the code above for standard Regional Format -->
    <link href="css/dashboard-calandar.css" rel="stylesheet" />
    <script>
        $(function () {
            $("#datepicker").datepicker();
        });

    </script>
    <%-- Calendar --%>

    <style>
  
        #Stats {
            padding: 20px 0;
        }

        .graybg {
            min-height: 600px;
            padding: 15px;
        }

        /*Dashboard*/

        .checkbox input[type="checkbox"] {
            cursor: pointer;
            opacity: 0;
            z-index: 1;
            outline: none !important;
        }

        .checkbox label::before {
            -o-transition: .3s ease-in-out;
            -webkit-transition: .3s ease-in-out;
            background-color: #fff;
            border-radius: 3px;
            border: 1px solid #ccc;
            content: "";
            display: inline-block;
            height: 17px;
            left: 0;
            margin-left: -20px;
            position: absolute;
            transition: .3s ease-in-out;
            width: 17px;
            outline: none !important;
        }

        .checkbox-primary input[type="checkbox"]:checked + label::before {
            background-color: #3F9CD2;
            border-color: #3F9CD2;
        }

        .checkbox-primary input[type="checkbox"]:checked + label::after {
            color: #fff;
        }

        .checkbox label {
            display: inline-block;
            padding-left: 15px !important;
            position: relative;
            float: left;
            font-weight: bold !important;
            color: #036197;
            width: 100%;
        }

        .radio input[type="radio"], .radio-inline input[type="radio"], .checkbox input[type="checkbox"], .checkbox-inline input[type="checkbox"] {
            position: absolute;
            margin-left: -20px;
            margin-top: 4px \9;
        }

            .checkbox input[type="checkbox"]:checked + label::after {
                content: "\f00c";
                font-family: 'FontAwesome';
            }

        .checkbox label::after {
            color: #555;
            display: inline-block;
            font-size: 11px;
            height: 16px;
            left: 0;
            margin-left: -20px;
            padding-left: 3px;
            padding-top: 1px;
            position: absolute;
            top: 0;
            width: 16px;
        }

        .checkbox span {
            float: left;
            padding-left: 20px;
            color: #838383;
        }

        .checkbox p {
            width: 100%;
            width: 100%;
            float: left;
            padding-left: 20px;
        }

        .list-group-item {
            position: static !important;
            position: relative;
            display: block;
            float: left;
            width: 100%;
            padding: 2px 15px;
            margin-bottom: -1px;
            background-color: #fff;
            border: 0px !important;
        }

        .form-control {
            border-radius: 1px !important;
            height: 30px !important;
        }

        .btn-primary {
            margin: 0px !important;
            padding: 6px 10px !important;
        }

        .colmnpadding .col-md-3 {
            padding: 0 6px;
        }

        .nav-tabs > li.active > a, .nav-tabs > li.active > a:hover, .nav-tabs > li.active > a:focus {
            box-shadow: 0px 3px 0px #3C96D2 inset !important;
        }

        .list-group {
            overflow: inherit !important;
        }

        .min-height-more {
            height: 250px;
    overflow: auto;
        }

        #clock_tab .tab-content > .active {
            border: 0px;
        }

        #clock_tab .tab-content {
            border: 0px;
            min-height: 261px;
        }

        #clock_tab .nav > li > a {
            color: #404040;
        }

        #clock_tab .nav > li > a {
            color: #404040;
            box-shadow: none !important;
            border: 0px;
            padding: 5px 17px;
        }

        #clock_tab .active a {
            color: #04C39E;
        }

        #clock_tab {
            min-height: 294px;
        }
    </style>
    <%--/////////////////////////--%>
    <style>
        @import url('https://fonts.googleapis.com/css?family=Arima+Madurai:100,200,300,400,500,700,800,900');

        .btn:hover,
        .btn:focus,
        .btn:active {
            outline: 0 !important;
        }
        /* entire container, keeps perspective */
        .card-container {
            -webkit-perspective: 800px;
            -moz-perspective: 800px;
            -o-perspective: 800px;
            perspective: 800px;
            margin-bottom: 30px;
        }
            /* flip the pane when hovered */
            .card-container:not(.manual-flip):hover .card,
            .card-container.hover.manual-flip .card {
                -webkit-transform: rotateY( 180deg );
                -moz-transform: rotateY( 180deg );
                -o-transform: rotateY( 180deg );
                transform: rotateY( 180deg );
            }


            .card-container.static:hover .card,
            .card-container.static.hover .card {
                -webkit-transform: none;
                -moz-transform: none;
                -o-transform: none;
                transform: none;
            }
        /* flip speed goes here */
        .card {
            -webkit-transition: -webkit-transform .5s;
            -moz-transition: -moz-transform .5s;
            -o-transition: -o-transform .5s;
            transition: transform .5s;
            -webkit-transform-style: preserve-3d;
            -moz-transform-style: preserve-3d;
            -o-transform-style: preserve-3d;
            transform-style: preserve-3d;
            position: relative;
        }

        /* hide back of pane during swap */
        .front, .back {
            -webkit-backface-visibility: hidden;
            -moz-backface-visibility: hidden;
            -o-backface-visibility: hidden;
            backface-visibility: hidden;
            position: absolute;
            top: 0;
            left: 0;
            background-color: #FFF;
            box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.14);
        }

        /* front pane, placed above back */
        .front {
            z-index: 2;
        }

        /* back, initially hidden pane */
        .back {
            -webkit-transform: rotateY( 180deg );
            -moz-transform: rotateY( 180deg );
            -o-transform: rotateY( 180deg );
            transform: rotateY( 180deg );
            z-index: 3;
        }

            .back .btn-simple {
                position: absolute;
                left: 0;
                bottom: 4px;
            }
        /*        Style       */


        .card {
            background: none repeat scroll 0 0 #FFFFFF;
            border-radius: 4px;
            color: #444444;
        }

        .card-container, .front, .back {
            width: 100%;
            height: 250px;
            border-radius: 4px;
            -webkit-box-shadow: 0px 0px 19px 0px rgba(0,0,0,0.16);
            -moz-box-shadow: 0px 0px 19px 0px rgba(0,0,0,0.16);
            box-shadow: 0px 0px 19px 0px rgba(0,0,0,0.16);
        }

        .card .cover {
            height: 105px;
            overflow: hidden;
            border-radius: 4px 4px 0 0;
        }

            .card .cover img {
                width: 100%;
            }

        .card .user {
            border-radius: 50%;
            display: block;
            height: 120px;
            margin: -55px auto 0;
            overflow: hidden;
            width: 120px;
        }

            .card .user img {
                background: none repeat scroll 0 0 #FFFFFF;
                border: 4px solid #FFFFFF;
                width: 100%;
            }

        .card .content {
            background-color: rgba(0, 0, 0, 0);
            box-shadow: none;
            padding: 10px 20px 20px;
        }

            .card .content .main {
                min-height: 160px;
            }

        .card .back .content .main {
            height: 215px;
        }

        .card .name {
            font-family: 'Arima Madurai', cursive;
            font-size: 22px;
            line-height: 28px;
            margin: 10px 0 0;
            text-align: center;
            text-transform: capitalize;
        }

        .card h5 {
            margin: 5px 0;
            font-weight: 400;
            line-height: 20px;
        }

        .card .profession {
            color: #999999;
            text-align: center;
            margin-bottom: 20px;
        }

        .card .footer {
            border-top: 1px solid #EEEEEE;
            color: #999999;
            margin: 30px 0 0;
            padding: 10px 0 0;
            text-align: center;
        }

            .card .footer .social-links {
                font-size: 18px;
            }

                .card .footer .social-links a {
                    margin: 0 7px;
                }

            .card .footer .btn-simple {
                margin-top: -6px;
            }

        .card .header {
            padding: 15px 20px;
            height: 90px;
        }

        .card .motto {
            font-family: 'Arima Madurai', cursive;
            border-bottom: 1px solid #EEEEEE;
            color: #999999;
            font-size: 14px;
            font-weight: 400;
            padding-bottom: 10px;
            text-align: center;
        }

        .card .stats-container {
            width: 100%;
            margin-top: 50px;
        }

        .card .stats {
            display: block;
            float: left;
            width: 33.333333%;
            text-align: center;
        }

            .card .stats:first-child {
                border-right: 1px solid #EEEEEE;
            }

            .card .stats:last-child {
                border-left: 1px solid #EEEEEE;
            }

            .card .stats h4 {
                font-family: 'Arima Madurai', cursive;
                font-weight: 300;
                margin-bottom: 5px;
            }

            .card .stats p {
                color: #777777;
            }
        /*      Just for presentation        */

        .title {
            color: #506A85;
            text-align: center;
            font-weight: 300;
            font-size: 44px;
            margin-bottom: 90px;
            line-height: 90%;
        }

            .title small {
                font-size: 17px;
                color: #999;
                text-transform: uppercase;
                margin: 0;
            }

        .space-30 {
            height: 30px;
            display: block;
        }

        .space-50 {
            height: 50px;
            display: block;
        }

        .space-200 {
            height: 200px;
            display: block;
        }

        .white-board {
            background-color: #FFFFFF;
            min-height: 200px;
            padding: 60px 60px 20px;
        }

        .ct-heart {
            color: #F74933;
        }

        pre.prettyprint {
            background-color: #ffffff;
            border: 1px solid #999;
            margin-top: 20px;
            padding: 20px;
            text-align: left;
        }

        .atv, .str {
            color: #05AE0E;
        }

        .tag, .pln, .kwd {
            color: #3472F7;
        }

        .atn {
            color: #2C93FF;
        }

        .pln {
            color: #333;
        }

        .com {
            color: #999;
        }

        .btn-simple {
            opacity: .8;
            color: #666666;
            background-color: transparent;
        }

            .btn-simple:hover,
            .btn-simple:focus {
                background-color: transparent;
                box-shadow: none;
                opacity: 1;
            }

            .btn-simple i {
                font-size: 16px;
            }

        .navbar-brand-logo {
            padding: 0;
        }

            .navbar-brand-logo .logo {
                border: 1px solid #333333;
                border-radius: 50%;
                float: left;
                overflow: hidden;
                width: 60px;
            }

        .navbar .navbar-brand-logo .brand {
            color: #FFFFFF;
            float: left;
            font-size: 18px;
            font-weight: 400;
            line-height: 20px;
            margin-left: 10px;
            margin-top: 10px;
            width: 60px;
        }

        .navbar-default .navbar-brand-logo .brand {
            color: #555;
        }


        /*       Fix bug for IE      */

        @media screen and (-ms-high-contrast: active), (-ms-high-contrast: none) {
            .front, .back {
                -ms-backface-visibility: visible;
                backface-visibility: visible;
            }

            .back {
                visibility: hidden;
                -ms-transition: all 0.2s cubic-bezier(.92,.01,.83,.67);
            }

            .front {
                z-index: 4;
            }

            .card-container:not(.manual-flip):hover .back,
            .card-container.manual-flip.hover .back {
                z-index: 5;
                visibility: visible;
            }
        }
    </style>
    <script>
        $().ready(function () {
            $('[rel="tooltip"]').tooltip();

        });

        function rotateCard(btn) {
            var $card = $(btn).closest('.card-container');
            console.log($card);
            if ($card.hasClass('hover')) {
                $card.removeClass('hover');
            } else {
                $card.addClass('hover');
            }
        }
    </script>
    <link href="http://netdna.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Arima+Madurai:100,200,300,400,500,700,800,900" rel="stylesheet">
   <%-- <script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js"></script>--%>
</asp:Content>
<asp:Content ID="Dashboard" ContentPlaceHolderID="content" runat="Server">

    <asp:ScriptManager  ID="ToolkitScriptManager1" runat="server">
        <Scripts>
           <%-- <asp:ScriptReference Path="~/JS_Code/Dashboard.js" />--%>
        </Scripts>
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/Testwebservice.asmx" />
            <%--<asp:ServiceReference Path="~/ASMX_WebServices/Dashboard.asmx" />--%>
            <asp:ServiceReference Path="~/ASMX_WebServices/SMS.asmx" />
        </Services>
    </asp:ScriptManager>

    <div class="graybg row margin-top-nav">
        <div class="pad-top-10 col-md-12" >
        <div class="pad-top-10 col-md-4">
            <div class=" col-sm-12 col-md-12">
                <div id="User" class="divcol bounceIn animated">
                    <div class="collapsible-panels">
                        <h2>
                            <a href="#">المستخدم</a>
                        </h2>
                        <div class="min-height-more">
                           <div style="padding: 10px;" dir="rtl"  >
  <div style="display:block;">
        <center>
           <asp:Image ID="imgUser1" ImageUrl="~/images/user_icon.png" runat="server" Width="100px" Height="100px" />
           </center>
             </div>    
         <table   style="width:100% ; margin-top:10px;">
            
            <tr>
                <td style="font-weight:bold;">البريد الالكترونى </td>
                <td>
                    <div class="userInfo">
                        <a id="lblLink" runat="server"></a>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="font-weight:bold;">
                    <label style="margin-top:5px;">الاسم</label></td>
                <td>
                    <%--<img src="Forms/agent.png" class="icon" />--%>
                    <asp:Label ID="lblUserName" runat="server" ></asp:Label>
                </td>
            </tr>
        </table>
        
        
    
    </div>
                        </div>
                    </div>
                </div>
            </div>

           <%-- <div class="col-sm-12 col-md-4" >
                <div id="" class="divcol bounceIn animated">
                    <div class="collapsible-panels">
                        <h2>
                            <a href="#">روابط سريعة</a>
                        </h2>
                        <div class="min-height-more">
                            <ul class="links-list">
                                <li>
                                    <a id="lbExpiredRentContracts" href="Work_Module/stors.aspx" target="_blank">المستودعات
                                      <span style="float: left;" class="highlight"></span>
                                    </a>
                                </li>

                                <li>
                                    <a id="lbMyContacts" href="Work_Module/cupbord.aspx" target="_blank">الدواليب
                                      <span style="float: left;" class="highlight"></span>
                                    </a>
                                </li>
                                <li>
                                    <a id="A1" href="Work_Module/rack.aspx" target="_blank">الارفف
                                      <span style="float: left;" class="highlight"></span>
                                    </a>
                                </li>
                                <li>
                                    <a id="A2" href="Work_Module/rack.aspx" target="_blank">اللأصناف
                                      <span style="float: left;" class="highlight"></span>
                                    </a>
                                </li>
                                <li>
                                    <a id="A3" href="Work_Module/transfer.aspx" target="_blank">حركة الأصناف
                                      <span style="float: left;" class="highlight"></span>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
          --%>
              <div class="col-sm-12 col-md-12">
                <div id="Div5" class="divcol bounceIn animated">
                    <div class="collapsible-panels" id="clock_tab">
                        <!-- Nav tabs -->


                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane active" id="home">
                                <div class="profile-nav alt">
                                    <section class="">
                                        <div class="user-heading alt clock-row terques-bg">
                                            <div id="clockDiv"></div>
                                            <h1><span id="month"></span><span id="day"></span></h1>
                                            <p class="text-left"><span id="year"></span>, <span id="dayName"></span></p>
                                            <p class="text-left"><span id="time"></span></p>
                                        </div>
                                        <ul id="clock">
                                            <li id="sec" style="transform: rotate(330deg);"></li>
                                            <li id="hour" style="transform: rotate(281deg);"></li>
                                            <li id="min" style="transform: rotate(132deg);"></li>
                                        </ul>



                                    </section>

                                </div>
                            </div>
                            <div role="tabpanel" class="tab-pane" id="profile">


                                <div id="datepicker"></div>
                            </div>

                        </div>
                        <ul class="nav nav-tabs" role="tablist">
                            <li role="presentation" class="active"><a href="#home" aria-controls="home" role="tab" data-toggle="tab">الساعة</a></li>
                            <li role="presentation"><a href="#profile" aria-controls="profile" role="tab" data-toggle="tab">التقويم</a></li>

                        </ul>


                    </div>
                </div>

                <div>
                </div>


            </div>

        </div>
           <div class="marg_top_10 col-md-8 colmnpadding">

            <div class="col-sm-12 col-md-3">

                <div class="card-container">
                    <div class="card">
                        <div class="front" style="background-color: #61b8ea;">
                            <h1 style="font-size: 80px;"><i class="fa fa-cogs"></i></h1>
                            <h1 class="">الإعدادات</h1>

                        </div>
                        <!-- end front panel -->
                        <div class="back">
                            <div class="main">
                                <div class="min-height-more">
                                    <ul class="links-list" id="settingsUL" runat="server" style="text-align: center;">
                                     
                                    </ul>
                                </div>
                            </div>


                            <!-- end back panel -->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end card-container -->

                </div>
            </div>
            <div class="col-sm-12 col-md-3">

                <div class="card-container">
                    <div class="card">
                        <div class="front" style="background-color: #55b7a3;">
                            <h1 style="font-size: 80px;"><i class="fa fa-users"></i></h1>
                            <h1 class="">مجالس الجمعية</h1>

                        </div>
                        <!-- end front panel -->
                        <div class="back">

                            <div class="main">
                                <div class="min-height-more">
                                    <ul class="links-list" id="bordsUL" runat="server"  style="text-align: center;">
                                   
                                     
                                    </ul>
                                </div>
                            </div>


                            <!-- end back panel -->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end card-container -->

                </div>
            </div>

            <div class="col-sm-12 col-md-3">

                <div class="card-container">
                    <div class="card">
                        <div class="front" style="background-color: #09c0c5;">
                            <h1 style="font-size: 80px;"><i class="fa fa-handshake-o"></i></h1>
                            <h1 class="">الإصلاح الاسرى</h1>

                        </div>
                        <!-- end front panel -->
                        <div class="back">

                            <div class="main">
                                <div class="min-height-more">
                                    <ul class="links-list" id="FamilyConcUL" runat="server" style="text-align: center;">
                                  
                                    </ul>
                                </div>
                            </div>


                            <!-- end back panel -->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end card-container -->

                </div>
            </div>
            <div class="col-sm-12 col-md-3">

                <div class="card-container">
                    <div class="card">
                        <div class="front" style="background-color: #C09C67;">
                            <h1 style="font-size: 80px;"><i class="fa fa-envelope-o"></i></h1>
                            <h1 class="">رسائل SMS</h1>

                        </div>
                        <!-- end front panel -->
                        <div class="back">

                            <div class="main">
                                <div class="min-height-more">
                                    <ul class="links-list" id="SMSUL" runat="server" style="text-align: center;">
                                       
                                    </ul>
                                </div>
                            </div>


                            <!-- end back panel -->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end card-container -->

                </div>
            </div>
        
            <div class="col-sm-12 col-md-3">

                <div class="card-container">
                    <div class="card">
                        <div class="front" style="background-color: #618fea;">
                            <h1 style="font-size: 80px;"><i class="fa fa-graduation-cap" aria-hidden="true"></i></h1>
                            <h1 class="">الإكاديمية</h1>

                        </div>
                        <!-- end front panel -->
                        <div class="back">

                            <div class="main">
                                <div class="min-height-more">
                                    <ul class="links-list" id="AcadymicUL" runat="server" style="text-align: center;">
                               
                                    </ul>
                                </div>
                            </div>


                            <!-- end back panel -->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end card-container -->

                </div>
            </div>
               <div class="col-sm-12 col-md-3">

                <div class="card-container">
                    <div class="card">
                        <div class="front" style="background-color: #61b8ea;">
                            <h1 style="font-size: 80px;"><i class="fa fa-cogs"></i></h1>
                            <h1 class="">الاتصالات الإدارية</h1>

                        </div>
                        <!-- end front panel -->
                        <div class="back">
                            <div class="main">
                                <div class="min-height-more">
                                    <ul class="links-list" id="ManagementCallsUl" runat="server" style="text-align: center;">
                                     
                                    </ul>
                                </div>
                            </div>


                            <!-- end back panel -->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end card-container -->

                </div>
            </div>
              
           <div class="col-sm-12 col-md-3">

                <div class="card-container">
                    <div class="card">
                        <div class="front" style="background-color: #55b7a3;">
                            <h1 style="font-size: 80px;"><i class="fa fa-flag"></i></h1>
                            <h1 class="">التقارير</h1>

                        </div>
                        <!-- end front panel -->
                        <div class="back">

                            <div class="main">
                                <div class="min-height-more">
                                    <ul class="links-list" id="ReportUL" runat="server"  style="text-align: center;">
                                   
                                     
                                    </ul>
                                </div>
                            </div>


                            <!-- end back panel -->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end card-container -->

                </div>
            </div>
               </div>

             </div>
          </div>
            <div class="marg_top_10 col-md-12 colmnpadding">

              </div>
        <script src="js/css3clock.js"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="content2" runat="Server">
</asp:Content>
