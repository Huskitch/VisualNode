using System.Windows.Controls;

namespace VisualNode.Pages
{
    /// <summary>
    /// Interakční logika pro StartPage.xaml
    /// </summary>
    public partial class StartPage : DockPanel
    {
        public string Header { get; set; } = "Start";

        public StartPage()
        {
            InitializeComponent();
        }
    }
}
