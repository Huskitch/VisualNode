using MahApps.Metro.Controls;
using System.Windows.Controls;
using System.Windows.Input;
using VisualNode.Data;
using VisualNode.Util;

namespace VisualNode
{
    /// <summary>
    /// Interakční logika pro CharacterPage.xaml
    /// </summary>
    public partial class CharacterPage : DockPanel
    {
        private MainWindow _mainWindow;

        public CharacterPage(MainWindow mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
        }

        private void OpenCharacterTab(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                _mainWindow.Things.Add(new TabContent((CharacterListBox.SelectedItem as Character).Name, new CharacterDetailPage() { Character = CharacterListBox.SelectedItem as Character }));
                _mainWindow.tabControl.SelectedIndex = _mainWindow.Things.Count - 1;
            }
        }
    }
}
