using System;
using System.ComponentModel;
using System.Diagnostics;

namespace EX.Model.DTO
{
    public class UserDTO : INotifyPropertyChanged
    {
        int id;
        string firstName;
        string lastName;
        string login;
        string password;
        bool isSelected;
        bool isDefault;

        public int Id { get { return id; } set { id = value; OnPropertyChanged(nameof(Id)); } }
        public string FirstName { get { return firstName; } set { firstName = value; OnPropertyChanged(nameof(FirstName)); } }
        public string LastName { get { return lastName; } set { lastName = value; OnPropertyChanged(nameof(LastName)); } }
        public string Login { get { return login; } set { login = value; OnPropertyChanged(nameof(Login)); } }
        public string Password { get { return password; } set { password = value; OnPropertyChanged(nameof(Password)); } }
        public bool IsSelected { get { return isSelected; } set { isSelected = value; OnPropertyChanged(nameof(IsSelected)); } }
        public bool IsDefault { get { return isDefault; } set { isDefault = value;OnPropertyChanged(nameof(IsDefault)); } }

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
