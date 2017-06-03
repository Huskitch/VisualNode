using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using VisualNode.Data;
using VisualNode.ViewModels;

namespace VisualNode.Pages
{
    /// <summary>
    /// Interakční logika pro NodeDetailPage.xaml
    /// </summary>
    public partial class NodeDetailPage : DockPanel, INotifyPropertyChanged
    {
        private MainWindow _mainWindow;

        public NodeViewModel Node
        {
            get => _node;
            set
            {
                _node = value;
                DataContext = _node;

                _node.PropertyChanged += (sender, e) =>
                {
                    OnPropertyChanged("");
                };
            }
        }

        private NodeViewModel _node;

        public string Header => Node.Name;

        public NodeDetailPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            TypeComboBox.ItemsSource = Enum.GetValues(typeof(NodeTypeEnum)).Cast<NodeTypeEnum>();
            MovementDirectionComboBox.ItemsSource = Enum.GetValues(typeof(MovementDirectionEnum)).Cast<MovementDirectionEnum>();
            MovementTypeComboBox.ItemsSource = Enum.GetValues(typeof(MovementTypeEnum)).Cast<MovementTypeEnum>();

            CharacterComboBox.ItemsSource = _mainWindow.CurrentProject.Characters;
            PoseComboBox.ItemsSource = (CharacterComboBox.SelectedItem as Character)?.Poses;

            BackgroundComboBox.ItemsSource = _mainWindow.CurrentProject.Backgrounds;
            SceneComboBox.ItemsSource = _mainWindow.CurrentProject.Scenes;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CharacterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Node == null) return;
            PoseComboBox.ItemsSource = (CharacterComboBox.SelectedItem as Character)?.Poses;
        }
    }
}
