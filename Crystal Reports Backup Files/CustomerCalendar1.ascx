<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CustomerCalendar.ascx.vb" Inherits="CustomerCalendar" %>
           
<div class="">
    <div id="calendar-converter" class="divCalendar" style="z-index: 20052;">
        <label>الميلادى</label>
       <input type="text" class="txtDatem form-control " id="txtDatem" onclick="showHideCalendar(this);" />
       <label>الهجرى</label>
         <input type="text" id="txtDateh" class="txtDateh form-control" onclick="showHideCalendar(this);" />
    </div>
</div>
<script type="text/javascript">
    var cal1 = new Calendar(false, 1, true, true),
        cal2 = new Calendar(true, 0, true, true),
        cal1Mode = cal1.isHijriMode(),
        cal2Mode = cal2.isHijriMode();
    var divid = "";
    cal1.getElement().style.marginLeft = '20px';
    cal2.getElement().style.marginLeft = '20px';
    
    cal2.callback = function () {
        if (cal2Mode !== cal2.isHijriMode()) {
            cal1.disableCallback(true);
            cal1.changeDateMode();
            cal1.disableCallback(false);
            cal1Mode = cal1.isHijriMode();
            cal2Mode = cal2.isHijriMode();
        }
        else {
            cal1.setTime(cal2.getTime());
            $(pickedTxt).val(cal1.getDate().getDateString());
            $(pickedTxt2).val(cal2.getDate().getDateStringHigri()); 
        }
        
    };
    cal1.callback = function () {
        if (cal2Mode !== cal2.isHijriMode()) {
            cal1.disableCallback(true);
            cal1.changeDateMode();
            cal1.disableCallback(false);
            cal1Mode = cal1.isHijriMode();
            cal2Mode = cal2.isHijriMode();
        }
        else {
            cal2.setTime(cal1.getTime());
            $(pickedTxt).val(cal1.getDate().getDateString());
            $(pickedTxt2).val(cal2.getDate().getDateStringHigri());
        }
    };
    function showHideCalendar(sender) {
        
        if ($(sender).prop("id") == "txtDateh") {
            cal1.hide();
            if (cal2.show()) {
                cal2.hide();
            } else {
                cal2.show();
            }
        } else {
            cal2.hide();
            if (cal1.show()) {
                cal1.hide();
            } else {
                cal1.show();
            }
        }

        pickedTxt = $(sender).closest("#calendar-converter").find(".txtDatem");
        pickedTxt2 = $(sender).closest("#calendar-converter").find(".txtDateh");
        divid = $(sender).closest(".fancy-form").prop("id");
        document.getElementById(divid).appendChild(cal1.getElement());
        document.getElementById(divid).appendChild(cal2.getElement());
    }
</script>