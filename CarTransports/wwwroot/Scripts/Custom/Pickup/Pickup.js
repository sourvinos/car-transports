// Startup
$(document).ready(function () {
    init();
});

// Initialize
function init() {
    $(".clickable").addClass("hidden");
    $(".clickable").removeClass("selected displayMe");
    $(".itemDetails").slideUp();
}

// Toggle details
$(".clickable").on("click", function (event) {
    $(this).next().find(".itemDetails").slideToggle();
});

// Search button
$("button.search").on("click", function (event) {
    event.preventDefault();
    init();
    filterTable();
});

// Toggle checkboxes and update boxes
$(".check-icon").on("click", function (event) {
    updateBoxes();
    if (checkUniqueCustomer()) {
        displayInvoiceIconForSelectedPickups();
    }
});

// Find customer and pickup details for selected rows
$("#createInvoice").on("click", function (e) {

    var v1 = $("#customerIds").val().split(",")[0];
    var v2 = $("#pickupIds").val();

    e.preventDefault();

    $.ajax({
        url: "pickup/invoicesForCustomer",
        method: "get",
        cache: false,
        data: { customerIds: v1, pickupIds: v2 }
    }).done(function (d) {
        $("#invoices").html(d);
    });

});

// Funnel button for mobile
$("#filterMobileToggle").on("click", function () {
    $("#filtersMobile").removeClass("changeColor");
    $("#filtersMobile").dialog("open");
});

// Update boxes with customerIds and pickupIds
function updateBoxes() {

    var customerIds = "";
    var pickupIds = "";
    var checkedBoxes = $(":checkbox:checked");

    for (var i = 0; i < checkedBoxes.length; i++) {
        if ($(checkedBoxes[i]).prop("checked")) {
            customerIds += checkedBoxes[i].getAttribute("data-customerId") + ",";
            pickupIds += checkedBoxes[i].getAttribute("data-pickupId") + ",";
        }
    }

    $("#customerIds").val(customerIds.substr(0, customerIds.length - 1));
    $("#pickupIds").val(pickupIds.substr(0, pickupIds.length - 1));

}

// Check for unique customers box
function checkUniqueCustomer() {

    var correct = true;
    var array = $("#customerIds").val().split(",").filter(Boolean);

    $(".invoice-icon").addClass("hidden");

    for (var i = 0; i < array.length; i++) {
        if (array[i] !== array[0]) {
            correct = false;
        }
    }

    return correct;

}

// Display icon to create invoice
function displayInvoiceIconForSelectedPickups() {

    var selectedPickups = $("#pickupIds").val().split(",").filter(Boolean);

    for (var i = 0; i < selectedPickups.length; i++) {
        $("td." + selectedPickups[i]).removeClass("hidden");
    }

}