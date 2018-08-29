using System;
using System.Collections.Generic;

namespace SmartPhone.Models.APIModels
{
    public partial class SubscriberNotification
    {
        public long Id { get; set; }
        public long SubscriberId { get; set; }
        public int NotificationId { get; set; }

        public Notification Notification { get; set; }
        public Subscriber Subscriber { get; set; }
    }
}
