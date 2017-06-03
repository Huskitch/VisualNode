using System.Windows.Controls;
using System.Windows.Input;
using VisualNode.Util;

namespace VisualNode.Pages
{
    /// <summary>
    /// Interakční logika pro BackgroundPage.xaml
    /// </summary>
    public partial class BackgroundPage : DockPanel
    {
        public string Header { get; set; } = "Backgrounds";

        private MainWindow _mainWindow;

        public BackgroundPage(MainWindow mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
        }

        private void OpenBackgroundTab(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                _mainWindow.FlyoutContent = new TabContent(new BackgroundDetailPage() { Background = BackgroundListBox.SelectedItem as Data.Background });
                _mainWindow.Flyout1.IsOpen = true;
            }
        }
    }
}
