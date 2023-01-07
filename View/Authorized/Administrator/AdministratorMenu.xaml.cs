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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ForeignLanguagesSchool.View.Authorized.Administrator
{
    /// <summary>
    /// Interaction logic for AdministratorMenu.xaml
    /// </summary>
    public partial class AdministratorMenu : UserControl
    {
        public AdministratorMenu()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (TgButton.IsChecked == true)
            {
                tt_accounts.Visibility = Visibility.Collapsed;
                tt_classes.Visibility = Visibility.Collapsed;
                tt_register.Visibility = Visibility.Collapsed;
                tt_register.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_accounts.Visibility = Visibility.Visible;
                tt_classes.Visibility = Visibility.Visible;
                tt_register.Visibility = Visibility.Visible;
                tt_register.Visibility = Visibility.Visible;
            }
        }

        private void Accounts_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            AccountsWindow window = new AccountsWindow();
            window.Show();
            myWindow.Close();
        }

        private void Classes_Click(object sender, MouseButtonEventArgs e)
        {
        }

        private void Register_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            RegisterUser window = new RegisterUser();
            window.Show();
            myWindow.Close();
        }

        private void SignOut_Click(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
