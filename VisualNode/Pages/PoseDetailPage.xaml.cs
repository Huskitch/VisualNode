using Microsoft.Win32;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Controls;
using VisualNode.Data;

namespace VisualNode.Pages
{
    /// <summary>
    /// Interakční logika pro PoseDetailPage.xaml
    /// </summary>
    public partial class PoseDetailPage : DockPanel, INotifyPropertyChanged
    {
        public Pose Pose {
            get => _pose;
            set {
                _pose = value;
                DataContext = _pose;

                _pose.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "Name") OnPropertyChanged("Header"); UpdateLayout();
                };
            }
        }

        public string Header => Pose.Name;

        private Pose _pose;

        public PoseDetailPage()
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

            if (dialog.ShowDialog() == true) Pose.Image.Path = dialog.FileName;
        }
    }
}
