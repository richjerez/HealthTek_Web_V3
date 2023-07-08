function ExportCSV(CsvName,col){
    /* Get the HTML data using Element by Id */
    var table = document.getElementById("dataTable");

    var csvContent = [];
    csvContent = "data:text/csv;charset=utf-8,";

      //iterate through rows of table
    for (var i = 0, row; row = table.rows[i]; i++){
        /* Declaring array variable */
        var rows = [];

        //rows would be accessed using the "row" variable assigned in the for loop
        //Get each cell value/column from the row
        for (var x = 0; x < col; x++) {
            /* add a new records in the array */
            rows.push(row.cells[x].innerText);
        }
        /* each row splitted by new line character (\n) */
        csvContent += rows + "\n";
    }
 
    /* create a hidden <a> DOM node and set its download attribute */
    var encodedUri = encodeURI(csvContent);
    var link = document.createElement("a");
    var csvName = CsvName + ".csv";
    link.setAttribute("href", encodedUri);
    link.setAttribute("download", csvName);
    document.body.appendChild(link);
        /* download the data file */
    link.click();
}