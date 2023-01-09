using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ForeignLanguagesSchool.View.Authorized.Administrator
{
    /// <summary>
    /// Interaction logic for AddClassWindow.xaml
    /// </summary>
    public partial class AddClassWindow : Window
    {
        private readonly ClassService _classService;
        private Professor selectedProfessor;
        private App app;
        public AddClassWindow(Professor selectedProfessor)
        {
            app = Application.Current as App;
            this.selectedProfessor = selectedProfessor;
            _classService = app.classService;
            InitializeComponent();
            datePicker.DisplayDateStart = DateTime.Today.AddDays(1);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && this.IsFocused == true)
                this.DragMove();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (IsInputValid())
            {
                Class classToAdd = new Class
                {
                    Id = _classService.GenerateId(),
                    ClassStatus = Utils.ClassStatus.Available,
                    Duration = Convert.ToInt32(DurationTb.Text),
                    ProfessorId = selectedProfessor.Id,
                    StartDate = (DateTime)datePicker.SelectedDate,
                    StartTime = TimeSpan.Parse(HourTb.Text + ":" + MinuteTb.Text),
                    StudentId = 0
                };
                _classService.Create(classToAdd);
                MessageBox.Show("Class successfully added!", "Successfully added!", MessageBoxButton.OK);
                foreach (Window window in Application.Current.Windows.OfType<ProfessorClassesWindow>())
                    ((ProfessorClassesWindow)window).UpdateProfessors();
                this.Close();

            }
        }

        private bool IsInputValid()
        {
            if (datePicker.SelectedDate == null)
                MessageBox.Show("Date isn't selected.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (HourTb.Text.Equals(""))
                MessageBox.Show("Hour is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (MinuteTb.Text.Equals(""))
                MessageBox.Show("Minute is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (DurationTb.Text.Equals(""))
                MessageBox.Show("Duration is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (!IsDigitsOnly(HourTb.Text))
                MessageBox.Show("Hour need to be number.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (!IsDigitsOnly(MinuteTb.Text))
                MessageBox.Show("City is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (!IsDigitsOnly(DurationTb.Text))
                MessageBox.Show("Street is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (!isHourValidFormat(HourTb.Text))
                MessageBox.Show("Hour range is from 0 to 24.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (!isMinuteValidFormat(MinuteTb.Text))
                MessageBox.Show("Minute range is from 0 to 59.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (!isDurationValidFormat(DurationTb.Text))
                MessageBox.Show("Duration need to be greater than 0.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                return true;
            return false;
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private bool isHourValidFormat(string str)
        {
            int number = Convert.ToInt32(str);
            if (number < 0 || number > 24)
                return false;
            return true;
        }

        private bool isMinuteValidFormat(string str)
        {
            int number = Convert.ToInt32(str);
            if (number < 0 || number > 59)
                return false;
            return true;
        }

        private bool isDurationValidFormat(string str)
        {
            int number = Convert.ToInt32(str);
            if (number < 0)
                return false;
            return true;
        }
    }
}
