using System.Collections.Generic;
using System.Threading.Tasks;
using SmartPhone.Models;
using SmartPhone.Models.APIModels;

namespace SmartPhone.Services
{
    public interface ISaleService
    {
        Task<APIData<List<Contract>>> GetSales(string searchText, int page = 1);
    }
}