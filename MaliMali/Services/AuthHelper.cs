using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Firebase.Auth;
using Foundation;

namespace MaliMali.Services
{

    public class AuthHelper
    {
        //Sending Verification Code
        public async Task<string> SendVerificationCode(string phoneNumber)
        {
            //Returns Firebase Verification ID
            return await PhoneAuthProvider.DefaultInstance.VerifyPhoneNumberAsync(phoneNumber, null);
        }

        //Getting User Credentials
        public async Task<PhoneAuthCredential> GetCredentials(string firebaseVerificationId, string verificationCode)
        {
            //Returns Credentials
            PhoneAuthCredential credential = await Task.Run(() =>
            {
                return PhoneAuthProvider.DefaultInstance.GetCredential(firebaseVerificationId, verificationCode);
            });
            return credential;
        }

        //Signing in User
        public async Task<User> SignInUser(PhoneAuthCredential credential)
        {
            //Returns Firebase Auth User Object
            AuthDataResult userData = await Task.Run(() =>
            {
                var signin = Auth.DefaultInstance.SignInAndRetrieveDataWithCredentialAsync(credential);
                return signin.Result;
            });
            return userData.User;
        }

        //Signing out User
        public async Task SignOutUser()
        {
            //Sign Out User
            try
            {
                await Task.Run(() =>
                {
                    Auth.DefaultInstance.SignOut(out NSError error);
                });
                Model.Settings.Name = string.Empty;
                Model.Settings.Profile_pic_url = string.Empty;
                Model.Settings.Date_joined = string.Empty;
                Model.Settings.IsLoggedIn = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static AuthHelper Instance { get; } = new AuthHelper();
    }

}
