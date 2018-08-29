using SmartPhone.Models;
using SmartPhone.Models.APIModels;
using SmartPhone.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaleService))]
namespace SmartPhone.Services
{
    public class SaleService : BaseRepository, ISaleService
    {
        public SaleService():base()
        {

        }
        public async Task<APIData<List<Contract>>> GetSales(string searchText,int page=1)
        {
            APIData<List<Contract>> sales;
            if (!string.IsNullOrEmpty(searchText))
                sales = await GetAsync<List<Contract>>("Contracts" + "?Name=" + searchText+ "&page="+page);
            else
                sales = await GetAsync<List<Contract>>("Contracts?page=" + page);
            if (sales == null) return new APIData<List<Contract>>();
            return sales;
        }
    }
}

