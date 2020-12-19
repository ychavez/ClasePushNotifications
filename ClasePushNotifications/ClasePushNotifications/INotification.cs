using System;
using System.Collections.Generic;
using System.Text;

namespace ClasePushNotifications
{
    public interface INotification
    {
        void CreateNotification(string title, string message);
    }
}
