using System;
using System.Collections.Generic;
using System.Data;
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
        public BreifView(string courseNum,string courseName,string tId,string tName)
        {
            //生成基本信息
            InitializeComponent();
            labelCourseName.Content = courseName;
            labelCourseNumber.Content = courseNum;
            lbTeacherInfo.Content = tId;
            lbTeacherInfo1.Content = tName;
            //从数据库中查找目前该课堂已经的布置作业
            TeacherDao.TDao td = new TeacherDao.TDao();
            DataTable table = td.getNotice(courseNum);  //有问题
            //MessageBox.Show(table.Rows[3][0].ToString());
            ////动态生成控件
            BreifHomework[] arrayBreifHomework = new BreifHomework[20];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                arrayBreifHomework[i] = new BreifHomework();
                arrayBreifHomework[i].Name = "arrayHk" + i.ToString();
                //加载作业标题
                arrayBreifHomework[i].title.Content = table.Rows[i][7].ToString();
                //加载作业描述
                arrayBreifHomework[i].description.Content = table.Rows[i][4].ToString();
                //加在canvas里面
                //arrayHomk.Children.Add(arrayBreifHomework[i]);
                homeworkListView.Items.Add(arrayBreifHomework[i]);
            }
        }
       // public BreifV
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                TeacherMainForm newTeacherMainForm = new TeacherMainForm(lbTeacherInfo.Content.ToString(),lbTeacherInfo1.Content.ToString());
                newTeacherMainForm.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnDeliverHomework_Click(object sender, RoutedEventArgs e)
        {
            // 打开子窗体
            AnnounceNotice newAnnounceNotice = new AnnounceNotice(lbTeacherInfo.Content.ToString(),lbTeacherInfo1.Content.ToString(),labelCourseNumber.Content.ToString()
                ,labelCourseName.Content.ToString());
            newAnnounceNotice.Show();
            // 隐藏自己(父窗体)
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnCheckDetail_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                CheckingClassHomework newHomeworkNoticeCheck = new CheckingClassHomework();
                newHomeworkNoticeCheck.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            // 打开子窗体
            AnnounceNotice newAnnounceNotice = new AnnounceNotice("","","","");
            newAnnounceNotice.Show();
            // 隐藏自己(父窗体)
            this.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
