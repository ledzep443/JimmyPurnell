
namespace Shared.Models
{
    public class UploadedImage
    {
        public string NewImageFileExtension { get; set; }
        // Base64 to represent image binary data
        public string NewImageBase64Content { get; set; }
        public string OldImagePath { get; set; }
    }
}
