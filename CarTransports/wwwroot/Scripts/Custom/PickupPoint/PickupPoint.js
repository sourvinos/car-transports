// Startup
$(function () {
    init();
});

// Toggle details
$(".clickable").click(function (event) {
    $(this).next().find(".itemDetails").slideToggle();
});

// Search button
$("button.search").on("click", function (event) {
    event.preventDefault();
    init();
    filterTable();
});

// Create a modal
$("#myMap").dialog({
    height: 600,
    width: 700,
    autoOpen: false,
    modal: true,
    title: $("#Description").val() + ", " + $("#Zip").val()
});

// Show map in modal
$("#showMap").on("click", function (event) {
    event.preventDefault();
    displayMap();
});

function init() {
    $(".clickable").addClass("hidden");
    $(".clickable").removeClass("selected displayMe");
    $(".itemDetails").slideUp();
}
