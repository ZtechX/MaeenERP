/************************************/
// Created By : Mostafa Abdelghffar
// Create Date : 13/10/2015 10:30 AM
// Description : This file contains all javaScript functions in dashboard
/************************************/
 
// page load function
$(function () {
    try {
        checkLanguage();
        getWhatToViewCounts();
        drawGeneralNotice();
        drawAgentNotice();
        drawTodayFollowUp();
        drawArchiveFollowUp();
    } catch (err) {
        alert(err);
    }
});


// get count of loggin user leads, contacts and expired rent contracts
function getWhatToViewCounts() {
    try {
        Dashboard.getWhatToViewCounts(function (count) {
            if (count != "") {
                $("#lblMyExpiredRentContracts").html(count.split("|")[0]);
                $("#lblMyLeads").html(count.split("|")[1]);
                $("#lblMyContacts").html(count.split("|")[2]);
                $(".highlight").show();
            }
        });
    } catch (err) {
        alert(err);
    }
}

// draw general notices in GeneralNotice frame
function drawGeneralNotice() {
    try {
        Dashboard.GetGeneralNotice(function (generalNoticeSTR) {
            var generalNoticeArrJson = JSON.parse(generalNoticeSTR);
            $.each(generalNoticeArrJson, function (i, generalNotice) {
                var liNotice = "<li><a href='#' onclick='return false;'>";
                liNotice += "<img class='img-avatar' src='" + generalNotice.UserPhoto + "' alt=''>";
                liNotice += "<i class='fa fa-circle text-success'></i>" + generalNotice.UserName + "<span> at " + generalNotice.NoticeDate + "</span>";
                liNotice += "<div class='font-w400 text-muted'>" + generalNotice.NoticeTitle + "</div>";
                liNotice += "<div class='font-w400 text-muted'><small>" + generalNotice.NoticeNotes + "</small></div>";
                liNotice += "</a></li>";
                $("#ulGeneralNotice").append(liNotice);
            });
            $("#ulGeneralNotice").append("<li class='clearfix clear'><br/></li>");
        });
    } catch (err) {
        alert(err);
    }
}

// draw login notices in MyNotice frame
function drawAgentNotice() {
    try {
        Dashboard.GetAgentNotice(function (agentNoticeSTR) {
            var agentNoticeArrJson = JSON.parse(agentNoticeSTR);
            $.each(agentNoticeArrJson, function (i, agentNotice) {
                var liNotice = "<li class='list-group-item'>";
                liNotice += "<p class='notice_caption'>" + agentNotice.NoticeTitle + "</p>";
                liNotice += "<p class='normal_p'>" + agentNotice.NoticeNotes + "</p>";
                liNotice += "<p class='date'>" + agentNotice.NoticeDate + "</p>";
                liNotice += "</li>";
                $("#ulAgentNotice").append(liNotice);
            });
            $("#ulGeneralNotice").append("<li class='clearfix clear'><br/></li>");
        });
    } catch (err) {
        alert(err);
    }
}

// draw correspondence with followup date is today in List tab in FollowUPList frame
function drawTodayFollowUp() {
    try {
        Dashboard.GetTodayFollowUp(function (todayFollowUpSTR) {
            var todayFollowUpArrJson = JSON.parse(todayFollowUpSTR);
            $.each(todayFollowUpArrJson, function (i, todayFollowUp) {
                var liFollowUp = "<li class='list-group-item'><div class='checkbox checkbox-primary'>";
                liFollowUp += "<input class='todo-done' type='checkbox' checked=''>"
                liFollowUp += "<label for='6'>" + todayFollowUp.CorresType + " at " + todayFollowUp.FollowTime + "</label>";
                liFollowUp += "<br><span>" + getValueOrEmpty(todayFollowUp.Subject) + "</span><br>";
                liFollowUp += "<span><small>" + getValueOrEmpty(todayFollowUp.Description) + "<small></span>";
                liFollowUp += "</div></li>";
                $("#ulTodayFollowUp").append(liFollowUp);
            });
            $("#ulTodayFollowUp").append("<li class='clearfix clear'><br/></li>");
        });
    } catch (err) {
        alert(err);
    }
}

// draw correspondence with followup date less than today and for past 3 months in Archive tab in FollowUPList frame
function drawArchiveFollowUp() {
    try {
        Dashboard.GetArchiveFollowUp(function (archiveFollowUpSTR) {
            var archiveFollowUpArrJson = JSON.parse(archiveFollowUpSTR);
            $.each(archiveFollowUpArrJson, function (i, archiveFollowUp) {
                var liFollowUp = "<li class='list-group-item'><div class='checkbox checkbox-primary'>";
                liFollowUp += "<input class='todo-done' type='checkbox' checked=''>"
                liFollowUp += "<label for='6'>" + archiveFollowUp.CorresType + "</label>";
                liFollowUp += "<br><span>at " + archiveFollowUp.FollowTime + "</span>";
                liFollowUp += "<br><span>" + getValueOrEmpty(archiveFollowUp.Subject) + "</span><br>";
                liFollowUp += "<span><small>" + getValueOrEmpty(archiveFollowUp.Description) + "</small></span>";
                liFollowUp += "</div></li>";
                $("#ulArchiveFollowUp").append(liFollowUp);
            });
            $("#ulArchiveFollowUp").append("<li class='clearfix clear'><br/></li>");
        });
    } catch (err) {
        alert(err);
    }
}

// send sms to contacts
function sendSMS() {
    try {
        var message = $("#txtMessage").val();
        var phoneNo = $("#txtPhoneNo").val();
        alert('Contact Linkinsoft for SMS package !');
        //SMS.SendSMS(message, phoneNo, function (val) {
        //    if (val.split("|")[0] == "True") {
        //        $("#txtMessage").val("");
        //        $("#txtPhoneNo").val("");
        //        alert("SMS Sent Successfully");
        //    } else {
        //        alert(val.split("|")[1]);
        //    }
        //});
    } catch (err) {
        alert(err);
    }
}