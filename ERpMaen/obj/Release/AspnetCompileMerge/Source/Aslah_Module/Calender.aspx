<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Calender.aspx.vb" Inherits="ERpMaen.test" MasterPageFile="~/Site.Master" %>
<%@ Register Src="~/UserControls/DeualCalendar1.ascx" TagPrefix="uc1" TagName="DeualCalendar1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Src="~/UserControls/Person.ascx" TagPrefix="uc1" TagName="Person" %>--%>
<%@ Register Src="~/UserControls/DeliveryDate.ascx" TagPrefix="uc1" TagName="DeliveryDate" %>
<%@ Register Src="~/UserControls/Appraisal.ascx" TagPrefix="uc1" TagName="Appraisal" %>
    

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
        <style>.w3-bar-item input[type=radio],.w3-bar-item label{cursor:pointer}</style>
      <link rel='stylesheet' href='https://ZulNs.github.io/w3css/w3.css'/>
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/cases.asmx" />
           <asp:ServiceReference Path="~/ASMX_WebServices/Testwebservice.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/MultiFileUploader.asmx" />
        </Services>
    </asp:ScriptManager>
      <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="update-progress02">
                <asp:UpdateProgress ID="upg" runat="server" AssociatedUpdatePanelID="up">
                    <ProgressTemplate>
                        <asp:Image ID="imgProgress" runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
  

<div class="w3-main" style="margin-top:60px">
	<div class="w3-container w3-padding w3-teal">
		<button class="w3-button w3-ripple w3-xlarge w3-hide-large w3-left" style="margin:6px 8px 0px -16px" onclick="openSidebar()">
			<svg width="18" height="23"><path d="M0 6L18 6L18 8L0 8Z M0 13L18 13L18 15L0 15Z M0 20L18 20L18 22L0 22Z"/></svg>
		</button>
	</div>
    <uc1:DeliveryDate runat="server" ID="DeliveryDate1" />
    <uc1:Appraisal runat="server" ID="Appraisal1" />
	<div id="calendar"     class="w3-container w3-margin-top"></div>

  
             <script src="../js/hijri-date1.js"></script>
             <script src="../js/calendar1.js"></script>
         <script src="../js_code/cases/cases.js"></script>
    <script type="text/javascript">
        var date_m = "";
        var date_hj = "";
        var month = 0;
        $("#calendar").click(function () {
            return false;

        });
   
      //          conver_dates($(".w3-xxlarge").html())
      //    var year  = $(".w3-xxxlarge").html().replace("AD", "");
      //          var newdate = day + "/" + month + "/" + year;
      //             var dateParts = new Date((Number(newdate.split("/")[2])), (Number(newdate.split("/")[1]) - 1), (Number(newdate.split("/")[0])));
      //          var date_hj= kuwaiticalendar(dateParts)
      //          alert(newdate);
      //          $("#lbldelivery_date_m").html(newdate)
      //          $("#lbldelivery_date_h").html(date_hj)
      //          alert(date_hj);
      //          $(this).css("color","red")

                
      //$("#receiving_delivery_details").dialog({
      //    width: "600px",
      //          });
            
      //          $('.w3-xlarge').off('click')
      //      });
            
      //    $('.calendar').off('click')
      //  });
      //  function find_row(evt) {
      //      console.log(evt);
      //  }
        //$(function () {
        // $("#calendar").click(function(callback) {

        //         callback();
          
        //});
        //      $(".w3-xlarge").click(function () {
        //        console.log(cal);
        //        alert($(this).html());
        //        alert($(".w3-xxlarge").html());
        //        alert($(".w3-xxxlarge").html());

        //});



        function conver_dates(ele) {
            if (ele == "March") {

                month = '03';
            } else if (ele == "April") {
                month = '04';
            }else if (ele == "May") {
                month = '05';
            }else if (ele == "June") {
                month = '06';
            }else if (ele == "July") {
                month = '07';
            }else if (ele == "August") {
                month = '08';
            }else if (ele == "September") {
                month = '09';
            }else if (ele == "October") {
                month = '10';
            }else if (ele == "November") {
                month = '11';
            }else if (ele == "December") {
                month = '12';
            }else if (ele == "January") {
                month = '01';
            }else if (ele == "February") {
                month = '02';
            }

         
        }
     
    </script>
    <style>
        .w3-display-container {
            background-color:#4DB4B6 !important;
            color:white !important;
        }
        .w3-dark-grey {
                background-color: #4DB4B6!important;

        }
       .w3-round-large {
                margin-right: 3px;
                    width: 14% !important;
                    margin-bottom:3px;
        }
    </style>
</div>
      <script>
          'use strict';
    
let cal=new Calendar(true);
          cal.attachTo(document.getElementById('calendar'));
          cal.setLanguage('ar')
          cal.setFirstDayOfWeek(0);
        
function openSidebar(){
	document.getElementById("mySidebar").style.display = "block"
}

function closeSidebar(){
	document.getElementById("mySidebar").style.display = "none"
}

function dropdown(el){
	if(el.className.indexOf('expanded')==-1){
		el.className=el.className.replace('collapsed','expanded');
	}else{
		el.className=el.className.replace('expanded','collapsed');
	}
}

          function selectLang(el) {

	el.children[0].checked=true;
	cal.setLanguage(el.children[0].value)
}

function setYear(){
	let el=document.getElementById('valYear');
	cal.setFullYear(el.value)
}

function setMonth(){
	let el=document.getElementById('valMonth');
	cal.setMonth(el.value)
}

function setTheme(){
	let el=document.getElementById('txtTheme');
	let n=parseInt(el.value);
	if(!isNaN(n))cal.setTheme(n);
	else cal.setTheme(el.value)
}

function setTodayTimeout(){
	let el=document.getElementById('valTimeout');
	cal.setTodayTimeout(el.value)
          }
// selectLang('ar');
    </script>
  </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
