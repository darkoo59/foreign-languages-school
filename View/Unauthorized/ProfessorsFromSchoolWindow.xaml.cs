using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Service;
using ForeignLanguagesSchool.Repository;
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
    /// Interaction logic for ProfessorsFromSchoolWindow.xaml
    /// </summary>
    public partial class ProfessorsFromSchoolWindow : Window
    {
        private List<Professor> professors;
        private readonly UserService _userService;
        private App app;
        public ProfessorsFromSchoolWindow(School selectedSchool)
        {
            app = Application.Current as App;
            _userService = app.userService;
            professors = _userService.GetAllProfessorsBySchoolId(selectedSchool.Id);
            InitializeComponent();
            this.DataContext = this;
            UpdateGridView();
            professorsGrid.Items.Clear();
            professorsGrid.ItemsSource = professors;

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && this.IsFocused == true)
                this.DragMove();
        }

        private void UpdateGridView()
        {
            professorsGrid.AutoGenerateColumns = false;
            professorsGrid.CanUserSortColumns = false;
            DataGridTextColumn dataColumn = new DataGridTextColumn();
            dataColumn.Header = "First name";
            dataColumn.Binding = new Binding("FirstName");
            professorsGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Last name";
            dataColumn.Binding = new Binding("LastName");
            professorsGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Email";
            dataColumn.Binding = new Binding("Email");
            professorsGrid.Columns.Add(dataColumn);
        }
    }
}
