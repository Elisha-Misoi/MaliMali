using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.ImageEdit;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace MaliMali.Helpers
{
    // This class uses MediaPlugin https://github.com/jamesmontemagno/MediaPlugin
    // &
    // ImageEdit plugin https://github.com/muak/Xamarin.Plugin.ImageEdit
    public class FilePicker
    {
        public async Task<string> TakePhotoAndSave()
        {
            string path = null;
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                Helpers.DialogsHelper.Instance.HandleError(DialogsHelper.Errors.Defined, "Failed to access camera.");
            }
            else
            {
                // Take photo and allow for cropping to square
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "CarRent",
                    SaveToAlbum = false,
                    CompressionQuality = 100,
                    CustomPhotoSize = 100,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2000,
                    DefaultCamera = CameraDevice.Rear,
                    AllowCropping = true,
                    RotateImage = true,
                    ModalPresentationStyle = MediaPickerModalPresentationStyle.OverFullScreen
                });

                if (file != null)
                {
                    var stream = file.GetStream();
                    var image = await CrossImageEdit.Current.CreateImageAsync(stream);
                    image.Rotate(-90);
                    var jpgBytes = image.ToJpeg(100);

                    var filename = DateTime.Now.ToString().Replace(" ", string.Empty).Replace("/", string.Empty);
                    path = Helpers.FileUtility.Instance.TemporarilySaveFile(filename, jpgBytes);
                    file.Dispose();
                }
            }
            return path;
        }


        private byte[] GetImageStreamAsBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public async Task<string> PickPhotoAndSave()
        {
            // Pick photo and crop it to square
            string path = null;
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                var action = await Application.Current.MainPage.DisplayAlert("Gallery Permissions", "MaliMali", "Yes", "No");
                if (action == true)
                {
                    // access settings
                }
            }
            else
            {
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                    CompressionQuality = 100,
                    RotateImage = false,
                    ModalPresentationStyle = MediaPickerModalPresentationStyle.OverFullScreen,
                    CustomPhotoSize = 100
                });

                if (file != null)
                {
                    var stream = file.GetStream();
                    var image = await CrossImageEdit.Current.CreateImageAsync(stream);
                    image.Crop(0, 150, 800, 800);
                    var jpgBytes = image.ToJpeg(100);

                    var filename = DateTime.Now.ToString().Replace(" ", string.Empty).Replace("/", string.Empty);
                    path = Helpers.FileUtility.Instance.TemporarilySaveFile(filename, jpgBytes);
                    file.Dispose();
                }
            }
            return path;
        }

        public static FilePicker Instance { get; } = new FilePicker();
    }
}
