const cardId = { id: 0 };
var fileArray = [];
function dropHandler(ev) {
    fileArray = [];
    var itemLength = ev.dataTransfer.items.length;
    // Prevent default behavior (Prevent file from being opened)
    ev.preventDefault();
    var inp = document.getElementById("documents_customFiles");
    inp.files = ev.dataTransfer.files;
    if (ev.dataTransfer.items) {
        // Use DataTransferItemList interface to access the file(s)
        for (var i = 0; i < itemLength; i++) {
            // If dropped items aren't files, reject them
            if (ev.dataTransfer.items[i].kind === 'file') {
                var file = ev.dataTransfer.items[i].getAsFile();
                createFilePanel(file.name);
                fileArray.push(file.name);
            }
        }
    } else {
        // Use DataTransfer interface to access the file(s)
        for (var i = 0; i < itemLength; i++) {
            var file = ev.dataTransfer.items[i].getAsFile();
            createFilePanel(file.name);
            fileArray.push(file.name);
        }
    }
}
function removeFromArray(item) {
    var oldItem = document.getElementById(item);
    for (var i = 0; i < fileArray.length; i++) {

        if (fileArray[i] === item) {
            fileArray.splice(i, 1);
        }
    }
}

function dragOverHandler(ev) {
    // Prevent default behavior (Prevent file from being opened)
    ev.preventDefault();
}
function getTypeDetails() {
    var clients = document.getElementById("activeclients");
    var emp = document.getElementById("activeemp");
    var documentTitleInput = document.getElementById("documentTitleInput");
    var documentTitle = document.getElementById("documentTitle");
    var clientid = $("#FkClientsId").val();
    var empid = $("#FkEmployeesId").val();
    var typesName = document.getElementById("DocumentType");
    var args = typesName.options[typesName.selectedIndex].text;
    switch (args) {
        case "Client":
            clients.style.display = '';
            documentTitle.style.display = 'none';
            documentTitleInput.style.display = '';
            emp.style.display = 'none';
            break;
        case "HR":
            clients.style.display = 'none';
            documentTitle.style.display = '';
            documentTitleInput.style.display = 'none';
            emp.style.display = '';
            var data = { id: empid, type: 'HR Chart' };
            getchart(data);
            break;
        case "Intake":
            clients.style.display = '';
            documentTitle.style.display = '';
            documentTitleInput.style.display = 'none';
            emp.style.display = 'none';
            var data = { id: clientid, type: 'Intake Chart' };
            getchart(data);
            break;
    }
}
function getchart(dataVal) {
    $.ajax({
        type: 'Get',
        url: "/Documents/GetLists",
        dataType: 'json',
        data: dataVal,
        success: function (intake) {
            $("#DocumentTitleSelect").empty()
            for (var i = 0; i < intake.length; i++) {
                $("#DocumentTitleSelect").append('<option value="' + intake[i].value + '">' + intake[i].text + '</option>');
            }
        },
        error: function (xhr, status, error) {
            var typesName = document.getElementById("DocumentType");
            var args = typesName.options[typesName.selectedIndex].text;
            $("#DocumentTitleSelect").empty();
            switch (args) {
                case "Intake Chart":
                    $("#DocumentTitleSelect").append('<option value="">This Client has no open Intakes</option>');
                    break;
                case "HR Chart":
                    $("#DocumentTitleSelect").append('<option value="">There are no active roles!</option>');
                    break;
            }
        }
    });
}
function getClientChart() {
    var type = $("#DocumentType").val();
    switch (type) {
        case "Clinical Chart":
            break;
        case "Intake Chart":
            var clientid = $("#FkClientsId").val();
            var data = { id: clientid, type: 'Intake Chart' };
            getchart(data);
            break;
    }
}
function getEmpChart() {
    var clientid = $("#FkEmployeesId").val();
    var data = { id: clientid, type: 'HR Chart' };
    getchart(data);
}
function changeTaskStatus(id) {
    fetch('/Documents/Sort?id=' + id, function (data) { });
}
function displayFiles(ev, full) {
    var files = ev.files;
    fileArray = [];
    $("#uploadedfilesPanel").empty();
    for (var a = 0; a < files.length; a++) {
        createFilePanel(files[a].name, full);
        fileArray.push(files[a].name);
    }
}
function createFilePanel(fileName, full) {
    var uploadPanel = document.getElementById('uploadedfilesPanel');
    var cardDiv = document.createElement('div');
    cardDiv.className = 'card border-bottom-success bg-white mx-auto shadow';
    cardDiv.style.width = '40%';
    if (full) {
        cardDiv.style.width = '100%';
    }
    cardDiv.id = 'card' + cardId.id;
    cardId.id++;
    var cardDivbody = document.createElement('div');
    cardDivbody.className = 'card-body p-2';

    var span = document.createElement('span');
    span.innerHTML = fileName;

    cardDivbody.appendChild(span);
    cardDiv.appendChild(cardDivbody);
    uploadPanel.appendChild(cardDiv);
    console.log(fileArray);
}