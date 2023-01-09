using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Service;
using ForeignLanguagesSchool.View.Authorized.Administrator;
using ForeignLanguagesSchool.View.Authorized.ProfessorPanel;
using ForeignLanguagesSchool.View.Authorized.StudentPanel;
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

namespace ForeignLanguagesSchool.View.Unauthorized
{
    /// <summary>
    /// Interaction logic for ProfessorsFilter.xaml
    /// </summary>
    public partial class ProfessorsFilter : Window
    {
        private readonly UserService _userService;
        private App app;
        private List<Professor> professorsToFilter;
        public ProfessorsFilter(List<Professor> professorsToFilter)
        {
            app = Application.Current as App;
            _userService = app.userService;
            this.professorsToFilter = professorsToFilter;
            InitializeComponent();
            foreach (Professor professor in professorsToFilter)
            {
                foreach (string language in professor.Languages)
                {
                    if (!LanguagesCmb.Items.Contains(language))
                        LanguagesCmb.Items.Add(language);
                }
            }
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

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (!IsDigitsOnly(NumberTb.Text))
                MessageBox.Show("Street number should contains numbers only.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                if (FirstNameTb.Text != "")
                    professorsToFilter = _userService.FilterListByFirstName(professorsToFilter, FirstNameTb.Text);
                if (LastNameTb.Text != "")
                    professorsToFilter = _userService.FilterListByLastName(professorsToFilter, LastNameTb.Text);
                if (EmailTb.Text != "")
                    professorsToFilter = _userService.FilterListByEmail(professorsToFilter, EmailTb.Text);
                if (CountryTb.Text != "")
                    professorsToFilter = _userService.FilterListByCountry(professorsToFilter, CountryTb.Text);
                if (CityTb.Text != "")
                    professorsToFilter = _userService.FilterListByCity(professorsToFilter, CityTb.Text);
                if (StreetTb.Text != "")
                    professorsToFilter = _userService.FilterListByStreet(professorsToFilter, StreetTb.Text);
                if (NumberTb.Text != "")
                    professorsToFilter = _userService.FilterListByNumber(professorsToFilter, NumberTb.Text);
                if (LanguagesCmb.SelectedIndex != -1)
                    professorsToFilter = _userService.FilterListByLanguages(professorsToFilter, (string)LanguagesCmb.SelectedValue);
                foreach (Window window in Application.Current.Windows.OfType<ProfessorsFromSchoolWindow>())
                    ((ProfessorsFromSchoolWindow)window).FilterList(professorsToFilter);
                foreach (Window window in Application.Current.Windows.OfType<ProfessorsWindow>())
                    ((ProfessorsWindow)window).FilterList(professorsToFilter);
                foreach (Window window in Application.Current.Windows.OfType<AllProfessorsWindow>())
                    ((AllProfessorsWindow)window).FilterList(professorsToFilter);
                foreach (Window window in Application.Current.Windows.OfType<ProfessorAllProfessorsWindow>())
                    ((ProfessorAllProfessorsWindow)window).FilterList(professorsToFilter);
                this.Close();
            }
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
    }
}
