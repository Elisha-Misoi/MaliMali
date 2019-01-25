using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MaliMali
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTcxNThAMzEzNjJlMzQyZTMwSi91RzVVYTY4Y0o1Z2RWV1ByZVJHT2phUzdYMFNlb3luN2J5S3hmNVZ2OD0=");

            InitializeComponent();

            MainPage = new Tabbed();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
