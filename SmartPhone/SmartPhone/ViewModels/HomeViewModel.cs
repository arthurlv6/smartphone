using SmartPhone.Models;
using SmartPhone.Models.APIModels;
using SmartPhone.Services;
using SmartPhone.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace SmartPhone.ViewModels
{
    public class HomeViewModel:BaseViewModel
    {
        #region properties
        public ICommand SaleItemSelectedCommand { get; private set; }
        public ICommand LoadItemsCommand { get; private set; }
        public ICommand Search { get; private set; }
        public ICommand AddSaleCommand { get; private set; }
        
        private string searchText="";
        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged("SearchText");
                }
            }
        }
        private ISaleService SaleService { get; set; }
        private InfiniteScrollCollection<Contract> sales;
        public InfiniteScrollCollection<Contract> Sales {
            get { return sales; }
            set {
                SetProperty(ref sales,value);
            }
        }
        //public List<Purchase> Purchases { get; set; }
        //public List<Report> Reports { get; set; }
        public int PageNumber { get; set; }
        public int TotalRecord { get; set; }
        #endregion
        public HomeViewModel()
        {
            try
            {
                SaleService = DependencyService.Get<ISaleService>();
            }
            catch (Exception ex)
            {

                throw;
            }
            
            SaleItemSelectedCommand = new Command<Contract>(SaleHandleItemSelected);
            LoadItemsCommand = new Command(LoadItems);
            Search = new Command(SearchItem);
            AddSaleCommand = new Command(AddSale);

            Sales = new InfiniteScrollCollection<Contract>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    PageNumber = PageNumber+1;

                    var items = await SaleService.GetSales(SearchText, PageNumber);

                    IsBusy = false;

                    // return the items that need to be added
                    return items.Data;
                },
                OnCanLoadMore = () =>
                {
                    return Sales.Count < TotalRecord;
                }
            };

            GetSales();
        }
        private void AddSale(object obj)
        {
            Navigation.PushAsync(new AddSalePage());
        }

        private void SearchItem(object obj)
        {
            Sales.Clear();
            GetSales();
        }

        private void LoadItems(object obj)
        {
            Sales.Clear();
            GetSales();
        }

        private async void GetSales()
        {
            var data = await SaleService.GetSales(SearchText);
            Sales.AddRange(data.Data);

            PageNumber = data.Header != null ? data.Header.CurrentPage : 1;
            TotalRecord = data.Header != null ? data.Header.TotalCount : 1;
            IsBusy = false;
        }
        private void SaleHandleItemSelected(Contract obj)
        {
            Navigation.PushAsync(new SaleDetailPage(new SaleDetailViewModel(obj)));
        }
    }
}
