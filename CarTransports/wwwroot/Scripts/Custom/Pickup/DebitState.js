// Select - deselect and update the box
$(".debitStatesList li").off("click").click(function () {
    // Get the selected li's data-id
    var spanId = $(this).find("span").attr("data-id");
    // Select - deselect
    $(".debitStatesList li span[data-id='" + spanId + "']").parent().toggleClass("selected");
    // Update the box
    updateArray(".debitStatesList li.selected", "debitStateId");
});

// Unselect all and update the box
$(".clearDebitStates").on("click", function () {
    clearListItems("debitStatesList", "debitStateId");
});
