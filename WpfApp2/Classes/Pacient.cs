using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfApp2.Classes
{
    public class Pacient : INotifyPropertyChanged
    {
        [JsonPropertyName("P_ID")]
        private int _pId;
        public int PId
        {
            get => _pId;
            set { _pId = value; OnPropertyChanged(); }
        }

        [JsonPropertyName("PName")]
        private string _pName;
        public string PName
        {
            get => _pName;
            set { _pName = value; OnPropertyChanged(); }
        }

        [JsonPropertyName("PLastName")]
        private string _pLastName;
        public string PLastName
        {
            get => _pLastName;
            set { _pLastName = value; OnPropertyChanged(); }
        }

        [JsonPropertyName("PMiddleName")]
        private string _pMiddleName;
        public string PMiddleName
        {
            get => _pMiddleName;
            set { _pMiddleName = value; OnPropertyChanged(); }
        }

        [JsonPropertyName("PBirthday")]
        private DateTimeOffset _pBirthday;
        public DateTimeOffset PBirthday
        {
            get => _pBirthday;
            set { _pBirthday = value; OnPropertyChanged(); }
        }

        [JsonPropertyName("PLastAppointment")]
        private DateTimeOffset _pLastAppointment;
        public DateTimeOffset PLastAppointment
        {
            get => _pLastAppointment;
            set { _pLastAppointment = value; OnPropertyChanged(); }
        }

        [JsonPropertyName("PLastDoctor")]
        private string _pLastDoctor;
        public string PLastDoctor
        {
            get => _pLastDoctor;
            set { _pLastDoctor = value; OnPropertyChanged(); }
        }

        [JsonPropertyName("PDiagnosis")]
        private string _pDiagnosis;
        public string PDiagnosis
        {
            get => _pDiagnosis;
            set { _pDiagnosis = value; OnPropertyChanged(); }
        }

        [JsonPropertyName("PRecommendations")]
        private string _pRecommendations;
        public string PRecommendations
        {
            get => _pRecommendations;
            set { _pRecommendations = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public string FullName => $"{PLastName} {PName} {PMiddleName}".Trim();
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
