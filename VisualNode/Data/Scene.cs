using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using VisualNode.Util;

namespace VisualNode.Data
{
    [DataContract(IsReference = true)]
    public class Scene : NotifiableClass
    {
        private string _name = "New Scene";
        private Background _background;

        [DataMember(EmitDefaultValue = false)]
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }

        [DataMember(EmitDefaultValue = false)]
        public Background Background { get => _background; set { _background = value; OnPropertyChanged(); } }

        [DataMember(EmitDefaultValue = false)]
        public ObservableCollection<Node> Nodes { get; set; } = new ObservableCollection<Node>();
    }
}
