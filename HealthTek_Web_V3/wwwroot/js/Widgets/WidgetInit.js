// Main Section - Chart Initialization
function selectChart(classname,id) {
    switch (classname) {
        case "AllClientsLastSixMonths":
            InitializeGetAllClientsLastSixMonths(id);
                    break;
        case "EmployeesRoles":
            InitializeGetEmployeesRoles(id);
            break;
        case "MonthlyTotalsByService":
            InitializeMonthlyTotalsByService(id);
            break;
        case "PreviousMonthlyTotalsByService":
            InitializePreviousMonthlyTotalsByService(id);
            break;
    }

}
function InitializeChart(labels, datasetLabel, data, max, id, decimal) {
    // Set new default font family and font color to mimic Bootstrap's default styling
    Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
    Chart.defaults.global.defaultFontColor = '#858796';

    function number_format(number, decimals, dec_point, thousands_sep) {
        // *     example: number_format(1234.56, 2, ',', ' ');
        // *     return: '1 234,56'
        number = (number + '').replace(',', '').replace(' ', '');
        var n = !isFinite(+number) ? 0 : +number,
            prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
            sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
            dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
            s = '',
            toFixedFix = function (n, prec) {
                var k = Math.pow(10, prec);
                return '' + Math.round(n * k) / k;
            };
        // Fix for IE parseFloat(0.55).toFixed(0) = 0;
        s = (prec ? toFixedFix(n, prec) : '' + Math.round(n)).split('.');
        if (s[0].length > 3) {
            s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
        }
        if ((s[1] || '').length < prec) {
            s[1] = s[1] || '';
            s[1] += new Array(prec - s[1].length + 1).join('0');
        }
        return s.join(dec);
    }
    const maximum = parseInt(max);

    // Bar Chart Example
    var ctx = document.getElementById(id);
    var myBarChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: datasetLabel,
                data: data,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(255, 159, 64, 0.2)',
                    'rgba(255, 205, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(201, 203, 207, 0.2)'
                ],
                borderColor: [
                    'rgb(255, 99, 132)',
                    'rgb(255, 159, 64)',
                    'rgb(255, 205, 86)',
                    'rgb(75, 192, 192)',
                    'rgb(54, 162, 235)',
                    'rgb(153, 102, 255)',
                    'rgb(201, 203, 207)'
                ],
                borderWidth: 1,
                maxBarThickness: 25
            }]
        },
        options: {
            maintainAspectRatio: false,
            layout: {
                padding: {
                    left: 10,
                    right: 25,
                    top: 25,
                    bottom: 0
                }
            },
            scales: {
                xAxes: [{
                    time: {
                        unit: 'month'
                    },
                    gridLines: {
                        display: false,
                        drawBorder: false
                    },
                    ticks: {
                        maxTicksLimit: 5
                    },
                }],
                yAxes: [{
                    ticks: {
                        min: 0,
                        max: maximum + 20,
                        maxTicksLimit: 5,
                        padding: 10,
                        // Include a dollar sign in the ticks
                        callback: function (value, index, values) {
                            if (decimal) {
                                return number_format(value, 2, '.', ' ');
                            } else {
                                return number_format(value);
                            }
                        }
                    },
                    gridLines: {
                        color: "rgb(234, 236, 244)",
                        zeroLineColor: "rgb(234, 236, 244)",
                        drawBorder: false,
                        borderDash: [2],
                        zeroLineBorderDash: [2]
                    }
                }],
            },
            legend: {
                display: false
            },
            tooltips: {
                titleMarginBottom: 10,
                titleFontColor: '#6e707e',
                titleFontSize: 14,
                backgroundColor: "rgb(255,255,255)",
                bodyFontColor: "#858796",
                borderColor: '#dddfeb',
                borderWidth: 1,
                xPadding: 15,
                yPadding: 15,
                displayColors: false,
                caretPadding: 10,
                callbacks: {
                    label: function (tooltipItem, chart) {
                        if (decimal) {
                            var datasetlabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                            return datasetlabel + ': ' + number_format(tooltipItem.ylabel, 2, '.', ' ');
                        } else {
                            var datasetlabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                            return datasetlabel + ': ' + number_format(tooltipItem.ylabel);
                        }
                    }
                }
            },
        }
    });
}

// Initialize a new bar chart from methods
function InitializeGetAllClientsLastSixMonths(id) {
    fetch('/Dashboard/GetAllClientsLastSixMonths', {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    }).then(response => response.json())
        .then(data => {
            const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec"];

            var today = new Date();
            var year = today.getFullYear();
            var currentMonth = today.getMonth()+2;
            var d;
            var month = [];

            for (var i = 6; i > 0; i -= 1) {
                d = new Date(year, currentMonth - i, 0);
                month.push(monthNames[d.getMonth()]);
            }
            var datasetLabel = "Clients";
            InitializeChart(month, datasetLabel, data.body, data.max, id);
        })
        .catch(console.error);
}
function InitializeGetEmployeesRoles(id) {
    fetch('/Dashboard/GetEmployeesRoles', {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    }).then(response => response.json())
        .then(data => {
            var roles = data.roles;
            var datasetLabel = "Employees";
            InitializeChart(roles, datasetLabel, data.body, data.max, id);
        })
        .catch(console.error);
}
function InitializeMonthlyTotalsByService(id) {
    fetch('/Dashboard/MonthlyTotalsByService?lastmonth=false', {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    }).then(response => response.json())
        .then(data => {
            var roles = data.labels;
            var datasetLabel = "Service";
            InitializeChart(roles, datasetLabel, data.body, data.max, id, true);
        })
        .catch(console.error);
}
function InitializePreviousMonthlyTotalsByService(id) {
    fetch('/Dashboard/MonthlyTotalsByService?lastmonth=true', {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    }).then(response => response.json())
        .then(data => {
            var roles = data.labels;
            var datasetLabel = "Service";
            InitializeChart(roles, datasetLabel, data.body, data.max, id, true);
        })
        .catch(console.error);
}

// Card Init
function selectCard(ClassName,id) {
    fetch('/Dashboard/'+ ClassName, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    }).then(response => response.json())
        .then(data => {
            if (data.max != undefined) {
                document.getElementById(id).innerHTML = data.body + "/" + data.max;
            } else {
                document.getElementById(id).innerHTML = data.body;
            }
        })
        .catch(console.error);

}