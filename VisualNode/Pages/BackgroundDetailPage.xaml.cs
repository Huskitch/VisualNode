using Microsoft.Win32;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Controls;
using VisualNode.Data;

namespace VisualNode.Pages
{
    /// <summary>
    /// Interakční logika pro BackgroundDetailPage.xaml
    /// </summary>
    public partial class BackgroundDetailPage : DockPanel, INotifyPropertyChanged
    {
        new public Background Background
        {
            get => _background;
            set {
                _background = value;
                DataContext = _background;

                _background.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "Name") OnPropertyChanged("Header");
                };
            }
        }

        public string Header => Background.Name;

        private Background _background;

        public BackgroundDetailPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BrowseImage(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            var imageExtensions = string.Join(";", ImageCodecInfo.GetImageDecoders().Select(ici => ici.FilenameExtension));
            dialog.Filter = string.Format("Images|{0}|All Files|*.*", imageExtensions);

            if (dialog.ShowDialog() == true) Background.Image.Path = dialog.FileName;
        }
    }
}
