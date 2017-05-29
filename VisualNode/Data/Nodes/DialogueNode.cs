using System;
using System.Xml;

namespace VisualNode.Data.Nodes
{
    class DialogueNode : Node
    {
        public string DialogueText { get => _dialogueText; set { _dialogueText = value; OnPropertyChanged(); } }
        public Character Character { get => _character; set { _character = value; OnPropertyChanged(); } }

        private string _dialogueText;
        private Character _character;

        public override void Serialize(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
