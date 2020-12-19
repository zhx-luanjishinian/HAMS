﻿using System;
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
using HAMS.Student.StudentDao;
using HAMS.Student.StudentService;
using HAMS.Student.StudentUserControl;
using HAMS.Student.StudentView;
using System.Data;

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// AnswerQuestion.xaml 的交互逻辑
    /// </summary>
    public partial class AnswerQuestion : Window
    {
        SDao sd = new SDao();
        public String notId { set; get; }
        public String classSpecId { set; get; }
        public String name { set; get; }
        public String account { set; get; }
        private SService ss = new SService();
        

        public AnswerQuestion(String account,String name,String classSpecId,String notId,String className)
        {
            InitializeComponent();
            labelClassName.Content = className;
            this.account = account;
            this.name = name;
            this.classSpecId = classSpecId;
            this.notId = notId;
            initAskView(notId);
            
        }
        public AnswerQuestion(String className,string teacherName,string notId)
        {
            InitializeComponent();
            labelClassName.Content = className;
            name = teacherName;
            this.notId = notId;
           
        }
        private void initAskView(String notId)
        {
            List<List<String>> result = ss.showAskQuestion(notId);
            for(int i = 0; i < result.Count; i++)
            {
             
                //当老师的评论不为空的时候
                if (result[i][1] != "")
                {
                    //添加一个listviewitem控件
                    ListViewItem ivi = new ListViewItem();
                    StudentAskQuestion saq = new StudentAskQuestion();
                    //首先放置学生的东西
                    saq.textBoxQuestion.Text = result[i][0];
                    //saq.textBoxQuestion.TextChanged += new TextChangedEventHandler();
                    //然后判断老师的评语是否为空，不为空就放置老师的评语
                    //不为空就加载老师的评语
                    saq.teacherResponse.Visibility = Visibility.Visible;
                    saq.tbResponse.Text = result[i][1];       

                    //添加content
                    ivi.Content = saq;
                    //添加子item控件
                    listViewQuestionAndAnswer.Items.Add(ivi);
                }
                else
                {
                    
                    //添加一个listviewitem控件
                    ListViewItem ivi = new ListViewItem();
                    StudentAskQuestion saq = new StudentAskQuestion();
                    //首先放置学生的东西
                    saq.textBoxQuestion.Text = result[i][0];
                    //添加点击事件
                    saq.btnInsert.Click += new RoutedEventHandler(btnInsert_Click);
                    //添加content
                    ivi.Content = saq;
                    //添加子item控件
                    listViewQuestionAndAnswer.Items.Add(ivi);
                }
                

                
            }

        }
        
        //btnInsert的点击事件
        private void btnInsert_Click(object sender,RoutedEventArgs e)
        {
            //进行信息的保存
            //TeacherAnswerQuestion newTeacherAnswerQuestion = new TeacherAnswerQuestion();
            Button sonBtn = (Button)sender;
            Canvas stuCanvas = (Canvas)sonBtn.Parent;
            StudentAskQuestion stuControl = (StudentAskQuestion)stuCanvas.Parent;
            //MessageBox.Show(stuControl.lbStuName.Content.ToString());   
            //往数据库里插入数据
            if (ss.insertStudentQuestion(notId, stuControl.textBoxQuestion.Text))
            {
                MessageBox.Show("发送答疑成功！");
            }
            else
            {
                MessageBox.Show("failed!");
            }
           

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button1_Click(object sender, RoutedEventArgs e)
        {
           // MessageBox.Show(account);
            if(account != null)
            {
                int sex = int.Parse(sd.getSexByStuSpecId(account).Rows[0][0].ToString());
                string pngfile;
                //headImage是image控件名
                if (sex == 0)
                {
                    pngfile = @"..\..\Resources\女生头像.png";

                }
                else
                {
                    pngfile = @"..\..\Resources\男生头像.png";

                }
                StuMainHomework sdh = new StuMainHomework(account, name, classSpecId, pngfile);
                sdh.Show();
                this.Visibility = System.Windows.Visibility.Hidden;

            }
            else
            {
                
                this.Visibility = System.Windows.Visibility.Hidden;
            }
            //CheckingClassHomework newHomeworkNoticeCheck = new CheckingClassHomework();
            //newHomeworkNoticeCheck.Show();
            //// 隐藏自己(父窗体)
            //this.Visibility = System.Windows.Visibility.Hidden;
            this.Close();
        }

        private void btnSubmitQuestion_Click(object sender, RoutedEventArgs e)
        {
            //发送答疑的时候就直接添加一个新的东西
            ListViewItem ivi = new ListViewItem();
            StudentAskQuestion saq = new StudentAskQuestion();
            saq.btnInsert.Click += new RoutedEventHandler(btnInsert_Click);
            //刚开始展示的时候赋值为空
            saq.textBoxQuestion.Text = "";
            ivi.Content = saq;
            listViewQuestionAndAnswer.Items.Add(ivi);
           
        }
        private void LoadQuestionAndAnswer(string noteId, AnswerQuestion newAnswerQuestion)
        {
            //进入答疑界面时加载目前已经有的疑问和解答
            TeacherDao.TDao td = new TeacherDao.TDao();
            DataTable table1 = td.getComment(noteId);
            //List<StudentAskQuestion> list = new List<StudentAskQuestion>();  //生成StudentAskQuestion动态数组
            //MessageBox.Show(table1.Rows.Count.ToString());    //
            StudentAskQuestion[] newStudentAskQuestion = new StudentAskQuestion[100];
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                newStudentAskQuestion[i] = new StudentAskQuestion();
                newStudentAskQuestion[i].lbStuName.Content = "本课堂学生";
                newStudentAskQuestion[i].textBoxQuestion.Text = table1.Rows[i][2].ToString();  //有问题
                newStudentAskQuestion[i].textBoxQuestion.IsReadOnly = true;
                newStudentAskQuestion[i].tbResponse.Text = table1.Rows[i][3].ToString();
                newStudentAskQuestion[i].btnComment.Click += new RoutedEventHandler(btnSubmitQuestion_Click);
                newStudentAskQuestion[i].btnInsert.Click += new RoutedEventHandler(btnbtnInsert_Click);
                newStudentAskQuestion[i].lbResponseName.Content = name + "老师";    //给老师姓名赋值
                newAnswerQuestion.listViewQuestionAndAnswer.Items.Add(newStudentAskQuestion[i]);
                //newAnswerQuestion.btnSubmitQuestion.Click += new RoutedEventHandler(btnSubmitQuestion_Click); //定义答疑按钮的事件
                if (newStudentAskQuestion[i].tbResponse.Text != "")
                {
                    newStudentAskQuestion[i].teacherResponse.Visibility = Visibility.Visible;
                    newStudentAskQuestion[i].tbResponse.IsReadOnly = true;
                    newStudentAskQuestion[i].btnComment.Visibility = Visibility.Hidden;
                    newStudentAskQuestion[i].btnInsert.Visibility = Visibility.Hidden;
                }
                else
                {
                    newStudentAskQuestion[i].teacherResponse.Visibility = Visibility.Hidden;
                }

            }

        }

        private void btnbtnInsert_Click(object sender, RoutedEventArgs e)
        {
            //进入答疑界面时加载目前已经有的疑问和解答
            TeacherDao.TDao td = new TeacherDao.TDao();
            //TeacherAnswerQuestion newTeacherAnswerQuestion = new TeacherAnswerQuestion();
            Button sonBtn = (Button)sender;
            Canvas stuCanvas = (Canvas)sonBtn.Parent;
            StudentAskQuestion stuControl = (StudentAskQuestion)stuCanvas.Parent;
            //MessageBox.Show(stuControl.lbStuName.Content.ToString());

            bool flag = td.UpdateComment(stuControl.tbResponse.Text, stuControl.textBoxQuestion.Text);
            //往数据库里插入数据
            if (flag == true)
            {
                MessageBox.Show("发送成功");
            }
            else
            {
                MessageBox.Show("发送失败");
            }
            stuControl.tbResponse.IsReadOnly = true;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

            //首先删除listview里面的东西
            listViewQuestionAndAnswer.Items.Clear();

            LoadQuestionAndAnswer(notId, this);   //加载疑问和回答
                                                               //然后再重新加载一遍
        }

    }
}
