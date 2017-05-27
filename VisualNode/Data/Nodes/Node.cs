using VisualNode.Util;

namespace VisualNode.Data.Nodes
{
    public enum NodeType
    {
        Enter,
        Exit,
        Dialogue,
        ChangeBackground,
        ChangeScene
    }

    abstract class Node : NotifiableClass
    {
        private bool _waitForInput;
        public bool WaitForInput { get => _waitForInput; set { _waitForInput = value; OnPropertyChanged(); } }
    }
}
