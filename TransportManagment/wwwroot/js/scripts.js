


function toGoogleString(str) {
    let replacedStr = str.replace(/\s+/g, '+').replace(',', '').replace('.', '');
    console.log(replacedStr);
    return replacedStr;
}

function httpGet(url) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", url, false); 
    xmlHttp.send(null);
    return xmlHttp.responseText;
}

 

function DisplayGoogleMap() {

    var destination = document.getElementById("Destination").value;
    var departure = document.getElementById("Departure").value;
    destination = toGoogleString(destination);
    departure = toGoogleString(departure);
    var destinationUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=" + destination + "&key=AIzaSyAtoW26znh3xMpWu5yqdHDdKTqZEJztJ8Q";
    var departureUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=" + departure + "&key=AIzaSyAtoW26znh3xMpWu5yqdHDdKTqZEJztJ8Q";
    var geoDestResponse = JSON.parse(httpGet(destinationUrl));
    var geoDeparResponse = JSON.parse(httpGet(departureUrl));

    var latDestCoord = geoDestResponse.results[0].geometry.location.lat;
    var lngDestCoord = geoDestResponse.results[0].geometry.location.lng;

    var latDepartCoord = geoDeparResponse.results[0].geometry.location.lat;
    var lngDepartCoord = geoDeparResponse.results[0].geometry.location.lng;

    var centerAdress = new google.maps.LatLng((latDestCoord + latDepartCoord) / 2, (lngDestCoord + lngDepartCoord) / 2);
   
    var mapOptions = {
        center: centerAdress,
        zoom: 5,
        minZoom: 5,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };


    var map = new google.maps.Map(document.getElementById("myDiv"), mapOptions);

    this.shortestRoute(departure, destination, map)
}


function shortestRoute(origin, destination, map) {

    directionsService = new google.maps.DirectionsService();
    var routesResponses = [];
   
    directionsService.route({
        origin: origin,
        destination: destination,
        provideRouteAlternatives: true,
        avoidTolls: true,
        travelMode: google.maps.TravelMode.DRIVING
    }, function (response, status) {
        if (status === google.maps.DirectionsStatus.OK) {
            routesResponses.push(response);
        } else {
            window.alert('Directions request failed due to ' + status);
        }
    });
   
    directionsService.route({
        origin: origin,
        destination: destination,
        provideRouteAlternatives: true,
        avoidHighways: true,
        travelMode: google.maps.TravelMode.DRIVING
    }, function (response, status) {
        if (status === google.maps.DirectionsStatus.OK) {
            routesResponses.push(response);
        } else {
            window.alert('Directions request failed due to ' + status);
        }


        var fastest = Number.MAX_VALUE,
            shortest = Number.MAX_VALUE;

        routesResponses.forEach(function (res) {
            res.routes.forEach(function (rou, index) {
                console.log("distance of route " + index + ": ", rou.legs[0].distance.value);
                console.log("duration of route " + index + ": ", rou.legs[0].duration.value);
                if (rou.legs[0].distance.value < shortest) shortest = rou.legs[0].distance.value;
                if (rou.legs[0].duration.value < fastest) fastest = rou.legs[0].duration.value;

            })
        })
        document.getElementById('Distance').innerHTML = "Distance: " + shortest/1000 + " km";
        console.log("shortest: ", shortest);
        console.log("fastest: ", fastest);
    
        routesResponses.forEach(function (res) {
            res.routes.forEach(function (rou, index) {
                new google.maps.DirectionsRenderer({
                    map: map,
                    directions: res,
                    routeIndex: index,
                    suppressMarkers: true,
                    polylineOptions: {
                        strokeColor: rou.legs[0].duration.value == fastest ? "red" : rou.legs[0].distance.value == shortest ? "aqua" : "blue",
                        strokeOpacity: rou.legs[0].duration.value == fastest ? 0.8 : rou.legs[0].distance.value == shortest ? 0.9 : 0.5,
                        strokeWeight: rou.legs[0].duration.value == fastest ? 9 : rou.legs[0].distance.value == shortest ? 8 : 3,
                    }
                });
            });
        });
    });
}

google.maps.event.addDomListener(window, 'load', DisplayGoogleMap);
