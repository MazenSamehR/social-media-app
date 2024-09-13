using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace SocailMediaApp.Services
{
    public class ImageService
    {
        private Cloudinary cloudinary;
        public ImageService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public string UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File cannot be null or empty");
            }

            var stream = file.OpenReadStream();

            // Create an UploadParams object for Cloudinary
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream)
            };

            var uploadResult = cloudinary.Upload(uploadParams);

            // Dispose of the stream after Cloudinary is done with it
            stream.Dispose();

            // Check if the upload was successful
            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.AbsoluteUri; // Return the URL of the uploaded image
            }
            else
            {
                throw new Exception("Image upload failed: " + uploadResult.Error.Message);
            }
        }

    }
}
