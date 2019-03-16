/************************************/
// Created By : Mostafa Abdelghffar
// Create Date : 23/5/2015 10:30 AM
// Description : This file contains all javaScript functions in Calendar form
/************************************/

$(document).ready(function () {
    $('#calendar').fullCalendar({
        showAgendaButton: true,
        //columnFormat: { month: 'DDD', week: 'DDD D/M', day: 'DDDD D/M' },
        timeFormat: 'H:mm',
        axisFormat: 'H:mm',

        header: {
            left: 'prev, next today',
            center: 'title',
            right: 'month, basicWeek, basicDay'
        },
        events: "Calendar.asmx/EventList",
        eventClick: function () {
            alert('a day has been clicked!');
        }
    });
    $(".mycalender_btn").click(function () {
        $("#divUsers").slideToggle();
    });
});