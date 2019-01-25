using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace MaliMali
{
    public partial class History : ContentPage
    {
        ObservableCollection<Model.Item> items = new ObservableCollection<Model.Item>();

        public History()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            items = await Services.DatabaseHelper.Instance.GetItems("Purchased/" + Model.Settings.UID);
            ItemsListView.ItemsSource = items;
            if (items.Any())
            {
                long total_price = 0;
                foreach (var item in items)
                {
                    total_price = total_price + item.Price;
                }
                total_spent.Text = "Ksh. " + total_price;
            }
            else 
            {
                total_spent.Text = "Ksh. " + 0;
            }
        }
    }
}
