using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class Channel
    {
        public Channel()
        {
            Contract = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SettingId { get; set; }

        public ICollection<Contract> Contract { get; set; }
    }
}
