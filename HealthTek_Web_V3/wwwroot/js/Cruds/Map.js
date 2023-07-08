function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 16,
        center: bbc
    });
    const legend = document.getElementById("legend");
    const icons = {
        parking: {
            name: "Clients",
            icon: "http://maps.google.com/mapfiles/ms/icons/green-dot.png",
        },
        library: {
            name: "Providers",
            icon: "http://maps.google.com/mapfiles/ms/icons/blue-dot.png",
        },
        info: {
            name: "Facilities",
            icon: "http://maps.google.com/mapfiles/ms/icons/red-dot.png",
        },
    };
    for (const key in icons) {
        const type = icons[key];
        const name = type.name;
        const icon = type.icon;
        const div = document.createElement("div");
        div.innerHTML = '<img src="' + icon + '" style="display: block;margin: auto;"> ' + name;
        legend.appendChild(div);
    }
    const btndiv = document.createElement("div");
    btndiv.innerHTML = '<button class="btn btn-info mr-2" onclick="clearggMap()">Clear Map</button>';
    const filterDiv = document.createElement("div");
    filterDiv.innerHTML = '<li style="list-style: none;" class="nav-item dropdown no-arrow show"><a class="nav-link dropdown-toggle mr-2 shadow btn btn-primary" href="#" id="tasksDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" ><i class="fas fa-filter fa-sm"></i></a ><div class="dropdown-list dropdown-menu dropdown-menu-right shadow animated--grow-in" aria-labelledby="tasksDropdown" style="position: absolute; transform: translate3d(-114px, 38px, 0px); top: 0px; left: 0px; will-change: transform;" x-placement="bottom-end"><h6 class="dropdown-header">Filter By</h6><a onclick="setEmployees(\'RBT\')"  class="dropdown-item d-flex align-items-center" href="#">RBT</a><a onclick="setEmployees(\'BCBA\')" class="dropdown-item d-flex align-items-center" href="#">BCBA</a><a onclick="setEmployees(\'BCaBa\')" class="dropdown-item d-flex align-items-center" href="#">BCaBa</a><a onclick="setEmployees(\'Supervisor\')" class="dropdown-item d-flex align-items-center" href="#">Supervisor</a></div></li >';

    map.controls[google.maps.ControlPosition.RIGHT_TOP].push(btndiv);
    map.controls[google.maps.ControlPosition.RIGHT_TOP].push(filterDiv);
    map.controls[google.maps.ControlPosition.RIGHT_BOTTOM].push(legend);
    addMarker(bbc, "<span class='text-secondary'>Bio-Bevahvioral-Corp</span>", 2);

    oTable = $('#dataTable1').dataTable();
    const boxes = document.querySelectorAll('#dataTable1 tr');
    oTable.$('tr').click(function () {
        boxes.forEach(box => {
            box.classList.remove('selected');
        });

        $(this).toggleClass('selected');
        if ($(this).hasClass('selected')) {
            var id = parseInt(oTable.fnGetData(this)[0]);
            var name = oTable.fnGetData(this)[3].substring(0, oTable.fnGetData(this)[3].indexOf("<"));
            var chartStep = oTable.fnGetData(this)[3].substring(oTable.fnGetData(this)[3].indexOf(">"));
            var chart = chartStep.substring(1, chartStep.indexOf("<"));
            var addressStep = chartStep.substring(chartStep.indexOf("</small>"));
            var address = addressStep.substring(8, addressStep.indexOf("<small"));
            var text = "<button type='button' onclick='AddEditElements(" + id + " , \"Assignments\" )' class='btn btn-success' style='margin: auto;display: block;'>Assign</button><p class='text-secondary' style='margin-top:10px'><strong style='display:block'>" + name + "</strong><small style='display:block'>" + chart + '</small>' + address + '</p> ';
            var ltd = parseFloat(oTable.fnGetData(this)[1]);
            var lng = parseFloat(oTable.fnGetData(this)[2]);
            var newLocation = { lat: ltd, lng: lng };
            var mark = addMarker(newLocation, text, 0, true);
            map.setZoom(10);
        }
    });
    getInitData('', true);

}
// Adds a marker to the map and push to the array.
function addMarker(location, text, markerIndex, showMarker,flag) {
    const marker = new google.maps.Marker({
        position: location,
        center: location,
        map: map,
    });
    var markerIcons = ["http://maps.google.com/mapfiles/ms/icons/green-dot.png", "http://maps.google.com/mapfiles/ms/icons/blue-dot.png", "http://maps.google.com/mapfiles/ms/icons/red-dot.png"]
    marker.setIcon(markerIcons[markerIndex]);
    // Adds a marker at the center of the map.
    const contentString = text;
    var selected = 0;
    const infowindow = new google.maps.InfoWindow({
        content: contentString,
    });
    marker.addListener("click", () => {
        infowindow.open(map, marker);
        if (selected != 0) {
            selected = 0;
        } else {
            selected = 1;
        }
    });
    if (showMarker) {
        infowindow.open(map, marker);
    }
    marker.addListener('mouseover', function () {
        infowindow.open(map, marker);
    });
    map.addListener('click', function () {
        infowindow.close();
    });
    // assuming you also want to hide the infowindow when user mouses-out
    marker.addListener('mouseout', function () {
        if (selected == 0) {
            infowindow.close();
        }
    });
    document.querySelectorAll('#dataTable1 tr')
        .forEach(e => e.addEventListener("click", function () {
            if (selected == 0) {
                infowindow.close();
            } else {
                this.classList.add('selected');
                infowindow.open(map, markers);
            }

        }));

    if (flag) {
        clientmarkers.push(marker);
    }
    markers.push(marker);
    window.setTimeout(() => {
        map.panTo(marker.getPosition());
    }, 1000);
}
// Sets the map on all markers in the array.
function setMapOnAll(map) {
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}
function clearggMap() {
    var all = $("tbody")[0];
    for (var i = 0; i < all.children.length; i++) {
        all.children[i].classList.remove("selected");
    }
    deleteMarkers();
    getInitData();
}
// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
    setMapOnAll(null);
    addMarker(bbc, "Bio-Bevahvioral-Corp", 2);
}
// Shows any markers currently in the array.
function showMarkers() {
    setMapOnAll(map);
}
// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
    clearMarkers();
    markers = [];
}

