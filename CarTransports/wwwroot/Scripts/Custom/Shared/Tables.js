// Hightlight row on hover
$(".row.clickable").mouseenter(function () {
    $(this).addClass("hover");
}).mouseleave(function () {
    $(this).removeClass("hover");
});

// Select, Edit, Delete, Invoice
$(".action").click(function (event) {

    var id = $(this).parent().attr("id");
    var action = $(this).attr("class");
    var pathname = "";

    switch (action) {
        case "action edit-icon":
            pathname = window.location.pathname.split('/')[1];
            window.location.href = "/" + pathname + "/" + "edit" + "/" + id;
            event.stopPropagation();
            break;
        case "action bin-icon":
            pathname = window.location.pathname.split('/')[1];
            window.location.href = "/" + pathname + "/" + "delete" + "/" + id;
            event.stopPropagation();
            break;
        case "action invoice-icon " + id:
            $("#createInvoice").trigger("click");
            event.stopPropagation();
            break;
        case "action check-icon":
            event.stopPropagation();
    }

});

// Trigger clicks
$(".back-icon").click(function () {
    window.history.back();
});
$(".ok-icon").click(function () {
    $("#ok-icon")[0].click();
});
$(".add-icon").click(function () {
    $("#add-icon")[0].click();
});
$(".delete-icon").click(function () {
    $("#delete-icon")[0].click();
});
