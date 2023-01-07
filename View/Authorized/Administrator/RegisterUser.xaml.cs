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

namespace ForeignLanguagesSchool.View.Authorized.Administrator
{
    /// <summary>
    /// Interaction logic for RegisterUser.xaml
    /// </summary>
    public partial class RegisterUser : Window
    {
        public RegisterUser()
        {
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
        private void RegisterStudent_Click(object sender, RoutedEventArgs e)
        {
            RegisterStudentWindow window = new RegisterStudentWindow();
            window.Show();
        }

        private void RegisterProfessor_Click(object sender, RoutedEventArgs e)
        {
            RegisterProfessorWindow window = new RegisterProfessorWindow();
            window.Show();
        }
    }
}
