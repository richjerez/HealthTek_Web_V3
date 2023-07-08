
// Copy text to clipboard
function copyToClipboard(whatToCopy, optional) {
    var code = 'codes';
    if (optional) {
        code = whatToCopy;
    }
    if (document.selection) {
        var range = document.body.createTextRange();
        range.moveToElementText(document.getElementById(code));
        range.select().createTextRange();
        document.execCommand("copy");
    } else if (window.getSelection) {
        var range = document.createRange();
        range.selectNode(document.getElementById(code));
        var select = window.getSelection();
        select.removeAllRanges();          // Remove all ranges from the selection.
        select.addRange(range);
        document.execCommand("copy");
        var alertDiv = "blah";
        switch (whatToCopy) {
            case "RecoveryCodes":
                alertDiv = '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>Recovery codes have been copied!';
                break;
            case "WalletAddress":
                $('#alertcodes').removeClass('d-none');
                alertDiv = '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>Wallet address has been copied!';
                break;
        }
        $('#alertcodes').html(alertDiv);
    }
}

// Toggle codes and change eye icon
function togglePassword(wallet, id) {
    const togglePassword = document.getElementById(wallet);
    const password = document.getElementById(id);
    // toggle the type attribute
    const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
    password.setAttribute('type', type);
    // toggle the eye / eye slash icon
    togglePassword.classList.toggle('fa-eye');
    togglePassword.classList.toggle('fa-eye-slash');
}

function animateValue(id, start, end, duration) {
    if (start === end) return;
    var range = end - start;
    var current = start;
    var increment = end > start ? 1 : -1;
    var stepTime = Math.abs(Math.floor(duration / range));
    var obj = document.getElementById(id);
    var timer = setInterval(function () {
        current += increment;
        obj.innerHTML = current;
        if (current == end) {
            clearInterval(timer);
        }
    }, stepTime);
}

// Loads The script after everything is loaded
function loadScript(src) {
    return new Promise(function (resolve, reject) {
        if ($("script[src='" + src + "']").length === 0) {
            var script = document.createElement('script');
            script.onload = function () {
                resolve();
            };
            script.onerror = function () {
                reject();
            };
            script.src = src;
            document.body.appendChild(script);
        } else {
            resolve();
        }
    });
}

// Sends data to controlles
function sendData(url, senddata, task) {
    $.ajax({
        type: 'Get',
        url: url,
        dataType: 'json',
        data: senddata,
        success: function (Data) {
            //Perform After Task
            task(Data);
        },
        error: function (xhr, status, error) {
            alert("Failed");
        }
    });

}

// Calculates units for authorizations
function calcUnits() {
    var amt = document.getElementById("UnitAmount");
    var weeklyhrs = document.getElementById("WeeklyHours");
    var weeklyunts = document.getElementById("WeeklyUnits");
    var num = amt.value / weeklyhrs.value;
    weeklyunts.value = Math.round(num * 100) / 100;
}

// Change Short Term Objective Status
function changeSTOStatus(id,status) {
    var url = '/ShortTermObjectives/ChangeStatus?id=' + id + '&status=' + status;
    var senddata = { id: id, status: status };
    $.ajax({
        type: 'Get',
        url: url,
        dataType: 'json',
        data: senddata,
        success: function (Data) {
            var ele = document.getElementById("dropdownMenu-" + id);
            ele.innerHTML = status + ' <i class="fas fa-edit"></i>';
        },
        error: function (xhr, status, error) {
            alert("Failed");
        }
    });
}

function performClick(id) {
    $("#" + id).click();
}

// Submit Forms
function SubmitForm(id) {
    $("#" + id).submit();
}

// Geo Coding key (converts addresses for gps locations)
function getGeoCodingKey() {
    return "AIzaSyAvOgQNUu3_KVOwTnGq9MLm93jXcHAq-V0";
}