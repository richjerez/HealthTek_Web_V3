// Creates a chart number for client
// If first and last name exhists create chart number
// with first character of each plus 6 random characters
// <-- Chart Start -->
function createChartNumber() {
    var firstname = document.getElementById("FirstName").value;
    var lastname = document.getElementById("LastName").value;
    var chartNumber = "";
    if (firstname == "" || lastname == "") {
        chartNumber = MakeRandomChart(2) + "-" + MakeRandomChart(6);
    } else {
        chartNumber = firstname.substring(0, 1) + lastname.substring(0, 1) + "-" + MakeRandomChart(6);
    }
    var chartText = document.getElementById("ChartNumber");
    chartText.value = chartNumber.toUpperCase();
}
// Makes random character depending on the number given
function MakeRandomChart(length) {
    var result = '';
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() *
            charactersLength));
    }
    return result;
}
// <-- Chart End -->

// User Submititon -- Geolocation call
function getGpsAddress() {
    const addressCommon = "AIzaSyCZmOkQop3E9_g1Y-eVKw6YVl0Nu4_sdfs";
    var gpslat = document.getElementById("GpsLatitude").value;
    var gpsltd = document.getElementById("GpsLongitude").value;
    var address = document.getElementById("Address").value;
    var city = document.getElementById("City").value;
    var state = document.getElementById("State").value;
    var zip = document.getElementById("Zipcode").value;
    //var country = document.getElementById("Country").value;
    var fulladdress = address.split(' ').join('+') + "," + city.split(' ').join('+') + "," + state.split(' ').join('+') + "," + zip.split(' ').join('+');
    let url = 'https://maps.googleapis.com/maps/api/geocode/json?address=' + fulladdress + '&key=' + addressCommon;
    if (gpslat === "" || gpsltd === "") {
        fetch(url).then(response => response.json())
            .then((data) => {
                gpslat = data.results[0].geometry.location.lat;
                gpsltd = data.results[0].geometry.location.lng;
            }).catch(err => console.log(err.message)).then(result => {
                document.getElementById("myForm").submit();
            });
    } else { document.getElementById("myForm").submit(); }
}
