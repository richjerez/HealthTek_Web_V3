
        // <-- Supervision Fields Handler Starts -->
function getSupervisorFields() {
    var e = document.getElementById("Supervisor");
    $("#Roles option:selected").each(function () {
        var $this = $(this);
        var selText = $this.text();
        if ($this.length) {
            if (selText === "Supervisor") {
                e.removeAttribute("class", "d-none");
            } else {
                e.setAttribute("class", "d-none");
            }
        }
    });
}
        // <-- Supervision Fields Handler End -->
function getFields() {
    var supervisor = document.getElementById("super");
    $("#FkRoleNamesId option:selected").each(function () {
        var $this = $(this);
        if ($this.length) {
            var selText = $this.text();
            if (selText === "Supervisor") {
                supervisor.style.display = "block";
            } else {
                supervisor.style.display = "none";
            }
        }
    });
}

// User Submititon -- Geolocation call
function getGpsAddress() {
    var gpslat = document.getElementById("Locations_GpsLatitude").value;
    var gpsltd = document.getElementById("Locations_GpsLongitude").value;
    const addressCommon = getGeoCodingKey();
    var address = document.getElementById("Locations_Address").value;
    var city = document.getElementById("Locations_City").value;
    var state = document.getElementById("Locations_State").value;
    var zip = document.getElementById("Locations_Zipcode").value;
    //var country = document.getElementById("Locations_Country").value;
    var fulladdress = address.split(' ').join('+') + "," + city.split(' ').join('+') + ","
        + state.split(' ').join('+') + "," + zip.split(' ').join('+');
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

function getRoleDetails() {
    var typesName = document.getElementById("Role");
    var args = typesName.options[typesName.selectedIndex].text;
    var clientid = $("#FkEmployeesId").val();
    var data = { id: clientid, type: 'HR Chart', role: args };
    getDocumentTitle(data);
}
function getDocumentTitle(dataVal) {
    $.ajax({
        type: 'Get',
        url: "/Documents/GetLists",
        dataType: 'json',
        data: dataVal,
        success: function (intake) {
            $("#documentTitle").style.display = "block";
            $("#DocumentTitle").empty()
            for (var i = 0; i < intake.length; i++) {
                $("#DocumentTitle").append('<option value="' + intake[i].value + '">' + intake[i].text + '</option>');
            }
        },
        error: function (xhr, status, error) {
            $("#DocumentTitle").empty();
            $("#DocumentTitle").append('<option value="">There are no active roles!</option>');
        }
    });
}
