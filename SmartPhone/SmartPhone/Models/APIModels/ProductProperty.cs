﻿
using System;
using System.Collections.Generic;

namespace SmartPhone.Models.APIModels
{
    public partial class ProductProperty 
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }

        public Product Product { get; set; }
    }
}
