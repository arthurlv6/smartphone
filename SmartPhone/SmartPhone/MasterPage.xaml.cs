using SmartPhone.Models;
using SmartPhone.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartPhone
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
        public ListView ListView { get { return listView; } }

        public MasterPage()
        {
            InitializeComponent();
            //BackgroundImage = "ground.png";
            var masterPageItems = new List<MasterPageItem>();
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Dashhoard",
                IconSource = "main.png",
                TargetType = typeof(Home)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Settings",
                IconSource = "settings.png",
                TargetType = typeof(MySettings)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Products",
                IconSource = "list.png",
                TargetType = typeof(ProductPage)
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "Invertories",
                IconSource = "manual.png",
                TargetType = typeof(Home),
                Parameter = "http://ouroptions.co.nz/PoneAdvertisement/manual"
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Suppliers",
                IconSource = "info.png",
                TargetType = typeof(Home),
                Parameter = "http://ouroptions.co.nz/PoneAdvertisement/contact"
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Customers",
                IconSource = "list.png",
                TargetType = typeof(Home)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Report",
                IconSource = "list.png",
                TargetType = typeof(Home)
            });
            listView.ItemsSource = masterPageItems;
        }
    }
}