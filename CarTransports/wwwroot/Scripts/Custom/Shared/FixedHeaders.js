$(function () {
    adjustColumnWidths();
});

$(window).resize(function () {
    adjustColumnWidths();
});

function adjustColumnWidths() {

    // Variables
    colWidths = [];
    numCols = $("table").find("tr")[0].cells.length;

    // Store column widths 
    $(".column-content").each(function (index) {
        if (index < numCols) {
            colWidths[index] = $(this).css("width");
        }
    });

    // Apply column widths to header
    $(".column-header").each(function (index) {
        $(this).css("width", colWidths[index]);
    });

}