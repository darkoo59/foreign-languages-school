using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Service;
using ForeignLanguagesSchool.Utils;
using ForeignLanguagesSchool.View.Authorized.ProfessorPanel;
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
    /// Interaction logic for AccountsFilter.xaml
    /// </summary>
    public partial class AccountsFilter : Window
    {
        private readonly UserService _userService;
        private App app;
        private List<User> usersToFilter;
        public AccountsFilter(List<User> usersToFilter)
        {
            app = Application.Current as App;
            _userService = app.userService;
            this.usersToFilter = usersToFilter;
            InitializeComponent();
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
                if (FirstNameTb.Text != "")
                    usersToFilter = _userService.FilterListByFirstName(usersToFilter, FirstNameTb.Text);
                if (LastNameTb.Text != "")
                    usersToFilter = _userService.FilterListByLastName(usersToFilter, LastNameTb.Text);
                if (EmailTb.Text != "")
                    usersToFilter = _userService.FilterListByEmail(usersToFilter, EmailTb.Text);
                if (CountryTb.Text != "")
                    usersToFilter = _userService.FilterListByCountry(usersToFilter, CountryTb.Text);
                if (CityTb.Text != "")
                    usersToFilter = _userService.FilterListByCity(usersToFilter, CityTb.Text);
                if (StreetTb.Text != "")
                    usersToFilter = _userService.FilterListByStreet(usersToFilter, StreetTb.Text);
                if (NumberTb.Text != "")
                    usersToFilter = _userService.FilterListByNumber(usersToFilter, NumberTb.Text);
                if (UserTypeCmb.SelectedIndex != -1)
                    usersToFilter = _userService.FilterListByUserType(usersToFilter, (Utils.UserType)UserTypeCmb.SelectedIndex);
                foreach (Window window in Application.Current.Windows.OfType<AccountsWindow>())
                    ((AccountsWindow)window).FilterList(usersToFilter);
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
