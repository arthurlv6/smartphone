using System;
using System.Collections.Generic;

namespace SmartPhone.Models.APIModels
{
    public partial class Subscriber
    {
        public Subscriber()
        {
            SubscriberEmail = new HashSet<SubscriberEmail>();
            SubscriberNotification = new HashSet<SubscriberNotification>();
        }

        public long Id { get; set; }
        public string DeviceOs { get; set; }
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public DateTime? VerifiedUtc { get; set; }

        public ICollection<SubscriberEmail> SubscriberEmail { get; set; }
        public ICollection<SubscriberNotification> SubscriberNotification { get; set; }
    }
}
