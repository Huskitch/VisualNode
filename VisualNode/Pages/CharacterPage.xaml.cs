using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VisualNode.Data;
using VisualNode.Util;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;

namespace VisualNode.Pages
{
    /// <summary>
    /// Interakční logika pro CharacterPage.xaml
    /// </summary>
    public partial class CharacterPage : DockPanel
    {
        public string Header { get; set; } = "Characters";

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
                TabContent page = _mainWindow.Tabs.FirstOrDefault(x => x.Content.GetType() == typeof(CharacterDetailPage) && (x.Content as CharacterDetailPage).Character == (CharacterListBox.SelectedItem as Character));
                if (page != null) _mainWindow.tabControl.SelectedItem = page;
                else
                {
                    _mainWindow.Tabs.Add(new TabContent(new CharacterDetailPage(_mainWindow) { Character = CharacterListBox.SelectedItem as Character }));
                    _mainWindow.tabControl.SelectedIndex = _mainWindow.Tabs.Count - 1;
                }
            }
        }

        private async void RemoveCharacter(object sender, RoutedEventArgs e)
        {
            Character target = CharacterListBox.SelectedItem as Character;
            var result = await _mainWindow.ShowMessageAsync("Warning", $"Are you sure you want to remove '{target.Name}'?", MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Negative) return;

            _mainWindow.CurrentProject.Characters.Remove(target);

            List<TabContent> content = new List<TabContent>();

            for (int i = 0; i < _mainWindow.Tabs.Count; i++)
            {
                if (_mainWindow.Tabs[i].Content is CharacterDetailPage detail)
                {
                    if (detail.Character == target) content.Add(_mainWindow.Tabs[i]);
                }
            }

            foreach (var item in content)
            {
                _mainWindow.Tabs.Remove(item);
            }
        }
    }
}
