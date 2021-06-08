let connection = new signalR.HubConnectionBuilder()
    .withUrl('/stockHub')
    .build();
connection.on('NewStock', function (serviceurl) { 
    LoadData(serviceurl); 
});

connection.start().catch(function (err) {
    return console.error(err.toString());
}).then(function () {
    connection.invoke("NewStock","/Invoice/Stocktest");
});


$("xx").click(function () {
    connection.invoke("NewStock","/Invoice/Stocktest");
});
 
function LoadData(serviceurl) { 

    var Parameters = {
        StockId: 1
    }
    var stock = Manager.GetAllData(serviceurl, Parameters);

    for (var key in stock) {
        var Item = "<div class='dropdown-divider'></div>";
        Item += "<a href = '#' class='dropdown-item'>";
        Item += "<i class='fas fa-file mr-2'></i> " + stock[key].name + "";
        Item += "<span class='float-right text-muted text-sm'> 100 days</span>";
        Item += "</a >";
        $(Item).insertBefore(".NewNotificationAddedAfterThisDiv");
        }
  
}


