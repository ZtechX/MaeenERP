$(function () {
    try {
        $("#pnlConfirm").hide();
        $("#pnlOps").show();
    }
    catch (err) {
        alert(err);
    }
});

function ClearXML(val) {
    alert(val)
}

function Edit() {
    $("#pnlConfirm").show();
    $("#pnlOps").hide();
}

function save() {
    try {
            var WebSites = '';
            var gridView = document.getElementById('<%= gvWebSites.ClientID %>');
            $('#gvWebSites').find('tr').each(function () {
                var row = $(this);
                if (row.find('input[type="checkbox"]').is(':checked')) {
                    WebSites = WebSites + row.find("#lblSiteXML").html() + "|";
                }
            });
                PublishXML.UpdateWebSites(WebSites, onSuccessPublish);
        }
    catch (err) {
        alert(err);
    }
}
function onSuccessPublish(val) {
    if (val["0"] == "0") {
        showErrorMessage("Failed")
    }
    else {
        $("#pnlConfirm").hide()
        $("#pnlOps").show()
        showSuccessMessage("Updated Succesfully")
    }
}