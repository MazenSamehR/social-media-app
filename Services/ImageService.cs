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

        public async Task<string> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File cannot be null or empty");
            }

            // Use a using statement to ensure the stream is disposed of properly
            using (var stream = file.OpenReadStream())
            {
                // Create an UploadParams object for Cloudinary
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream)
                };

                // Upload the image
                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                // Check if the upload was successful
                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return uploadResult.SecureUrl.AbsoluteUri; // Return the URL of the uploaded image
                }
                else
                {
                    throw new Exception("Image upload failed: " + uploadResult.Error.Message);
                }
            } // Stream is automatically disposed here
        }


    }
}
