using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Service;
using ForeignLanguagesSchool.View.Authorized.ProfessorPanel;
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

namespace ForeignLanguagesSchool.View.Authorized.StudentPanel
{
    /// <summary>
    /// Interaction logic for StudentMenu.xaml
    /// </summary>
    public partial class StudentMenu : UserControl
    {
        private App app;
        public StudentMenu()
        {
            app = Application.Current as App;
            InitializeComponent();
        }
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (TgButton.IsChecked == true)
            {
                tt_professors.Visibility = Visibility.Collapsed;
                tt_signOut.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_professors.Visibility = Visibility.Visible;
                tt_signOut.Visibility = Visibility.Visible;
            }
        }

        private void Professors_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            AllProfessorsWindow window = new AllProfessorsWindow();
            window.Show();
            myWindow.Close();
        }

        private void Home_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            StudentHomeWindow window = new StudentHomeWindow();
            window.Show();
            myWindow.Close();
        }

        private void SignOut_Click(object sender, MouseButtonEventArgs e)
        {
            app.loggedUser = null;
            var myWindow = Window.GetWindow(this);
            Home window = new Home();
            window.Show();
            myWindow.Close();
        }
    }
}
