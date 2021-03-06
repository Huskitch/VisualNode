﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace VisualNode.Util
{
    [DataContract(IsReference = true)]
    public class NotifiableClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
