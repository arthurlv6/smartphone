﻿using System;
using System.Collections.Generic;

namespace DataModel
{
    public partial class SUserLink
    {
        public int Id { get; set; }
        public int SUserId { get; set; }
        public int SLinkId { get; set; }

        public SLink SLink { get; set; }
        public SUser SUser { get; set; }
    }
}
