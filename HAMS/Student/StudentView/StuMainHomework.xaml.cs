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
using HAMS.Teacher.TeacherView;

namespace HAMS.Student.StudentView
{
    /// <summary>
    /// StuMainHomework.xaml 的交互逻辑
    /// </summary>
    public partial class StuMainHomework : Window
    {
        private SService ss = new SService();
        public String account { set; get; }
        public String name { set; get; }
        public String notId { set; get; }
        public String classId { set; get; }
        public String Message { set; get; }
        public StuMainHomework(String account,String name,String classId)
        {
            InitializeComponent();
            this.account = account;
            this.name = name;
            this.classId = classId;
            tbuserNameAc.Text = account + name;
            
            MainHomeworkShow(classId);
        }
        public void MainHomeworkShow(String clId)
        {
            Dictionary<int, List<String>> info = ss.showAllHomeworkInfo(clId);
            labelClassName.Content = info[info.Count-1][0];
            for(int i = 0; i < info.Count-1; i++)
            {
                String[] temp = new String[2];
                ListViewItem ivi = new ListViewItem();
                MainHomeworkInfo mhi = new MainHomeworkInfo(info[i][1], info[i][2]);
                temp[0] = info[i][1];
                temp[1] = info[i][2];
                mhi.labelHomeworkName.Content = info[i][0];
                //如果长度很长的话只显示7个
                if (info[i][1].Length > 7) {
                mhi.labelHomeworkDescription.Content = info[i][1].Substring(0,7)+"...";
                }
                else
                {
                    mhi.labelHomeworkDescription.Content = info[i][1];
                }
                notId = info[i][2].ToString();
                mhi.tbHomeworkStatus.Text = ss.judgeHomeworkStatus(account, notId, info[i][3].ToString());
                this.Message = mhi.tbHomeworkStatus.Text;
                mhi.btnHomRe1.Tag = temp;
                mhi.btnCheckRank.Tag = temp;
                mhi.btnHomeworkAnswer.Tag = temp;
                mhi.btnHomRe1.Click += new RoutedEventHandler(doHomework_Click);
                mhi.btnCheckRank.Click += new RoutedEventHandler(homRk);
                mhi.btnHomeworkAnswer.Click += new RoutedEventHandler(asq);
                ivi.Content = mhi;
                listview.Items.Add(ivi);
            }

        }
        //打开具体的做作业界面的点击事件
        private void doHomework_Click(object sender,RoutedEventArgs e)
        {
            Button mh = (Button)sender;
            String[] info = (String[])mh.Tag;
            if(Message != "已逾期"){
            StuDoHomework sdh = new StuDoHomework(account, name, info[1],classId);
            sdh.Show();
            this.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("该作业已逾期，无法再进行作答");
            }
        }
        //查看排行榜的点击事件（后面还要传参，这里目前是这样写)
        private void homRk(object sender, RoutedEventArgs e)
        {
            Button mh = (Button)sender;
            String[] info = (String[])mh.Tag;
            StuHomeworkRank shr = new StuHomeworkRank(account,name,info[1], classId);
            shr.Show();
            this.Visibility = Visibility.Hidden;
        }
       
        //打开作业答疑区的点击事件
        private void asq(object sender, RoutedEventArgs e)
        {
            Button mh = (Button)sender;
            String[] info = (String[])mh.Tag;
            //AnswerQuestion aq = new AnswerQuestion(info[0], info[1]);
            AnswerQuestion aq = new AnswerQuestion(info[0]);
            aq.Show();
            this.Visibility = Visibility.Hidden;
        }
       
        private void homeworkManagement_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StudentMainForm smf = new StudentMainForm(account,name);
                smf.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StudentMainForm smh = new StudentMainForm(account, name);
                smh.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

         private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void homeworkAlert_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                AlertForm af = new AlertForm(account,name);
                af.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            //首先删除listview里面的东西
            listview.Items.Clear();
            //然后再重新加载一遍
            MainHomeworkShow(classId);
        }
    }
}

