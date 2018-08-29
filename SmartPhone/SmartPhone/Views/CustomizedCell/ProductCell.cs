using SmartPhone.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartPhone.Views.CustomizedCell
{
    public class ProductCell:ViewCell
    {
        Image image = null;

        public ProductCell()
        {
            image = new Image();
            View = image;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var item = BindingContext as Product;
            if (item != null)
            {
                image.Source = item.Profile;
            }
        }
    }
}
