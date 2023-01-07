using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Service;
using ForeignLanguagesSchool.Utils;
using ForeignLanguagesSchool.View.Authorized.Administrator;
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
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        private App app;
        private readonly UserService _userService;
        public SignInWindow()
        {
            app = Application.Current as App;
            _userService = app.userService;
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

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (IsInputValid())
            {
                string password = "";
                if (PasswordPb.Visibility == Visibility.Visible)
                    password = PasswordPb.Password;
                else
                    password = PasswordTb.Text;
                if (_userService.GetUserByCredentials(JmbgTb.Text, password) != null)
                {
                    app.loggedUser = _userService.GetUserByCredentials(JmbgTb.Text, password);
                    //TODO: Go to Home page for specific user
                    if(app.loggedUser.UserType.Equals(UserType.Administrator))
                    {
                        var myWindow = Window.GetWindow(this);
                        AccountsWindow window = new AccountsWindow();
                        window.Show();
                        myWindow.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Wrong credentials used.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
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
            if (JmbgTb.Text.Equals(""))
                MessageBox.Show("Jmbg is empty.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (!IsDigitsOnly(JmbgTb.Text))
                MessageBox.Show("Jmbg should contains numbers only.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (JmbgTb.Text.Length != 13)
                MessageBox.Show("Jmbg should contains exactly 13 numbers.", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
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