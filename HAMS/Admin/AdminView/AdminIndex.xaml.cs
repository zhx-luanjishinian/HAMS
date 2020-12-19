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
        public string adminId{get;set;}
        public AdminIndex(string session,string number)
        {
            this.adminId = number;
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            try { if (session != null) { userName.Text = session; } }
            catch (Exception ex)
            {
                throw new Exception("界面间传值发生异常" + ex.Message);
            }
            
        }

        private void BtnStuManage_Click(object sender, RoutedEventArgs e)
        {
            StudentManagement StudentManagement = new StudentManagement();
            btnStuManage.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            btnTeaManage.Background = new SolidColorBrush(Color.FromRgb(2, 136, 209));
            btnClassManage.Background = new SolidColorBrush(Color.FromRgb(2, 136, 209));
            btnNoticeManage.Background = new SolidColorBrush(Color.FromRgb(2, 136, 209));
            content.Content = new Frame()
            {
                Content = StudentManagement
            };
        }

        private void BtnTeaManage_Click(object sender, RoutedEventArgs e)
        {
            TeacherManagement TeacherManagement = new TeacherManagement();
            btnTeaManage.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            btnStuManage.Background = new SolidColorBrush(Color.FromRgb(2, 136, 209));
            btnClassManage.Background = new SolidColorBrush(Color.FromRgb(2, 136, 209));
            btnNoticeManage.Background = new SolidColorBrush(Color.FromRgb(2, 136, 209));
            content.Content = new Frame()
            {
                Content = TeacherManagement
            };
        }

        private void BtnClassManage_Click(object sender, RoutedEventArgs e)
        {
            ClassManagement ClassManagement = new ClassManagement();
            btnClassManage.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            btnStuManage.Background = new SolidColorBrush(Color.FromRgb(2, 136, 209));
            btnTeaManage.Background = new SolidColorBrush(Color.FromRgb(2, 136, 209));
            btnNoticeManage.Background = new SolidColorBrush(Color.FromRgb(2, 136, 209));
            content.Content = new Frame()
            {
                Content = ClassManagement
            };
        }

        private void BtnNoticeManage_Click(object sender, RoutedEventArgs e)
        {
            NoticeManagement NoticeManagement = new NoticeManagement(adminId);
            btnStuManage.Background = new SolidColorBrush(Color.FromRgb(2, 136, 209));
            btnTeaManage.Background = new SolidColorBrush(Color.FromRgb(2, 136, 209));
            btnClassManage.Background = new SolidColorBrush(Color.FromRgb(2, 136, 209));
            btnNoticeManage.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            content.Content = new Frame()
            {
                Content = NoticeManagement
            };
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
