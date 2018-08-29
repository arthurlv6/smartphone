using System;
using System.Collections.Generic;

namespace SmartPhone.Models.APIModels
{
    public partial class SubscriberEmail
    {
        public long Id { get; set; }
        public long SubscriberId { get; set; }
        public string EmailPhone { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? VerifiedUtc { get; set; }
        public string Token { get; set; }

        public Subscriber Subscriber { get; set; }
    }
}
