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
    /// Interaction logic for SchoolFilter.xaml
    /// </summary>
    public partial class SchoolFilter : Window
    {
        private readonly SchoolService _schoolService;
        private App app;
        private List<School> schoolsToFilter;
        public SchoolFilter(List<School> schoolsToFilter)
        {
            app = Application.Current as App;
            _schoolService = app.schoolService;
            this.schoolsToFilter = schoolsToFilter;
            InitializeComponent();
            foreach (School school in schoolsToFilter)
            {
                if (school.Languages != null)
                {
                    foreach (string language in school.Languages)
                    {
                        if (!LanguagesCmb.Items.Contains(language))
                            LanguagesCmb.Items.Add(language);
                    }
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
                if (NameTb.Text != "")
                    schoolsToFilter = _schoolService.FilterListByName(schoolsToFilter, NameTb.Text);
                if (CountryTb.Text != "")
                    schoolsToFilter = _schoolService.FilterListByCountry(schoolsToFilter, CountryTb.Text);
                if (CityTb.Text != "")
                    schoolsToFilter = _schoolService.FilterListByCity(schoolsToFilter, CityTb.Text);
                if (StreetTb.Text != "")
                    schoolsToFilter = _schoolService.FilterListByStreet(schoolsToFilter, StreetTb.Text);
                if (NumberTb.Text != "")
                    schoolsToFilter = _schoolService.FilterListByNumber(schoolsToFilter, NumberTb.Text);
                if (LanguagesCmb.SelectedIndex != -1)
                    schoolsToFilter = _schoolService.FilterListByLanguages(schoolsToFilter, (string)LanguagesCmb.SelectedValue);
                foreach (Window window in Application.Current.Windows.OfType<Home>())
                    ((Home)window).FilterList(schoolsToFilter);
                foreach (Window window in Application.Current.Windows.OfType<StudentHomeWindow>())
                    ((StudentHomeWindow)window).FilterList(schoolsToFilter);
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
