

// User Submititon -- Geolocation call
function getGpsAddress() {
    const addressCommon = getRecaptchaKey();
    var gpslat = document.getElementById("Locations_GpsLatitude");
    var gpsltd = document.getElementById("Locations_GpsLongitude");
    var address = document.getElementById("Locations_Address").value;
    var city = document.getElementById("Locations_City").value;
    var state = document.getElementById("Locations_State").value;
    var zip = document.getElementById("Locations_Zipcode").value;
    var country = document.getElementById("Locations_Country").value;
    var fulladdress = address.split(' ').join('+') + "," + city.split(' ').join('+') + "," + state.split(' ').join('+') + "," + zip.split(' ').join('+') + "," + country.split(' ').join('+');
    let url = 'https://maps.googleapis.com/maps/api/geocode/json?address=' + fulladdress + '&key=' + addressCommon;
    if (gpslat === "" || gpsltd === "") {
        fetch(url).then(response => response.json())
            .then((data) => {
                gpslat.value = data.results[0].geometry.location.lat;
                gpsltd.value = data.results[0].geometry.location.lng;
            }).catch(err => console.log(err.message)).then(result => {
                document.getElementById("myForm").submit();

            });
    } else { document.getElementById("myForm").submit(); }
}
