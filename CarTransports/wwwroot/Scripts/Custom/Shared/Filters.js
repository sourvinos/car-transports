$(".clearFilter h4").text("Καθαρισμος ολων");

window.clearListItems = function clearListItems(className, textBox) {
    $("." + className + " li.selected").each(function () {
        $(this).removeClass("selected");
    });
    $("#" + textBox).attr("value", "");
};

window.updateArray = function updateArray(className, queryName) {
    var query = "";
    $(className).each(function () {
        query += $(this).find("span").attr("data-id") + ",";
    });
    query = query.substr(0, query.length - 1);
    $("#" + queryName).attr("value", query);
};

function filterTable() {

    var tr = $("tr.clickable");
    var numberOfInputs = $(".criteriaInput").length;
    var criteriaInputArray = [];
    var totalCriteriaCount = 0;
    var position = 0;
    var criteriaIndex = -1;
    var i = 0;

    for (i = 0; i < numberOfInputs; i++) {
        criteriaInputArray.push($(".criteriaInput")[i].value.split(","));
    }
    for (var col = 1; col <= $("th").length; col++) {
        if ($("th:nth-child(" + col + ")").attr("class") === "filter") {
            criteriaIndex++;
            for (row = 0; row < tr.length; row++) {
                td = tr[row].getElementsByTagName("td")[col - 1];
                if (td) {
                    position = criteriaInputArray[criteriaIndex].indexOf(td.innerHTML);
                    if (position > -1) {
                        tr[row].className += " selected";
                    }
                }
            }
        }
    }

    for (i = 0; i < $(".criteriaInput").length; i++) {
        if ($(".criteriaInput")[i].value !== "") {
            totalCriteriaCount++;
        }
    }

    $("tr.clickable").each(function (i, row) {
        var count = 0;
        var classNames = $(row).attr("class").toString().split(" ");

        $(classNames).each(function (key, value) {
            if (value === "selected") {
                count++;
            }
        });

        $(row).addClass(count === totalCriteriaCount && totalCriteriaCount > 0 ? "displayMe" : "");
    });

    $("tbody > tr.displayMe").removeClass("hidden");

}

$("#filtersDesktop").css("margin-top", "100px");