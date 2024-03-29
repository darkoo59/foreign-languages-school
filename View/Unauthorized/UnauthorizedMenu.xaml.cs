﻿using System;
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

namespace ForeignLanguagesSchool.View.Unauthorized
{
    /// <summary>
    /// Interaction logic for UnauthorizedMenu.xaml
    /// </summary>
    public partial class UnauthorizedMenu : UserControl
    {
        public UnauthorizedMenu()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (TgButton.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_login.Visibility = Visibility.Collapsed;
                tt_register.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_login.Visibility = Visibility.Visible;
                tt_register.Visibility = Visibility.Visible;
            }
        }

        private void Home_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            Home window = new Home();
            window.Show();
            myWindow.Close();
        }

        private void Register_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            RegisterWindow window = new RegisterWindow();
            window.Show();
            myWindow.Close();
        }

        private void Login_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            SignInWindow window = new SignInWindow();
            window.Show();
            myWindow.Close();
        }

    }
}
