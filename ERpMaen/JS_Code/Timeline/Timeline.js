$(function () {
    $("#SavedivLoader").show();
    var case_id = $("#case_id").html();
    var done = $("#done").html();
    var start_dt = $("#start_dt").html();
    var end_dt = $("#end_dt").html();
  debugger
    Timeline.get_data(case_id, done, start_dt, end_dt, function (val) {
        
        var data_case = [];
        var data = [];
        var data_recieveChildren = [];
        var data_sessionChildren = [];
        var data_sessionCompanions = [];
        var data_caseChildren = [];
        if (val[0] != "") {
             data_case = JSON.parse(val[0]);
        }
        if (val[1] != "") {
            data = JSON.parse(val[1]);
        }
        if (val[2] != "") {
            data_recieveChildren = JSON.parse(val[2]);
        }
        if (val[3] != "") {
            data_sessionChildren = JSON.parse(val[3]);
        }
        if (val[4] != "") {
            data_sessionCompanions = JSON.parse(val[4]);
        }
        if (val[5] != "") {
            data_caseChildren = JSON.parse(val[5]);
        }
        var count = 0;
        var group = "";
        var Newgroup = "";
        var divGroup = "";
        var header = "";
        var paragraph = "";
        var date = "";
        var div_alternative = "";
        
        for (var i = 0; i < data.length; i++) {
                Newgroup = "";
                divGroup = "";
                header = "";
                paragraph = "";
            date = "";
            time = "";
                div_alternative = "";
            if (data[i].Tabel == "Session") {
                var session_date_m = data[i].date_m;
                Newgroup = getGroup(session_date_m);
                header = "جلسة تهيئة وتدرج";

                var more_info = "<br/>الأطفال المقيدين :  ";
                var index_session_Children = data_sessionChildren.findIndex(x => x.session_id === data[i].session_id);
                if (index_session_Children != -1) {
                    while (index_session_Children < data_sessionChildren.length && data_sessionChildren[index_session_Children].session_id == data[i].session_id) {
                        more_info += " " + data_sessionChildren[index_session_Children].name + " ـــ ";
                        index_session_Children++;
                    }
                    more_info = more_info.substr(0, (more_info.length - 4));
                }
                more_info += "<br/>المرافقين :  ";
                var index_session_Companions = data_sessionCompanions.findIndex(x => x.session_id === data[i].session_id);
                if (index_session_Companions != -1) {
                    while (index_session_Companions < data_sessionCompanions.length && data_sessionCompanions[index_session_Companions].session_id == data[i].session_id) {
                        more_info += " " + data_sessionCompanions[index_session_Companions].name + " ـــ ";
                        index_session_Companions++;
                    }
                    more_info = more_info.substr(0, (more_info.length - 4));
                }

                paragraph = "جلسة رقم : " + data[i].code + "<br/>طالب التنفيذ : " + data[i].owner +
                    "<br/>المنفذ ضده : " + data[i].agains + "<br/>مكان الجلسة : " + data[i].place +
                    "<br/>وقت الدخول : " + data[i].entry_time + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;وقت الخروج : " + data[i].exite_time +
                    more_info + "<br/>النتيجة : " + data[i].result;
                date = " " + session_date_m + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + data[i].date_h;

            }
            else if (data[i].Tabel == "recieve_delivery") {
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

                date = " " + recieve_delivery_date_m + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + data[i].date_h;
            }
            else if (data[i].Tabel == "Conciliation") {
                var conciliation_date_m = data[i].date_m;
                Newgroup = getGroup(conciliation_date_m);
                header = "محضر صلح للحالة";
                paragraph = "رقم الصلح : " + data[i].code +
                    "<br/>الطرف الأول : " + data[i].owner +
                    "<br/>الطرف الثانى : " + data[i].agains +
                    "<br/>بنود الإتفاق<pre><p>" + data[i].result+"</p></pre>";
                date = " " + conciliation_date_m + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + data[i].date_h;

            }
            else if (data[i].Tabel == "Correspondence") {
                var conciliation_date_m = data[i].date_m;
                Newgroup = getGroup(conciliation_date_m);
                header = "اجرائات العضو المباشر للحالة";
                paragraph = "رقم الإجراء : " + data[i].code +
                    "<br/>الشخص : " + data[i].owner +
                    "<br/>نوع الإجراء : " + data[i].type +
                    "<br/>ملاحظات على الإجراء <pre><p>" + data[i].result + "</p></pre>";
                date = " " + conciliation_date_m + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + data[i].date_h;

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
          <p><small class="text-muted"><i class="fa fa-calendar fa-md" style="color:red;"></i>${date}</small> <small style="float:left;" class="text-muted"><i class="fa fa-clock-o fa-md" style="color:red;"></i> ${data[i].entry_time}</small></p>
        </div>
        <div class="tl-body">
          <p>${paragraph}</p>
        </div>
      </div></li>`;
                count++;
                $("#data").append(str);
        }
        var index = 0;
        var childrens = "";
        while (index < data_caseChildren.length) {
            childrens += data_caseChildren[index].name + " &nbsp;&nbsp;ـــ&nbsp;&nbsp; ";
            index++;
        }
        var case_data = "<pre style='margin-top:9px;padding-bottom:0px;background:#148083;color:#fff;border-radius:10px;'><p>تم إنشاء الحالة<p></pre>" +
            "رقم الحالة : " + data_case[0].code +
            "<br/>تاريخ إنشاء الحالة : " + data_case[0].date_m + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + data_case[0].date_h+
            "<br/>وارد من : " + data_case[0].C_from +
            "<br/>رقم الصك : " + data_case[0].instrument_no +
            "<br/>تاريخ الصك : " + data_case[0].instrument__date_m + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"  + data_case[0].instrument_date_h+
            "<br/>الأولاد : " + childrens+
            "<br/>المنفذ : " + data_case[0].P_from +
            "<br/>المنفذ ضده : " + data_case[0].P_against +
            "<br/>الحالة الحالية للحالة : " + data_case[0].status +
            "<br/>الخدمة المقدمة : " + data_case[0].depart +
            "<br/>المستشار المسؤل عن الحالة : " + data_case[0].advisor_nm +
            "<br/>الحكم الصدار من المحكمة : " + data_case[0].court_details +
            "<br/><br/>ملاحظات عن الحالة<pre style='padding-bottom: 0px;background:#148083;color:#fff;border-radius:10px;'><p>" + data_case[0].details +"</p></pre > ";
        $("#data").append('<li><div class="tldate" style="border-radius:35px;background:#C09C67;border:3px solid #d0b793;width:100%;padding-left:20px;padding-right:20px;">'+case_data+'</div></li>');
        $("#SavedivLoader").hide();
    });

});
function convertDateToNumber(str_date) {
    var arr_data = str_date.split("/");
    return Number("" + arr_data[2] + "" + arr_data[0] + ""+arr_data[1]);
}

function SaveImage() {
    try {
        //debugger
        //$("#SavedivLoader").show();
        //var pdf = new jsPDF('p', 'pt', 'a4');
        //var width = pdf.internal.pageSize.getWidth;
        //var options = {
        //    pagesplit: true,
        //    'width': width
        //};

        //pdf.addHTML($("#divContainer"), options, function () {
        //    pdf.save("test.pdf");
        //});
        debugger
        domtoimage.toJpeg(document.getElementById('divContainer'), { quality: 0.95 })
            .then(function (dataUrl) {
                debugger
                var link = document.createElement('a');
                link.download = 'الجدول الزمنى للحالة.jpeg';
                link.href = dataUrl;
                link.click();
            });
        $("#SavedivLoader").hide();
    } catch (ex) {
        alert(ex);
    }
}
 
function getGroup(str_date) {
    if (str_date.indexOf("/") == -1) {
        return "&nbsp;&nbsp;&nbsp;&nbsp;";
    } else {
        var arr_data = str_date.split("/");
        var month = arr_data[1];
        var res = "";
        if (month == "01") {
            res = "يناير";
        } else if (month == "02") {
            res = "فبراير";
        }
        else if (month == "03") {
            res = "مارس";
        }
        else if (month == "04") {
            res = "إبريل";
        }
        else if (month == "05") {
            res = "مايو";
        }
        else if (month == "06") {
            res = "يونيو";
        }
        else if (month == "07") {
            res = "يوليو";
        }
        else if (month == "08") {
            res = "إغسطس";
        }
        else if (month == "09") {
            res = "سبتمبر";
        }
        else if (month == "10") {
            res = "أكتوبر";
        }
        else if (month == "11") {
            res = "نوفمبر";
        }
        else if (month == "12") {
            res = "ديسمبر";
        }
        return res + " " + arr_data[2];
    }
}

  