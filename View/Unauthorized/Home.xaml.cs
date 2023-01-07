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
using ForeignLanguagesSchool.Repository;
using ForeignLanguagesSchool.Model;

namespace ForeignLanguagesSchool.View.Unauthorized
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private readonly SchoolService _schoolService;
        private readonly AddressService _addressService;
        private List<School> schools;
        private App app;

        public Home()
        {
            app = Application.Current as App;
            _addressService = app.addressService;
            _schoolService = app.schoolService;
            schools = _schoolService.getAllSchooles();
            InitializeComponent();
            this.DataContext = this;
            UpdateGridView();

            ComboCity.Items.Add("None");
            ComboCity.SelectedItem = "None";
            foreach (string city in _addressService.getAllCities())
            {
                ComboCity.Items.Add(city);
            }

            ComboLanguage.Items.Add("None");
            ComboLanguage.SelectedItem = "None";
            foreach (string language in _schoolService.getAllLanguages())
            {
                ComboLanguage.Items.Add(language);
            }
            schoolDataGrid.Items.Clear();
            schoolDataGrid.ItemsSource = schools;
        }

        private void UpdateGridView()
        {
            schoolDataGrid.AutoGenerateColumns = false;
            schoolDataGrid.CanUserSortColumns = false;
            DataGridTextColumn dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Name";
            dataColumn.Binding = new Binding("Name");
            schoolDataGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "City";
            dataColumn.Binding = new Binding("Address.City");
            schoolDataGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Street";
            dataColumn.Binding = new Binding("Address.Street");
            schoolDataGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Number";
            dataColumn.Binding = new Binding("Address.Number");
            schoolDataGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Country";
            dataColumn.Binding = new Binding("Address.Country");
            schoolDataGrid.Columns.Add(dataColumn);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && this.IsFocused == true)
                this.DragMove();
        }

        private void Search_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && this.IsFocused == true)
                this.DragMove();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if ((string)ComboCity.SelectedValue != "None" || (string)ComboLanguage.SelectedValue != "None")
            {
                if((string)ComboCity.SelectedValue != "None" && (string)ComboLanguage.SelectedValue == "None")
                {
                    schools = _schoolService.getAllSchoolsForCity((string)ComboCity.SelectedValue);
                    schoolDataGrid.ItemsSource = schools;
                }else if((string)ComboCity.SelectedValue == "None" && (string)ComboLanguage.SelectedValue != "None")
                {
                    schools = _schoolService.getAllSchoolsForLanguage((string)ComboLanguage.SelectedValue);
                    schoolDataGrid.ItemsSource = schools;
                }
                else
                {
                    schools = _schoolService.getAllSchoolsForCityAndLanguage((string)ComboCity.SelectedValue,(string)ComboLanguage.SelectedValue);
                    schoolDataGrid.ItemsSource = schools;
                }
            }
            else
            {
                schools = _schoolService.getAllSchooles();
                schoolDataGrid.ItemsSource = schools;
            }
        }

        private void Show_Professors_Click(object sender, RoutedEventArgs e)
        {
            if (schoolDataGrid.SelectedItem != null)
            {
                ProfessorsFromSchoolWindow window = new ProfessorsFromSchoolWindow((School)schoolDataGrid.SelectedItem);
                window.Show();
            }
        }
    }
}
