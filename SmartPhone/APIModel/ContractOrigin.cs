using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class ContractOrigin
    {
        public int Id { get; set; }
        public long ContractId { get; set; }
        public string Path { get; set; }
    }
}
