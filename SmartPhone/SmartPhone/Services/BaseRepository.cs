using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartPhone.Models;
using Xamarin.Forms;

namespace SmartPhone.Services
{
    public class BaseRepository
    {
        //public string Endpoint { get; private set; } = "http://api.inventory.arthurdid.net/" + "/api/";
        public string Endpoint { get; private set; } = "http://eca9835f.ngrok.io" + "/api/";
        //http://eca9835f.ngrok.io

        //private ILocalData _db;
        public BaseRepository()
        {
            //_db = DependencyService.Get<ILocalData>();
        }
        internal HttpClient CreateHttpClient()
        {
            /*
            var info = _db.GetUserInfo();
            if (info == null)
            {
                var userInfo = new UserInfo();
                userInfo.Token= "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhcnRodXIubHY2QGdtYWlsLmNvbSIsImp0aSI6ImRkMjNlOTY1LWM5ZjQtNGEwMC1hYzEyLWQ4YmNmZjY4OTk4OSIsImVtYWlsIjoiYXJ0aHVyLmx2NkBnbWFpbC5jb20iLCJleHAiOjE1NDU0NDcyMjIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDkzNjEvIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0OTM2MS8ifQ.A4RWrc10i_p5cLRPPaesWT_1DYibfJvSCWm8YYhXZGc";
                userInfo.ServerEndPoint = Endpoint;
                _db.SaveUserInfo(userInfo);
                info = userInfo;
            }
            else
            {
                Endpoint = info.ServerEndPoint;
            }
            */
            var info = new UserInfo()
            {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhcnRodXIubHY2QGdtYWlsLmNvbSIsImp0aSI6ImRkMjNlOTY1LWM5ZjQtNGEwMC1hYzEyLWQ4YmNmZjY4OTk4OSIsImVtYWlsIjoiYXJ0aHVyLmx2NkBnbWFpbC5jb20iLCJleHAiOjE1NDU0NDcyMjIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDkzNjEvIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0OTM2MS8ifQ.A4RWrc10i_p5cLRPPaesWT_1DYibfJvSCWm8YYhXZGc",
            };
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",info.Token);
            return httpClient;
        }

        protected async Task<APIData<T>> GetAsync<T>(string url)
            where T : new()
        {
            APIData<T> result=new APIData<T>();
            try
            {
                using (HttpClient httpClient = CreateHttpClient())
                {
                    using (var readContent = await httpClient.GetAsync(Endpoint + url))
                    {
                        string data = await readContent.Content.ReadAsStringAsync();
                        result.Data = await Task.Run(() => JsonConvert.DeserializeObject<T>(data));

                        var headers = readContent.Headers.Concat(readContent.Content.Headers).FirstOrDefault(d => d.Key == "X-Pagination").Value;
                        if (headers != null)
                            result.Header = await Task.Run(() => JsonConvert.DeserializeObject<APIHeader>(headers.FirstOrDefault()));
                        return result;
                    }
                }
            }
            catch
            {
                result = null;
            }

            return result;
        }
        protected async Task<T> GetSingleAsync<T>(string url)
            where T : new()
        {
            T result ;
            try
            {
                using (HttpClient httpClient = CreateHttpClient())
                {
                    var t = await httpClient.GetStringAsync(Endpoint + url);
                    return await Task.Run(() => JsonConvert.DeserializeObject<T>(t));
                }
            }
            catch
            {
                result = default(T);
            }

            return result;
        }
        protected async Task<T> PostAsync<T>(string url,T t) where T : new()
        {
            HttpClient httpClient = CreateHttpClient();
            try
            {
                var item = JsonConvert.SerializeObject(t);

                var response = await httpClient.PostAsync(Endpoint + url, new StringContent(item, System.Text.Encoding.Unicode, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var contents = await response.Content.ReadAsStringAsync();
                    var newProduct = await Task.Run(() => JsonConvert.DeserializeObject<T>(contents));
                    return newProduct;
                }
                else
                    return new T();
            }
            catch
            {
                return new T();
            }
        }
        protected async Task<bool> PutAsync<T>(string url, T t) where T : new()
        {
            HttpClient httpClient = CreateHttpClient();
            try
            {
                var item = JsonConvert.SerializeObject(t);
                var response = await httpClient.PutAsync(Endpoint + url, new StringContent(item, System.Text.Encoding.Unicode, "application/json"));
                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        protected async Task<bool> DeleteAsync(string url)
        {
            HttpClient httpClient = CreateHttpClient();
            try
            {
                var response = await httpClient.DeleteAsync(Endpoint + url);
                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}