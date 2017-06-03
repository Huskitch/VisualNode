using System.Runtime.Serialization;
using VisualNode.Util;

namespace VisualNode.Data
{
    [DataContract(IsReference = true)]
    public class Background : NotifiableClass
    {
        private string _name = "New Background";
        private Image _image = new Image();

        [DataMember(EmitDefaultValue = false)]
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }

        [DataMember(EmitDefaultValue = false)]
        public Image Image { get => _image; set { _image = value; OnPropertyChanged(); } }
    }
}
