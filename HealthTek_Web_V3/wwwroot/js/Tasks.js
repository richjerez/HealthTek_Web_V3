
function changeStatus(id, status) {
    var url = '/Tasks/StatusChange?Status=' + status + '&id=' + id;
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            window.location.reload();
        }
    })

}
