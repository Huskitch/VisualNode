using System;
using System.Xml;
using VisualNode.Util;

namespace VisualNode.Data.Nodes
{
    class ChangeSceneNode : Node
    {
        public Scene Scene { get => _scene; set { _scene = value; OnPropertyChanged(); } }
        public Variable Variable { get => _variable; set { _variable = value; OnPropertyChanged(); } }
        public string Condition { get => _condition; set { _condition = value; OnPropertyChanged(); } }

        private Scene _scene;
        private Variable _variable;
        private string _condition;

        public override void Serialize(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
