var connection = new signalR.HubConnectionBuilder()
    .withUrl('/employeeBalanceHub')
    .build();

connection.on('RefreshEmployeeData', function () {
    alert("Hub Fired");
});
connection.start();
$("#InvoiceCount").on("click", function () {
    connection.invoke("SendMessage");
})
function x () {
    connection.invoke("RefreshEmployeeData");
}
$(document).ready(function () { connection.invoke("RefreshEmployeeData"); });
