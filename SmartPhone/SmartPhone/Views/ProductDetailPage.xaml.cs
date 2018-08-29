using Plugin.Media;
using Plugin.Media.Abstractions;
using SmartPhone.Models.APIModels;
using SmartPhone.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPhone.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductDetailPage : ContentPage
	{
        private MediaFile _mediaFile;
        IProductService ProductService { get; set; }
        Product product;
        public Product Product
        {
            get { return product; }
            set
            {
                if (product != value)
                {
                    product = value;
                    OnPropertyChanged("Product");
                }
            }
        }
        public ProductDetailPage (Product productFromListPage)
		{
            Product = productFromListPage;
            BindingContext = this;
			InitializeComponent ();
            ProductService = DependencyService.Get<IProductService>();
        }
        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Product.Name))
            {
                await DisplayAlert("Warn", "Name can't be empty.", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(Product.ProductCode))
            {
                await DisplayAlert("Warn", "Product Code can't be empty.", "Ok");
                return;
            }
            IsBusy = true;
            if (Product.Profile != null)
            {
                var profile = Product.Profile;
                if (Product.Profile.Contains("http"))
                {
                    int positon = profile.LastIndexOf("/");
                    Product.Profile = profile.Substring(positon + 1);
                }
            }

            var apiReturnProduct = await ProductService.SaveProduct(Product);
            if (apiReturnProduct!=null)
            {
                if (_mediaFile != null)
                {
                    IsBusy = true;
                    var content = new MultipartFormDataContent();
                    content.Add(new StreamContent(_mediaFile.GetStream()), "\"file\"", $"\"{_mediaFile.Path}\"");
                    var fileName = await ProductService.PostImage(apiReturnProduct.Id, content);
                }
                var p = await ProductService.GetProductById(apiReturnProduct.Id);
                MessagingCenter.Send<ProductDetailPage, Product>(this, "SaveProduct", p);
                await Navigation.PopAsync();
            }
            else
            {
                IsBusy = false;
                await DisplayAlert("Warn", "Save failed, please try again", "Ok");
            }
        }
        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", " no camera available", "OK");
                    return;
                }
                _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize=PhotoSize.Small
                });
                if (_mediaFile == null) return;
                FileImage.Source = ImageSource.FromStream(() =>
                {
                    return _mediaFile.GetStream();
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async void PickupPhoto_Clicked(object sender,EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Oops","Pick photo is not supported!","Ok");
                }
                _mediaFile = await CrossMedia.Current.PickPhotoAsync();
                if (_mediaFile == null) return;

                FileImage.Source = ImageSource.FromStream(()=> {
                    return _mediaFile.GetStream();
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Oops", ex.Message, "Ok");
            }
        }
        
    }
}