using Microcharts;
using SkiaSharp;
using SmartPhone.ViewModels;
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
    public partial class Home : CarouselPage
    {
        public Home()
        {
            Title = "My Inventory";
            
            var vm = new HomeViewModel();
            vm.Navigation = Navigation;
            BindingContext = vm;
            InitializeComponent();
            Chart1.Chart = new RadialGaugeChart() { Entries = entries };
            Chart2.Chart = new LineChart() { Entries = entries, LineMode = LineMode.Straight, };
            Chart3.Chart = new DonutChart() { Entries = entries };
            Chart4.Chart = new BarChart() { Entries = entries };
            Chart5.Chart = new PointChart() { Entries = entries };
            //Chart6.Chart = new RadarChart() { Entries = entries };
        }
        List<Microcharts.Entry> entries = new List<Microcharts.Entry>
        {
            new Microcharts.Entry(260)
            {
                Color=SKColor.Parse("#FF1943"),
                Label ="January",
                ValueLabel = "260"
            },
             new Microcharts.Entry(200)
            {
                Color=SKColor.Parse("#F11243"),
                Label ="Feburary",
                ValueLabel = "200"
            },
            new Microcharts.Entry(400)
            {
                Color = SKColor.Parse("00BFFF"),
                Label = "March",
                ValueLabel = "400"
            },
             new Microcharts.Entry(330)
            {
                Color=SKColor.Parse("#FF3943"),
                Label ="April",
                ValueLabel = "330"
            },
              new Microcharts.Entry(410)
            {
                Color=SKColor.Parse("#1F1943"),
                Label ="May",
                ValueLabel = "410"
            },
               new Microcharts.Entry(450)
            {
                Color=SKColor.Parse("#AF1943"),
                Label ="June",
                ValueLabel = "450"
            },
                new Microcharts.Entry(600)
            {
                Color=SKColor.Parse("#FA1943"),
                Label ="July",
                ValueLabel = "600"
            },
                 new Microcharts.Entry(500)
            {
                Color=SKColor.Parse("#FF1943"),
                Label ="August",
                ValueLabel = "500"
            },
                  new Microcharts.Entry(400)
            {
                Color=SKColor.Parse("#F11943"),
                Label ="September",
                ValueLabel = "400"
            },
            new Microcharts.Entry(350)
            {
                Color =  SKColor.Parse("#A4CED1"),
                Label = "Octobar",
                ValueLabel = "350"
            },
            new Microcharts.Entry(400)
            {
                Color =  SKColor.Parse("#1BCED1"),
                Label = "November",
                ValueLabel = "400"
            },
            new Microcharts.Entry(500)
            {
                Color =  SKColor.Parse("#F2CED1"),
                Label = "December",
                ValueLabel = "500"
            },
            };
    }
}