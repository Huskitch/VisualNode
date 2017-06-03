using System.Windows.Media;
using VisualNode.Data;
using VisualNode.Util;

namespace VisualNode.ViewModels
{
    public class NodeViewModel : NotifiableClass
    {
        public Node Model { get; }

        public string Name
        {
            get
            {
                switch (NodeType)
                {
                    case NodeTypeEnum.Empty:
                        return "Empty Node";
                    case NodeTypeEnum.Dialogue:
                        return "Dialogue";
                    case NodeTypeEnum.ChangeBackground:
                        return "Background Change";
                    case NodeTypeEnum.ChangeScene:
                        return "Scene Change";
                    case NodeTypeEnum.Movement:
                        return "Character Movement";
                    case NodeTypeEnum.SetVariable:
                        return "Set Variable";
                    case NodeTypeEnum.Option:
                        return "Option";
                    default:
                        return "Invalid Node!";
                }
            }
        }
        public string Note { get => Model.Note; set { Model.Note = value; OnPropertyChanged(); } }
        public NodeTypeEnum NodeType
        {
            get => Model.NodeType;
            set
            {
                Model.NodeType = value;
                OnPropertyChanged();
                OnPropertyChanged("Color");
                OnPropertyChanged("Name");
                OnPropertyChanged("HasBackgroundTab");
                OnPropertyChanged("HasSceneTab");
                OnPropertyChanged("HasCharacterTab");
                OnPropertyChanged("HasMovementTab");
                OnPropertyChanged("HasDialogueTab");
            }
        }
        public bool WaitForInput { get => Model.WaitForInput; set { Model.WaitForInput = value; OnPropertyChanged(); } }
        public Brush Color
        {
            get
            {
                switch (NodeType)
                {
                    case NodeTypeEnum.Empty:
                        return Brushes.Orange;
                    case NodeTypeEnum.Dialogue:
                        return Brushes.Lime;
                    case NodeTypeEnum.ChangeBackground:
                        return Brushes.Purple;
                    case NodeTypeEnum.ChangeScene:
                        return Brushes.Cyan;
                    case NodeTypeEnum.Movement:
                        return Brushes.Wheat;
                    case NodeTypeEnum.SetVariable:
                        return Brushes.White;
                    case NodeTypeEnum.Option:
                        return Brushes.HotPink;
                    default:
                        return Brushes.Red;
                }
            }
        }

        public bool HasBackgroundTab => Model.NodeType == NodeTypeEnum.ChangeBackground;
        public Background Background { get => Model.Background; set { Model.Background = value; OnPropertyChanged(); } }

        public bool HasSceneTab => Model.NodeType == NodeTypeEnum.ChangeScene;
        public Scene Scene { get => Model.Scene; set { Model.Scene = value; OnPropertyChanged(); } }

        public bool HasMovementTab => Model.NodeType == NodeTypeEnum.Movement;
        public MovementTypeEnum MovementType { get => Model.MovementType; set { Model.MovementType = value; OnPropertyChanged(); } }
        public MovementDirectionEnum MovementDirection { get => Model.MovementDirection; set { Model.MovementDirection = value; OnPropertyChanged(); } }

        public bool HasCharacterTab => Model.NodeType == NodeTypeEnum.Dialogue || Model.NodeType == NodeTypeEnum.Movement;
        public Character Character { get => Model.Character; set { Model.Character = value; OnPropertyChanged(); } }
        public Pose Pose { get => Model.Pose; set { Model.Pose = value; OnPropertyChanged(); } }

        public bool HasDialogueTab => Model.NodeType == NodeTypeEnum.Dialogue;
        public string DialogueText { get => Model.Dialogue; set { Model.Dialogue = value; OnPropertyChanged(); } }

        public NodeViewModel(Node node)
        {
            Model = node;
        }
    }
}
