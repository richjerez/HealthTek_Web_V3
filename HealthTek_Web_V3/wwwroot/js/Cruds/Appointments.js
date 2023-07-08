
function getFields() {
    var empServicecodes = document.getElementById("showHide");
    var client = document.getElementById("client");
    var endTime = document.getElementById("endTime");
    var location = document.getElementById("LocationShowHide");
    $("#AppointmentType option:selected").each(function () {
        var $this = $(this);
        if ($this.length) {
            var selText = $this.text();
            if (selText === "Services") {
                empServicecodes.style.display = "flex";
                client.style.display = "block";
                endTime.style.display = "block";
                location.style.display = "flex";
            } else {
                empServicecodes.style.display = "none";
                client.style.display = "none";
                endTime.style.display = "none";
                location.style.display = "none";
            }
        }
    });
}
function getClientLocations() {
    $("#FkClientsId option:selected").each(function () {
        var $this = $(this);
        if ($this.length) {
            var selText = $this.val();
            $.ajax({
                type: 'Get',
                url: "/Appointments/GetLocation",
                dataType: 'json',
                data: { patId: selText },
                success: function (locations) {
                    for (var i = 0; i < locations.length; i++) {
                        $("#FkStartLocationId").empty()
                        $("#FkEndLocationId").empty()
                        $("#FkStartLocationId").append('<option value="' + locations[i].value + '">' + locations[i].text + '</option>');
                        $("#FkEndLocationId").append('<option value="' + locations[i].value + '">' + locations[i].text + '</option>');
                        $('.ModalSelect').selectpicker();
                    }
                },
                error: function (xhr, status, error) {
                    alert("Failed");
                }
            });

        }
    });

}
function getCalendar(data) {
    var date = new Date();

    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialDate: date,
        weekends: false,
        slotMinTime: "08:00:00",
        slotMaxTime: "17:00:00",
        slotDuration: "00:15:00",
        slotLabelInterval: "00:15:00",
        initialView: 'timeGridWeek',
        nowIndicator: true,
        headerToolbar: {
            left: 'prevYear,prev,today,next,nextYear',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
        },
        navLinks: true, // can click day/week names to navigate views
        editable: true,
        selectable: true,
        selectMirror: true,
        dateClick: function (arg) {
            AddEditElements(0, 'Appointments');
            calendar.unselect()
        },
        eventClick: function (arg) {
            var id = parseInt(arg.event.id);
            arg.jsEvent.preventDefault(); // don't let the browser navigate
            AddEditElements(id, 'Appointments');

        },
        eventDrop: function (info) {
            var senddata = { id: info.event.id, date: info.event.start.toISOString(), flag: 'start'}
            $.ajax({
                type: 'Get',
                url: '/Appointments/UpdateDates',
                dataType: 'json',
                data: senddata,
                success: function (Data) {
                    //Perform After Task
                },
                error: function (xhr, status, error) {
                    info.revert();
                }
            });
        },
        eventResize: function (info) {
            var senddata = { id: info.event.id, date: info.event.end.toISOString(), flag: 'end'}
            $.ajax({
                type: 'Get',
                url: '/Appointments/UpdateDates',
                dataType: 'json',
                data: senddata,
                success: function (Data) {
                    //Perform After Task
                },
                error: function (xhr, status, error) {
                    info.revert();
                }
            });

        },
        dayMaxEvents: true, // allow "more" link when too many events
        events: data
    });

    calendar.render();
}
function getEntityCalendar(data,ids) {
    var date = new Date();

    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialDate: date,
        height: "auto",
        weekends: false,
        slotMinTime: "08:00:00",
        slotMaxTime: "17:00:00",
        slotDuration: "00:15:00",
        slotLabelInterval: "00:15:00",
        initialView: 'timeGridWeek',
        nowIndicator: true,
        headerToolbar: {
            left: 'prevYear,prev,today,next,nextYear',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
        },
        navLinks: true, // can click day/week names to navigate views
        editable: true,
        selectable: true,
        selectMirror: true,
        dateClick: function (arg) {
            AddEditElements(0, 'Appointments');
            calendar.unselect()
        },
        eventClick: function (arg) {
            var id = parseInt(arg.event.id);
            arg.jsEvent.preventDefault(); // don't let the browser navigate
            AddEditElements(id, 'Appointments',false, ids);

        },
        eventDrop: function (info) {
            var senddata = { id: info.event.id, date: info.event.start.toISOString(), flag: 'start'}
            $.ajax({
                type: 'Get',
                url: '/Appointments/UpdateDates',
                dataType: 'json',
                data: senddata,
                success: function (Data) {
                    //Perform After Task
                },
                error: function (xhr, status, error) {
                    info.revert();
                }
            });
        },
        eventResize: function (info) {
            var senddata = { id: info.event.id, date: info.event.end.toISOString(), flag: 'end'}
            $.ajax({
                type: 'Get',
                url: '/Appointments/UpdateDates',
                dataType: 'json',
                data: senddata,
                success: function (Data) {
                    //Perform After Task
                },
                error: function (xhr, status, error) {
                    info.revert();
                }
            });

        },
        dayMaxEvents: true, // allow "more" link when too many events
        events: data
    });

    calendar.render();
}
