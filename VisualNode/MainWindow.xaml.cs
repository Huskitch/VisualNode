using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.Windows;
using VisualNode.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System;
using MahApps.Metro.Controls.Dialogs;
using VisualNode.Util;
using VisualNode.Pages;
using System.Linq;

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

        public ObservableCollection<TabContent> Tabs { get; set; } = new ObservableCollection<TabContent>();

        // TODO: Maybe make it slide in before changing content
        public TabContent FlyoutContent { get => _flyoutContent; set { _flyoutContent = value; OnPropertyChanged(); } }
        private TabContent _flyoutContent;

        public MainWindow()
        {
            InitializeComponent();

            tabControl.DataContext = this;
            Flyout1.DataContext = this;
            DataContext = CurrentProject;

            Tabs.CollectionChanged += (sender, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add || Tabs.Count > 0) return;

                Dispatcher.BeginInvoke(new Action<MainWindow>((thing) => {
                    Tabs.Add(new TabContent(new StartPage() { DataContext = CurrentProject }));
                    tabControl.SelectedIndex = 0;
                }),
                this);
            };

            Tabs.Add(new TabContent(new StartPage() { DataContext = CurrentProject }));
            tabControl.SelectedIndex = 0;

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                CurrentProject = VisualNovel.LoadFromFile(args[1]);
                DataContext = CurrentProject;
                foreach (var tab in Tabs)
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

            if (window.CurrentProject != null) CurrentProject = window.CurrentProject;
            if (CurrentProject == null) return;

            DataContext = CurrentProject;
            foreach (var tab in Tabs)
            {
                (tab.Content as DockPanel).DataContext = CurrentProject;
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
                foreach (var tab in Tabs)
                {
                    (tab.Content as DockPanel).DataContext = CurrentProject;
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

            TabContent page = Tabs.FirstOrDefault(x => x.Content.GetType() == typeof(CharacterPage));
            if (page != null) tabControl.SelectedItem = page;
            else
            {
                Tabs.Add(new TabContent(new CharacterPage(this) { DataContext = CurrentProject }));
                tabControl.SelectedIndex = Tabs.Count - 1;
            }
        }

        private async void SceneTabButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentProject == null)
            {
                await this.ShowMessageAsync("Error", "You need to open a project first");
                return;
            }

            TabContent page = Tabs.FirstOrDefault(x => x.Content.GetType() == typeof(ScenePage));
            if (page != null) tabControl.SelectedItem = page;
            else
            {
                Tabs.Add(new TabContent(new ScenePage(this) { DataContext = CurrentProject }));
                tabControl.SelectedIndex = Tabs.Count - 1;
            }
        }

        private async void VariableTabButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentProject == null)
            {
                await this.ShowMessageAsync("Error", "You need to open a project first");
                return;
            }

            TabContent page = Tabs.FirstOrDefault(x => x.Content.GetType() == typeof(VariablePage));
            if (page != null) tabControl.SelectedItem = page;
            else
            {
                Tabs.Add(new TabContent(new VariablePage(this) { DataContext = CurrentProject }));
                tabControl.SelectedIndex = Tabs.Count - 1;
            }
        }

        private async void BackgroundTabButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentProject == null)
            {
                await this.ShowMessageAsync("Error", "You need to open a project first");
                return;
            }

            TabContent page = Tabs.FirstOrDefault(x => x.Content.GetType() == typeof(BackgroundPage));
            if (page != null) tabControl.SelectedItem = page;
            else
            {
                Tabs.Add(new TabContent(new BackgroundPage(this) { DataContext = CurrentProject }));
                tabControl.SelectedIndex = Tabs.Count - 1;
            }
        }
    }
}
