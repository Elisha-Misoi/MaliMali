using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Foundation;
using Xamarin.Forms;

namespace MaliMali
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void SigninFrame_Tapped(object sender, System.EventArgs e)
        {
            if (signinLable.Text == "SEND CODE")
            {
                restart.IsVisible = true;
                await SendCode();
            }
            else if (signinLable.Text == "VERIFY CODE")
            {
                await VerifyCode();
            }
        }

        async Task SendCode()
        {
            if (string.IsNullOrEmpty(phoneEntry.Text))
            {
                Helpers.DialogsHelper.Instance.HandleError(Helpers.DialogsHelper.Errors.InputError, "Phone number");
            }
            else
            {
                try
                {
                    activityIndicator.IsRunning = true;
                    description.Text = "Sending code...";
                    signinFrame.IsEnabled = false;
                    phoneEntry.IsEnabled = false;
                    Model.Settings.FirebaseVerificationID = await Services.AuthHelper.Instance.SendVerificationCode(prefix.Text + phoneEntry.Text);
                    Model.Settings.PhoneNumber = prefix.Text + phoneEntry.Text;
                    Debug.WriteLine("PhoneNumber: " + Model.Settings.PhoneNumber);
                    signinLable.Text = "VERIFY CODE";
                    prefix.IsVisible = false;
                    flagImage.IsVisible = false;
                    activityIndicator.IsRunning = false;
                    signinFrame.IsEnabled = true;
                    phoneEntry.IsEnabled = true;
                    phoneEntry.Text = "";
                    phoneEntry.Placeholder = "verification code";
                    description.Text = "Enter the code sent to you";
                }
                catch (NSErrorException ex)
                {
                    Debug.WriteLine(ex.Message);

                    Helpers.DialogsHelper.Instance.HandleError(Helpers.DialogsHelper.Errors.Defined, ex.Error.LocalizedDescription);
                    activityIndicator.IsRunning = false;
                    signinFrame.IsEnabled = true;
                    phoneEntry.IsEnabled = true;
                    description.Text = "Your mobile number to get started";
                    signinLable.Text = "SEND CODE";
                }
            }
        }

        async Task VerifyCode()
        {
            try
            {
                if (string.IsNullOrEmpty(phoneEntry.Text))
                {
                    Helpers.DialogsHelper.Instance.HandleError(Helpers.DialogsHelper.Errors.InputError, "Verification code");
                }
                else
                {
                    activityIndicator.IsRunning = true;
                    description.Text = "Verifying code...";
                    phoneEntry.IsEnabled = false;
                    signinLable.IsEnabled = false;
                    var credentials = await Services.AuthHelper.Instance.GetCredentials(Model.Settings.FirebaseVerificationID, phoneEntry.Text);
                    var User = await Services.AuthHelper.Instance.SignInUser(credentials);
                    Model.Settings.UID = User.Uid;
                    Model.Settings.AccessToken = await User.GetIdTokenAsync(true);
                    description.Text = "Signing you in...";
                    Model.Settings.IsLoggedIn = true;
                }
            }
            catch (AggregateException ex)
            {
                Helpers.DialogsHelper.Instance.HandleError(Helpers.DialogsHelper.Errors.Defined, ex.Message + "\nPlease try again.");
                activityIndicator.IsRunning = false;
                phoneEntry.IsEnabled = true;
                signinFrame.IsEnabled = true;
                description.Text = "Enter the code sent to you";
                signinLable.Text = "VERIFY CODE";
            }
        }

        void Restart_Tapped(object sender, System.EventArgs e)
        {
            restart.IsVisible = false;
            activityIndicator.IsRunning = false;
            signinFrame.IsEnabled = true;
            phoneEntry.IsEnabled = true;
            phoneEntry.Text = string.Empty;
            phoneEntry.Placeholder = "phone number";
            description.Text = "Your mobile number to get started";
            signinLable.Text = "SEND CODE";
        }
    }
}
