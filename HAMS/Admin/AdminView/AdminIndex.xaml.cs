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
using System.Windows.Navigation;

namespace HAMS.Admin.AdminView
{
    /// <summary>
    /// AdminIndex.xaml 的交互逻辑
    /// </summary>
    public partial class AdminIndex : Window
    {
        public AdminIndex(string session)
        {
            InitializeComponent();
            
            try { if (session != null) { userName.Text = session; } }
            catch (Exception ex)
            {
                throw new Exception("界面间传值发生异常" + ex.Message);
            }
            
        }

        private void BtnStuManage_Click(object sender, RoutedEventArgs e)
        {
            StudentManagement StudentManagement = new StudentManagement();
            content.Content = new Frame()
            {
                Content = StudentManagement
            };
        }

        private void BtnTeaManage_Click(object sender, RoutedEventArgs e)
        {
            TeacherManagement TeacherManagement = new TeacherManagement();
            content.Content = new Frame()
            {
                Content = TeacherManagement
            };
        }

        private void BtnClassManage_Click(object sender, RoutedEventArgs e)
        {
            ClassManagement ClassManagement = new ClassManagement();
            content.Content = new Frame()
            {
                Content = ClassManagement
            };
        }

        private void BtnNoticeManage_Click(object sender, RoutedEventArgs e)
        {
            NoticeManagement NoticeManagement = new NoticeManagement();
            content.Content = new Frame()
            {
                Content = NoticeManagement
            };
        }
    }
}
