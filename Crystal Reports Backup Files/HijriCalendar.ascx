<%@ Control Language="VB" AutoEventWireup="false" CodeFile="HijriCalendar.ascx.vb" Inherits="UserControls_HijriCalendar" %>
           
<div class="">
    <div id="calendar-converter" class="divCalendar" style="z-index: 20052;">
        <input type="text" style="display:none"id="txtDatem form-control" class="txtDatem " onclick="showHideCalendar(this);" />
        <input type="text" id="txtDateh" class="txtDateh form-control" onclick="showHideCalendar(this);" />
    </div>
</div>
<script type="text/javascript">
    var cal1 = new Calendar(false, 1, true, true),
        cal2 = new Calendar(true, 0, true, true),
        cal1Mode = cal1.isHijriMode(),
        cal2Mode = cal2.isHijriMode();
    var divid = "";
    cal2.getElement().style.marginLeft = '20px';
    cal1.hide();
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
    function showHideCalendar(sender) {
        if (cal2.show()) {
            cal2.hide();
        } else {
            cal2.show();
        }
        pickedTxt = $(sender).closest("#calendar-converter").find(".txtDatem");
        pickedTxt2 = $(sender).closest("#calendar-converter").find(".txtDateh");
        divid = $(sender).closest(".fancy-form").prop("id");
        document.getElementById(divid).appendChild(cal1.getElement());
        document.getElementById(divid).appendChild(cal2.getElement());
    }
</script>