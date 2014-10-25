function geoFindMe(success, error) {
	var output = document.getElementById("out");

	if (!navigator.geolocation){
		output.innerHTML = "<p>Geolocation is not supported by your browser</p>";
		return;
	}

	function internalSuccess(position) {
		var latitude  = position.coords.latitude;
		var longitude = position.coords.longitude;
		output.innerHTML = "<p>Located: " + latitude + " " + longitude + "</p>";
		success(latitude, longitude);
	};
	
	output.innerHTML = "<p>Locating</p>";

	navigator.geolocation.watchPosition(internalSuccess, error, { 
	    enableHighAccuracy: true,
	    maximumAge: 0
	});
}