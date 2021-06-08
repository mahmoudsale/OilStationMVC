using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace OilStationMVC.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }
        public void NewStock(string StockName)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.NewStockCreated(StockName);
        }

        public void NewItem()
        { 
            Clients.All.NewItemAdded(); 
        }
         public void broadcastNotification()
        { 
            Clients.All.broadcastNotification(); 
        }

    }
}