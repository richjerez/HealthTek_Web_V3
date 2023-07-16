function AddEditElements(itemId, ItemClass, details, optionalClass,optionalParameter) {
    var url;
    if (itemId != 0) {
        url = "/" + ItemClass + "/Edit/" + itemId;
    }
    else {
        url = "/" + ItemClass + "/Create/";

    }
    switch (ItemClass) {
        case "Diagnosis":
            $("#addeditModallabel").text("Manage Diagnosis");
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Diagnosis")
                    break;
                default:
                    $("#modalSubHeading").text("Update Diagnosis")
                    break;
            }
            break;
        case "EnvironmentalChanges":
            $("#addeditModallabel").text("Manage Environmental Changes");
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?" + optionalParameter + "=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Environmental Change")
                    break;
                default:
                    $("#modalSubHeading").text("Update Environmental Change")
                    break;
            }
            break;
        case "CaregiverCompetencies":
            $("#addeditModallabel").text("Manage Caregiver Competencies");
            $(".modal-dialog").addClass("modal-xl");
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Caregiver Competency")
                    break;
                default:
                    $("#modalSubHeading").text("Update Caregiver Competency")
                    details = false;
                    break;
            }
            break;
        case "Intakes":
            $("#addeditModallabel").text("Manage Intakes");
            if (details) {
                $(".modal-dialog").addClass("modal-lg")
            }
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Intake")
                    break;
                default:
                    $("#modalSubHeading").text("Update Intake")
                    break;
            }
            break;
        case "RbtCompetencies":
            $("#addeditModallabel").text("Manage Rbt Competencies");
            $(".modal-dialog").addClass("modal-xl")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Competency")
                    break;
                default:
                    $("#modalSubHeading").text("Update Competency")
                    break;
            }
            break;
        case "Facilities":
            $("#addeditModallabel").text("Manage Facility");
            $(".modal-dialog").addClass("modal-lg")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Facility")
                    break;
                default:
                    $("#modalSubHeading").text("Update Facility")
                    break;
            }
            break;
        case "Interventions":
            $("#addeditModallabel").text("Manage Interventions");
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Intervention")
                    break;
                default:
                    $("#modalSubHeading").text("Update Intervention")
                    break;
            }
            break;
        case "SupportTickets":
            $("#addeditModallabel").text("Manage Support Tickets");
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Ticket")
                    break;
                default:
                    $("#modalSubHeading").text("Update Ticket")
                    break;
            }
            break;
        case "DashboardSettings":
            $("#addeditModallabel").text("Manage Dashboard");
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Dashboard")
                    break;
                default:
                    $("#modalSubHeading").text("Update Dashboard")
                    break;
            }
            break;
        case "AbcReports":
            $("#addeditModallabel").text("Manage Abc Reports");
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Abc Report")
                    break;
                default:
                    $("#modalSubHeading").text("Update Abc Report")
                    break;
            }
            break;
        case "UserRoles":
            $("#addeditModallabel").text("Manage User Roles")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New User Role")
                    break;
                default:
                    $("#modalSubHeading").text("Update User Role")
                    break;
            }
            break;
        case "Messages":
            switch (itemId) {
                default:
                    $("#addeditModallabel").text("Contact Us");
                    $("#modalSubHeading").text("Send us a message");
                    break;
            }
            break;
        case "IntakeDocsCatalog":
            $("#addeditModallabel").text("Manage Intake Doc")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Intake Doc")
                    break;
                default:
                    $("#modalSubHeading").text("Update Intake Doc")
                    break;
            }
            break;
        case "ServiceCodes":
            $("#addeditModallabel").text("Manage Service Code")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Service Code")
                    break;
                default:
                    $("#modalSubHeading").text("Update Service Code")
                    break;
            }
            break;
        case "OperatingCounties":
            $("#addeditModallabel").text("Manage Operating County")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Operating County")
                    break;
                default:
                    $("#modalSubHeading").text("Update Operating County")
                    break;
            }
            break;
        case "ClientInsurancesCatalog":
            $("#addeditModallabel").text("Manage Insurance Plan")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Insurance Plan")
                    break;
                default:
                    $("#modalSubHeading").text("Update Insurance Plan")
                    break;
            }
            break;
        case "ClientEvents":
            $("#addeditModallabel").text("Manage Event Type")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Event Type")
                    break;
                default:
                    $("#modalSubHeading").text("Update Event Type")
                    break;
            }
            break;
        case "ClientEventTypesCatalog":
            $("#addeditModallabel").text("Manage Event Type")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Event Type")
                    break;
                default:
                    $("#modalSubHeading").text("Update Event Type")
                    break;
            }
            break;
        case "MaladaptivesInterventions":
            $("#addeditModallabel").text("Manage Maladaptive Interventions")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Maladaptive Interventions")
                    break;
                default:
                    $("#modalSubHeading").text("Update Maladaptive Interventions")
                    break;
            }
            break;
        case "Maladaptives":
            $("#addeditModallabel").text("Manage Maladaptive")
            $(".modal-dialog").addClass("modal-lg")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Maladaptive")
                    break;
                default:
                    $("#modalSubHeading").text("Update Maladaptive")
                    break;
            }
            break;
        case "MaladaptivesCatalog":
            $("#addeditModallabel").text("Manage Maladaptive")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Maladaptive")
                    break;
                default:
                    $("#modalSubHeading").text("Update Maladaptive")
                    break;
            }
            break;
        case "ReplacementsCatalog":
            $("#addeditModallabel").text("Manage Replacement")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Replacement")
                    break;
                default:
                    $("#modalSubHeading").text("Update Replacement")
                    break;
            }
            break;
        case "ReinforcerCatalog":
            $("#addeditModallabel").text("Manage Reinforcer")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Reinforcer")
                    break;
                default:
                    $("#modalSubHeading").text("Update Reinforcer")
                    break;
            }
            break;
        case "Functions":
            $("#addeditModallabel").text("Manage Function")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Function")
                    break;
                default:
                    $("#modalSubHeading").text("Update Function")
                    break;
            }
            break;
        case "BaAssessmentsInterventions":
            $("#addeditModallabel").text("Manage Intervention")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Intervention")
                    break;
                default:
                    $("#modalSubHeading").text("Update Intervention")
                    break;
            }
            break;
        case "Locations":
            $("#addeditModallabel").text("Manage Addresses")
            $(".modal-dialog").addClass("modal-lg")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Address")
                    break;
                default:
                    $("#modalSubHeading").text("Update Address")
                    break;
            }
            break;
        case "Comments":
            $("#addeditModallabel").text("Manage Comments")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Comment")
                    break;
                default:
                    $("#modalSubHeading").text("Update Comment")
                    break;
            }
            break;
        case "PreferencesCatalog":
            $("#addeditModallabel").text("Manage Preferences")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Preference")
                    break;
                default:
                    $("#modalSubHeading").text("Update Preference")
                    break;
            }
            break;
        case "EnvironmentalsCatalog":
            $("#addeditModallabel").text("Manage Environmental")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Environmental")
                    break;
                default:
                    $("#modalSubHeading").text("Update Environmental")
                    break;
            }
            break;
        case "CaregiverCompChecksCatalog":
            $("#addeditModallabel").text("Manage Training Item")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Training Item")
                    break;
                default:
                    $("#modalSubHeading").text("Update Training Item")
                    break;
            }
            break;
        case "CaregiverFeedback":
            $("#addeditModallabel").text("Manage Feedback Item")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Feedback Item")
                    break;
                default:
                    $("#modalSubHeading").text("Update Feedback Item")
                    break;
            }
            break;
        case "RbtCompTrainingsCatalog":
            $("#addeditModallabel").text("Manage Training Item")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Training Item")
                    break;
                default:
                    $("#modalSubHeading").text("Update Training Item")
                    break;
            }
            break;
        case "RoleNames":
            $("#addeditModallabel").text("Manage Employee Roles")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Employee Role")
                    break;
                default:
                    $("#modalSubHeading").text("Update Employee Role")
                    break;
            }
            break;
        case "RoleDocsCatalog":
            $("#addeditModallabel").text("Manage HR Requirement")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New HR Requirement")
                    break;
                default:
                    $("#modalSubHeading").text("Update HR Requirement")
                    break;
            }
            break;
        case "Appointments":
            $("#addeditModallabel").text("Manage Appointments")
            $(".modal-dialog").addClass("modal-lg")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Appointment")
                    break;
                default:
                    $("#modalSubHeading").text("Update Appointment")
                    break;
            }
            break;
        case "Assignments":
            $("#addeditModallabel").text("Manage Assignments");
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass + "&client=" + optionalParameter;
                    }
                    $("#modalSubHeading").text("New Assignment")
                    break;
                default:
                    $("#modalSubHeading").text("Update Assignment")
                    break;
            }
            break;
        case "Widgets":
            $("#addeditModallabel").text("Manage Widgets");
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Widget")
                    break;
                default:
                    $("#modalSubHeading").text("Update Widget")
                    break;
            }
            break;
        case "Tasks":
            $("#addeditModallabel").text("Manage Tasks");
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Task")
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    break;
                default:
                    $("#modalSubHeading").text("Update Tasks")
                    if (details) {
                        $("#modalSubHeading").text("Task Details")
                    }
                    break;
            }
            break;
        case "ClientContacts":
            $("#addeditModallabel").text("Manage Client Contacts")
            $(".modal-dialog").addClass("modal-lg")
            switch (itemId) {
                case 0:
                    $("#modalSubHeading").text("New Contact")
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    break;
                default:
                    $("#modalSubHeading").text("Update Contact")
                    break;
            }
            break;
        case "Users":
            $("#addeditModallabel").text("Manage Users")
            $("#modalSubHeading").text("Edit User")
            break;
        case "EmployeesRoleNames":
            $("#addeditModallabel").text("Manage Employee Roles")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Role")
                    break;
                default:
                    $("#modalSubHeading").text("Update Role")
                    break;
            }
            break;
        case "EmployeesFacilities":
            $("#addeditModallabel").text("Manage Employee Facilities")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Facility")
                    break;
                default:
                    $("#modalSubHeading").text("Update Facility")
                    break;
            }
            break;
        case "EmployeesOperatingCounties":
            $("#addeditModallabel").text("Manage Employee Operating Counties")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New County")
                    break;
                default:
                    $("#modalSubHeading").text("Update County")
                    break;
            }
            break;
        case "FacilitiesOperatingCounties":
            $("#addeditModallabel").text("Manage Operating Counties")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New County")
                    break;
                default:
                    $("#modalSubHeading").text("Update County")
                    break;
            }
            break;
        case "ClientInsurances":
            $("#addeditModallabel").text("Manage Client Insurances")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Insurance")
                    break;
                default:
                    $("#modalSubHeading").text("Update Insurance")
                    break;
            }

            break;
        case "MaladaptiveMeasurements":
            $("#addeditModallabel").text("Manage Maladaptives Measurements")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass + "&BaNotesId=" + optionalParameter;
                    }
                    $("#modalSubHeading").text("New Measurements")
                    break;
                default:
                    if (!details) {
                        url = "/" + ItemClass + "/Edit?id=" + itemId + "&BaNotesId=" + optionalClass;
                    }
                    $("#modalSubHeading").text("Update Measurements")
                    break;
            }

            break;
        case "Caregivers":
            $("#addeditModallabel").text("Manage Client Caregivers")
            $(".modal-dialog").addClass("modal-lg")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Caregiver")
                    break;
                default:
                    $("#modalSubHeading").text("Update Caregiver")
                    break;
            }

            break;
        case "MaladaptiveDischarges":
            $("#addeditModallabel").text("Manage Maladaptives Discharge")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Discharge")
                    break;
                default:
                    $("#modalSubHeading").text("Update Discharge")
                    break;
            }

            break;
        case "Replacements":
            $("#addeditModallabel").text("Manage Replacements")
            $(".modal-dialog").addClass("modal-lg")
             switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Replacement")
                    break;
                default:
                    $("#modalSubHeading").text("Update Replacement")
                    break;
            }

            break;
        case "ReplacementMeasurements":
            $("#addeditModallabel").text("Manage Replacement Measurements")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass + "&BaNotesId=" + optionalParameter;
                    }
                    $("#modalSubHeading").text("New Measurements")
                    break;
                default:
                    if (!details) {
                        url = "/" + ItemClass + "/Edit?id=" + itemId + "&BaNotesId=" + optionalClass;
                    }
                    $("#modalSubHeading").text("Update Measurements")
                    break;
            }

            break;
        case "DocumentationProcesses":
            $("#addeditModallabel").text("Manage Employee Documentation Process")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Document")
                    break;
                default:
                    $("#modalSubHeading").text("Update Document")
                    break;
            }

            break;
        case "BaCrisisPlans":
            $("#addeditModallabel").text("Manage Crisis Plan")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Crisis Plan")
                    break;
                default:
                    $("#modalSubHeading").text("Update Crisis Plan")
                    break;
            }

            break;
        case "ClientsFacilities":
            $("#addeditModallabel").text("Manage Client Facilities")
            switch (itemId) {
                case 0:
                    if (!details) {
                        url = "/" + ItemClass + "/Create?id=" + optionalClass;
                    }
                    $("#modalSubHeading").text("New Facility")
                    break;
                default:
                    $("#modalSubHeading").text("Update Facility")
                    break;
            }
            break;
        case "FileDropbox":
            $("#addeditModallabel").text("Manage Documents")
            $("#modalSubHeading").text("New Document")
            if (!details) {
                url = "/Documents/" + ItemClass + "?id=" + optionalClass;
            }
            break;
        case "LongTermObjectives":
            $("#addeditModallabel").text("Manage Long Term Objectives");
            switch (itemId) {
                case 0:
                    url = "/" + ItemClass + "/Create?id=" + optionalClass + "&ObjType=" + optionalParameter;
                    $("#modalSubHeading").text("New Objective")
                    break;
                default:
                    $("#modalSubHeading").text("Update Objective")
                    break;
            }
            break;
        case "ShortTermObjectives":
            $("#addeditModallabel").text("Manage Short Term Objectives");
            $(".modal-dialog").addClass("modal-lg")
            switch (itemId) {
                case 0:
                    url = "/" + ItemClass + "/Create?id=" + optionalClass + "&ObjType=" + optionalParameter;
                    $("#modalSubHeading").text("New Objective")
                    break;
                default:
                    $("#modalSubHeading").text("Update Objective")
                    break;
            }
            break;
        case "Authorizations":
            $("#addeditModallabel").text("Manage Authorizations");
            $(".modal-dialog").addClass("modal-lg")
            switch (itemId) {
                case 0:
                    url = "/" + ItemClass + "/Create?id=" + optionalClass + "&BaAssessmentId=" + optionalParameter;
                    $("#modalSubHeading").text("New Authorization")
                    break;
                default:
                    $("#modalSubHeading").text("Update Authorization")
                    break;
            }
            break;
        case "CaregiverTrainingGoals":
            $("#addeditModallabel").text("Manage Caregiver Training Goals")
            switch (itemId) {
                case 0:
                    url = "/" + ItemClass + "/Create?id=" + optionalClass + "&name=" + optionalParameter;
                    $("#modalSubHeading").text("New Training Goal")
                    break;
                default:
                    $("#modalSubHeading").text("Update Training Goal")
                    break;
            }
            break;
        case "Medications":
            $("#addeditModallabel").text("Manage Medications")
            switch (itemId) {
                case 0:
                    url = "/" + ItemClass + "/Create?id=" + optionalClass ;
                    $("#modalSubHeading").text("New Medication")
                    break;
                default:
                    $("#modalSubHeading").text("Update Medication")
                    break;
            }
            break;
        case "Preferences":
            $("#addeditModallabel").text("Manage Preferences")
            switch (itemId) {
                case 0:
                    url = "/" + ItemClass + "/Create?id=" + optionalClass ;
                    $("#modalSubHeading").text("New Preference")
                    break;
                default:
                    $("#modalSubHeading").text("Update Preference")
                    break;
            }
            break;
        case "BcabaSupvMeetings":
            $("#addeditModallabel").text("Manage Bcaba Supvervision Meetings")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Update Bcaba Supvervision Meeting")
                    break;
            }
            break;
    }
    if (details) {
        url = "/" + ItemClass + "/Details/" + itemId;
    }
    //Added backdrop attribute so clicking background does not close modal and resets data
    showInPopup(url);
};
function showInPopup(url) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {

            $('#addeditModal .modal-body').html(res);
            $('#addeditModal').modal('show');
            $('.ModalSelect').selectpicker();
        }
    })
}

