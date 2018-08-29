using SmartPhone.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SmartPhone.Views
{
	public partial class MySettings : ContentPage
	{
		MySettingsViewModel vm;
		public MySettings ()
		{
			vm = new MySettingsViewModel ();
			Title = "Settings";
			BindingContext = vm;
			InitializeComponent ();
		}

	}
}

