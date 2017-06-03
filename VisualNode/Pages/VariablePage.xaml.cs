using System.Windows.Controls;
using System.Windows.Input;
using VisualNode.Data;
using VisualNode.Util;

namespace VisualNode.Pages
{
    /// <summary>
    /// Interakční logika pro VariablePage.xaml
    /// </summary>
    public partial class VariablePage : DockPanel
    {
        public string Header { get; set; } = "Variables";

        private MainWindow _mainWindow;

        public VariablePage(MainWindow mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
        }

        private void OpenVariableTab(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                _mainWindow.FlyoutContent = new TabContent(new VariableDetailPage() { Variable = VariableListBox.SelectedItem as Variable });
                _mainWindow.Flyout1.IsOpen = true;
            }
        }
    }
}
