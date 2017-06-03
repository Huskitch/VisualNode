using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Input;
using System.Xml;
using VisualNode.Util;

namespace VisualNode.Data
{
    [DataContract(IsReference = true)]
    public class VisualNovel : NotifiableClass
    {
        public string Path { get => _path; set { _path = value; OnPropertyChanged(); } }

        [DataMember]
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }

        // TODO: Missing remove options
        [DataMember]
        public ObservableCollection<Character> Characters { get; set; } = new ObservableCollection<Character>();

        [DataMember]
        public ObservableCollection<Scene> Scenes { get; set; } = new ObservableCollection<Scene>();

        [DataMember]
        public ObservableCollection<Variable> Variables { get; set; } = new ObservableCollection<Variable>();

        [DataMember]
        public ObservableCollection<Background> Backgrounds { get; set; } = new ObservableCollection<Background>();

        private string _path;
        private string _name = "New Project";

        private ICommand _addCharacterCommand;

        public ICommand AddCharacterCommand
        {
            get
            {
                if (_addCharacterCommand == null)
                {
                    _addCharacterCommand = new RelayCommand(param => AddCharacter());
                }
                return _addCharacterCommand;
            }
        }

        private void AddCharacter()
        {
            System.Diagnostics.Debug.WriteLine("yep");
            Characters.Add(new Character());
        }

        private ICommand _saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(param => Save());
                }
                return _saveCommand;
            }
        }

        private void Save()
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(VisualNovel));

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t"
            };

            using (var w = XmlWriter.Create(System.IO.Path.Combine(Path, "novel.vn"), xmlWriterSettings))
            {
                serializer.WriteObject(w, this);
            }
        }

        private ICommand _exportCommand;

        public ICommand ExportCommand
        {
            get
            {
                if (_exportCommand == null)
                {
                    _exportCommand = new RelayCommand(param => Export());
                }
                return _exportCommand;
            }
        }

        private void Export()
        {
            Save();

            string exportDir = System.IO.Path.Combine(Path, "export");
            string contentDir = System.IO.Path.Combine(Path, "export", "content");

            // TODO: FEEL MORE SECURE ABOUT THIS LINE
            //Directory.Delete(exportDir, true);

            Directory.CreateDirectory(exportDir);
            Directory.CreateDirectory(contentDir);

            Directory.CreateDirectory(System.IO.Path.Combine(contentDir, "backgrounds"));
            foreach (var background in Backgrounds)
            {
                if (string.IsNullOrEmpty(background.Image.Path)) continue;

                string extension = System.IO.Path.GetExtension(background.Image.Path);
                File.Copy(background.Image.Path, System.IO.Path.Combine(contentDir, "backgrounds", background.Name + extension));
            }

            Directory.CreateDirectory(System.IO.Path.Combine(contentDir, "characters"));
            foreach (var character in Characters)
            {
                string charDir = System.IO.Path.Combine(contentDir, "characters", character.Name);
                Directory.CreateDirectory(charDir);

                foreach (var pose in character.Poses)
                {
                    if (string.IsNullOrEmpty(pose.Image.Path)) continue;

                    string extension = System.IO.Path.GetExtension(pose.Image.Path);
                    File.Copy(pose.Image.Path, System.IO.Path.Combine(charDir, pose.Name + extension));
                }
            }

            string vnfile = System.IO.Path.Combine(Path, "novel.vn");
            File.Copy(vnfile, System.IO.Path.Combine(exportDir, "novel.vn"));
        }

        private ICommand _addSceneCommand;

        public ICommand AddSceneCommand
        {
            get
            {
                if (_addSceneCommand == null)
                {
                    _addSceneCommand = new RelayCommand(param => AddScene());
                }
                return _addSceneCommand;
            }
        }

        private void AddScene()
        {
            Scenes.Add(new Scene());
        }

        private ICommand _addVariableCommand;

        public ICommand AddVariableCommand
        {
            get
            {
                if (_addVariableCommand == null)
                {
                    _addVariableCommand = new RelayCommand(param => AddVariable());
                }
                return _addVariableCommand;
            }
        }

        private void AddVariable()
        {
            Variables.Add(new Variable());
        }

        private ICommand _addBackgroundCommand;

        public ICommand AddBackgroundCommand
        {
            get
            {
                if (_addBackgroundCommand == null)
                {
                    _addBackgroundCommand = new RelayCommand(param => AddBackground());
                }
                return _addBackgroundCommand;
            }
        }

        private void AddBackground()
        {
            Backgrounds.Add(new Background());
        }

        public static VisualNovel LoadFromFile(string path)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(VisualNovel));
            VisualNovel novel;

            using (FileStream stream = File.OpenRead(path))
            {
                novel = serializer.ReadObject(stream) as VisualNovel;
            }

            novel.Path = System.IO.Path.GetDirectoryName(path);

            return novel;
        }
    }
}
