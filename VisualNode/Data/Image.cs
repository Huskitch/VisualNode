using VisualNode.Util;

namespace VisualNode.Data
{
    public class Image : NotifiableClass
    {
        public enum ImageTypeEnum
        {
            Pose,
            Background
        }

        public string Path { get => _path; set { _path = value; OnPropertyChanged(); } }
        public ImageTypeEnum ImageType { get => _imageType; set { _imageType = value; OnPropertyChanged(); } }

        private string _path;
        private ImageTypeEnum _imageType;
    }
}
