
var x = document.getElementById("dataTable_wrapper").querySelectorAll(".row");
for (i = 0; i < x.length; i++) {
    if (i == 0) {
        x[0].classList.add("d-print-none");
        x[2].classList.add("d-print-none");
    }
}
var search = document.getElementById("dataTable_filter");
search.setAttribute('class', 'float-right');
search.firstChild.setAttribute('style', 'width:100%');
$("ul").addClass("float-right");

var newItem = document.getElementById('activity');
if (newItem != null) {
    search.appendChild(newItem);
}
