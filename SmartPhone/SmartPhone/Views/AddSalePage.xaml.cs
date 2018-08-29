using SmartPhone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPhone.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddSalePage : ContentPage
	{
		public AddSalePage ()
		{
            var vm= new AddSaleViewModel();
            vm.Navigation = Navigation;
            BindingContext = vm;
			InitializeComponent ();
		}
	}
}