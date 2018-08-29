using Plugin.ExternalMaps;
using Plugin.Messaging;
using SmartPhone.Helper;
using SmartPhone.Models;
using SmartPhone.Models.APIModels;
using SmartPhone.Services;
using SmartPhone.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;
using ZXing.Net.Mobile.Forms;

namespace SmartPhone.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        #region properties
        public int PageNumber { get; set; }
        public int TotalRecord { get; set; }
        public ICommand LoadItemsCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddProductCommand { get; private set; }
        public ICommand ScanProductCommand { get; private set; }
        
        public IProductService ProductService { get; set; }

        public InfiniteScrollCollection<Product> Products { get; set; }
        #endregion
        public ProductViewModel()
        {
            IsBusy = true;
            ProductService = DependencyService.Get<IProductService>();
            EditCommand = new Command<Product>(ProductEditCommand);
            DeleteCommand = new Command<Product>(ProductDeleteCommand);
            LoadItemsCommand = new Command(LoadItems);
            AddProductCommand= new Command(AddItem);
            ScanProductCommand = new Command(ScanItem);
            Products = new InfiniteScrollCollection<Product>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    PageNumber = PageNumber + 1;

                    var items = await ProductService.GetProducts("", PageNumber);

                    IsBusy = false;

                    // return the items that need to be added
                    Embellish(items.Data);
                    return items.Data;
                },
                OnCanLoadMore = () =>
                {
                    return Products.Count < TotalRecord;
                }
            };

            GetProducs();
            MessagingCenter.Subscribe<ProductDetailPage, Product>(this, "SaveProduct", (sender, arg) =>
            {
                FindItemAndUpdate(arg);
            });
        }

        ZXingScannerPage scanPage;
        private async void ScanItem(object obj)
        {
            scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += ScanPage_OnScanResult;
            await Navigation.PushAsync(scanPage);
        }

        private void ScanPage_OnScanResult(ZXing.Result result)
        {
            scanPage.IsScanning = false;

            Device.BeginInvokeOnMainThread(async () =>
            {
                var product = await ProductService.GetProductByBarcode(result.Text);
                
                if (product != null)
                {
                    var p = Products.FirstOrDefault(d => d.Id == product.Id);
                    if (p != null)
                    {
                        p.ShowDetail = false;
                        UpdownDetail(p);
                        MessagingCenter.Send<ProductViewModel, Product>(this, "ScanProduct", p);
                    }
                    else
                    {
                        EmbellishSingleProduct(product);
                        product.ShowDetail = true;
                        Products.Insert(0, product);
                        MessagingCenter.Send<ProductViewModel, Product>(this, "ScanProduct", product);
                    }
                    
                    await Navigation.PopAsync();
                }
                else
                {
                    await Navigation.PopAsync();
                    await App.Current.MainPage.DisplayAlert("Sorry","There is not barcode:"+result.Text, "Ok");
                }
            });
        }

        private void AddItem(object obj)
        {
            Navigation.PushAsync(new ProductDetailPage(new Product()));
        }

        private void LoadItems(object obj)
        {
            Products.Clear();
            GetProducs();
        }
        private async void ProductDeleteCommand(Product obj)
        {
            
            var answer = await App.Current.MainPage.DisplayAlert("Confirm", "Do you want to delete " + obj.Name + "?", "Yes", "No");
            if (answer)
            {
                if( await ProductService.Delete(obj.Id))
                    FindItemAndUpdate(obj, true);
                else
                    await App.Current.MainPage.DisplayAlert("Warn", "Delete failed, please try again", "Ok");
            }
        }
        private void ProductEditCommand(Product obj)
        {
            Navigation.PushAsync(new ProductDetailPage(obj));
        }
        private void FindItemAndUpdate(Product p, bool remove = false)
        {
            var item = Products.FirstOrDefault(d => d.Id == p.Id);
            if (item != null)
            {
                var i = Products.IndexOf(item);
                Products.Remove(item);
                if (!remove)
                {
                    EmbellishSingleProduct(p);
                    Products.Insert(i, p);
                }
                return;
            }
            else
            {
                EmbellishSingleProduct(p);
                Products.Insert(0, p);
            }
        }
        private async void GetProducs()
        {
            var apiReturn = await ProductService.GetProducts();
            Embellish(apiReturn.Data);
            Products.AddRange(apiReturn.Data);

            PageNumber = apiReturn.Header != null ? apiReturn.Header.CurrentPage : 1;
            TotalRecord = apiReturn.Header != null ? apiReturn.Header.TotalCount : 1;

            IsBusy = false;
        }
        private void Embellish(List<Product> list)
        {
            foreach (var product in list)
            {
                EmbellishSingleProduct(product);
            }
        }
        private void EmbellishSingleProduct(Product p)
        {
            if (p.Profile == null || p.Profile.Contains("null"))
                p.Profile = ProductService.EndPoint + "image/none.gif";
            else
                if (p.Profile.Contains("/"))
                {
                    var position = p.Profile.LastIndexOf("/") + 1;
                    p.Profile = ProductService.EndPoint + "image/" + p.Profile.Substring(position);
                }
                else
                    p.Profile = ProductService.EndPoint + "image/" + p.Profile;

            p.UpdownCommand = new Command<Product>(UpdownDetail);
            p.CallSupplier = new Command<Product>(CallSupplier);
            p.SupplierAddressCommand = new Command<Product>(SupplierAddress);
        }
        private void CallSupplier(Product obj)
        {
            if (obj.Supplier == null) return;
            if (string.IsNullOrEmpty(obj.Supplier.ContactPhoneNumber)) return;
            var phoneCall = CrossMessaging.Current.PhoneDialer;
            if (phoneCall.CanMakePhoneCall)
            {
                phoneCall.MakePhoneCall(obj.Supplier.ContactPhoneNumber, obj.Supplier.Name);
            }
        }
        private void UpdownDetail(Product obj)
        {
            obj.ShowDetail = !obj.ShowDetail;
            if (obj.ShowDetail)
                obj.UpdownImage = "collapse.png";
            else
                obj.UpdownImage = "expand.png";
            FindItemAndUpdate(obj);
        }
        private void SupplierAddress(Product obj)
        {
            CrossExternalMaps.Current.NavigateTo("Arthur home", "29 Killarney street.", "Auckland", "", "00199-0000", "New zealand", "64");
        }
    }
}
