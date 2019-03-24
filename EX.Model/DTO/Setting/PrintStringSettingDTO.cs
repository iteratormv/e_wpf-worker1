using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX.Model.DTO.Setting
{
    class PrintStringSettingDTO : INotifyPropertyChanged
    {
        int id;
        string name;
        string fontName;
        string fontStyle;
        int fontSize;
        bool visible;
        bool toUpper;
        int printSettingId;

        public int Id {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string FontName
        {
            get { return fontName; }
            set
            {
                fontName = value;
                OnPropertyChanged(nameof(FontName));
            }
        }
        public string FontStyle
        {
            get { return fontStyle; }
            set
            {
                fontStyle = value;
                OnPropertyChanged(nameof(FontStyle));
            }
        }
        public int FontSize
        {
            get { return fontSize; }
            set
            {
                fontSize = value;
                OnPropertyChanged(nameof(FontSize));
            }
        }
        public bool Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }
        public bool ToUpper
        {
            get { return toUpper; }
            set
            {
                toUpper = value;
                OnPropertyChanged(nameof(ToUpper));
            }
        }
        public int PrintSettingId
        {
            get { return printSettingId; }
            set
            {
                printSettingId = value;
                OnPropertyChanged(nameof(PrintSettingId));
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
