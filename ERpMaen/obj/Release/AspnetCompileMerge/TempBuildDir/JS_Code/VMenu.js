/************************************/
// Created By : Mostafa Abdelghffar
// Create Date : 21/5/2015 01:00 PM
// Description : This file contains all javaScript functions in VMenu form
/************************************/

function gerd() {
    if (document.getElementById('cssmenu').className == 'menu_continer') {
        document.getElementById('right_cont').style.width = '96%';
        document.getElementById('cssmenu').className = '';
        document.getElementById('cssmenu').className += 'menu_continersmall';
    }
    else {
        document.getElementById('right_cont').style.width = '85%';
        document.getElementById('cssmenu').className = '';
        document.getElementById('cssmenu').className += 'menu_continer';
    }
}

function menu() {
    if (document.getElementById('cssmenu').className == 'menu_continer') {
        document.getElementById('right_cont').style.width = '85%';
    }
    else if (document.getElementById('cssmenu').className == 'menu_continersmall') {
        document.getElementById('cssmenu').className = '';
        document.getElementById('cssmenu').className += 'menu_continer';
        document.getElementById('right_cont').style.width = '85%';
    }
}

$(document).ready(function () {
    $('.nav li').click(function () {
        $(this).find('.dd-content').slideToggle('fast');
    });
});

$(document).ready(function () {
    $(".access_btn").click(function () {
        $(".toggle_box").slideToggle();
    });
});

$(document).ready(function () {
    $(".responsive_btn").click(function () {
        $("#reponsive_menu").slideToggle();
    });
});

$(document).ready(function () {
    $("#html").mouseup(function (e) {
        var subject = $(".toggle_box, .dd-content");
        if (e.target.id != subject.attr('id') && !subject.has(e.target).length) {
            subject.fadeOut();
        }
    });
});

$(document).ready(function () {
    $("#cssmenu").hover(function () {
        $(this).addClass("menu_continer");
        $(this).removeClass("menu_continersmall");
    });
});
$(document).ready(function () {
    $("#cssmenu").hover(function () {
        $(this).addClass("menu_continersmall");
        $(this).removeClass("menu_continer");
    });
});
$(document).ready(function () {
    var F = document.getElementById("myFrame");
    if (F.contentDocument) {
        F.height = F.contentDocument.documentElement.scrollHeight + 30; //FF 3.0.11, Opera 9.63, and Chrome
    } else {
        F.height = F.contentWindow.document.body.scrollHeight + 30; //IE6, IE7 and Chrome
    }
});

$(document).ready(function () {
    $('.hastip').tooltipsy();
});

function changeForm(sender) {
    Zwebservice.GetFormUrl(sender.value, function (val) {
        if (val != "") {
            setFrame(val);
        }
    });
}

function setFrame(val) {
    $('#myFrame').attr('src', val);
    return false;
}