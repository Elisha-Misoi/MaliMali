using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MaliMali.Model
{
    public class Settings
    {
        static ISettings AppSettings => CrossSettings.Current;

        public static string FirebaseVerificationID
        {
            get => AppSettings.GetValueOrDefault(nameof(FirebaseVerificationID), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(FirebaseVerificationID), value);
        }

        public static string PhoneNumber
        {
            get => AppSettings.GetValueOrDefault(nameof(PhoneNumber), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(PhoneNumber), value);
        }

        public static string UID
        {
            get => AppSettings.GetValueOrDefault(nameof(UID), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(UID), value);
        }


        public static string Name
        {
            get => AppSettings.GetValueOrDefault(nameof(Last_name), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Last_name), value);
        }

        public static string AccessToken
        {
            get => AppSettings.GetValueOrDefault(nameof(AccessToken), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(AccessToken), value);
        }

        public static string Messaging_token
        {
            get => AppSettings.GetValueOrDefault(nameof(Messaging_token), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Messaging_token), value);
        }

        public static string Mobile_no
        {
            get => AppSettings.GetValueOrDefault(nameof(Mobile_no), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Mobile_no), value);
        }

        public static string Name
        {
            get => AppSettings.GetValueOrDefault(nameof(Name), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Name), value);
        }

        public static string National_id
        {
            get => AppSettings.GetValueOrDefault(nameof(National_id), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(National_id), value);
        }

        public static string Passport_no
        {
            get => AppSettings.GetValueOrDefault(nameof(Passport_no), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Passport_no), value);
        }

        public static string Profile_pic_url
        {
            get => AppSettings.GetValueOrDefault(nameof(Profile_pic_url), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Profile_pic_url), value);
        }

        public static string Date_joined
        {
            get => AppSettings.GetValueOrDefault(nameof(Date_joined), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Date_joined), value);
        }

        public static bool CompletedProfile
        {
            get => AppSettings.GetValueOrDefault(nameof(CompletedProfile), false);
            set => AppSettings.AddOrUpdateValue(nameof(CompletedProfile), value);
        }

        public static bool IsLoggedIn
        {
            get => AppSettings.GetValueOrDefault(nameof(IsLoggedIn), false);
            set => AppSettings.AddOrUpdateValue(nameof(IsLoggedIn), value);
        }
    }
}
