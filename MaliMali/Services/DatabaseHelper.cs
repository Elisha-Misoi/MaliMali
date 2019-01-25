using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using MaliMali.Helpers;

namespace MaliMali.Services
{
    // This class uses FirebaseDatabase.net plugin
    public class DatabaseHelper
    {
        // Firebase Database Client
        public readonly FirebaseClient firebaseClient = new FirebaseClient("https://malimali.firebaseio.com/");

        // Firebase Database OfflineClient

        // Putting Objects to DB
        public async Task<int> PutObject(string path, object obj)
        {
            int status_code = 599; // network connect timeout error
            try
            {
                await firebaseClient
                    .Child(path)
                    .PutAsync(obj);
                status_code = 201; // db update OK
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Helpers.DialogsHelper.Instance.HandleError(DialogsHelper.Errors.NetworkError, null);
            }
            return status_code;
        }

        // Posting Objects to DB
        public async Task<int> PostObject(string path, object obj)
        {
            int status_code = 599; // network connect timeout error
            try
            {
                await firebaseClient
                    .Child(path)
                    .PostAsync(obj);
                status_code = 201; // db update OK
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Helpers.DialogsHelper.Instance.HandleError(DialogsHelper.Errors.NetworkError, null);
            }
            return status_code;
        }


        // Deleting Objects in DB
        public async Task<int> DeleteObject(string path)
        {
            int status_code = 599; // network connect timeout error
            try
            {
                await firebaseClient
                    .Child(path)
                    .DeleteAsync();
                status_code = 200; // db update OK
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Helpers.DialogsHelper.Instance.HandleError(DialogsHelper.Errors.NetworkError, null);
            }
            return status_code;
        }

        // Getting single user
        public async Task<Model.User> GetSingleUser(string path)
        {
            var user = new Model.User();
            try
            {
                user = await firebaseClient
                       .Child(path)
                       .OnceSingleAsync<Model.User>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Helpers.DialogsHelper.Instance.HandleError(DialogsHelper.Errors.NetworkError, null);
            }
            return user;
        }

        // Get Items
        public async Task<ObservableCollection<Model.Item>> GetItems(string path)
        {
            ObservableCollection<Model.Item> items = new ObservableCollection<Model.Item>();
            try
            {
                var _items = await firebaseClient.Child(path).OnceAsync<Model.Item>();
                foreach(var item in _items) 
                {
                    item.Object.Image_url = item.Object.Images[0];
                    item.Object.Category = "Category: " + item.Object.Category;
                    items.Add(item.Object);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Helpers.DialogsHelper.Instance.HandleError(DialogsHelper.Errors.NetworkError, null);
            }
            return items;
        }


        // Generate Firebase ID
        public string FirebaseID()
        {
            return FirebaseKeyGenerator.Next();
        }

        public static DatabaseHelper Instance { get; } = new DatabaseHelper();
    }
}
