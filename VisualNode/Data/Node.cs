using System.Xml;
using System.Runtime.Serialization;

namespace VisualNode.Data
{
    public enum MovementTypeEnum
    {
        Enter,
        Exit
    }

    public enum MovementDirectionEnum
    {
        Left,
        Right
    }

    public enum NodeTypeEnum
    {
        Empty,
        Dialogue,
        ChangeBackground,
        ChangeScene,
        Movement,
        SetVariable,
        Option
    }

    [DataContract(IsReference = true)]
    public class Node
    {
        // Generic
        [DataMember(EmitDefaultValue = false)]
        public bool WaitForInput = false;

        [DataMember(EmitDefaultValue = false)]
        public string Note;

        [DataMember(EmitDefaultValue = false)]
        public NodeTypeEnum NodeType = NodeTypeEnum.Empty;

        // Dialogue
        [DataMember(EmitDefaultValue = false)]
        public string Dialogue;

        // Condition
        [DataMember(EmitDefaultValue = false)]
        public Variable Variable;
        [DataMember(EmitDefaultValue = false)]
        public string Condition;

        // Jump
        [DataMember(EmitDefaultValue = false)]
        public Node TargetNode;
        [DataMember(EmitDefaultValue = false)]
        public Scene Scene;

        // Character
        [DataMember(EmitDefaultValue = false)]
        public Character Character;
        [DataMember(EmitDefaultValue = false)]
        public Pose Pose;

        // Movement
        [DataMember(EmitDefaultValue = false)]
        public MovementDirectionEnum MovementDirection;
        [DataMember(EmitDefaultValue = false)]
        public MovementTypeEnum MovementType;

        //Background
        [DataMember(EmitDefaultValue = false)]
        public Background Background;

        public void Serialize(XmlWriter writer, VisualNovel currentProject)
        {
            writer.WriteAttributeString("type", NodeType.ToString());
            writer.WriteAttributeString("note", Note);
            writer.WriteAttributeString("wait", WaitForInput.ToString());

            switch (NodeType)
            {
                case NodeTypeEnum.Empty:
                    break;
                case NodeTypeEnum.Dialogue:
                    writer.WriteAttributeString("text", Dialogue);
                    writer.WriteAttributeString("character", currentProject.Characters.IndexOf(Character).ToString());
                    break;
                case NodeTypeEnum.ChangeBackground:
                    writer.WriteAttributeString("background", currentProject.Backgrounds.IndexOf(Background).ToString());
                    break;
                case NodeTypeEnum.ChangeScene:
                    writer.WriteAttributeString("scene", currentProject.Scenes.IndexOf(Scene).ToString());
                    break;
                case NodeTypeEnum.Movement:
                    writer.WriteAttributeString("direction", MovementDirection.ToString());
                    writer.WriteAttributeString("movetype", MovementType.ToString());
                    writer.WriteAttributeString("character", currentProject.Characters.IndexOf(Character).ToString());
                    break;
                case NodeTypeEnum.SetVariable:
                    break;
                case NodeTypeEnum.Option:
                    break;
                default:
                    break;
            }
        }
    }
}
