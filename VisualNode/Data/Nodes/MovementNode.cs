using System;
using System.Xml;

namespace VisualNode.Data.Nodes
{
    class MovementNode : Node
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

        private Character _character;
        private Pose _entrancePose;
        private MovementTypeEnum _movementType;
        private MovementDirectionEnum _movementDirection;

        public Character Character { get => _character; set { _character = value; OnPropertyChanged(); } }
        public MovementDirectionEnum MovementDirection { get => _movementDirection; set { _movementDirection = value; OnPropertyChanged(); } }
        public MovementTypeEnum MovementType { get => _movementType; set { _movementType = value; OnPropertyChanged(); } }
        public Pose EntrancePose { get => _entrancePose; set { _entrancePose = value; OnPropertyChanged(); } }

        public override void Serialize(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
