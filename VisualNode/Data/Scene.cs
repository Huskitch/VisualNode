using System.Collections.ObjectModel;
using VisualNode.Data.Nodes;
using VisualNode.Util;

namespace VisualNode.Data
{
    public class Scene : NotifiableClass
    {
        private string _name = "New Scene";
        private Background _background;

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public Background Background { get => _background; set { _background = value; OnPropertyChanged(); } }
        public ObservableCollection<Node> Nodes { get; set; } = new ObservableCollection<Node>();

        public Scene()
        {
            Nodes.CollectionChanged += (sender, e) => OnPropertyChanged("Nodes");
        }
    }
}
