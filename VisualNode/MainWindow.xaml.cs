using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.Windows;
using VisualNode.Data;

namespace VisualNode
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public VisualNovel CurrentProject { get; set; }
        public bool IsProjectLoaded { get { return CurrentProject != null; } }
        public string ProjectTitle
        {
            get
            {
                if (IsProjectLoaded) return string.Format("VisualNode - {0}", CurrentProject.Name);
                else return "VisualNode";
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = CurrentProject;
        }

        private void NewProject(object sender, RoutedEventArgs e)
        {
            CurrentProject = new VisualNovel();
            DataContext = CurrentProject;

            CurrentProject.Characters.Add(new Character());
        }

        private void OpenProject(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "VN Project (*.vnp)|*.vnp|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                CurrentProject = VisualNovel.LoadFromFile(openFileDialog.FileName);
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
