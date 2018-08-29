using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SmartPhone.Models;
using SmartPhone.Models.APIModels;

namespace SmartPhone.Services
{
    public interface IProductService
    {
        Task<APIData<List<Product>>> GetProducts(string name = null,int page = 1);
        Task<Product> GetProductByBarcode(string code = null);
        Task<string> PostImage(int id, MultipartFormDataContent content);
        string EndPoint { get; }
        Task<Product> SaveProduct(Product p);
        Task<Product> GetProductById(int Id);
        Task<bool> Delete(int id);
    }
}