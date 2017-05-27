using System.Collections.ObjectModel;
using VisualNode.Util;

namespace VisualNode.Data
{
    public class Character : NotifiableClass
    {
        private string _name = "New Character";

        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        public ObservableCollection<Pose> Poses { get; set; } = new ObservableCollection<Pose>();

        public Character()
        {
            Poses.CollectionChanged += (sender, e) => OnPropertyChanged("Poses");

            for (int i = 0; i < 4; i++)
            {
                Poses.Add(new Pose());
            }
        }
    }
}
