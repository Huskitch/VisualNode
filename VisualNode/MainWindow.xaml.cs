using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.Windows;
using VisualNode.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using VisualNode.Util;

namespace VisualNode
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
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

        public ObservableCollection<TabContent> Things { get; set; } = new ObservableCollection<TabContent>();

        public MainWindow()
        {
            InitializeComponent();

            tabControl.DataContext = this;
            DataContext = CurrentProject;

            Things.CollectionChanged += (sender, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add || Things.Count > 0) return;

                Dispatcher.BeginInvoke(new Action<MainWindow>((thing) => {
                    Things.Add(new TabContent("Start", new StartPage()));
                    tabControl.SelectedIndex = 0;
                }),
                this);
            };

            Things.Add(new TabContent("Start", new StartPage()));
            tabControl.SelectedIndex = 0;

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                CurrentProject = VisualNovel.LoadFromFile(args[1]);
                DataContext = CurrentProject;
                foreach (var tab in Things)
                {
                    (tab.Content as DockPanel).DataContext = CurrentProject;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NewProject(object sender, RoutedEventArgs e)
        {
            NewProjectWindow window = new NewProjectWindow();
            window.ShowDialog();

            CurrentProject = window.CurrentProject;
            if (CurrentProject == null) return;

            DataContext = CurrentProject;
            foreach (var tab in Things)
            {
                (tab.Content as Control).DataContext = CurrentProject;
            }
        }

        private void OpenProject(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "VN Project (*.vn)|*.vn|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                CurrentProject = VisualNovel.LoadFromFile(openFileDialog.FileName);
                DataContext = CurrentProject;
                foreach (var tab in Things)
                {
                    (tab.Content as Control).DataContext = CurrentProject;
                }
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void CharacterTabButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentProject == null)
            {
                await this.ShowMessageAsync("Error", "You need to open a project first");
                return;
            }

            Things.Add(new TabContent("Characters", new CharacterPage(this) { DataContext = CurrentProject }));
        }

        private async void SceneTabButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentProject == null)
            {
                await this.ShowMessageAsync("Error", "You need to open a project first");
                return;
            }

            Things.Add(new TabContent("Scenes", new ScenePage() { DataContext = CurrentProject }));
        }

        private async void VariableTabButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentProject == null)
            {
                await this.ShowMessageAsync("Error", "You need to open a project first");
                return;
            }

            Things.Add(new TabContent("Variables", new VariablePage() { DataContext = CurrentProject }));
        }
    }
}
