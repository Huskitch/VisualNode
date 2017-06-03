using System.Runtime.Serialization;
using VisualNode.Util;

namespace VisualNode.Data
{
    [DataContract]
    public class Variable : NotifiableClass
    {
        private string _value = "";

        [DataMember(EmitDefaultValue = false)]
        public string Value { get => _value; set { _value = value; OnPropertyChanged(); } }

        private string _name = "New Variable";

        [DataMember(EmitDefaultValue = false)]
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
    }
}
