using System;
namespace MaliMali.Helpers
{
    public class FileUtility
    {
        public string SaveFile(string folder, string fileName, byte[] imageStream)
        {
            string path = null;
            string folderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), folder);

            // Check if the folder exist or not
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
            string imagefilePath = System.IO.Path.Combine(folderPath, fileName);

            // Try to write the file bytes to the specified location.
            try
            {
                System.IO.File.WriteAllBytes(imagefilePath, imageStream);
                path = imagefilePath;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return path;
        }

        public string TemporarilySaveFile(string fileName, byte[] imageStream)
        {
            string path = null;
            var tmpdir = System.IO.Path.GetTempPath();
            string folderPath = tmpdir;

            // Check if the folder exist or not
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
            string imagefilePath = System.IO.Path.Combine(folderPath, fileName);

            // Try to write the file bytes to the specified location.
            try
            {
                System.IO.File.WriteAllBytes(imagefilePath, imageStream);
                path = imagefilePath;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return path;
        }


        public void DeleteDirectory()
        {
            string imageFolderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Images");
            if (System.IO.Directory.Exists(imageFolderPath))
            {
                System.IO.Directory.Delete(imageFolderPath, true);
            }
        }

        public static FileUtility Instance { get; } = new FileUtility();
    }
}
