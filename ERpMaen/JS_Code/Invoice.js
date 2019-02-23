
$(function () {
    try {
        debugger;
    var par = getUrlVars();
    if (jQuery.isEmptyObject(par) == false) {
        var operation = par.operation;
        if (operation.toLowerCase() == "search") {
            var code = par.code;
            gettransferData(code);
        }
    }
    } catch (err) {
        alert(err);
    }
});

function gettransferData(auction_id) {
    try {
        transfers.getTransferData(auction_id, function (val) {
            if (val[0] != "0") {
                var data = JSON.parse(val[1]);
                $("#lblItem").html(data[0].itemname);
                $("#lblQuantity").html(data[0].quantity);
                $("#lblStore").html(data[0].storename);
                $("#lblcupbord").html(data[0].cupbordname);
                $("#lblrack").html(data[0].rackname);
                $("#lbldate").html(data[0].date);
                var type = data[0].type
                if (type == 1) {
                    $("#lblTypeName").html("اضافة صنف للمستودع");
                } else {
                    $("#lblTypeName").html("صرف صنف من المستودع");
                }
                $("#lblUserName").html(val[2]);
                window.print();
            }
        });
    } catch (err) {

    }
}