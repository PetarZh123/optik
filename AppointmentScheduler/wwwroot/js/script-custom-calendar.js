console.log("In custom script");
/*
document.addEventListener('DOMContentLoaded', function InitializeCalendar() {
    try {
        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null) {
            var calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
                headerToolbar: {
                    left: 'prev,next,today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                selectable: true,
                editable: false,
                events: function (fetchInfo, successCallback, failureCallback) {
                    console.log("Fetching events...");
                    $.ajax({
                        url: '/api/Appointment/GetAppointments',
                        method: 'GET',
                        dataType: 'json',
                        success: function (data) {
                            console.log("Data fetched:", data);
                            var events = data.map(function (appointment) {
                                return {
                                    id: appointment.id,
                                    title: appointment.title,
                                    start: appointment.start,
                                    end: appointment.end,
                                    description: appointment.description
                                };
                            });
                            console.log("Mapped events:", events);
                            successCallback(events);
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.error("Error fetching events:", textStatus, errorThrown);
                            failureCallback();
                        }
                    });
                },
                select: function (info) {
                    console.log("User selected from:", info.start, "to", info.end);

                    $("#appointmentInput").modal("show");
                    $("#title").val("");
                    $("#id").val("");
                    $("#startDate").val(info.start.toISOString());
                    $("#endDate").val(info.end.toISOString());
                },
                //eventClick: function (info) {
                //    alert("Title: " + info.event.title + "\n" +
                //        "Start: " + info.event.start.toLocaleString() + "\n" +
                //        "End: " + info.event.end.toLocaleString() + "\n" +
                //        "Description: " + (info.event.extendedProps.description || "No description"));
                //}
            });
            calendar.render();
        }
    }
    catch (e) {
        alert(e);
    }
});
*/

$('document').ready(function InitializeCalendar() {
    try {
        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null) {
            var calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
                headerToolbar: {
                    left: 'prev,next,today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                selectable: true,
                editable: false,
                events: function (fetchInfo, successCallback, failureCallback) {
                    console.log("Fetching events...");
                    $.ajax({
                        url: '/api/Appointment/GetAppointments',
                        method: 'GET',
                        dataType: 'json',
                        success: function (data) {
                            console.log("Data fetched:", data);
                            var events = data.map(function (appointment) {
                                return {
                                    id: appointment.id,
                                    title: appointment.title,
                                    start: appointment.start,
                                    end: appointment.end,
                                    description: appointment.description
                                };
                            });
                            console.log("Mapped events:", events);
                            successCallback(events);
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.error("Error fetching events:", textStatus, errorThrown);
                            failureCallback();
                        }
                    });
                },
                select: function (info) {
                    console.log("User selected from:", info.start, "to", info.end);

                    $("#appointmentInput").modal("show");
                    $("#title").val("");
                    $("#id").val("");
                    $("#startDate").val(info.start.toISOString());
                    $("#endDate").val(info.end.toISOString());
                },
                eventClick: function (info) {
                    alert("Title: " + info.event.title + "\n" +
                        "Start: " + info.event.start.toLocaleString() + "\n" +
                        "End: " + info.event.end.toLocaleString() + "\n" +
                        "Description: " + (info.event.extendedProps.description || "No description"));
                }
            });
            calendar.render();
        }
    }
    catch (e) {
        alert(e);
    }
});

//document.addEventListener('DOMContentLoaded', function InitializeCalendar() {
//    var calendarEl = document.getElementById('calendar');
//    if (calendarEl != null) {
//        var calendar = new FullCalendar.Calendar(calendarEl, {
//            initialView: 'dayGridMonth',
//            selectable: true,
//            editable: false,
//            headerToolbar: {
//                left: 'prev,next,today',
//                center: 'title',
//                right: 'dayGridMonth,timeGridWeek,timeGridDay'
//            },
//            select: function (info) {
//                console.log("User selected from:", info.start, "to", info.end);

//                // Store the clicked start and end times in the hidden input fields
//                $("#startTime").val(info.start.toISOString());
//                $("#endTime").val(info.end.toISOString());

//                // Open the modal for input
//                $("#appointmentInput").modal("show");
//            }
//        });
//        calendar.render();
//    }
//});

// Handle form submission via AJAX
$("#appointmentForm").off("submit").on("submit", function (event) {
    event.preventDefault(); // Prevent default form submission

    var formData = {
        id: $("#id").val(),
        title: $("#title").val(),
        description: $("#description").val(),
        patientId: $("#patientId").val(),
        doctorId: $("#doctorId").val(),
        startDate: $("#startDate").val(),
        endDate: $("#endDate").val()
    };

    console.log("Submitting data:", formData);

    $.ajax({
        url: "/api/Appointment/SaveCalendarData",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(formData),
        success: function (response) {
            alert("Appointment saved successfully!");
            $("#appointmentInput").modal("hide");
            location.reload(); // Refresh the calendar
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error("Error saving appointment:", textStatus, errorThrown);
            alert("Failed to save appointment. Please try again.");
        }
    });
});


function onShowModal(obj, isEventDetail) {
    $("#appointmentInput").modal("show");
}

function onCloseModal() {
    $("#appointmentInput").modal("hide");
}

