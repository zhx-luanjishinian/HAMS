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
        public string pngfile;
        public string account { set; get; }
        public string name { set; get; }
        //此处值指的是课堂号
        public String cls { set; get; }
        public String notId { set; get; }
       

        public StudentMainForm(string account,string name,string pgfile)

        {
            InitializeComponent();
            this.pngfile = pgfile;
            //设置该img控件的Source
            headImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(System.IO.Path.Combine(System.Environment.CurrentDirectory, pngfile))));
            
            this.account = account;
            this.name = name;
            textBlockUserId.Text = account+name;
            MainShow();
            HomeWorkShow();
        }
        //主界面生成
        public void MainShow()
        {
            Dictionary<int, List<string>> info = sts.showCourseInfo(this.account);
            
            for(int i = 0; i < info.Count; i++)
            {
                ListViewItem ivi = new ListViewItem();
               
                cls = info[i][2];
                MainInfo mf = new MainInfo(cls);
                mf.Name = "mf_" + i;
                mf.labelClassId1.Content = info[i][2];
                mf.textBlockClassName1.Text = info[i][3];
                mf.class1.Tag = cls;
                mf.class1.Click += new RoutedEventHandler(btnStuMainHomework_Click);
                mf.textBlockDepartmentName1.Text = info[i][1];
                mf.textBlockTeacherName1.Text = info[i][0];
                ivi.Content = mf;
                listView2.Items.Add(ivi);
            }
            
        }
        //主界面点击到达stuMainHomework界面的点击事件
        private void btnStuMainHomework_Click(object sender, RoutedEventArgs e)
        {
            Button mf = (Button)sender;
            StuMainHomework smh = new StuMainHomework(account, name, (String)mf.Tag,pngfile);//真实课堂号
            smh.pngfile = this.pngfile;
            smh.Show();
            this.Visibility = Visibility.Hidden;

        }
        //作业公告生成调用
        public void HomeWorkShow()
        {
            List<List<String>> result = sts.showHomeNoticeInfo(this.account);
            for(int i = 0; i < result.Count; i++) {
                ListViewItem ivi = new ListViewItem();
                //定义一个数组用来放东西
                String[] temp = new String[2];
                this.notId = result[i][1];
                this.cls = result[i][2];
                temp[0] = result[i][1];
                temp[1] = result[i][2];
                HomeworkNoteInfo hni = new HomeworkNoteInfo(notId,cls);
                hni.btnRecntNo1.Tag = temp;
                hni.btnRecntNo1.Click += new RoutedEventHandler(HomeworkNote_Click);
                hni.lab1.Content = result[i][0];
                ivi.Content = hni;
                listView.Items.Add(ivi);
                    }
        }
        
        //作业公告的按钮点击事件
       
        private void HomeworkNote_Click(object sender , RoutedEventArgs e)
        {
            //记录生成的是哪个动态控件
            Button hnif = (Button)sender;
            String[] info = (String[])hnif.Tag;
            StuDoHomework sdh = new StuDoHomework(account,name,info[0],info[1],pngfile);
            sdh.pngfile = this.pngfile;
            sdh.Show();
            this.Visibility = Visibility.Hidden;
        }
       
        private void BtnHomewordAlert_Click(object sender, RoutedEventArgs e)
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

        private void btnHomeworkMana_Click(object sender, RoutedEventArgs e)
        {

        }

       

      

      

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
