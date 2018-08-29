using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class SUserLog
    {
        public int Id { get; set; }
        public int? SuserId { get; set; }
        public string Section { get; set; }
        public string Operation { get; set; }
        public DateTime? Date { get; set; }

        public SUser Suser { get; set; }
    }
}
