using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace EX.Model.DTO.Setting
{
    [DataContract]
    public class DSCollumnSettingDTO : INotifyPropertyChanged
    {
        [DataMember]
        int id;
        [DataMember]
        string name;
        [DataMember]
        string alias;
        [DataMember]
        bool visible;
        [DataMember]
        int width;
        [DataMember]
        bool isSelected;
        [DataMember]
        string intendant;
        [DataMember]
        int displaySettingId;
        [DataMember]
        bool isRequiredInput;

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        [DataMember]
        public string Alias
        {
            get { return alias; }
            set { alias = value;
                OnPropertyChanged(nameof(Alias));
            }
        }
        [DataMember]
        public bool Visible
        {
            get { return visible; }
            set { visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }
        [DataMember]
        public int Width
        {
            get { return width; }
            set { width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
        [DataMember]
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        [DataMember]
        public string Intendant
        {
            get { return intendant; }
            set { intendant = value;
                OnPropertyChanged(nameof(Intendant));
            }
        }
        [DataMember]
        public int DisplaySettingId
        {
            get { return displaySettingId; }
            set { displaySettingId = value;
                OnPropertyChanged(nameof(DisplaySettingId));
            }
        }
        [DataMember]
        public bool IsRequiredInput
        {
            get { return isRequiredInput; }
            set
            {
                isRequiredInput = value;
                OnPropertyChanged(nameof(IsRequiredInput));
            }
        }

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