jQueryAjaxPost = form => {
    $('#formSpinner').removeClass('d-none');
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid || res.isValid === undefined) {
                    //window.location.reload();
                    $('#addeditModal').modal('hide');
                    location.reload()
                } else {
                    if (res.html == '') {
                        res.html = 'We apologize this form is not working at the moment. Try again!';
                    }
                    $('#addeditModal .modal-body').html(res.html);
                    $('.ModalSelect').selectpicker();
                }
            },
            error: function (err) {
                console.log(err)
            }
            
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

function DeleteElement(itemId, ItemClass, returnUrl) {
    var url = "/" + ItemClass + "/Delete?id=" + itemId;
    switch (ItemClass) {
        case "Appointments":
            $("#addeditModallabel").text("Manage Appointments")
            $(".modal-dialog").removeClass("modal-lg")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Appointment")
                    break;
            }
            break;
        case "Diagnosis":
            $("#addeditModallabel").text("Manage Diagnosis")
            $(".modal-dialog").removeClass("modal-lg")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Diagnosis")
                    break;
            }
            break;
        case "AbcReports":
            $("#addeditModallabel").text("Manage Abc Reports")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Abc Report")
                    break;
            }
        case "DashboardWidgets":
            $("#addeditModallabel").text("Manage Dashboard Widgets")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Widget")
                    break;
            }
            break;
        case "SupportTickets":
            $("#addeditModallabel").text("Manage Support Tickets")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Ticket")
                    break;
            }
            break;
        case "MaladaptiveDischarges":
            $("#addeditModallabel").text("Manage Maladaptives Discharge")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Discharge")
                    break;
            }
            break;
        case "Maladaptives":
            $("#addeditModallabel").text("Manage Maladaptives")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Maladaptive")
                    break;
            }
            break;
        case "ClientEventTypesCatalog":
            $("#addeditModallabel").text("Manage Event Type")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Event Type")
                    break;
            }
            break;
        case "ShortTermObjectives":
            $("#addeditModallabel").text("Manage Short Term Objectives")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Objective")
                    break;
            }
            break;
        case "LongTermObjectives":
            $("#addeditModallabel").text("Manage Long Term Objectives")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Objective")
                    break;
            }
            break;
        case "Employees":
            $("#addeditModallabel").text("Manage Employee")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Employee")
                    break;
            }
            break;
        case "EmployeesRoleNames":
            $("#addeditModallabel").text("Manage Employee Role")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Role")
                    break;
            }
            break;
        case "EmployeesFacilities":
            $("#addeditModallabel").text("Manage Employee Facilities")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Employee Facility")
                    break;
            }
            break;
        case "ClientInsurances":
            $("#addeditModallabel").text("Manage Client Insurances")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Client Insurance")
                    break;
            }
            break;
        case "Facilities":
            $("#addeditModallabel").text("Manage Facilities")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Facility")
                    break;
            }
            break;
        case "ClientsFacilities":
            $("#addeditModallabel").text("Manage Client Facilities")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Client Facility")
                    break;
            }
            break;
        case "EmployeesOperatingCounties":
            $("#addeditModallabel").text("Manage Employee Operating Counties")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Operating County")
                    break;
            }
            break;
        case "RoleNames":
            $("#addeditModallabel").text("Manage Employee Roles")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Employee Role")
                    break;
            }
            break;
        case "UserRoles":
            $("#addeditModallabel").text("Manage User Roles")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove User Role")
                    break;
            }
            break;
        case "Users":
            $("#addeditModallabel").text("Manage Application Users")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove User")
                    break;
            }
            break;
        case "Clients":
            $("#addeditModallabel").text("Manage Clients")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Client")
                    break;
            }
            break;
        case "IntakeDocsCatalog":
            $("#addeditModallabel").text("Manage Intake Doc")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Intake Doc")
                    break;
            }
            break;
        case "ServiceCodes":
            $("#addeditModallabel").text("Manage Service Code")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Service Code")
                    break;
            }
            break;
        case "OperatingCounties":
            $("#addeditModallabel").text("Manage Operating County")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Operating County")
                    break;
            }
            break;
        case "ClientInsurancesCatalog":
            $("#addeditModallabel").text("Manage Insurance Plan")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Insurance Plan")
                    break;
            }
            break;
        case "MaladaptivesCatalog":
            $("#addeditModallabel").text("Manage Maladaptive")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Maladaptive")
                    break;
            }
            break;
        case "Replacements":
            $("#addeditModallabel").text("Manage Replacements")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Replacement")
                    break;
            }
            break;
        case "ReplacementsCatalog":
            $("#addeditModallabel").text("Manage Replacement")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Replacement")
                    break;
            }
            break;
        case "ReinforcerCatalog":
            $("#addeditModallabel").text("Manage Reinforcer")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Reinforcer")
                    break;
            }
            break;
        case "Functions":
            $("#addeditModallabel").text("Manage Function")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Function")
                    break;
            }
            break;
        case "Interventions":
            $("#addeditModallabel").text("Manage Intervention")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Intervention")
                    break;
            }
            break;
        case "PreferencesCatalog":
            $("#addeditModallabel").text("Manage Preference")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Preference")
                    break;
            }
            break;
        case "EnvironmentalsCatalog":
            $("#addeditModallabel").text("Manage Environmental")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Environmental")
                    break;
            }
            break;
        case "Documents":
            $("#addeditModallabel").text("Manage Documents")
            switch (itemId) {
                default:
                    url = "/" + ItemClass + "/Delete?id=" + itemId + "&returnUrl=" + returnUrl;
                    $("#modalSubHeading").text("Remove Document")
                    break;
            }
            break;
        case "CaregiverCompChecksCatalog":
            $("#addeditModallabel").text("Manage Training Item")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Training Item")
                    break;
            }
            break;
        case "RbtCompTrainingsCatalog":
            $("#addeditModallabel").text("Manage Training Item")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Training Item")
                    break;
            }
            break;
        case "RoleDocsCatalog":
            $("#addeditModallabel").text("Manage HR Requirement")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove HR Requirement")
                    break;
            }
            break;
        case "Supervisions":
            $("#addeditModallabel").text("Manage Supervisions")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Supervisions")
                    break;
            }
            break;
        case "Comments":
            $("#addeditModallabel").text("Manage Comments")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Comment")
                    break;
            }
            break;
        case "Locations":
            $("#addeditModallabel").text("Manage Addresses")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Address")
                    break;
            }
            break;
        case "Caregivers":
            $("#addeditModallabel").text("Manage Client Caregivers")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Caregiver")
                    break;
            }
            break;
        case "CaregiverFeedback":
            $("#addeditModallabel").text("Manage Caregiver Feedback")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Caregiver Feedback")
                    break;
            }
            break;
        case "CaregiverTrainingGoals":
            $("#addeditModallabel").text("Manage Caregiver Training Goals")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Training Goal")
                    break;
            }
            break;
        case "ClientContacts":
            $("#addeditModallabel").text("Manage Client Contacts")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Contact")
                    break;
            }
            break;
        case "EnvironmentalChanges":
            $("#addeditModallabel").text("Manage Environmental Changes")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Environmental Change")
                    break;
            }
            break;
        case "BaCrisisPlans":
            $("#addeditModallabel").text("Manage Crisis Plan")
            switch (itemId) {
                default:
                    $("#modalSubHeading").text("Remove Crisis Plan")
                    break;
            }
            break;
    }

    //Added backdrop attribute so clicking background does not close modal and resets data
    $("#addeditBody").load(url, function () {
        $("#addeditModal").modal({ backdrop: 'static' }, "show");
    });
};

// Row double click action
function LoadEdit(id, itemclass) {
    window.location = "/" + itemclass + "/Edit/" + id;
}

// Constant Tasks
const task = { id: 0 };
$('#addeditModal').on('hidden.bs.modal', function () {
    $(".modal-dialog").removeClass("modal-lg")
    $(".modal-dialog").removeClass("modal-xl")
})