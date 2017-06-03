using System.ComponentModel;
using System.Windows.Controls;
using VisualNode.Data;

namespace VisualNode.Pages
{
    /// <summary>
    /// Interakční logika pro VariableDetailPage.xaml
    /// </summary>
    public partial class VariableDetailPage : DockPanel, INotifyPropertyChanged
    {
        public Variable Variable {
            get => _variable;
            set {
                _variable = value;
                DataContext = _variable;

                _variable.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "Name") OnPropertyChanged("Header");
                };
            }
        }

        public string Header => Variable.Name;

        private Variable _variable;

        public VariableDetailPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
