using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MaliMali
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ItemsListView.ItemsSource = await Services.DatabaseHelper.Instance.GetItems("Items");
        }

        async void Categories_Tapped(object sender, System.EventArgs e)
        {

        }

        async void ItemsListView_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var tapped = e.Item as Model.Item;
            await Navigation.PushAsync(new ViewItem(tapped));
        }
    }
}
