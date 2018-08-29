using SmartPhone.Models.APIModels;
using SmartPhone.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPhone.Views.Modal
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectCustomerPage : ContentPage
	{
		public SelectCustomerPage ()
		{
            GetCustomers();            
			InitializeComponent ();
		}
        public async void GetCustomers()
        {
            var customerService = new CustomerService();
            var data= await customerService.GetCustomers();
            Customers = data.Data;
            BindingContext = this;
        }
        public List<Customer> Customers { get; set; }
    }
}