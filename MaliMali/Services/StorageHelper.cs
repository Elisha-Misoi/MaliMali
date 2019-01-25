using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Firebase.Storage;
using MaliMali.Helpers;

namespace MaliMali.Services
{
    public class StorageHelper
    {
        //Firebase Storage Client
        private readonly FirebaseStorage firebaseStorage = new FirebaseStorage("malimali.appspot.com");

        public async Task<string> UploadTempFile(string Filename, string Bucket, string Media_Path)
        {
            string download_url = "";
            try
            {
                Stream stream = new MemoryStream(GetFileBytes(Media_Path));

                var upload = await firebaseStorage
                    .Child(Bucket + "/" + Filename)
                    .PutAsync(stream);

                //task.Progress.ProgressChanged += (s, f) => UploadProgress(f.Percentage);
                download_url = upload;
                Debug.WriteLine("Download_Url: " + download_url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Helpers.DialogsHelper.Instance.HandleError(DialogsHelper.Errors.NetworkError, null);
            }
            return download_url;
        }

        private byte[] GetFileBytes(string media_path)
        {
            byte[] fileBytes = null;

            using (var streamReader = new StreamReader(media_path))
            {
                using (var memstream = new MemoryStream())
                {
                    streamReader.BaseStream.CopyTo(memstream);
                    fileBytes = memstream.ToArray();
                }
            }

            return fileBytes;
        }

        public async Task<string> UploadFile(string Filename, string Bucket, string Folder, string Media_Name)
        {
            string download_url = "";
            try
            {
                string media_path = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), Folder, Media_Name);

                Stream stream = new MemoryStream(GetFileBytes(media_path));

                var upload = await firebaseStorage
                    .Child(Bucket + "/" + Filename)
                    .PutAsync(stream);

                //task.Progress.ProgressChanged += (s, f) => UploadProgress(f.Percentage);
                download_url = upload;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Helpers.DialogsHelper.Instance.HandleError(DialogsHelper.Errors.NetworkError, null);
            }
            return download_url;
        }

        public static StorageHelper Instance { get; } = new StorageHelper();
    }
}
