using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MaliMali
{
    public partial class ViewItem : ContentPage
    {
        Model.Item Item;
        public ViewItem(Model.Item item)
        {
            InitializeComponent();
            Item = item;
            Title = item.Title;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            carousel.ItemsSource = Item.Images;
            price.Text = "Ksh. " + Item.Price;
            description.Text = Item.Description;
            category.Text = Item.Category;
        }

        async void Checkout_Tapped(object sender, System.EventArgs e)
        {
            var action = await DisplayAlert("Add to cart?", "", "Yes", "No");
            if (action == true)
            {
                Helpers.DialogsHelper.Instance.progressDialog.Show();
                var db_path = "Cart/" + Model.Settings.UID + "/";
                var status = await Services.DatabaseHelper.Instance.PostObject(db_path, Item);
                if (status == 201)
                {
                    Helpers.DialogsHelper.Instance.progressDialog.Hide();
                    await DisplayAlert("Success", "The item has been added to cart.", "OK");
                    await Navigation.PopAsync();
                }
            }
        }


    }
}
