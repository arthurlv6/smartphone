using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class ErrorLog
    {
        public int Id { get; set; }
        public DateTime? EventUtc { get; set; }
        public string DeviceId { get; set; }
        public string Message { get; set; }
        public string MessageDump { get; set; }
        public string MethodName { get; set; }
    }
}
