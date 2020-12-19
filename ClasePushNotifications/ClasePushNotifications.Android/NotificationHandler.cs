using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasePushNotifications.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    class NotificationHandler: FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage p0)
        {
            base.OnMessageReceived(p0);
            new NotificationHelper().CreateNotification(p0.GetNotification().Title, p0.GetNotification().Body);
        }
    } 
}