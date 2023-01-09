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

namespace ForeignLanguagesSchool.View.Authorized.ProfessorPanel
{
    /// <summary>
    /// Interaction logic for ShowStudentInfo.xaml
    /// </summary>
    public partial class ShowStudentInfo : Window
    {
        public ShowStudentInfo(User selectedStudent)
        {
            InitializeComponent();
            FirstNameTb.Text = selectedStudent.FirstName;
            LastNameTb.Text = selectedStudent.LastName;
            EmailTb.Text = selectedStudent.Email;
            JmbgTb.Text = selectedStudent.JMBG;
            GenderTb.Text = selectedStudent.Gender.ToString();
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
    }
}
