using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.API.Infrastructure
{
    public class RequestInput
    {
        public string Sort { get; set; }
        public string Name { get; set; }
        public string Fields { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
