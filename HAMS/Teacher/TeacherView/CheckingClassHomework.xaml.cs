using HAMS.Teacher.TeacherUserControl;
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
    /// CheckingClassHomework.xaml 的交互逻辑
    /// </summary>
    public partial class CheckingClassHomework : Window
    {
        TeacherService.TService ts = new TeacherService.TService();
        TeacherDao.TDao td = new TeacherDao.TDao();
        public CheckingClassHomework()
        {
            InitializeComponent();
        }

        public CheckingClassHomework(string homeworkTitle, string description, string teacherSpecId, string teacherName, string classSpecId, string className)
        {
            //生成基本信息
            InitializeComponent();
            lbNotTitle.Content = homeworkTitle;
            textBlockDescription.Text = description;
            tbTeacherInfo.Text = teacherSpecId;
            tbTeacherInfo1.Text = teacherName;
            tbClassInfo.Text = classSpecId;
            tbClassInfo1.Text = className;
            labelHomeworkArrangeTime.Content = "发布时间：" + ts.PasteSubmitTimeInForm(classSpecId, homeworkTitle);
            //加载已批改的动态控件
            LoadFinished(classSpecId, homeworkTitle);
            //加载待批改的动态控件
            LoadNeedCorrect(classSpecId, homeworkTitle);
            //加载未完成的动态控件
            LoadUnfinished(classSpecId, homeworkTitle);

        }

        public void LoadFinished(string classSpecId, string homeworkTitle)
        {
            DataTable table1 = td.getClassId(classSpecId);
            int classId = Convert.ToInt32(table1.Rows[0][0]);
            DataTable table2 = td.getNotIdByClassIdAndNotTitle(homeworkTitle, classId);
            //获得noteId
            DataTable table3 = td.SelectHomeworkCheckedInfo(table2.Rows[0][0].ToString());
            //MessageBox.Show(table3.Rows.Count.ToString());
            int checkedNum = table3.Rows.Count;
            //加载已批改的动态控件
            TbItemChecked.Header = "已批改   " + checkedNum;

            StudentCheck[] checkedStudent = new StudentCheck[50];
            for (int i = 0; i < table3.Rows.Count; i++)
            {
                checkedStudent[i] = new StudentCheck();
                DataTable table4 = td.GetStudentNameAndIdByStuID(table3.Rows[i][0].ToString());
                checkedStudent[i].lbStudentInfo1.Content = table4.Rows[0][0].ToString();
                //加载作业标题
                checkedStudent[i].lbStudentInfo2.Content = table4.Rows[0][1].ToString();
                checkedStudent[i].lbHomeworkState1.Content = "已批改";
                //为什么这里不能向listview中加数据
                listViewChecked.Items.Add(checkedStudent[i]);
                checkedStudent[i].btnHomeworkCorrect1.Click += new RoutedEventHandler(btnHomeworkCorrect1_Click);
            }
        }

        private void btnHomeworkCorrect1_Click(object sender, RoutedEventArgs e)
        {
            Button sonBtn = (Button)sender;
            Canvas stuCanvas = (Canvas)sonBtn.Parent;
            StudentCheck stuControl = (StudentCheck)stuCanvas.Parent;
            //获得学生姓名
            string studentName = stuControl.lbStudentInfo2.Content.ToString();
            //获得学生学号
            string studentId = stuControl.lbStudentInfo1.Content.ToString();
            //还需要写根据学号得作业提交描述
            string postil = ts.GetPostilByForm(tbClassInfo.Text,lbNotTitle.Content.ToString(),studentId);
            MessageBox.Show(postil);

            //TeacherHomeworkCheck newTeacherHomeworkCheck = new TeacherHomeworkCheck(lbNotTitle.Content.ToString(),tbTeacherInfo.Text,tbTeacherInfo1.Text,studentId,studentName,"1111");
            //newTeacherHomeworkCheck.Show();
        }

        public void LoadNeedCorrect(string classSpecId, string homeworkTitle)
        {
            DataTable table5 = td.getClassId(classSpecId);
            int classId = Convert.ToInt32(table5.Rows[0][0]);
            DataTable table6 = td.getNotIdByClassIdAndNotTitle(homeworkTitle, classId);
            //获得noteId
            //MessageBox.Show(table2.Rows[0][0].ToString());
            DataTable table7 = td.SelectHomeworkNeedCorrectInfo(table6.Rows[0][0].ToString());
            //MessageBox.Show(table3.Rows.Count.ToString());
            int checkedNum = table7.Rows.Count;
            //MessageBox.Show(checkedNum.ToString());
            //加载已批改的动态控件
            TbItemUnCheck.Header = "待批改   " + checkedNum;

            StudentCheck[] checkedStudent = new StudentCheck[50];
            for (int i = 0; i < table7.Rows.Count; i++)
            {
                checkedStudent[i] = new StudentCheck();
                DataTable table8 = td.GetStudentNameAndIdByStuID(table7.Rows[i][0].ToString());
                checkedStudent[i].lbStudentInfo1.Content = table8.Rows[0][0].ToString();
                //加载作业标题
                checkedStudent[i].lbStudentInfo2.Content = table8.Rows[0][1].ToString();
                checkedStudent[i].lbHomeworkState1.Content = "待批改";
                //为什么这里不能向listview中加数据
                listViewUnCheck.Items.Add(checkedStudent[i]);
            }
        }

        public void LoadUnfinished(string classSpecId, string homeworkTitle)
        {
            DataTable table1 = td.getClassId(classSpecId);
            int classId = Convert.ToInt32(table1.Rows[0][0]);
            DataTable table2 = td.getNotIdByClassIdAndNotTitle(homeworkTitle, classId);
            //获得noteId
            DataTable table3 = td.SelectHomeworkUnfinishedInfo(table2.Rows[0][0].ToString());
            //MessageBox.Show(table3.Rows.Count.ToString());
            int checkedNum = table3.Rows.Count;
            //加载已批改的动态控件
            TbItemUnFinish.Header = "未完成   " + checkedNum;

            StudentCheck[] checkedStudent = new StudentCheck[50];
            for (int i = 0; i < table3.Rows.Count; i++)
            {
                checkedStudent[i] = new StudentCheck();
                DataTable table4 = td.GetStudentNameAndIdByStuID(table3.Rows[i][0].ToString());
                checkedStudent[i].lbStudentInfo1.Content = table4.Rows[0][0].ToString();
                //加载作业标题
                checkedStudent[i].lbStudentInfo2.Content = table4.Rows[0][1].ToString();
                checkedStudent[i].lbHomeworkState1.Content = "未完成";
                //为什么这里不能向listview中加数据
                listViewUnFinish.Items.Add(checkedStudent[i]);
            }
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

        private void btnAnswerQuestion_Click(object sender, RoutedEventArgs e)
        {
            AnswerQuestion newAnswerQuestion = new AnswerQuestion(tbClassInfo1.Text);

            newAnswerQuestion.Show();

           // this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            //首先删除listview里面的东西
            listViewChecked.Items.Clear();
            listViewUnCheck.Items.Clear();
            listViewUnFinish.Items.Clear();
            //然后再重新加载一遍

            //加载已批改的动态控件
            LoadFinished(tbClassInfo.Text, lbNotTitle.Content.ToString());
            //加载待批改的动态控件
            LoadNeedCorrect(tbClassInfo.Text, lbNotTitle.Content.ToString());
            //加载未完成的动态控件
            LoadUnfinished(tbClassInfo.Text, lbNotTitle.Content.ToString());
        }
    }
}
