/************************************/
// Created By : Mostafa Abdelghffar
// Create Date : 21/5/2015 01:00 PM
// Description : This file contains all javaScript functions for notifications
/************************************/

// onload, draw notifications and set refresh interval for reDrawing
$(document).ready(function () {
    drawNotifications();
    WebService.GetRefreshInterval(function (interval) {
        setInterval(drawNotifications, interval);
    });
});

// toggle notifications (show, hide)
$(document).ready(function () {
    $("#notification_btn").click(function () {
        $("#notifications").slideToggle();
    });
});

// update notification IsSeen based on the passed <a> 
function updateNotificationIsSeen(sender,value,note_id) {
    try {
        var NotID_SeenValue = "" + note_id + "|" + value + "|" + $('#lblUserId').html();
        WebService.UpdateNotificationIsSeen(NotID_SeenValue, function (res) {
            if (res != -1) {
                if (value) {
                    $(sender).parent().find("#lbRead").removeClass();
                    $(sender).parent().find("#lbRead").addClass("fa fa-eye-slash");
                    $(sender).parent().find("#lbRead").attr('title', 'Mark as read');
                    $(sender).parent().find("#lbRead").attr('onclick', "'updateNotificationIsSeen(this, false, " + note_id + "); return false;'");
                } else {
                    $(sender).parent().find("#lbRead").removeClass();
                    $(sender).parent().find("#lbRead").addClass("fa fa-eye");
                    $(sender).parent().find("#lbRead").attr('title', 'Mark as unread');
                    $(sender).parent().find("#lbRead").attr('onclick', "'updateNotificationIsSeen(this, true, " + note_id + "); return false;'");

                }
                $('#lblNotCount').html(res);
            }
        });
        $('#notifications').show();
    }
    catch (err) {
        alert(err);
    }
}

// get notifications and append li for each notification
function drawNotifications() {
    try {
        showNotificationPart(false);
        getNotificationsCount();
        WebService.GetNotifications($('#lblUserId').html(), function (notifications) {
            $('#ulNotifications').empty();
            if (notifications != "") {
                var notificationsJSON = JSON.parse(notifications);
                if (notificationsJSON.length > 0) {
                    for (i = 0; i < notificationsJSON.length; i++) {
                        var liHtml = getLiHtmlString(notificationsJSON[i]);
                        $('#ulNotifications').append(liHtml);
                    }
                } 
            } else {
                $('#ulNotifications').append("<span> No Notifications Found </span>");
            }
            showNotificationPart(true);
        });
    }
    catch (err) {
        alert(err);
    }
}

// get notifications count
function getNotificationsCount() {
    try {
        WebService.GetNotificationsCount($('#lblUserId').html(), function (NotCount) {
            $('#lblNotCount').html(NotCount);
        });
    }
    catch (err) {
        alert(err);
    }
}

// show or hide loader part and notification part 
function showNotificationPart(b) {
    try {
        if (b == true) {
            $('#divLoaderPart').hide();
            $('#divNotificationPart').show();
        } else {
            $('#divLoaderPart').show();
            $('#divNotificationPart').hide();
        }
    }
    catch (err) {
        alert(err);
    }
}

// get a html string that draw li for each notification
function getLiHtmlString(notificationJSON) {
    try {
        var html = `<li><div id='pnlNotification' class='popover-content'> 
                <a ID='lbNotification' class='text-size-15' onclick='updateNotificationIsSeen(this,false,${notificationJSON.ID});' href='../${notificationJSON.FormUrl}'>${notificationJSON.NotTitle}</a>
                <div class='pull-right view_btns'>`;
        if (notificationJSON.IsSeen == true) {
            html += "<a id='lbRead'  href='#' onclick='updateNotificationIsSeen(this,true,"+notificationJSON.ID+"); return false;' title='Mark as unread' class='fa fa-eye'></a>";
        } else {
            html += "<a id='lbRead'  href='#' onclick='updateNotificationIsSeen(this,false,"+notificationJSON.ID+"); return false;' title='Mark as read' class='fa fa-eye-slash'></a>";
        }
        html +="</div><span id='lblNotID' Style='display:none'>" + notificationJSON.ID + "</span>" +
            "<p>" + notificationJSON.Remarks + "</p>" +
            "<p>" + new Date(notificationJSON.NotDate.match(/\d+/)[0] * 1).toLocaleTimeString() + "</p>";
           
        html += "</div></li>";
        return html;
    }
    catch (err) {
        alert(err);
    }
}