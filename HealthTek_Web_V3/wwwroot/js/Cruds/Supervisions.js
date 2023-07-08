function submitFilterForm() {
    var datevalue = $("#superdate").val();
    var emp = $("#employee").val();
    var clientvalue = $("#client").val();

    var data = { superdate: datevalue, employee: emp, client: clientvalue };

    sendData("/Supervisions/GetSupervision", data, sethourValues);

}
// Sets Hour Values
function sethourValues(Data) {
    $("#totalHours").text(Data.resultValue.toFixed(2) + " hrs");
    var percentageOfHours = .05 * Data.resultValue;
    $("#percentageHours").text(percentageOfHours.toFixed(2) + " hrs");
    var groupHours = .50 * percentageOfHours;
    $("#groupHours").text(groupHours.toFixed(2) + " hrs");
    if (Data.searchFilter != null && Data.searchFilter != 'undefined' && Data.searchFilter.length != 0) {
        var datevalue = $("#superdate").val();
        var emp = $("#employee").val();
        var clientvalue = $("#client").val();
        if (datevalue != null && emp == null && clientvalue == null) {
            $('#dataTable').DataTable().column(0).search(Data.searchFilter).draw();
        } 
        if (emp != null && clientvalue == null) {
            $('#dataTable').DataTable().column(4).search(Data.searchFilter).draw();
        } 
        if (clientvalue != null) {
            $('#dataTable').DataTable().column(3).search(Data.searchFilter).draw();
        } 
        
    } else {
        $('#dataTable').DataTable().search(' ').draw();
    }
}

function UpdateSignature(sendid, action) {
    var data = { id: sendid, actionESign: action };
    sendData("/Supervisions/UpdateSupervisionESig", data, refresh);
}
function refresh() {
    location.reload();
}

function getSupervisorNumber() {
    var name = document.getElementById("SupervisorName");
    var args = name.options[name.selectedIndex].value;
    var data = { id: args };
    sendData("/Supervisions/GetSupervisorNumber", data, setSupervisorNumber);
}
function setSupervisorNumber(data) {
    var number = document.getElementById("SupervisorNumber");
    number.value = data.html;
}