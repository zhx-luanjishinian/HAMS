using HAMS.Teacher.TeacherUserControl;
using HAMS.Student.StudentUserControl;
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
using HAMS.Entity;

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// CheckingClassHomework.xaml 的交互逻辑
    /// </summary>
    public partial class CheckingClassHomework : Window
    {
        private int[] homIdCorrecteds;//已批改作业学生的homId数组
        private int[] homIdNeedCorrects;//待批改作业学生的homId数组
        private int[] homIdUnfinisheds;//未完成作业学生的homId数组

        private String[] stuSpecIdCorrecteds;//已批改作业学生的stuId数组
        private String[] stuNameCorrecteds;//已批改作业学生的stuName数组

        private String[] stuSpecIdNeedCorrects;//待批改作业学生的stuId数组
        private String[] stuNameNeedCorrects;//待批改作业学生的stuName数组

        private String[] stuSpecIdUnfinisheds;//未完成作业学生的stuId数组
        private String[] stuNameUnfinisheds;//未完成作业学生的stuName数组

        private string notId;//当前作业公告Id

        //private string teacherName;

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

            //获得notid
            DataTable tableClassId = td.getClassId(classSpecId);
            DataTable tableNotId = td.getNotIdByClassIdAndNotTitle(homeworkTitle, Convert.ToInt32(tableClassId.Rows[0][0]));
            notId = tableNotId.Rows[0][0].ToString();



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
            //定义存储学生列表对应stuId的int数组
            stuSpecIdCorrecteds = new String[stuListLength];
            //定义存储学生列表对应stuName的int数组
            stuNameCorrecteds = new String[stuListLength];
            for (int i = 0; i < stuListLength; i++)
            {
                checkedStudent[i] = new StudentCheck(i);
                String stuId = table3.Rows[i][0].ToString();
                

                DataTable table4 = td.GetStudentNameAndIdByStuID(stuId);
                checkedStudent[i].lbStudentInfo1.Content = table4.Rows[0][0].ToString();
                stuSpecIdCorrecteds[i] = table4.Rows[0][0].ToString();
                
                checkedStudent[i].lbStudentInfo2.Content = table4.Rows[0][1].ToString();
                stuNameCorrecteds[i] = table4.Rows[0][1].ToString();

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
            //定义存储学生列表对应stuId的int数组
            stuSpecIdNeedCorrects = new String[stuListLength];
            //定义存储学生列表对应stuName的int数组
            stuNameNeedCorrects = new String[stuListLength];
            for (int i = 0; i < stuListLength; i++)
            {
                checkedStudent[i] = new StudentCheck(i);
                String stuId = table7.Rows[i][0].ToString();
                DataTable table8 = td.GetStudentNameAndIdByStuID(stuId );
                checkedStudent[i].lbStudentInfo1.Content = table8.Rows[0][0].ToString();
                stuSpecIdNeedCorrects[i] = table8.Rows[0][0].ToString();

                checkedStudent[i].lbStudentInfo2.Content = table8.Rows[0][1].ToString();
                stuNameNeedCorrects[i] = table8.Rows[0][1].ToString();
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
            //定义存储学生列表对应stuId的int数组
            stuSpecIdUnfinisheds = new String[stuListLength];
            //定义存储学生列表对应stuName的int数组
            stuNameUnfinisheds = new String[stuListLength];
            for (int i = 0; i < stuListLength; i++)
            {
                checkedStudent[i] = new StudentCheck(i);
                string stuId = table3.Rows[i][0].ToString();
                DataTable table4 = td.GetStudentNameAndIdByStuID(stuId);
                checkedStudent[i].lbStudentInfo1.Content = table4.Rows[0][0].ToString();
                stuSpecIdUnfinisheds[i] = table4.Rows[0][0].ToString();

                checkedStudent[i].lbStudentInfo2.Content = table4.Rows[0][1].ToString();
                stuNameUnfinisheds[i] = table4.Rows[0][1].ToString();

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
            newAnswerQuestion.btnSubmitQuestion.Visibility = Visibility.Hidden;
            newAnswerQuestion.Show();
            LoadQuestionAndAnswer(notId, newAnswerQuestion);

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

        private void LoadQuestionAndAnswer(string noteId, AnswerQuestion newAnswerQuestion)
        {
            //进入答疑界面时加载目前已经有的疑问和解答
            DataTable table1 = td.getComment(noteId);
            //List<StudentAskQuestion> list = new List<StudentAskQuestion>();  //生成StudentAskQuestion动态数组
            //MessageBox.Show(table1.Rows.Count.ToString());    //
            StudentAskQuestion[] newStudentAskQuestion = new StudentAskQuestion[50];
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                newStudentAskQuestion[i] = new StudentAskQuestion();
                newStudentAskQuestion[i].lbStuName.Content = "本课堂学生";
                newStudentAskQuestion[i].textBoxQuestion.Text = table1.Rows[i][2].ToString();  //有问题
                newStudentAskQuestion[i].btnComment.Click += new RoutedEventHandler(btnSubmitQuestion_Click);
                newStudentAskQuestion[i].btnInsert.Click += new RoutedEventHandler(btnbtnInsert_Click);
                newStudentAskQuestion[i].lbResponseName.Content = tbTeacherInfo1.Text;    //给老师姓名赋值
                newAnswerQuestion.listViewQuestionAndAnswer.Items.Add(newStudentAskQuestion[i]);
                //newAnswerQuestion.btnSubmitQuestion.Click += new RoutedEventHandler(btnSubmitQuestion_Click); //定义答疑按钮的事件
                newStudentAskQuestion[i].teacherResponse.Visibility = Visibility.Hidden;
            }

        }

        private void btnSubmitQuestion_Click(object sender, RoutedEventArgs e)
        {
            //TeacherAnswerQuestion newTeacherAnswerQuestion = new TeacherAnswerQuestion();
            Button sonBtn = (Button)sender;
            Canvas stuCanvas = (Canvas)sonBtn.Parent;
            StudentAskQuestion stuControl = (StudentAskQuestion)stuCanvas.Parent;
            //MessageBox.Show(stuControl.lbStuName.Content.ToString());
            stuControl.teacherResponse.Visibility = Visibility.Visible;  //点击，此时可见

        }
        private void btnbtnInsert_Click(object sender, RoutedEventArgs e)
        {
            //TeacherAnswerQuestion newTeacherAnswerQuestion = new TeacherAnswerQuestion();
            Button sonBtn = (Button)sender;
            Canvas stuCanvas = (Canvas)sonBtn.Parent;
            StudentAskQuestion stuControl = (StudentAskQuestion)stuCanvas.Parent;
            //MessageBox.Show(stuControl.lbStuName.Content.ToString());
           
            bool flag = td.UpdateComment(stuControl.tbResponse.Text, stuControl.textBoxQuestion.Text);
            //往数据库里插入数据
            if(flag ==true)
            {
                MessageBox.Show("successfully insert!");
            }
            else
            {
                MessageBox.Show("failed!");
            }
            stuControl.tbResponse.IsReadOnly = true;
        }


        private void btnHomeworkStatistic_Click(object sender, RoutedEventArgs e)
        {
            HomeworkStatistic hs = new HomeworkStatistic();
            hs.tSpecId = tbClassInfo.Text;
            hs.tName = tbClassInfo1.Text;
            hs.tbNotTitle.Text = lbNotTitle.Content.ToString();
            hs.className = tbClassInfo1.Text;
            hs.classSpecId = tbClassInfo.Text;
            hs.description = textBlockDescription.Text;
            hs.notId = this.notId;//传递作业公告id
            hs.Show();
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void tbSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            //实现鼠标聚焦时文本消失
            tbStuNameSearch.Text = "";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //存在问题，如果有重名则只能找到第一个
            //非空判断
            if (tbStuNameSearch.Text == null)
            {
                MessageBox.Show("请输入查询条件");
            }
            else 
            {
                string stuName = tbStuNameSearch.Text;
                int i;
                for ( i = 0; i < stuNameCorrecteds.Length; i++)//首先在已批改作业学生姓名中找
                {
                    if(stuNameCorrecteds[i] == stuName)
                    {
                        StudentCheck sc = new StudentCheck(i);//i恰好为在homIdCorrecteds中的下标（关于Correcteds的三个数组下标是对应的）
                        sc.lbStudentInfo1.Content = stuSpecIdCorrecteds[i];
                        sc.lbStudentInfo2.Content = stuNameCorrecteds[i];
                        sc.lbHomeworkState1.Content = "已批改";
                        sc.btnHomeworkCorrect1.Content = "检查作业";//修改button名称
                        TbItemChecked.IsSelected = true;
                        listViewChecked.Items.Add(sc);                                                           
                        sc.btnHomeworkCorrect1.Click += new RoutedEventHandler(btnHomeworkCorrect1_Click);
                        
                       
                        break;
                    }
                }
                for (i = 0; i < stuNameNeedCorrects.Length; i++)//然后在待批改作业学生姓名中找
                {
                   if (stuNameNeedCorrects[i] == stuName)
                   {
                            break;
                   }
                 }
              
                 for (i = 0; i < stuNameUnfinisheds.Length; i++)//最后在未完成作业学生姓名中找
                 {
                     if (stuNameUnfinisheds[i] == stuName)
                     {
                          break;
                     }
                 }
                     
            }
        }
    }
}
