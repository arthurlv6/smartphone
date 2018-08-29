﻿using System;
using System.Collections.Generic;

namespace SmartPhone.Models.APIModels
{
    public partial class Notification
    {
        public Notification()
        {
            SubscriberNotification = new HashSet<SubscriberNotification>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<SubscriberNotification> SubscriberNotification { get; set; }
    }
}
