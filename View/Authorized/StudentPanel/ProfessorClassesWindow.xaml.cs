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

namespace ForeignLanguagesSchool.View.Authorized.StudentPanel
{
    /// <summary>
    /// Interaction logic for ProfessorClassesWindow.xaml
    /// </summary>
    public partial class ProfessorClassesWindow : Window
    {
        private Professor selectedProfessor;
        private List<Class> availableClasses;
        private List<Class> reservedClasses;
        private readonly UserService _userService;
        private readonly ClassService _classService;
        private App app;
        public ProfessorClassesWindow(Professor selectedProfessor)
        {
            app = Application.Current as App;
            _userService = app.userService;
            _classService = app.classService;
            this.selectedProfessor = selectedProfessor;
            InitializeComponent();
            UpdateGridView();
            availableClasses = _classService.GetAllAvailableClassesForProfessorId(selectedProfessor.Id);
            availableDataGrid.ItemsSource = availableClasses;
            reservedClasses = _classService.GetAllReservedClassesForProfessorIdAndStudentId(selectedProfessor.Id, app.loggedUser.Id);
            reservedDataGrid.ItemsSource = reservedClasses;
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

        private void UpdateGridView()
        {
            availableDataGrid.AutoGenerateColumns = false;
            availableDataGrid.CanUserSortColumns = false;
            DataGridTextColumn dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Start date";
            dataColumn.Binding = new Binding("StartDate");
            availableDataGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Start time";
            dataColumn.Binding = new Binding("StartTime");
            availableDataGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Duration";
            dataColumn.Binding = new Binding("Duration");
            availableDataGrid.Columns.Add(dataColumn);


            reservedDataGrid.AutoGenerateColumns = false;
            reservedDataGrid.CanUserSortColumns = false;
            DataGridTextColumn dataColumn2 = new DataGridTextColumn();
            dataColumn2.Header = "Start date";
            dataColumn2.Binding = new Binding("StartDate");
            reservedDataGrid.Columns.Add(dataColumn2);
            dataColumn2 = new DataGridTextColumn();
            dataColumn2.Header = "Start time";
            dataColumn2.Binding = new Binding("StartTime");
            reservedDataGrid.Columns.Add(dataColumn2);
            dataColumn2 = new DataGridTextColumn();
            dataColumn2.Header = "Duration";
            dataColumn2.Binding = new Binding("Duration");
            reservedDataGrid.Columns.Add(dataColumn2);
        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            if (availableDataGrid.SelectedItem != null)
            {
                Class selectedClass = (Class)availableDataGrid.SelectedItem;
                selectedClass.StudentId = app.loggedUser.Id;
                _classService.ScheduleClass(selectedClass);
                UpdateGrids();
                MessageBox.Show("Class successfully scheduled!", "Successfully added!", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Please select available class first!", "Not selected class!", MessageBoxButton.OK);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (reservedDataGrid.SelectedItem != null)
            {
                Class selectedClass = (Class)reservedDataGrid.SelectedItem;
                _classService.CancelClass(selectedClass);
                UpdateGrids();
                MessageBox.Show("Class successfully caneled!", "Successfully canceled!", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Please select reserved class first!", "Not selected class!", MessageBoxButton.OK);
        }

        public void UpdateGrids()
        {
            availableClasses = _classService.GetAllAvailableClassesForProfessorId(selectedProfessor.Id);
            availableDataGrid.ItemsSource = availableClasses;
            reservedClasses = _classService.GetAllReservedClassesForProfessorIdAndStudentId(selectedProfessor.Id, app.loggedUser.Id);
            reservedDataGrid.ItemsSource = reservedClasses;
        }
    }
}
