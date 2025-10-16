using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfApp2.Classes
{
    public class Doctor : INotifyPropertyChanged
    {
        [JsonPropertyName("Id")]
        private int _id;
        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        [JsonPropertyName("Name")]
        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        [JsonPropertyName("LastName")]
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); }
        }

        [JsonPropertyName("MiddleName")]
        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set { _middleName = value; OnPropertyChanged(); }
        }

        [JsonPropertyName("Specialisation")]
        private string _specialisation;
        public string Specialisation
        {
            get => _specialisation;
            set { _specialisation = value; OnPropertyChanged(); }
        }

        [JsonPropertyName("Password")]
        private int _password;
        public int Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string FullName => $"{LastName} {Name} {MiddleName}".Trim();
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
