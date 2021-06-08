$(document).ready(function () {
    LoadData();
});
function LoadData() {
    var Parameters = parms;
    $("#example2").DataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "info": true,
        "autoWidth": false,
        "responsive": true, 
        "ajax": {
            "url": "/MazotInvoice/LoadDataAsync",
            "type": "POST",
            "datatype": "json",
            "data": Parameters
        },
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false,
                  "orderData": [2, 3]
            },
            {
                "targets": [2], render: function (data) { return moment(data).format('YYYY-MM-DD'); }
            },
            {
                "targets": [1],
                "visible": false,
            },
        ],
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "InnvoiceNo", "name": "InnvoiceNo", "autoWidth": true },
            { "data": "dDate", "name": "dDate", "autoWidth": true },
            { "data": "Company.Name", "name": "Company", "autoWidth": true },
            { "data": "ChargeArea.Name", "name": "ChargeArea", "autoWidth": true },
            { "data": "ChargeCustomer.Name", "name": "ChargeCustomer", "autoWidth": true },
            { "data": "Qty", "name": "Qty", "autoWidth": true },
            { "data": "TotalPrice", "name": "TotalPrice", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    var EditParameters = {

                        id: full.InnvoiceNo,
                        JournalType: parms.JournalType,
                    }
                    return '<a class="btn btn-info" href="/MazotInvoice/Edit/' + EditParameters.id + '?JournalType=' + EditParameters.JournalType + '">Edit</a>';
                }
            },
            {
                "render": function (data, type, full, meta) {
                    var EditParameters = {
                       
                        id: full.InnvoiceNo,
                        JournalType: parms.JournalType,
                    }
                    return '<a class="btn btn-info" href="/MazotInvoice/Details/' + EditParameters.id + '?JournalType=' + EditParameters.JournalType + '">Details</a>';
                }
            },
            {
                "render": function (data, type, full, meta) {
                    var element = "<span id='SpanDelete_" + full.InnvoiceNo + "'><a href='#' class='btn btn-danger' onclick='ConfirmDelete(" + full.InnvoiceNo + "," + true + ")'>Delete</a></span>";
                    element += "<span id='SpanConfirmDelete_" + full.InnvoiceNo + "'  style = 'display: none' >";
                    element += "<a href='/MazotInvoice/Delete/" + full.InnvoiceNo + "' class='btn btn-primary' onclick='ConfirmDelete(" + full.InnvoiceNo + ", " + true + ")'>yes</a>";
                    element += " <a href='#' class='btn btn-primary' onclick='ConfirmDelete(" + full.InnvoiceNo + ", " + false + ")'>No</a>";
                    element += "</span>";
                    return element;
                }

            },
        ]

    });
}
function DeleteData(CustomerID) {
    if (confirm("Are you sure you want to delete ...?")) {
        Delete(CustomerID);
    } else {
        return false;
    }
}
function Delete(CustomerID) {
    var url = '@Url.Content("~/")' + "MazotInvoice/Delete";

    $.post(url, { id: CustomerID }, function (data) {
        if (data) {
            oTable = $('#example').DataTable();
            oTable.draw();
        } else {
            alert("Something Went Wrong!");
        }
    });
}

