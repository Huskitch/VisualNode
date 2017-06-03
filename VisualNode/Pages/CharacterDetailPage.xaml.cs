using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VisualNode.Data;
using VisualNode.Util;

namespace VisualNode.Pages
{
    /// <summary>
    /// Interakční logika pro CharacterDetailPage.xaml
    /// </summary>
    public partial class CharacterDetailPage : DockPanel, INotifyPropertyChanged
    {
        private MainWindow _mainWindow;

        public Character Character {
            get => _character;
            set {
                _character = value;
                DataContext = _character;

                _character.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "Name") OnPropertyChanged("Header");
                };
            }
        }

        public string Header => Character.Name;

        private Character _character;

        public CharacterDetailPage(MainWindow mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
        }

        public void OpenPoseTab(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                _mainWindow.FlyoutContent = new TabContent(new PoseDetailPage() { Pose = PoseListBox.SelectedItem as Pose });
                _mainWindow.Flyout1.IsOpen = true;
            }
        }

        private void RemovePose(object sender, RoutedEventArgs e)
        {
            Character.Poses.Remove(PoseListBox.SelectedItem as Pose);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
