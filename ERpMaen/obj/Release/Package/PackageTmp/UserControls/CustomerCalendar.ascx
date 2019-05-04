<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CustomerCalendar.ascx.vb" Inherits="ERpMaen.CustomerCalendar" %>
 <link href="../css/cases/w3.css" rel="stylesheet" />
<div id= "calendar-converter" class="divCalendar" style="z-index: 20052;">
<label for="txtDatem">الميلادى</label>
<input id="txtDatem" onclick="pickDate(event)" class="form-control" type="text" readonly>

<label for="txtDateh">الهجرى</label>
<input id="txtDateh" onclick="pickDate(event)" class="form-control" type="text"readonly>
</div>