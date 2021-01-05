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
using System.Windows.Forms;
using HAMS.ToolClass;

namespace HAMS.Student.StudentView
{
    /// <summary>
    /// StuDoHomework.xaml 的交互逻辑
    /// </summary>
    public partial class StuDoHomework : Window
    {
        private SService ss = new SService();
        public string pngfile;
        public String account { set; get; }
        public String name { set; get; }
        public String notId { set; get; }
        public String classId { set; get; }
        public String message { set; get; }
        public StuDoHomework(String account, String name,String notId,String classSpecId,string pgfile)
        {
            InitializeComponent();
            this.account = account;
            this.name = name;
            this.notId = notId;
            this.classId = classSpecId;
            tbUserNameAc.Text = account + name;
            this.pngfile = pgfile;
            //设置该img控件的Source
            headImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(System.IO.Path.Combine(System.Environment.CurrentDirectory, pngfile))));
            this.message = ss.judgeHomeworkStatus(account, notId);
            if (message == "未完成")
            {
                btnDoHomework.Content = "作答";
            }
            else if (message == "待批改")
            {
                btnDoHomework.Content = "修改";
            }
            else if (message == "已批改")
            {
                btnDoHomework.Content = "查看";
            }
            else if (message == "已逾期")
            {
                System.Windows.Forms.MessageBox.Show("该作业已逾期，无法再进行作答");
                btnDoHomework.Content = "";
            }
            string homeworkName = ss.homeworkName(account, notId);
            if(homeworkName == "还未提交")
            {
                //如果不存在作业附件则不进行显示
                tbMyHomework.Visibility = Visibility.Collapsed;
                imgAccessory_Copy.Source = null;
            }
            else
            {
                tbMyHomework.Content = homeworkName;
            }
            doHomeworkInfoShow();
        }
        
        public StuDoHomework(String account, String name, String notId, String classId,string pgfile,String message)
        {
            InitializeComponent();
            this.account = account;
            this.name = name;
            this.notId = notId;
            this.classId = classId;
            this.pngfile = pgfile;
            //设置该img控件的Source
            headImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(System.IO.Path.Combine(System.Environment.CurrentDirectory, pngfile))));


            Dictionary<int, List<String>> info = ss.showAllHomeworkInfo(classId);
            this.message = message;
            if (message == "未完成")
            {
                btnDoHomework.Content = "作答";
            }
           else if (message == "待批改")
            {
                btnDoHomework.Content = "修改";
            }
           else if (message == "已批改")
            {
                btnDoHomework.Content = "查看";
            }
           else if (message == "已逾期")
            {
                btnDoHomework.Content = "";
            }
            tbUserNameAc.Text = account +" " +name;
            string homeworkName = ss.homeworkName(account, notId);
            if (homeworkName == "还未提交")
            {
                //如果不存在作业附件则不进行显示
                tbMyHomework.Visibility = Visibility.Collapsed;
                imgAccessory_Copy.Source = null;
                Myhomework.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbMyHomework.Content = homeworkName;
            }
            doHomeworkInfoShow();
        }
        
        public void doHomeworkInfoShow()
        {
            List<String> result = ss.showDohomeworkInfo(notId);
            List<String> content = new List<string>();
            labelHomeworkName.Content = result[0];
            string lbiContent = result[1];
            int notlength = 60;
            if (result[1].Length > notlength)
            {
                for (int i = 0; i < result[1].Length / notlength + 1 ;i++)
                {
                    
                    //ListBoxItem lbi = new ListBoxItem();
                    if (((i+1)* notlength) > result[1].Length)
                    {
                        content.Add(result[1].Substring(i* notlength, result[1].Length-1-i* notlength));
                    }
                    else
                    {
                        content.Add(result[1].Substring(i* notlength, notlength));
                    }
                }
                String text ="";
                int number =content.Count();
                for (int i=0; i<number;i++)
                {
                    text =text + content[i] + "\r\n";
                }
                textBoxRequest.Text = text.ToString();
            }
            else
            {
                content.Add(result[1].ToString());
                textBoxRequest.Text = content[0];
            }
            
            tbDeadLineTime.Text = result[2];
            if (result[3] != "")
            {
                tbAccessoryName.Content = result[3];
            }
            else
            {
               
                //如果不存在作业附件则不进行显示
                tbAccessoryName.Visibility = Visibility.Collapsed;
                //减掉某个具体的事件
                tbAccessoryName.Click -= new RoutedEventHandler(tbAccessoryName_Click);
                //tbAccessoryName.Content = "";
                imgAccessory.Source = null;
            }

        }
       
       
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StuMainHomework smh = new StuMainHomework(account,name,classId,pngfile);
                smh.pngfile = this.pngfile;
                smh.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void homeworkManagement_Click(object sender, RoutedEventArgs e)
        {
            if (true)//里面是验证函数
            {
                // 打开子窗体
                StudentMainForm smf= new StudentMainForm(account,name,pngfile);
                smf.pngfile = this.pngfile;
                smf.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void homeworkAlert_Click(object sender, RoutedEventArgs e)
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

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            headImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(System.IO.Path.Combine(System.Environment.CurrentDirectory, pngfile))));
            this.message = ss.judgeHomeworkStatus(account, notId);
            if (message == "未完成")
            {
                btnDoHomework.Content = "作答";
            }
            else if (message == "待批改")
            {
                btnDoHomework.Content = "修改";
            }
            else if (message == "已批改")
            {
                btnDoHomework.Content = "查看";
            }
            else if (message == "已逾期")
            {
                System.Windows.Forms.MessageBox.Show("该作业已逾期，无法再进行作答");
                btnDoHomework.Content = "";
            }
            doHomeworkInfoShow();
        }

        private void btnDoHomework_Click(object sender, RoutedEventArgs e)
        {
            string content = btnDoHomework.Content.ToString();
            //System.Windows.MessageBox.Show(content);
            if (content == "修改" || content== "作答")//里面是验证函数
            {
                // 打开子窗体
                StuSubmitHomework ssh = new StuSubmitHomework(account,name,notId,classId,pngfile,message);//真实课堂号
                ssh.pngfile = this.pngfile;
                ssh.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }
            if (content == "查看")//里面是验证函数
            {
                // 打开子窗体
                HomeworkSubmit hs = new HomeworkSubmit(account, name, notId, classId,pngfile);//真实课堂号
                hs.pngfile = this.pngfile;
                hs.Show();
                // 隐藏自己(父窗体)
                this.Visibility = System.Windows.Visibility.Hidden;
            }

        }
        private void tbAccessoryName_Click(object sender, RoutedEventArgs e)
        {
            string notTitle = ss.downloadLink(notId);
            

            SaveFileDialog sfd = new SaveFileDialog();
            //设置默认要保存文件的文件名（文件名.扩展名）
            sfd.FileName = tbAccessoryName.Content.ToString();//这个名字也是原本服务器上保存文件的文件名
            //初始化提示保存文件的路径地址,默认保存在D盘中
            sfd.InitialDirectory = @"D:\";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string errorinfo;
                //获取要保存文件名的本地完整路径
                string localpath = sfd.FileName;// System.IO.Path.GetFullPath(sfd.FileName);
                                                //调用下载文件函数，将教师作业公告附件从服务器上下载下来，其中localpath是本地路径,NotURL是数据库中存放的文件路径（文件在服务器上的路径）
                string NotURL = classId + "/" +notTitle +"/作业附件/"+ tbAccessoryName.Content.ToString();
                bool flag = FtpUpDown.download(localpath, NotURL, out errorinfo);

                if (flag == true)
                    System.Windows.MessageBox.Show("下载成功");
                else
                    System.Windows.MessageBox.Show("下载失败：" + errorinfo + "");
            }
        }
    }
}
