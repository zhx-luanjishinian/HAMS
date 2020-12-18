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
using HAMS.Student.StudentUserControl;

namespace HAMS.Teacher.TeacherView
{
    /// <summary>
    /// AnswerQuestion.xaml 的交互逻辑
    /// </summary>
    public partial class AnswerQuestion : Window
    {
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
        public AnswerQuestion(String className)
        {
            InitializeComponent();
            labelClassName.Content = className;
           
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
                MessageBox.Show("successfully insert!");
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

        //private void btnSubmitQuestion_Click(object sender, RoutedEventArgs e)
        //{
        //    Student.StudentUserControl.StudentAskQuestion newStudentAskQuestion = new Student.StudentUserControl.StudentAskQuestion();

        //}
    }
}
