using SmartPhone.Models;
using SmartPhone.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartPhone.ViewModels
{
    public class SaleDetailViewModel:BaseViewModel
    {
        public Contract Sale { get; set; }
        public SaleDetailViewModel(Contract sale =null)
        {
            Title = sale?.CustomerName;
            Sale = sale;
        }
    }
}
