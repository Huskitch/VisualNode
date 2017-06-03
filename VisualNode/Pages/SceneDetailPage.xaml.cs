using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VisualNode.Data;
using VisualNode.Util;
using VisualNode.ViewModels;

namespace VisualNode.Pages
{
    /// <summary>
    /// Interakční logika pro SceneDetailPage.xaml
    /// </summary>
    public partial class SceneDetailPage : DockPanel, INotifyPropertyChanged
    {
        private MainWindow _mainWindow;

        public SceneDetailViewModel SceneModel {
            get => _scene;
            set {
                _scene = value;
                DataContext = _scene;

                _scene.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "Name") OnPropertyChanged("Header");
                };
            }
        }

        public string Header => SceneModel.Name;

        private SceneDetailViewModel _scene;

        public SceneDetailPage(MainWindow mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;

            BackgroundCombo.DataContext = _mainWindow.CurrentProject;
        }

        public void OpenNodeTab(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                NodeViewModel selectedNode = NodeListBox.SelectedItem as NodeViewModel;
                _mainWindow.FlyoutContent = new TabContent(new NodeDetailPage(_mainWindow) { Node = selectedNode });
                _mainWindow.Flyout1.IsOpen = true;
            }
        }

        private void RemoveNode(object sender, RoutedEventArgs e)
        {
            SceneModel.RemoveNode(NodeListBox.SelectedItem as Node);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
