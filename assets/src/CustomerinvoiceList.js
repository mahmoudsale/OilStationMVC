$(document).ready(function () {
    LoadData();
});
function LoadData() {
    $("#example2").DataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "ajax": {
            "url": "/InnvoiceCustomer/LoadDataAsync",
            "type": "POST",
            "datatype": "json",
        },
        "columnDefs":
            [
                {
            "targets": [0],
            "visible": true,
            "searchable": false,
                },
                {
                    "targets": [1], render: function (data) { return moment(data).format('YYYY-MM-DD'); },
                },
            ],
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "dDate", "name": "dDate", "autoWidth": true },
            { "data": "Customer.Name", "name": "Customer", "autoWidth": true },
            { "data": "TotalPrice", "name": "TotalPrice", "autoWidth": true },
            {
                "render": function (data, type, full, meta) { return '<a class="btn btn-info" href="/InnvoiceCustomer/Edit/' + full.Id + '">Edit</a>'; }
            },
            {
                "render": function (data, type, full, meta) { return '<a class="btn btn-info" href="/InnvoiceCustomer/Details/' + full.Id + '">Details</a>'; }
            },
            //{
            //    data: null,
            //    render: function (data, type, row) {
            //        return "<a href='#' class='btn btn-danger' onclick=DeleteData('" + row.id + "'); >Delete</a>";
            //    }
            //},
            {
                "render": function (data, type, full, meta) {
                    var element = "<span id='SpanDelete_" + full.Id + "'><a href='#' class='btn btn-danger' onclick='ConfirmDelete(" + full.Id + "," + true + ")'>Delete</a></span>";
                    element += "<span id='SpanConfirmDelete_" + full.Id + "'  style = 'display: none' >";
                    element += "<span><strong>Are You Sure To Delete</strong></span>";
                    element += "<a href='/InnvoiceCustomer/Delete/" + full.Id + "' class='btn btn-primary' onclick='ConfirmDelete(" + full.Id + ", " + true + ")'>yes</a>";
                    element += " <a href='#' class='btn btn-primary' onclick='ConfirmDelete(" + full.Id + ", " + false + ")'>No</a>";
                    element += "</span>";
                    return element;
                }

            }
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
    var url = '@Url.Content("~/")' + "CustomerInvoice/Delete";

    $.post(url, { ID: CustomerID }, function (data) {
        if (data) {
            oTable = $('#example').DataTable();
            oTable.draw();
        } else {
            alert("Something Went Wrong!");
        }
    });
}