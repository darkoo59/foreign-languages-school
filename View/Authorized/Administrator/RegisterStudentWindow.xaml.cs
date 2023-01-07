using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Service;
using ForeignLanguagesSchool.Utils;
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
    /// Interaction logic for RegisterStudentWindow.xaml
    /// </summary>
    public partial class RegisterStudentWindow : Window
    {
        private App app;
        private readonly UserService _userService;
        private readonly AddressService _addressService;
        public RegisterStudentWindow()
        {
            app = Application.Current as App;
            _userService = app.userService;
            _addressService = app.addressService;
            InitializeComponent();
        }

        private void ShowPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (PasswordPb.Visibility == Visibility.Visible)
            {
                PasswordTb.Text = PasswordPb.Password;
                PasswordTb.Visibility = Visibility.Visible;
                PasswordPb.Visibility = Visibility.Hidden;
            }
            else
            {
                PasswordPb.Password = PasswordTb.Text;
                PasswordPb.Visibility = Visibility.Visible;
                PasswordTb.Visibility = Visibility.Hidden;
            }
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

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (IsInputValid())
            {
                string password = "";
                if (PasswordPb.Visibility == Visibility.Visible)
                    password = PasswordPb.Password;
                else
                    password = PasswordTb.Text;
                Address address = new Address
                {
                    City = CityTb.Text,
                    Country = CountryTb.Text,
                    Number = Convert.ToInt32(NumberTb.Text),
                    Street = StreetTb.Text,
                    Id = _addressService.GenerateId()
                };
                _addressService.Create(address);
                User user = new User
                {
                    FirstName = FirstNameTb.Text,
                    LastName = LastNameTb.Text,
                    Email = EmailTb.Text,
                    Password = password,
                    JMBG = JmbgTb.Text,
                    UserType = Utils.UserType.Student,
                    Gender = GetGenderType(),
                    Address = address,
                    Id = _userService.GenerateId()
                };
                _userService.Create(user);
                MessageBox.Show("Student successfully registered!", "Successfully registered!", MessageBoxButton.OK);
                this.Close();
            }
        }

        private Gender GetGenderType()
        {
            if (GenderCmb.SelectedIndex == 0)
                return Gender.Male;
            else
                return Gender.Female;
        }

        private bool IsInputValid()
        {
            if (PasswordPb.Visibility == Visibility.Visible)
            {
                if (PasswordPb.Password.Equals(""))
                {
                    MessageBox.Show("Password is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            else
            {
                if (PasswordTb.Text.Equals(""))
                {
                    MessageBox.Show("Password is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            if (FirstNameTb.Text.Equals(""))
                MessageBox.Show("First name is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (LastNameTb.Text.Equals(""))
                MessageBox.Show("Last name is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (JmbgTb.Text.Equals(""))
                MessageBox.Show("Jmbg is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (EmailTb.Text.Equals(""))
                MessageBox.Show("Email is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (CountryTb.Text.Equals(""))
                MessageBox.Show("Country is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (CityTb.Text.Equals(""))
                MessageBox.Show("City is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (StreetTb.Text.Equals(""))
                MessageBox.Show("Street is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (NumberTb.Text.Equals(""))
                MessageBox.Show("Street number is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (GenderCmb.SelectedIndex == -1)
                MessageBox.Show("Gender isn't selected.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (!IsDigitsOnly(NumberTb.Text))
                MessageBox.Show("Street number should contains numbers only.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (!IsDigitsOnly(JmbgTb.Text))
                MessageBox.Show("Jmbg should contains numbers only.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (JmbgTb.Text.Length != 13)
                MessageBox.Show("Jmbg should contains exactly 13 numbers.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (!_userService.IsJmbgUnique(JmbgTb.Text))
                MessageBox.Show("User with same JMBG already registered.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                return true;
            return false;
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
