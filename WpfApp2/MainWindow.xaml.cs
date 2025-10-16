using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.Classes;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Doctor _currentDoctor = new Doctor();
        private Pacient _currentPacient = new Pacient();
        private int _doctorsCount;
        private int _pacientsCount;
        private int _totalCount;
        private string doctorsFolder = @"C:\Users\Олег\Desktop\WpfApp2\WpfApp2\Accounts\Doctor";
        private string pacientFolder = @"C:\Users\Олег\Desktop\WpfApp2\WpfApp2\Accounts\Pacient";
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            UpdateStats();
        }
        public Doctor CurrentDoctor
        {
            get => _currentDoctor;
            set { _currentDoctor = value; OnPropertyChanged(); }
        }

        public Pacient CurrentPacient
        {
            get => _currentPacient;
            set { _currentPacient = value; OnPropertyChanged(); }
        }

        public int DoctorsCount
        {
            get => _doctorsCount;
            set { _doctorsCount = value; OnPropertyChanged(); }
        }

        public int PacientsCount
        {
            get => _pacientsCount;
            set { _pacientsCount = value; OnPropertyChanged(); }
        }

        public int TotalCount
        {
            get => _totalCount;
            set { _totalCount = value; OnPropertyChanged(); }
        }
        private void UpdateStats()
        {
            DoctorsCount = Directory.GetFiles(@"C:\Users\Олег\Desktop\WpfApp2\WpfApp2\Accounts\Doctor", "D_*.json").Length;
            PacientsCount = Directory.GetFiles(@"C:\Users\Олег\Desktop\WpfApp2\WpfApp2\Accounts\Pacient", "P_*.json").Length;
            TotalCount = DoctorsCount + PacientsCount;
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (NameDoctorTextBox.Text.Length == 0 || LastNameDoctorTextBox.Text.Length == 0 || SpecialisationDoctorTextBox.Text.Length == 0 || MiddleNameDoctorTextBox.Text.Length == 0 || PasswordDoctorTextBox.Password.Length == 0 || ConfirmDoctorTextBox.Password.Length == 0)
            {
                MessageBox.Show("Поле ввода пустое, добавление невозможно");

            }
            if (PasswordDoctorTextBox.Password != ConfirmDoctorTextBox.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }
            else
            {
                P_Information.Visibility = Visibility.Visible;
                P_Add.Visibility = Visibility.Visible;
                D_Change.Visibility = Visibility.Visible;
                AllCount.Visibility = Visibility.Visible;
                var userData = new Doctor
                {
                    Name = NameDoctorTextBox.Text,
                    LastName = LastNameDoctorTextBox.Text,
                    Specialisation = SpecialisationDoctorTextBox.Text,
                    Id = rnd.Next(),
                    MiddleName = MiddleNameDoctorTextBox.Text,

                };
                int iddoctor = rnd.Next(10000, 99999);
                string fileName = @$"C:\Users\Олег\Desktop\WpfApp2\WpfApp2\Accounts\Doctor\D_{iddoctor}.json";
                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                string jsonString = JsonSerializer.Serialize(userData, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(filePath, jsonString);
                UpdateStats();
                MessageBox.Show($"Данные успешно сохранены, файл сохранен как D_{iddoctor} ");
            }
        }

        private void EnterAccount_Click(object sender, RoutedEventArgs e)
        {
            string doctorId = IdEnter.Text.Trim();
            string password = PasswordEnter.Password;
            if (string.IsNullOrEmpty(doctorId))
            {
                IdEnter.Text = "Введите ID доктора";
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                PasswordEnter.Password = "Введите пароль";
                return;
            }
            if (!doctorId.StartsWith("D_"))
            {
                IdEnter.Text = "ID доктора должен начинаться с D_";
                return;
            }
            string filePath = System.IO.Path.Combine(doctorsFolder, $"{doctorId}.json");
            string doctorjsonread = File.ReadAllText(filePath);
            Doctor doctor = JsonSerializer.Deserialize<Doctor>(doctorjsonread);
            if (doctor.Password.ToString() == password)
            {
                CurrentDoctor = doctor;
                MessageBox.Show($"Успешный вход {doctorId}");
                P_Information.Visibility = Visibility.Visible;
                P_Add.Visibility = Visibility.Visible;
                D_Change.Visibility = Visibility.Visible;
                AllCount.Visibility = Visibility.Visible;

            }
            else
            {
                IdEnter.Text = "Неверный пароль";
            }
        }
        private void PacientAdd_Click(object sender, RoutedEventArgs e)
        {
            if (PLastNameBox.Text.Length == 0 || PNameBox.Text.Length == 0 || PDiagnosisTextBox.Text.Length == 0 || PRecomendationsTextBox.Text.Length == 0)
            {
                MessageBox.Show("Поле ввода пустое, добавление невозможно");

            }
            if (PasswordDoctorTextBox.Password != ConfirmDoctorTextBox.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }
            else
            {
                P_Information.Visibility = Visibility.Visible;
                P_Add.Visibility = Visibility.Visible;
                D_Change.Visibility = Visibility.Visible;
                AllCount.Visibility = Visibility.Visible;
                var userData = new Pacient
                {
                    PName = PNameBox.Text,
                    PLastName = PLastNameBox.Text,
                    PMiddleName = PMiddleNameBox.Text,
                    PId = rnd.Next(),
                    PBirthday = (DateTimeOffset)PBirthday.SelectedDate,
                    PLastAppointment = (DateTimeOffset)PLastAppointment.SelectedDate,
                    PDiagnosis = PDiagnosisTextBox.Text,
                    PRecommendations = PRecomendationsTextBox.Text,

                };
                int idpacient = rnd.Next(1000000, 9999999);
                string fileName = @$"C:\Users\Олег\Desktop\WpfApp2\WpfApp2\Accounts\Pacient\P_{idpacient}.json";
                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                string jsonString = JsonSerializer.Serialize(userData, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(filePath, jsonString);
                UpdateStats();
                MessageBox.Show($"Данные успешно сохранены, файл сохранен как P_{idpacient} ");
            }
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            string patientId = SearchPacientId.Text.Trim();

            if (patientId.Length != 7 || !int.TryParse(patientId, out _))
            {
                MessageBox.Show("ID пациента должен состоять из 7 цифр");
                return;
            }

            string filePath = System.IO.Path.Combine(pacientFolder, $"P_{patientId}.json");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Пациент не найден");
                return;
            }

            else
            {
                string json = File.ReadAllText(filePath);
                CurrentPacient = JsonConvert.DeserializeObject<Pacient>(json);
                MessageBox.Show($"Пациент найден: {CurrentPacient.PId}");
            }
        }

        private void ChangePacient_Click(object sender, RoutedEventArgs e)
        {
            string patientId = SearchPacientId.Text.Trim();

            if (string.IsNullOrEmpty(SearchPacientId.Text.Trim()))
            {
                MessageBox.Show("Пусто");
                return;
            }
            if (patientId.Length != 7 || !int.TryParse(patientId, out _))
            {
                MessageBox.Show("ID пациента должен состоять из 7 цифр");
                return;
            }
            else
            {
                string json = JsonConvert.SerializeObject(CurrentPacient, Formatting.Indented);
                string filePath = System.IO.Path.Combine(pacientFolder, $"P_{CurrentPacient.PId}.json");                
                MessageBox.Show($"Изменения сохранены");
                File.WriteAllText(filePath, json);
                UpdateStats();
                OnPropertyChanged(nameof(CurrentPacient));
            }
        }

        private void DelayPacient_Click(object sender, RoutedEventArgs e)
        {
      //      int pId = CurrentPacient.PId;
      //      if (!string.IsNullOrEmpty(Convert.ToString(pId)))
     //       {
     //           string filePath = System.IO.Path.Combine(pacientFolder, $"P_{CurrentPacient.PId}.json");
      //          if (File.Exists(filePath))
     //           {
     //               string json = File.ReadAllText(filePath);
     //               Pacient originalPatient = JsonConvert.DeserializeObject<Pacient>(json);
     //               CurrentPacient = originalPatient;
     //               UpdateStats();
     //               OnPropertyChanged(nameof(CurrentPacient));
      //              MessageBox.Show("Данные сброшены до изначальных из файла!");
      //              File.WriteAllText(filePath, json);
                }
            }
        }