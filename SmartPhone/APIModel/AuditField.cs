using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class AuditField
    {
        public int Id { get; set; }
        public int AuditId { get; set; }
        public string Field { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public Audit Audit { get; set; }
    }
}
