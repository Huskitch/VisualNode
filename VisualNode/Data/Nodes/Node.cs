using System.Xml;
using VisualNode.Util;

namespace VisualNode.Data.Nodes
{
    public abstract class Node : NotifiableClass
    {
        private bool _waitForInput;
        public bool WaitForInput { get => _waitForInput; set { _waitForInput = value; OnPropertyChanged(); } }

        public abstract void Serialize(XmlWriter writer);
    }
}
