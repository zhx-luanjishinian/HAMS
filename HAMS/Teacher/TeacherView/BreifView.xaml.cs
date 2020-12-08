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

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// BreifView.xaml 的交互逻辑
    /// </summary>
    public partial class BreifView : Window
    {
        public BreifView(string courseName,string courseNumber)
        {
            InitializeComponent();
            labelCourseName.Content=courseName;
            labelCourseNumber.Content = courseNumber;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                TeacherMainForm newTeacherMainForm = new TeacherMainForm("1","0");
                newTeacherMainForm.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnDeliverHomework_Click(object sender, RoutedEventArgs e)
        {
            // 打开子窗体
            AnnounceNotice newAnnounceNotice = new AnnounceNotice();
            newAnnounceNotice.Show();
            // 隐藏自己(父窗体)
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnCheckDetail_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                HomeworkNoticeCheck newHomeworkNoticeCheck = new HomeworkNoticeCheck();
                newHomeworkNoticeCheck.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            // 打开子窗体
            AnnounceNotice newAnnounceNotice = new AnnounceNotice();
            newAnnounceNotice.Show();
            // 隐藏自己(父窗体)
            this.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
