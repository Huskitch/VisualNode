using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using VisualNode.Data;
using VisualNode.Util;
using VisualNode.ViewModels;

namespace VisualNode.Pages
{
    /// <summary>
    /// Interakční logika pro ScenePage.xaml
    /// </summary>
    public partial class ScenePage : DockPanel
    {
        private MainWindow _mainWindow;

        public string Header { get; set; } = "Scenes";
        public ScenePage(MainWindow mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
        }

        private void OpenSceneTab(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                TabContent page = _mainWindow.Tabs.FirstOrDefault(x => x.Content.GetType() == typeof(SceneDetailPage) && (x.Content as SceneDetailPage).SceneModel.Scene == (SceneListBox.SelectedItem as Scene));
                if (page != null) _mainWindow.tabControl.SelectedItem = page;
                else
                {
                    _mainWindow.Tabs.Add(new TabContent(new SceneDetailPage(_mainWindow) { SceneModel = new SceneDetailViewModel(SceneListBox.SelectedItem as Scene) }));
                    _mainWindow.tabControl.SelectedIndex = _mainWindow.Tabs.Count - 1;
                }
            }
        }
    }
}
