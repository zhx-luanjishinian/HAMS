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
using HAMS.Student.StudentService;
using HAMS.Student.StudentView;
using HAMS.Student.StudentUserControl;

namespace HAMS.Student.StudentView
{
    /// <summary>
    /// StudentMainForm.xaml 的交互逻辑
    /// </summary>
    public partial class StudentMainForm : Window
    {
        private SService sts = new SService();
        public string account { set; get; }
        public string name { set; get; }
        public StudentMainForm(string account,string name)

        {
          
            InitializeComponent();
            this.account = account;
            this.name = name;
            textBlockUserId.Text = account+name;
            MainShow();
            HomeWorkShow();
        }
        //主界面生成
        public void MainShow()
        {
            Dictionary<int, List<string>> info = sts.showCourseInfo(this.account);
            
            for(int i = 0; i < info.Count; i++)
            {
                ListViewItem ivi = new ListViewItem();
                MainInfo mf = new MainInfo();
                mf.Name = "mf_" + i;
                mf.labelClassId1.Content = info[i][2];
                mf.textBlockClassName1.Text = info[i][3];
                mf.textBlockDepartmentName1.Text = info[i][1];
                mf.textBlockTeacherName1.Text = info[i][0];
                ivi.Content = mf;
                listView2.Items.Add(ivi);
            }
            
        }
        //作业公告生成调用
        public void HomeWorkShow()
        {
            List<String> result = sts.showHomeNoticeInfo(this.account);
           
            for(int i = 0; i < result.Count; i++) {
                ListViewItem ivi = new ListViewItem();
                HomeworkNoteInfo hni = new HomeworkNoteInfo();
                hni.lab1.Content = result[i];
                ivi.Content = hni;
                listView.Items.Add(ivi);
                     }
        }
        //作业主界面每个button的点击事件
       

        private void BtnHomewordAlert_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                AlertForm af = new AlertForm();
                af.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnHomeworkMana_Click(object sender, RoutedEventArgs e)
        {

        }

        private void class1_Click(object sender, RoutedEventArgs e)
        {
            
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StuMainHomework smw = new StuMainHomework();
                smw.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void class2_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StuMainHomework smw = new StuMainHomework();
                smw.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void class3_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StuMainHomework smw = new StuMainHomework();
                smw.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

       

        private void listView2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnRecntNo1_Click_1(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StuCheckHomework sch = new StuCheckHomework();
                sch.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
