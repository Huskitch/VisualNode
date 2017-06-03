using System.Runtime.Serialization;
using VisualNode.Util;

namespace VisualNode.Data
{
    [DataContract]
    public class Image : NotifiableClass
    {
        [DataMember(EmitDefaultValue = false)]
        public string Path { get => _path; set { _path = value; OnPropertyChanged(); } }

        private string _path;
    }
}
