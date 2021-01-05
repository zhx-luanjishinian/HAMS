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
        //创建SService类的实体，方便类中进行调用
        public string pngfile;
        //用于获取用户头像的路径
        public string account { set; get; }
        //学生的账号，也就是对应数据库中的stuSpecId
        public string name { set; get; }
        //获取学生姓名
        public String cls { set; get; }
        //此处值指的是课堂号
        public String notId { set; get; }
        //作业公告Id，该值无法从前一界面获取，作用是通过赋值传递给下一界面

        //学生主界面的构造函数
        public StudentMainForm(string account,string name,string pgfile)
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;//窗口最大化
            this.pngfile = pgfile;//头像文件路径
            //设置该img控件的Source
            headImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(System.IO.Path.Combine(System.Environment.CurrentDirectory, pngfile))));
            this.account = account;//获取传入的学生stuSpecId的值
            this.name = name;//获取学生姓名
            textBlockUserId.Text = account+" "+name;//将学号+姓名传给前端textBlock控件进行界面展示
            mainShow(); //调用MainShow函数加载学生所选课程展示动态控件
            homeWorkShow();//调用HomeWorkShow函数加载作业公告动态控件
        }


        //作业公告生成调用
        public void homeWorkShow()
        {
            List<List<String>> result = sts.showHomeNoticeInfo(this.account);//将该学生的全部作业公告放在list中进行存储
            //作业公告标题、Id，每门课课堂号
            for (int i = 0; i < result.Count; i++)
            {
                ListViewItem ivi = new ListViewItem();
                //定义一个数组用来放东西
                String[] temp = new String[2];
                this.notId = result[i][1];//存放查到的notId
                this.cls = result[i][2];//存放classId
                temp[0] = result[i][1];
                temp[1] = result[i][2];
                HomeworkNoteInfo hni = new HomeworkNoteInfo(notId, cls);//调用动态控件的构造函数进行动态控件的加载
                hni.btnRecntNo1.Tag = temp;
                hni.btnRecntNo1.Click += new RoutedEventHandler(homeworkNote_Click);//按钮事件的绑定
                hni.lab1.Content = result[i][0];//将动态控件的Lab1中的content值设为notId进行展示
                ivi.Content = hni;//将动态控件作为listview的一个content
                listView.Items.Add(ivi); //将动态控件放在listview里，从而实现动态加载
            }
        }


        //所选课程展示动态控件加载
        public void mainShow()
        {
            Dictionary<int, List<string>> info = sts.showCourseInfo(this.account);//调用sservise中的函数并将结果保存在字典中
            //字典中的值和顺序：教师名、学院、真实课堂号、课堂名
            for(int i = 0; i < info.Count; i++) //遍历字典中的值
            {
                ListViewItem ivi = new ListViewItem(); //新建一个listview控件
                cls = info[i][2]; //将真实课堂号赋值给cls
                MainInfo mf = new MainInfo(cls); //动态控件的生成，参数为真实课堂号
                mf.Name = "mf_" + i;//新控件命名为mf_0,mf_1等等依次增加
                mf.labelClassId1.Content = info[i][2];//将动态控件中需要展示真实课堂号的label中写入真实课堂号
                mf.textBlockClassName1.Text = info[i][3];//将动态控件中需要展示课堂名的地方展示对应的课堂名
                mf.class1.Tag = cls;
                mf.class1.Click += new RoutedEventHandler(btnStuMainHomework_Click);//按钮的绑定
                mf.textBlockDepartmentName1.Text = info[i][1];//将动态控件中需要展示院系的地方展示对应的院系
                mf.textBlockTeacherName1.Text = info[i][0];//将动态控件中需要展示教师名的地方展示对应的教师名
                ivi.Content = mf;//将动态控件作为listview中的一个content
                listView2.Items.Add(ivi);//将动态控件放在listview里，从而实现动态加载
            }
            
        }


        //主界面点击到达stuMainHomework界面的点击事件
        private void btnStuMainHomework_Click(object sender, RoutedEventArgs e)
        {
            Button mf = (Button)sender;
            StuMainHomework smh = new StuMainHomework(account, name, (String)mf.Tag,pngfile);//调用构造函数进行界面创建，真实课堂号
            smh.pngfile = this.pngfile;//给下一个界面头像传值
            smh.Show();//下一个界面的展示
            this.Visibility = Visibility.Hidden;//上一个界面的隐藏

        }

        //作业公告的按钮点击事件
        private void homeworkNote_Click(object sender , RoutedEventArgs e)
        {
            //记录生成的是哪个动态控件
            Button hnif = (Button)sender;
            String[] info = (String[])hnif.Tag;
            //跳转到查看作业公告界面，也就是doHomework界面
            //调用该界面的构造函数进行界面的初始化
            StuDoHomework sdh = new StuDoHomework(account,name,info[0],info[1],pngfile);
            sdh.pngfile = this.pngfile;//给下一个界面头像传值
            sdh.Show();//下一个界面的显示
            this.Visibility = Visibility.Hidden;//上一个界面的隐藏
        }
       
        //点击作业预警按钮跳转
        private void btnHomeworkAlert_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                AlertForm af = new AlertForm(account,name,pngfile);
                af.pngfile = this.pngfile;
                af.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        //点击作业管理按钮跳转
        private void btnHomeworkMana_Click(object sender, RoutedEventArgs e)
        {

        }

        //注销按钮，退出系统
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

       

        

        
        private void ListView2_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
