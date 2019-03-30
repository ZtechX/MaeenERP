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
function updateNotificationIsSeen(sender) {
    try {
        var NotID_SeenValue = sender.type + "|" + $(sender).parent().parent().find('#lblSeen').html() + "|" + $('#lblUserId').html();
        WebService.UpdateNotificationIsSeen(NotID_SeenValue, function (res) {
            if (res != -1) {
                if ($(sender).parent().parent().find('#lblSeen').html() == 'True' || $(sender).parent().parent().find('#lblSeen').html() == 'true') {
                    $(sender).removeClass();
                    $(sender).addClass("fa fa-eye-slash");
                    $(sender).parent().parent().find('#lblSeen').html('False');
                    $(sender).attr('title', 'Mark as read');
                } else {
                    $(sender).removeClass();
                    $(sender).addClass("fa fa-eye");
                    $(sender).parent().parent().find('#lblSeen').html('True');
                    $(sender).attr('title', 'Mark as unread');
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
        var html = "<li><div id='pnlNotification' class='popover-content'>" + 
                "<a ID='lbNotification' class='text-size-15' href='/../Work_Module/" +
                notificationJSON.RefType + ".aspx?Code=" + notificationJSON.RefCode + "&operation=search" +
                "' onclick='openForm(this); return false;'>" + notificationJSON.NotTitle + "</a>" +
                "<div class='pull-right view_btns'>";
        if (notificationJSON.IsSeen == true) {
            html += "<a id='lbRead' type='" + notificationJSON.ID + "' href='#' onclick='updateNotificationIsSeen(this); return false;' title='Mark as unread' class='fa fa-eye'></a>";
        } else {
            html += "<a id='lbRead' type='" + notificationJSON.ID + "' href='#' onclick='updateNotificationIsSeen(this); return false;' title='Mark as read' class='fa fa-eye-slash'></a>";
        }
        html += "</div>" +
            "<span id='lblNotID' Style='display:none'>" + notificationJSON.ID + "</span>" +
            "<span id='lblRefCode' Style='display:none'>" + notificationJSON.RefCode + "</span>" +
            "<span id='lblRefType' Style='display:none'>" + notificationJSON.RefType + "</span>" +
            "<p>" + notificationJSON.Remarks + "</p>" +
            "<p>" + new Date(notificationJSON.NotDate.match(/\d+/)[0] * 1).toLocaleTimeString() + "</p>" +
            "<span id='lblNotStatus' Style='display:none'>" + notificationJSON.Status + "</span>" +
            "<span id='lblSeen' Style='display:none'>" + notificationJSON.IsSeen + "</span>";

        html += "</div></li>";
        return html;
    }
    catch (err) {
        alert(err);
    }
}

// open form of the clicked notification
function openForm(sender) {
    try {
        var NotID_SeenValue = $(sender).parent().find('#lblNotID').html() + "|" + $(sender).parent().find('#lblSeen').html() + "|" + $('#lblUserId').html();
        WebService.UpdateNotificationIsSeen(NotID_SeenValue, function (res) {
            if (res != -1) {
                window.location = '/' + "../Work_Module/" +
                    $(sender).parent().find('#lblRefType').html() + ".aspx?Code=" +
                    $(sender).parent().find('#lblRefCode').html() + "&operation=search"
            }
        });
    }
    catch (err) {
        alert(err);
    }
}