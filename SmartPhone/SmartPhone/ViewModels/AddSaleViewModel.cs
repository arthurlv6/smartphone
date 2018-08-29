using SmartPhone.Models.APIModels;
using SmartPhone.Views.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartPhone.ViewModels
{
    public class AddSaleViewModel:BaseViewModel
    {
        public ICommand SaveSaleCommand { get; private set; }
        public ICommand SelectCustomerCommand { get; private set; }
        public ICommand SelectProductCommand { get; private set; }
        public Contract Sale { get; set; }
        public AddSaleViewModel()
        {
            SaveSaleCommand = new Command(SaveSale);
            SelectCustomerCommand = new Command(SelectCustomer);
            SelectProductCommand = new Command(SelectProduct);
            SelectedProducts = new ObservableCollection<Product>();
            Sale = new Contract();
            Sale.CreateDate = DateTime.Now;
            Sale.DeliverDate = DateTime.Now;

        }
        public ObservableCollection<Product> SelectedProducts { get; set; }
        private void SelectProduct(object obj)
        {
            var p = new SelectProductPage(this);

            Navigation.PushAsync(p);
        }

        private void SelectCustomer(object obj)
        {
            Navigation.PushModalAsync(new SelectCustomerPage());
        }

        private void SaveSale(object obj)
        {
            Navigation.PopAsync();
        }
    }
}
