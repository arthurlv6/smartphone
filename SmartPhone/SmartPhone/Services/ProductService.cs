using SmartPhone.Models;
using SmartPhone.Models.APIModels;
using SmartPhone.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProductService))]
namespace SmartPhone.Services
{
    public class ProductService : BaseRepository, IProductService
    {
        public ProductService():base()
        {

        }

        public string EndPoint { get => Endpoint; }

        public async Task<APIData<List<Product>>> GetProducts(string name=null, int page = 1)
        {
            APIData<List<Product>> products;
            if (!string.IsNullOrEmpty(name))
                products = await GetAsync<List<Product>>("Products"+"?name="+name+"&page="+page);
            else
                products = await GetAsync<List<Product>>("Products?page=" + page);
            return products;
        }
        public async Task<Product> GetProductById(int Id )
        {
            return await GetSingleAsync<Product>("Products/" + Id);
        }
        public async Task<Product> GetProductByBarcode(string code = null)
        {
                return await GetSingleAsync<Product>("Products/Barcode/" + code);
        }

        public async Task<Product> SaveProduct(Product p)
        {
            if (p.Id == 0) //add
            {
                var newProduct= await PostAsync("Products", p);
                return newProduct;
            }
            else //update
                if (await PutAsync("Products/" + p.Id, p))
                    return p;
                else
                    return null;
        }
        public async Task<string> PostImage(int id, MultipartFormDataContent content)
        {
            HttpClient httpClient = CreateHttpClient();
            var httpResponseMessage = await httpClient.PostAsync(EndPoint+"image/" + id, content);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<bool> Delete(int id)
        {
            return await DeleteAsync("products/"+id);
        }
    }
}
