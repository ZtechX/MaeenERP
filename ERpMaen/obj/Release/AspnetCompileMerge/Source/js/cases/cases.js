
    $(document).ready(function () {
        $("div.bhoechie-tab-menu>div.list-group>a").click(function (e) {
            e.preventDefault();
            $(this).siblings('a.active').removeClass("active");
            $(this).addClass("active");
            var index = $(this).index();
            $("div.bhoechie-tab>div.bhoechie-tab-content").removeClass("active");
            $("div.bhoechie-tab>div.bhoechie-tab-content").eq(index).addClass("active");
        });
});
$(function () {
    $("#pnlConfirm").hide();
    $("#divData").hide();
    $("#SavedivLoader").show();

    try {
        // drawDynamicTable();
     //   form_load();
        //aslah.checkUser(function (val) {

        //    if (val == "Superadmin") {
        //        form_load();
        //        //$("#pnlConfirm").show();
        //        $("#divData").show();
        //        $("#SavedivLoader").hide();
        //        $("#comp_id").css("display", "");
        //        $("#Dyntabel").css("display", "");
        //    } else if (val != "0") {

        //        sms_setting.get_data(val, function (val1) {
        //            edit(val1);
        //            $("#ddlComps").val(val);
        //        });
        //    }
        //});

    } catch (err) {
        alert(err);
    }
});
function resetAll() {
    try {
        resetFormControls();
        $("#lblmainid").html("");

    } catch (err) {
        alert(err);
    }
}



////////////////
function addm() {
   
    var val1 = $("#women_Num").val();
    var val2 = $("#men_Num").val();
    var val3 = parseInt(val1) + parseInt(val2);
    $("#sumR").val(val3);

}

function state_setting() {
//    ﬂÊœ «⁄œ«œ«   »ÊÌ» «·Õ«·… 
    //
}
$(function () {
    $.widget("custom.combobox", {
        _create: function () {
            this.wrapper = $("<span>")
                .addClass("custom-combobox")
                .insertAfter(this.element);

            this.element.hide();
            this._createAutocomplete();
            this._createShowAllButton();
        },

        _createAutocomplete: function () {
            var selected = this.element.children(":selected"),
                value = selected.val() ? selected.text() : "";

            this.input = $("<input onchange='show_all();' >")
                .appendTo(this.wrapper)
                .val(value)
                .attr("title", "")
                .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
                .autocomplete({
                    delay: 0,
                    minLength: 0,
                    source: $.proxy(this, "_source")
                })
                .tooltip({
                    classes: {
                        "ui-tooltip": "ui-state-highlight"
                    }
                });

            this._on(this.input, {
                autocompleteselect: function (event, ui) {
             
                    ui.item.option.selected = true;
                    show_all($("#combobox").val(),1);
                    this._trigger("select ", event, {
                        item: ui.item.option
                    });
                },

                autocompletechange: "_removeIfInvalid"
            });
        },

        _createShowAllButton: function () {
            var input = this.input,
                wasOpen = false

            $("<a>")
                .attr("tabIndex", -1)
                .attr("title", "Show All Items")
                .attr("height", "")
                .tooltip()
                .appendTo(this.wrapper)
                .button({
                    icons: {
                        primary: "ui-icon-triangle-1-s"
                    },
                    text: "false"
                })
                .removeClass("ui-corner-all")
                .addClass("custom-combobox-toggle ui-corner-right")
                .on("mousedown", function () {
                    wasOpen = input.autocomplete("widget").is(":visible");
                })
                .on("click", function () {
                    input.trigger("focus");
                    //get_cases_data();

                    // Close if already visible
                    if (wasOpen) {
                        return;
                    }

                    // Pass empty string as value to search for, displaying all results
                    input.autocomplete("search", "");
                });
        },

        _source: function (request, response) {
            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
            response(this.element.children("option").map(function () {
                var text = $(this).text();
                if (this.value && (!request.term || matcher.test(text)))
                    return {
                        label: text,
                        value: text,
                        option: this
                    };
            }));
        },

        _removeIfInvalid: function (event, ui) {

            // Selected an item, nothing to do
            if (ui.item) {
                return;
            }

            // Search for a match (case-insensitive)
            var value = this.input.val(),
                valueLowerCase = value.toLowerCase(),
                valid = false;
            this.element.children("option").each(function () {
                if ($(this).text().toLowerCase() === valueLowerCase) {
                    this.selected = valid = true;
                    return false;
                }
            });

            // Found a match, nothing to do
            if (valid) {
                return;
            }

            // Remove invalid value
            this.input
                .val("")
                .attr("title", value + " didn't match any item")
                .tooltip("open");
            this.element.val("");
            this._delay(function () {
                this.input.tooltip("close").attr("title", "");
            }, 2500);
            this.input.autocomplete("instance").term = "";
        },

        _destroy: function () {
            this.wrapper.remove();
            this.element.show();
        }
    });
    //$("#combobox").on("change", function () { });



    $("#combobox").combobox();
    $("#toggle").on("click", function () {
        $("#combobox").toggle();
      
    });
});     

