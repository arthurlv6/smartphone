using SmartPhone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPhone.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartPhone.Models.APIModels;

namespace SmartPhone.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductPage : ContentPage
	{
        public ProductPage ()
		{
            var vm= new ProductViewModel();
            BindingContext = vm;
            vm.Navigation = Navigation;
            InitializeComponent();
            MessagingCenter.Subscribe<ProductViewModel, Product>(this, "ScanProduct", (sender, arg) =>
            {
                ProductsList.ScrollTo(arg, ScrollToPosition.Start, true);
            });
        }
	}
}