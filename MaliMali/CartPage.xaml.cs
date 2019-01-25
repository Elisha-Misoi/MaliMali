using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaliMali
{
    public partial class CartPage : ContentPage
    {
        public static List<string> images = new List<string>();
        ObservableCollection<Model.Item> items = new ObservableCollection<Model.Item>();

        public CartPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await FetchItems();
        }

        async Task FetchItems() 
        {
            items = await Services.DatabaseHelper.Instance.GetItems("Cart/" + Model.Settings.UID);
            carousel.ItemsSource = items;
            if (items.Any()) 
            {
                long total_price = 0;
                foreach (var item in items)
                {
                    total_price = total_price + item.Price;
                }
                subtotal.Text = "Ksh. " + total_price;
                total.Text = "Ksh. " + total_price;
            }
        }


        void Carousel_SelectionChanged(object sender, Syncfusion.SfCarousel.XForms.SelectionChangedEventArgs e)
        {

        }

        async void Checkout_Tapped(object sender, System.EventArgs e)
        {
            var action = await Application.Current.MainPage.DisplayAlert("Purchase item?", "", "Yes", "No");
            if (action == true)
            {
                int status = 0;
                foreach (var item in items)
                {
                    Helpers.DialogsHelper.Instance.progressDialog.Show();
                    var db_path = "Purchased/" + Model.Settings.UID + "/";
                    status = await Services.DatabaseHelper.Instance.PostObject(db_path, item);
                }
                if (status == 201)
                {
                    var cart_path = "Cart/" + Model.Settings.UID + "/";
                    await Services.DatabaseHelper.Instance.DeleteObject(cart_path);

                }
                await Application.Current.MainPage.DisplayAlert("Success", "The item(s) successfully purchased.", "OK");
                subtotal.Text = "Ksh. " + 0;
                total.Text = "Ksh. " + 0;
                carousel.ItemsSource = null;
                Helpers.DialogsHelper.Instance.progressDialog.Hide();
            }
        }
    }
}
