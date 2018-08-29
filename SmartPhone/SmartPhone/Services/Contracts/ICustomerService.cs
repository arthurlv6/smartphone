using System.Collections.Generic;
using System.Threading.Tasks;
using SmartPhone.Models;
using SmartPhone.Models.APIModels;

namespace SmartPhone.Services
{
    public interface ICustomerService
    {
        Task<APIData<List<Customer>>> GetCustomers(string searchText = null);
    }
}