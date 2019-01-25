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

        async void Categories_Tapped(object sender, System.EventArgs e)
        {

        }

        async void ItemsListView_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
           
        }
    }
}
