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

namespace ForeignLanguagesSchool.View.Authorized.Administrator
{
    /// <summary>
    /// Interaction logic for ProfessorsWindow.xaml
    /// </summary>
    public partial class ProfessorsWindow : Window
    {
        private readonly UserService _userService;
        private List<Professor> professors;
        private App app;
        public ProfessorsWindow()
        {
            app = Application.Current as App;
            _userService = app.userService;
            professors = _userService.getAllProfessors();
            InitializeComponent();
            this.DataContext = this;
            UpdateGridView();
            professorsDataGrid.Items.Clear();
            professorsDataGrid.ItemsSource = professors;
        }

        private void UpdateGridView()
        {
            professorsDataGrid.AutoGenerateColumns = false;
            professorsDataGrid.CanUserSortColumns = false;
            DataGridTextColumn dataColumn = new DataGridTextColumn();
            dataColumn.Header = "First name";
            dataColumn.Binding = new Binding("FirstName");
            professorsDataGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Last name";
            dataColumn.Binding = new Binding("LastName");
            professorsDataGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Email";
            dataColumn.Binding = new Binding("Email");
            professorsDataGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Gender";
            dataColumn.Binding = new Binding("Gender");
            professorsDataGrid.Columns.Add(dataColumn);
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

        private void ShowClasses_Click(object sender, RoutedEventArgs e)
        {
            if (professorsDataGrid.SelectedItem != null)
            {
                ProfessorClassesWindow window = new ProfessorClassesWindow((Professor)professorsDataGrid.SelectedItem);
                window.Show();
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            ProfessorsFilter window = new ProfessorsFilter(_userService.getAllProfessors());
            window.Show();
        }

        internal void FilterList(List<Professor> filteredProfessors)
        {
            professors = filteredProfessors;
            professorsDataGrid.ItemsSource = filteredProfessors;
        }
    }
}
