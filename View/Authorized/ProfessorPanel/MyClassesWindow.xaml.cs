using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Service;
using ForeignLanguagesSchool.Utils;
using ForeignLanguagesSchool.View.Authorized.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

namespace ForeignLanguagesSchool.View.Authorized.ProfessorPanel
{
    /// <summary>
    /// Interaction logic for MyClassesWindow.xaml
    /// </summary>
    public partial class MyClassesWindow : Window
    {
        private readonly UserService _userService;
        private readonly ClassService _classService;
        private List<Class> classes;
        private App app;
        private Class selectedClass;
        public MyClassesWindow()
        {
            app = Application.Current as App;
            _userService = app.userService;
            _classService = app.classService;
            InitializeComponent();
            datePicker.DisplayDateStart = DateTime.Today;
            UpdateGridView();
            classesDataGrid.Items.Clear();
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
            ProfessorAddClass window = new ProfessorAddClass();
            window.Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (classesDataGrid.SelectedItem != null)
            {
                if (((Class)classesDataGrid.SelectedItem).ClassStatus.Equals(ClassStatus.Available))
                {
                    _classService.Delete((Class)classesDataGrid.SelectedItem);
                    UpdateClasses();
                }else
                    MessageBox.Show("You can only delete available classes!", "Can't delete!", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Please select class first!", "Not selected class!", MessageBoxButton.OK);
        }

        private void ShowStudent_Click(object sender, RoutedEventArgs e)
        {
            if (classesDataGrid.SelectedItem != null)
            {
                ShowStudentInfo window = new ShowStudentInfo(_userService.GetUserById(((Class)classesDataGrid.SelectedItem).Id));
                window.Show();
            }
            else
                MessageBox.Show("Please select class first!", "Not selected class!", MessageBoxButton.OK);
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = (DateTime)datePicker.SelectedDate;
            classes = _classService.GetClassesForProfessorAndDate(app.loggedUser.Id, selectedDate);
            classesDataGrid.ItemsSource = classes;
        }

        public void UpdateClasses()
        {
            DateTime selectedDate = (DateTime)datePicker.SelectedDate;
            if (selectedDate != null)
            {
                classes = _classService.GetClassesForProfessorAndDate(app.loggedUser.Id, selectedDate);
                classesDataGrid.ItemsSource = classes;
            }
        }
    }
}
