/************************************/
// Created By : Mostafa Abdelghffar
// Create Date : 4/6/2015 1:30 PM
// Description : This file contains all javaScript functions resopsable for sorting(ordering) rows in html table
/************************************/

// helper function that used as sorting helper function
var sortableHelper = function (e, tr) {
    var $originals = tr.children();
    var $helper = tr.clone();
    $helper.children().each(function (index) {
        $(this).width($originals.eq(index).width())
    });
    return $helper;
}

// update the index after sorting(ordering) rows
var updateIndex = function (e, ui) {
    $('td .index', ui.item.parent()).each(function (i) {
        $(this).html(i + 1);
    });
};

///////// Usage /////////
//$("#tblId tbody").sortable({
//    helper: sortableHelper,
//    stop: updateIndex
//}).disableSelection();
//////////////////////////