function setEmployees(text) {
    var all = $("tbody")[0];
    for (var i = 0; i < all.children.length; i++) {
        all.children[i].classList.remove("selected");
    }
    var emp = "";
    if (text == null) {
        emp = allEmployees;
        setEmp(emp);
    } else {
        clearMarkers();
        markers = clientmarkers;
        getData(text);
    }

}
function getInitData() {
    $.ajax({
        type: 'Get',
        url: '/Assignments/GetAllAssignments/',
        dataType: 'json',
        success: function (empData) {
            setMarkers(empData);
        },
        error: function (xhr, status, error) {
            alert("Failed xhr:" + xhr + "\nerror:" + error);
        }
    });

}
function getData(role) {
    $.ajax({
        type: 'Get',
        url: '/Assignments/getEmployees/',
        dataType: 'json',
        data: { role: role },
        success: function (empData) {
            setMarkers(empData);
        },
        error: function (xhr, status, error) {
            alert("Failed xhr:" + xhr + "\nerror:" + error);
        }
    });

}
function setMarkers(newMarkers) {

    for (var i = 0; i < newMarkers.length; i++) {
        var data = newMarkers[i];
        var text = data.html;
        var ltd = parseFloat(data.lat);
        var lng = parseFloat(data.lng);
        var newLocation = { lat: ltd, lng: lng };
        addMarker(newLocation, text, data.icon, false, true);
        map.setZoom(10);

    }

}

