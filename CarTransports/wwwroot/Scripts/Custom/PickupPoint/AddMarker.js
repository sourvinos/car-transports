function addMarker(map, properties) {

    var marker = new google.maps.Marker({
        position: properties.coords,
        map: map,
        title: properties.title,
        icon: "../images/" + properties.image + ".png",
        label: properties.label,
        draggable: true
    });

    var contentString = "<b>" + properties.description + "</b>" + "<br/>" + properties.zip + " " + properties.city + "<br/>" + properties.address + "<br/>" + properties.country + "<br/>" + properties.workingHours + "<br/>";

    var infowindow = new google.maps.InfoWindow({
        content: contentString
    });

    marker.addListener("click", function () {
        infowindow.open(map, marker);
    });

    google.maps.event.addListener(marker, 'dragend', function (marker) {
        var latLng = marker.latLng;
        $("#Lat").val(latLng.lat().toFixed(5));
        $("#Lng").val(latLng.lng().toFixed(5));
    });

}
