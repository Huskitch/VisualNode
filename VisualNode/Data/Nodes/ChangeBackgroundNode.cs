using System;
using System.Xml;

namespace VisualNode.Data.Nodes
{
    class ChangeBackgroundNode : Node
    {
        public Background Background { get => _background; set { _background = value; OnPropertyChanged(); } }
        private Background _background;

        public override void Serialize(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
