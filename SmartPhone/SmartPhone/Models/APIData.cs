using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPhone.Models
{
    public class APIData<T> where T:new()
    {
        public APIData()
        {
            Data = new T();
        }
        public T Data { get; set; }
        public APIHeader Header { get; set; }
    }
}
