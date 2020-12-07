using System;
using System.Windows;


namespace HAMS.Student.StudentView
{
    /// <summary>
    /// StudentMainForm.xaml 的交互逻辑
    /// </summary>
    public partial class StudentMainForm : Window
    {
        public StudentMainForm(string session)
        {
            InitializeComponent();
            textBlockUserId.Text = session;
           
            
            
        }
        private void BtnHomewordAlert_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
