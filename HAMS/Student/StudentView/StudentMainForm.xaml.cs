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
            try { if (session != null) { textBlockUserId.Text = session; } }
            catch(Exception ex)
            {
                throw new Exception("界面间传值发生异常" + ex.Message);
            }
            
            
            
        }
        private void BtnHomewordAlert_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
