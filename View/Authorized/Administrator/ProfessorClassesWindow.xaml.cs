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
    /// Interaction logic for ProfessorClassesWindow.xaml
    /// </summary>
    public partial class ProfessorClassesWindow : Window
    {
        private readonly UserService _userService;
        private readonly ClassService _classService;
        private List<Class> classes;
        private App app;
        private Professor selectedProfessor;
        public ProfessorClassesWindow(Professor selectedProfessor)
        {
            this.selectedProfessor = selectedProfessor;
            app = Application.Current as App;
            _userService = app.userService;
            _classService = app.classService;
            classes = _classService.GetAllClassesForProfessorId(selectedProfessor.Id);
            InitializeComponent();
            UpdateGridView();
            classesDataGrid.Items.Clear();
            classesDataGrid.ItemsSource = classes;
        }

        private void UpdateGridView()
        {
            classesDataGrid.AutoGenerateColumns = false;
            classesDataGrid.CanUserSortColumns = false;
            DataGridTextColumn dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Start date";
            dataColumn.Binding = new Binding("StartDate");
            classesDataGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Start time";
            dataColumn.Binding = new Binding("StartTime");
            classesDataGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Duration";
            dataColumn.Binding = new Binding("Duration");
            classesDataGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Class status";
            dataColumn.Binding = new Binding("ClassStatus");
            classesDataGrid.Columns.Add(dataColumn);
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
            AddClassWindow window = new AddClassWindow(selectedProfessor);
            window.Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (classesDataGrid.SelectedItem != null)
            {
                _classService.Delete((Class)classesDataGrid.SelectedItem);
                classes = _classService.GetAllClassesForProfessorId(selectedProfessor.Id);
                classesDataGrid.ItemsSource = classes;
            }else
                MessageBox.Show("Please select class first!", "Not selected class!", MessageBoxButton.OK);
        }

        public void UpdateProfessors()
        {
            classes = _classService.GetAllClassesForProfessorId(selectedProfessor.Id);
            classesDataGrid.ItemsSource = classes;
        }
    }
}
