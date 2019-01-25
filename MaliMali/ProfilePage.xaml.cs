using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MaliMali
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Model.Settings.IsLoggedIn == true)
            {
                profileGrid.IsVisible = true;
                loginPage.IsVisible = false;

                names.Text = "John Doe";
                phoneNumber.Text = Model.Settings.PhoneNumber;
            }
            else
            {
                profileGrid.IsVisible = false;
                loginPage.IsVisible = true;
            }
        }

        async void Login_Tapped(object sender, System.EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        void Edit_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}
