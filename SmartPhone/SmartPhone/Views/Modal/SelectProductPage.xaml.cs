using SmartPhone.Models.APIModels;
using SmartPhone.Services;
using SmartPhone.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace SmartPhone.Views.Modal
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectProductPage : ContentPage
	{
        AddSaleViewModel ParentVM;
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Product> SelectedProducts { get; set; }
        IProductService ProductService { get; set; }
        public SelectProductPage (AddSaleViewModel parent) :base()
		{
            InitializeComponent();
            ParentVM = parent;
            SelectedProducts = ParentVM.SelectedProducts;
            ProductService = DependencyService.Get<IProductService>();
            SearchProductBar.TextChanged += SearchProductBar_TextChanged;
            GetProducts();
            
        }
        
        private async void SearchProductBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                var data = await ProductService.GetProducts(e.NewTextValue);
                Products = new ObservableCollection<Product>(data.Data);
            }
            else
            {
                var data = await ProductService.GetProducts();
                Products = new ObservableCollection<Product>(data.Data);
            }
            ProductsListView.ItemsSource = null;
            ProductsListView.ItemsSource = Products;
        }

        public async void GetProducts()
        {
            var data = await ProductService.GetProducts();
            Products = new ObservableCollection<Product>(data.Data);
            foreach (var product in Products)
            {
                product.Profile = ProductService.EndPoint + "image/" + product.Profile ?? "none.gif";
            }

            BindingContext = this;
        }
        void AddClicked(object sender, EventArgs e)
        {
            var obj = ((Button)sender).CommandParameter as Product;
            RefreshList(true, obj);
        }
        void DelClicked(object sender, EventArgs e)
        {
            var obj = ((Button)sender).CommandParameter as Product;
            RefreshList(false, obj);
        }
        private void RefreshList(bool isadd, Product p)
        {
            if (p == null || p.Id == 0)
            {
                ScanNoFoundMessage.Text = "No found barcode";
                return;
            }
            var existingProduct = SelectedProducts.FirstOrDefault(d => d.Id == p.Id);
            if (isadd)
            {
                if (existingProduct == null)
                {
                    p.Quantity = 1;
                    SelectedProducts.Add(p);
                }
                else
                {
                    existingProduct.Quantity++;
                }
            }
            else
            {
                if (existingProduct == null)
                {
                    return;
                }
                else
                {
                    if (existingProduct.Quantity > 1)
                        existingProduct.Quantity--;
                    else
                        SelectedProducts.Remove(existingProduct);
                }
            }
            Total.Text = SelectedProducts.Sum(d => d.Price * d.Quantity).ToString();
            SelectedProductsListView.ItemsSource = null;
            SelectedProductsListView.ItemsSource = SelectedProducts;
        }
        void ConfirmProductsClicked(object sender, EventArgs e)
        {
            ParentVM.SelectedProducts = SelectedProducts;
            Navigation.PopAsync();
        }
        ZXingScannerPage scanPage;
        private async void ScanBarcode(object sender, EventArgs e)
        {
            ScanNoFoundMessage.Text = "";
            scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += ScanPage_OnScanResult;
            await Navigation.PushAsync(scanPage);
        }

        private void ScanPage_OnScanResult(ZXing.Result result)
        {
            scanPage.IsScanning = false;

            Device.BeginInvokeOnMainThread(async() =>
            {
                var product = await ProductService.GetProductByBarcode(result.Text);
                RefreshList(true, product);
                await Navigation.PopAsync();
            });
        }


    }
}