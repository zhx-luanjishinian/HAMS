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

namespace HAMS.Student.StudentView
{
    /// <summary>
    /// StudentMainForm.xaml 的交互逻辑
    /// </summary>
    public partial class StudentMainForm : Window
    {
        public StudentMainForm(string session)

        {
            //textBlockUserId.Text = session;
            InitializeComponent();
        }

        private void BtnHomewordAlert_Click(object sender, RoutedEventArgs e)
        {
            AlertForm af = new AlertForm();
        }

        private void btnHomeworkMana_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void class1_Click(object sender, RoutedEventArgs e)
        {
            StuMainHomework smw = new StuMainHomework();
        }

        private void class2_Click(object sender, RoutedEventArgs e)
        {
            StuMainHomework smw = new StuMainHomework();
        }

        private void class3_Click(object sender, RoutedEventArgs e)
        {
            StuMainHomework smw = new StuMainHomework();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
