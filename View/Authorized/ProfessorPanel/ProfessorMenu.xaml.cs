using ForeignLanguagesSchool.View.Authorized.Administrator;
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

namespace ForeignLanguagesSchool.View.Authorized.ProfessorPanel
{
    /// <summary>
    /// Interaction logic for ProfessorMenu.xaml
    /// </summary>
    public partial class ProfessorMenu : UserControl
    {
        private App app;
        public ProfessorMenu()
        {
            app = Application.Current as App;
            InitializeComponent();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (TgButton.IsChecked == true)
            {
                tt_classes.Visibility = Visibility.Collapsed;
                tt_signOut.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_classes.Visibility = Visibility.Visible;
                tt_signOut.Visibility = Visibility.Visible;
            }
        }

        private void MyClasses_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            MyClassesWindow window = new MyClassesWindow();
            window.Show();
            myWindow.Close();
        }

        private void AllProfessors_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            ProfessorAllProfessorsWindow window = new ProfessorAllProfessorsWindow();
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
