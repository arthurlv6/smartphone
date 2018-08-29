using SmartPhone.Models;
using SmartPhone.Models.APIModels;
using SmartPhone.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(CustomerService))]
namespace SmartPhone.Services
{
    public class CustomerService : BaseRepository, ICustomerService
    {
        public CustomerService():base()
        {

        }
        public async Task<APIData<List<Customer>>> GetCustomers(string searchText=null)
        {
            APIData<List<Customer>> customers;
            if (!string.IsNullOrEmpty(searchText))
                customers = await GetAsync<List<Customer>>("Customers" + "?Name=" + searchText);
            else
                customers = await GetAsync<List<Customer>>("Customers");

            return customers;
        }
    }
}
