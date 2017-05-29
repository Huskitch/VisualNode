using VisualNode.Util;

namespace VisualNode.Data
{
    public class Variable : NotifiableClass
    {
        private string _value = "";
        public string Value { get => _value; set { _value = value; OnPropertyChanged(); } }

        private string _name = "New Variable";
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
    }
}
