using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPhone.Models
{
    public class APIHeader
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
