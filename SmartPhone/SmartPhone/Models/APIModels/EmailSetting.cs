using System;
using System.Collections.Generic;

namespace SmartPhone.Models.APIModels
{
    public partial class EmailSetting
    {
        public int Id { get; set; }
        public int SettingId { get; set; }
        public string Type { get; set; }
        public string Sender { get; set; }
        public string Address { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
