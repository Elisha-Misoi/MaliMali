using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace MaliMali
{
    public partial class AdminPage : ContentPage
    {
        List<string> images = new List<string>();
        List<string> download_urls = new List<string>();
        List<string> categoryList;

        public AdminPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            categoryList = new List<string>
            {
                "Toys",
                "Vehicles",
                "Clothes",
                "Households",
                "Food",
                "Electronics"
            };
            categoryPicker.ItemsSource = categoryList;
        }

        void Carousel_SelectionChanged(object sender, Syncfusion.SfCarousel.XForms.SelectionChangedEventArgs e)
        {

        }

        async void AddImage_Clicked(object sender, System.EventArgs e)
        {
            await ChooseOptions();
        }


        async Task ChooseOptions()
        {
            var action = await DisplayActionSheet("Choose an option", "Cancel", null, "Use Camera", "Choose Photo");

            if (action == "Use Camera")
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        await AskForPermissions();
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Camera))
                        status = results[Permission.Camera];
                }
                if (status == PermissionStatus.Granted)
                {
                    var _path = await Helpers.FilePicker.Instance.TakePhotoAndSave();
                    if(_path != null) 
                    { 
                        images.Add(_path);
                        carousel.ItemsSource = null;
                        carousel.ItemsSource = images;  
                    }
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await AskForPermissions();
                }
            }
            else if (action == "Choose Photo")
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Photos))
                    {
                        await AskForPermissions();
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Photos);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Photos))
                        status = results[Permission.Photos];
                }
                if (status == PermissionStatus.Granted)
                {
                    var _path = await Helpers.FilePicker.Instance.PickPhotoAndSave();
                    if(_path != null) 
                    { 
                        images.Add(_path);
                        carousel.ItemsSource = null;
                        carousel.ItemsSource = images;
                    }
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await AskForPermissions();
                }
            }
        }

        async Task AskForPermissions()
        {
            var action = await Application.Current.MainPage.DisplayAlert("Gallery Permissions", "MaliMali would like to access you Gallery", "Yes", "No");
            if (action == true)
            {
                // access settings
            }
        }


        async Task Verify_Item()
        {
            if (string.IsNullOrEmpty(title.Text) || !images.Any() || string.IsNullOrEmpty(price.Text) || string.IsNullOrEmpty(ammount_available.Text) || string.IsNullOrEmpty(categoryPicker.Items[categoryPicker.SelectedIndex]))
            {
                Helpers.DialogsHelper.Instance.HandleError(Helpers.DialogsHelper.Errors.InputError, "All fields must be field");
            }
            else
            {
                string ID = Services.DatabaseHelper.Instance.FirebaseID();
                await UploadItem(new Model.Item
                {
                    Available = long.Parse(ammount_available.Text),
                    Description = description.Text,
                    Images = download_urls,
                    Item_ID = ID,
                    Post_Date = Helpers.DateHelper.Instance.ReturnCurrentTimeInLong(),
                    Price = long.Parse(price.Text),
                    Rating = 4,
                    Title = title.Text,
                    User_ID = Model.Settings.UID,
                    Category = categoryPicker.Items[categoryPicker.SelectedIndex]
                });
            }
        }

        async Task UploadItem(Model.Item item)
        {
            Helpers.DialogsHelper.Instance.progressDialog.Show();

            await UploadImages();

            var db_path = "Items/";
            var status = await Services.DatabaseHelper.Instance.PostObject(db_path, item);
            if (status == 201)
            {
                Helpers.DialogsHelper.Instance.progressDialog.Hide();
                await DisplayAlert("Success", "The item has been successfully uploaded.", "OK");
            }
            price.Text = "";
            description.Text = "";
            download_urls.Clear();
            ammount_available.Text = "";
            title.Text = "";
        }

        async Task UploadImages()
        {
            string download_url = null;

            foreach(var item in images) 
            {
                var filename = DateTime.Now.ToString().Replace(" ", string.Empty);
                download_url = await Services.StorageHelper.Instance.UploadTempFile(Filename: filename, Bucket: "Items", Media_Path: item);
                download_urls.Add(download_url);
            }
        }


        async void Upload_Tapped(object sender, System.EventArgs e)
        {
            await Verify_Item();
        }
    }
}
