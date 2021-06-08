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
            "url": "/Financial/LoadDataAsync",
            "type": "POST",
            "datatype": "json",
            "data": Parameters
        },
        "columnDefs": [
            {
                "targets": [0],
                "visible": true,
                "searchable": false,
            },
            {
                "targets": [1], render: function (data) { return moment(data).format('YYYY-MM-DD'); },
            },
            {
                "targets": [2],
                "visible": false,

            }, 
        ],
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "dDate", "name": "dDate", "autoWidth": true },
            { "data": "JournalType", "name": "JournalType", "autoWidth": true },
            { "data": "Subledger.Name", "name": "Subledger", "autoWidth": true },
            { "data": "Amount", "name": "Amount", "autoWidth": true },
            { "data": "DepositNo", "name": "DepositNo", "autoWidth": true }, 
            //{ "data": "custodyType.typeName", "name": "CustodyType", "autoWidth": true },
            { "data": "Desc", "name": "Desc", "autoWidth": true },
            {
                "autoWidth": true,
                "render": function (data, type, full, meta) {
                    var EditParameters = {
                        id: full.Id,
                        JournalType: parms.JournalType,

                    }
                    return '<a class="btn btn-info" href="/Financial/Edit/' + EditParameters.id + '?JournalType=' + EditParameters.JournalType + '">Edit</a>';

                }
            },
            {
                "autoWidth": true,
                "render": function (data, type, full, meta) {
                    var EditParameters = {
                        id: full.id,
                        JournalType: parms.JournalType,

                    }
                    return '<a class="btn btn-info" href="/Financial/Details/' + EditParameters.id + '?JournalType=' + EditParameters.JournalType + '">Details</a>';
                }
            },
            {
                "autoWidth": true,
                "render": function (data, type, full, meta) {
                    var EditParameters = {
                        id: full.id,
                        JournalType: parms.JournalType,
                    }
                    var element = "<span id='SpanDelete_" + EditParameters.id + "'><a href='#' class='btn btn-danger' onclick='ConfirmDelete(" + full.id + "," + true + ")'>Delete</a></span>";
                    element += "<span id='SpanConfirmDelete_" + EditParameters.id + "'  style = 'display: none' >";
                    //element += "<span><strong>Are You Sure To Delete</strong></span>";
                    element += "<a href='/Financial/Delete/" + EditParameters.id + "?JournalType=" + EditParameters.JournalType + "' class='btn btn-primary' onclick='ConfirmDelete(" + full.id + ", " + true + ")'>yes</a>";
                    element += " <a href='#' class='btn btn-primary' onclick='ConfirmDelete(" + EditParameters.id + ", " + false + ")'>No</a>";
                    element += "</span>";
                    return element;
                }

            }
        ]

    });
}

