﻿using System;
using System.Collections.Generic;

namespace SmartPhone.Models.APIModels
{
    public partial class CommonOrganisationType
    {
        public CommonOrganisationType()
        {
            Setting = new HashSet<Setting>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Setting> Setting { get; set; }
    }
}
