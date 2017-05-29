using MahApps.Metro.Controls;
using WinForms = System.Windows.Forms;
using VisualNode.Data;
using System.IO;
using System.ComponentModel;
using System;

namespace VisualNode
{
    /// <summary>
    /// Interakční logika pro NewProjectWindow.xaml
    /// </summary>
    public partial class NewProjectWindow : MetroWindow, INotifyPropertyChanged
    {
        public VisualNovel CurrentProject;
        public bool IsValidPath
        {
            get
            {
                return Directory.Exists(projectPath.Text) && !Directory.Exists(Path.Combine(projectPath.Text, projectName.Text));
            }
        }

        public NewProjectWindow()
        {
            InitializeComponent();
            DataContext = this;

            projectPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void Browse(object sender, System.Windows.RoutedEventArgs e)
        {
            using (var folderDialog = new WinForms.FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == WinForms.DialogResult.OK)
                {
                    projectPath.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void Cancel(object sender, System.Windows.RoutedEventArgs e)
        {
            CurrentProject = null;
            Close();
        }

        private void Create(object sender, System.Windows.RoutedEventArgs e)
        {
            CurrentProject = new VisualNovel()
            {
                Name = projectName.Text,
                Path = Path.Combine(projectPath.Text, projectName.Text)
            };

            Directory.CreateDirectory(Path.Combine(projectPath.Text, projectName.Text));
            File.Create(Path.Combine(projectPath.Text, projectName.Text, "novel.vn"));

            Close();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            OnPropertyChanged("IsValidPath");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void projectName_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (projectName.Text == "Project Name") projectName.Text = "";
        }

        private void projectName_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (projectName.Text == "") projectName.Text = "Project Name";
        }
    }
}
