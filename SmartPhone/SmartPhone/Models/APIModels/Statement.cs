using System;
using System.Collections.Generic;

namespace SmartPhone.Models.APIModels
{
    public partial class Statement
    {
        public Statement()
        {
            StatementDetail = new HashSet<StatementDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Money { get; set; }
        public DateTime? OperationDate { get; set; }

        public ICollection<StatementDetail> StatementDetail { get; set; }
    }
}
