using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MaliMali
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTcxNThAMzEzNjJlMzQyZTMwSi91RzVVYTY4Y0o1Z2RWV1ByZVJHT2phUzdYMFNlb3luN2J5S3hmNVZ2OD0=");

            InitializeComponent();

            if (string.IsNullOrEmpty(Model.Settings.UID)) 
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else 
            {
                MainPage = new NavigationPage(new Tabbed());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("ios=c922d353-a990-4184-9872-09968e129ed6;", typeof(Analytics), typeof(Crashes));
            try
            {
                // Test Crash.
                Crashes.GenerateTestCrash();
            }
            catch (Exception exception)
            {
                Crashes.TrackError(exception);
            }
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
