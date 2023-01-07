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
    /// Interaction logic for AccountsWindow.xaml
    /// </summary>
    public partial class AccountsWindow : Window
    {
        private readonly UserService _userService;
        private List<User> users;
        private App app;
        public AccountsWindow()
        {
            app = Application.Current as App;
            _userService = app.userService;
            users = _userService.getAllUsers();
            InitializeComponent();
            this.DataContext = this;
            UpdateGridView();
            accountsDataGrid.Items.Clear();
            accountsDataGrid.ItemsSource = users;
        }


        private void UpdateGridView()
        {
            accountsDataGrid.AutoGenerateColumns = false;
            accountsDataGrid.CanUserSortColumns = false;
            DataGridTextColumn dataColumn = new DataGridTextColumn();
            dataColumn.Header = "First name";
            dataColumn.Binding = new Binding("FirstName");
            accountsDataGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Last name";
            dataColumn.Binding = new Binding("LastName");
            accountsDataGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Email";
            dataColumn.Binding = new Binding("Email");
            accountsDataGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Type";
            dataColumn.Binding = new Binding("UserType");
            accountsDataGrid.Columns.Add(dataColumn);
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

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (accountsDataGrid.SelectedItem != null)
            {
                UpdateUserWindow window = new UpdateUserWindow((User)accountsDataGrid.SelectedItem);
                window.Show();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (accountsDataGrid.SelectedItem != null)
            {
                _userService.Delete((User)accountsDataGrid.SelectedItem);
                users = _userService.getAllUsers();
                accountsDataGrid.ItemsSource = users;
            }
        }

        public void UpdateUsers()
        {
            users = _userService.getAllUsers();
            accountsDataGrid.ItemsSource = users;
        }
    }
}
