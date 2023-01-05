using ForeignLanguagesSchool.Model;
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
        public ProfessorsFromSchoolWindow(School selectedSchool)
        {
            InitializeComponent();
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
    }
}
