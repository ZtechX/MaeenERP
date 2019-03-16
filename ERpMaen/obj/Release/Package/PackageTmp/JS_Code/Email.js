var UIDs = new Array();
var EmailsCount;

$(document).ready(function () {
    LoadEmails("UserId", "Inbox");
    //ReadEmail();
   // setInterv();
    setCounts();
});
function LoadEmails(UserId,type) {
    $("#MailType").html(type);
    WebService.GetEmails(UserId,type, OnSuccess);
}// i deleted all data in email table and connected to new mail  ok?  Thats okay letme check 
function OnSuccess(res) {
    var tableDiv = document.getElementById("tableDiv");
    tableDiv.innerHTML = "";
    $("#tableDiv").append('<table id="tablelist" class="display dataTable" role="grid" aria-describedby="tablelist_info">')   
    var tablelist = document.getElementById("tablelist");
    var Emai = res;
    var count = Emai.length;
    var hrow = document.createElement("thead");
    var row = document.createElement("tr");
    for (var k = 0; k < 4; k++) {
        if (k == 0) {
            var cell = document.createElement("th");
            cell.innerHTML = "From";
            row.appendChild(cell);
        }
        else if (k == 1) {
            var cell = document.createElement("th");
            cell.innerHTML = "Subject";
            row.appendChild(cell);
        }
        else if (k == 2) {
            var cell = document.createElement("th");
            cell.innerHTML = "Date";
            row.appendChild(cell);
        }       
    }
    var frow = document.createElement("tfoot");
    var row1 = document.createElement("tr");
    for (var l = 0; l < 4; l++) {
        if (l == 0) {
            var cell = document.createElement("th");
            cell.innerHTML = "From";
            row1.appendChild(cell);
        }
        else if (l == 1) {
            var cell = document.createElement("th");
            cell.innerHTML = "Subject";
            row1.appendChild(cell);
        }
        else if (l == 2) {
            var cell = document.createElement("th");
            cell.innerHTML = "Date";
            row1.appendChild(cell);
        }

    }
    hrow.appendChild(row);
    frow.appendChild(row1);
    tablelist.appendChild(hrow);
    tablelist.appendChild(frow);
    //////////////////////////////////////////

    var body = document.createElement("tbody");
    tablelist.appendChild(body);
    for (var i = 0; i < count; i++) {
        if (i == count - 1) {
            var last = Emai[i];
            var array = Emai[i].split(',');
            var no = array.length;
            for (var j = 0; j < no ; j++) {
                if (Emai[j] !== "Removed") {
                    var val = Emai[i].toString();
                    UIDs.push(val + "-" + j);
                }
            }
        }
        else {
           
            var row = Emai[i];
            var hrow = document.createElement("tr");
            row.toString();
            var Id = row.split("|")[0];
            var ElId = document.createElement("td");
            var cb = document.createElement('input');
            cb.type = 'checkbox';
            var MsgId = row.split("|")[1];
            var EmailAddress = row.split("|")[4];
            var ElEmailAddress = document.createElement("td");
            var EmailAddressAnchor = document.createElement('a');
            EmailAddressAnchor.setAttribute('href', EmailAddress);
            EmailAddressAnchor.innerHTML = EmailAddress;
            ElEmailAddress.appendChild(EmailAddressAnchor);
            hrow.appendChild(ElEmailAddress);
            var Sub = row.split("|")[2];
            var ElSub = document.createElement("td");
            var SubAnchor = document.createElement('a');
            SubAnchor.setAttribute('href', Sub);
            SubAnchor.innerHTML = Sub;
            ElSub.appendChild(SubAnchor);
            hrow.appendChild(ElSub);
            var DateSent = row.split("|")[3];
            var ElDateSent = document.createElement("td");
            var DateSentAnchor = document.createElement('a');
            DateSentAnchor.setAttribute('href', DateSent);
            DateSentAnchor.innerHTML = DateSent;
            ElDateSent.appendChild(DateSentAnchor);
            hrow.appendChild(ElDateSent);
            EmailsCount = row.split("|")[8];
            body.appendChild(hrow);
        }
        tablelist.appendChild(body);
    }
    $('#tablelist tfoot th').each(function () {
        var title = $('#tablelist thead th').eq($(this).index()).text();
        $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    });

    var table = $('#tablelist').DataTable();
    table.columns().every(function () {
        var that = this;
        $('input', this.footer()).on('keyup change', function () {
            that
                .search(this.value)
                .draw();
        });
    });
    ReadEmail();
}
function ReadEmail() {
    var ar = new Array();
    ar = UIDs[0].split(",");
    var count = ar.length;
    for (var j = 0; j < count; j++) {
        if (ar[j] !== "Removed") {
            var msgNo = ar[j];
            WebService.ReadNewEmail(j, OnSuccess);
        }
    }
    function OnSuccess(res) {
        //if ($("#MailType").html() = "Inbox") {
        var result = res;
        alert(result);
            if (result) {
                res = res.toString();
                var uidcount = UIDs.length;
                var messageId = result.split("|")[0];
                var from = result.split("|")[1];
                var Sub = result.split("|")[2];
                var DateSent = result.split("|")[3];
                var UniqueId = result.split("|")[4];
                var exists = result.split("|")[5];
                var row = document.createElement("tr");
                row.className = "Unread";
                var ElId = document.createElement("td");
                var cb = document.createElement('input');
                cb.type = 'checkbox';
                ElId.appendChild(cb);
                row.appendChild(ElId);
                var ElEmailAddress = document.createElement("td");
                var EmailAddressAnchor = document.createElement('a');
                EmailAddressAnchor.setAttribute('href', from);
                EmailAddressAnchor.innerHTML = from;
                ElEmailAddress.appendChild(EmailAddressAnchor);
                row.appendChild(ElEmailAddress);
                var ElSub = document.createElement("td");
                var SubAnchor = document.createElement('a');
                SubAnchor.setAttribute('href', Sub);
                SubAnchor.innerHTML = Sub;
                ElSub.appendChild(SubAnchor);
                row.appendChild(ElSub);
                var ElDateSent = document.createElement("td");
                var DateSentAnchor = document.createElement('a');
                DateSentAnchor.setAttribute('href', DateSent);
                DateSentAnchor.innerHTML = DateSent;
                ElDateSent.appendChild(DateSentAnchor);
                row.appendChild(ElDateSent);
                var attachment = document.createElement("td");
                row.appendChild(attachment);
                var tbl = document.getElementById("tablelist");
                tbl.appendChild(row);
            }
        //}
    }
}
function setInterv() {
    WebService.GetInterval("", function (interval) {
        if (interval != 0) {
            setInterval(ReadEmail, interval);
        }
    });
}
function setCounts() {
    WebService.GetCounts("prifix", counts);
}
function counts (count)
{
           if (count != "") {
            $("#lnInbox").html("Inbox (" + count[0] + ")");
            $("#lnSent").html ("Sent (" + count[1] + ")");
            $("#lnDeleted").html ("Deleted (" + count[2] + ")");
        }
}