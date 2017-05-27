using VisualNode.Util;

namespace VisualNode.Data
{
    public class Pose : NotifiableClass
    {
        private string _name = "New Pose";

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
    }
}
