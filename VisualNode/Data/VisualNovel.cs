using System.Collections.ObjectModel;
using System.Windows.Input;
using VisualNode.Util;

namespace VisualNode.Data
{
    public class VisualNovel : NotifiableClass
    {
        public string Path {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Character> Characters { get; set; } = new ObservableCollection<Character>();
        public ObservableCollection<Scene> Scenes { get; set; } = new ObservableCollection<Scene>();

        private string _path;
        private string _name = "New Project";

        public void Export()
        {
        }

        public void Save()
        {
        }

        private ICommand _addCharacterCommand;

        public ICommand AddCharacterCommand
        {
            get
            {
                if (_addCharacterCommand == null)
                {
                    _addCharacterCommand = new RelayCommand(
                        param => AddCharacter(),
                        param => true
                    );
                }
                return _addCharacterCommand;
            }
        }

        private void AddCharacter()
        {
            Characters.Add(new Character());
        }

        public VisualNovel()
        {
            Characters.CollectionChanged += (sender, e) => OnPropertyChanged("Characters");
            Scenes.CollectionChanged += (sender, e) => OnPropertyChanged("Scenes");
        }

        public static VisualNovel LoadFromFile(string path)
        {
            return new VisualNovel();
        }
    }
}
