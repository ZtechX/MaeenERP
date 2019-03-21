$(function () {
    
    Timeline.get_data($("#case_id").html(), function (val) {
        
        var data_case = [];
        var data = [];
        var data_recieveChildren = [];
        var data_sessionChildren = [];
        if (val[0] != "") {
             data_case = JSON.parse(val[0]);
        }
        if (val[1] != "") {
            data = JSON.parse(val[1]);
        }
        if (val[2] != "") {
            data_recieveChildren = JSON.parse(val[2]);
        }
        //if (val[3] != "") {
        //    data_sessionChildren = JSON.parse(val[3]);
        //}
        var count = 0;
        var group = "";
        var Newgroup = "";
        var divGroup = "";
        var header = "";
        var paragraph = "";
        var date = "";
        var div_alternative = "";
        
        for (var i = 0; i < data.length;i++) {
                Newgroup = "";
                divGroup = "";
                header = "";
                paragraph = "";
                date = "";
                div_alternative = "";
            if (data[i].Tabel == "Session") {
                    var session_date_m = data[i].date_m;
                    Newgroup = getGroup(session_date_m);
                        header = "جلسة تهيئة وتدرج";
                paragraph = "جلسة رقم : " + data[i].code + "<br/>طالب التنفيذ : " + data[i].owner +
                    "<br/>المنفذ ضده : " + data[i].agains + "<br/>مكان الجلسة : " + data[i].place +
                    "<br/>وقت الدخول : " + data[i].entry_time + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;وقت الخروج : " + data[i].exite_time+
                    "<br/>النتيجة : " + data[i].result;
                        date = " "+session_date_m + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + data[i].date_h;
                        
            } else if (data[i].Tabel == "recieve_delivery"){
                var recieve_delivery_date_m = data[i].date_m;
                Newgroup = getGroup(recieve_delivery_date_m);
                header = data[i].type;
                var more_info = "";
                if (header == 'تسليم واستلام نفقة') {
                    more_info = "<br/>المبلغ : " + data[i].amount;
                } else {
                    more_info = "<br/>الأطفال :  ";
                   var index_recieve_Children = data_recieveChildren.findIndex(x => x.details_id === data[i].code);
                    if (index_recieve_Children != -1) {
                        while (index_recieve_Children < data_recieveChildren.length && data_recieveChildren[index_recieve_Children].details_id == data[i].code) {
                            more_info += " " + data_recieveChildren[index_recieve_Children].name + " ـــ ";
                            index_recieve_Children++;
                        }
                        more_info = more_info.substr(0, (more_info.length - 4));
                    }
                   
                }
                paragraph = "المسلم : " + data[i].deliver + "<br/>المستلم : " + data[i].recieve + more_info;

                        date =" "+ recieve_delivery_date_m + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + data[i].date_h;
                      }
            
                if (Newgroup != group) {
                    group = Newgroup;
                    divGroup = '<li><div class="tldate">' + group + '</div></li>';
                }
                var add_class = "";
            var panel_style = "";
            var headerBackground = "";
                if (count % 2 == 0) {
                    add_class = "Rightcirc";
                    panel_style = "style='border: 4px solid #9fa0a0;'";
                    div_alternative = " class='timeline-inverted'";
                    headerBackground =" style='background: #9fa0a0;'";
                }
                var str = divGroup + `<li ${div_alternative}> <div class="tl-circ ${add_class}" ></div >
                <div class="timeline-panel" ${panel_style}>
                <div class="tl-heading"> <h4 ${headerBackground}>${header}</h4>
          <p><small class="text-muted"><i class="glyphicon glyphicon-time"></i>${date}</small></p>
        </div>
        <div class="tl-body">
          <p>${paragraph}</p>
        </div>
      </div></li>`;
                count++;
                $("#data").append(str);
            }
    });

});
function convertDateToNumber(str_date) {
    var arr_data = str_date.split("/");
    return Number("" + arr_data[2] + "" + arr_data[0] + ""+arr_data[1]);
}

function getGroup(str_date) {
    var arr_data = str_date.split("/");
    var month = arr_data[1];
    var res = "";
    if (month == "00") {
        res = "يناير";
    } else if (month == "01") {
        res = "فبراير";
    }
    else if (month == "02") {
        res = "مارس";
    }
    else if (month == "03") {
        res = "إبريل";
    }
    else if (month == "04") {
        res = "مايو";
    }
    else if (month == "05") {
        res = "يونيو";
    }
    else if (month == "06") {
        res = "يوليو";
    }
    else if (month == "07") {
        res = "إغسطس";
    }
    else if (month == "08") {
            res = "سبتمبر";
    }
    else if (month == "09") {
        res = "أكتوبر";
    }
    else if (month == "10") {
        res = "نوفمبر";
    }
    else if (month == "07") {
        res = "ديسمبر";
    }
    return res+" "+arr_data[2];
}