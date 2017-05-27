using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VisualNode.Util
{
    public partial class NotifiableClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
