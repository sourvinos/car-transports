function displayMap() {

    var lat = $("#Lat").val();
    var lng = $("#Lng").val();

    lat = lat.replace(",", ".");
    lng = lng.replace(",", ".");

    $("#myMap").dialog("open");

    var map = new Microsoft.Maps.Map(document.getElementById("myMap"), {
        center: new Microsoft.Maps.Location(lat, lng),
        mapTypeId: Microsoft.Maps.MapTypeId.aerial,
        zoom: 16
    });

}
