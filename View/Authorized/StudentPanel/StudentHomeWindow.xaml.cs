using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Service;
using ForeignLanguagesSchool.View.Unauthorized;
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

namespace ForeignLanguagesSchool.View.Authorized.StudentPanel
{
    /// <summary>
    /// Interaction logic for StudentHomeWindow.xaml
    /// </summary>
    public partial class StudentHomeWindow : Window
    {
        private readonly SchoolService _schoolService;
        private readonly AddressService _addressService;
        private List<School> schools;
        private App app;
        public StudentHomeWindow()
        {
            app = Application.Current as App;
            _addressService = app.addressService;
            _schoolService = app.schoolService;
            schools = _schoolService.getAllSchooles();
            InitializeComponent();
            this.DataContext = this;
            UpdateGridView();
            InitializeComponent();
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

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            SchoolFilter window = new SchoolFilter(_schoolService.getAllSchooles());
            window.Show();
        }

        internal void FilterList(List<School> filteredSchools)
        {
            schools = filteredSchools;
            schoolDataGrid.ItemsSource = filteredSchools;
        }
    }
}
