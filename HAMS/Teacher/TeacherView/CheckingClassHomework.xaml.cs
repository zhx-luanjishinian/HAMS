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
    /// CheckingClassHomework.xaml 的交互逻辑
    /// </summary>
    public partial class CheckingClassHomework : Window
    {
        TeacherService.TService ts = new TeacherService.TService();

        public CheckingClassHomework()
        {
            InitializeComponent();
        }

        public CheckingClassHomework(string homeworkTitle, string description,string teacherSpecId,string teacherName,string classSpecId,string className)
        {
            //生成基本信息
            InitializeComponent();
            lbNotTitle.Content = homeworkTitle;
            textBlockDescription.Text = description;
            tbTeacherInfo.Text = teacherSpecId;
            tbTeacherInfo1.Text = teacherName;
            tbClassInfo.Text = classSpecId;
            tbClassInfo1.Text = className;
            labelHomeworkArrangeTime.Content = "发布时间："+ts.PasteSubmitTimeInForm(classSpecId, homeworkTitle);
        }

        private void btnHomeworkCorrect_Click(object sender, RoutedEventArgs e)
        {
            //!!定义homIds的操作应该属于业务层的方法，然后是在ListViewUnCheck加载的时候就对homIds进行了赋值操作
            List<int> homIds = new List<int>();
            //对homIds进行赋值操作


            //获取当前点击的作业再homIds中的下标
            //int index = getHomIdsIndex();
            int index = 0;//这里设置一个假值

            TeacherHomeworkCheck thc = new TeacherHomeworkCheck(homIds,index,(string)lbNotTitle.Content,(string)lbStudentInfo.Content);
            //这里的homId是要带给下一个界面的值,表示待的homId数组，其中index是正要批改的homId在homId数组中的索引
            thc.ShowDialog();
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnHomeworkCorrect1_Click(object sender, RoutedEventArgs e)
        {
            //!!定义homIds的操作应该属于业务层的方法，然后是在ListViewUnCheck加载的时候就对homIds进行了赋值操作
            List<int> homIds = new List<int>();
            //对homIds进行赋值操作


            //获取当前点击的作业再homIds中的下标
            //int index = getHomIdsIndex();
            int index = 0;//这里设置一个假值

            TeacherHomeworkCheck thc = new TeacherHomeworkCheck(homIds, index, (string)lbNotTitle.Content, (string)lbStudentInfo.Content,true);//最后的true表示这属于已经批改的界面
            //这里的homId是要带给下一个界面的值,表示待的homId数组，其中index是正要批改的homId在homId数组中的索引
            thc.ShowDialog();
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            //注销程序
            App.Current.Shutdown();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            BreifView newBreifView = new BreifView(tbClassInfo.Text.ToString(),tbClassInfo1.Text.ToString(),tbTeacherInfo.Text.ToString(),tbTeacherInfo1.Text.ToString());
            newBreifView.Show();
            this.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
