// Startup
$(function () {
    init();
});

// Toggle details
$("tr.clickable").click(function (event) {
    $("tr.rowDetails." + $(this).attr("id")).toggleClass("hidden");
});

function init() {
    $("tr.rowDetails").addClass("hidden");
}
