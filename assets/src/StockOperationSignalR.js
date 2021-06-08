$(function () {
    RefreshNotification();
});
function playAudio() {
    var x = document.getElementById("notificationAudio");
    x.play();
}
// This optional function html-encodes messages for display in the page.
function RefreshNotification() {
    // Reference the auto-generated proxy for the hub.
    var chat = $.connection.chatHub;
    // Create a function that the hub can call back to display messages.
    chat.client.NewItemAdded = function () {
        var Object = Manager.GetAllData("/Notification/LoadNotifications", null);
        if (Object) {
            $('#NotificationMenu').html(''); 
            $('#NotificationCount').empty();
            var Span = "<span class='dropdown-item dropdown-header' id='NotificationCountWithText'>" + Object.length + " Notifications</span>";
            Span += "<div class='dropdown-divider NewNotificationAddedAfterThisDiv'></div>";
            $('#NotificationMenu').append(Span);
            if (Object.length > 0) {
                $.each(Object, function (i, item) {
                    var str = "<div class='dropdown-divider'></div><a href = '/Notification/Index' class='dropdown-item' onclick=MarkAsRead(" + item.Id + ")><i class='fas fa-file mr-2'></i> " + item.Title + " <span class='float-right text-muted text-sm' > " + item.SinceFor + "</span ></a>";
                    $('#NotificationMenu').append(str);
                    if (i == 10) {
                        return false;
                    }
                });
                var NewNotificationCount = 0;
                $.each(Object, function (i, item) {

                    if (item.IsNew === true) {
                        NewNotificationCount =NewNotificationCount+1;
                    }
                });
                if (NewNotificationCount > 0) {
                    $('#NotificationCount').text(NewNotificationCount);
                    playAudio();
                }
                else {
                    $('#NotificationCount').text(NewNotificationCount);
                }
            
            }

            $('#NotificationMenu').append("<div class='dropdown-divider'></div><a href='/Notification/Index' class='dropdown-item dropdown-footer'>See All Notifications</a>");

        }
    };
    chat.client.broadcastNotification = function () {
        //playAudio();
    };
    // Start the connection.
    $.connection.hub.start().done(function () {
        chat.server.newItem();
        chat.server.broadcastNotification();
    });
}
var interval = setInterval(function () { RefreshNotification(); }, 100000);
function MarkAsRead(Id) {
    $.ajax({
        type: "POST",
        url: "/Notification/MarkAsReadAsync",
        data: {
            Id: Id
        },
        dataType: 'json',
        success: function (data) {
            alert(data.Message);
        }
    });
}