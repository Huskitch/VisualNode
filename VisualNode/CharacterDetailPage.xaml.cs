using MahApps.Metro.Controls;
using System.Windows.Controls;
using VisualNode.Data;

namespace VisualNode
{
    /// <summary>
    /// Interakční logika pro CharacterDetailPage.xaml
    /// </summary>
    public partial class CharacterDetailPage : DockPanel
    {
        public Character Character { get => _character; set { _character = value; DataContext = _character; } }

        private Character _character;

        public CharacterDetailPage()
        {
            InitializeComponent();
        }
    }
}
