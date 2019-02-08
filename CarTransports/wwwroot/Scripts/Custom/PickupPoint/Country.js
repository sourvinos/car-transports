// Select - deselect and update the box
$(".countriesList li").off("click").click(function () {
    // Get the selected li's data-id
    var spanId = $(this).find("span").attr("data-id");
    // Select - deselect
    $(".countriesList li span[data-id='" + spanId + "']").parent().toggleClass("selected");
    // Update the box
    updateArray(".countriesList li.selected", "countryId");
});

// Unselect all and update the box
$(".clearCountries").on("click", function () {
    clearListItems("countriesList", "countryId");
});
