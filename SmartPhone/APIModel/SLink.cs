using System;
using System.Collections.Generic;

namespace APIModel
{
    public partial class SLink
    {
        public SLink()
        {
            SUserLink = new HashSet<SUserLink>();
        }

        public int Id { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Control { get; set; }
        public string Action { get; set; }

        public ICollection<SUserLink> SUserLink { get; set; }
    }
}
