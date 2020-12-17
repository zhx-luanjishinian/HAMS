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
        private int[] homIdCorrecteds;//已完成作业学生的homId数组
        private int[] homIdNeedCorrects;//已完成作业学生的homId数组
        private int[] homIdUnfinisheds;//已完成作业学生的homId数组
        private string notId;//当前作业公告Id

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
            LoadCorrected(classSpecId, homeworkTitle);
            //加载待批改的动态控件
            LoadNeedCorrect(classSpecId, homeworkTitle);
            //加载未完成的动态控件
            LoadUnfinished(classSpecId, homeworkTitle);

            //对查看相关附件按钮进行初始化：能够实现鼠标放上去之后显示作业附件的名称
            //根据notId获得notURLName
            string notURLName = td.getNotURLNameByNotId(int.Parse(notId)).Rows[0][0].ToString();
            //加载作业附件名
            if(notURLName == "")
            {
                labelAccessoryName1.ToolTip = "该作业公告无作业附件";
            }
            else
            {
                labelAccessoryName1.ToolTip = notURLName;
            }
            

        }

        public void LoadCorrected(string classSpecId, string homeworkTitle)
        {
            DataTable table1 = td.getClassId(classSpecId);
            int classId = Convert.ToInt32(table1.Rows[0][0]);
            DataTable table2 = td.getNotIdByClassIdAndNotTitle(homeworkTitle, classId);
            //获得notId
            this.notId = table2.Rows[0][0].ToString();
            DataTable table3 = td.SelectHomeworkCheckedInfo(notId);
            //MessageBox.Show(table3.Rows.Count.ToString());
            int checkedNum = table3.Rows.Count;
            //加载已批改的动态控件
            TbItemChecked.Header = "已批改   " + checkedNum;
            //获取学生列表的长度
            int stuListLength = table3.Rows.Count;

            //定义动态生成控件的数组，长度与学生列表长一致
            StudentCheck[] checkedStudent = new StudentCheck[stuListLength];

            //定义存储学生列表对应homId的int数组
            homIdCorrecteds = new int[stuListLength];
            for (int i = 0; i < stuListLength; i++)
            {
                checkedStudent[i] = new StudentCheck(i);
                String stuId = table3.Rows[i][0].ToString();
                DataTable table4 = td.GetStudentNameAndIdByStuID(stuId);
                checkedStudent[i].lbStudentInfo1.Content = table4.Rows[0][0].ToString();
                //加载作业标题
                checkedStudent[i].lbStudentInfo2.Content = table4.Rows[0][1].ToString();
                checkedStudent[i].lbHomeworkState1.Content = "已批改";
                //为什么这里不能向listview中加数据
                listViewChecked.Items.Add(checkedStudent[i]);
                checkedStudent[i].btnHomeworkCorrect1.Click += new RoutedEventHandler(btnHomeworkCorrect1_Click);

                //根据stuId和notId查询homId,然后保存在homIds这个数组中
                DataTable tbHomId = td.GetHomIdByStuIdAndNotId(stuId,notId);
                int homId = int.Parse(tbHomId.Rows[0][0].ToString());
                homIdCorrecteds[i] = homId;//保存homId到数组
            }
        }

        private void btnHomeworkCorrect1_Click(object sender, RoutedEventArgs e)//检查作业按钮
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
            //MessageBox.Show(postil);
            int index = stuControl.index;
            string notTitle = lbNotTitle.Content.ToString();
            string studentInfo = stuControl.lbStudentInfo1.Content.ToString()+" "+stuControl.lbStudentInfo2.Content.ToString() ;
            bool ifCorrect ;//表示是否进行了作业批改
            if (stuControl.btnHomeworkCorrect1.Content.ToString() == "检查作业")//说明作业已被批改，需要查询出之前的批改记录
            {
                ifCorrect = true;
                //需要传入的是已批改的homIds列表
                TeacherHomeworkCheck newTeacherHomeworkCheck = new TeacherHomeworkCheck(homIdCorrecteds, index, notTitle, studentInfo, ifCorrect);
                newTeacherHomeworkCheck.className = tbClassInfo1.Text;
                newTeacherHomeworkCheck.classSpecId = tbClassInfo.Text;
                newTeacherHomeworkCheck.description = textBlockDescription.Text;
                newTeacherHomeworkCheck.Show();
            }
            else
            {
                ifCorrect = false;
                //需要传入的是待批改的homIds列表
                TeacherHomeworkCheck newTeacherHomeworkCheck = new TeacherHomeworkCheck(homIdNeedCorrects, index, notTitle, studentInfo, ifCorrect);
                newTeacherHomeworkCheck.className = tbClassInfo1.Text;
                newTeacherHomeworkCheck.classSpecId = tbClassInfo.Text;
                newTeacherHomeworkCheck.description = textBlockDescription.Text;
                newTeacherHomeworkCheck.Show();
            }
            
            
            this.Visibility = System.Windows.Visibility.Hidden;
            
        }

        public void LoadNeedCorrect(string classSpecId, string homeworkTitle)
        {
            DataTable table5 = td.getClassId(classSpecId);
            int classId = Convert.ToInt32(table5.Rows[0][0]);
            DataTable table6 = td.getNotIdByClassIdAndNotTitle(homeworkTitle, classId);
            //获得noteId
            this.notId =  table6.Rows[0][0].ToString();
            DataTable table7 = td.SelectHomeworkNeedCorrectInfo(notId);
            //MessageBox.Show(table3.Rows.Count.ToString());
            int checkedNum = table7.Rows.Count;
            //MessageBox.Show(checkedNum.ToString());
            //加载已批改的动态控件
            TbItemUnCheck.Header = "待批改   " + checkedNum;

            int stuListLength = table7.Rows.Count;
            StudentCheck[] checkedStudent = new StudentCheck[stuListLength];

            //定义存储学生列表对应homId的int数组
            homIdNeedCorrects = new int[stuListLength];
            for (int i = 0; i < stuListLength; i++)
            {
                checkedStudent[i] = new StudentCheck(i);
                String stuId = table7.Rows[i][0].ToString();
                DataTable table8 = td.GetStudentNameAndIdByStuID(stuId );
                checkedStudent[i].lbStudentInfo1.Content = table8.Rows[0][0].ToString();
                //加载作业标题
                checkedStudent[i].lbStudentInfo2.Content = table8.Rows[0][1].ToString();
                checkedStudent[i].lbHomeworkState1.Content = "待批改";
                checkedStudent[i].btnHomeworkCorrect1.Content = "批改作业";//修改button名称
                //为什么这里不能向listview中加数据
                listViewUnCheck.Items.Add(checkedStudent[i]);
                checkedStudent[i].btnHomeworkCorrect1.Click += new RoutedEventHandler(btnHomeworkCorrect1_Click);

                //根据stuId和notId查询homId,然后保存在homIds这个数组中
                DataTable tbHomId = td.GetHomIdByStuIdAndNotId(stuId, notId);
                int homId = int.Parse(tbHomId.Rows[0][0].ToString());
                homIdNeedCorrects[i] = homId;//保存homId到数组
            }
        }

        public void LoadUnfinished(string classSpecId, string homeworkTitle)
        {
            DataTable table1 = td.getClassId(classSpecId);
            int classId = Convert.ToInt32(table1.Rows[0][0]);
            DataTable table2 = td.getNotIdByClassIdAndNotTitle(homeworkTitle, classId);
            //获得noteId
            this.notId = table2.Rows[0][0].ToString();
            DataTable table3 = td.SelectHomeworkUnfinishedInfo(notId);
            //MessageBox.Show(table3.Rows.Count.ToString());
            int checkedNum = table3.Rows.Count;
            //加载已批改的动态控件
            TbItemUnFinish.Header = "未完成   " + checkedNum;

            int stuListLength = table3.Rows.Count;
            StudentCheck[] checkedStudent = new StudentCheck[stuListLength];

            //定义存储学生列表对应homId的int数组
            homIdUnfinisheds = new int[stuListLength];
            for (int i = 0; i < stuListLength; i++)
            {
                checkedStudent[i] = new StudentCheck(i);
                string stuId = table3.Rows[i][0].ToString();
                DataTable table4 = td.GetStudentNameAndIdByStuID(stuId);
                checkedStudent[i].lbStudentInfo1.Content = table4.Rows[0][0].ToString();
                //加载作业标题
                checkedStudent[i].lbStudentInfo2.Content = table4.Rows[0][1].ToString();
               
                checkedStudent[i].lbHomeworkState1.Content = "";
                checkedStudent[i].btnHomeworkCorrect1.Content = "";//修改button名称
                //为什么这里不能向listview中加数据
                listViewUnFinish.Items.Add(checkedStudent[i]);

                //根据stuId和notId查询homId,然后保存在homIds这个数组中
                DataTable tbHomId = td.GetHomIdByStuIdAndNotId(stuId, notId);
                int homId = int.Parse(tbHomId.Rows[0][0].ToString());
                homIdUnfinisheds[i] = homId;//保存homId到数组
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
            LoadCorrected(tbClassInfo.Text, lbNotTitle.Content.ToString());
            //加载待批改的动态控件
            LoadNeedCorrect(tbClassInfo.Text, lbNotTitle.Content.ToString());
            //加载未完成的动态控件
            LoadUnfinished(tbClassInfo.Text, lbNotTitle.Content.ToString());
        }
    }
}
