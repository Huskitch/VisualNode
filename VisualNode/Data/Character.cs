using System.Collections.ObjectModel;
using System.Windows.Input;
using VisualNode.Util;

namespace VisualNode.Data
{
    public class Character : NotifiableClass
    {
        private string _name = "New Character";

        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        public ObservableCollection<Pose> Poses { get; set; } = new ObservableCollection<Pose>();

        private ICommand _addPoseCommand;

        public ICommand AddPoseCommand
        {
            get
            {
                if (_addPoseCommand == null)
                {
                    _addPoseCommand = new RelayCommand(param => AddPose());
                }
                return _addPoseCommand;
            }
        }

        private void AddPose()
        {
            Poses.Add(new Pose());
        }
    }
}
