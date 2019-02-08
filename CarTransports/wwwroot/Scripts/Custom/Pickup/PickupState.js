// Select - deselect and update the box
$(".pickupStatesList li").off("click").click(function () {
    // Get the selected li's data-id
    var spanId = $(this).find("span").attr("data-id");
    // Select - deselect
    $(".pickupStatesList li span[data-id='" + spanId + "']").parent().toggleClass("selected");
    // Update the box
    updateArray(".pickupStatesList li.selected", "pickupStateId");
});

// Unselect all and update the box
$(".clearPickupStates").on("click", function () {
    clearListItems("pickupStatesList", "pickupStateId");
});
