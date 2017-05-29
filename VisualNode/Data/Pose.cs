using VisualNode.Util;

namespace VisualNode.Data
{
    public class Pose : NotifiableClass
    {
        private string _name = "New Pose";
        private Image _image;

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public Image Image { get => _image; set { _image = value; OnPropertyChanged(); } }
    }
}
