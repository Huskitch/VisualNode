using VisualNode.Util;

namespace VisualNode.Data
{
    public class Scene : NotifiableClass
    {
        private string _name;

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
    }
}
