using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using System.Xml;
using VisualNode.Util;

namespace VisualNode.Data
{
    public class VisualNovel : NotifiableClass
    {
        public string Path
        {
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
        public ObservableCollection<Variable> Variables { get; set; } = new ObservableCollection<Variable>();

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
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };

            using (XmlWriter writer = XmlWriter.Create(System.IO.Path.Combine(Path, "novel.vn"), xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Novel");
                writer.WriteAttributeString("name", Name);

                writer.WriteStartElement("Characters");

                for (int i = 0; i < Characters.Count; i++)
                {
                    writer.WriteStartElement("Character");
                    writer.WriteAttributeString("id", i.ToString());
                    writer.WriteAttributeString("name", Characters[i]?.Name);

                    writer.WriteStartElement("Poses");
                    for (int j = 0; j < Characters[i].Poses.Count; j++)
                    {
                        writer.WriteStartElement("Pose");
                        writer.WriteAttributeString("id", j.ToString());
                        writer.WriteAttributeString("name", Characters[i].Poses[j].Name);
                        writer.WriteString(Characters[i].Poses[j].Image?.Path ?? "");
                        writer.WriteEndElement();
                    };
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.WriteStartElement("Scenes");
                for (int i = 0; i < Scenes.Count; i++)
                {
                    writer.WriteStartElement("Scene");
                    writer.WriteAttributeString("id", i.ToString());
                    writer.WriteAttributeString("name", Scenes[i]?.Name);

                    writer.WriteStartElement("Nodes");
                    for (int j = 0; j < Scenes[i]?.Nodes?.Count; j++)
                    {
                        Scenes[i]?.Nodes[j]?.Serialize(writer);
                    };
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteEndDocument();
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

        public static VisualNovel LoadFromFile(string path)
        {
            return new VisualNovel()
            {
                Path = System.IO.Path.GetDirectoryName(path)
            };
        }
    }
}
