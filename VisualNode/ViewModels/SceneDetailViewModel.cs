using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VisualNode.Data;
using VisualNode.Util;

namespace VisualNode.ViewModels
{
    public class SceneDetailViewModel : NotifiableClass
    {
        public ObservableCollection<NodeViewModel> Nodes { get; } = new ObservableCollection<NodeViewModel>();
        public string Name { get => Scene.Name; set { Scene.Name = value; OnPropertyChanged(); } }

        public Scene Scene { get; }

        public void RemoveNode(Node node)
        {
            Scene.Nodes.Remove(node);
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].Model == node)
                {
                    Nodes.RemoveAt(i);
                    return;
                }
            }

            throw new KeyNotFoundException();
        }

        private ICommand _addNodeCommand;

        public ICommand AddNodeCommand
        {
            get
            {
                if (_addNodeCommand == null)
                {
                    _addNodeCommand = new RelayCommand(param => AddNode());
                }
                return _addNodeCommand;
            }
        }

        private void AddNode()
        {
            Node newNode = new Node();
            Scene.Nodes.Add(newNode);
            Nodes.Add(new NodeViewModel(newNode));
        }

        public SceneDetailViewModel(Scene scene)
        {
            Scene = scene;

            foreach (var node in scene.Nodes)
            {
                Nodes.Add(new NodeViewModel(node));
            }
        }
    }
}
