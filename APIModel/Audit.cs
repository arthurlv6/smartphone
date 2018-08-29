using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class Audit
    {
        public Audit()
        {
            AuditField = new HashSet<AuditField>();
        }

        public int Id { get; set; }
        public string Entity { get; set; }
        public int EntityId { get; set; }
        public DateTime EventDtUtc { get; set; }
        public string Username { get; set; }

        public ICollection<AuditField> AuditField { get; set; }
    }
}
