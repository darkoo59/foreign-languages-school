using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Service;
using ForeignLanguagesSchool.View.Authorized.ProfessorPanel;
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
    /// Interaction logic for AllProfessorsWindow.xaml
    /// </summary>
    public partial class AllProfessorsWindow : Window
    {
        private readonly UserService _userService;
        private readonly ClassService _classService;
        private List<Professor> professors;
        private App app;
        public AllProfessorsWindow()
        {
            app = Application.Current as App;
            _userService = app.userService;
            _classService = app.classService;
            InitializeComponent();
            UpdateGridView();
            professors = _userService.getAllProfessors();
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
            else
                MessageBox.Show("Please select class first!", "Not selected class!", MessageBoxButton.OK);
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
