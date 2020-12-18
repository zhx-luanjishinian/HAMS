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
        
        public string pngfile;
        public StuMainHomework(String account, String name, String classId, string pngfile)//真实课堂号
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.account = account;
            this.name = name;
            this.classId = classId;
            this.pngfile = pngfile;
            tbuserNameAc.Text = account + name;
            //设置该img控件的Source
            headImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(System.IO.Path.Combine(System.Environment.CurrentDirectory, pngfile))));
            MainHomeworkShow(classId);
        }


        //显示主页面，加载动态控件,参数是classId
        public void MainHomeworkShow(String clId)
        {
            Dictionary<int, List<String>> info = ss.showAllHomeworkInfo(clId); //返回的是所有作业的信息列表+【作业数量，课堂名】
            labelClassName.Content = info[info.Count-1][0]; //取课堂名，显示在前端课堂名的位置
            for(int i = 0; i < info.Count-1; i++) //对作业进行遍历，遍历初始化动态控件，并展示在前端界面listview位置
            {
                String[] temp = new String[4]; //定义一个字符串数组temp
                ListViewItem ivi = new ListViewItem();  //初始化一个listview元素，用来装一条一条的作业
                //notTitle,content,notId,truDeadline
               //this.Message = ss.judgeHomeworkStatus(account, notId, info[i][3].ToString());
                MainHomeworkInfo mhi = new MainHomeworkInfo(info[i][1], info[i][2]); //传入两个参数，一个是content,一个是notId
                //mhi.tbHomeworkStatus.Text = this.Message;
                
                temp[0] = info[i][1]; //获取content的值
                temp[1] = info[i][2]; //获取作业公告Id
                this.notId = info[i][2];
                temp[2] = info[info.Count - 1][0];//获取课堂名
                temp[3] = ss.judgeHomeworkStatus(account, notId, info[i][3].ToString());//获得作业的批改状态传到下一个值

                mhi.labelHomeworkName.Content = info[i][0];//将作业公告的标题notTitle显示在作业公告名这个前端控件中
                //如果作业内容长度很长的话只显示7个
                if (info[i][1].Length > 7) {
                    mhi.labelHomeworkDescription.Content = info[i][1].Substring(0,7)+"...";//将作业内容content值显示在对应的控件中
                }
                else
                {
                    mhi.labelHomeworkDescription.Content = info[i][1];//不超过7条就全部展示
                }
                //notId值的获取
                notId = info[i][2].ToString();
                //根据学生真实学号、还有作业Id、还有真实截止时间对作业状态进行判断
                //返回值为作业状态字符串，将该字符串赋值给作业状态的textBlock
                mhi.tbHomeworkStatus.Text = ss.judgeHomeworkStatus(account, info[i][2], info[i][3].ToString());
         
                mhi.btnHomRe1.Tag = temp;
                mhi.btnCheckRank.Tag = temp;
                mhi.btnHomeworkAnswer.Tag = temp;
                mhi.btnHomRe1.Click += new RoutedEventHandler(doHomework_Click);
                mhi.btnCheckRank.Click += new RoutedEventHandler(homRk);
                mhi.btnHomeworkAnswer.Click += new RoutedEventHandler(asq);      //答疑区的按钮


                ivi.Content = mhi;
                listview.Items.Add(ivi);
            }

        }
        //打开具体的做作业界面的点击事件
        private void doHomework_Click(object sender,RoutedEventArgs e)
        {
            Button mh = (Button)sender;
            String[] info = (String[])mh.Tag;
            StuDoHomework sdh = new StuDoHomework(account, name, info[1], classId,pngfile,info[3]);//这里的classId是真实课堂号
         
            //String account, String name,String notId, String classId,String message
            sdh.Show();
            this.Visibility = Visibility.Hidden;
            //如果作业逾期了的话，就跳转之后进行弹窗提示
            if (info[3] == "已逾期")
            {
                MessageBox.Show("该作业已逾期，无法再进行作答");
            }
        }
        //查看排行榜的点击事件（后面还要传参，这里目前是这样写)
        private void homRk(object sender, RoutedEventArgs e)
        {
            Button mh = (Button)sender;
            String[] info = (String[])mh.Tag;
            StuHomeworkRank shr = new StuHomeworkRank(account,name,info[1], classId, this.pngfile);
            shr.pngfile = this.pngfile;
            shr.Show();
            this.Visibility = Visibility.Hidden;
        }

        // 打开作业答疑区的点击事件
        private void asq(object sender, RoutedEventArgs e)
        {
            Button mh = (Button)sender;
   
            String[] info = (String[])mh.Tag;
            //获取的直接是作业id
            AnswerQuestion aq = new AnswerQuestion(account,name,classId, info[1],info[2]);
            aq.Show();
            //AnswerQuestion aq = new AnswerQuestion(info[0]);
            this.Visibility = Visibility.Hidden;
        }


        private void homeworkManagement_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StudentMainForm smf = new StudentMainForm(account,name,this.pngfile);
                smf.pngfile = this.pngfile;
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
                StudentMainForm smh = new StudentMainForm(account, name, this.pngfile);
                smh.pngfile = this.pngfile;
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
                AlertForm af = new AlertForm(account,name,this.pngfile);
                af.pngfile = this.pngfile;
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

