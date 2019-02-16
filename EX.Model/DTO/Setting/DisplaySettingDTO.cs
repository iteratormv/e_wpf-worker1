using System;
using System.ComponentModel;
using System.Diagnostics;

namespace EX.Model.DTO.Setting
{
    public class DisplaySettingDTO : INotifyPropertyChanged
    {
        int id;
        string name;
        string intendant;
        bool isSelected;

        public int Id { get { return id; } set { id = value; OnPropertyChanged(nameof(Id)); } }
        public string Name { get { return name; } set { name = value; OnPropertyChanged(nameof(Name)); } }
        public string Intendant { get { return intendant; } set { intendant = value; OnPropertyChanged(nameof(Intendant)); } }
        public bool IsSelected { get { return isSelected; } set { isSelected = value; OnPropertyChanged(nameof(IsSelected)); } }



        private void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                throw new ArgumentNullException(GetType().Name + " does not contain property: " + propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
