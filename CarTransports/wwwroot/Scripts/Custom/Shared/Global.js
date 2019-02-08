$(function () {

    $(window).resize(function () {
        adjustBodyWidth();
    });

    $("#navMobileToggle").click(function () {
        $("#outerWrapper").toggleClass("open");
    });

    adjustBodyWidth();

    $('.ui.dropdown').dropdown();

});

function adjustBodyWidth() {

    bodyWidth = $("body").width();
    navMobileWidth = $("#navMobile").width();

    $("#outerWrapper").width(bodyWidth + navMobileWidth);

}
